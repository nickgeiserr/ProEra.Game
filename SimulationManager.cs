// Decompiled with JetBrains decompiler
// Type: SimulationManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class SimulationManager
{
  public static Award offensivePlayerOfTheWeek;
  public static Award defensivePlayerOfTheWeek;
  private static Award offensivePlayerOfTheYear;
  private static Award defensivePlayerOfTheYear;
  private static Award rookieOfTheYear;
  private static Award quarterbackOfTheYear;
  private static Award runningBackOfTheYear;
  private static Award receiverOfTheYear;
  private static Award defensiveLinemanOfTheYear;
  private static Award linebackerOfTheYear;
  private static Award defensiveBackOfTheYear;
  private static SeasonModeManager seasonMode;
  private static SGD_SeasonModeData seasonModeData;
  private static GameResults gameResults;
  private static TeamData homeTeamData;
  private static TeamData awayTeamData;

  public static Award mvp { get; private set; }

  public static void Init()
  {
    SimulationManager.seasonMode = SeasonModeManager.self;
    SimulationManager.seasonModeData = SimulationManager.seasonMode.seasonModeData;
  }

  public static void ResetWeeklyAwardData()
  {
    SimulationManager.offensivePlayerOfTheWeek = Award.NewAward(AwardType.Offensive_Player_Of_The_Week);
    SimulationManager.defensivePlayerOfTheWeek = Award.NewAward(AwardType.Defensive_Player_Of_The_Week);
  }

  public static void SetPlayersOfWeekFromUserGame()
  {
    SimulationManager.offensivePlayerOfTheWeek = PersistentData.offPlayerOfTheGame;
    SimulationManager.defensivePlayerOfTheWeek = PersistentData.defPlayerOfTheGame;
  }

  public static void SetYearAwards()
  {
    SimulationManager.mvp = Award.NewAward(AwardType.League_MVP);
    SimulationManager.offensivePlayerOfTheYear = Award.NewAward(AwardType.Offensive_Player_Of_The_Year);
    SimulationManager.defensivePlayerOfTheYear = Award.NewAward(AwardType.Defensive_Player_Of_The_Year);
    SimulationManager.rookieOfTheYear = Award.NewAward(AwardType.Rookie_Of_The_Year);
    SimulationManager.quarterbackOfTheYear = Award.NewAward(AwardType.Quarterback_Of_The_Year);
    SimulationManager.runningBackOfTheYear = Award.NewAward(AwardType.Running_Back_Of_The_Year);
    SimulationManager.receiverOfTheYear = Award.NewAward(AwardType.Receiver_Of_The_Year);
    SimulationManager.defensiveLinemanOfTheYear = Award.NewAward(AwardType.Defensive_Lineman_Of_The_Year);
    SimulationManager.linebackerOfTheYear = Award.NewAward(AwardType.Linebacker_Of_The_Year);
    SimulationManager.defensiveBackOfTheYear = Award.NewAward(AwardType.Defensive_Back_Of_The_Year);
    for (int index = 0; index < SimulationManager.seasonModeData.NumberOfTeamsInLeague; ++index)
    {
      TeamData teamData = SimulationManager.seasonMode.GetTeamData(SimulationManager.seasonModeData.TeamIndexMasterList[index]);
      int overallMvp = PlayerStats.GetOverallMVP(teamData.MainRoster, StatDuration.CurrentSeason);
      float totalStatScore = PlayerStats.GetTotalStatScore(teamData.GetPlayer(overallMvp).CurrentSeasonStats);
      int offensiveMvp = PlayerStats.GetOffensiveMVP(teamData.MainRoster, StatDuration.CurrentSeason, overallMvp);
      float offensiveStatScore1 = PlayerStats.GetOffensiveStatScore(teamData.GetPlayer(offensiveMvp).CurrentSeasonStats);
      int defensiveMvp = PlayerStats.GetDefensiveMVP(teamData.MainRoster, StatDuration.CurrentSeason, overallMvp);
      float defensiveStatScore1 = PlayerStats.GetDefensiveStatScore(teamData.GetPlayer(defensiveMvp).CurrentSeasonStats);
      int rookieMvp = PlayerStats.GetRookieMVP(teamData.MainRoster, StatDuration.CurrentSeason);
      float _statScore = PlayerStats.GetTotalStatScore(teamData.GetPlayer(rookieMvp).CurrentSeasonStats);
      if (teamData.GetPlayer(rookieMvp).YearsPro > 0)
        _statScore = 0.0f;
      int mvpByPosition1 = PlayerStats.GetMVPByPosition(teamData.MainRoster, Position.QB, StatDuration.CurrentSeason);
      float offensiveStatScore2 = PlayerStats.GetOffensiveStatScore(teamData.GetPlayer(mvpByPosition1).CurrentSeasonStats);
      int mvpByPosition2 = PlayerStats.GetMVPByPosition(teamData.MainRoster, Position.RB, StatDuration.CurrentSeason);
      float offensiveStatScore3 = PlayerStats.GetOffensiveStatScore(teamData.GetPlayer(mvpByPosition2).CurrentSeasonStats);
      int mvpByPosition3 = PlayerStats.GetMVPByPosition(teamData.MainRoster, Position.WR, StatDuration.CurrentSeason);
      float offensiveStatScore4 = PlayerStats.GetOffensiveStatScore(teamData.GetPlayer(mvpByPosition3).CurrentSeasonStats);
      int mvpByPosition4 = PlayerStats.GetMVPByPosition(teamData.MainRoster, Position.DL, StatDuration.CurrentSeason);
      float defensiveStatScore2 = PlayerStats.GetDefensiveStatScore(teamData.GetPlayer(mvpByPosition4).CurrentSeasonStats);
      int mvpByPosition5 = PlayerStats.GetMVPByPosition(teamData.MainRoster, Position.LB, StatDuration.CurrentSeason);
      float defensiveStatScore3 = PlayerStats.GetDefensiveStatScore(teamData.GetPlayer(mvpByPosition5).CurrentSeasonStats);
      int mvpByPosition6 = PlayerStats.GetMVPByPosition(teamData.MainRoster, Position.DB, StatDuration.CurrentSeason);
      float defensiveStatScore4 = PlayerStats.GetDefensiveStatScore(teamData.GetPlayer(mvpByPosition6).CurrentSeasonStats);
      if ((double) totalStatScore > (double) SimulationManager.mvp.statScore)
      {
        if (SimulationManager.mvp.IsOffensivePlayer())
          SimulationManager.offensivePlayerOfTheYear.CopyAwardData(SimulationManager.mvp, AwardType.Offensive_Player_Of_The_Year);
        else
          SimulationManager.defensivePlayerOfTheYear.CopyAwardData(SimulationManager.mvp, AwardType.Defensive_Player_Of_The_Year);
        SimulationManager.mvp.SetAwardData(teamData, teamData.GetPlayer(overallMvp).CurrentSeasonStats, overallMvp, totalStatScore, AwardType.League_MVP);
      }
      else if ((double) offensiveStatScore1 > (double) SimulationManager.offensivePlayerOfTheYear.statScore)
        SimulationManager.offensivePlayerOfTheYear.SetAwardData(teamData, teamData.GetPlayer(offensiveMvp).CurrentSeasonStats, offensiveMvp, offensiveStatScore1, AwardType.Offensive_Player_Of_The_Year);
      else if ((double) defensiveStatScore1 > (double) SimulationManager.defensivePlayerOfTheYear.statScore)
        SimulationManager.defensivePlayerOfTheYear.SetAwardData(teamData, teamData.GetPlayer(defensiveMvp).CurrentSeasonStats, defensiveMvp, defensiveStatScore1, AwardType.Defensive_Player_Of_The_Year);
      if ((double) _statScore > (double) SimulationManager.rookieOfTheYear.statScore)
        SimulationManager.rookieOfTheYear.SetAwardData(teamData, teamData.GetPlayer(rookieMvp).CurrentSeasonStats, rookieMvp, _statScore, AwardType.Rookie_Of_The_Year);
      if ((double) offensiveStatScore2 > (double) SimulationManager.quarterbackOfTheYear.statScore)
        SimulationManager.quarterbackOfTheYear.SetAwardData(teamData, teamData.GetPlayer(mvpByPosition1).CurrentSeasonStats, mvpByPosition1, offensiveStatScore2, AwardType.Quarterback_Of_The_Year);
      if ((double) offensiveStatScore3 > (double) SimulationManager.runningBackOfTheYear.statScore)
        SimulationManager.runningBackOfTheYear.SetAwardData(teamData, teamData.GetPlayer(mvpByPosition2).CurrentSeasonStats, mvpByPosition2, offensiveStatScore3, AwardType.Running_Back_Of_The_Year);
      if ((double) offensiveStatScore4 > (double) SimulationManager.receiverOfTheYear.statScore)
        SimulationManager.receiverOfTheYear.SetAwardData(teamData, teamData.GetPlayer(mvpByPosition3).CurrentSeasonStats, mvpByPosition3, offensiveStatScore4, AwardType.Receiver_Of_The_Year);
      if ((double) defensiveStatScore2 > (double) SimulationManager.defensiveLinemanOfTheYear.statScore)
        SimulationManager.defensiveLinemanOfTheYear.SetAwardData(teamData, teamData.GetPlayer(mvpByPosition4).CurrentSeasonStats, mvpByPosition4, defensiveStatScore2, AwardType.Defensive_Lineman_Of_The_Year);
      if ((double) defensiveStatScore3 > (double) SimulationManager.linebackerOfTheYear.statScore)
        SimulationManager.linebackerOfTheYear.SetAwardData(teamData, teamData.GetPlayer(mvpByPosition5).CurrentSeasonStats, mvpByPosition5, defensiveStatScore3, AwardType.Linebacker_Of_The_Year);
      if ((double) defensiveStatScore4 > (double) SimulationManager.defensiveBackOfTheYear.statScore)
        SimulationManager.defensiveBackOfTheYear.SetAwardData(teamData, teamData.GetPlayer(mvpByPosition6).CurrentSeasonStats, mvpByPosition6, defensiveStatScore4, AwardType.Defensive_Back_Of_The_Year);
    }
    SimulationManager.seasonModeData.mvp = SimulationManager.mvp;
    SimulationManager.seasonModeData.offPlayerOfTheYear = SimulationManager.offensivePlayerOfTheYear;
    SimulationManager.seasonModeData.defPlayerOfTheYear = SimulationManager.defensivePlayerOfTheYear;
    SimulationManager.seasonModeData.rookieOfTheYear = SimulationManager.rookieOfTheYear;
    SimulationManager.seasonModeData.quarterbackOfTheYear = SimulationManager.quarterbackOfTheYear;
    SimulationManager.seasonModeData.runningBackOfTheYear = SimulationManager.runningBackOfTheYear;
    SimulationManager.seasonModeData.receiverOfTheYear = SimulationManager.receiverOfTheYear;
    SimulationManager.seasonModeData.defensiveLinemanOfTheYear = SimulationManager.defensiveLinemanOfTheYear;
    SimulationManager.seasonModeData.linebackerOfTheYear = SimulationManager.linebackerOfTheYear;
    SimulationManager.seasonModeData.defensiveBackOfTheYear = SimulationManager.defensiveBackOfTheYear;
  }

  public static GameResults SimulateGame(TeamData homeTeamData, TeamData awayTeamData)
  {
    float num1;
    switch (PersistentData.quarterLength)
    {
      case 0:
        num1 = 0.6f;
        break;
      case 1:
        num1 = 0.9f;
        break;
      case 2:
        num1 = 1.2f;
        break;
      default:
        num1 = 1.4f;
        break;
    }
    GameResults gameResults = new GameResults();
    GameSummary gameSummary1 = new GameSummary();
    GameSummary gameSummary2 = new GameSummary();
    homeTeamData.CreateNewTeamGameStats();
    awayTeamData.CreateNewTeamGameStats();
    TeamGameStats currentGameStats1 = homeTeamData.CurrentGameStats;
    TeamGameStats currentGameStats2 = awayTeamData.CurrentGameStats;
    Debug.Log((object) ("TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER: " + TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER.ToString()));
    for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
    {
      homeTeamData.GetPlayer(playerIndex).CreateNewPlayerStatsForGame();
      awayTeamData.GetPlayer(playerIndex).CreateNewPlayerStatsForGame();
    }
    int[] teamRating1 = SimulationManager.GetTeamRating(homeTeamData);
    int[] teamRating2 = SimulationManager.GetTeamRating(awayTeamData);
    int num2 = Mathf.RoundToInt((float) (teamRating1[0] - 72) / 5f);
    int num3 = Mathf.RoundToInt((float) (teamRating1[1] - 72) / 5f);
    int num4 = Mathf.RoundToInt((float) (teamRating1[2] - 72) / 5f);
    int num5 = Mathf.RoundToInt((float) (teamRating1[3] - 72) / 5f);
    int num6 = Mathf.RoundToInt((float) (teamRating1[4] - 72) / 5f);
    int num7 = Mathf.RoundToInt((float) (teamRating2[0] - 72) / 5f);
    int num8 = Mathf.RoundToInt((float) (teamRating2[1] - 72) / 5f);
    int num9 = Mathf.RoundToInt((float) (teamRating2[2] - 72) / 5f);
    int num10 = Mathf.RoundToInt((float) (teamRating2[3] - 72) / 5f);
    int num11 = Mathf.RoundToInt((float) (teamRating2[4] - 72) / 5f);
    currentGameStats1.PassYards = Mathf.RoundToInt(num1 * (float) (Random.Range(200, 325) + num2 * Random.Range(30, 50) - num9 * Random.Range(30, 40)));
    currentGameStats2.PassYards = Mathf.RoundToInt(num1 * (float) (Random.Range(200, 325) + num7 * Random.Range(30, 50) - num4 * Random.Range(30, 40)));
    currentGameStats1.RushYards = Mathf.RoundToInt(num1 * (float) (Random.Range(80, 140) + num3 * Random.Range(20, 40) - num10 * Random.Range(20, 30)));
    currentGameStats2.RushYards = Mathf.RoundToInt(num1 * (float) (Random.Range(80, 140) + num8 * Random.Range(20, 40) - num5 * Random.Range(20, 30)));
    currentGameStats1.Turnovers = Mathf.RoundToInt(num1 * (float) Random.Range(0, Mathf.Clamp(4 + num10 + num9 - num3 - num2, 3, 7)));
    currentGameStats2.Turnovers = Mathf.RoundToInt(num1 * (float) Random.Range(0, Mathf.Clamp(4 + num5 + num4 - num8 - num7, 3, 7)));
    currentGameStats1.Sacks = Mathf.RoundToInt(num1 * (float) Random.Range(0, (num9 + num10 + 7) / 2));
    currentGameStats2.Sacks = Mathf.RoundToInt(num1 * (float) Random.Range(0, (num9 + num10 + 7) / 2));
    int num12 = currentGameStats2.PassYards + currentGameStats2.RushYards + currentGameStats1.Turnovers * Random.Range(50, 100) + currentGameStats2.Sacks * 10 + num11 * 15;
    int num13 = currentGameStats1.PassYards + currentGameStats1.RushYards + currentGameStats2.Turnovers * Random.Range(50, 100) + currentGameStats1.Sacks * 10 + num6 * 15;
    int num14 = Random.Range(0, 3) + Mathf.Clamp(num13 - num12, 0, 21) / 7;
    int num15 = Random.Range(0, 3) + Mathf.Clamp(num12 - num13, 0, 21) / 7;
    int num16 = Random.Range(0, 4);
    int num17 = Random.Range(0, 4);
    currentGameStats1.Score = num14 * 6 + num16 * 3;
    currentGameStats2.Score = num15 * 6 + num17 * 3;
    int num18 = Random.Range(0, 2);
    int num19 = Random.Range(0, 3);
    if (currentGameStats1.Score > currentGameStats2.Score)
    {
      currentGameStats2.Score += num18 * 6 + num19 * 3;
      num17 += num19;
      num15 += num18;
    }
    else
    {
      currentGameStats1.Score += num18 * 6 + num19 * 3;
      num16 += num19;
      num14 += num18;
    }
    int maxExclusive1 = num14;
    int num20 = 0;
    int num21 = 0;
    int num22 = 0;
    int num23 = 0;
    int num24 = 0;
    int num25 = 0;
    if (maxExclusive1 > 0)
    {
      num20 = Random.Range(0, 1000) < 15 ? 1 : 0;
      maxExclusive1 -= num20;
    }
    if (maxExclusive1 > 0)
    {
      num21 = Random.Range(0, 1000) < 15 ? 1 : 0;
      maxExclusive1 -= num21;
    }
    if (maxExclusive1 > 0)
    {
      num22 = Random.Range(0, 1000) < 62 ? 1 : 0;
      maxExclusive1 -= num22;
    }
    int num26 = Random.Range(0, maxExclusive1);
    int num27 = maxExclusive1 - num26;
    int maxExclusive2 = num15;
    if (maxExclusive2 > 0)
    {
      num23 = Random.Range(0, 1000) < 15 ? 1 : 0;
      maxExclusive2 -= num23;
    }
    if (maxExclusive2 > 0)
    {
      num24 = Random.Range(0, 1000) < 15 ? 1 : 0;
      maxExclusive2 -= num24;
    }
    if (maxExclusive2 > 0)
    {
      num25 = Random.Range(0, 1000) < 62 ? 1 : 0;
      maxExclusive2 -= num25;
    }
    int num28 = Random.Range(0, maxExclusive2);
    int num29 = maxExclusive2 - num28;
    if (num25 > currentGameStats1.Turnovers)
      currentGameStats1.Turnovers = num25;
    if (num22 > currentGameStats2.Turnovers)
      currentGameStats2.Turnovers = num22;
    int num30 = Mathf.RoundToInt((float) currentGameStats1.Turnovers * 0.6f);
    int num31 = currentGameStats1.Turnovers - num30;
    int num32 = Mathf.RoundToInt((float) currentGameStats2.Turnovers * 0.6f);
    int num33 = currentGameStats2.Turnovers - num32;
    int startingRbIndex1 = homeTeamData.TeamDepthChart.GetStartingRBIndex();
    int overall1 = homeTeamData.GetOverall(startingRbIndex1);
    List<int> intList1 = SimulationManager.BuildRunnerList(homeTeamData);
    int startingRbIndex2 = awayTeamData.TeamDepthChart.GetStartingRBIndex();
    int overall2 = awayTeamData.GetOverall(startingRbIndex2);
    List<int> intList2 = SimulationManager.BuildRunnerList(awayTeamData);
    int num34 = Random.Range(15, 25) + (overall1 - 70) / 3 + currentGameStats1.RushYards / 30;
    int num35 = Random.Range(15, 25) + (overall2 - 70) / 3 + currentGameStats2.RushYards / 30;
    currentGameStats1.TotalRunPlays = num34;
    currentGameStats2.TotalRunPlays = num35;
    currentGameStats1.RushYards = 0;
    currentGameStats2.RushYards = 0;
    for (int index = 0; index < num34; ++index)
    {
      int playerIndex = intList1[Random.Range(0, intList1.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.RushAttempts;
      if (num26 > 0)
      {
        ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.RushTDs;
        --num26;
      }
      int num36 = Random.Range(0, 40) == 0 ? Random.Range(10, 99) : Random.Range(-2, 5 + num3);
      homeTeamData.GetPlayer(playerIndex).CurrentGameStats.RushYards += num36;
      if (homeTeamData.GetPlayer(playerIndex).CurrentGameStats.LongestRush < num36)
        homeTeamData.GetPlayer(playerIndex).CurrentGameStats.LongestRush = num36;
      currentGameStats1.RushYards += num36;
    }
    for (int index = 0; index < num35; ++index)
    {
      int playerIndex = intList2[Random.Range(0, intList2.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.RushAttempts;
      if (num28 > 0)
      {
        ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.RushTDs;
        --num28;
      }
      int num37 = Random.Range(0, 40) == 0 ? Random.Range(10, 99) : Random.Range(-2, 5 + num8);
      awayTeamData.GetPlayer(playerIndex).CurrentGameStats.RushYards += num37;
      if (awayTeamData.GetPlayer(playerIndex).CurrentGameStats.LongestRush < num37)
        awayTeamData.GetPlayer(playerIndex).CurrentGameStats.LongestRush = num37;
      currentGameStats2.RushYards += num37;
    }
    for (int index = 0; index < num26; ++index)
    {
      int playerIndex = intList1[Random.Range(0, intList1.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.RushTDs;
    }
    for (int index = 0; index < num28; ++index)
    {
      int playerIndex = intList2[Random.Range(0, intList2.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.RushTDs;
    }
    int startingQbIndex1 = homeTeamData.TeamDepthChart.GetStartingQBIndex();
    int overall3 = homeTeamData.GetOverall(startingQbIndex1);
    int startingQbIndex2 = awayTeamData.TeamDepthChart.GetStartingQBIndex();
    int overall4 = awayTeamData.GetOverall(startingQbIndex2);
    int num38 = Random.Range(25, 35) + (overall3 - 70) / 3 + currentGameStats1.PassYards / 50;
    int num39 = Random.Range(25, 35) + (overall4 - 70) / 3 + currentGameStats2.PassYards / 50;
    homeTeamData.GetPlayer(startingQbIndex1).CurrentGameStats.QBAttempts += num38;
    awayTeamData.GetPlayer(startingQbIndex2).CurrentGameStats.QBAttempts += num39;
    int num40 = Mathf.RoundToInt((Random.Range(0.45f, 0.6f) + 0.04f * (float) num2) * (float) num38);
    int num41 = Mathf.RoundToInt((Random.Range(0.45f, 0.6f) + 0.04f * (float) num7) * (float) num39);
    homeTeamData.GetPlayer(startingQbIndex1).CurrentGameStats.QBCompletions += num40;
    awayTeamData.GetPlayer(startingQbIndex2).CurrentGameStats.QBCompletions += num41;
    homeTeamData.GetPlayer(startingQbIndex1).CurrentGameStats.QBPassTDs += num27;
    awayTeamData.GetPlayer(startingQbIndex2).CurrentGameStats.QBPassTDs += num29;
    homeTeamData.GetPlayer(startingQbIndex1).CurrentGameStats.QBInts += num30;
    awayTeamData.GetPlayer(startingQbIndex2).CurrentGameStats.QBInts += num32;
    currentGameStats1.TotalPassPlays = num38 + currentGameStats2.Sacks;
    currentGameStats2.TotalPassPlays = num39 + currentGameStats1.Sacks;
    List<int> intList3 = SimulationManager.BuildReceiverList(homeTeamData);
    List<int> intList4 = SimulationManager.BuildReceiverList(awayTeamData);
    currentGameStats1.PassYards = 0;
    currentGameStats2.PassYards = 0;
    for (int index = 0; index < num40; ++index)
    {
      int playerIndex = intList3[Random.Range(0, intList3.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.Receptions;
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.Targets;
      if (num27 > 0)
      {
        ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.ReceivingTDs;
        --num27;
      }
      int num42 = Random.Range(0, 40) == 0 ? Random.Range(10, 99) : Random.Range(3, 10 + num2);
      homeTeamData.GetPlayer(playerIndex).CurrentGameStats.ReceivingYards += num42;
      if (homeTeamData.GetPlayer(playerIndex).PlayerPosition == Position.RB)
        homeTeamData.GetPlayer(playerIndex).CurrentGameStats.YardsAfterCatch += Mathf.RoundToInt((float) num42 * Random.Range(0.4f, 0.6f));
      else
        homeTeamData.GetPlayer(playerIndex).CurrentGameStats.YardsAfterCatch += Mathf.RoundToInt((float) num42 * Random.Range(0.2f, 0.5f));
      if (homeTeamData.GetPlayer(playerIndex).CurrentGameStats.LongestReception < num42)
      {
        homeTeamData.GetPlayer(playerIndex).CurrentGameStats.LongestReception = num42;
        if (homeTeamData.GetPlayer(startingQbIndex1).CurrentGameStats.QBLongestPass < num42)
          homeTeamData.GetPlayer(startingQbIndex1).CurrentGameStats.QBLongestPass = num42;
      }
      currentGameStats1.PassYards += num42;
    }
    for (int index = 0; index < num41; ++index)
    {
      int playerIndex = intList4[Random.Range(0, intList4.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.Receptions;
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.Targets;
      if (num29 > 0)
      {
        ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.ReceivingTDs;
        --num29;
      }
      int num43 = Random.Range(0, 40) == 0 ? Random.Range(10, 99) : Random.Range(3, 10 + num7);
      awayTeamData.GetPlayer(playerIndex).CurrentGameStats.ReceivingYards += num43;
      if (awayTeamData.GetPlayer(playerIndex).PlayerPosition == Position.RB)
        awayTeamData.GetPlayer(playerIndex).CurrentGameStats.YardsAfterCatch += Mathf.RoundToInt((float) num43 * Random.Range(0.4f, 0.6f));
      else
        awayTeamData.GetPlayer(playerIndex).CurrentGameStats.YardsAfterCatch += Mathf.RoundToInt((float) num43 * Random.Range(0.2f, 0.5f));
      if (awayTeamData.GetPlayer(playerIndex).CurrentGameStats.LongestReception < num43)
      {
        awayTeamData.GetPlayer(playerIndex).CurrentGameStats.LongestReception = num43;
        if (awayTeamData.GetPlayer(startingQbIndex2).CurrentGameStats.QBLongestPass < num43)
          awayTeamData.GetPlayer(startingQbIndex2).CurrentGameStats.QBLongestPass = num43;
      }
      currentGameStats2.PassYards += num43;
    }
    int num44 = num38 - num40;
    for (int index = 0; index < num44; ++index)
    {
      int playerIndex = intList3[Random.Range(0, intList3.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.Targets;
    }
    int num45 = num39 - num41;
    for (int index = 0; index < num45; ++index)
    {
      int playerIndex = intList4[Random.Range(0, intList4.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.Targets;
    }
    for (int index = 0; index < num31; ++index)
    {
      int playerIndex = Random.Range(0, 2) == 0 ? intList1[Random.Range(0, intList1.Count)] : intList3[Random.Range(0, intList3.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.Fumbles;
    }
    for (int index = 0; index < num33; ++index)
    {
      int playerIndex = Random.Range(0, 2) == 0 ? intList2[Random.Range(0, intList2.Count)] : intList4[Random.Range(0, intList4.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.Fumbles;
    }
    int num46 = Random.Range(0, 7);
    for (int index = 0; index < num46; ++index)
    {
      int playerIndex1 = intList3[Random.Range(0, intList3.Count)];
      ++homeTeamData.GetPlayer(playerIndex1).CurrentGameStats.Drops;
      int playerIndex2 = intList4[Random.Range(0, intList4.Count)];
      ++awayTeamData.GetPlayer(playerIndex2).CurrentGameStats.Drops;
    }
    homeTeamData.GetPlayer(startingQbIndex1).CurrentGameStats.QBPassYards += currentGameStats1.PassYards;
    awayTeamData.GetPlayer(startingQbIndex2).CurrentGameStats.QBPassYards += currentGameStats2.PassYards;
    List<int> intList5 = SimulationManager.BuildRushDefList(homeTeamData);
    List<int> intList6 = SimulationManager.BuildRushDefList(awayTeamData);
    List<int> intList7 = SimulationManager.BuildPassDefList(homeTeamData);
    List<int> intList8 = SimulationManager.BuildPassDefList(awayTeamData);
    int num47 = Random.Range(45, 55);
    int num48 = Random.Range(45, 55);
    int num49 = Mathf.RoundToInt((float) num47 * 0.07f);
    int num50 = Mathf.RoundToInt((float) num48 * 0.07f);
    int num51 = Random.Range(1, 4);
    int num52 = Random.Range(1, 4);
    bool flag1 = Random.Range(0, 100) < 50;
    bool flag2 = Random.Range(0, 100) < 50;
    currentGameStats1.Interceptions = num32;
    currentGameStats2.Interceptions = num30;
    currentGameStats1.TacklesForLoss = num49;
    currentGameStats2.TacklesForLoss = num50;
    currentGameStats1.ForcedFumbles = num33;
    currentGameStats2.ForcedFumbles = num31;
    currentGameStats1.FumbleRecoveries = num33;
    currentGameStats2.FumbleRecoveries = num31;
    for (int index = 0; index < num47; ++index)
    {
      int playerIndex = index % 2 != 0 ? intList7[Random.Range(0, intList7.Count)] : intList5[Random.Range(0, intList5.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.Tackles;
    }
    for (int index = 0; index < num48; ++index)
    {
      int playerIndex = index % 2 != 0 ? intList8[Random.Range(0, intList8.Count)] : intList6[Random.Range(0, intList6.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.Tackles;
    }
    for (int index = 0; index < num49; ++index)
    {
      int playerIndex = Random.Range(0, 100) >= 75 ? intList7[Random.Range(0, intList7.Count)] : intList5[Random.Range(0, intList5.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.TacklesForLoss;
    }
    for (int index = 0; index < num50; ++index)
    {
      int playerIndex = Random.Range(0, 100) >= 75 ? intList8[Random.Range(0, intList8.Count)] : intList6[Random.Range(0, intList6.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.TacklesForLoss;
    }
    for (int index = 0; index < currentGameStats1.Sacks; ++index)
    {
      int playerIndex = intList5[Random.Range(0, intList5.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.Sacks;
    }
    for (int index = 0; index < currentGameStats2.Sacks; ++index)
    {
      int playerIndex = intList6[Random.Range(0, intList6.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.Sacks;
    }
    for (int index = 0; index < num32; ++index)
    {
      int playerIndex = intList7[Random.Range(0, intList7.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.Interceptions;
      if (num22 > 0 & flag1)
      {
        --num22;
        ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.DefensiveTDs;
      }
    }
    for (int index = 0; index < num30; ++index)
    {
      int playerIndex = intList8[Random.Range(0, intList8.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.Interceptions;
      if (num25 > 0 & flag2)
      {
        --num25;
        ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.DefensiveTDs;
      }
    }
    for (int index = 0; index < num51; ++index)
    {
      int playerIndex = intList7[Random.Range(0, intList7.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.KnockDowns;
    }
    for (int index = 0; index < num52; ++index)
    {
      int playerIndex = intList8[Random.Range(0, intList8.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.KnockDowns;
    }
    for (int index = 0; index < num33; ++index)
    {
      if (Random.Range(0, 100) < 90)
      {
        int playerIndex = intList5[Random.Range(0, intList5.Count)];
        ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.ForcedFumbles;
      }
    }
    for (int index = 0; index < num31; ++index)
    {
      if (Random.Range(0, 100) < 90)
      {
        int playerIndex = intList6[Random.Range(0, intList6.Count)];
        ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.ForcedFumbles;
      }
    }
    for (int index = 0; index < num33; ++index)
    {
      int playerIndex = intList5[Random.Range(0, intList5.Count)];
      ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.FumbleRecoveries;
      if (num22 > 0 && !flag1)
      {
        --num22;
        ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.DefensiveTDs;
      }
      else if (Random.Range(0, 60) < 100)
        ++homeTeamData.GetPlayer(playerIndex).CurrentGameStats.FumbleRecoveries;
    }
    for (int index = 0; index < num31; ++index)
    {
      int playerIndex = intList6[Random.Range(0, intList6.Count)];
      ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.FumbleRecoveries;
      if (num25 > 0 && !flag2)
      {
        --num25;
        ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.DefensiveTDs;
      }
      else if (Random.Range(0, 60) < 100)
        ++awayTeamData.GetPlayer(playerIndex).CurrentGameStats.FumbleRecoveries;
    }
    int startingKickerIndex1 = homeTeamData.TeamDepthChart.GetStartingKickerIndex();
    int overall5 = homeTeamData.GetOverall(startingKickerIndex1);
    homeTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.FGMade = num16;
    homeTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.FGAttempted = num16;
    while ((double) Random.Range(0.0f, 100f) > (double) overall5)
      ++homeTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.FGAttempted;
    int num53 = num14;
    while ((double) Random.Range(0.0f, 90f) > (double) overall5)
    {
      if (num53 > 0)
        --num53;
    }
    currentGameStats1.Score += num53;
    homeTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.XPAttempted = num14;
    homeTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.XPMade = num53;
    int startingKickerIndex2 = awayTeamData.TeamDepthChart.GetStartingKickerIndex();
    int overall6 = awayTeamData.GetOverall(startingKickerIndex2);
    awayTeamData.GetPlayer(startingKickerIndex2).CurrentGameStats.FGMade = num17;
    awayTeamData.GetPlayer(startingKickerIndex2).CurrentGameStats.FGAttempted = num17;
    while ((double) Random.Range(0.0f, 100f) > (double) overall6)
      ++awayTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.FGAttempted;
    int num54 = num15;
    while ((double) Random.Range(0.0f, 90f) > (double) overall6)
    {
      if (num54 > 0)
        --num54;
    }
    currentGameStats2.Score += num54;
    awayTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.XPAttempted = num15;
    awayTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.XPMade = num54;
    int num55 = num14 + num16 + 1;
    int num56 = num15 + num17 + 1;
    int num57 = Mathf.RoundToInt((float) num55 * 0.5f);
    int num58 = Mathf.RoundToInt((float) num56 * 0.5f);
    if (num20 > num57)
      num57 = num20;
    for (int index = 0; index < num20; ++index)
    {
      --num57;
      ++homeTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturns;
      ++homeTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturnTDs;
      homeTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturnYards += Random.Range(90, 110);
    }
    Debug.Log((object) ("homeTeamData: " + homeTeamData?.ToString()));
    Debug.Log((object) ("homeTeamData.GetFullDisplayName(): " + homeTeamData.GetFullDisplayName()));
    Debug.Log((object) ("homeTeamData.TeamDepthChart: " + homeTeamData.TeamDepthChart?.ToString()));
    Debug.Log((object) ("homeTeamData.TeamDepthChart.GetStartingKickReturner (): " + homeTeamData.TeamDepthChart.GetStartingKickReturner()?.ToString()));
    Debug.Log((object) ("homeTeamData.TeamDepthChart.GetStartingKickReturner ().FirstInitalAndLastName: " + homeTeamData.TeamDepthChart.GetStartingKickReturner().FirstInitalAndLastName));
    Debug.Log((object) ("homeTeamData.TeamDepthChart.GetStartingKickReturner ().CurrentGameStats: " + homeTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats?.ToString()));
    PlayerStats currentGameStats3 = homeTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats;
    for (int index = 0; index < num57; ++index)
    {
      ++homeTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturns;
      homeTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturnYards += Random.Range(15, 35);
    }
    if (num23 > num58)
      num58 = num23;
    for (int index = 0; index < num23; ++index)
    {
      --num58;
      ++awayTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturns;
      ++awayTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturnTDs;
      awayTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturnYards += Random.Range(90, 110);
    }
    PlayerStats currentGameStats4 = awayTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats;
    for (int index = 0; index < num58; ++index)
    {
      ++awayTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturns;
      awayTeamData.TeamDepthChart.GetStartingKickReturner().CurrentGameStats.KickReturnYards += Random.Range(15, 35);
    }
    int startingPunterIndex1 = homeTeamData.TeamDepthChart.GetStartingPunterIndex();
    int kickPower1 = homeTeamData.GetPlayer(startingPunterIndex1).KickPower;
    int kickAccuracy1 = homeTeamData.GetPlayer(startingPunterIndex1).KickAccuracy;
    int overall7 = homeTeamData.GetOverall(startingPunterIndex1);
    int num59 = Mathf.Clamp(Random.Range(3, 9) - Mathf.RoundToInt((float) num14 * 0.33f), 0, 7);
    float num60 = (float) (1 + (kickPower1 - 85) / 85);
    int num61 = 0;
    for (int index = 0; index < num59; ++index)
      num61 += Mathf.RoundToInt((float) Random.Range(40, 50) * num60);
    int num62 = 0;
    while ((double) Random.Range(0.0f, 1f) * 100.0 > (double) overall7)
      ++num62;
    int num63 = Mathf.RoundToInt(Random.Range(0.0f, 1.66f + (float) (1 + (kickAccuracy1 - 65) / 65)));
    while (num59 - num62 - num63 < 0)
    {
      if (num62 > num63)
        --num62;
      else
        --num63;
    }
    homeTeamData.GetPlayer(startingPunterIndex1).CurrentGameStats.Punts = num59;
    homeTeamData.GetPlayer(startingPunterIndex1).CurrentGameStats.PuntsInside20 = num63;
    homeTeamData.GetPlayer(startingPunterIndex1).CurrentGameStats.PuntTouchbacks = num62;
    homeTeamData.GetPlayer(startingPunterIndex1).CurrentGameStats.PuntYards = num61;
    int startingPunterIndex2 = awayTeamData.TeamDepthChart.GetStartingPunterIndex();
    int kickPower2 = awayTeamData.GetPlayer(startingPunterIndex2).KickPower;
    int kickAccuracy2 = awayTeamData.GetPlayer(startingPunterIndex2).KickAccuracy;
    int overall8 = awayTeamData.GetOverall(startingPunterIndex2);
    int num64 = Mathf.Clamp(Random.Range(3, 9) - Mathf.RoundToInt((float) num15 * 0.33f), 0, 7);
    float num65 = (float) (1 + (kickPower2 - 85) / 85);
    int num66 = 0;
    for (int index = 0; index < num64; ++index)
      num66 += Mathf.RoundToInt((float) Random.Range(40, 50) * num65);
    int num67 = 0;
    while ((double) Random.Range(0.0f, 1f) * 100.0 > (double) overall8)
      ++num67;
    float f = (float) (1 + (kickAccuracy2 - 85) / 85);
    int num68 = Mathf.RoundToInt(Random.Range(0.0f, 3f) * Mathf.Pow(f, 2f));
    while (num64 - num67 - num68 < 0)
    {
      if (num67 > num68)
        --num67;
      else
        --num68;
    }
    awayTeamData.GetPlayer(startingPunterIndex2).CurrentGameStats.Punts = num64;
    awayTeamData.GetPlayer(startingPunterIndex2).CurrentGameStats.PuntsInside20 = num68;
    awayTeamData.GetPlayer(startingPunterIndex2).CurrentGameStats.PuntTouchbacks = num67;
    awayTeamData.GetPlayer(startingPunterIndex2).CurrentGameStats.PuntYards = num66;
    int num69 = Mathf.RoundToInt((float) num64 * 0.5f);
    int num70 = Mathf.RoundToInt((float) num59 * 0.5f);
    if (num21 > num69)
      num69 = num21;
    for (int index = 0; index < num21; ++index)
    {
      --num69;
      ++homeTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturns;
      ++homeTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturnTDs;
      homeTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturnYards += Random.Range(50, 95);
    }
    for (int index = 0; index < num69; ++index)
    {
      ++homeTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturns;
      homeTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturnYards += Random.Range(0, 17);
    }
    if (num24 > num70)
      num70 = num24;
    for (int index = 0; index < num24; ++index)
    {
      --num70;
      ++awayTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturns;
      ++awayTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturnTDs;
      awayTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturnYards += Random.Range(50, 95);
    }
    for (int index = 0; index < num70; ++index)
    {
      ++awayTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturns;
      awayTeamData.TeamDepthChart.GetStartingPuntReturner().CurrentGameStats.PuntReturnYards += Random.Range(0, 17);
    }
    if (currentGameStats1.Score == currentGameStats2.Score)
    {
      currentGameStats1.Score += 3;
      currentGameStats1.ScoreByQuarter[3] += 3;
      ++homeTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.FGMade;
      ++homeTeamData.GetPlayer(startingKickerIndex1).CurrentGameStats.FGAttempted;
    }
    int num71 = 0;
    int num72 = 0;
    for (int index1 = 0; index1 < num14; ++index1)
    {
      int index2 = Random.Range(0, 4);
      currentGameStats1.ScoreByQuarter[index2] += 6;
      if (num71 < num53)
      {
        ++currentGameStats1.ScoreByQuarter[index2];
        ++num71;
      }
    }
    for (int index3 = 0; index3 < num15; ++index3)
    {
      int index4 = Random.Range(0, 4);
      currentGameStats2.ScoreByQuarter[index4] += 6;
      if (num72 < num54)
      {
        ++currentGameStats2.ScoreByQuarter[index4];
        ++num72;
      }
    }
    for (int index = 0; index < num16; ++index)
      currentGameStats1.ScoreByQuarter[Random.Range(0, 4)] += 3;
    for (int index = 0; index < num17; ++index)
      currentGameStats2.ScoreByQuarter[Random.Range(0, 4)] += 3;
    TeamData team = currentGameStats2.Score <= currentGameStats1.Score ? homeTeamData : awayTeamData;
    int offensiveMvp = PlayerStats.GetOffensiveMVP(team.MainRoster, StatDuration.CurrentGame);
    float offensiveStatScore = PlayerStats.GetOffensiveStatScore(team.GetPlayer(offensiveMvp).CurrentGameStats);
    Award award1 = Award.NewAward(AwardType.Offensive_Player_Of_The_Game);
    award1.SetAwardData(team, team.GetPlayer(offensiveMvp).CurrentGameStats, offensiveMvp, offensiveStatScore, AwardType.Offensive_Player_Of_The_Game);
    int defensiveMvp = PlayerStats.GetDefensiveMVP(team.MainRoster, StatDuration.CurrentGame);
    float defensiveStatScore = PlayerStats.GetDefensiveStatScore(team.GetPlayer(defensiveMvp).CurrentGameStats);
    Award award2 = Award.NewAward(AwardType.Defensive_Player_Of_The_Game);
    award2.SetAwardData(team, team.GetPlayer(defensiveMvp).CurrentGameStats, defensiveMvp, defensiveStatScore, AwardType.Defensive_Player_Of_The_Game);
    gameSummary1.TeamIndex = homeTeamData.TeamIndex;
    gameSummary2.TeamIndex = awayTeamData.TeamIndex;
    gameSummary1.TeamGameStats = currentGameStats1;
    gameSummary2.TeamGameStats = currentGameStats2;
    gameSummary1.RecordPlayerStats(homeTeamData);
    gameSummary2.RecordPlayerStats(awayTeamData);
    gameResults.HomeGameSummary = gameSummary1;
    gameResults.AwayGameSummary = gameSummary2;
    gameResults.OffensivePlayerOfTheGame = award1;
    gameResults.DefensivePlayerOfTheGame = award2;
    return gameResults;
  }

  public static bool SimulateFranchiseGame(
    int homeIndex,
    int awayIndex,
    int homeIndexInSchedule,
    int awayIndexInSchedule,
    bool regularSeason,
    int conference,
    int? forcedWinnerIndex = null)
  {
    Debug.Log((object) ("homeIndex: " + homeIndex.ToString()));
    Debug.Log((object) ("awayIndex: " + awayIndex.ToString()));
    Debug.Log((object) ("seasonMode.GetTeamData (homeIndex): " + SimulationManager.seasonMode.GetTeamData(homeIndex).GetName()));
    Debug.Log((object) ("seasonMode.GetTeamData (awayIndex): " + SimulationManager.seasonMode.GetTeamData(awayIndex).GetName()));
    SimulationManager.homeTeamData = SimulationManager.seasonMode.GetTeamData(homeIndex);
    SimulationManager.awayTeamData = SimulationManager.seasonMode.GetTeamData(awayIndex);
    SimulationManager.gameResults = SimulationManager.SimulateGame(SimulationManager.homeTeamData, SimulationManager.awayTeamData);
    int num1 = homeIndex;
    int? nullable1 = forcedWinnerIndex;
    int valueOrDefault1 = nullable1.GetValueOrDefault();
    if (num1 == valueOrDefault1 & nullable1.HasValue)
    {
      SimulationManager.gameResults.HomeGameSummary.TeamGameStats.Score = 100;
      SimulationManager.gameResults.AwayGameSummary.TeamGameStats.Score = 0;
    }
    else
    {
      int num2 = awayIndex;
      int? nullable2 = forcedWinnerIndex;
      int valueOrDefault2 = nullable2.GetValueOrDefault();
      if (num2 == valueOrDefault2 & nullable2.HasValue)
      {
        SimulationManager.gameResults.HomeGameSummary.TeamGameStats.Score = 0;
        SimulationManager.gameResults.AwayGameSummary.TeamGameStats.Score = 100;
      }
    }
    if (SimulationManager.offensivePlayerOfTheWeek != null)
    {
      if ((double) SimulationManager.gameResults.OffensivePlayerOfTheGame.statScore > (double) SimulationManager.offensivePlayerOfTheWeek.statScore)
        SimulationManager.offensivePlayerOfTheWeek.CopyAwardData(SimulationManager.gameResults.OffensivePlayerOfTheGame, AwardType.Offensive_Player_Of_The_Week);
      if ((double) SimulationManager.gameResults.DefensivePlayerOfTheGame.statScore > (double) SimulationManager.defensivePlayerOfTheWeek.statScore)
        SimulationManager.defensivePlayerOfTheWeek.CopyAwardData(SimulationManager.gameResults.DefensivePlayerOfTheGame, AwardType.Defensive_Player_Of_The_Week);
    }
    if (regularSeason)
    {
      GameSummary[,] scoresByWeek = SimulationManager.seasonModeData.GetScoresByWeek(conference);
      scoresByWeek[SimulationManager.seasonModeData.currentWeek - 1, homeIndexInSchedule] = SimulationManager.gameResults.HomeGameSummary;
      scoresByWeek[SimulationManager.seasonModeData.currentWeek - 1, awayIndexInSchedule] = SimulationManager.gameResults.AwayGameSummary;
    }
    else
    {
      GameSummary[] gameSummaryArray = (GameSummary[]) null;
      if (SimulationManager.seasonMode.IsFirstRoundOfPlayoffs())
        gameSummaryArray = SimulationManager.seasonModeData.GetPlayoffScoresByTier_R1(conference);
      else if (SimulationManager.seasonMode.IsSecondRoundOfPlayoffs())
        gameSummaryArray = SimulationManager.seasonModeData.GetPlayoffScoresByTier_R2(conference);
      else if (SimulationManager.seasonMode.IsThirdRoundOfPlayoffs())
        gameSummaryArray = SimulationManager.seasonModeData.GetPlayoffScoresByTier_R3(conference);
      else if (!SimulationManager.seasonModeData.UsesTierSystem && SimulationManager.seasonMode.IsFourthRoundOfNFLPlayoffs())
        gameSummaryArray = SimulationManager.seasonModeData.GetPlayoffScores_R4();
      gameSummaryArray[homeIndexInSchedule] = SimulationManager.gameResults.HomeGameSummary;
      gameSummaryArray[awayIndexInSchedule] = SimulationManager.gameResults.AwayGameSummary;
    }
    TeamStatGameType seasonGameType = SeasonModeManager.self.GetSeasonGameType(SimulationManager.homeTeamData, SimulationManager.awayTeamData);
    int teamIndex = PersistentData.GetUserTeam().TeamIndex;
    StatSet statSet1 = SimulationManager.homeTeamData.CurrentSeasonStats.AddStatsFromGame(SimulationManager.homeTeamData.CurrentGameStats, SimulationManager.awayTeamData.CurrentGameStats, seasonGameType, SimulationManager.awayTeamData.TeamIndex, SimulationManager.homeTeamData);
    if (homeIndex == teamIndex)
    {
      StatSet statSet2 = SeasonModeManager.self.CareerStats + statSet1;
    }
    StatSet statSet3 = SimulationManager.awayTeamData.CurrentSeasonStats.AddStatsFromGame(SimulationManager.awayTeamData.CurrentGameStats, SimulationManager.homeTeamData.CurrentGameStats, seasonGameType, SimulationManager.homeTeamData.TeamIndex, SimulationManager.awayTeamData);
    if (awayIndex == teamIndex)
    {
      StatSet statSet4 = SeasonModeManager.self.CareerStats + statSet3;
    }
    int score1 = SimulationManager.gameResults.HomeGameSummary.TeamGameStats.Score;
    int score2 = SimulationManager.gameResults.AwayGameSummary.TeamGameStats.Score;
    SimulationManager.homeTeamData.AddPlayerStatsFromGame();
    SimulationManager.awayTeamData.AddPlayerStatsFromGame();
    if (homeIndex == SimulationManager.seasonModeData.UserTeamIndex || awayIndex == SimulationManager.seasonModeData.UserTeamIndex)
    {
      if (homeIndex == SimulationManager.seasonModeData.UserTeamIndex)
      {
        PersistentData.userGameSummary = SimulationManager.gameResults.HomeGameSummary;
        PersistentData.compGameSummary = SimulationManager.gameResults.AwayGameSummary;
      }
      else
      {
        PersistentData.userGameSummary = SimulationManager.gameResults.AwayGameSummary;
        PersistentData.compGameSummary = SimulationManager.gameResults.HomeGameSummary;
      }
    }
    int num3 = score2;
    return score1 > num3;
  }

  private static List<int> BuildRunnerList(TeamData teamData)
  {
    List<int> intList = new List<int>();
    for (int index = 0; index < 25; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingRBIndex());
    for (int index = 0; index < 4; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingFBIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingWRZIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingQBIndex());
    return intList;
  }

  private static List<int> BuildReceiverList(TeamData teamData)
  {
    List<int> intList = new List<int>();
    for (int index = 0; index < 10; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingWRXIndex());
    for (int index = 0; index < 7; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingWRZIndex());
    for (int index = 0; index < 4; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingWRYIndex());
    for (int index = 0; index < 2; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingTEIndex());
    for (int index = 0; index < 2; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingRBIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingWRAIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingWRBIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingFBIndex());
    return intList;
  }

  private static List<int> BuildRushDefList(TeamData teamData)
  {
    List<int> intList = new List<int>();
    if (teamData.TeamDepthChart.NumberOfDLUsed == 3)
    {
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingLDEIndex_34());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingNTIndex());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingRDEIndex_34());
    }
    else
    {
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingLDEIndex_43());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingLDTIndex());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingRDTIndex());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingRDEIndex_43());
    }
    if (teamData.TeamDepthChart.NumberOfLBUsed == 3)
    {
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingWLBIndex());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingMLBIndex());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingSLBIndex());
    }
    else
    {
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingLOLBIndex());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingLILBIndex());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingRILBIndex());
      for (int index = 0; index < 2; ++index)
        intList.Add(teamData.TeamDepthChart.GetStartingROLBIndex());
    }
    return intList;
  }

  private static List<int> BuildPassDefList(TeamData teamData)
  {
    List<int> intList = new List<int>();
    for (int index = 0; index < 2; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingLCBIndex());
    for (int index = 0; index < 2; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingSSIndex());
    for (int index = 0; index < 2; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingFSIndex());
    for (int index = 0; index < 2; ++index)
      intList.Add(teamData.TeamDepthChart.GetStartingRCBIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingNickelBackIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingDimeBackIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingLOLBIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingLILBIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingRILBIndex());
    intList.Add(teamData.TeamDepthChart.GetStartingROLBIndex());
    return intList;
  }

  private static List<int> BuildLinemenList(TeamData teamData) => new List<int>()
  {
    teamData.TeamDepthChart.GetStartingLTIndex(),
    teamData.TeamDepthChart.GetStartingLGIndex(),
    teamData.TeamDepthChart.GetStartingCIndex(),
    teamData.TeamDepthChart.GetStartingRTIndex(),
    teamData.TeamDepthChart.GetStartingRGIndex()
  };

  private static int[] GetTeamRating(TeamData teamData)
  {
    int[] teamRating = new int[5]
    {
      Mathf.RoundToInt(0.0f + (float) teamData.TeamDepthChart.GetStartingQB().GetOverall() * 0.35f + (float) teamData.TeamDepthChart.GetStartingWRX().GetOverall() * 0.16f + (float) teamData.TeamDepthChart.GetStartingWRZ().GetOverall() * 0.16f + (float) teamData.TeamDepthChart.GetStartingTE().GetOverall() * 0.1f + (float) teamData.TeamDepthChart.GetStartingWRY().GetOverall() * 0.08f + (float) teamData.TeamDepthChart.GetStartingRB().GetOverall() * 0.05f + (float) teamData.TeamDepthChart.GetStartingLT().GetOverall() * 0.02f + (float) teamData.TeamDepthChart.GetStartingLG().GetOverall() * 0.02f + (float) teamData.TeamDepthChart.GetStartingC().GetOverall() * 0.02f + (float) teamData.TeamDepthChart.GetStartingRG().GetOverall() * 0.02f + (float) teamData.TeamDepthChart.GetStartingRT().GetOverall() * 0.02f),
      Mathf.RoundToInt(0.0f + (float) teamData.TeamDepthChart.GetStartingRB().GetOverall() * 0.4f + (float) teamData.TeamDepthChart.GetStartingLT().GetOverall() * 0.09f + (float) teamData.TeamDepthChart.GetStartingLG().GetOverall() * 0.09f + (float) teamData.TeamDepthChart.GetStartingC().GetOverall() * 0.09f + (float) teamData.TeamDepthChart.GetStartingRG().GetOverall() * 0.09f + (float) teamData.TeamDepthChart.GetStartingRT().GetOverall() * 0.09f + (float) teamData.TeamDepthChart.GetStartingTE().GetOverall() * 0.08f + (float) teamData.TeamDepthChart.GetStartingFB().GetOverall() * 0.07f),
      0,
      0,
      0
    };
    float num1 = 0.0f + (float) teamData.TeamDepthChart.GetStartingLCB().GetOverall() * 0.19f + (float) teamData.TeamDepthChart.GetStartingRCB().GetOverall() * 0.19f + (float) teamData.TeamDepthChart.GetStartingFS().GetOverall() * 0.17f + (float) teamData.TeamDepthChart.GetStartingSS().GetOverall() * 0.17f;
    teamRating[2] = Mathf.RoundToInt((teamData.TeamDepthChart.NumberOfLBUsed != 3 ? num1 + (float) teamData.TeamDepthChart.GetStartingLOLB().GetOverall() * 0.05f + (float) teamData.TeamDepthChart.GetStartingLILB().GetOverall() * 0.05f + (float) teamData.TeamDepthChart.GetStartingRILB().GetOverall() * 0.05f + (float) teamData.TeamDepthChart.GetStartingROLB().GetOverall() * 0.05f : num1 + (float) teamData.TeamDepthChart.GetStartingWLB().GetOverall() * 0.0667f + (float) teamData.TeamDepthChart.GetStartingMLB().GetOverall() * 0.0667f + (float) teamData.TeamDepthChart.GetStartingSLB().GetOverall() * 0.0667f) + (float) teamData.TeamDepthChart.GetStartingNickelBack().GetOverall() * 0.04f + (float) teamData.TeamDepthChart.GetStartingDimeBack().GetOverall() * 0.04f);
    float num2 = 0.0f;
    teamRating[3] = Mathf.RoundToInt((teamData.TeamDepthChart.NumberOfDLUsed != 3 ? num2 + (float) teamData.TeamDepthChart.GetStartingLDE_43().GetOverall() * 0.15f + (float) teamData.TeamDepthChart.GetStartingLDT().GetOverall() * 0.15f + (float) teamData.TeamDepthChart.GetStartingRDT().GetOverall() * 0.15f + (float) teamData.TeamDepthChart.GetStartingRDE_43().GetOverall() * 0.15f : num2 + (float) teamData.TeamDepthChart.GetStartingLDE_34().GetOverall() * 0.2f + (float) teamData.TeamDepthChart.GetStartingNT().GetOverall() * 0.2f + (float) teamData.TeamDepthChart.GetStartingRDE_34().GetOverall() * 0.2f) + (float) teamData.TeamDepthChart.GetStartingLOLB().GetOverall() * 0.1f + (float) teamData.TeamDepthChart.GetStartingLILB().GetOverall() * 0.1f + (float) teamData.TeamDepthChart.GetStartingRILB().GetOverall() * 0.1f + (float) teamData.TeamDepthChart.GetStartingROLB().GetOverall() * 0.1f);
    teamRating[4] = Mathf.RoundToInt(0.0f + (float) teamData.TeamDepthChart.GetStartingKicker().GetOverall() * 0.3f + (float) teamData.TeamDepthChart.GetStartingPunter().GetOverall() * 0.3f + (float) teamData.TeamDepthChart.GetStartingPuntReturner().GetOverall() * 0.2f + (float) teamData.TeamDepthChart.GetStartingKickReturner().GetOverall() * 0.2f);
    return teamRating;
  }
}
