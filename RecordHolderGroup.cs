// Decompiled with JetBrains decompiler
// Type: RecordHolderGroup
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;

[MessagePackObject(false)]
[Serializable]
public class RecordHolderGroup
{
  [Key(0)]
  public RecordHolder QBRating;
  [Key(1)]
  public RecordHolder Completions;
  [Key(2)]
  public RecordHolder PassAttempts;
  [Key(3)]
  public RecordHolder CompletionPercentage;
  [Key(4)]
  public RecordHolder PassYards;
  [Key(5)]
  public RecordHolder PassTDs;
  [Key(6)]
  public RecordHolder ThrownInts;
  [Key(7)]
  public RecordHolder YardsPerPass;
  [Key(8)]
  public RecordHolder LongestPass;
  [Key(9)]
  public RecordHolder RushAttempts;
  [Key(10)]
  public RecordHolder RushYards;
  [Key(11)]
  public RecordHolder RushTDs;
  [Key(12)]
  public RecordHolder YardsPerRush;
  [Key(13)]
  public RecordHolder LongestRush;
  [Key(14)]
  public RecordHolder Fumbles;
  [Key(15)]
  public RecordHolder Receptions;
  [Key(16)]
  public RecordHolder ReceivingYards;
  [Key(17)]
  public RecordHolder ReceivingTDs;
  [Key(18)]
  public RecordHolder YardsPerCatch;
  [Key(19)]
  public RecordHolder LongestReception;
  [Key(20)]
  public RecordHolder YardsAfterCatch;
  [Key(21)]
  public RecordHolder Drops;
  [Key(22)]
  public RecordHolder Targets;
  [Key(23)]
  public RecordHolder TotalTDs;
  [Key(24)]
  public RecordHolder Tackles;
  [Key(25)]
  public RecordHolder Sacks;
  [Key(26)]
  public RecordHolder Interceptions;
  [Key(27)]
  public RecordHolder TacklesForLoss;
  [Key(28)]
  public RecordHolder DefensiveTDs;
  [Key(29)]
  public RecordHolder KnockDowns;
  [Key(30)]
  public RecordHolder ForcedFumbles;
  [Key(31)]
  public RecordHolder FumbleRecoveries;
  [Key(32)]
  public RecordHolder FGMade;
  [Key(33)]
  public RecordHolder FGAttempted;
  [Key(34)]
  public RecordHolder XPMade;
  [Key(35)]
  public RecordHolder XPAttempted;
  [Key(36)]
  public RecordHolder Punts;
  [Key(37)]
  public RecordHolder PuntsInside20;
  [Key(38)]
  public RecordHolder PuntTouchbacks;
  [Key(39)]
  public RecordHolder YardsPerPunt;
  [Key(40)]
  public RecordHolder PuntReturns;
  [Key(41)]
  public RecordHolder PuntReturnYards;
  [Key(42)]
  public RecordHolder YardsPerPuntReturn;
  [Key(43)]
  public RecordHolder PuntReturnTDs;
  [Key(44)]
  public RecordHolder KickReturns;
  [Key(45)]
  public RecordHolder KickReturnYards;
  [Key(46)]
  public RecordHolder YardsPerKickReturn;
  [Key(47)]
  public RecordHolder KickReturnTDs;
  [Key(48)]
  public RecordHolder TotalYards;
  [Key(49)]
  public RecordHolder PassYardsAllowed;
  [Key(50)]
  public RecordHolder RushYardsAllowed;
  [Key(51)]
  public RecordHolder TotalYardsAllowed;
  [Key(52)]
  public RecordHolder Turnovers;
  [Key(53)]
  public RecordHolder TurnoverMargin;
  [Key(54)]
  public RecordHolder PointsScored;
  [Key(55)]
  public RecordHolder PointsAllowed;
  [Key(56)]
  public RecordHolder Wins;
  [Key(57)]
  public RecordHolder Losses;
  [Key(58)]
  public RecordHolder WinStreak;
  [Key(59)]
  public RecordHolder LossStreak;

