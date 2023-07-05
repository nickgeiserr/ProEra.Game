// Decompiled with JetBrains decompiler
// Type: SeasonModeManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballWorld;
using Framework;
using Framework.StateManagement;
using ProEra.Game.Achievements;
using ProEra.Game.Sources.SeasonMode.SeasonTablet;
using ProEra.Game.Sources.TeamData;
using ProEra.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TB12;
using TB12.AppStates;
using TB12.UI;
using UDB;
using UnityEngine;

public class SeasonModeManager : MonoBehaviour
{
  [SerializeField]
  private CanvasTabManagerConfig _canvasTabManagerConfig;
  private int[] sortedTeams;
  [Header("Team Stats")]
  public TeamStatsManager teamStatsManager;
  [Header("Career Stats Window")]
  public CareerStatsManager careerStatsManager;
  [HideInInspector]
  public static SeasonModeManager self;
  [HideInInspector]
  public global::TeamData userTeamData;
  [HideInInspector]
  public int baseYear;
  [HideInInspector]
  public SGD_SeasonModeData seasonModeData;
  private RoutineHandle routineNewSeason = new RoutineHandle();
  private const int NEW_SEASON_BASE_YEAR = 2022;
  private HashSet<int> _superBowlWinningTeams;
  private StatSet _careerStats = new StatSet();
  private int[] _teams;

  private AchievementData achievementData => SaveManager.GetAchievementData();

  public event System.Action OnInitComplete;

  public int CareerInterceptions => this.CareerStats.interceptions;

  public int CareerIntsThrown => this.CareerStats.qbInts;

  public int CareerPassingYards => this.CareerStats.passYards;

  public int CareerWins => this.CareerStats.wins;

  public int CareerGames => this.CareerStats.Games;

  public int CareerDivisionWins => this.CareerStats.divWins;

  public int CareerMvp => this.CareerStats.mvpAwards;

  public int CareerTotalPlayerOfTheWeek => this.CareerStats.TotalPlayerOfTheWeekAwards;

  public int CareerRivalWins => this.CareerStats.rivalWins;

  public int CareerTouchdownPasses => this.CareerStats.touchdownPasses;

  public int CareerPassCompletions => this.CareerStats.passCompletions;

  public int CareerSuperBowlWins => this.CareerStats.superBowlWins;

  public int CareerConferenceChampionships => this.CareerStats.confChampionships;

  public int SuperBowlMvpAwards => this.CareerStats.superBowlMvpAwards;

  public bool SeasonOverForUser => this.seasonModeData.seasonState != ProEraSeasonState.RegularSeason && this.seasonModeData.seasonState != ProEraSeasonState.InPlayoffs;

  public bool IsCurrentSeasonTdPassLeader => !((IEnumerable<int>) this.GetSortedTeamsInLeague()).Select<int, global::TeamData>(new Func<int, global::TeamData>(this.GetTeamData)).Any<global::TeamData>((Func<global::TeamData, bool>) (teamData => !this.IsSeasonOver() || teamData.CurrentSeasonTouchdownPasses > this.userTeamData.CurrentSeasonTouchdownPasses));

  public List<int> ConferenceLeaders
  {
    get
    {
      List<int> conferenceLeaders = new List<int>();
      for (int conference = 0; conference < this.seasonModeData.NumberOfConferences; ++conference)
        conferenceLeaders.Add(((IEnumerable<int>) this.GetSortedTeamsInConference(conference)).FirstOrDefault<int>());
      return conferenceLeaders;
    }
  }

  public List<int> DivisionLeaders
  {
    get
    {
      List<int> divisionLeaders = new List<int>();
      for (int conference = 1; conference < this.seasonModeData.NumberOfConferences; ++conference)
      {
        for (int division = 0; division < this.seasonModeData.NumberOfDivisions; ++division)
          divisionLeaders.Add(((IEnumerable<int>) this.GetSortedTeamsInDivision(conference, division)).FirstOrDefault<int>());
      }
      return divisionLeaders;
    }
  }

  public HashSet<int> CareerTeamsBeaten => this.CareerStats.teamsBeaten;

  public StatSet CareerStats => this._careerStats;

  public void Awake()
  {
    if (!((UnityEngine.Object) SeasonModeManager.self == (UnityEngine.Object) null))
      return;
    SeasonModeManager.self = this;
    this.baseYear = 2022;
  }

  public void CreateNewSeasonMode()
  {
    PersistentData.saveSlot = "0";
    this.seasonModeData = PersistentSingleton<SaveManager>.Instance.CreateNewSeasonModeData();
    this.seasonModeData.SetupLeague(PersistentData.GetUserTeamIndex());
    this.seasonModeData.AssignTeamsToConferences(false);
    Debug.Log((object) ("Created New Season at slot " + PersistentData.saveSlot + " with team index " + PersistentData.GetUserTeamIndex().ToString()));
  }

  public void StartNewSeasonMode()
  {
    Debug.Log((object) "Start New Season Mode");
    bool flag = false;
    for (int index1 = 0; index1 < this.seasonModeData.NumberOfConferences; ++index1)
    {
      int[] teamsInConference = this.seasonModeData.GetTeamsInConference(index1 + 1);
      for (int index2 = 0; index2 < teamsInConference.Length; ++index2)
      {
        if (teamsInConference[index2] == this.seasonModeData.UserTeamIndex)
        {
          flag = true;
          this.seasonModeData.UsersConference = index1 + 1;
          this.seasonModeData.UsersDivision = Mathf.FloorToInt((float) (index2 / 4)) + 1;
          break;
        }
      }
      if (flag)
        break;
    }
    if (!flag)
    {
      WarningWindowManager.instance.ShowWindow("THE TEAM YOU SELECTED (" + TeamDataCache.GetTeam(this.seasonModeData.UserTeamIndex).GetFullDisplayName() + ") MUST BE INCLUDED IN THE LEAGUE.");
    }
    else
    {
      Dictionary<string, global::TeamData> teams = new Dictionary<string, global::TeamData>();
      this.seasonModeData.PopuplateMasterList();
      int[] teamIndexMasterList = this.seasonModeData.TeamIndexMasterList;
      for (int index = 0; index < this.seasonModeData.NumberOfTeamsInLeague; ++index)
      {
        int teamIndex = teamIndexMasterList[index];
        global::TeamData team = TeamDataCache.GetTeam(teamIndex);
        team.CreateNewSeasonStatsForAllPlayers(this.baseYear);
        teams[teamIndex.ToString()] = team;
        this.seasonModeData.teamAbbrevMap.Add(team.GetAbbreviation(), teamIndex);
      }
      this.seasonModeData.SaveAllTeamData(teams);
      int userTeamIndex = PersistentData.GetUserTeamIndex();
      this.userTeamData = teams[userTeamIndex.ToString()];
      this.CreateNewSeason();
      this.Init(false);
      System.Action onInitComplete = this.OnInitComplete;
      if (onInitComplete == null)
        return;
      onInitComplete();
    }
  }

  public void Init(bool loadData = true)
  {
    Debug.Log((object) ("Init() started " + Time.realtimeSinceStartup.ToString()));
    if (loadData)
      this.LoadSeasonData();
    this.InitFranchiseWindows_BeforeSimulatWeek();
    this.sortedTeams = new int[this.seasonModeData.NumberOfTeamsInLeague];
    if (PersistentData.watchingNonUserMatch)
      this.ApplyDataFromSpectatedMatch();
    if (PersistentData.simulateWeek)
    {
      this.SimulateWeek(false);
      PersistentData.simulateWeek = false;
    }
    else if (PersistentData.saveGameStats)
    {
      this.ApplyGameStatsFromGame(false);
      PersistentData.saveGameStats = false;
    }
    Debug.Log((object) (this.seasonModeData.UserTeamIndex.ToString() + " is the current user team index in the season"));
    Debug.Log((object) (this.seasonModeData.GetTeamData(this.seasonModeData.UserTeamIndex).GetName() + " is the current user team in the season"));
    PersistentData.userIsHome = true;
    PersistentData.SetUserTeam(this.seasonModeData.GetTeamData(this.seasonModeData.UserTeamIndex));
    int teamOpponentForWeek = this.GetTeamOpponentForWeek(this.seasonModeData.UserTeamIndex, this.seasonModeData.currentWeek, out int _, out int _);
    SeasonModeGameRound gameRound = this.GetGameRound(this.seasonModeData.currentWeek);
    if (teamOpponentForWeek < 0 && (gameRound == SeasonModeGameRound.RegularSeason || gameRound == SeasonModeGameRound.WildCard))
      this.DoByeWeekTransition();
    this.InitFranchiseWindows_AfterSimulatWeek();
    this.SortTeams();
    PersistentSingleton<PlayerApi>.Instance.SyncScoreOnReturnToLockerRoom();
    Debug.Log((object) ("Init() completed " + Time.realtimeSinceStartup.ToString()));
  }

  private void InitFranchiseWindows_BeforeSimulatWeek() => SimulationManager.Init();

  private void InitFranchiseWindows_AfterSimulatWeek()
  {
    if (!this.IsSeasonOver())
      return;
    this.achievementData.SyncPerSeasonAchievements();
  }

  public void ShowSeasonMode()
  {
    this.Init();
    System.Action onInitComplete = this.OnInitComplete;
    if (onInitComplete == null)
      return;
    onInitComplete();
  }

  public global::TeamData GetTeamData(int teamIndex)
  {
    global::TeamData teamData = (global::TeamData) null;
    try
    {
      teamData = this.seasonModeData.GetTeamData(teamIndex);
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine("ERROR: Failed to get team data for team index: " + teamIndex.ToString());
    }
    return teamData;
  }

  public int GetCurrentYear() => this.baseYear + this.seasonModeData.seasonNumber;

  public void StartNextSeason()
  {
    this.CreateNewSeason();
    AppEvents.SaveSeasonMode.Trigger();
    PersistentSingleton<StateManager<EAppState, GameState>>.Instance.ForceReloadState();
  }

  public void ReorderAllDepthCharts()
  {
    for (int index = 0; index < this.seasonModeData.NumberOfTeamsInLeague; ++index)
      this.GetTeamData(this.seasonModeData.TeamIndexMasterList[index]).TeamDepthChart.SetBestStartersForAllPositions();
  }

