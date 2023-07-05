// Decompiled with JetBrains decompiler
// Type: SteamAchievementManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Steamworks;
using System.Collections.Generic;
using UnityEngine;

public class SteamAchievementManager : MonoBehaviour
{
  private static SteamAchievementManager Instance;

  private void Awake() => SteamAchievementManager.Instance = this;

  public static void UnlockAchievements(List<int> achievements) => SteamAchievementManager.Instance?.UnlockAchievementsInstanced(achievements);

  public static void UpdateStats(List<int> stats) => SteamAchievementManager.Instance?.UpdateStatsInstanced(stats);

  private void UnlockAchievementsInstanced(List<int> achievements)
  {
    for (int index = 0; index < achievements.Count; ++index)
      SteamUserStats.SetAchievement(((SteamAchievementManager.SteamAchievements) achievements[index]).ToString());
  }

  private void UpdateStatsInstanced(List<int> stats)
  {
    for (int index = 0; index < stats.Count; ++index)
      SteamUserStats.SetStat(((SteamAchievementManager.SteamStats) index).ToString(), stats[index]);
  }

  private enum SteamAchievements
  {
    ACH_3_TOUCHDOWN_DAY = 1,
    ACH_5_TOUCHDOWN_DAY = 2,
    ACH_7_TOUCHDOWN_DAY = 3,
    ACH_500_PASSING_YARDS = 4,
    ACH_NO_PICKS = 5,
    ACH_CHAIN_MOVER = 6,
    ACH_CANT_BE_STOPPED = 7,
    ACH_LIKE_BRETT = 8,
    ACH_PLAYOFF_BOUND = 9,
    ACH_UPPER_ECHELON = 10, // 0x0000000A
    ACH_NOT_TODAY = 11, // 0x0000000B
    ACH_30_TD_SEASON = 12, // 0x0000000C
    ACH_40_TD_SEASON = 13, // 0x0000000D
    ACH_THE_MAGICIAN = 14, // 0x0000000E
    ACH_THROWINGS_A_BREES = 15, // 0x0000000F
    ACH_DIVISION_CHAMP = 16, // 0x00000010
    ACH_CONFERENCE_CHAMP = 17, // 0x00000011
    ACH_SUPER_BOWL = 18, // 0x00000012
    ACH_MASTER_TACTICIAN = 19, // 0x00000013
    ACH_IN_CONTROL = 20, // 0x00000014
    ACH_250_PASSING = 21, // 0x00000015
    ACH_HAWKEYE = 22, // 0x00000016
    ACH_DEADEYE = 23, // 0x00000017
    ACH_300_PASSING = 24, // 0x00000018
    ACH_400_PASSING = 25, // 0x00000019
    ACH_6_INTS = 26, // 0x0000001A
    ACH_4K_CLUB = 27, // 0x0000001B
    ACH_3K_CLUB = 28, // 0x0000001C
    ACH_COMPLETIONIST = 29, // 0x0000001D
  }

  private enum SteamStats
  {
    stat_season_wins,
    stat_season_TD_passes,
    stat_yards_thrown,
    stat_complete_passes,
    stat_season_yards_thrown,
    stat_teams_defeated,
  }
}
