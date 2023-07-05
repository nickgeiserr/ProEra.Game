// Decompiled with JetBrains decompiler
// Type: Game
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game;
using System.Collections.Generic;
using UnityEngine;
using Vars;

public static class Game
{
  private static bool isFirstTime = true;
  private static PlayDataDef _defPlay;
  private static PlayDataOff _offPlay;

  public static int OffensiveFieldDirection => FieldState.OffensiveFieldDirection;

  public static bool OffenseGoingNorth => (bool) FieldState.OffenseGoingNorth;

  public static TeamFile OffensiveTeamFile => global::Game.IsPlayerOneOnOffense ? (!PersistentData.userIsHome ? PersistentData.GetAwayTeamData().teamFile : PersistentData.GetHomeTeamData().teamFile) : (!PersistentData.userIsHome ? PersistentData.GetHomeTeamData().teamFile : PersistentData.GetAwayTeamData().teamFile);

  public static bool IsPlaybookVisible => (bool) PlaybookState.IsShown.GetValue();

  public static bool HasScreenOverlay => false;

  public static PlayerAI OffensiveQB => global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[5] : MatchManager.instance.playersManager.curCompScriptRef[5];

  public static PlayerAI OffensiveCenter => global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[2] : MatchManager.instance.playersManager.curCompScriptRef[2];

  public static PlayerAI Punter => global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[6] : MatchManager.instance.playersManager.curCompScriptRef[6];

  public static PlayerAI Kicker => global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[6] : MatchManager.instance.playersManager.curCompScriptRef[6];

  public static PlayerAI Holder => global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[5] : MatchManager.instance.playersManager.curCompScriptRef[5];

  public static List<PlayerAI> OffensivePlayers => global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef : MatchManager.instance.playersManager.curCompScriptRef;

  public static List<PlayerAI> DefensivePlayers => global::Game.IsPlayerOneOnDefense ? MatchManager.instance.playersManager.curUserScriptRef : MatchManager.instance.playersManager.curCompScriptRef;

  public static BaseFormation BaseFormation_Offense => MatchManager.instance.playManager.savedOffPlay.GetFormation().GetBaseFormation();

  public static int PrimaryReceiverIndex => MatchManager.instance.playManager.savedOffPlay.GetPrimaryReceiver();

  public static PlayerAI HandoffTarget => global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[MatchManager.instance.playManager.savedOffPlay.GetHandoffTarget()] : MatchManager.instance.playersManager.curCompScriptRef[MatchManager.instance.playManager.savedOffPlay.GetHandoffTarget()];

  public static PlayerAI PlayActionTarget => global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[MatchManager.instance.playManager.savedOffPlay.GetPlayActionTargetIndex()] : MatchManager.instance.playersManager.curCompScriptRef[MatchManager.instance.playManager.savedOffPlay.GetPlayActionTargetIndex()];

  public static bool QBHasBallInPocket => PlayState.IsPass && !MatchManager.instance.playersManager.ballWasThrownOrKicked && !MatchManager.instance.playersManager.convergeOnBall;

  public static bool PlayHasHandoff => global::Game.IsRun && !global::Game.IsPitchPlay && (Object) global::Game.HandoffTarget != (Object) MatchManager.instance.playersManager.GetCurrentSnapTarget();

  public static bool IsPitchPlay
  {
    get
    {
      HandoffType handoffType = MatchManager.instance.playManager.savedOffPlay.GetHandoffType();
      switch (handoffType)
      {
        case HandoffType.GunTossRight:
        case HandoffType.GunTossLeft:
        case HandoffType.UnderCenterTossLeft:
          return true;
        default:
          return handoffType == HandoffType.UnderCenterTossRight;
      }
    }
  }

  public static bool IsPitchPlayUnderCenter
  {
    get
    {
      HandoffType handoffType = MatchManager.instance.playManager.savedOffPlay.GetHandoffType();
      return handoffType == HandoffType.UnderCenterTossLeft || handoffType == HandoffType.UnderCenterTossRight;
    }
  }

