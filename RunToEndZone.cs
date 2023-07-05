// Decompiled with JetBrains decompiler
// Type: RunToEndZone
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class RunToEndZone : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedInt currentPlayAssignment;
  private RunToEndZoneAssignment playerRoute;
  private List<Vector3> actualRoutePoints;
  private PlayerAI ownerPlayerAI;
  private PlayersManager pm;
  private MatchManager matchManager;
  private Transform trans;
  private AnimatorCommunicator animCom;
  private float dirUpdateDuration;
  private float targetUpdateTime;
  private bool foundHole;
  private bool behindLos = true;
  private bool isAvoidingDef;
  private PlayerAI closestDef;
  private const float MAX_DIR_CHANGE_DURATION = 0.8f;
  private const float MIN_DIR_CHANGE_DURATION = 0.2f;
  private const float AVOID_DEFENDER_ANGLE = 80f;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.targetUpdateTime = 0.0f;
    this.matchManager = MatchManager.instance;
    this.pm = this.matchManager.playersManager;
    this.foundHole = false;
    this.animCom = this.ownerPlayerAI.animatorCommunicator;
    this.trans = this.ownerPlayerAI.trans;
    this.dirUpdateDuration = AIUtil.Remap(0.0f, 0.4f, 0.2f, 0.8f, 1f - this.ownerPlayerAI.speed);
    this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.Regular);
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI || (Object) this.ownerPlayerAI != (Object) this.pm.ballHolderScript || Game.IsTurnover && this.ownerPlayerAI.onOffense)
      return TaskStatus.Failure;
    if (this.ownerPlayerAI.IsTackled)
      return TaskStatus.Success;
    this.targetUpdateTime -= this.ownerPlayerAI.AITimingInterval;
    if ((double) this.targetUpdateTime < 0.0)
    {
      this.targetUpdateTime = this.dirUpdateDuration;
      Vector3 position = this.trans.position;
      if (Game.IsRun && Field.FurtherDownfield(MatchManager.ballOn - 0.2f * Field.ONE_YARD * (float) Game.OffensiveFieldDirection, position.z))
      {
        this.behindLos = true;
        this.foundHole = true;
      }
      else
      {
        this.behindLos = false;
        this.closestDef = this.ownerPlayerAI.FindPotentialTacklers();
        if ((Object) this.closestDef != (Object) null)
        {
          bool flag = this.ownerPlayerAI.ShouldRunnerGoOutOfBounds();
          Vector3 forward = this.closestDef.trans.position - position;
          if (forward != Vector3.zero)
          {
            Quaternion quaternion = Quaternion.LookRotation(forward, Vector3.up);
            if (Field.MoreLeftOf(this.closestDef.trans.position.x, position.x))
            {
              if ((double) Mathf.Abs(Field.RIGHT_OUT_OF_BOUNDS - position.x) > (double) Field.ConvertYardsToDistance(AIUtil.MIN_YARDS_FROM_SIDELINE_TO_AVOID_DEF) | flag)
              {
                this.isAvoidingDef = true;
                quaternion *= Quaternion.Euler(0.0f, 80f, 0.0f);
              }
            }
            else if ((double) Mathf.Abs(Field.LEFT_OUT_OF_BOUNDS - position.x) > (double) Field.ConvertYardsToDistance(AIUtil.MIN_YARDS_FROM_SIDELINE_TO_AVOID_DEF) | flag)
            {
              this.isAvoidingDef = true;
              quaternion *= Quaternion.Euler(0.0f, -80f, 0.0f);
            }
            if (this.isAvoidingDef)
              this.animCom.SetGoalDirection((quaternion * Vector3.forward * Field.FIVE_YARDS).normalized, 0.8f);
            else
              this.animCom.SetGoalDirection(AIUtil.GetBestAngleToEZ(this.ownerPlayerAI), 1f);
          }
          else
            this.animCom.SetGoalDirection(AIUtil.GetBestAngleToEZ(this.ownerPlayerAI), 1f);
        }
        else
          this.animCom.SetGoalDirection(AIUtil.GetBestAngleToEZ(this.ownerPlayerAI), 1f);
      }
    }
    return TaskStatus.Running;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    if (!((Object) this.ownerPlayerAI != (Object) null))
      return;
    this.ownerPlayerAI.EndCurrentAssignment();
  }

  public override void OnDrawGizmos()
  {
  }
}
