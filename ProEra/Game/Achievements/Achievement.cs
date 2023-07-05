// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Achievements.Achievement
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProEra.Game.Achievements
{
  [MessagePackObject(false)]
  [Serializable]
  public class Achievement
  {
    [Key(0)]
    public string Name;
    [Key(1)]
    public string Description;
    [Key(2)]
    public AchievementTier Tiers;
    [Key(3)]
    public string Type;
    [Key(4)]
    public int _currentValue;
    [SerializeField]
    [Key(5)]
    public int CurrentTier = -1;
    [Key(6)]
    public double[] Timestamps;
    [Key(7)]
    public string[] Platforms;
    [Key(8)]
    public bool Acknowledged;

    [IgnoreMember]
    public int CurrentValue
    {
      get => this._currentValue;
      set
      {
        this._currentValue = value;
        this.UpdateCurrentTier();
      }
    }

    [IgnoreMember]
    public bool ShouldPlaySfx { get; private set; }

    public Achievement()
    {
    }

    public Achievement(string name, int currentValue = 0)
    {
      this.Name = name;
      this.Type = AchievementMetaData.AchievementTypes[this.Name];
      this.Description = AchievementMetaData.Descriptions[this.Name];
      this.Tiers = AchievementMetaData.AchievementTiers[this.Name];
      this.Timestamps = new double[this.Tiers.Count];
      this.Platforms = new string[this.Tiers.Count];
      this._currentValue = currentValue;
      this.Acknowledged = false;
    }

    private void UpdateCurrentTier()
    {
      if (this.Tiers == null)
        this.Tiers = AchievementMetaData.AchievementTiers[this.Name];
      if (this.Timestamps == null)
        this.Timestamps = new double[this.Tiers.Count];
      if (this.Platforms == null)
        this.Platforms = new string[this.Tiers.Count];
      int tierFromProgress = this.Tiers.GetTierFromProgress(this._currentValue);
      if (tierFromProgress < 0 || this.CurrentTier == tierFromProgress)
        return;
      this.Timestamps[tierFromProgress] = SaveSyncUtils.Current();
      this.Platforms[tierFromProgress] = Application.platform.ToString();
      this.CurrentTier = tierFromProgress;
      string str;
      switch (tierFromProgress)
      {
        case 0:
          str = "Gold";
          break;
        case 1:
          str = "Silver";
          break;
        case 2:
          str = "Bronze";
          break;
        default:
          str = string.Empty;
          break;
      }
      AnalyticEvents.Record<TrophyUnlockedArgs>(new TrophyUnlockedArgs(this.Name + " (" + str + ")"));
      this.ShouldPlaySfx = true;
    }

    public void Maximize()
    {
      AchievementTier achievementTier = AchievementMetaData.AchievementTiers[this.Name];
      this.CurrentValue = achievementTier.Levels[achievementTier.Count - 1];
    }

    public void AlmostThere()
    {
      AchievementTier achievementTier = AchievementMetaData.AchievementTiers[this.Name];
      int level;
      if (achievementTier.Count > 1)
      {
        int index = Mathf.Min(this.CurrentTier + 1, achievementTier.Count - 1);
        level = achievementTier.Levels[index];
      }
      else
        level = achievementTier.Levels[0];
      this.CurrentValue = (int) ((double) level * 0.99);
    }

    public void DisableFx() => this.ShouldPlaySfx = false;

    public class Progress
    {
      public static float GetProgressPercentage(string name)
      {
        SaveAchievements saveAchievements = PersistentSingleton<SaveManager>.Instance.GetSaveAchievements();
        Achievement achievement;
        return saveAchievements != null && saveAchievements.Achievements.TryGetValue(name, out achievement) && achievement.CurrentTier > 0 ? (float) achievement.CurrentValue / (float) AchievementMetaData.AchievementTiers[name].Levels[achievement.CurrentTier] : 0.0f;
      }

      public static float GetProgress(string name)
      {
        Achievement achievement;
        return PersistentSingleton<SaveManager>.Instance.GetSaveAchievements().Achievements.TryGetValue(name, out achievement) ? (float) achievement.CurrentValue : 0.0f;
      }

      public static int? GetTier(string name)
      {
        Achievement achievement;
        return !PersistentSingleton<SaveManager>.Instance.GetSaveAchievements().Achievements.TryGetValue(name, out achievement) ? new int?(achievement.CurrentTier) : new int?();
      }

      public static bool TrySetProgress(string name, int progress)
      {
        Achievement achievement;
        if (PersistentSingleton<SaveManager>.Instance.GetSaveAchievements().Achievements.TryGetValue(name, out achievement))
          return false;
        achievement.CurrentValue = progress;
        return true;
      }

      public static bool TryAddProgress(string name, int progress)
      {
        Dictionary<string, Achievement> achievements = PersistentSingleton<SaveManager>.Instance.GetSaveAchievements().Achievements;
        if (!achievements.ContainsKey(name))
          return false;
        achievements[name].CurrentValue += progress;
        return true;
      }

      public static bool TrySetTier(string name, int tier, int offset = 0)
      {
        SaveAchievements saveAchievements = PersistentSingleton<SaveManager>.Instance.GetSaveAchievements();
        Achievement achievement1;
        if (saveAchievements.Achievements.TryGetValue(name, out achievement1) && achievement1.CurrentTier > 0)
          return false;
        int[] levels = AchievementMetaData.AchievementTiers[name].Levels;
        Achievement achievement2 = saveAchievements.Achievements[name];
        if (tier < 0 || tier >= levels.Length)
          return false;
        achievement2.CurrentValue = levels[levels.Length - 1 - tier] - offset;
        return true;
      }

      public static bool TryIncreaseTier(string name, int offset = 0)
      {
        SaveAchievements saveAchievements = PersistentSingleton<SaveManager>.Instance.GetSaveAchievements();
        AchievementTier achievementTier = (AchievementTier) null;
        Achievement achievement = (Achievement) null;
        if (AchievementMetaData.AchievementTiers.TryGetValue(name, out achievementTier) && saveAchievements.Achievements.TryGetValue(name, out achievement))
        {
          int tierFromProgress = achievement.Tiers.GetTierFromProgress(achievement.CurrentValue);
          int index = tierFromProgress == -1 ? achievementTier.Count - 1 : (tierFromProgress - 1 < 0 ? 0 : tierFromProgress - 1);
          if (achievement.CurrentTier < achievementTier.Count)
          {
            achievement.CurrentValue = achievementTier.Levels[index] + offset;
            return true;
          }
        }
        return false;
      }

      public static bool TryIncreaseTierOffsetOne(string name) => Achievement.Progress.TryIncreaseTier(name, -1);

      public static bool TryDecreaseTier(string name)
      {
        SaveAchievements saveAchievements = PersistentSingleton<SaveManager>.Instance.GetSaveAchievements();
        AchievementTier achievementTier = (AchievementTier) null;
        Achievement achievement = (Achievement) null;
        if (!AchievementMetaData.AchievementTiers.TryGetValue(name, out achievementTier) || !saveAchievements.Achievements.TryGetValue(name, out achievement))
          return false;
        if (achievement.CurrentTier >= 0 && achievement.CurrentTier < achievementTier.Levels.Length - 1)
          achievement.CurrentValue = achievementTier.Levels[achievement.CurrentTier + 1];
        else if (achievement.CurrentTier == achievementTier.Levels.Length - 1)
          achievement.CurrentValue = 0;
        return true;
      }
    }

    [Serializable]
    public class Types
    {
      public static readonly string Career = nameof (Career);
      public static readonly string Game = nameof (Game);
      public static readonly string Season = nameof (Season);
    }

    [Serializable]
    public class Names
    {
      private static HashSet<string> _allNames = new HashSet<string>();
      public static readonly Achievement.Names.AchievementName TdLegend = new Achievement.Names.AchievementName("TD Legend");
      public static readonly Achievement.Names.AchievementName EvenTheGreats = new Achievement.Names.AchievementName("Even the Greats");
      public static readonly Achievement.Names.AchievementName PassingYardLegends = new Achievement.Names.AchievementName("Passing Yard Legends");
      public static readonly Achievement.Names.AchievementName CareerCompletionist = new Achievement.Names.AchievementName("Career Completionist");
      public static readonly Achievement.Names.AchievementName AllIDoIsWin = new Achievement.Names.AchievementName("All I do is Win Award");
      public static readonly Achievement.Names.AchievementName IronMan = new Achievement.Names.AchievementName("Iron Man Award");
      public static readonly Achievement.Names.AchievementName DartThrower = new Achievement.Names.AchievementName("Dart Thrower");
      public static readonly Achievement.Names.AchievementName SuperBowlChamps = new Achievement.Names.AchievementName("Super Bowl Champs");
      public static readonly Achievement.Names.AchievementName DivisionChamps = new Achievement.Names.AchievementName("Division Champs");
      public static readonly Achievement.Names.AchievementName MVP = new Achievement.Names.AchievementName(nameof (MVP));
      public static readonly Achievement.Names.AchievementName SuperBowlMVP = new Achievement.Names.AchievementName("Super Bowl MVP");
      public static readonly Achievement.Names.AchievementName ThrowingsABreeze = new Achievement.Names.AchievementName("Throwings a breeze");
      public static readonly Achievement.Names.AchievementName TouchdownGOAT = new Achievement.Names.AchievementName("Touchdown GOAT");
      public static readonly Achievement.Names.AchievementName MasterTactician = new Achievement.Names.AchievementName("Master Tactician");
      public static readonly Achievement.Names.AchievementName BigGameHunter = new Achievement.Names.AchievementName("Big Game Hunter");
      public static readonly Achievement.Names.AchievementName GOAT = new Achievement.Names.AchievementName(nameof (GOAT));
      public static readonly Achievement.Names.AchievementName LikeBrett = new Achievement.Names.AchievementName("Like Brett");
      public static readonly Achievement.Names.AchievementName OldHead = new Achievement.Names.AchievementName("Old Head");
      public static readonly Achievement.Names.AchievementName FranchiseChanger = new Achievement.Names.AchievementName("Franchise Changer");
      public static readonly Achievement.Names.AchievementName PlayerOfTheWeek = new Achievement.Names.AchievementName("Player of the Week");
      public static readonly Achievement.Names.AchievementName TouchdownPassingLeader = new Achievement.Names.AchievementName("Touchdown Passing Leader");
      public static readonly Achievement.Names.AchievementName PassingYardLeader = new Achievement.Names.AchievementName("Passing Yard Leader");
      public static readonly Achievement.Names.AchievementName FourKClub = new Achievement.Names.AchievementName("4k Club");
      public static readonly Achievement.Names.AchievementName ThreeKClub = new Achievement.Names.AchievementName("3K Club");
      public static readonly Achievement.Names.AchievementName TwentyFiveTouchdownSeason = new Achievement.Names.AchievementName("25 Touchdown Season");
      public static readonly Achievement.Names.AchievementName WhatAreThose = new Achievement.Names.AchievementName("What are those?");
      public static readonly Achievement.Names.AchievementName UpperEchelon = new Achievement.Names.AchievementName("Upper Echelon");
      public static readonly Achievement.Names.AchievementName PlayoffBound = new Achievement.Names.AchievementName("Playoff Bound");
      public static readonly Achievement.Names.AchievementName DivisionChamp = new Achievement.Names.AchievementName("Division Champ");
      public static readonly Achievement.Names.AchievementName ConferenceChampAFC = new Achievement.Names.AchievementName("AFC Conference Champ");
      public static readonly Achievement.Names.AchievementName ConferenceChampNFC = new Achievement.Names.AchievementName("NFC Conference Champ");
      public static readonly Achievement.Names.AchievementName SuperBowlChamp = new Achievement.Names.AchievementName("Super Bowl Champ");
      public static readonly Achievement.Names.AchievementName BigGameWinner = new Achievement.Names.AchievementName("Big Game Winner");
      public static readonly Achievement.Names.AchievementName YoungTechnician = new Achievement.Names.AchievementName("Young Technician");
      public static readonly Achievement.Names.AchievementName SeasonCompletionist = new Achievement.Names.AchievementName("Season Completionist");
      public static readonly Achievement.Names.AchievementName PerfectPassingRating = new Achievement.Names.AchievementName("Perfect Passing Rating");
      public static readonly Achievement.Names.AchievementName FortyTdSeason = new Achievement.Names.AchievementName("40 TD Season");
      public static readonly Achievement.Names.AchievementName ThirtyTdSeason = new Achievement.Names.AchievementName("30 TD Season");
      public static readonly Achievement.Names.AchievementName TheMagician = new Achievement.Names.AchievementName("The Magician");
      public static readonly Achievement.Names.AchievementName MvpTypeSeason = new Achievement.Names.AchievementName("MVP Type Season");
      public static readonly Achievement.Names.AchievementName ThreeTouchdownDay = new Achievement.Names.AchievementName("3 Touchdown Day");
      public static readonly Achievement.Names.AchievementName FiveTouchdownDay = new Achievement.Names.AchievementName("5 Touchdown Day");
      public static readonly Achievement.Names.AchievementName SevenTouchdownDay = new Achievement.Names.AchievementName("7 Touchdown Day");
      public static readonly Achievement.Names.AchievementName FiveHundredPassingYards = new Achievement.Names.AchievementName("500 Passing Yards");
      public static readonly Achievement.Names.AchievementName TwoHundredFiftyPassingYards = new Achievement.Names.AchievementName("250 Passing Yards");
      public static readonly Achievement.Names.AchievementName VFormation = new Achievement.Names.AchievementName("V Formation");
      public static readonly Achievement.Names.AchievementName NoPicks = new Achievement.Names.AchievementName("No Picks");
      public static readonly Achievement.Names.AchievementName ChainMover = new Achievement.Names.AchievementName("Chain Mover");
      public static readonly Achievement.Names.AchievementName CantBeStopped = new Achievement.Names.AchievementName("Cant be Stopped");
      public static readonly Achievement.Names.AchievementName BraggingRights = new Achievement.Names.AchievementName("Bragging Rights");
      public static readonly Achievement.Names.AchievementName Deadeye = new Achievement.Names.AchievementName(nameof (Deadeye));
      public static readonly Achievement.Names.AchievementName Hawkeye = new Achievement.Names.AchievementName(nameof (Hawkeye));
      public static readonly Achievement.Names.AchievementName InControl = new Achievement.Names.AchievementName("In Control");
      public static readonly Achievement.Names.AchievementName UpBig = new Achievement.Names.AchievementName("Up Big");
      public static readonly Achievement.Names.AchievementName NotToday = new Achievement.Names.AchievementName("Not Today");
      public static readonly Achievement.Names.AchievementName ThreeHundredPassingYards = new Achievement.Names.AchievementName("300 Passing Yards");
      public static readonly Achievement.Names.AchievementName FourHundredPassingYards = new Achievement.Names.AchievementName("400 Passing Yards");

      public static HashSet<string> GetAll() => Achievement.Names._allNames;

      public class AchievementName
      {
        public string Name { get; }

        public AchievementName(string name)
        {
          Achievement.Names._allNames.Add(name);
          this.Name = name;
        }

        public static implicit operator string(Achievement.Names.AchievementName name) => name.Name;
      }
    }
  }
}
