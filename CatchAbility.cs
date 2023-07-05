// Decompiled with JetBrains decompiler
// Type: CatchAbility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System;
using System.Collections.Generic;
using UDB;
using UnityEngine;
using UnityEngine.Events;
using Vars;

public class CatchAbility : MonoBehaviour
{
  [SerializeField]
  private AnimationEventAgent _eventAgent;
  [SerializeField]
  private float _maxAcceptableCost = 1.5f;
  [SerializeField]
  private AnimationEventScenario _offensiveCatches;
  [SerializeField]
  private float _offenseCatchRadius = 5f;
  [SerializeField]
  private AnimationEventScenario _defensiveCatches;
  [SerializeField]
  private float _defenseCatchRadius = 4f;
  [SerializeField]
  private AnimationEventScenario _sidelineCatches;
  [SerializeField]
  private AnimationEventScenario _fumbleRecoveries;
  [SerializeField]
  private float _fumbleRecoveryRadius = 2.2f;
  [SerializeField]
  private AnimationEventScenario _specialTeamsCatches;
  [SerializeField]
  private RigHumanoidMapping _rigMapping;
  [SerializeField]
  private CatchHandTrigger _ballCatchTrigger;
  [SerializeField]
  private List<Collider> _bodyColliders;
  [SerializeField]
  private VisibilityAreaConfig _visibilityConfig;
  private CatchOutcomeTracker _catchOutcomeTracker = new CatchOutcomeTracker();
  private AnimatorCullingMode _cachedCullingMode;
  private Transform _trans;
  private Animator _animator;
  private PlayerAI _playerAI;
  private bool _isAttemptingCatch;
  private bool _isAttemptingFumbleRecovery;
  private const float _narrowVisionMaxDot = 0.5f;
  private const float _catchWindupTime = 0.5f;
  private const float _maxCatchHeight = 2.5f;
  private const float _minCatchHeight = 0.1f;
  private const float _confortableCatchHeight = 1f;
  private const float _playerForwardLookAheadDistance = 3f;
  private static bool _subscibedForBallState;
  private static bool _offenseGoingForBall;
  private static bool _defenseGoingForBall;
  private static Rect _fieldRect;
  private static Rect _midfieldRect;
  private static Rect _southEndZone;
  private static Rect _northEndZone;
  private static float _fieldEdgeProximity = Field.ONE_YARD * 2f;
  private static int _playerCapsuleLayerIndex;
  private AnimationEventController _delayedCatchCandidate;
  private int _delayedCatchFrame;
  private static float _lastCollisionGlobalTime;
  private float _lastCollisionIndividualTime;

  public static bool OffenseGoingForBall => CatchAbility._offenseGoingForBall;

  public CatchOutcomeTracker.ECatchOutcome CatchOutcome => this._catchOutcomeTracker.CatchOutcome;

  public float ScaledMaxCatchHeight => 2.5f * this._trans.lossyScale.x;

  public float ScaledMinCatchHeight => 0.1f * this._trans.lossyScale.x;

  public float ScaledDefenseCatchRadius => this._defenseCatchRadius * this._trans.lossyScale.x;

  public float ScaledOffenseCatchRadius => this._offenseCatchRadius * this._trans.lossyScale.x;

  public float ScaledFumbleRecoveryRadius => this._fumbleRecoveryRadius * this._trans.lossyScale.x;

  public float ScaledConfortableCatchHeight => 1f * this._trans.lossyScale.x;

