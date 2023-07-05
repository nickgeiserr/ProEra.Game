// Decompiled with JetBrains decompiler
// Type: TimeoutManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using ProEra.Game;
using System.Collections;
using UDB;
using UnityEngine;

public class TimeoutManager : SingletonBehaviour<TimeoutManager, MonoBehaviour>
{
  [SerializeField]
  private Playbook _playbookP1;
  [SerializeField]
  private Playbook _playbookP2;
  private const int TIMEOUT_LENGTH = 5;

  public TimeOutState currentTimeOutState { get; private set; }

  public event System.Action OnUserTimeOutCalled;

  private new void Awake()
  {
    GameTimeoutState.UserTimeouts.Value = 3;
    GameTimeoutState.CompTimeouts.Value = 3;
    GameTimeoutState.TimeoutCalledP1.Value = false;
    GameTimeoutState.TimeoutCalledP2.Value = false;
  }

  public static void CallTimeOut(Team team)
  {
    PEGameplayEventManager.RecordTimeoutEvent();
    SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance._CallTimeOut(team);
  }

  public static void EndTimeOut() => SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance._EndTimeOut();

  public static bool NoTimoutCalled() => SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance._NoTimoutCalled();

  public void ResetTimeouts()
  {
    GameTimeoutState.UserTimeouts.Value = 3;
    GameTimeoutState.CompTimeouts.Value = 3;
    GameTimeoutState.TimeoutCalledP1.Value = false;
    GameTimeoutState.TimeoutCalledP2.Value = false;
  }

  public void CheckForAITimeout(Team team)
  {
    if (!this.ShouldAICallTimeout(team))
      return;
    string str = (team == Team.Computer ? (global::Game.IsPlayerOneOnDefense ? 1 : 0) : (global::Game.IsPlayerOneOnOffense ? 1 : 0)) != 0 ? "Offense" : "Defense";
    MatchManager.instance.playManager.AddToCurrentPlayLog(str + " called a timeout");
    Debug.Log((object) (str + " called a timeout"));
    TimeoutManager.CallTimeOut(team);
  }

  protected void _CallTimeOut(Team team)
  {
    if (!global::Game.IsPlayInactive || TimeState.IsZero() && !(bool) ProEra.Game.MatchState.RunningPat)
      return;
    if (!(bool) GameTimeoutState.TimeoutCalledP1 && team == Team.Player1 && (int) GameTimeoutState.UserTimeouts > 0)
    {
      this.TimeoutCalledByTeam(Team.Player1);
      ScoreClockState.ShowTimeout.Trigger(true);
    }
    else if (!(bool) GameTimeoutState.TimeoutCalledP1 && team == Team.Computer && (int) GameTimeoutState.CompTimeouts > 0)
    {
      this.TimeoutCalledByTeam(Team.Computer);
      ScoreClockState.ShowTimeout.Trigger(false);
    }
    else
    {
      if ((bool) GameTimeoutState.TimeoutCalledP2 || team != Team.Player2 || (int) GameTimeoutState.CompTimeouts <= 0)
        return;
      this.TimeoutCalledByTeam(Team.Player2);
      ScoreClockState.ShowTimeout.Trigger(false);
    }
  }

  protected void _EndTimeOut()
  {
    GameTimeoutState.TimeoutCalledP1.Value = false;
    GameTimeoutState.TimeoutCalledP2.Value = false;
    this.currentTimeOutState = TimeOutState.None;
  }

  protected bool _NoTimoutCalled() => GameTimeoutState.NoTimeoutCalled();

  private void TimeoutCalledByTeam(Team team)
  {
    MatchManager.instance.ResetPlay();
    MatchManager.instance.timeManager.SetRunGameClock(false);
    int num = PersistentData.userIsHome ? 1 : 0;
    if (team == Team.Player1)
    {
      GameTimeoutState.TimeoutCalledP1.Value = true;
      --GameTimeoutState.UserTimeouts.Value;
    }
    else
    {
      GameTimeoutState.TimeoutCalledP2.Value = true;
      --GameTimeoutState.CompTimeouts.Value;
    }
    FatigueManager.ResetAllFatigueVales();
    if (global::Game.UserCallsPlays && global::Game.IsPlayerOneOnOffense)
    {
      MatchManager.instance.playersManager.SetAfterPlayActionsForAllPlayers();
      System.Action userTimeOutCalled = this.OnUserTimeOutCalled;
      if (userTimeOutCalled != null)
        userTimeOutCalled();
    }
    MatchManager.instance.playManager.playSelectedDef = false;
    MatchManager.instance.playManager.playSelectedOff = false;
    MatchManager.instance.DisallowSnap();
    this.StartCoroutine(this.WaitForTimeoutTime());
  }

