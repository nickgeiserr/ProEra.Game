// Decompiled with JetBrains decompiler
// Type: Blitz
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class Blitz : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  private BlitzAssignment assignment;
  private List<Vector3> actualRoutePoints;
  private int currentLeg;
  private PlayerAI ownerPlayerAI;
  private bool gotoChaseBall;

  private event System.Action OnRouteEnd;

  public override void OnStart()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayer.Value)
      return;
    this.OnRouteEnd += new System.Action(this.HandleRouteEnd);
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.assignment = this.ownerPlayerAI.CurrentPlayAssignment as BlitzAssignment;
    this.actualRoutePoints = new List<Vector3>();
    this.gotoChaseBall = false;
    if (this.assignment == null)
      return;
    this.ownerPlayerAI.animatorCommunicator.isRunningRoute = false;
    float num1 = 0.8f;
    List<RouteLeg> route = this.assignment.GetRoute();
    int num2 = 0;
    foreach (RouteLeg routeLeg in route)
    {
      ++num2;
      Vector3 moveVector = routeLeg.moveVector;
      moveVector.x = moveVector.x * Field.ONE_YARD * (float) global::Game.OffensiveFieldDirection + MatchManager.instance.ballHashPosition;
      moveVector.z = moveVector.z * Field.ONE_YARD * (float) global::Game.OffensiveFieldDirection + ProEra.Game.MatchState.BallOn.Value;
      float num3 = num1 * routeLeg.speedPercent;
      double num4 = (double) Mathf.Clamp(0.3f, this.ownerPlayerAI.animatorCommunicator.maxEffort01, num1 + num3);
      if (num2 == route.Count)
        this.ownerPlayerAI.animatorCommunicator.AddGoalToQueue(moveVector, this.ownerPlayerAI.animatorCommunicator.maxEffort01 + num3, this.OnRouteEnd);
      else
        this.ownerPlayerAI.animatorCommunicator.AddGoalToQueue(moveVector, this.ownerPlayerAI.animatorCommunicator.maxEffort01 + num3);
      this.actualRoutePoints.Add(moveVector);
    }
    Vector3 vector3 = this.actualRoutePoints[this.actualRoutePoints.Count - 1] - (this.actualRoutePoints.Count > 1 ? this.actualRoutePoints[this.actualRoutePoints.Count - 2] : this.ownerPlayerAI.trans.position);
    vector3.Normalize();
    Vector3 position = this.actualRoutePoints[this.actualRoutePoints.Count - 1] + vector3 * Field.FIVE_YARDS;
    this.ownerPlayerAI.animatorCommunicator.AddGoalToQueue(position);
    this.actualRoutePoints.Add(position);
    this.currentLeg = 0;
    this.DebugDrawRoute();
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayerAI || this.assignment == null || this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.Blitz || this.ownerPlayerAI.isEngagedInBlock || global::Game.BS_IsInAirPass || global::Game.BS_IsOnGround)
      return TaskStatus.Failure;
    if (this.ownerPlayerAI.animatorCommunicator.GoalReached)
      ++this.currentLeg;
    if (this.ownerPlayerAI.animatorCommunicator.atFinalGoal)
      this.HandleRouteEnd();
    if (this.gotoChaseBall)
      return TaskStatus.Success;
    if (!PlayerAI.IsQBBetweenTackles())
      this.SwitchToChaseBall();
    return TaskStatus.Running;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    this.OnRouteEnd -= new System.Action(this.HandleRouteEnd);
  }

  private RouteLeg GetCurrentLeg() => this.assignment.GetRoute()[this.currentLeg];

  private void DebugDrawRoute()
  {
    Vector3 start = this.ownerPlayerAI.trans.position;
    foreach (Vector3 actualRoutePoint in this.actualRoutePoints)
    {
      Debug.DrawLine(start, actualRoutePoint, Color.white, 8f);
      start = actualRoutePoint;
    }
  }

  private void HandleRouteEnd() => this.SwitchToChaseBall();

  private void SwitchToChaseBall()
  {
    this.ownerPlayerAI.attackBallHolder = true;
    this.ownerPlayerAI.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
    this.gotoChaseBall = true;
  }
}
