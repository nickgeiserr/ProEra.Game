// Decompiled with JetBrains decompiler
// Type: PlayerData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using MessagePack;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class PlayerData
{
  [Key(0)]
  public string FirstName;
  [Key(1)]
  public string LastName;
  [Key(2)]
  public Position PlayerPosition;
  [Key(3)]
  public int Number;
  [Key(4)]
  public int SkinColor;
  [Key(5)]
  public int Height;
  [Key(6)]
  public int Weight;
  [Key(7)]
  public int Age;
  [Key(8)]
  public int PortraitID;
  [Key(52)]
  public int IsLeftHanded;
  [Key(9)]
  public int Speed;
  [Key(10)]
  public int Catching;
  [Key(11)]
  public int Fumbling;
  [Key(12)]
  public int Blocking;
  [Key(13)]
  public int TackleBreaking;
  [Key(14)]
  public int ThrowAccuracy;
  [Key(15)]
  public int ThrowPower;
  [Key(16)]
  public int KickAccuracy;
  [Key(17)]
  public int KickPower;
  [Key(18)]
  public int BlockBreaking;
  [Key(19)]
  public int Tackling;
  [Key(20)]
  public int Fitness;
  [Key(21)]
  public int Awareness;
  [Key(22)]
  public int Coverage;
  [Key(23)]
  public int HitPower;
  [Key(24)]
  public int Endurance;
  [Key(25)]
  public int Agility;
  [Key(26)]
  public int Potential;
  [Key(27)]
  public int Discipline;
  [Key(28)]
  public int Fatigue;
  [Key(29)]
  public string AvatarID;
  [Key(30)]
  public int Visor;
  [Key(31)]
  public int Sleeves;
  [Key(32)]
  public int Bands;
  [Key(33)]
  public int Wraps;
  [Key(34)]
  public ScoutingRegion ScoutRegion;
  [Key(35)]
  public int ScoutPercentage;
  [Key(36)]
  public int EstimatedOverall;
  [Key(37)]
  public int ScoutingPointsUsedOn;
  [Key(38)]
  public int RoundDrafted;
  [Key(39)]
  public int ContractLength;
  [Key(40)]
  public int Salary;
  [Key(41)]
  public int YearsRemainingOnContract;
  [Key(42)]
  public int YearsPro;
  [Key(43)]
  public Injury CurrentInjury;
  [Key(44)]
  public List<Injury> AllInjuries;
  [Key(45)]
  public List<Award> AllAwards;
  [Key(46)]
  public PlayerStats CurrentGameStats;
  [Key(47)]
  public PlayerStats CurrentSeasonStats;
  [Key(48)]
  public PlayerStats TotalCareerStats;
  [Key(49)]
  public List<PlayerStats> CareerStats;
  [Key(50)]
  public bool OnUserTeam;
  [Key(51)]
  public int IndexOnTeam;

  [IgnoreMember]
  public string FullName => this.FirstName + " " + this.LastName;

  [IgnoreMember]
  public string FirstInitalAndLastName => this.FirstName.Substring(0, 1) + ". " + this.LastName;

  public PlayerData()
  {
    this.CurrentGameStats = new PlayerStats();
    this.CurrentSeasonStats = new PlayerStats();
    this.TotalCareerStats = new PlayerStats();
    this.CareerStats = new List<PlayerStats>();
    this.IsLeftHanded = 0;
  }

  public int GetOverall() => this.GetOverall(this.PlayerPosition);

  public int GetOverall(Position position)
  {
    float f = 0.0f;
    switch (position)
    {
      case Position.QB:
        f = f + (float) this.ThrowAccuracy * 0.4f + (float) this.ThrowPower * 0.3f + (float) this.Awareness * 0.165f + (float) this.Speed * 0.077f + (float) this.Fitness * 0.055f + (float) this.Agility * 0.033f;
        if ((double) f > 99.0)
        {
          f = 99f;
          break;
        }
        break;
      case Position.RB:
        f = f + (float) this.Speed * 0.22f + (float) this.TackleBreaking * 0.22f + (float) this.Awareness * 0.165f + (float) this.Blocking * 0.165f + (float) this.Catching * 0.11f + (float) this.Fumbling * 0.077f + (float) this.Fitness * 0.055f + (float) this.Agility * 0.055f + (float) this.Endurance * 0.033f;
        if ((double) f > 99.0)
        {
          f = 99f;
          break;
        }
        break;
      case Position.FB:
        f = f + (float) this.Blocking * 0.27f + (float) this.Awareness * 0.22f + (float) this.TackleBreaking * 0.165f + (float) this.Speed * 0.11f + (float) this.Catching * 0.11f + (float) this.Fumbling * 0.077f + (float) this.Fitness * 0.055f + (float) this.Agility * 0.055f + (float) this.Endurance * 0.033f;
        if ((double) f > 99.0)
        {
          f = 99f;
          break;
        }
        break;
      default:
        if (position == Position.WR || position == Position.SLT)
        {
          f = f + (float) this.Speed * 0.22f + (float) this.Catching * 0.22f + (float) this.Awareness * 0.22f + (float) this.Blocking * 0.12f + (float) this.Endurance * 0.077f + (float) this.Fitness * 0.077f + (float) this.Agility * 0.066f + (float) this.Fumbling * 0.055f + (float) this.TackleBreaking * 0.055f;
          if ((double) f > 99.0)
          {
            f = 99f;
            break;
          }
          break;
        }
        if (position == Position.TE)
        {
          f = f + (float) this.Blocking * 0.3f + (float) this.Catching * 0.27f + (float) this.Awareness * 0.22f + (float) this.Speed * 0.11f + (float) this.Endurance * 0.055f + (float) this.Fitness * 0.055f + (float) this.Agility * 0.022f + (float) this.TackleBreaking * 0.022f + (float) this.Fumbling * (11f / 1000f);
          if ((double) f > 99.0)
          {
            f = 99f;
            break;
          }
          break;
        }
        if (position == Position.OL || position == Position.LT || position == Position.LG || position == Position.C || position == Position.RG || position == Position.RT || position == Position.OL)
        {
          f = f + (float) this.Blocking * 0.55f + (float) this.Awareness * 0.27f + (float) this.Agility * 0.088f + (float) this.Fitness * 0.055f + (float) this.Speed * 0.055f + (float) this.Endurance * 0.022f;
          if ((double) f > 99.0)
          {
            f = 99f;
            break;
          }
          break;
        }
        if (position == Position.DL || position == Position.DE || position == Position.DT || position == Position.DE || position == Position.NT)
        {
          f = f + (float) this.Awareness * 0.27f + (float) this.Tackling * 0.27f + (float) this.BlockBreaking * 0.22f + (float) this.HitPower * 0.11f + (float) this.Speed * 0.088f + (float) this.Agility * 0.077f + (float) this.Fitness * 0.033f + (float) this.Endurance * 0.022f;
          if ((double) f > 99.0)
          {
            f = 99f;
            break;
          }
          break;
        }
        if (position == Position.LB || position == Position.OLB || position == Position.ILB || position == Position.MLB)
        {
          f = f + (float) this.Awareness * 0.22f + (float) this.Tackling * 0.22f + (float) this.Coverage * 0.165f + (float) this.Speed * 0.132f + (float) this.BlockBreaking * 0.11f + (float) this.HitPower * 0.11f + (float) this.Agility * 0.077f + (float) this.Fitness * 0.033f + (float) this.Endurance * 0.033f;
          if ((double) f > 99.0)
          {
            f = 99f;
            break;
          }
          break;
        }
        if (position == Position.DB || position == Position.CB || position == Position.FS || position == Position.SS)
        {
          f = f + (float) this.Awareness * 0.27f + (float) this.Coverage * 0.27f + (float) this.Speed * 0.165f + (float) this.Tackling * 0.11f + (float) this.Agility * 0.077f + (float) this.Fitness * 0.055f + (float) this.Endurance * 0.055f + (float) this.BlockBreaking * 0.055f + (float) this.HitPower * 0.033f;
          if ((double) f > 99.0)
          {
            f = 99f;
            break;
          }
          break;
        }
        switch (position)
        {
          case Position.K:
            f = f + (float) this.KickAccuracy * 0.5f + (float) this.KickPower * 0.37f + (float) this.Awareness * 0.13f;
            break;
          case Position.P:
            f = f + (float) this.KickPower * 0.52f + (float) this.KickAccuracy * 0.33f + (float) this.Awareness * 0.15f;
            break;
          case Position.KR:
            f = f + (float) this.Speed * 0.55f + (float) this.Awareness * 0.25f + (float) this.Catching * 0.1f + (float) this.TackleBreaking * 0.05f + (float) this.Fumbling * 0.05f;
            if ((double) f > 99.0)
            {
              f = 99f;
              break;
            }
            break;
          case Position.PR:
            f = f + (float) this.Speed * 0.55f + (float) this.Catching * 0.25f + (float) this.Awareness * 0.01f + (float) this.TackleBreaking * 0.05f + (float) this.Fumbling * 0.05f;
            if ((double) f > 99.0)
            {
              f = 99f;
              break;
            }
            break;
          case Position.GUN:
            f = f + (float) this.Speed * 0.275f + (float) this.Awareness * 0.22f + (float) this.Coverage * 0.22f + (float) this.Tackling * 0.11f + (float) this.Agility * 0.077f + (float) this.Fitness * 0.055f + (float) this.Endurance * 0.055f + (float) this.BlockBreaking * 0.055f + (float) this.HitPower * 0.033f;
            if ((double) f > 99.0)
            {
              f = 99f;
              break;
            }
            break;
          case Position.BLK:
            f = f + (float) this.Blocking * 0.44f + (float) this.Awareness * 0.275f + (float) this.Speed * 0.11f + (float) this.Endurance * 0.055f + (float) this.Fitness * 0.055f + (float) this.Agility * 0.022f;
            if ((double) f > 99.0)
            {
              f = 99f;
              break;
            }
            break;
          default:
            Debug.Log((object) ("Unknown position specified: " + position.ToString()));
            break;
        }
        break;
    }
    return Mathf.Clamp(Mathf.RoundToInt(f), 1, 99);
  }

  public void CalculateEstimatedOverallRating()
  {
    int num = 20 - this.ScoutPercentage / 5;
    int overall = this.GetOverall();
    this.EstimatedOverall = Mathf.Clamp(UnityEngine.Random.Range(overall - num, overall + num), 0, 99);
  }

  public void CreateNewPlayerStatsForGame() => this.CurrentGameStats = new PlayerStats();

  public void CreateNewPlayerStatsForSeason(int year, string teamAbbreviation)
  {
    this.CurrentSeasonStats = new PlayerStats();
    this.CurrentSeasonStats.StatYear = year;
    this.CurrentSeasonStats.StatYearTeam = teamAbbreviation;
  }

  public void UpdateSeasonStatsWithNewTeamName(string newTeamName)
  {
    if (this.CurrentSeasonStats.StatYearTeam == "" || this.CurrentSeasonStats.StatYearTeam == newTeamName)
      return;
    PlayerStats currentSeasonStats = this.CurrentSeasonStats;
    currentSeasonStats.StatYearTeam = currentSeasonStats.StatYearTeam + "," + newTeamName;
  }

  public void AddStatsFromGameToSeasonTotal()
  {
    if (this.CurrentSeasonStats == null)
    {
      Debug.Log((object) "Season stats are null");
      Debug.Log((object) this.FullName);
    }
    else if (this.CurrentGameStats == null)
    {
      Debug.Log((object) "Game Stats are null");
      Debug.Log((object) this.FullName);
    }
    else
    {
      if (this.CareerStats == null || this.TotalCareerStats == null)
        this.CreateCareerStats();
      this.CurrentSeasonStats.QBCompletions += this.CurrentGameStats.QBCompletions;
      this.CurrentSeasonStats.QBAttempts += this.CurrentGameStats.QBAttempts;
      this.CurrentSeasonStats.QBPassYards += this.CurrentGameStats.QBPassYards;
      this.CurrentSeasonStats.QBPassTDs += this.CurrentGameStats.QBPassTDs;
      this.CurrentSeasonStats.QBInts += this.CurrentGameStats.QBInts;
      this.CurrentSeasonStats.RushAttempts += this.CurrentGameStats.RushAttempts;
      this.CurrentSeasonStats.RushYards += this.CurrentGameStats.RushYards;
      this.CurrentSeasonStats.RushTDs += this.CurrentGameStats.RushTDs;
      this.CurrentSeasonStats.Receptions += this.CurrentGameStats.Receptions;
      this.CurrentSeasonStats.ReceivingYards += this.CurrentGameStats.ReceivingYards;
      this.CurrentSeasonStats.ReceivingTDs += this.CurrentGameStats.ReceivingTDs;
      this.CurrentSeasonStats.YardsAfterCatch += this.CurrentGameStats.YardsAfterCatch;
      this.CurrentSeasonStats.Drops += this.CurrentGameStats.Drops;
      this.CurrentSeasonStats.Fumbles += this.CurrentGameStats.Fumbles;
      this.CurrentSeasonStats.Targets += this.CurrentGameStats.Targets;
      this.CurrentSeasonStats.Tackles += this.CurrentGameStats.Tackles;
      this.CurrentSeasonStats.DefensiveTDs += this.CurrentGameStats.DefensiveTDs;
      this.CurrentSeasonStats.TacklesForLoss += this.CurrentGameStats.TacklesForLoss;
      this.CurrentSeasonStats.Sacks += this.CurrentGameStats.Sacks;
      this.CurrentSeasonStats.Interceptions += this.CurrentGameStats.Interceptions;
      this.CurrentSeasonStats.KnockDowns += this.CurrentGameStats.KnockDowns;
      this.CurrentSeasonStats.ForcedFumbles += this.CurrentGameStats.ForcedFumbles;
      this.CurrentSeasonStats.FumbleRecoveries += this.CurrentGameStats.FumbleRecoveries;
      this.CurrentSeasonStats.Penalties += this.CurrentGameStats.Penalties;
      this.CurrentSeasonStats.PenaltyYards += this.CurrentGameStats.PenaltyYards;
      if (this.CurrentSeasonStats.QBLongestPass < this.CurrentGameStats.QBLongestPass)
        this.CurrentSeasonStats.QBLongestPass = this.CurrentGameStats.QBLongestPass;
      if (this.CurrentSeasonStats.LongestRush < this.CurrentGameStats.LongestRush)
        this.CurrentSeasonStats.LongestRush = this.CurrentGameStats.LongestRush;
      if (this.CurrentSeasonStats.LongestReception < this.CurrentGameStats.LongestReception)
        this.CurrentSeasonStats.LongestReception = this.CurrentGameStats.LongestReception;
      this.CurrentSeasonStats.KickReturns += this.CurrentGameStats.KickReturns;
      this.CurrentSeasonStats.KickReturnYards += this.CurrentGameStats.KickReturnYards;
      this.CurrentSeasonStats.KickReturnTDs += this.CurrentGameStats.KickReturnTDs;
      this.CurrentSeasonStats.PuntReturns += this.CurrentGameStats.PuntReturns;
      this.CurrentSeasonStats.PuntReturnYards += this.CurrentGameStats.PuntReturnYards;
      this.CurrentSeasonStats.PuntReturnTDs += this.CurrentGameStats.PuntReturnTDs;
      this.CurrentSeasonStats.FGMade += this.CurrentGameStats.FGMade;
      this.CurrentSeasonStats.FGAttempted += this.CurrentGameStats.FGAttempted;
      this.CurrentSeasonStats.XPMade += this.CurrentGameStats.XPMade;
      this.CurrentSeasonStats.XPAttempted += this.CurrentGameStats.XPAttempted;
      this.CurrentSeasonStats.Punts += this.CurrentGameStats.Punts;
      this.CurrentSeasonStats.PuntsInside20 += this.CurrentGameStats.PuntsInside20;
      this.CurrentSeasonStats.PuntTouchbacks += this.CurrentGameStats.PuntTouchbacks;
      this.CurrentSeasonStats.PuntYards += this.CurrentGameStats.PuntYards;
      this.CurrentSeasonStats.QBTenYardCompletions += this.CurrentGameStats.QBTenYardCompletions;
      this.CurrentSeasonStats.QBTwentyYardCompletions += this.CurrentGameStats.QBTwentyYardCompletions;
      this.CurrentSeasonStats.QBThirtyYardCompletions += this.CurrentGameStats.QBThirtyYardCompletions;
      this.CurrentSeasonStats.QBFiftyYardCompletions += this.CurrentGameStats.QBFiftyYardCompletions;
      if (this.TotalCareerStats == null)
        return;
      this.TotalCareerStats.QBCompletions += this.CurrentGameStats.QBCompletions;
      this.TotalCareerStats.QBAttempts += this.CurrentGameStats.QBAttempts;
      this.TotalCareerStats.QBPassYards += this.CurrentGameStats.QBPassYards;
      this.TotalCareerStats.QBPassTDs += this.CurrentGameStats.QBPassTDs;
      this.TotalCareerStats.QBInts += this.CurrentGameStats.QBInts;
      this.TotalCareerStats.RushAttempts += this.CurrentGameStats.RushAttempts;
      this.TotalCareerStats.RushYards += this.CurrentGameStats.RushYards;
      this.TotalCareerStats.RushTDs += this.CurrentGameStats.RushTDs;
      this.TotalCareerStats.Receptions += this.CurrentGameStats.Receptions;
      this.TotalCareerStats.ReceivingYards += this.CurrentGameStats.ReceivingYards;
      this.TotalCareerStats.ReceivingTDs += this.CurrentGameStats.ReceivingTDs;
      this.TotalCareerStats.YardsAfterCatch += this.CurrentGameStats.YardsAfterCatch;
      this.TotalCareerStats.Drops += this.CurrentGameStats.Drops;
      this.TotalCareerStats.Fumbles += this.CurrentGameStats.Fumbles;
      this.TotalCareerStats.Targets += this.CurrentGameStats.Targets;
      this.TotalCareerStats.Tackles += this.CurrentGameStats.Tackles;
      this.TotalCareerStats.DefensiveTDs += this.CurrentGameStats.DefensiveTDs;
      this.TotalCareerStats.TacklesForLoss += this.CurrentGameStats.TacklesForLoss;
      this.TotalCareerStats.Sacks += this.CurrentGameStats.Sacks;
      this.TotalCareerStats.Interceptions += this.CurrentGameStats.Interceptions;
      this.TotalCareerStats.KnockDowns += this.CurrentGameStats.KnockDowns;
      this.TotalCareerStats.ForcedFumbles += this.CurrentGameStats.ForcedFumbles;
      this.TotalCareerStats.FumbleRecoveries += this.CurrentGameStats.FumbleRecoveries;
      this.TotalCareerStats.Penalties += this.CurrentGameStats.Penalties;
      this.TotalCareerStats.PenaltyYards += this.CurrentGameStats.PenaltyYards;
      if (this.TotalCareerStats.QBLongestPass < this.CurrentGameStats.QBLongestPass)
        this.TotalCareerStats.QBLongestPass = this.CurrentGameStats.QBLongestPass;
      if (this.TotalCareerStats.LongestRush < this.CurrentGameStats.LongestRush)
        this.TotalCareerStats.LongestRush = this.CurrentGameStats.LongestRush;
      if (this.TotalCareerStats.LongestReception < this.CurrentGameStats.LongestReception)
        this.TotalCareerStats.LongestReception = this.CurrentGameStats.LongestReception;
      this.TotalCareerStats.KickReturns += this.CurrentGameStats.KickReturns;
      this.TotalCareerStats.KickReturnYards += this.CurrentGameStats.KickReturnYards;
      this.TotalCareerStats.KickReturnTDs += this.CurrentGameStats.KickReturnTDs;
      this.TotalCareerStats.PuntReturns += this.CurrentGameStats.PuntReturns;
      this.TotalCareerStats.PuntReturnYards += this.CurrentGameStats.PuntReturnYards;
      this.TotalCareerStats.PuntReturnTDs += this.CurrentGameStats.PuntReturnTDs;
      this.TotalCareerStats.FGMade += this.CurrentGameStats.FGMade;
      this.TotalCareerStats.FGAttempted += this.CurrentGameStats.FGAttempted;
      this.TotalCareerStats.XPMade += this.CurrentGameStats.XPMade;
      this.TotalCareerStats.XPAttempted += this.CurrentGameStats.XPAttempted;
      this.TotalCareerStats.Punts += this.CurrentGameStats.Punts;
      this.TotalCareerStats.PuntsInside20 += this.CurrentGameStats.PuntsInside20;
      this.TotalCareerStats.PuntTouchbacks += this.CurrentGameStats.PuntTouchbacks;
      this.TotalCareerStats.PuntYards += this.CurrentGameStats.PuntYards;
      this.TotalCareerStats.QBTenYardCompletions += this.CurrentGameStats.QBTenYardCompletions;
      this.TotalCareerStats.QBTwentyYardCompletions += this.CurrentGameStats.QBTwentyYardCompletions;
      this.TotalCareerStats.QBThirtyYardCompletions += this.CurrentGameStats.QBThirtyYardCompletions;
      this.TotalCareerStats.QBFiftyYardCompletions += this.CurrentGameStats.QBFiftyYardCompletions;
    }
  }

  public void AddSeasonStatsToCareerStats()
  {
    if (this.CareerStats == null)
      this.CreateCareerStats();
    this.CareerStats.Add(this.CurrentSeasonStats);
  }

  public void CreateCareerStats()
  {
    this.CareerStats = new List<PlayerStats>();
    this.TotalCareerStats = new PlayerStats();
  }

  public List<CsvCell> GetCsvRowForPlayerPositionAndName() => new List<CsvCell>()
  {
    new CsvCell()
    {
      Name = "Position",
      Type = CsvCellType.String,
      Value = (object) this.PlayerPosition.ToString()
    },
    new CsvCell()
    {
      Name = "FirstName",
      Type = CsvCellType.String,
      Value = (object) this.FirstName
    },
    new CsvCell()
    {
      Name = "LastName",
      Type = CsvCellType.String,
      Value = (object) this.LastName
    }
  };

  public void TransferAttributes(PlayerAI playerScript, UniformSet uSet, UniformAssetType type)
  {
    playerScript.indexOnTeam = this.IndexOnTeam;
    playerScript.firstName = this.FirstName;
    playerScript.lastName = this.LastName;
    playerScript.playerName = this.FirstName.Substring(0, 1) + ". " + this.LastName;
    playerScript.number = this.Number;
    playerScript.discipline = this.Discipline;
    playerScript.height = this.Height;
    playerScript.weight = this.Weight;
    playerScript.playerPosition = this.PlayerPosition;
    playerScript.speed = AIUtil.Remap(0.6f, 1f, 0.7f, 1f, (float) this.Speed * 0.01f);
    playerScript.savedSpeed = playerScript.speed;
    playerScript.tackleBreaking = this.TackleBreaking;
    playerScript.fumbling = this.Fumbling;
    playerScript.catching = this.Catching;
    playerScript.blocking = this.Blocking;
    playerScript.throwingAccuracy = this.ThrowAccuracy;
    playerScript.kickingPower = this.KickPower;
    playerScript.kickingAccuracy = this.KickAccuracy;
    playerScript.blockBreaking = this.BlockBreaking;
    playerScript.tackling = this.Tackling;
    playerScript.throwingPower = this.ThrowPower;
    playerScript.fitness = this.Fitness;
    playerScript.awareness = this.Awareness;
    playerScript.agility = this.Agility;
    playerScript.coverage = this.Coverage;
    playerScript.hitPower = this.HitPower;
    playerScript.endurance = this.Endurance;
    playerScript.onUserTeam = this.OnUserTeam;
    playerScript.fatigue = (float) this.Fatigue;
    playerScript.isLeftHanded = this.IsLeftHanded;
  }

  public void CopyAttributesFrom(PlayerData player)
  {
    this.Speed = player.Speed;
    this.Catching = player.Catching;
    this.Fumbling = player.Fumbling;
    this.Blocking = player.Blocking;
    this.TackleBreaking = player.TackleBreaking;
    this.ThrowAccuracy = player.ThrowAccuracy;
    this.ThrowPower = player.ThrowPower;
    this.KickAccuracy = player.KickAccuracy;
    this.KickPower = player.KickPower;
    this.BlockBreaking = player.BlockBreaking;
    this.Tackling = player.Tackling;
    this.Fitness = player.Fitness;
    this.Awareness = player.Awareness;
    this.Coverage = player.Coverage;
    this.HitPower = player.HitPower;
    this.Endurance = player.Endurance;
    this.Agility = player.Agility;
    this.Potential = player.Potential;
    this.Discipline = player.Discipline;
    this.Fatigue = player.Fatigue;
    this.PlayerPosition = player.PlayerPosition;
    this.IsLeftHanded = player.IsLeftHanded;
  }

  public PlayerData_Basic CreatePlayerData_Basic() => new PlayerData_Basic(this);

  public string GetStandardHeight()
  {
    int height = this.Height;
    int num1 = height / 12;
    int num2 = height % 12;
    return num1.ToString() + "' " + num2.ToString() + "\"";
  }

  public int GetAttributeByIndex(int attributeIndex)
  {
    switch (attributeIndex)
    {
      case 0:
        return this.Speed;
      case 1:
        return this.TackleBreaking;
      case 2:
        return this.Fumbling;
      case 3:
        return this.Catching;
      case 4:
        return this.Blocking;
      case 5:
        return this.ThrowAccuracy;
      case 6:
        return this.ThrowPower;
      case 7:
        return this.KickAccuracy;
      case 8:
        return this.KickPower;
      case 9:
        return this.BlockBreaking;
      case 10:
        return this.Tackling;
      case 11:
        return this.Fitness;
      case 12:
        return this.Awareness;
      case 13:
        return this.Coverage;
      case 14:
        return this.HitPower;
      case 15:
        return this.Endurance;
      case 16:
        return this.Agility;
      default:
        Debug.Log((object) ("Unknown attribute index: " + attributeIndex.ToString()));
        return 0;
    }
  }

  public string GetCharacteristicByIndex(int index)
  {
    switch (index)
    {
      case 0:
        return this.FirstName;
      case 1:
        return this.LastName;
      case 2:
        return this.SkinColor.ToString();
      case 3:
        return this.Number.ToString();
      case 4:
        return this.Height.ToString();
      case 5:
        return this.Weight.ToString();
      case 6:
        return this.PlayerPosition.ToString();
      case 7:
        return this.Speed.ToString();
      case 8:
        return this.TackleBreaking.ToString();
      case 9:
        return this.Fumbling.ToString();
      case 10:
        return this.Catching.ToString();
      case 11:
        return this.Blocking.ToString();
      case 12:
        return this.ThrowAccuracy.ToString();
      case 13:
        return this.KickPower.ToString();
      case 14:
        return this.KickAccuracy.ToString();
      case 15:
        return this.BlockBreaking.ToString();
      case 16:
        return this.Tackling.ToString();
      case 17:
        return this.ThrowPower.ToString();
      case 18:
        return this.Fitness.ToString();
      case 19:
        return this.Awareness.ToString();
      case 20:
        return this.Agility.ToString();
      case 21:
        return this.Coverage.ToString();
      case 22:
        return this.HitPower.ToString();
      case 23:
        return this.Visor.ToString();
      case 25:
        return this.Sleeves.ToString();
      case 26:
        return this.Bands.ToString();
      case 27:
        return this.Wraps.ToString();
      case 28:
        return this.Age.ToString();
      case 29:
        return this.Potential.ToString();
      case 30:
        return this.PortraitID.ToString();
      case 31:
        return this.Discipline.ToString();
      default:
        return "NOT FOUND";
    }
  }
}