  private void Awake()
  {
    this._trans = this.transform;
    this._animator = this.GetComponent<Animator>();
    CatchAbility._fieldRect = new Rect(-Field.OUT_OF_BOUNDS, Field.SOUTH_BACK_OF_ENDZONE, Field.FIELD_WIDTH, Field.FIELD_LENGTH + Field.TEN_YARDS * 2f);
    CatchAbility._midfieldRect = new Rect(CatchAbility._fieldRect.x + CatchAbility._fieldEdgeProximity, CatchAbility._fieldRect.y + CatchAbility._fieldEdgeProximity, Field.FIELD_WIDTH - CatchAbility._fieldEdgeProximity * 2f, CatchAbility._fieldRect.height - CatchAbility._fieldEdgeProximity * 2f);
    CatchAbility._southEndZone = new Rect(-Field.OUT_OF_BOUNDS, Field.SOUTH_BACK_OF_ENDZONE, Field.FIELD_WIDTH, Field.TEN_YARDS);
    CatchAbility._northEndZone = new Rect(-Field.OUT_OF_BOUNDS, Field.NORTH_BACK_OF_ENDZONE - Field.TEN_YARDS, Field.FIELD_WIDTH, Field.TEN_YARDS);
    this._playerAI = this.GetComponent<PlayerAI>();
    this._cachedCullingMode = this._animator.cullingMode;
    CatchAbility._playerCapsuleLayerIndex = this.gameObject.layer;
    if (!CatchAbility._subscibedForBallState)
    {
      Ball.State.BallState.OnValueChanged += new Action<EBallState>(CatchAbility.BallStateHandler);
      CatchAbility._subscibedForBallState = true;
    }
    this._ballCatchTrigger.onBallInsideTrigger.AddListener((UnityAction) (() =>
    {
      this.OnCatchKeyMoment(SingletonBehaviour<BallManager, MonoBehaviour>.instance, this._playerAI);
      this.DisableBallCatchTrigger();
    }));
  }

  private void OnCatchEventPlaybackStart()
  {
    this._catchOutcomeTracker.ResetCatchOutcome();
    this._isAttemptingCatch = true;
  }

  private void OnFumbleRecoveryEventPlaybackStart()
  {
    this._catchOutcomeTracker.ResetCatchOutcome();
    this._isAttemptingFumbleRecovery = true;
  }

  private void OnCatchPreKeyMoment()
  {
    this.EnableBodyColliders(false);
    this.SetupBallCatchTrigger();
  }

  private void OnCatchPostKeyMoment()
  {
    this.DisableBallCatchTrigger();
    this.EnableBodyColliders(true);
  }

  private void OnCatchEventPlaybackEnd()
  {
    this._isAttemptingCatch = false;
    this.SetIsGoingForBall(false);
  }

  private void OnFumbleRecoveryEventPlaybackEnd()
  {
    this._isAttemptingFumbleRecovery = false;
    this.SetIsGoingForBall(false);
  }

  private void SetIsGoingForBall(bool val)
  {
    if (this._playerAI.onOffense)
      CatchAbility._offenseGoingForBall = val;
    else
      CatchAbility._defenseGoingForBall = val;
  }

  public void SetAlwaysAnimateAnimatorCullingMode() => this._animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;

  public void SetCachedAnimatorCullingMode() => this._animator.cullingMode = this._cachedCullingMode;

  private static void BallStateHandler(EBallState state)
  {
    CatchAbility._offenseGoingForBall = false;
    CatchAbility._defenseGoingForBall = false;
  }

  public void EnableBodyColliders(bool enable)
  {
    for (int index = 0; index < this._bodyColliders.Count; ++index)
      this._bodyColliders[index].enabled = enable;
  }

  public static bool IsDefenderCloseAndDownfield(
    Vector3 candidateCatchPos,
    Vector3 ballFlightDirection,
    List<PlayerAI> opponents)
  {
    for (int index = 0; index < opponents.Count; ++index)
    {
      Vector3 position = opponents[index].transform.position;
      Vector3 vector3 = position - candidateCatchPos;
      if ((double) Vector3.Distance(candidateCatchPos, position) < 2.0 & (double) Vector3.Dot(ballFlightDirection.normalized, vector3.normalized) > 0.33300000429153442)
        return true;
    }
    return false;
  }

  private bool PreCatchChecksPassed(PlayersManager playersManager, PlayerAI playerAI)
  {
    bool flag = playerAI.IsQB() && global::Game.CurrentPlayHasUserQBOnField;
    return (((UnityEngine.Object) playersManager.ballHolderScript == (UnityEngine.Object) playerAI || this._eventAgent.IsInsideEvent ? 1 : (playerAI.nteractAgent.IsInsideInteraction ? 1 : 0)) | (flag ? 1 : 0)) == 0 && (!playerAI.onOffense || !CatchAbility._offenseGoingForBall) && (playerAI.onOffense || !CatchAbility._defenseGoingForBall) && (playerAI.onOffense || !CatchAbility._offenseGoingForBall);
  }

  public void SetupBallCatchTrigger()
  {
    this._ballCatchTrigger.transform.localPosition = this._eventAgent.AdjustedLocalKeyEventPos;
    this._ballCatchTrigger.gameObject.SetActive(true);
  }