  public void CreateNewSeason()
  {
    this.seasonModeData.waitingOnDemotionGame = false;
    this.seasonModeData.userReceivesFirstRoundBye = false;
    this.seasonModeData.seasonOverForUser = false;
    this.seasonModeData.playerProgressionDone = false;
    this.seasonModeData.seasonState = ProEraSeasonState.RegularSeason;
    this.userTeamData.CurrentSeasonStats.IsSuperbowlWinner = false;
    this.SetCurrentWeek(1);
    if (this.seasonModeData.UsersConference == 1)
      ++this.seasonModeData.seasonsInTier1;
    else if (this.seasonModeData.UsersConference == 2)
      ++this.seasonModeData.seasonsInTier2;
    else
      ++this.seasonModeData.seasonsInTier3;
    for (int index = 0; index < this.seasonModeData.NumberOfTeamsInLeague; ++index)
    {
      global::TeamData teamData = this.GetTeamData(this.seasonModeData.TeamIndexMasterList[index]);
      teamData.CreateNewTeamSeasonStats();
      if (this.seasonModeData.seasonNumber > 0)
        teamData.CreateNewSeasonStatsForAllPlayers(this.baseYear + this.seasonModeData.seasonNumber);
    }
    this.SetSchedule();
    this.seasonModeData.ResetAwards();
    if (this.seasonModeData.UsesTierSystem)
    {
      this.seasonModeData.scoresByWeek_tier1 = new GameSummary[this.seasonModeData.NumberOfWeeksInSeason, this.seasonModeData.NumberOfTeamsPerConference];
      this.seasonModeData.scoresByWeek_tier2 = new GameSummary[this.seasonModeData.NumberOfWeeksInSeason, this.seasonModeData.NumberOfTeamsPerConference];
      this.seasonModeData.scoresByWeek_tier3 = new GameSummary[this.seasonModeData.NumberOfWeeksInSeason, this.seasonModeData.NumberOfTeamsPerConference];
    }
    else
      this.seasonModeData.scoresByWeek_League = new GameSummary[this.seasonModeData.NumberOfWeeksInSeason, this.seasonModeData.MaxNumberOfGamesPerWeek * 2];
    this.seasonModeData.seasonWins = 0;
    this.seasonModeData.seasonLosses = 0;
  }

  public void PlayWeek_Normal()
  {
    if (this.seasonModeData.seasonOverForUser && this.ArePlayoffsOver() || !this.AllowMatchToStart())
      return;
    if (this.FindTeamInWeek(this.seasonModeData.UserTeamIndex, this.seasonModeData.currentWeek) >= 0)
    {
      this.PlayWeek(1);
    }
    else
    {
      PersistentData.simulateWeek = false;
      this.DoByeWeekTransition();
    }
  }

