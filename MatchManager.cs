// Decompiled with JetBrains decompiler
// Type: MatchManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using ControllerSupport;
using FootballVR;
using Framework;
using Framework.Data;
using Framework.StateManagement;
using MovementEffects;
using ProEra.Game;
using ProEra.Game.Achievements;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12;
using TB12.AppStates;
using TB12.UI;
using UDB;
using UnityEngine;
using Vars;

public class MatchManager : MonoBehaviour
{
  [SerializeField]
  private SuperbowlPodium _championStandPrefab;
  public static MatchManager instance;
  public PlayManager playManager;
  public PlayersManager playersManager;
  public FieldManager FieldManager;
  public TimeManager timeManager;
  public BallManager ballManager;
  private const float MAXENDOFPLAYDELAY = 4.5f;
  private const float MINENDOFPLAYDELAY = 2.5f;
  [HideInInspector]
  public float savedLineOfScrim;
  [HideInInspector]
  public int lastPlayYardsGained;
  [HideInInspector]
  public float ballHashPosition;
  [HideInInspector]
  public float prePlayTimeSincePenaltyCheck;
  [HideInInspector]
  public float playTimer;
  [HideInInspector]
  public float prePlayTimer;
  [HideInInspector]
  public float endOfPlayTimer;
  [HideInInspector]
  public float fumbleTimer;
  [HideInInspector]
  public bool allowPATAfterTimeHasExpired;
  [HideInInspector]
  public bool homeTeamGetsBallAtHalf;
  [HideInInspector]
  public bool homeTeamGotBallFirstInOT;
  [HideInInspector]
  public bool haveBothTeamsHadTheBallInOT;
  [HideInInspector]
  public int brokenTackles;
  [HideInInspector]
  public int afterPlayTimer;
  [HideInInspector]
  public int dynamicDif;
  [HideInInspector]
  public int allowThrowAt;
  [HideInInspector]
  public bool fumbleOccured;
  [HideInInspector]
  public bool onsideKick;
  [HideInInspector]
  public bool soundOn = true;
  [HideInInspector]
  public bool checkForEndOfQuarter;
  [HideInInspector]
  public bool waitingForPenalty;
  [HideInInspector]
  public string defaultClock;
  [HideInInspector]
  public Vector3 kickoffLocation;
  [HideInInspector]
  public float caughtPassOrKickAtPos;
  [HideInInspector]
  public bool canCallTimeouts = true;
  public PenaltyManager penaltyManager;
  [HideInInspector]
  public Save_GameSettings gameSettings;
  public CoroutineHandle resolvePlayCoroutine;
  public LayerMask layerMask;
  public Vector3 windSpeed = new Vector3(0.0f, 0.0f);
  public VariableInt LastPlayYardsGained = new VariableInt(0);
  [SerializeField]
  private GameObject _playbookPrefab;
  private bool allowKickAction = true;
  private float timeSinceHardCount;
  private bool checkedForInjuriesAlready;
  private bool allowSnap;
  [SerializeField]
  private AiTimingStore _aiTimingStore;
  private float _aIDecisionTimer;
  private float _aiTimingInterval;
  private float _blockZoneTimer;
  private int _halfAIUpdatedPerFrame = 2;
  private int _curAIUpdateIndex;
  private const int MAX_AI_INDEX = 11;
  private float _gameStartTime;
  private bool _celebratingSuperbowlWin;
  private bool _isOTAllowed = true;
  private bool onSideline;
  private bool _hasDriveEnteredRedzone;
  private bool _isDestroying;
  private CoroutineHandle _allowSnapCoroutine;
  private RoutineHandle _twoMinuteWarningRoutine;
  private List<Penalty> _penalties;
  private List<PlayerAI> _defensiveTeam;
  private List<int> _closeEnough = new List<int>();
  private WaitForSeconds _delayPlayOver;
  private List<int> _injuryCandidates = new List<int>();
  public SavedMatchState beforePlay;
  public SavedMatchState afterPlay;
  private bool _isSimulating;
  private List<Vector3> _renderedSimPositions;
  public VariableBool ShouldSimulate = new VariableBool(false);
  private bool subscribedToNotifications;

  private AchievementData achievementData => SaveManager.GetAchievementData();

  public static bool Exists() => (UnityEngine.Object) MatchManager.instance != (UnityEngine.Object) null;

  public EMatchState currentMatchState { get; private set; }

  public CPUDefenseAdjustmentManager cpuDefenseAdjustmentManager => CPUDefenseAdjustmentManager.instance;

  public event System.Action OnQBPositionChange;

  public event System.Action OnUserPenaltyCalled;

  public event Action<bool> OnAllowUserHurryUp;

  public event Action<bool> OnAllowUserTimeout;

  public event Action<bool> OnAllowConfirmButton;

  public event Action<bool> OnAllowWristPlayCallUI;

  public event System.Action OnSnapAllowed;

  public event System.Action OnPlayReset;

  public AiTimingStore AITimeStore
  {
    get => this._aiTimingStore;
    set => this._aiTimingStore = value;
  }

  private void IncrementCurAIUpdateIndex()
  {
    ++this._curAIUpdateIndex;
    if (this._curAIUpdateIndex < 11)
      return;
    this._curAIUpdateIndex = 0;
  }

  private int MakeAIToUpdateList()
  {
    int updateList = 0;
    for (int index = 0; index < this._halfAIUpdatedPerFrame; ++index)
    {
      updateList += 1 << this._curAIUpdateIndex;
      this.IncrementCurAIUpdateIndex();
    }
    return updateList;
  }

  private bool ShouldUpdateAIIndex(int AIToUpdateList, int AIIndex) => (AIToUpdateList & 1 << AIIndex) != 0;

  public void Start()
  {
    this._gameStartTime = Time.time;
    if (AppState.SeasonMode.Value == ESeasonMode.kUnknown)
      AnalyticEvents.Record<ExhibitionGameStartedArgs>(new ExhibitionGameStartedArgs());
    else
      AnalyticEvents.Record<SeasonGameStartedArgs>(new SeasonGameStartedArgs());
    AppEvents.LoadMainMenu.Link((System.Action) (() => this._isDestroying = true));
    this.SetAiTimingStoreReference();
    this._twoMinuteWarningRoutine = new RoutineHandle();
  }