  public static bool BallHolderIsNull => (Object) MatchManager.instance.playersManager.ballHolder == (Object) null;

  public static bool BallHolderIsNotNull => (Object) MatchManager.instance.playersManager.ballHolder != (Object) null;

  public static bool BallHolderIsUser => (Object) MatchManager.instance.playersManager.ballHolderScript == (Object) global::Game.OffensiveQB && global::Game.CurrentPlayHasUserQBOnField;

  public static bool P1IsButtonPassing => PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP1;

  public static bool P1IsAimedPassing => !PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP1;

  public static bool P2IsButtonPassing => PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP2;

  public static bool P2IsAimedPassing => !PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP2;

  public static bool IsOnsidesKick => MatchManager.instance.onsideKick;

  public static bool IsNotOnsidesKick => !MatchManager.instance.onsideKick;

  public static bool FumbleOccured => MatchManager.instance.fumbleOccured;

  public static bool FumbleDidNotOccured => !MatchManager.instance.fumbleOccured;

  public static bool IsConvergeOnBall => MatchManager.instance.playersManager.convergeOnBall;

  public static bool IsNotConvergeOnBall => !MatchManager.instance.playersManager.convergeOnBall;

  public static bool IsQuickMatch => PersistentData.gameType == GameType.QuickMatch;

  public static bool IsSeasonMode => PersistentData.gameType == GameType.SeasonMode;

  public static bool Is2PMatch => PersistentData.Is2PMatch;

  public static bool IsNot2PMatch => !PersistentData.Is2PMatch;

  public static bool UserCallsPlays => PersistentData.UserCallsPlays;

  public static bool UserDoesNotCallPlays => !PersistentData.UserCallsPlays;

  public static bool CoachCallsPlays => PersistentData.CoachCallsPlays;

  public static bool UserControlsPlayers => PersistentData.UserControlsPlayers;

  public static bool UserControlsQB => PersistentData.UserControlsQB;

  public static bool UserDoesNotControlPlayers => !PersistentData.UserControlsPlayers;

  public static bool IsSpectateMode => PersistentData.GameMode == GameMode.Spectate;

  public static bool IsNotSpectateMode => PersistentData.GameMode != GameMode.Spectate;

  public static bool IsPlayerOneOnOffense => ProEra.Game.MatchState.IsPlayerOneOnOffense;

  public static bool IsPlayerOneOnDefense => ProEra.Game.MatchState.IsPlayerOneOnDefense;

  public static bool IsPlayerTwoOnOffense => ProEra.Game.MatchState.IsPlayerTwoOnOffense;

  public static bool IsPlayerTwoOnDefense => ProEra.Game.MatchState.IsPlayerTwoOnDefense;

  public static bool IsHomeTeamOnOffense => ProEra.Game.MatchState.IsHomeTeamOnOffense;

  public static bool IsAwayTeamOnOffense => ProEra.Game.MatchState.IsAwayTeamOnOffense;

  public static bool GameIsTied => ProEra.Game.MatchState.Stats.EqualScore();

  public static bool GameIsNotTied => !global::Game.GameIsTied;

  public static bool IsOffenseWinning => ProEra.Game.MatchState.IsOffenseWinning;

  public static bool IsDefenseWinning => !global::Game.IsOffenseWinning;

  public static bool IsPlayActive => MatchManager.instance.playManager.PlayActive;

  public static bool IsPlayInactive => !MatchManager.instance.playManager.PlayActive;

  public static bool IsPlayCleanedUp => MatchManager.instance.playManager.playIsCleanedUp;

  public static bool IsPass => PlayState.IsPass;

  public static bool IsNotPass => !global::Game.IsPass;

  public static bool IsRun => PlayState.IsRun;

  public static bool IsNotRun => !PlayState.IsRun;

  public static bool IsRunOrPass => PlayState.IsRunOrPass;

  public static bool IsKickoff => PlayState.IsKickoff;