  private void DoByeWeekTransition()
  {
    Debug.Log((object) "Start Simulating bye week...");
    TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.ByeWeek, true);
    this.SimulateWeek(true);
    Debug.Log((object) "End Simulating bye week...");
    TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.ByeWeek, false);
  }

  public void PlayWeek_CoachMode()
  {
    if (!this.AllowMatchToStart())
      return;
    this.PlayWeek(2);
  }

  public void PlayWeek_Spectate()
  {
    if (!this.AllowMatchToStart())
      return;
    this.PlayWeek(3);
  }

  public void PlayWeek_Simulate()
  {
    if (!this.AllowMatchToStart())
      return;
    Debug.Log((object) " Season Mode: Simulating Matches...");
    this.StartCoroutine(this.Continue_SimulateWeek());
  }

  private bool AllowMatchToStart()
  {
    int filledRosterSpots1 = this.userTeamData.MainRoster.FindNumberOfFilledRosterSpots();
    int filledRosterSpots2 = this.userTeamData.PracticeSquad.FindNumberOfFilledRosterSpots();
    int ofPlayersOnRoster = TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER;
    if (filledRosterSpots1 < ofPlayersOnRoster)
    {
      Debug.Log((object) ("YOU MUST HAVE " + TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER.ToString() + " PLAYERS ON YOUR ACTIVE ROSTER TO START OR SIMULATE A MATCH."));
      return false;
    }
    if (filledRosterSpots2 <= TeamAssetManager.NUMBER_OF_PLAYERS_ON_PRACTICE_SQUAD)
      return true;
    Debug.Log((object) ("YOU MUST HAVE " + TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER.ToString() + " PLAYERS ON YOUR ACTIVE ROSTER TO START OR SIMULATE A MATCH."));
    return false;
  }

  private void PlayWeek(int matchType)
  {
    AppState.SeasonMode.Value = ESeasonMode.kLoad;
    PersistentData.watchingNonUserMatch = false;
    global::TeamData teamData = this.GetTeamData(this.seasonModeData.UserTeamIndex);
    PersistentData.userIsHome = this.FindTeamInWeek(this.seasonModeData.UserTeamIndex, this.seasonModeData.currentWeek) % 2 == 0;
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.PlayerSide.SetValue(PersistentData.userIsHome ? ETeamUniformFlags.Home : ETeamUniformFlags.Away);
    PersistentData.SetUserTeam(teamData);
    PersistentData.SetCompTeam(this.GetTeamData(this.GetTeamOpponentForWeek(this.seasonModeData.UserTeamIndex, this.seasonModeData.currentWeek, out int _, out int _)));
    WorldState.TimeOfDay.Value = this.seasonModeData.currentWeek > this.seasonModeData.NumberOfWeeksInSeason ? ((double) UnityEngine.Random.value > 0.5 ? ETimeOfDay.Night : ETimeOfDay.Clear) : (this.seasonModeData.seasonNumber != 0 ? (this.seasonModeData.currentWeek % 4 == 0 ? ETimeOfDay.Night : ETimeOfDay.Clear) : (this.GetScheduleMatchupData(teamData.GetAbbreviation(), this.seasonModeData.currentWeek - 1).night ? ETimeOfDay.Night : ETimeOfDay.Clear));
    if (matchType == 1)
      GameplayManager.LoadLevelActivation(EGameMode.kAxisGame, WorldState.TimeOfDay.Value);
    else if (matchType == 2)
      Debug.LogError((object) "Invalid match type called for Playweek");
    else
      Debug.LogError((object) "Invalid match type called for Playweek");
  }

  private ScheduleMatchupData GetScheduleMatchupData(string teamAbb, int weekIndex) => TeamResourcesManager.LoadSeasonSchedule(this.seasonModeData.seasonNumber)[teamAbb][weekIndex];

  private IEnumerator Continue_SimulateWeek()
  {
    yield return (object) null;
    this.SimulateWeek(true);
    PersistentData.simulateWeek = false;
  }

  public void SimulateWeek(
    bool simUserGame,
    SeasonModeManager.ForcedSimResult forcedResult = SeasonModeManager.ForcedSimResult.Random,
    bool bSaveSeason = true)
  {
    if (this.seasonModeData.currentWeek > this.seasonModeData.NumberOfWeeksInSeason + this.seasonModeData.NumberOfWeeksInPlayoffs)
      return;
    Debug.Log((object) ("Simulating week " + this.seasonModeData.currentWeek.ToString() + "..."));
    SimulationManager.ResetWeeklyAwardData();
    bool flag1 = false;
    global::TeamData userTeam = PersistentData.GetUserTeam();
    global::TeamData compTeam = PersistentData.GetCompTeam();
    if (!simUserGame && compTeam.TeamIndex != userTeam.TeamIndex)
    {
      this.ApplyGameStatsFromGame();
      SimulationManager.SetPlayersOfWeekFromUserGame();
    }
    if (this.seasonModeData.currentWeek <= this.seasonModeData.NumberOfWeeksInSeason)
    {
      if (!simUserGame)
      {
        GameSummary[,] scoresByWeek = this.seasonModeData.GetScoresByWeek(this.seasonModeData.UsersConference);
        int teamInWeek = this.FindTeamInWeek(this.seasonModeData.UserTeamIndex, this.seasonModeData.currentWeek);
        if (teamInWeek >= 0)
        {
          flag1 = teamInWeek % 2 == 0;
          int index = !flag1 ? teamInWeek - 1 : teamInWeek + 1;
          scoresByWeek[this.seasonModeData.currentWeek - 1, teamInWeek] = PersistentData.userGameSummary;
          scoresByWeek[this.seasonModeData.currentWeek - 1, index] = PersistentData.compGameSummary;
        }
      }
      GameSummary[,] scoresByWeek1 = this.seasonModeData.GetScoresByWeek(0);
      int[,] scheduleByTier = this.seasonModeData.GetScheduleByTier();
      for (int index = 0; index < this.seasonModeData.MaxNumberOfGamesPerWeek * 2; index += 2)
      {
        int homeIndexInSchedule = index;
        int awayIndexInSchedule = index + 1;
        int homeIndex = scheduleByTier[this.seasonModeData.currentWeek - 1, homeIndexInSchedule];
        int awayIndex = scheduleByTier[this.seasonModeData.currentWeek - 1, awayIndexInSchedule];
        if (homeIndex != -1 && awayIndex != -1)
        {
          bool flag2 = awayIndex == this.seasonModeData.UserTeamIndex || homeIndex == this.seasonModeData.UserTeamIndex;
          if (flag2 && homeIndex == this.seasonModeData.UserTeamIndex)
            flag1 = true;
          int num1 = 0;
          int num2 = 0;
          if (scoresByWeek1[this.seasonModeData.currentWeek - 1, homeIndexInSchedule] != null)
          {
            num1 = scoresByWeek1[this.seasonModeData.currentWeek - 1, homeIndexInSchedule].TeamGameStats.Score;
            num2 = scoresByWeek1[this.seasonModeData.currentWeek - 1, awayIndexInSchedule].TeamGameStats.Score;
          }
          if ((simUserGame || !flag2) && num1 == 0 && num2 == 0)
          {
            int? forcedWinnerIndex = new int?();
            if (simUserGame && forcedResult == SeasonModeManager.ForcedSimResult.UserWin)
              forcedWinnerIndex = new int?(this.seasonModeData.UserTeamIndex);
            else if (simUserGame && forcedResult == SeasonModeManager.ForcedSimResult.UserLoss)
              forcedWinnerIndex = new int?(flag1 ? awayIndex : homeIndex);
            SimulationManager.SimulateFranchiseGame(homeIndex, awayIndex, homeIndexInSchedule, awayIndexInSchedule, true, 0, forcedWinnerIndex);
          }
        }
        else
          break;
      }
      if (this.seasonModeData.offPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] == null)
        this.seasonModeData.offPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] = SimulationManager.offensivePlayerOfTheWeek;
      else if ((double) this.seasonModeData.offPlayersOfTheWeek[this.seasonModeData.currentWeek - 1].statScore < (double) SimulationManager.offensivePlayerOfTheWeek.statScore)
        this.seasonModeData.offPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] = SimulationManager.offensivePlayerOfTheWeek;
      Award award1 = this.seasonModeData.offPlayersOfTheWeek[this.seasonModeData.currentWeek - 1];
      if (award1 != null && award1.teamIndex == userTeam.TeamIndex)
        ++this.CareerStats.offesensivePowAwards;
      if (this.seasonModeData.defPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] == null)
        this.seasonModeData.defPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] = SimulationManager.defensivePlayerOfTheWeek;
      else if ((double) this.seasonModeData.defPlayersOfTheWeek[this.seasonModeData.currentWeek - 1].statScore < (double) SimulationManager.defensivePlayerOfTheWeek.statScore)
        this.seasonModeData.defPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] = SimulationManager.defensivePlayerOfTheWeek;
      Award award2 = this.seasonModeData.defPlayersOfTheWeek[this.seasonModeData.currentWeek - 1];
      if (award2 != null && award2.teamIndex == userTeam.TeamIndex)
        ++this.CareerStats.defensivePowAwards;
      if (this.seasonModeData.currentWeek == this.seasonModeData.NumberOfWeeksInSeason)
      {
        this.SetPlayoffBrackets();
        if (this.GetSortedTeamsInDivision(this.seasonModeData.UsersConference, this.seasonModeData.UsersDivision)[0] == userTeam.TeamIndex)
          ++this.CareerStats.divChampionships;
        SimulationManager.SetYearAwards();
        if (SimulationManager.mvp.teamIndex == userTeam.TeamIndex)
          ++this.CareerStats.mvpAwards;
      }
    }
    else if (this.seasonModeData.currentWeek < this.seasonModeData.NumberOfWeeksInSeason + this.seasonModeData.NumberOfWeeksInPlayoffs + 1)
    {
      if (!simUserGame)
      {
        GameSummary[] gameSummaryArray = !this.seasonModeData.UsesTierSystem ? (!this.IsFirstRoundOfPlayoffs() ? (!this.IsSecondRoundOfPlayoffs() ? (!this.IsThirdRoundOfPlayoffs() ? this.seasonModeData.GetPlayoffScores_R4() : this.seasonModeData.GetPlayoffScoresByTier_R3(this.seasonModeData.UsersConference)) : this.seasonModeData.GetPlayoffScoresByTier_R2(this.seasonModeData.UsersConference)) : this.seasonModeData.GetPlayoffScoresByTier_R1(this.seasonModeData.UsersConference)) : (!this.IsFirstRoundOfPlayoffs() ? (!this.IsSecondRoundOfPlayoffs() ? this.seasonModeData.GetPlayoffScoresByTier_R3(this.seasonModeData.UsersConference) : this.seasonModeData.GetPlayoffScoresByTier_R2(this.seasonModeData.UsersConference)) : this.seasonModeData.GetPlayoffScoresByTier_R1(this.seasonModeData.UsersConference));
        int teamInWeek = this.FindTeamInWeek(this.seasonModeData.UserTeamIndex, this.seasonModeData.currentWeek);
        int index = teamInWeek % 2 != 0 ? teamInWeek - 1 : teamInWeek + 1;
        if (teamInWeek >= 0 || index >= 0)
        {
          gameSummaryArray[teamInWeek] = PersistentData.userGameSummary;
          gameSummaryArray[index] = PersistentData.compGameSummary;
          int winningTeam;
          if (PersistentData.userGameSummary.TeamGameStats.Score > PersistentData.compGameSummary.TeamGameStats.Score)
          {
            winningTeam = PersistentData.GetUserTeamIndex();
          }
          else
          {
            winningTeam = PersistentData.GetCompTeamIndex();
            this.seasonModeData.seasonOverForUser = true;
            this.seasonModeData.seasonState = ProEraSeasonState.LostInPlayoffs;
            this.SetEndOfSeasonLostInPlayoffsText();
          }
          this.AdvancePlayoffWinner_NFL(!this.IsFirstRoundOfPlayoffs() ? (!this.IsSecondRoundOfPlayoffs() ? (!this.IsThirdRoundOfPlayoffs() ? (int[]) null : this.seasonModeData.GetPlayoffSchedule_R4()) : this.seasonModeData.GetPlayoffScheduleByTier_R3(this.seasonModeData.UsersConference)) : this.seasonModeData.GetPlayoffScheduleByTier_R2(this.seasonModeData.UsersConference), winningTeam, teamInWeek);
        }
      }
      this.SimulatePlayoffRound_NFL(simUserGame, forcedResult);
      this.SortPlayoffTeamsForComingWeek(!this.IsFirstRoundOfPlayoffs() ? (!this.IsSecondRoundOfPlayoffs() ? (!this.IsThirdRoundOfPlayoffs() ? (int[]) null : this.seasonModeData.GetPlayoffSchedule_R4()) : this.seasonModeData.GetPlayoffScheduleByTier_R3(this.seasonModeData.UsersConference)) : this.seasonModeData.GetPlayoffScheduleByTier_R2(this.seasonModeData.UsersConference));
      if (this.IsFirstRoundOfPlayoffs() || this.IsSecondRoundOfPlayoffs())
      {
        int seasonState = (int) this.seasonModeData.seasonState;
      }
      if (this.ArePlayoffsOver())
        this.SetPlayoffResults();
    }
    if (this.seasonModeData.currentWeek < this.seasonModeData.NumberOfWeeksInSeason + this.seasonModeData.NumberOfWeeksInPlayoffs + 1)
    {
      Debug.Log((object) string.Format("Finished simulation for week {0}", (object) this.seasonModeData.currentWeek));
      this.IncreaseSeasonWeekByOne();
    }
    PersistentData.userGameSummary = (GameSummary) null;
    PersistentData.compGameSummary = (GameSummary) null;
    if (!bSaveSeason)
      return;
    Debug.Log((object) "SIMULATING MATCHES");
    AppEvents.SaveSeasonMode.Trigger();
  }

  public void SetSchedule() => this.SetNFLStyleSchedule();

  private void SetNFLStyleSchedule()
  {
    this.seasonModeData.leagueSchedule = new int[this.seasonModeData.NumberOfWeeksInSeason, this.seasonModeData.MaxNumberOfGamesPerWeek * 2];
    for (int index1 = 0; index1 < this.seasonModeData.NumberOfWeeksInSeason; ++index1)
    {
      for (int index2 = 0; index2 < this.seasonModeData.MaxNumberOfGamesPerWeek * 2; ++index2)
        this.seasonModeData.leagueSchedule[index1, index2] = -1;
    }
    Dictionary<string, List<ScheduleMatchupData>> dictionary1 = TeamResourcesManager.LoadSeasonSchedule(this.seasonModeData.seasonNumber);
    Dictionary<int, List<string>> dictionary2 = new Dictionary<int, List<string>>();
    for (int index3 = 0; index3 < this.seasonModeData.NumberOfWeeksInSeason; ++index3)
    {
      List<string> stringList = new List<string>();
      int index4 = 0;
      foreach (string key in dictionary1.Keys)
      {
        int teamIndexFromAbbrev = this.seasonModeData.GetTeamIndexFromAbbrev(key);
        string matchup = dictionary1[key][index3].matchup;
        bool flag = matchup.Contains("@");
        if (!matchup.Contains("BYE") && !flag)
        {
          stringList.Add(key);
          this.seasonModeData.leagueSchedule[index3, index4] = teamIndexFromAbbrev;
          index4 += 2;
        }
      }
      dictionary2.Add(index3, stringList);
    }
    for (int index = 0; index < this.seasonModeData.NumberOfWeeksInSeason; ++index)
    {
      List<string> stringList = dictionary2[index];
      foreach (string key in dictionary1.Keys)
      {
        int teamIndexFromAbbrev = this.seasonModeData.GetTeamIndexFromAbbrev(key);
        string matchup = dictionary1[key][index].matchup;
        bool flag = matchup.Contains("@");
        if (!matchup.Contains("BYE") & flag)
        {
          string s = matchup.TrimStart('@');
          this.seasonModeData.GetTeamIndexFromAbbrev(s);
          int num = stringList.IndexOf(s);
          if (num >= 0)
            this.seasonModeData.leagueSchedule[index, num * 2 + 1] = teamIndexFromAbbrev;
          else
            Debug.LogError((object) (" Matchup not found for" + s + "in week" + index.ToString()));
        }
      }
    }
    Debug.Log((object) "Week 1 schedule");
    for (int index = 0; index < this.seasonModeData.MaxNumberOfGamesPerWeek; index += 2)
    {
      this.seasonModeData.leagueSchedule[0, index].ToString();
      this.seasonModeData.leagueSchedule[0, index + 1].ToString();
    }
  }

  private void RandomizeArray(int[] a)
  {
    for (int minInclusive = 0; minInclusive < a.Length; ++minInclusive)
    {
      int num = a[minInclusive];
      int index = UnityEngine.Random.Range(minInclusive, a.Length);
      a[minInclusive] = a[index];
      a[index] = num;
    }
  }

  private void SetPlayoffBrackets()
  {
    this.SortTeams();
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    int[] teamsInConference1 = this.GetSortedTeamsInConference(1);
    int[] teamsInConference2 = this.GetSortedTeamsInConference(2);
    int[] numArray = this.seasonModeData.UsersConference != 1 ? teamsInConference2 : teamsInConference1;
    for (int index = 0; index < 7; ++index)
    {
      if (numArray[index] == this.seasonModeData.UserTeamIndex)
      {
        if (index == 0)
          flag3 = true;
        flag1 = true;
        ++this.CareerStats.playoffAppearances;
        break;
      }
    }
    this.seasonModeData.playoffsR1_league = new int[12]
    {
      teamsInConference1[3],
      teamsInConference1[4],
      teamsInConference1[2],
      teamsInConference1[5],
      teamsInConference1[1],
      teamsInConference1[6],
      teamsInConference2[3],
      teamsInConference2[4],
      teamsInConference2[2],
      teamsInConference2[5],
      teamsInConference2[1],
      teamsInConference2[6]
    };
    this.seasonModeData.playoffsR2_league = new int[8]
    {
      teamsInConference1[0],
      -1,
      -1,
      -1,
      teamsInConference2[0],
      -1,
      -1,
      -1
    };
    this.seasonModeData.playoffsR3_league = new int[4]
    {
      -1,
      -1,
      -1,
      -1
    };
    this.seasonModeData.playoffsR4_league = new int[2]
    {
      -1,
      -1
    };
    this.seasonModeData.scoresPlayoffR1_league = new GameSummary[this.seasonModeData.playoffsR1_league.Length];
    this.seasonModeData.scoresPlayoffR2_league = new GameSummary[this.seasonModeData.playoffsR2_league.Length];
    this.seasonModeData.scoresPlayoffR3_league = new GameSummary[this.seasonModeData.playoffsR3_league.Length];
    this.seasonModeData.scoresPlayoffR4_league = new GameSummary[this.seasonModeData.playoffsR4_league.Length];
    if (!flag1)
    {
      this.seasonModeData.seasonState = ProEraSeasonState.DidNotMakePlayoffs;
      this.seasonModeData.SetEndOfYearText("DID NOT REACH THE PLAYOFFS");
      this.seasonModeData.seasonOverForUser = true;
    }
    else if (flag3)
    {
      this.seasonModeData.seasonOverForUser = true;
      this.seasonModeData.userReceivesFirstRoundBye = true;
    }
    else
    {
      if (!flag2)
        return;
      this.seasonModeData.waitingOnDemotionGame = true;
      this.seasonModeData.seasonOverForUser = true;
    }
  }

  private void SimulateRemainingPlayoffGames()
  {
  }

  private void SimulateUntilChampionship()
  {
  }

  private void SimulatePlayoffRound_NFL(
    bool simUserGame,
    SeasonModeManager.ForcedSimResult forcedResult = SeasonModeManager.ForcedSimResult.Random)
  {
    int usersConference = this.seasonModeData.UsersConference;
    GameSummary[] gameSummaryArray;
    int[] numArray1;
    int[] numArray2;
    if (this.IsFirstRoundOfPlayoffs())
    {
      gameSummaryArray = this.seasonModeData.GetPlayoffScoresByTier_R1(usersConference);
      numArray1 = this.seasonModeData.GetPlayoffScheduleByTier_R1(usersConference);
      numArray2 = this.seasonModeData.GetPlayoffScheduleByTier_R2(usersConference);
      if (this.seasonModeData.userReceivesFirstRoundBye)
        this.seasonModeData.seasonOverForUser = false;
    }
    else if (this.IsSecondRoundOfPlayoffs())
    {
      gameSummaryArray = this.seasonModeData.GetPlayoffScoresByTier_R2(usersConference);
      numArray1 = this.seasonModeData.GetPlayoffScheduleByTier_R2(usersConference);
      numArray2 = this.seasonModeData.GetPlayoffScheduleByTier_R3(usersConference);
    }
    else if (this.IsThirdRoundOfPlayoffs())
    {
      gameSummaryArray = this.seasonModeData.GetPlayoffScoresByTier_R3(usersConference);
      numArray1 = this.seasonModeData.GetPlayoffScheduleByTier_R3(usersConference);
      numArray2 = this.seasonModeData.GetPlayoffSchedule_R4();
    }
    else
    {
      gameSummaryArray = this.seasonModeData.GetPlayoffScores_R4();
      numArray1 = this.seasonModeData.GetPlayoffSchedule_R4();
      numArray2 = (int[]) null;
    }
    for (int index1 = 0; index1 < numArray1.Length; index1 += 2)
    {
      int index2 = index1;
      int awayIndexInSchedule = index1 + 1;
      int homeIndex = numArray1[index2];
      int awayIndex = numArray1[awayIndexInSchedule];
      bool flag = homeIndex == this.seasonModeData.UserTeamIndex || awayIndex == this.seasonModeData.UserTeamIndex;
      int num1 = 0;
      int num2 = 0;
      if (gameSummaryArray[index2] != null)
      {
        num1 = gameSummaryArray[index2].TeamGameStats.Score;
        num2 = gameSummaryArray[awayIndexInSchedule].TeamGameStats.Score;
      }
      if ((simUserGame || !flag) && num1 == 0 && num2 == 0)
      {
        int? forcedWinnerIndex = new int?();
        if (flag & simUserGame && forcedResult == SeasonModeManager.ForcedSimResult.UserWin)
          forcedWinnerIndex = new int?(this.seasonModeData.UserTeamIndex);
        else if (flag & simUserGame && forcedResult == SeasonModeManager.ForcedSimResult.UserLoss)
          forcedWinnerIndex = new int?(homeIndex == this.seasonModeData.UserTeamIndex ? awayIndex : homeIndex);
        int winningTeam = SimulationManager.SimulateFranchiseGame(homeIndex, awayIndex, index2, awayIndexInSchedule, false, usersConference, forcedWinnerIndex) ? homeIndex : awayIndex;
        if (flag && winningTeam != this.seasonModeData.UserTeamIndex)
        {
          this.seasonModeData.seasonState = ProEraSeasonState.LostInPlayoffs;
          this.seasonModeData.seasonOverForUser = true;
          this.SetEndOfSeasonLostInPlayoffsText();
        }
        this.AdvancePlayoffWinner_NFL(numArray2, winningTeam, index2);
      }
    }
    if (this.IsThirdRoundOfPlayoffs())
      return;
    this.SortPlayoffTeamsForComingWeek(numArray2);
  }

  private void SortPlayoffTeamsForComingWeek(int[] upcomingSchedule)
  {
    if (upcomingSchedule == null || upcomingSchedule.Length <= 2)
      return;
    int length = upcomingSchedule.Length;
    int[] numArray = new int[length];
    Array.Fill<int>(numArray, -1);
    Span<int> teamsInConference1 = this.GetTopTeamsInConference(1, 7);
    HashSet<int> intSet1 = new HashSet<int>((IEnumerable<int>) new Span<int>(upcomingSchedule, 0, length / 2).ToArray());
    int num1 = 0;
    int num2 = teamsInConference1.Length - 1;
    int index1 = 0;
    while (index1 < length / 2)
    {
      if (numArray[index1] == -1)
      {
        if (intSet1.Contains(teamsInConference1[num1]))
          numArray[index1] = teamsInConference1[num1];
        ++num1;
      }
      if (numArray[index1 + 1] == -1)
      {
        if (intSet1.Contains(teamsInConference1[num2]))
          numArray[index1 + 1] = teamsInConference1[num2];
        --num2;
      }
      if (numArray[index1] != -1 && numArray[index1 + 1] != -1)
        index1 += 2;
    }
    Span<int> teamsInConference2 = this.GetTopTeamsInConference(2, 7);
    int num3 = 0;
    int num4 = teamsInConference2.Length - 1;
    HashSet<int> intSet2 = new HashSet<int>((IEnumerable<int>) new Span<int>(upcomingSchedule, length / 2, length / 2).ToArray());
    while (index1 < length)
    {
      if (numArray[index1] == -1)
      {
        if (intSet2.Contains(teamsInConference2[num3]))
          numArray[index1] = teamsInConference2[num3];
        ++num3;
      }
      if (numArray[index1 + 1] == -1)
      {
        if (intSet2.Contains(teamsInConference2[num4]))
          numArray[index1 + 1] = teamsInConference2[num4];
        --num4;
      }
      if (numArray[index1] != -1 && numArray[index1 + 1] != -1)
        index1 += 2;
    }
    for (int index2 = 0; index2 < length; ++index2)
      upcomingSchedule[index2] = numArray[index2];
  }

  private void AdvancePlayoffWinner_NFL(
    int[] nextRoundPlayoffSchedule,
    int winningTeam,
    int indexInSchedule)
  {
    if (nextRoundPlayoffSchedule != null)
    {
      if (this.IsFirstRoundOfPlayoffs())
      {
        switch (indexInSchedule)
        {
          case 0:
            nextRoundPlayoffSchedule[1] = winningTeam;
            break;
          case 2:
            nextRoundPlayoffSchedule[2] = winningTeam;
            break;
          case 4:
            nextRoundPlayoffSchedule[3] = winningTeam;
            break;
          case 6:
            nextRoundPlayoffSchedule[5] = winningTeam;
            break;
          case 8:
            nextRoundPlayoffSchedule[6] = winningTeam;
            break;
          case 10:
            nextRoundPlayoffSchedule[7] = winningTeam;
            break;
          default:
            Debug.Log((object) ("Unknown index in schedule specified: " + indexInSchedule.ToString()));
            break;
        }
      }
      else
        nextRoundPlayoffSchedule[indexInSchedule / 2] = winningTeam;
    }
    if (!this.IsFourthRoundOfNFLPlayoffs())
      return;
    this.seasonModeData.leagueChampion = winningTeam;
    if (winningTeam != this.userTeamData.TeamIndex)
      return;
    this.seasonModeData.seasonState = ProEraSeasonState.WonInChampionShip;
  }

  private void SetPlayoffResults()
  {
    string s = "";
    if (this.seasonModeData.UsesTierSystem)
    {
      if (this.seasonModeData.champion_tier1 == this.seasonModeData.UserTeamIndex)
      {
        ++this.seasonModeData.tier1Championships;
        s = "TIER 1 CHAMPIONS";
      }
      else if (this.seasonModeData.champion_tier2 == this.seasonModeData.UserTeamIndex)
      {
        ++this.seasonModeData.tier2Championships;
        s = "TIER 2 CHAMPIONS";
      }
      else if (this.seasonModeData.champion_tier3 == this.seasonModeData.UserTeamIndex)
      {
        ++this.seasonModeData.tier3Championships;
        s = "TIER 3 CHAMPIONS";
      }
      int num1 = this.seasonModeData.playoffR3_tier2[0];
      int num2 = this.seasonModeData.playoffR3_tier2[1];
      int userTeamIndex = this.seasonModeData.UserTeamIndex;
      if (num1 == userTeamIndex || num2 == this.seasonModeData.UserTeamIndex)
        s = this.seasonModeData.winner_tier12Game != this.seasonModeData.UserTeamIndex ? (this.seasonModeData.UsersConference != 1 ? s + "  |  LOST TIER PROMOTION GAME  |  WILL REMAIN IN TIER 2 " : s + "  |  LOST TIER DEMOTION GAME  |  DEMOTED TO TIER 2 ") : (this.seasonModeData.UsersConference != 1 ? s + "  |  WON TIER PROMOTION GAME  |  PROMOTED TO TIER 1 " : s + "  |  WON TIER DEMOTION GAME  |  WILL REMAIN IN TIER 1 ");
      int num3 = this.seasonModeData.playoffR3_tier3[0];
      int num4 = this.seasonModeData.playoffR3_tier3[1];
      if (num3 == this.seasonModeData.UserTeamIndex || num4 == this.seasonModeData.UserTeamIndex)
        s = this.seasonModeData.winner_tier23Game != this.seasonModeData.UserTeamIndex ? (this.seasonModeData.UsersConference != 2 ? s + "  |  LOST TIER PROMOTION GAME  |  WILL REMAIN IN TIER 3 " : s + "  |  LOST TIER DEMOTION GAME  |  DEMOTED TO TIER 3 ") : (this.seasonModeData.UsersConference != 2 ? s + "  |  WON TIER PROMOTION GAME  |  PROMOTED TO TIER 2 " : s + "  |  WON TIER DEMOTION GAME  |  WILL REMAIN IN TIER 2 ");
    }
    else if (this.seasonModeData.leagueChampion == this.seasonModeData.UserTeamIndex)
    {
      ++this.seasonModeData.axisBowls;
      s = "AXIS BOWL CHAMPIONS";
    }
    if (s != "")
      this.seasonModeData.SetEndOfYearText(s);
    this.seasonModeData.seasonOverForUser = true;
    this.FinalizeStatsForSeason();
  }

  public void AddNewPlayerToUsersRoster(PlayerData player)
  {
    int nextOpenRosterIndex1 = this.userTeamData.MainRoster.FindNextOpenRosterIndex();
    if (player.CurrentSeasonStats == null)
      player.CreateNewPlayerStatsForSeason(this.baseYear + this.seasonModeData.seasonNumber, this.userTeamData.GetAbbreviation());
    else
      player.UpdateSeasonStatsWithNewTeamName(this.userTeamData.GetAbbreviation());
    if (nextOpenRosterIndex1 != -1)
    {
      this.userTeamData.AddPlayerToMainRoster(player, nextOpenRosterIndex1);
    }
    else
    {
      int nextOpenRosterIndex2 = this.userTeamData.PracticeSquad.FindNextOpenRosterIndex();
      this.userTeamData.AddPlayerToPracticeSquad(player, nextOpenRosterIndex2);
    }
  }

  public void AddNewPlayerToCompRoster(global::TeamData teamData_AddTo, PlayerData player)
  {
    int nextOpenRosterIndex = teamData_AddTo.MainRoster.FindNextOpenRosterIndex();
    if (nextOpenRosterIndex == -1)
    {
      Debug.Log((object) "Attempting to add a player to a comp team's roster, but there are no availalbe spots.");
    }
    else
    {
      if (player.CurrentSeasonStats == null)
        player.CreateNewPlayerStatsForSeason(this.baseYear + this.seasonModeData.seasonNumber, teamData_AddTo.GetAbbreviation());
      else
        player.UpdateSeasonStatsWithNewTeamName(teamData_AddTo.GetAbbreviation());
      teamData_AddTo.AddPlayerToMainRoster(player, nextOpenRosterIndex);
    }
  }

  private void FinalizeStatsForSeason()
  {
    for (int index = 0; index < this.seasonModeData.NumberOfTeamsInLeague; ++index)
    {
      global::TeamData teamData = this.GetTeamData(this.seasonModeData.TeamIndexMasterList[index]);
      teamData.AddLastYearsStatsToTotals();
      for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
      {
        if (teamData.GetPlayer(playerIndex) != null)
          teamData.GetPlayer(playerIndex).AddSeasonStatsToCareerStats();
      }
      for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_PRACTICE_SQUAD; ++playerIndex)
      {
        if (teamData.GetPlayerOnPracticeSquad(playerIndex) != null)
          teamData.GetPlayerOnPracticeSquad(playerIndex).AddSeasonStatsToCareerStats();
      }
    }
  }

  private void IncreaseSeasonWeekByOne() => this.SetCurrentWeek(this.seasonModeData.currentWeek + 1);

  private void SetCurrentWeek(int weekNum)
  {
    this.seasonModeData.currentWeek = weekNum;
    PersistentData.seasonWeek = this.seasonModeData.currentWeek;
    if (weekNum <= this.seasonModeData.NumberOfWeeksInSeason || this.seasonModeData.seasonState == ProEraSeasonState.LostInPlayoffs || this.seasonModeData.seasonState == ProEraSeasonState.LostInChampionship || this.seasonModeData.seasonState == ProEraSeasonState.WonInChampionShip)
      return;
    Span<int> teamsInConference = this.GetTopTeamsInConference(this.GetConferenceOfTeam(this.userTeamData.TeamIndex), 7);
    bool flag = false;
    for (int index = 0; index < 7; ++index)
    {
      if (teamsInConference[index] == this.userTeamData.TeamIndex)
      {
        flag = true;
        break;
      }
    }
    if (flag)
      this.seasonModeData.seasonState = ProEraSeasonState.InPlayoffs;
    else
      this.seasonModeData.seasonState = ProEraSeasonState.DidNotMakePlayoffs;
  }

  private int FindIndexOfTeamInConference(int _conference, int _teamIndex)
  {
    this._teams = this.seasonModeData.GetTeamsInConference(_conference);
    for (int teamInConference = 0; teamInConference < this._teams.Length; ++teamInConference)
    {
      if (_teamIndex == this._teams[teamInConference])
        return teamInConference;
    }
    Debug.Log((object) ("Team Index: " + _teamIndex.ToString() + " not found in tier: " + _conference.ToString()));
    return -1;
  }

  private void SetEndOfSeasonLostInPlayoffsText()
  {
    if (this.ArePlayoffsOver())
    {
      if (this.seasonModeData.UsesTierSystem)
        this.seasonModeData.SetEndOfYearText("LOST IN THE TIER 1 CHAMPIONSHIP GAME");
      else
        this.seasonModeData.SetEndOfYearText("LOST IN THE AXIS BOWL");
    }
    if (this.IsFirstRoundOfPlayoffs())
    {
      if (this.seasonModeData.UsesTierSystem)
        this.seasonModeData.SetEndOfYearText("LOST IN THE 1ST ROUND OF PLAYOFFS");
      else
        this.seasonModeData.SetEndOfYearText("LOST IN THE WILCARD ROUND");
    }
    else if (this.IsSecondRoundOfPlayoffs())
    {
      if (this.seasonModeData.UsesTierSystem)
      {
        if (this.seasonModeData.UsersConference == 1)
          this.seasonModeData.SetEndOfYearText("LOST IN THE 2ND ROUND OF PLAYOFFS");
        else
          this.seasonModeData.SetEndOfYearText("LOST IN THE TIER " + this.seasonModeData.UsersConference.ToString() + " CHAMPIONSHIP GAME");
      }
      else
        this.seasonModeData.SetEndOfYearText("LOST IN THE DIVISIONAL ROUND");
    }
    else
      this.seasonModeData.SetEndOfYearText("LOST IN THE CONFERENCE CHAMPIONSHIP");
  }

  private void ApplyDataFromSpectatedMatch()
  {
    PersistentData.watchingNonUserMatch = false;
    PersistentData.simulateWeek = false;
    TeamDataCache.SetTeamData(PersistentData.GetUserTeam().TeamIndex, PersistentData.GetUserTeam());
    TeamDataCache.SetTeamData(PersistentData.GetCompTeam().TeamIndex, PersistentData.GetCompTeam());
    this.ApplyGameStatsFromGame();
    this.seasonModeData.SaveTeamData(this.GetTeamData(PersistentData.GetUserTeamIndex()), PersistentData.GetUserTeamIndex());
    this.seasonModeData.SaveTeamData(this.GetTeamData(PersistentData.GetCompTeamIndex()), PersistentData.GetCompTeamIndex());
    if (PersistentData.playoffRound == 0)
    {
      GameSummary[,] scoresByWeek = this.seasonModeData.GetScoresByWeek(this.seasonModeData.UsersConference);
      int teamIndexInSchedule1 = this.FindTeamIndexInSchedule(this.seasonModeData.currentWeek, PersistentData.nonUserMatchTier, PersistentData.GetUserTeamIndex());
      int teamIndexInSchedule2 = this.FindTeamIndexInSchedule(this.seasonModeData.currentWeek, PersistentData.nonUserMatchTier, PersistentData.GetCompTeamIndex());
      scoresByWeek[this.seasonModeData.currentWeek - 1, teamIndexInSchedule1] = PersistentData.userGameSummary;
      scoresByWeek[this.seasonModeData.currentWeek - 1, teamIndexInSchedule2] = PersistentData.compGameSummary;
      if (this.seasonModeData.offPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] == null)
        this.seasonModeData.offPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] = PersistentData.offPlayerOfTheGame;
      else if ((double) PersistentData.offPlayerOfTheGame.statScore > (double) this.seasonModeData.offPlayersOfTheWeek[this.seasonModeData.currentWeek - 1].statScore)
        this.seasonModeData.offPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] = PersistentData.offPlayerOfTheGame;
      if (this.seasonModeData.defPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] == null)
        this.seasonModeData.defPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] = PersistentData.defPlayerOfTheGame;
      else if ((double) PersistentData.defPlayerOfTheGame.statScore > (double) this.seasonModeData.defPlayersOfTheWeek[this.seasonModeData.currentWeek - 1].statScore)
        this.seasonModeData.defPlayersOfTheWeek[this.seasonModeData.currentWeek - 1] = PersistentData.defPlayerOfTheGame;
    }
    else
    {
      GameSummary[] gameSummaryArray = (GameSummary[]) null;
      int[] nextRoundPlayoffSchedule = (int[]) null;
      if (this.seasonModeData.UsesTierSystem)
      {
        switch (PersistentData.playoffRound)
        {
          case 1:
            gameSummaryArray = this.seasonModeData.GetPlayoffScoresByTier_R1(PersistentData.nonUserMatchTier);
            nextRoundPlayoffSchedule = this.seasonModeData.GetPlayoffScheduleByTier_R2(PersistentData.nonUserMatchTier);
            break;
          case 2:
            gameSummaryArray = this.seasonModeData.GetPlayoffScoresByTier_R2(PersistentData.nonUserMatchTier);
            nextRoundPlayoffSchedule = this.seasonModeData.GetPlayoffScheduleByTier_R3(PersistentData.nonUserMatchTier);
            break;
          case 3:
            gameSummaryArray = this.seasonModeData.GetPlayoffScoresByTier_R3(PersistentData.nonUserMatchTier);
            nextRoundPlayoffSchedule = (int[]) null;
            break;
        }
      }
      else
      {
        switch (PersistentData.playoffRound)
        {
          case 1:
            gameSummaryArray = this.seasonModeData.GetPlayoffScoresByTier_R1(PersistentData.nonUserMatchTier);
            nextRoundPlayoffSchedule = this.seasonModeData.GetPlayoffScheduleByTier_R2(PersistentData.nonUserMatchTier);
            break;
          case 2:
            gameSummaryArray = this.seasonModeData.GetPlayoffScoresByTier_R2(PersistentData.nonUserMatchTier);
            nextRoundPlayoffSchedule = this.seasonModeData.GetPlayoffScheduleByTier_R3(PersistentData.nonUserMatchTier);
            break;
          case 3:
            gameSummaryArray = this.seasonModeData.GetPlayoffScoresByTier_R3(PersistentData.nonUserMatchTier);
            nextRoundPlayoffSchedule = this.seasonModeData.GetPlayoffSchedule_R4();
            break;
          case 4:
            gameSummaryArray = this.seasonModeData.GetPlayoffScores_R4();
            break;
        }
      }
      int inPlayoffSchedule1 = this.FindTeamIndexInPlayoffSchedule(PersistentData.playoffRound, PersistentData.nonUserMatchTier, PersistentData.GetUserTeamIndex());
      int inPlayoffSchedule2 = this.FindTeamIndexInPlayoffSchedule(PersistentData.playoffRound, PersistentData.nonUserMatchTier, PersistentData.GetCompTeamIndex());
      gameSummaryArray[inPlayoffSchedule1] = PersistentData.userGameSummary;
      gameSummaryArray[inPlayoffSchedule2] = PersistentData.compGameSummary;
      int inPlayoffSchedule3 = this.FindTeamIndexInPlayoffSchedule(PersistentData.playoffRound, PersistentData.nonUserMatchTier, PersistentData.userIsHome ? PersistentData.GetUserTeamIndex() : PersistentData.GetCompTeamIndex());
      int winningTeam = PersistentData.userGameSummary.TeamGameStats.Score <= PersistentData.compGameSummary.TeamGameStats.Score ? PersistentData.GetCompTeamIndex() : PersistentData.GetUserTeamIndex();
      this.AdvancePlayoffWinner_NFL(nextRoundPlayoffSchedule, winningTeam, inPlayoffSchedule3);
    }
    AppEvents.SaveSeasonMode.Trigger();
  }

  private void ApplyDataFromUserMatch()
  {
    this.seasonModeData.SaveTeamData(PersistentData.GetUserTeam(), PersistentData.GetUserTeamIndex());
    this.seasonModeData.SaveTeamData(PersistentData.GetCompTeam(), PersistentData.GetCompTeamIndex());
    TeamDataCache.SetTeamData(PersistentData.GetUserTeam().TeamIndex, PersistentData.GetUserTeam());
    TeamDataCache.SetTeamData(PersistentData.GetCompTeam().TeamIndex, PersistentData.GetCompTeam());
    this.userTeamData = PersistentData.GetUserTeam();
  }

  private void ApplyGameStatsFromGame(bool seasonGame = true)
  {
    int teamIndex1 = PersistentData.GetUserTeam().TeamIndex;
    int teamIndex2 = PersistentData.GetCompTeam().TeamIndex;
    global::TeamData teamData1 = this.GetTeamData(teamIndex1);
    global::TeamData teamData2 = this.GetTeamData(teamIndex2);
    TeamSeasonStats teamSeasonStats1 = teamData1.CurrentSeasonStats;
    TeamSeasonStats teamSeasonStats2 = teamData2.CurrentSeasonStats;
    if (!seasonGame)
    {
      teamSeasonStats1 = new TeamSeasonStats();
      teamSeasonStats2 = new TeamSeasonStats();
    }
    TeamStatGameType seasonGameType = this.GetSeasonGameType(teamData1, teamData2);
    StatSet statSet = teamSeasonStats1.AddStatsFromGame(teamData1.CurrentGameStats, teamData2.CurrentGameStats, seasonGameType, teamData2.TeamIndex, this.userTeamData);
    teamSeasonStats2.AddStatsFromGame(teamData2.CurrentGameStats, teamData1.CurrentGameStats, seasonGameType, teamData1.TeamIndex, teamData2);
    this._careerStats += statSet;
    this.achievementData.SyncPerGameAchievements(!seasonGame);
    teamData1.AddPlayerStatsFromGame();
    teamData2.AddPlayerStatsFromGame();
  }

  public void LoadSeasonData()
  {
    Debug.Log((object) ("LoadSeasonData() started " + Time.realtimeSinceStartup.ToString()));
    this.seasonModeData = PersistentSingleton<SaveManager>.Instance.seasonModeData;
    this._careerStats = PersistentSingleton<SaveManager>.Instance.careerStats.Stats;
    PersistentData.gameType = GameType.SeasonMode;
    PersistentData.quarterLength = this.seasonModeData.quarterPref;
    PersistentData.seasonWeek = this.seasonModeData.currentWeek;
    Debug.Log((object) string.Format("<b>{0} IS INDEX</b>", (object) this.seasonModeData.UserTeamIndex));
    try
    {
      this.userTeamData = this.GetTeamData(this.seasonModeData.UserTeamIndex);
    }
    catch (NullReferenceException ex)
    {
      Debug.Log((object) "Could not find season mode data!");
    }
    Debug.Log((object) ("LoadSeasonData() completed " + Time.realtimeSinceStartup.ToString()));
  }

  public void UICreateNewSeason()
  {
    this.routineNewSeason.Stop();
    this.routineNewSeason.Run(this.CreateNewSeasonTransition(1f));
  }

  public void UIRequestLogin()
  {
    this.routineNewSeason.Stop();
    this.routineNewSeason.Run(this.RequestLogin(1f));
  }

  private IEnumerator CreateNewSeasonTransition(float blinkTime)
  {
    yield return (object) GamePlayerController.CameraFade.Fade(blinkTime / 2f);
    UIDispatch.FrontScreen.DisplayView(EScreens.kSeasonTeamSelect);
  }

  private IEnumerator RequestLogin(float blinkTime)
  {
    yield return (object) GamePlayerController.CameraFade.Fade(blinkTime / 2f);
    PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(new Vector3(-2f, 0.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 90f, 0.0f)));
    VREvents.PlayerPositionUpdated.Trigger();
    AppState.SeasonMode.Value = ESeasonMode.kNew;
    yield return (object) new WaitForSeconds(0.5f);
    if (!PersistentSingleton<PlayerApi>.Instance.IsLoggedIn)
    {
      bool waitingForAccountCreation = true;
      PlayerApi.LoginFailure += (System.Action) (() => waitingForAccountCreation = false);
      PlayerApi.LoginSuccess += (Action<SaveKeycloakUserData>) (_ => waitingForAccountCreation = false);
      PlayerApi.CreateUserFailure += (System.Action) (() => waitingForAccountCreation = false);
      PlayerApi.CreateUserSuccess += (Action<SaveKeycloakUserData>) (_ => waitingForAccountCreation = false);
      PersistentSingleton<PlayerApi>.Instance.CreateUser();
      TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.Loading, false);
      GamePlayerController.CameraFade.Clear();
      while (waitingForAccountCreation)
        yield return (object) null;
      UIDispatch.FrontScreen.HideView(EScreens.kSettings);
    }
    yield return (object) GamePlayerController.CameraFade.Clear(blinkTime / 2f);
    if (!PersistentSingleton<SaveManager>.Instance.gameSettings._didCompleteTutorial)
    {
      VRState.LocomotionEnabled.SetValue(false);
      AppSounds.PlayVO(EVOTypes.kLamarLockerRoomPostCAP);
      yield return (object) new WaitForSeconds(3.9f);
      GameplayManager.LoadLevelActivation(EGameMode.kOnboarding, ETimeOfDay.Clear);
    }
    else
      VRState.LocomotionEnabled.SetValue(true);
  }

  public void SortTeams()
  {
    if (this.sortedTeams == null)
      return;
    for (int index = 0; index < this.seasonModeData.NumberOfTeamsInLeague; ++index)
      this.sortedTeams[index] = this.seasonModeData.TeamIndexMasterList[index];
    Dictionary<int, TeamSeasonStats> dictionary = new Dictionary<int, TeamSeasonStats>();
    for (int index = 0; index < this.sortedTeams.Length; ++index)
    {
      int sortedTeam = this.sortedTeams[index];
      dictionary.Add(sortedTeam, this.GetTeamData(sortedTeam).CurrentSeasonStats);
    }
    for (int index1 = 0; index1 < this.sortedTeams.Length - 1; ++index1)
    {
      int index2 = index1;
      int num1 = dictionary[this.sortedTeams[index1]].wins;
      int num2 = dictionary[this.sortedTeams[index1]].teamPlusMinus;
      for (int index3 = index1 + 1; index3 < this.seasonModeData.NumberOfTeamsInLeague; ++index3)
      {
        int wins = dictionary[this.sortedTeams[index3]].wins;
        int teamPlusMinus = dictionary[this.sortedTeams[index3]].teamPlusMinus;
        if (wins > num1 || wins == num1 && teamPlusMinus > num2)
        {
          index2 = index3;
          num1 = wins;
          num2 = teamPlusMinus;
        }
      }
      int sortedTeam = this.sortedTeams[index1];
      this.sortedTeams[index1] = this.sortedTeams[index2];
      this.sortedTeams[index2] = sortedTeam;
    }
  }

  public int[] GetSortedTeamsInLeague()
  {
    int[] sortedTeamsInLeague = new int[this.seasonModeData.NumberOfTeamsInLeague];
    for (int index = 0; index < this.seasonModeData.NumberOfTeamsInLeague; ++index)
      sortedTeamsInLeague[index] = this.sortedTeams[index];
    return sortedTeamsInLeague;
  }

  public int[] GetSortedTeamsInConference(int conference)
  {
    int[] teamsInConference = new int[this.seasonModeData.NumberOfTeamsPerConference];
    int index1 = 0;
    for (int index2 = 0; index2 < this.seasonModeData.NumberOfTeamsInLeague; ++index2)
    {
      if (this.IsTeamInConference(this.sortedTeams[index2], conference))
      {
        teamsInConference[index1] = this.sortedTeams[index2];
        ++index1;
      }
    }
    return teamsInConference;
  }

  public Span<int> GetTopTeamsInConference(int conference, int numTeams)
  {
    this.SortTeams();
    return new Span<int>(this.GetSortedTeamsInConference(conference), 0, numTeams);
  }

  public int[] GetSortedTeamsInDivision(int conference, int division)
  {
    int[] sortedTeamsInDivision = new int[this.seasonModeData.NumberOfTeamsPerDivision];
    int index1 = 0;
    for (int index2 = 0; index2 < this.seasonModeData.NumberOfTeamsInLeague; ++index2)
    {
      if (this.IsTeamInConferenceDivision(this.sortedTeams[index2], conference, division))
      {
        sortedTeamsInDivision[index1] = this.sortedTeams[index2];
        ++index1;
      }
    }
    return sortedTeamsInDivision;
  }

  public int GetTeamRankInConference(int teamIndex)
  {
    int conferenceOfTeam = this.GetConferenceOfTeam(teamIndex);
    this.SortTeams();
    int[] teamsInConference = this.GetSortedTeamsInConference(conferenceOfTeam);
    for (int rankInConference = 0; rankInConference < teamsInConference.Length; ++rankInConference)
    {
      if (teamsInConference[rankInConference] == teamIndex)
        return rankInConference;
    }
    Debug.Log((object) "Team not found in tier. GetTeamRankInTier (int teamIndex)");
    return -1;
  }

  public int GetTeamRankInLeague(int teamIndex)
  {
    this.SortTeams();
    for (int index = 0; index < this.sortedTeams.Length; ++index)
    {
      if (teamIndex == this.sortedTeams[index])
        return index + 1;
    }
    Debug.Log((object) ("Unable to find team index: " + teamIndex.ToString() + " in GetTeamRankInLeague ()"));
    return -1;
  }

  public bool IsTeamInConference(int teamIndex, int conference)
  {
    foreach (int num in this.seasonModeData.GetTeamsInConference(conference))
    {
      if (num == teamIndex)
        return true;
    }
    return false;
  }

  public bool IsTeamInConferenceDivision(int teamIndex, int conference, int division)
  {
    int indexInConference = this.GetTeamIndexInConference(teamIndex, conference);
    return indexInConference != -1 && this.GetTeamDivision(indexInConference) == division;
  }

  public int GetTeamDivision(int teamIndexInConference) => Mathf.FloorToInt((float) (teamIndexInConference / this.seasonModeData.NumberOfTeamsPerDivision)) + 1;

  public int GetConferenceOfTeam(int teamIndex)
  {
    if (this.IsTeamInConference(teamIndex, 1))
      return 1;
    return this.IsTeamInConference(teamIndex, 2) ? 2 : 3;
  }

  public int GetTeamIndexInLeague(int teamIndex)
  {
    for (int teamIndexInLeague = 0; teamIndexInLeague < this.seasonModeData.TeamIndexMasterList.Length; ++teamIndexInLeague)
    {
      if (teamIndex == this.seasonModeData.TeamIndexMasterList[teamIndexInLeague])
        return teamIndexInLeague;
    }
    Debug.Log((object) ("Team not found in master list: " + teamIndex.ToString()));
    return -1;
  }

  public int GetTeamIndexInConference(int teamIndex, int conference)
  {
    int[] teamsInConference = this.seasonModeData.GetTeamsInConference(conference);
    for (int indexInConference = 0; indexInConference < teamsInConference.Length; ++indexInConference)
    {
      if (teamsInConference[indexInConference] == teamIndex)
        return indexInConference;
    }
    return -1;
  }

  public int FindTeamInWeek(int teamIndex, int week)
  {
    int week1 = week;
    int[,] scheduleByTier = this.seasonModeData.GetScheduleByTier(this.seasonModeData.UsersConference);
    int num = this.seasonModeData.MaxNumberOfGamesPerWeek * 2;
    if (week1 <= this.seasonModeData.NumberOfWeeksInSeason)
    {
      for (int teamInWeek = 0; teamInWeek < num; ++teamInWeek)
      {
        if (scheduleByTier[week1 - 1, teamInWeek] == teamIndex)
          return teamInWeek;
      }
    }
    else if (week1 < this.seasonModeData.NumberOfWeeksInSeason + this.seasonModeData.NumberOfWeeksInPlayoffs + 1)
    {
      int[] numArray = (int[]) null;
      if (this.seasonModeData.UsesTierSystem)
      {
        if (this.IsFirstRoundOfPlayoffs(week1))
          numArray = this.seasonModeData.GetPlayoffScheduleByTier_R1(this.seasonModeData.UsersConference);
        else if (this.IsSecondRoundOfPlayoffs(week1))
          numArray = this.seasonModeData.GetPlayoffScheduleByTier_R2(this.seasonModeData.UsersConference);
        else if (this.IsThirdRoundOfPlayoffs(week1))
          numArray = !this.seasonModeData.waitingOnDemotionGame ? this.seasonModeData.GetPlayoffScheduleByTier_R3(this.seasonModeData.UsersConference) : this.seasonModeData.GetPlayoffScheduleByTier_R3(this.seasonModeData.UsersConference + 1);
      }
      else
        numArray = !this.IsFirstRoundOfPlayoffs(week1) ? (!this.IsSecondRoundOfPlayoffs(week1) ? (!this.IsThirdRoundOfPlayoffs(week1) ? this.seasonModeData.GetPlayoffSchedule_R4() : this.seasonModeData.GetPlayoffScheduleByTier_R3(this.seasonModeData.UsersConference)) : this.seasonModeData.GetPlayoffScheduleByTier_R2(this.seasonModeData.UsersConference)) : this.seasonModeData.GetPlayoffScheduleByTier_R1(this.seasonModeData.UsersConference);
      for (int teamInWeek = 0; teamInWeek < numArray.Length; ++teamInWeek)
      {
        if (numArray[teamInWeek] == teamIndex)
          return teamInWeek;
      }
    }
    return -1;
  }

  public int FindTeamIndexInSchedule(int week, int tier, int teamIndex)
  {
    int[,] scheduleByTier = this.seasonModeData.GetScheduleByTier(tier);
    if (week <= this.seasonModeData.NumberOfWeeksInSeason)
    {
      for (int teamIndexInSchedule = 0; teamIndexInSchedule < this.seasonModeData.MaxNumberOfGamesPerWeek * 2; ++teamIndexInSchedule)
      {
        if (scheduleByTier[week - 1, teamIndexInSchedule] == teamIndex)
          return teamIndexInSchedule;
      }
      Debug.Log((object) ("Unable to find team index in week. Week: " + week.ToString() + " Tier: " + tier.ToString() + " Team Index: " + teamIndex.ToString()));
      return -1;
    }
    Debug.Log((object) "Cannot get the index of a AIvsAI match during a playoff game");
    return -1;
  }

  public int FindTeamIndexInPlayoffSchedule(int playoffRound, int tier, int teamIndex) => this.FindTeamIndexInPlayoffSchedule(playoffRound, tier, teamIndex, out int _);

  public int FindTeamIndexInPlayoffSchedule(
    int playoffRound,
    int tier,
    int teamIndex,
    out int opponentTeamIndex)
  {
    int[] numArray = (int[]) null;
    switch (playoffRound)
    {
      case 1:
        numArray = this.seasonModeData.GetPlayoffScheduleByTier_R1(tier);
        break;
      case 2:
        numArray = this.seasonModeData.GetPlayoffScheduleByTier_R2(tier);
        break;
      case 3:
        numArray = this.seasonModeData.GetPlayoffScheduleByTier_R3(tier);
        break;
      case 4:
        numArray = this.seasonModeData.GetPlayoffSchedule_R4();
        break;
    }
    for (int index = 0; index < numArray.Length; ++index)
    {
      if (numArray[index] == teamIndex)
      {
        int inPlayoffSchedule = index % 2 == 0 ? index + 1 : index - 1;
        opponentTeamIndex = numArray[inPlayoffSchedule];
        return inPlayoffSchedule;
      }
    }
    Debug.Log((object) ("Unable to find team index in playoff schedule week. Round: " + playoffRound.ToString() + " Tier: " + tier.ToString() + " Team Index: " + teamIndex.ToString()));
    opponentTeamIndex = -1;
    return -1;
  }

  public TeamStatGameType GetSeasonGameType(global::TeamData a, global::TeamData b)
  {
    int[] numArray1 = this.seasonModeData.TeamsInConferencesByTeamIndex[a.TeamIndex.ToString()];
    int[] numArray2 = this.seasonModeData.TeamsInConferencesByTeamIndex[b.TeamIndex.ToString()];
    if (numArray1[0] != numArray2[0])
      return TeamStatGameType.NonConference;
    return numArray1[1] / 4 != numArray2[1] / 4 ? TeamStatGameType.Conference : TeamStatGameType.Division;
  }

  public bool InRegularSeason() => this.seasonModeData.currentWeek <= this.seasonModeData.NumberOfWeeksInSeason;

  public bool IsSeasonOver() => this.seasonModeData.currentWeek > this.seasonModeData.NumberOfWeeksInSeason + this.seasonModeData.NumberOfWeeksInPlayoffs;

  public bool IsFirstRoundOfPlayoffs(int week = -2)
  {
    if (week == -2)
      week = this.seasonModeData.currentWeek;
    return week == this.seasonModeData.NumberOfWeeksInSeason + 1;
  }

  public bool IsSecondRoundOfPlayoffs(int week = -2)
  {
    if (week == -2)
      week = this.seasonModeData.currentWeek;
    return week == this.seasonModeData.NumberOfWeeksInSeason + 2;
  }

  public bool IsThirdRoundOfPlayoffs(int week = -2)
  {
    if (week == -2)
      week = this.seasonModeData.currentWeek;
    return week == this.seasonModeData.NumberOfWeeksInSeason + 3;
  }

  public bool IsFourthRoundOfNFLPlayoffs() => this.seasonModeData.NumberOfWeeksInSeason > 0 && this.seasonModeData.currentWeek == this.seasonModeData.NumberOfWeeksInSeason + this.seasonModeData.NumberOfWeeksInPlayoffs;

  public bool ArePlayoffsOver() => !this.seasonModeData.UsesTierSystem ? this.IsFourthRoundOfNFLPlayoffs() : this.IsThirdRoundOfPlayoffs();

  public SeasonModeGameRound GetGameRound(int week)
  {
    if (week <= this.seasonModeData.NumberOfWeeksInSeason)
      return SeasonModeGameRound.RegularSeason;
    if (this.IsFirstRoundOfPlayoffs())
      return SeasonModeGameRound.WildCard;
    if (this.IsSecondRoundOfPlayoffs())
      return SeasonModeGameRound.DivisionalRound;
    if (this.IsThirdRoundOfPlayoffs())
      return SeasonModeGameRound.ConferenceChampionship;
    return this.IsFourthRoundOfNFLPlayoffs() ? SeasonModeGameRound.SuperBowl : SeasonModeGameRound.Invalid;
  }

  public SeasonModeGameRound GetCurrentGameRound() => this.seasonModeData != null ? this.GetGameRound(this.seasonModeData.currentWeek) : SeasonModeGameRound.Invalid;

  public int GetTeamOpponentForWeek(
    int teamIndex,
    int week,
    out int teamIndexInWeek,
    out int opponentIndexInWeek)
  {
    int[,] scheduleByTier = SeasonModeManager.self.seasonModeData.GetScheduleByTier();
    teamIndexInWeek = SeasonModeManager.self.FindTeamInWeek(teamIndex, week);
    if (teamIndexInWeek >= 0)
    {
      opponentIndexInWeek = teamIndexInWeek % 2 != 0 ? teamIndexInWeek - 1 : teamIndexInWeek + 1;
      int opponentTeamIndex;
      if (this.seasonModeData.currentWeek > this.seasonModeData.NumberOfWeeksInSeason + this.seasonModeData.NumberOfWeeksInPlayoffs)
        opponentTeamIndex = -1;
      else if (this.seasonModeData.currentWeek > this.seasonModeData.NumberOfWeeksInSeason)
        this.FindTeamIndexInPlayoffSchedule(this.seasonModeData.currentWeek - this.seasonModeData.NumberOfWeeksInSeason, 0, teamIndex, out opponentTeamIndex);
      else
        opponentTeamIndex = scheduleByTier[week - 1, opponentIndexInWeek];
      return opponentTeamIndex;
    }
    opponentIndexInWeek = -1;
    return -1;
  }

  public SeasonModeTeamGameResults GetSeasonTeamGameResults(int teamIndex)
  {
    GameSummary[,] scoresByWeek = SeasonModeManager.self.seasonModeData.GetScoresByWeek(SeasonModeManager.self.seasonModeData.UsersConference);
    SeasonModeTeamGameResults seasonTeamGameResults = new SeasonModeTeamGameResults();
    seasonTeamGameResults.teamIndex = teamIndex;
    global::TeamData teamData = SeasonModeManager.self.GetTeamData(teamIndex);
    if (teamData == null)
      return (SeasonModeTeamGameResults) null;
    string abbreviation1 = teamData.GetAbbreviation();
    seasonTeamGameResults.teamAbbrev = abbreviation1;
    int currentWeek = SeasonModeManager.self.seasonModeData.currentWeek;
    for (int week = 1; week <= SeasonModeManager.self.seasonModeData.NumberOfWeeksInSeason; ++week)
    {
      SeasonModeGameInfo seasonModeGameInfo = new SeasonModeGameInfo();
      seasonModeGameInfo.week = week;
      int teamIndexInWeek;
      int opponentIndexInWeek;
      int teamOpponentForWeek = SeasonModeManager.self.GetTeamOpponentForWeek(teamIndex, week, out teamIndexInWeek, out opponentIndexInWeek);
      if (teamOpponentForWeek >= 0 && teamIndexInWeek >= 0)
      {
        int num1 = teamIndexInWeek % 2 == 0 ? 1 : 0;
        string abbreviation2 = SeasonModeManager.self.GetTeamData(teamOpponentForWeek).GetAbbreviation();
        int num2 = 0;
        int num3 = 0;
        if (week < currentWeek)
        {
          num2 = scoresByWeek[week - 1, teamIndexInWeek].TeamGameStats.Score;
          num3 = scoresByWeek[week - 1, opponentIndexInWeek].TeamGameStats.Score;
        }
        if (num1 != 0)
        {
          seasonModeGameInfo.homeTeamIndex = teamIndex;
          seasonModeGameInfo.awayTeamIndex = teamOpponentForWeek;
          seasonModeGameInfo.homeTeamAbbrev = abbreviation1;
          seasonModeGameInfo.awayTeamAbbrev = abbreviation2;
          seasonModeGameInfo.homeScore = num2;
          seasonModeGameInfo.awayScore = num3;
        }
        else
        {
          seasonModeGameInfo.homeTeamIndex = teamOpponentForWeek;
          seasonModeGameInfo.awayTeamIndex = teamIndex;
          seasonModeGameInfo.homeTeamAbbrev = abbreviation2;
          seasonModeGameInfo.awayTeamAbbrev = abbreviation1;
          seasonModeGameInfo.homeScore = num3;
          seasonModeGameInfo.awayScore = num2;
        }
      }
      else
      {
        seasonModeGameInfo.homeTeamIndex = -1;
        seasonModeGameInfo.awayTeamIndex = -1;
        seasonModeGameInfo.homeTeamAbbrev = "";
        seasonModeGameInfo.awayTeamAbbrev = "";
        seasonModeGameInfo.homeScore = 0;
        seasonModeGameInfo.awayScore = 0;
      }
      seasonTeamGameResults.games.Add(seasonModeGameInfo);
    }
    return seasonTeamGameResults;
  }

  public List<SeasonModeGameInfo> GetGameResultsForWeek(int week)
  {
    GameSummary[,] scoresByWeek = SeasonModeManager.self.seasonModeData.GetScoresByWeek(0);
    List<SeasonModeGameInfo> gameResultsForWeek = new List<SeasonModeGameInfo>();
    SeasonModeManager self = SeasonModeManager.self;
    if (self.InRegularSeason())
    {
      SeasonModeGameInfo seasonModeGameInfo = new SeasonModeGameInfo();
      for (int index = 0; index < this.seasonModeData.MaxNumberOfGamesPerWeek * 2; index += 2)
      {
        int teamIndex1 = this.seasonModeData.leagueSchedule[week - 1, index];
        int teamIndex2 = this.seasonModeData.leagueSchedule[week - 1, index + 1];
        if (teamIndex1 >= 0 && teamIndex2 >= 0)
        {
          seasonModeGameInfo.homeTeamIndex = teamIndex1;
          seasonModeGameInfo.awayTeamIndex = teamIndex2;
          seasonModeGameInfo.homeTeamAbbrev = this.GetTeamData(teamIndex1).GetAbbreviation();
          seasonModeGameInfo.awayTeamAbbrev = this.GetTeamData(teamIndex2).GetAbbreviation();
          if (scoresByWeek[week - 1, index] != null)
          {
            seasonModeGameInfo.homeScore = scoresByWeek[week - 1, index].TeamGameStats.Score;
            seasonModeGameInfo.awayScore = scoresByWeek[week - 1, index + 1].TeamGameStats.Score;
          }
          gameResultsForWeek.Add(seasonModeGameInfo);
        }
      }
    }
    else
    {
      int[] numArray = Array.Empty<int>();
      GameSummary[] gameSummaryArray = Array.Empty<GameSummary>();
      if (self.IsFirstRoundOfPlayoffs())
      {
        numArray = this.seasonModeData.playoffsR1_league;
        gameSummaryArray = this.seasonModeData.scoresPlayoffR1_league;
      }
      else if (self.IsSecondRoundOfPlayoffs())
      {
        numArray = this.seasonModeData.playoffsR2_league;
        gameSummaryArray = this.seasonModeData.scoresPlayoffR2_league;
      }
      else if (self.IsThirdRoundOfPlayoffs())
      {
        numArray = this.seasonModeData.playoffsR3_league;
        gameSummaryArray = this.seasonModeData.scoresPlayoffR3_league;
      }
      else if (self.IsFourthRoundOfNFLPlayoffs())
      {
        numArray = this.seasonModeData.playoffsR4_league;
        gameSummaryArray = this.seasonModeData.scoresPlayoffR4_league;
      }
      for (int index = 0; index < numArray.Length; index += 2)
      {
        SeasonModeGameInfo seasonModeGameInfo = new SeasonModeGameInfo();
        int teamIndex3 = numArray[index];
        int teamIndex4 = numArray[index + 1];
        if (teamIndex3 >= 0 && teamIndex4 >= 0)
        {
          seasonModeGameInfo.homeTeamIndex = teamIndex3;
          seasonModeGameInfo.awayTeamIndex = teamIndex4;
          seasonModeGameInfo.homeTeamAbbrev = this.GetTeamData(teamIndex3).GetAbbreviation();
          seasonModeGameInfo.awayTeamAbbrev = this.GetTeamData(teamIndex4).GetAbbreviation();
          if (gameSummaryArray[index] != null)
          {
            seasonModeGameInfo.homeScore = gameSummaryArray[index].TeamGameStats.Score;
            seasonModeGameInfo.awayScore = gameSummaryArray[index + 1].TeamGameStats.Score;
          }
          gameResultsForWeek.Add(seasonModeGameInfo);
        }
      }
    }
    return gameResultsForWeek;
  }

  public static void PrintTeamSchedule(int teamIndex) => SeasonModeManager.self.GetSeasonTeamGameResults(teamIndex).Print();

  public static void PrintWeekSchedule(int week)
  {
    List<SeasonModeGameInfo> gameResultsForWeek = SeasonModeManager.self.GetGameResultsForWeek(week);
    Debug.Log((object) string.Format(" Schedule for week {0}:", (object) week));
    foreach (SeasonModeGameInfo seasonModeGameInfo in gameResultsForWeek)
      Debug.Log((object) ("   " + seasonModeGameInfo.homeTeamAbbrev + " vs " + seasonModeGameInfo.awayTeamAbbrev + " "));
  }

  public HashSet<int> GetSuperBowlWinningTeams()
  {
    if (this._superBowlWinningTeams == null)
    {
      this._superBowlWinningTeams = new HashSet<int>();
      int count = this.userTeamData.AllSeasonStats.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.userTeamData.AllSeasonStats[index].IsSuperbowlWinner)
          this._superBowlWinningTeams.Add(this.userTeamData.AllSeasonStats[index].TeamIndex);
      }
    }
    return this._superBowlWinningTeams;
  }

  public void SetSaveData(SGD_SeasonModeData saveData) => this.seasonModeData = saveData;

  public static void ShowDebugSeasonModeInfo()
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self || SeasonModeManager.self.seasonModeData == null)
      return;
    Debug.Log((object) "=====================================Current Season Mode Info=======================================");
    Debug.Log((object) ("Slot = " + PersistentData.saveSlot + " Team = " + SeasonModeManager.self.userTeamData.GetAbbreviation()));
    Debug.Log((object) string.Format("Year = {0} Current Week = {1}", (object) SeasonModeManager.self.GetCurrentYear(), (object) PersistentData.seasonWeek));
    SeasonModeManager.PrintTeamSchedule(PersistentData.GetUserTeamIndex());
  }

  public static void DebugSimulateWeek(bool forceWin = false, bool saveSeason = true)
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self || SeasonModeManager.self.seasonModeData == null)
      return;
    SeasonModeManager.self.SimulateWeek(true, forceWin ? SeasonModeManager.ForcedSimResult.UserWin : SeasonModeManager.ForcedSimResult.Random, saveSeason);
    PersistentSingleton<StateManager<EAppState, GameState>>.Instance.ForceReloadState();
  }

  public static void DebugSimulateWeeks(int numberOfWeeks = 1, bool forceWin = false)
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self || SeasonModeManager.self.seasonModeData == null)
      return;
    if (numberOfWeeks == -1)
      numberOfWeeks = SeasonModeManager.self.seasonModeData.NumberOfWeeksInSeason + SeasonModeManager.self.seasonModeData.NumberOfWeeksInPlayoffs;
    for (int index = 0; index < numberOfWeeks; ++index)
      SeasonModeManager.self.SimulateWeek(true, forceWin ? SeasonModeManager.ForcedSimResult.UserWin : SeasonModeManager.ForcedSimResult.Random, false);
    Debug.Log((object) "SIMULATING MATCHES");
    AppEvents.SaveSeasonMode.Trigger();
    PersistentSingleton<StateManager<EAppState, GameState>>.Instance.ForceReloadState();
  }

  public static void DebugSimulateSeason(bool forceWin = false) => SeasonModeManager.DebugSimulateSeason(forceWin ? SeasonModeManager.ForcedSimResult.UserWin : SeasonModeManager.ForcedSimResult.Random);

  public static void DebugSimulateSeason(SeasonModeManager.ForcedSimResult simResult)
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self || SeasonModeManager.self.seasonModeData == null)
      return;
    for (int currentWeek = SeasonModeManager.self.seasonModeData.currentWeek; currentWeek < SeasonModeManager.self.seasonModeData.NumberOfWeeksInSeason + 1; ++currentWeek)
      SeasonModeManager.self.SimulateWeek(true, simResult, false);
    Debug.Log((object) "SIMULATING MATCHES");
    AppEvents.SaveSeasonMode.Trigger();
    PersistentSingleton<StateManager<EAppState, GameState>>.Instance.ForceReloadState();
  }

  public static void DebugPrintTeamStats(int teamIndex)
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self || SeasonModeManager.self.seasonModeData == null)
      return;
    SeasonModeManager.self.GetTeamData(teamIndex).CurrentSeasonStats.Print();
  }

  private void OnDestroy() => this.routineNewSeason.Stop();

  public enum ForcedSimResult
  {
    Random,
    UserWin,
    UserLoss,
  }
}