  public RecordHolderGroup()
  {
    this.QBRating = RecordHolder.New("QB RATING <size=11>(MIN 200)</size>");
    this.Completions = RecordHolder.New("COMPLETIONS");
    this.PassAttempts = RecordHolder.New("PASS ATTEMPTS");
    this.CompletionPercentage = RecordHolder.New("COMP PERCENTAGE <size=11>(MIN 200)</size>");
    this.PassYards = RecordHolder.New("PASS YARDS");
    this.PassTDs = RecordHolder.New("PASS TDS");
    this.ThrownInts = RecordHolder.New("INTERCEPTIONS");
    this.YardsPerPass = RecordHolder.New("YARDS PER PASS <size=11>(MIN 200)</size>");
    this.LongestPass = RecordHolder.New("LONGEST PASS");
    this.RushAttempts = RecordHolder.New("RUSH ATTEMPTS");
    this.RushYards = RecordHolder.New("RUSH YARDS");
    this.RushTDs = RecordHolder.New("RUSH TDS");
    this.YardsPerRush = RecordHolder.New("YARDS PER RUSH <size=11>(MIN 100)</size>");
    this.LongestRush = RecordHolder.New("LONGEST RUSH");
    this.Fumbles = RecordHolder.New("FUMBLES");
    this.Receptions = RecordHolder.New("RECEPTIONS");
    this.ReceivingYards = RecordHolder.New("RECEIVING YARDS");
    this.ReceivingTDs = RecordHolder.New("RECEIVING TDS");
    this.YardsPerCatch = RecordHolder.New("YARDS PER CATCH <size=11>(MIN 30)</size>");
    this.LongestReception = RecordHolder.New("LONGEST RECEPTION");
    this.YardsAfterCatch = RecordHolder.New("YARDS AFTER CATCH");
    this.Drops = RecordHolder.New("DROPS");
    this.Targets = RecordHolder.New("TARGETS");
    this.TotalTDs = RecordHolder.New("TOTAL TDS");
    this.Tackles = RecordHolder.New("TACKLES");
    this.Sacks = RecordHolder.New("SACKS");
    this.Interceptions = RecordHolder.New("INTERCEPTIONS");
    this.TacklesForLoss = RecordHolder.New("TACKLES FOR LOSS");
    this.DefensiveTDs = RecordHolder.New("DEFENSIVE TDS");
    this.KnockDowns = RecordHolder.New("DEFLECTED PASSES");
    this.ForcedFumbles = RecordHolder.New("FORCED FUMBLES");
    this.FumbleRecoveries = RecordHolder.New("FUMBLE RECOVERIES");
    this.FGMade = RecordHolder.New("FGS MADE");
    this.FGAttempted = RecordHolder.New("FGS ATTEMPTED");
    this.XPMade = RecordHolder.New("EXTRA POINTS MADE");
    this.XPAttempted = RecordHolder.New("EXTRA POINTS ATT");
    this.Punts = RecordHolder.New("PUNTS");
    this.PuntsInside20 = RecordHolder.New("PUNTS INSIDE 20");
    this.PuntTouchbacks = RecordHolder.New("PUNT TOUCHBACKS");
    this.YardsPerPunt = RecordHolder.New("YARDS PER PUNT <size=11>(MIN 45)</size>");
    this.PuntReturns = RecordHolder.New("PUNT RETURNS");
    this.PuntReturnYards = RecordHolder.New("PUNT RETURN YARDS");
    this.YardsPerPuntReturn = RecordHolder.New("YDS PER PUNT RET <size=11>(MIN 20)</size>");
    this.PuntReturnTDs = RecordHolder.New("PUNT RETURN TDS");
    this.KickReturns = RecordHolder.New("KICK RETURNS");
    this.KickReturnYards = RecordHolder.New("KICK RETURN YARDS");
    this.YardsPerKickReturn = RecordHolder.New("YDS PER KICK RET <size=11>(MIN 20)</size>");
    this.KickReturnTDs = RecordHolder.New("KICK RETURN TDS");
    this.TotalYards = RecordHolder.New("TOTAL YARDS");
    this.PassYardsAllowed = RecordHolder.New("PASS YDS ALLOWED", 10000f);
    this.RushYardsAllowed = RecordHolder.New("RUSH YDS ALLOWED", 10000f);
    this.TotalYardsAllowed = RecordHolder.New("TOTAL YDS ALLOWED", 10000f);
    this.TurnoverMargin = RecordHolder.New("BEST TURNOVER MARGIN", -100f);
    this.Turnovers = RecordHolder.New("MOST TURNOVERS");
    this.PointsScored = RecordHolder.New("POINTS SCORED");
    this.PointsAllowed = RecordHolder.New("POINTS ALLOWED", 10000f);
    this.Wins = RecordHolder.New("MOST WINS");
    this.Losses = RecordHolder.New("MOST LOSSES");
    this.WinStreak = RecordHolder.New("BEST WIN STREAK");
    this.LossStreak = RecordHolder.New("WORST LOSS STREAK");
  }