  public static bool IsNotKickoff => !PlayState.IsKickoff;

  public static bool IsFG => PlayState.PlayType.Value == PlayType.FG;

  public static bool IsNotFG => PlayState.PlayType.Value != PlayType.FG;

  public static bool IsPunt => PlayState.IsPunt;

  public static bool IsNotPunt => !PlayState.IsPunt;

  public static bool IsPlayAction => MatchManager.instance.playManager.playTypeSpecific == PlayTypeSpecific.PlayAction;

  public static bool IsDrawPlay => MatchManager.instance.playManager.playTypeSpecific == PlayTypeSpecific.Draw;

  public static bool IsQBKneel => MatchManager.instance.playManager.playTypeSpecific == PlayTypeSpecific.QB_Kneel;

  public static bool IsQBSpike => MatchManager.instance.playManager.playTypeSpecific == PlayTypeSpecific.QB_Spike;

  public static bool IsQBKeeper => MatchManager.instance.playManager.playTypeSpecific == PlayTypeSpecific.QB_Keeper;

  public static bool IsReadOption => MatchManager.instance.playManager.playTypeSpecific == PlayTypeSpecific.ReadOption;

  public static bool IsDesignedQBRun => global::Game.IsQBKeeper || global::Game.IsReadOption;

  public static bool CanFakeHandoff => global::Game.IsPlayAction || global::Game.IsReadOption;

  public static bool IsPuntDefense
  {
    get
    {
      global::Game._defPlay = MatchManager.instance.playManager.savedDefPlay;
      return global::Game._defPlay == Plays.self.dspc_puntReturnLeft || global::Game._defPlay == Plays.self.dspc_puntReturnRight || global::Game._defPlay == Plays.self.dspc_puntBlock;
    }
  }

  public static bool IsSpecialTeamsPlay => PlayState.PlayType.Value == PlayType.Punt || PlayState.PlayType.Value == PlayType.FG || PlayState.PlayType.Value == PlayType.Kickoff;

  public static bool BallIsThrownOrKicked => MatchManager.instance.playersManager.ballWasThrownOrKicked;

  public static bool BallIsNotThrownOrKicked => !MatchManager.instance.playersManager.ballWasThrownOrKicked;

  public static bool IsTurnover => (bool) ProEra.Game.MatchState.Turnover;

  public static bool IsNotTurnover => !(bool) ProEra.Game.MatchState.Turnover;

  public static bool IsPlayOver => (bool) PlayState.PlayOver;

  public static bool PlayIsNotOver => !(bool) PlayState.PlayOver;

  public static bool IsRunningPAT => (bool) ProEra.Game.MatchState.RunningPat;

  public static bool IsNotRunningPAT => !(bool) ProEra.Game.MatchState.RunningPat;

  public static bool CurrentPlayHasUserQBOnField
  {
    get
    {
      if (!global::Game.IsPlayerOneOnOffense || !global::Game.IsNotKickoff || !global::Game.IsNotPunt || !global::Game.IsNotFG)
        return false;
      return global::Game.IsNotRunningPAT || MatchManager.instance.playManager.savedOffPlay.GetPlayType() != PlayType.FG;
    }
  }

  public static bool PET_IsOOB => MatchManager.instance.playManager.playEndType == PlayEndType.OOB;

  public static bool PET_IsOOB_In_Endzone => MatchManager.instance.playManager.playEndType == PlayEndType.OOB_In_Endzone;

  public static bool PET_IsTackle => MatchManager.instance.playManager.playEndType == PlayEndType.Tackle;

  public static bool PET_IsTouchdown => MatchManager.instance.playManager.playEndType == PlayEndType.Touchdown;

  public static bool PET_IsNotTouchdown => MatchManager.instance.playManager.playEndType != PlayEndType.Touchdown;

  public static bool PET_IsIncomplete => MatchManager.instance.playManager.playEndType == PlayEndType.Incomplete;