  public void DisableBallCatchTrigger() => this._ballCatchTrigger.gameObject.SetActive(false);

  public void OnDisable()
  {
    this.DisableBallCatchTrigger();
    this.EnableBodyColliders(true);
  }

  private void OnCatchKeyMoment(BallManager ballManager, PlayerAI playerAI)
  {
    this._catchOutcomeTracker.StartCatchOutcomeTracking(this._rigMapping, this._eventAgent.CurrentControllerMeta, CatchAbility._fieldRect);
    ballManager.BallCatchAnalysis(playerAI);
  }

  public void LookForBallPickUp(
    PlayersManager playersManager,
    BallManager ballManager,
    PlayerAI playerAI)
  {
    if (!this.PreCatchChecksPassed(playersManager, playerAI) || (UnityEngine.Object) playersManager.ballHolderScript != (UnityEngine.Object) null)
      return;
    Vector3 vector3 = ballManager.trans.position + ballManager.rigidbd.velocity * 0.5f;
    float num1 = 0.2f;
    float num2 = 0.0f;
    CatchSelectionDataBox extraDataBox = new CatchSelectionDataBox();
    extraDataBox.fieldBoundsRect = CatchAbility._fieldRect;
    extraDataBox.factorInTargetExitDir = false;
    if ((double) vector3.y >= (double) num1 || (double) vector3.y <= (double) num2 || !CatchAbility.IsBallPositionCandidateInFrontAndInRange(this._trans.position, this._trans.forward, this._animator.velocity, vector3, this.ScaledFumbleRecoveryRadius))
      return;
    AffineTransform affineTransform = new AffineTransform(vector3, Quaternion.identity);
    float cost;
    AnimationEventController animationEventController = this._fumbleRecoveries.SelectController(affineTransform, this._trans, out cost, (object) extraDataBox);
    if ((double) cost > (double) this._maxAcceptableCost)
      return;
    this.SetIsGoingForBall(true);
    this._eventAgent.EnterEvent(affineTransform, animationEventController);
  }

  public static bool IsBallPositionCandidateInFrontAndInRange(
    Vector3 agentPos,
    Vector3 agentFwd,
    Vector3 agentVel,
    Vector3 evaluatedBallPos,
    float maxRadius)
  {
    agentFwd.Scale(new Vector3(1f, 0.0f, 1f));
    agentFwd = agentFwd.normalized;
    bool side = new Plane(agentFwd, agentPos).GetSide(evaluatedBallPos);
    int num1 = (double) Vector3.Distance(agentPos, evaluatedBallPos) < (double) maxRadius ? 1 : 0;
    Vector3 rhs = evaluatedBallPos - agentPos;
    rhs.Scale(new Vector3(1f, 0.0f, 1f));
    rhs = rhs.normalized;
    agentVel.Scale(new Vector3(1f, 0.0f, 1f));
    bool flag = (double) Vector3.Dot(agentFwd, rhs) > (double) Mathf.Lerp(0.0f, 0.5f, Vector3.Dot(agentFwd, agentVel));
    int num2 = side ? 1 : 0;
    return (num1 & num2 & (flag ? 1 : 0)) != 0;
  }

