// Decompiled with JetBrains decompiler
// Type: QBLookToPass
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class QBLookToPass : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedInt currentPlayAssignment;
  private PlayerAI ownerPlayerAI;
  private PlayersManager pm;
  private MatchManager matchManager;
  private Transform trans;
  private LocomotionAgentController locoAgent;
  private float targetUpdateTime;
  private bool foundHole;
  private float qbTimeUnderPressure;
  private const float TARGETDURATION = 0.25f;
  private const float TIME_UNDERPRESSURE_FORCETHROW = 2.5f;
  private const float TIME_UNDERPRESSURE_DANGER = 0.2f;
  private const float MIN_DIST_FOR_PRESSURE = 4f;
  private const float PASS_RUSH_SCORE_FOR_PANIC = 135f;
  private const float PASS_RUSH_REACT_DELAY = 1.5f;
  private const float MIN_PASS_RUSH_SCORE_FOR_POCKET_MOVE = 80f;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.targetUpdateTime = 0.0f;
    this.matchManager = MatchManager.instance;
    this.pm = this.matchManager.playersManager;
    this.foundHole = false;
    this.locoAgent = (LocomotionAgentController) this.ownerPlayerAI.animatorCommunicator;
    this.trans = this.ownerPlayerAI.trans;
    this.qbTimeUnderPressure = 0.0f;
    this.locoAgent.SetLocomotionStyle(ELocomotionStyle.QuaterbackStrafe, this.ownerPlayerAI.LeftHanded);
    this.locoAgent.SetGoal(this.trans.position);
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI || Game.IsTurnover && this.ownerPlayerAI.onOffense)
      return TaskStatus.Failure;
    if (this.ownerPlayerAI.IsTackled || Game.BallIsThrownOrKicked || (Object) this.pm.ballHolderScript != (Object) this.ownerPlayerAI)
      return TaskStatus.Success;
    if (this.ownerPlayerAI.eventAgent.IsInsideEvent || this.ownerPlayerAI.nteractAgent.IsInsideInteraction)
      return TaskStatus.Running;
    this.targetUpdateTime -= this.ownerPlayerAI.AITimingInterval;
    if ((double) this.targetUpdateTime < 0.0)
    {
      this.targetUpdateTime = 0.25f;
      if (this.pm.forceQBScramble)
      {
        if (!this.foundHole)
        {
          this.locoAgent.SetGoal(PlayerAI.FindHole());
          Debug.DrawLine(this.trans.position + Vector3.up, this.locoAgent.CurrentGoal.position, Color.cyan);
        }
      }
      else
      {
        PlayerAI.FindOpenReceiver(false, false);
        Vector3 resultClosestRusher;
        float passRushers = this.ownerPlayerAI.FindPassRushers(out resultClosestRusher);
        double num = (double) Vector3.Distance(resultClosestRusher, this.trans.position);
        if ((double) passRushers < 80.0)
        {
          this.locoAgent.SetGoal(this.trans.position);
        }
        else
        {
          if (this.locoAgent.atFinalGoal)
          {
            Vector3 zero = Vector3.zero with
            {
              x = !Field.MoreRightOf(resultClosestRusher.x, this.trans.position.x) ? Field.ONE_YARD_RIGHT : Field.ONE_YARD_LEFT,
              z = !PlayerAI.IsQBBetweenTackles() ? ((double) Mathf.Abs(this.trans.position.x) <= 7.5 ? 0.0f : Field.ONE_YARD_FORWARD * 1.5f) : Field.ONE_YARD_FORWARD * -0.5f
            };
            Debug.DrawLine(this.trans.position + Vector3.up, resultClosestRusher, Color.red);
            Debug.DrawLine(this.trans.position + Vector3.up, this.trans.position + zero, Color.black);
            this.locoAgent.SetGoal(this.trans.position + zero.normalized * Field.TWO_YARDS);
          }
          this.qbTimeUnderPressure += this.ownerPlayerAI.AITimingInterval;
          if ((double) this.qbTimeUnderPressure > 2.5 || (double) passRushers > 135.0)
            PlayerAI.FindOpenReceiver(true, true);
          else if ((double) this.qbTimeUnderPressure > 0.20000000298023224)
            PlayerAI.FindOpenReceiver(true, false);
        }
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
    if (!((Object) this.locoAgent != (Object) null))
      return;
    this.locoAgent.SetLocomotionStyle(ELocomotionStyle.Regular);
  }
}
