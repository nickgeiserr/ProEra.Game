// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Achievements.AchievementData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ProEra.Game.Achievements
{
  [CreateAssetMenu(menuName = "Achievements/AchievementData")]
  [Serializable]
  public class AchievementData : ScriptableObject
  {
    private static readonly int MaxTeamCount = 32;
    private static readonly string SuperBowlAward = "{0} Ring";
    private static readonly string DefeatedTeamAward = "{0} Football";
    [NonSerialized]
    private bool _isInitialized;

    private SaveAchievements saveData => PersistentSingleton<SaveManager>.Instance.GetSaveAchievements();

    public Dictionary<string, Achievement> Achievements => this.saveData.Achievements;

    public Dictionary<string, AcknowledgeableAward> TeamsDefeatedByIndex => this.saveData.TeamsDefeatedByIndex;

    public Dictionary<string, AcknowledgeableAward> SuperBowlAwardsByTeam => this.saveData.SuperBowlAwardsByTeam;

    public int AchievementScore => this.GetAchievementScore();

    public UnityEvent OnLoadComplete { get; private set; } = new UnityEvent();

    public void Init()
    {
      if (this._isInitialized)
        return;
      this._isInitialized = true;
      int num1 = this.InitializeAchievementDictionary() ? 1 : 0;
      bool flag1 = this.InitializeDefeatedTeamsAwards();
      bool flag2 = this.InitializeSuperBowlAwards();
      int num2 = flag1 ? 1 : 0;
      if ((num1 | num2 | (flag2 ? 1 : 0)) == 0)
        return;
      this.Save();
    }

    public void SyncPerSeasonAchievements()
    {
      this.Achievements[(string) Achievement.Names.DartThrower].CurrentValue += SeasonModeManager.self.userTeamData.CurrentSeasonTouchdownPasses >= 20 ? 1 : 0;
      int careerSuperBowlWins = SeasonModeManager.self.CareerSuperBowlWins;
      this.Achievements[(string) Achievement.Names.SuperBowlChamps].CurrentValue += (double) careerSuperBowlWins >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.SuperBowlChamps] ? 1 : 0;
      this.Achievements[(string) Achievement.Names.DivisionChamps].CurrentValue = SeasonModeManager.self.CareerStats.divChampionships;
      this.Achievements[(string) Achievement.Names.MVP].CurrentValue += (double) SeasonModeManager.self.CareerMvp >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.MVP] ? 1 : 0;
      this.Achievements[(string) Achievement.Names.SuperBowlMVP].CurrentValue += (double) SeasonModeManager.self.CareerStats.superBowlMvpAwards >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.SuperBowlMVP] ? 1 : 0;
      this.Achievements[(string) Achievement.Names.TouchdownGOAT].CurrentValue = SeasonModeManager.self.CareerTouchdownPasses;
      this.Achievements[(string) Achievement.Names.GOAT].CurrentValue = careerSuperBowlWins;
      this.Achievements[(string) Achievement.Names.FranchiseChanger].CurrentValue += (double) SeasonModeManager.self.seasonModeData.seasonWins >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.FranchiseChanger] ? 1 : 0;
      this.Achievements[(string) Achievement.Names.PlayerOfTheWeek].CurrentValue += (double) SeasonModeManager.self.CareerTotalPlayerOfTheWeek >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.PlayerOfTheWeek] ? 1 : 0;
      if ((UnityEngine.Object) SeasonModeManager.self == (UnityEngine.Object) null || SeasonModeManager.self.userTeamData == null || SeasonModeManager.self.userTeamData.CurrentSeasonStats == null)
        return;
      this.Achievements[(string) Achievement.Names.TouchdownPassingLeader].CurrentValue += SeasonModeManager.self.IsCurrentSeasonTdPassLeader ? 1 : 0;
      int passYards = SeasonModeManager.self.userTeamData.CurrentSeasonStats.passYards;
      int mostPassYards = SeasonModeManager.self.seasonModeData.mostPassYards;
      this.Achievements[(string) Achievement.Names.PassingYardLeader].CurrentValue += passYards == mostPassYards ? 1 : 0;
      this.Achievements[(string) Achievement.Names.WhatAreThose].CurrentValue += SeasonModeManager.self.userTeamData.CurrentSeasonStats.qbInts < 6 ? 1 : 0;
      ProEraSeasonState seasonState = SeasonModeManager.self.seasonModeData.seasonState;
      this.Achievements[(string) Achievement.Names.PlayoffBound].CurrentValue += seasonState == ProEraSeasonState.DidNotMakePlayoffs || seasonState == ProEraSeasonState.RegularSeason ? 0 : 1;
      this.Achievements[(string) Achievement.Names.SuperBowlChamp].CurrentValue += seasonState == ProEraSeasonState.WonInChampionShip ? 1 : 0;
      this.Achievements[(string) Achievement.Names.YoungTechnician].CurrentValue += (double) SeasonModeManager.self.userTeamData.CurrentSeasonPassCompletionPercentage >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.YoungTechnician] ? 1 : 0;
      this.Achievements[(string) Achievement.Names.PerfectPassingRating].CurrentValue += (double) Math.Abs(SeasonModeManager.self.userTeamData.HighestSeasonQbRating - AchievementMetaData.Thresholds[(string) Achievement.Names.PerfectPassingRating]) < 0.0099999997764825821 ? 1 : 0;
      for (int teamIndex = 0; teamIndex < AchievementData.MaxTeamCount; ++teamIndex)
      {
        AcknowledgeableAward acknowledgeableAward = this.SuperBowlAwardsByTeam[teamIndex.ToString()];
        if (this.TeamHasWonSuperBowl(teamIndex) && !acknowledgeableAward.HasBeenAwarded)
        {
          AnalyticEvents.Record<TrophyUnlockedArgs>(new TrophyUnlockedArgs(acknowledgeableAward.Name));
          acknowledgeableAward.GrantAward();
        }
      }
      this.Steam_HandleStats();
      this.Steam_HandleAchievements();
      this.Save();
    }

    public void SyncPerGameAchievements(
      bool isExhibitionGame,
      TeamGameStats userStatsOverride = null,
      TeamGameStats compStatsOverride = null)
    {
      Dictionary<string, float> thresholds = AchievementMetaData.Thresholds;
      if (!isExhibitionGame)
      {
        TeamSeasonStats currentSeasonStats = SeasonModeManager.self.userTeamData.CurrentSeasonStats;
        int passYards = currentSeasonStats.passYards;
        this.Achievements[(string) Achievement.Names.FourKClub].CurrentValue += (double) passYards >= (double) thresholds[(string) Achievement.Names.FourKClub] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.ThreeKClub].CurrentValue += (double) passYards >= (double) thresholds[(string) Achievement.Names.ThreeKClub] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.UpperEchelon].CurrentValue += (double) currentSeasonStats.wins >= (double) thresholds[(string) Achievement.Names.UpperEchelon] ? 1 : 0;
        int teamIndex = SeasonModeManager.self.userTeamData.TeamIndex;
        bool flag = SeasonModeManager.self.DivisionLeaders.Contains(teamIndex);
        this.Achievements[(string) Achievement.Names.DivisionChamp].CurrentValue += flag == Convert.ToBoolean(AchievementMetaData.Thresholds[(string) Achievement.Names.DivisionChamp]) ? 1 : 0;
        this.Achievements[(string) (SeasonModeManager.self.GetConferenceOfTeam(teamIndex) == 1 ? Achievement.Names.ConferenceChampAFC : Achievement.Names.ConferenceChampNFC)].CurrentValue += currentSeasonStats.IsConferenceChampion ? 1 : 0;
        int rivalWins = currentSeasonStats.rivalWins;
        this.Achievements[(string) Achievement.Names.BigGameWinner].CurrentValue += (double) rivalWins >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.BigGameWinner] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.SeasonCompletionist].CurrentValue += (double) SeasonModeManager.self.userTeamData.CurrentSeasonPassCompletions >= (double) thresholds[(string) Achievement.Names.SeasonCompletionist] ? 1 : 0;
        int touchdowns = currentSeasonStats.touchdowns;
        this.Achievements[(string) Achievement.Names.FortyTdSeason].CurrentValue += (double) touchdowns >= (double) thresholds[(string) Achievement.Names.FortyTdSeason] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.ThirtyTdSeason].CurrentValue += (double) touchdowns >= (double) thresholds[(string) Achievement.Names.ThirtyTdSeason] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.TheMagician].CurrentValue += (double) passYards >= (double) thresholds[(string) Achievement.Names.TheMagician] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.MvpTypeSeason].CurrentValue += (double) SeasonModeManager.self.userTeamData.CurrentSeasonTouchdownPasses >= (double) thresholds[(string) Achievement.Names.MvpTypeSeason] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.TwentyFiveTouchdownSeason].CurrentValue += (double) currentSeasonStats.touchdowns >= (double) thresholds[(string) Achievement.Names.TwentyFiveTouchdownSeason] ? 1 : 0;
      }
      this.Achievements[(string) Achievement.Names.TdLegend].CurrentValue = SeasonModeManager.self.CareerTouchdownPasses;
      this.Achievements[(string) Achievement.Names.EvenTheGreats].CurrentValue = SeasonModeManager.self.CareerIntsThrown;
      this.Achievements[(string) Achievement.Names.PassingYardLegends].CurrentValue = SeasonModeManager.self.CareerPassingYards;
      this.Achievements[(string) Achievement.Names.CareerCompletionist].CurrentValue = SeasonModeManager.self.CareerPassCompletions;
      this.Achievements[(string) Achievement.Names.AllIDoIsWin].CurrentValue = SeasonModeManager.self.CareerWins;
      this.Achievements[(string) Achievement.Names.IronMan].CurrentValue = SeasonModeManager.self.CareerGames;
      this.Achievements[(string) Achievement.Names.ThrowingsABreeze].CurrentValue = SeasonModeManager.self.CareerPassingYards;
      this.Achievements[(string) Achievement.Names.MasterTactician].CurrentValue = SeasonModeManager.self.CareerPassCompletions;
      this.Achievements[(string) Achievement.Names.BigGameHunter].CurrentValue = SeasonModeManager.self.CareerRivalWins;
      this.Achievements[(string) Achievement.Names.LikeBrett].CurrentValue = SeasonModeManager.self.CareerTeamsBeaten.Count;
      this.Achievements[(string) Achievement.Names.OldHead].CurrentValue = SeasonModeManager.self.CareerWins;
      TeamGameStats teamGameStats1 = userStatsOverride ?? ProEra.Game.MatchState.Stats.User;
      TeamGameStats teamGameStats2 = compStatsOverride ?? ProEra.Game.MatchState.Stats.Comp;
      if (teamGameStats1 != null && teamGameStats2 != null)
      {
        int touchdowns = teamGameStats1.Touchdowns;
        this.Achievements[(string) Achievement.Names.ThreeTouchdownDay].CurrentValue += (double) touchdowns >= (double) thresholds[(string) Achievement.Names.ThreeTouchdownDay] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.FiveTouchdownDay].CurrentValue += (double) touchdowns >= (double) thresholds[(string) Achievement.Names.FiveTouchdownDay] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.SevenTouchdownDay].CurrentValue += (double) touchdowns >= (double) thresholds[(string) Achievement.Names.SevenTouchdownDay] ? 1 : 0;
        int passYards = teamGameStats1.PassYards;
        this.Achievements[(string) Achievement.Names.FiveHundredPassingYards].CurrentValue += (double) passYards >= (double) thresholds[(string) Achievement.Names.FiveHundredPassingYards] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.FourHundredPassingYards].CurrentValue += (double) passYards >= (double) thresholds[(string) Achievement.Names.FourHundredPassingYards] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.ThreeHundredPassingYards].CurrentValue += (double) passYards >= (double) thresholds[(string) Achievement.Names.ThreeHundredPassingYards] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.TwoHundredFiftyPassingYards].CurrentValue += (double) passYards >= (double) thresholds[(string) Achievement.Names.TwoHundredFiftyPassingYards] ? 1 : 0;
        bool vformationSatisfied = teamGameStats1.VFormationSatisfied;
        this.Achievements[(string) Achievement.Names.VFormation].CurrentValue += vformationSatisfied == Convert.ToBoolean(AchievementMetaData.Thresholds[(string) Achievement.Names.VFormation]) ? 1 : 0;
        this.Achievements[(string) Achievement.Names.NoPicks].CurrentValue += teamGameStats2.Interceptions == (int) thresholds[(string) Achievement.Names.NoPicks] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.ChainMover].CurrentValue += (double) teamGameStats1.TotalFirstDowns >= (double) thresholds[(string) Achievement.Names.ChainMover] ? 1 : 0;
        float redZoneEfficiency = teamGameStats1.RedZoneEfficiency;
        this.Achievements[(string) Achievement.Names.CantBeStopped].CurrentValue += (double) redZoneEfficiency >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.CantBeStopped] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.BraggingRights].CurrentValue += (!ProEra.Game.MatchState.Stats.IsCompLoosing() ? 0 : (ProEra.Game.MatchState.Stats.IsRivalMatch ? 1 : 0)) == (Convert.ToBoolean(thresholds[(string) Achievement.Names.BraggingRights]) ? 1 : 0) ? 1 : 0;
        this.Achievements[(string) Achievement.Names.Deadeye].CurrentValue += (double) teamGameStats1.PassCompletionPercentage >= (double) thresholds[(string) Achievement.Names.Deadeye] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.Hawkeye].CurrentValue += (double) teamGameStats1.PassCompletionPercentage >= (double) thresholds[(string) Achievement.Names.Hawkeye] ? 1 : 0;
        this.Achievements[(string) Achievement.Names.InControl].CurrentValue += !ProEra.Game.MatchState.Stats.IsCompLoosing() || (double) ProEra.Game.MatchState.Stats.ScoreDifference() < (double) thresholds[(string) Achievement.Names.InControl] ? 0 : 1;
        this.Achievements[(string) Achievement.Names.UpBig].CurrentValue += !ProEra.Game.MatchState.Stats.IsCompLoosing() || (double) ProEra.Game.MatchState.Stats.ScoreDifference() < (double) thresholds[(string) Achievement.Names.UpBig] ? 0 : 1;
        this.Achievements[(string) Achievement.Names.NotToday].CurrentValue += teamGameStats2.Sacks == (int) thresholds[(string) Achievement.Names.NotToday] ? 1 : 0;
        if (teamGameStats1.Score > teamGameStats2.Score)
        {
          TeamData compTeam = PersistentData.GetCompTeam();
          if (compTeam != null)
          {
            AcknowledgeableAward acknowledgeableAward = this.TeamsDefeatedByIndex[compTeam.TeamIndex.ToString()];
            if (!acknowledgeableAward.HasBeenAwarded)
            {
              acknowledgeableAward.GrantAward();
              AnalyticEvents.Record<TrophyUnlockedArgs>(new TrophyUnlockedArgs(acknowledgeableAward.Name));
            }
          }
        }
      }
      else
        Debug.Log((object) "Missing user or cpu match stats, or no game was played");
      for (int teamIndex = 0; teamIndex < AchievementData.MaxTeamCount; ++teamIndex)
      {
        AcknowledgeableAward acknowledgeableAward = this.TeamsDefeatedByIndex[teamIndex.ToString()];
        if (this.UserHasDefeatedTeam(teamIndex) && !acknowledgeableAward.HasBeenAwarded)
        {
          AnalyticEvents.Record<TrophyUnlockedArgs>(new TrophyUnlockedArgs(acknowledgeableAward.Name));
          acknowledgeableAward.GrantAward();
        }
      }
      this.Steam_HandleStats();
      this.Steam_HandleAchievements();
      this.Save();
    }

    public void Reset()
    {
      this.Achievements.Clear();
      this.InitializeAchievementDictionary();
      this.Save();
    }

    public void Save() => AppEvents.SaveAchievements.Trigger();

    private bool InitializeAchievementDictionary()
    {
      if (this.Achievements.Keys.Count != 0)
        return false;
      foreach (string str in Achievement.Names.GetAll())
        this.Achievements[str] = new Achievement(str);
      return true;
    }

    private bool InitializeDefeatedTeamsAwards()
    {
      if (this.TeamsDefeatedByIndex.Count != 0)
        return false;
      for (int index = 0; index < AchievementData.MaxTeamCount; ++index)
      {
        string name = string.Format(AchievementData.DefeatedTeamAward, (object) TeamResourcesManager.BASE_TEAM_FOLDERS[index]);
        AcknowledgeableAward acknowledgeableAward = new AcknowledgeableAward();
        acknowledgeableAward.Init(name);
        this.TeamsDefeatedByIndex.Add(index.ToString(), acknowledgeableAward);
      }
      return true;
    }

    private bool InitializeSuperBowlAwards()
    {
      if (this.SuperBowlAwardsByTeam.Count != 0)
        return false;
      for (int index = 0; index < AchievementData.MaxTeamCount; ++index)
      {
        string name = string.Format(AchievementData.SuperBowlAward, (object) TeamResourcesManager.BASE_TEAM_FOLDERS[index]);
        AcknowledgeableAward acknowledgeableAward = new AcknowledgeableAward();
        acknowledgeableAward.Init(name);
        this.SuperBowlAwardsByTeam.Add(index.ToString(), acknowledgeableAward);
      }
      return true;
    }

    private int GetAchievementScore()
    {
      Func<Achievement, int> selector = (Func<Achievement, int>) (achievement => achievement.CurrentTier >= 0 ? achievement.Tiers.Levels.Length - achievement.CurrentTier : 0);
      return this.Achievements.Select<KeyValuePair<string, Achievement>, Achievement>((Func<KeyValuePair<string, Achievement>, Achievement>) (keyValuePair => keyValuePair.Value)).Sum<Achievement>(selector);
    }

    public AcknowledgeableAward GetAwardForTeamDefeated(int teamIndex)
    {
      AcknowledgeableAward awardForTeamDefeated = (AcknowledgeableAward) null;
      this.TeamsDefeatedByIndex.TryGetValue(teamIndex.ToString(), out awardForTeamDefeated);
      return awardForTeamDefeated;
    }

    private bool UserHasDefeatedTeam(int teamIndex) => SeasonModeManager.self.CareerTeamsBeaten.Contains(teamIndex);

    public AcknowledgeableAward GetAwardForSuperBowlTeam(int teamIndex)
    {
      AcknowledgeableAward forSuperBowlTeam = (AcknowledgeableAward) null;
      this.SuperBowlAwardsByTeam.TryGetValue(teamIndex.ToString(), out forSuperBowlTeam);
      return forSuperBowlTeam;
    }

    private bool TeamHasWonSuperBowl(int teamIndex)
    {
      HashSet<int> superBowlWiningTeams = SeasonModeManager.self.CareerStats.superBowlWiningTeams;
      return superBowlWiningTeams != null && superBowlWiningTeams.Contains(teamIndex);
    }

    public bool UserHasWonSuperBowl() => SeasonModeManager.self.CareerSuperBowlWins > 0;

    private void Steam_HandleStats()
    {
      List<int> intList = new List<int>();
      TeamSeasonStats currentSeasonStats = SeasonModeManager.self.userTeamData.CurrentSeasonStats;
      intList.Add(currentSeasonStats.wins);
      intList.Add(currentSeasonStats.touchdowns);
      intList.Add(this.Achievements[(string) Achievement.Names.ThrowingsABreeze].CurrentValue);
      intList.Add(this.Achievements[(string) Achievement.Names.MasterTactician].CurrentValue);
      intList.Add(currentSeasonStats.passYards);
      intList.Add(this.Achievements[(string) Achievement.Names.LikeBrett].CurrentValue);
    }

    private void Steam_HandleAchievements()
    {
      List<int> achievements = new List<int>();
      if (this.Achievements[(string) Achievement.Names.ThreeTouchdownDay].CurrentValue == 1)
        achievements.Add(1);
      if (this.Achievements[(string) Achievement.Names.FiveTouchdownDay].CurrentValue == 1)
        achievements.Add(2);
      if (this.Achievements[(string) Achievement.Names.SevenTouchdownDay].CurrentValue == 1)
        achievements.Add(3);
      if (this.Achievements[(string) Achievement.Names.FiveHundredPassingYards].CurrentValue == 1)
        achievements.Add(4);
      if (this.Achievements[(string) Achievement.Names.NoPicks].CurrentValue == 1)
        achievements.Add(5);
      if (this.Achievements[(string) Achievement.Names.ChainMover].CurrentValue == 1)
        achievements.Add(6);
      if (this.Achievements[(string) Achievement.Names.CantBeStopped].CurrentValue == 1)
        achievements.Add(7);
      if ((double) this.Achievements[(string) Achievement.Names.LikeBrett].CurrentValue == (double) AchievementMetaData.Thresholds[(string) Achievement.Names.LikeBrett])
        achievements.Add(8);
      if (this.Achievements[(string) Achievement.Names.NotToday].CurrentValue == 1)
        achievements.Add(11);
      if (this.Achievements[(string) Achievement.Names.ThirtyTdSeason].CurrentValue == 1)
        achievements.Add(12);
      if (this.Achievements[(string) Achievement.Names.FortyTdSeason].CurrentValue == 1)
        achievements.Add(13);
      if (this.Achievements[(string) Achievement.Names.TheMagician].CurrentValue == 1)
        achievements.Add(14);
      if ((double) this.Achievements[(string) Achievement.Names.ThrowingsABreeze].CurrentValue >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.ThrowingsABreeze])
        achievements.Add(15);
      if (this.Achievements[(string) Achievement.Names.DivisionChamp].CurrentValue == 1)
        achievements.Add(16);
      if (this.Achievements[(string) Achievement.Names.ConferenceChampAFC].CurrentValue == 1 || this.Achievements[(string) Achievement.Names.ConferenceChampNFC].CurrentValue == 1)
        achievements.Add(17);
      if ((double) this.Achievements[(string) Achievement.Names.MasterTactician].CurrentValue >= (double) AchievementMetaData.Thresholds[(string) Achievement.Names.MasterTactician])
        achievements.Add(19);
      if (this.Achievements[(string) Achievement.Names.InControl].CurrentValue == 1)
        achievements.Add(20);
      if (this.Achievements[(string) Achievement.Names.TwoHundredFiftyPassingYards].CurrentValue == 1)
        achievements.Add(21);
      if (this.Achievements[(string) Achievement.Names.Hawkeye].CurrentValue == 1)
        achievements.Add(22);
      if (this.Achievements[(string) Achievement.Names.Deadeye].CurrentValue == 1)
        achievements.Add(23);
      if (this.Achievements[(string) Achievement.Names.ThreeHundredPassingYards].CurrentValue == 1)
        achievements.Add(24);
      if (this.Achievements[(string) Achievement.Names.FourHundredPassingYards].CurrentValue == 1)
        achievements.Add(25);
      if (this.Achievements[(string) Achievement.Names.FourKClub].CurrentValue == 1)
        achievements.Add(27);
      if (this.Achievements[(string) Achievement.Names.ThreeKClub].CurrentValue == 1)
        achievements.Add(28);
      if (this.Achievements[(string) Achievement.Names.PlayoffBound].CurrentValue == 1)
        achievements.Add(9);
      if (this.Achievements[(string) Achievement.Names.UpperEchelon].CurrentValue == 1)
        achievements.Add(10);
      if (this.Achievements[(string) Achievement.Names.SuperBowlChamp].CurrentValue == 1)
        achievements.Add(18);
      if (this.Achievements[(string) Achievement.Names.WhatAreThose].CurrentValue == 1)
        achievements.Add(26);
      SteamAchievementManager.UnlockAchievements(achievements);
    }
  }
}