  public void CheckForBrokenRecords_TeamSeasonFranchise()
  {
    if (SeasonModeManager.self.seasonModeData.currentWeek > SeasonModeManager.self.seasonModeData.NumberOfWeeksInSeason + 1)
      return;
    TeamData userTeamData = SeasonModeManager.self.userTeamData;
    this.CheckForBrokenRecords_TeamSeason(userTeamData.CurrentSeasonStats, userTeamData.GetCity(), userTeamData.GetAbbreviation(), SeasonModeManager.self.GetCurrentYear());
  }

  public void CheckForBrokenRecords_TeamGameLeague()
  {
    int currentYear = SeasonModeManager.self.GetCurrentYear();
    for (int index = 0; index < SeasonModeManager.self.seasonModeData.NumberOfTeamsInLeague; ++index)
    {
      TeamData teamData = SeasonModeManager.self.GetTeamData(SeasonModeManager.self.seasonModeData.TeamIndexMasterList[index]);
      this.CheckForBrokenRecords_TeamGame(teamData.CurrentGameStats, teamData.GetCity(), teamData.GetAbbreviation(), currentYear);
    }
  }

  public void CheckForBrokenRecords_TeamSeasonLeague()
  {
    if (SeasonModeManager.self.seasonModeData.currentWeek > SeasonModeManager.self.seasonModeData.NumberOfWeeksInSeason + 1)
      return;
    int currentYear = SeasonModeManager.self.GetCurrentYear();
    for (int index = 0; index < SeasonModeManager.self.seasonModeData.NumberOfTeamsInLeague; ++index)
    {
      TeamData teamData = SeasonModeManager.self.GetTeamData(SeasonModeManager.self.seasonModeData.TeamIndexMasterList[index]);
      this.CheckForBrokenRecords_TeamSeason(teamData.CurrentSeasonStats, teamData.GetCity(), teamData.GetAbbreviation(), currentYear);
    }
  }