  public static bool PET_IsNotIncomplete => MatchManager.instance.playManager.playEndType != PlayEndType.Incomplete;

  public static bool PET_IsMadeFG => MatchManager.instance.playManager.playEndType == PlayEndType.MadeFG;

  public static bool PET_IsMissedFG => MatchManager.instance.playManager.playEndType == PlayEndType.MissedFG;

  public static bool PET_IsSafety => MatchManager.instance.playManager.playEndType == PlayEndType.Safety;

  public static bool PET_IsNotSafety => MatchManager.instance.playManager.playEndType != PlayEndType.Safety;

  public static bool PET_IsQBSlide => MatchManager.instance.playManager.playEndType == PlayEndType.QBSlide;

  public static bool BS_InCentersHandsBeforeSnap => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.InCentersHandsBeforeSnap;

  public static bool BS_IsPlayersHands => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.PlayersHands;

  public static bool BS_IsInAirPass => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.InAirPass;

  public static bool BS_IsInAirToss => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.InAirToss;

  public static bool BS_IsOnGround => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.OnGround;

  public static bool BS_IsKick => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.Kick;

  public static bool BS_IsFumble => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.Fumble;

  public static bool BS_IsPlayOver => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.PlayOver;

  public static bool BS_IsInAirSnap => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.InAirSnap;

  public static bool BS_IsInAirDeflected => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.InAirDeflected;

  public static bool BS_OnTee => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.OnTee;

  public static bool BS_IsInAirDrop => (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.InAirDrop;

  public static bool IsSoundOn => MatchManager.instance.soundOn;

  public static bool IsSoundOff => !MatchManager.instance.soundOn;

  public static bool IsBallHolder(GameObject holderTest) => (Object) MatchManager.instance.playersManager.ballHolder == (Object) holderTest;

  public static bool CanUserRunHurryUp() => global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB && global::Game.IsPlayOver && !global::Game.IsPlayActive && !(bool) ProEra.Game.MatchState.IsKickoff && !(bool) ProEra.Game.MatchState.RunningPat && !global::Game.PET_IsTouchdown && PlayState.PlayOver.Value && !MatchManager.instance.playManager.ShouldOffenseHurryUp && MatchManager.instance.playManager.HasSavedUserPlay();

  public static T GetGameplayConfig<T>() where T : IGameplayConfig => PersistentSingleton<GameplayConfig>.Instance.GetConfig<T>();

  public static DefensivePlayCallingConfig DefensivePlayCallingConfig => global::Game.GetGameplayConfig<DefensivePlayCallingConfig>();

  public static FieldGoalConfig FieldGoalConfig => global::Game.GetGameplayConfig<FieldGoalConfig>();

  public static HandoffConfig HandoffConfig => global::Game.GetGameplayConfig<HandoffConfig>();

  public static KickoffConfig KickoffConfig => global::Game.GetGameplayConfig<KickoffConfig>();

  public static KickReturnConfig KickReturnConfig => global::Game.GetGameplayConfig<KickReturnConfig>();

  public static PassBlockConfig PassBlockConfig => global::Game.GetGameplayConfig<PassBlockConfig>();

  public static PostPlayConfig PostPlayConfig => global::Game.GetGameplayConfig<PostPlayConfig>();

  public static PreplayConfig PreplayConfig => global::Game.GetGameplayConfig<PreplayConfig>();

  public static ThrowingConfig ThrowingConfig => global::Game.GetGameplayConfig<ThrowingConfig>();

  public static TwoPointConfig TwoPointConfig => global::Game.GetGameplayConfig<TwoPointConfig>();

  public static UserMovementConfig UserMovementConfig => global::Game.GetGameplayConfig<UserMovementConfig>();

  public static ZoneCoverageConfig ZoneCoverageConfig => global::Game.GetGameplayConfig<ZoneCoverageConfig>();

  public static SuperSimTuningConfig SuperSimTuningConfig => global::Game.GetGameplayConfig<SuperSimTuningConfig>();
}
