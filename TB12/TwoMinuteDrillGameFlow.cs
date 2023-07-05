// Decompiled with JetBrains decompiler
// Type: TB12.TwoMinuteDrillGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using ProEra.Game;
using System;
using TB12.UI;
using UDB;
using UnityEngine;

namespace TB12
{
  public class TwoMinuteDrillGameFlow : AxisGameFlow
  {
    [SerializeField]
    private int _startingYardLine = 10;
    [SerializeField]
    private int _yardageAddedPerTD = 5;
    private int _currentYardLine;
    private int _currentScore;
    private bool _caughtPass;
    private bool _calledAudible;
    private int _clockStopCount;
    private int _audibleCount;
    private int _downTurnoverCount;
    private bool _didShowIntro;
    private bool _doReset;
    private bool _firstReset = true;
    private float _gameStartTime;
    public const int PASS_COMPLETION_SCORE = 25;
    public const int TEN_COMPLETION_SCORE = 100;
    public const int TWENTY_COMPLETION_SCORE = 250;
    public const int THIRTY_COMPLETION_SCORE = 350;
    public const int FIFTY_COMPLETION_SCORE = 450;
    public const int INTERCEPTION_SCORE = -250;
    public const int TOUCHDOWN_SCORE = 500;
    public const int CLOCK_STOP_SCORE = 50;
    public const int AUDIBLE_SCORE = 10;
    public const int SACKED_SCORE = -150;
    public const int TURNOVER_FOR_DOWNS_SCORE = -250;
    public const int PAT_SCORE = 375;
    public const int RUSH_HIGH_YARDAGE_SCORE = 50;
    public const int RUSH_TD_SCORE = 500;
    public const int TOTAL_YARDAGE_SCORE = 10;
    public const float SCORE_14_PERCENT = 0.15f;
    public const float SCORE_21_PERCENT = 0.3f;
    public float[] DIFFICULTY_BONUS_PERCENT = new float[3]
    {
      0.0f,
      0.5f,
      1f
    };

    public int ClockStops => this._clockStopCount;

    public int AudibleCount => this._audibleCount;

    public int DownTurnovers => this._downTurnoverCount;

    protected override void HandleGameEvent(PEGameplayEvent e)
    {
      switch (e)
      {
        case PEBallCaughtEvent peEvent:
          this.OnBallCaught(peEvent);
          break;
        case PEPlayOverEvent pePlayOverEvent:
          this.OnPlayComplete(pePlayOverEvent.Type.ToString());
          break;
        case PEBallHikedEvent _:
          this.OnBallSnapped();
          break;
        case PEQuarterEndEvent peQuarterEndEvent:
          this.OnQuarterEnd(peQuarterEndEvent.Quarter);
          break;
        case PEAudibleCalledEvent _:
          this.OnAudibleCalled();
          break;
        case PEAboutToCallOffPlayEvent _:
          this.OnNextPlayReady();
          break;
        case PEHandoffAbortedEvent _:
        case PEBallHandoffEvent _:
          base.HandleGameEvent(e);
          break;
        case PEHandoffTimeReachedEvent _:
          this._idealHandoffTimeReached = true;
          break;
      }
    }

    private void OnBallCaught(PEBallCaughtEvent peEvent)
    {
      if (!peEvent.Interception)
      {
        this._caughtPass = true;
        this.AddToScore(25, peEvent.Receiver.trans);
      }
      else
        this.AddToScore(-250, peEvent.Receiver.trans);
    }

    private void OnNextPlayReady()
    {
      if (!this._doReset)
        return;
      this.ResetBallToStart();
    }

