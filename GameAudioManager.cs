// Decompiled with JetBrains decompiler
// Type: GameAudioManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.Data;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12;
using TB12.UI;
using UDB;
using UnityEngine;
using Vars;

public class GameAudioManager : MonoBehaviour
{
  [SerializeField]
  private PlayersManager _playersManager;
  [SerializeField]
  private PlayerProfile _playerProfile;
  private EPlayerEmotions _currentPlayerEmotions;
  private bool _didAnnouncePlayEnd;
  private bool _canSayFirstDown;
  private bool _wasIntercepted;
  private bool _wasSacked;
  private float _crowdStareTimer;
  private readonly LinksHandler _linksHandler = new LinksHandler();
  private bool _gameStarted;
  private PlayDataOff _currentPlay;
  private bool _malePlayer;
  private bool _didOpeningKickoff;
  private bool[] _passingYardChecks = new bool[3];
  private int _puntUpByOneCount;
  private bool _calledAudible;
  private bool _isChatterAllowed;
  private bool _didPlayDefense = true;
  private bool _didCrowdChant = true;
  private float _crowdExtraReactionLevel;
  private const int THREE_SECONDS = 3;
  private const int FIVE_SECONDS = 5;
  private const int THIRTY_SECONDS = 30;
  private const int TWO_MINUTES = 120;
  private const int THREE_MINUTES = 180;
  private const int FOUR_MINUTES = 240;

  public event System.Action PlayerChatterFinished;

  public event System.Action CoachChatterFinished;

