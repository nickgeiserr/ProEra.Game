// Decompiled with JetBrains decompiler
// Type: ReceiveHandoff
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveHandoff : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  private PlayerAI ownerPlayerAI;
  public SharedInt currentPlayAssignment;
  private ReceiveHandoffAssignment playerRoute;
  private List<Vector3> actualRoutePoints;
  private PlayersManager pm;
  private MatchManager matchManager;
  private PlayManager playMan;
  private Transform trans;
  private AnimatorCommunicator locoAgent;
  private float targetUpdateTime;
  private const float TARGETDURATION = 0.5f;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.playerRoute = this.ownerPlayerAI.CurrentPlayAssignment as ReceiveHandoffAssignment;
    this.actualRoutePoints = new List<Vector3>();
    if (this.playerRoute != null)
    {
      this.ownerPlayerAI.SetPlayerRouteForAssignment((RunPathAssignment) this.playerRoute, this.actualRoutePoints, 1f);
      if (this.actualRoutePoints.Count > 0)
      {
        this.ownerPlayerAI.animatorCommunicator.AddGoalToQueue(this.actualRoutePoints[this.actualRoutePoints.Count - 1] + Vector3.forward * Field.THREE_YARDS * (float) Game.OffensiveFieldDirection);
        this.DebugDrawRoute();
      }
    }
    this.targetUpdateTime = 0.0f;
    this.matchManager = MatchManager.instance;
    this.pm = this.matchManager.playersManager;
    this.playMan = MatchManager.instance.playManager;
    this.locoAgent = this.ownerPlayerAI.animatorCommunicator;
    this.trans = this.ownerPlayerAI.trans;
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI || (Object) this.ownerPlayerAI != (Object) this.playMan.handOffTarget || Game.IsTurnover && this.ownerPlayerAI.onOffense || this.ownerPlayerAI.IsTackled)
      return TaskStatus.Failure;
    if (!this.ownerPlayerAI.eventAgent.IsInsideEvent && !this.ownerPlayerAI.nteractAgent.IsInsideInteraction && (Object) this.pm.ballHolderScript == (Object) this.ownerPlayerAI)
    {
      this.ownerPlayerAI.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.RunToEndZone, (RouteGraphicData) null, (float[]) null), true);
      return TaskStatus.Success;
    }
    this.targetUpdateTime -= this.ownerPlayerAI.AITimingInterval;
    return TaskStatus.Running;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    if (!((Object) this.ownerPlayerAI != (Object) null))
      return;
    this.ownerPlayerAI.EndCurrentAssignment();
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
