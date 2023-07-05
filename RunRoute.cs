// Decompiled with JetBrains decompiler
// Type: RunRoute
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class RunRoute : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedVector3 targetPosition;
  public SharedBool atTargetPos;
  public SharedInt currentPlayAssignment;
  private RunRouteAssignment playerRoute;
  private List<Vector3> actualRoutePoints;
  private int currentLeg;
  private PlayerAI ownerPlayerAI;
  private Color distColor = Color.white;
  private PlayersManager pm;
  private AnimatorCommunicator animCom;
  private float lastPassSpeedAdjustment;
  private float passSpeedUpdateTime;
  private bool hasCatchPoint;
  private const float PASSSPEEDUPDATEDURATION = 0.5f;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.playerRoute = this.ownerPlayerAI.CurrentPlayAssignment as RunRouteAssignment;
    this.actualRoutePoints = new List<Vector3>();
    if (this.playerRoute != null)
    {
      this.ownerPlayerAI.animatorCommunicator.isRunningRoute = true;
      this.atTargetPos.Value = false;
      this.ownerPlayerAI.SetPlayerRouteForAssignment((RunPathAssignment) this.playerRoute, this.actualRoutePoints, 1f);
      this.currentLeg = 0;
    }
    this.passSpeedUpdateTime = 0.0f;
    this.lastPassSpeedAdjustment = 0.0f;
    this.pm = MatchManager.instance.playersManager;
    this.animCom = this.ownerPlayerAI.animatorCommunicator;
    this.hasCatchPoint = false;
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI || this.playerRoute == null || this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.RunRoute || Game.IsTurnover || Game.BS_IsInAirDeflected || Game.BS_IsOnGround)
      return TaskStatus.Failure;
    if (this.animCom.GoalReached)
      ++this.currentLeg;
    if (this.currentLeg > this.actualRoutePoints.Count)
    {
      this.atTargetPos.Value = true;
      return TaskStatus.Success;
    }
    this.pm = MatchManager.instance.playersManager;
    if (Game.IsPass && this.pm.ballWasThrownOrKicked && (Object) this.pm.intendedReceiver == (Object) this.ownerPlayerAI && Game.BS_IsInAirPass)
    {
      if (!this.hasCatchPoint)
      {
        this.hasCatchPoint = true;
        this.ownerPlayerAI.GotoCatchPos(this.pm.passDestination);
      }
      this.passSpeedUpdateTime -= this.ownerPlayerAI.AITimingInterval;
      if ((double) this.passSpeedUpdateTime < 0.0)
      {
        this.passSpeedUpdateTime = 0.5f;
        this.ownerPlayerAI.CalculateReceiverAnimSpeedAdjustmentForPass();
        if ((double) Mathf.Abs(this.lastPassSpeedAdjustment - this.ownerPlayerAI.currentAdjustedAnimSpeedForPass) > 0.10000000149011612)
        {
          this.animCom.CurrentGoal.enforceEffort = true;
          this.animCom.CurrentGoal.effortCeiling01 = this.ownerPlayerAI.currentAdjustedAnimSpeedForPass;
        }
      }
      if (this.ownerPlayerAI.eventAgent.IsInsideEvent)
        this.animCom.SetGoalDirection(AIUtil.GetBestAngleToEZ(this.ownerPlayerAI), 0.8f);
    }
    return TaskStatus.Running;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    if (!((Object) this.ownerPlayerAI != (Object) null))
      return;
    this.ownerPlayerAI.animatorCommunicator.isRunningRoute = false;
    this.ownerPlayerAI.EndCurrentAssignment();
  }

  private RouteLeg GetCurrentLeg() => this.playerRoute.GetRoute()[this.currentLeg];

  public override void OnDrawGizmos()
  {
  }
}
