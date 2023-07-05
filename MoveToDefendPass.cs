// Decompiled with JetBrains decompiler
// Type: MoveToDefendPass
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UDB;
using UnityEngine;

public class MoveToDefendPass : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedInt currentPlayAssignment;
  private PlayerAI ownerPlayerAI;
  private float targetUpdateTime;
  private PlayersManager pm;
  private AnimatorCommunicator animCom;
  private PlayerAI catchReceiver;
  private Vector3 ballCatchPos;
  private Vector3 ballToCatch;
  private Vector3 ballVelXZ;
  private Vector3 playerToCatch;
  private Vector3 recToCatch;
  private Vector3 recTacklePoint;
  private Vector3 targetGoal;
  private float dirUpdateDuration;
  private bool shouldContainRec;
  private bool shouldAttackBallRecPoint;
  private bool shouldCutOffBall;
  private bool shouldFaceBall;
  private const float MAX_DIR_CHANGE_DURATION = 0.8f;
  private const float MIN_DIR_CHANGE_DURATION = 0.3f;
  private const float FACEBALLDIST = 2.5f;
  private const float SLOWDOWNDIST = 5f;
  private const float MINPLAYERESTCLOSINGSPEED = 6.26f;
  private const float BALLCATCHVELPROJECTIONTIME = 0.05f;
  private const float TIMEDIFFTORECEIVERCATCHFORPLAYINFRONTBALL = 0.5f;
  private const float TIMEDIFFTOBALLFORATTACKBALL = 0.5f;
  private const float MAXINFRONTRECDIST = 5f;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.pm = MatchManager.instance.playersManager;
    this.animCom = this.ownerPlayerAI.animatorCommunicator;
    this.dirUpdateDuration = AIUtil.Remap(0.0f, 0.6f, 0.3f, 0.8f, (float) (1.0 - (double) this.ownerPlayerAI.coverage * 0.10000000149011612));
    this.catchReceiver = this.pm.intendedReceiver;
    this.ballCatchPos = this.pm.passDestination;
    this.ballToCatch = SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position - this.ballCatchPos;
    this.ballVelXZ = SingletonBehaviour<BallManager, MonoBehaviour>.instance.rigidbd.velocity;
    this.playerToCatch = this.ownerPlayerAI.trans.position - this.ballCatchPos;
    this.ownerPlayerAI.SetIsGoingForThrownBall(true);
    this.shouldContainRec = false;
    this.shouldAttackBallRecPoint = false;
    this.shouldCutOffBall = false;
    this.shouldFaceBall = false;
    this.targetUpdateTime = 0.0f;
    this.targetGoal = this.animCom.CurrentGoal.position;
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI || Game.IsTurnover && !this.ownerPlayerAI.onOffense || this.ownerPlayerAI.isEngagedInBlock || this.ownerPlayerAI.isTackling)
      return TaskStatus.Failure;
    if (Game.BS_IsPlayersHands && this.pm.GetBallHolder().onOffense)
    {
      this.ownerPlayerAI.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
      return TaskStatus.Success;
    }
    this.targetUpdateTime -= this.ownerPlayerAI.AITimingInterval;
    if ((double) this.targetUpdateTime < 0.0)
    {
      this.targetUpdateTime = this.dirUpdateDuration;
      this.shouldContainRec = false;
      this.shouldAttackBallRecPoint = false;
      this.shouldCutOffBall = false;
      this.shouldFaceBall = false;
      this.ballVelXZ = SingletonBehaviour<BallManager, MonoBehaviour>.instance.rigidbd.velocity;
      this.ballVelXZ.y = 0.0f;
      float magnitude1 = this.ballVelXZ.magnitude;
      if ((Object) this.pm.intendedReceiver != (Object) null && (double) magnitude1 > 0.0)
      {
        this.catchReceiver = this.pm.intendedReceiver;
        this.ballCatchPos = this.pm.passDestination;
        this.ballToCatch = SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position - this.ballCatchPos;
        this.ballToCatch.y = 0.0f;
        this.playerToCatch = this.ownerPlayerAI.trans.position - this.ballCatchPos;
        this.playerToCatch.y = 0.0f;
        this.recToCatch = this.catchReceiver.trans.position - this.ballCatchPos;
        this.recToCatch.y = 0.0f;
        float magnitude2 = this.catchReceiver.animatorCommunicator.velocity.magnitude;
        float num1 = this.ballToCatch.magnitude / magnitude1;
        float num2 = this.playerToCatch.magnitude / Mathf.Max(magnitude2, 6.26f);
        double num3 = (double) this.recToCatch.magnitude / (double) Mathf.Max(magnitude2, 6.26f);
        this.recTacklePoint = this.ballCatchPos + Vector3.forward * Mathf.Lerp(Field.ONE_YARD_FORWARD, Field.ONE_YARD_FORWARD * 5f, Mathf.Clamp(this.playerToCatch.magnitude / (Field.ONE_YARD * 3f), 0.0f, 1f));
        double num4 = (double) num2;
        if (num3 - num4 < 0.5)
        {
          this.shouldContainRec = true;
          this.targetGoal = this.recTacklePoint;
        }
        else if ((double) num1 - (double) num2 > 0.5)
        {
          this.shouldAttackBallRecPoint = true;
          this.targetGoal = this.ballCatchPos - this.ballVelXZ * 0.05f;
        }
        else
        {
          this.shouldCutOffBall = true;
          this.targetGoal = this.ballCatchPos;
        }
      }
      else
        this.targetGoal = this.ballCatchPos + Vector3.forward * (Field.ONE_YARD_FORWARD * 2f);
      this.targetGoal = PlayerAI.ClampMoveToFieldBounds(this.targetGoal);
      this.targetGoal.y = 0.0f;
      float num = Vector3.Distance(this.ownerPlayerAI.trans.position, this.targetGoal);
      if (this.shouldCutOffBall && (double) num < 2.5)
      {
        this.shouldFaceBall = true;
        this.animCom.SetLocomotionStyle(ELocomotionStyle.DefaultStrafe);
        this.animCom.SetGoal(this.targetGoal, 0.3f, PlayerAI.LookAtBallRotation(this.targetGoal));
      }
      else if (this.shouldAttackBallRecPoint && (double) num < 5.0)
      {
        this.shouldFaceBall = true;
        this.animCom.SetGoal(this.targetGoal, 0.65f);
      }
      else
        this.animCom.SetGoal(this.targetGoal, 1f);
    }
    return TaskStatus.Running;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    if (!((Object) this.ownerPlayerAI != (Object) null) || this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.MoveToDefendPass)
      return;
    this.ownerPlayerAI.EndCurrentAssignment();
  }

  public override void OnDrawGizmos()
  {
  }
}
