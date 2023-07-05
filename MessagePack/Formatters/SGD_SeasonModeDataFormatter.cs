// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SGD_SeasonModeDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class SGD_SeasonModeDataFormatter : 
    IMessagePackFormatter<SGD_SeasonModeData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SGD_SeasonModeData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(117);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.NumberOfTeamsInLeague);
        writer.Write(value.NumberOfWeeksInSeason);
        writer.Write(value.MaxNumberOfGamesPerWeek);
        writer.Write(value.UsesTierSystem);
        writer.Write(value.NumberOfConferences);
        writer.Write(value.NumberOfTeamsPerConference);
        writer.Write(value.NumberOfDivisions);
        writer.Write(value.NumberOfTeamsPerDivision);
        writer.Write(value.UsersConference);
        writer.Write(value.UsersDivision);
        writer.Write(value.NumberOfWeeksInPlayoffs);
        writer.WriteNil();
        writer.WriteNil();
        writer.WriteNil();
        writer.Write(value.seasonNumber);
        writer.Write(value.UserTeamIndex);
        writer.WriteNil();
        writer.Write(value.UnlimitedFunds);
        writer.Write(value.allTimeWins);
        writer.Write(value.allTimeLosses);
        writer.Write(value.seasonsInTier1);
        writer.Write(value.seasonsInTier2);
        writer.Write(value.seasonsInTier3);
        writer.Write(value.tier1Championships);
        writer.Write(value.tier2Championships);
        writer.Write(value.tier3Championships);
        writer.Write(value.axisBowls);
        writer.Write(value.bestSeason_Wins);
        writer.Write(value.bestSeason_Losses);
        writer.Write(value.timesInPlayoffs);
        writer.Write(value.largestWin);
        writer.Write(value.mostPassYards);
        writer.Write(value.mostRushYards);
        writer.Write(value.mostTotalYards);
        writer.Write(value.lowestPassYards);
        writer.Write(value.lowestRushYards);
        writer.Write(value.lowestTotalYards);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.previousWeekTeamRanks, options);
        writer.Write(value.seasonWins);
        writer.Write(value.seasonLosses);
        writer.Write(value.playerProgressionDone);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.endOfSeasonText, options);
        resolver.GetFormatterWithVerify<ProEraSeasonState>().Serialize(ref writer, value.seasonState, options);
        writer.Write(value.waitingOnDemotionGame);
        writer.Write(value.userReceivesFirstRoundBye);
        writer.Write(value.seasonOverForUser);
        resolver.GetFormatterWithVerify<int[,]>().Serialize(ref writer, value.leagueSchedule, options);
        resolver.GetFormatterWithVerify<int[,]>().Serialize(ref writer, value.schedule_tier1, options);
        resolver.GetFormatterWithVerify<int[,]>().Serialize(ref writer, value.schedule_tier2, options);
        resolver.GetFormatterWithVerify<int[,]>().Serialize(ref writer, value.schedule_tier3, options);
        resolver.GetFormatterWithVerify<GameSummary[,]>().Serialize(ref writer, value.scoresByWeek_tier1, options);
        resolver.GetFormatterWithVerify<GameSummary[,]>().Serialize(ref writer, value.scoresByWeek_tier2, options);
        resolver.GetFormatterWithVerify<GameSummary[,]>().Serialize(ref writer, value.scoresByWeek_tier3, options);
        resolver.GetFormatterWithVerify<GameSummary[,]>().Serialize(ref writer, value.scoresByWeek_League, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffR1_tier1, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffR2_tier1, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffR3_tier1, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffR1_tier2, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffR2_tier2, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffR3_tier2, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffR1_tier3, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffR2_tier3, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffR3_tier3, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffsR1_league, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffsR2_league, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffsR3_league, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.playoffsR4_league, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR1_tier1, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR1_tier2, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR1_tier3, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR2_tier1, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR2_tier2, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR2_tier3, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR3_tier1, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR3_tier2, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR3_tier3, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR1_league, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR2_league, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR3_league, options);
        resolver.GetFormatterWithVerify<GameSummary[]>().Serialize(ref writer, value.scoresPlayoffR4_league, options);
        resolver.GetFormatterWithVerify<List<int[]>>().Serialize(ref writer, value.TeamsInConferences, options);
        resolver.GetFormatterWithVerify<Dictionary<string, int[]>>().Serialize(ref writer, value.TeamsInConferencesByTeamIndex, options);
        writer.Write(value.champion_tier1);
        writer.Write(value.champion_tier2);
        writer.Write(value.champion_tier3);
        writer.Write(value.leagueChampion);
        writer.Write(value.winner_tier12Game);
        writer.Write(value.winner_tier23Game);
        writer.Write(value.currentWeek);
        resolver.GetFormatterWithVerify<Award[]>().Serialize(ref writer, value.offPlayersOfTheWeek, options);
        resolver.GetFormatterWithVerify<Award[]>().Serialize(ref writer, value.defPlayersOfTheWeek, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.offPlayerOfTheYear, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.defPlayerOfTheYear, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.rookieOfTheYear, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.mvp, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.quarterbackOfTheYear, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.runningBackOfTheYear, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.receiverOfTheYear, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.defensiveLinemanOfTheYear, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.linebackerOfTheYear, options);
        resolver.GetFormatterWithVerify<Award>().Serialize(ref writer, value.defensiveBackOfTheYear, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.TeamIndexMasterList, options);
        resolver.GetFormatterWithVerify<Dictionary<string, int>>().Serialize(ref writer, value.teamAbbrevMap, options);
        resolver.GetFormatterWithVerify<Dictionary<string, TeamData>>().Serialize(ref writer, value.teamsInFranchise, options);
        resolver.GetFormatterWithVerify<RecordHolderGroup>().Serialize(ref writer, value.TeamSeasonRecords_Franchise, options);
        resolver.GetFormatterWithVerify<RecordHolderGroup>().Serialize(ref writer, value.IndividualGameRecords_Franchise, options);
        resolver.GetFormatterWithVerify<RecordHolderGroup>().Serialize(ref writer, value.IndividualSeasonRecords_Franchise, options);
        resolver.GetFormatterWithVerify<RecordHolderGroup>().Serialize(ref writer, value.IndividualCareerRecords_Franchise, options);
        resolver.GetFormatterWithVerify<RecordHolderGroup>().Serialize(ref writer, value.TeamSeasonRecords_League, options);
        resolver.GetFormatterWithVerify<RecordHolderGroup>().Serialize(ref writer, value.TeamGameRecords_League, options);
        resolver.GetFormatterWithVerify<RecordHolderGroup>().Serialize(ref writer, value.IndividualGameRecords_League, options);
        resolver.GetFormatterWithVerify<RecordHolderGroup>().Serialize(ref writer, value.IndividualSeasonRecords_League, options);
        resolver.GetFormatterWithVerify<RecordHolderGroup>().Serialize(ref writer, value.IndividualCareerRecords_League, options);
        writer.Write(value.quarterPref);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.difficultyPref, options);
        resolver.GetFormatterWithVerify<UserCareerStats>().Serialize(ref writer, value.CareerStats, options);
      }
    }

    public SGD_SeasonModeData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SGD_SeasonModeData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      SGD_SeasonModeData sgdSeasonModeData = new SGD_SeasonModeData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            sgdSeasonModeData.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            sgdSeasonModeData.NumberOfTeamsInLeague = reader.ReadInt32();
            break;
          case 2:
            sgdSeasonModeData.NumberOfWeeksInSeason = reader.ReadInt32();
            break;
          case 3:
            sgdSeasonModeData.MaxNumberOfGamesPerWeek = reader.ReadInt32();
            break;
          case 4:
            sgdSeasonModeData.UsesTierSystem = reader.ReadBoolean();
            break;
          case 5:
            sgdSeasonModeData.NumberOfConferences = reader.ReadInt32();
            break;
          case 6:
            sgdSeasonModeData.NumberOfTeamsPerConference = reader.ReadInt32();
            break;
          case 7:
            sgdSeasonModeData.NumberOfDivisions = reader.ReadInt32();
            break;
          case 8:
            sgdSeasonModeData.NumberOfTeamsPerDivision = reader.ReadInt32();
            break;
          case 9:
            sgdSeasonModeData.UsersConference = reader.ReadInt32();
            break;
          case 10:
            sgdSeasonModeData.UsersDivision = reader.ReadInt32();
            break;
          case 11:
            sgdSeasonModeData.NumberOfWeeksInPlayoffs = reader.ReadInt32();
            break;
          case 15:
            sgdSeasonModeData.seasonNumber = reader.ReadInt32();
            break;
          case 16:
            sgdSeasonModeData.UserTeamIndex = reader.ReadInt32();
            break;
          case 18:
            sgdSeasonModeData.UnlimitedFunds = reader.ReadBoolean();
            break;
          case 19:
            sgdSeasonModeData.allTimeWins = reader.ReadInt32();
            break;
          case 20:
            sgdSeasonModeData.allTimeLosses = reader.ReadInt32();
            break;
          case 21:
            sgdSeasonModeData.seasonsInTier1 = reader.ReadInt32();
            break;
          case 22:
            sgdSeasonModeData.seasonsInTier2 = reader.ReadInt32();
            break;
          case 23:
            sgdSeasonModeData.seasonsInTier3 = reader.ReadInt32();
            break;
          case 24:
            sgdSeasonModeData.tier1Championships = reader.ReadInt32();
            break;
          case 25:
            sgdSeasonModeData.tier2Championships = reader.ReadInt32();
            break;
          case 26:
            sgdSeasonModeData.tier3Championships = reader.ReadInt32();
            break;
          case 27:
            sgdSeasonModeData.axisBowls = reader.ReadInt32();
            break;
          case 28:
            sgdSeasonModeData.bestSeason_Wins = reader.ReadInt32();
            break;
          case 29:
            sgdSeasonModeData.bestSeason_Losses = reader.ReadInt32();
            break;
          case 30:
            sgdSeasonModeData.timesInPlayoffs = reader.ReadInt32();
            break;
          case 31:
            sgdSeasonModeData.largestWin = reader.ReadInt32();
            break;
          case 32:
            sgdSeasonModeData.mostPassYards = reader.ReadInt32();
            break;
          case 33:
            sgdSeasonModeData.mostRushYards = reader.ReadInt32();
            break;
          case 34:
            sgdSeasonModeData.mostTotalYards = reader.ReadInt32();
            break;
          case 35:
            sgdSeasonModeData.lowestPassYards = reader.ReadInt32();
            break;
          case 36:
            sgdSeasonModeData.lowestRushYards = reader.ReadInt32();
            break;
          case 37:
            sgdSeasonModeData.lowestTotalYards = reader.ReadInt32();
            break;
          case 38:
            sgdSeasonModeData.previousWeekTeamRanks = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 39:
            sgdSeasonModeData.seasonWins = reader.ReadInt32();
            break;
          case 40:
            sgdSeasonModeData.seasonLosses = reader.ReadInt32();
            break;
          case 41:
            sgdSeasonModeData.playerProgressionDone = reader.ReadBoolean();
            break;
          case 42:
            sgdSeasonModeData.endOfSeasonText = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 43:
            sgdSeasonModeData.seasonState = resolver.GetFormatterWithVerify<ProEraSeasonState>().Deserialize(ref reader, options);
            break;
          case 44:
            sgdSeasonModeData.waitingOnDemotionGame = reader.ReadBoolean();
            break;
          case 45:
            sgdSeasonModeData.userReceivesFirstRoundBye = reader.ReadBoolean();
            break;
          case 46:
            sgdSeasonModeData.seasonOverForUser = reader.ReadBoolean();
            break;
          case 47:
            sgdSeasonModeData.leagueSchedule = resolver.GetFormatterWithVerify<int[,]>().Deserialize(ref reader, options);
            break;
          case 48:
            sgdSeasonModeData.schedule_tier1 = resolver.GetFormatterWithVerify<int[,]>().Deserialize(ref reader, options);
            break;
          case 49:
            sgdSeasonModeData.schedule_tier2 = resolver.GetFormatterWithVerify<int[,]>().Deserialize(ref reader, options);
            break;
          case 50:
            sgdSeasonModeData.schedule_tier3 = resolver.GetFormatterWithVerify<int[,]>().Deserialize(ref reader, options);
            break;
          case 51:
            sgdSeasonModeData.scoresByWeek_tier1 = resolver.GetFormatterWithVerify<GameSummary[,]>().Deserialize(ref reader, options);
            break;
          case 52:
            sgdSeasonModeData.scoresByWeek_tier2 = resolver.GetFormatterWithVerify<GameSummary[,]>().Deserialize(ref reader, options);
            break;
          case 53:
            sgdSeasonModeData.scoresByWeek_tier3 = resolver.GetFormatterWithVerify<GameSummary[,]>().Deserialize(ref reader, options);
            break;
          case 54:
            sgdSeasonModeData.scoresByWeek_League = resolver.GetFormatterWithVerify<GameSummary[,]>().Deserialize(ref reader, options);
            break;
          case 55:
            sgdSeasonModeData.playoffR1_tier1 = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 56:
            sgdSeasonModeData.playoffR2_tier1 = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 57:
            sgdSeasonModeData.playoffR3_tier1 = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 58:
            sgdSeasonModeData.playoffR1_tier2 = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 59:
            sgdSeasonModeData.playoffR2_tier2 = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 60:
            sgdSeasonModeData.playoffR3_tier2 = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 61:
            sgdSeasonModeData.playoffR1_tier3 = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 62:
            sgdSeasonModeData.playoffR2_tier3 = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 63:
            sgdSeasonModeData.playoffR3_tier3 = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 64:
            sgdSeasonModeData.playoffsR1_league = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 65:
            sgdSeasonModeData.playoffsR2_league = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 66:
            sgdSeasonModeData.playoffsR3_league = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 67:
            sgdSeasonModeData.playoffsR4_league = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 68:
            sgdSeasonModeData.scoresPlayoffR1_tier1 = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 69:
            sgdSeasonModeData.scoresPlayoffR1_tier2 = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 70:
            sgdSeasonModeData.scoresPlayoffR1_tier3 = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 71:
            sgdSeasonModeData.scoresPlayoffR2_tier1 = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 72:
            sgdSeasonModeData.scoresPlayoffR2_tier2 = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 73:
            sgdSeasonModeData.scoresPlayoffR2_tier3 = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 74:
            sgdSeasonModeData.scoresPlayoffR3_tier1 = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 75:
            sgdSeasonModeData.scoresPlayoffR3_tier2 = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 76:
            sgdSeasonModeData.scoresPlayoffR3_tier3 = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 77:
            sgdSeasonModeData.scoresPlayoffR1_league = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 78:
            sgdSeasonModeData.scoresPlayoffR2_league = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 79:
            sgdSeasonModeData.scoresPlayoffR3_league = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 80:
            sgdSeasonModeData.scoresPlayoffR4_league = resolver.GetFormatterWithVerify<GameSummary[]>().Deserialize(ref reader, options);
            break;
          case 81:
            sgdSeasonModeData.TeamsInConferences = resolver.GetFormatterWithVerify<List<int[]>>().Deserialize(ref reader, options);
            break;
          case 82:
            sgdSeasonModeData.TeamsInConferencesByTeamIndex = resolver.GetFormatterWithVerify<Dictionary<string, int[]>>().Deserialize(ref reader, options);
            break;
          case 83:
            sgdSeasonModeData.champion_tier1 = reader.ReadInt32();
            break;
          case 84:
            sgdSeasonModeData.champion_tier2 = reader.ReadInt32();
            break;
          case 85:
            sgdSeasonModeData.champion_tier3 = reader.ReadInt32();
            break;
          case 86:
            sgdSeasonModeData.leagueChampion = reader.ReadInt32();
            break;
          case 87:
            sgdSeasonModeData.winner_tier12Game = reader.ReadInt32();
            break;
          case 88:
            sgdSeasonModeData.winner_tier23Game = reader.ReadInt32();
            break;
          case 89:
            sgdSeasonModeData.currentWeek = reader.ReadInt32();
            break;
          case 90:
            sgdSeasonModeData.offPlayersOfTheWeek = resolver.GetFormatterWithVerify<Award[]>().Deserialize(ref reader, options);
            break;
          case 91:
            sgdSeasonModeData.defPlayersOfTheWeek = resolver.GetFormatterWithVerify<Award[]>().Deserialize(ref reader, options);
            break;
          case 92:
            sgdSeasonModeData.offPlayerOfTheYear = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 93:
            sgdSeasonModeData.defPlayerOfTheYear = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 94:
            sgdSeasonModeData.rookieOfTheYear = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 95:
            sgdSeasonModeData.mvp = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 96:
            sgdSeasonModeData.quarterbackOfTheYear = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 97:
            sgdSeasonModeData.runningBackOfTheYear = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 98:
            sgdSeasonModeData.receiverOfTheYear = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 99:
            sgdSeasonModeData.defensiveLinemanOfTheYear = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 100:
            sgdSeasonModeData.linebackerOfTheYear = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 101:
            sgdSeasonModeData.defensiveBackOfTheYear = resolver.GetFormatterWithVerify<Award>().Deserialize(ref reader, options);
            break;
          case 102:
            sgdSeasonModeData.TeamIndexMasterList = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 103:
            sgdSeasonModeData.teamAbbrevMap = resolver.GetFormatterWithVerify<Dictionary<string, int>>().Deserialize(ref reader, options);
            break;
          case 104:
            sgdSeasonModeData.teamsInFranchise = resolver.GetFormatterWithVerify<Dictionary<string, TeamData>>().Deserialize(ref reader, options);
            break;
          case 105:
            sgdSeasonModeData.TeamSeasonRecords_Franchise = resolver.GetFormatterWithVerify<RecordHolderGroup>().Deserialize(ref reader, options);
            break;
          case 106:
            sgdSeasonModeData.IndividualGameRecords_Franchise = resolver.GetFormatterWithVerify<RecordHolderGroup>().Deserialize(ref reader, options);
            break;
          case 107:
            sgdSeasonModeData.IndividualSeasonRecords_Franchise = resolver.GetFormatterWithVerify<RecordHolderGroup>().Deserialize(ref reader, options);
            break;
          case 108:
            sgdSeasonModeData.IndividualCareerRecords_Franchise = resolver.GetFormatterWithVerify<RecordHolderGroup>().Deserialize(ref reader, options);
            break;
          case 109:
            sgdSeasonModeData.TeamSeasonRecords_League = resolver.GetFormatterWithVerify<RecordHolderGroup>().Deserialize(ref reader, options);
            break;
          case 110:
            sgdSeasonModeData.TeamGameRecords_League = resolver.GetFormatterWithVerify<RecordHolderGroup>().Deserialize(ref reader, options);
            break;
          case 111:
            sgdSeasonModeData.IndividualGameRecords_League = resolver.GetFormatterWithVerify<RecordHolderGroup>().Deserialize(ref reader, options);
            break;
          case 112:
            sgdSeasonModeData.IndividualSeasonRecords_League = resolver.GetFormatterWithVerify<RecordHolderGroup>().Deserialize(ref reader, options);
            break;
          case 113:
            sgdSeasonModeData.IndividualCareerRecords_League = resolver.GetFormatterWithVerify<RecordHolderGroup>().Deserialize(ref reader, options);
            break;
          case 114:
            sgdSeasonModeData.quarterPref = reader.ReadInt32();
            break;
          case 115:
            sgdSeasonModeData.difficultyPref = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 116:
            sgdSeasonModeData.CareerStats = resolver.GetFormatterWithVerify<UserCareerStats>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return sgdSeasonModeData;
    }
  }
}
