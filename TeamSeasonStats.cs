// Decompiled with JetBrains decompiler
// Type: TeamSeasonStats
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class TeamSeasonStats
{
  [Key(0)]
  public int wins;
  [Key(1)]
  public int losses;
  [Key(2)]
  public int teamPlusMinus;
  [Key(3)]
  public int streak;
  [Key(4)]
  public int turnovers;
  [Key(5)]
  public int tacklesForLoss;
  [Key(6)]
  public int interceptions;
  [Key(7)]
  public int forcedFumbles;
  [Key(8)]
  public int fumbleRecoveries;
  [Key(9)]
  public int passYards;
  [Key(10)]
  public int rushYards;
  [Key(11)]
  public int totalYards;
  [Key(12)]
  public int passYardsAllowed;
  [Key(13)]
  public int rushYardsAllowed;
  [Key(14)]
  public int totalYardsAllowed;
  [Key(15)]
  public int sacks;
  [Key(16)]
  public int turnoverMargin;
  [Key(17)]
  public int pointsScored;
  [Key(18)]
  public int pointsAllowed;
  [Key(19)]
  public int confWins;
  [Key(20)]
  public int confLosses;
  [Key(21)]
  public int divWins;
  [Key(22)]
  public int divLosses;
  [Key(23)]
  public int touchdowns;
  [Key(24)]
  public int totalPassPlays;
  [Key(25)]
  public int droppedPasses;
  [Key(26)]
  public int rivalWins;
  [Key(27)]
  public int redZoneAppearances;
  [Key(28)]
  public int redZoneTouchdowns;
  [Key(29)]
  public bool IsSuperbowlWinner;
  [Key(31)]
  public int TeamIndex;
  [Key(32)]
  public bool IsConferenceChampion;
  [Key(33)]
  public int qbInts;

  [Key(30)]
  public HashSet<int> TeamsBeaten { get; set; } = new HashSet<int>();

  [IgnoreMember]
  public float RedZoneEfficiency => this.redZoneTouchdowns == 0 ? 0.0f : (float) (this.redZoneAppearances / this.redZoneTouchdowns);

  public int GetCategoryValue(TeamStatCategory c)
  {
    switch (c)
    {
      case TeamStatCategory.Total_Yards:
        return this.totalYards;
      case TeamStatCategory.Pass_Yards:
        return this.passYards;
      case TeamStatCategory.Rush_Yards:
        return this.rushYards;
      case TeamStatCategory.Total_Yards_Allowed:
        return this.totalYardsAllowed;
      case TeamStatCategory.Pass_Defense:
        return this.passYardsAllowed;
      case TeamStatCategory.Rush_Defense:
        return this.rushYardsAllowed;
      case TeamStatCategory.Sacks:
        return this.sacks;
      case TeamStatCategory.Turnover_Margin:
        return this.turnoverMargin;
      case TeamStatCategory.Points_Scored:
        return this.pointsScored;
      case TeamStatCategory.Points_Allowed:
        return this.pointsAllowed;
      default:
        Debug.Log((object) "Trying to sort on a field that doesn't exist");
        return 0;
    }
  }

  public StatSet AddStatsFromGame(
    TeamGameStats statsFromGame,
    TeamGameStats opponentStatsFromGame,
    TeamStatGameType type,
    int opponentTeamIndex,
    TeamData userTeamData)
  {
    StatSet statDelta = new StatSet();
    if (statsFromGame == null || opponentStatsFromGame == null)
      return statDelta;
    bool flag = statsFromGame.Score > opponentStatsFromGame.Score;
    if (flag)
    {
      int num = (int) this.IncreaseWins(type);
      this.TeamsBeaten.Add(opponentTeamIndex);
      ++statDelta.wins;
      if (!SeasonModeManager.self.InRegularSeason())
        ++statDelta.playoffWins;
      if ((num & 4) != 0)
        ++statDelta.confWins;
      if ((num & 2) != 0)
        ++statDelta.divWins;
      statDelta.teamsBeaten.Add(opponentTeamIndex);
    }
    else
    {
      int num = (int) this.IncreaseLosses(type);
      ++statDelta.losses;
      if ((num & 4) != 0)
        ++statDelta.confLosses;
      if ((num & 2) != 0)
        ++statDelta.divLosses;
    }
    if (PersistentData.LastPlayedGameType != GameType.QuickMatch)
    {
      if (SeasonModeManager.self.IsFourthRoundOfNFLPlayoffs() & flag)
      {
        this.IsSuperbowlWinner = true;
        ++statDelta.superBowlWins;
        statDelta.superBowlWiningTeams.Add(this.TeamIndex);
        ++statDelta.superBowlMvpAwards;
      }
      else if (SeasonModeManager.self.IsThirdRoundOfPlayoffs() & flag)
      {
        this.IsConferenceChampion = true;
        ++statDelta.confChampionships;
      }
    }
    statDelta.teamPlusMinus = statsFromGame.Score - opponentStatsFromGame.Score;
    this.teamPlusMinus += statDelta.teamPlusMinus;
    statDelta.passYards = statsFromGame.PassYards;
    this.passYards += statDelta.passYards;
    statDelta.passYardsAllowed = opponentStatsFromGame.PassYards;
    this.passYardsAllowed += statDelta.passYardsAllowed;
    statDelta.rushYards = statsFromGame.RushYards;
    this.rushYards += statDelta.rushYards;
    statDelta.rushYardsAllowed = opponentStatsFromGame.RushYards;
    this.rushYardsAllowed += statDelta.rushYardsAllowed;
    statDelta.totalYards = statsFromGame.RushYards + statsFromGame.PassYards;
    this.totalYards += statDelta.totalYards;
    statDelta.totalYardsAllowed = opponentStatsFromGame.RushYards + opponentStatsFromGame.PassYards;
    this.totalYardsAllowed += statDelta.totalYardsAllowed;
    statDelta.sacks = statsFromGame.Sacks;
    this.sacks += statDelta.sacks;
    statDelta.turnovers = statsFromGame.Turnovers;
    this.turnovers += statDelta.turnovers;
    statDelta.turnoverMargin = opponentStatsFromGame.Turnovers - statsFromGame.Turnovers;
    this.turnoverMargin += statDelta.turnoverMargin;
    statDelta.pointsScored = statsFromGame.Score;
    this.pointsScored += statDelta.pointsScored;
    statDelta.pointsAllowed = opponentStatsFromGame.Score;
    this.pointsAllowed += statDelta.pointsAllowed;
    statDelta.tacklesForLoss = statsFromGame.TacklesForLoss;
    this.tacklesForLoss += statDelta.tacklesForLoss;
    statDelta.interceptions = statsFromGame.Interceptions;
    this.interceptions += statDelta.interceptions;
    statDelta.qbInts = statsFromGame.Ints;
    this.qbInts += statDelta.qbInts;
    statDelta.forcedFumbles = statsFromGame.ForcedFumbles;
    this.forcedFumbles += statDelta.forcedFumbles;
    statDelta.fumbleRecoveries = statsFromGame.FumbleRecoveries;
    this.fumbleRecoveries += statDelta.fumbleRecoveries;
    statDelta.touchdowns = statsFromGame.Touchdowns;
    this.touchdowns += statDelta.touchdowns;
    statDelta.totalPassPlays = statsFromGame.TotalPassPlays;
    this.totalPassPlays += statDelta.totalPassPlays;
    statDelta.droppedPasses = statsFromGame.DroppedPasses;
    this.droppedPasses += statDelta.droppedPasses;
    statDelta.rivalWins = !ProEra.Game.MatchState.Stats.IsRivalMatch || statsFromGame.Score <= opponentStatsFromGame.Score ? 0 : 1;
    this.rivalWins += statDelta.rivalWins;
    statDelta.redZoneAppearances = statsFromGame.RedZoneAppearances;
    this.redZoneAppearances += statDelta.redZoneAppearances;
    statDelta.redZoneTouchdowns = statsFromGame.RedZoneTouchdowns;
    this.redZoneTouchdowns += statDelta.redZoneTouchdowns;
    this.UpdatePassingStatsForGame(userTeamData, statDelta);
    return statDelta;
  }

  private void UpdatePassingStatsForGame(TeamData userTeamData, StatSet statDelta)
  {
    int numberOfPlayers = userTeamData.MainRoster.GetNumberOfPlayers();
    for (int playerIndex = 0; playerIndex < numberOfPlayers; ++playerIndex)
    {
      PlayerData player = userTeamData.GetPlayer(playerIndex);
      statDelta.touchdownPasses += player.CurrentGameStats.QBPassTDs;
      statDelta.passCompletions += player.CurrentGameStats.QBCompletions;
    }
  }

  public TeamSeasonStats.WinLossType IncreaseWins(TeamStatGameType type)
  {
    TeamSeasonStats.WinLossType winLossType = TeamSeasonStats.WinLossType.Game;
    ++this.wins;
    switch (type)
    {
      case TeamStatGameType.Conference:
        ++this.confWins;
        winLossType |= TeamSeasonStats.WinLossType.Conference;
        break;
      case TeamStatGameType.Division:
        ++this.divWins;
        winLossType |= TeamSeasonStats.WinLossType.Division | TeamSeasonStats.WinLossType.Conference;
        break;
    }
    if (this.streak > 0)
      ++this.streak;
    else
      this.streak = 1;
    return winLossType;
  }

  public TeamSeasonStats.WinLossType IncreaseLosses(TeamStatGameType type)
  {
    TeamSeasonStats.WinLossType winLossType = TeamSeasonStats.WinLossType.Game;
    ++this.losses;
    switch (type)
    {
      case TeamStatGameType.Conference:
        ++this.confLosses;
        winLossType |= TeamSeasonStats.WinLossType.Conference;
        break;
      case TeamStatGameType.Division:
        ++this.divLosses;
        winLossType |= TeamSeasonStats.WinLossType.Division | TeamSeasonStats.WinLossType.Conference;
        break;
    }
    if (this.streak < 0)
      --this.streak;
    else
      this.streak = -1;
    return winLossType;
  }

  public string GetRecordString(TeamStatGameType type)
  {
    switch (type)
    {
      case TeamStatGameType.NonConference:
        return "(" + this.wins.ToString() + "-" + this.losses.ToString() + ")";
      case TeamStatGameType.Conference:
        return "(" + this.confWins.ToString() + "-" + this.confLosses.ToString() + ")";
      case TeamStatGameType.Division:
        return "(" + this.divWins.ToString() + "-" + this.divLosses.ToString() + ")";
      default:
        return "(" + this.wins.ToString() + "-" + this.losses.ToString() + ")";
    }
  }

  public string GetStreakString() => this.streak > 0 ? "W" + this.streak.ToString() : "L" + (this.streak * -1).ToString();

  public void Print()
  {
    Debug.Log((object) "Team Season Stats--");
    string recordString = this.GetRecordString(~TeamStatGameType.NonConference);
    this.GetRecordString(TeamStatGameType.NonConference);
    this.GetRecordString(TeamStatGameType.Conference);
    this.GetRecordString(TeamStatGameType.Division);
    Debug.Log((object) (" Overall Record: " + recordString));
    Debug.Log((object) (" Non Conf Record: " + recordString));
    Debug.Log((object) (" Conf Record: " + recordString));
    Debug.Log((object) (" Div Record: " + recordString));
    Debug.Log((object) string.Format("  teamPlusMinus: {0}", (object) this.teamPlusMinus));
    Debug.Log((object) string.Format("  passYards: {0}", (object) this.passYards));
    Debug.Log((object) string.Format("  teamPlusMinus: {0}", (object) this.teamPlusMinus));
    Debug.Log((object) string.Format("  passYardsAllowed: {0}", (object) this.passYardsAllowed));
    Debug.Log((object) string.Format("  rushYards: {0}", (object) this.rushYards));
    Debug.Log((object) string.Format("  rushYardsAllowed: {0}", (object) this.rushYardsAllowed));
    Debug.Log((object) string.Format("  totalYards: {0}", (object) this.totalYards));
    Debug.Log((object) string.Format("  totalYardsAllowed: {0}", (object) this.totalYardsAllowed));
    Debug.Log((object) string.Format("  sacks: {0}", (object) this.sacks));
    Debug.Log((object) string.Format("  turnovers: {0}", (object) this.turnovers));
    Debug.Log((object) string.Format("  turnoverMargin: {0}", (object) this.turnoverMargin));
    Debug.Log((object) string.Format("  pointsScored: {0}", (object) this.pointsScored));
    Debug.Log((object) string.Format("  pointsAllowed: {0}", (object) this.pointsAllowed));
    Debug.Log((object) string.Format("  tacklesForLoss: {0}", (object) this.tacklesForLoss));
    Debug.Log((object) string.Format("  interceptions: {0}", (object) this.interceptions));
    Debug.Log((object) string.Format("  forcedFumbles: {0}", (object) this.forcedFumbles));
    Debug.Log((object) string.Format("  fumbleRecoveries: {0}", (object) this.fumbleRecoveries));
  }

  [Flags]
  public enum WinLossType
  {
    Game = 1,
    Division = 2,
    Conference = 4,
  }
}
