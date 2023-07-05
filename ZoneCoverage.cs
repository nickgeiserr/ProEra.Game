// Decompiled with JetBrains decompiler
// Type: ZoneCoverage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZoneCoverage : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedInt currentPlayAssignment;
  private ZoneDefenseAssignment playAssign;
  private PlayerAI ownerPlayerAI;
  private ZoneCoverage.ReceiverInfo targetZoneReceiver;
  private ZoneCoverage.ReceiverInfo closestRecToZone;
  private List<ZoneCoverage.ReceiverInfo> receiverInfoList;
  private Vector3 targetPosition;
  private Quaternion targetRotation;
  private float targetUpdateTime;
  private PlayersManager playersManager;
  private MatchManager matchManager;
  private AnimatorCommunicator animCom;
  private List<ZoneCoverage.ReceiverInfo> receiversInZone;
  private ZoneCoverageConfig zoneCoverageConfig;
  private bool handOffDetected;
  private bool throwDetected;
  private const float SAME_LEVEL_THRESHOLD = 1.8288f;

  public override void OnStart()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.playAssign = this.ownerPlayerAI.GetCurrentAssignment() as ZoneDefenseAssignment;
    this.targetUpdateTime = 0.0f;
    this.matchManager = MatchManager.instance;
    this.playersManager = this.matchManager.playersManager;
    this.receiverInfoList = new List<ZoneCoverage.ReceiverInfo>(this.playersManager.Offense.Count);
    this.animCom = this.ownerPlayerAI.animatorCommunicator;
    this.animCom.SetLocomotionStyle(ELocomotionStyle.DefaultStrafe);
    this.receiversInZone = new List<ZoneCoverage.ReceiverInfo>();
    this.closestRecToZone = (ZoneCoverage.ReceiverInfo) null;
    this.handOffDetected = false;
    this.throwDetected = false;
    this.zoneCoverageConfig = Game.ZoneCoverageConfig;
    PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);
  }

  public override TaskStatus OnUpdate()
  {
    PlayerAI offensiveQb = Game.OffensiveQB;
    bool flag1 = false;
    if (!(bool) (UnityEngine.Object) this.ownerPlayerAI || this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.ZoneCoverage || Game.IsTurnover && !this.ownerPlayerAI.onOffense)
      return TaskStatus.Failure;
    if (this.playAssign.ShouldChargeQBIfOutsidePocket() && (UnityEngine.Object) offensiveQb != (UnityEngine.Object) null && !PlayerAI.IsQBBetweenTackles())
    {
      if ((double) Game.OffensiveQB.trans.position.x * (double) this.ownerPlayerAI.trans.position.x > 0.0)
        this.handOffDetected = true;
      else
        flag1 = true;
    }
    if (this.handOffDetected)
    {
      this.ownerPlayerAI.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
      return TaskStatus.Success;
    }
    if (this.throwDetected)
    {
      this.ownerPlayerAI.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.MoveToDefendPass, (RouteGraphicData) null, (float[]) null), true);
      return TaskStatus.Success;
    }
    if (this.ownerPlayerAI.isEngagedInBlock || this.ownerPlayerAI.isTackling)
      return TaskStatus.Success;
    this.targetUpdateTime -= this.ownerPlayerAI.AITimingInterval;
    Vector3 position = this.ownerPlayerAI.trans.position;
    if ((double) this.targetUpdateTime < 0.0)
    {
      this.targetUpdateTime = this.zoneCoverageConfig.UpdateIntervalSeconds;
      this.GatherReceiverInfo();
      this.targetZoneReceiver = this.FindReceiverInZone();
      this.closestRecToZone = this.FindClosestReceiverToZone(this.receiversInZone);
      this.animCom.SetLocomotionStyle(ELocomotionStyle.DefaultStrafe);
      bool flag2 = false;
      this.targetRotation = Field.DEFENSE_TOWARDS_LOS_QUATERNION;
      if ((UnityEngine.Object) this.playersManager.ballHolderScript != (UnityEngine.Object) null)
        this.targetRotation = Quaternion.LookRotation(this.playersManager.ballHolderScript.trans.position - position);
      float effortCeiling01 = 1f;
      this.targetPosition = this.playAssign.homePos;
      if (this.targetZoneReceiver != null)
      {
        Vector3 predictedPosition = this.targetZoneReceiver.predictedPosition;
        predictedPosition.z += this.zoneCoverageConfig.DownfieldDistanceFromTargetPosition * (float) Game.OffensiveFieldDirection;
        Vector3 vector3 = (double) this.playAssign.homePos.x <= 0.0 ? predictedPosition + Vector3.right * this.zoneCoverageConfig.XOffsetFromReceiver * (float) Game.OffensiveFieldDirection : predictedPosition + Vector3.left * this.zoneCoverageConfig.XOffsetFromReceiver * (float) Game.OffensiveFieldDirection;
        if (this.playAssign.ShouldTurnAndChaseReceiver() & Field.FurtherDownfield(this.targetZoneReceiver.predictedPosition.z, position.z) & Field.FurtherDownfield(Field.OFFENSIVE_GOAL_LINE, position.z))
          flag2 = true;
        this.targetPosition = vector3;
      }
      if (flag1)
      {
        float x = offensiveQb.trans.position.x;
        this.targetPosition.x = !Field.MoreRightOf(x, this.playAssign.right) ? (!Field.MoreLeftOf(x, this.playAssign.left) ? x : this.playAssign.left) : this.playAssign.right;
      }
      if (flag2)
      {
        this.animCom.SetLocomotionStyle(ELocomotionStyle.Regular);
        this.targetRotation = Quaternion.LookRotation(this.targetPosition - position);
      }
      if (this.playAssign.ShouldStayDeeperThanDeepestReceiver())
      {
        ZoneCoverage.ReceiverInfo deepestReceiver = this.FindDeepestReceiver(this.receiverInfoList);
        if (deepestReceiver != null)
          this.targetPosition.z = Field.MostDownfield(this.targetPosition.z, deepestReceiver.predictedPosition.z + this.zoneCoverageConfig.DownfieldDistanceFromTargetPosition * (float) Game.OffensiveFieldDirection);
      }
      this.targetPosition.z = Field.LeastDownfield(this.targetPosition.z, this.playAssign.GetMostDownfieldCoveragePosition());
      this.animCom.SetGoal(this.targetPosition, effortCeiling01, this.targetRotation);
    }
    return TaskStatus.Running;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    if (!((UnityEngine.Object) this.ownerPlayerAI != (UnityEngine.Object) null))
      return;
    this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.Regular);
    PEGameplayEventManager.OnEventOccurred -= new Action<PEGameplayEvent>(this.HandleGameEvent);
    if (this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.ZoneCoverage)
      return;
    this.ownerPlayerAI.EndCurrentAssignment();
  }

  private void GatherReceiverInfo()
  {
    this.receiverInfoList.Clear();
    for (int index = 6; index < this.playersManager.Offense.Count; ++index)
      this.receiverInfoList.Add(new ZoneCoverage.ReceiverInfo(this.playersManager.Offense[index], this.zoneCoverageConfig.ReceiverPredictionTimeInSeconds, this.playAssign));
  }

  private ZoneCoverage.ReceiverInfo FindReceiverInZone()
  {
    List<PlayerAI> offense = this.playersManager.Offense;
    this.receiversInZone = this.receiverInfoList.Where<ZoneCoverage.ReceiverInfo>((Func<ZoneCoverage.ReceiverInfo, bool>) (x => x.IsInZone)).ToList<ZoneCoverage.ReceiverInfo>();
    if (this.receiversInZone.Count == 0)
      return (ZoneCoverage.ReceiverInfo) null;
    if (this.receiversInZone.Count == 1)
      return this.receiversInZone[0];
    if (this.playAssign.GetPrimaryZoneCoveragePrecedence() == ZoneCoveragePrecedence.High)
      this.EliminateReceivers_ZPosition(this.receiversInZone, this.FindDeepestReceiverInZone(this.receiversInZone));
    else if (this.playAssign.GetPrimaryZoneCoveragePrecedence() == ZoneCoveragePrecedence.Low)
      this.EliminateReceivers_ZPosition(this.receiversInZone, this.FindLowestReceiverInZone(this.receiversInZone));
    else if (this.playAssign.GetPrimaryZoneCoveragePrecedence() == ZoneCoveragePrecedence.Inside)
      this.EliminateReceivers_XPosition(this.receiversInZone, this.FindInsideReceiverInZone(this.receiversInZone));
    else if (this.playAssign.GetPrimaryZoneCoveragePrecedence() == ZoneCoveragePrecedence.Outside)
      this.EliminateReceivers_XPosition(this.receiversInZone, this.FindOutsideReceiverInZone(this.receiversInZone));
    else
      this.EliminateReceivers_Distance(this.receiversInZone, this.FindClosestReceiverInZone(this.receiversInZone));
    if (this.receiversInZone.Count == 1)
      return this.receiversInZone[0];
    if (this.playAssign.GetSecondaryZoneCoveragePrecedence() == ZoneCoveragePrecedence.High)
      return this.FindDeepestReceiverInZone(this.receiversInZone);
    if (this.playAssign.GetSecondaryZoneCoveragePrecedence() == ZoneCoveragePrecedence.Low)
      return this.FindLowestReceiverInZone(this.receiversInZone);
    if (this.playAssign.GetSecondaryZoneCoveragePrecedence() == ZoneCoveragePrecedence.Inside)
      return this.FindInsideReceiverInZone(this.receiversInZone);
    return this.playAssign.GetSecondaryZoneCoveragePrecedence() == ZoneCoveragePrecedence.Outside ? this.FindOutsideReceiverInZone(this.receiversInZone) : this.FindClosestReceiverInZone(this.receiversInZone);
  }

  private ZoneCoverage.ReceiverInfo FindDeepestReceiver(List<ZoneCoverage.ReceiverInfo> receivers)
  {
    if (receivers.Count == 0)
      return (ZoneCoverage.ReceiverInfo) null;
    ZoneCoverage.ReceiverInfo receiver = receivers[0];
    for (int index = 1; index < receivers.Count; ++index)
    {
      if (Field.FurtherDownfield(receivers[index].predictedPosition.z, receiver.predictedPosition.z))
        receiver = receivers[index];
    }
    return receiver;
  }

  private ZoneCoverage.ReceiverInfo FindDeepestReceiverInZone(
    List<ZoneCoverage.ReceiverInfo> receiversInZone)
  {
    ZoneCoverage.ReceiverInfo deepestReceiverInZone = (ZoneCoverage.ReceiverInfo) null;
    float secondObjectZPos = 0.0f;
    for (int index = 0; index < receiversInZone.Count; ++index)
    {
      float z = receiversInZone[index].predictedPosition.z;
      if (deepestReceiverInZone == null || Field.FurtherDownfield(z, secondObjectZPos))
      {
        deepestReceiverInZone = receiversInZone[index];
        secondObjectZPos = z;
      }
    }
    return deepestReceiverInZone;
  }

  private ZoneCoverage.ReceiverInfo FindLowestReceiverInZone(
    List<ZoneCoverage.ReceiverInfo> receiversInZone)
  {
    ZoneCoverage.ReceiverInfo lowestReceiverInZone = (ZoneCoverage.ReceiverInfo) null;
    float firstObjectZPos = 100f;
    for (int index = 0; index < receiversInZone.Count; ++index)
    {
      float z = receiversInZone[index].predictedPosition.z;
      if (lowestReceiverInZone == null || Field.FurtherDownfield(firstObjectZPos, z))
      {
        lowestReceiverInZone = receiversInZone[index];
        firstObjectZPos = z;
      }
    }
    return lowestReceiverInZone;
  }

  private ZoneCoverage.ReceiverInfo FindInsideReceiverInZone(
    List<ZoneCoverage.ReceiverInfo> receiversInZone)
  {
    ZoneCoverage.ReceiverInfo insideReceiverInZone = (ZoneCoverage.ReceiverInfo) null;
    float num1 = 100f;
    for (int index = 0; index < receiversInZone.Count; ++index)
    {
      float num2 = Mathf.Abs(receiversInZone[index].predictedPosition.x);
      if (insideReceiverInZone == null || (double) num2 < (double) num1)
      {
        insideReceiverInZone = receiversInZone[index];
        num1 = num2;
      }
    }
    return insideReceiverInZone;
  }

  private ZoneCoverage.ReceiverInfo FindOutsideReceiverInZone(
    List<ZoneCoverage.ReceiverInfo> receiversInZone)
  {
    ZoneCoverage.ReceiverInfo outsideReceiverInZone = (ZoneCoverage.ReceiverInfo) null;
    float num1 = 0.0f;
    for (int index = 0; index < receiversInZone.Count; ++index)
    {
      float num2 = Mathf.Abs(receiversInZone[index].predictedPosition.x);
      if (outsideReceiverInZone == null || (double) num2 > (double) num1)
      {
        outsideReceiverInZone = receiversInZone[index];
        num1 = num2;
      }
    }
    return outsideReceiverInZone;
  }

  private ZoneCoverage.ReceiverInfo FindClosestReceiverInZone(
    List<ZoneCoverage.ReceiverInfo> receiversInZone)
  {
    ZoneCoverage.ReceiverInfo closestReceiverInZone = (ZoneCoverage.ReceiverInfo) null;
    float num1 = 100f;
    for (int index = 0; index < receiversInZone.Count; ++index)
    {
      Vector3 predictedPosition = receiversInZone[index].predictedPosition;
      if (this.playAssign.IsReceiverInDefendersZone(predictedPosition))
      {
        float num2 = Vector3.Distance(predictedPosition, this.ownerPlayerAI.trans.position);
        if (closestReceiverInZone == null || (double) num2 < (double) num1)
        {
          closestReceiverInZone = receiversInZone[index];
          num1 = num2;
        }
      }
    }
    return closestReceiverInZone;
  }

  private void EliminateReceivers_ZPosition(
    List<ZoneCoverage.ReceiverInfo> receiversInZone,
    ZoneCoverage.ReceiverInfo referenceReceiver)
  {
    for (int index = receiversInZone.Count - 1; index >= 0; --index)
    {
      if ((double) Mathf.Abs(receiversInZone[index].predictedPosition.z - referenceReceiver.predictedPosition.z) > 1.8287999629974365)
        receiversInZone.RemoveAt(index);
    }
  }

  private void EliminateReceivers_XPosition(
    List<ZoneCoverage.ReceiverInfo> receiversInZone,
    ZoneCoverage.ReceiverInfo referenceReceiver)
  {
    for (int index = receiversInZone.Count - 1; index >= 0; --index)
    {
      if ((double) Mathf.Abs(receiversInZone[index].predictedPosition.x - referenceReceiver.predictedPosition.x) > 1.8287999629974365)
        receiversInZone.RemoveAt(index);
    }
  }

  private void EliminateReceivers_Distance(
    List<ZoneCoverage.ReceiverInfo> receiversInZone,
    ZoneCoverage.ReceiverInfo referenceReceiver)
  {
    float num = Vector3.Distance(this.ownerPlayerAI.trans.position, referenceReceiver.predictedPosition) + 1.8288f;
    for (int index = receiversInZone.Count - 1; index >= 0; --index)
    {
      if ((double) Vector3.Distance(this.ownerPlayerAI.trans.position, receiversInZone[index].predictedPosition) > (double) num)
        receiversInZone.RemoveAt(index);
    }
  }

  private bool IsAnotherDefenderCoveringReceiver(
    int defenderIndexToIgnore,
    ZoneCoverage.ReceiverInfo recInfo)
  {
    List<PlayerAI> playerAiList = !this.ownerPlayerAI.onUserTeam ? this.playersManager.curCompScriptRef : this.playersManager.curUserScriptRef;
    for (int index = 0; index < playerAiList.Count; ++index)
    {
      if (index != defenderIndexToIgnore && (playerAiList[index].GetCurrentAssignment() is ManDefenseAssignment currentAssignment ? (UnityEngine.Object) currentAssignment.GetCoverageOn() : (UnityEngine.Object) null) == (UnityEngine.Object) recInfo.receiver)
        return true;
    }
    return false;
  }

  private ZoneCoverage.ReceiverInfo FindClosestReceiverToZone(
    List<ZoneCoverage.ReceiverInfo> receiversInZone)
  {
    ZoneCoverage.ReceiverInfo closestReceiverToZone = (ZoneCoverage.ReceiverInfo) null;
    for (int index = 0; index < this.receiverInfoList.Count; ++index)
    {
      ZoneCoverage.ReceiverInfo receiverInfo = this.receiverInfoList[index];
      if (!receiverInfo.IsInZone && (closestReceiverToZone == null || (double) receiverInfo.distanceToZone < (double) closestReceiverToZone.distanceToZone))
        closestReceiverToZone = receiverInfo;
    }
    return closestReceiverToZone;
  }

  private void HandleGameEvent(PEGameplayEvent e)
  {
    if (!((UnityEngine.Object) this.ownerPlayerAI != (UnityEngine.Object) null))
      return;
    switch (e)
    {
      case PEHandoffAbortedEvent _:
        this.StartCoroutine(this.DelayHandoffDetection(0.0f));
        break;
      case PEBallHandoffEvent _:
      case PEQBRunningEvent _:
        this.StartCoroutine(this.DelayHandoffDetection(PlayerAI.CalcHandoffDetectionReactionDelay(this.ownerPlayerAI)));
        break;
      case PEBallThrownEvent _:
        this.StartCoroutine(this.DelayPassThrowDetection(PlayerAI.CalcBallThrowDetection(this.ownerPlayerAI, this.playersManager.passDestination)));
        break;
    }
  }

  private IEnumerator DelayHandoffDetection(float delay)
  {
    yield return (object) new WaitForSeconds(delay);
    this.handOffDetected = true;
  }

  private IEnumerator DelayPassThrowDetection(float delay)
  {
    yield return (object) new WaitForSeconds(delay);
    this.throwDetected = true;
  }

  public override void OnDrawGizmos()
  {
  }

  private class ReceiverInfo
  {
    public PlayerAI receiver;
    public Vector3 predictedPosition;
    public float distanceToZone;
    private ZoneDefenseAssignment _zoneDefenseAssignment;

    public bool IsInZone => (double) this.distanceToZone <= 0.0;

    public ReceiverInfo(
      PlayerAI receiver,
      float predictionTime,
      ZoneDefenseAssignment zoneDefenseAssignment)
    {
      this.receiver = receiver;
      this.predictedPosition = PlayerPrediction.PredictPosition(receiver, predictionTime);
      this._zoneDefenseAssignment = zoneDefenseAssignment;
      this.distanceToZone = this.GetDistanceToZone(this.predictedPosition);
    }

    private float GetDistanceToZone(Vector3 testPosition)
    {
      float num1 = 0.0f;
      if (Field.MoreLeftOf(testPosition.x, this._zoneDefenseAssignment.left))
        num1 = Mathf.Abs(this._zoneDefenseAssignment.left - testPosition.x);
      else if (Field.MoreRightOf(testPosition.x, this._zoneDefenseAssignment.right))
        num1 = Mathf.Abs(this._zoneDefenseAssignment.right - testPosition.x);
      float num2 = 0.0f;
      if (Field.FurtherDownfield(testPosition.z, this._zoneDefenseAssignment.top))
        num2 = Mathf.Abs(this._zoneDefenseAssignment.top - testPosition.z);
      else if (Field.FurtherDownfield(this._zoneDefenseAssignment.bottom, testPosition.z))
        num2 = Mathf.Abs(this._zoneDefenseAssignment.bottom - testPosition.z);
      return Mathf.Sqrt((float) ((double) num1 * (double) num1 + (double) num2 * (double) num2));
    }
  }
}
