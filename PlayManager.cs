// Decompiled with JetBrains decompiler
// Type: PlayManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using FootballVR;
using Framework;
using MovementEffects;
using ProEra.Game;
using ProEra.Game.Services.RemoteConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TB12;
using TB12.UI;
using UDB;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
  public TimeManager timeManager;
  [SerializeField]
  private Playbook _playbookP1;
  [HideInInspector]
  public bool playSelectedOff;
  [HideInInspector]
  public bool playSelectedDef;
  [HideInInspector]
  public bool shownIns1;
  [HideInInspector]
  public bool shownIns2;
  [HideInInspector]
  public PlayType playType;
  [HideInInspector]
  public PlayTypeSpecific playTypeSpecific;
  [HideInInspector]
  public PlayEndType playEndType;
  [HideInInspector]
  public int runnerHoleOffset;
  [HideInInspector]
  public int handOffTargetIndex;
  [HideInInspector]
  public PlayerAI handOffTarget;
  [HideInInspector]
  public PlayerAI fieldGoalKicker;
  [HideInInspector]
  public PlayerAI ballSnapper;
  [HideInInspector]
  public bool autoSelectPlayForAI = true;
  [HideInInspector]
  public bool quickCleanUp;
  [HideInInspector]
  public CurrentPlayStats userTeamCurrentPlayStats;
  [HideInInspector]
  public CurrentPlayStats compTeamCurrentPlayStats;
  [HideInInspector]
  public bool playIsCleanedUp;
  [HideInInspector]
  public PlayDataOff savedOffPlay;
  [HideInInspector]
  public PlayDataDef savedDefPlay;
  [HideInInspector]
  public Vector3 passDestination;
  [HideInInspector]
  public float droppedPassTimer;
  [HideInInspector]
  public float deflectedPassTimer;
  [HideInInspector]
  public float brokenTackleTimer;
  [HideInInspector]
  public float fumbleOccurredTimer;
  [HideInInspector]
  public int samePlayCounter;
  [HideInInspector]
  public FormationData lastPlay;
  [HideInInspector]
  public FormationData lastPlayP2;
  [HideInInspector]
  public bool canUserCallAudible;
  [HideInInspector]
  public bool noKicking;
  [HideInInspector]
  private bool playCallingEnabled = true;
  private MatchManager matchManager;
  private PlayersManager playersManager;
  private FieldManager FieldManager;
  private BallManager ballManager;
  private PlayData savedUserPlay;
  [Header("Settings")]
  [SerializeField]
  private float userPlayCallMinDelay = 1f;
  [Header("Settings")]
  [SerializeField]
  private float userPlayCallMaxDelay = 2f;
  private bool[] savedUserPlayParams;
  private Utility.Logging.Logger PlayLogger;
  private Coroutine pickPlayForAICoroutine;
  private Coroutine waitToHandlePlayConfirmationCoroutine;
  private RoutineHandle hurryUpTransitionRoutine = new RoutineHandle();
  private bool playOver;
  private bool savedFlip;
  private bool _playActive;
  private bool _userHuddlePlayConfirmed;
  private bool _userAudiblePlayConfirmed;
  private List<PlayDataOff> offensivePlayCategory;

  public event System.Action OnUserPlayCallStarted;

  public event System.Action OnUserPlayCallEnded;

  public event System.Action OnUserPlayCallMade;

  public event Action<bool> On;

  public event Action<bool> OnPlayActiveEvent;

  public bool PlayActive
  {
    get => this._playActive;
    set
    {
      this._playActive = value;
      this.OnPlayActiveEvent(value);
    }
  }

  public bool ShouldOffenseHurryUp { get; set; }

  public bool IsUserRunningHurryUp { get; private set; }

  public bool HasSavedUserPlay() => this.savedUserPlay != null;

  public GameLog GameLog { get; private set; }

  public bool UserHuddlePlayConfirmed => this._userHuddlePlayConfirmed;

  public bool UserAudiblePlayConfirmed => this._userAudiblePlayConfirmed;

  private void Start()
  {
    this.GameLog = new GameLog();
    this.matchManager = MatchManager.instance;
    this.playersManager = MatchManager.instance.playersManager;
    this.FieldManager = SingletonBehaviour<FieldManager, MonoBehaviour>.instance;
    this.userTeamCurrentPlayStats = new CurrentPlayStats();
    this.compTeamCurrentPlayStats = new CurrentPlayStats();
    this.ballManager = this.matchManager.ballManager;
    this.savedUserPlayParams = new bool[2];
    this.shownIns1 = false;
    this.samePlayCounter = 0;
    PlayState.PlayOver.Value = true;
    PlayState.HuddleBreak.Value = false;
    this.playSelectedDef = this.playSelectedOff = false;
  }

  private void OnDestroy()
  {
    Debug.Log((object) "PlayManager -> OnDestroy");
    this.StopAllCoroutines();
    Timing.KillCoroutines();
    this.hurryUpTransitionRoutine.Stop();
    if (!((UnityEngine.Object) GUIManager.instance != (UnityEngine.Object) null))
      return;
    GUIManager.instance.HidePrePlayWindows();
  }

  public void BeginNewPlay() => this.GameLog.BeginNewPlay();

  public void AddToCurrentPlayLog(string log)
  {
  }

  private void ApplyCurrentPlayStatsToPlayers()
  {
    foreach (KeyValuePair<int, PlayerStats> player in this.userTeamCurrentPlayStats.players)
      this.playersManager.userTeamData.GetPlayer(player.Key).CurrentGameStats.AddToStats(player.Value);
    foreach (KeyValuePair<int, PlayerStats> player in this.compTeamCurrentPlayStats.players)
      this.playersManager.compTeamData.GetPlayer(player.Key).CurrentGameStats.AddToStats(player.Value);
    this.userTeamCurrentPlayStats.ClearCurrentPlayStats();
    this.compTeamCurrentPlayStats.ClearCurrentPlayStats();
  }

  public void ShowKickReturnPlays()
  {
    MatchManager.instance.timeManager.SetRunPlayClock(false);
    this.matchManager.SetCurrentMatchState(EMatchState.UserOnDefense);
  }

  public void ShowKickoffPlays()
  {
    MatchManager.instance.timeManager.SetRunPlayClock(false);
    this.matchManager.SetCurrentMatchState(EMatchState.UserOnOffense);
    PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Offense);
  }

  public bool SetPlayToLastPlay()
  {
    Debug.Log((object) nameof (SetPlayToLastPlay));
    if (!global::Game.IsNot2PMatch || this.savedUserPlay == null || (bool) ProEra.Game.MatchState.IsKickoff || (bool) ProEra.Game.MatchState.RunningPat || !global::Game.IsPlayerOneOnOffense)
      return false;
    Debug.Log((object) "SetPlayToLastPlay: success");
    ++this.samePlayCounter;
    bool savedUserPlayParam = this.savedUserPlayParams[0];
    bool audible = false;
    this.SetupPlay((PlayDataOff) this.savedUserPlay, this.GetDefensivePlayForAI((PlayDataOff) this.savedUserPlay), savedUserPlayParam, audible);
    int huddlePlayClockOffset = (int) ScriptableSingleton<VRSettings>.Instance.NoHuddlePlayClockOffset;
    if (MatchManager.instance.timeManager.IsGameClockRunning())
      MatchManager.instance.timeManager.AddToGameClock((float) huddlePlayClockOffset);
    MatchManager.instance.timeManager.AddToPlayClock((float) huddlePlayClockOffset);
    MatchManager.instance.timeManager.SetAcceleratedClockEnabled(false);
    MatchManager.instance.timeManager.SetRunPlayClock(true);
    PlaybookState.CurrentFormation.SetValue(Plays.self.GetOffFormDataFromPlay(true, this.savedUserPlay));
    PlaybookState.CurrentPlay.SetValue(this.savedUserPlay);
    this.IsUserRunningHurryUp = true;
    this.hurryUpTransitionRoutine.Run(this.DelayedSetPlayToLastPlay());
    this.canUserCallAudible = false;
    PlaybookState.HidePlaybook.Trigger();
    return true;
  }

  private IEnumerator DelayedSetPlayToLastPlay()
  {
    MatchManager.instance.playersManager.BreakHuddle();
    yield return (object) GamePlayerController.CameraFade.Fade();
    this.ResetPlayLines();
    MatchManager.instance.playersManager.PutAllPlayersInPlayPosition();
    System.Action userPlayCallMade = this.OnUserPlayCallMade;
    if (userPlayCallMade != null)
      userPlayCallMade();
    MatchManager.instance.playManager._userHuddlePlayConfirmed = false;
    MatchManager.instance.playManager._userAudiblePlayConfirmed = false;
    this.OnUserPlayCallEnded();
    this.IsUserRunningHurryUp = false;
  }

  private void ResetPlayLines()
  {
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetFirstDownLine();
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.SetActive(FieldState.FirstDownLineVisible.Value);
  }

  public void SetPlay(PlayData userPlay, bool flip, bool audible, bool offensivePlay)
  {
    PlayDataOff playDataOff = new PlayDataOff();
    if (userPlay is PlayDataOff)
    {
      playDataOff = (PlayDataOff) userPlay;
      this.savedOffPlay = playDataOff;
    }
    if ((bool) ProEra.Game.MatchState.RunningPat & offensivePlay && playDataOff.GetPlayType() == PlayType.Punt)
      return;
    if (global::Game.IsNot2PMatch)
    {
      if (global::Game.IsPlayerOneOnOffense)
      {
        if (this.savedUserPlay == userPlay && !audible)
          ++this.samePlayCounter;
        else
          this.samePlayCounter = 0;
        this.savedUserPlay = userPlay;
        this.savedUserPlayParams[0] = flip;
        this.savedUserPlayParams[1] = audible;
        this.SetupPlay((PlayDataOff) userPlay, this.GetDefensivePlayForAI((PlayDataOff) userPlay), flip, audible);
      }
      else
      {
        this.samePlayCounter = 0;
        this.savedDefPlay = (PlayDataDef) userPlay;
        if (global::Game.UserCallsPlays)
          this.savedOffPlay = this.GetOffensivePlayForAI();
        this.SetupPlay(this.savedOffPlay, this.savedDefPlay, false, audible);
      }
      if (audible)
        return;
      PlaybookState.HidePlaybook.Trigger();
    }
    else
    {
      if (audible)
      {
        if (userPlay is PlayDataOff)
        {
          int playerCallingAudible = !global::Game.IsPlayerOneOnOffense ? 2 : 1;
          this.SetupPlay((PlayDataOff) userPlay, this.savedDefPlay, this.savedFlip, audible, playerCallingAudible);
        }
        else
        {
          int playerCallingAudible = !global::Game.IsPlayerOneOnOffense ? 1 : 2;
          this.SetupPlay(this.savedOffPlay, (PlayDataDef) userPlay, this.savedFlip, audible, playerCallingAudible);
        }
      }
      else if (this.playSelectedOff)
      {
        this.savedDefPlay = (PlayDataDef) userPlay;
        this.SetupPlay((PlayDataOff) this.savedUserPlay, this.savedDefPlay, this.savedFlip, audible, 0);
      }
      else
      {
        this.savedDefPlay = (PlayDataDef) this.savedUserPlay;
        this.SetupPlay((PlayDataOff) userPlay, this.savedDefPlay, flip, audible, 0);
      }
      if (audible)
        return;
      PlaybookState.HidePlaybook.Trigger();
    }
  }

  public void LoadPlay(
    PlayDataOff offensivePlay,
    PlayDataDef defensivePlay,
    int yardLine,
    float hashPosition = 0.0f,
    bool onOffensiveTeamSideOfField = false,
    bool isPlayFlipped = false)
  {
    float locationByYardline = Field.GetFieldLocationByYardline(yardLine, onOffensiveTeamSideOfField);
    this.LoadPlay(offensivePlay, defensivePlay, locationByYardline, hashPosition, onOffensiveTeamSideOfField, isPlayFlipped);
  }

  public void LoadPlay(
    PlayDataOff offensivePlay,
    PlayDataDef defensivePlay,
    float fieldPosition,
    float hashPosition = 0.0f,
    bool onOffensiveTeamSideOfField = false,
    bool isPlayFlipped = false)
  {
    Vector3 zero = Vector3.zero with
    {
      x = hashPosition,
      z = fieldPosition
    };
    FieldState.OffenseGoingNorth.Value = onOffensiveTeamSideOfField;
    MatchManager.instance.SetBallOn(fieldPosition);
    MatchManager.instance.SetBallHashPosition(zero.x);
    SingletonBehaviour<BallManager, MonoBehaviour>.instance.SetPosition(zero);
    MatchManager.instance.playersManager.PutAllPlayersInHuddle();
    this.SetupPlay(offensivePlay, defensivePlay, isPlayFlipped);
  }

  public void SetupPlay(
    PlayDataOff offensivePlay,
    PlayDataDef defensivePlay,
    bool isPlayFlipped,
    bool audible = false,
    int playerCallingAudible = 1)
  {
    NotificationCenter.Broadcast("setupPlayBegin");
    bool flag1 = false;
    bool flag2 = offensivePlay == Plays.self.spc_kickoffMid || offensivePlay == Plays.self.spc_kickoffRight || offensivePlay == Plays.self.spc_kickoffLeft || offensivePlay == Plays.self.spc_onsideKick;
    int num1 = defensivePlay == Plays.self.dspc_kickRetLeft || defensivePlay == Plays.self.dspc_kickRetMid || defensivePlay == Plays.self.dspc_kickRetRight || defensivePlay == Plays.self.dspc_kickRetPinch ? 1 : (defensivePlay == Plays.self.dspc_kickRetOnside ? 1 : 0);
    bool flag3 = offensivePlay == Plays.self.spc_puntLeft || offensivePlay == Plays.self.spc_puntRight || offensivePlay == Plays.self.spc_puntProtect;
    bool flag4 = false;
    TimeoutManager.EndTimeOut();
    if (audible)
    {
      Globals.PauseGame.SetValue(false);
      if (playerCallingAudible == 0)
        Debug.Log((object) "Player calling audible is 0 -> should be 1 or 2");
      if (global::Game.IsPlayerOneOnOffense && playerCallingAudible == 1)
        flag1 = true;
      else if (global::Game.IsPlayerOneOnDefense && playerCallingAudible == 2)
        flag1 = true;
    }
    if (offensivePlay == Plays.self.spc_fieldGoal)
    {
      if (ProEra.Game.MatchState.RunningPat.Value)
      {
        this.matchManager.SetBallOn(PersistentData.GetHomeTeamData().GetTeamPATLocation());
        this.matchManager.SetBallHashPosition(0.0f);
        this.matchManager.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
        this.ballManager.SetPosition(new Vector3(this.matchManager.ballHashPosition + 0.1f * (float) global::Game.OffensiveFieldDirection, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
      }
      defensivePlay = Plays.self.dspc_fgBlock;
    }
    else if (flag3)
    {
      defensivePlay = (double) UnityEngine.Random.value > 0.5 ? Plays.self.dspc_puntReturnLeft : Plays.self.dspc_puntReturnLeft;
      flag4 = true;
    }
    else if (flag2)
    {
      MatchManager.instance.timeManager.SetRunPlayClock(false);
      if (global::Game.IsNot2PMatch && global::Game.IsPlayerOneOnOffense)
        defensivePlay = Plays.self.dspc_kickRetMid;
      if (offensivePlay == Plays.self.spc_onsideKick)
      {
        this.matchManager.onsideKick = true;
        defensivePlay = Plays.self.dspc_kickRetOnside;
      }
      else
        this.matchManager.onsideKick = false;
    }
    else if (ProEra.Game.MatchState.RunningPat.Value)
    {
      this.matchManager.SetBallOn(Field.TWO_POINT_ATTEMPT_LINE);
      this.matchManager.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
      this.ballManager.SetPosition(new Vector3(this.matchManager.ballHashPosition, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
      if (global::Game.IsNot2PMatch && global::Game.IsPlayerOneOnOffense)
        defensivePlay = (PlayDataDef) Plays.self.fiveThreePlays.GetPlay(0);
    }
    else if ((defensivePlay == Plays.self.dspc_fgBlock || defensivePlay == Plays.self.dspc_puntBlock || defensivePlay == Plays.self.dspc_puntReturnLeft || defensivePlay == Plays.self.dspc_puntReturnRight) && global::Game.IsNot2PMatch && global::Game.IsPlayerOneOnOffense)
      defensivePlay = (PlayDataDef) Plays.self.nickelPlays.GetPlay(0);
    this.playType = offensivePlay.GetPlayType();
    PlayState.PlayType.Value = this.playType;
    this.playTypeSpecific = offensivePlay.GetPlayTypeSpecific();
    int playType = (int) this.playType;
    ScoreClockState.Personnel.SetValue(offensivePlay.GetFormation().GetPersonnel());
    FormationPositions formation1 = offensivePlay.GetFormation();
    FormationPositionsDef formation2 = defensivePlay.GetFormation();
    int shiftedIndex = formation1.GetDefensiveFormationVariation();
    if (playType > 2)
      shiftedIndex = 0;
    float[] xlocations1 = formation1.GetXLocations();
    float[] zlocations1 = formation1.GetZLocations();
    float[] xlocations2 = formation2.GetXLocations(shiftedIndex);
    float[] zlocations2 = formation2.GetZLocations(shiftedIndex);
    int[] stances1 = formation1.GetStances();
    int[] stances2 = formation2.GetStances();
    this.FieldManager.SetRefereePositions();
    this.FieldManager.SetChainGangPositions();
    int num2 = 1;
    if (isPlayFlipped)
      num2 = -1;
    int flip = num2 * global::Game.OffensiveFieldDirection;
    if (!audible)
      this.matchManager.playTimer = 0.0f;
    List<PlayerAI> playerAiList1;
    List<PlayerAI> playerAiList2;
    if (global::Game.IsPlayerOneOnOffense)
    {
      playerAiList1 = this.playersManager.curUserScriptRef;
      playerAiList2 = this.playersManager.curCompScriptRef;
    }
    else
    {
      playerAiList1 = this.playersManager.curCompScriptRef;
      playerAiList2 = this.playersManager.curUserScriptRef;
    }
    if (!audible)
    {
      this.SubInOffPlayersForPlay(formation1);
      this.SubInDefPlayersForPlay(formation2);
      for (int index = 0; index < 11; ++index)
      {
        Vector3 _position = new Vector3(xlocations1[index] * Field.ONE_YARD * (float) flip + this.matchManager.ballHashPosition, 0.0f, (float) ((double) zlocations1[index] * (double) Field.ONE_YARD * (double) global::Game.OffensiveFieldDirection + (double) ProEra.Game.MatchState.BallOn.Value - (double) Field.TWO_FEET * (double) global::Game.OffensiveFieldDirection));
        playerAiList1[index].SetBeforePlayActions(_position, stances1[index], this.playType, this.playTypeSpecific);
        playerAiList1[index].animatorCommunicator.maxEffort01 = Mathf.Clamp01(playerAiList1[index].speed);
        _position = new Vector3(xlocations2[index] * Field.ONE_YARD * (float) flip + this.matchManager.ballHashPosition, 0.0f, Mathf.Clamp((float) ((double) zlocations2[index] * (double) Field.ONE_YARD * (double) global::Game.OffensiveFieldDirection + (double) ProEra.Game.MatchState.BallOn.Value + (double) Field.THIRTY_INCHES * (double) global::Game.OffensiveFieldDirection), Field.SOUTH_BACK_OF_ENDZONE, Field.NORTH_BACK_OF_ENDZONE));
        playerAiList2[index].SetBeforePlayActions(_position, stances2[index], this.playType, this.playTypeSpecific);
        playerAiList2[index].animatorCommunicator.maxEffort01 = Mathf.Clamp01(playerAiList1[index].speed);
      }
      if (stances1[5] == 1)
        playerAiList1[5].SetPlayStartingPosition(playerAiList1[5].GetPlayStartPosition() + new Vector3(0.0f, 0.0f, Field.TWO_FEET * (float) global::Game.OffensiveFieldDirection));
    }
    this.runnerHoleOffset = offensivePlay.GetRunnerHoleOffset();
    this.ballSnapper = playerAiList1[2];
    if (this.playType == PlayType.Run)
    {
      int handoffType = (int) offensivePlay.GetHandoffType();
      int offensiveFieldDirection = global::Game.OffensiveFieldDirection;
      if (global::Game.IsPlayerOneOnOffense)
      {
        ++ProEra.Game.MatchState.Stats.User.ConsecutiveRunPlays;
        ProEra.Game.MatchState.Stats.User.ConsecutivePassPlays = 0;
        ++ProEra.Game.MatchState.Stats.User.TotalRunPlays;
        this.handOffTargetIndex = offensivePlay.GetHandoffTarget();
        this.handOffTarget = this.playersManager.curUserScriptRef[this.handOffTargetIndex];
      }
      else
      {
        ++ProEra.Game.MatchState.Stats.Comp.ConsecutiveRunPlays;
        ProEra.Game.MatchState.Stats.Comp.ConsecutivePassPlays = 0;
        ++ProEra.Game.MatchState.Stats.Comp.TotalRunPlays;
        this.handOffTargetIndex = offensivePlay.GetHandoffTarget();
        this.handOffTarget = this.playersManager.curCompScriptRef[this.handOffTargetIndex];
      }
    }
    else if (this.playType == PlayType.Pass)
    {
      int handoffType = (int) offensivePlay.GetHandoffType();
      int offensiveFieldDirection = global::Game.OffensiveFieldDirection;
      this.matchManager.allowThrowAt = -1;
      if (global::Game.IsPlayerOneOnOffense)
      {
        ProEra.Game.MatchState.Stats.User.ConsecutiveRunPlays = 0;
        ++ProEra.Game.MatchState.Stats.User.ConsecutivePassPlays;
        ++ProEra.Game.MatchState.Stats.User.TotalPassPlays;
        this.handOffTargetIndex = offensivePlay.GetHandoffTarget();
        this.handOffTarget = this.playersManager.curUserScriptRef[this.handOffTargetIndex];
        if (this.playTypeSpecific == PlayTypeSpecific.PlayAction)
        {
          this.handOffTargetIndex = offensivePlay.GetHandoffTarget();
          this.handOffTarget = this.playersManager.curUserScriptRef[this.handOffTargetIndex];
        }
      }
      else
      {
        ProEra.Game.MatchState.Stats.Comp.ConsecutiveRunPlays = 0;
        ++ProEra.Game.MatchState.Stats.Comp.ConsecutivePassPlays;
        ++ProEra.Game.MatchState.Stats.Comp.TotalPassPlays;
        this.handOffTargetIndex = offensivePlay.GetHandoffTarget();
        this.handOffTarget = this.playersManager.curUserScriptRef[this.handOffTargetIndex];
        if (this.playTypeSpecific == PlayTypeSpecific.PlayAction)
        {
          this.handOffTargetIndex = offensivePlay.GetHandoffTarget();
          this.handOffTarget = this.playersManager.curCompScriptRef[this.handOffTargetIndex];
        }
      }
    }
    else if (this.playType == PlayType.FG)
    {
      playerAiList1[6].SetPlayStartingPosition(playerAiList1[6].GetPlayStartPosition() + new Vector3(0.33f * (float) global::Game.OffensiveFieldDirection, 0.0f, 0.1f * (float) global::Game.OffensiveFieldDirection));
      this.fieldGoalKicker = playerAiList1[6];
      if (global::Game.UserControlsPlayers)
      {
        if (global::Game.IsPlayerOneOnOffense)
        {
          ProEra.Game.Sources.UI.KickMeter.SetAimSpeed(playerAiList1[6].kickingAccuracy);
          ProEra.Game.Sources.UI.KickMeter.ShowWindow();
        }
        else if (global::Game.Is2PMatch)
        {
          ProEra.Game.Sources.UI.KickMeter.SetAimSpeed(playerAiList1[6].kickingAccuracy);
          ProEra.Game.Sources.UI.KickMeter.ShowWindow();
        }
      }
      this.matchManager.allowThrowAt = -1;
    }
    else if (this.playType == PlayType.Punt)
    {
      float z = Mathf.Clamp(playerAiList1[6].GetPlayStartPosition().z, Field.SOUTH_BACK_OF_ENDZONE + Field.ONE_YARD, Field.NORTH_BACK_OF_ENDZONE - Field.ONE_YARD);
      playerAiList1[6].SetPlayStartingPosition(new Vector3(this.matchManager.ballHashPosition, 0.0f, z));
      if (flag4)
      {
        float locationByYardline = Field.GetFieldLocationByYardline(10, false);
        float firstObjectZPos = (float) ProEra.Game.MatchState.BallOn + Field.TEN_YARDS * (float) global::Game.OffensiveFieldDirection;
        float secondObjectZPos = Field.FurtherDownfield(firstObjectZPos, locationByYardline) ? firstObjectZPos : locationByYardline;
        float num3 = playerAiList2[9].GetPlayStartPosition().z;
        if (Field.FurtherDownfield(num3, secondObjectZPos))
          num3 = secondObjectZPos;
        playerAiList2[9].SetPlayStartingPosition(new Vector3(this.matchManager.ballHashPosition, 0.0f, num3));
      }
      if (global::Game.IsPlayerOneOnOffense && global::Game.UserControlsPlayers)
      {
        ProEra.Game.Sources.UI.KickMeter.SetAimSpeed(this.playersManager.curUserScriptRef[6].kickingAccuracy);
        ProEra.Game.Sources.UI.KickMeter.ShowWindow();
      }
      else if (global::Game.Is2PMatch && global::Game.UserControlsPlayers)
      {
        ProEra.Game.Sources.UI.KickMeter.SetAimSpeed(this.playersManager.curCompScriptRef[6].kickingAccuracy);
        ProEra.Game.Sources.UI.KickMeter.ShowWindow();
      }
      this.matchManager.allowThrowAt = -1;
    }
    else if (this.playType == PlayType.Kickoff)
    {
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.SetActive(false);
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.lineOfScrimmage.SetActive(false);
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetBallOnTee();
      AffineTransform fromBallToKicker = playerAiList1[6].GetIdealTransformFromBallToKicker(global::Game.IsOnsidesKick);
      Vector3 _position = SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position.SetY(0.0f) + fromBallToKicker.position * (float) global::Game.OffensiveFieldDirection;
      playerAiList1[6].SetPlayStartingPosition(_position);
      if (global::Game.IsPlayerOneOnOffense && global::Game.UserControlsPlayers)
      {
        ProEra.Game.Sources.UI.KickMeter.SetAimSpeed(this.playersManager.curUserScriptRef[6].kickingAccuracy);
        ProEra.Game.Sources.UI.KickMeter.ShowWindow();
      }
      else if (global::Game.Is2PMatch && global::Game.UserControlsPlayers)
      {
        ProEra.Game.Sources.UI.KickMeter.SetAimSpeed(this.playersManager.curCompScriptRef[6].kickingAccuracy);
        ProEra.Game.Sources.UI.KickMeter.ShowWindow();
      }
      this.matchManager.allowThrowAt = -1;
    }
    MatchManager.instance.playersManager.receiversLeftToRight.Clear();
    MatchManager.instance.playersManager.defendersInManCoverageLeftToRight.Clear();
    for (int index = 0; index < 11; ++index)
    {
      if (global::Game.IsPlayerOneOnOffense)
      {
        this.playersManager.curUserScriptRef[index].SetupOffPlay(offensivePlay.GetRouteData(index), flip, offensivePlay.GetFormation().GetPositions(index), this.handOffTarget);
        this.playersManager.curCompScriptRef[index].SetupDefPlay(defensivePlay.GetRouteData(index), flip, defensivePlay.GetFormation().GetPositions(index));
        if ((double) defensivePlay.GetRoute(index)[0] == 1.0)
          MatchManager.instance.playersManager.defendersInManCoverageLeftToRight.Add(new KeyValuePair<int, float>(index, this.playersManager.curCompScriptRef[index].GetPlayStartPosition().x));
      }
      else
      {
        this.playersManager.curCompScriptRef[index].SetupOffPlay(offensivePlay.GetRouteData(index), flip, offensivePlay.GetFormation().GetPositions(index), this.handOffTarget);
        this.playersManager.curUserScriptRef[index].SetupDefPlay(defensivePlay.GetRouteData(index), flip, defensivePlay.GetFormation().GetPositions(index));
        if ((double) defensivePlay.GetRoute(index)[0] == 1.0)
          MatchManager.instance.playersManager.defendersInManCoverageLeftToRight.Add(new KeyValuePair<int, float>(index, this.playersManager.curUserScriptRef[index].GetPlayStartPosition().x));
      }
    }
    this.SetManDefenseAssignments();
    MatchManager.instance.playersManager.AllowPlayerCycle();
    float beforeSnapAllowed = global::Game.PreplayConfig.minSecondsBeforeSnapAllowed;
    MatchManager.instance.AllowSnap(beforeSnapAllowed);
    this.playersManager.mouseDown = false;
    this.playersManager.ballWasThrownOrKicked = false;
    this.playersManager.hasStartedHandoffFailsafe = false;
    ProEra.Game.MatchState.Turnover.Value = false;
    this.playersManager.throwStarted = false;
    this.matchManager.playTimer = 0.0f;
    this.playersManager.convergeOnBall = false;
    PlayState.PlayOver.Value = false;
    this.passDestination = Vector3.zero;
    this.droppedPassTimer = 0.0f;
    this.deflectedPassTimer = 0.0f;
    this.brokenTackleTimer = 0.0f;
    this.playersManager.passCircleGO.SetActive(false);
    this.playersManager.passCircleTrans.position = new Vector3(0.0f, this.playersManager.passCircleTrans.position.y, ProEra.Game.MatchState.BallOn.Value + 5f);
    if (global::Game.IsPlayerOneOnOffense)
    {
      if (global::Game.Is2PMatch && global::Game.UserControlsPlayers)
      {
        if (this.playType == PlayType.Kickoff)
          this.playersManager.SetUserPlayerP2(-1);
        else if (!audible)
          this.playersManager.SetUserPlayerP2(this.playersManager.savedDefPlayerP2);
      }
      else if (global::Game.UserDoesNotControlPlayers && !global::Game.UserControlsQB && !audible || global::Game.IsPunt || global::Game.IsKickoff || global::Game.IsFG || (bool) ProEra.Game.MatchState.IsKickoff || (bool) ProEra.Game.MatchState.IsSafetyKickoff)
        this.playersManager.AISnapBall();
    }
    else
    {
      if (this.playType == PlayType.Kickoff || global::Game.UserDoesNotControlPlayers)
        this.playersManager.SetUserPlayer(-1);
      else if (!audible && global::Game.UserControlsPlayers)
        this.playersManager.SetUserPlayer(this.playersManager.savedDefPlayer);
      if (global::Game.IsNot2PMatch || global::Game.Is2PMatch && !audible && global::Game.UserDoesNotControlPlayers)
        this.playersManager.AISnapBall();
    }
    if (audible & flag1)
    {
      PEGameplayEventManager.RecordAudibleEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, offensivePlay);
      PEGameplayEventManager.RecordPlaySelectedEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, MatchManager.instance.timeManager.GetQuarter(), global::Game.IsHomeTeamOnOffense, global::Game.OffenseGoingNorth, offensivePlay, this.savedDefPlay, MatchManager.down, ProEra.Game.MatchState.FirstDown.Value, ProEra.Game.MatchState.BallOn.Value);
    }
    this.savedDefPlay = defensivePlay;
    this.savedOffPlay = offensivePlay;
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetDownMarkerTexture();
    this.matchManager.SaveMatchStateBeforePlay();
    MatchManager.instance.CheckForTimeouts();
    MainGameTablet.RefreshPlayStats();
    NotificationCenter.Broadcast("setupPlayEnd");
  }

  private void MarkOffPlayersForSub(FormationPositions offFormation)
  {
    if (global::Game.IsPlayerOneOnOffense)
      this.playersManager.MarkPlayersToSwap(1, PersistentData.GetUserTeam().TeamDepthChart.GetPlayersInFormation(offFormation));
    else
      this.playersManager.MarkPlayersToSwap(2, PersistentData.GetCompTeam().TeamDepthChart.GetPlayersInFormation(offFormation));
  }

  public void SubInOffPlayersForPlay(FormationPositions offFormation)
  {
    if (global::Game.IsPlayerOneOnOffense)
      this.playersManager.CreateTeamPlayers(1, PersistentData.GetUserTeam().TeamDepthChart.GetPlayersInFormation(offFormation));
    else
      this.playersManager.CreateTeamPlayers(2, PersistentData.GetCompTeam().TeamDepthChart.GetPlayersInFormation(offFormation));
  }

  private void MarkDefPlayersForSub(FormationPositions defFormation)
  {
    if (global::Game.IsPlayerOneOnOffense)
      this.playersManager.MarkPlayersToSwap(2, PersistentData.GetCompTeam().TeamDepthChart.GetPlayersInFormation(defFormation));
    else
      this.playersManager.MarkPlayersToSwap(1, PersistentData.GetUserTeam().TeamDepthChart.GetPlayersInFormation(defFormation));
  }

  public void SubInDefPlayersForPlay(FormationPositionsDef defFormation)
  {
    if (global::Game.IsPlayerOneOnOffense)
      this.playersManager.CreateTeamPlayers(2, PersistentData.GetCompTeam().TeamDepthChart.GetPlayersInFormation((FormationPositions) defFormation));
    else
      this.playersManager.CreateTeamPlayers(1, PersistentData.GetUserTeam().TeamDepthChart.GetPlayersInFormation((FormationPositions) defFormation));
  }

  public void PutBallInCentersHands()
  {
    PlayerAI offensivePlayer = global::Game.OffensivePlayers[global::Game.OffensiveCenter.indexInFormation];
    if ((UnityEngine.Object) offensivePlayer == (UnityEngine.Object) null || PlayState.IsKickoff || !offensivePlayer.animatorCommunicator.atPlayPosition)
      return;
    this.playersManager.SetBallHolder(global::Game.OffensiveCenter.indexInFormation, global::Game.OffensiveCenter.onUserTeam);
  }

  private void SetManDefenseAssignments()
  {
    MatchManager.instance.playersManager.defendersInManCoverageLeftToRight.Sort((Comparison<KeyValuePair<int, float>>) ((pair1, pair2) => pair1.Value.CompareTo(pair2.Value)));
    if (global::Game.IsPlayerOneOnOffense)
    {
      for (int index = 6; index < this.playersManager.curUserScriptRef.Count; ++index)
        MatchManager.instance.playersManager.receiversLeftToRight.Add(new KeyValuePair<int, float>(index, this.playersManager.curUserScriptRef[index].GetPlayStartPosition().x));
    }
    else
    {
      for (int index = 6; index < this.playersManager.curCompScriptRef.Count; ++index)
        MatchManager.instance.playersManager.receiversLeftToRight.Add(new KeyValuePair<int, float>(index, this.playersManager.curCompScriptRef[index].GetPlayStartPosition().x));
    }
    MatchManager.instance.playersManager.receiversLeftToRight.Sort((Comparison<KeyValuePair<int, float>>) ((pair1, pair2) => pair1.Value.CompareTo(pair2.Value)));
    List<PlayerAI> playerAiList = this.playersManager.curUserScriptRef;
    if (global::Game.IsPlayerOneOnOffense)
      playerAiList = this.playersManager.curCompScriptRef;
    for (int index = 0; index < MatchManager.instance.playersManager.defendersInManCoverageLeftToRight.Count; ++index)
    {
      int key1 = MatchManager.instance.playersManager.defendersInManCoverageLeftToRight[index].Key;
      int key2 = MatchManager.instance.playersManager.receiversLeftToRight[Mathf.Min(index, MatchManager.instance.playersManager.receiversLeftToRight.Count - 1)].Key;
      PlayerAI playerAi = playerAiList[key1];
      if (playerAi.GetCurrentAssignment() is ManDefenseAssignment currentAssignment)
        playerAi.SetManDefense(key2, currentAssignment);
      else
        Debug.LogError((object) (" Trying to set Man Defense on player " + playerAi.playerName + " who does not have Man Coverage Assignment"));
    }
  }

  public void CleanUpPlay()
  {
    if ((UnityEngine.Object) MatchManager.instance == (UnityEngine.Object) null)
    {
      Debug.LogError((object) "PlayManager.CleanUpPlay is trying to access MatchManager.instance which is null!!!");
    }
    else
    {
      Debug.Log((object) ("Start cleanup Play: SetBallOn (" + ProEra.Game.MatchState.BallOn.Value.ToString() + ", firstDown = " + ProEra.Game.MatchState.FirstDown.Value.ToString() + ", down = " + ProEra.Game.MatchState.Down.Value.ToString() + ", savedLineOfScrim = " + this.matchManager.savedLineOfScrim.ToString() + ", tempBallPos = " + this.FieldManager.tempBallPos.ToString()));
      MatchManager.instance.timeManager.ResetPlayClock();
      MatchManager.instance.timeManager.SetRunPlayClock(true);
      Ball.State.BallState.SetValue(EBallState.PlayOver);
      ControllerManagerGame.playSelectedWithCont = false;
      this.playSelectedDef = false;
      this.playSelectedOff = false;
      this.playersManager.userPlayerScript = (PlayerAI) null;
      this.playersManager.userPlayerScriptP2 = (PlayerAI) null;
      this.savedFlip = false;
      this.matchManager.checkForEndOfQuarter = true;
      this.ballManager.toss = false;
      this.playIsCleanedUp = true;
      this.playersManager.passTargetScript = (PlayerAI) null;
      bool forceKickOff = false;
      this.playersManager.cachedBallHolderAtPlayEnd = this.playersManager.ballHolderScript;
      if (this.quickCleanUp)
      {
        this.FieldManager.SetLineOfScrimmageLine();
        this.playersManager.passCircleGO.SetActive(false);
        this.playersManager.ballLandingSpotGO.SetActive(false);
        FatigueManager.RecoverAllPlayers();
        SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.ClearPenalty();
        this.ballManager.ResetColliderSize();
        this.playersManager.BallHolderReleaseBall();
        this.playersManager.SetAfterPlayActionsForAllPlayers();
        if (this.ShouldOffenseHurryUp && this.SetPlayToLastPlay())
          this.ShouldOffenseHurryUp = false;
        else if (AppState.GameMode == EGameMode.kPracticeMode)
          PlaybookState.ShowPlaybook.Trigger();
        else
          this.playersManager.PutAllPlayersInHuddle();
      }
      else
      {
        if (PlayState.IsRunOrPass)
          PlayStats.QbIndexReference.SetValue(global::Game.OffensiveQB.indexOnTeam);
        if (PlayState.IsKickoff)
        {
          this.FieldManager.tee.SetActive(false);
          ProEra.Game.MatchState.IsKickoff.Value = false;
          ProEra.Game.MatchState.IsSafetyKickoff.Value = false;
        }
        this.playersManager.passCircleGO.SetActive(false);
        this.playersManager.ballLandingSpotGO.SetActive(false);
        FatigueManager.RecoverAllPlayers();
        this.playersManager.SetUserPlayer(-1);
        if (global::Game.Is2PMatch)
          this.playersManager.SetUserPlayerP2(-1);
        if (global::Game.IsOnsidesKick && !(bool) ProEra.Game.MatchState.Turnover)
          this.FieldManager.SetFirstDownAndFirstDownLine();
        if (Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, Field.OFFENSIVE_GOAL_LINE) && (bool) ProEra.Game.MatchState.Turnover)
        {
          this.matchManager.SetBallOn(Field.OWN_TWENTY_YARD_LINE);
          this.matchManager.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
        }
        if (Field.FurtherDownfield(Field.DEFENSIVE_GOAL_LINE, ProEra.Game.MatchState.BallOn.Value) && !(bool) ProEra.Game.MatchState.Turnover)
        {
          ProEra.Game.MatchState.Down.Value = 1;
          this.matchManager.AddScore(-2);
          this.playEndType = PlayEndType.Safety;
          MatchManager.instance.timeManager.SetRunGameClock(false);
          forceKickOff = true;
          ProEra.Game.MatchState.IsSafetyKickoff.Value = true;
        }
        if (Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, ProEra.Game.MatchState.FirstDown.Value))
        {
          if (ProEra.Game.MatchState.Down.Value == 4 && !(bool) ProEra.Game.MatchState.Turnover)
          {
            if (global::Game.IsPlayerOneOnOffense)
              ++ProEra.Game.MatchState.Stats.User.ThirdDownSuc;
            else
              ++ProEra.Game.MatchState.Stats.Comp.ThirdDownSuc;
          }
          ++ProEra.Game.MatchState.Stats.DriveFirstDowns;
          this.FieldManager.SetFirstDownAndFirstDownLine();
        }
        if ((!SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyOnPlay || !SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyAccepted || MatchManager.down == 5) && global::Game.PET_IsTouchdown && (bool) ProEra.Game.MatchState.Turnover)
        {
          if (global::Game.IsPlayerOneOnOffense)
          {
            PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Defense);
            this.matchManager.SetCurrentMatchState(EMatchState.UserOnDefense);
          }
          else
          {
            PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Offense);
            this.matchManager.SetCurrentMatchState(EMatchState.UserOnOffense);
          }
        }
        if ((!SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyOnPlay || !SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyAccepted || MatchManager.down == 5) && !(bool) ProEra.Game.MatchState.Turnover && (this.matchManager.onsideKick || this.playType == PlayType.Kickoff))
        {
          if (global::Game.IsPlayerOneOnOffense)
            PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Offense);
          else
            PlaybookState.SetPlaybookType.Trigger(EPlaybookType.Defense);
        }
        if (!SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyOnPlay || !SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyAccepted || MatchManager.down == 5)
        {
          if (global::Game.PET_IsTouchdown || global::Game.IsFG)
          {
            ProEra.Game.MatchState.Down.Value = 1;
            if (global::Game.PET_IsTouchdown)
            {
              if ((bool) ProEra.Game.MatchState.RunningPat)
              {
                this.matchManager.AddScore(2);
                if (global::Game.IsPlayerOneOnOffense)
                  ++ProEra.Game.MatchState.Stats.User.TwoPointConversions;
                else
                  ++ProEra.Game.MatchState.Stats.Comp.TwoPointConversions;
                if (!this.noKicking)
                {
                  this.matchManager.ChangePossession(ChangePosType.TeamScored, DriveEndType.Touchdown, false);
                  forceKickOff = true;
                  ProEra.Game.MatchState.IsKickoff.Value = true;
                }
                else
                {
                  ProEra.Game.MatchState.RunningPat.Value = false;
                  ProEra.Game.MatchState.Down.Value = 1;
                }
              }
              else
              {
                this.matchManager.checkForEndOfQuarter = false;
                if (global::Game.IsTurnover)
                  Field.FlipFieldDirection();
                ProEra.Game.MatchState.RunningPat.Value = true;
                this.matchManager.AddScore(RemoteConfigGameService.GetIntValue("DEBUG.PointsForTouchdown", 6));
                this.matchManager.SetBallOn(Field.DEFAULT_PAT_LOCATION);
                this.matchManager.SetBallHashPosition(0.0f);
                this.matchManager.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
                SingletonBehaviour<FieldManager, MonoBehaviour>.instance.lineOfScrimmage.SetActive(false);
                SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.SetActive(false);
                this.playersManager.SetAfterPlayActionsForAllPlayers();
              }
            }
            else if (global::Game.PET_IsMadeFG || (bool) ProEra.Game.MatchState.RunningPat)
            {
              this.matchManager.ChangePossession(ChangePosType.TeamScored, DriveEndType.FieldGoal, false);
              forceKickOff = true;
              ProEra.Game.MatchState.IsKickoff.Value = true;
            }
            else if (global::Game.PET_IsMissedFG)
              this.matchManager.ChangePossession(ChangePosType.AtPos, DriveEndType.MissedFieldGoal);
          }
          else if ((bool) ProEra.Game.MatchState.RunningPat || global::Game.PET_IsSafety)
          {
            if (!this.noKicking)
            {
              this.matchManager.ChangePossession(ChangePosType.TeamScored, DriveEndType.Safety, false);
              forceKickOff = true;
              ProEra.Game.MatchState.IsKickoff.Value = true;
              if ((UnityEngine.Object) this.playersManager != (UnityEngine.Object) null && (UnityEngine.Object) this.playersManager.ballHolderScript != (UnityEngine.Object) null && global::Game.PET_IsSafety)
                ScoreboardAnimations.PlayAnimation(ScoreboardAnimations.BoardAnimType.Safety, this.playersManager.ballHolderScript.teamIndex);
            }
            else
            {
              ProEra.Game.MatchState.Down.Value = 1;
              ProEra.Game.MatchState.RunningPat.Value = false;
            }
          }
          else if (PlayState.IsPunt && this.playersManager.ballWasThrownOrKicked)
          {
            if ((bool) ProEra.Game.MatchState.Turnover)
              this.matchManager.ChangePossession(ChangePosType.Punted, DriveEndType.Punt);
          }
          else if (PlayState.IsKickoff)
          {
            if ((bool) ProEra.Game.MatchState.Turnover)
              this.matchManager.ChangePossession(ChangePosType.KickedOff, DriveEndType.KickOff);
          }
          else if (((bool) ProEra.Game.MatchState.Turnover || ProEra.Game.MatchState.Down.Value == 5) && AppState.GameMode != EGameMode.k2MD)
          {
            ScoreClockState.DownAndDistanceVisible.SetValue(false);
            if (ProEra.Game.MatchState.Down.Value == 5)
            {
              int num = PlayState.IsRunOrPass ? 1 : 0;
            }
            this.matchManager.ChangePossession(ChangePosType.AtPos, DriveEndType.Failed4thDown);
          }
        }
        if (!SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyOnPlay || SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyOnPlay && !SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyAccepted)
          this.ApplyCurrentPlayStatsToPlayers();
        PenaltyType penaltyType = SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetPenaltyType();
        SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.ClearPenalty();
        this.ballManager.ResetColliderSize();
        this.playersManager.BallHolderReleaseBall();
        if (MatchManager.instance.IsSimulating && ProEra.Game.MatchState.Turnover.Value)
          this.ClearSimGraphics();
        ProEra.Game.MatchState.Turnover.Value = false;
        bool flag = false;
        if (MatchManager.instance.IsSimulating)
        {
          this.playersManager.SetAfterPlayActionsForAllPlayers();
          flag = true;
        }
        if (!GameTimeoutState.TimeoutCalledP1.Value && !GameTimeoutState.TimeoutCalledP2.Value || penaltyType == PenaltyType.DelayOfGame)
          this.CallNewPlayAfterEndPlay(forceKickOff);
        if (!flag)
          this.playersManager.SetAfterPlayActionsForAllPlayers();
        SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetDownMarkerTexture();
        if (PlayState.IsKickoff)
          ScoreClockState.DownAndDistanceVisible.SetValue(false);
        this.AddToCurrentPlayLog("Play is over - " + ScoreClockState.GetDownAndDistance(ProEra.Game.MatchState.IsHomeTeamOnOffense) + " from the " + Field.GetYardLineStringByFieldLocation(this.matchManager.savedLineOfScrim) + " yard line");
        this.AddToCurrentPlayLog("Score is " + PersistentData.GetUserTeam().GetAbbreviation() + " - " + ProEra.Game.MatchState.Stats.User.Score.ToString() + ", " + PersistentData.GetCompTeam().GetAbbreviation() + " - " + ProEra.Game.MatchState.Stats.Comp.Score.ToString());
        if (!PersistentSingleton<SaveManager>.Instance.gameSettings.LogGamesToFile)
          return;
        this.PlayLogger.Log(Utility.Logging.LogType.INFO, this.GameLog.CurrentPlayToString());
      }
    }
  }

  public void CallNewPlayAfterEndPlay(bool forceKickOff = false)
  {
    Debug.Log((object) nameof (CallNewPlayAfterEndPlay));
    if (this.ShouldOffenseHurryUp && this.SetPlayToLastPlay())
    {
      this.ShouldOffenseHurryUp = false;
    }
    else
    {
      this.ShouldOffenseHurryUp = false;
      if ((double) MatchManager.instance.timeManager.GetGameClockTimer() >= (double) ProEra.Game.MatchState.GameLength.Value && !this.matchManager.allowPATAfterTimeHasExpired && (!MatchManager.instance.timeManager.IsFourthQuarter() || !ProEra.Game.MatchState.Stats.EqualScore()))
        return;
      PlayDataOff offensivePlayForAi = this.GetOffensivePlayForAI();
      bool flag1 = (bool) ProEra.Game.MatchState.RunningPat && !this.ShouldOffenseGoForTwo();
      bool flag2 = offensivePlayForAi.GetPlayType() == PlayType.Run || offensivePlayForAi.GetPlayType() == PlayType.Pass;
      if (((global::Game.UserDoesNotCallPlays ? 1 : (ProEra.Game.MatchState.IsPlayerOneOnDefense ? 1 : 0)) | (flag1 ? 1 : 0) | (forceKickOff ? 1 : 0)) != 0 || global::Game.IsPlayerOneOnOffense && !flag2)
      {
        if (this.autoSelectPlayForAI)
          this.SelectNextPlaysForAI(offensivePlayForAi);
        else
          this.SelectNextOffPlayForUser(offensivePlayForAi);
      }
      else
        this.SelectNextOffPlayForUser(offensivePlayForAi);
    }
  }

  public void SetPlayOver(bool _playIsOver)
  {
    this.playOver = _playIsOver;
    PlayState.PlayOver.Value = _playIsOver;
  }

  public bool IsPlayOver() => this.playOver;

  public void ResetPlay() => this.SetupPlay(this.savedOffPlay, this.savedDefPlay, this.savedFlip);

  public bool ShouldAttemptOnsideKick()
  {
    KickoffConfig kickoffConfig = global::Game.KickoffConfig;
    int secondsRemainingInGame = MatchManager.instance.timeManager.GetTotalSecondsRemainingInGame();
    int offenseScoreDifference = ProEra.Game.MatchState.Stats.GetOffenseScoreDifference();
    if (-8 <= offenseScoreDifference && offenseScoreDifference < 0)
    {
      int num = GameTimeoutState.OffenseTimeouts < 2 ? 180 : 120;
      return secondsRemainingInGame < num;
    }
    if (-16 <= offenseScoreDifference && offenseScoreDifference < -8)
      return secondsRemainingInGame < 180;
    return -24 <= offenseScoreDifference && offenseScoreDifference < -16;
  }

  public void PickPlaysForAI_SpectateMode(PlayDataOff preselectedOffPlay = null)
  {
    if (ProEra.Game.MatchState.IsKickoff.Value)
    {
      bool flag = this.ShouldAttemptOnsideKick();
      this.savedOffPlay = flag ? Plays.self.spc_onsideKick : Plays.self.spc_kickoffMid;
      this.savedDefPlay = flag ? Plays.self.dspc_kickRetOnside : Plays.self.dspc_kickRetMid;
    }
    else
    {
      this.savedOffPlay = preselectedOffPlay ?? this.GetOffensivePlayForAI();
      this.savedDefPlay = this.GetDefensivePlayForAI(this.savedOffPlay);
    }
    ScoreClockState.Personnel.Value = this.savedOffPlay.GetFormation().GetPersonnel();
    PEGameplayEventManager.RecordPlaySelectedEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, MatchManager.instance.timeManager.GetQuarter(), global::Game.IsHomeTeamOnOffense, global::Game.OffenseGoingNorth, this.savedOffPlay, this.savedDefPlay, MatchManager.down, ProEra.Game.MatchState.FirstDown.Value, ProEra.Game.MatchState.BallOn.Value);
  }

  public void SelectNextOffPlayForUser(PlayDataOff preselectedPlay = null)
  {
    this.canUserCallAudible = false;
    System.Action userPlayCallStarted = this.OnUserPlayCallStarted;
    if (userPlayCallStarted != null)
      userPlayCallStarted();
    this.pickPlayForAICoroutine = this.StartCoroutine(this.DelayedSelectOffUserPlay(preselectedPlay));
  }

  private IEnumerator DelayedSelectOffUserPlay(PlayDataOff preselectedPlay)
  {
    PlayManager playManager = this;
    if (global::Game.IsPlayerOneOnOffense)
    {
      MatchManager.instance.IsSimulating = false;
      PlayDataOff p = (PlayDataOff) null;
      p = preselectedPlay == null ? (!global::Game.CoachCallsPlays || preselectedPlay != null ? playManager.savedOffPlay : playManager.GetOffensivePlayForAI()) : preselectedPlay;
      playManager.savedOffPlay = p;
      playManager.playType = playManager.savedOffPlay.GetPlayType();
      PlayState.PlayType.Value = playManager.playType;
      Debug.Log((object) ("Off Coord Picked: " + p.GetFormation().GetBaseFormationString() + ", " + p.GetFormation().GetSubFormationString() + ", " + p.GetPlayName()));
      playManager.MarkOffPlayersForSub(playManager.savedOffPlay.GetFormation());
      yield return (object) new WaitForSeconds(UnityEngine.Random.Range(playManager.userPlayCallMinDelay, playManager.userPlayCallMaxDelay));
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(playManager.\u003CDelayedSelectOffUserPlay\u003Eb__112_0));
      yield return (object) GamePlayerController.CameraFade.Fade();
      if (!global::Game.CurrentPlayHasUserQBOnField)
      {
        playManager.SetPlay((PlayData) p, false, false, true);
      }
      else
      {
        playManager.SubInOffPlayersForPlay(playManager.savedOffPlay.GetFormation());
        Debug.Log((object) ("Sub in players for Formation = " + playManager.savedOffPlay.GetFormation().GetBaseFormationString() + " "));
      }
      playManager.PositionPlayersAfterPlayCall();
      PlaybookState.CurrentFormation.SetValue(Plays.self.GetOffFormDataFromPlay(true, (PlayData) playManager.savedOffPlay));
      PlaybookState.CurrentPlay.SetValue((PlayData) playManager.savedOffPlay);
      System.Action userPlayCallMade = playManager.OnUserPlayCallMade;
      if (userPlayCallMade != null)
        userPlayCallMade();
      MatchManager.instance.playManager._userHuddlePlayConfirmed = false;
      MatchManager.instance.playManager._userAudiblePlayConfirmed = false;
      p = (PlayDataOff) null;
    }
    else
      Debug.LogError((object) " Should not be calling this method for user defense plays");
  }

  public void HandleUserPlayConfirmed(bool isAudible)
  {
    if (isAudible)
    {
      if (MatchManager.instance.playManager.UserAudiblePlayConfirmed)
        return;
      MatchManager.instance.playManager._userAudiblePlayConfirmed = true;
      this.waitToHandlePlayConfirmationCoroutine = this.StartCoroutine(this.WaitToHandlePlayConfirmation(true));
    }
    else
    {
      if (MatchManager.instance.playManager.UserHuddlePlayConfirmed)
        return;
      MatchManager.instance.playManager._userHuddlePlayConfirmed = true;
      this.waitToHandlePlayConfirmationCoroutine = this.StartCoroutine(this.WaitToHandlePlayConfirmation(false));
    }
  }

  private IEnumerator WaitToHandlePlayConfirmation(bool isAudible)
  {
    if (isAudible)
    {
      FormationData formationData = PlaybookState.CurrentFormation.Value;
      PlayData play = formationData?.GetPlay(PlaybookState.CurrentPlayIndex.Value);
      if (play == null)
      {
        Debug.LogError((object) string.Format("Null formation or play not found {0}, {1}", (object) (formationData != null), (object) PlaybookState.CurrentPlayIndex.Value));
        yield break;
      }
      else
      {
        PlaybookState.CurrentPlay.SetValue(play);
        Debug.Log((object) ("Audible called: " + play.GetFormation().GetBaseFormationString() + ", " + play.GetFormation().GetSubFormationString() + ", " + play.GetPlayName()));
        this.SetPlay(play, false, true, true);
      }
    }
    else
    {
      yield return (object) GamePlayerController.CameraFade.Fade();
      Debug.Log((object) ("Confirmed play: " + this.savedOffPlay.GetFormation().GetBaseFormationString() + ", " + this.savedOffPlay.GetFormation().GetSubFormationString() + ", " + this.savedOffPlay.GetPlayName()));
      this.SetPlay((PlayData) this.savedOffPlay, false, false, true);
      PlaybookState.CurrentFormation.SetValue(Plays.self.GetOffFormDataFromPlay(true, (PlayData) this.savedOffPlay));
      PlaybookState.CurrentPlay.SetValue((PlayData) this.savedOffPlay);
      MatchManager.instance.playersManager.BreakHuddle();
      MatchManager.instance.playersManager.PutAllPlayersInPlayPosition();
    }
    System.Action userPlayCallEnded = this.OnUserPlayCallEnded;
    if (userPlayCallEnded != null)
      userPlayCallEnded();
  }

  public void SelectNextPlaysForAI(PlayDataOff preselectedOffPlay = null)
  {
    this.StopAIPickPlayCoroutine();
    this.pickPlayForAICoroutine = this.StartCoroutine(this.SelectNextPlaysForAITeams(preselectedOffPlay));
  }

  private IEnumerator SelectNextPlaysForAITeams(PlayDataOff preselectedOffPlay)
  {
    ScoreClockState.SetYardLine.Trigger();
    ScoreClockState.PlayClockVisible.Value = true;
    MatchManager.instance.timeManager.SetRunPlayClock(true);
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetDownMarkerTexture();
    bool flag1 = false;
    if (MatchManager.instance.timeManager.IsFirstQuarter() && (bool) ProEra.Game.MatchState.IsKickoff && MatchManager.instance.timeManager.GetDisplaySeconds() + 60 * MatchManager.instance.timeManager.GetDisplayMinutes() == ProEra.Game.MatchState.GameLength.Value)
      flag1 = true;
    if (MatchManager.instance.timeManager.IsGameClockRunning() && (MatchManager.instance.timeManager.IsSecondQuarter() || MatchManager.instance.timeManager.IsFourthQuarter()))
    {
      int score1 = ProEra.Game.MatchState.Stats.User.Score;
      int score2 = ProEra.Game.MatchState.Stats.Comp.Score;
      int num1 = global::Game.IsPlayerOneOnOffense ? score1 : score2;
      int num2 = (global::Game.IsPlayerOneOnOffense ? score2 : score1) - num1;
      bool flag2 = num2 > 0;
      bool flag3 = Field.FurtherDownfield(MatchManager.ballOn, Field.MIDFIELD);
      int num3 = num2 * 10;
      int num4 = MatchManager.instance.timeManager.GetDisplaySeconds() + 60 * MatchManager.instance.timeManager.GetDisplayMinutes();
      if (MatchManager.instance.timeManager.IsFourthQuarter() & flag2 && num4 < num3)
        flag1 = true;
      else if (((!MatchManager.instance.timeManager.IsSecondQuarter() ? 0 : (num4 < 60 ? 1 : 0)) & (flag3 ? 1 : 0)) != 0)
        flag1 = true;
    }
    MatchManager.instance.SaveMatchStateBeforePlay();
    this.PickPlaysForAI_SpectateMode(preselectedOffPlay);
    this.MarkOffPlayersForSub(this.savedOffPlay.GetFormation());
    this.MarkDefPlayersForSub((FormationPositions) this.savedDefPlay.GetFormation());
    if (!MatchManager.instance.IsSimulating)
    {
      if (flag1)
      {
        yield return (object) new WaitForSeconds(UnityEngine.Random.Range(2f, 3f));
      }
      else
      {
        int timeBetweenPlays = PersistentSingleton<SaveManager>.Instance.gameSettings.TimeBetweenPlays;
        yield return (object) new WaitForSeconds((float) UnityEngine.Random.Range(timeBetweenPlays - 1, timeBetweenPlays));
      }
      yield return (object) GamePlayerController.CameraFade.Fade();
    }
    this.SetupPlay(this.savedOffPlay, this.savedDefPlay, false, playerCallingAudible: 0);
    MatchManager.instance.playersManager.BreakHuddle();
    MatchManager.instance.AllowSnap();
    this.PositionPlayersAfterPlayCall();
    if (!global::Game.IsPlayerOneOnOffense)
    {
      MatchManager.instance.SimulatePlay();
      if (!MatchManager.instance.IsSimulating)
        this.ClearSimGraphics();
    }
    else
    {
      MatchManager.instance.IsSimulating = false;
      this.ClearSimGraphics();
      Debug.Log((object) ("MatchManager.down: " + MatchManager.down.ToString()));
      if (MatchManager.down == 4)
        PEGameplayEventManager.RecordPlayCallMadeEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position);
    }
  }

  private void PositionPlayersAfterPlayCall()
  {
    this.ballManager.FreezeAfterPlay();
    if (global::Game.IsNotKickoff && (global::Game.IsNotRunningPAT || MatchManager.instance.playManager.savedOffPlay.GetPlayType() != PlayType.FG))
    {
      this.ResetPlayLines();
      if (global::Game.IsPlayerOneOnOffense && global::Game.IsNotFG && global::Game.IsNotPunt)
        MatchManager.instance.playersManager.ForcePlayersToHuddleAfterPlay();
      else
        MatchManager.instance.playersManager.PutAllPlayersInPlayPosition();
    }
    else
      MatchManager.instance.playersManager.PutAllPlayersInPlayPosition();
  }

  public void ClearSimGraphics()
  {
    for (int index = 0; index < 11; ++index)
    {
      global::Game.OffensivePlayers[index].ShowPlayerAvatar();
      global::Game.DefensivePlayers[index].ShowPlayerAvatar();
    }
    if (!(bool) (UnityEngine.Object) OnFieldCanvas.Instance)
      return;
    OnFieldCanvas.Instance.HideSimulatedPlayLines();
  }

  public void StopAIPickPlayCoroutine()
  {
    if (this.pickPlayForAICoroutine == null)
      return;
    this.StopCoroutine(this.pickPlayForAICoroutine);
  }

  public void StopWaitToHandlePlayConfirmationCoroutine()
  {
    if (this.waitToHandlePlayConfirmationCoroutine == null)
      return;
    this.StopCoroutine(this.waitToHandlePlayConfirmationCoroutine);
  }

  public PlayDataOff GetOffensivePlayForAI()
  {
    PEGameplayEventManager.RecordAboutToCallOffPlayEvent();
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.alwaysKickFieldGoals)
      return Plays.self.spc_fieldGoal;
    int score1 = ProEra.Game.MatchState.Stats.User.Score;
    int score2 = ProEra.Game.MatchState.Stats.Comp.Score;
    int quarter = MatchManager.instance.timeManager.GetQuarter();
    int num1 = global::Game.IsPlayerOneOnOffense ? score1 : score2;
    int num2 = (global::Game.IsPlayerOneOnOffense ? score2 : score1) - num1;
    int num3 = global::Game.IsPlayerOneOnOffense ? GameTimeoutState.UserTimeouts.Value : GameTimeoutState.CompTimeouts.Value;
    int num4 = global::Game.IsPlayerOneOnDefense ? GameTimeoutState.UserTimeouts.Value : GameTimeoutState.CompTimeouts.Value;
    int displayMinutes = MatchManager.instance.timeManager.GetDisplayMinutes();
    int num5 = MatchManager.instance.timeManager.GetDisplaySeconds() + 60 * displayMinutes;
    int playClockTime = MatchManager.instance.timeManager.GetPlayClockTime();
    Field.GetYardLineByFieldLocation(MatchManager.ballOn);
    int yardToGo = this.GetYardToGo();
    int yards1 = Field.ConvertDistanceToYards(Mathf.Abs(Field.OFFENSIVE_GOAL_LINE - MatchManager.ballOn));
    int down = MatchManager.down;
    int kickPower = ProEra.Game.MatchState.IsHomeTeamOnOffense ? PersistentData.GetHomeTeamData().TeamDepthChart.GetStartingKicker().KickPower : PersistentData.GetAwayTeamData().TeamDepthChart.GetStartingKicker().KickPower;
    bool flag1 = MatchManager.instance.timeManager.IsGameClockRunning();
    int fieldGoalRange = Field.GetFieldGoalRange(kickPower);
    int yards2 = Field.ConvertDistanceToYards(Mathf.Abs(Field.OFFENSIVE_GOAL_LINE - MatchManager.ballOn));
    bool flag2 = yards2 < fieldGoalRange;
    int num6 = SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyOnPlay ? 1 : 0;
    bool flag3 = num2 > 0;
    bool flag4 = num2 < 0;
    bool flag5 = Field.FurtherDownfield(MatchManager.ballOn, Field.MIDFIELD);
    int num7 = num2 * 10;
    PlayManager.PlayFilter<PlayDataOff> playFilter = (PlayManager.PlayFilter<PlayDataOff>) null;
    if (AppState.GameMode == EGameMode.k2MD)
      playFilter = (PlayManager.PlayFilter<PlayDataOff>) (play => play.GetFormation().GetBaseFormation() == BaseFormation.Shotgun);
    else if (quarter == 1)
      playFilter = (PlayManager.PlayFilter<PlayDataOff>) (play => play.GetFormation().GetBaseFormation() == BaseFormation.Shotgun || play.GetFormation().GetBaseFormation() == BaseFormation.Pistol);
    else if (quarter == 2 && displayMinutes < 3 || quarter >= 4)
      playFilter = (PlayManager.PlayFilter<PlayDataOff>) (play => play.GetFormation().GetBaseFormation() == BaseFormation.Shotgun);
    if (ProEra.Game.MatchState.IsKickoff.Value)
      return flag3 && MatchManager.instance.timeManager.IsFourthQuarter() && MatchManager.instance.timeManager.GetDisplayMinutes() < 1 ? Plays.self.spc_onsideKick : Plays.self.spc_kickoffMid;
    if ((bool) ProEra.Game.MatchState.RunningPat)
    {
      if (!this.ShouldOffenseGoForTwo() && !this.noKicking)
        return Plays.self.spc_fieldGoal;
      return !this.Randomizer(50) ? this.SelectShortPassPlay(playFilter) : this.SelectInsideRunPlay(playFilter);
    }
    if (yards2 <= 2 && UnityEngine.Random.Range(0, 100) < 75)
      return displayMinutes < 2 && (quarter == 2 || quarter == 4 || quarter == 5) ? this.SelectGoallinePassPlay(playFilter) : this.SelectGoallineRunPlay(playFilter);
    if (quarter == 2 && displayMinutes < 2)
    {
      if (!flag5)
      {
        switch (down)
        {
          case 1:
          case 2:
            return this.SelectPassPlay(20, 60, playFilter);
          case 3:
            if (yardToGo <= 3)
              return this.SelectShortPassPlay(playFilter);
            return !this.Randomizer(50) ? this.SelectOutsideRunPlay(playFilter) : this.SelectInsideRunPlay(playFilter);
          default:
            return Plays.self.spc_puntProtect;
        }
      }
      else
      {
        if (num5 < 10)
          return !flag2 ? (PlayDataOff) Plays.self.hailMaryPlays_Normal.GetPlay(0) : Plays.self.spc_fieldGoal;
        if (down == 4)
        {
          if (flag2)
            return Plays.self.spc_fieldGoal;
          return yardToGo < 2 ? this.SelectShortPassPlay(playFilter) : Plays.self.spc_puntProtect;
        }
        return yardToGo < 4 ? (num5 > 60 && this.Randomizer(40) ? this.SelectInsideRunPlay(playFilter) : this.SelectShortPassPlay(playFilter)) : (yardToGo < 10 ? this.SelectPassPlay(40, 80, playFilter) : this.SelectPassPlay(0, 60, playFilter));
      }
    }
    else
    {
      if (((quarter == 4 || quarter == 5 ? (num5 < 10 ? 1 : 0) : 0) & (flag3 ? 1 : 0)) != 0)
      {
        int num8 = !flag2 ? 1 : (num2 > 3 ? 1 : 0);
        bool flag6 = num2 <= 8;
        bool flag7 = (double) yards1 > 30.0;
        int num9 = flag6 ? 1 : 0;
        if ((num8 & num9 & (flag7 ? 1 : 0)) != 0)
          return (PlayDataOff) Plays.self.hailMaryPlays_Normal.GetPlay(0);
      }
      if (quarter == 4 && displayMinutes < 3 && !flag4 && !this.noKicking)
      {
        if (down == 4)
        {
          if (flag2 && num2 <= 3 || flag2 && num2 > 8 && num2 <= 11 || flag2 && num2 <= 6 && displayMinutes > 2)
            return Plays.self.spc_fieldGoal;
          if (num2 <= 8 && displayMinutes > 2 && !flag2 || num2 == 0 && !flag2)
            return Plays.self.spc_puntProtect;
          return yardToGo < 4 ? this.SelectPassPlay(50, 90, playFilter) : this.SelectPassPlay(0, 60, playFilter);
        }
        if (flag2 && num2 <= 3)
        {
          if (num5 < 15)
            return Plays.self.spc_fieldGoal;
          if (num2 < 3)
            return !this.Randomizer(75) ? this.SelectOutsideRunPlay(playFilter) : this.SelectInsideRunPlay(playFilter);
          if (yardToGo >= 4)
            return this.SelectPassPlay(35, 70, playFilter);
          return !this.Randomizer(50) ? this.SelectShortPassPlay(playFilter) : this.SelectInsideRunPlay(playFilter);
        }
        if (yards1 < 20 && num2 <= 8)
        {
          bool flag8 = num5 > 45;
          return yardToGo < 4 || down == 1 || down == 2 ? (!flag8 ? this.SelectShortPassPlay(playFilter) : this.SelectInsideRunPlay(playFilter)) : (yardToGo < 8 ? this.SelectPassPlay(50, 100, playFilter) : this.SelectPassPlay(0, 50, playFilter));
        }
        if (num5 < 30 && num3 == 0 && MatchManager.instance.timeManager.IsGameClockRunning() && (down == 1 || down == 2 || down == 3))
          return (PlayDataOff) Plays.self.clockManagementPlays.GetPlay(1);
        if (num5 < num7)
          return this.SelectPassPlay(0, 50, playFilter);
        if (yardToGo < 4)
          return this.SelectPassPlay(70, 100, playFilter);
        return yardToGo < 8 ? this.SelectPassPlay(30, 70, playFilter) : this.SelectPassPlay(0, 50, playFilter);
      }
      if (((quarter != 4 ? 0 : (displayMinutes < 3 ? 1 : 0)) & (flag4 ? 1 : 0)) != 0 && !this.noKicking)
      {
        if (down == 4)
          return !flag2 ? Plays.self.spc_puntProtect : Plays.self.spc_fieldGoal;
        int num10 = (4 - down) * 35 + (flag1 ? playClockTime : 0);
        int num11 = 35 * num4;
        if (num5 < num10 - num11)
          return (PlayDataOff) Plays.self.clockManagementPlays.GetPlay(0);
        if (num5 < num7)
          return !this.Randomizer(75) ? this.SelectOutsideRunPlay(playFilter) : this.SelectInsideRunPlay(playFilter);
        if (num5 < Mathf.Abs(num7))
          return !this.Randomizer(75) ? this.SelectOutsideRunPlay(playFilter) : this.SelectInsideRunPlay(playFilter);
      }
      if (quarter == 5 & flag3 && down == 4)
      {
        if (flag2 && num2 <= 3)
          return Plays.self.spc_fieldGoal;
        if (yardToGo < 4)
          return this.SelectPassPlay(70, 100, playFilter);
        return yardToGo < 8 ? this.SelectPassPlay(30, 70, playFilter) : this.SelectPassPlay(0, 50, playFilter);
      }
      if (down == 4 && !this.noKicking)
      {
        if (flag2)
          return Plays.self.spc_fieldGoal;
        int num12 = 50;
        bool flag9 = false;
        if (flag5 && yardToGo < 2 && UnityEngine.Random.Range(0, 100) < num12)
          flag9 = true;
        if (!flag9)
          return Plays.self.spc_puntProtect;
      }
      switch (PersistentData.GetOffensiveTeamData().PlayCalling.SelectPlayConcept_Offense(down, yardToGo))
      {
        case PlayConcept.Inside_Run:
          return this.SelectInsideRunPlay(playFilter);
        case PlayConcept.Mid_Pass:
          return this.SelectMidPassPlay(playFilter);
        case PlayConcept.Deep_Pass:
          return this.SelectDeepPassPlay(playFilter);
        case PlayConcept.Play_Action:
          return this.SelectPlayActionPlay(playFilter);
        case PlayConcept.QB_Keeper:
          return this.SelectQbKeeperPlay(playFilter);
        case PlayConcept.Read_Option:
          return this.SelectReadOptionPlay(playFilter);
        default:
          return this.SelectShortPassPlay(playFilter);
      }
    }
  }

  public PlayDataDef GetDefensivePlayForAI(PlayDataOff offensivePlay = null)
  {
    IEnumerable<PlayDataDef> playDataDefs = Enumerable.Empty<PlayDataDef>();
    DefensivePlayCallingConfig playCallingConfig = global::Game.DefensivePlayCallingConfig;
    PlayConcept[] playConceptArray1 = new PlayConcept[4]
    {
      PlayConcept.Cover_Two,
      PlayConcept.Cover_Three,
      PlayConcept.Cover_Four,
      PlayConcept.Man_Coverage
    };
    PlayConcept[] playConceptArray2 = new PlayConcept[7]
    {
      PlayConcept.Cover_Two,
      PlayConcept.Cover_Three,
      PlayConcept.Cover_Four,
      PlayConcept.Man_Coverage,
      PlayConcept.Man_Zone_Double,
      PlayConcept.Man_Blitz,
      PlayConcept.Zone_Blitz
    };
    foreach (PlayConcept concept in (double) Field.GetDistanceDownfield(Field.OFFENSIVE_GOAL_LINE, (float) ProEra.Game.MatchState.BallOn) < (double) playCallingConfig.GetMaxDistanceFromGoalLineForManCoverage() ? playConceptArray2 : playConceptArray1)
      playDataDefs = playDataDefs.Concat<PlayDataDef>((IEnumerable<PlayDataDef>) Plays.self.GetAllDefensivePlaysByConcept(concept, global::Game.IsPlayerOneOnDefense));
    PlayManager.PlayFilter<PlayDataDef> filter = (PlayManager.PlayFilter<PlayDataDef>) null;
    if (offensivePlay != null)
    {
      int receiversInFormation = offensivePlay.GetFormation().GetReceiversInFormation();
      filter = receiversInFormation < playCallingConfig.minReceiversForDime ? (receiversInFormation < playCallingConfig.minReceiversForNickel ? (PlayManager.PlayFilter<PlayDataDef>) (play => play.GetFormation().GetBaseFormation() != BaseFormation.Nickel && play.GetFormation().GetBaseFormation() != BaseFormation.Dime) : (PlayManager.PlayFilter<PlayDataDef>) (play => play.GetFormation().GetBaseFormation() == BaseFormation.Nickel)) : (PlayManager.PlayFilter<PlayDataDef>) (play => play.GetFormation().GetBaseFormation() == BaseFormation.Dime);
    }
    else
      Debug.Log((object) "GetDefensivePlayForAI was called before the offense had chosen their play, so personnel will not be taken into account.");
    return this.SelectPlayFromList<PlayDataDef>(this.AdjustDefensivePlayListForUserQB(playDataDefs.ToList<PlayDataDef>()), filter);
  }

  private List<PlayDataDef> AdjustDefensivePlayListForUserQB(List<PlayDataDef> possiblePlays)
  {
    List<PlayDataDef> source = possiblePlays;
    if (global::Game.CurrentPlayHasUserQBOnField)
    {
      int rushYards = this.playersManager.userTeamData.GetPlayer(PersistentData.GetUserTeam().TeamDepthChart.GetStartingQB().IndexOnTeam).CurrentGameStats.RushYards;
      if (rushYards > 10)
      {
        float num = MathUtils.MapRange((float) rushYards, 10f, 40f, 0.25f, 0.9f);
        if ((double) UnityEngine.Random.Range(0.0f, 1f) < (double) num)
          source = this.ApplyPlayFilter<PlayDataDef>(source.ToList<PlayDataDef>(), (PlayManager.PlayFilter<PlayDataDef>) (play => Plays.self.GetPlayCategory_QbSpy(global::Game.IsPlayerOneOnDefense).Contains(play)));
      }
    }
    return source;
  }

  public List<T> ApplyPlayFilter<T>(List<T> playList, PlayManager.PlayFilter<T> filter) where T : PlayData
  {
    List<T> objList = playList;
    if (filter != null)
    {
      IEnumerable<T> source = playList.Where<T>((Func<T, bool>) (play => filter(play)));
      if (source.Any<T>())
        objList = source.ToList<T>();
    }
    return objList;
  }

  private T SelectPlayFromList<T>(List<T> playList, PlayManager.PlayFilter<T> filter) where T : PlayData
  {
    List<T> objList = this.ApplyPlayFilter<T>(playList, filter);
    int index = UnityEngine.Random.Range(0, objList.Count);
    return objList[index];
  }

  private PlayDataOff SelectInsideRunPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_InsideRun(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectOutsideRunPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_OutsideRun(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectQbKeeperPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_QbKeeper(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectReadOptionPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_ReadOption(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectShortPassPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_ShortPass(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectMidPassPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_MidPass(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectDeepPassPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_DeepPass(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectScreenPassPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_ScreenPass(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectPlayActionPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_PlayAction(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectGoallineRunPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_GoalLineRun(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectGoallinePassPlay(PlayManager.PlayFilter<PlayDataOff> playFilter) => this.SelectPlayFromList<PlayDataOff>(Plays.self.GetPlayCategory_GoalLinePass(global::Game.IsPlayerOneOnOffense), playFilter);

  private PlayDataOff SelectPassPlay(
    int shortPassThreshold,
    int midPassThreshold,
    PlayManager.PlayFilter<PlayDataOff> playFilter)
  {
    int num = UnityEngine.Random.Range(0, 100);
    if (num < shortPassThreshold)
      return this.SelectShortPassPlay(playFilter);
    return num < midPassThreshold ? this.SelectMidPassPlay(playFilter) : this.SelectDeepPassPlay(playFilter);
  }

  private PlayDataDef SelectManCoveragePlay(int yardsToGo)
  {
    if (yardsToGo < TeamPlayCalling.CLOSE)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManCoverage_Close(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ManCoverage_Close(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.SHORT)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManCoverage_Short(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ManCoverage_Short(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.MEDIUM)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManCoverage_Medium(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ManCoverage_Medium(global::Game.IsPlayerOneOnDefense)[index];
    }
    int index1 = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManCoverage_Long(global::Game.IsPlayerOneOnDefense).Count);
    return Plays.self.GetPlayCategory_ManCoverage_Long(global::Game.IsPlayerOneOnDefense)[index1];
  }

  private PlayDataDef SelectManZoneDoublePlay(int yardsToGo)
  {
    if (yardsToGo < TeamPlayCalling.CLOSE)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManZoneDouble_Close(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ManZoneDouble_Close(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.SHORT)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManZoneDouble_Short(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ManZoneDouble_Short(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.MEDIUM)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManZoneDouble_Medium(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ManZoneDouble_Medium(global::Game.IsPlayerOneOnDefense)[index];
    }
    int index1 = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManZoneDouble_Long(global::Game.IsPlayerOneOnDefense).Count);
    return Plays.self.GetPlayCategory_ManZoneDouble_Long(global::Game.IsPlayerOneOnDefense)[index1];
  }

  private PlayDataDef SelectCover2Play(int yardsToGo)
  {
    if (yardsToGo < TeamPlayCalling.CLOSE)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover2_Close(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_Cover2_Close(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.SHORT)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover2_Short(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_Cover2_Short(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.MEDIUM)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover2_Medium(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_Cover2_Medium(global::Game.IsPlayerOneOnDefense)[index];
    }
    int index1 = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover2_Long(global::Game.IsPlayerOneOnDefense).Count);
    return Plays.self.GetPlayCategory_Cover2_Long(global::Game.IsPlayerOneOnDefense)[index1];
  }

  private PlayDataDef SelectCover3Play(int yardsToGo)
  {
    if (yardsToGo < TeamPlayCalling.CLOSE)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover3_Close(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_Cover3_Close(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.SHORT)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover3_Short(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_Cover3_Short(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.MEDIUM)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover3_Medium(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_Cover3_Medium(global::Game.IsPlayerOneOnDefense)[index];
    }
    int index1 = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover3_Long(global::Game.IsPlayerOneOnDefense).Count);
    return Plays.self.GetPlayCategory_Cover3_Long(global::Game.IsPlayerOneOnDefense)[index1];
  }

  private PlayDataDef SelectCover4Play(int yardsToGo)
  {
    if (yardsToGo < TeamPlayCalling.CLOSE)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover4_Close(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_Cover4_Close(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.SHORT)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover4_Short(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_Cover4_Short(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.MEDIUM)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover4_Medium(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_Cover4_Medium(global::Game.IsPlayerOneOnDefense)[index];
    }
    int index1 = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_Cover4_Long(global::Game.IsPlayerOneOnDefense).Count);
    return Plays.self.GetPlayCategory_Cover4_Long(global::Game.IsPlayerOneOnDefense)[index1];
  }

  private PlayDataDef SelectManBlitzPlay(int yardsToGo)
  {
    if (yardsToGo < TeamPlayCalling.CLOSE)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManBlitz_Close(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ManBlitz_Close(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.SHORT)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManBlitz_Short(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ManBlitz_Short(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.MEDIUM)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManBlitz_Medium(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ManBlitz_Medium(global::Game.IsPlayerOneOnDefense)[index];
    }
    int index1 = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ManBlitz_Long(global::Game.IsPlayerOneOnDefense).Count);
    return Plays.self.GetPlayCategory_ManBlitz_Long(global::Game.IsPlayerOneOnDefense)[index1];
  }

  private PlayDataDef SelectZoneBlitzPlay(int yardsToGo)
  {
    if (yardsToGo < TeamPlayCalling.CLOSE)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ZoneBlitz_Close(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ZoneBlitz_Close(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.SHORT)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ZoneBlitz_Short(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ZoneBlitz_Short(global::Game.IsPlayerOneOnDefense)[index];
    }
    if (yardsToGo < TeamPlayCalling.MEDIUM)
    {
      int index = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ZoneBlitz_Medium(global::Game.IsPlayerOneOnDefense).Count);
      return Plays.self.GetPlayCategory_ZoneBlitz_Medium(global::Game.IsPlayerOneOnDefense)[index];
    }
    int index1 = UnityEngine.Random.Range(0, Plays.self.GetPlayCategory_ZoneBlitz_Long(global::Game.IsPlayerOneOnDefense).Count);
    return Plays.self.GetPlayCategory_ZoneBlitz_Long(global::Game.IsPlayerOneOnDefense)[index1];
  }

  private bool ShouldOffenseGoForTwo()
  {
    TwoPointConfig twoPointConfig = global::Game.TwoPointConfig;
    if ((double) MatchManager.instance.timeManager.GetTotalSecondsRemainingInGame() >= (double) twoPointConfig.maxSecondsLeftInGame)
      return false;
    int offenseScoreDifference = ProEra.Game.MatchState.Stats.GetOffenseScoreDifference();
    return ((IEnumerable<int>) twoPointConfig.pointDifferentials).Contains<int>(offenseScoreDifference);
  }

  private int GetYardToGo() => Field.ConvertDistanceToYards(Mathf.Abs((!Field.FurtherDownfield(MatchManager.firstDown, Field.OFFENSIVE_GOAL_LINE) ? MatchManager.firstDown : Field.OFFENSIVE_GOAL_LINE) - MatchManager.ballOn));

  private bool Randomizer(int threshold) => UnityEngine.Random.Range(0, 100) < threshold;

  public static DropbackType GetDropbackType(PlayDataOff play)
  {
    if (global::Game.IsPlayAction)
      return DropbackType.OneStep;
    return (FieldState.IsBallInOpponentTerritory() ? 0 : (Field.GetYardLineByFieldLocation((float) ProEra.Game.MatchState.BallOn) < 10 ? 1 : 0)) == 0 ? play.GetDropbackType() : DropbackType.ThreeStep;
  }

  public void ShowPlaysOfType(EShowPlayType type)
  {
    if (type == EShowPlayType.Kickoff)
    {
      this.ShowKickoffPlays();
    }
    else
    {
      if (type != EShowPlayType.KickReturn)
        return;
      this.ShowKickReturnPlays();
    }
  }

  public static FormationData GetFormationByName(
    string checkName,
    List<FormationData> formationList)
  {
    FormationData formationByName = (FormationData) null;
    foreach (FormationData formation in formationList)
    {
      formation.GetName();
      if (formation.GetName().Contains(checkName))
      {
        formationByName = formation;
        break;
      }
    }
    return formationByName;
  }

  public static FormationData GetFormationByNameWithSubFormation(
    string checkFormationName,
    string checkSubFormationName,
    List<FormationData> formationList)
  {
    FormationData withSubFormation = (FormationData) null;
    foreach (FormationData formation in formationList)
    {
      formation.GetName();
      if (formation.GetName().Contains(checkFormationName) && formation.GetSubFormationName().Contains(checkSubFormationName))
      {
        withSubFormation = formation;
        break;
      }
    }
    return withSubFormation;
  }

  public static FormationData GetFormationByBaseName(
    string checkName,
    List<FormationData> formationList)
  {
    FormationData formationByBaseName = (FormationData) null;
    foreach (FormationData formation in formationList)
    {
      if (formation.GetBaseFormationName().Contains(checkName))
      {
        formationByBaseName = formation;
        break;
      }
    }
    return formationByBaseName;
  }

  public static PlayData GetFormationPlayByName(string checkName, FormationData f)
  {
    PlayData formationPlayByName = (PlayData) null;
    foreach (PlayData play in f.GetPlays())
    {
      if (play.GetPlayName().Contains(checkName))
      {
        formationPlayByName = play;
        break;
      }
    }
    return formationPlayByName;
  }

  public void RunHurryUp()
  {
    if (global::Game.IsPlayActive || !global::Game.IsPlayerOneOnOffense || !global::Game.CanUserRunHurryUp())
      return;
    this.ShouldOffenseHurryUp = true;
  }

  public void SetPlayCallingEnabled(bool enable) => this.playCallingEnabled = enable;

  public void SetOnboardingPlay(int index)
  {
    MatchManager.instance.playManager.savedOffPlay = (PlayDataOff) PlaybookState.CurrentFormation.Value.GetPlay(index);
    PlaybookState.CurrentPlay.SetValue((PlayData) MatchManager.instance.playManager.savedOffPlay);
    MatchManager.instance.playManager.canUserCallAudible = false;
    MatchManager.instance.playManager._userHuddlePlayConfirmed = false;
  }

  public delegate bool PlayFilter<T>(T play) where T : PlayData;
}
