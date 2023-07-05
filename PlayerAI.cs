// Decompiled with JetBrains decompiler
// Type: PlayerAI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using BehaviorDesigner.Runtime;
using FootballVR;
using Framework;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12;
using UDB;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAI : MonoBehaviour
{
  public GameObject mainGO;
  public AnimatorCommunicator animatorCommunicator;
  public IAnimatorQuerries iAnimQuerries;
  public Transform trans;
  public CapsuleCollider capCollider;
  public MotionTracker motionTracker;
  public Transform headJoint;
  public const float TIME_UNDERPRESSURE_FORCETHROW = 2.5f;
  public const float TIME_UNDERPRESSURE_DANGER = 0.2f;
  public const float MIN_DIST_FOR_PRESSURE = 4f;
  public const float PASS_RUSH_SCORE_FOR_PANIC = 80f;
  public const float PASS_RUSH_REACT_DELAY = 1.5f;
  public CelebrationManager celebrations;
  private static BallManager ballManager;
  public static int numberOfPlayersInHuddle_Off;
  public static int numberOfPlayersInHuddle_Def;
  public static bool justCaughtPass = false;
  public static float qbTimeUnderPressure;
  public static int flipVal;
  public static float normalColliderHeight;
  [Header("Pass Block Settings")]
  public static float center_passBlockSpeed = 1.0200001f;
  public static float guard_passBlockSpeed = 1.0200001f;
  public static float tackle_passBlockSpeed = 1.5f;
  public static float rb_passBlockSpeed = 3f;
  public static float te_passBlockSpeed = 2.1f;
  public Stack<PlayAssignment> PlayAssignments;
  public GameObject playerMeshGroup;
  public AvatarGraphics avatarGraphics;
  public Behavior behaviorTree;
  public Behavior duringPlaybehaviorTree;
  public int catching;
  public bool hasAttemptedCatch;
  public PlayerAI.OnBallEventRequested onBallCatchRequested;
  public PlayerAI.OnBallEventRequested onPuntOrKickReturnEventRequested;
  public PlayerAI.OnBallEventRequested onBallPickUpRequested;
  public PlayerAI.OnBallEventRequested onBallSwatRequested;
  public AnimationEventAgent eventAgent;
  public bool isBeingSubbed;
  [HideInInspector]
  public int blockBreaking;
  [HideInInspector]
  public int tackling;
  [HideInInspector]
  public int fitness;
  [HideInInspector]
  public int awareness;
  [HideInInspector]
  public int agility;
  [HideInInspector]
  public int coverage;
  [HideInInspector]
  public int hitPower;
  [HideInInspector]
  public int endurance;
  [HideInInspector]
  public int discipline;
  [HideInInspector]
  public int height;
  [HideInInspector]
  public int weight;
  [HideInInspector]
  public int tackleBreaking;
  [HideInInspector]
  public int fumbling;
  [HideInInspector]
  public int blocking;
  [HideInInspector]
  public int throwingAccuracy;
  [HideInInspector]
  public int throwingPower;
  [HideInInspector]
  public int kickingPower;
  [HideInInspector]
  public int kickingAccuracy;
  [HideInInspector]
  public int isLeftHanded;
  [HideInInspector]
  public int tackleType;
  [HideInInspector]
  public int sprint;
  [HideInInspector]
  public int posInBlockForm;
  [HideInInspector]
  public float blockDuration;
  [HideInInspector]
  public float reactionDelayToReceiverCut;
  [HideInInspector]
  public int indexInFormation;
  [HideInInspector]
  public int number;
  [HideInInspector]
  public int indexOnTeam;
  [HideInInspector]
  public int savedStance;
  [HideInInspector]
  public int teamIndex;
  [HideInInspector]
  public EPlayAssignmentId defType;
  [HideInInspector]
  public float fatigue;
  [HideInInspector]
  public float speed;
  [HideInInspector]
  public float savedSpeed;
  [HideInInspector]
  public float distanceToBlocker;
  [HideInInspector]
  public float blockDirection;
  [HideInInspector]
  public bool onUserTeam;
  [HideInInspector]
  public bool lookForBlockTarget;
  [HideInInspector]
  public bool isEngagedInBlock;
  [HideInInspector]
  public bool onOffense;
  [HideInInspector]
  public bool inBlockZone;
  [HideInInspector]
  public bool isLocalAvoiding;
  [HideInInspector]
  public bool hasDeflectedPass;
  [HideInInspector]
  public PlayerAI initialBlockTarget;
  [HideInInspector]
  public PlayerAI blockerAssignedToThisDefender;
  [HideInInspector]
  public bool hasShiftedStartingPosition;
  [HideInInspector]
  public bool isTackling;
  [HideInInspector]
  public bool affectedByMove;
  [HideInInspector]
  public bool droppedPass;
  [HideInInspector]
  public GameObject inBlockWith;
  [HideInInspector]
  public PlayerAI inBlockWithScript;
  [HideInInspector]
  public PlayerAI coveringPlayerScript;
  [HideInInspector]
  public PlayerAI blockTarget;
  [HideInInspector]
  public float blockTargetScore;
  [HideInInspector]
  public string firstName;
  [HideInInspector]
  public string lastName;
  [HideInInspector]
  public string playerName;
  [HideInInspector]
  public string position;
  [HideInInspector]
  public Position playerPosition;
  [HideInInspector]
  public Position formationPosition;
  [HideInInspector]
  public bool showingBlitz;
  private static float speedDampTime = 0.2f;
  private static float directionDampTime = 0.4f;
  private static float strafeSpeedDampTime = 0.2f;
  private static float speedDecay = 6f;
  private static float directionDecay = 0.6f;
  private static float qbRunPlayTurnSpeed = 10f;
  private static float lookAtIKWeightSpeed = 5f;
  private static float handIKWeightSpeed = 5f;
  private static float baseTimerCountdown = 30f;
  private static float radiusOfSatisfaction;
  private static float runPlaySpeed = 0.825f;
  private static float qb3StepDropSpeed = -2.7f;
  private static float qb5StepDropSpeed = -1.3f;
  private static int layerMask;
  private static float postplayHuddleForceTime = 2f;
  private static float minAfterPlayDelay = 0.25f;
  private static float maxAfterPlayDelay = 0.5f;
  private static float minAfterPlayRunSpeed = 0.25f;
  private static float maxAfterPlayRunSpeed = 0.75f;
  public static float MINLOOKFORBLOCKTIME = 0.25f;
  public static float PASS_BLOCK_LOOKFORBLOCK_DIST_DROPPING_BACK = 1.5f;
  public static float PASS_BLOCK_LOOKFORBLOCK_DIST_AT_DEPTH = 2f;
  public static float LOOKFORBLOCKTURNSPEED = 14f;
  public static float PLAYERDRAG_HACK = 4f;
  public static bool bShowBlockDebug = true;
  private static float distanceToBeginTackle;
  private static float qbDropbackSpeed;
  private static float normalColliderRadius;
  public const float LOCO_WALK_EFFORT = 0.3f;
  public const float LOCO_JOG_EFFORT = 0.65f;
  public const float LOCO_RUN_EFFORT = 0.8f;
  public const float LOCO_SPRINT_EFFORT = 1f;
  public static float FUMBLE_CHANCE = 0.01f;
  [SerializeField]
  private Playbook _playbookP1;
  private Vector3 _sidelinePosition;
  public NteractAgent nteractAgent;
  public TackleAbility tackleAbility;
  public ThrowAbility throwAbility;
  [SerializeField]
  private CatchAbility _catchAbility;
  [SerializeField]
  private PlayInitiationAbility _playInitiationAbility;
  [SerializeField]
  private BallPossessionAbility _ballPossessionAbility;
  private FakeAbility _fakeAbility;
  private bool _isTackled;
  private int frameCounter;
  private float _aIDecisionTimer;
  private AiTimingStore _aiTimingStore;
  private float _aiTimingInterval;
  public bool attackBallHolder;
  private const float CelebrationHuddleRadius = 8f;
  private Vector3 initialPosition;
  private Vector3 initialEulerAngles;
  private bool isGoingForThrownBall;
  private int pathIndex;
  private int tackleTimer;
  private float lookAtIKWeightTarget;
  private float reactionTimer;
  private bool hasReactedToPass;
  private float routeSpeedModifier;
  private bool foundHole;
  private float[] path;
  private float savedDot;
  private float passBlockSpeed;
  private Vector3 playStartPosition;
  private bool qbIsDroppingBack;
  private bool afterPlayInstructionsGiven;
  private bool IsBeforePlayInitialized;
  private bool IsDuringPlayOffInitialized;
  private bool IsDuringPlayDefInitialized;
  private bool IsAfterPlayInitialized;
  private float afterPlayDelay;
  private float afterPlayTime;
  private bool hasFinalAfterPlayGoal;
  private float speedOnStart;
  private float accelerationOnStart = 0.1f;
  private float distAdj;
  private float heightAdj;
  private bool detectedSnap;
  private RoutineHandle _putBallInCentersHandsRoutine = new RoutineHandle();
  private static Vector3[] offensiveHuddlePositions = new Vector3[11]
  {
    new Vector3(-1.82f, 0.0f, -0.2f),
    new Vector3(-1.34f, 0.0f, 0.43f),
    new Vector3(-0.44f, 0.0f, 0.4f),
    new Vector3(0.46f, 0.0f, 0.44f),
    new Vector3(1.38f, 0.0f, 0.45f),
    new Vector3(0.0f, 0.0f, -1.2f),
    new Vector3(1f, 0.0f, -1.8f),
    new Vector3(1.8f, 0.0f, -0.23f),
    new Vector3(-2.04f, 0.0f, -1.1f),
    new Vector3(2.14f, 0.0f, -1.1f),
    new Vector3(-1f, 0.0f, -2.2f)
  };
  public static Vector3[] defensiveHuddlePositions = new Vector3[11]
  {
    new Vector3(-2.48f, 0.0f, 1.11f),
    new Vector3(-1.45f, 0.0f, 1.08f),
    new Vector3(1f, 0.0f, 1.1f),
    new Vector3(1.86f, 0.0f, 2f),
    new Vector3(-1.2f, 0.0f, 2.1f),
    new Vector3(0.0f, 0.0f, 0.5f),
    new Vector3(2.6f, 0.0f, 2.64f),
    new Vector3(-4.2f, 0.0f, 2.33f),
    new Vector3(0.6f, 0.0f, 3f),
    new Vector3(-2.2f, 0.0f, 3.9f),
    new Vector3(3.9f, 0.0f, 2.25f)
  };
  public static float[] offensiveHuddleRotations = new float[11]
  {
    22f,
    45f,
    75f,
    -250f,
    -225f,
    -90f,
    -135f,
    -200f,
    -25f,
    -155f,
    -55f
  };
  public static float[] defensiveHuddleRotations = new float[11]
  {
    50f,
    55f,
    105f,
    120f,
    75f,
    -90f,
    115f,
    45f,
    80f,
    50f,
    125f
  };
  private static WaitForSeconds _lookAtHandoffTrgtDelay = new WaitForSeconds(0.25f);
  private static WaitForSeconds _centerPickUpBallDelay = new WaitForSeconds(1f);
  private static WaitForSeconds _qbStanceDelay = new WaitForSeconds(0.5f);
  private static WaitForSeconds _beginDropback = new WaitForSeconds(0.2f);
  private static WaitForSeconds _endDropback = new WaitForSeconds(0.4f);
  private static bool usePredictedTacklerPos = true;
  private static int[] holeLocations = new int[75];
  private static int[] holeLocationValues = new int[75];
  private static WaitForSeconds _justCaughtPassWFS = new WaitForSeconds(1f);
  private WaitForSeconds _gotoCatchPosDelay;
  private float _lastBrokenTackleTime;
  private const float _brokenTackleRecoveryTime = 5f;
  private static float _maxSuccessfulTackleUserDistanceFromAIQB = 1.25f;
  [SerializeField]
  private BlockAbility _blockAbility;
  private static List<PlayerAI> validBlockers = new List<PlayerAI>();
  private static List<PlayerAI> validBlockTargets = new List<PlayerAI>();
  private const float LeadBlockTargetAngle = 15f;
  private const float RunBlockTargetAngle = 22f;
  private const float PassDropBackBlockTargetAngle = 45f;
  private const float PassBlockTargetAngle = 90f;
  private const float OpenFieldBlockTargetAngle = 60f;
  private float _cachedBlockDuration;
  private PlayerAI _cachedBlockedDefender;
  private static Vector2 _easyDifficultyLoopDurationRange = new Vector2(2f, 7.78f);
  private static Vector2 _mediumDifficultyLoopDurationRange = new Vector2(1.5f, 6.28f);
  private static Vector2 _hardDifficultyLoopDurationRange = new Vector2(0.6f, 4.28f);
  private static float _defenderEarlyExitChance = 0.15f;
  private static float SAME_LEVEL_THRESHOLD = 2f;
  private readonly UnityEngine.RaycastHit[] _cachedHits = new UnityEngine.RaycastHit[3];
  private static bool sDebugAvoidance = true;
  public static float MAX_PASS_RUSH_SCORE = 100f;
  public static float MIN_PASS_RUSH_SCORE = 20f;
  public static float BASE_THROW_THESHOLD = 7.3f;
  public static float THROW_NOW_THRESHOLD = 5.8f;
  private static int[] receiverScore = new int[5];

  public void PIPutPlayerInPlayPosition(bool movePlayer = true) => this.PutPlayerInPlayPosition(movePlayer);

  public bool PULocalAvoidance(ref Vector3 lookDir) => this.LocalAvoidance(ref lookDir);

  public static float SpeedDampTime => PlayerAI.speedDampTime;

  public static float DirectionDampTime => PlayerAI.directionDampTime;

  public static float StrafeSpeedDampTime => PlayerAI.strafeSpeedDampTime;

  public static float SpeedDecay => PlayerAI.speedDecay;

  public static float DirectionDecay => PlayerAI.directionDecay;

  public static float QBRunPlayTurnSpeed => PlayerAI.qbRunPlayTurnSpeed;

  public static float LookAtIKWeightSpeed => PlayerAI.lookAtIKWeightSpeed;

  public static float HandIKWeightSpeed => PlayerAI.handIKWeightSpeed;

  public static float BaseTimerCountdown => PlayerAI.baseTimerCountdown;

  public static float RadiusOfSatisfaction => PlayerAI.radiusOfSatisfaction;

  public float LookAtIKWeightTarget => this.lookAtIKWeightTarget;

  public float HandIKWeightTarget => PlayerAI.handIKWeightSpeed;

  public static HashIDs Hash => PlayerAI.hash;

  public bool IsGoingForThrownBall => this.isGoingForThrownBall;

  private static MatchManager matchManager => MatchManager.instance;

  private static PlayersManager playersManager => MatchManager.instance.playersManager;

  private static PlayManager playManager => MatchManager.instance.playManager;

  public static event System.Action OnUserQBInHuddle;

  public PlayAssignment CurrentPlayAssignment => this.GetCurrentAssignment();

  public BlockType blockType { get; set; }

  public bool LeftHanded => this.isLeftHanded > 0;

  public static HashIDs hash { get; private set; }

  public BallPossessionAbility BallPossession => this._ballPossessionAbility;

  public float currentAdjustedAnimSpeedForPass { get; private set; }

  public CatchOutcomeTracker.ECatchOutcome CatchOutcome => this._catchAbility.CatchOutcome;

  public bool IsAttemptingCatchOrFumbleRecovery => this._catchAbility.IsAttemptingCatchOrFumbleRecovery;

  public bool IsAttemptingFumbleRecovery => this._catchAbility.IsAttemptingFumbleRecovery;

  public LayerMask TackleLayerMask => (LayerMask) PlayerAI.layerMask;

  public event PlayerAI.OnIsTackledEvent IsTackledEvent;

  public bool IsTackled
  {
    get => this._isTackled;
    set
    {
      this._isTackled = value;
      PlayerAI.OnIsTackledEvent isTackledEvent = this.IsTackledEvent;
      if (isTackledEvent == null)
        return;
      isTackledEvent(value);
    }
  }

  public Vector3 Velocity => (UnityEngine.Object) this.motionTracker != (UnityEngine.Object) null ? this.motionTracker.Velocity : this.trans.forward;

  public event Action<float[]> OnPlayStart;

  public event Action<float[]> OnInPrePlayPosition;

  public float AITimingInterval => this._aiTimingInterval;

  public float[] Path => this.path;

  public PlayerAI ManCoverTarget => !(this.CurrentPlayAssignment is ManDefenseAssignment currentPlayAssignment) ? (PlayerAI) null : currentPlayAssignment.GetCoverageOn();

  public PlayerAI BallHolderPursuitTarget
  {
    get
    {
      PlayerAI targetIfNotFaked = PlayerAI.playersManager.ballHolderScript;
      if ((UnityEngine.Object) this._fakeAbility != (UnityEngine.Object) null)
        targetIfNotFaked = this._fakeAbility.GetFakeTarget(targetIfNotFaked);
      return targetIfNotFaked;
    }
  }

  private void Awake()
  {
    this.iAnimQuerries = this.GetComponent<IAnimatorQuerries>();
    PlayerAI.flipVal = 1;
    this.initialPosition = this.transform.position;
    this.initialEulerAngles = this.transform.eulerAngles;
    this.savedStance = 0;
    this.tackleType = 0;
    this.pathIndex = 0;
    this.lookAtIKWeightTarget = 0.0f;
    this.posInBlockForm = -1;
    this.inBlockZone = true;
    PlayerAI.layerMask = LayerMask.GetMask("PlayerCapsule");
    this.SetAiTimingStoreReference();
    this.SetReferences();
    this.celebrations = new CelebrationManager(this.animatorCommunicator);
    this.behaviorTree = this.GetComponent<Behavior>();
    this.behaviorTree.EnableBehavior();
    this.detectedSnap = false;
    this.PlayAssignments = new Stack<PlayAssignment>();
    this._fakeAbility = this.GetComponent<FakeAbility>();
    this._ballPossessionAbility.Initiate(this, PlayerAI.ballManager);
    this.motionTracker = this.GetComponent<MotionTracker>();
  }

  private void Start()
  {
    if (AppState.GameMode == EGameMode.kTunnel)
      return;
    this.behaviorTree.SetVariableValue("OwnerPlayer", (object) this.gameObject);
    this._aIDecisionTimer = 0.0f;
    MatchManager.instance.playManager.OnPlayActiveEvent += new Action<bool>(PlayerAI.UpdateBehaviorTreeVar_PlayActive);
    PlayState.PlayOver.OnValueChanged += new Action<bool>(PlayerAI.UpdateBehaviorTreeVar_PlayOver);
  }

  private void OnEnable()
  {
    if ((UnityEngine.Object) MatchManager.instance.playManager == (UnityEngine.Object) null || (UnityEngine.Object) MatchManager.instance == (UnityEngine.Object) null)
      return;
    MatchManager.instance.playManager.OnPlayActiveEvent += new Action<bool>(this.PlayActiveChanged);
    this.animatorCommunicator.enabled = true;
    this.animatorCommunicator.atHuddlePosition = false;
    this.animatorCommunicator.atPlayPosition = false;
    PlayerAI.radiusOfSatisfaction = Field.ONE_HALF_YARD;
    PlayerAI.normalColliderHeight = 2f;
    PlayerAI.normalColliderRadius = 0.35f;
    PlayerAI.distanceToBeginTackle = Field.TWO_YARDS + Field.FIFTEEN_INCHES;
  }

  private void OnDisable()
  {
    Debug.Log((object) "PlayerAI Disabled");
    if (!MatchManager.Exists())
      return;
    MatchManager.instance.playManager.OnPlayActiveEvent -= new Action<bool>(this.PlayActiveChanged);
    this.animatorCommunicator.enabled = false;
  }

  public void SetReferences()
  {
    PlayerAI.hash = HashIDs.self;
    PlayerAI.ballManager = PlayerAI.matchManager.ballManager;
  }

  public void SetPlayerIndex(int index)
  {
    this.indexInFormation = index;
    this.animatorCommunicator.indexInFormation = index;
  }

  public void SetTeamIndex(int value) => this.teamIndex = value;

  public static void InitializeBehaviorTree()
  {
    PlayerAI.UpdateBehaviorTreeVar_PlayActive(false);
    PlayerAI.UpdateBehaviorTreeVar_PlayOver(false);
  }

  public static void UpdateBehaviorTreeVar_PlayOver(bool val) => GlobalVariables.Instance.SetVariable("PlayState_PlayOver", (SharedVariable) (SharedBool) val);

  public static void UpdateBehaviorTreeVar_PlayActive(bool val) => GlobalVariables.Instance.SetVariable("Game_IsPlayActive", (SharedVariable) (SharedBool) val);

  public void PlayActiveChanged(bool playActive)
  {
    int num = playActive ? 1 : 0;
  }

  public void ExecuteKick() => PlayerAI.playersManager.ExecuteKick();

  public void ExecuteThrow()
  {
    if (this.IsTackled || global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB)
      return;
    PlayerAI.playersManager.ExecuteThrow();
  }

  public void DelayedFinishQBKneel() => this.Invoke("FinishQBKneel", 1f);

  public void FinishQBKneel()
  {
    if (!global::Game.IsPlayActive)
      return;
    PlayerAI.matchManager.EndPlay(PlayEndType.Tackle);
  }

  public void DelayedSpikeBall() => this.Invoke("SpikeBall", 1f);

  public void SpikeBall()
  {
    if (!global::Game.IsPlayActive)
      return;
    PlayerAI.playersManager.BallHolderReleaseBall();
    PlayerAI.ballManager.SetParent((Transform) null);
    PlayerAI.ballManager.SetPosition(new Vector3(this.trans.position.x, Ball.BALL_ON_GROUND_HEIGHT, this.trans.position.z + Field.ONE_YARD * (float) global::Game.OffensiveFieldDirection));
    PlayerAI.ballManager.SetAngularVelocity(Vector3.zero);
    PlayerAI.matchManager.EndPlay(PlayEndType.Incomplete);
  }

  public void PickUpBallFromGround()
  {
    if (global::Game.IsPlayOver || global::Game.BS_IsPlayersHands || global::Game.CurrentPlayHasUserQBOnField && this.IsQB())
      return;
    if (this.onOffense && global::Game.IsPunt)
    {
      int key = this.onUserTeam ? MatchManager.instance.playersManager.curUserScriptRef[6].indexOnTeam : MatchManager.instance.playersManager.curCompScriptRef[6].indexOnTeam;
      PlayerStats playerStats = this.onUserTeam ? MatchManager.instance.playManager.userTeamCurrentPlayStats.players[key] : MatchManager.instance.playManager.compTeamCurrentPlayStats.players[key];
      float distance = this.trans.position.z - MatchManager.instance.savedLineOfScrim;
      ++playerStats.Punts;
      playerStats.PuntYards += Field.ConvertDistanceToYards(distance);
      if ((double) MatchManager.instance.playManager.deflectedPassTimer > 0.0)
      {
        MatchManager.instance.playersManager.SetBallHolder(this.mainGO, this.onUserTeam);
        if (!global::Game.UserControlsPlayers)
          return;
        MatchManager.instance.playersManager.SetUserPlayer(this.indexInFormation);
      }
      else
      {
        if (Field.FurtherDownfield(this.trans.position.z, Field.OPPONENT_TWENTY_YARD_LINE))
          ++playerStats.PuntsInside20;
        MatchManager.turnover = true;
        MatchManager.instance.EndPlay(PlayEndType.OOB);
      }
    }
    else
    {
      if (global::Game.IsRunOrPass)
      {
        if (global::Game.IsPlayerOneOnDefense && this.onUserTeam)
        {
          ++MatchManager.instance.playManager.userTeamCurrentPlayStats.players[this.indexOnTeam].FumbleRecoveries;
          ++ProEra.Game.MatchState.Stats.User.FumbleRecoveries;
          AppSounds.PlayCrowdReaction(true, AppSounds.CrowdReactionSizes.Big);
        }
        else if (global::Game.IsPlayerOneOnOffense && !this.onUserTeam)
        {
          ++MatchManager.instance.playManager.compTeamCurrentPlayStats.players[this.indexOnTeam].FumbleRecoveries;
          ++ProEra.Game.MatchState.Stats.Comp.FumbleRecoveries;
          AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Big);
        }
      }
      else if (global::Game.IsPunt)
      {
        int key = this.onUserTeam ? MatchManager.instance.playersManager.curCompScriptRef[6].indexOnTeam : MatchManager.instance.playersManager.curUserScriptRef[6].indexOnTeam;
        PlayerStats playerStats = this.onUserTeam ? MatchManager.instance.playManager.userTeamCurrentPlayStats.players[key] : MatchManager.instance.playManager.compTeamCurrentPlayStats.players[key];
        float distance = this.trans.position.z - MatchManager.instance.savedLineOfScrim;
        ++playerStats.Punts;
        playerStats.PuntYards += Field.ConvertDistanceToYards(distance);
        if (Field.FurtherDownfield(this.trans.position.z, Field.OPPONENT_TWENTY_YARD_LINE))
          ++playerStats.PuntsInside20;
      }
      MatchManager.instance.playersManager.SetBallHolder(this.mainGO, this.onUserTeam);
    }
  }

  public void RecoverBallOnKickoff()
  {
    bool flag = false;
    if (this.onOffense && !global::Game.BS_IsFumble)
    {
      if (!PlayerAI.ballManager.WasDeflected && Field.FurtherDownfield((float) ProEra.Game.MatchState.BallOn + Field.ONE_YARD_FORWARD * 10f, PlayerAI.ballManager.trans.position.z))
        ProEra.Game.MatchState.Turnover.SetValue(true);
      flag = true;
    }
    MatchManager.instance.playersManager.SetBallHolder(this.mainGO, this.onUserTeam);
    if (!flag)
      return;
    MatchManager.instance.EndPlay(PlayEndType.OOB);
  }

  public async void PlaySound(int i)
  {
  }

  public void SetVolume(float vol)
  {
  }

  public void ResetSpeedModifier() => this.SetRouteSpeedModifier(0.0f);

  private void SetRouteSpeedModifier(float spd) => this.routeSpeedModifier = spd;

  public void SetLookAtTarget(Transform _target, float _weight) => this.lookAtIKWeightTarget = _weight;

  private void ManageLookAtIKWeight()
  {
  }

  public void ResetIK() => this.SetLookAtTarget((Transform) null, 0.0f);

  public static Quaternion LookAtBallRotation(Vector3 lookFromPosition) => Quaternion.LookRotation(((PlayerAI.ballManager.trans.position - lookFromPosition) with
  {
    y = 0.0f
  }).normalized);

  private bool NeedToAvoid(PlayerAI detectedPlayer)
  {
    if ((UnityEngine.Object) PlayerAI.playersManager.ballHolderScript != (UnityEngine.Object) null && (UnityEngine.Object) PlayerAI.playersManager.ballHolderScript == (UnityEngine.Object) this)
      return true;
    if ((UnityEngine.Object) detectedPlayer == (UnityEngine.Object) null || global::Game.IsTurnover || (UnityEngine.Object) detectedPlayer == (UnityEngine.Object) this)
      return false;
    if (this.iAnimQuerries.IsStrafing())
      return true;
    if (detectedPlayer.isLocalAvoiding)
      return false;
    if (this.onOffense && (UnityEngine.Object) PlayerAI.playersManager.ballHolderScript != (UnityEngine.Object) null && (UnityEngine.Object) detectedPlayer == (UnityEngine.Object) PlayerAI.playersManager.ballHolderScript)
      return true;
    if (!this.onOffense && (UnityEngine.Object) PlayerAI.playersManager.ballHolderScript != (UnityEngine.Object) null && (UnityEngine.Object) detectedPlayer == (UnityEngine.Object) PlayerAI.playersManager.ballHolderScript || this.blockType != BlockType.None && this.onOffense && (UnityEngine.Object) this.blockTarget != (UnityEngine.Object) null && (UnityEngine.Object) this.blockTarget == (UnityEngine.Object) detectedPlayer)
      return false;
    Vector3 normalized = (this.trans.position - detectedPlayer.trans.position).normalized;
    return (double) Vector3.Dot(detectedPlayer.trans.forward, normalized) > 0.60000002384185791;
  }

  public void SetBeforePlayActions(
    Vector3 _position,
    int stance,
    PlayType playType,
    PlayTypeSpecific playTypeSpecific)
  {
    this.ResetCelebration();
    this.ResetIK();
    if (!this.animatorCommunicator.atHuddlePosition && !PlayerAI.playManager.ShouldOffenseHurryUp)
    {
      int num = (UnityEngine.Object) global::Game.OffensiveQB == (UnityEngine.Object) this ? 1 : 0;
      this.PutPlayerInHuddlePosition(false);
    }
    this.SetPlayStartingPosition(_position);
    this.savedStance = stance;
    this.animatorCommunicator.atHuddlePosition = false;
    this.showingBlitz = false;
  }

  public void SetPlayStartingPosition(Vector3 _position)
  {
    this.playStartPosition = _position;
    this.animatorCommunicator.SetGoal(this.playStartPosition, this.StanceTargetRotation());
  }

  public Vector3 GetPlayStartPosition() => this.playStartPosition;

  public void InitBeforePlay()
  {
    if (global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB && this.IsQB() && global::Game.CurrentPlayHasUserQBOnField)
      this.animatorCommunicator.SetGoal(this.playStartPosition, this.transform.rotation);
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) (int) this.GetCurrentAssignId());
    this.behaviorTree.SetVariableValue("OwnerPlayer", (object) this.gameObject);
    this.behaviorTree.SetVariableValue("DoDefenseShift", (object) false);
    this.behaviorTree.SetVariableValue("DoShowBlitz", (object) false);
    this.behaviorTree.SetVariableValue("IsPlayerOffense", (object) this.onOffense);
    if (this.animatorCommunicator.atPlayPosition || PlayerAI.playManager.IsUserRunningHurryUp)
    {
      this.behaviorTree.SetVariableValue("GetToFormPos", (object) false);
      this.behaviorTree.SetVariableValue("AtLinePos", (object) true);
    }
    else
    {
      this.behaviorTree.SetVariableValue("GetToFormPos", (object) true);
      this.behaviorTree.SetVariableValue("AtLinePos", (object) false);
    }
  }

  public void RunBeforePlayActions()
  {
    if (!this.IsBeforePlayInitialized)
    {
      this.InitBeforePlay();
      this.IsBeforePlayInitialized = true;
      this.IsAfterPlayInitialized = false;
      this.IsDuringPlayOffInitialized = false;
      this.IsDuringPlayDefInitialized = false;
    }
    BehaviorManager.instance.Tick(this.behaviorTree);
  }

  private void PutPlayerInPlayPosition(bool movePlayer = true)
  {
    if (this.onOffense && this.indexInFormation == 2)
      this._putBallInCentersHandsRoutine.Run(this.PutBallInCentersHandsDelay());
    this.animatorCommunicator.Reset(this.playStartPosition, this.StanceTargetRotation(), false);
    this.foundHole = false;
    this.qbIsDroppingBack = false;
    this.attackBallHolder = false;
    this.droppedPass = false;
    this.isBeingSubbed = false;
    this.animatorCommunicator.atPlayPosition = true;
    this.animatorCommunicator.onOffense = this.onOffense;
    this.lookAtIKWeightTarget = 0.0f;
    this.ResetIK();
    this.animatorCommunicator.hasBall = false;
    this.lookForBlockTarget = false;
    this.IsTackled = false;
    this.isTackling = false;
    this.isEngagedInBlock = false;
    this.inBlockWith = (GameObject) null;
    this.inBlockWithScript = (PlayerAI) null;
    this.tackleTimer = 30;
    this.animatorCommunicator.IsPrePlay = true;
    this.animatorCommunicator.IsLeftSideOfBall = AIUtil.GetPlayerSide(this.trans.position.x - PlayerAI.matchManager.ballHashPosition, (float) global::Game.OffensiveFieldDirection) == LOS_Side.Side_Left;
    if ((UnityEngine.Object) global::Game.OffensiveQB == (UnityEngine.Object) this & movePlayer)
      MatchManager.instance.UpdateUserPlayerPosition();
    else if (this.blockType == BlockType.None && this.indexInFormation > 5 && this.onOffense)
    {
      Action<float[]> inPrePlayPosition = this.OnInPrePlayPosition;
      if (inPrePlayPosition != null)
        inPrePlayPosition(this.path);
    }
    if (!((UnityEngine.Object) this.behaviorTree != (UnityEngine.Object) null))
      return;
    this.behaviorTree.SetVariableValue("GetToFormPos", (object) false);
  }

  private IEnumerator PutBallInCentersHandsDelay()
  {
    yield return (object) PlayerAI._centerPickUpBallDelay;
    if (MatchManager.Exists() && (UnityEngine.Object) MatchManager.instance.playManager != (UnityEngine.Object) null)
      PlayerAI.playManager.PutBallInCentersHands();
  }

  public void StartPlayDelayed()
  {
    float delay = 0.0f;
    if (!global::Game.IsKickoff && !this.IsQB() && !this.IsCenter() && (UnityEngine.Object) MatchManager.instance.playManager.handOffTarget != (UnityEngine.Object) this)
    {
      float a;
      float b;
      if (this.onOffense)
      {
        if (this.IsLineman())
        {
          a = 0.05f;
          b = 0.15f;
        }
        else
        {
          a = 0.2f;
          b = 0.33f;
        }
      }
      else if (this.IsLineman())
      {
        a = 0.3f;
        b = 0.6f;
      }
      else
      {
        a = 0.2f;
        b = 0.35f;
      }
      float num = Mathf.Lerp(a, b, (float) ((100.0 - (double) this.awareness) / 100.0));
      delay = num + UnityEngine.Random.Range(0.0f, 0.3f) * num;
    }
    this.detectedSnap = false;
    this.StartCoroutine(this.StartPlay(delay));
  }

  private IEnumerator StartPlay(float delay)
  {
    yield return (object) new WaitForSeconds(delay);
    if (global::Game.IsKickoff && this.onOffense && !this.IsKicker())
      yield return (object) new WaitForSeconds(4.75f);
    Action<float[]> onPlayStart = this.OnPlayStart;
    if (onPlayStart != null)
      onPlayStart(this.path);
    this.afterPlayInstructionsGiven = false;
    this.EnableColliders();
    EPlayAssignmentId currentAssignId = this.GetCurrentAssignId();
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) (int) currentAssignId);
    if (this.onOffense)
    {
      if (currentAssignId == EPlayAssignmentId.None)
        this.SetNextPointInPathAsTarget();
    }
    else if (this.defType == EPlayAssignmentId.PuntKickReceive)
      this.animatorCommunicator.SetGoal(this.trans.position, this.StanceTargetRotation());
    else if (this.defType == EPlayAssignmentId.KickReturnBlocker && global::Game.IsPunt)
      this.SetNextPointInPathAsTarget();
    if (this.IsQB())
    {
      if (global::Game.IsRun || global::Game.IsPlayAction || global::Game.IsPass)
        this.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.QuaterbackStrafe, this.LeftHanded);
    }
    else if (global::Game.IsPitchPlayUnderCenter && this.onOffense && PlayerAI.playManager.savedOffPlay.GetHandoffTarget() == this.indexInFormation)
      this.SetLookAtTarget(PlayerAI.ballManager.trans, 0.7f);
    else if (global::Game.IsFG && this.onOffense && this.indexInFormation == 5)
      this.SetLookAtTarget(PlayerAI.ballManager.trans.parent, 1f);
    this.animatorCommunicator.atPlayPosition = false;
    this.animatorCommunicator.isPlayActive = true;
    this.detectedSnap = true;
    yield return (object) null;
  }

  private void HandlePreplay()
  {
    ++this.frameCounter;
    this._aIDecisionTimer += Time.deltaTime;
    if ((double) this._aIDecisionTimer <= (double) this._aiTimingInterval)
      return;
    this._aIDecisionTimer = 0.0f;
    this.frameCounter = 0;
    this.behaviorTree.SetVariableValue("target", (object) this.animatorCommunicator.CurrentGoal.position);
  }

  public void FinishPlay()
  {
    if (this._putBallInCentersHandsRoutine != null)
      this._putBallInCentersHandsRoutine.Stop();
    this._blockAbility.ExitBlock();
    this.animatorCommunicator.isPlayActive = false;
    this.animatorCommunicator.hasBall = false;
    this.animatorCommunicator.atPlayPosition = false;
    this.hasShiftedStartingPosition = false;
    this.IsTackled = false;
    this.isTackling = false;
    this.coveringPlayerScript = (PlayerAI) null;
    this.animatorCommunicator.speed = 1f;
    this.SetIsGoingForThrownBall(false);
    this.ResetIK();
    this.ResetBehaviorTree();
    this.detectedSnap = false;
  }

  public void SetAfterPlayActions()
  {
    this.hasShiftedStartingPosition = false;
    if (!((UnityEngine.Object) this.animatorCommunicator != (UnityEngine.Object) null))
      return;
    this.animatorCommunicator.SetStance(0);
    this.animatorCommunicator.atHuddlePosition = false;
    this.animatorCommunicator.atPlayPosition = false;
  }

  public (Vector3, Vector3) GetSidelineTransform() => (this.initialPosition, this.initialEulerAngles);

  public Vector3 GetHuddlePosition()
  {
    TeamData userTeam = PersistentData.GetUserTeam();
    TeamData compTeam = PersistentData.GetCompTeam();
    if (!this.onUserTeam)
      compTeam.GetName();
    else
      userTeam.GetName();
    if (!this.onUserTeam)
      userTeam.GetName();
    else
      compTeam.GetName();
    int playerPosition = (int) this.playerPosition;
    bool flag = global::Game.IsKickoff ? !this.onOffense : this.onOffense;
    Vector3 vector3_1 = new Vector3(PlayerAI.matchManager.ballHashPosition, 0.0f, MatchManager.ballOn);
    if (global::Game.PET_IsTouchdown)
      vector3_1.z = PersistentData.GetHomeTeamData().GetTeamPATLocation();
    if (flag)
    {
      vector3_1 += Vector3.forward * (float) (-(double) Field.ONE_YARD_FORWARD * 7.0);
    }
    else
    {
      vector3_1 += Vector3.forward * (Field.ONE_YARD_FORWARD * 5f);
      if (global::Game.IsRunningPAT)
        vector3_1.z = Field.OFFENSIVE_GOAL_LINE;
    }
    if (this._playbookP1.IsKickoffOrKickReturnPlay() && !flag)
      vector3_1 += Vector3.forward * (Field.ONE_YARD_FORWARD * 20f);
    if (flag)
      return vector3_1 + PlayerAI.offensiveHuddlePositions[this.indexInFormation] * (float) global::Game.OffensiveFieldDirection;
    Vector3 vector3_2 = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0.0f, UnityEngine.Random.Range(-1f, 1f));
    return vector3_1 + (PlayerAI.defensiveHuddlePositions[this.indexInFormation] + vector3_2) * (float) global::Game.OffensiveFieldDirection;
  }

  public Vector3 GetChampionHuddlePosition(Vector3 celebrationLocation) => celebrationLocation + new Vector3(Mathf.Lerp(-3.5f, 3.5f, (float) this.indexInFormation / 10f), 0.0f, UnityEngine.Random.Range(-3f, -8f));

  public void InitAfterPlay()
  {
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) (int) this.GetCurrentAssignId());
    this.behaviorTree.SetVariableValue("OwnerPlayer", (object) this.gameObject);
  }

  public void RunAfterPlayActions()
  {
    if (!this.IsAfterPlayInitialized)
    {
      this.InitAfterPlay();
      this.IsAfterPlayInitialized = true;
      this.IsBeforePlayInitialized = false;
      this.IsDuringPlayOffInitialized = false;
      this.IsDuringPlayDefInitialized = false;
    }
    BehaviorManager.instance.Tick(this.behaviorTree);
  }

  public Quaternion GetHuddleRotation() => !this.onOffense ? Quaternion.Euler(new Vector3(0.0f, PlayerAI.defensiveHuddleRotations[this.indexInFormation] + (float) (90 * global::Game.OffensiveFieldDirection), 0.0f)) : Quaternion.Euler(new Vector3(0.0f, PlayerAI.offensiveHuddleRotations[this.indexInFormation] + (float) (90 * global::Game.OffensiveFieldDirection), 0.0f));

  public Quaternion GetChampionHuddleRotation(Vector3 celebrationLocation) => Quaternion.identity;

  public void PlayerInHuddle()
  {
    if (this.onOffense)
      ++PlayerAI.numberOfPlayersInHuddle_Off;
    else
      ++PlayerAI.numberOfPlayersInHuddle_Def;
    if (this.celebrations.GetCelebrationCategory() != CelebrationCategory.None)
      this.celebrations.ResetCelebration();
    this.isBeingSubbed = false;
    this.animatorCommunicator.atHuddlePosition = true;
  }

  public void PutPlayerInHuddlePosition(bool movePlayer = true)
  {
    this.PlayerInHuddle();
    this.animatorCommunicator.Reset(this.GetHuddlePosition(), this.GetHuddleRotation(), false);
    this.animatorCommunicator.atHuddlePosition = true;
    if (((!global::Game.IsPlayerOneOnOffense || !((UnityEngine.Object) global::Game.OffensiveQB == (UnityEngine.Object) this) ? 0 : (AppState.GameMode != EGameMode.kAISimGameMode ? 1 : 0)) & (movePlayer ? 1 : 0)) == 0 && (!global::Game.IsPlayerOneOnDefense || this.indexInFormation != 5 || ProEra.Game.MatchState.Stats.CurrentDrivePlays != 0))
      return;
    MatchManager.instance.UpdateUserPlayerPosition();
  }

  public void ResetCelebration() => this.celebrations.ResetCelebration();

  public void SetCelebrationFromCategory(CelebrationCategory category, string celebrationEvent)
  {
    MonoBehaviour.print((object) (celebrationEvent + " Setting Celebration Category: " + category.ToString() + " for: " + this.playerName + " #" + this.number.ToString()));
    this.celebrations.SetCelebrationFromCategory(category);
  }

  public void SetToDefense()
  {
    this.onOffense = false;
    this.animatorCommunicator.onOffense = false;
  }

  public void SetupDefPlay(PlayAssignment defPlayAssignment, int flip, Position formPosition)
  {
    this.ClearAllAssignments();
    this.path = defPlayAssignment.GetRoutePoints();
    this.hasShiftedStartingPosition = false;
    this.blockerAssignedToThisDefender = (PlayerAI) null;
    PlayerAI.flipVal = flip;
    this.sprint = 90;
    this.distanceToBlocker = 100f;
    this.hasReactedToPass = false;
    this.formationPosition = formPosition;
    this.speed = this.savedSpeed;
    this.hasDeflectedPass = false;
    this.hasAttemptedCatch = false;
    this.animatorCommunicator.speed = 1f;
    this.pathIndex = 1;
    this.ResetSpeedModifier();
    this.defType = defPlayAssignment.GetAssignmentType();
    this.blockType = BlockType.None;
    this.reactionTimer = 0.0f;
    this.posInBlockForm = -1;
    switch (defPlayAssignment.GetAssignmentType())
    {
      case EPlayAssignmentId.ManCoverage:
        if (defPlayAssignment is ManDefenseAssignment copyDefAssign1)
        {
          ManDefenseAssignment assign = new ManDefenseAssignment(copyDefAssign1);
          if (assign.GetIsQBSpy())
            this.SetManDefense(5, assign);
          this.PlayAssignments.Push((PlayAssignment) assign);
          break;
        }
        Debug.LogError((object) " Error getting play assigment at ManDefenseAssignment");
        break;
      case EPlayAssignmentId.ZoneCoverage:
        if (defPlayAssignment is ZoneDefenseAssignment copyDefAssign2)
        {
          ZoneDefenseAssignment defenseAssignment = new ZoneDefenseAssignment(copyDefAssign2);
          defenseAssignment.SetZoneDefense(MatchManager.ballOn, (float) PlayerAI.flipVal);
          this.PlayAssignments.Push((PlayAssignment) defenseAssignment);
          break;
        }
        break;
      case EPlayAssignmentId.Blitz:
        this.PlayAssignments.Push((PlayAssignment) (defPlayAssignment as BlitzAssignment));
        break;
      case EPlayAssignmentId.KickReturnBlocker:
        if (defPlayAssignment is KickRetBlockingAssignment copyAssign)
        {
          this.PlayAssignments.Push((PlayAssignment) new KickRetBlockingAssignment(copyAssign));
          break;
        }
        break;
    }
    if (this.defType == EPlayAssignmentId.KickReturnBlocker && global::Game.IsPunt)
      this.SetPuntReturnDefense(this.path);
    else if (this.defType == EPlayAssignmentId.KickReturnBlocker && global::Game.IsKickoff)
      this.SetRouteSpeedModifier(this.path[3] * 0.1f);
    this.inBlockZone = true;
  }

  public void SetManDefense(int coverThisReceiver, ManDefenseAssignment assign)
  {
    this.defType = EPlayAssignmentId.ManCoverage;
    List<PlayerAI> playerAiList = !global::Game.IsPlayerOneOnOffense ? PlayerAI.playersManager.curCompScriptRef : PlayerAI.playersManager.curUserScriptRef;
    assign.SetCoverageOn(playerAiList[coverThisReceiver]);
    playerAiList[coverThisReceiver].coveringPlayerScript = this;
  }

  public void SetPuntReturnDefense(float[] p)
  {
    this.SetRouteSpeedModifier(p[3] * 0.1f);
    this.path = new float[p.Length];
    for (int index = 1; index < p.Length; index += 3)
    {
      float num = p[index];
      if (PlayerAI.flipVal == -1)
        num *= -1f;
      this.path[index] = num;
      this.path[index + 1] = p[index + 1];
      this.path[index + 2] = p[index + 2];
    }
  }

  public void InitActivePlayDefense()
  {
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) (int) this.GetCurrentAssignId());
    this.behaviorTree.SetVariableValue("OwnerPlayer", (object) this.gameObject);
    this.behaviorTree.SetVariableValue("DoDefenseShift", (object) false);
    this.behaviorTree.SetVariableValue("DoShowBlitz", (object) false);
    this.behaviorTree.SetVariableValue("IsPlayerOffense", (object) this.onOffense);
    this.behaviorTree.SetVariableValue("GetToFormPos", (object) false);
    this.behaviorTree.SetVariableValue("AtLinePos", (object) false);
    this.animatorCommunicator.IsPrePlay = false;
  }

  public void RunDefense()
  {
    if (!this.IsDuringPlayDefInitialized)
    {
      this.InitActivePlayDefense();
      this.IsDuringPlayDefInitialized = true;
      this.IsDuringPlayOffInitialized = false;
      this.IsAfterPlayInitialized = false;
      this.IsBeforePlayInitialized = false;
    }
    if (!this.detectedSnap)
      return;
    BehaviorManager.instance.Tick(this.behaviorTree);
    if ((double) this.reactionTimer > 0.0)
    {
      this.reactionTimer -= this._aiTimingInterval;
      if ((double) this.reactionTimer <= 0.0)
        this.attackBallHolder = true;
    }
    if ((double) this.reactionDelayToReceiverCut >= 0.0)
      this.reactionDelayToReceiverCut -= this._aiTimingInterval;
    else if (global::Game.IsTurnover)
    {
      if ((UnityEngine.Object) PlayerAI.playersManager.ballHolder == (UnityEngine.Object) this.mainGO)
      {
        if (this.CurrentPlayAssignment != null && this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.RunToEndZone)
          return;
        this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.RunToEndZone, (RouteGraphicData) null, (float[]) null), true);
      }
      else
      {
        PlayAssignment currentPlayAssignment = this.CurrentPlayAssignment;
        if ((currentPlayAssignment != null ? (int) currentPlayAssignment.GetAssignmentType() : 0) == 15)
          return;
        Vector3 blockTarget = this.FindBlockTarget();
        if ((UnityEngine.Object) this.blockTarget != (UnityEngine.Object) null)
          this.animatorCommunicator.SetGoal(blockTarget);
        else
          this.RunOpenFieldBlockingLogic();
      }
    }
    else
    {
      if (this.CurrentPlayAssignment != null && (this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.ChaseBall || this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.KickReturnBlocker || this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.ManCoverage || this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.ZoneCoverage || this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.MoveToDefendPass))
        return;
      if (global::Game.BS_IsInAirDeflected && this.isGoingForThrownBall)
      {
        if (this.CurrentPlayAssignment != null && this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.ChaseBall)
          return;
        this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
      }
      else if (this.defType == EPlayAssignmentId.PuntKickReceive && global::Game.IsPunt && !global::Game.BallIsThrownOrKicked)
        this.animatorCommunicator.Stop();
      else if (global::Game.BS_IsOnGround && global::Game.IsPunt && !(bool) ProEra.Game.MatchState.Turnover && (double) MatchManager.instance.playManager.deflectedPassTimer <= 0.0)
        this.animatorCommunicator.Stop();
      else if ((global::Game.BS_IsOnGround || global::Game.BS_IsFumble) && this.defType == EPlayAssignmentId.PuntKickReceive && global::Game.BallHolderIsNull)
      {
        if (this.CurrentPlayAssignment != null && this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.ChaseBall)
          return;
        this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
      }
      else if ((global::Game.BS_IsOnGround || global::Game.BS_IsFumble) && (global::Game.IsRunOrPass || global::Game.IsOnsidesKick) && global::Game.BallHolderIsNull)
      {
        if (this.CurrentPlayAssignment != null && this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.ChaseBall)
          return;
        this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
      }
      else if (global::Game.BS_IsKick && global::Game.IsKickoff)
      {
        if (global::Game.IsOnsidesKick)
          this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
        else if (this.defType == EPlayAssignmentId.PuntKickReceive || PlayerAI.matchManager.onsideKick)
        {
          Vector3 vector3 = new Vector3(PlayerAI.playersManager.passDestination.x, 0.0f, Mathf.Clamp(PlayerAI.playersManager.passDestination.z, Field.SOUTH_BACK_OF_ENDZONE + Field.THREE_YARDS, Field.NORTH_BACK_OF_ENDZONE - Field.THREE_YARDS));
          this.animatorCommunicator.SetGoal(vector3, 0.8f, PlayerAI.LookAtBallRotation(vector3));
        }
        else
        {
          Vector3 vector3 = new Vector3(PlayerAI.playersManager.passDestination.x + this.path[1], 0.0f, Mathf.Clamp(PlayerAI.playersManager.passDestination.z, Field.SOUTH_BACK_OF_ENDZONE + Field.THREE_YARDS, Field.NORTH_BACK_OF_ENDZONE - Field.THREE_YARDS));
          this.animatorCommunicator.SetGoal(vector3, 1f, PlayerAI.LookAtBallRotation(vector3));
        }
        if ((double) this.animatorCommunicator.CurrentGoal.position.x < -(double) Field.OUT_OF_BOUNDS + (double) Field.ONE_YARD)
        {
          this.animatorCommunicator.SetGoal(new Vector3(Field.OUT_OF_BOUNDS + Field.TWO_YARDS, 0.0f, this.animatorCommunicator.CurrentGoal.position.z));
        }
        else
        {
          if ((double) this.animatorCommunicator.CurrentGoal.position.x <= (double) Field.OUT_OF_BOUNDS - (double) Field.ONE_YARD)
            return;
          this.animatorCommunicator.SetGoal(new Vector3(Field.OUT_OF_BOUNDS - Field.TWO_YARDS, 0.0f, this.animatorCommunicator.CurrentGoal.position.z));
        }
      }
      else if (global::Game.BS_IsKick && global::Game.IsPunt)
      {
        if (!this.hasReactedToPass)
        {
          this.hasReactedToPass = true;
          if (this.defType == EPlayAssignmentId.PuntKickReceive)
            this.GotoCatchPos(PlayerAI.playersManager.passDestination);
          else
            this.GotoCatchPos(PlayerAI.playersManager.passDestination + new Vector3(0.0f, 0.0f, Field.ONE_YARD_FORWARD * -5f));
        }
        else
        {
          if (!this.isGoingForThrownBall)
            return;
          if (global::Game.IsPunt && Field.FurtherDownfield(this.animatorCommunicator.CurrentGoal.position.z, Field.OPPONENT_TEN_YARD_LINE))
            this.animatorCommunicator.SetGoal(this.trans.position, 0.8f, Field.DEFENSE_TOWARDS_LOS_QUATERNION);
          else if ((double) this.animatorCommunicator.CurrentGoal.position.x < -(double) Field.OUT_OF_BOUNDS + (double) Field.ONE_YARD)
          {
            this.animatorCommunicator.SetGoal(new Vector3(Field.OUT_OF_BOUNDS + Field.TWO_YARDS, 0.0f, this.animatorCommunicator.CurrentGoal.position.z), 0.8f, Field.DEFENSE_TOWARDS_LOS_QUATERNION);
          }
          else
          {
            if ((double) this.animatorCommunicator.CurrentGoal.position.x <= (double) Field.OUT_OF_BOUNDS - (double) Field.ONE_YARD)
              return;
            this.animatorCommunicator.SetGoal(new Vector3(Field.RIGHT_OUT_OF_BOUNDS - Field.TWO_YARDS, 0.0f, this.animatorCommunicator.CurrentGoal.position.z), 0.8f, Field.DEFENSE_TOWARDS_LOS_QUATERNION);
          }
        }
      }
      else if (global::Game.BS_IsInAirPass)
      {
        this.UpdateReactToThrownPass(true);
      }
      else
      {
        if (!global::Game.BallIsThrownOrKicked && (!global::Game.IsRun && !global::Game.IsQBKeeper && !global::Game.IsPlayAction && !PlayerAI.ballManager.HasCrossedLOS && this.CurrentPlayAssignment != null || !global::Game.BallIsNotThrownOrKicked) || !this.attackBallHolder || !((UnityEngine.Object) PlayerAI.playersManager.ballHolder != (UnityEngine.Object) null) || this.isEngagedInBlock || this.CurrentPlayAssignment != null && this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.ChaseBall)
          return;
        this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
      }
    }
  }

  public void RunDefensePerFrame()
  {
    if (global::Game.BS_IsKick && global::Game.IsKickoff)
      this.CheckForKickCatch();
    else if (global::Game.BS_IsKick && global::Game.IsPunt)
    {
      if (this.defType == EPlayAssignmentId.PuntKickReceive)
        this.CheckForKickCatch();
    }
    else if ((global::Game.BS_IsInAirPass || global::Game.BS_IsInAirDeflected) && this.isGoingForThrownBall)
      this.CheckForInt();
    if (global::Game.IsTurnover || this.CurrentPlayAssignment != null && this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.KickReturnBlocker)
    {
      if (!this.CheckForEngage(this.blockTarget))
        return;
      PlayerAI.EngageBlocking(this, this.blockTarget);
    }
    else
    {
      if (this.isEngagedInBlock)
        return;
      this.RequestTackle();
    }
  }

  public void CheckForInt()
  {
    if (!this.onOffense && !this.onUserTeam && !GameSettings.GetDifficulty().OpponentCanIntercept)
      return;
    bool flag1 = this.defType == EPlayAssignmentId.ZoneCoverage || this.defType == EPlayAssignmentId.SpyCoverage;
    bool flag2 = this.defType == EPlayAssignmentId.ManCoverage;
    if (global::Game.BS_IsInAirDeflected)
      this.onBallCatchRequested.Invoke(PlayerAI.playersManager, PlayerAI.ballManager, this);
    else if (flag1)
    {
      this.onBallCatchRequested.Invoke(PlayerAI.playersManager, PlayerAI.ballManager, this);
      this.onBallSwatRequested.Invoke(PlayerAI.playersManager, PlayerAI.ballManager, this);
    }
    else
    {
      if (!flag2)
        return;
      this.onBallSwatRequested.Invoke(PlayerAI.playersManager, PlayerAI.ballManager, this);
    }
  }

  public void SetHandoffDetectionReactionDelay()
  {
    this.ResetSpeedModifier();
    this.reactionTimer = PlayerAI.CalcHandoffDetectionReactionDelay(this);
  }

  public static float CalcHandoffDetectionReactionDelay(PlayerAI player)
  {
    float num;
    if (global::Game.IsPass || global::Game.BallHolderIsUser)
    {
      player.attackBallHolder = true;
      num = 0.0f;
    }
    else
      num = Mathf.Clamp(!Field.IsBehindLineOfScrimmage(player.trans.position.z) ? (player.defType != EPlayAssignmentId.ManCoverage || !((UnityEngine.Object) player.ManCoverTarget != (UnityEngine.Object) null) || !Field.IsBehindLineOfScrimmage(player.ManCoverTarget.trans.position.z) ? (!player.onUserTeam ? (!global::Game.UserCallsPlays || !global::Game.IsNot2PMatch ? (float) (UnityEngine.Random.Range(0, 100 - player.awareness) * 2 + Mathf.Min(10, ProEra.Game.MatchState.Stats.User.ConsecutivePassPlays)) : (float) Mathf.Max(1, UnityEngine.Random.Range(0, 100 - player.awareness) - (10 - PersistentData.offDifficulty) * 2)) : (float) (UnityEngine.Random.Range(1, 100 - player.awareness) * 2 + Mathf.Min(10, ProEra.Game.MatchState.Stats.Comp.ConsecutivePassPlays))) : (!global::Game.UserCallsPlays || !global::Game.IsNot2PMatch ? (float) UnityEngine.Random.Range(1, 100 - player.awareness) : (float) (UnityEngine.Random.Range(1, 100 - player.awareness) - (10 - PersistentData.offDifficulty) * 2))) : 1f, 1f, 20f);
    return num * player._aiTimingInterval;
  }

  public void CheckForKickCatch()
  {
    if (!global::Game.IsKickoff && !global::Game.IsPunt)
      return;
    this.onPuntOrKickReturnEventRequested.Invoke(PlayerAI.playersManager, PlayerAI.ballManager, this);
  }

  public void SetToOffense()
  {
    this.onOffense = true;
    this.animatorCommunicator.onOffense = true;
  }

  public void SetupOffPlay(
    PlayAssignment offPlayAssign,
    int flip,
    Position formPosition,
    PlayerAI handOffTargetPlayer)
  {
    this.ClearAllAssignments();
    this.hasShiftedStartingPosition = false;
    this.initialBlockTarget = (PlayerAI) null;
    this.posInBlockForm = -1;
    PlayerAI.flipVal = flip;
    this.lookForBlockTarget = false;
    this.sprint = 90;
    this.SetIsGoingForThrownBall(false);
    this.IsTackled = false;
    this.isTackling = false;
    this.formationPosition = formPosition;
    this.hasReactedToPass = false;
    this.SetPlayerRoutePath(offPlayAssign.GetRoutePoints());
    this.pathIndex = 1;
    switch (offPlayAssign.GetAssignmentType())
    {
      case EPlayAssignmentId.RunRoute:
        this.PlayAssignments.Push((PlayAssignment) (offPlayAssign as RunRouteAssignment));
        break;
      case EPlayAssignmentId.PassBlock:
        this.PlayAssignments.Push((PlayAssignment) (offPlayAssign as PassBlockAssignment));
        this.passBlockSpeed = 1f;
        this.distanceToBlocker = 100f;
        break;
      case EPlayAssignmentId.RunBlock:
        this.PlayAssignments.Push((PlayAssignment) (offPlayAssign as RunBlockAssignment));
        break;
      case EPlayAssignmentId.RunToEndZone:
        this.PlayAssignments.Push((PlayAssignment) (offPlayAssign as RunToEndZoneAssignment));
        break;
    }
    this.passBlockSpeed = 1f;
    this.distanceToBlocker = 100f;
    this.inBlockZone = true;
    this.animatorCommunicator.speed = 1f;
    if (this.IsPullingLineman())
      FatigueManager.PullLineman(this);
    if (this.IsQB() && (global::Game.IsRun || global::Game.IsPlayAction))
    {
      this.blockType = BlockType.QBOnRunPlay;
      this.speed = PlayerAI.runPlaySpeed;
    }
    else
      this.speed = this.savedSpeed;
    if (this.IsCenter())
      this.PlayAssignments.Push(new PlayAssignment(EPlayAssignmentId.OffPlayInitiation, (RouteGraphicData) null, (float[]) null));
    else if ((global::Game.IsPass || global::Game.IsRun || global::Game.IsPlayAction) && this.IsQB())
      this.PlayAssignments.Push(new PlayAssignment(EPlayAssignmentId.OffPlayInitiation, (RouteGraphicData) null, offPlayAssign.GetRoutePoints()));
    else if ((global::Game.IsRun || global::Game.IsPlayAction) && (UnityEngine.Object) handOffTargetPlayer == (UnityEngine.Object) this && !global::Game.IsQBKeeper)
      this.PlayAssignments.Push(new PlayAssignment(EPlayAssignmentId.OffPlayInitiation, (RouteGraphicData) null, offPlayAssign.GetRoutePoints()));
    else if (global::Game.IsPunt && (UnityEngine.Object) global::Game.Punter == (UnityEngine.Object) this)
      this.PlayAssignments.Push(new PlayAssignment(EPlayAssignmentId.OffPlayInitiation, (RouteGraphicData) null, (float[]) null));
    else if ((global::Game.IsFG || global::Game.IsKickoff) && (UnityEngine.Object) global::Game.Kicker == (UnityEngine.Object) this)
    {
      this.PlayAssignments.Push(new PlayAssignment(EPlayAssignmentId.OffPlayInitiation, (RouteGraphicData) null, (float[]) null));
    }
    else
    {
      if (!global::Game.IsFG || !((UnityEngine.Object) global::Game.Holder == (UnityEngine.Object) this))
        return;
      this.PlayAssignments.Push(new PlayAssignment(EPlayAssignmentId.OffPlayInitiation, (RouteGraphicData) null, (float[]) null));
    }
  }

  public void SetPlayerRoutePath(float[] p)
  {
    if (p == null || p.Length < 3)
    {
      Debug.LogError((object) ("Player route path is invalid for player " + this.playerName));
    }
    else
    {
      this.blockType = (BlockType) p[0];
      float num1 = this.playStartPosition.x - PlayerAI.matchManager.ballHashPosition;
      if ((double) num1 <= 0.75 && (double) num1 > -0.75)
        num1 = 0.0f;
      this.path = new float[p.Length];
      for (int index = 1; index < p.Length; index += 3)
      {
        float num2 = p[index];
        if ((double) num1 > 0.0 || (double) num1 == 0.0 && PlayerAI.flipVal == -1)
          num2 *= -1f;
        this.path[index] = (float) Mathf.RoundToInt(num2 + num1);
        this.path[index + 1] = p[index + 1];
        this.path[index + 2] = p[index + 2];
      }
    }
  }

  public void SetPlayerRouteForAssignment(
    RunPathAssignment assignment,
    List<Vector3> actualRoutePoints,
    float baseEffort = 0.8f)
  {
    this.SetPlayerRouteForAssignment(assignment, actualRoutePoints, baseEffort, Quaternion.identity);
  }

  public void SetPlayerRouteForAssignment(
    RunPathAssignment assignment,
    List<Vector3> actualRoutePoints,
    float baseEffort,
    Quaternion facingRot,
    bool flipSide = true)
  {
    float num1 = this.trans.position.x - MatchManager.instance.ballHashPosition;
    if ((double) num1 <= 0.75 && (double) num1 > -0.75)
      num1 = 0.0f;
    List<RouteLeg> route = assignment.GetRoute();
    for (int index = 0; index < route.Count; ++index)
    {
      RouteLeg routeLeg = route[index];
      Vector3 vector3 = routeLeg.moveVector;
      if (flipSide && (double) num1 > 0.0)
        vector3.x *= -1f;
      float num2 = flipSide ? 1f : (float) global::Game.OffensiveFieldDirection;
      vector3.x *= num2;
      vector3.x += num1;
      vector3.x += MatchManager.instance.ballHashPosition;
      vector3.z = vector3.z * (float) global::Game.OffensiveFieldDirection + ProEra.Game.MatchState.BallOn.Value;
      vector3 = PlayerAI.ClampMoveToFieldBounds(vector3);
      float num3 = baseEffort * routeLeg.speedPercent;
      float effortCeiling01 = Mathf.Clamp(baseEffort + num3, 0.3f, this.animatorCommunicator.maxEffort01);
      if (this.animatorCommunicator.LocomotioStyle == ELocomotionStyle.Regular)
      {
        if (index == 0)
          this.animatorCommunicator.SetGoal(vector3, effortCeiling01);
        else
          this.animatorCommunicator.AddGoalToQueue(vector3, effortCeiling01);
      }
      else if (index == 0)
        this.animatorCommunicator.SetGoal(vector3, effortCeiling01, facingRot);
      else
        this.animatorCommunicator.AddGoalToQueue(vector3, effortCeiling01, facingRot);
      actualRoutePoints.Add(vector3);
    }
  }

  public void SetTEPassBlock()
  {
    this.passBlockSpeed = PlayerAI.te_passBlockSpeed;
    this.blockType = BlockType.PassBlockBegin;
  }

  public void SetTERunRoute() => this.blockType = BlockType.None;

  private IEnumerator BeginDropback()
  {
    yield return (object) PlayerAI._beginDropback;
    this.qbIsDroppingBack = true;
    PlayerAI.qbDropbackSpeed = PlayerAI.playManager.savedOffPlay.GetDropbackType() != DropbackType.ThreeStep ? PlayerAI.qb5StepDropSpeed * (float) global::Game.OffensiveFieldDirection : PlayerAI.qb3StepDropSpeed * (float) global::Game.OffensiveFieldDirection;
    yield return (object) PlayerAI._endDropback;
    this.qbIsDroppingBack = false;
  }

  public void InitActivePlayOffense()
  {
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) (int) this.GetCurrentAssignId());
    this.behaviorTree.SetVariableValue("OwnerPlayer", (object) this.gameObject);
    this.behaviorTree.SetVariableValue("DoDefenseShift", (object) false);
    this.behaviorTree.SetVariableValue("DoShowBlitz", (object) false);
    this.behaviorTree.SetVariableValue("IsPlayerOffense", (object) this.onOffense);
    this.behaviorTree.SetVariableValue("GetToFormPos", (object) false);
    this.behaviorTree.SetVariableValue("AtLinePos", (object) false);
    this.animatorCommunicator.IsPrePlay = false;
  }

  public void RunOffense()
  {
    bool flag = false;
    if (!this.IsDuringPlayOffInitialized)
    {
      this.InitActivePlayOffense();
      this.IsDuringPlayOffInitialized = true;
      this.IsDuringPlayDefInitialized = false;
      this.IsAfterPlayInitialized = false;
      this.IsBeforePlayInitialized = false;
    }
    if (!this.detectedSnap)
      return;
    if (global::Game.IsTurnover)
    {
      if (this.CurrentPlayAssignment == null || this.CurrentPlayAssignment.GetAssignmentType() != EPlayAssignmentId.ChaseBall)
        this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
    }
    else if (global::Game.BS_IsInAirDeflected && this.isGoingForThrownBall)
    {
      if (this.CurrentPlayAssignment == null || this.CurrentPlayAssignment.GetAssignmentType() != EPlayAssignmentId.ChaseBall)
        this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.ChaseBall, (RouteGraphicData) null, (float[]) null), true);
    }
    else if (global::Game.BS_IsKick && global::Game.IsPunt)
    {
      Vector3 passDestination = PlayerAI.playersManager.passDestination;
      if (this.ShouldKickCoverageStayInLane(passDestination))
      {
        this.SetKickCoverageLaneTarget(passDestination);
      }
      else
      {
        this.animatorCommunicator.SetGoal(passDestination, 0.8f);
        flag = true;
      }
    }
    else if (global::Game.BS_IsOnGround || global::Game.BS_IsFumble || global::Game.IsOnsidesKick && global::Game.BallIsThrownOrKicked)
    {
      if (Field.IsBehindLineOfScrimmage(PlayerAI.ballManager.trans.position.z) && (double) MatchManager.instance.playTimer - (double) MatchManager.instance.playManager.fumbleOccurredTimer < 0.5)
      {
        if ((double) Vector3.Distance(this.trans.position, this.animatorCommunicator.CurrentGoal.position) <= (double) PlayerAI.radiusOfSatisfaction)
        {
          this.animatorCommunicator.SetGoal(this.animatorCommunicator.CurrentGoal.position, 1f, PlayerAI.LookAtBallRotation(this.animatorCommunicator.CurrentGoal.position));
          flag = true;
        }
      }
      else
      {
        this.animatorCommunicator.SetGoal(PlayerAI.ballManager.trans.position.SetY(0.0f), 1f);
        flag = true;
        this.CheckForPickUpBallAnimation();
      }
    }
    int currentAssignId = (int) this.GetCurrentAssignId();
    if (currentAssignId == 0)
      this.UpdateBlocking();
    if (currentAssignId == 0 && !flag)
      this.UpdateOffenseMovement();
    if (currentAssignId == 0 && global::Game.BS_IsInAirPass && (UnityEngine.Object) PlayerAI.playersManager.intendedReceiver == (UnityEngine.Object) this)
      this.UpdateReactToThrownPass();
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) (int) this.GetCurrentAssignId());
    BehaviorManager.instance.Tick(this.behaviorTree);
  }

  private void UpdateReactToThrownPass(bool includeReactionDelay = false)
  {
    float num = Vector3.Distance(this.trans.position, this.animatorCommunicator.CurrentGoal.position);
    if (!this.hasReactedToPass)
    {
      this.hasReactedToPass = true;
      if (includeReactionDelay)
        this.StartCoroutine(this.GotoCatchPosDelay(PlayerAI.playersManager.passDestination));
      else
        this.GotoCatchPos(PlayerAI.playersManager.passDestination);
    }
    else
    {
      if (!this.isGoingForThrownBall || (double) num >= (double) PlayerAI.radiusOfSatisfaction)
        return;
      this.animatorCommunicator.SetGoal(this.animatorCommunicator.CurrentGoal.position, 0.3f, PlayerAI.LookAtBallRotation(this.animatorCommunicator.CurrentGoal.position));
    }
  }

  public void RunOffenseBH()
  {
    if (!this.IsDuringPlayOffInitialized)
    {
      this.InitActivePlayOffense();
      this.IsDuringPlayOffInitialized = true;
      this.IsDuringPlayDefInitialized = false;
      this.IsAfterPlayInitialized = false;
      this.IsBeforePlayInitialized = false;
    }
    if (!this.detectedSnap)
      return;
    if (this.IsQB() && global::Game.IsPass && Field.FurtherDownfield(MatchManager.ballOn, this.trans.position.z))
    {
      if (this.CurrentPlayAssignment == null || this.CurrentPlayAssignment.GetAssignmentType() != EPlayAssignmentId.QBLookToPass && this.CurrentPlayAssignment.GetAssignmentType() != EPlayAssignmentId.OffPlayInitiation)
        this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.QBLookToPass, (RouteGraphicData) null, (float[]) null), true);
    }
    else if (this.IsQB() && global::Game.IsQBKeeper)
    {
      if (this.CurrentPlayAssignment == null || this.CurrentPlayAssignment.GetAssignmentType() != EPlayAssignmentId.RunToEndZone)
        this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.RunToEndZone, (RouteGraphicData) null, (float[]) null), true);
    }
    else if (!this._isTackled && (this.CurrentPlayAssignment == null || this.CurrentPlayAssignment.GetAssignmentType() != EPlayAssignmentId.RunToEndZone && this.CurrentPlayAssignment.GetAssignmentType() != EPlayAssignmentId.OffPlayInitiation))
      this.SetNewAssignment(new PlayAssignment(EPlayAssignmentId.RunToEndZone, (RouteGraphicData) null, (float[]) null), true);
    BehaviorManager.instance.Tick(this.behaviorTree);
  }

  private void RunOpenFieldBlockingLogic()
  {
    if ((UnityEngine.Object) PlayerAI.playersManager.ballHolder != (UnityEngine.Object) null)
    {
      Vector3 vector3 = PlayerAI.playersManager.ballHolderScript.trans.position + new Vector3(0.0f, 0.0f, Field.ONE_YARD_FORWARD * 5f);
      if ((double) Vector3.Distance(this.trans.position, PlayerAI.playersManager.ballHolderScript.trans.position) < (double) Field.FIVE_YARDS)
        vector3.x = this.trans.position.x;
      this.animatorCommunicator.SetGoal(vector3);
      Debug.DrawLine(this.trans.position, vector3, Color.white, this._aiTimingInterval);
      AIUtil.DrawDebugCross(vector3, new Vector3(0.5f, 0.5f, 0.5f), Color.white, this._aiTimingInterval);
    }
    else
      this.animatorCommunicator.SetGoal(PlayerAI.ballManager.trans.position.SetY(0.0f));
  }

  public void RunOffensePerFrame()
  {
    if (global::Game.IsTurnover)
    {
      if (this.isEngagedInBlock)
        return;
      this.RequestTackle();
    }
    else
    {
      bool flag = !global::Game.BS_IsOnGround && (global::Game.BS_IsInAirDeflected || this.GetCurrentAssignId() == EPlayAssignmentId.RunRoute);
      if (((!global::Game.IsPitchPlay || PlayerAI.playManager.savedOffPlay.GetHandoffTarget() != this.indexInFormation ? 0 : (global::Game.BS_IsInAirToss ? 1 : 0)) | (flag ? 1 : 0)) != 0)
      {
        this.onBallCatchRequested.Invoke(PlayerAI.playersManager, PlayerAI.ballManager, this);
      }
      else
      {
        if (!this.CheckForEngage(this.blockTarget))
          return;
        PlayerAI.EngageBlocking(this, this.blockTarget);
      }
    }
  }

  private void UpdateOffenseMovement()
  {
    if (global::Game.IsNotTurnover)
    {
      if (this.path != null && this.pathIndex <= this.path.Length)
      {
        if (this.pathIndex == 1 && !this.isGoingForThrownBall)
          this.SetNextPointInPathAsTarget();
        float num1 = Vector3.Distance(this.trans.position, this.animatorCommunicator.CurrentGoal.position);
        float num2 = !this.isLocalAvoiding || this.isGoingForThrownBall ? PlayerAI.radiusOfSatisfaction : Field.THREE_YARDS;
        Vector3 normalized = (this.animatorCommunicator.CurrentGoal.position - this.trans.position).normalized;
        if ((double) num1 < (double) Field.THREE_YARDS && this.blockType != BlockType.PassBlockBegin && (double) Vector3.Dot(normalized, this.trans.forward) < 0.0)
          num1 = 0.0f;
        if ((double) num1 <= (double) num2 && !this.isGoingForThrownBall)
        {
          this.pathIndex += 3;
          if (this.pathIndex >= this.path.Length)
          {
            this.ResetSpeedModifier();
            if (this.blockType == BlockType.QBOnRunPlay)
            {
              this.animatorCommunicator.SetGoal(this.trans.position);
              this.lookForBlockTarget = false;
            }
            else if (this.blockType == BlockType.None && global::Game.IsPass)
              this.FindNewTargetForOOBReceiver();
            else if (this.blockType == BlockType.PassBlockBegin)
            {
              this.blockType = BlockType.PassBlockNormal;
              this.animatorCommunicator.SetGoal(this.trans.position, Field.OFFENSE_TOWARDS_LOS_QUATERNION);
            }
            else
            {
              this.lookForBlockTarget = true;
              this.animatorCommunicator.SetGoal(this.trans.position);
            }
          }
        }
      }
    }
    else if (global::Game.IsNotTurnover && !this.isGoingForThrownBall)
      this.animatorCommunicator.SetGoal(this.trans.position);
    if (this.isGoingForThrownBall || !global::Game.IsNotTurnover || !global::Game.IsPass)
      return;
    if (Field.FurtherDownfield(this.trans.position.z, Field.OFFENSIVE_BACK_OF_ENDZONE - Field.ONE_YARD_FORWARD * 2f))
      this.FindNewTargetForOOBReceiver();
    else if ((double) this.trans.position.x < -(double) Field.OUT_OF_BOUNDS + (double) Field.ONE_YARD)
    {
      this.FindNewTargetForOOBReceiver();
    }
    else
    {
      if ((double) this.trans.position.x <= (double) Field.OUT_OF_BOUNDS - (double) Field.ONE_YARD)
        return;
      this.FindNewTargetForOOBReceiver();
    }
  }

  public bool ShouldKickCoverageStayInLane(Vector3 pursuitPoint)
  {
    float num1 = Field.ONE_YARD * 15f * (float) global::Game.OffensiveFieldDirection;
    float num2 = Field.ONE_YARD * 20f;
    return !Field.FurtherDownfield(pursuitPoint.z, this.trans.position.z + num1) && (double) Vector3.Distance(pursuitPoint, this.trans.position) >= (double) num2;
  }

  public void SetKickCoverageLaneTarget(Vector3 pursuitPoint)
  {
    float num1 = Field.ONE_YARD * 15f;
    float num2 = Field.ONE_YARD * 8f;
    float num3 = 1.5f;
    float f = pursuitPoint.x - this.trans.position.x;
    if ((double) Mathf.Abs(f) < (double) num2)
      this.animatorCommunicator.SetGoal(pursuitPoint);
    else if ((double) Mathf.Abs(f) > (double) num1)
      this.animatorCommunicator.SetGoal(new Vector3(this.trans.position.x + f / num3, 0.0f, pursuitPoint.z));
    else
      this.animatorCommunicator.SetGoal(new Vector3(this.trans.position.x, 0.0f, pursuitPoint.z));
  }

  private Vector3 GetNextPointInPath()
  {
    if (this.pathIndex < this.path.Length - 2)
      return new Vector3(this.path[this.pathIndex] + PlayerAI.matchManager.ballHashPosition, 0.0f, this.path[this.pathIndex + 1] * (float) global::Game.OffensiveFieldDirection + ProEra.Game.MatchState.BallOn.Value);
    Debug.LogError((object) "Path index out of range . Setting to next point to last point in path");
    return new Vector3(this.path[this.path.Length - 3] + PlayerAI.matchManager.ballHashPosition, 0.0f, this.path[this.path.Length - 2] * (float) global::Game.OffensiveFieldDirection + ProEra.Game.MatchState.BallOn.Value);
  }

  public Vector3 GetPointInPathAfterCurrent()
  {
    if (this.IsThereAnotherPointInPath())
      return new Vector3(this.path[this.pathIndex + 3] + PlayerAI.matchManager.ballHashPosition, 0.0f, this.path[this.pathIndex + 4] * (float) global::Game.OffensiveFieldDirection + MatchManager.ballOn);
    Debug.Log((object) "Trying to access the next route point when there isn't one. Call IsThereAnotherPointInPath () to validate before calling this function.");
    return Vector3.zero;
  }

  public bool IsThereAnotherPointInPath() => this.pathIndex + 5 < this.path.Length;

  private void SetNextPointInPathAsTarget()
  {
    this.animatorCommunicator.SetGoal(this.GetNextPointInPath());
    if (this.pathIndex < this.path.Length - 2)
    {
      this.SetRouteSpeedModifier(this.path[this.pathIndex + 2] * 0.1f);
    }
    else
    {
      Debug.LogError((object) "Path index out of range to set speed mod. Setting to 0");
      this.ResetSpeedModifier();
    }
  }

  public void ClearPath()
  {
    this.path = (float[]) null;
    this.pathIndex = -1;
  }

  private void FindNewTargetForOOBReceiver()
  {
    float z = !Field.FurtherDownfield(this.trans.position.z, Field.OFFENSIVE_GOAL_LINE) ? this.trans.position.z + UnityEngine.Random.Range(0.0f, Field.ONE_YARD * 8f) * (float) global::Game.OffensiveFieldDirection : UnityEngine.Random.Range(Field.OFFENSIVE_GOAL_LINE, Field.OFFENSIVE_BACK_OF_ENDZONE - Field.ONE_YARD_FORWARD * 2f);
    this.animatorCommunicator.SetGoal(new Vector3((double) this.trans.position.x >= 0.0 ? (float) (-(double) Field.OUT_OF_BOUNDS - (double) Field.ONE_YARD * 2.0) : Field.OUT_OF_BOUNDS + Field.ONE_YARD * 2f, 0.0f, z));
  }

  public PlayerAI FindPlayerToAvoid(
    float maxDistanceForAvoidance = 8f,
    float maxDotAngleDiff = 0.0f,
    bool justMyTeam = true,
    bool bStopOnFirstFound = false,
    bool bUseCullDist = false)
  {
    List<PlayerAI> playerAiList;
    if (this.onOffense)
    {
      playerAiList = global::Game.OffensivePlayers;
      if (!justMyTeam)
        playerAiList.AddRange((IEnumerable<PlayerAI>) global::Game.DefensivePlayers);
    }
    else
    {
      playerAiList = global::Game.DefensivePlayers;
      if (!justMyTeam)
        playerAiList.AddRange((IEnumerable<PlayerAI>) global::Game.DefensivePlayers);
    }
    PlayerAI playerToAvoid = (PlayerAI) null;
    float num1 = 1E+07f;
    Vector3 position1 = this.trans.position;
    int count = playerAiList.Count;
    for (int index = 0; index < count; ++index)
    {
      Vector3 position2 = playerAiList[index].trans.position;
      if (!(position1 == position2) && (!bUseCullDist || (double) maxDistanceForAvoidance * 2.0 >= (double) Vector3.Distance(position2, position1)))
      {
        Vector3 a = position2 + playerAiList[index].animatorCommunicator.velocity * this.AITimingInterval;
        position1 += this.animatorCommunicator.velocity * this.AITimingInterval;
        Vector3 vector3 = a - position1;
        Vector3 normalized = vector3.normalized;
        vector3 = this.animatorCommunicator.velocity;
        if ((double) Vector3.Dot(vector3.normalized, normalized) >= (double) maxDotAngleDiff)
        {
          float num2 = Vector3.Distance(a, position1);
          if ((double) num2 < (double) maxDistanceForAvoidance && (double) num2 < (double) num1)
          {
            num1 = num2;
            playerToAvoid = playerAiList[index];
            if (bStopOnFirstFound)
              return playerToAvoid;
          }
        }
      }
    }
    return playerToAvoid;
  }

  public PlayerAI FindPotentialTacklers()
  {
    float num1;
    List<PlayerAI> playerAiList;
    if (global::Game.IsTurnover)
    {
      num1 = Field.ONE_YARD * 100f;
      playerAiList = global::Game.OffensivePlayers;
    }
    else
    {
      num1 = Field.ONE_YARD * 25f;
      playerAiList = global::Game.DefensivePlayers;
    }
    PlayerAI potentialTacklers = (PlayerAI) null;
    float num2 = num1 + 10f;
    for (int index = 0; index < 11; ++index)
    {
      if (!playerAiList[index].isTackling && !playerAiList[index].isEngagedInBlock)
      {
        Vector3 position1 = playerAiList[index].trans.position;
        Vector3 position2 = this.trans.position;
        if (PlayerAI.usePredictedTacklerPos)
        {
          position1 += playerAiList[index].animatorCommunicator.velocity * this.AITimingInterval;
          position2 += this.animatorCommunicator.velocity * this.AITimingInterval;
        }
        if (Field.FurtherDownfield(position1.z, position2.z))
        {
          Vector3 vector3 = position1 - position2;
          float sqrMagnitude = vector3.sqrMagnitude;
          if ((double) sqrMagnitude < (double) num1)
          {
            float num3 = Vector3.Angle(this.trans.forward, vector3.normalized);
            if ((double) sqrMagnitude < (double) num2 && (double) num3 < 110.0)
            {
              num2 = sqrMagnitude;
              potentialTacklers = playerAiList[index];
            }
          }
        }
      }
    }
    return potentialTacklers;
  }

  public static Vector3 FindHole()
  {
    float twoFeet = Field.TWO_FEET;
    int offensiveFieldDirection = global::Game.OffensiveFieldDirection;
    float num1 = MatchManager.instance.ballHashPosition - (float) (PlayerAI.holeLocations.Length / 2) * twoFeet * (float) offensiveFieldDirection;
    for (int index = 0; index < PlayerAI.holeLocations.Length; ++index)
    {
      Vector3 vector3;
      float maxDistance;
      if (PlayerAI.playersManager.forceQBScramble)
      {
        vector3 = new Vector3(num1 + (float) index * twoFeet * (float) offensiveFieldDirection, Field.ONE_YARD, MatchManager.ballOn + Field.FIVE_YARDS * (float) offensiveFieldDirection);
        maxDistance = Field.EIGHT_YARDS;
      }
      else
      {
        vector3 = new Vector3(num1 + (float) index * twoFeet * (float) offensiveFieldDirection, Field.ONE_YARD, MatchManager.ballOn + Field.THREE_YARDS * (float) offensiveFieldDirection);
        maxDistance = Field.SIX_YARDS;
      }
      Vector3 direction = Vector3.back * (float) offensiveFieldDirection;
      UnityEngine.RaycastHit hitInfo;
      if (Physics.Raycast(vector3, direction, out hitInfo, maxDistance, PlayerAI.layerMask))
      {
        PlayerAI playerAi = AssetCache.GetPlayerAI(hitInfo.transform);
        if ((UnityEngine.Object) playerAi == (UnityEngine.Object) null || (UnityEngine.Object) playerAi == (UnityEngine.Object) PlayerAI.playersManager.ballHolderScript)
        {
          PlayerAI.holeLocations[index] = 3;
          Debug.DrawLine(vector3, vector3 + direction * maxDistance, new Color(0.0f, 0.75f, 0.0f));
        }
        else if (playerAi.onOffense && !playerAi.isEngagedInBlock)
        {
          PlayerAI.holeLocations[index] = 1;
          Debug.DrawLine(vector3, vector3 + direction * maxDistance, Color.white);
        }
        else if (playerAi.onOffense && playerAi.isEngagedInBlock)
        {
          PlayerAI.holeLocations[index] = 0;
          Debug.DrawLine(vector3, vector3 + direction * maxDistance, Color.yellow);
        }
        else
        {
          PlayerAI.holeLocations[index] = 0;
          Debug.DrawLine(vector3, vector3 + direction * maxDistance, Color.red);
        }
      }
      else
      {
        PlayerAI.holeLocations[index] = 3;
        Debug.DrawLine(vector3, vector3 + direction * maxDistance, new Color(0.0f, 0.75f, 0.0f));
      }
    }
    for (int index = 2; index < PlayerAI.holeLocations.Length - 3; ++index)
    {
      PlayerAI.holeLocationValues[index] = 0;
      if (PlayerAI.holeLocations[index] == 3)
      {
        int holeLocation1 = PlayerAI.holeLocations[index - 2];
        int holeLocation2 = PlayerAI.holeLocations[index - 1];
        int holeLocation3 = PlayerAI.holeLocations[index + 1];
        int holeLocation4 = PlayerAI.holeLocations[index + 2];
        int num2 = holeLocation2;
        int num3 = holeLocation1 + num2 + holeLocation3 + holeLocation4;
        PlayerAI.holeLocationValues[index] = num3;
      }
    }
    int num4 = Mathf.FloorToInt(Mathf.Abs(MatchManager.instance.playersManager.ballHolderScript.trans.position.x - num1) / twoFeet) + 1;
    int num5 = Mathf.Clamp(MatchManager.instance.playManager.runnerHoleOffset != 0 ? PlayerAI.holeLocationValues.Length / 2 + MatchManager.instance.playManager.runnerHoleOffset * PlayerAI.flipVal * offensiveFieldDirection : num4, 0, PlayerAI.holeLocationValues.Length - 1);
    int num6 = 4;
    int num7 = Mathf.Max(num5 - num6, 0);
    int num8 = Mathf.Min(num5 + num6, PlayerAI.holeLocationValues.Length - 1);
    float num9 = 1f;
    for (int index = num7; index <= num8; ++index)
    {
      int num10 = Mathf.Abs(num7 - num5);
      PlayerAI.holeLocationValues[index] -= Mathf.RoundToInt((float) num10 * num9);
    }
    int num11 = 0;
    int num12 = -1000;
    for (int index = num7; index <= num8; ++index)
    {
      int holeLocationValue = PlayerAI.holeLocationValues[index];
      if (holeLocationValue > num12)
      {
        num12 = holeLocationValue;
        num11 = index;
      }
    }
    int num13 = 8;
    if (num12 < num13)
      num11 = num5 >= PlayerAI.holeLocationValues.Length / 2 ? Mathf.Min(PlayerAI.holeLocationValues.Length - 1, num8 + 3) : Mathf.Max(0, num7 - 3);
    double x = (double) num1 + (double) num11 * (double) twoFeet * (double) offensiveFieldDirection;
    float num14 = Field.THREE_YARDS * (float) offensiveFieldDirection;
    float num15 = 0.0f;
    if (Mathf.Abs(num5 - num11) > 2)
      num15 = (float) Mathf.Abs(num5 - num11) * Field.FIFTEEN_INCHES * (float) offensiveFieldDirection;
    float secondObjectZPos = MatchManager.ballOn - num15 + num14;
    if (Field.FurtherDownfield(MatchManager.instance.playersManager.ballHolderScript.trans.position.z, secondObjectZPos))
      secondObjectZPos = MatchManager.instance.playersManager.ballHolderScript.trans.position.z + Field.FIFTEEN_INCHES * (float) offensiveFieldDirection;
    double z = (double) secondObjectZPos;
    return new Vector3((float) x, 0.0f, (float) z);
  }

  public bool ShouldRunnerGoOutOfBounds() => this.onOffense && !MatchManager.instance.timeManager.IsFirstQuarter() && !MatchManager.instance.timeManager.IsThirdQuarter() && MatchManager.instance.timeManager.GetDisplaySeconds() < 60 && (MatchManager.instance.timeManager.IsSecondQuarter() || MatchManager.instance.timeManager.IsFourthQuarter() && global::Game.IsDefenseWinning);

  public IEnumerator SetDroppedPass()
  {
    this.droppedPass = true;
    MatchManager.instance.playManager.droppedPassTimer = MatchManager.instance.playTimer;
    yield return (object) PlayerAI._qbStanceDelay;
    this.droppedPass = false;
  }

  public void RunPlayerCaughtPass()
  {
    PlayerAI.justCaughtPass = true;
    this.StartCoroutine(this.ResetJustCaughtPass());
  }

  private IEnumerator ResetJustCaughtPass()
  {
    yield return (object) PlayerAI._justCaughtPassWFS;
    PlayerAI.justCaughtPass = false;
  }

  public void CheckForPickUpBallAnimation()
  {
    if (!global::Game.IsPlayActive)
      return;
    this.onBallPickUpRequested.Invoke(PlayerAI.playersManager, PlayerAI.ballManager, this);
  }

  public Vector3 GetBallHolderPursuitAngle()
  {
    PlayerAI holderPursuitTarget = this.BallHolderPursuitTarget;
    Vector3 position = holderPursuitTarget.trans.position;
    float num1 = Vector3.Distance(this.trans.position, position);
    Vector3 normalized = (this.trans.position - position).normalized;
    int num2 = !global::Game.IsTurnover ? global::Game.OffensiveFieldDirection : global::Game.OffensiveFieldDirection * -1;
    float num3 = Vector3.Dot(Vector3.forward * (float) num2, normalized);
    Vector3 vector3_1 = holderPursuitTarget.Velocity.normalized * num1 * 0.5f;
    Vector3 vector3_2 = (1f - num3) * num1 * Vector3.forward * (float) num2 * 1.5f;
    Vector3 vector3_3 = position + vector3_1 + vector3_2;
    float x = vector3_3.x;
    float num4 = vector3_3.z;
    if ((double) x > (double) Field.OUT_OF_BOUNDS - (double) Field.SIX_INCHES)
      x = Field.OUT_OF_BOUNDS - Field.SIX_INCHES;
    else if ((double) x < -(double) Field.OUT_OF_BOUNDS + (double) Field.SIX_INCHES)
      x = -Field.OUT_OF_BOUNDS + Field.SIX_INCHES;
    if (Field.FurtherDownfield(num4, Field.OFFENSIVE_GOAL_LINE))
      num4 = Field.OFFENSIVE_GOAL_LINE - Field.ONE_YARD_FORWARD;
    return new Vector3(x, 0.0f, num4);
  }

  public Vector3 GetPlayerPursuitAngle(PlayerAI targetPlayer)
  {
    Vector3 position = targetPlayer.trans.position;
    float num = Vector3.Distance(this.trans.position, position);
    Vector3 vector3 = targetPlayer.trans.forward * num * 0.4f;
    return position + vector3;
  }

  public static bool IsQBBetweenTackles()
  {
    float x = global::Game.OffensiveQB.trans.position.x;
    float secondObjectXPos1 = PlayerAI.matchManager.ballHashPosition - Field.ONE_YARD * 7f * (float) global::Game.OffensiveFieldDirection;
    float secondObjectXPos2 = PlayerAI.matchManager.ballHashPosition + Field.ONE_YARD * 7f * (float) global::Game.OffensiveFieldDirection;
    return Field.MoreRightOf(x, secondObjectXPos1) && Field.MoreLeftOf(x, secondObjectXPos2);
  }

  public void SetIsGoingForThrownBall(bool isGoingForBall) => this.isGoingForThrownBall = isGoingForBall;

  public bool IsQB() => this.indexInFormation == 5 && this.onOffense;

  public bool IsCenter() => !global::Game.IsKickoff && this.indexInFormation == 2 && this.onOffense;

  public bool IsLineman() => this.indexInFormation < 5;

  public bool IsReceiver() => 6 <= this.indexInFormation && this.indexInFormation <= 10;

  public bool IsKicker() => this.indexInFormation == 6 && this.onOffense;

  public bool IsPullingLineman() => this.indexInFormation < 5 && this.path.Length > 4;

  public void DisableColliders() => this.capCollider.enabled = false;

  public void EnableColliders() => this.capCollider.enabled = true;

  public int GetKickingPower() => this.GetExaggeratedValue(this.kickingPower);

  private int GetExaggeratedValue(int value)
  {
    double num = System.Math.Pow((double) value / 100.0, 3.0) + 0.5;
    return (int) System.Math.Round((double) value * num);
  }

  public void HidePlayerAvatar()
  {
    this.playerMeshGroup.SetActive(false);
    this.avatarGraphics.EnableShadows(false);
    this.nteractAgent.enabled = false;
    this.eventAgent.enabled = false;
  }

  public void ShowPlayerAvatar()
  {
    this.playerMeshGroup.SetActive(true);
    this.avatarGraphics.EnableShadows(true);
    this.nteractAgent.enabled = true;
    this.eventAgent.enabled = true;
  }

  public void LockQBToPlayer(Vector3 pos, Quaternion rot)
  {
    if (!global::Game.UserControlsQB || !global::Game.IsPlayerOneOnOffense || !global::Game.IsPlayActive || !global::Game.IsNotKickoff || !global::Game.IsNotFG || global::Game.IsPunt)
      return;
    this.trans.position = pos;
    this.trans.rotation = rot;
  }

  public IEnumerator GotoCatchPosDelay(Vector3 dest)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    PlayerAI player = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      player.GotoCatchPos(dest);
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    float seconds = PlayerAI.CalcBallThrowDetection(player, dest);
    player._gotoCatchPosDelay = new WaitForSeconds(seconds);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) player._gotoCatchPosDelay;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  public static float CalcBallThrowDetection(PlayerAI player, Vector3 dest)
  {
    Vector3 normalized = (PlayerAI.ballManager.trans.position - player.trans.position).normalized;
    float num1 = (double) Vector3.Dot(player.trans.forward, normalized) >= 0.0 ? (float) (100 - player.awareness) * 0.005f : (float) (100 - player.awareness) * 0.01f;
    PlayerAI manCoverTarget = player.ManCoverTarget;
    if (!player.onOffense && (UnityEngine.Object) PlayerAI.playersManager.passTargetScript != (UnityEngine.Object) null)
    {
      float num2 = Vector3.Distance(player.trans.position, PlayerAI.playersManager.passTargetScript.trans.position);
      if ((double) num2 > (double) Field.SIX_YARDS && (UnityEngine.Object) manCoverTarget != (UnityEngine.Object) null && (UnityEngine.Object) manCoverTarget != (UnityEngine.Object) PlayerAI.playersManager.passTargetScript)
        num1 += Mathf.Min(num2 * 0.1f, 2f);
    }
    if (Field.IsBehindLineOfScrimmage(dest.z))
      num1 = 0.0f;
    if (global::Game.IsPunt && !player.onOffense && player.defType != EPlayAssignmentId.PuntKickReceive)
      num1 = UnityEngine.Random.Range(1.25f, 1.5f);
    return num1;
  }

  public void GotoCatchPos(Vector3 dest)
  {
    this.SetIsGoingForThrownBall(true);
    this.routeSpeedModifier = 0.0f;
    this.distAdj = PlayerAI.ballManager.passVelocity / 10f;
    this.heightAdj = PlayerAI.ballManager.passVelocity / 15f;
    if ((double) dest.x > (double) Field.OUT_OF_BOUNDS - (double) Field.SIX_INCHES)
      dest.x = Field.OUT_OF_BOUNDS - Field.SIX_INCHES;
    else if ((double) dest.x < -(double) Field.OUT_OF_BOUNDS + (double) Field.SIX_INCHES)
      dest.x = -Field.OUT_OF_BOUNDS + Field.SIX_INCHES;
    else if ((double) dest.z > (double) Field.NORTH_BACK_OF_ENDZONE - (double) Field.ONE_FOOT)
      dest.z = Field.NORTH_BACK_OF_ENDZONE - Field.ONE_FOOT;
    else if ((double) dest.z < (double) Field.SOUTH_BACK_OF_ENDZONE + (double) Field.ONE_FOOT)
      dest.z = Field.SOUTH_BACK_OF_ENDZONE + Field.ONE_FOOT;
    if (!this.onOffense)
    {
      if (global::Game.IsPass)
        dest += PlayerAI.ballManager.trans.forward * UnityEngine.Random.Range(-Field.FIFTEEN_INCHES, Field.FIFTEEN_INCHES);
      else if (global::Game.IsPunt || global::Game.IsKickoff)
        dest += PlayerAI.ballManager.trans.forward * Field.FIFTEEN_INCHES;
    }
    dest.y = 0.0f;
    this.animatorCommunicator.SetGoal(dest, 1f, PlayerAI.LookAtBallRotation(dest));
  }

  public void CalculateReceiverAnimSpeedAdjustmentForPass()
  {
    float currentEffort = this.animatorCommunicator.CurrentEffort;
    if ((double) PlayerAI.ballManager.rigidbd.velocity.magnitude > 0.0 && (double) this.animatorCommunicator.velocity.magnitude > 0.0)
    {
      float num1 = 0.03f;
      double num2 = (double) Vector3.Distance(PlayerAI.playersManager.passDestination, this.trans.position);
      float num3 = this.animatorCommunicator.velocity.magnitude * Time.deltaTime;
      double num4 = (double) num3;
      double num5 = num2 / num4;
      float num6 = Vector3.Distance(PlayerAI.ballManager.passTarget, PlayerAI.ballManager.trans.position) / (PlayerAI.ballManager.rigidbd.velocity.magnitude * Time.deltaTime);
      double num7 = (double) num6;
      float num8 = (float) (num5 - num7) * num3 / num6;
      float num9 = Mathf.Clamp((num3 + num8) / num3, 0.5f, 1f);
      this.currentAdjustedAnimSpeedForPass = (double) currentEffort > (double) num9 ? currentEffort - num1 : currentEffort + num1;
      this.currentAdjustedAnimSpeedForPass = Mathf.Clamp(this.currentAdjustedAnimSpeedForPass, 0.5f, 1f);
    }
    else
      this.currentAdjustedAnimSpeedForPass = currentEffort;
  }

  public bool RequestTackle() => this.tackleAbility.LookForTackleOpportunities(this);

  public void OnBrokenTackleKeyMoment_BallCarrier() => this._lastBrokenTackleTime = Time.time;

  public float BrokenTackleRecoveryCoefficient => MathUtils.MapRange(Mathf.Clamp(Time.time - this._lastBrokenTackleTime, 0.0f, 5f), 0.0f, 5f, 0.0f, 1f);

  public void OnTackleInteractionContact()
  {
    PlayerAI ballHolderScript = PlayerAI.playersManager.ballHolderScript;
    if ((UnityEngine.Object) ballHolderScript == (UnityEngine.Object) null || !this.nteractAgent.IsInInteractionWith(ballHolderScript.nteractAgent) || ballHolderScript.IsQB() && global::Game.CurrentPlayHasUserQBOnField && (double) Vector3.Distance(ballHolderScript.trans.position, PersistentSingleton<GamePlayerController>.Instance.position) > (double) PlayerAI._maxSuccessfulTackleUserDistanceFromAIQB)
      return;
    this.TackleStarted(ballHolderScript);
  }

  private void TackleStarted(PlayerAI ballHolder)
  {
    MatchManager.instance.playersManager.tackler = this;
    this.isTackling = true;
    ballHolder.IsTackled = true;
    PlayerAI.playManager.AddToCurrentPlayLog(this.firstName + " " + this.lastName + " (" + this.playerPosition.ToString() + ") tackled " + ballHolder.firstName + " " + ballHolder.lastName + " at the " + Field.GetYardLineStringByFieldLocation(ballHolder.trans.position.z) + " yard line");
    PEGameplayEventManager.RecordTackleEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, this, ballHolder, global::Game.CurrentPlayHasUserQBOnField && ballHolder.IsQB());
    if ((!ballHolder.IsQB() || !global::Game.CurrentPlayHasUserQBOnField ? 0 : (global::Game.UserControlsQB ? 1 : 0)) != 0)
    {
      this.TackleFinished(ballHolder);
    }
    else
    {
      if ((double) UnityEngine.Random.value >= (double) PlayerAI.FUMBLE_CHANCE || ballHolder.IsQB() && global::Game.CurrentPlayHasUserQBOnField && global::Game.UserControlsQB)
        return;
      PlayerAI.playersManager.ActivateFumble();
    }
  }

  public void OnTackleInteraction_TacklerOnGround()
  {
    AppSounds.Play3DSfx(ESfxTypes.kBodyFall, this.trans.position);
    if (!PlayerAI.playManager.PlayActive)
      return;
    PlayerAI ballHolderScript = PlayerAI.playersManager.ballHolderScript;
    if ((UnityEngine.Object) ballHolderScript == (UnityEngine.Object) null || !this.nteractAgent.IsInInteractionWith(ballHolderScript.nteractAgent))
      return;
    this.TackleFinished(ballHolderScript);
  }

  private void TackleFinished(PlayerAI ballHolder)
  {
    if (!global::Game.IsPlayActive)
      return;
    if (Field.IsBehindLineOfScrimmage(ballHolder.transform.position.z) && global::Game.IsNotTurnover)
    {
      if (global::Game.IsPass && !global::Game.BallIsThrownOrKicked)
      {
        this.SetCelebrationFromCategory(CelebrationCategory.DefensiveCelebration, "SACK");
        AppSounds.PlayPlayerCelebrationChatter(ESfxTypes.kCelebrationChatterSack, this.trans);
        if (this.onUserTeam)
        {
          ++ProEra.Game.MatchState.Stats.User.Sacks;
          ++PlayerAI.playManager.userTeamCurrentPlayStats.players[this.indexOnTeam].Sacks;
          ++PlayerAI.playManager.compTeamCurrentPlayStats.players[PlayerAI.playersManager.curCompScriptRef[5].indexOnTeam].QBSacked;
          if ((UnityEngine.Object) PlayerAI.playersManager.userPlayer == (UnityEngine.Object) this.mainGO)
            ++ProEra.Game.MatchState.Stats.User.Sacks;
        }
        else
        {
          ++ProEra.Game.MatchState.Stats.Comp.Sacks;
          ++PlayerAI.playManager.compTeamCurrentPlayStats.players[this.indexOnTeam].Sacks;
          ++PlayerAI.playManager.userTeamCurrentPlayStats.players[PlayerAI.playersManager.curUserScriptRef[5].indexOnTeam].QBSacked;
          if ((UnityEngine.Object) PlayerAI.playersManager.userPlayerP2 != (UnityEngine.Object) null && (UnityEngine.Object) PlayerAI.playersManager.userPlayerP2 == (UnityEngine.Object) this.mainGO)
            ++ProEra.Game.MatchState.Stats.Comp.Sacks;
        }
      }
      else
      {
        this.SetCelebrationFromCategory(CelebrationCategory.DefensiveCelebration, "TACKLE FOR LOSS");
        if (this.onUserTeam)
        {
          ++ProEra.Game.MatchState.Stats.User.TacklesForLoss;
          ++PlayerAI.playManager.userTeamCurrentPlayStats.players[this.indexOnTeam].TacklesForLoss;
        }
        else
        {
          ++ProEra.Game.MatchState.Stats.Comp.TacklesForLoss;
          ++PlayerAI.playManager.compTeamCurrentPlayStats.players[this.indexOnTeam].TacklesForLoss;
        }
      }
    }
    if (this.onUserTeam)
      ++PlayerAI.playManager.userTeamCurrentPlayStats.players[this.indexOnTeam].Tackles;
    else
      ++PlayerAI.playManager.compTeamCurrentPlayStats.players[this.indexOnTeam].Tackles;
    MatchManager.instance.playersManager.tackler = this;
    if (!((UnityEngine.Object) PlayerAI.playersManager.ballHolderScript != (UnityEngine.Object) null))
      return;
    PlayerAI.matchManager.EndPlay(PlayEndType.Tackle);
  }

  public void ForceTacklePlayer(PlayerAI ballHolder)
  {
    this.TackleStarted(ballHolder);
    this.TackleFinished(ballHolder);
  }

  private void SetAiTimingStoreReference()
  {
    if ((UnityEngine.Object) this._aiTimingStore == (UnityEngine.Object) null)
      this._aiTimingStore = MatchManager.instance.AITimeStore;
    if ((UnityEngine.Object) this._aiTimingStore != (UnityEngine.Object) null)
      this._aiTimingInterval = this._aiTimingStore.GetCurrentPlatformInterval();
    else
      Console.Error.WriteLine("PlayerAI::SetAiTimingStoreReference: _aiTimingStore = null");
  }

  public void RequestHandoff() => this._playInitiationAbility.LookForHandoffOpportunities(PlayerAI.playersManager, this);

  public void ExecuteHandoff()
  {
    PEGameplayEventManager.RecordHandoffTimeReachedEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position);
    if (global::Game.CurrentPlayHasUserQBOnField && global::Game.UserControlsQB || global::Game.IsPlayAction)
      return;
    this.ExecuteHandoff(0);
  }

  public void ExecuteHandoff(int toss)
  {
    bool flag = true;
    if (global::Game.IsReadOption)
    {
      HandoffConfig gameplayConfig = global::Game.GetGameplayConfig<HandoffConfig>();
      if ((double) UnityEngine.Random.Range(0.0f, 1f) < (double) gameplayConfig.chanceToKeepBall)
        flag = false;
    }
    if (flag)
    {
      if (toss == 0)
        PlayerAI.playersManager.HandOffBall(false);
      else
        PlayerAI.playersManager.HandOffBall(true);
      this.SetLookAtTarget((Transform) null, 0.0f);
    }
    else
      PlayerAI.playersManager.TriggerFakeHandoff();
  }

  private void OnHandoffInteraction_QuaterbackTrackEnd()
  {
    this.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.QuaterbackStrafe, this.LeftHanded);
    this.animatorCommunicator.SetGoal(this.trans.position);
  }

  public void RequestFieldGoal() => this._playInitiationAbility.LookForFieldGoalOpportunities(PlayerAI.playersManager, this);

  public void RequestKickoff(bool isOnsideKick) => this._playInitiationAbility.StartKickoff(this, isOnsideKick);

  public AffineTransform GetIdealTransformFromBallToKicker(bool isOnsideKick)
  {
    AffineTransform fromBallToKicker = this._playInitiationAbility.GetIdealTransformFromBallToKicker(isOnsideKick);
    fromBallToKicker.position.Scale(this.transform.lossyScale);
    return fromBallToKicker;
  }

  public void ResetBehaviorTree()
  {
    this.behaviorTree.SetVariableValue("OwnerPlayer", (object) this.gameObject);
    this.behaviorTree.SetVariableValue("GetToFormPos", (object) false);
    this.behaviorTree.SetVariableValue("AtLinePos", (object) false);
    this.behaviorTree.SetVariableValue("DoDefenseShift", (object) false);
    this.behaviorTree.SetVariableValue("DoShowBlitz", (object) false);
    this.behaviorTree.SetVariableValue("IsPlayerOffense", (object) this.onOffense);
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) (int) this.GetCurrentAssignId());
  }

  private void OnPlayInitiationInteractionStart() => ProEra.Game.MatchState.IsPlayInitiated.SetValue(true);

  private void OnDropBall()
  {
    if (this.IsQB() && global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB && global::Game.CurrentPlayHasUserQBOnField)
      return;
    PlayerAI.ballManager.DropBallAfterPlay(!global::Game.IsPlayActive);
    PlayerAI.playersManager.SetBallHolder(-1, false);
  }

  public void OnDropBallFromInteractionOrEvent()
  {
    if (global::Game.IsPlayActive || global::Game.PET_IsTouchdown || !((UnityEngine.Object) MatchManager.instance.playersManager.ballHolderScript == (UnityEngine.Object) this) && !((UnityEngine.Object) MatchManager.instance.playersManager.cachedBallHolderAtPlayEnd == (UnityEngine.Object) this))
      return;
    this.OnDropBall();
  }

  public void RequestSnap() => this._playInitiationAbility.LookForSnapOpportunities(PlayerAI.playersManager, this);

  public void RequestPunt() => this._playInitiationAbility.LookForPuntOpportunities(PlayerAI.playersManager, this);

  public Quaternion StanceTargetRotation()
  {
    Quaternion quaternion = !this.onOffense ? Field.DEFENSE_TOWARDS_LOS_QUATERNION : Field.OFFENSE_TOWARDS_LOS_QUATERNION;
    if (this.onOffense)
    {
      if (global::Game.IsFG)
      {
        if (this.indexInFormation == 5)
          quaternion = Quaternion.Euler(new Vector3(0.0f, (float) (45.0 + -90.0 * (double) global::Game.OffensiveFieldDirection), 0.0f));
        else if (this.indexInFormation == 6)
          quaternion = Quaternion.Euler(new Vector3(0.0f, (float) (90.0 * (double) global::Game.OffensiveFieldDirection - 45.0), 0.0f));
      }
      else if (PlayState.IsKickoff && this.indexInFormation == 6)
        quaternion = Quaternion.Euler(new Vector3(0.0f, (float) (90.0 * (double) global::Game.OffensiveFieldDirection - 45.0), 0.0f));
    }
    return quaternion;
  }

  public void SetNewAssignment(PlayAssignment newPlayerAssignment, bool shouldClearAssignments)
  {
    if (shouldClearAssignments)
      this.ClearAllAssignments();
    this.PlayAssignments.Push(newPlayerAssignment);
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) (int) newPlayerAssignment.GetAssignmentType());
  }

  public PlayAssignment GetCurrentAssignment() => this.PlayAssignments.Count > 0 ? this.PlayAssignments.Peek() : (PlayAssignment) null;

  public bool HasAssignmentInStack(EPlayAssignmentId assignId)
  {
    foreach (PlayAssignment playAssignment in this.PlayAssignments)
    {
      if (playAssignment.GetAssignmentType() == assignId)
        return true;
    }
    return false;
  }

  public void EndCurrentAssignment()
  {
    if (this.GetCurrentAssignment() != null)
      this.PlayAssignments.Pop();
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) (int) this.GetCurrentAssignId());
  }

  public EPlayAssignmentId GetCurrentAssignId()
  {
    PlayAssignment currentAssignment = this.GetCurrentAssignment();
    return currentAssignment != null ? currentAssignment.GetAssignmentType() : EPlayAssignmentId.None;
  }

  public void ClearAllAssignments()
  {
    this.PlayAssignments.Clear();
    this.behaviorTree.SetVariableValue("CurrentPlayAssignment", (object) 0);
  }

  public static Vector3 ClampMoveToFieldBounds(Vector3 dest)
  {
    Vector3 fieldBounds = dest;
    if ((double) fieldBounds.x > (double) Field.OUT_OF_BOUNDS - (double) Field.ONE_HALF_YARD)
      fieldBounds.x = Field.OUT_OF_BOUNDS - Field.ONE_HALF_YARD;
    else if ((double) fieldBounds.x < -(double) Field.OUT_OF_BOUNDS + (double) Field.ONE_HALF_YARD)
      fieldBounds.x = -Field.OUT_OF_BOUNDS + Field.ONE_HALF_YARD;
    else if ((double) fieldBounds.z > (double) Field.NORTH_BACK_OF_ENDZONE - (double) Field.ONE_HALF_YARD)
      fieldBounds.z = Field.NORTH_BACK_OF_ENDZONE - Field.ONE_HALF_YARD;
    else if ((double) fieldBounds.z < (double) Field.SOUTH_BACK_OF_ENDZONE + (double) Field.ONE_HALF_YARD)
      fieldBounds.z = Field.SOUTH_BACK_OF_ENDZONE + Field.ONE_HALF_YARD;
    return fieldBounds;
  }

  private void OnTriggerEnter(Collider other) => this.AttemptToBlock(other);

  private void OnTriggerStay(Collider other) => this.AttemptToBlock(other);

  private void AttemptToBlock(Collider other)
  {
    if (!this.onOffense || this.isEngagedInBlock || !this.lookForBlockTarget || this.GetCurrentAssignId() != EPlayAssignmentId.PassBlock)
      return;
    PlayerAI component = other.gameObject.GetComponent<PlayerAI>();
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || component.onOffense)
      return;
    this.blockTarget = component;
  }

  public void ForceStopAllNteractCallbacks()
  {
    if (!((UnityEngine.Object) this.nteractAgent != (UnityEngine.Object) null))
      return;
    this.nteractAgent.AllowCallbacks = false;
  }

  public BlockAbility PlayerBlockAbility => this._blockAbility;

  private void UpdateBlocking()
  {
    if (this.isEngagedInBlock)
    {
      this.blockDuration -= this._aiTimingInterval;
      this.inBlockWithScript.CheckForBlockDisengage();
      if ((double) this.blockDuration > 0.0)
        return;
      this._blockAbility.ExitBlock();
    }
    else if ((UnityEngine.Object) this.initialBlockTarget != (UnityEngine.Object) null)
    {
      this.animatorCommunicator.SetGoal(this.GetPlayerPursuitAngle(this.initialBlockTarget));
    }
    else
    {
      if (!this.lookForBlockTarget || this.blockType != BlockType.MoveToBallHolder && this.blockType != BlockType.QBOnRunPlay && this.blockType != BlockType.StayWhereYouAre)
        return;
      if (this.inBlockZone)
      {
        Vector3 blockTarget = this.FindBlockTarget();
        if ((UnityEngine.Object) this.blockTarget != (UnityEngine.Object) null)
          this.animatorCommunicator.SetGoal(blockTarget);
        else if (this.blockType == BlockType.MoveToBallHolder)
          this.RunOpenFieldBlockingLogic();
        else if (this.blockType == BlockType.StayWhereYouAre)
        {
          this.animatorCommunicator.SetGoal(this.trans.position);
        }
        else
        {
          if (this.blockType != BlockType.PassBlockNormal)
            return;
          this.animatorCommunicator.SetGoal(this.trans.position, Field.OFFENSE_TOWARDS_LOS_QUATERNION);
        }
      }
      else
        this.animatorCommunicator.SetGoal(PlayerAI.playersManager.blockZone.position + new Vector3(0.0f, 0.0f, Field.FOUR_YARDS) * (float) global::Game.OffensiveFieldDirection);
    }
  }

  public static void SetInitialBlockTargets()
  {
    PlayerAI.validBlockers.Clear();
    PlayerAI.validBlockTargets.Clear();
    List<PlayerAI> playerAiList1;
    List<PlayerAI> playerAiList2;
    if (global::Game.IsPlayerOneOnOffense)
    {
      playerAiList1 = MatchManager.instance.playersManager.curUserScriptRef;
      playerAiList2 = MatchManager.instance.playersManager.curCompScriptRef;
    }
    else
    {
      playerAiList1 = MatchManager.instance.playersManager.curCompScriptRef;
      playerAiList2 = MatchManager.instance.playersManager.curUserScriptRef;
    }
    for (int index = 0; index < playerAiList1.Count; ++index)
    {
      if ((double) Mathf.Abs(MatchManager.ballOn - playerAiList1[index].trans.position.z) <= (double) Field.ONE_YARD * 2.0 && playerAiList1[index].blockType == BlockType.MoveToBallHolder && playerAiList1[index].savedStance == 0 && !playerAiList1[index].IsPullingLineman())
        PlayerAI.validBlockers.Add(playerAiList1[index]);
    }
    for (int index = 0; index < playerAiList2.Count; ++index)
    {
      if (playerAiList2[index].savedStance == 0)
        PlayerAI.validBlockTargets.Add(playerAiList2[index]);
    }
    for (int index1 = 0; index1 < PlayerAI.validBlockTargets.Count; ++index1)
    {
      float num1 = 100f;
      int index2 = -1;
      for (int index3 = 0; index3 < PlayerAI.validBlockers.Count; ++index3)
      {
        float num2 = Vector3.Distance(PlayerAI.validBlockTargets[index1].trans.position, PlayerAI.validBlockers[index3].trans.position);
        if ((double) num2 < (double) num1)
        {
          num1 = num2;
          index2 = index3;
        }
      }
      if (index2 != -1)
      {
        PlayerAI.validBlockers[index2].initialBlockTarget = PlayerAI.validBlockTargets[index1];
        PlayerAI.validBlockTargets[index1].blockerAssignedToThisDefender = PlayerAI.validBlockers[index2];
        PlayerAI.validBlockers[index2].blockTarget = PlayerAI.validBlockers[index2].initialBlockTarget;
        MonoBehaviour.print((object) ("Assigning offensive: " + PlayerAI.validBlockers[index2].number.ToString() + " to block defensive: " + PlayerAI.validBlockTargets[index1].number.ToString()));
        Debug.DrawLine(PlayerAI.validBlockers[index2].trans.position, PlayerAI.validBlockers[index2].initialBlockTarget.trans.position, Color.white, PlayerAI.validBlockers[index2]._aiTimingInterval);
        PlayerAI.validBlockers.RemoveAt(index2);
      }
    }
    for (int index4 = 0; index4 < PlayerAI.validBlockers.Count; ++index4)
    {
      if (global::Game.IsPlayAction)
      {
        PlayerAI.validBlockers[index4].path[1] = 0.0f;
        PlayerAI.validBlockers[index4].path[2] = 0.0f;
        PlayerAI.validBlockers[index4].blockType = BlockType.PassBlockNormal;
      }
      else
      {
        float num3 = 100f;
        int index5 = -1;
        for (int index6 = 0; index6 < playerAiList2.Count; ++index6)
        {
          if (!((UnityEngine.Object) playerAiList2[index6].blockerAssignedToThisDefender != (UnityEngine.Object) null))
          {
            float num4 = Vector3.Distance(PlayerAI.validBlockers[index4].trans.position, playerAiList2[index6].trans.position);
            if ((double) num4 < (double) num3)
            {
              num3 = num4;
              index5 = index6;
            }
          }
        }
        if (index5 != -1)
        {
          PlayerAI.validBlockers[index4].initialBlockTarget = playerAiList2[index5];
          playerAiList2[index5].blockerAssignedToThisDefender = PlayerAI.validBlockers[index4];
          PlayerAI.validBlockers[index4].blockTarget = PlayerAI.validBlockers[index4].initialBlockTarget;
          MonoBehaviour.print((object) ("Assigning secondary block target. Offensive: " + PlayerAI.validBlockers[index4].number.ToString() + " to block defensive: " + playerAiList2[index5].number.ToString()));
          Debug.DrawLine(PlayerAI.validBlockers[index4].trans.position, PlayerAI.validBlockers[index4].initialBlockTarget.trans.position, Color.white, 3f);
        }
      }
    }
  }

  public void CheckForBlockDisengage()
  {
    if ((double) PlayerAI.matchManager.playTimer < 1.7000000476837158 || (UnityEngine.Object) PlayerAI.playersManager.ballHolderScript == (UnityEngine.Object) null)
      return;
    Vector3 position = PlayerAI.playersManager.ballHolderScript.trans.position;
    Vector3 normalized = (position - this.trans.position).normalized;
    bool flag = Field.FurtherDownfield(position.z + Field.ONE_YARD_FORWARD * 4f, this.trans.position.z);
    if (!((double) Vector3.Dot(this.trans.forward, normalized) < 0.20000000298023224 & flag))
      return;
    this._blockAbility.ExitBlock();
  }

  public void CheckBlockZone(Vector3 pos)
  {
    if (!this.isEngagedInBlock && (double) Vector3.Distance(this.trans.position, pos + Vector3.forward * Field.FOUR_YARDS) < (double) Field.ONE_YARD * 30.0)
      this.inBlockZone = true;
    else
      this.inBlockZone = false;
  }

  public Vector3 FindBlockTarget()
  {
    float num1 = 60f;
    if (this.blockType == BlockType.MoveToBallHolder && Field.FurtherDownfield((float) ProEra.Game.MatchState.BallOn, this.trans.position.z + Field.ONE_YARD_FORWARD))
      num1 = 22f;
    else if (this.blockType == BlockType.PassBlockBegin)
      num1 = 45f;
    else if (this.blockType == BlockType.PassBlockNormal)
      num1 = 90f;
    List<PlayerAI> playerAiList = (bool) ProEra.Game.MatchState.Turnover ? global::Game.OffensivePlayers : global::Game.DefensivePlayers;
    float num2 = Field.ONE_YARD * 10f;
    if (PlayState.IsKickoff)
      num2 = Field.ONE_YARD * 20f;
    else if (PlayState.IsPass)
    {
      num2 = Field.ONE_YARD * 10f;
      if (!this.IsLineman() && Field.FurtherDownfield((float) ProEra.Game.MatchState.BallOn, this.trans.position.z))
        num2 = Field.ONE_YARD * 5f;
    }
    else if (PlayState.IsRun)
      num2 = Field.ONE_YARD * 5f;
    PlayerAI targetPlayer = (PlayerAI) null;
    for (int index = 0; index < 11; ++index)
    {
      PlayerAI playerAi = playerAiList[index];
      if (playerAi.inBlockZone || (bool) ProEra.Game.MatchState.Turnover)
      {
        float num3 = Vector3.Distance(this.trans.position, playerAi.trans.position);
        float num4 = Vector3.Angle(this.trans.forward, playerAi.trans.position - this.trans.position);
        if (!playerAi.isEngagedInBlock && (UnityEngine.Object) playerAi.mainGO != (UnityEngine.Object) this.inBlockWith && (double) num3 < (double) num2 && (double) num4 <= (double) num1 && ((UnityEngine.Object) playerAi.blockerAssignedToThisDefender == (UnityEngine.Object) null || (double) num3 < (double) playerAi.distanceToBlocker) && (playerAi.defType == EPlayAssignmentId.Blitz || PlayerAI.playersManager.convergeOnBall || global::Game.IsRun) && playerAi.tackleType == 0)
        {
          targetPlayer = playerAi;
          num2 = num3;
        }
      }
    }
    if ((UnityEngine.Object) targetPlayer == (UnityEngine.Object) null)
    {
      this.blockTarget = (PlayerAI) null;
      return Vector3.zero;
    }
    this.blockTarget = targetPlayer;
    this.blockTarget.distanceToBlocker = num2;
    this.blockTarget.blockerAssignedToThisDefender = this;
    if ((bool) ProEra.Game.MatchState.Turnover)
      return this.GetPlayerPursuitAngle(targetPlayer);
    Vector3 blockTarget = !PlayState.IsPass || global::Game.BallIsThrownOrKicked ? this.GetPlayerPursuitAngle(targetPlayer) : targetPlayer.trans.position + targetPlayer.trans.forward * num2 * Field.ONE_YARD_SIX_INCHES;
    if (PlayerAI.bShowBlockDebug)
    {
      Debug.DrawLine(this.trans.position + Vector3.up, blockTarget + Vector3.up, Color.red, this._aiTimingInterval * 2f);
      Debug.DrawLine(this.trans.position + Vector3.up, this.blockTarget.trans.position + Vector3.up, Color.yellow, this._aiTimingInterval * 2f);
    }
    return blockTarget;
  }

  private bool CheckForEngage(PlayerAI blockTarget) => !global::Game.IsPlayOver && (!global::Game.IsPass || (double) MatchManager.instance.playTimer >= (double) global::Game.PassBlockConfig.minPlayTimeForPassBlockEngagement) && (UnityEngine.Object) blockTarget != (UnityEngine.Object) null && ((this.CurrentPlayAssignment == null ? 0 : (this.CurrentPlayAssignment.GetAssignmentType() == EPlayAssignmentId.KickReturnBlocker ? 1 : 0)) != 0 || !global::Game.BS_IsOnGround) && this.blockType != BlockType.None && this.onUserTeam != blockTarget.onUserTeam && (UnityEngine.Object) PlayerAI.playersManager.ballHolder != (UnityEngine.Object) this.mainGO && !this.isEngagedInBlock && !blockTarget.isEngagedInBlock;

  private static void EngageBlocking(PlayerAI blocker, PlayerAI defender)
  {
    NteractAgent nteractAgent1 = blocker.nteractAgent;
    NteractAgent nteractAgent2 = defender.nteractAgent;
    AnimationEventAgent eventAgent1 = blocker.eventAgent;
    AnimationEventAgent eventAgent2 = defender.eventAgent;
    if (nteractAgent1.IsInsideInteraction && !nteractAgent1.CanBeInterupted || nteractAgent2.IsInsideInteraction && !nteractAgent2.CanBeInterupted || eventAgent1.IsInsideEvent || eventAgent2.IsInsideEvent)
      return;
    BlockAbility.BlockTypeInteractionTag typeTagId1 = blocker.blockType == BlockType.MoveToBallHolder ? BlockAbility.BlockTypeInteractionTag.Run : BlockAbility.BlockTypeInteractionTag.Pass;
    float z = blocker.trans.position.z;
    bool flag = global::Game.IsKickoff || global::Game.IsPunt ? Field.IsBehindLineOfScrimmage(z) : Field.IsBeyondLineOfScrimmage(z);
    BlockAbility.BlockTypeInteractionTag typeTagId2 = BlockAbility.BlockTypeInteractionTag.None;
    if (global::Game.IsKickoff)
    {
      if (blocker.indexInFormation > 5)
        typeTagId1 = BlockAbility.BlockTypeInteractionTag.Chip;
      else
        typeTagId2 = BlockAbility.BlockTypeInteractionTag.Chip;
    }
    else if (global::Game.IsPunt)
    {
      if (blocker.onOffense && global::Game.BallIsThrownOrKicked)
      {
        if (blocker.indexInFormation > 5)
          typeTagId1 = BlockAbility.BlockTypeInteractionTag.Chip;
        else
          typeTagId2 = BlockAbility.BlockTypeInteractionTag.Chip;
      }
    }
    else
      typeTagId1 = (double) PlayerAI.matchManager.playTimer > 1.0 & flag ? BlockAbility.BlockTypeInteractionTag.Chip : typeTagId1;
    blocker._cachedBlockDuration = blocker.GetBlockDuration(defender);
    blocker._cachedBlockedDefender = defender;
    blocker._blockAbility.LookForBlockOpportunities(blocker, defender, (int) typeTagId1);
    if (typeTagId2 == BlockAbility.BlockTypeInteractionTag.None)
      return;
    blocker._blockAbility.LookForBlockOpportunities(blocker, defender, (int) typeTagId2);
  }

  private float GetBlockDuration(PlayerAI defScript) => this.blocking >= defScript.blockBreaking || (double) UnityEngine.Random.value >= (double) PlayerAI._defenderEarlyExitChance ? (GameSettings.DifficultyLevel.Value != 0 ? (GameSettings.DifficultyLevel.Value != 1 ? (GameSettings.DifficultyLevel.Value != 2 ? UnityEngine.Random.Range(PlayerAI._easyDifficultyLoopDurationRange.x, PlayerAI._easyDifficultyLoopDurationRange.y) : UnityEngine.Random.Range(PlayerAI._hardDifficultyLoopDurationRange.x, PlayerAI._hardDifficultyLoopDurationRange.y)) : UnityEngine.Random.Range(PlayerAI._mediumDifficultyLoopDurationRange.x, PlayerAI._mediumDifficultyLoopDurationRange.y)) : UnityEngine.Random.Range(PlayerAI._easyDifficultyLoopDurationRange.x, PlayerAI._easyDifficultyLoopDurationRange.y)) : 0.0f;

  private void SetAxisParamsOnBlockExit()
  {
    this.isEngagedInBlock = false;
    this.inBlockWith = (GameObject) null;
  }

  private void OnBlockInteractionExit_Defender() => this.SetAxisParamsOnBlockExit();

  private void OnBlockInteractionExit_Blocker() => this.SetAxisParamsOnBlockExit();

  private void SetAxisParamsOnBlockEntry(PlayerAI otherPlayerAI, float duration)
  {
    if ((UnityEngine.Object) this == (UnityEngine.Object) null)
    {
      Debug.LogError((object) "PlayerAI_Blocking - SetAxisParamsOnBlockEntry - This is NULL!!");
    }
    else
    {
      this.isEngagedInBlock = true;
      if ((UnityEngine.Object) otherPlayerAI == (UnityEngine.Object) null)
        Debug.LogError((object) "otherPlayerAI is null when setting SetAxisParamsOnBlockEntry");
      else
        this.inBlockWith = otherPlayerAI.mainGO;
      this.inBlockWithScript = otherPlayerAI;
      this.blockDuration = duration;
      this.distanceToBlocker = 100f;
    }
  }

  private void OnBlockEnter_Blocker()
  {
    if ((UnityEngine.Object) this == (UnityEngine.Object) null)
      Debug.LogError((object) "PlayerAI_Blocking - OnBlockInteractionEnter_Blocker - This is NULL!!");
    else if ((UnityEngine.Object) this._cachedBlockedDefender == (UnityEngine.Object) null)
    {
      Debug.LogError((object) "PlayerAI_Blocking - OnBlockInteractionEnter_Blocker - _cachedBlockedDefender is NULL!!");
    }
    else
    {
      this.SetAxisParamsOnBlockEntry(this._cachedBlockedDefender, this._cachedBlockDuration);
      this.blockTarget = this._cachedBlockedDefender;
      this.blockTargetScore = 10000f;
      this.lookForBlockTarget = false;
      if (this.posInBlockForm != -1)
      {
        PlayerAI.playersManager.blockScript.blockPosFilled[this.posInBlockForm] = false;
        this.posInBlockForm = -1;
      }
      this._cachedBlockedDefender.SetAxisParamsOnBlockEntry(this, this._cachedBlockDuration);
      this._cachedBlockedDefender.blockTarget = (PlayerAI) null;
      this._cachedBlockedDefender.blockTargetScore = -1000f;
      if (!((UnityEngine.Object) this._cachedBlockedDefender.blockerAssignedToThisDefender != (UnityEngine.Object) null))
        return;
      this._cachedBlockedDefender.blockerAssignedToThisDefender.initialBlockTarget = (PlayerAI) null;
      this._cachedBlockedDefender.blockerAssignedToThisDefender = (PlayerAI) null;
    }
  }

  private void OnBlockInteractionEnter_Blocker()
  {
    this.OnBlockEnter_Blocker();
    this._blockAbility.StartBlockProgressTracking(this._blockAbility, this._cachedBlockedDefender._blockAbility);
  }

  private void OnChipBlockInteractionEnter_Blocker() => this.OnBlockEnter_Blocker();

  public static void NotifyAllPlayersOfDeflectedPass()
  {
    for (int index = 0; index < 11; ++index)
    {
      MatchManager.instance.playersManager.curUserScriptRef[index].NotifyPlayerOfDeflectedPass();
      MatchManager.instance.playersManager.curCompScriptRef[index].NotifyPlayerOfDeflectedPass();
    }
  }

  public void NotifyPlayerOfDeflectedPass()
  {
    if ((double) Vector3.Distance(this.trans.position, PlayerAI.ballManager.trans.position) > 7.0 * (double) Field.ONE_YARD)
      return;
    this.SetIsGoingForThrownBall(true);
  }

  private IEnumerator ReactToDeflectedPass(int _framesToWait)
  {
    for (int i = 0; i < _framesToWait; ++i)
      yield return (object) null;
    this.SetIsGoingForThrownBall(true);
  }

  public void MovementManagement()
  {
  }

  private bool LocalAvoidance(ref Vector3 lookDir)
  {
    if (!this.onOffense && this.defType == EPlayAssignmentId.Blitz || this.blockType != BlockType.None && this.onOffense && global::Game.IsPass && global::Game.BallIsNotThrownOrKicked || this.onOffense && global::Game.IsRun && (UnityEngine.Object) this != (UnityEngine.Object) PlayerAI.playersManager.ballHolderScript || this.IsQB() && !PlayerAI.playersManager.forceQBScramble || (UnityEngine.Object) this == (UnityEngine.Object) PlayerAI.playersManager.ballHolderScript && (global::Game.OffenseGoingNorth && (double) this.trans.forward.z < 0.0 || !global::Game.OffenseGoingNorth && (double) this.trans.forward.z > 0.0))
      return false;
    float twoYards = Field.TWO_YARDS;
    Vector3 normalized1 = lookDir.normalized;
    Vector3 vector3_1 = this.trans.position + Vector3.up + normalized1 * 0.4f;
    Vector3 vector3_2 = this.trans.right * 0.2f;
    Vector3 vector3_3 = this.trans.right;
    if (this.iAnimQuerries.IsStrafing())
    {
      vector3_3 = this.trans.forward;
      vector3_2 = this.trans.forward * 0.2f;
    }
    Vector3 vector3_4 = normalized1 + vector3_3 * -0.4f;
    Vector3 normalized2 = vector3_4.normalized;
    vector3_4 = normalized1 + vector3_3 * 0.4f;
    Vector3 normalized3 = vector3_4.normalized;
    Vector3 vector3_5 = vector3_1 - vector3_2 * 4f - normalized1 * 0.2f;
    Vector3 vector3_6 = vector3_1 + vector3_2 * 4f - normalized1 * 0.2f;
    vector3_4 = normalized1 + vector3_3 * -1.6f;
    Vector3 normalized4 = vector3_4.normalized;
    vector3_4 = normalized1 + vector3_3 * 1.6f;
    Vector3 normalized5 = vector3_4.normalized;
    PlayerAI playerAi1 = (PlayerAI) null;
    Color color1 = Color.white;
    Color color2 = Color.white;
    Color color3 = Color.white;
    Color color4 = Color.white;
    Color color5 = Color.white;
    if (Physics.RaycastNonAlloc(vector3_1, normalized1, this._cachedHits, twoYards, PlayerAI.layerMask) != 0)
    {
      PlayerAI playerAi2 = AssetCache.GetPlayerAI(this._cachedHits[0].transform);
      if (this.NeedToAvoid(playerAi2))
      {
        color3 = Color.red;
        playerAi1 = playerAi2;
      }
      else
        color3 = Color.yellow;
    }
    if (Physics.RaycastNonAlloc(vector3_5, normalized4, this._cachedHits, twoYards * 1.2f, PlayerAI.layerMask) != 0)
    {
      PlayerAI playerAi3 = AssetCache.GetPlayerAI(this._cachedHits[0].transform);
      if (this.NeedToAvoid(playerAi3))
      {
        color1 = Color.red;
        playerAi1 = playerAi3;
      }
      else
        color1 = Color.yellow;
    }
    if (Physics.RaycastNonAlloc(vector3_1 - vector3_2, normalized2, this._cachedHits, twoYards, PlayerAI.layerMask) != 0)
    {
      PlayerAI playerAi4 = AssetCache.GetPlayerAI(this._cachedHits[0].transform);
      if (this.NeedToAvoid(playerAi4))
      {
        color2 = Color.red;
        playerAi1 = playerAi4;
      }
      else
        color2 = Color.yellow;
    }
    if (Physics.RaycastNonAlloc(vector3_1 + vector3_2, normalized3, this._cachedHits, twoYards, PlayerAI.layerMask) != 0)
    {
      PlayerAI playerAi5 = AssetCache.GetPlayerAI(this._cachedHits[0].transform);
      if (this.NeedToAvoid(playerAi5))
      {
        color4 = Color.red;
        playerAi1 = playerAi5;
      }
      else
        color4 = Color.yellow;
    }
    if (Physics.RaycastNonAlloc(vector3_6, normalized5, this._cachedHits, twoYards * 1.2f, PlayerAI.layerMask) != 0)
    {
      PlayerAI playerAi6 = AssetCache.GetPlayerAI(this._cachedHits[0].transform);
      if (this.NeedToAvoid(playerAi6))
      {
        color5 = Color.red;
        playerAi1 = playerAi6;
      }
      else
        color5 = Color.yellow;
    }
    if (PlayerAI.sDebugAvoidance)
    {
      Debug.DrawRay(vector3_5, normalized4 * (twoYards * 1.2f), color1);
      Debug.DrawRay(vector3_1 - vector3_2, normalized2 * twoYards, color2);
      Debug.DrawRay(vector3_1, normalized1 * twoYards, color3);
      Debug.DrawRay(vector3_1 + vector3_2, normalized3 * twoYards, color4);
      Debug.DrawRay(vector3_6, normalized5 * (twoYards * 1.2f), color5);
    }
    if (!((UnityEngine.Object) playerAi1 != (UnityEngine.Object) null))
      return false;
    vector3_4 = playerAi1.trans.position + playerAi1.trans.forward * 1.2f - this.trans.position;
    Vector3 normalized6 = vector3_4.normalized;
    float num = 350f;
    Vector3 vector3_7 = !this.iAnimQuerries.IsStrafing() ? this.trans.right * num * Time.deltaTime : this.trans.forward * num * Time.deltaTime;
    if ((double) Vector3.Dot(this.trans.right, normalized6) > 0.0)
      lookDir -= vector3_7;
    else
      lookDir += vector3_7;
    return true;
  }

  private void Update()
  {
    if (global::Game.IsPlayActive && this.detectedSnap || (bool) PlayState.PlayOver)
      return;
    this.HandlePreplay();
  }

  public void OnCollisionEnter_Handler(GameObject hitObject, PlayerAI objScript)
  {
  }

  public float FindPassRushers(out Vector3 resultClosestRusher)
  {
    float num1 = 100f;
    float num2 = 0.0f;
    Vector3 vector3 = Vector3.zero;
    for (int index = 0; index < 11; ++index)
    {
      PlayerAI defensivePlayer = global::Game.DefensivePlayers[index];
      float num3 = Vector3.Distance(defensivePlayer.trans.position, this.trans.position);
      if ((double) num3 <= 5.4863996505737305)
      {
        float num4 = (float) ((double) PlayerAI.MAX_PASS_RUSH_SCORE + (double) PlayerAI.MIN_PASS_RUSH_SCORE - (double) PlayerAI.MAX_PASS_RUSH_SCORE / 5.4863996505737305 * (double) num3);
        if (defensivePlayer.isEngagedInBlock)
          num4 *= 0.0f;
        num2 += num4;
        if ((double) num3 < (double) num1)
        {
          vector3 = defensivePlayer.trans.position;
          num1 = num3;
        }
      }
    }
    float num5 = 0.0f;
    for (int index = 0; index < 11; ++index)
    {
      PlayerAI offensivePlayer = global::Game.OffensivePlayers[index];
      if ((offensivePlayer.blockType == BlockType.PassBlockBegin || offensivePlayer.blockType == BlockType.PassBlockNormal) && !offensivePlayer.isEngagedInBlock)
      {
        float num6 = Vector3.Distance(offensivePlayer.trans.position, this.trans.position);
        if ((double) num6 <= 5.4863996505737305)
        {
          float num7 = (float) ((double) PlayerAI.MAX_PASS_RUSH_SCORE + (double) PlayerAI.MIN_PASS_RUSH_SCORE - (double) PlayerAI.MAX_PASS_RUSH_SCORE / 5.4863996505737305 * (double) num6);
          num5 += num7;
        }
      }
    }
    float passRushers = num2 - num5 * 0.45f;
    resultClosestRusher = vector3;
    return passRushers;
  }

  public static void FindOpenReceiver(bool imminentDanger, bool forceThrowBallAway)
  {
    if (MatchManager.instance.playersManager.throwStarted)
      return;
    for (int index = 0; index < PlayerAI.receiverScore.Length; ++index)
      PlayerAI.receiverScore[index] = 0;
    for (int index = 6; index < 11; ++index)
      PlayerAI.receiverScore[index - 6] = PlayerAI.GetReceiverOpenessScore(global::Game.OffensivePlayers[index]);
    int num1 = 0;
    int num2 = 0;
    float num3 = 1f;
    for (int index = 0; index < PlayerAI.receiverScore.Length; ++index)
    {
      int num4 = (int) ((double) (100 - global::Game.OffensiveQB.awareness) * (double) num3);
      if (UnityEngine.Random.Range(0, 2) == 0)
        num4 *= -1;
      PlayerAI.receiverScore[index] += num4;
    }
    for (int index = 6; index < 11; ++index)
      Debug.Log((object) (global::Game.OffensivePlayers[index].number.ToString() + " has a TOTAL value = " + PlayerAI.receiverScore[index - 6].ToString()));
    for (int index = 0; index < PlayerAI.receiverScore.Length; ++index)
    {
      if (PlayerAI.receiverScore[index] > num1)
      {
        num2 = index;
        num1 = PlayerAI.receiverScore[index];
      }
    }
    float num5 = !imminentDanger ? PlayerAI.BASE_THROW_THESHOLD - PlayerAI.matchManager.playTimer : PlayerAI.THROW_NOW_THRESHOLD - PlayerAI.matchManager.playTimer;
    Debug.Log((object) ("Required receiver score to throw: " + num5.ToString()));
    MonoBehaviour.print((object) "----------------------------------------------------------------------\n\n");
    if (!((double) num1 >= (double) num5 | forceThrowBallAway))
      return;
    Debug.Log((object) ("timer = " + MatchManager.instance.playTimer.ToString() + ", " + global::Game.OffensivePlayers[num2 + 6].firstName + " " + global::Game.OffensivePlayers[num2 + 6].lastName + ", bestReceiver = " + (num2 + 6).ToString() + ", bestScore = " + num1.ToString() + ", imminentDanger = " + imminentDanger.ToString()));
    if (global::Game.OffensiveQB.eventAgent.IsInsideEvent || global::Game.OffensiveQB.nteractAgent.IsInsideInteraction)
      return;
    if (forceThrowBallAway)
    {
      MatchManager.instance.playersManager.isThrowingBallAway = true;
      MatchManager.instance.playersManager.intendedReceiver = (PlayerAI) null;
    }
    else
      MatchManager.instance.playersManager.intendedReceiver = global::Game.OffensivePlayers[num2 + 6];
    if (forceThrowBallAway)
      MonoBehaviour.print((object) "QB is throwing ball away!");
    MatchManager.instance.playersManager.compReceiver = num2 + 6;
    MatchManager.instance.playersManager.BeginQBThrow();
    MonoBehaviour.print((object) ("Throwing ball at: " + PlayerAI.matchManager.playTimer.ToString()));
  }

  public static int GetReceiverOpenessScore(PlayerAI receiver)
  {
    int num1 = 0;
    if (receiver.blockType != BlockType.None || Field.IsBehindLineOfScrimmage(receiver.trans.position.z) && ((double) MatchManager.instance.playTimer < 1.3300000429153442 || (double) Mathf.Abs(receiver.trans.position.x - global::Game.OffensiveQB.trans.position.x) < (double) Field.SIX_YARDS))
      return -1000;
    if (receiver.indexInFormation == global::Game.PrimaryReceiverIndex)
      num1 += 30;
    float num2 = 100f;
    for (int index = 0; index < 11; ++index)
    {
      if (!global::Game.DefensivePlayers[index].isEngagedInBlock)
      {
        float num3 = Vector3.Distance(receiver.trans.position, global::Game.DefensivePlayers[index].trans.position);
        Vector3 rhs = global::Game.DefensivePlayers[index].trans.position - receiver.trans.position;
        if ((double) Vector3.Dot(receiver.trans.forward, rhs) < 0.0)
          num3 *= 2f;
        if ((double) num3 < (double) MatchManager.instance.playersManager.playersCloseDistance)
          num1 -= 10;
        else if ((double) num3 < (double) MatchManager.instance.playersManager.playersNearbyDistance)
          num1 -= 20;
        if ((double) num3 < (double) num2)
          num2 = num3;
      }
    }
    return num1 + Mathf.RoundToInt(num2 * 50f) + PlayerAI.GetReceiverFieldValue(receiver.trans.position.z, ProEra.Game.MatchState.Down.Value, ProEra.Game.MatchState.FirstDown.Value, MatchManager.instance.savedLineOfScrim);
  }

  private static int GetReceiverFieldValue(
    float receiverZPos,
    int down,
    float firstDown,
    float lineOfScrimmage)
  {
    float num1 = 6f;
    float num2 = Field.FurtherDownfield(firstDown, Field.OFFENSIVE_GOAL_LINE) ? Field.OFFENSIVE_GOAL_LINE : firstDown;
    switch (down)
    {
      case 3:
        float num3 = Mathf.Abs(receiverZPos - lineOfScrimmage) / Mathf.Abs(num2 + Field.ONE_YARD_FORWARD - lineOfScrimmage);
        return (double) num3 < 0.75 ? -75 : Mathf.RoundToInt(num3 * 100f);
      case 4:
        return Field.FurtherDownfield(receiverZPos, num2 + Field.ONE_YARD_FORWARD) ? 100 + Mathf.RoundToInt(Mathf.Abs(receiverZPos - lineOfScrimmage) * Field.ONE_UNIT_PER_YARD) : -150;
      default:
        return Field.FurtherDownfield(lineOfScrimmage + Field.ONE_YARD_FORWARD * 5f, receiverZPos) ? -100 : Mathf.RoundToInt(Mathf.Abs(receiverZPos - lineOfScrimmage) * Field.ONE_UNIT_PER_YARD * num1);
    }
  }

  public static Vector3 GetPassLocation(PlayerAI receiver, bool bulletPass = false)
  {
    bool flag = false;
    Vector3 normalized1 = (receiver.animatorCommunicator.CurrentGoal.position - receiver.trans.position).normalized;
    float num1 = 1.8f;
    float num2 = 0.2f;
    float num3 = 0.37f;
    float num4 = 0.7f;
    Vector3 position1 = receiver.trans.position;
    Vector3 position2 = MatchManager.instance.playersManager.ballHolderScript.trans.position;
    float distance = Vector3.Distance(position1, position2);
    int yards = Field.ConvertDistanceToYards(distance);
    float num5 = (float) ((double) Field.ONE_YARD * 2.0 * ((double) yards * 0.10000000149011612));
    Vector3 vector3_1;
    if ((double) Vector3.Distance(receiver.trans.position, receiver.animatorCommunicator.CurrentGoal.position) < (double) num5 && receiver.IsThereAnotherPointInPath())
    {
      flag = true;
      Vector3 nextPointInPath = receiver.GetNextPointInPath();
      vector3_1 = receiver.GetPointInPathAfterCurrent() - nextPointInPath;
      normalized1 = vector3_1.normalized;
    }
    Vector3 vector3_2 = normalized1 * num1;
    vector3_1 = position1 - position2;
    Vector3 normalized2 = vector3_1.normalized;
    double num6 = (double) Vector3.Dot(normalized1, normalized2);
    Vector3 vector3_3 = (float) num6 * normalized1 * distance * num2;
    Vector3 vector3_4 = normalized1 * distance * num3;
    if (num6 < -0.75)
      vector3_4 *= 0.3f;
    if (flag)
      vector3_4 *= num4;
    if ((double) receiver.animatorCommunicator.speed < 1.0)
    {
      float num7 = 1f - receiver.animatorCommunicator.speed;
      vector3_4 *= num7;
    }
    float num8 = 1f;
    if (bulletPass)
      num8 = 0.9f;
    Vector3 vector3_5 = vector3_4 * num8;
    Vector3 vector3_6 = position1 + vector3_2 + vector3_5 + vector3_3;
    if ((double) vector3_6.x < -(double) Field.OUT_OF_BOUNDS)
    {
      vector3_6.z += distance * 0.01f * (float) global::Game.OffensiveFieldDirection;
      vector3_6.x = UnityEngine.Random.Range(-Field.OUT_OF_BOUNDS - Field.ONE_HALF_YARD, -Field.OUT_OF_BOUNDS + Field.TWO_YARDS);
    }
    else if ((double) vector3_6.x > (double) Field.OUT_OF_BOUNDS)
    {
      vector3_6.z += distance * 0.01f * (float) global::Game.OffensiveFieldDirection;
      vector3_6.x = UnityEngine.Random.Range(Field.OUT_OF_BOUNDS - Field.TWO_YARDS, Field.OUT_OF_BOUNDS + Field.ONE_HALF_YARD);
    }
    if ((double) vector3_6.z > (double) Field.NORTH_BACK_OF_ENDZONE)
      vector3_6.z = UnityEngine.Random.Range(Field.NORTH_BACK_OF_ENDZONE + Field.ONE_HALF_YARD, Field.NORTH_BACK_OF_ENDZONE - Field.TWO_YARDS);
    else if ((double) vector3_6.z < (double) Field.SOUTH_BACK_OF_ENDZONE)
      vector3_6.z = UnityEngine.Random.Range(Field.SOUTH_BACK_OF_ENDZONE - Field.ONE_HALF_YARD, Field.SOUTH_BACK_OF_ENDZONE + Field.TWO_YARDS);
    ThrowingConfig throwingConfig = global::Game.ThrowingConfig;
    int throwingAccuracy = global::Game.OffensiveQB.throwingAccuracy;
    Vector3 vector3_7 = UnityEngine.Random.insideUnitSphere * (distance / throwingConfig.AIAccuracyVarianceMultiplierDistance * MathUtils.MapRange((float) throwingAccuracy, (float) throwingConfig.AccuracyRatingForMaxAIAccuracyVariance, (float) throwingConfig.AccuracyRatingForMinAIAccuracyVariance, throwingConfig.MaxAIAccuracyVariance, throwingConfig.MinAIAccuracyVariance));
    Vector3 vector3_8 = Vector3.up * Field.ONE_YARD * 0.5f;
    if (true)
      vector3_7 = vector3_8 = Vector3.zero;
    return vector3_6 + vector3_7 + vector3_8;
  }

  [Serializable]
  public class OnBallEventRequested : UnityEvent<PlayersManager, BallManager, PlayerAI>
  {
  }

  public delegate void OnIsTackledEvent(bool isTackled);
}