  private IEnumerator WaitForTimeoutTime()
  {
    yield return (object) new WaitForSeconds(5f);
    MatchManager.instance.playManager.CallNewPlayAfterEndPlay();
  }

  public bool ShouldAICallTimeout(Team team, bool ignoreClockRunning = false)
  {
    if (team != Team.Computer && global::Game.CurrentPlayHasUserQBOnField)
      return false;
    int score1 = SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance.userTeamStats.Score;
    int score2 = SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance.compTeamStats.Score;
    int quarter = MatchManager.instance.timeManager.GetQuarter();
    int num1 = team == Team.Computer ? score2 : score1;
    int num2 = team == Team.Computer ? score1 : score2;
    int num3 = num2 - num1;
    int num4 = num1 - num2;
    int num5 = team == Team.Computer ? GameTimeoutState.CompTimeouts.Value : GameTimeoutState.UserTimeouts.Value;
    int displayMinutes = MatchManager.instance.timeManager.GetDisplayMinutes();
    int secondsRemaining = MatchManager.instance.timeManager.GetTotalSecondsRemaining();
    int lineByFieldLocation = Field.GetYardLineByFieldLocation(MatchManager.ballOn);
    int kickPower = team == Team.Computer ? PersistentData.GetCompTeam().TeamDepthChart.GetStartingKicker().KickPower : PersistentData.GetUserTeam().TeamDepthChart.GetStartingKicker().KickPower;
    int num6 = Field.FurtherDownfield(MatchManager.ballOn, MatchManager.firstDown) ? 1 : MatchManager.down + 1;
    bool flag1 = MatchManager.instance.timeManager.IsGameClockRunning();
    bool flag2 = team == Team.Computer ? global::Game.IsPlayerOneOnDefense : global::Game.IsPlayerOneOnOffense;
    int fieldGoalRange = Field.GetFieldGoalRange(kickPower);
    bool flag3 = Field.ConvertDistanceToYards(Mathf.Abs(Field.OFFENSIVE_GOAL_LINE - MatchManager.ballOn)) < fieldGoalRange;
    bool isPenaltyOnPlay = SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyOnPlay;
    bool flag4 = num3 > 0;
    bool flag5 = num4 > 0;
    bool flag6 = Field.FurtherDownfield(MatchManager.ballOn, Field.OWN_FORTY_YARD_LINE);
    bool flag7 = Field.FurtherDownfield(MatchManager.ballOn, Field.MIDFIELD);
    if (!flag1 && !ignoreClockRunning || quarter == 1 || quarter == 3 || displayMinutes > 1 || isPenaltyOnPlay || num5 == 0 || secondsRemaining < 1)
      return false;
    int a = 15;
    if (flag2)
    {
      if (quarter == 2)
      {
        if (!flag6)
          return false;
        if (num6 == 4)
        {
          if (!flag3)
            return false;
          if (secondsRemaining < a)
            return true;
        }
        if (displayMinutes > 0)
          return false;
        int num7 = Mathf.Max(5, Mathf.RoundToInt((float) lineByFieldLocation * 1.6f));
        if (secondsRemaining < num7)
          return true;
      }
      else
      {
        if (flag5)
          return false;
        int num8 = flag7 ? Mathf.Max(5, Mathf.RoundToInt((float) lineByFieldLocation * 1.6f)) : 50 + (50 - lineByFieldLocation);
        if (num3 <= 3)
          num8 = Mathf.Max(a, num8 - 30);
        if (secondsRemaining < num8)
          return true;
      }
    }
    else if (quarter == 2)
    {
      if (num6 >= 3 && !flag7 && secondsRemaining >= 45 || num6 == 4 && !flag3 && secondsRemaining >= 30)
        return true;
    }
    else
      return num4 < 3 && (flag3 && num4 > 0 && secondsRemaining >= 30 || flag4 && secondsRemaining < 130);
    return false;
  }
}
