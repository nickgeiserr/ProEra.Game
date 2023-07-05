// Decompiled with JetBrains decompiler
// Type: OffPlayInitiation
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class OffPlayInitiation : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  private PlayerAI ownerPlayerAI;
  public SharedInt currentPlayAssignment;
  private EPlayInitRole playerRole;
  private PlayersManager playersMan;
  private MatchManager matchManager;
  private PlayManager playMan;
  private Transform trans;
  private AnimatorCommunicator locoAgent;
  private Vector3 runTargetPos;
  private List<Vector3> actualRoutePoints;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.matchManager = MatchManager.instance;
    this.playersMan = this.matchManager.playersManager;
    this.playMan = MatchManager.instance.playManager;
    this.locoAgent = this.ownerPlayerAI.animatorCommunicator;
    this.trans = this.ownerPlayerAI.trans;
    this.runTargetPos = Vector3.zero;
    this.actualRoutePoints = new List<Vector3>();
    this.locoAgent.SetGoal(this.trans.position);
    if (global::Game.IsFG && (Object) global::Game.Kicker == (Object) this.ownerPlayerAI)
      this.playerRole = EPlayInitRole.FGKicker;
    else if (global::Game.IsKickoff && (Object) global::Game.Kicker == (Object) this.ownerPlayerAI)
    {
      this.playerRole = EPlayInitRole.KickoffKicker;
      if (!(bool) ProEra.Game.MatchState.IsPlayInitiated)
        this.CheckForPlayInitiation();
    }
    else if ((global::Game.IsRun || global::Game.IsPlayAction) && !global::Game.IsQBKeeper && (Object) this.playMan.handOffTarget == (Object) this.ownerPlayerAI)
      this.playerRole = EPlayInitRole.HandoffReceiver;
    else if (!global::Game.IsKickoff && (Object) global::Game.OffensiveCenter == (Object) this.ownerPlayerAI)
    {
      this.playerRole = EPlayInitRole.Snapper;
      this.playersMan.DisableCenterBlockingForPlayInit();
      this.locoAgent.SetStance(14);
      this.locoAgent.SetLocomotionStyle(ELocomotionStyle.BlockingStrafe);
      this.locoAgent.SetGoalDirection(Field.DEFENSE_TOWARDS_LOS_QUATERNION * Vector3.forward);
    }
    else if (global::Game.IsPunt && (Object) global::Game.Punter == (Object) this.ownerPlayerAI)
    {
      this.playerRole = EPlayInitRole.SnapReceiver;
      if (!(bool) ProEra.Game.MatchState.IsPlayInitiated)
        this.CheckForPlayInitiation();
    }
    else if (global::Game.IsFG && (Object) global::Game.Holder == (Object) this.ownerPlayerAI)
    {
      this.playerRole = EPlayInitRole.SnapReceiver;
      if (!(bool) ProEra.Game.MatchState.IsPlayInitiated)
        this.CheckForPlayInitiation();
    }
    else if ((Object) global::Game.OffensiveQB == (Object) this.ownerPlayerAI)
    {
      this.playerRole = EPlayInitRole.SnapReceiver;
      if (!(bool) ProEra.Game.MatchState.IsPlayInitiated)
        this.CheckForPlayInitiation();
      this.locoAgent.SetLocomotionStyle(ELocomotionStyle.QuaterbackStrafe, this.ownerPlayerAI.LeftHanded);
      this.locoAgent.SetGoalDirection(Field.DEFENSE_TOWARDS_LOS_QUATERNION * Vector3.forward);
    }
    else
    {
      this.playerRole = EPlayInitRole.None;
      Debug.LogError((object) " Invalid player starting OffPlayInitition behavior -- role set to NONE. Initiation will fail");
    }
    if (this.playerRole != EPlayInitRole.HandoffReceiver && !this.ownerPlayerAI.HasAssignmentInStack(EPlayAssignmentId.RunToEndZone))
      return;
    this.SetLocomotionGoalForAfterPlayInit();
  }

  public void SetLocomotionGoalForAfterPlayInit()
  {
    RunPathAssignment assignment = new RunPathAssignment(EPlayAssignmentId.RunPath, (RouteGraphicData) null, this.ownerPlayerAI.GetCurrentAssignment().GetRoutePoints());
    this.actualRoutePoints = new List<Vector3>();
    bool flipSide = (Object) this.ownerPlayerAI != (Object) global::Game.OffensiveQB;
    this.ownerPlayerAI.SetPlayerRouteForAssignment(assignment, this.actualRoutePoints, 1f, Quaternion.identity, flipSide);
    if (this.actualRoutePoints.Count > 0)
    {
      this.ownerPlayerAI.animatorCommunicator.AddGoalToQueue(this.actualRoutePoints[this.actualRoutePoints.Count - 1] + Vector3.forward * Field.TEN_YARDS * (float) global::Game.OffensiveFieldDirection);
      this.DebugDrawRoute();
    }
    this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.Regular);
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI || this.playerRole == EPlayInitRole.None || !global::Game.IsPlayActive)
      return TaskStatus.Failure;
    switch (this.playerRole)
    {
      case EPlayInitRole.Snapper:
        if ((bool) ProEra.Game.MatchState.IsPlayInitiated && !this.ownerPlayerAI.nteractAgent.IsInsideInteraction)
        {
          this.playersMan.EnableCenterBlockingForPlayInit();
          return TaskStatus.Success;
        }
        break;
      case EPlayInitRole.SnapReceiver:
        if (!(bool) ProEra.Game.MatchState.IsPlayInitiated)
        {
          this.CheckForPlayInitiation();
          break;
        }
        if ((!global::Game.IsPlayerOneOnOffense || !global::Game.UserControlsQB || !global::Game.CurrentPlayHasUserQBOnField) && (global::Game.BS_IsInAirSnap || global::Game.BS_InCentersHandsBeforeSnap) && ((Object) this.playersMan.GetBallHolder() == (Object) null || (Object) this.playersMan.GetBallHolder() == (Object) global::Game.OffensiveCenter))
        {
          this.playersMan.SetBallHolder(this.ownerPlayerAI.mainGO, this.ownerPlayerAI.onUserTeam);
          break;
        }
        if (!this.ownerPlayerAI.nteractAgent.IsInsideInteraction)
          return TaskStatus.Success;
        break;
      case EPlayInitRole.HandoffReceiver:
        if ((bool) ProEra.Game.MatchState.IsPlayInitiated && !this.ownerPlayerAI.nteractAgent.IsInsideInteraction)
          return TaskStatus.Success;
        break;
      case EPlayInitRole.FGKicker:
        if ((bool) ProEra.Game.MatchState.IsPlayInitiated && !this.ownerPlayerAI.nteractAgent.IsInsideInteraction)
          return TaskStatus.Success;
        break;
      case EPlayInitRole.KickoffKicker:
        if ((bool) ProEra.Game.MatchState.IsPlayInitiated && !this.ownerPlayerAI.eventAgent.IsInsideEvent)
          return TaskStatus.Success;
        break;
      default:
        Debug.LogError((object) "Invalid player role in OffPLayInitiation task");
        break;
    }
    return TaskStatus.Running;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    if (!((Object) this.ownerPlayerAI != (Object) null) || this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.OffPlayInitiation)
      return;
    if ((Object) this.ownerPlayerAI == (Object) this.playersMan.ballHolderScript)
    {
      if (this.ownerPlayerAI.IsQB() && global::Game.IsPass)
        this.ownerPlayerAI.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.QBLookToPass, (RouteGraphicData) null, (float[]) null), true);
      else
        this.ownerPlayerAI.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.RunToEndZone, (RouteGraphicData) null, (float[]) null), true);
    }
    else
    {
      if (this.playerRole == EPlayInitRole.HandoffReceiver && global::Game.IsRun)
      {
        this.ownerPlayerAI.ClearPath();
        this.ownerPlayerAI.animatorCommunicator.SetGoalDirection(this.ownerPlayerAI.transform.forward);
        this.ownerPlayerAI.lookForBlockTarget = true;
        this.ownerPlayerAI.blockType = BlockType.MoveToBallHolder;
      }
      this.ownerPlayerAI.EndCurrentAssignment();
    }
  }

  private void CheckForPlayInitiation()
  {
    if (!global::Game.IsPlayActive || (bool) ProEra.Game.MatchState.IsPlayInitiated)
      return;
    if ((!global::Game.IsRun && !global::Game.IsPlayAction || global::Game.IsQBKneel || global::Game.IsQBSpike ? 0 : (!global::Game.IsPitchPlay ? 1 : 0)) != 0)
      this.ownerPlayerAI.RequestHandoff();
    if (global::Game.IsFG)
      this.ownerPlayerAI.RequestFieldGoal();
    if (global::Game.IsKickoff)
      this.ownerPlayerAI.RequestKickoff(global::Game.IsOnsidesKick);
    if ((global::Game.IsPass || global::Game.IsQBKneel || global::Game.IsQBSpike ? 1 : (global::Game.IsQBKeeper ? 1 : 0)) != 0)
      this.ownerPlayerAI.RequestSnap();
    if (!global::Game.IsPunt)
      return;
    global::Game.Punter.RequestPunt();
  }

  private void DebugDrawRoute()
  {
    Vector3 start = this.ownerPlayerAI.trans.position;
    foreach (Vector3 actualRoutePoint in this.actualRoutePoints)
    {
      Debug.DrawLine(start, actualRoutePoint, Color.green, 8f);
      start = actualRoutePoint;
    }
  }
}