  public void CallAwake()
  {
    UnityEngine.Object.Instantiate<GameObject>(this._playbookPrefab);
    PlayerAI.InitializeBehaviorTree();
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startedFromGameScene)
    {
      this.SetupGameFromGameScene();
    }
    else
    {
      switch (AppState.GameMode)
      {
        case EGameMode.kPracticeMode:
        case EGameMode.kOnboarding:
          this.SetupPracticeMode();
          break;
        case EGameMode.k2MD:
          this.SetupTwoMinuteDrillMode();
          break;
        case EGameMode.kHeroMoment:
          this.SetupHeroMode();
          break;
        default:
          if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioAISimGame)
          {
            this.SetupAISimGame();
            break;
          }
          TeamData userTeam;
          TeamData compTeam;
          if (AppState.SeasonMode.Value != ESeasonMode.kUnknown)
          {
            userTeam = PersistentData.GetUserTeam();
            compTeam = PersistentData.GetCompTeam();
          }
          else
          {
            userTeam = PersistentData.GetUserTeam();
            compTeam = PersistentData.GetCompTeam();
            PersistentData.SetUserTeam(userTeam);
            PersistentData.SetCompTeam(compTeam);
          }
          Plays.self.SetOffensivePlaybookP1(userTeam.GetOffensivePlaybook());
          Plays.self.SetDefensivePlaybookP1(userTeam.GetDefensivePlaybook());
          Plays.self.SetOffensivePlaybookP2(compTeam.GetOffensivePlaybook());
          Plays.self.SetDefensivePlaybookP2(compTeam.GetDefensivePlaybook());
          ProEra.Game.MatchState.Difficulty.SetValue(PersistentData.difficulty);
          ProEra.Game.MatchState.UserDifficulty.SetValue(PersistentData.difficulty);
          if (global::Game.IsSpectateMode)
            ProEra.Game.MatchState.UserDifficulty.Value = (int) ProEra.Game.MatchState.Difficulty / 3 + 1;
          this.SetBallOn(Field.KICKOFF_LOCATION);
          this.SetBallHashPosition(0.0f);
          this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
          this.checkedForInjuriesAlready = false;
          this.afterPlayTimer = 1000;
          this.DisallowSnap();
          ProEra.Game.MatchState.RunningPat.Value = false;
          ProEra.Game.MatchState.GameLength.SetValue(ScriptableSingleton<VRSettings>.Instance.QuarterLength.Value * 60);
          this.SetWindValue();
          this.brokenTackles = 0;
          this.timeSinceHardCount = 0.0f;
          this.checkForEndOfQuarter = false;
          this._isOTAllowed = true;
          this.kickoffLocation = new Vector3(0.0f, 0.182f, Field.KICKOFF_LOCATION);
          Time.timeScale = (float) GameSettings.TimeScale;
          break;
      }
    }
  }

  public void CallStart()
  {
    Globals.GameOver.SetValue(false);
    this.gameSettings = PersistentSingleton<SaveManager>.Instance.gameSettings;
    this.ballManager.SetPosition(new Vector3(this.ballHashPosition, Ball.BALL_ON_GROUND_HEIGHT, MatchManager.ballOn + 0.5f * (float) global::Game.OffensiveFieldDirection));
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startedFromGameScene)
    {
      this.StartFromGameScene();
    }
    else
    {
      switch (AppState.GameMode)
      {
        case EGameMode.kPracticeMode:
          this.StartPracticeMode();
          break;
        case EGameMode.kOnboarding:
          this.StartPracticeMode();
          break;
        case EGameMode.k2MD:
          this.StartTwoMinuteDrillMode();
          break;
        case EGameMode.kHeroMoment:
          this.StartHeroMode();
          break;
        default:
          this.timeManager.clockEnabled = true;
          this.timeManager.SetRunPlayClock(false);
          ProEra.Game.MatchState.Down.Value = 1;
          this.timeManager.SetQuarter(1);
          ScoreClockState.Quarter.Value = this.timeManager.GetQuarterString();
          this.timeManager.AddToGameClock(0.0f);
          MatchManager.instance.playersManager.SetAfterPlayActionsForAllPlayers();
          FieldState.OffenseGoingNorth.Value = true;
          this.SetBallOn(Field.KICKOFF_LOCATION);
          this.ballManager.SetPosition(new Vector3(0.0f, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
          this.savedLineOfScrim = Field.KICKOFF_LOCATION;
          ProEra.Game.MatchState.FirstDown.Value = ProEra.Game.MatchState.BallOn.Value + (float) (10.0 * ((double) Field.ONE_YARD * (double) global::Game.OffensiveFieldDirection));
          SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
          break;
      }
    }
  }

  private void SetupPracticeMode()
  {
    TeamData team1 = TeamDataCache.GetTeam(PersistentSingleton<SaveManager>.Instance.seasonModeData.UserTeamIndex);
    TeamData team2 = TeamDataCache.GetTeam(PersistentSingleton<SaveManager>.Instance.seasonModeData.UserTeamIndex);
    PersistentData.SetUserTeam(team1);
    PersistentData.SetCompTeam(team2);
    if (ProEra.Game.MatchState.Stats.User == null)
      ProEra.Game.MatchState.Stats.User = new TeamGameStats();
    if (ProEra.Game.MatchState.Stats.Comp == null)
      ProEra.Game.MatchState.Stats.Comp = new TeamGameStats();
    ProEra.Game.MatchState.Reset();
    Plays.self.SetOffensivePlaybookP1(team1.GetOffensivePlaybook());
    Plays.self.SetOffensivePlaybookP2(team2.GetOffensivePlaybook());
    Plays.self.SetDefensivePlaybookP1(team1.GetDefensivePlaybook());
    Plays.self.SetDefensivePlaybookP2(team2.GetDefensivePlaybook());
    this.checkedForInjuriesAlready = false;
    this.afterPlayTimer = 1000;
    this.brokenTackles = 0;
    this.timeSinceHardCount = 0.0f;
    this.checkForEndOfQuarter = false;
    this.kickoffLocation = new Vector3(0.0f, 0.182f, Field.KICKOFF_LOCATION);
    Time.timeScale = (float) GameSettings.TimeScale;
  }

  private void SetupTwoMinuteDrillMode()
  {
    if (ProEra.Game.MatchState.Stats.User == null)
      ProEra.Game.MatchState.Stats.User = new TeamGameStats();
    if (ProEra.Game.MatchState.Stats.Comp == null)
      ProEra.Game.MatchState.Stats.Comp = new TeamGameStats();
    ProEra.Game.MatchState.Reset();
    TeamData userTeam = PersistentData.GetUserTeam();
    TeamData compTeam = PersistentData.GetCompTeam();
    Plays.self.SetOffensivePlaybookP1(userTeam.GetOffensivePlaybook());
    Plays.self.SetOffensivePlaybookP2(compTeam.GetOffensivePlaybook());
    Plays.self.SetDefensivePlaybookP1(userTeam.GetDefensivePlaybook());
    Plays.self.SetDefensivePlaybookP2(compTeam.GetDefensivePlaybook());
    this.checkedForInjuriesAlready = false;
    this.afterPlayTimer = 1000;
    this.brokenTackles = 0;
    this.timeSinceHardCount = 0.0f;
    this.checkForEndOfQuarter = false;
    this._isOTAllowed = false;
    this.kickoffLocation = new Vector3(0.0f, 0.182f, Field.KICKOFF_LOCATION);
    Time.timeScale = (float) GameSettings.TimeScale;
  }

  private void SetupHeroMode()
  {
    if (ProEra.Game.MatchState.Stats.User == null)
      ProEra.Game.MatchState.Stats.User = new TeamGameStats();
    if (ProEra.Game.MatchState.Stats.Comp == null)
      ProEra.Game.MatchState.Stats.Comp = new TeamGameStats();
    ProEra.Game.MatchState.Reset();
    TeamData userTeam = PersistentData.GetUserTeam();
    TeamData compTeam = PersistentData.GetCompTeam();
    Plays.self.SetOffensivePlaybookP1(userTeam.GetOffensivePlaybook());
    Plays.self.SetOffensivePlaybookP2(compTeam.GetOffensivePlaybook());
    Plays.self.SetDefensivePlaybookP1(userTeam.GetDefensivePlaybook());
    Plays.self.SetDefensivePlaybookP2(compTeam.GetDefensivePlaybook());
    this.checkedForInjuriesAlready = false;
    this.afterPlayTimer = 1000;
    this.brokenTackles = 0;
    this.timeSinceHardCount = 0.0f;
    this.checkForEndOfQuarter = false;
    this.kickoffLocation = new Vector3(0.0f, 0.182f, Field.KICKOFF_LOCATION);
    Time.timeScale = (float) GameSettings.TimeScale;
    this._isOTAllowed = false;
  }

  private void StartPracticeMode()
  {
    MonoBehaviour.print((object) "Start Practice Mode called!");
    this.timeManager.SetQuarter(1);
    this.timeManager.SetDisplayMinutes(5);
    this.timeManager.SetDisplaySeconds(0);
    this.timeManager.SetRunGameClock(false);
    this.timeManager.SetRunPlayClock(false);
    this.timeManager.clockEnabled = false;
    ScoreClockState.Quarter.Value = this.timeManager.GetQuarterString();
    ProEra.Game.MatchState.GameLength.Value = 300;
    ProEra.Game.MatchState.RunningPat.Value = false;
    this.DisallowSnap();
    this.SetWindValue();
    ProEra.Game.MatchState.Down.Value = 1;
    FieldState.OffenseGoingNorth.Value = true;
    this.SetBallOn(Field.TWENTY_YARD_LINE);
    if (AppState.GameMode == EGameMode.kOnboarding)
    {
      this.SetBallOn(-Field.TWENTY_FIVE_YARD_LINE);
      ((OnboardingFlow) AxisGameFlow.instance).StartTutorial();
    }
    this.SetBallHashPosition(0.0f);
    this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
    ProEra.Game.MatchState.FirstDown.Value = ProEra.Game.MatchState.BallOn.Value + (float) (10.0 * ((double) Field.ONE_YARD * (double) global::Game.OffensiveFieldDirection));
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
    this.ballManager.SetPosition(new Vector3(0.0f, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
    PersistentData.difficulty = PersistentData.offDifficulty = 10;
    this.SetCurrentMatchState(EMatchState.UserOnOffense);
    PersistentData.userIsHome = true;
    ProEra.Game.MatchState.Stats.User.Score = 0;
    ProEra.Game.MatchState.Stats.Comp.Score = 0;
    MatchManager.instance.playersManager.SetAfterPlayActionsForAllPlayers();
    PlaybookState.ShowPlaybook.Trigger();
  }

  private void StartTwoMinuteDrillMode()
  {
    this.timeManager.SetQuarter(4);
    this.timeManager.SetDisplayMinutes(2);
    this.timeManager.SetDisplaySeconds(0);
    this.timeManager.SetRunGameClock(false);
    this.timeManager.SetRunPlayClock(false);
    this.timeManager.clockEnabled = true;
    ScoreClockState.Quarter.Value = this.timeManager.GetQuarterString();
    this.onSideline = false;
    ProEra.Game.MatchState.IsKickoff.Value = false;
    ProEra.Game.MatchState.GameLength.Value = 120;
    ProEra.Game.MatchState.RunningPat.Value = false;
    this.DisallowSnap();
    this.SetWindValue();
    ProEra.Game.MatchState.Down.Value = 1;
    FieldState.OffenseGoingNorth.Value = true;
    this.SetBallOn(-Field.TEN_YARD_LINE);
    this.SetBallHashPosition(0.0f);
    this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
    ProEra.Game.MatchState.FirstDown.Value = ProEra.Game.MatchState.BallOn.Value + (float) (10.0 * ((double) Field.ONE_YARD * (double) global::Game.OffensiveFieldDirection));
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
    this.ballManager.SetPosition(new Vector3(0.0f, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
    PersistentData.difficulty = PersistentData.offDifficulty = 10;
    this.SetCurrentMatchState(EMatchState.UserOnOffense);
    PersistentData.userIsHome = true;
    this.playManager.autoSelectPlayForAI = false;
    this.playManager.noKicking = true;
    this.canCallTimeouts = false;
  }

  private void StartHeroMode()
  {
    this.timeManager.SetQuarter(4);
    this.timeManager.SetDisplayMinutes(0);
    this.timeManager.SetDisplaySeconds(1);
    this.timeManager.SetRunGameClock(false);
    this.timeManager.SetRunPlayClock(false);
    this.timeManager.clockEnabled = true;
    ProEra.Game.MatchState.Stats.User.Score = 24;
    ProEra.Game.MatchState.Stats.Comp.Score = 28;
    ScoreClockState.Quarter.Value = this.timeManager.GetQuarterString();
    this.onSideline = false;
    ProEra.Game.MatchState.IsKickoff.Value = false;
    ProEra.Game.MatchState.GameLength.Value = 1;
    ProEra.Game.MatchState.RunningPat.Value = false;
    this.DisallowSnap();
    this.SetWindValue();
    ProEra.Game.MatchState.Down.Value = 4;
    FieldState.OffenseGoingNorth.Value = true;
    this.SetBallOn(-Field.TEN_YARD_LINE);
    this.SetBallHashPosition(0.0f);
    this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
    ProEra.Game.MatchState.FirstDown.Value = ProEra.Game.MatchState.BallOn.Value + (float) (10.0 * ((double) Field.ONE_YARD * (double) global::Game.OffensiveFieldDirection));
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
    this.ballManager.SetPosition(new Vector3(0.0f, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
    PersistentData.difficulty = PersistentData.offDifficulty = 10;
    this.SetCurrentMatchState(EMatchState.UserOnOffense);
    PersistentData.userIsHome = true;
    this.playManager.autoSelectPlayForAI = false;
    this.playManager.noKicking = true;
    this.canCallTimeouts = false;
    MatchManager.instance.SetCurrentMatchState(EMatchState.UserOnOffense);
    MatchManager.instance.playersManager.SetAfterPlayActionsForAllPlayers();
  }

  private void SetupAISimGame()
  {
    PersistentData.SetGameMode(GameMode.Spectate);
    PersistentData.userIsHome = true;
    TeamData team1 = TeamDataCache.GetTeam(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startFromGameScene_UserTeamIndex);
    TeamData team2 = TeamDataCache.GetTeam(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startFromGameScene_CompTeamIndex);
    PersistentData.SetUserTeam(team1);
    PersistentData.SetCompTeam(team2);
    if (ProEra.Game.MatchState.Stats.User == null)
      ProEra.Game.MatchState.Stats.User = new TeamGameStats();
    if (ProEra.Game.MatchState.Stats.Comp == null)
      ProEra.Game.MatchState.Stats.Comp = new TeamGameStats();
    Plays.self.SetOffensivePlaybookP1(team1.GetOffensivePlaybook());
    Plays.self.SetDefensivePlaybookP1(team1.GetDefensivePlaybook());
    Plays.self.SetOffensivePlaybookP2(team2.GetOffensivePlaybook());
    Plays.self.SetDefensivePlaybookP2(team2.GetDefensivePlaybook());
    ProEra.Game.MatchState.Difficulty.SetValue(PersistentData.difficulty);
    ProEra.Game.MatchState.UserDifficulty.SetValue(PersistentData.difficulty);
    this.SetBallOn(Field.KICKOFF_LOCATION);
    this.SetBallHashPosition(0.0f);
    this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
    this.checkedForInjuriesAlready = false;
    this.afterPlayTimer = 100;
    this.DisallowSnap();
    ProEra.Game.MatchState.RunningPat.Value = false;
    ProEra.Game.MatchState.GameLength.SetValue(SingletonBehaviour<AISimGameTestManager, MonoBehaviour>.instance.TestGameQuarterLen * 60);
    this.SetWindValue();
    this.brokenTackles = 0;
    this.timeSinceHardCount = 0.0f;
    this.checkForEndOfQuarter = false;
    this.kickoffLocation = new Vector3(0.0f, 0.182f, Field.KICKOFF_LOCATION);
    Time.timeScale = SingletonBehaviour<AISimGameTestManager, MonoBehaviour>.instance.TestGameTimeScale;
  }

  private void SetupUniforms()
  {
    UniformSet uniformSet1 = UniformAssetManager.GetUniformSet(PersistentData.GetHomeTeamIndex());
    int uniformIndexByName1 = uniformSet1.GetUniformIndexByName("HOME");
    int pieceForUniformSet1 = uniformSet1.GetPieceForUniformSet(uniformIndexByName1, UniformPiece.Helmets);
    int pieceForUniformSet2 = uniformSet1.GetPieceForUniformSet(uniformIndexByName1, UniformPiece.Jerseys);
    int pieceForUniformSet3 = uniformSet1.GetPieceForUniformSet(uniformIndexByName1, UniformPiece.Pants);
    uniformSet1.LockInUniformPieces(uniformIndexByName1, pieceForUniformSet1, pieceForUniformSet2, pieceForUniformSet3);
    PersistentData.homeTeamUniform = uniformSet1;
    UniformSet uniformSet2 = UniformAssetManager.GetUniformSet(PersistentData.GetAwayTeamIndex());
    int uniformIndexByName2 = uniformSet2.GetUniformIndexByName("AWAY");
    int pieceForUniformSet4 = uniformSet2.GetPieceForUniformSet(uniformIndexByName2, UniformPiece.Helmets);
    int pieceForUniformSet5 = uniformSet2.GetPieceForUniformSet(uniformIndexByName2, UniformPiece.Jerseys);
    int pieceForUniformSet6 = uniformSet2.GetPieceForUniformSet(uniformIndexByName2, UniformPiece.Pants);
    uniformSet2.LockInUniformPieces(uniformIndexByName2, pieceForUniformSet4, pieceForUniformSet5, pieceForUniformSet6);
    PersistentData.awayTeamUniform = uniformSet2;
  }

  private void SetPlaySelection()
  {
    if (global::Game.IsPlayerOneOnOffense)
    {
      if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceOffPlay)
      {
        FormationData formationByName = PlayManager.GetFormationByName(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceOffenseFormation, Plays.self.GetOffPlaybookForPlayer(Player.One));
        this.playManager.savedOffPlay = formationByName == null ? (PlayDataOff) Plays.self.shotgunPlays_Normal.GetPlay(2) : (PlayDataOff) PlayManager.GetFormationPlayByName(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceOffensePlay, formationByName);
      }
      else
        PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Offense);
    }
    else if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceDefPlay)
    {
      FormationData formationByName = PlayManager.GetFormationByName(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceDefenseFormation, Plays.self.GetOffPlaybookForPlayer(Player.One));
      this.playManager.savedDefPlay = formationByName == null ? (PlayDataDef) Plays.self.fourThreePlays.GetPlay(1) : (PlayDataDef) PlayManager.GetFormationPlayByName(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceDefensePlay, formationByName);
      this.playManager.savedOffPlay = this.playManager.GetOffensivePlayForAI();
    }
    else
    {
      PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Defense);
      this.playManager.savedOffPlay = this.playManager.GetOffensivePlayForAI();
    }
    if (global::Game.UserCallsPlays)
      this.playManager.SelectNextOffPlayForUser();
    else
      this.playManager.SelectNextPlaysForAI();
  }

  public void StartFromCoinFlip(bool offenseNorth, EMatchState state)
  {
    this.onSideline = false;
    FieldState.OffenseGoingNorth.Value = offenseNorth;
    ProEra.Game.MatchState.IsKickoff.Value = true;
    ProEra.Game.MatchState.Down.Value = 1;
    this.SetCurrentMatchState(state);
    this.homeTeamGetsBallAtHalf = global::Game.IsHomeTeamOnOffense;
    this.SetBallOn(Field.KICKOFF_LOCATION);
    this.FieldManager.SetLineOfScrimmageLine();
    this.playersManager.SetAfterPlayActionsForAllPlayers();
    this.playManager.SelectNextPlaysForAI();
  }

  public void EmptyField()
  {
    this.onSideline = true;
    this.playersManager.PutAllPlayersOnSideline();
  }

  public void CheckForTimeouts()
  {
    if (!global::Game.IsNot2PMatch || !this.timeManager.IsGameClockRunning() || !this.canCallTimeouts)
      return;
    int num = (ProEra.Game.MatchState.GameLength.Value - Mathf.CeilToInt(this.timeManager.GetGameClockTimer())) / 60 % 60;
    if (global::Game.IsPlayerOneOnOffense)
      SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance.CheckForAITimeout(Team.Player1);
    SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance.CheckForAITimeout(Team.Computer);
    if (!global::Game.IsPlayerOneOnDefense)
      return;
    SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance.CheckForAITimeout(Team.Player1);
  }

  private void OnEnable()
  {
    this.SubscribeToUserInteractionNotifcations();
    NotificationCenter.AddListener("setupPlayBegin", new Callback(this.SetupPlayBegin));
  }

  private void OnDisable()
  {
    this.UnsubscribeToUserInteractionNotifcations();
    NotificationCenter.RemoveListener("setupPlayBegin", new Callback(this.SetupPlayBegin));
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this.playersManager != (UnityEngine.Object) null)
      this.playersManager.ForceStopAllNteractCallbacks();
    Debug.Log((object) "MatchManager -> OnDestroy");
    this._isDestroying = true;
    if ((UnityEngine.Object) this.ballManager != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.ballManager.gameObject);
    if ((UnityEngine.Object) this.penaltyManager != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.penaltyManager.gameObject);
    this.StopAllCoroutines();
    Timing.KillCoroutines();
    this._twoMinuteWarningRoutine?.Stop();
    MatchManager.instance = (MatchManager) null;
  }

  private void Update()
  {
    if (this._isDestroying)
      return;
    if ((UnityEngine.Object) this.playersManager == (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    if (!this.playersManager.isInitialized)
      return;
    this.playTimer += Time.deltaTime;
    if (!this.onSideline)
      this.RunActionsForAllPlayers();
    this.timeSinceHardCount += Time.deltaTime;
    if (this.timeManager.GetPlayClockTime() == 0 && PersistentSingleton<SaveManager>.Instance.gameSettings.DelayOfGame)
    {
      this.timeManager.WasGameClockRunningBeforeBeingSet = this.timeManager.IsGameClockRunning();
      this.timeManager.SetRunGameClock(false);
      this.penaltyManager.CallPenalty(PenaltyType.DelayOfGame, PersistentData.GetOffensiveTeamIndex(), PersistentData.GetOffensiveTeamData().GetPlayer(global::Game.OffensiveQB.indexOnTeam));
      SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyAccepted = true;
      this.checkForEndOfQuarter = false;
      if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetPenaltyTime() == PenaltyTime.PrePlay)
      {
        this.playManager.PlayActive = false;
        ProEra.Game.PlayState.PlayOver.Value = true;
        ProEra.Game.PlayState.HuddleBreak.Value = false;
        this.playManager.playIsCleanedUp = false;
        if (global::Game.IsPlayerOneOnOffense && global::Game.CurrentPlayHasUserQBOnField)
        {
          System.Action userPenaltyCalled = this.OnUserPenaltyCalled;
          if (userPenaltyCalled != null)
            userPenaltyCalled();
        }
        this.EndPlay(PlayEndType.PrePlayPenalty);
      }
      this.timeManager.ResetPlayClock();
    }
    else
    {
      if (this.IsSnapAllowed())
      {
        this.prePlayTimeSincePenaltyCheck += Time.deltaTime;
        if ((double) this.prePlayTimeSincePenaltyCheck > (double) PenaltyManager.PREPLAY_PENALTY_CHECKTIME)
        {
          if (!this.penaltyManager.isPenaltyOnPlay && this.penaltyManager.IsPenaltyBeforePlay(PersistentData.GetOffensiveTeamIndex()))
            this.DisallowSnap();
          this.prePlayTimeSincePenaltyCheck = 0.0f;
        }
      }
      if (this.checkForEndOfQuarter && !this.allowPATAfterTimeHasExpired && !ProEra.Game.MatchState.RunningPat.Value && this.timeManager.GetDisplayMinutes() <= 0 && this.timeManager.GetDisplaySeconds() <= 0)
      {
        if (this.resolvePlayCoroutine.IsRunning)
          return;
        this.ResetPlay();
        this.timeManager.SetRunGameClock(false);
        this.timeManager.SetRunPlayClock(false);
        this.timeManager.ResetGameClock();
        this.playersManager.StopAISnapBallCoroutine();
        this.playManager.StopAIPickPlayCoroutine();
        if (this.timeManager.IsFirstQuarter())
        {
          PEGameplayEventManager.RecordQuarterEndEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, 1);
          this.timeManager.EndOfFirstQuarter();
          this.StartCoroutine(this.DelayProcessEndOfQuarter(1));
          Debug.Log((object) "END OF 1ST Quarter");
        }
        else if (this.timeManager.IsSecondQuarter())
        {
          PEGameplayEventManager.RecordQuarterEndEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, 2);
          this.timeManager.EndOfSecondQuarter();
          this.StartCoroutine(this.DelayProcessEndOfQuarter(2));
          Debug.Log((object) "END OF Half");
        }
        else if (this.timeManager.IsThirdQuarter())
        {
          PEGameplayEventManager.RecordQuarterEndEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, 3);
          this.timeManager.EndOfThirdQuarter();
          this.StartCoroutine(this.DelayProcessEndOfQuarter(3));
          Debug.Log((object) "END OF 3rd Quarter");
        }
        else if (this.timeManager.IsFourthQuarter())
        {
          PEGameplayEventManager.RecordQuarterEndEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, 4);
          if (ProEra.Game.MatchState.Stats.EqualScore() && this._isOTAllowed)
          {
            this.timeManager.EndOfFourthQuarter();
            this.StartCoroutine(this.DelayProcessEndOfQuarter(4));
            Debug.Log((object) "END OF 4th Quarter -- Overtime");
          }
          else
          {
            this.timeManager.EndOfRegulation();
            Debug.Log((object) "End of Game");
          }
        }
        else if (this.timeManager.IsInOvertime())
        {
          PEGameplayEventManager.RecordQuarterEndEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, 5);
          if (ProEra.Game.MatchState.Stats.EqualScore())
          {
            this.StartCoroutine(this.DelayProcessEndOfQuarter(5));
            Debug.Log((object) "Another OVERTIME!");
          }
          else
          {
            this.timeManager.EndOfRegulation();
            Debug.Log((object) "End of Game");
          }
        }
      }
      if (!((UnityEngine.Object) this.playersManager.ballHolder != (UnityEngine.Object) null) || !global::Game.IsPlayActive || !global::Game.UserControlsPlayers)
        return;
      this.playersManager.CheckForUserInput(global::Game.IsPlayerOneOnOffense, this.playTimer);
    }
  }

  private void FixedUpdate()
  {
    if (this._isDestroying)
      return;
    if (this.timeManager.IsGameClockRunning())
      this.timeManager.AddToGameClock((float) PersistentSingleton<SaveManager>.Instance.gameSettings.ClockSpeed * (1f / 400f));
    this.timeManager.RunPlayClock(ProEra.Game.MatchState.GameLength.Value, this.IsQBReadyForSnap(), this.penaltyManager.OptDelayOfGame);
  }

  public void SnapBallWithButton() => this.SnapBall(Vector2.one);

  public void SnapBall(Vector2 inputPosition)
  {
    if (global::Game.IsPlaybookVisible | (bool) VRState.PauseMenu.GetValue())
      return;
    bool flag = false;
    if (ProEra.Game.PlayState.IsRunOrPass)
      flag = this.penaltyManager.IsPenaltyOnSnap(PersistentData.GetOffensiveTeamIndex());
    if (flag)
      ScoreClockState.PenaltyVisible.Value = true;
    if (ProEra.Game.PlayState.IsRun || global::Game.IsPlayAction)
      PlayerAI.SetInitialBlockTargets();
    this._aIDecisionTimer = 0.0f;
    this.playTimer = Time.deltaTime;
    this.prePlayTimer = 0.0f;
    PlayerAI.qbTimeUnderPressure = 0.0f;
    MatchManager.instance.playersManager.isThrowingBallAway = false;
    this.timeManager.ResetPlayClock();
    this.timeManager.SetRunPlayClock(true);
    this.timeManager.SetAcceleratedClockEnabled(true);
    ScoreClockState.PlayClockVisible.SetValue(false);
    ScoreClockState.PersonnelVisible.SetValue(false);
    ScoreClockState.DownAndDistanceVisible.SetValue(false);
    this.checkedForInjuriesAlready = false;
    this.playersManager.hasNotifiedBlockersToTurn = false;
    this.playManager.playIsCleanedUp = false;
    PlayStats.QbIndexReference.SetValue(-1);
    PlayStats.OffPlayerReference.SetValue(-1);
    PlayStats.DefPlayerReference.SetValue(-1);
    if (ProEra.Game.MatchState.Down.Value == 3)
    {
      if (global::Game.IsPlayerOneOnOffense)
        ++ProEra.Game.MatchState.Stats.User.ThirdDownAtt;
      else
        ++ProEra.Game.MatchState.Stats.Comp.ThirdDownAtt;
    }
    this.checkForEndOfQuarter = false;
    if (!(bool) ProEra.Game.MatchState.RunningPat && !ProEra.Game.PlayState.IsKickoff)
      this.timeManager.SetRunGameClock(true);
    if (ProEra.Game.PlayState.IsPass && (global::Game.UserControlsPlayers && global::Game.IsPlayerOneOnDefense && global::Game.IsNot2PMatch || global::Game.UserDoesNotControlPlayers))
      this.playersManager.SetCheckForAIQBScramble(true);
    else
      this.playersManager.SetCheckForAIQBScramble(false);
    if (global::Game.UserControlsPlayers && ProEra.Game.PlayState.IsPass && (global::Game.IsPlayerOneOnOffense || global::Game.Is2PMatch) && (global::Game.IsPlayerOneOnOffense && !this.gameSettings.ButtonPassingP1 || global::Game.IsPlayerOneOnDefense && !this.gameSettings.ButtonPassingP2))
    {
      this.playersManager.passCircleTrans.localScale = new Vector3(1f, 1f, 1f);
      if (global::Game.UserControlsPlayers)
      {
        if (global::Game.IsPlayAction)
          this.playersManager.passCircleGO.SetActive(false);
        else
          this.playersManager.passCircleGO.SetActive(true);
      }
    }
    if (!ProEra.Game.PlayState.IsKickoff)
      PEGameplayEventManager.RecordBallSnappedEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, this.playersManager.GetCurrentQB());
    if (global::Game.UserControlsPlayers)
      Cursor.visible = false;
    this.playManager.BeginNewPlay();
    this.playManager.AddToCurrentPlayLog(this.timeManager.GetQuarterString() + ", " + (string) ScoreClockState.GameClockString + " remaining");
    this.playManager.AddToCurrentPlayLog("Ball at the " + Field.GetYardLineStringByFieldLocation(this.savedLineOfScrim) + ".  " + ScoreClockState.GetDownAndDistance(ProEra.Game.MatchState.IsHomeTeamOnOffense));
    this.playManager.AddToCurrentPlayLog("Offense picked " + this.playManager.savedOffPlay.GetFormation()?.ToString() + " - " + this.playManager.savedOffPlay.GetPlayName() + " (" + this.playManager.savedOffPlay.GetPlayConceptString() + ")");
    this.playManager.AddToCurrentPlayLog("Defense picked " + this.playManager.savedDefPlay.GetFormation()?.ToString() + " - " + this.playManager.savedDefPlay.GetPlayName() + " (" + this.playManager.savedDefPlay.GetPlayConceptString() + ")");
    this.playManager.PlayActive = true;
    PlayersManager playersManager = this.playersManager;
    for (int index = 0; index < 11; ++index)
    {
      playersManager.curUserScriptRef[index].StartPlayDelayed();
      playersManager.curCompScriptRef[index].StartPlayDelayed();
    }
    if (!ProEra.Game.PlayState.IsKickoff)
      this.ballManager.Snap();
    this.DisallowSnap();
    this.playManager.ShouldOffenseHurryUp = false;
  }

  private void RunHardCountCadence(Player userIndex)
  {
    if (this.IsSnapAllowed() && userIndex == Player.One && global::Game.IsPlayerOneOnOffense && global::Game.IsPlayInactive)
    {
      if ((double) this.timeSinceHardCount <= 0.5)
        return;
      this.timeSinceHardCount = 0.0f;
    }
    else
    {
      if (!this.IsSnapAllowed() || userIndex != Player.Two || !ProEra.Game.MatchState.IsPlayerTwoOnOffense || !global::Game.IsPlayInactive || (double) this.timeSinceHardCount <= 0.5)
        return;
      this.timeSinceHardCount = 0.0f;
    }
  }

  private void RunActionsForAllPlayers()
  {
    if (global::Game.IsPlayActive)
    {
      this.UpdateMatch();
      this.RunActivePlayActionsForAllPlayers();
    }
    else if ((bool) ProEra.Game.PlayState.PlayOver)
    {
      this.RunAfterPlayActionsForAllPlayers();
    }
    else
    {
      if (this.timeManager.IsGameClockRunning() && !this.checkForEndOfQuarter)
        Debug.LogError((object) "Why are we not checking for END of QTR when the game clock is running--this might result in a softlock");
      this.RunBeforePlayActionsForAllPlayers();
      this.CheckForRunningToLineOfScrimmage();
    }
  }

  private void RunActivePlayActionsForAllPlayers()
  {
    PlayersManager playersManager = this.playersManager;
    int updateList = this.MakeAIToUpdateList();
    for (int index = 0; index < 11; ++index)
    {
      bool flag = this.ShouldUpdateAIIndex(updateList, index);
      if (global::Game.IsPlayerOneOnOffense)
      {
        if (global::Game.UserDoesNotControlPlayers || (UnityEngine.Object) playersManager.userPlayer != (UnityEngine.Object) playersManager.curUserPlayers[index])
        {
          if ((UnityEngine.Object) playersManager.ballHolder == (UnityEngine.Object) null || (UnityEngine.Object) playersManager.ballHolder != (UnityEngine.Object) playersManager.curUserPlayers[index])
          {
            playersManager.curUserScriptRef[index].RunOffensePerFrame();
            if (flag)
              playersManager.curUserScriptRef[index].RunOffense();
          }
          else if (flag)
            playersManager.curUserScriptRef[index].RunOffenseBH();
        }
        if (global::Game.IsNot2PMatch || global::Game.UserDoesNotControlPlayers || (UnityEngine.Object) playersManager.userPlayerP2 != (UnityEngine.Object) playersManager.curCompPlayers[index])
        {
          playersManager.curCompScriptRef[index].RunDefensePerFrame();
          if (flag)
            playersManager.curCompScriptRef[index].RunDefense();
        }
      }
      else
      {
        if (global::Game.IsNot2PMatch || global::Game.UserDoesNotControlPlayers || (UnityEngine.Object) playersManager.userPlayerP2 != (UnityEngine.Object) playersManager.curCompPlayers[index])
        {
          if ((UnityEngine.Object) playersManager.ballHolder == (UnityEngine.Object) null || (UnityEngine.Object) playersManager.ballHolder != (UnityEngine.Object) playersManager.curCompPlayers[index])
          {
            playersManager.curCompScriptRef[index].RunOffensePerFrame();
            if (flag)
              playersManager.curCompScriptRef[index].RunOffense();
          }
          else if (flag)
            playersManager.curCompScriptRef[index].RunOffenseBH();
        }
        if (global::Game.UserDoesNotControlPlayers || (UnityEngine.Object) playersManager.userPlayer != (UnityEngine.Object) playersManager.curUserPlayers[index])
        {
          playersManager.curUserScriptRef[index].RunDefensePerFrame();
          if (flag)
            playersManager.curUserScriptRef[index].RunDefense();
        }
      }
    }
    this._blockZoneTimer += Time.deltaTime;
    if ((double) this._blockZoneTimer > 0.25)
    {
      this._blockZoneTimer = 0.0f;
      if (global::Game.BallHolderIsNotNull)
      {
        Vector3 position = playersManager.ballHolderScript.trans.position;
        for (int index = 0; index < 11; ++index)
        {
          playersManager.curUserScriptRef[index].CheckBlockZone(position);
          playersManager.curCompScriptRef[index].CheckBlockZone(position);
        }
      }
    }
    this.CheckForEndOfPlayScenarios();
  }

  private void RunAfterPlayActionsForAllPlayers()
  {
    int updateList = this.MakeAIToUpdateList();
    PlayersManager playersManager = this.playersManager;
    int count = playersManager.curUserScriptRef.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.ShouldUpdateAIIndex(updateList, index))
      {
        playersManager.curUserScriptRef[index].RunAfterPlayActions();
        playersManager.curCompScriptRef[index].RunAfterPlayActions();
      }
    }
  }

  private void RunBeforePlayActionsForAllPlayers()
  {
    int updateList = this.MakeAIToUpdateList();
    this.prePlayTimer += Time.deltaTime;
    PlayersManager playersManager = this.playersManager;
    int count = playersManager.curUserScriptRef.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.ShouldUpdateAIIndex(updateList, index))
      {
        playersManager.curUserScriptRef[index].RunBeforePlayActions();
        playersManager.curCompScriptRef[index].RunBeforePlayActions();
      }
    }
  }

  private void UpdateMatch()
  {
    PlayersManager playersManager = this.playersManager;
    if ((ProEra.Game.PlayState.IsKickoff || global::Game.IsFG ? (global::Game.BallIsNotThrownOrKicked ? 1 : 0) : 0) == 0)
      playersManager.SetBlockZone();
    if (global::Game.IsPlayActive && !(bool) ProEra.Game.MatchState.IsPlayInitiated)
    {
      if ((!global::Game.IsRun && !global::Game.IsPlayAction || global::Game.IsQBKneel || global::Game.IsQBSpike || global::Game.IsPitchPlay ? 0 : (!global::Game.IsQBKeeper ? 1 : 0)) != 0)
        global::Game.OffensiveQB.RequestHandoff();
      if ((global::Game.IsPass || global::Game.IsQBKneel || global::Game.IsQBSpike ? 1 : (global::Game.IsQBKeeper ? 1 : 0)) != 0)
        global::Game.OffensiveQB.RequestSnap();
      if (global::Game.IsFG)
        global::Game.OffensiveQB.RequestFieldGoal();
      if (global::Game.IsKickoff)
        global::Game.Kicker.RequestKickoff(global::Game.IsOnsidesKick);
      if (global::Game.IsPunt)
        global::Game.Punter.RequestPunt();
    }
    bool flag1 = (double) this.ballManager.trans.position.y <= (double) Field.FIFTEEN_INCHES;
    bool flag2 = (double) Mathf.Abs(this.ballManager.trans.position.x) > (double) Field.OUT_OF_BOUNDS + (double) Field.FIVE_YARDS;
    bool flag3 = (double) this.ballManager.trans.position.z > (double) Field.NORTH_BACK_OF_ENDZONE + (double) Field.FIVE_YARDS || (double) this.ballManager.trans.position.z < (double) Field.SOUTH_BACK_OF_ENDZONE - (double) Field.FIVE_YARDS;
    if (((global::Game.BS_IsInAirPass || global::Game.BS_IsInAirDeflected || global::Game.BS_IsInAirDrop ? 1 : (global::Game.BS_IsInAirToss ? 1 : 0)) & (flag1 ? 1 : 0) | (flag2 ? 1 : 0) | (flag3 ? 1 : 0)) != 0)
    {
      this.ballManager.PlayBallSound(0);
      Ball.State.BallState.SetValue(EBallState.OnGround);
      if (!global::Game.BS_IsFumble && !global::Game.IsKickoff && this.playersManager.ballWasThrownOrKicked && Field.FurtherDownfield(this.playersManager.passDestination.z, global::Game.OffensiveQB.trans.position.z))
        this.EndPlay(PlayEndType.Incomplete);
    }
    else if (global::Game.IsFG && global::Game.BallIsThrownOrKicked)
    {
      PlayerData playerData = global::Game.IsPlayerOneOnOffense ? playersManager.userTeamData.GetPlayer(playersManager.curUserScriptRef[6].indexOnTeam) : playersManager.compTeamData.GetPlayer(playersManager.curCompScriptRef[6].indexOnTeam);
      PlayerStats playerStats = global::Game.IsPlayerOneOnOffense ? this.playManager.userTeamCurrentPlayStats.players[playerData.IndexOnTeam] : this.playManager.compTeamCurrentPlayStats.players[playerData.IndexOnTeam];
      float difference = Field.FindDifference(Field.OFFENSIVE_GOAL_LINE, ProEra.Game.MatchState.BallOn.Value);
      float num = (float) (Field.ConvertDistanceToYards(difference) + 17);
      if (Field.FurtherDownfield(this.ballManager.trans.position.z, Field.OFFENSIVE_BACK_OF_ENDZONE))
      {
        Ball.State.BallState.SetValue(EBallState.PlayOver);
        if (Field.IsBetweenGoalPosts(this.ballManager.trans.position.x) && (double) this.ballManager.trans.position.y >= (double) Field.FG_CROSSBAR_HEIGHT)
        {
          if ((bool) ProEra.Game.MatchState.RunningPat)
          {
            this.AddScore(1);
            this.EndPlay(PlayEndType.MadeFG);
            ++playerStats.XPAttempted;
            ++playerStats.XPMade;
          }
          else
          {
            this.AddScore(3);
            this.EndPlay(PlayEndType.MadeFG);
            string str = num.ToString() + " YARD FG IS GOOD!";
            this.playManager.AddToCurrentPlayLog(playerData.FirstName + " " + playerData.LastName + " made a " + (Field.ConvertDistanceToYards(difference) + 17).ToString() + " yard FG");
            ++playerStats.FGAttempted;
            ++playerStats.FGMade;
            if (!global::Game.IsPlayerOneOnOffense || (double) num < 45.0 || !global::Game.UserControlsPlayers)
              ;
          }
        }
        else
        {
          this.EndPlay(PlayEndType.MissedFG);
          if ((bool) ProEra.Game.MatchState.RunningPat)
          {
            ++playerStats.XPAttempted;
          }
          else
          {
            string str = num.ToString() + " YARD FG IS NO GOOD!";
            this.playManager.AddToCurrentPlayLog(playerData.FirstName + " " + playerData.LastName + " missed a " + (Field.ConvertDistanceToYards(difference) + 17).ToString() + " yard FG");
            ++playerStats.FGAttempted;
          }
        }
      }
      if ((double) this.ballManager.trans.position.y <= (double) Ball.BALL_HIT_GROUND_HEIGHT && (UnityEngine.Object) this.ballManager.trans.parent == (UnityEngine.Object) null)
      {
        if ((bool) ProEra.Game.MatchState.RunningPat)
        {
          ++playerStats.XPAttempted;
        }
        else
        {
          string str = num.ToString() + " YARD FG IS NO GOOD!";
          this.playManager.AddToCurrentPlayLog(playerData.FirstName + " " + playerData.LastName + " missed a " + (Field.ConvertDistanceToYards(difference) + 17).ToString() + " yard FG");
          ++playerStats.FGAttempted;
        }
        this.ballManager.PlayBallSound(0);
        Ball.State.BallState.SetValue(EBallState.OnGround);
        this.EndPlay(PlayEndType.MissedFG);
      }
    }
    else if (ProEra.Game.PlayState.IsPuntOrKickoff && global::Game.BallIsThrownOrKicked)
    {
      if (flag1 && Field.FurtherDownfield(this.ballManager.trans.position.z, ProEra.Game.MatchState.BallOn.Value + Field.FIVE_YARDS * (float) global::Game.OffensiveFieldDirection))
      {
        this.ballManager.PlayBallSound(0);
        Ball.State.BallState.SetValue(EBallState.OnGround);
      }
      if (ProEra.Game.PlayState.IsKickoff)
      {
        Vector3 position = this.ballManager.trans.position;
        if (Field.FurtherDownfield(position.z, Field.OFFENSIVE_GOAL_LINE) && global::Game.BS_IsOnGround)
        {
          ProEra.Game.MatchState.Turnover.Value = true;
          this.EndPlay(PlayEndType.OOB_In_Endzone);
        }
        else if ((double) Mathf.Abs(position.x) > (double) Field.OUT_OF_BOUNDS || (double) Mathf.Abs(position.z) > (double) Field.NORTH_BACK_OF_ENDZONE)
        {
          if (Field.FurtherDownfield(this.ballManager.trans.position.z, Field.OFFENSIVE_GOAL_LINE))
          {
            this.EndPlay(PlayEndType.OOB_In_Endzone);
          }
          else
          {
            if ((UnityEngine.Object) playersManager.ballHolderScript == (UnityEngine.Object) null)
            {
              double deflectedPassTimer = (double) this.playManager.deflectedPassTimer;
            }
            this.EndPlay(PlayEndType.OOB);
          }
          ProEra.Game.MatchState.Turnover.Value = true;
        }
      }
      if (ProEra.Game.PlayState.IsPunt && (Field.FurtherDownfield(this.ballManager.trans.position.z, Field.OFFENSIVE_GOAL_LINE) || (double) Mathf.Abs(this.ballManager.trans.position.x) > (double) Field.OUT_OF_BOUNDS))
      {
        PlayerData playerData = global::Game.IsPlayerOneOnOffense ? playersManager.userTeamData.GetPlayer(playersManager.curUserScriptRef[6].indexOnTeam) : playersManager.compTeamData.GetPlayer(playersManager.curCompScriptRef[6].indexOnTeam);
        PlayerStats playerStats = global::Game.IsPlayerOneOnOffense ? this.playManager.userTeamCurrentPlayStats.players[playerData.IndexOnTeam] : this.playManager.compTeamCurrentPlayStats.players[playerData.IndexOnTeam];
        float distance = Mathf.Abs(this.ballManager.trans.position.z - this.savedLineOfScrim);
        if (Field.FurtherDownfield(this.ballManager.trans.position.z, Field.OFFENSIVE_GOAL_LINE))
        {
          ProEra.Game.MatchState.Turnover.Value = true;
          this.EndPlay(PlayEndType.OOB_In_Endzone);
          if ((UnityEngine.Object) playersManager.ballHolderScript == (UnityEngine.Object) null || playersManager.ballHolderScript.indexOnTeam == playerData.IndexOnTeam)
          {
            ++playerStats.Punts;
            ++playerStats.PuntTouchbacks;
            playerStats.PuntYards += Field.ConvertDistanceToYards(distance);
          }
        }
        else if ((double) Mathf.Abs(this.ballManager.trans.position.x) > (double) Field.OUT_OF_BOUNDS)
        {
          ProEra.Game.MatchState.Turnover.Value = true;
          this.EndPlay(PlayEndType.OOB);
          if ((UnityEngine.Object) playersManager.ballHolderScript == (UnityEngine.Object) null || playersManager.ballHolderScript.indexOnTeam == playerData.IndexOnTeam)
          {
            ++playerStats.Punts;
            playerStats.PuntYards += Field.ConvertDistanceToYards(distance);
            if (Field.FurtherDownfield(this.ballManager.trans.position.z, Field.OPPONENT_TWENTY_YARD_LINE))
              ++playerStats.PuntsInside20;
          }
        }
      }
    }
    else if (global::Game.BS_IsOnGround && !ProEra.Game.PlayState.IsPunt && !ProEra.Game.PlayState.IsKickoff)
    {
      this.ballManager.SetPosition(new Vector3(this.ballManager.trans.position.x, Ball.BALL_ON_GROUND_HEIGHT, this.ballManager.trans.position.z));
      this.ballManager.SetVelocity(Vector3.zero);
      this.ballManager.SetAngularVelocity(Vector3.zero);
      ++this.fumbleTimer;
      if ((double) this.ballManager.trans.position.z > (double) Field.NORTH_BACK_OF_ENDZONE || (double) this.ballManager.trans.position.z < (double) Field.SOUTH_BACK_OF_ENDZONE)
        this.EndPlay(PlayEndType.OOB_In_Endzone);
      else if ((double) Mathf.Abs(this.ballManager.trans.position.x) > (double) Field.OUT_OF_BOUNDS)
        this.EndPlay(PlayEndType.OOB);
    }
    if (!ProEra.Game.PlayState.IsPass && !global::Game.IsQBKeeper || !global::Game.IsNotConvergeOnBall || !global::Game.BallHolderIsNotNull || !((UnityEngine.Object) playersManager.GetBallHolder() == (UnityEngine.Object) global::Game.OffensiveQB) || !Field.FurtherDownfield(playersManager.ballHolderScript.trans.position.z, ProEra.Game.MatchState.BallOn.Value - 0.2f * (float) global::Game.OffensiveFieldDirection))
      return;
    playersManager.passCircleGO.SetActive(false);
    playersManager.convergeOnBall = true;
    PEGameplayEventManager.RecordQBRunningEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, playersManager.curUserScriptRef[5]);
    if (global::Game.IsPlayerOneOnOffense)
    {
      for (int index = 0; index < 11; ++index)
        playersManager.curCompScriptRef[index].SetHandoffDetectionReactionDelay();
      ++this.playManager.userTeamCurrentPlayStats.players[playersManager.curUserScriptRef[5].indexOnTeam].RushAttempts;
      playersManager.EnableAllBlockers(playersManager.curUserScriptRef);
    }
    else
    {
      for (int index = 0; index < 11; ++index)
        playersManager.curUserScriptRef[index].SetHandoffDetectionReactionDelay();
      ++this.playManager.compTeamCurrentPlayStats.players[playersManager.curCompScriptRef[5].indexOnTeam].RushAttempts;
      playersManager.EnableAllBlockers(playersManager.curCompScriptRef);
    }
    playersManager.ballHolderScript.animatorCommunicator.hasBall = true;
  }

  public void ResetPlay()
  {
    if (this._allowSnapCoroutine.IsRunning)
      Timing.KillCoroutines(this._allowSnapCoroutine);
    this.playManager.StopAIPickPlayCoroutine();
    this.playManager.StopWaitToHandlePlayConfirmationCoroutine();
    this.DisallowSnap();
    ProEra.Game.PlayState.PlayOver.Value = true;
    ProEra.Game.PlayState.HuddleBreak.Value = false;
    FatigueManager.RecoverAllPlayers();
    Cursor.visible = true;
    this.ballManager.SetParent((Transform) null);
    Ball.State.BallState.SetValue(EBallState.PlayOver);
    this.playersManager.FinishPlayForAllPlayers();
    this.playersManager.SetAfterPlayActionsForAllPlayers();
    this.playersManager.SetUserPlayer(-1);
    if (global::Game.Is2PMatch)
      this.playersManager.SetUserPlayerP2(-1);
    this.playManager.canUserCallAudible = false;
    PlaybookState.HidePlaybook.Trigger();
    this.timeManager.ResetPlayClock();
    System.Action onPlayReset = this.OnPlayReset;
    if (onPlayReset != null)
      onPlayReset();
    if (GameTimeoutState.TimeoutCalledP1.Value)
    {
      Action<bool> allowUserTimeout = this.OnAllowUserTimeout;
      if (allowUserTimeout != null)
        allowUserTimeout(true);
    }
    this.playManager.ShouldOffenseHurryUp = false;
  }

  private void CheckForEndOfPlayScenarios()
  {
    if (!global::Game.BallHolderIsNotNull)
      return;
    float z = this.playersManager.ballHolderScript.trans.position.z;
    if (this.playersManager.ballHolderScript.IsAttemptingCatchOrFumbleRecovery)
    {
      if (this.playersManager.ballHolderScript.CatchOutcome == CatchOutcomeTracker.ECatchOutcome.CaughtOutOfBounds)
      {
        this.EndPlay(PlayEndType.Incomplete);
      }
      else
      {
        if (this.playersManager.ballHolderScript.CatchOutcome != CatchOutcomeTracker.ECatchOutcome.CaughtInBounds)
          return;
        this.CheckForTouchdown();
      }
    }
    else
    {
      if ((double) z > (double) Field.NORTH_BACK_OF_ENDZONE || (double) z < (double) Field.SOUTH_BACK_OF_ENDZONE)
        this.EndPlay(PlayEndType.OOB_In_Endzone);
      else if ((double) Mathf.Abs(this.playersManager.ballHolderScript.trans.position.x) > (double) Field.OUT_OF_BOUNDS)
        this.EndPlay(PlayEndType.OOB);
      this.CheckForTouchdown();
    }
  }

  private bool CheckForTouchdown()
  {
    if (!SingletonBehaviour<BallManager, MonoBehaviour>.instance.IsPastOffensiveGoalLine())
      return false;
    if (global::Game.IsPlayerOneOnDefense && (bool) ProEra.Game.MatchState.Turnover)
    {
      int num1 = global::Game.UserControlsPlayers ? 1 : 0;
    }
    if (!(bool) ProEra.Game.MatchState.RunningPat)
    {
      if (!(bool) ProEra.Game.MatchState.Turnover)
      {
        int num2 = global::Game.IsPlayerOneOnOffense ? 1 : 0;
      }
      else
      {
        int num3 = global::Game.IsPlayerOneOnOffense ? 1 : 0;
      }
    }
    this.EndPlay(PlayEndType.Touchdown);
    return true;
  }

  private void TrackStats()
  {
    if (global::Game.PET_IsTouchdown && !(bool) ProEra.Game.MatchState.RunningPat)
    {
      if (this.playersManager.ballHolderScript.onUserTeam && global::Game.IsPlayerOneOnOffense)
      {
        if (ProEra.Game.PlayState.IsPass && global::Game.BallIsThrownOrKicked)
        {
          ++this.playManager.userTeamCurrentPlayStats.players[this.playersManager.curUserScriptRef[5].indexOnTeam].QBPassTDs;
          ++this.playManager.userTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].ReceivingTDs;
        }
        else
        {
          ++ProEra.Game.MatchState.Stats.User.RushTDs;
          ++this.playManager.userTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].RushTDs;
        }
      }
      else if (!this.playersManager.ballHolderScript.onUserTeam && global::Game.IsPlayerOneOnDefense)
      {
        if (global::Game.IsPass && global::Game.BallIsThrownOrKicked)
        {
          ++this.playManager.compTeamCurrentPlayStats.players[this.playersManager.curCompScriptRef[5].indexOnTeam].QBPassTDs;
          ++this.playManager.compTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].ReceivingTDs;
        }
        else
        {
          ++ProEra.Game.MatchState.Stats.Comp.RushTDs;
          ++this.playManager.compTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].RushTDs;
        }
      }
      else if ((bool) ProEra.Game.MatchState.Turnover)
      {
        if (global::Game.IsPlayerOneOnOffense)
        {
          if (global::Game.IsPunt)
            ++this.playManager.compTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].PuntReturnTDs;
          else if (global::Game.IsKickoff)
            ++this.playManager.compTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].KickReturnTDs;
          else
            ++this.playManager.compTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].DefensiveTDs;
        }
        else if (global::Game.IsPunt)
          ++this.playManager.userTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].PuntReturnTDs;
        else if (global::Game.IsKickoff)
          ++this.playManager.userTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].KickReturnTDs;
        else
          ++this.playManager.userTeamCurrentPlayStats.players[this.playersManager.ballHolderScript.indexOnTeam].DefensiveTDs;
      }
      else
        MonoBehaviour.print((object) "Punt or Kick return for a TD?");
    }
    if (ProEra.Game.PlayState.IsRunOrPass && (bool) ProEra.Game.MatchState.Turnover)
    {
      if (global::Game.IsPlayerOneOnOffense)
        ++ProEra.Game.MatchState.Stats.User.Turnovers;
      else
        ++ProEra.Game.MatchState.Stats.Comp.Turnovers;
    }
    if (ProEra.Game.PlayState.IsRun)
      ++ProEra.Game.MatchState.Stats.DriveRunPlays;
    else if (ProEra.Game.PlayState.IsPass)
      ++ProEra.Game.MatchState.Stats.DrivePassPlays;
    this.lastPlayYardsGained = Field.ConvertDistanceToYards(MatchManager.ballOn - this.savedLineOfScrim) * global::Game.OffensiveFieldDirection;
    this.LastPlayYardsGained.ForceValue(this.lastPlayYardsGained);
    PlayDataOff savedOffPlay = this.playManager.savedOffPlay;
    if (global::Game.IsPlayerOneOnOffense)
      ProEra.Game.MatchState.Stats.User.StoreYardGainByPlayType(savedOffPlay, (float) this.lastPlayYardsGained);
    else
      ProEra.Game.MatchState.Stats.Comp.StoreYardGainByPlayType(savedOffPlay, (float) this.lastPlayYardsGained);
    SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance.UpdateStats(global::Game.IsPlayerOneOnOffense, this.savedLineOfScrim);
  }

  private IEnumerator DelayPlayOver(PlayerAI p)
  {
    this._delayPlayOver = new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.7f));
    yield return (object) this._delayPlayOver;
    p.FinishPlay();
  }

  public void EndPlay(PlayEndType playEndType)
  {
    MonoBehaviour.print((object) ("Ending play; " + playEndType.ToString()));
    PEGameplayEventManager.RecordPlayOverEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, playEndType, MatchManager.instance.playersManager.ballHolderScript);
    this.playManager.AddToCurrentPlayLog("Play was ended as " + playEndType.ToString());
    this.CheckForInjuries(playEndType);
    this.UpdateInjuries(false);
    this.playersManager.StopAISnapBallCoroutine();
    bool flag = false;
    int quarter = this.timeManager.GetQuarter();
    int displayMinutes = this.timeManager.GetDisplayMinutes();
    this.endOfPlayTimer = UnityEngine.Random.Range(2.5f, 4.5f);
    if (playEndType == PlayEndType.Touchdown && global::Game.IsNotRunningPAT)
    {
      if (global::Game.IsPlayerOneOnOffense)
        ++ProEra.Game.MatchState.Stats.User.Touchdowns;
      else if (global::Game.IsPlayerTwoOnOffense)
        ++ProEra.Game.MatchState.Stats.Comp.Touchdowns;
    }
    if (!this._hasDriveEnteredRedzone)
    {
      if (global::Game.IsPlayerOneOnOffense)
      {
        if ((double) ProEra.Game.MatchState.BallOn.Value <= (double) Field.OPPONENT_TWENTY_YARD_LINE)
        {
          ++ProEra.Game.MatchState.Stats.User.RedZoneAppearances;
          this._hasDriveEnteredRedzone = true;
        }
      }
      else if (global::Game.IsPlayerTwoOnOffense && (double) ProEra.Game.MatchState.BallOn.Value <= (double) Field.OWN_TWENTY_YARD_LINE)
      {
        ++ProEra.Game.MatchState.Stats.Comp.RedZoneAppearances;
        this._hasDriveEnteredRedzone = true;
      }
    }
    else if (this._hasDriveEnteredRedzone && playEndType == PlayEndType.Touchdown)
    {
      if (global::Game.IsPlayerOneOnOffense)
        ++ProEra.Game.MatchState.Stats.User.RedZoneTouchdowns;
      else if (global::Game.IsPlayerTwoOnOffense)
        ++ProEra.Game.MatchState.Stats.Comp.RedZoneTouchdowns;
    }
    if ((!global::Game.IsQBKneel || this.timeManager.GetQuarter() != 4 ? 0 : (ProEra.Game.MatchState.Stats.User.Score > ProEra.Game.MatchState.Stats.Comp.Score ? 1 : 0)) != 0)
      ProEra.Game.MatchState.Stats.User.VFormationSatisfied = true;
    if (playEndType == PlayEndType.Incomplete || playEndType == PlayEndType.Touchdown || playEndType == PlayEndType.MissedFG)
      flag = true;
    if (playEndType == PlayEndType.OOB && (quarter == 2 && displayMinutes < 2 || quarter == 4 && displayMinutes < 5 || quarter == 5))
      flag = true;
    if ((bool) ProEra.Game.MatchState.Turnover || ProEra.Game.PlayState.IsPuntOrKickoff || global::Game.IsFG)
      flag = true;
    if (ProEra.Game.MatchState.Down.Value == 4 && Field.FurtherDownfield(this.savedLineOfScrim, ProEra.Game.MatchState.BallOn.Value))
      flag = true;
    if (flag)
    {
      this.timeManager.SetRunGameClock(false);
      this.allowPATAfterTimeHasExpired = playEndType == PlayEndType.Touchdown && this.timeManager.HasGameClockExpired() && !(bool) ProEra.Game.MatchState.RunningPat && (!this.penaltyManager.isPenaltyOnPlay || !this.penaltyManager.isPenaltyAccepted);
    }
    else
    {
      this.endOfPlayTimer = Mathf.Min(this.endOfPlayTimer, (float) (ProEra.Game.MatchState.GameLength.Value - Mathf.CeilToInt(this.timeManager.GetGameClockTimer())));
      this.allowPATAfterTimeHasExpired = false;
    }
    if (this.timeManager.ShowTwoMinuteWarningAfterPlay())
    {
      this.HandleTwoMinuteWarning();
      this.timeManager.SetRunGameClock(false);
      this.timeManager.SetShowTwoMinuteWarningAfterPlay(false);
    }
    this.playersManager.recButtonIndex = -1;
    ProEra.Game.PlayState.PlayOver.Value = true;
    ProEra.Game.PlayState.HuddleBreak.Value = false;
    this.playManager.PlayActive = false;
    this.playersManager.userSprint = false;
    this.playersManager.userSprintP2 = false;
    this.playersManager.userStrafe = false;
    this.playersManager.userStrafeP2 = false;
    ProEra.Game.MatchState.IsPlayInitiated.SetValue(false);
    PlayerAI.numberOfPlayersInHuddle_Off = 0;
    PlayerAI.numberOfPlayersInHuddle_Def = 0;
    this.playManager.playEndType = playEndType;
    this.afterPlayTimer = 0;
    Cursor.visible = true;
    this.playersManager.forceQBScramble = false;
    this.playersManager.ballLandingSpotGO.SetActive(false);
    this.playersManager.passCircleGO.SetActive(false);
    if (global::Game.BallHolderIsNotNull && playEndType != PlayEndType.Incomplete || ProEra.Game.PlayState.IsPuntOrKickoff && playEndType == PlayEndType.OOB || this.IsSimulating)
    {
      this.FieldManager.tempBallPos = this.IsSimulating ? this.ballManager.GetClosestPointToEndzone() : this.ballManager.GetForwardProgressLine();
      this.SetBallOn(this.FieldManager.tempBallPos);
    }
    if (!this.IsSimulating)
      this.CheckForEndOfPlayCelebrations(playEndType);
    this.CheckForTimeouts();
    for (int index = 0; index < 11; ++index)
    {
      if (!this.IsSimulating)
      {
        this.StartCoroutine(this.DelayPlayOver(this.playersManager.curUserScriptRef[index]));
        this.StartCoroutine(this.DelayPlayOver(this.playersManager.curCompScriptRef[index]));
      }
      else
      {
        this.playersManager.curUserScriptRef[index].FinishPlay();
        this.playersManager.curCompScriptRef[index].FinishPlay();
      }
    }
    if (playEndType != PlayEndType.PrePlayPenalty)
    {
      Action<bool> allowUserTimeout = this.OnAllowUserTimeout;
      if (allowUserTimeout != null)
        allowUserTimeout(true);
    }
    if ((EMatchState) (Variable<EMatchState>) ProEra.Game.MatchState.CurrentMatchState == EMatchState.End)
      return;
    this.StartCoroutine(this.FinishEndOfPlay());
  }

  private IEnumerator FinishEndOfPlay()
  {
    if (!this.IsSimulating)
      yield return (object) Timing.WaitForSeconds(1f);
    this.resolvePlayCoroutine = Timing.RunCoroutine(this.ResolvePlay());
  }

  private IEnumerator<float> ResolvePlay()
  {
    if (global::Game.BallHolderIsNotNull && global::Game.PET_IsNotTouchdown && global::Game.PET_IsNotIncomplete)
    {
      if ((double) Mathf.Abs(this.ballManager.trans.position.x) > (double) Field.OUT_OF_BOUNDS)
        this.SetBallOn(this.FieldManager.tempBallPos);
      else if (global::Game.OffenseGoingNorth)
        this.SetBallOn(Mathf.Max(this.ballManager.trans.position.z, this.FieldManager.tempBallPos));
      else
        this.SetBallOn(Mathf.Min(this.ballManager.trans.position.z, this.FieldManager.tempBallPos));
      this.CheckForTouchdown();
      if (global::Game.PET_IsTouchdown)
        yield break;
    }
    if (!this.checkForEndOfQuarter)
    {
      if (!(bool) ProEra.Game.MatchState.Turnover)
      {
        ++ProEra.Game.MatchState.Stats.CurrentDrivePlays;
        this.timeManager.FinishPlay();
      }
      if (ProEra.Game.PlayState.IsRunOrPass && !this.penaltyManager.isPenaltyOnPlay && (UnityEngine.Object) MatchManager.instance.playersManager.ballHolderScript != (UnityEngine.Object) null && (UnityEngine.Object) MatchManager.instance.playersManager.tackler != (UnityEngine.Object) null)
      {
        if (ProEra.Game.PlayState.IsPass)
          this.penaltyManager.IsPenaltyAfterPlay(MatchManager.instance.playersManager.ballHolderScript, MatchManager.instance.playersManager.tackler, PersistentData.GetOffensiveTeamIndex(), PlayType.Pass);
        else if (ProEra.Game.PlayState.IsRun)
          this.penaltyManager.IsPenaltyAfterPlay(MatchManager.instance.playersManager.ballHolderScript, MatchManager.instance.playersManager.tackler, PersistentData.GetOffensiveTeamIndex(), PlayType.Run);
      }
      this.playManager.AddToCurrentPlayLog("Play ended at the " + Field.GetYardLineStringByFieldLocation(this.FieldManager.tempBallPos) + " yard line");
      Penalty p = this.penaltyManager.GetPenaltyOnPlay();
      this.timeManager.IsGameClockRunning();
      bool flag = false;
      if (this.penaltyManager.isPenaltyOnPlay)
      {
        this.playersManager.SetAfterPlayActionsForAllPlayers();
        this.playersManager.SetUserPlayer(-1);
        if (global::Game.Is2PMatch)
          this.playersManager.SetUserPlayerP2(-1);
        this.timeManager.SetRunGameClock(false);
        yield return 0.0f;
        flag = true;
        if (p.GetPenaltyTime() != PenaltyTime.PrePlay)
          this.ResetPlay();
        this.playersManager.SetAfterPlayActionsForAllPlayers();
      }
      if (flag)
      {
        this.timeManager.SetRunGameClock(this.timeManager.WasGameClockRunningBeforeBeingSet);
        this.SaveMatchStateAfterPlay();
        this.timeManager.SetRunGameClock(false);
      }
      else
        this.SaveMatchStateAfterPlay();
      if (this.penaltyManager.isPenaltyOnPlay && this.penaltyManager.isPenaltyAccepted)
      {
        PlayManager playManager = this.playManager;
        Penalty penaltyOnPlay = this.penaltyManager.GetPenaltyOnPlay();
        string log = "There was a penalty on the play - " + penaltyOnPlay.ToString() + ".  It was accepted";
        playManager.AddToCurrentPlayLog(log);
        penaltyOnPlay = this.penaltyManager.GetPenaltyOnPlay();
        if (penaltyOnPlay.GetPenaltyTime() == PenaltyTime.PrePlay)
          this.afterPlay = this.beforePlay;
        penaltyOnPlay = this.penaltyManager.GetPenaltyOnPlay();
        if (penaltyOnPlay.GetPenaltyType() != PenaltyType.KickoffOutOfBounds)
        {
          this.RestoreMatchStateAfterPenalty();
          this.ApplyPenalty();
        }
        else
          this.ChangePossession(ChangePosType.Location, DriveEndType.Penalty, ballLocation: Field.KICKOFF_OOB_LOCATION);
        this.ChooseNewPlay();
      }
      else
      {
        if (this.penaltyManager.isPenaltyOnPlay && !this.penaltyManager.isPenaltyAccepted)
        {
          PlayManager playManager = this.playManager;
          Penalty penaltyOnPlay = this.penaltyManager.GetPenaltyOnPlay();
          string log = "There was a penalty on the play - " + penaltyOnPlay.ToString() + ".  It was declined";
          playManager.AddToCurrentPlayLog(log);
          penaltyOnPlay = this.penaltyManager.GetPenaltyOnPlay();
          if (penaltyOnPlay.GetPenaltyTime() == PenaltyTime.PrePlay)
          {
            this.RestoreMatchStateAfterPenalty();
            this.ChooseNewPlay();
          }
          else
            ++ProEra.Game.MatchState.Down.Value;
        }
        else
          ++ProEra.Game.MatchState.Down.Value;
        if (global::Game.BallHolderIsNotNull || ProEra.Game.PlayState.IsPuntOrKickoff)
        {
          this.TrackStats();
          this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
          if ((double) this.ballManager.trans.position.x > (double) Field.POSITIVE_HASH_XPOS)
            this.SetBallHashPosition(Field.POSITIVE_HASH_XPOS);
          else if ((double) this.ballManager.trans.position.x < (double) Field.NEGATIVE_HASH_XPOS)
            this.SetBallHashPosition(Field.NEGATIVE_HASH_XPOS);
          else
            this.SetBallHashPosition(this.ballManager.trans.position.x);
        }
        if (global::Game.PET_IsTackle)
        {
          int num1 = this.IsSimulating ? 1 : 0;
        }
        this.timeManager.GetQuarter();
        if (!(bool) ProEra.Game.MatchState.Turnover && !global::Game.PET_IsTackle && !global::Game.PET_IsOOB)
        {
          int num2 = global::Game.PET_IsQBSlide ? 1 : 0;
        }
        if ((bool) ProEra.Game.MatchState.Turnover && ProEra.Game.PlayState.IsRunOrPass)
        {
          int num3 = this.fumbleOccured ? 1 : 0;
          this.endOfPlayTimer = Mathf.Min(this.endOfPlayTimer, (float) (ProEra.Game.MatchState.GameLength.Value - Mathf.CeilToInt(this.timeManager.GetGameClockTimer())));
        }
        else if (!global::Game.PET_IsTouchdown && !(bool) ProEra.Game.MatchState.Turnover && (global::Game.PET_IsTackle || global::Game.PET_IsOOB || global::Game.PET_IsQBSlide))
          UnityEngine.Random.Range(0, 100);
        if (global::Game.CanUserRunHurryUp())
        {
          switch (this.playManager.GetOffensivePlayForAI().GetPlayType())
          {
            case PlayType.Run:
            case PlayType.Pass:
              Action<bool> allowUserHurryUp = this.OnAllowUserHurryUp;
              if (allowUserHurryUp != null)
              {
                allowUserHurryUp(true);
                break;
              }
              break;
          }
        }
      }
      if (!this.IsSimulating)
      {
        yield return Timing.WaitForSeconds(this.endOfPlayTimer);
        if (global::Game.IsNotSpectateMode && this.playManager.quickCleanUp)
        {
          GamePlayerController.CameraFade.Fade();
          yield return Timing.WaitForSeconds(0.35f);
        }
      }
      this.playManager.CleanUpPlay();
      Action<bool> allowUserHurryUp1 = this.OnAllowUserHurryUp;
      if (allowUserHurryUp1 != null)
        allowUserHurryUp1(false);
      if (!this.checkForEndOfQuarter && global::Game.BS_OnTee)
        SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetBallOnTee();
    }
  }

  public void SetBallOn(float p)
  {
    ProEra.Game.MatchState.BallOn.Value = p;
    this.FieldManager.SetLineOfScrimmageLine();
  }

  public void SetBallHashPosition(float p) => this.ballHashPosition = p;

  private void CheckForEndOfPlayCelebrations(PlayEndType playEndType)
  {
    float range1 = 10f;
    Vector3 position = global::Game.OffensiveQB.trans.position;
    List<PlayerAI> playerAiList = global::Game.OffensiveQB.onUserTeam ? this.playersManager.curUserScriptRef : this.playersManager.curCompScriptRef;
    double range2 = (double) range1;
    List<PlayerAI> players1 = playerAiList;
    List<PlayerAI> playersInRange = PlayersManager.GetPlayersInRange(position, (float) range2, players1);
    Vector3 referencePoint = global::Game.BallHolderIsNotNull ? this.playersManager.ballHolderScript.trans.position : Vector3.zero;
    List<PlayerAI> players2 = !global::Game.BallHolderIsNotNull || !this.playersManager.ballHolderScript.onUserTeam ? this.playersManager.curUserScriptRef : this.playersManager.curCompScriptRef;
    bool flag = Field.FurtherDownfield(this.FieldManager.tempBallPos, MatchManager.firstDown);
    int num = Mathf.RoundToInt((float) (((double) this.FieldManager.tempBallPos - (double) this.savedLineOfScrim) / 24.0 * 50.0)) * global::Game.OffensiveFieldDirection;
    if (playEndType == PlayEndType.Incomplete)
    {
      num = 0;
      flag = false;
    }
    if (playEndType == PlayEndType.Touchdown && global::Game.BallHolderIsNotNull)
    {
      this.playersManager.ballHolderScript.SetCelebrationFromCategory(CelebrationCategory.Touchdown, "TOUCHDOWN");
      AppSounds.PlayPlayerCelebrationChatter(ESfxTypes.kCelebrationChatterTD, this.playersManager.ballHolderScript.trans);
      MatchManager.SetCelebrationForPlayerList(playersInRange, CelebrationCategory.Generic, "GENERIC LINEMEN TOUCHDOWN");
      MatchManager.SetCelebrationForPlayerList(PlayersManager.GetPlayersInRange(referencePoint, range1, players2), CelebrationCategory.GiveUpTouchdown, "GAVE UP TOUCHDOWN");
    }
    else if (playEndType == PlayEndType.Incomplete)
    {
      List<PlayerAI> players3 = global::Game.IsPlayerOneOnOffense ? this.playersManager.curCompScriptRef : this.playersManager.curUserScriptRef;
      MatchManager.SetCelebrationForPlayerList(PlayersManager.GetPlayersInRange(this.ballManager.trans.position, range1, players3), CelebrationCategory.IncompletePass, "DEFENDERS CELEBRATING INCOMPLETION");
    }
    else if (global::Game.IsTurnover && !global::Game.IsPunt && global::Game.IsNotKickoff && !this.IsSimulating)
    {
      this.playersManager.ballHolderScript.SetCelebrationFromCategory(CelebrationCategory.Generic, "TURNOVER");
      AppSounds.PlayPlayerCelebrationChatter(ESfxTypes.kCelebrationChatterTurnover, this.playersManager.ballHolderScript.trans);
      MatchManager.SetCelebrationForPlayerList(playersInRange, CelebrationCategory.Frustrated, "FRUSTRATED LINEMEN TURNOVER");
    }
    else if (global::Game.IsKickoff)
    {
      if (!global::Game.IsOnsidesKick || global::Game.IsTurnover)
        return;
      MatchManager.SetCelebrationForPlayerList(this.playersManager.Offense, CelebrationCategory.Generic, "KICKING TEAM RECOVERED ONSIDE KICK");
      MatchManager.SetCelebrationForPlayerList(this.playersManager.Defense, CelebrationCategory.Frustrated, "RECEIVING TEAM GAVE UP ONSIDE KICK");
    }
    else
    {
      if (global::Game.IsPunt)
        return;
      if (global::Game.IsRunOrPass && num > 25 && global::Game.BallHolderIsNotNull)
      {
        if ((double) UnityEngine.Random.value + (double) num / 10.0 < 0.7)
          return;
        if (global::Game.IsPass && this.playersManager.ballWasThrownOrKicked && num > 35)
          global::Game.OffensiveQB.SetCelebrationFromCategory(CelebrationCategory.QBCelebration, "BIG THROW");
        if (global::Game.IsRun || global::Game.IsPass && global::Game.BallIsNotThrownOrKicked)
        {
          this.playersManager.ballHolderScript.SetCelebrationFromCategory(CelebrationCategory.Generic, "BIG RUN");
          MatchManager.SetCelebrationForPlayerList(playersInRange, CelebrationCategory.Generic, "GENERIC LINEMEN BIG RUN");
        }
        else
        {
          this.playersManager.ballHolderScript.SetCelebrationFromCategory(CelebrationCategory.Generic, "BIG RECEPTION");
          MatchManager.SetCelebrationForPlayerList(playersInRange, CelebrationCategory.Generic, "GENERIC LINEMEN BIG RECEPTION");
        }
        AppSounds.PlayPlayerCelebrationChatter(ESfxTypes.kCelebrationChatterFD, this.playersManager.ballHolderScript.trans);
        MatchManager.SetCelebrationForPlayerList(PlayersManager.GetPlayersInRange(referencePoint, range1, players2), CelebrationCategory.Frustrated, "FRUSTRATED BY BIG PLAY");
      }
      else if (playEndType == PlayEndType.MadeFG && !MatchManager.runningPat)
      {
        if ((double) UnityEngine.Random.value < 0.5)
          global::Game.Kicker.SetCelebrationFromCategory(CelebrationCategory.Generic, "MADE FG");
        MatchManager.SetCelebrationForPlayerList(playersInRange, CelebrationCategory.Generic, "GENERIC LINEMEN MADE FG");
      }
      else if (playEndType == PlayEndType.MissedFG)
      {
        global::Game.Kicker.SetCelebrationFromCategory(CelebrationCategory.MissedFG, "MISSED FG");
        MatchManager.SetCelebrationForPlayerList(playersInRange, CelebrationCategory.Frustrated, "GENERIC LINEMEN MISSED FG");
      }
      else if (flag && !MatchManager.runningPat && global::Game.BallHolderIsNotNull)
      {
        if ((double) UnityEngine.Random.value < 0.60000002384185791 || num < 3)
          return;
        if (global::Game.IsPass && this.playersManager.ballWasThrownOrKicked)
          global::Game.OffensiveQB.SetCelebrationFromCategory(CelebrationCategory.QBCelebration, "FIRST DOWN");
        this.playersManager.ballHolderScript.SetCelebrationFromCategory(CelebrationCategory.FirstDownCelebration, "FIRST DOWN");
        AppSounds.PlayPlayerCelebrationChatter(ESfxTypes.kCelebrationChatterFD, this.playersManager.ballHolderScript.trans);
        MatchManager.SetCelebrationForPlayerList(playersInRange, CelebrationCategory.Generic, "GENERIC LINEMEN FIRST DOWN");
        MatchManager.SetCelebrationForPlayerList(PlayersManager.GetPlayersInRange(referencePoint, range1, players2), CelebrationCategory.Frustrated, "FRUSTRATED BY FIRST DOWN");
      }
      else if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetPenaltyType() == PenaltyType.DelayOfGame)
      {
        for (int index = 0; index < this.playersManager.curUserScriptRef.Count; ++index)
        {
          PlayerAI playerAi1 = this.playersManager.curUserScriptRef[index];
          if (playerAi1.celebrations.GetCelebrationCategory() == CelebrationCategory.None)
          {
            if (playerAi1.onUserTeam)
              playerAi1.celebrations.SetFrustratedInPlaceCelebration();
            else
              playerAi1.celebrations.SetCelebrationFromCategory(CelebrationCategory.Neutral);
          }
          PlayerAI playerAi2 = this.playersManager.curCompScriptRef[index];
          if (playerAi2.celebrations.GetCelebrationCategory() == CelebrationCategory.None)
          {
            if (playerAi2.onUserTeam)
              playerAi2.celebrations.SetFrustratedInPlaceCelebration();
            else
              playerAi2.celebrations.SetCelebrationFromCategory(CelebrationCategory.Neutral);
          }
        }
      }
      else
      {
        for (int index = 0; index < this.playersManager.curUserScriptRef.Count; ++index)
        {
          PlayerAI playerAi3 = this.playersManager.curUserScriptRef[index];
          if (playerAi3.celebrations.GetCelebrationCategory() == CelebrationCategory.None)
            playerAi3.celebrations.SetCelebrationFromCategory(CelebrationCategory.Neutral);
          PlayerAI playerAi4 = this.playersManager.curCompScriptRef[index];
          if (playerAi4.celebrations.GetCelebrationCategory() == CelebrationCategory.None)
            playerAi4.celebrations.SetCelebrationFromCategory(CelebrationCategory.Neutral);
        }
      }
    }
  }

  private static void SetCelebrationForPlayerList(
    List<PlayerAI> playerList,
    CelebrationCategory category,
    string message)
  {
    for (int index = 0; index < playerList.Count; ++index)
    {
      playerList[index].SetCelebrationFromCategory(category, message);
      if (category == CelebrationCategory.IncompletePass)
        AppSounds.PlayPlayerCelebrationChatter(ESfxTypes.kCelebrationChatterINC, playerList[index].trans);
    }
  }

  public void ShowHalftime()
  {
    ScoreClockState.DownAndDistanceVisible.SetValue(false);
    this.timeManager.SetTwoMinuteWarningDone(false);
    this.timeManager.SetRunPlayClock(false);
    PlaybookState.HidePlaybook.Trigger();
  }

  public void ShowEndOfGame()
  {
    ScoreClockState.DownAndDistanceVisible.SetValue(false);
    PlaybookState.HidePlaybook.Trigger();
  }

  public void EndOfGame()
  {
    PEGameplayEventManager.RecordGameOverEvent();
    ProEra.Game.MatchState.Stats.User.FinializeGameStats(this.playersManager.userTeamData);
    int num = AppState.SeasonMode.Value == ESeasonMode.kUnknown ? 1 : 0;
    PersistentData.LastPlayedGameType = num != 0 ? GameType.QuickMatch : GameType.SeasonMode;
    bool playerWonSeasonGame = ProEra.Game.MatchState.Stats.User.Score > ProEra.Game.MatchState.Stats.Comp.Score;
    TeamData team = ProEra.Game.MatchState.Stats.User.Score > ProEra.Game.MatchState.Stats.Comp.Score ? this.playersManager.userTeamData : this.playersManager.compTeamData;
    int overallMvp = PlayerStats.GetOverallMVP(team.MainRoster, StatDuration.CurrentGame);
    PlayerData player1 = team.GetPlayer(overallMvp);
    float _statScore = PlayerStats.GetOffensiveStatScore(player1.CurrentGameStats) + PlayerStats.GetDefensiveStatScore(player1.CurrentGameStats);
    int offensiveMvp = PlayerStats.GetOffensiveMVP(team.MainRoster, StatDuration.CurrentGame);
    PlayerData player2 = team.GetPlayer(offensiveMvp);
    float offensiveStatScore = PlayerStats.GetOffensiveStatScore(player2.CurrentGameStats);
    int defensiveMvp = PlayerStats.GetDefensiveMVP(team.MainRoster, StatDuration.CurrentGame);
    PlayerData player3 = team.GetPlayer(defensiveMvp);
    float defensiveStatScore = PlayerStats.GetDefensiveStatScore(player3.CurrentGameStats);
    ProEra.Game.MatchState.CurrentMatchState.SetValue(EMatchState.End);
    if (AppState.SeasonMode.Value != ESeasonMode.kUnknown)
    {
      if (playerWonSeasonGame)
        ++SeasonModeManager.self.seasonModeData.seasonWins;
      else
        ++SeasonModeManager.self.seasonModeData.seasonLosses;
      PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState.SaveSeasonDuringLoading = true;
    }
    if (num != 0)
    {
      AnalyticEvents.Record<ExhibitionGameCompletedArgs>(new ExhibitionGameCompletedArgs(Time.timeSinceLevelLoad, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, Time.time - this._gameStartTime));
    }
    else
    {
      int seasonWins = SeasonModeManager.self.seasonModeData.seasonWins;
      int seasonLosses = SeasonModeManager.self.seasonModeData.seasonLosses;
      AnalyticEvents.Record<SeasonGameCompletedArgs>(new SeasonGameCompletedArgs(seasonWins + seasonLosses, playerWonSeasonGame, seasonWins, seasonLosses, Time.time - this._gameStartTime, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad, ProEra.Game.MatchState.Stats.User.Score, ProEra.Game.MatchState.Stats.Comp.Score, MatchManager.gameLength, (int) GameSettings.DifficultyLevel, !ScriptableSingleton<ThrowSettings>.Instance.AutoAimSettings.UseMinimalPassAssistSettings));
      if (SeasonModeManager.self.IsSeasonOver())
        AnalyticEvents.Record<SeasonCompletedArgs>(new SeasonCompletedArgs(SeasonModeManager.self.IsFourthRoundOfNFLPlayoffs() & playerWonSeasonGame));
      if (SeasonModeManager.self.IsSeasonOver() & playerWonSeasonGame)
        SeasonModeManager.self.seasonModeData.seasonState = ProEraSeasonState.WonInChampionShip;
    }
    PersistentData.gameMvp = Award.NewAward(AwardType.Overall_Player_Of_The_Game);
    PersistentData.gameMvp.SetAwardData(team, player1.CurrentGameStats, player1.IndexOnTeam, _statScore, AwardType.Overall_Player_Of_The_Game);
    PersistentData.offPlayerOfTheGame = Award.NewAward(AwardType.Offensive_Player_Of_The_Week);
    PersistentData.offPlayerOfTheGame.SetAwardData(team, player2.CurrentGameStats, player2.IndexOnTeam, offensiveStatScore, AwardType.Offensive_Player_Of_The_Week);
    PersistentData.defPlayerOfTheGame = Award.NewAward(AwardType.Defensive_Player_Of_The_Week);
    PersistentData.defPlayerOfTheGame.SetAwardData(team, player3.CurrentGameStats, player3.IndexOnTeam, defensiveStatScore, AwardType.Defensive_Player_Of_The_Week);
    if (num == 0)
      this._celebratingSuperbowlWin = SeasonModeManager.self.IsFourthRoundOfNFLPlayoffs() & playerWonSeasonGame;
    this.StartCoroutine(this.ExitGame(true, 5f));
  }

  public IEnumerator ExitGame(bool gameEnded = false, float exitWaitTime = 2f)
  {
    VRState.PausePermission = false;
    System.Action qbPositionChange = this.OnQBPositionChange;
    if (qbPositionChange != null)
      qbPositionChange();
    if ((UnityEngine.Object) OnFieldCanvas.Instance != (UnityEngine.Object) null)
      OnFieldCanvas.Instance.ShowGameOver();
    yield return (object) new WaitForSeconds(11f);
    Globals.PauseGame.SetValue(false);
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.ClearUniform(UniformAssetType.USER);
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.ClearUniform(UniformAssetType.COMP);
    if (gameEnded)
    {
      this.StoreGameResults();
      if (global::Game.IsSeasonMode)
      {
        PersistentData.showFranchise = true;
        PersistentData.simulateWeek = true;
      }
      else
        PersistentData.saveGameStats = true;
    }
    else if (global::Game.IsSeasonMode)
    {
      PersistentData.watchingNonUserMatch = false;
      PersistentData.simulateWeek = false;
      PersistentData.showFranchise = true;
    }
    else
      PersistentData.showFranchise = false;
    if (this._celebratingSuperbowlWin)
    {
      this.timeManager.clockEnabled = false;
      this.timeManager.SetRunGameClock(false);
      MainGameTablet.SelfDestroy();
      ProEra.Game.MatchState.CurrentMatchState.SetValue(EMatchState.End);
      Vector3 position = UnityEngine.Object.Instantiate<SuperbowlPodium>(this._championStandPrefab).PlayerTpTarget.position;
      this.playersManager.PutPlayersInCelebrationHuddle(new Vector3(position.x, 0.0f, position.z));
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(position, Quaternion.Euler(0.0f, 180f, 0.0f));
      VRState.LocomotionEnabled.SetValue(false);
      yield return (object) new WaitForSeconds(15f);
      Globals.GameOver.SetValue(true);
      VRState.LocomotionEnabled.SetValue(true);
    }
    else
      Globals.GameOver.SetValue(true);
    yield return (object) null;
  }

  private void StoreGameResults()
  {
    MatchStatsManager instance = SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance;
    PersistentData.userGameSummary = instance.userGameSummary;
    PersistentData.compGameSummary = instance.compGameSummary;
  }

  private void SetupPlayBegin()
  {
    this.fumbleOccured = false;
    this.onsideKick = false;
    this.playersManager.forceQBScramble = false;
    this.brokenTackles = 0;
    MatchManager.instance.playersManager.tackler = (PlayerAI) null;
  }

  private IEnumerator DelayProcessEndOfQuarter(int quarter)
  {
    if (!this.IsSimulating)
      yield return (object) new WaitForSeconds(3f);
    while ((bool) (UnityEngine.Object) AxisGameFlow.instance && AxisGameFlow.instance.IsTackling)
      yield return (object) null;
    if (quarter == 1 || quarter == 3)
    {
      OnFieldCanvas.Instance.ShowOnFieldScore(0.0f);
      if (this.IsSimulating)
        yield return (object) new WaitForSeconds(3f);
      MatchManager.instance.HandleEndOfQuarter(quarter);
    }
    else
    {
      switch (quarter)
      {
        case 4:
          MatchManager.instance.StartOvertime();
          break;
        case 5:
          MatchManager.instance.HandleEndOfQuarter(5);
          break;
      }
    }
  }

  private void ProcessHalfTime()
  {
    OnFieldCanvas.Instance.ShowOnFieldScore();
    ScoreClockState.DownAndDistanceVisible.SetValue(false);
    this.timeManager.SetTwoMinuteWarningDone(false);
    this.timeManager.SetRunPlayClock(false);
    PlaybookState.HidePlaybook.Trigger();
  }

  public void StartOvertime()
  {
    this.SetBallHashPosition(0.0f);
    this.SetBallOn(Field.KICKOFF_LOCATION);
    this.savedLineOfScrim = MatchManager.ballOn;
    this.haveBothTeamsHadTheBallInOT = false;
    this.ballManager.SetPosition(new Vector3(this.ballHashPosition, Ball.BALL_ON_GROUND_HEIGHT, MatchManager.ballOn));
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.SetActive(false);
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.lineOfScrimmage.SetActive(false);
    ScoreClockState.Quarter.Value = this.timeManager.GetQuarterString();
    this.playersManager.SetAfterPlayActionsForAllPlayers();
    if (global::Game.IsSpectateMode)
    {
      MatchManager.instance.StartFromCoinFlip((double) UnityEngine.Random.value > 0.5, (double) UnityEngine.Random.value > 0.5 ? EMatchState.UserOnDefense : EMatchState.UserOnOffense);
    }
    else
    {
      OnFieldCanvas.Instance.ShowOnFieldScore();
      this.StartCoroutine(this.ContinueOvertime());
    }
  }

  private IEnumerator ContinueOvertime()
  {
    yield return (object) new WaitForSeconds(10f);
    UIDispatch.FrontScreen.DisplayView(EScreens.kCoinToss);
    PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(new Vector3(0.0f, 180f, 0.0f)));
    yield return (object) null;
  }

  public void HandleEndOfQuarter(int quarter)
  {
    this.timeManager.ResetPlayClock();
    this.timeManager.SetRunPlayClock(false);
    ScoreClockState.PlayClock.Value = this.timeManager.GetPlayClockTime();
    this.SetWindValue();
    if (this.playersManager.snapBallCoroutine != null)
      this.StopCoroutine(this.playersManager.snapBallCoroutine);
    switch (quarter)
    {
      case 1:
      case 3:
        Field.FlipFieldDirection();
        ScoreClockState.Quarter.Value = this.timeManager.GetQuarterString();
        this.SetBallHashPosition(this.ballHashPosition * -1f);
        this.SetBallOn(ProEra.Game.MatchState.BallOn.Value * -1f);
        ProEra.Game.MatchState.FirstDown.Value *= -1f;
        this.FieldManager.SetFirstDownLine();
        this.ballManager.FreezeAfterPlay();
        this.FieldManager.SetLineOfScrimmageLine();
        this.playersManager.PutAllPlayersInHuddle();
        this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
        if (global::Game.UserCallsPlays && global::Game.IsPlayerOneOnOffense && !ProEra.Game.MatchState.IsKickoff.Value)
        {
          this.playManager.SelectNextOffPlayForUser();
          this.timeManager.SetRunPlayClock(true);
          break;
        }
        this.playManager.SelectNextPlaysForAI();
        break;
      case 2:
        SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance.ResetTimeouts();
        ScoreClockState.Quarter.Value = this.timeManager.GetQuarterString();
        ScoreClockState.SetDownAndDistance.Trigger("KICKOFF");
        this.ProcessHalfTime();
        ProEra.Game.MatchState.HalftimeMargin.Value = ProEra.Game.MatchState.Stats.ScoreDifference();
        this.SetBallHashPosition(0.0f);
        this.SetBallOn(Field.KICKOFF_LOCATION);
        this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
        this.ballManager.SetPosition(new Vector3(this.ballHashPosition, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
        if (this.homeTeamGetsBallAtHalf && global::Game.IsHomeTeamOnOffense)
          this.SetCurrentMatchState((bool) Globals.UserIsHome ? EMatchState.UserOnDefense : EMatchState.UserOnOffense);
        else if (!this.homeTeamGetsBallAtHalf && !global::Game.IsHomeTeamOnOffense)
          this.SetCurrentMatchState((bool) Globals.UserIsHome ? EMatchState.UserOnOffense : EMatchState.UserOnDefense);
        ProEra.Game.MatchState.IsKickoff.Value = true;
        this.playersManager.SetAfterPlayActionsForAllPlayers();
        this.playManager.SelectNextPlaysForAI();
        break;
      case 4:
        this.playersManager.AISnapBall();
        ScoreClockState.DownAndDistanceVisible.SetValue(false);
        PlaybookState.HidePlaybook.Trigger();
        if (AppState.GameMode == EGameMode.k2MD)
          break;
        this.EndOfGame();
        break;
      case 5:
        Field.FlipFieldDirection();
        ScoreClockState.Quarter.Value = this.timeManager.GetQuarterString();
        this.ballManager.SetParent((Transform) null);
        this.SetBallHashPosition(this.ballHashPosition * -1f);
        this.SetBallOn(ProEra.Game.MatchState.BallOn.Value * -1f);
        ProEra.Game.MatchState.FirstDown.Value *= -1f;
        this.savedLineOfScrim = MatchManager.ballOn;
        this.FieldManager.SetFirstDownLine();
        this.ballManager.FreezeAfterPlay();
        this.FieldManager.SetLineOfScrimmageLine();
        this.playersManager.PutAllPlayersInHuddle();
        if (global::Game.UserCallsPlays && global::Game.IsPlayerOneOnOffense)
        {
          this.playManager.SelectNextOffPlayForUser();
          this.timeManager.SetRunPlayClock(true);
          break;
        }
        this.playManager.SelectNextPlaysForAI();
        break;
    }
  }

  public void HandlePuntBlocked() => PEGameplayEventManager.RecordPuntBlockedEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position);

  public void HandleTwoMinuteWarning()
  {
    PEGameplayEventManager.RecordTwoMinWarningEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position);
    if (this.IsSimulating)
      return;
    TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.TwoMinuteWarning, true);
    this._twoMinuteWarningRoutine.Run(this.WaitForTwoMinuteWarningTransition());
  }

  private IEnumerator WaitForTwoMinuteWarningTransition()
  {
    yield return (object) new WaitForSeconds(3f);
    TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.TwoMinuteWarning, false);
  }

  public void SetWindValue()
  {
    if (PersistentData.windType == 0 || !PersistentData.stadiumSet.affectedByWind)
      this.windSpeed.Set(0.0f, 0.0f, 0.0f);
    else if ((UnityEngine.Object) PersistentData.stadiumSet != (UnityEngine.Object) null)
    {
      if (PersistentData.stadiumSet.affectedByWind)
        return;
      this.windSpeed.Set(0.0f, 0.0f, 0.0f);
    }
    else if ((UnityEngine.Object) PersistentData.stadiumSet == (UnityEngine.Object) null)
      Debug.LogWarning((object) "CD: Not checking Axis stadium wind conditions");
    else if (PersistentData.windType == 1)
    {
      this.windSpeed.Set(UnityEngine.Random.Range(-Field.FIVE_YARDS, Field.FIVE_YARDS), 0.0f, UnityEngine.Random.Range(-Field.FIVE_YARDS, Field.FIVE_YARDS));
    }
    else
    {
      if (PersistentData.windType != 2)
        return;
      this.windSpeed.Set(UnityEngine.Random.Range(-Field.TEN_YARDS, Field.TEN_YARDS), 0.0f, UnityEngine.Random.Range(-Field.TEN_YARDS, Field.TEN_YARDS));
    }
  }

  public void SpinWindArrow()
  {
  }

  public void KickBallFromButton() => this.PerformKickActionForPlayerOneIfNeeded();

  private void PerformKickActionForPlayerOneIfNeeded()
  {
    if (global::Game.IsPlaybookVisible || !this.IsSnapAllowed())
      return;
    if (global::Game.IsSpecialTeamsPlay)
    {
      if (!global::Game.IsPlayerOneOnOffense)
        return;
      this.KickStateAction((Vector2) Input.mousePosition);
    }
    else
    {
      if (!global::Game.IsPlayerOneOnOffense)
        return;
      this.SnapBall((Vector2) Input.mousePosition);
    }
  }

  private void KickStateAction(Vector2 inputPosition)
  {
    if (global::Game.IsPlaybookVisible || !this.allowKickAction)
      return;
    this.SnapBall(inputPosition);
  }

  private bool CanPerformSnapOrKickAction()
  {
    bool flag = (bool) VRState.PauseMenu.GetValue();
    return global::Game.IsPlayInactive && (global::Game.UserControlsPlayers || global::Game.UserControlsQB) && !flag && !global::Game.HasScreenOverlay;
  }

  private void CheckForInjuries(PlayEndType playEndType)
  {
    if (!PersistentData.injuriesOn || this.checkedForInjuriesAlready)
      return;
    this.checkedForInjuriesAlready = true;
    int injuryFrequency = PersistentSingleton<SaveManager>.Instance.gameSettings.InjuryFrequency;
    int num1 = UnityEngine.Random.Range(0, 100);
    if (num1 >= injuryFrequency)
      return;
    int num2 = num1 % 2 == 0 ? 1 : 0;
    bool flag = num2 == 0 ? global::Game.IsPlayerOneOnDefense : global::Game.IsPlayerOneOnOffense;
    TeamData teamData;
    List<PlayerAI> playerAiList;
    if (num2 != 0)
    {
      teamData = this.playersManager.userTeamData;
      playerAiList = this.playersManager.curUserScriptRef;
    }
    else
    {
      teamData = this.playersManager.compTeamData;
      playerAiList = this.playersManager.curCompScriptRef;
    }
    this._injuryCandidates.Clear();
    if (!flag)
    {
      for (int index = 0; index < 11; ++index)
        this._injuryCandidates.Add(playerAiList[index].indexOnTeam);
    }
    else
    {
      for (int index = 0; index < 11; ++index)
      {
        if (index == 5)
        {
          if (ProEra.Game.PlayState.IsPass && !this.playersManager.ballWasThrownOrKicked)
            this._injuryCandidates.Add(playerAiList[index].indexOnTeam);
        }
        else
          this._injuryCandidates.Add(playerAiList[index].indexOnTeam);
      }
    }
    int index1 = UnityEngine.Random.Range(0, this._injuryCandidates.Count);
    for (PlayerData player = teamData.GetPlayer(this._injuryCandidates[index1]); player.PlayerPosition == Position.K || player.PlayerPosition == Position.P; player = teamData.GetPlayer(this._injuryCandidates[index1]))
      index1 = UnityEngine.Random.Range(0, this._injuryCandidates.Count);
    int injuryCandidate = this._injuryCandidates[index1];
    if (teamData.GetPlayer(injuryCandidate).CurrentInjury != null)
      return;
    InjuryManager.CreateAndSetPlayerInjury(teamData, injuryCandidate, true);
    teamData.TeamDepthChart.SubInNextPlayerOnDepthChartForThisStarter(injuryCandidate);
  }

  public void UpdateInjuries(bool clearAllInjuries)
  {
    this.UpdateInjuriesForTeam(this.playersManager.userTeamData, clearAllInjuries, true);
    this.UpdateInjuriesForTeam(this.playersManager.compTeamData, clearAllInjuries, false);
  }

  private void UpdateInjuriesForTeam(TeamData team, bool clearAllInjuries, bool isUserTeam)
  {
    for (int index = 0; index < team.GetNumberOfPlayersOnRoster(); ++index)
    {
      if (team.GetPlayer(index).CurrentInjury != null)
      {
        if (clearAllInjuries)
          team.GetPlayer(index).CurrentInjury.playsRemaining = 0;
        else
          --team.GetPlayer(index).CurrentInjury.playsRemaining;
        if (team.GetPlayer(index).CurrentInjury.playsRemaining <= 0 && team.GetPlayer(index).CurrentInjury.weeksRemaining <= 0)
        {
          int playerIndexOfStarter = team.TeamDepthChart.GetPlayerIndexOfStarter(team.GetPlayer(index).CurrentInjury.startingPosition);
          team.TeamDepthChart.SubstitutePlayers(playerIndexOfStarter, index);
          team.GetPlayer(index).CurrentInjury = (Injury) null;
        }
      }
    }
  }

  public void SetCurrentMatchState(EMatchState newMatchState)
  {
    this.currentMatchState = newMatchState;
    ProEra.Game.MatchState.CurrentMatchState.SetValue(newMatchState);
    switch (newMatchState)
    {
      case EMatchState.UserOnOffense:
        for (int index = 0; index < this.playersManager.curUserScriptRef.Count; ++index)
        {
          this.playersManager.curUserScriptRef[index].SetToOffense();
          this.playersManager.curCompScriptRef[index].SetToDefense();
        }
        break;
      case EMatchState.UserOnDefense:
        for (int index = 0; index < this.playersManager.curUserScriptRef.Count; ++index)
        {
          this.playersManager.curUserScriptRef[index].SetToDefense();
          this.playersManager.curCompScriptRef[index].SetToOffense();
        }
        break;
    }
  }

  public void AddScore(int amt)
  {
    int quarter = this.timeManager.GetQuarter() - 1;
    bool flag1;
    if (global::Game.IsPlayerOneOnOffense)
    {
      if (amt == -2)
      {
        flag1 = !PersistentData.userIsHome;
        ProEra.Game.MatchState.Stats.Comp.IncrementScore(quarter, 2);
      }
      else
      {
        flag1 = PersistentData.userIsHome;
        ProEra.Game.MatchState.Stats.User.IncrementScore(quarter, amt);
      }
    }
    else if (amt == -2)
    {
      flag1 = PersistentData.userIsHome;
      ProEra.Game.MatchState.Stats.User.IncrementScore(quarter, 2);
    }
    else
    {
      flag1 = !PersistentData.userIsHome;
      ProEra.Game.MatchState.Stats.Comp.IncrementScore(quarter, amt);
    }
    if (amt <= 3)
      ;
    if (global::Game.IsNot2PMatch)
    {
      int score1 = ProEra.Game.MatchState.Stats.User.Score;
      int score2 = ProEra.Game.MatchState.Stats.Comp.Score;
      int num = ProEra.Game.MatchState.Difficulty.Value;
      if (this.dynamicDif > 0 && score1 > score2)
      {
        num -= this.dynamicDif;
        this.dynamicDif = 0;
      }
      else if (num > 1 && this.dynamicDif < 0 && score2 > score1)
      {
        num -= this.dynamicDif;
        this.dynamicDif = 0;
      }
      if (amt > 2 && num >= 1)
      {
        if (global::Game.IsPlayerOneOnDefense && score2 > score1 + 7 && this.dynamicDif < 5)
        {
          ++this.dynamicDif;
          ++num;
        }
        if (global::Game.IsPlayerOneOnOffense && score1 > score2 + 7 && this.dynamicDif > -5)
        {
          --this.dynamicDif;
          --num;
        }
      }
      ProEra.Game.MatchState.Difficulty.SetValue(num);
    }
    if (!this.timeManager.IsInOvertime())
      return;
    bool flag2 = false;
    bool flag3 = ProEra.Game.MatchState.Stats.EqualScore();
    if (amt == 6 || amt == -2)
    {
      flag2 = true;
    }
    else
    {
      MonoBehaviour.print((object) ("Have both teams had the ball: " + this.haveBothTeamsHadTheBallInOT.ToString()));
      MonoBehaviour.print((object) ("Home Team Got Ball first: " + this.homeTeamGotBallFirstInOT.ToString()));
      MonoBehaviour.print((object) ("Home team scored: " + flag1.ToString()));
      if (this.haveBothTeamsHadTheBallInOT && !flag3)
        flag2 = true;
      else if (this.homeTeamGotBallFirstInOT && !flag1 && !flag3)
        flag2 = true;
      else if (!this.homeTeamGotBallFirstInOT & flag1 && !flag3)
        flag2 = true;
    }
    if (!flag2)
      return;
    this.ForceOvertimeGameToEnd();
  }

  private void ForceOvertimeGameToEnd()
  {
    this.timeManager.SetGameClockToZero();
    this.timeManager.SetRunGameClock(false);
    this.timeManager.SetRunPlayClock(false);
    this.allowPATAfterTimeHasExpired = false;
    this.checkForEndOfQuarter = true;
    ProEra.Game.MatchState.RunningPat.Value = false;
  }

  public void ChangePossession(
    ChangePosType type,
    DriveEndType driveEndType,
    bool flipField = true,
    float ballLocation = 24f)
  {
    MonoBehaviour.print((object) "Changing possession");
    if (this.timeManager.IsInOvertime())
    {
      if ((!global::Game.GameIsTied || !global::Game.IsKickoff ? 0 : (!this.haveBothTeamsHadTheBallInOT ? 1 : 0)) == 0)
      {
        if (global::Game.IsPlayerOneOnOffense)
        {
          if (PersistentData.userIsHome && !this.homeTeamGotBallFirstInOT)
            this.haveBothTeamsHadTheBallInOT = true;
          else if (!PersistentData.userIsHome && this.homeTeamGotBallFirstInOT)
            this.haveBothTeamsHadTheBallInOT = true;
        }
        else if (PersistentData.userIsHome && this.homeTeamGotBallFirstInOT)
          this.haveBothTeamsHadTheBallInOT = true;
        else if (!PersistentData.userIsHome && !this.homeTeamGotBallFirstInOT)
          this.haveBothTeamsHadTheBallInOT = true;
      }
      if (this.haveBothTeamsHadTheBallInOT && global::Game.GameIsNotTied)
      {
        this.ForceOvertimeGameToEnd();
        return;
      }
    }
    if (flipField)
      Field.FlipFieldDirection();
    if (global::Game.IsPlayerOneOnOffense)
    {
      if (ProEra.Game.MatchState.Stats.CurrentDrivePlays > ProEra.Game.MatchState.Stats.User.LongestDrive)
        ProEra.Game.MatchState.Stats.User.LongestDrive = ProEra.Game.MatchState.Stats.CurrentDrivePlays;
      if (ProEra.Game.MatchState.Stats.DriveFirstDowns > ProEra.Game.MatchState.Stats.User.MaxDriveFirstDowns)
        ProEra.Game.MatchState.Stats.User.MaxDriveFirstDowns = ProEra.Game.MatchState.Stats.DriveFirstDowns;
      ProEra.Game.MatchState.Stats.User.TotalFirstDowns += ProEra.Game.MatchState.Stats.DriveFirstDowns;
      ProEra.Game.MatchState.Stats.User.StoreDriveEnd(driveEndType);
    }
    else
    {
      if (ProEra.Game.MatchState.Stats.CurrentDrivePlays > ProEra.Game.MatchState.Stats.Comp.LongestDrive)
        ProEra.Game.MatchState.Stats.Comp.LongestDrive = ProEra.Game.MatchState.Stats.CurrentDrivePlays;
      ProEra.Game.MatchState.Stats.Comp.TotalFirstDowns += ProEra.Game.MatchState.Stats.DriveFirstDowns;
      ProEra.Game.MatchState.Stats.Comp.StoreDriveEnd(driveEndType);
    }
    SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance.ResetDriveStats();
    FatigueManager.ResetAllFatigueVales();
    this.timeManager.SetRunGameClock(false);
    switch (type)
    {
      case ChangePosType.AtPos:
        if (global::Game.PET_IsMissedFG)
          this.SetBallOn(Field.MostDownfield(this.FieldManager.lastFGKickPos, Field.OWN_TWENTY_YARD_LINE));
        else if ((UnityEngine.Object) this.playersManager.ballHolderScript == (UnityEngine.Object) null)
          this.SetBallOn(this.FieldManager.tempBallPos);
        else if ((bool) ProEra.Game.MatchState.Turnover && Field.FurtherDownfield(this.playersManager.ballHolderScript.trans.position.z, Field.OFFENSIVE_GOAL_LINE))
          this.SetBallOn(Field.OWN_TWENTY_YARD_LINE);
        else if (global::Game.PET_IsIncomplete)
          this.SetBallOn(this.savedLineOfScrim);
        else
          this.SetBallOn(this.FieldManager.tempBallPos);
        this.SetBallOn(Mathf.Clamp(ProEra.Game.MatchState.BallOn.Value, Field.SOUTH_GOAL_LINE + Field.SIX_INCHES, Field.NORTH_GOAL_LINE - Field.SIX_INCHES));
        this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
        this.SetBallHashPosition(0.0f);
        break;
      case ChangePosType.TeamScored:
        if ((!global::Game.PET_IsTouchdown ? 0 : ((bool) ProEra.Game.MatchState.Turnover ? 1 : 0)) != 0)
        {
          this.SetBallOn(PersistentData.GetHomeTeamData().GetTeamPATLocation());
          this.SetBallHashPosition(0.0f);
          this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
          this.checkForEndOfQuarter = false;
          return;
        }
        if (global::Game.PET_IsSafety)
        {
          this.SetBallOn(Field.SAFETY_KICKOFF_LOCATION);
          this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
        }
        else
        {
          OnFieldCanvas.Instance.ShowOnFieldScore();
          this.SetBallOn(Field.KICKOFF_LOCATION);
          this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
        }
        this.SetBallHashPosition(0.1f * (float) global::Game.OffensiveFieldDirection);
        if (global::Game.IsPlayerOneOnOffense)
        {
          if (!global::Game.Is2PMatch)
            ;
        }
        else
        {
          int num = global::Game.Is2PMatch ? 1 : 0;
        }
        if (this.allowPATAfterTimeHasExpired)
        {
          PlaybookState.HidePlaybook.Trigger();
          break;
        }
        break;
      case ChangePosType.Location:
        this.SetBallOn(Mathf.Clamp(ballLocation, Field.SOUTH_GOAL_LINE + Field.SIX_INCHES, Field.NORTH_GOAL_LINE - Field.SIX_INCHES));
        this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
        break;
      case ChangePosType.KickedOff:
      case ChangePosType.Punted:
        bool flag = (UnityEngine.Object) this.playersManager.ballHolderScript != (UnityEngine.Object) null && !this.playersManager.ballHolderScript.onOffense;
        float p = type == ChangePosType.Punted ? Field.OWN_TWENTY_YARD_LINE : Field.OWN_TWENTY_FIVE_YARD_LINE;
        if (global::Game.PET_IsOOB_In_Endzone || flag && Field.FurtherDownfield(this.FieldManager.tempBallPos, Field.OFFENSIVE_GOAL_LINE))
          this.SetBallOn(p);
        else
          this.SetBallOn(this.FieldManager.tempBallPos);
        this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
        this.SetBallHashPosition(0.0f);
        break;
    }
    ProEra.Game.MatchState.Down.Value = 1;
    this.allowPATAfterTimeHasExpired = false;
    ProEra.Game.MatchState.RunningPat.Value = false;
    if (type != ChangePosType.TeamScored)
    {
      if (global::Game.IsPlayerOneOnOffense)
      {
        PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Defense);
        this.SetCurrentMatchState(EMatchState.UserOnDefense);
        if (!global::Game.IsHomeTeamOnOffense)
          PEGameplayEventManager.RecordHomeTeamTurnover();
        if (global::Game.Is2PMatch)
          throw new NotImplementedException();
      }
      else
      {
        PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Offense);
        this.SetCurrentMatchState(EMatchState.UserOnOffense);
        if (!global::Game.IsHomeTeamOnOffense)
          PEGameplayEventManager.RecordHomeTeamTurnover();
        if (global::Game.Is2PMatch)
          throw new NotImplementedException();
      }
    }
    if (type != ChangePosType.TeamScored)
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetFirstDownAndFirstDownLine();
    this.playersManager.SetAfterPlayActionsForAllPlayers();
    this._hasDriveEnteredRedzone = false;
  }

  private void ChangeDifficultyDynamically()
  {
    if (!global::Game.UserCallsPlays)
      return;
    if (ProEra.Game.MatchState.Stats.ScoreDifference() > 14)
    {
      --PersistentData.offDifficulty;
      --PersistentData.defDifficulty;
    }
    else
    {
      if (ProEra.Game.MatchState.Stats.ScoreDifference() <= 14)
        return;
      ++PersistentData.offDifficulty;
      ++PersistentData.defDifficulty;
    }
  }

  public int GetNumberOfSetPlayers() => this.CheckOffPlayersSet();

  public int GetNumberOfSetPlayersDef() => this.CheckDefPlayersSet();

  public void AllowSnap(float timeToWaitBeforeAllowingSnap = 0.0f)
  {
    this.DisallowSnap();
    if ((double) timeToWaitBeforeAllowingSnap > 0.0)
    {
      this._allowSnapCoroutine = Timing.RunCoroutine(this.DelayAllowSnap(timeToWaitBeforeAllowingSnap));
    }
    else
    {
      this.allowSnap = true;
      System.Action onSnapAllowed = this.OnSnapAllowed;
      if (onSnapAllowed == null)
        return;
      onSnapAllowed();
    }
  }

  private IEnumerator<float> DelayAllowSnap(float timeToWait)
  {
    yield return Timing.WaitForSeconds(timeToWait);
    this.allowSnap = true;
    System.Action onSnapAllowed = this.OnSnapAllowed;
    if (onSnapAllowed != null)
      onSnapAllowed();
  }

  public void DisallowSnap()
  {
    if (this._allowSnapCoroutine.IsRunning)
      Timing.KillCoroutines(this._allowSnapCoroutine);
    this.allowSnap = false;
  }

  public bool IsQBReadyForSnap() => this.playersManager.isInitialized && global::Game.OffensiveQB.iAnimQuerries.IsQBReadyForSnap();

  public bool IsSnapAllowed()
  {
    double oneYard1 = (double) Field.ONE_YARD;
    double oneYard2 = (double) Field.ONE_YARD;
    bool flag = true;
    if (ProEra.Game.PlayState.IsRunOrPass)
      flag = this.IsQBReadyForSnap();
    int numberOfSetPlayers = this.GetNumberOfSetPlayers();
    if (!(this.allowSnap & flag) || numberOfSetPlayers != 11)
      return false;
    return global::Game.BS_InCentersHandsBeforeSnap || ProEra.Game.PlayState.IsKickoff;
  }

  private int CheckOffPlayersSet()
  {
    int num = 0;
    for (int index = 0; index < 11; ++index)
    {
      if (global::Game.IsPlayerOneOnOffense)
      {
        if (this.playersManager.curUserScriptRef[index].animatorCommunicator.atPlayPosition)
          ++num;
      }
      else if (this.playersManager.curCompScriptRef[index].animatorCommunicator.atPlayPosition)
        ++num;
    }
    return num;
  }

  private int CheckDefPlayersSet()
  {
    int num = 0;
    for (int index = 0; index < 11; ++index)
    {
      if (global::Game.IsPlayerOneOnDefense)
      {
        if (this.playersManager.curUserScriptRef[index].animatorCommunicator.atPlayPosition)
          ++num;
      }
      else if (this.playersManager.curCompScriptRef[index].animatorCommunicator.atPlayPosition)
        ++num;
    }
    return num;
  }

  public void UpdateUserPlayerPosition()
  {
    System.Action qbPositionChange = this.OnQBPositionChange;
    if (qbPositionChange == null)
      return;
    qbPositionChange();
  }

  private void CheckDebugInput()
  {
    if (Input.GetKeyDown(KeyCode.L))
      this.timeManager.DebugEndQuarter();
    if (Input.GetKeyDown(KeyCode.H))
      this.timeManager.SetRunGameClock(true);
    if (Input.GetKeyDown(KeyCode.U))
      this.AddScore(1);
    else if (Input.GetKeyDown(KeyCode.I))
      this.AddScore(-1);
    if (Input.GetKey(KeyCode.J))
      this.timeManager.AddToGameClock(1.5f);
    if (!Input.GetKey(KeyCode.K))
      return;
    this.timeManager.AddToPlayClock(1.5f);
  }

  public static float ballOn => ProEra.Game.MatchState.BallOn.Value;

  public static float firstDown => ProEra.Game.MatchState.FirstDown.Value;

  public static bool turnover
  {
    get => ProEra.Game.MatchState.Turnover.Value;
    set => ProEra.Game.MatchState.Turnover.Value = value;
  }

  public static bool runningPat => (bool) ProEra.Game.MatchState.RunningPat;

  public static int gameLength => ProEra.Game.MatchState.GameLength.Value;

  public static int down => ProEra.Game.MatchState.Down.Value;

  private void SetAiTimingStoreReference()
  {
    if ((UnityEngine.Object) this._aiTimingStore == (UnityEngine.Object) null)
      this._aiTimingStore = MatchManager.instance.AITimeStore;
    if ((UnityEngine.Object) this._aiTimingStore != (UnityEngine.Object) null)
    {
      this._aiTimingInterval = this._aiTimingStore.GetCurrentPlatformInterval();
      this._halfAIUpdatedPerFrame = this._aiTimingStore.GetAIUpdatedPerFrame() / 2;
    }
    else
      Console.Error.WriteLine("Cannot get current platform interval because _aiTimingStore is still null");
  }

  public void SaveMatchStateBeforePlay() => this.beforePlay = new SavedMatchState(this.savedLineOfScrim, ProEra.Game.MatchState.FirstDown.Value, this.ballHashPosition, ProEra.Game.MatchState.Down.Value, PersistentData.GetOffensiveTeamIndex(), PersistentData.GetDefensiveTeamIndex(), ProEra.Game.MatchState.GameLength.Value, ProEra.Game.MatchState.Stats.User.Score, ProEra.Game.MatchState.Stats.Comp.Score, this.timeManager.IsGameClockRunning(), ProEra.Game.MatchState.RunningPat.Value);

  public void SaveMatchStateAfterPlay() => this.afterPlay = new SavedMatchState(ProEra.Game.MatchState.BallOn.Value, ProEra.Game.MatchState.FirstDown.Value, this.ballHashPosition, ProEra.Game.MatchState.Down.Value, PersistentData.GetOffensiveTeamIndex(), PersistentData.GetDefensiveTeamIndex(), ProEra.Game.MatchState.GameLength.Value, ProEra.Game.MatchState.Stats.User.Score, ProEra.Game.MatchState.Stats.Comp.Score, this.timeManager.IsGameClockRunning(), ProEra.Game.MatchState.RunningPat.Value);

  public void RestoreMatchStateAfterPenalty()
  {
    this.SetBallHashPosition(this.beforePlay.Hash);
    ProEra.Game.MatchState.Stats.User.Score = this.beforePlay.PlayerScore;
    ProEra.Game.MatchState.Stats.Comp.Score = this.beforePlay.CompScore;
    if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetPenaltyType() == PenaltyType.DelayOfGame)
      this.timeManager.SetRunGameClock(this.timeManager.WasGameClockRunningBeforeBeingSet);
    else if (this.timeManager.IsSecondQuarter() && this.timeManager.GetTotalSecondsRemaining() < 120)
      this.timeManager.SetRunGameClock(false);
    else if ((this.timeManager.IsFourthQuarter() || this.timeManager.IsInOvertime()) && this.timeManager.GetTotalSecondsRemaining() < 300)
      this.timeManager.SetRunGameClock(false);
    else
      this.timeManager.SetRunGameClock(true);
    ProEra.Game.MatchState.RunningPat.Value = this.beforePlay.RunningPAT;
    if ((bool) ProEra.Game.MatchState.Turnover && this.beforePlay.TeamOnOffense == PersistentData.GetUserTeamIndex())
    {
      this.SetBallOn(this.beforePlay.YardLine);
      ProEra.Game.MatchState.FirstDown.Value = this.beforePlay.FirstDownLine;
      this.SetCurrentMatchState(EMatchState.UserOnOffense);
      PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Offense);
    }
    else
    {
      if (!(bool) ProEra.Game.MatchState.Turnover || this.beforePlay.TeamOnOffense != PersistentData.GetCompTeamIndex())
        return;
      this.SetBallOn(this.beforePlay.YardLine);
      ProEra.Game.MatchState.FirstDown.Value = this.beforePlay.FirstDownLine;
      this.SetCurrentMatchState(EMatchState.UserOnDefense);
    }
  }

  public void ApplyPenalty()
  {
    Penalty penaltyOnPlay = SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay();
    if ((bool) ProEra.Game.MatchState.Turnover && penaltyOnPlay.GetTeamIndex() == PersistentData.GetDefensiveTeamIndex())
      ProEra.Game.MatchState.Turnover.Value = false;
    switch (penaltyOnPlay.GetPenaltyDownResult())
    {
      case PenaltyDownResult.LossOfDown:
        ++ProEra.Game.MatchState.Down.Value;
        break;
      case PenaltyDownResult.FirstDown:
        ProEra.Game.MatchState.Down.Value = 1;
        break;
    }
    if (penaltyOnPlay.GetPenaltyType() == PenaltyType.Facemask)
      this.MoveBall(this.FieldManager.tempBallPos, penaltyOnPlay);
    else if (penaltyOnPlay.GetPenaltyType() == PenaltyType.PassInterference && penaltyOnPlay.GetOffenseOrDefense() == "Defense")
      this.MoveBall(this.playersManager.ballLandingSpot.position.z, penaltyOnPlay);
    else
      this.MoveBall(this.savedLineOfScrim, penaltyOnPlay);
    if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.IsAgainstPlayer1())
    {
      ++this.playManager.userTeamCurrentPlayStats.players[penaltyOnPlay.GetPlayerIndex()].Penalties;
      this.playManager.userTeamCurrentPlayStats.players[penaltyOnPlay.GetPlayerIndex()].PenaltyYards += Mathf.Abs(penaltyOnPlay.GetPenaltyYards());
      ++ProEra.Game.MatchState.Stats.User.Penalties;
      ProEra.Game.MatchState.Stats.User.PenaltyYards += Mathf.Abs(penaltyOnPlay.GetPenaltyYards());
    }
    else
    {
      if (!SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.IsAgainstPlayer2())
        return;
      ++this.playManager.compTeamCurrentPlayStats.players[penaltyOnPlay.GetPlayerIndex()].Penalties;
      this.playManager.compTeamCurrentPlayStats.players[penaltyOnPlay.GetPlayerIndex()].PenaltyYards += Mathf.Abs(penaltyOnPlay.GetPenaltyYards());
      ++ProEra.Game.MatchState.Stats.Comp.Penalties;
      ProEra.Game.MatchState.Stats.Comp.PenaltyYards += Mathf.Abs(penaltyOnPlay.GetPenaltyYards());
    }
  }

  public float FindAcceptPenaltyFieldPosition(int penaltyYards)
  {
    float num1 = (float) penaltyYards * Field.ONE_YARD * (float) global::Game.OffensiveFieldDirection;
    Penalty penaltyOnPlay = SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay();
    float num2 = penaltyOnPlay.GetPenaltyType() != PenaltyType.Facemask ? (penaltyOnPlay.GetPenaltyType() != PenaltyType.PassInterference || !(penaltyOnPlay.GetOffenseOrDefense() == "Defense") ? this.beforePlay.YardLine : MatchManager.instance.playersManager.ballLandingSpot.position.z) : this.FieldManager.tempBallPos;
    float penaltyFieldPosition = num2 + num1;
    if (Field.FurtherDownfield(Field.DEFENSIVE_GOAL_LINE, penaltyFieldPosition))
    {
      float num3 = Mathf.Abs(Field.DEFENSIVE_GOAL_LINE - num2) / 2f;
      penaltyFieldPosition = num2 - num3 * (float) global::Game.OffensiveFieldDirection;
    }
    else if (Field.FurtherDownfield(penaltyFieldPosition, Field.OFFENSIVE_GOAL_LINE))
    {
      float num4 = Mathf.Abs(Field.OFFENSIVE_GOAL_LINE - num2) / 2f;
      penaltyFieldPosition = num2 + num4 * (float) global::Game.OffensiveFieldDirection;
    }
    return penaltyFieldPosition;
  }

  public float FindDeclinePenaltyFieldPosition()
  {
    if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetPenaltyTime() == PenaltyTime.PrePlay)
      return this.savedLineOfScrim;
    float penaltyFieldPosition = this.savedLineOfScrim;
    if (global::Game.PET_IsTackle || global::Game.PET_IsOOB)
      penaltyFieldPosition = (bool) ProEra.Game.MatchState.Turnover ? (!global::Game.OffenseGoingNorth ? Mathf.Max(this.ballManager.trans.position.z, this.FieldManager.tempBallPos) : Mathf.Min(this.ballManager.trans.position.z, this.FieldManager.tempBallPos)) : (!global::Game.OffenseGoingNorth ? Mathf.Min(this.ballManager.trans.position.z, this.FieldManager.tempBallPos) : Mathf.Max(this.ballManager.trans.position.z, this.FieldManager.tempBallPos));
    return penaltyFieldPosition;
  }

  public string GetAcceptPenaltyDownAndDistance(Penalty penalty, int newDown)
  {
    int penaltyYards = penalty.GetPenaltyYards();
    string str1 = "";
    float penaltyFieldPosition = this.FindAcceptPenaltyFieldPosition(penaltyYards);
    if ((bool) ProEra.Game.MatchState.RunningPat)
      return "Kick PAT from OPP " + Field.GetYardLineByFieldLocation(penaltyFieldPosition).ToString();
    string str2;
    if (penalty.GetPenaltyDownResult() == PenaltyDownResult.FirstDown)
    {
      newDown = 1;
      str2 = "10";
    }
    else if (Field.FurtherDownfield(penaltyFieldPosition, MatchManager.firstDown))
    {
      newDown = 1;
      str2 = "10";
    }
    else
      str2 = (double) Mathf.Abs(ProEra.Game.MatchState.FirstDown.Value - penaltyFieldPosition) >= (double) Field.ONE_YARD ? Mathf.RoundToInt(Mathf.Abs(ProEra.Game.MatchState.FirstDown.Value - penaltyFieldPosition) / Field.ONE_YARD).ToString() : "IN";
    if (Field.FurtherDownfield(penaltyFieldPosition, Field.OPPONENT_TEN_YARD_LINE))
      str2 = "GOAL";
    if (Field.FurtherDownfield(penaltyFieldPosition, Field.MIDFIELD))
      str1 = !global::Game.IsPlayerOneOnOffense ? PersistentData.GetUserTeam().GetAbbreviation() : PersistentData.GetCompTeam().GetAbbreviation();
    else if (Field.FurtherDownfield(Field.MIDFIELD, penaltyFieldPosition))
      str1 = !global::Game.IsPlayerOneOnOffense ? PersistentData.GetCompTeam().GetAbbreviation() : PersistentData.GetUserTeam().GetAbbreviation();
    int lineByFieldLocation = Field.GetYardLineByFieldLocation(penaltyFieldPosition);
    if (ProEra.Game.PlayState.IsKickoff)
    {
      SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.downIfAccept = 1;
      return "1st & 10 from " + (!global::Game.IsPlayerOneOnOffense ? PersistentData.GetUserTeam().GetAbbreviation() : PersistentData.GetCompTeam().GetAbbreviation()) + " 35";
    }
    string penaltyDownAndDistance;
    switch (newDown)
    {
      case 1:
        penaltyDownAndDistance = "1st";
        break;
      case 2:
        penaltyDownAndDistance = "2nd";
        break;
      case 3:
        penaltyDownAndDistance = "3rd";
        break;
      case 4:
        penaltyDownAndDistance = "4th";
        break;
      case 5:
        penaltyDownAndDistance = "Turnover on Downs";
        break;
      default:
        penaltyDownAndDistance = "???";
        Debug.Log((object) ("Unknown down = '" + newDown.ToString() + "'"));
        break;
    }
    if (newDown <= 4)
      penaltyDownAndDistance = penaltyDownAndDistance + " & " + str2 + " from " + str1 + " " + lineByFieldLocation.ToString();
    SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.downIfAccept = newDown;
    return penaltyDownAndDistance;
  }

  public string GetDeclinePenaltyDownAndDistance()
  {
    float penaltyFieldPosition = this.FindDeclinePenaltyFieldPosition();
    int num = this.beforePlay.Down;
    string penaltyDownAndDistance = "";
    string str1 = "";
    string str2 = "";
    if ((bool) ProEra.Game.MatchState.RunningPat)
      return "Kick PAT from OPP " + Field.GetYardLineByFieldLocation(penaltyFieldPosition).ToString();
    if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetPenaltyTime() == PenaltyTime.PrePlay)
      num = this.beforePlay.Down;
    else if (global::Game.PET_IsIncomplete)
    {
      ++num;
    }
    else
    {
      if (global::Game.PET_IsMadeFG)
        return "Field Goal is Good";
      if (global::Game.PET_IsTouchdown)
        return "Touchdown";
      if (global::Game.PET_IsSafety || (double) this.ballManager.trans.position.z < 0.0 && !(bool) ProEra.Game.MatchState.Turnover)
        return "Safety";
      if (global::Game.PET_IsTackle || global::Game.PET_IsOOB)
      {
        ++num;
        if ((bool) ProEra.Game.MatchState.Turnover && !ProEra.Game.PlayState.IsKickoff)
          return "Turnover";
      }
    }
    if (Field.FurtherDownfield(penaltyFieldPosition, ProEra.Game.MatchState.FirstDown.Value))
    {
      num = 1;
      str1 = "10";
    }
    if (Field.FurtherDownfield(penaltyFieldPosition, Field.OPPONENT_TEN_YARD_LINE))
      str1 = "GOAL";
    else if (Field.FurtherDownfield(ProEra.Game.MatchState.FirstDown.Value, penaltyFieldPosition) && (double) Mathf.Abs(ProEra.Game.MatchState.FirstDown.Value - penaltyFieldPosition) < (double) Field.ONE_YARD)
      str1 = "IN";
    else if (Field.FurtherDownfield(ProEra.Game.MatchState.FirstDown.Value, penaltyFieldPosition))
      str1 = Mathf.RoundToInt(Mathf.Abs(ProEra.Game.MatchState.FirstDown.Value - penaltyFieldPosition) / Field.ONE_YARD).ToString();
    switch (num)
    {
      case 1:
        penaltyDownAndDistance = "1st";
        break;
      case 2:
        penaltyDownAndDistance = "2nd";
        break;
      case 3:
        penaltyDownAndDistance = "3rd";
        break;
      case 4:
        penaltyDownAndDistance = "4th";
        break;
      case 5:
        penaltyDownAndDistance = "Turnover on downs";
        break;
    }
    if (Field.FurtherDownfield(penaltyFieldPosition, Field.MIDFIELD))
      str2 = !global::Game.IsPlayerOneOnOffense ? PersistentData.GetUserTeam().GetAbbreviation() : PersistentData.GetCompTeam().GetAbbreviation();
    else if (Field.FurtherDownfield(Field.MIDFIELD, penaltyFieldPosition))
      str2 = !global::Game.IsPlayerOneOnOffense ? PersistentData.GetCompTeam().GetAbbreviation() : PersistentData.GetUserTeam().GetAbbreviation();
    int lineByFieldLocation = Field.GetYardLineByFieldLocation(penaltyFieldPosition);
    if (ProEra.Game.PlayState.IsKickoff)
      return "1st & 10 from " + (global::Game.IsPlayerOneOnOffense ? PersistentData.GetCompTeam().GetAbbreviation() : PersistentData.GetUserTeam().GetAbbreviation()) + " " + Field.GetYardLineByFieldLocation(SingletonBehaviour<FieldManager, MonoBehaviour>.instance.tempBallPos).ToString();
    if (num <= 4)
      penaltyDownAndDistance = penaltyDownAndDistance + " & " + str1.ToLower() + " from " + str2 + " " + lineByFieldLocation.ToString();
    SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.downIfDecline = num;
    return penaltyDownAndDistance;
  }

  public void MoveBall(float moveFrom, Penalty penalty)
  {
    float num1 = (float) penalty.GetPenaltyYards() * Field.ONE_YARD * (float) global::Game.OffensiveFieldDirection;
    float num2 = moveFrom + num1;
    if (Field.FurtherDownfield(Field.DEFENSIVE_GOAL_LINE, num2))
    {
      float num3 = Mathf.Abs(Field.DEFENSIVE_GOAL_LINE - moveFrom) / 2f;
      this.SetBallOn(moveFrom - num3 * (float) global::Game.OffensiveFieldDirection);
    }
    else if (Field.FurtherDownfield(num2, Field.OFFENSIVE_GOAL_LINE))
    {
      float num4 = Mathf.Abs(Field.OFFENSIVE_GOAL_LINE - moveFrom) / 2f;
      this.SetBallOn(moveFrom + num4 * (float) global::Game.OffensiveFieldDirection);
    }
    else
      this.SetBallOn(num2);
    this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
    this.ballManager.SetPosition(new Vector3(this.ballHashPosition, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value - Ball.BALL_LENGTH * (float) global::Game.OffensiveFieldDirection));
    if (Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, ProEra.Game.MatchState.FirstDown.Value) || penalty.GetPenaltyDownResult() == PenaltyDownResult.FirstDown)
    {
      ProEra.Game.MatchState.Down.Value = 1;
      ProEra.Game.MatchState.FirstDown.Value = ProEra.Game.MatchState.BallOn.Value + (float) (10.0 * ((double) Field.ONE_YARD * (double) global::Game.OffensiveFieldDirection));
    }
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetFirstDownLine();
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
  }

  public void ChooseNewPlay()
  {
    this.DisallowSnap();
    this.playManager.playSelectedDef = false;
    this.playManager.playSelectedOff = false;
    this.checkForEndOfQuarter = true;
  }

  public bool IsSimulating
  {
    get => this._isSimulating;
    set => this._isSimulating = value;
  }

  private void Awake()
  {
    MatchManager.instance = this;
    this._renderedSimPositions = new List<Vector3>();
  }

  public void SimulatePlay()
  {
    this.IsSimulating = false;
    if (!(bool) this.ShouldSimulate)
      return;
    PlayDataOff savedOffPlay = this.playManager.savedOffPlay;
    PlayDataDef savedDefPlay = this.playManager.savedDefPlay;
    this.checkForEndOfQuarter = false;
    PlayType playType = savedOffPlay.GetPlayType();
    this._renderedSimPositions.Clear();
    Debug.Log((object) ("#SidelineSim playType[" + playType.ToString() + "]"));
    SuperSimTuningConfig superSimTuningConfig = global::Game.SuperSimTuningConfig;
    if (global::Game.IsQBSpike)
    {
      this.AddPlaySetupTime();
      this.RecordSimulationResult(PlayEndType.Incomplete, superSimTuningConfig.SpikePlayTime, new Vector3(this.ballHashPosition, 0.0f, (float) ProEra.Game.MatchState.BallOn));
    }
    else
    {
      switch (playType)
      {
        case PlayType.Run:
          this.AddPlaySetupTime();
          Debug.Log((object) "#SidelineSim Simulate run");
          this.IsSimulating = true;
          this.playManager.PlayActive = true;
          if (ProEra.Game.MatchState.Down.Value == 3)
          {
            if (global::Game.IsPlayerOneOnOffense)
              ++ProEra.Game.MatchState.Stats.User.ThirdDownAtt;
            else
              ++ProEra.Game.MatchState.Stats.Comp.ThirdDownAtt;
          }
          this._renderedSimPositions.Add(this.ballManager.trans.position);
          PlayerAI receiver1 = (PlayerAI) null;
          for (int index = 6; index < 11; ++index)
          {
            PlayerAI offensivePlayer = global::Game.OffensivePlayers[index];
            if (offensivePlayer.blockType == BlockType.None && index == global::Game.PrimaryReceiverIndex)
            {
              receiver1 = offensivePlayer;
              break;
            }
          }
          float num1 = 1f;
          this.timeManager.AddToGameClock(num1);
          if ((UnityEngine.Object) receiver1 != (UnityEngine.Object) null)
          {
            MatchManager.instance.playersManager.SetBallHolder(receiver1.gameObject, false);
            ++this.playManager.compTeamCurrentPlayStats.players[this.playManager.handOffTarget.indexOnTeam].RushAttempts;
            this.DoRunChecks(num1, receiver1, playType, receiver1.GetPlayStartPosition(), false);
            break;
          }
          Debug.Log((object) "#SidelineSim Simulating QB Kneel");
          PlayerAI offensivePlayer1 = global::Game.OffensivePlayers[5];
          PlayerAI defensivePlayer = global::Game.DefensivePlayers[2];
          PEGameplayEventManager.RecordTackleEvent(this.timeManager.GetGameClockTimer(), offensivePlayer1.trans.position, defensivePlayer, offensivePlayer1);
          this.RecordSimulationResult(PlayEndType.Tackle, UnityEngine.Random.Range(1f, 2f), offensivePlayer1.trans.position);
          break;
        case PlayType.Pass:
          this.AddPlaySetupTime();
          this.IsSimulating = true;
          this.playManager.PlayActive = true;
          int num2 = global::Game.IsPlayerOneOnDefense ? PersistentData.GetUserTeam().GetTeamRating_DEF() : PersistentData.GetCompTeam().GetTeamRating_DEF();
          int num3 = global::Game.IsPlayerOneOnOffense ? PersistentData.GetUserTeam().GetTeamRating_OFF() : PersistentData.GetCompTeam().GetTeamRating_OFF();
          float sackChance = superSimTuningConfig.SackChance;
          int num4 = num3;
          float num5 = (float) (num2 - num4) * (1f / 1000f);
          if ((double) UnityEngine.Random.value < (double) sackChance + (double) num5)
          {
            float num6 = UnityEngine.Random.Range(superSimTuningConfig.MinSackTime, superSimTuningConfig.MaxThrowTime);
            this.timeManager.AddToGameClock(num6);
            Debug.Log((object) "#SidelineSim Simulating QB Sack");
            PlayerAI offensiveQb = global::Game.OffensiveQB;
            PlayerAI tackler = (PlayerAI) null;
            for (int index = 0; index < global::Game.DefensivePlayers.Count; ++index)
            {
              if (global::Game.DefensivePlayers[index].GetCurrentAssignment() is BlitzAssignment)
              {
                float num7;
                switch (global::Game.DefensivePlayers[index].playerPosition)
                {
                  case Position.DL:
                  case Position.DE:
                    num7 = 0.25f;
                    break;
                  case Position.LB:
                  case Position.OLB:
                  case Position.ILB:
                  case Position.MLB:
                    num7 = 0.15f;
                    break;
                  default:
                    num7 = 0.1f;
                    break;
                }
                if ((double) UnityEngine.Random.value < (double) num7)
                {
                  tackler = global::Game.DefensivePlayers[index];
                  break;
                }
              }
            }
            if ((UnityEngine.Object) tackler == (UnityEngine.Object) null)
              tackler = global::Game.DefensivePlayers[0];
            PEGameplayEventManager.RecordTackleEvent(this.timeManager.GetGameClockTimer(), offensiveQb.trans.position, tackler, offensiveQb);
            this.RecordSimulationResult(PlayEndType.Tackle, num6, offensiveQb.trans.position);
            break;
          }
          MatchManager.instance.playersManager.ballWasThrownOrKicked = true;
          if (ProEra.Game.MatchState.Down.Value == 3)
          {
            if (global::Game.IsPlayerOneOnOffense)
              ++ProEra.Game.MatchState.Stats.User.ThirdDownAtt;
            else
              ++ProEra.Game.MatchState.Stats.Comp.ThirdDownAtt;
          }
          this._renderedSimPositions.Add(this.ballManager.trans.position);
          float num8 = UnityEngine.Random.Range(superSimTuningConfig.MinThrowTime, superSimTuningConfig.MaxThrowTime);
          if ((double) num8 > (double) superSimTuningConfig.MinThrowAwayTime && (double) UnityEngine.Random.value < (double) superSimTuningConfig.ThrowAwayChance)
          {
            Debug.Log((object) "#SidelineSim QB is Throwing ball away");
            this.RecordSimulationResult(PlayEndType.Incomplete, num8, new Vector3(this.ballHashPosition, 0.0f, (float) ProEra.Game.MatchState.BallOn));
            break;
          }
          List<int> intList = new List<int>();
          int index1 = -1;
          for (int index2 = 6; index2 < 11; ++index2)
          {
            if (global::Game.OffensivePlayers[index2].blockType == BlockType.None)
            {
              if (index2 == global::Game.PrimaryReceiverIndex)
              {
                Debug.Log((object) ("#SidelineSim Found main rec: " + index2.ToString()));
                index1 = index2;
              }
              else
                intList.Add(index2);
            }
          }
          float num9 = (float) (1.0 / (double) (intList.Count + 1) + 0.30000001192092896);
          float num10 = (1f - num9) / (float) intList.Count;
          float num11 = UnityEngine.Random.value;
          PlayerAI offensivePlayer2;
          int index3;
          if ((double) num11 <= (double) num9 && index1 >= 0)
          {
            Debug.Log((object) "#SidelineSim Selected main receiver");
            offensivePlayer2 = global::Game.OffensivePlayers[index1];
            index3 = index1;
          }
          else
          {
            index3 = Mathf.Min(Mathf.FloorToInt(num11 * (1f - num9) / num10), intList.Count - 1);
            Debug.Log((object) ("#SidelineSim Selected other receiver: index[" + index3.ToString() + "] receivers.Count[" + intList.Count.ToString() + "]"));
            offensivePlayer2 = global::Game.OffensivePlayers[intList[index3]];
          }
          if (index3 != index1)
            Debug.Log((object) ("#SidelineSim designatedReceiver[" + offensivePlayer2.playerName + "] mainRec[" + index3.ToString() + "] receivers[index][" + intList[index3].ToString() + "]"));
          else
            Debug.Log((object) ("#SidelineSim designatedReceiver[" + offensivePlayer2.playerName + "] mainRec[" + index1.ToString() + "]"));
          float[] path = offensivePlayer2.Path;
          float speed = offensivePlayer2.speed;
          Vector3 playStartPosition = offensivePlayer2.GetPlayStartPosition();
          Vector3 positionAlongPath = this.GetPositionAlongPath(path, playStartPosition, num8, speed, out float _, out bool _);
          PlayerAI receiver2 = (PlayerAI) null;
          for (int index4 = 0; index4 < global::Game.DefensivePlayers.Count; ++index4)
          {
            if ((UnityEngine.Object) global::Game.DefensivePlayers[index4].ManCoverTarget == (UnityEngine.Object) offensivePlayer2)
            {
              receiver2 = global::Game.DefensivePlayers[index4];
              break;
            }
            if (global::Game.DefensivePlayers[index4].GetCurrentAssignment() is ZoneDefenseAssignment currentAssignment && currentAssignment.IsReceiverInDefendersZone(positionAlongPath))
            {
              receiver2 = global::Game.DefensivePlayers[index4];
              break;
            }
          }
          PlayerAI offensiveQb1 = global::Game.OffensiveQB;
          if (global::Game.IsPlayerOneOnOffense)
            ++this.playManager.userTeamCurrentPlayStats.players[offensiveQb1.indexOnTeam].QBAttempts;
          else
            ++this.playManager.compTeamCurrentPlayStats.players[offensiveQb1.indexOnTeam].QBAttempts;
          float passDistance = Vector3.Distance(offensiveQb1.GetPlayStartPosition(), positionAlongPath);
          int throwingAccuracy = offensiveQb1.throwingAccuracy;
          float goodPassChance = superSimTuningConfig.GetGoodPassChance(passDistance, (float) throwingAccuracy);
          float num12 = UnityEngine.Random.value;
          bool flag = (double) num12 < (double) goodPassChance;
          Debug.Log((object) ("#SidelineSim Throw is [" + (flag ? "Good" : "Bad") + "] Accuracy chance =" + goodPassChance.ToString() + "Rand = " + num12.ToString()));
          bool interception = false;
          if (!flag && (UnityEngine.Object) receiver2 != (UnityEngine.Object) null)
          {
            float interceptionRating = (float) (int) ((double) receiver2.catching * 0.699999988079071 + (double) receiver2.awareness * 0.30000001192092896);
            float interceptionChance = superSimTuningConfig.GetInterceptionChance(interceptionRating);
            float num13 = UnityEngine.Random.value;
            interception = (double) num13 <= (double) interceptionChance;
            Debug.Log((object) ("#SidelineSim Interception rating is [" + interceptionRating.ToString() + "] Interception chance =" + interceptionChance.ToString() + "Rand = " + num13.ToString()));
          }
          this.timeManager.AddToGameClock(num8);
          PEGameplayEventManager.RecordBallThrownEvent(this.timeManager.GetGameClockTimer(), offensiveQb1.transform.position, offensiveQb1, offensivePlayer2, false);
          this.timeManager.AddToGameClock(num8 / 2f);
          if (!flag && !interception)
          {
            this.RecordSimulationResult(PlayEndType.Incomplete, num8, new Vector3(this.ballHashPosition, 0.0f, (float) ProEra.Game.MatchState.BallOn));
            break;
          }
          Debug.Log((object) ("#SidelineSim RecordSimulationResult: recPos[" + positionAlongPath.ToString() + "]"));
          if (interception)
          {
            ProEra.Game.MatchState.Turnover.Value = true;
            MatchManager.instance.playersManager.SetBallHolder(receiver2.gameObject, true);
            ++MatchManager.instance.playManager.userTeamCurrentPlayStats.players[receiver2.indexOnTeam].Interceptions;
            ++MatchManager.instance.playManager.compTeamCurrentPlayStats.players[offensiveQb1.indexOnTeam].QBInts;
            ++ProEra.Game.MatchState.Stats.User.Interceptions;
            PEGameplayEventManager.RecordBallCaughtEvent(this.timeManager.GetGameClockTimer(), positionAlongPath, receiver2, interception);
          }
          else
          {
            MatchManager.instance.playersManager.SetBallHolder(offensivePlayer2.gameObject, false);
            PEGameplayEventManager.RecordBallCaughtEvent(this.timeManager.GetGameClockTimer(), positionAlongPath, offensivePlayer2, interception);
          }
          this._renderedSimPositions.Add(this._renderedSimPositions[0] + (positionAlongPath - this._renderedSimPositions[0]) / 2f + new Vector3(0.0f, 3f, 0.0f));
          this._renderedSimPositions.Add(positionAlongPath);
          this.DoRunChecks(num8, interception ? receiver2 : offensivePlayer2, playType, positionAlongPath, true);
          break;
        case PlayType.FG:
          this.AddPlaySetupTime();
          this.IsSimulating = true;
          this.playManager.PlayActive = true;
          MatchManager.instance.playersManager.ballWasThrownOrKicked = true;
          List<PlayerAI> offensivePlayers = global::Game.OffensivePlayers;
          PEGameplayEventManager.RecordBallKickedEvent(this.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, offensivePlayers[6], offensivePlayers[2], offensivePlayers[5]);
          Vector3 kickLandingPos = FieldGoalUtil.GetKickLandingPos((float) offensivePlayers[6].GetKickingPower(), (float) offensivePlayers[6].kickingAccuracy, global::Game.FieldGoalConfig);
          float x = kickLandingPos.x;
          float z = kickLandingPos.z;
          if (MathUtils.LineSegmentsIntersect(SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position.x, SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position.z, kickLandingPos.x, kickLandingPos.z, -Field.FG_HALF_POST_WIDTH, Field.OFFENSIVE_BACK_OF_ENDZONE, Field.FG_HALF_POST_WIDTH, Field.OFFENSIVE_BACK_OF_ENDZONE))
          {
            if ((bool) ProEra.Game.MatchState.RunningPat)
              this.AddScore(1);
            else
              this.AddScore(3);
            this.RecordSimulationResult(PlayEndType.MadeFG, 3f, new Vector3(x, 0.0f, z));
            break;
          }
          this.RecordSimulationResult(PlayEndType.MissedFG, 3f, new Vector3(x, 0.0f, z));
          break;
      }
    }
  }

  private bool CheckSimulatedTimeOut()
  {
    if (this.playManager.playEndType == PlayEndType.Tackle)
    {
      if (SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance.ShouldAICallTimeout(Team.Computer, true))
      {
        --GameTimeoutState.CompTimeouts.Value;
        return true;
      }
      if (SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance.ShouldAICallTimeout(Team.Player1, true))
      {
        --GameTimeoutState.UserTimeouts.Value;
        return true;
      }
    }
    return false;
  }

  private void AddPlaySetupTime()
  {
    if (this.CheckSimulatedTimeOut())
      return;
    this.timeManager.AddToGameClock(UnityEngine.Random.Range(0.0f, 30f));
  }

  private void RecordSimulationResult(PlayEndType type, float throwTime, Vector3 ballPosition)
  {
    Debug.Log((object) ("#SidelineSim Result Down[" + ProEra.Game.MatchState.Down?.ToString() + "] LOS[" + ProEra.Game.MatchState.BallOn.Value.ToString() + "] Result[" + type.ToString() + "] ballPos[" + ballPosition.ToString() + "] firstDown[" + ProEra.Game.MatchState.FirstDown?.ToString() + "]"));
    Debug.Log((object) ("#SidelineSim Result BallCarrier[" + ((object) MatchManager.instance.playersManager.ballHolderScript)?.ToString() + "]"));
    this._renderedSimPositions.Add(ballPosition);
    this.ballManager.trans.position = ballPosition;
    this.IsSimulating = true;
    if (type != PlayEndType.MadeFG && type != PlayEndType.MissedFG)
    {
      if (this.CheckForTouchdown() || !global::Game.IsPlayActive)
        return;
      this.EndPlay(type);
    }
    else
      this.EndPlay(type);
  }

  private bool CheckForTackle(PlayerAI ballCarrier, PlayerAI tackler)
  {
    SuperSimTuningConfig superSimTuningConfig = global::Game.SuperSimTuningConfig;
    float recoveryCoefficient = ballCarrier.BrokenTackleRecoveryCoefficient;
    int tackleBreaking = ballCarrier.tackleBreaking;
    float num1 = MathUtils.MapRange((float) tackleBreaking, 0.0f, (float) (tackleBreaking + tackler.tackling), 0.0f, 1f);
    float num2 = UnityEngine.Random.value;
    Debug.Log((object) ("#SidelineSim CheckForTackle: rand[" + num2.ToString() + "] normalizedTackleBreakStat[" + num1.ToString() + "] carrierRecoveryCoefficient[" + recoveryCoefficient.ToString() + "]"));
    float num3 = Mathf.Clamp(num1 * 1f * recoveryCoefficient, 0.0f, superSimTuningConfig.MaxBreakTackleChance);
    int num4 = (double) num2 >= (double) num3 ? 1 : 0;
    if (num4 != 0)
      return num4 != 0;
    ballCarrier.OnBrokenTackleKeyMoment_BallCarrier();
    return num4 != 0;
  }

  private Vector3 GetPositionAlongPath(
    float[] path,
    Vector3 startPos,
    float runTime,
    float playerSpeed,
    out float timeRemaining,
    out bool finishedPath)
  {
    Vector3 positionAlongPath = startPos;
    finishedPath = false;
    timeRemaining = runTime;
    float num1 = (float) (global::Game.OffensiveFieldDirection * ((bool) ProEra.Game.MatchState.Turnover ? -1 : 1));
    int index = 1;
    while ((double) timeRemaining > 0.0)
    {
      if (index >= path.Length)
      {
        finishedPath = true;
        break;
      }
      float num2 = path[index];
      float num3 = path[index + 1];
      float num4 = path[index + 2];
      Vector3 a = positionAlongPath;
      Vector3 b = new Vector3(num2 * Field.ONE_YARD * num1 + this.ballHashPosition, 0.0f, num3 * Field.ONE_YARD * num1 + ProEra.Game.MatchState.BallOn.Value);
      string[] strArray = new string[5]
      {
        "#SidelineSim start[",
        null,
        null,
        null,
        null
      };
      Vector3 vector3 = a;
      strArray[1] = vector3.ToString();
      strArray[2] = "] end[";
      vector3 = b;
      strArray[3] = vector3.ToString();
      strArray[4] = "]";
      Debug.Log((object) string.Concat(strArray));
      float num5 = Vector3.Distance(a, b);
      float num6 = (playerSpeed + num4 * 0.1f) * AIUtil.SPEEDRATING_TO_VELFACTOR;
      float num7 = num5 / num6;
      Debug.Log((object) ("#SidelineSim dist[" + num5.ToString() + "] speed[" + num6.ToString() + "] path[i + 2][" + path[index + 2].ToString() + "]"));
      Debug.Log((object) ("#SidelineSim timeToRun[" + num7.ToString() + "] timeRemaining[" + timeRemaining.ToString() + "]"));
      if ((double) timeRemaining - (double) num7 > 0.0)
      {
        timeRemaining -= num7;
        positionAlongPath = b;
      }
      else
      {
        float t = timeRemaining / num7;
        positionAlongPath = Vector3.Lerp(a, b, t);
        timeRemaining = 0.0f;
      }
      index += 3;
    }
    return positionAlongPath;
  }

  private void DoRunChecks(
    float startTime,
    PlayerAI receiver,
    PlayType type,
    Vector3 pos,
    bool checkForTackleFirst)
  {
    Debug.Log((object) "#SidelineSim DoRunChecks");
    Vector3 vector3 = pos;
    bool flag1 = true;
    PlayerAI[] playerAiArray = new PlayerAI[global::Game.DefensivePlayers.Count];
    if (ProEra.Game.MatchState.Turnover.Value)
      global::Game.OffensivePlayers.CopyTo(playerAiArray);
    else
      global::Game.DefensivePlayers.CopyTo(playerAiArray);
    List<PlayerAI> playerAiList = new List<PlayerAI>((IEnumerable<PlayerAI>) playerAiArray);
    float speed = receiver.speed;
    bool finishedPath = type != PlayType.Run;
    bool flag2 = !checkForTackleFirst;
    float[] path = receiver.Path;
    Debug.Log((object) ("#SidelineSim path.Length[" + path.Length.ToString() + "]"));
    float time = 1f;
    while ((double) startTime < 30.0)
    {
      float timeRemaining = time;
      if (type == PlayType.Run && !finishedPath)
      {
        vector3 = this.GetPositionAlongPath(path, pos, startTime, speed, out timeRemaining, out finishedPath);
        if (flag1)
          PEGameplayEventManager.RecordBallHandoffEvent(this.timeManager.GetGameClockTimer(), vector3, receiver);
      }
      if (finishedPath & flag2)
      {
        Vector3 a = vector3;
        Vector3 b = new Vector3(pos.x, 0.0f, ProEra.Game.MatchState.Turnover.Value ? Field.DEFENSIVE_BACK_OF_ENDZONE : Field.OFFENSIVE_BACK_OF_ENDZONE);
        Debug.Log((object) ("#SidelineSim start[" + a.ToString() + "] end[" + b.ToString() + "]"));
        float num1 = Vector3.Distance(a, b);
        float num2 = speed * AIUtil.SPEEDRATING_TO_VELFACTOR;
        float num3 = num1 / num2;
        Debug.Log((object) ("#SidelineSim dist[" + num1.ToString() + "] speed[" + num2.ToString() + "]"));
        Debug.Log((object) ("#SidelineSim timeToRun[" + num3.ToString() + "] totalRunTime[" + timeRemaining.ToString() + "]"));
        if ((double) timeRemaining - (double) num3 > 0.0)
        {
          Vector3 ballPosition = b;
          this.RecordSimulationResult(PlayEndType.Touchdown, startTime, ballPosition);
          break;
        }
        float t = timeRemaining / num3;
        vector3 = Vector3.Lerp(a, b, t);
      }
      bool flag3 = false;
      float num4 = float.PositiveInfinity;
      PlayerAI tackler = (PlayerAI) null;
      for (int index = 0; index < playerAiList.Count; ++index)
      {
        float num5 = Vector3.Distance(playerAiList[index].GetPlayStartPosition(), vector3);
        if ((double) num5 < (double) num4)
        {
          num4 = num5;
          tackler = playerAiList[index];
        }
        if ((UnityEngine.Object) playerAiList[index].ManCoverTarget == (UnityEngine.Object) receiver)
        {
          flag3 = true;
          float num6 = num5 / (playerAiList[index].speed * AIUtil.SPEEDRATING_TO_VELFACTOR);
          Debug.Log((object) ("#SidelineSim Man defender: runTime[" + num6.ToString() + "] startTime[" + startTime.ToString() + "]"));
          if ((double) num6 <= (double) startTime)
          {
            if (this.CheckForTackle(receiver, playerAiList[index]))
            {
              PEGameplayEventManager.RecordTackleEvent(this.timeManager.GetGameClockTimer(), vector3, playerAiList[index], receiver);
              this.RecordSimulationResult(PlayEndType.Tackle, startTime, vector3);
              return;
            }
            playerAiList.RemoveAt(index);
            break;
          }
        }
        else if (playerAiList[index].GetCurrentAssignment() is ZoneDefenseAssignment currentAssignment && currentAssignment.IsReceiverInDefendersZone(vector3))
        {
          Debug.Log((object) "#SidelineSim Zone defender");
          flag3 = true;
          if (this.CheckForTackle(receiver, playerAiList[index]))
          {
            PEGameplayEventManager.RecordTackleEvent(this.timeManager.GetGameClockTimer(), vector3, playerAiList[index], receiver);
            this.RecordSimulationResult(PlayEndType.Tackle, startTime, vector3);
            return;
          }
          playerAiList.RemoveAt(index);
          break;
        }
      }
      bool flag4 = Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, vector3.z) && !(bool) ProEra.Game.MatchState.Turnover;
      if (!flag3 && (UnityEngine.Object) tackler != (UnityEngine.Object) null && !flag4)
      {
        float num7 = Vector3.Distance(tackler.GetPlayStartPosition(), vector3) / (tackler.speed * AIUtil.SPEEDRATING_TO_VELFACTOR);
        Debug.Log((object) ("#SidelineSim Closest defender: runTime[" + num7.ToString() + "] startTime[" + startTime.ToString() + "]"));
        if ((double) num7 <= (double) startTime)
        {
          if (this.CheckForTackle(receiver, tackler))
          {
            PEGameplayEventManager.RecordTackleEvent(this.timeManager.GetGameClockTimer(), vector3, tackler, receiver);
            this.RecordSimulationResult(PlayEndType.Tackle, startTime, vector3);
            break;
          }
          playerAiList.Remove(tackler);
        }
      }
      if (flag2)
      {
        startTime += time;
        this.timeManager.AddToGameClock(time);
      }
      flag1 = false;
      flag2 = true;
    }
  }

  internal void SetupGameFromGameScene()
  {
    if (!SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startedFromGameScene)
      return;
    PersistentData.SetGameMode(PersistentData.GameMode);
    TeamData team1 = TeamDataCache.GetTeam(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startFromGameScene_UserTeamIndex);
    TeamData team2 = TeamDataCache.GetTeam(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startFromGameScene_CompTeamIndex);
    PersistentData.SetUserTeam(team1);
    PersistentData.SetCompTeam(team2);
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceOffPlay)
    {
      Plays.self.SetOffensivePlaybookP1(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceOffensePlaybook);
      Plays.self.SetOffensivePlaybookP2(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceOffensePlaybook);
    }
    else
    {
      Plays.self.SetOffensivePlaybookP1(team1.GetOffensivePlaybook());
      Plays.self.SetOffensivePlaybookP2(team2.GetOffensivePlaybook());
    }
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceDefPlay)
    {
      Plays.self.SetDefensivePlaybookP1(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceDefensePlaybook);
      Plays.self.SetDefensivePlaybookP2(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioForceDefensePlaybook);
    }
    else
    {
      Plays.self.SetDefensivePlaybookP1(team1.GetDefensivePlaybook());
      Plays.self.SetDefensivePlaybookP2(team2.GetDefensivePlaybook());
    }
    PersistentData.difficulty = PersistentData.offDifficulty;
    this.SetBallOn((float) SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioLineOfScrimmageYardline);
    this.SetBallHashPosition(0.0f);
    this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
    this.checkedForInjuriesAlready = false;
    this.afterPlayTimer = 1000;
    this.DisallowSnap();
    ProEra.Game.MatchState.RunningPat.Value = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioRunningPAT;
    if (PersistentData.quarterLength == 0)
      ProEra.Game.MatchState.GameLength.Value = 180;
    else
      ProEra.Game.MatchState.GameLength.Value = PersistentData.quarterLength * 300;
    this.SetWindValue();
    this.brokenTackles = 0;
    this.timeSinceHardCount = 0.0f;
    this.checkForEndOfQuarter = false;
    this.kickoffLocation = new Vector3(0.0f, 0.182f, Field.KICKOFF_LOCATION);
    Time.timeScale = (float) GameSettings.TimeScale;
  }

  internal void StartFromGameScene()
  {
    if (!SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startedFromGameScene)
      return;
    MonoBehaviour.print((object) "Start From Game Scene called!");
    this.timeManager.SetRunPlayClock(false);
    ProEra.Game.MatchState.Down.Value = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioDown;
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioDoCoinToss && AppState.GameMode != EGameMode.kPracticeMode && AppState.GameMode != EGameMode.kOnboarding)
      return;
    this.timeManager.SetQuarter(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioQuarter);
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioSecondsRemaining > 0)
      ProEra.Game.MatchState.GameLength.Value = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioSecondsRemaining;
    FieldState.OffenseGoingNorth.Value = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioOffenseGoingNorth;
    ScoreClockState.Quarter.Value = this.timeManager.GetQuarterString();
    this.timeManager.AddToGameClock(0.0f);
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioUserOnOffense)
      this.SetCurrentMatchState(EMatchState.UserOnOffense);
    else
      this.SetCurrentMatchState(EMatchState.UserOnDefense);
    this.savedLineOfScrim = Field.GetFieldLocationByYardline(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioLineOfScrimmageYardline, SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioOwnSideOfField);
    ProEra.Game.MatchState.FirstDown.Value = Field.GetFieldLocationByYardline(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioFirstDownYardline, SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioOwnSideOfField);
    if (global::Game.OffenseGoingNorth)
    {
      if ((double) this.savedLineOfScrim > (double) ProEra.Game.MatchState.FirstDown.Value)
        ProEra.Game.MatchState.FirstDown.Value = Mathf.Min(this.savedLineOfScrim + 10f * Field.ONE_YARD, Field.NORTH_GOAL_LINE);
    }
    else if ((double) this.savedLineOfScrim < (double) ProEra.Game.MatchState.FirstDown.Value)
      ProEra.Game.MatchState.FirstDown.Value = Mathf.Max(this.savedLineOfScrim - 10f * Field.ONE_YARD, Field.SOUTH_GOAL_LINE);
    this.SetBallOn(this.savedLineOfScrim);
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
    if (ProEra.Game.MatchState.Stats.User == null)
      ProEra.Game.MatchState.Stats.User = new TeamGameStats();
    if (ProEra.Game.MatchState.Stats.Comp == null)
      ProEra.Game.MatchState.Stats.Comp = new TeamGameStats();
    ProEra.Game.MatchState.Reset();
    if (PersistentData.userIsHome)
    {
      ProEra.Game.MatchState.Stats.User.Score = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioHomeScore;
      ProEra.Game.MatchState.Stats.Comp.Score = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioAwayScore;
    }
    else
    {
      ProEra.Game.MatchState.Stats.User.Score = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioAwayScore;
      ProEra.Game.MatchState.Stats.Comp.Score = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioHomeScore;
    }
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioRunningPAT)
    {
      ProEra.Game.MatchState.RunningPat.Value = true;
      this.SetBallOn(PersistentData.GetHomeTeamData().GetTeamPATLocation());
      this.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
      PlaybookState.HidePlaybook.Trigger();
      MatchManager.instance.playManager.PickPlaysForAI_SpectateMode();
      this.ballManager.SetPosition(new Vector3(0.0f, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
    }
    else
    {
      MatchManager.instance.playersManager.SetAfterPlayActionsForAllPlayers();
      this.SetPlaySelection();
      if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioDoKickoff)
      {
        this.SetBallOn(Field.KICKOFF_LOCATION);
        if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioUserOnOffense)
        {
          MatchManager.instance.playManager.ShowKickoffPlays();
          this.homeTeamGetsBallAtHalf = false;
        }
        else
        {
          MatchManager.instance.playManager.ShowKickReturnPlays();
          this.homeTeamGetsBallAtHalf = true;
        }
      }
      this.ballManager.SetPosition(new Vector3(0.0f, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
      this.timeManager.SetRunGameClock(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioRunGameClock);
      this.timeManager.SetRunPlayClock(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioRunGameClock);
    }
  }

  private void KeyDown(KeyCode code)
  {
    if (code != KeyCode.Space)
      return;
    this.PerformKickActionForPlayerOneIfNeeded();
  }

  private void KeyUp(KeyCode code)
  {
  }

  private void GetMouseInputDown()
  {
    if (this.CanPerformSnapOrKickAction())
      this.PerformKickActionForPlayerOneIfNeeded();
    if (!global::Game.IsPlayActive || !global::Game.UserControlsPlayers)
      return;
    this.playersManager.ManageSelectingClosestPlayer(1);
  }

  private void InputDown(Vector2 inputPosition)
  {
    if (global::Game.IsPlayActive && global::Game.UserControlsPlayers)
      this.playersManager.ManageSelectingClosestPlayer(1);
    if (!this.CanPerformSnapOrKickAction() || !this.IsSnapAllowed())
      return;
    if (global::Game.IsSpecialTeamsPlay)
    {
      if (!global::Game.IsPlayerOneOnOffense)
        return;
      this.KickStateAction(inputPosition);
    }
    else
    {
      if (!global::Game.IsPlayerOneOnOffense || !ProEra.Game.Sources.UI.PrePlayWindowP1.IsOffensiveControlVisible() || !this.IsSnapAllowed())
        return;
      this.SnapBall(inputPosition);
    }
  }

  private void Action1WasPressed(Player userIndex)
  {
    if (this.CanPerformSnapOrKickAction() && this.IsSnapAllowed())
    {
      if (global::Game.IsSpecialTeamsPlay)
      {
        if (global::Game.IsPlayerOneOnOffense)
        {
          if (userIndex == Player.One && ProEra.Game.Sources.UI.PrePlayWindowP1.CanSnapBallWithController())
            this.KickStateAction((Vector2) Input.mousePosition);
        }
        else if (global::Game.Is2PMatch && userIndex == Player.Two && ProEra.Game.Sources.UI.PrePlayWindowP2.CanSnapBallWithController())
          this.KickStateAction((Vector2) Input.mousePosition);
      }
      else
      {
        if (global::Game.IsPlayerOneOnOffense && userIndex == Player.One && ProEra.Game.Sources.UI.PrePlayWindowP1.CanSnapBallWithController())
        {
          if (global::Game.Is2PMatch)
            this.playersManager.savedDefPlayerP2 = this.playersManager.userPlayerIndexP2;
          this.SnapBall((Vector2) Input.mousePosition);
        }
        if (global::Game.Is2PMatch && global::Game.IsPlayerOneOnDefense && userIndex == Player.Two && ProEra.Game.Sources.UI.PrePlayWindowP2.CanSnapBallWithController())
        {
          this.playersManager.savedDefPlayer = this.playersManager.userPlayerIndex;
          this.SnapBall((Vector2) Input.mousePosition);
        }
      }
    }
    if (!global::Game.IsPlayActive || !global::Game.UserControlsPlayers)
      return;
    if (userIndex == Player.One)
    {
      this.playersManager.ManageSelectingClosestPlayer(1);
    }
    else
    {
      if (userIndex != Player.Two)
        return;
      this.playersManager.ManageSelectingClosestPlayer(2);
    }
  }

  private void Action2WasPressed(Player userIndex)
  {
  }

  private void LeftStickButtonWasPressed(Player userIndex) => this.RunHardCountCadence(userIndex);

  private void RightBumperWasPressed(Player userIndex)
  {
  }

  private void LeftBumperWasPressed(Player userIndex)
  {
  }

  private void LeftTriggerWasPressed(Player userIndex)
  {
  }

  private void LeftTriggerWasReleased(Player userIndex)
  {
  }

  public void RightStickButtonWasPressed(Player userIndex)
  {
  }

  private void DPadLeftWasPressed(Player userIndex)
  {
  }

  private void DPadRightWasPressed(Player userIndex)
  {
  }

  private void CheckForRunningToLineOfScrimmage()
  {
    this.playersManager.runningToLineOfScrimmage = false;
    if (global::Game.UserDoesNotControlPlayers)
      return;
    if (global::Game.IsPlayerOneOnOffense)
    {
      if (!UserManager.instance.Action1IsPressed(Player.One) && !Input.GetKey(KeyCode.Space) && !Input.GetMouseButton(0))
        return;
      this.playersManager.runningToLineOfScrimmage = true;
    }
    else
    {
      if (!global::Game.Is2PMatch || !ProEra.Game.MatchState.IsPlayerTwoOnOffense || !UserManager.instance.Action1IsPressed(Player.Two))
        return;
      this.playersManager.runningToLineOfScrimmage = true;
    }
  }

  private void SubscribeToUserInteractionNotifcations()
  {
    NotificationCenter<KeyCode>.AddListener("KeyDown", new Callback<KeyCode>(this.KeyDown));
    NotificationCenter<KeyCode>.AddListener("keyUp", new Callback<KeyCode>(this.KeyUp));
    NotificationCenter<Vector2>.AddListener("InputDown", new Callback<Vector2>(this.InputDown));
    NotificationCenter<Player>.AddListener("Action1WasPressed", new Callback<Player>(this.Action1WasPressed));
    NotificationCenter<Player>.AddListener("Action2WasPressed", new Callback<Player>(this.Action2WasPressed));
    NotificationCenter<Player>.AddListener("LeftStickButtonWasPressed", new Callback<Player>(this.LeftStickButtonWasPressed));
    NotificationCenter<Player>.AddListener("RightBumperWasPressed", new Callback<Player>(this.RightBumperWasPressed));
    NotificationCenter<Player>.AddListener("LeftBumperWasPressed", new Callback<Player>(this.LeftBumperWasPressed));
    NotificationCenter<Player>.AddListener("LeftTriggerWasPressed", new Callback<Player>(this.LeftTriggerWasPressed));
    NotificationCenter<Player>.AddListener("LeftTriggerWasReleased", new Callback<Player>(this.LeftTriggerWasReleased));
    NotificationCenter<Player>.AddListener("RightStickButtonWasPressed", new Callback<Player>(this.RightStickButtonWasPressed));
    NotificationCenter<Player>.AddListener("DPadLeftWasPressed", new Callback<Player>(this.DPadLeftWasPressed));
    NotificationCenter<Player>.AddListener("DPadRightWasPressed", new Callback<Player>(this.DPadRightWasPressed));
    GameplayEvents.DoSnapOrKickAction.OnTrigger += new System.Action(this.GetMouseInputDown);
    this.subscribedToNotifications = true;
  }

  private void UnsubscribeToUserInteractionNotifcations()
  {
    if (!this.subscribedToNotifications)
      return;
    NotificationCenter<KeyCode>.RemoveListener("KeyDown", new Callback<KeyCode>(this.KeyDown));
    NotificationCenter<KeyCode>.RemoveListener("keyUp", new Callback<KeyCode>(this.KeyUp));
    NotificationCenter<Vector2>.RemoveListener("InputDown", new Callback<Vector2>(this.InputDown));
    NotificationCenter<Player>.RemoveListener("Action1WasPressed", new Callback<Player>(this.Action1WasPressed));
    NotificationCenter<Player>.RemoveListener("Action2WasPressed", new Callback<Player>(this.Action2WasPressed));
    NotificationCenter<Player>.RemoveListener("LeftStickButtonWasPressed", new Callback<Player>(this.LeftStickButtonWasPressed));
    NotificationCenter<Player>.RemoveListener("RightBumperWasPressed", new Callback<Player>(this.RightBumperWasPressed));
    NotificationCenter<Player>.RemoveListener("LeftBumperWasPressed", new Callback<Player>(this.LeftBumperWasPressed));
    NotificationCenter<Player>.RemoveListener("LeftTriggerWasPressed", new Callback<Player>(this.LeftTriggerWasPressed));
    NotificationCenter<Player>.RemoveListener("LeftTriggerWasReleased", new Callback<Player>(this.LeftTriggerWasReleased));
    NotificationCenter<Player>.RemoveListener("RightStickButtonWasPressed", new Callback<Player>(this.RightStickButtonWasPressed));
    NotificationCenter<Player>.RemoveListener("DPadLeftWasPressed", new Callback<Player>(this.DPadLeftWasPressed));
    NotificationCenter<Player>.RemoveListener("DPadRightWasPressed", new Callback<Player>(this.DPadRightWasPressed));
    GameplayEvents.DoSnapOrKickAction.OnTrigger -= new System.Action(this.GetMouseInputDown);
  }
}
