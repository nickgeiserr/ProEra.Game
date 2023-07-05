// Decompiled with JetBrains decompiler
// Type: TimeManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using ProEra.Game;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
  public PlayManager playManager;
  public bool clockEnabled;
  public bool WasGameClockRunningBeforeBeingSet;
  [SerializeField]
  private float gameClockTimer;
  [SerializeField]
  private float gameClockLastDown;
  [SerializeField]
  private float playClockTimer;
  [SerializeField]
  private bool runGameClock;
  [SerializeField]
  private bool runPlayClock;
  [SerializeField]
  private bool stopClockAfterPlay;
  [SerializeField]
  private int quarter;
  [SerializeField]
  private int playClockLength;
  [SerializeField]
  private int displaySeconds;
  [SerializeField]
  private int displayMinutes;
  [SerializeField]
  private bool twoMinWarnDone;
  [SerializeField]
  private bool showTwoMinWarningAfterPlay;
  private bool _acceleratedClockEnabled;

  public MatchManager matchManager => MatchManager.instance;

  private void Start()
  {
    this.playClockLength = 40;
    this.gameClockTimer = 0.0f;
    this.quarter = 1;
    this.clockEnabled = true;
    this._acceleratedClockEnabled = true;
    this.SetRunGameClock(false);
    this.SetRunPlayClock(false);
    this.ResetPlayClock();
    this.ResetGameClock();
    this.SetDisplaySeconds(ProEra.Game.MatchState.GameLength.Value % 60);
    this.SetDisplayMinutes(ProEra.Game.MatchState.GameLength.Value / 60 % 60);
  }

  public void AddToPlayClock(float add)
  {
    if (!this.clockEnabled)
      return;
    this.playClockTimer += add;
  }

  public int GetPlayClockTime() => this.playClockLength - (int) this.playClockTimer;

  public void ResetPlayClock()
  {
    this.playClockTimer = 0.0f;
    ScoreClockState.PlayClock.SetValue(this.playClockLength);
  }

  public void SetGameClockToZero()
  {
    this.gameClockTimer = (float) MatchManager.gameLength;
    this.SetDisplaySeconds(0);
    this.SetDisplayMinutes(0);
  }

  public void SetRunPlayClock(bool run)
  {
    if (!GameSettings.GetDifficulty().DelayOfGame)
      run = false;
    this.runPlayClock = run;
    if (run)
      Debug.Log((object) "<b>PLAYCLOCK SET TO TRUE</b>");
    else
      Debug.Log((object) "<b>PLAYCLOCK SET TO FALSE</b>");
  }

  public bool IsPlayClockRunning() => this.runPlayClock;

  public void SetAcceleratedClockEnabled(bool enabled) => this._acceleratedClockEnabled = enabled;

  public void AddToGameClock(float time)
  {
    if (!this.clockEnabled)
      return;
    this.gameClockTimer += time;
    ProEra.Game.MatchState.AddPossessionTime(time);
    this.RunGameClock();
  }

  private void RunGameClock()
  {
    if (!this.clockEnabled)
      return;
    int num = ProEra.Game.MatchState.GameLength.Value - Mathf.CeilToInt(this.gameClockTimer);
    this.SetDisplaySeconds(num % 60);
    this.SetDisplayMinutes(num / 60 % 60);
    if (PersistentSingleton<SaveManager>.Instance.gameSettings.TwoMinuteWarningEnabled && (this.IsSecondQuarter() || this.IsFourthQuarter()) && !this.twoMinWarnDone && this.displayMinutes == 2 && this.displaySeconds == 0)
    {
      this.SetTwoMinuteWarningDone(true);
      if (global::Game.IsPlayActive)
        this.SetShowTwoMinuteWarningAfterPlay(true);
      else if (global::Game.IsPlayOver)
      {
        this.matchManager.HandleTwoMinuteWarning();
        this.SetRunGameClock(false);
      }
      else
      {
        this.matchManager.HandleTwoMinuteWarning();
        this.SetRunGameClock(false);
        this.matchManager.ResetPlay();
        if (global::Game.UserCallsPlays && global::Game.IsPlayerOneOnOffense)
          this.playManager.SelectNextOffPlayForUser();
        else
          this.playManager.SelectNextPlaysForAI();
      }
    }
    TimeState.SetTime(this.displayMinutes, this.displaySeconds);
    ScoreClockState.SetGameClock(this.displayMinutes, this.displaySeconds);
  }

  public void ResetGameClock()
  {
    this.gameClockTimer = 0.0f;
    this.gameClockLastDown = 0.0f;
    this.SetDisplayMinutes((ProEra.Game.MatchState.GameLength.Value - Mathf.CeilToInt(this.gameClockTimer)) / 60 % 60);
    this.SetDisplaySeconds(0);
    if (this.GetDisplayMinutes() == 0)
    {
      Debug.Log((object) "Starting next quarter with 0 minutes due to the initial clock being set to start with less than one minute on the clock.Adding 10 seconds to prevent the next quarter from being automatically triggered.");
      this.SetDisplaySeconds(10);
    }
    TimeState.SetTime(this.displayMinutes, this.displaySeconds);
    ScoreClockState.SetGameClock(this.displayMinutes, this.displaySeconds);
  }

  public float GetGameClockTimer() => this.gameClockTimer;

  public void SetRunGameClock(bool run) => this.runGameClock = run;

  public bool IsGameClockRunning() => this.runGameClock;

  public void SetDisplayMinutes(int v) => this.displayMinutes = v;

  public void SetDisplaySeconds(int v) => this.displaySeconds = v;

  public int GetDisplayMinutes() => this.displayMinutes;

  public int GetDisplaySeconds() => this.displaySeconds;

  public int GetTotalSecondsRemaining() => this.GetDisplaySeconds() + 60 * this.GetDisplayMinutes();

  public int GetTotalSecondsRemainingInGame()
  {
    int secondsRemaining = this.GetTotalSecondsRemaining();
    if (this.quarter < 4)
      secondsRemaining += ProEra.Game.MatchState.GameLength.Value * (4 - this.quarter);
    return secondsRemaining;
  }

  public bool ShowTwoMinuteWarningAfterPlay() => this.showTwoMinWarningAfterPlay;

  public void SetShowTwoMinuteWarningAfterPlay(bool v) => this.showTwoMinWarningAfterPlay = v;

  public void SetTwoMinuteWarningDone(bool v) => this.twoMinWarnDone = v;

  public bool HasGameClockExpired() => (double) this.GetGameClockTimer() >= (double) ProEra.Game.MatchState.GameLength.Value;

  public void DebugEndQuarter()
  {
    this.gameClockTimer = (float) ProEra.Game.MatchState.GameLength.Value;
    this.SetDisplayMinutes(0);
    this.SetDisplaySeconds(0);
  }

  public int GetQuarter() => this.quarter;

  public void SetQuarter(int newQuarter) => this.quarter = newQuarter;

  public void EndOfFirstQuarter() => this.quarter = 2;

  public void EndOfSecondQuarter()
  {
    this.quarter = 3;
    this.matchManager.HandleEndOfQuarter(2);
  }

  public void EndOfThirdQuarter() => this.quarter = 4;

  public void EndOfFourthQuarter() => this.quarter = 5;

  public void EndOfRegulation() => this.matchManager.HandleEndOfQuarter(4);

  public string GetQuarterString()
  {
    if (this.quarter == 1)
      return "1ST QTR";
    if (this.quarter == 2)
      return "2ND QTR";
    if (this.quarter == 3)
      return "3RD QTR";
    if (this.quarter == 4)
      return "4TH QTR";
    return this.quarter == 5 ? "OT" : "";
  }

  public bool IsFirstQuarter() => this.quarter == 1;

  public bool IsSecondQuarter() => this.quarter == 2;

  public bool IsThirdQuarter() => this.quarter == 3;

  public bool IsFourthQuarter() => this.quarter == 4;

  public bool IsInOvertime() => this.quarter == 5;

  public void FinishPlay()
  {
    int num = Mathf.CeilToInt(this.gameClockTimer) - Mathf.CeilToInt(this.gameClockLastDown);
    if ((double) this.gameClockTimer < (double) this.gameClockLastDown)
      num = Mathf.CeilToInt((float) ProEra.Game.MatchState.GameLength.Value - this.gameClockLastDown + this.gameClockTimer);
    this.gameClockLastDown = this.gameClockTimer;
    ProEra.Game.MatchState.Stats.DriveTimeInSeconds += num;
  }

  public void RunPlayClock(int gameLength, bool isSnapAllowed, bool optDelayOfGame)
  {
    if (!global::Game.IsPlayInactive || !this.IsPlayClockRunning() || !optDelayOfGame || !this.clockEnabled)
      return;
    if (((!this.IsGameClockRunning() || (double) gameLength - (double) this.GetGameClockTimer() < 3.0 ? 0 : (this.GetPlayClockTime() > PersistentSingleton<SaveManager>.Instance.gameSettings.AcceleratedClockValue ? 1 : 0)) & (isSnapAllowed ? 1 : 0)) != 0 && this._acceleratedClockEnabled)
    {
      this.AddToGameClock(0.5f);
      this.AddToPlayClock(0.5f);
    }
    else
      this.AddToPlayClock((float) PersistentSingleton<SaveManager>.Instance.gameSettings.ClockSpeed * (1f / 400f) / (float) GameSettings.TimeScale);
    ScoreClockState.PlayClock.SetValue(this.GetPlayClockTime());
  }
}
