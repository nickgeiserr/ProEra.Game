// Decompiled with JetBrains decompiler
// Type: ManCoverage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System;
using System.Collections;
using UnityEngine;

public class ManCoverage : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  private ManDefenseAssignment assignment;
  private PlayerAI ownerPlayerAI;
  private PlayerAI coverageOn;
  private float targetUpdateTime;
  private PlayersManager playersManager;
  private MatchManager matchManager;
  private AnimatorCommunicator animCom;
  private int coverPhase = -1;
  private bool handOffDetected;
  private bool throwDetected;
  private const float TARGETDURATION = 0.167f;
  private const float RADIUSOFSATISFACTION = 0.6f;

  public override void OnStart()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.assignment = this.ownerPlayerAI.CurrentPlayAssignment as ManDefenseAssignment;
    if (this.assignment != null)
    {
      this.coverageOn = this.assignment.GetCoverageOn();
    }
    else
    {
      Debug.LogError((object) " Assignment is null in ManCoverage OnStart");
      this.coverageOn = (PlayerAI) null;
    }
    this.targetUpdateTime = 0.0f;
    this.matchManager = MatchManager.instance;
    this.playersManager = this.matchManager.playersManager;
    this.animCom = this.ownerPlayerAI.animatorCommunicator;
    this.animCom.SetLocomotionStyle(ELocomotionStyle.DefaultStrafe);
    this.coverPhase = -1;
    this.handOffDetected = false;
    this.throwDetected = false;
    PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayerAI || (UnityEngine.Object) this.coverageOn == (UnityEngine.Object) null || this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.ManCoverage || global::Game.IsTurnover && !this.ownerPlayerAI.onOffense)
      return TaskStatus.Failure;
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
    if (this.ownerPlayerAI.isTackling)
      return TaskStatus.Success;
    this.targetUpdateTime -= this.ownerPlayerAI.AITimingInterval;
    Vector3 position1 = this.ownerPlayerAI.trans.position;
    Vector3 position2 = this.coverageOn.trans.position;
    float threeYards = Field.THREE_YARDS;
    if ((double) this.targetUpdateTime < 0.0)
    {
      this.targetUpdateTime = 0.167f;
      float num1 = this.coverageOn.speed * AIUtil.SPEEDRATING_TO_VELFACTOR;
      Vector3 vector3_1 = position2 + this.coverageOn.trans.forward * num1 * this.ownerPlayerAI.AITimingInterval;
      if ((double) Vector3.Distance(position1, position2) < 0.60000002384185791)
        this.animCom.SetGoal(position2 + this.coverageOn.trans.forward);
      else if (this.coverPhase < 1 && Field.IsBehindLineOfScrimmage(position2.z + (this.coverageOn.trans.forward * Field.THREE_YARDS).z))
      {
        this.coverPhase = 0;
        Vector3 dest = new Vector3(position2.x + this.coverageOn.trans.forward.x * Field.EIGHT_YARDS, 0.0f, !global::Game.OffenseGoingNorth ? Mathf.Max(position1.z, ProEra.Game.MatchState.BallOn.Value - Field.EIGHT_YARDS) : Mathf.Min(position1.z, ProEra.Game.MatchState.BallOn.Value + Field.EIGHT_YARDS));
        Quaternion rotation = Quaternion.LookRotation(position2 - position1);
        this.animCom.SetGoal(PlayerAI.ClampMoveToFieldBounds(dest), rotation);
        Debug.DrawLine(position1 + new Vector3(0.0f, 0.1f, 0.0f), position2, Color.cyan, 0.167f);
      }
      else if ((this.coverPhase == -1 || this.coverPhase == 1) && Field.FurtherDownfield(position1.z, vector3_1.z + threeYards * (float) global::Game.OffensiveFieldDirection) && (double) Mathf.Abs(position1.x - position2.x) < (double) Field.ONE_YARD * 1.5)
      {
        this.coverPhase = 1;
        Vector3 dest = position1 + Field.FIVE_YARDS * Vector3.forward * (float) global::Game.OffensiveFieldDirection;
        if (Field.FurtherDownfield(dest.z, Field.OFFENSIVE_BACK_OF_ENDZONE - Field.ONE_YARD_FORWARD * 2f))
          dest.z = Field.OFFENSIVE_BACK_OF_ENDZONE - Field.ONE_YARD_FORWARD * 2f;
        this.animCom.SetGoal(PlayerAI.ClampMoveToFieldBounds(dest), Field.DEFENSE_TOWARDS_LOS_QUATERNION);
        Debug.DrawLine(position1 + new Vector3(0.0f, 0.1f, 0.0f), position2, Color.yellow, 0.167f);
      }
      else
      {
        this.coverPhase = 2;
        float oneYard = Field.ONE_YARD;
        float num2 = Mathf.Abs(position2.z - ProEra.Game.MatchState.BallOn.Value);
        float num3 = Mathf.Max(Field.ONE_YARD, (float) (((double) Field.TEN_YARDS - (double) num2) / 3.0));
        float num4 = this.coverageOn.speed - this.ownerPlayerAI.speed;
        float num5 = (double) num4 <= 0.30000001192092896 ? ((double) num4 <= 0.15000000596046448 ? Field.THREE_YARDS : Field.FIVE_YARDS) : Field.EIGHT_YARDS;
        float num6 = num3 + num5;
        float num7 = this.assignment == null || this.assignment.GetTechnique() != ManDefenseAssignment.EManCoverTypeTechnique.Under ? 0.0f : -1f;
        Vector3 vector3_2 = position2 + this.coverageOn.trans.forward * num7 + this.coverageOn.trans.forward * num1 + Vector3.forward * num6 * (float) global::Game.OffensiveFieldDirection;
        Vector3 dest;
        if ((double) vector3_2.x > 0.0)
        {
          Vector3 vector3_3 = this.assignment == null || this.assignment.GetLeverage() != ManDefenseAssignment.EManCoverTypeLeverage.Inside ? Vector3.right : Vector3.left;
          dest = vector3_2 + vector3_3 * 0.5f * (float) global::Game.OffensiveFieldDirection;
        }
        else
        {
          Vector3 vector3_4 = this.assignment == null || this.assignment.GetLeverage() != ManDefenseAssignment.EManCoverTypeLeverage.Inside ? Vector3.left : Vector3.right;
          dest = vector3_2 + vector3_4 * 0.5f * (float) global::Game.OffensiveFieldDirection;
        }
        if (Field.FurtherDownfield(dest.z, Field.OFFENSIVE_BACK_OF_ENDZONE - Field.ONE_YARD_FORWARD * 2f))
          dest.z = Field.OFFENSIVE_BACK_OF_ENDZONE - Field.ONE_YARD_FORWARD * 2f;
        Vector3 fieldBounds = PlayerAI.ClampMoveToFieldBounds(dest);
        this.animCom.SetLocomotionStyle(ELocomotionStyle.Regular);
        this.animCom.SetGoal(fieldBounds);
        Debug.DrawLine(position1 + new Vector3(0.0f, 0.1f, 0.0f), fieldBounds, Color.red, 0.167f);
      }
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
    if (this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.ManCoverage)
      return;
    this.ownerPlayerAI.EndCurrentAssignment();
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
}