    private void OnPlayComplete(string type)
    {
      EPlayEndType eplayEndType = (EPlayEndType) Enum.Parse(typeof (EPlayEndType), "k" + type);
      bool flag1 = !Field.FurtherDownfield(SingletonBehaviour<BallManager, MonoBehaviour>.instance.GetForwardProgressLine(), ProEra.Game.MatchState.FirstDown.Value) || eplayEndType == EPlayEndType.kIncomplete;
      string[] strArray = new string[15];
      strArray[0] = "TwoMinuteDrillGameFlow: epet[";
      strArray[1] = eplayEndType.ToString();
      strArray[2] = "] Game.IsRunningPAT[";
      bool flag2 = global::Game.IsRunningPAT;
      strArray[3] = flag2.ToString();
      strArray[4] = "] MatchManager.turnover[";
      flag2 = MatchManager.turnover;
      strArray[5] = flag2.ToString();
      strArray[6] = "] MatchState.Down.Value == 4[";
      flag2 = ProEra.Game.MatchState.Down.Value == 4;
      strArray[7] = flag2.ToString();
      strArray[8] = "] isNotFirstDown[";
      strArray[9] = flag1.ToString();
      strArray[10] = "] BallManager.instance.GetForwardProgressLine()[";
      float forwardProgressLine = SingletonBehaviour<BallManager, MonoBehaviour>.instance.GetForwardProgressLine();
      strArray[11] = forwardProgressLine.ToString();
      strArray[12] = "] MatchState.FirstDown.Value[";
      forwardProgressLine = ProEra.Game.MatchState.FirstDown.Value;
      strArray[13] = forwardProgressLine.ToString();
      strArray[14] = "]";
      Debug.Log((object) string.Concat(strArray));
      int amount = 0;
      bool flag3 = MatchManager.instance.playManager.savedOffPlay.GetPlayType() == global::PlayType.Run;
      bool flag4 = MatchManager.turnover || ProEra.Game.MatchState.Down.Value == 4 & flag1;
      int num1 = Field.ConvertDistanceToYards(SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position.z - MatchManager.instance.savedLineOfScrim) * global::Game.OffensiveFieldDirection;
      if (num1 > 0)
      {
        if (this._caughtPass)
        {
          if (num1 >= 50)
            amount += 450;
          else if (num1 >= 30)
            amount += 350;
          else if (num1 >= 20)
            amount += 250;
          else if (num1 >= 10)
            amount += 100;
        }
        if (eplayEndType == EPlayEndType.kOOB)
        {
          ++this._clockStopCount;
          amount += 50;
        }
        if (this._calledAudible)
          amount += 10;
        if (flag3 && num1 >= 5)
          amount += 50;
      }
      this._doReset = false;
      if (eplayEndType == EPlayEndType.kTouchdown && !global::Game.IsRunningPAT)
      {
        this._currentYardLine += this._yardageAddedPerTD;
        int num2 = amount + 50;
        ProEra.Game.MatchState.Stats.User.StoreDriveEnd(DriveEndType.Touchdown);
        ++this._clockStopCount;
        amount = !flag3 ? num2 + 500 : num2 + 500;
      }
      else if (flag4 || global::Game.IsRunningPAT)
      {
        if (ProEra.Game.MatchState.Down.Value == 4)
        {
          amount += -250;
          ++this._downTurnoverCount;
        }
        else if (global::Game.IsRunningPAT && eplayEndType == EPlayEndType.kTouchdown)
          amount += 375;
        if (global::Game.IsRunningPAT)
        {
          this._doReset = true;
          ProEra.Game.MatchState.Down.Value = 1;
          MatchManager.instance.SetCurrentMatchState(EMatchState.UserOnOffense);
          MatchManager.turnover = false;
        }
      }
      if (amount != 0)
      {
        Transform trans = global::Game.OffensiveQB.trans;
        if ((UnityEngine.Object) MatchManager.instance.playersManager.ballHolder != (UnityEngine.Object) null)
          trans = MatchManager.instance.playersManager.ballHolder.transform;
        this.AddToScore(amount, trans);
      }
      this._caughtPass = false;
      this._calledAudible = false;
      GameplayUI.HidePointer();
      this._idealHandoffTimeReached = false;
      this._ballHasBeenInSweetSpot = false;
      this._ballHasLeftSweetSpot = false;
      if (!flag4)
        return;
      this.GameOver();
    }