  public void LookForCatchOpportunities(
    PlayersManager playersManager,
    BallManager ballManager,
    PlayerAI playerAI)
  {
    if (!this.PreCatchChecksPassed(playersManager, playerAI))
      return;
    float time = Time.time + 0.5f;
    Vector3 position1 = BallPhysicsSimulation.EvaluatePosition(time);
    if ((double) position1.y >= (double) this.ScaledMaxCatchHeight || (double) position1.y <= (double) this.ScaledMinCatchHeight)
      return;
    bool flag1 = false;
    bool flag2 = this._visibilityConfig.IsBallPositionCandidateInsideVisibilityArea(this._trans.position, this._trans.forward, this._animator.velocity / Time.timeScale, position1);
    Vector3 position2 = ballManager.trans.position;
    Vector3 vector3_1 = position1 - position2;
    vector3_1.Scale(new Vector3(1f, 0.0f, 1f));
    Vector3 normalized = vector3_1.normalized;
    Quaternion rotation1 = Quaternion.Euler(0.0f, Vector3.SignedAngle(Vector3.forward, new Vector3(normalized.x, 0.0f, normalized.z).normalized, Vector3.up), 0.0f);
    AffineTransform affineTransform = new AffineTransform(position1, rotation1);
    if (playerAI.onOffense)
    {
      List<PlayerAI> opponents = global::Game.IsPlayerOneOnDefense ? MatchManager.instance.playersManager.curUserScriptRef : MatchManager.instance.playersManager.curCompScriptRef;
      if (flag2)
      {
        if (CatchAbility._defenseGoingForBall)
        {
          flag1 = true;
        }
        else
        {
          float num1 = Mathf.Max(Time.deltaTime, 0.05f);
          Vector3 position3 = this._trans.position;
          Vector3 position4 = BallPhysicsSimulation.EvaluatePosition(time + num1);
          Quaternion rotation2 = this._trans.rotation;
          Vector3 vector3_2 = this._animator.velocity * num1;
          Vector3 vector3_3 = this._animator.angularVelocity * num1;
          Vector3 vector3_4 = position3 + rotation2 * vector3_2;
          Vector3 playerFwd = this._trans.forward + rotation2 * vector3_3;
          float num2 = (double) Time.timeScale < (double) Mathf.Epsilon ? Mathf.Epsilon : Time.timeScale;
          int num3 = this._visibilityConfig.IsBallPositionCandidateInsideForwardVisibilityRadius(vector3_4, playerFwd, this._animator.velocity / num2, position4) ? 1 : 0;
          Vector2 point = new Vector2(position4.x, position4.z);
          bool flag3 = CatchAbility._fieldRect.Contains(point);
          bool flag4 = (double) position4.y >= (double) this.ScaledConfortableCatchHeight && (double) position4.y <= (double) this.ScaledMaxCatchHeight;
          float num4 = Vector3.Distance(position1, position3);
          bool flag5 = (double) Vector3.Distance(position4, vector3_4) < (double) num4;
          int num5 = flag3 ? 1 : 0;
          flag1 = (num3 & num5 & (flag4 ? 1 : 0) & (flag5 ? 1 : 0)) == 0 || CatchAbility.IsDefenderCloseAndDownfield(position1, normalized, opponents);
          if (!flag1)
          {
            float cost;
            AnimationEventController animationEventController = CatchAbility.RunCatchSelection(affineTransform, playerAI, this, out cost);
            this._delayedCatchCandidate = (double) cost <= (double) this._maxAcceptableCost ? animationEventController : (AnimationEventController) null;
            this._delayedCatchFrame = Time.frameCount;
          }
        }
      }
    }
    else
    {
      bool flag6 = (double) Vector3.Dot(this._trans.forward.normalized, ballManager.transform.position - this._trans.position) < -0.20000000298023224;
      flag1 = flag2 && !flag6;
    }
    if (!flag1)
      return;
    float cost1;
    AnimationEventController animationEventController1 = CatchAbility.RunCatchSelection(affineTransform, playerAI, this, out cost1);
    bool flag7 = this._delayedCatchFrame == Time.frameCount - 1 && (UnityEngine.Object) this._delayedCatchCandidate != (UnityEngine.Object) null;
    if (!((double) cost1 <= (double) this._maxAcceptableCost | flag7))
      return;
    if ((double) cost1 > (double) this._maxAcceptableCost & flag7)
      animationEventController1 = this._delayedCatchCandidate;
    this.SetIsGoingForBall(true);
    float targetHeight = animationEventController1.keyEvent.absoluteLocation.y * this.transform.lossyScale.y;
    float timeAtHeight;
    if (BallPhysicsSimulation.GetLastTimeAtHeight(ballManager.rigidbd, targetHeight, out timeAtHeight))
    {
      Vector3 position5 = BallPhysicsSimulation.EvaluatePosition(timeAtHeight);
      affineTransform.position = position5;
      this._eventAgent.EnterEvent(affineTransform, animationEventController1, timeAtHeight - Time.time);
    }
    else
      this._eventAgent.EnterEvent(affineTransform, animationEventController1);
  }

  public bool IsAttemptingCatchOrFumbleRecovery
  {
    get
    {
      if (!this._eventAgent.IsInsideEvent)
        return false;
      return this._isAttemptingCatch || this._isAttemptingFumbleRecovery;
    }
  }

