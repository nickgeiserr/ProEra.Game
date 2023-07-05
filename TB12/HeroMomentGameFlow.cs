// Decompiled with JetBrains decompiler
// Type: TB12.HeroMomentGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using Framework;
using ProEra.Game;
using System;
using System.Collections;
using TB12.AppStates;
using TB12.UI;
using UDB;
using UnityEngine;

namespace TB12
{
  public class HeroMomentGameFlow : AxisGameFlow
  {
    [SerializeField]
    private int _startingYardLine = 10;
    private RoutineHandle _audioHandler = new RoutineHandle();

    protected override void Awake()
    {
      base.Awake();
      ((HeroMomentGameState) this._state).FinishedEnable += new System.Action(this.HandleTunnelFinished);
      this._showTutorialScreen = true;
    }

    protected override void HandleGameEvent(PEGameplayEvent e)
    {
      switch (e)
      {
        case PEPlayOverEvent pePlayOverEvent:
          this.OnPlayComplete(pePlayOverEvent.Type.ToString());
          break;
        case PEBallHikedEvent _:
          this.OnBallSnapped();
          this._audioHandler.Stop();
          AppSounds.StopVO(EVOTypes.kLamarHeroMoment1);
          AppSounds.StopVO(EVOTypes.kLamarHeroMoment2);
          AppSounds.PlayVO(EVOTypes.kLamarHeroMoment3);
          break;
        case PEBallThrownEvent _:
          AppSounds.StopVO(EVOTypes.kLamarHeroMoment3);
          AppSounds.PlayVO(EVOTypes.kLamarHeroMoment4);
          break;
      }
    }

    private void OnPlayComplete(string type)
    {
      if ((EPlayEndType) Enum.Parse(typeof (EPlayEndType), "k" + type) == EPlayEndType.kTouchdown && !(bool) ProEra.Game.MatchState.Turnover)
      {
        AnalyticEvents.Record<HeroMomentCompletedArgs>(new HeroMomentCompletedArgs(true));
        ProEra.Game.MatchState.CurrentMatchState.SetValue(EMatchState.End);
        MatchManager.instance.playManager.StopAIPickPlayCoroutine();
        this.StartCoroutine(this.WaitBeforeLeaving());
      }
      else
        this.StartCoroutine(this.WaitBeforeAllowingRetry());
    }

    private IEnumerator WaitBeforeAllowingRetry()
    {
      HeroMomentGameFlow heroMomentGameFlow = this;
      yield return (object) new WaitUntil((Func<bool>) (() => MatchManager.instance.playManager.playIsCleanedUp));
      MatchManager.instance.timeManager.SetQuarter(4);
      MatchManager.instance.timeManager.SetDisplayMinutes(0);
      MatchManager.instance.timeManager.SetDisplaySeconds(1);
      ProEra.Game.MatchState.GameLength.Value = 1;
      ScoreClockState.SetGameClock(0, 1);
      heroMomentGameFlow._didThrowBall = false;
      AnalyticEvents.Record<HeroMomentCompletedArgs>(new HeroMomentCompletedArgs(false));
      UIDispatch.FrontScreen.DisplayView(EScreens.kHeroMomentFailure);
    }

    private IEnumerator WaitBeforeLeaving()
    {
      HeroMomentGameFlow heroMomentGameFlow = this;
      ((HeroMomentGameState) heroMomentGameFlow._state).FinishedEnable -= new System.Action(heroMomentGameFlow.HandleTunnelFinished);
      yield return (object) new WaitForSeconds(4f);
      AppSounds.StopCrowd(ECrowdTypes.kTouchdownHome, 1);
      AppSounds.StopCrowd(ECrowdTypes.kCloseGameScore, 1);
      AppSounds.StopCrowd(ECrowdTypes.kCrowdBuildupPrePlay, 1);
      AppSounds.StopCrowd(ECrowdTypes.kCrowdCheerBig, 1);
      AppSounds.StopVO(EVOTypes.kLamarHeroMoment4);
      AppSounds.PlayVO(EVOTypes.kLamarHeroMoment5);
      yield return (object) new WaitForSeconds(4.97f);
      AppEvents.LoadMainMenu.Trigger();
    }