    private void ResetBallToStart()
    {
      Debug.Log((object) "TwoMinuteDrillGameFlow: ResetBallToStart");
      ProEra.Game.MatchState.Down.Value = 1;
      FieldState.OffenseGoingNorth.Value = true;
      MatchManager.instance.SetBallOn(Field.NORTH_GOAL_LINE - (float) this._currentYardLine * Field.ONE_YARD);
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
      this._currentYardLine = this._startingYardLine;
      this._currentScore = 0;
      this._stopQBFollow = false;
      if (this._firstReset)
      {
        this._firstReset = false;
        this._gameStartTime = Time.time;
      }
      ProEra.Game.MatchState.Reset();
      MatchManager.instance.playersManager.userTeamData.CurrentGameStats.Reset();
      MatchManager.instance.playersManager.userTeamData.GetPlayer(0).CurrentGameStats = new PlayerStats();
      MatchManager.instance.playersManager.StopAISnapBallCoroutine();
      MatchManager.instance.timeManager.SetQuarter(4);
      MatchManager.instance.timeManager.SetDisplayMinutes(2);
      MatchManager.instance.timeManager.SetDisplaySeconds(0);
      MatchManager.instance.timeManager.SetRunGameClock(false);
      MatchManager.instance.timeManager.SetRunPlayClock(false);
      MatchManager.instance.timeManager.clockEnabled = true;
      ScoreClockState.Quarter.Value = MatchManager.instance.timeManager.GetQuarterString();
      ProEra.Game.MatchState.IsKickoff.Value = false;
      ProEra.Game.MatchState.GameLength.Value = 120;
      ProEra.Game.MatchState.Stats.User.Score = 0;
      ProEra.Game.MatchState.Stats.Comp.Score = 0;
      SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance.ResetTimeouts();
      ScoreClockState.SetGameClock(2, 0);
      MatchManager.instance.timeManager.ResetGameClock();
      MatchManager.instance.timeManager.ResetPlayClock();
      this.ResetBallToStart();
      MatchManager.instance.SetCurrentMatchState(EMatchState.UserOnOffense);
      MatchManager.instance.playersManager.SetAfterPlayActionsForAllPlayers();
      MatchManager.instance.playersManager.PutAllPlayersInHuddle();
      MatchManager.instance.playManager.SelectNextOffPlayForUser();
    }

    private void OnQuarterEnd(int quarter) => this.GameOver();

    private void GameOver()
    {
      Transform sidelinePosition = this.GetSidelinePosition();
      VREvents.BlinkMovePlayer.Trigger(1f, sidelinePosition.position.SetZ(0.0f), (double) sidelinePosition.position.x > 0.0 ? Quaternion.Euler(0.0f, 270f, 0.0f) : Quaternion.Euler(0.0f, 90f, 0.0f));
      this._stopQBFollow = true;
      ProEra.Game.MatchState.CurrentMatchState.SetValue(EMatchState.End);
      MatchManager.instance.timeManager.SetRunGameClock(false);
      MatchManager.instance.timeManager.SetRunPlayClock(false);
      MatchManager.instance.timeManager.clockEnabled = false;
      MatchManager.instance.playManager.StopAIPickPlayCoroutine();
      this.SetWristPlayCallVisibility(false);
      this.SetPlayConfirmButtonVisibility(false);
      this.SetHurryUpVisibility(false);
      this.SetTimeoutVisibility(false);
      EligibilityManager.Instance.TurnOffReceiverUI();
      this._scene.DeactivateSweetSpot();
      UIDispatch.FrontScreen.DisplayView(EScreens.k2MDResults);
    }

    protected override void HandleMatchStateChanged(EMatchState state)
    {
      if (state == EMatchState.Beginning || this._didShowIntro)
        return;
      this._didShowIntro = true;
      ((TwoMinuteDrillGameScene) this._scene).ShowIntroScreen();
    }

    private void AddToScore(int amount, Transform trans)
    {
      this._currentScore += amount;
      OnFieldCanvas.Instance.ShowArcadeScoreText(amount, trans.position + new Vector3(0.0f, 2f, 0.0f));
    }

    private void OnAudibleCalled()
    {
      this.UpdatePointerState();
      EligibilityManager.Instance.TurnOffReceiverUI();
      EligibilityManager.Instance.UpdateEligibleReceivers();
      this._calledAudible = true;
      ++this._audibleCount;
    }

    protected override void HandleQBTackle(bool isTackled)
    {
      base.HandleQBTackle(isTackled);
      int num = Field.ConvertDistanceToYards(SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position.z - MatchManager.instance.savedLineOfScrim) * global::Game.OffensiveFieldDirection;
      if (!isTackled || num > 0)
        return;
      this.AddToScore(-150, global::Game.OffensiveQB.trans);
    }
  }
}