  private void Awake()
  {
    PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);
    PEGameplayEventManager.PlayerOnSideline.OnValueChanged += new Action<bool>(this.HandleSidelineSwitch);
    this.PlayerChatterFinished += new System.Action(this.FinishedPlayerChatter);
    this.CoachChatterFinished += new System.Action(this.FinishedCoachChatter);
    MatchManager.instance.LastPlayYardsGained.OnValueChanged += new Action<int>(this.OnYardageChangeUpdate);
    ProEra.Game.MatchState.Down.OnValueChanged += new Action<int>(this.HandleDownChanged);
    this._linksHandler.SetLinks(new List<EventHandle>()
    {
      ProEra.Game.MatchState.CurrentMatchState.Link<EMatchState>(new Action<EMatchState>(this.HandleMatchStateChanged)),
      PlaybookState.ShowPlaybook.Link(new System.Action(this.ShowPlaybookHandler)),
      VREvents.PlayerHitGround.Link(new System.Action(this.HandlePlayerHitGround))
    });
    this._currentPlayerEmotions = EPlayerEmotions.kNeutral;
    this._malePlayer = this._playerProfile.Customization.BodyType.Value == EBodyType.Male;
  }

  private void OnDestroy()
  {
    this._linksHandler.Clear();
    PEGameplayEventManager.OnEventOccurred -= new Action<PEGameplayEvent>(this.HandleGameEvent);
    PEGameplayEventManager.PlayerOnSideline.OnValueChanged -= new Action<bool>(this.HandleSidelineSwitch);
    this.PlayerChatterFinished -= new System.Action(this.FinishedPlayerChatter);
    this.CoachChatterFinished -= new System.Action(this.FinishedCoachChatter);
    if (MatchManager.Exists())
      MatchManager.instance.LastPlayYardsGained.OnValueChanged -= new Action<int>(this.OnYardageChangeUpdate);
    ProEra.Game.MatchState.Down.OnValueChanged -= new Action<int>(this.HandleDownChanged);
    AppSounds.StopPlayerChatter();
    AppSounds.StopSideline();
  }

  private void Update()
  {
    if (!(bool) PEGameplayEventManager.PlayerOnSideline || (EMatchState) (Variable<EMatchState>) ProEra.Game.MatchState.CurrentMatchState != EMatchState.UserOnDefense && (EMatchState) (Variable<EMatchState>) ProEra.Game.MatchState.CurrentMatchState != EMatchState.UserOnOffense)
      return;
    if ((double) Vector3.Dot(PlayerCamera.Camera.transform.forward, (Vector3.zero - PlayerCamera.Camera.transform.position.SetY(0.0f)).normalized) < -0.44999998807907104 && !AppSounds.IsPlaying(ECrowdTypes.kCrowdSidelineOOB))
    {
      this._crowdStareTimer += Time.deltaTime;
      if ((double) this._crowdStareTimer <= 4.0)
        return;
      if (!AppSounds.IsPlaying(ECrowdTypes.kCrowdSidelineOOB))
        AppSounds.PlayCrowd(ECrowdTypes.kCrowdSidelineOOB);
      this._crowdStareTimer = 0.0f;
    }
    else
      this._crowdStareTimer = 0.0f;
  }

  private void HandleGameEvent(PEGameplayEvent e)
  {
    if (MatchManager.instance.IsSimulating)
      return;
    switch (e)
    {
      case PEBallCaughtEvent peBallCaughtEvent:
        this.HandlePassCaught(peBallCaughtEvent.Receiver.onUserTeam);
        break;
      case PEBallThrownEvent _:
        this.HandleBallThrown();
        break;
      case PETackleEvent peTackleEvent:
        this.HandleTackle(peTackleEvent.Tackler.trans);
        break;
      case PEBallKickedEvent _:
        this.HandleBallKicked();
        break;
      case PEPlayOverEvent pePlayOverEvent:
        this.HandlePlayOver(pePlayOverEvent.Type);
        break;
      case PEBallHikedEvent _:
        this.HandleBallSnapped();
        break;
      case PEBallHandoffEvent _:
        this.HandleHandoff();
        break;
      case PEPenaltyEvent pePenaltyEvent:
        this.HandlePenalty(pePenaltyEvent.Type);
        break;
      case PEPlaySelectedEvent playSelectedEvent:
        this.HandlePlaySelected(playSelectedEvent.OffenseData);
        break;
      case PEQuarterEndEvent peQuarterEndEvent:
        this.HandleQuarterEnd(peQuarterEndEvent.Quarter);
        break;
      case PEShowPlaybookEvent _:
        this.ShowPlaybookHandler();
        break;
      case PEAudibleCalledEvent audibleCalledEvent:
        this.HandleAudible(audibleCalledEvent.NewPlay);
        break;
      case PETwoMinWarningEvent _:
        this.OnTwoMinuteWarning();
        break;
      case PEKickReturnEvent _:
        this.OnKickReturn();
        break;
      case PEPuntBlockedEvent _:
        this.OnPuntBlocked();
        break;
      case PEPlayCallMadeEvent _:
        this.HandlePlayCallMade();
        break;
      case PETimeoutEvent _:
        this.HandleTimeout();
        break;
      case PEHikeCompleteEvent _:
        this.HandleHikeComplete();
        break;
      case PEGameOverEvent _:
        this.HandleGameOver();
        break;
      case PEReadyToHikeEvent _:
        this.HandleReadyToHike();
        break;
      case PEHomeTeamTurnover _:
        this.HandleHomeTeamTurnover();
        break;
    }
  }

  private void HandleSidelineSwitch(bool sideline)
  {
    if (sideline)
    {
      AppSounds.PlaySideline();
      this.PlayCoachChatter();
    }
    else
      AppSounds.StopSideline();
  }

  private void HandlePassCaught(bool onUserTeam)
  {
    AppSounds.Play3DSfx(ESfxTypes.kCatchBall, Ball.State.BallPosition);
    this.UpdateCrowdVolume();
    if ((!onUserTeam || !global::Game.IsPlayerOneOnOffense ? (onUserTeam ? 0 : (!global::Game.IsPlayerOneOnOffense ? 1 : 0)) : 1) != 0)
    {
      AppSounds.PlayCrowdReaction(true, AppSounds.CrowdReactionSizes.Med);
      if (global::Game.IsPlayerOneOnOffense)
      {
        this._currentPlayerEmotions = EPlayerEmotions.kPositive;
        this.PlayPlayerChatter(false);
      }
      if (global::Game.IsHomeTeamOnOffense)
        AppSounds.PlayCrowd(ECrowdTypes.kCrowdLongPlay);
    }
    else
    {
      this._wasIntercepted = true;
      AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Big);
      if (global::Game.IsPlayerOneOnOffense)
      {
        this._currentPlayerEmotions = EPlayerEmotions.kNegative;
        this.PlayPlayerChatter(false);
      }
      else
        AppSounds.PlayOC(EOCTypes.kInterceptionDef);
    }
    this.StartCoroutine(this.DoCrowdExtraReaction());
  }

  private void HandleBallThrown()
  {
    if (!global::Game.IsPlayerOneOnOffense)
      return;
    AppSounds.Play3DSfx(ESfxTypes.kThrowBall, SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform);
    AppSounds.PlaySfx(ESfxTypes.kQBThrow);
    AppSounds.StopSfx(ESfxTypes.kQBBreathing, 1);
  }

  private void HandleHandoff() => AppSounds.StopSfx(ESfxTypes.kQBBreathing, 1);

  private void HandleTackle(Transform trans)
  {
    if (MatchManager.instance.IsSimulating)
      return;
    PlayerAI ballHolderScript = MatchManager.instance.playersManager.ballHolderScript;
    AppSounds.Play3DSfx(ESfxTypes.kTackle, trans.position);
    if (global::Game.IsPlayerOneOnOffense && global::Game.IsNotKickoff && global::Game.IsNotPunt)
    {
      AppSounds.PlaySfx(ESfxTypes.kTackle2D);
      if (ballHolderScript.IsQB())
      {
        this._wasSacked = true;
        AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Med);
        AppSounds.PlaySfx(ESfxTypes.kUserSackedImpact);
      }
    }
    float yards = (float) Field.ConvertDistanceToYards(Field.FindDifference(ballHolderScript.trans.position.z, ProEra.Game.MatchState.BallOn.Value));
    if ((double) yards < 0.0)
    {
      if ((double) ballHolderScript.trans.position.z >= (double) Field.NORTH_GOAL_LINE || (double) ballHolderScript.trans.position.z <= (double) Field.SOUTH_GOAL_LINE)
        AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Big);
      else if (PlayState.IsPass && !MatchManager.instance.playersManager.ballWasThrownOrKicked)
        AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Med);
      else
        AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Small);
    }
    else if ((double) yards < 7.0)
      AppSounds.PlayCrowdReaction(true, AppSounds.CrowdReactionSizes.Small);
    else
      AppSounds.PlayCrowdReaction(true, AppSounds.CrowdReactionSizes.Med);
  }

  private void HandleBallKicked() => AppSounds.Play3DSfx(ESfxTypes.kKick, Ball.State.BallPosition);

  private void HandlePlayOver(PlayEndType type)
  {
    AppSounds.StopSfx(ESfxTypes.kQBBreathing);
    AppSounds.StopCrowd(ECrowdTypes.kCrowdLongPlay, 2);
    AppSounds.PlaySfx(ESfxTypes.kRefWhistle);
    this.CheckForPostPlaySituationalCrowdNoises(type);
    this._didOpeningKickoff = true;
    switch (type)
    {
      case PlayEndType.Touchdown:
        if (!(bool) ProEra.Game.MatchState.RunningPat)
        {
          int num = !global::Game.IsHomeTeamOnOffense || !global::Game.IsNotTurnover ? (global::Game.IsHomeTeamOnOffense ? 0 : (global::Game.IsTurnover ? 1 : 0)) : 1;
          AppSounds.PlayAnnouncer(num != 0 ? EAnnouncerType.kTouchdownHome : EAnnouncerType.kTouchdownAway);
          if (num != 0 && AppState.GameMode != EGameMode.kOnboarding && AppState.GameMode != EGameMode.kPracticeMode)
          {
            if (!AppSounds.IsPlaying(EAnnouncerType.kHype4thQ))
              AppSounds.PlayStinger(EStingerType.kStadium);
            switch (ProEra.Game.MatchState.Stats.GetHomeScore() - ProEra.Game.MatchState.Stats.GetAwayScore())
            {
              case 0:
              case 1:
              case 2:
              case 3:
              case 4:
              case 5:
              case 6:
                AppSounds.PlayCrowd(ECrowdTypes.kCloseGameScore);
                break;
            }
            AppSounds.PlayCrowd(ECrowdTypes.kTouchdownHome);
          }
          this._currentPlayerEmotions = EPlayerEmotions.kPositive;
          this.PlayPlayerChatter(false);
        }
        if (global::Game.IsPlayerOneOnOffense && !global::Game.IsTurnover)
        {
          this._currentPlayerEmotions = EPlayerEmotions.kTD;
          this.PlayPlayerChatter(false);
        }
        else if (!global::Game.IsPlayerOneOnOffense && global::Game.IsTurnover)
        {
          AppSounds.PlayOC(EOCTypes.kDefTD);
          this._currentPlayerEmotions = EPlayerEmotions.kTD;
          this.PlayPlayerChatter(false);
        }
        else
        {
          this._currentPlayerEmotions = EPlayerEmotions.kNegative;
          this.PlayPlayerChatter(false);
        }
        AppSounds.PlayCrowdReaction(true, AppSounds.CrowdReactionSizes.Big);
        this._didAnnouncePlayEnd = true;
        break;
      case PlayEndType.Incomplete:
        AppSounds.PlayAnnouncer(EAnnouncerType.kPassIncomplete);
        if ((double) (Mathf.Abs(Field.OFFENSIVE_GOAL_LINE - (float) ProEra.Game.MatchState.BallOn) * Field.ONE_YARD) <= 50.0)
          ;
        this._currentPlayerEmotions = EPlayerEmotions.kNegative;
        this.PlayPlayerChatter(false);
        this._didAnnouncePlayEnd = true;
        break;
      case PlayEndType.MadeFG:
        AppSounds.PlayAnnouncer(global::Game.IsHomeTeamOnOffense ? EAnnouncerType.kKickGoodHome : EAnnouncerType.kKickGoodAway);
        AppSounds.PlayCrowdReaction(true, AppSounds.CrowdReactionSizes.Big);
        if (global::Game.IsHomeTeamOnOffense)
        {
          switch (ProEra.Game.MatchState.Stats.GetHomeScore() - ProEra.Game.MatchState.Stats.GetAwayScore())
          {
            case 0:
            case 1:
            case 2:
            case 3:
              AppSounds.PlayCrowd(ECrowdTypes.kCloseGameScore);
              break;
          }
        }
        if (global::Game.IsPlayerOneOnOffense && !global::Game.IsRunningPAT)
          AppSounds.PlayOC(EOCTypes.kFG);
        this._currentPlayerEmotions = EPlayerEmotions.kPositive;
        this.PlayPlayerChatter(false);
        this._didAnnouncePlayEnd = true;
        break;
      case PlayEndType.Safety:
        AppSounds.PlayAnnouncer(global::Game.IsHomeTeamOnOffense ? EAnnouncerType.kSafetyHome : EAnnouncerType.kSafetyAway);
        this._didAnnouncePlayEnd = true;
        break;
      case PlayEndType.MissedFG:
        AppSounds.PlayAnnouncer(EAnnouncerType.kKickNoGood);
        AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Big);
        this._currentPlayerEmotions = EPlayerEmotions.kNegative;
        this.PlayPlayerChatter(false);
        this._didAnnouncePlayEnd = true;
        break;
    }
    if (!global::Game.IsPlayerOneOnOffense && global::Game.IsTurnover)
    {
      int num = ProEra.Game.MatchState.GameLength.Value - Mathf.CeilToInt(MatchManager.instance.timeManager.GetGameClockTimer());
      if (MatchManager.instance.timeManager.IsSecondQuarter() && num > 0)
      {
        if (num < 30)
        {
          int yards = Field.ConvertDistanceToYards(Field.FindDifference(Field.OFFENSIVE_GOAL_LINE, SingletonBehaviour<BallManager, MonoBehaviour>.instance.GetForwardProgressLine()));
          if (yards > 60)
            AppSounds.PlayOC(EOCTypes.kTurnover_Q2_30sec_Above60yards);
          else if (yards < 60)
            AppSounds.PlayOC(EOCTypes.kTurnover_Q2_30sec_Below60yards);
        }
        else if (num < 240)
          AppSounds.PlayOC(EOCTypes.kTurnover_Q2_4min);
      }
      else if (MatchManager.instance.timeManager.IsFourthQuarter() && num > 0 && (PersistentData.userIsHome ? ProEra.Game.MatchState.Stats.GetHomeScore() - ProEra.Game.MatchState.Stats.GetAwayScore() : ProEra.Game.MatchState.Stats.GetAwayScore() - ProEra.Game.MatchState.Stats.GetHomeScore()) >= 1)
      {
        if (num < 120)
          AppSounds.PlayOC(EOCTypes.kTurnover_Q4_1scr_2min);
        else if (num < 180)
          AppSounds.PlayOC(EOCTypes.kTurnover_Q4_1scr_3min);
      }
    }
    this.UpdateCrowdVolume();
    AppSounds.SetAmbientMuffle(true);
    if (!global::Game.IsHomeTeamOnOffense || type != PlayEndType.Touchdown)
      AppSounds.PlayCrowdRamp(false);
    this._calledAudible = false;
  }

  private void HandleBallSnapped()
  {
    AppSounds.StopAnnouncer(EAnnouncerType.kHype4thQ, 4);
    AppSounds.PlayCrowd(ECrowdTypes.kCrowdBuildupHigh);
    this.UpdateCrowdVolume();
    List<PlayerAI> curUserScriptRef = MatchManager.instance.playersManager.curUserScriptRef;
    for (int index = 0; index < 11; ++index)
    {
      if (curUserScriptRef[index].indexInFormation == 2)
      {
        AppSounds.Play3DSfx(ESfxTypes.kImpactLineRush, curUserScriptRef[index].trans.position);
        if (!(bool) PEGameplayEventManager.PlayerOnSideline)
        {
          AppSounds.PlaySfx(ESfxTypes.kLineRush2D);
          break;
        }
        break;
      }
    }
    this._didAnnouncePlayEnd = false;
    this._canSayFirstDown = true;
    this._wasIntercepted = false;
    this._wasSacked = false;
    if (global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB)
    {
      AppSounds.PlayVO(EVOTypes.kHike, this._malePlayer);
    }
    else
    {
      if (!global::Game.IsPlayerOneOnDefense || !global::Game.IsRunOrPass)
        return;
      AppSounds.PlayVO3D(EVOTypes.kHike3D, global::Game.OffensiveQB.trans);
    }
  }

  private void HandlePlaySelected(PlayDataOff offData)
  {
    int num1 = PersistentData.userIsHome ? ProEra.Game.MatchState.Stats.GetHomeScore() - ProEra.Game.MatchState.Stats.GetAwayScore() : ProEra.Game.MatchState.Stats.GetAwayScore() - ProEra.Game.MatchState.Stats.GetHomeScore();
    if (ProEra.Game.MatchState.Down.Value == 3 && !global::Game.IsHomeTeamOnOffense)
    {
      AppSounds.PlayAnnouncer(EAnnouncerType.kHype);
      AppSounds.PlayCrowd(ECrowdTypes.kCrowdBuildupPrePlay);
    }
    if (global::Game.IsPlayerOneOnOffense)
    {
      if (!this._calledAudible && (offData.GetPlayType() == PlayType.Pass || offData.GetPlayType() == PlayType.Run))
        AppSounds.PlaySfx(ESfxTypes.kHuddleBreak);
      else if (global::Game.IsRunningPAT && !global::Game.CurrentPlayHasUserQBOnField)
      {
        bool flag = false;
        int touchdowns = ProEra.Game.MatchState.Stats.User.Touchdowns;
        if (AppState.SeasonMode.Value != ESeasonMode.kUnknown && SeasonModeManager.self.GetTeamData(PersistentData.GetUserTeamIndex()).CurrentSeasonTouchdownPasses == 0 && touchdowns == 1)
        {
          flag = true;
          AppSounds.PlayOC(EOCTypes.kSidelineSeasonFirstTD);
        }
        if (!flag)
        {
          int num2 = ProEra.Game.MatchState.GameLength.Value - Mathf.CeilToInt(MatchManager.instance.timeManager.GetGameClockTimer());
          if (MatchManager.instance.timeManager.IsFourthQuarter() && num2 <= 240)
          {
            if (num1 >= 17)
            {
              if (num2 < 180)
                AppSounds.PlayOC(EOCTypes.kTD_up_3_3min);
            }
            else if (num1 >= 9)
            {
              if (num2 < 30)
                AppSounds.PlayOC(EOCTypes.kTD_up_2_30sec);
              else if (num2 < 120)
                AppSounds.PlayOC(EOCTypes.kTD_up_2_2min);
              else if (num2 < 240)
                AppSounds.PlayOC(EOCTypes.kTD_up_2_4min);
            }
            else if (num1 >= 1)
            {
              if (num2 < 30)
                AppSounds.PlayOC(EOCTypes.kTD_up_1_30sec);
              else if (num2 < 120)
                AppSounds.PlayOC(EOCTypes.kTD_up_1_2min);
              else if (num2 < 240)
                AppSounds.PlayOC(EOCTypes.kTD_up_1_4min);
            }
            else if (num1 == 0)
            {
              if (num2 < 30)
                AppSounds.PlayOC(EOCTypes.kTD_tie_30sec);
              else if (num2 < 120)
                AppSounds.PlayOC(EOCTypes.kTD_tie_2min);
              else if (num2 < 240)
                AppSounds.PlayOC(EOCTypes.kTD_tie_4min);
            }
            else if (num1 == -1)
            {
              if (num2 < 30)
                AppSounds.PlayOC(EOCTypes.kTD_1pt_30sec);
              else if (num2 < 120)
                AppSounds.PlayOC(EOCTypes.kTD_1pt_2min);
              else if (num2 < 240)
                AppSounds.PlayOC(EOCTypes.kTD_1pt_4min);
            }
            else if (num1 <= -8)
            {
              if (num2 < 3)
                AppSounds.PlayOC(EOCTypes.kTD_down_1_3sec);
              else if (num2 < 30)
                AppSounds.PlayOC(EOCTypes.kTD_down_1_30sec);
              else if (num2 < 120)
                AppSounds.PlayOC(EOCTypes.kTD_down_1_2min);
              else if (num2 < 240)
                AppSounds.PlayOC(EOCTypes.kTD_down_1_4min);
            }
            else if (num1 <= -16)
            {
              if (num2 < 180)
                AppSounds.PlayOC(EOCTypes.kTD_down_2_3min);
              else if (num2 < 240)
                AppSounds.PlayOC(EOCTypes.kTD_down_2_4min);
            }
          }
          else if (touchdowns > 1 && touchdowns <= 3)
          {
            if ((double) UnityEngine.Random.value >= 0.5)
            {
              switch (touchdowns)
              {
                case 2:
                  AppSounds.PlayOC(EOCTypes.kTD2);
                  break;
                case 3:
                  AppSounds.PlayOC(EOCTypes.kTD3);
                  break;
                default:
                  AppSounds.PlayOC(EOCTypes.kTD);
                  break;
              }
            }
            else
              AppSounds.PlayOC(EOCTypes.kTD);
          }
          else
            AppSounds.PlayOC(EOCTypes.kTD);
        }
      }
      else if (offData.GetPlayType() == PlayType.Kickoff)
      {
        if (num1 >= 17 && num1 <= 22)
          AppSounds.PlayOC(EOCTypes.kSidelineScoreUp3);
        else if (num1 >= 9 && num1 <= 16)
          AppSounds.PlayOC(EOCTypes.kSidelineScoreUp2);
        else if (num1 >= 1 && num1 <= 8)
          AppSounds.PlayOC(EOCTypes.kSidelineScoreUp1);
        else if (num1 <= -1 && num1 >= -8)
          AppSounds.PlayOC(EOCTypes.kSidelineScoreDown1);
        else if (num1 <= -9 && num1 >= -16)
          AppSounds.PlayOC(EOCTypes.kSidelineScoreDown2);
        else if (num1 <= -17 && num1 >= -22)
          AppSounds.PlayOC(EOCTypes.kSidelineScoreDown3);
        else
          AppSounds.PlayOC(EOCTypes.kSidelineKickoff);
      }
      else if (offData.GetPlayType() == PlayType.Punt)
      {
        if (num1 <= -1 && num1 >= -8)
          AppSounds.PlayOC(EOCTypes.kSidelinePuntDown1);
        else if (num1 <= -9 && num1 >= -16)
          AppSounds.PlayOC(EOCTypes.kSidelinePuntDown2);
        else
          AppSounds.PlayOC(EOCTypes.kSidelinePunt);
      }
    }
    else if (this._wasIntercepted)
    {
      bool flag = false;
      PlayerData player = MatchManager.instance.playersManager.userTeamData.GetPlayer(0);
      int qbInts = player.CurrentGameStats.QBInts;
      if (AppState.SeasonMode.Value != ESeasonMode.kUnknown)
      {
        SeasonModeManager.self.GetTeamData(PersistentData.GetUserTeamIndex());
        if (player.CurrentSeasonStats.QBInts == 0 && qbInts == 1)
        {
          flag = true;
          AppSounds.PlayOC(EOCTypes.kSidelineSeasonFirstINT);
        }
      }
      if (!flag)
      {
        if (qbInts > 1 && qbInts <= 3)
        {
          if ((double) UnityEngine.Random.value >= 0.5)
          {
            switch (qbInts)
            {
              case 2:
                AppSounds.PlayOC(EOCTypes.kSidelineINT2);
                break;
              case 3:
                AppSounds.PlayOC(EOCTypes.kSidelineINT3);
                break;
              default:
                AppSounds.PlayOC(EOCTypes.kSidelineINT);
                break;
            }
          }
          else
            AppSounds.PlayOC(EOCTypes.kSidelineINT);
        }
        else
          AppSounds.PlayOC(EOCTypes.kSidelineINT);
      }
    }
    else if (offData.GetPlayType() == PlayType.Punt)
    {
      int passYards = ProEra.Game.MatchState.Stats.User.PassYards;
      if (passYards >= 500 && !this._passingYardChecks[0])
      {
        this._passingYardChecks[0] = true;
        AppSounds.PlayOC(EOCTypes.kSidelinePassing500);
      }
      else if (passYards >= 400 && !this._passingYardChecks[1])
      {
        this._passingYardChecks[1] = true;
        AppSounds.PlayOC(EOCTypes.kSidelinePassing400);
      }
      else if (passYards >= 300 && !this._passingYardChecks[2])
      {
        this._passingYardChecks[2] = true;
        AppSounds.PlayOC(EOCTypes.kSidelinePassing300);
      }
      else if (num1 >= 9 && num1 <= 16)
        AppSounds.PlayOC(EOCTypes.kSidelinePuntUp2);
      else if (num1 >= 1 && num1 <= 8)
      {
        if (this._puntUpByOneCount < 2)
        {
          ++this._puntUpByOneCount;
          AppSounds.PlayOC(EOCTypes.kSidelinePuntUp1);
        }
      }
      else
        AppSounds.PlayOC(EOCTypes.kSidelineOpponentPunt);
    }
    else if (offData.GetPlayType() == PlayType.Kickoff)
    {
      if (MatchManager.instance.timeManager.IsThirdQuarter() && (double) MatchManager.instance.timeManager.GetGameClockTimer() == 0.0)
      {
        if (num1 > 0)
          AppSounds.PlayOC(EOCTypes.kHalftimeWinning);
        else if (num1 > 0)
          AppSounds.PlayOC(EOCTypes.kHalftimeLosing);
        else
          AppSounds.PlayOC(EOCTypes.kHalftimeTied);
      }
      else
        AppSounds.PlayOC(EOCTypes.kSidelineOpponentKickoff);
    }
    AppSounds.SetAmbientMuffle(false);
    AppSounds.PlayCrowdRamp(true);
    this._currentPlayerEmotions = EPlayerEmotions.kNeutral;
    this.PlayPlayerChatter(false);
  }

  private void HandlePlayCallMade()
  {
    if (global::Game.IsPlayerOneOnOffense)
    {
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      this._currentPlay = savedOffPlay;
      if (ProEra.Game.MatchState.Stats.User.IsHighestYardPlay(savedOffPlay) && (double) UnityEngine.Random.value > 0.5)
        AppSounds.PlayOC(EOCTypes.kYardsOutOfFormation);
      else
        AppSounds.PlayOCPlayCall(savedOffPlay);
      this.CheckForPrePlaySituationalCrowdNoises();
      if (MatchManager.down != 4 || Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, ProEra.Game.MatchState.FirstDown.Value) || global::Game.PET_IsTouchdown || !global::Game.IsPlayerOneOnOffense)
        return;
      if (ProEra.Game.MatchState.Stats.User.HadRecentDriveEndType(DriveEndType.Touchdown) && (double) UnityEngine.Random.value > 0.5)
        AppSounds.PlayOC(EOCTypes.kPreviousTD);
      else if (ProEra.Game.MatchState.Stats.User.HadRecentDriveEndType(DriveEndType.Interception) && (double) UnityEngine.Random.value > 0.5)
        AppSounds.PlayOC(EOCTypes.kINT);
      else if (ProEra.Game.MatchState.Stats.User.TotalPassPlays > 100 && (double) UnityEngine.Random.value > 0.5)
      {
        AppSounds.PlayOC(EOCTypes.kTotalYards);
      }
      else
      {
        int playType = (int) savedOffPlay.GetPlayType();
      }
    }
    else
      Debug.LogError((object) "HandlePlayCallMade should not be called for defense");
  }

  private void HandleDownChanged(int down)
  {
    if (MatchManager.instance.IsSimulating)
      return;
    if (!this._didAnnouncePlayEnd && this._canSayFirstDown)
    {
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      Debug.Log((object) ("!_didAnnouncePlayEnd: " + down.ToString()));
      if (!Field.FurtherDownfield(SingletonBehaviour<BallManager, MonoBehaviour>.instance.GetForwardProgressLine(), Field.DEFENSIVE_GOAL_LINE) && !this._wasIntercepted && !MatchManager.turnover && global::Game.IsNotRunningPAT)
      {
        ScoreboardAnimations.PlayAnimation(ScoreboardAnimations.BoardAnimType.Safety, PersistentData.GetOffensiveTeamIndex());
        AppSounds.PlayAnnouncer(global::Game.IsHomeTeamOnOffense ? EAnnouncerType.kSafetyHome : EAnnouncerType.kSafetyAway);
        this._didAnnouncePlayEnd = true;
      }
      else if (down == 1 && !this._wasIntercepted && !MatchManager.turnover && global::Game.IsNotRunningPAT)
      {
        if (AppState.GameMode != EGameMode.kOnboarding && AppState.GameMode != EGameMode.kPracticeMode)
        {
          AppSounds.PlayAnnouncer(global::Game.IsHomeTeamOnOffense ? EAnnouncerType.k1stDownHome : EAnnouncerType.k1stDownAway);
          if (global::Game.IsHomeTeamOnOffense && !AppSounds.IsPlaying(EAnnouncerType.kHype4thQ))
            AppSounds.PlayStinger(EStingerType.kStadium);
          ScoreboardAnimations.PlayAnimation(ScoreboardAnimations.BoardAnimType.FirstDown, PersistentData.GetOffensiveTeamIndex());
        }
        this._didAnnouncePlayEnd = true;
      }
      else if (savedOffPlay.GetPlayType() == PlayType.Pass && !this._wasSacked && global::Game.BallIsThrownOrKicked)
      {
        if (!this._wasIntercepted && !Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, ProEra.Game.MatchState.FirstDown.Value))
          AppSounds.PlayAnnouncer(EAnnouncerType.kPassComplete);
        else if (this._wasIntercepted)
        {
          AppSounds.PlayAnnouncer(EAnnouncerType.kPassIntercepted);
          if ((UnityEngine.Object) this._playersManager.ballHolderScript != (UnityEngine.Object) null)
            ScoreboardAnimations.PlayAnimation(ScoreboardAnimations.BoardAnimType.Interception, this._playersManager.ballHolderScript.teamIndex);
          this._didAnnouncePlayEnd = true;
        }
      }
    }
    bool flag1 = !Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, ProEra.Game.MatchState.FirstDown.Value) && !global::Game.PET_IsTouchdown;
    if (down == 1 && ProEra.Game.MatchState.Stats.CurrentDrivePlays == 0 && global::Game.IsPlayerOneOnOffense && !(bool) ProEra.Game.MatchState.Turnover)
    {
      float num1 = ProEra.Game.MatchState.BallOn.Value;
      float num2 = !global::Game.OffenseGoingNorth ? Mathf.Abs(Field.SOUTH_GOAL_LINE - num1) : Mathf.Abs(Field.NORTH_GOAL_LINE - num1);
      if ((double) num2 <= 20.0)
      {
        AppSounds.PlayOC(EOCTypes.kRedzoneAmazing);
      }
      else
      {
        if ((double) num2 > 30.0)
          return;
        AppSounds.PlayOC(EOCTypes.kRedzone30);
      }
    }
    else if (down == 4 & flag1)
    {
      bool flag2 = false;
      if (!global::Game.IsPlayerOneOnOffense)
      {
        float num = (float) (int) ProEra.Game.MatchState.GameLength - MatchManager.instance.timeManager.GetGameClockTimer();
        if (MatchManager.instance.timeManager.IsFourthQuarter() && ProEra.Game.MatchState.Stats.User.Score <= ProEra.Game.MatchState.Stats.Comp.Score && (double) num < 300.0 && (double) num >= 180.0)
        {
          AppSounds.PlayOC(EOCTypes.kWinGame);
          flag2 = true;
        }
      }
      int num3 = flag2 ? 1 : 0;
      AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Big);
    }
    else
    {
      if (!(down == 5 & flag1) || !PlayState.IsRunOrPass)
        return;
      bool flag3 = false;
      if (!global::Game.IsPlayerOneOnOffense)
      {
        float num = (float) (int) ProEra.Game.MatchState.GameLength - MatchManager.instance.timeManager.GetGameClockTimer();
        if (MatchManager.instance.timeManager.IsFourthQuarter())
        {
          if (ProEra.Game.MatchState.Stats.User.Score > ProEra.Game.MatchState.Stats.Comp.Score)
          {
            if ((double) num < 40.0)
            {
              AppSounds.PlayOC(EOCTypes.kBleedClock);
              flag3 = true;
            }
            else if ((double) num < 120.0)
            {
              AppSounds.PlayOC(EOCTypes.kVictoryFormation);
              flag3 = true;
            }
          }
          else if ((double) num < 300.0)
          {
            AppSounds.PlayOC(EOCTypes.kWinGame);
            flag3 = true;
          }
        }
      }
      int num4 = flag3 ? 1 : 0;
      AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Big);
    }
  }

  private void HandleQuarterEnd(int quarter)
  {
    AppSounds.PlaySfx(ESfxTypes.kRefWhistle);
    switch (quarter)
    {
      case 1:
        AppSounds.PlayAnnouncer(EAnnouncerType.kQuarter1End);
        this._didPlayDefense = false;
        this._didCrowdChant = false;
        break;
      case 2:
        AppSounds.PlayAnnouncer(EAnnouncerType.kQuarter2End);
        break;
      case 3:
        AppSounds.PlayAnnouncer(EAnnouncerType.kQuarter3End);
        this._didPlayDefense = false;
        this._didCrowdChant = false;
        AppSounds.PlayAnnouncer(EAnnouncerType.kHype4thQ);
        AppSounds.PlayCrowd(ECrowdTypes.kCrowdBuildupPrePlay, 2);
        break;
      case 4:
        AppSounds.PlayAnnouncer(EAnnouncerType.kQuarter4End);
        this._canSayFirstDown = false;
        break;
    }
  }

  private void ShowPlaybookHandler()
  {
    this._currentPlayerEmotions = EPlayerEmotions.kNeutral;
    this.PlayPlayerChatter(false);
  }

  private void HandlePenalty(PenaltyType type)
  {
    if (type != PenaltyType.DelayOfGame)
      return;
    AppSounds.PlaySfx(ESfxTypes.kRefWhistle);
    AppSounds.PlayAnnouncer(EAnnouncerType.kPenaltyDelay);
  }

  private void HandleAudible(PlayDataOff newPlay)
  {
    this._calledAudible = true;
    Debug.Log((object) ("_currentPlay.GetPlayType(): " + this._currentPlay.GetPlayType().ToString()));
    Debug.Log((object) ("newPlay.GetPlayType(): " + newPlay.GetPlayType().ToString()));
    if (this._currentPlay.GetPlayType() == newPlay.GetPlayType())
      AppSounds.PlayVO(EVOTypes.kAudible, this._malePlayer);
    else
      AppSounds.PlayVO(EVOTypes.kAudible2, this._malePlayer);
  }

  private void OnKickReturn()
  {
    AppSounds.PlayKickReturnCrowdBed();
    if (this._didOpeningKickoff)
      return;
    this._didOpeningKickoff = true;
    AppSounds.PlayOC(EOCTypes.kOpeningDrive);
  }

  private void OnTwoMinuteWarning() => AppSounds.PlayAnnouncer(EAnnouncerType.k2Minutes);

  private void OnPuntBlocked()
  {
    AppSounds.PlayAnnouncer(EAnnouncerType.kPuntBlocked);
    AppSounds.PlayCrowdReaction(false, AppSounds.CrowdReactionSizes.Big);
  }

  private void OnYardageChangeUpdate(int yardage)
  {
    if (!global::Game.IsNotKickoff || !global::Game.IsNotFG || global::Game.IsPunt || !global::Game.PET_IsNotTouchdown || !global::Game.IsNotTurnover || MatchManager.instance.IsSimulating || !global::Game.PET_IsNotSafety)
      return;
    AppSounds.PlayAnnouncer(EAnnouncerType.kYardage);
    float num = (float) (int) ProEra.Game.MatchState.GameLength - MatchManager.instance.timeManager.GetGameClockTimer();
    if (this._didCrowdChant || !global::Game.IsHomeTeamOnOffense || (double) num >= 240.0)
      return;
    this._didCrowdChant = true;
    AppSounds.PlayCrowd(ECrowdTypes.kCrowdHomeTeamChant);
  }

  private void UpdateCrowdVolume(bool forceValue = false)
  {
    if ((UnityEngine.Object) SingletonBehaviour<BallManager, MonoBehaviour>.instance == (UnityEngine.Object) null || (UnityEngine.Object) SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans == (UnityEngine.Object) null)
      return;
    if (global::Game.IsPlayActive & Field.FurtherDownfield(SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position.z, ProEra.Game.MatchState.FirstDown.Value))
    {
      this.GetAdditionalCrowdVolumeAdjustments();
      float volume = Ball.State.BallState.Value != EBallState.PlayersHands ? AppSounds.AMBIENT_LOWEST + 0.05f : AppSounds.AMBIENT_HIGHEST;
      AppSounds.AdjustAmbientVolume(volume, forceValue);
      WorldState.CrowdMovement.SetValue(volume * 0.6f + this._crowdExtraReactionLevel);
    }
    else
    {
      float t = Mathf.Max((float) (1.0 - (double) (Mathf.Abs(Field.OFFENSIVE_GOAL_LINE - SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position.z) * Field.ONE_YARD) / 100.0), 0.1f);
      float volume = Mathf.Lerp(AppSounds.AMBIENT_LOWEST, AppSounds.AMBIENT_HIGHEST, t);
      if (!global::Game.IsHomeTeamOnOffense)
        volume += 0.05f * (float) ((int) ProEra.Game.MatchState.Down - 1);
      AppSounds.AdjustAmbientVolume(volume, forceValue);
      WorldState.CrowdMovement.SetValue(t * 0.6f + this._crowdExtraReactionLevel);
    }
  }

  private void PlayPlayerChatter(bool callback)
  {
    if (!this._isChatterAllowed)
      return;
    Transform trans = (Transform) null;
    Vector3 pos = Vector3.zero;
    ESfxTypes sound = ESfxTypes.kButtonPress;
    if (!(bool) PEGameplayEventManager.PlayerOnSideline)
    {
      switch (this._currentPlayerEmotions)
      {
        case EPlayerEmotions.kPositive:
          sound = ESfxTypes.kFieldChatterPos;
          break;
        case EPlayerEmotions.kNeutral:
          sound = ESfxTypes.kFieldChatterNeutral;
          break;
        case EPlayerEmotions.kNegative:
          sound = ESfxTypes.kFieldChatterNeg;
          break;
        case EPlayerEmotions.kTD:
          sound = ESfxTypes.kFieldChatterTD;
          break;
      }
      if (MatchManager.instance.playersManager.curUserScriptRef != null)
        trans = MatchManager.instance.playersManager.curUserScriptRef[UnityEngine.Random.Range(0, MatchManager.instance.playersManager.curUserScriptRef.Count)].headJoint;
      else
        Debug.LogError((object) "MatchManager.instance.playersManager.curUserScriptRef is null!");
    }
    else
    {
      sound = ESfxTypes.kPlayerChatter;
      pos = PersistentSingleton<GamePlayerController>.Instance.position with
      {
        z = UnityEngine.Random.Range(-16.5f, 16.5f)
      };
    }
    float delay = UnityEngine.Random.Range(2f, 5f);
    AppSounds.PlayPlayerChatter(sound, pos, trans, delay, callback ? this.PlayerChatterFinished : (System.Action) null);
  }

  private void FinishedPlayerChatter() => this.PlayPlayerChatter(true);

  private void PlayCoachChatter()
  {
    Transform trans = (Transform) null;
    Vector3 zero = Vector3.zero;
    if (!(bool) PEGameplayEventManager.PlayerOnSideline)
      return;
    ESfxTypes sound = ESfxTypes.kCoachChatter;
    Vector3 position = PersistentSingleton<GamePlayerController>.Instance.position with
    {
      z = UnityEngine.Random.Range(-16.5f, 16.5f)
    };
    float delay = UnityEngine.Random.Range(2f, 5f);
    AppSounds.PlayPlayerChatter(sound, position, trans, delay, this.CoachChatterFinished);
  }

  private void FinishedCoachChatter() => this.PlayCoachChatter();

  private IEnumerator DoCrowdExtraReaction()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    GameAudioManager gameAudioManager = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated method
      LeanTween.value(gameAudioManager.gameObject, 0.4f, 0.0f, 5f).setOnUpdate(new Action<float>(gameAudioManager.\u003CDoCrowdExtraReaction\u003Eb__60_1));
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated method
    LeanTween.value(gameAudioManager.gameObject, 0.0f, 0.4f, 1f).setOnUpdate(new Action<float>(gameAudioManager.\u003CDoCrowdExtraReaction\u003Eb__60_0));
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) new WaitForSeconds(1f);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  private void CheckForPostPlaySituationalCrowdNoises(PlayEndType endType)
  {
    float num1 = ProEra.Game.MatchState.BallOn.Value;
    float secondObjectZPos = ProEra.Game.MatchState.FirstDown.Value;
    int num2 = ProEra.Game.MatchState.Down.Value;
    bool flag1 = !Field.FurtherDownfield(num1, secondObjectZPos);
    bool flag2 = MatchManager.instance.timeManager.IsFourthQuarter();
    int displayMinutes = MatchManager.instance.timeManager.GetDisplayMinutes();
    int num3 = !flag1 ? 0 : (num2 == 2 ? 1 : 0);
    bool flag3 = global::Game.IsNotTurnover && Field.ConvertDistanceToYards(Field.FindDifference(Field.OFFENSIVE_GOAL_LINE, num1)) <= 5;
    bool flag4 = flag1 && num2 >= 4;
    bool flag5 = global::Game.IsPass && !global::Game.BallIsThrownOrKicked;
    bool flag6 = ((endType == PlayEndType.Incomplete ? 1 : (global::Game.IsTurnover ? 1 : 0)) | (flag5 ? 1 : 0) | (flag4 ? 1 : 0)) != 0;
    bool flag7 = ((!flag2 || displayMinutes >= 2 ? 0 : (!global::Game.IsHomeTeamOnOffense ? 1 : 0)) & (flag6 ? 1 : 0)) != 0;
    int num4 = Mathf.Abs(ProEra.Game.MatchState.Stats.GetHomeScore() - ProEra.Game.MatchState.Stats.GetAwayScore());
    bool flag8 = flag2 && displayMinutes < 1 && global::Game.IsHomeTeamOnOffense && num4 <= 16;
    int num5 = !(global::Game.IsHomeTeamOnOffense & flag2) || num4 <= 0 ? 0 : (!flag1 ? 1 : 0);
    bool flag9 = flag2 && !global::Game.IsHomeTeamOnOffense && num2 > 2;
    bool flag10 = flag2 && !global::Game.IsHomeTeamOnOffense && (flag5 || endType == PlayEndType.Incomplete || global::Game.IsTurnover && global::Game.IsNotPunt);
    if (flag7 | flag3 | flag8 | flag9)
      AppSounds.PlayCrowd(ECrowdTypes.kCrowdBuildupPrePlay);
    int num6 = flag10 ? 1 : 0;
    if ((num5 | num6) == 0)
      return;
    AppSounds.PlayCrowd(ECrowdTypes.kCrowdCheerHuge);
  }

  private void CheckForPrePlaySituationalCrowdNoises()
  {
    double firstObjectZPos = (double) ProEra.Game.MatchState.BallOn.Value;
    float num1 = ProEra.Game.MatchState.FirstDown.Value;
    int num2 = ProEra.Game.MatchState.Down.Value;
    double secondObjectZPos = (double) num1;
    int num3 = !Field.FurtherDownfield((float) firstObjectZPos, (float) secondObjectZPos) ? 1 : 0;
    PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
    if (((num3 == 0 || num2 != 4 || !global::Game.IsRunOrPass ? 0 : (global::Game.IsPlayerOneOnDefense ? 1 : 0)) | (savedOffPlay.GetPlayType() != PlayType.FG ? (false ? 1 : 0) : (global::Game.IsHomeTeamOnOffense ? 1 : 0))) == 0)
      return;
    AppSounds.PlayCrowd(ECrowdTypes.kCrowdBuildupPrePlay);
  }

  private void HandleMatchStateChanged(EMatchState state)
  {
    Debug.Log((object) ("OnMatchStateChanged: " + state.ToString()));
    if (state == EMatchState.Beginning || this._gameStarted)
      return;
    SeasonModeManager self = SeasonModeManager.self;
    if (AppState.SeasonMode.Value != ESeasonMode.kUnknown)
    {
      SGD_SeasonModeData seasonModeData = self.seasonModeData;
      SeasonModeGameRound gameRound = self.GetGameRound(seasonModeData.currentWeek);
      int teamOpponentForWeek = self.GetTeamOpponentForWeek(seasonModeData.UserTeamIndex, seasonModeData.currentWeek, out int _, out int _);
      switch (gameRound)
      {
        case SeasonModeGameRound.DivisionalRound:
          AppSounds.PlayOC(EOCTypes.kDivisionChamp);
          break;
        case SeasonModeGameRound.SuperBowl:
          AppSounds.PlayOC(EOCTypes.kSuperBowl);
          break;
        default:
          if (seasonModeData.currentWeek == 1)
          {
            AppSounds.PlayOC(EOCTypes.kSidelineSeasonStart);
            break;
          }
          if (TeamDataCache.IsRival(seasonModeData.UserTeamIndex, teamOpponentForWeek))
          {
            AppSounds.PlayOC(EOCTypes.kRivalry);
            break;
          }
          if (self.userTeamData.CurrentSeasonStats.streak > 0)
          {
            AppSounds.PlayOC(EOCTypes.kWinStreak);
            break;
          }
          break;
      }
    }
    else if (AppState.GameMode != EGameMode.kHeroMoment && AppState.GameMode != EGameMode.k2MD && TeamDataCache.IsRival(PersistentData.GetUserTeamIndex(), PersistentData.GetCompTeamIndex()))
      AppSounds.PlayOC(EOCTypes.kRivalry);
    this.StartCoroutine(this.WaitToStartPlayerChatter());
    this.UpdateCrowdVolume();
    this._gameStarted = true;
  }

  private IEnumerator WaitToStartPlayerChatter()
  {
    this._isChatterAllowed = false;
    yield return (object) new WaitForSeconds(5f);
    if (AppState.GameMode != EGameMode.kPracticeMode)
      this._isChatterAllowed = true;
    this.PlayPlayerChatter(true);
  }

  private void HandlePlayerHitGround() => AppSounds.PlaySfx(ESfxTypes.kUserSackedGround);

  private void HandleTimeout()
  {
    Debug.Log((object) "GameAudioManager: HandleTimeout");
    this.StartCoroutine(AppSounds.PlayTimeout(this._malePlayer, global::Game.IsPlayerOneOnOffense));
  }

  private void HandleHikeComplete()
  {
    AppSounds.Play3DSfx(ESfxTypes.kCatchBall, Ball.State.BallPosition);
    AppSounds.PlaySfx(ESfxTypes.kQBBreathing);
  }

  private void GetAdditionalCrowdVolumeAdjustments()
  {
    int num = Mathf.Abs(ProEra.Game.MatchState.Stats.GetHomeScore() - ProEra.Game.MatchState.Stats.GetAwayScore());
    if (!MatchManager.instance.timeManager.IsFourthQuarter() || num > 16)
      return;
    AppSounds.AMBIENT_MOD = 0.1f;
  }

  public void HandleGameOver()
  {
    AppSounds.PlayCrowd(ECrowdTypes.kTouchdownHome);
    AppSounds.StopCrowd(ECrowdTypes.kCrowdHomeTeamChant, 3);
    if (ProEra.Game.MatchState.Stats.User.Score > ProEra.Game.MatchState.Stats.Comp.Score)
      AppSounds.PlayOC(EOCTypes.kGameWrapWin);
    else
      AppSounds.PlayOC(EOCTypes.kGameWrapLoss);
  }

  public void HandleReadyToHike()
  {
    if (global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB && SingletonBehaviour<BallManager, MonoBehaviour>.instance.ballState == EBallState.InCentersHandsBeforeSnap)
    {
      AppSounds.PlayVO(EVOTypes.kSet, this._malePlayer);
    }
    else
    {
      if (!global::Game.IsPlayerOneOnDefense || !global::Game.IsRunOrPass)
        return;
      AppSounds.PlayVO3D(EVOTypes.kSet3D, global::Game.OffensiveQB.trans);
    }
  }

  private void HandleHomeTeamTurnover()
  {
    if (this._didPlayDefense)
      return;
    ScoreboardAnimations.PlayAnimation(ScoreboardAnimations.BoardAnimType.Defense, PersistentData.GetHomeTeamIndex());
    AppSounds.PlayCrowd(ECrowdTypes.kCrowdDefenseChant);
    this._didPlayDefense = true;
  }
}
