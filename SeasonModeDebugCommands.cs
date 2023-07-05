// Decompiled with JetBrains decompiler
// Type: SeasonModeDebugCommands
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using IngameDebugConsole;
using UDB;
using UnityEngine;

public class SeasonModeDebugCommands : MonoBehaviour
{
  [ConsoleMethod("ShowDebugSeasonModeInfo", "Prints info about the current season mode", new string[] {})]
  public static void ShowSeasonModeInfo() => SeasonModeManager.ShowDebugSeasonModeInfo();

  [ConsoleMethod("DebugSimulateWeek", "Simulate the current week for the entire league", new string[] {})]
  public static void DebugSimulateWeek(bool forceWin = false) => SeasonModeManager.DebugSimulateWeek(forceWin);

  [ConsoleMethod("DebugSimulateWeeks", "Simulate an arbitrary number of weeks for the league", new string[] {})]
  public static void DebugSimulateWeeks(int numberOfWeeks = 1, bool forceWin = true) => SeasonModeManager.DebugSimulateWeeks(numberOfWeeks, forceWin);

  [ConsoleMethod("DebugSimulateSeason", "Simulate the rest of the season for the entire league", new string[] {})]
  public static void DebugSimulateSeason(bool forceWin = false) => SeasonModeManager.DebugSimulateSeason(forceWin);

  [ConsoleMethod("DebugLionsLikeSeason", "Simulate the worst season ever.", new string[] {})]
  public static void DebugLionsLikeSeason() => SeasonModeManager.DebugSimulateSeason(SeasonModeManager.ForcedSimResult.UserLoss);

  [ConsoleMethod("DebugPlayWeek", "Play the current week for user team", new string[] {})]
  public static void DebugPlayWeek() => SeasonModeManager.self.PlayWeek_Normal();

  [ConsoleMethod("DebugPrintCurrentRound", "Print the current round of the season", new string[] {})]
  public static void DebugPrintCurrentRound() => Debug.Log((object) ("Current Game Week Round = " + SeasonModeManager.self.GetGameRound(PersistentData.seasonWeek).ToString()));

  [ConsoleMethod("DebugPrintWeekSchedule", "Print schedule for current week", new string[] {})]
  public static void DebugPrintCurrentWeekSchedule() => SeasonModeManager.PrintWeekSchedule(PersistentData.seasonWeek);

  [ConsoleMethod("DebugPrintLeagueSchedule", "Print schedule for all weeks", new string[] {})]
  public static void DebugPrintLeagueSchedule()
  {
    for (int week = 1; week < SeasonModeManager.self.seasonModeData.NumberOfWeeksInSeason + 1; ++week)
      SeasonModeManager.PrintWeekSchedule(week);
  }

  [ConsoleMethod("DebugSkipQuarter", "Set the quarter time to 1 second", new string[] {})]
  public static void DebugSkipQuarter() => MatchManager.instance.timeManager.DebugEndQuarter();

  [ConsoleMethod("DebugPrintTeamSeasonStats", "Print the season stats for the team given team index", new string[] {})]
  public static void DebugPrintTeamSeasonStats(int index) => SeasonModeManager.DebugPrintTeamStats(index);

  [ConsoleMethod("DebugPrintMatchTeamStats", "Print the stats for the current match", new string[] {})]
  public static void DebugPrintMatchTeamStats()
  {
    if (!(bool) (Object) SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance)
      return;
    SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance.PrintTeamStats();
  }
}
