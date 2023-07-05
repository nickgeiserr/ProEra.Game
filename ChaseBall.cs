// Decompiled with JetBrains decompiler
// Type: ChaseBall
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UDB;
using UnityEngine;

public class ChaseBall : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedInt currentPlayAssignment;
  private PlayerAI ownerPlayerAI;
  private float targetUpdateTime;
  private const float TARGETDURATION = 0.167f;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.targetUpdateTime = 0.0f;
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI || global::Game.IsTurnover && !this.ownerPlayerAI.onOffense)
      return TaskStatus.Failure;
    if (this.ownerPlayerAI.isEngagedInBlock || this.ownerPlayerAI.isTackling)
      return TaskStatus.Success;
    this.targetUpdateTime -= this.ownerPlayerAI.AITimingInterval;
    if ((double) this.targetUpdateTime < 0.0)
    {
      this.targetUpdateTime = 0.167f;
      float num1 = (global::Game.IsTurnover ? (float) global::Game.OffensiveFieldDirection : (float) -global::Game.OffensiveFieldDirection) * Field.TEN_YARDS;
      PlayerAI holderPursuitTarget = this.ownerPlayerAI.BallHolderPursuitTarget;
      Vector3 vector3_1 = (Object) holderPursuitTarget != (Object) null ? holderPursuitTarget.transform.position : SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position;
      bool flag = Field.FurtherDownfield(vector3_1.z + num1, this.ownerPlayerAI.trans.position.z);
      if (!(this.CanRelaxJog() & flag))
      {
        if ((Object) holderPursuitTarget != (Object) null)
        {
          float num2 = 1f;
          if (holderPursuitTarget.IsTackled)
            return TaskStatus.Failure;
          Vector3 vector3_2 = holderPursuitTarget.Velocity;
          if ((double) vector3_2.magnitude < (double) num2 || global::Game.QBHasBallInPocket)
          {
            this.ownerPlayerAI.attackBallHolder = true;
            vector3_2 = vector3_1 - this.ownerPlayerAI.trans.position;
            Vector3 vector3_3 = vector3_2.normalized * Field.FIVE_YARDS;
            Vector3 vector3_4 = vector3_1 + vector3_3;
            this.ownerPlayerAI.animatorCommunicator.SetGoal(vector3_4, 1f);
            Debug.DrawLine(this.ownerPlayerAI.trans.position, vector3_4, Color.red, this.ownerPlayerAI.AITimingInterval);
          }
          else
          {
            this.ownerPlayerAI.attackBallHolder = true;
            Vector3 holderPursuitAngle = this.ownerPlayerAI.GetBallHolderPursuitAngle();
            vector3_2 = holderPursuitAngle - this.ownerPlayerAI.trans.position;
            Vector3 vector3_5 = holderPursuitAngle + vector3_2.normalized * Field.FIVE_YARDS;
            this.ownerPlayerAI.animatorCommunicator.SetGoal(vector3_5, 1f);
            Debug.DrawLine(this.ownerPlayerAI.trans.position, vector3_5, Color.red, this.ownerPlayerAI.AITimingInterval);
          }
          if (global::Game.IsTurnover && (global::Game.IsKickoff || global::Game.IsPunt))
          {
            Vector3 position = holderPursuitTarget.trans.position;
            if (this.ownerPlayerAI.ShouldKickCoverageStayInLane(position))
              this.ownerPlayerAI.SetKickCoverageLaneTarget(position);
          }
        }
        else
        {
          this.ownerPlayerAI.animatorCommunicator.SetGoal((SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position + SingletonBehaviour<BallManager, MonoBehaviour>.instance.rigidbd.velocity).SetY(0.0f), 1f);
          this.ownerPlayerAI.CheckForPickUpBallAnimation();
        }
      }
      else
      {
        Vector3 normalized = (vector3_1 - this.ownerPlayerAI.trans.position).normalized;
        Vector3 vector3_6 = Vector3.RotateTowards(normalized, -normalized, Random.Range(0.0f, 6.28318548f), 5f);
        float effortCeiling01 = Random.Range(0.3f, 0.65f);
        this.ownerPlayerAI.animatorCommunicator.SetGoal((vector3_1 + vector3_6).SetY(0.0f), effortCeiling01);
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

  private bool CanRelaxJog() => !global::Game.BallHolderIsUser && !Field.FurtherDownfield((float) ProEra.Game.MatchState.BallOn, SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position.z);
}
