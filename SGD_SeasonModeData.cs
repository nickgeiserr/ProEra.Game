// Decompiled with JetBrains decompiler
// Type: SGD_SeasonModeData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class SGD_SeasonModeData : ISaveSync
{
  [IgnoreMember]
  public static string FileName = "SaveSeasonMode";
  [IgnoreMember]
  private bool isDirty;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public int NumberOfTeamsInLeague;
  [Key(2)]
  public int NumberOfWeeksInSeason;
  [Key(3)]
  public int MaxNumberOfGamesPerWeek;
  [Key(4)]
  public bool UsesTierSystem;
  [Key(5)]
  public int NumberOfConferences;
  [Key(6)]
  public int NumberOfTeamsPerConference;
  [Key(7)]
  public int NumberOfDivisions;
  [Key(8)]
  public int NumberOfTeamsPerDivision;
  [Key(9)]
  public int UsersConference;
  [Key(10)]
  public int UsersDivision;
  [Key(11)]
  public int NumberOfWeeksInPlayoffs;
  [Key(12)]
  private TeamConferenceData[] teamConferenceAssignments;
  [Key(13)]
  private string[] conferenceNames = new string[2]
  {
    "AFC",
    "NFC"
  };
  [Key(14)]
  private string[] divisionNames = new string[4]
  {
    "NORTH",
    "SOUTH",
    "EAST",
    "WEST"
  };
  [Key(15)]
  public int seasonNumber;
  [Key(16)]
  public int UserTeamIndex = 2;
  [Key(17)]
  private int availableFunds;
  [Key(18)]
  public bool UnlimitedFunds;
  [Key(19)]
  public int allTimeWins;
  [Key(20)]
  public int allTimeLosses;
  [Key(21)]
  public int seasonsInTier1;
  [Key(22)]
  public int seasonsInTier2;
  [Key(23)]
  public int seasonsInTier3;
  [Key(24)]
  public int tier1Championships;
  [Key(25)]
  public int tier2Championships;
  [Key(26)]
  public int tier3Championships;
  [Key(27)]
  public int axisBowls;
  [Key(28)]
  public int bestSeason_Wins;
  [Key(29)]
  public int bestSeason_Losses;
  [Key(30)]
  public int timesInPlayoffs;
  [Key(31)]
  public int largestWin;
  [Key(32)]
  public int mostPassYards;
  [Key(33)]
  public int mostRushYards;
  [Key(34)]
  public int mostTotalYards;
  [Key(35)]
  public int lowestPassYards;
  [Key(36)]
  public int lowestRushYards;
  [Key(37)]
  public int lowestTotalYards;
  [Key(38)]
  public int[] previousWeekTeamRanks;
  [Key(39)]
  public int seasonWins;
  [Key(40)]
  public int seasonLosses;
  [Key(41)]
  public bool playerProgressionDone;
  [Key(42)]
  public string endOfSeasonText;
  [Key(43)]
  public ProEraSeasonState seasonState;
  [Key(44)]
  public bool waitingOnDemotionGame;
  [Key(45)]
  public bool userReceivesFirstRoundBye;
  [Key(46)]
  public bool seasonOverForUser;
  [Key(47)]
  public int[,] leagueSchedule;
  [Key(48)]
  public int[,] schedule_tier1;
  [Key(49)]
  public int[,] schedule_tier2;
  [Key(50)]
  public int[,] schedule_tier3;
  [Key(51)]
  public GameSummary[,] scoresByWeek_tier1;
  [Key(52)]
  public GameSummary[,] scoresByWeek_tier2;
  [Key(53)]
  public GameSummary[,] scoresByWeek_tier3;
  [Key(54)]
  public GameSummary[,] scoresByWeek_League;
  [Key(55)]
  public int[] playoffR1_tier1;
  [Key(56)]
  public int[] playoffR2_tier1;
  [Key(57)]
  public int[] playoffR3_tier1;
  [Key(58)]
  public int[] playoffR1_tier2;
  [Key(59)]
  public int[] playoffR2_tier2;
  [Key(60)]
  public int[] playoffR3_tier2;
  [Key(61)]
  public int[] playoffR1_tier3;
  [Key(62)]
  public int[] playoffR2_tier3;
  [Key(63)]
  public int[] playoffR3_tier3;
  [Key(64)]
  public int[] playoffsR1_league;
  [Key(65)]
  public int[] playoffsR2_league;
  [Key(66)]
  public int[] playoffsR3_league;
  [Key(67)]
  public int[] playoffsR4_league;
  [Key(68)]
  public GameSummary[] scoresPlayoffR1_tier1;
  [Key(69)]
  public GameSummary[] scoresPlayoffR1_tier2;
  [Key(70)]
  public GameSummary[] scoresPlayoffR1_tier3;
  [Key(71)]
  public GameSummary[] scoresPlayoffR2_tier1;
  [Key(72)]
  public GameSummary[] scoresPlayoffR2_tier2;
  [Key(73)]
  public GameSummary[] scoresPlayoffR2_tier3;
  [Key(74)]
  public GameSummary[] scoresPlayoffR3_tier1;
  [Key(75)]
  public GameSummary[] scoresPlayoffR3_tier2;
  [Key(76)]
  public GameSummary[] scoresPlayoffR3_tier3;
  [Key(77)]
  public GameSummary[] scoresPlayoffR1_league;
  [Key(78)]
  public GameSummary[] scoresPlayoffR2_league;
  [Key(79)]
  public GameSummary[] scoresPlayoffR3_league;
  [Key(80)]
  public GameSummary[] scoresPlayoffR4_league;
  [Key(81)]
  public List<int[]> TeamsInConferences;
  [Key(82)]
  public Dictionary<string, int[]> TeamsInConferencesByTeamIndex;
  [Key(83)]
  public int champion_tier1;
  [Key(84)]
  public int champion_tier2;
  [Key(85)]
  public int champion_tier3;
  [Key(86)]
  public int leagueChampion;
  [Key(87)]
  public int winner_tier12Game;
  [Key(88)]
  public int winner_tier23Game;
  [Key(89)]
  public int currentWeek;
  [Key(90)]
  public Award[] offPlayersOfTheWeek;
  [Key(91)]
  public Award[] defPlayersOfTheWeek;
  [Key(92)]
  public Award offPlayerOfTheYear;
  [Key(93)]
  public Award defPlayerOfTheYear;
  [Key(94)]
  public Award rookieOfTheYear;
  [Key(95)]
  public Award mvp;
  [Key(96)]
  public Award quarterbackOfTheYear;
  [Key(97)]
  public Award runningBackOfTheYear;
  [Key(98)]
  public Award receiverOfTheYear;
  [Key(99)]
  public Award defensiveLinemanOfTheYear;
  [Key(100)]
  public Award linebackerOfTheYear;
  [Key(101)]
  public Award defensiveBackOfTheYear;
  [Key(102)]
  public int[] TeamIndexMasterList;
  [Key(103)]
  public Dictionary<string, int> teamAbbrevMap;
  [Key(104)]
  public Dictionary<string, TeamData> teamsInFranchise;
  [Key(105)]
  public RecordHolderGroup TeamSeasonRecords_Franchise;
  [Key(106)]
  public RecordHolderGroup IndividualGameRecords_Franchise;
  [Key(107)]
  public RecordHolderGroup IndividualSeasonRecords_Franchise;
  [Key(108)]
  public RecordHolderGroup IndividualCareerRecords_Franchise;
  [Key(109)]
  public RecordHolderGroup TeamSeasonRecords_League;
  [Key(110)]
  public RecordHolderGroup TeamGameRecords_League;
  [Key(111)]
  public RecordHolderGroup IndividualGameRecords_League;
  [Key(112)]
  public RecordHolderGroup IndividualSeasonRecords_League;
  [Key(113)]
  public RecordHolderGroup IndividualCareerRecords_League;
  [Key(114)]
  public int quarterPref;
  [Key(115)]
  public string difficultyPref;
  [Key(116)]
  [Obsolete]
  public UserCareerStats CareerStats;

  public void SetupLeague(int _userTeamIndex)
  {
    this.UsesTierSystem = false;
    this.UnlimitedFunds = false;
    this.UserTeamIndex = _userTeamIndex;
    this.NumberOfConferences = 2;
    this.NumberOfDivisions = 4;
    this.NumberOfWeeksInPlayoffs = 4;
    this.NumberOfTeamsInLeague = 32;
    this.NumberOfTeamsPerConference = this.NumberOfTeamsInLeague / this.NumberOfConferences;
    this.NumberOfTeamsPerDivision = this.NumberOfTeamsPerConference / this.NumberOfDivisions;
    this.NumberOfWeeksInSeason = 18;
    this.MaxNumberOfGamesPerWeek = 17;
    this.teamConferenceAssignments = TeamResourcesManager.LoadTeamConferenceAssignment();
    this.seasonNumber = 0;
    this.difficultyPref = "PRO";
    this.quarterPref = 1;
    this.seasonWins = 0;
    this.seasonLosses = 0;
    this.allTimeWins = 0;
    this.allTimeLosses = 0;
    this.seasonsInTier1 = 0;
    this.seasonsInTier2 = 0;
    this.seasonsInTier3 = 0;
    this.tier1Championships = 0;
    this.tier2Championships = 0;
    this.tier3Championships = 0;
    this.axisBowls = 0;
    this.bestSeason_Wins = 0;
    this.bestSeason_Losses = 0;
    this.largestWin = 0;
    this.timesInPlayoffs = 0;
    this.mostPassYards = 0;
    this.mostRushYards = 0;
    this.mostTotalYards = 0;
    this.lowestPassYards = 999;
    this.lowestRushYards = 999;
    this.lowestTotalYards = 999;
    this.userReceivesFirstRoundBye = false;
    this.seasonOverForUser = false;
    this.previousWeekTeamRanks = new int[6];
    this.TeamSeasonRecords_Franchise = new RecordHolderGroup();
    this.IndividualGameRecords_Franchise = new RecordHolderGroup();
    this.IndividualSeasonRecords_Franchise = new RecordHolderGroup();
    this.IndividualCareerRecords_Franchise = new RecordHolderGroup();
    this.TeamSeasonRecords_League = new RecordHolderGroup();
    this.TeamGameRecords_League = new RecordHolderGroup();
    this.IndividualGameRecords_League = new RecordHolderGroup();
    this.IndividualSeasonRecords_League = new RecordHolderGroup();
    this.IndividualCareerRecords_League = new RecordHolderGroup();
    this.teamAbbrevMap = new Dictionary<string, int>();
  }

  public void ResetAwards()
  {
    this.offPlayersOfTheWeek = new Award[this.NumberOfWeeksInSeason];
    this.defPlayersOfTheWeek = new Award[this.NumberOfWeeksInSeason];
    this.offPlayerOfTheYear = (Award) null;
    this.defPlayerOfTheYear = (Award) null;
    this.rookieOfTheYear = (Award) null;
    this.mvp = (Award) null;
    this.quarterbackOfTheYear = (Award) null;
    this.runningBackOfTheYear = (Award) null;
    this.receiverOfTheYear = (Award) null;
    this.defensiveLinemanOfTheYear = (Award) null;
    this.linebackerOfTheYear = (Award) null;
    this.defensiveBackOfTheYear = (Award) null;
  }

  public void SetEndOfYearText(string s) => this.endOfSeasonText = s;

  public int[] GetPlayoffScheduleByTier_R1(int _tier)
  {
    if (!this.UsesTierSystem)
      return this.playoffsR1_league;
    if (_tier == 1)
      return this.playoffR1_tier1;
    return _tier == 2 ? this.playoffR1_tier2 : this.playoffR1_tier3;
  }

  public int[] GetPlayoffScheduleByTier_R2(int _tier)
  {
    if (!this.UsesTierSystem)
      return this.playoffsR2_league;
    if (_tier == 1)
      return this.playoffR2_tier1;
    return _tier == 2 ? this.playoffR2_tier2 : this.playoffR2_tier3;
  }

  public int[] GetPlayoffScheduleByTier_R3(int _tier)
  {
    if (!this.UsesTierSystem)
      return this.playoffsR3_league;
    if (_tier == 1)
      return this.playoffR3_tier1;
    return _tier == 2 ? this.playoffR3_tier2 : this.playoffR3_tier3;
  }

  public int[] GetPlayoffSchedule_R4() => this.playoffsR4_league;

  public GameSummary[] GetPlayoffScoresByTier_R1(int _tier)
  {
    if (!this.UsesTierSystem)
      return this.scoresPlayoffR1_league;
    if (_tier == 1)
      return this.scoresPlayoffR1_tier1;
    return _tier == 2 ? this.scoresPlayoffR1_tier2 : this.scoresPlayoffR1_tier3;
  }

  public GameSummary[] GetPlayoffScoresByTier_R2(int _tier)
  {
    if (!this.UsesTierSystem)
      return this.scoresPlayoffR2_league;
    if (_tier == 1)
      return this.scoresPlayoffR2_tier1;
    return _tier == 2 ? this.scoresPlayoffR2_tier2 : this.scoresPlayoffR2_tier3;
  }

  public GameSummary[] GetPlayoffScoresByTier_R3(int _tier)
  {
    if (!this.UsesTierSystem)
      return this.scoresPlayoffR3_league;
    if (_tier == 1)
      return this.scoresPlayoffR3_tier1;
    return _tier == 2 ? this.scoresPlayoffR3_tier2 : this.scoresPlayoffR3_tier3;
  }

  public GameSummary[] GetPlayoffScoresByWeek(int playoffWeek)
  {
    switch (playoffWeek)
    {
      case 0:
        return this.scoresPlayoffR1_league;
      case 1:
        return this.scoresPlayoffR2_league;
      case 2:
        return this.scoresPlayoffR3_league;
      case 3:
        return this.scoresPlayoffR4_league;
      default:
        return (GameSummary[]) null;
    }
  }

  public int[] GetPlayoffScheduleByWeek(int playoffWeek)
  {
    switch (playoffWeek)
    {
      case 0:
        return this.playoffsR1_league;
      case 1:
        return this.playoffsR2_league;
      case 2:
        return this.playoffsR3_league;
      case 3:
        return this.playoffsR4_league;
      default:
        return (int[]) null;
    }
  }

  public GameSummary[] GetPlayoffScores_R4() => this.scoresPlayoffR4_league;

  public int[,] GetScheduleByTier(int _tier = 0) => this.leagueSchedule;

  public GameSummary[,] GetScoresByWeek(int _tier)
  {
    if (!this.UsesTierSystem)
      return this.scoresByWeek_League;
    if (_tier == 1)
      return this.scoresByWeek_tier1;
    return _tier == 2 ? this.scoresByWeek_tier2 : this.scoresByWeek_tier3;
  }

  public string GetConferenceName(int conferenceIndex) => conferenceIndex >= 0 && conferenceIndex < this.conferenceNames.Length ? this.conferenceNames[conferenceIndex] : "";

  public string GetDivisionName(int divisionIndex) => divisionIndex >= 0 && divisionIndex < this.divisionNames.Length ? this.divisionNames[divisionIndex] : "";

  public int[] GetTeamsInConference(int conference) => this.TeamsInConferences[conference];

  public void AssignTeamsToConferences(bool randomize)
  {
    this.TeamsInConferences = new List<int[]>();
    this.TeamsInConferencesByTeamIndex = new Dictionary<string, int[]>();
    this.TeamsInConferences.Add((int[]) null);
    for (int index = 0; index < this.NumberOfConferences; ++index)
      this.TeamsInConferences.Add(new int[this.NumberOfTeamsPerConference]);
    List<int> intList = new List<int>(this.NumberOfTeamsInLeague);
    for (int index1 = 0; index1 < this.NumberOfTeamsInLeague; ++index1)
    {
      intList.Add(index1);
      int conference = (int) this.teamConferenceAssignments[index1].conference;
      int index2 = (int) this.teamConferenceAssignments[index1].division * this.NumberOfTeamsPerDivision + this.teamConferenceAssignments[index1].numInDivision;
      int index3 = this.teamConferenceAssignments[index1].teamMap.index;
      this.TeamsInConferences[conference + 1][index2] = this.teamConferenceAssignments[index1].teamMap.index;
      this.TeamsInConferencesByTeamIndex.Add(index3.ToString(), new int[2]
      {
        conference,
        index2
      });
    }
    bool flag = false;
    for (int index = 0; index < this.NumberOfConferences; ++index)
    {
      foreach (int num in this.GetTeamsInConference(index + 1))
      {
        if (num == this.UserTeamIndex)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        break;
    }
    if (!flag)
      this.TeamsInConferences[1][0] = this.UserTeamIndex;
    this.TeamIndexMasterList = new int[this.NumberOfTeamsInLeague];
  }

  public void PopuplateMasterList()
  {
    for (int index1 = 0; index1 < this.NumberOfConferences; ++index1)
    {
      for (int index2 = 0; index2 < this.NumberOfTeamsPerConference; ++index2)
        this.TeamIndexMasterList[index1 * this.NumberOfTeamsPerConference + index2] = this.TeamsInConferences[index1 + 1][index2];
    }
  }

  public TeamData GetTeamData(int i) => this.teamsInFranchise?[i.ToString()];

  public void SaveTeamData(TeamData team, int i) => this.teamsInFranchise[i.ToString()] = team;

  public void SaveAllTeamData(Dictionary<string, TeamData> teams) => this.teamsInFranchise = teams;

  private TeamData DuplicateTeamData(TeamData teamData)
  {
    TeamData teamData1 = new TeamData();
    RosterData roster1 = new RosterData(teamData.GetNumberOfPlayersOnRoster());
    RosterData roster2 = new RosterData(teamData.GetNumberOfPlayersOnPracticeSquad());
    for (int playerIndex = 0; playerIndex < teamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      if (teamData.GetPlayer(playerIndex) != null)
      {
        PlayerData player = new PlayerData();
        player.CopyAttributesFrom(teamData.GetPlayer(playerIndex));
        roster1.SetPlayer(playerIndex, player);
      }
    }
    for (int playerIndex = 0; playerIndex < teamData.GetNumberOfPlayersOnPracticeSquad(); ++playerIndex)
    {
      if (teamData.GetPlayerOnPracticeSquad(playerIndex) != null)
      {
        PlayerData player = new PlayerData();
        player.CopyAttributesFrom(teamData.GetPlayerOnPracticeSquad(playerIndex));
        roster2.SetPlayer(playerIndex, player);
      }
    }
    roster1.defaultPlayers = new Dictionary<string, int>((IDictionary<string, int>) teamData.MainRoster.defaultPlayers);
    roster2.defaultPlayers = new Dictionary<string, int>((IDictionary<string, int>) teamData.PracticeSquad.defaultPlayers);
    teamData1.SetMainRoster(roster1);
    teamData1.SetPracticeSquad(roster2);
    return teamData1;
  }

  public int GetTeamIndexFromAbbrev(string s)
  {
    int teamIndexFromAbbrev;
    if (this.teamAbbrevMap.TryGetValue(s, out teamIndexFromAbbrev))
      return teamIndexFromAbbrev;
    Debug.LogError((object) ("GetTeamIndexFromAbbrev:Key = " + s + " not found in TeamAbbrevMap"));
    return -1;
  }

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value)
  {
    this.isDirty = value;
    if (!this.isDirty)
      return;
    AppEvents.SaveSeasonMode.Trigger();
  }

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    SGD_SeasonModeData sgdSeasonModeData = await SaveIO.LoadAsync<SGD_SeasonModeData>(SaveIO.GetPath(SGD_SeasonModeData.FileName));
    if (sgdSeasonModeData != null)
    {
      PersistentSingleton<SaveManager>.Instance.MarkSeasonDataAsExisting();
      PersistentSingleton<SaveManager>.Instance.seasonModeData = sgdSeasonModeData;
    }
    else
      Debug.Log((object) "COULD NOT FIND SEASON MODE DATA");
  }

  public async Task Save()
  {
    SGD_SeasonModeData objectTarget = this;
    PersistentSingleton<SaveManager>.Instance.MarkSeasonDataAsExisting();
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<SGD_SeasonModeData>(objectTarget, SaveIO.GetPath(SGD_SeasonModeData.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