  private void CheckForBrokenRecords_TeamGame(
    TeamGameStats teamStats,
    string teamName,
    string teamAbbrev,
    int year)
  {
    if ((double) teamStats.PassYards > (double) this.PassYards.RecordValue)
      this.PassYards.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.PassYards);
    if ((double) teamStats.RushYards > (double) this.RushYards.RecordValue)
      this.RushYards.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.RushYards);
    if ((double) teamStats.TotalYards() > (double) this.TotalYards.RecordValue)
      this.TotalYards.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.TotalYards());
    if ((double) teamStats.Score > (double) this.PointsScored.RecordValue)
      this.PointsScored.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.Score);
    if ((double) teamStats.Turnovers > (double) this.Turnovers.RecordValue)
      this.Turnovers.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.Turnovers);
    if ((double) teamStats.Sacks > (double) this.Sacks.RecordValue)
      this.Sacks.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.Sacks);
    if ((double) teamStats.TacklesForLoss > (double) this.TacklesForLoss.RecordValue)
      this.TacklesForLoss.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.TacklesForLoss);
    if ((double) teamStats.Interceptions > (double) this.Interceptions.RecordValue)
      this.Interceptions.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.Interceptions);
    if ((double) teamStats.ForcedFumbles > (double) this.ForcedFumbles.RecordValue)
      this.ForcedFumbles.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.ForcedFumbles);
    if ((double) teamStats.FumbleRecoveries <= (double) this.FumbleRecoveries.RecordValue)
      return;
    this.FumbleRecoveries.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.FumbleRecoveries);
  }

  private void CheckForBrokenRecords_TeamSeason(
    TeamSeasonStats teamStats,
    string teamName,
    string teamAbbrev,
    int year)
  {
    if ((double) teamStats.passYards > (double) this.PassYards.RecordValue)
      this.PassYards.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.passYards);
    if ((double) teamStats.rushYards > (double) this.RushYards.RecordValue)
      this.RushYards.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.rushYards);
    if ((double) teamStats.totalYards > (double) this.TotalYards.RecordValue)
      this.TotalYards.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.totalYards);
    if ((double) teamStats.pointsScored > (double) this.PointsScored.RecordValue)
      this.PointsScored.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.pointsScored);
    if ((double) teamStats.turnovers > (double) this.Turnovers.RecordValue)
      this.Turnovers.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.turnovers);
    if ((double) teamStats.turnoverMargin > (double) this.TurnoverMargin.RecordValue)
      this.TurnoverMargin.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.turnoverMargin);
    if ((double) teamStats.wins > (double) this.Wins.RecordValue)
      this.Wins.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.wins);
    if ((double) teamStats.losses > (double) this.Losses.RecordValue)
      this.Losses.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.losses);
    if ((double) teamStats.streak > (double) this.WinStreak.RecordValue)
      this.WinStreak.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.streak);
    if ((double) teamStats.streak < (double) this.LossStreak.RecordValue)
      this.LossStreak.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.streak);
    if ((double) teamStats.sacks > (double) this.Sacks.RecordValue)
      this.Sacks.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.sacks);
    if ((double) teamStats.tacklesForLoss > (double) this.TacklesForLoss.RecordValue)
      this.TacklesForLoss.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.tacklesForLoss);
    if ((double) teamStats.interceptions > (double) this.Interceptions.RecordValue)
      this.Interceptions.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.interceptions);
    if ((double) teamStats.forcedFumbles > (double) this.ForcedFumbles.RecordValue)
      this.ForcedFumbles.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.forcedFumbles);
    if ((double) teamStats.fumbleRecoveries > (double) this.FumbleRecoveries.RecordValue)
      this.FumbleRecoveries.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.fumbleRecoveries);
    if (SeasonModeManager.self.seasonModeData.currentWeek != SeasonModeManager.self.seasonModeData.NumberOfWeeksInSeason + 1)
      return;
    if ((double) teamStats.pointsAllowed < (double) this.PointsAllowed.RecordValue)
      this.PointsAllowed.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.pointsAllowed);
    if ((double) teamStats.passYardsAllowed < (double) this.PassYardsAllowed.RecordValue)
      this.PassYardsAllowed.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.passYardsAllowed);
    if ((double) teamStats.rushYardsAllowed < (double) this.RushYardsAllowed.RecordValue)
      this.RushYardsAllowed.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.rushYardsAllowed);
    if ((double) teamStats.totalYardsAllowed >= (double) this.TotalYardsAllowed.RecordValue)
      return;
    this.TotalYardsAllowed.UpdateRecord(teamName, teamAbbrev, year, (float) teamStats.totalYardsAllowed);
  }

  public void CheckForBrokenRecords_IndividualGameFranchise()
  {
    TeamData userTeamData = SeasonModeManager.self.userTeamData;
    string abbreviation = userTeamData.GetAbbreviation();
    int currentYear = SeasonModeManager.self.GetCurrentYear();
    for (int playerIndex = 0; playerIndex < userTeamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      PlayerData player = userTeamData.GetPlayer(playerIndex);
      if (player != null && player.CurrentGameStats != null)
        this.CheckForBrokenRecords_Individual(player.CurrentGameStats, player.FirstInitalAndLastName, abbreviation, currentYear);
    }
  }

  public void CheckForBrokenRecords_IndividualSeasonFranchise()
  {
    if (SeasonModeManager.self.seasonModeData.currentWeek > SeasonModeManager.self.seasonModeData.NumberOfWeeksInSeason + 1)
      return;
    TeamData userTeamData = SeasonModeManager.self.userTeamData;
    string abbreviation = userTeamData.GetAbbreviation();
    int currentYear = SeasonModeManager.self.GetCurrentYear();
    for (int playerIndex = 0; playerIndex < userTeamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      PlayerData player = userTeamData.GetPlayer(playerIndex);
      if (player != null && player.CurrentSeasonStats != null)
        this.CheckForBrokenRecords_Individual(player.CurrentSeasonStats, player.FirstInitalAndLastName, abbreviation, currentYear);
    }
  }

  public void CheckForBrokenRecords_IndividualCareerFranchise()
  {
    TeamData userTeamData = SeasonModeManager.self.userTeamData;
    string abbreviation = userTeamData.GetAbbreviation();
    int currentYear = SeasonModeManager.self.GetCurrentYear();
    for (int playerIndex = 0; playerIndex < userTeamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      PlayerData player = userTeamData.GetPlayer(playerIndex);
      if (player != null && player.TotalCareerStats != null)
        this.CheckForBrokenRecords_Individual(player.TotalCareerStats, player.FirstInitalAndLastName, abbreviation, currentYear);
    }
  }

  public void CheckForBrokenRecords_IndividualGameLeague()
  {
    int currentYear = SeasonModeManager.self.GetCurrentYear();
    for (int index = 0; index < SeasonModeManager.self.seasonModeData.NumberOfTeamsInLeague; ++index)
    {
      TeamData teamData = SeasonModeManager.self.GetTeamData(SeasonModeManager.self.seasonModeData.TeamIndexMasterList[index]);
      string abbreviation = teamData.GetAbbreviation();
      for (int playerIndex = 0; playerIndex < teamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
      {
        PlayerData player = teamData.GetPlayer(playerIndex);
        if (player != null)
        {
          string initalAndLastName = player.FirstInitalAndLastName;
          PlayerStats currentGameStats = player.CurrentGameStats;
          if (currentGameStats != null)
            this.CheckForBrokenRecords_Individual(currentGameStats, initalAndLastName, abbreviation, currentYear);
        }
      }
    }
  }

  public void CheckForBrokenRecords_IndividualSeasonLeague()
  {
    if (SeasonModeManager.self.seasonModeData.currentWeek > SeasonModeManager.self.seasonModeData.NumberOfWeeksInSeason + 1)
      return;
    int currentYear = SeasonModeManager.self.GetCurrentYear();
    for (int index = 0; index < SeasonModeManager.self.seasonModeData.NumberOfTeamsInLeague; ++index)
    {
      TeamData teamData = SeasonModeManager.self.GetTeamData(SeasonModeManager.self.seasonModeData.TeamIndexMasterList[index]);
      string abbreviation = teamData.GetAbbreviation();
      for (int playerIndex = 0; playerIndex < teamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
      {
        PlayerData player = teamData.GetPlayer(playerIndex);
        if (player != null)
        {
          string initalAndLastName = player.FirstInitalAndLastName;
          PlayerStats currentSeasonStats = player.CurrentSeasonStats;
          if (currentSeasonStats != null)
            this.CheckForBrokenRecords_Individual(currentSeasonStats, initalAndLastName, abbreviation, currentYear);
        }
      }
    }
  }

  public void CheckForBrokenRecords_IndividualCareerLeague()
  {
    int currentYear = SeasonModeManager.self.GetCurrentYear();
    for (int index = 0; index < SeasonModeManager.self.seasonModeData.NumberOfTeamsInLeague; ++index)
    {
      TeamData teamData = SeasonModeManager.self.GetTeamData(SeasonModeManager.self.seasonModeData.TeamIndexMasterList[index]);
      string abbreviation = teamData.GetAbbreviation();
      for (int playerIndex = 0; playerIndex < teamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
      {
        PlayerData player = teamData.GetPlayer(playerIndex);
        if (player != null)
        {
          string initalAndLastName = player.FirstInitalAndLastName;
          PlayerStats totalCareerStats = player.TotalCareerStats;
          if (totalCareerStats != null)
            this.CheckForBrokenRecords_Individual(totalCareerStats, initalAndLastName, abbreviation, currentYear);
        }
      }
    }
  }

  private void CheckForBrokenRecords_Individual(
    PlayerStats playerStats,
    string playerName,
    string playerTeam,
    int year)
  {
    if (playerStats.QBAttempts > 0)
    {
      if ((double) playerStats.QBCompletions > (double) this.Completions.RecordValue)
        this.Completions.UpdateRecord(playerName, playerTeam, year, (float) playerStats.QBCompletions);
      if ((double) playerStats.QBAttempts > (double) this.PassAttempts.RecordValue)
        this.PassAttempts.UpdateRecord(playerName, playerTeam, year, (float) playerStats.QBAttempts);
      if ((double) playerStats.QBPassYards > (double) this.PassYards.RecordValue)
        this.PassYards.UpdateRecord(playerName, playerTeam, year, (float) playerStats.QBPassYards);
      if ((double) playerStats.QBPassTDs > (double) this.PassTDs.RecordValue)
        this.PassTDs.UpdateRecord(playerName, playerTeam, year, (float) playerStats.QBPassTDs);
      if ((double) playerStats.QBInts > (double) this.ThrownInts.RecordValue)
        this.ThrownInts.UpdateRecord(playerName, playerTeam, year, (float) playerStats.QBInts);
      if ((double) playerStats.QBLongestPass > (double) this.LongestPass.RecordValue)
        this.LongestPass.UpdateRecord(playerName, playerTeam, year, (float) playerStats.QBLongestPass);
      if (playerStats.QBAttempts >= 200)
      {
        if ((double) playerStats.GetQBRating() > (double) this.QBRating.RecordValue)
          this.QBRating.UpdateRecord(playerName, playerTeam, year, (float) playerStats.GetQBRating());
        if ((double) playerStats.QBCompletions / (double) playerStats.QBAttempts > (double) this.CompletionPercentage.RecordValue)
          this.CompletionPercentage.UpdateRecord(playerName, playerTeam, year, (float) playerStats.QBCompletions / (float) playerStats.QBAttempts);
        if ((double) playerStats.QBPassYards / (double) playerStats.QBAttempts > (double) this.YardsPerPass.RecordValue)
          this.YardsPerPass.UpdateRecord(playerName, playerTeam, year, (float) playerStats.QBPassYards / (float) playerStats.QBAttempts);
      }
    }
    if (playerStats.RushAttempts > 0)
    {
      if ((double) playerStats.RushAttempts > (double) this.RushAttempts.RecordValue)
        this.RushAttempts.UpdateRecord(playerName, playerTeam, year, (float) playerStats.RushAttempts);
      if ((double) playerStats.RushYards > (double) this.RushYards.RecordValue)
        this.RushYards.UpdateRecord(playerName, playerTeam, year, (float) playerStats.RushYards);
      if ((double) playerStats.RushTDs > (double) this.RushTDs.RecordValue)
        this.RushTDs.UpdateRecord(playerName, playerTeam, year, (float) playerStats.RushTDs);
      if ((double) playerStats.LongestRush > (double) this.LongestRush.RecordValue)
        this.LongestRush.UpdateRecord(playerName, playerTeam, year, (float) playerStats.LongestRush);
      if ((double) playerStats.Fumbles > (double) this.Fumbles.RecordValue)
        this.Fumbles.UpdateRecord(playerName, playerTeam, year, (float) playerStats.Fumbles);
      if (playerStats.RushAttempts >= 100 && (double) playerStats.RushYards / (double) playerStats.RushAttempts > (double) this.YardsPerRush.RecordValue)
        this.YardsPerRush.UpdateRecord(playerName, playerTeam, year, (float) playerStats.RushYards / (float) playerStats.RushAttempts);
    }
    if (playerStats.Receptions > 0)
    {
      if ((double) playerStats.Receptions > (double) this.Receptions.RecordValue)
        this.Receptions.UpdateRecord(playerName, playerTeam, year, (float) playerStats.Receptions);
      if ((double) playerStats.ReceivingYards > (double) this.ReceivingYards.RecordValue)
        this.ReceivingYards.UpdateRecord(playerName, playerTeam, year, (float) playerStats.ReceivingYards);
      if ((double) playerStats.ReceivingTDs > (double) this.ReceivingTDs.RecordValue)
        this.ReceivingTDs.UpdateRecord(playerName, playerTeam, year, (float) playerStats.ReceivingTDs);
      if ((double) playerStats.LongestReception > (double) this.LongestReception.RecordValue)
        this.LongestReception.UpdateRecord(playerName, playerTeam, year, (float) playerStats.LongestReception);
      if ((double) playerStats.YardsAfterCatch > (double) this.YardsAfterCatch.RecordValue)
        this.YardsAfterCatch.UpdateRecord(playerName, playerTeam, year, (float) playerStats.YardsAfterCatch);
      if ((double) playerStats.Drops > (double) this.Drops.RecordValue)
        this.Drops.UpdateRecord(playerName, playerTeam, year, (float) playerStats.Drops);
      if ((double) playerStats.Targets > (double) this.Targets.RecordValue)
        this.Targets.UpdateRecord(playerName, playerTeam, year, (float) playerStats.Targets);
      if (playerStats.Receptions >= 30 && (double) playerStats.ReceivingYards / (double) playerStats.Receptions > (double) this.YardsPerCatch.RecordValue)
        this.YardsPerCatch.UpdateRecord(playerName, playerTeam, year, (float) playerStats.ReceivingYards / (float) playerStats.Receptions);
    }
    if ((double) playerStats.TotalTDs > (double) this.TotalTDs.RecordValue)
      this.TotalTDs.UpdateRecord(playerName, playerTeam, year, (float) playerStats.TotalTDs);
    if ((double) playerStats.Tackles > (double) this.Tackles.RecordValue)
      this.Tackles.UpdateRecord(playerName, playerTeam, year, (float) playerStats.Tackles);
    if ((double) playerStats.Sacks > (double) this.Sacks.RecordValue)
      this.Sacks.UpdateRecord(playerName, playerTeam, year, (float) playerStats.Sacks);
    if ((double) playerStats.Interceptions > (double) this.Interceptions.RecordValue)
      this.Interceptions.UpdateRecord(playerName, playerTeam, year, (float) playerStats.Interceptions);
    if ((double) playerStats.TacklesForLoss > (double) this.TacklesForLoss.RecordValue)
      this.TacklesForLoss.UpdateRecord(playerName, playerTeam, year, (float) playerStats.TacklesForLoss);
    if ((double) playerStats.DefensiveTDs > (double) this.DefensiveTDs.RecordValue)
      this.DefensiveTDs.UpdateRecord(playerName, playerTeam, year, (float) playerStats.DefensiveTDs);
    if ((double) playerStats.KnockDowns > (double) this.KnockDowns.RecordValue)
      this.KnockDowns.UpdateRecord(playerName, playerTeam, year, (float) playerStats.KnockDowns);
    if ((double) playerStats.ForcedFumbles > (double) this.ForcedFumbles.RecordValue)
      this.ForcedFumbles.UpdateRecord(playerName, playerTeam, year, (float) playerStats.ForcedFumbles);
    if ((double) playerStats.FumbleRecoveries > (double) this.FumbleRecoveries.RecordValue)
      this.FumbleRecoveries.UpdateRecord(playerName, playerTeam, year, (float) playerStats.FumbleRecoveries);
    if ((double) playerStats.FGMade > (double) this.FGMade.RecordValue)
      this.FGMade.UpdateRecord(playerName, playerTeam, year, (float) playerStats.FGMade);
    if ((double) playerStats.FGAttempted > (double) this.FGAttempted.RecordValue)
      this.FGAttempted.UpdateRecord(playerName, playerTeam, year, (float) playerStats.FGAttempted);
    if ((double) playerStats.XPMade > (double) this.XPMade.RecordValue)
      this.XPMade.UpdateRecord(playerName, playerTeam, year, (float) playerStats.XPMade);
    if ((double) playerStats.XPAttempted > (double) this.XPAttempted.RecordValue)
      this.XPAttempted.UpdateRecord(playerName, playerTeam, year, (float) playerStats.XPAttempted);
    if (playerStats.Punts > 0)
    {
      if ((double) playerStats.Punts > (double) this.Punts.RecordValue)
        this.Punts.UpdateRecord(playerName, playerTeam, year, (float) playerStats.Punts);
      if ((double) playerStats.PuntsInside20 > (double) this.PuntsInside20.RecordValue)
        this.PuntsInside20.UpdateRecord(playerName, playerTeam, year, (float) playerStats.PuntsInside20);
      if ((double) playerStats.PuntTouchbacks > (double) this.PuntTouchbacks.RecordValue)
        this.PuntTouchbacks.UpdateRecord(playerName, playerTeam, year, (float) playerStats.PuntTouchbacks);
      if (playerStats.Punts >= 45 && (double) playerStats.PuntYards / (double) playerStats.Punts > (double) this.YardsPerPunt.RecordValue)
        this.YardsPerPunt.UpdateRecord(playerName, playerTeam, year, (float) playerStats.PuntYards / (float) playerStats.Punts);
    }
    if (playerStats.PuntReturns > 0)
    {
      if ((double) playerStats.PuntReturns > (double) this.PuntReturns.RecordValue)
        this.PuntReturns.UpdateRecord(playerName, playerTeam, year, (float) playerStats.PuntReturns);
      if ((double) playerStats.PuntReturnYards > (double) this.PuntReturnYards.RecordValue)
        this.PuntReturnYards.UpdateRecord(playerName, playerTeam, year, (float) playerStats.PuntReturnYards);
      if ((double) playerStats.PuntReturnTDs > (double) this.PuntReturnTDs.RecordValue)
        this.PuntReturnTDs.UpdateRecord(playerName, playerTeam, year, (float) playerStats.PuntReturnTDs);
      if (playerStats.PuntReturns >= 20 && (double) playerStats.PuntReturnYards / (double) playerStats.PuntReturns > (double) this.YardsPerPuntReturn.RecordValue)
        this.YardsPerPuntReturn.UpdateRecord(playerName, playerTeam, year, (float) playerStats.PuntReturnYards / (float) playerStats.PuntReturns);
    }
    if (playerStats.KickReturns <= 0)
      return;
    if ((double) playerStats.KickReturns > (double) this.KickReturns.RecordValue)
      this.KickReturns.UpdateRecord(playerName, playerTeam, year, (float) playerStats.KickReturns);
    if ((double) playerStats.KickReturnYards > (double) this.KickReturnYards.RecordValue)
      this.KickReturnYards.UpdateRecord(playerName, playerTeam, year, (float) playerStats.KickReturnYards);
    if ((double) playerStats.KickReturnTDs > (double) this.KickReturnTDs.RecordValue)
      this.KickReturnTDs.UpdateRecord(playerName, playerTeam, year, (float) playerStats.KickReturnTDs);
    if (playerStats.KickReturns < 20 || (double) playerStats.KickReturnYards / (double) playerStats.KickReturns <= (double) this.YardsPerKickReturn.RecordValue)
      return;
    this.YardsPerKickReturn.UpdateRecord(playerName, playerTeam, year, (float) playerStats.KickReturnYards / (float) playerStats.KickReturns);
  }
}