  public bool IsAttemptingFumbleRecovery => this._eventAgent.IsInsideEvent && this._isAttemptingFumbleRecovery;

  private static AnimationEventController RunCatchSelection(
    AffineTransform catchAT,
    PlayerAI playerAI,
    CatchAbility catchAbility,
    out float cost)
  {
    bool flag1 = CatchAbility._midfieldRect.Contains(new Vector2(catchAT.position.x, catchAT.position.z));
    Vector3 vector3_1 = 3f * Vector3.forward;
    Vector3 vector3_2 = playerAI.trans.position + playerAI.trans.rotation * vector3_1;
    Vector2 point1 = new Vector2(vector3_2.x, vector3_2.z);
    bool flag2 = !CatchAbility._fieldRect.Contains(point1);
    AnimationEventScenario animationEventScenario = !global::Game.IsSpecialTeamsPlay ? (!flag1 & flag2 ? catchAbility._sidelineCatches : (playerAI.onOffense ? catchAbility._offensiveCatches : catchAbility._defensiveCatches)) : catchAbility._specialTeamsCatches;
    CatchSelectionDataBox extraDataBox = new CatchSelectionDataBox();
    extraDataBox.fieldBoundsRect = CatchAbility._fieldRect;
    int num = playerAI.onOffense ? global::Game.OffensiveFieldDirection : -global::Game.OffensiveFieldDirection;
    extraDataBox.preferredWorldExitDirection = Vector3.forward * (float) num;
    Vector2 point2 = new Vector2(catchAT.position.x, catchAT.position.z);
    bool flag3 = CatchAbility._northEndZone.Contains(point2);
    bool flag4 = CatchAbility._southEndZone.Contains(point2);
    bool flag5 = playerAI.onOffense && (global::Game.OffensiveFieldDirection > 0 & flag3 || global::Game.OffensiveFieldDirection < 0 & flag4);
    extraDataBox.factorInTargetExitDir = !flag5;
    float cost1;
    AnimationEventController animationEventController = animationEventScenario.SelectController(catchAT, catchAbility._trans, out cost1, (object) extraDataBox);
    cost = cost1;
    return animationEventController;
  }

  private void OnCollisionEnter(Collision collisionInfo)
  {
    if (collisionInfo.gameObject.layer == CatchAbility._playerCapsuleLayerIndex)
      this.HandleCollisionWithPlayer(collisionInfo);
    if (!((UnityEngine.Object) collisionInfo.rigidbody == (UnityEngine.Object) SingletonBehaviour<BallManager, MonoBehaviour>.instance.rigidbd))
      return;
    this.HandleCollisionWithBall();
  }

  private void HandleCollisionWithPlayer(Collision collisionInfo)
  {
    if (!global::Game.IsPlayActive || !this._isAttemptingFumbleRecovery || (UnityEngine.Object) MatchManager.instance.playersManager.ballHolderScript != (UnityEngine.Object) this._playerAI || this.CatchOutcome != CatchOutcomeTracker.ECatchOutcome.CaughtInBounds)
      return;
    PlayerAI component = collisionInfo.gameObject.GetComponent<PlayerAI>();
    if ((UnityEngine.Object) component == (UnityEngine.Object) null || this._playerAI.onOffense == component.onOffense)
      return;
    MatchManager.instance.EndPlay(PlayEndType.Tackle);
  }

  private void HandleCollisionWithBall()
  {
    if ((EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.PlayersHands || MatchManager.instance.playManager.savedOffPlay.GetPlayType() == PlayType.FG || MatchManager.instance.playManager.savedOffPlay.GetPlayType() == PlayType.Punt || (double) this._lastCollisionIndividualTime + 2.0 > (double) Time.time)
      return;
    if ((double) CatchAbility._lastCollisionGlobalTime + 0.5 > (double) Time.time)
      SingletonBehaviour<BallManager, MonoBehaviour>.instance.DisableCollider();
    Debug.Log((object) ("Ball hit player " + this.name));
    CatchAbility._lastCollisionGlobalTime = Time.time;
    this._lastCollisionIndividualTime = Time.time;
    if ((EBallState) (Variable<EBallState>) Ball.State.BallState != EBallState.InAirDeflected)
      Ball.State.BallState.SetValue(EBallState.InAirDeflected);
    PlayerAI.NotifyAllPlayersOfDeflectedPass();
  }
}
