// Decompiled with JetBrains decompiler
// Type: KickReturnBlock
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class KickReturnBlock : BehaviorDesigner.Runtime.Tasks.Action
{
  private KickReturnBlock.KickReturnBlockState currentKickReturnBlockState;
  private KickReturnConfig kickReturnConfig;
  public SharedGameObject ownerPlayer;
  public SharedInt currentPlayAssignment;
  private KickRetBlockingAssignment assignment;
  private List<Vector3> actualRoutePoints;
  private Vector3 endGoalPosition;
  private int currentLeg;
  private PlayerAI ownerPlayerAI;
  private bool gotoChaseBall;

  public override void OnStart()
  {
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.assignment = this.ownerPlayerAI.CurrentPlayAssignment as KickRetBlockingAssignment;
    if (this.assignment.blockerType == EKickRetBlockerType.PuntJammer)
    {
      this.currentKickReturnBlockState = KickReturnBlock.KickReturnBlockState.Jamming;
      this.ownerPlayerAI.animatorCommunicator.Stop();
    }
    else
      this.currentKickReturnBlockState = KickReturnBlock.KickReturnBlockState.WaitingForKick;
    this.kickReturnConfig = global::Game.KickReturnConfig;
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayerAI || this.assignment == null || this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.KickReturnBlocker)
      return TaskStatus.Failure;
    if (global::Game.IsKickoff && global::Game.BallHolderIsNotNull && !(bool) ProEra.Game.MatchState.Turnover || global::Game.IsPunt && global::Game.BallIsThrownOrKicked && global::Game.BallHolderIsNotNull && !(bool) ProEra.Game.MatchState.Turnover)
      return TaskStatus.Success;
    if (this.ownerPlayerAI.isEngagedInBlock)
    {
      this.ownerPlayerAI.blockDuration -= this.ownerPlayerAI.AITimingInterval;
      this.ownerPlayerAI.inBlockWithScript.CheckForBlockDisengage();
      if ((double) this.ownerPlayerAI.blockDuration <= 0.0)
        this.ownerPlayerAI.PlayerBlockAbility.ExitBlock();
    }
    switch (this.currentKickReturnBlockState)
    {
      case KickReturnBlock.KickReturnBlockState.WaitingForKick:
        if (global::Game.BS_IsKick)
        {
          if (this.assignment.ShouldImmediatelyChaseBallAfterKick())
          {
            this.ownerPlayerAI.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
            return TaskStatus.Success;
          }
          this.StartRunningToSpot();
          this.currentKickReturnBlockState = KickReturnBlock.KickReturnBlockState.RunningToSpot;
          goto case KickReturnBlock.KickReturnBlockState.RunningToSpot;
        }
        else
          break;
      case KickReturnBlock.KickReturnBlockState.RunningToSpot:
        if (global::Game.BallIsThrownOrKicked && global::Game.BallHolderIsNotNull)
        {
          this.currentKickReturnBlockState = KickReturnBlock.KickReturnBlockState.Blocking;
          goto case KickReturnBlock.KickReturnBlockState.Blocking;
        }
        else
          break;
      case KickReturnBlock.KickReturnBlockState.WaitingForCatch:
        if (global::Game.BallHolderIsNotNull || !this.assignment.ShouldWaitForCatchBeforeBlocking())
        {
          this.currentKickReturnBlockState = KickReturnBlock.KickReturnBlockState.Blocking;
          goto case KickReturnBlock.KickReturnBlockState.Blocking;
        }
        else
        {
          if (!global::Game.BS_IsKick && this.UpdateKickReturnBlocking(this.kickReturnConfig.GetMaxDistanceFromTargetForUpBlockerToSwitchEarly(), false))
          {
            this.currentKickReturnBlockState = KickReturnBlock.KickReturnBlockState.Blocking;
            break;
          }
          break;
        }
      case KickReturnBlock.KickReturnBlockState.Blocking:
        this.UpdateKickReturnBlocking(this.kickReturnConfig.GetMaxDistanceFromBlockerToTarget(), true);
        break;
      case KickReturnBlock.KickReturnBlockState.Jamming:
        this.StartRunningToSpot();
        this.currentKickReturnBlockState = KickReturnBlock.KickReturnBlockState.RunningToSpot;
        goto case KickReturnBlock.KickReturnBlockState.RunningToSpot;
    }
    return TaskStatus.Running;
  }

  private void StartRunningToSpot()
  {
    if (this.assignment == null)
      return;
    float num1 = 1f;
    this.ownerPlayerAI.animatorCommunicator.isRunningRoute = false;
    List<RouteLeg> route = this.assignment.GetRoute();
    int num2 = route.Count - 1;
    this.actualRoutePoints = new List<Vector3>();
    for (int index = 0; index <= num2; ++index)
    {
      System.Action onGoalReached = (System.Action) null;
      RouteLeg routeLeg = route[index];
      routeLeg.moveVector *= (float) global::Game.OffensiveFieldDirection;
      if (global::Game.IsPunt)
        routeLeg.moveVector += this.ownerPlayerAI.trans.position;
      float num3 = num1 * routeLeg.speedPercent;
      float effortCeiling01 = Mathf.Clamp(num1 + num3, 0.3f, this.ownerPlayerAI.animatorCommunicator.maxEffort01);
      if (index == num2)
      {
        if (this.assignment.blockerType != EKickRetBlockerType.PuntJammer)
        {
          float depthOffsetDistance = this.kickReturnConfig.GetRandomDepthOffsetDistance(this.assignment.blockerType);
          routeLeg.moveVector.z += depthOffsetDistance;
          float f = MatchManager.instance.playersManager.ballLandingSpot.position.z + this.kickReturnConfig.GetMinDistanceInFrontOfLandingSpot() * (float) -global::Game.OffensiveFieldDirection;
          if ((double) Mathf.Abs(routeLeg.moveVector.z) > (double) Mathf.Abs(f))
            routeLeg.moveVector.z = f;
        }
        onGoalReached = new System.Action(this.HandleRouteEnd);
      }
      if (global::Game.IsPunt)
        this.ownerPlayerAI.animatorCommunicator.AddGoalToQueue(routeLeg.moveVector, effortCeiling01, Field.OFFENSE_TOWARDS_LOS_QUATERNION, onGoalReached);
      else
        this.ownerPlayerAI.animatorCommunicator.AddGoalToQueue(routeLeg.moveVector, effortCeiling01, Field.DEFENSE_TOWARDS_LOS_QUATERNION, onGoalReached);
      this.actualRoutePoints.Add(routeLeg.moveVector);
    }
    this.endGoalPosition = this.actualRoutePoints[this.actualRoutePoints.Count - 1];
    this.DebugDrawRoute();
  }

  private bool UpdateKickReturnBlocking(float maxDistanceToTarget, bool runIfNoTargetFound)
  {
    PlayerAI blockTarget = this.FindBlockTarget(maxDistanceToTarget);
    if ((UnityEngine.Object) blockTarget != (UnityEngine.Object) null)
      this.ownerPlayerAI.animatorCommunicator.SetGoalDirection(this.ownerPlayerAI.GetPlayerPursuitAngle(blockTarget) - this.ownerPlayerAI.trans.position);
    else if (runIfNoTargetFound)
      this.ownerPlayerAI.animatorCommunicator.SetGoalDirection(Vector3.forward * (float) -global::Game.OffensiveFieldDirection);
    return (UnityEngine.Object) blockTarget != (UnityEngine.Object) null;
  }

  private PlayerAI FindBlockTarget(float maxDistanceFromTarget)
  {
    PlayerAI blockTarget = (PlayerAI) null;
    float num1 = maxDistanceFromTarget;
    double fromBlockerToTarget = (double) this.kickReturnConfig.GetMaxDistanceFromBlockerToTarget();
    foreach (PlayerAI offensivePlayer in global::Game.OffensivePlayers)
    {
      if (!offensivePlayer.isEngagedInBlock && !((UnityEngine.Object) offensivePlayer.mainGO == (UnityEngine.Object) this.ownerPlayerAI.inBlockWith) && offensivePlayer.tackleType == 0)
      {
        float num2 = Vector3.Distance(this.ownerPlayerAI.transform.position, offensivePlayer.transform.position);
        if ((double) num2 < (double) num1 && (double) Vector3.Angle(this.ownerPlayerAI.transform.forward, offensivePlayer.transform.position - this.ownerPlayerAI.transform.position) <= (double) this.kickReturnConfig.minAngleDiffFromForwardToBlockTargetDegrees && (!((UnityEngine.Object) offensivePlayer.blockerAssignedToThisDefender != (UnityEngine.Object) null) || (double) num2 <= (double) offensivePlayer.distanceToBlocker))
        {
          blockTarget = offensivePlayer;
          num1 = num2;
        }
      }
    }
    this.ownerPlayerAI.blockType = BlockType.MoveToBallHolder;
    this.ownerPlayerAI.blockTarget = blockTarget;
    if ((UnityEngine.Object) blockTarget != (UnityEngine.Object) null)
    {
      blockTarget.distanceToBlocker = num1;
      blockTarget.blockerAssignedToThisDefender = this.ownerPlayerAI;
    }
    return blockTarget;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    if (!((UnityEngine.Object) this.ownerPlayerAI != (UnityEngine.Object) null))
      return;
    this.ownerPlayerAI.EndCurrentAssignment();
  }

  private void DebugDrawRoute()
  {
    Vector3 start = this.ownerPlayerAI.trans.position;
    for (int index = 0; index < this.actualRoutePoints.Count; ++index)
    {
      Vector3 actualRoutePoint = this.actualRoutePoints[index];
      Color white = Color.white;
      Debug.DrawLine(start, actualRoutePoint, white, 8f);
      start = actualRoutePoint;
    }
  }

  private void HandleRouteEnd()
  {
    if (this.currentKickReturnBlockState != KickReturnBlock.KickReturnBlockState.RunningToSpot)
      return;
    this.currentKickReturnBlockState = KickReturnBlock.KickReturnBlockState.WaitingForCatch;
  }

  public enum KickReturnBlockState
  {
    WaitingForKick,
    RunningToSpot,
    WaitingForCatch,
    Blocking,
    Jamming,
  }
}