    private void ResetBallToStart()
    {
      ProEra.Game.MatchState.Down.Value = 4;
      FieldState.OffenseGoingNorth.Value = true;
      MatchManager.instance.SetBallOn(Field.NORTH_GOAL_LINE - (float) this._startingYardLine * Field.ONE_YARD);
      MatchManager.instance.SetBallHashPosition(0.0f);
      MatchManager.instance.savedLineOfScrim = ProEra.Game.MatchState.BallOn.Value;
      ProEra.Game.MatchState.FirstDown.Value = ProEra.Game.MatchState.BallOn.Value + (float) (10.0 * ((double) Field.ONE_YARD * (double) global::Game.OffensiveFieldDirection));
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetFirstDownLine();
      MatchManager.instance.ballManager.SetPosition(new Vector3(0.0f, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
    }

    public void ResetGame()
    {
      this._gameStarted = true;
      this._stopQBFollow = false;
      ProEra.Game.MatchState.Reset();
      MatchManager.instance.playersManager.userTeamData.CurrentGameStats.Reset();
      MatchManager.instance.playersManager.StopAISnapBallCoroutine();
      ProEra.Game.MatchState.Stats.User.Score = 24;
      ProEra.Game.MatchState.Stats.Comp.Score = 28;
      GameTimeoutState.UserTimeouts.SetValue(0);
      GameTimeoutState.CompTimeouts.SetValue(0);
      MatchManager.instance.timeManager.SetQuarter(4);
      MatchManager.instance.timeManager.SetDisplayMinutes(0);
      MatchManager.instance.timeManager.SetDisplaySeconds(1);
      MatchManager.instance.timeManager.SetRunGameClock(false);
      MatchManager.instance.timeManager.SetRunPlayClock(false);
      MatchManager.instance.timeManager.clockEnabled = true;
      ScoreClockState.Quarter.Value = MatchManager.instance.timeManager.GetQuarterString();
      ProEra.Game.MatchState.IsKickoff.Value = false;
      ProEra.Game.MatchState.GameLength.Value = 1;
      ScoreClockState.SetGameClock(0, 1);
      this.ResetBallToStart();
      PlaybookState.CurrentFormation.SetValue(Plays.self.shotgunPlays_Normal);
      MatchManager.instance.playManager.savedOffPlay = (PlayDataOff) PlaybookState.CurrentFormation.Value.GetPlay(3);
      PlaybookState.CurrentPlay.SetValue((PlayData) MatchManager.instance.playManager.savedOffPlay);
      MatchManager.instance.SetCurrentMatchState(EMatchState.UserOnOffense);
      MatchManager.instance.playersManager.SetAfterPlayActionsForAllPlayers();
      MatchManager.instance.playManager.SetPlay((PlayData) MatchManager.instance.playManager.savedOffPlay, false, false, true);
      MatchManager.instance.playersManager.PutAllPlayersInPlayPosition();
      MatchManager.instance.playManager.canUserCallAudible = false;
      this.SetWristPlayCallVisibility(true);
      this.SetPlayConfirmButtonVisibility(false);
      this.SetTimeoutVisibility(false);
    }

    private void HandleTunnelFinished() => this._audioHandler.Run(this.PlayInitialAudio());

    private IEnumerator PlayInitialAudio()
    {
      AppSounds.PlayOC(EOCTypes.kHeroIntro);
      yield return (object) new WaitForSeconds(11.4f);
      AppSounds.PlaySfx(ESfxTypes.kHuddleBreak);
      this.ResetGame();
      AppSounds.PlayVO(EVOTypes.kLamarHeroMoment1);
      yield return (object) new WaitForSeconds(6.22f);
      AppSounds.PlayVO(EVOTypes.kLamarHeroMoment2);
    }
  }
}
