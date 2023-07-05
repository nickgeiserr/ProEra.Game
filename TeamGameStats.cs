// Decompiled with JetBrains decompiler
// Type: TeamGameStats
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

[MessagePackObject(false)]
[Serializable]
public class TeamGameStats
{
  [IgnoreMember]
  public VariableInt VarScore = new VariableInt(0);
  [Key(0)]
  public int PlayerScore;
  [Key(1)]
  public int RushYards;
  [Key(2)]
  public int PassYards;
  [Key(3)]
  public int Sacks;
  [Key(4)]
  public int DroppedPasses;
  [Key(5)]
  public int Turnovers;
  [Key(6)]
  public int ThirdDownAtt;
  [Key(7)]
  public int ThirdDownSuc;
  [Key(8)]
  public int Ints;
  [Key(9)]
  public int ConsecutiveRunPlays;
  [Key(10)]
  public int ConsecutivePassPlays;
  [Key(11)]
  public int TotalRunPlays;
  [Key(12)]
  public int TotalPassPlays;
  [Key(13)]
  public int Penalties;
  [Key(14)]
  public int PenaltyYards;
  [Key(15)]
  public int TacklesForLoss;
  [Key(16)]
  public int LongestDrive;
  [Key(17)]
  public int FumbleRecoveries;
  [Key(18)]
  public int Interceptions;
  [Key(19)]
  public int ForcedFumbles;
  [Key(20)]
  public readonly int[] ScoreByQuarter = new int[5];
  [Key(21)]
  public float TotalPossessionTime;
  [Key(22)]
  public bool VFormationSatisfied;
  [Key(23)]
  public int MaxDriveFirstDowns;
  [Key(24)]
  public int RedZoneAppearances;
  [Key(25)]
  public int RedZoneTouchdowns;
  [Key(26)]
  public int RushTDs;
  [Key(27)]
  public int RushFiveYards;
  [Key(28)]
  public int TwoPointConversions;
  [Key(31)]
  public List<DriveEndType> PreviousDriveResults = new List<DriveEndType>();
  [Key(32)]
  public Dictionary<string, float> YardGainByPlayType = new Dictionary<string, float>();
  [Key(33)]
  private string HighestYardPlay;
  [Key(34)]
  private float HighestYardValue;

  [IgnoreMember]
  public int Score
  {
    get => this.PlayerScore;
    set
    {
      this.PlayerScore = value;
      this.VarScore.Value = value;
    }
  }

  [IgnoreMember]
  public float RedZoneEfficiency => this.RedZoneAppearances != 0 ? (float) this.RedZoneTouchdowns / (float) this.RedZoneAppearances : 0.0f;

  [IgnoreMember]
  public float PassCompletionPercentage { get; private set; }

  [Key(29)]
  public int Touchdowns { get; set; }

  [Key(30)]
  public int TotalFirstDowns { get; set; }

  public int TotalYards() => this.PassYards + this.RushYards;

  public void IncrementScore(int quarter, int score)
  {
    this.Score += score;
    try
    {
      this.ScoreByQuarter[quarter] += score;
    }
    catch (Exception ex)
    {
      Debug.LogException(ex);
    }
  }

  public void StoreDriveEnd(DriveEndType det) => this.PreviousDriveResults.Add(det);

  public bool HadRecentDriveEndType(DriveEndType type)
  {
    int num1 = 3;
    int num2 = this.PreviousDriveResults.Count - 1;
    for (int index = num2; index >= Mathf.Max(0, num2 - num1); --index)
    {
      if (this.PreviousDriveResults[index] == type)
        return true;
    }
    return false;
  }

  public void StoreYardGainByPlayType(PlayDataOff offPlay, float yardsGained)
  {
    string fullPlayName = this.GetFullPlayName(offPlay);
    if (!this.YardGainByPlayType.ContainsKey(fullPlayName))
      this.YardGainByPlayType.Add(fullPlayName, yardsGained);
    else
      this.YardGainByPlayType[fullPlayName] += yardsGained;
    if ((double) this.YardGainByPlayType[fullPlayName] <= (double) this.HighestYardValue)
      return;
    this.HighestYardValue = this.YardGainByPlayType[fullPlayName];
    this.HighestYardPlay = fullPlayName;
  }

  public bool IsHighestYardPlay(PlayDataOff offPlay) => this.GetFullPlayName(offPlay) == this.HighestYardPlay;

  private string GetFullPlayName(PlayDataOff offPlay)
  {
    FormationPositions formation = offPlay.GetFormation();
    return formation.GetBaseFormationString() + formation.GetSubFormationString() + offPlay.GetPlayName();
  }

  public void AddPossessionTime(float time) => this.TotalPossessionTime += time;

  private void CalculateCompletionPercent(TeamData teamData)
  {
    int numberOfPlayers = teamData.MainRoster.GetNumberOfPlayers();
    int num1 = 0;
    int num2 = 0;
    for (int playerIndex = 0; playerIndex < numberOfPlayers; ++playerIndex)
    {
      PlayerData player = teamData.GetPlayer(playerIndex);
      if (player != null && player.CurrentGameStats != null)
      {
        num1 += player.CurrentGameStats.QBCompletions;
        num2 += player.CurrentGameStats.QBAttempts;
      }
    }
    this.PassCompletionPercentage = num2 > 0 ? (float) num1 / (float) num2 : 0.0f;
  }

  public void FinializeGameStats(TeamData teamData) => this.CalculateCompletionPercent(teamData);

  public void Reset()
  {
    this.Score = 0;
    this.RushYards = 0;
    this.PassYards = 0;
    this.Sacks = 0;
    this.DroppedPasses = 0;
    this.Turnovers = 0;
    this.ThirdDownAtt = 0;
    this.ThirdDownSuc = 0;
    this.Ints = 0;
    this.ConsecutiveRunPlays = 0;
    this.ConsecutivePassPlays = 0;
    this.TotalRunPlays = 0;
    this.TotalPassPlays = 0;
    this.Penalties = 0;
    this.PenaltyYards = 0;
    this.TacklesForLoss = 0;
    this.LongestDrive = 0;
    this.FumbleRecoveries = 0;
    this.Interceptions = 0;
    this.ForcedFumbles = 0;
    this.ScoreByQuarter[0] = 0;
    this.ScoreByQuarter[1] = 0;
    this.ScoreByQuarter[2] = 0;
    this.ScoreByQuarter[3] = 0;
    this.ScoreByQuarter[4] = 0;
    this.PreviousDriveResults.Clear();
    this.YardGainByPlayType.Clear();
    this.HighestYardValue = 0.0f;
    this.HighestYardPlay = "";
    this.VFormationSatisfied = false;
    this.MaxDriveFirstDowns = 0;
    this.RushTDs = 0;
    this.RushFiveYards = 0;
    this.TwoPointConversions = 0;
    this.Touchdowns = 0;
    this.TotalFirstDowns = 0;
  }

  public void Print()
  {
    Debug.Log((object) " Team Stats:");
    Debug.Log((object) string.Format("  Score: {0}", (object) this.Score));
    Debug.Log((object) string.Format("  RushYards: {0}", (object) this.RushYards));
    Debug.Log((object) string.Format("  PassYards: {0}", (object) this.PassYards));
    Debug.Log((object) string.Format("  Sacks: {0}", (object) this.Sacks));
    Debug.Log((object) string.Format("  DroppedPasses: {0}", (object) this.DroppedPasses));
    Debug.Log((object) string.Format("  Turnovers: {0}", (object) this.Turnovers));
    Debug.Log((object) string.Format("  ThirdDownAtt: {0}", (object) this.ThirdDownAtt));
    Debug.Log((object) string.Format("  ThirdDownSuc: {0}", (object) this.ThirdDownSuc));
    Debug.Log((object) string.Format("  Ints: {0}", (object) this.Ints));
    Debug.Log((object) string.Format("  ConsecutiveRunPlays: {0}", (object) this.ConsecutiveRunPlays));
    Debug.Log((object) string.Format("  ConsecutivePassPlays: {0}", (object) this.ConsecutivePassPlays));
    Debug.Log((object) string.Format("  TotalRunPlays: {0}", (object) this.TotalRunPlays));
    Debug.Log((object) string.Format("  TotalPassPlays: {0}", (object) this.TotalPassPlays));
    Debug.Log((object) string.Format("  Penalties: {0}", (object) this.Penalties));
    Debug.Log((object) string.Format("  PenaltyYards: {0}", (object) this.PenaltyYards));
    Debug.Log((object) string.Format("  TacklesForLoss: {0}", (object) this.TacklesForLoss));
    Debug.Log((object) string.Format("  LongestDrive: {0}", (object) this.LongestDrive));
    Debug.Log((object) string.Format("  FumbleRecoveries: {0}", (object) this.FumbleRecoveries));
    Debug.Log((object) string.Format("  Interceptions: {0}", (object) this.Interceptions));
    Debug.Log((object) string.Format("  ForcedFumbles: {0}", (object) this.ForcedFumbles));
    Debug.Log((object) string.Format("  ScoreByQuarter: 1: {0} 2:{1}  3:{2} 4:{3} 5:{4}", (object) this.ScoreByQuarter[0], (object) this.ScoreByQuarter[1], (object) this.ScoreByQuarter[2], (object) this.ScoreByQuarter[3], (object) this.ScoreByQuarter[4]));
  }

  public List<CsvCell> GetCsvRow() => new List<CsvCell>()
  {
    new CsvCell()
    {
      Name = "Score",
      Type = CsvCellType.Number,
      Value = (object) this.Score
    },
    new CsvCell()
    {
      Name = "RushYards",
      Type = CsvCellType.Number,
      Value = (object) this.RushYards
    },
    new CsvCell()
    {
      Name = "PassYards",
      Type = CsvCellType.Number,
      Value = (object) this.PassYards
    },
    new CsvCell()
    {
      Name = "Sacks",
      Type = CsvCellType.Number,
      Value = (object) this.Sacks
    },
    new CsvCell()
    {
      Name = "DroppedPasses",
      Type = CsvCellType.Number,
      Value = (object) this.DroppedPasses
    },
    new CsvCell()
    {
      Name = "Turnovers",
      Type = CsvCellType.Number,
      Value = (object) this.Turnovers
    },
    new CsvCell()
    {
      Name = "ThirdDownAtt",
      Type = CsvCellType.Number,
      Value = (object) this.ThirdDownAtt
    },
    new CsvCell()
    {
      Name = "ThirdDownSuc",
      Type = CsvCellType.Number,
      Value = (object) this.ThirdDownSuc
    },
    new CsvCell()
    {
      Name = "Ints",
      Type = CsvCellType.Number,
      Value = (object) this.Ints
    },
    new CsvCell()
    {
      Name = "ConsecutiveRunPlays",
      Type = CsvCellType.Number,
      Value = (object) this.ConsecutiveRunPlays
    },
    new CsvCell()
    {
      Name = "ConsecutivePassPlays",
      Type = CsvCellType.Number,
      Value = (object) this.ConsecutivePassPlays
    },
    new CsvCell()
    {
      Name = "TotalRunPlays",
      Type = CsvCellType.Number,
      Value = (object) this.TotalRunPlays
    },
    new CsvCell()
    {
      Name = "TotalPassPlays",
      Type = CsvCellType.Number,
      Value = (object) this.TotalPassPlays
    },
    new CsvCell()
    {
      Name = "Penalties",
      Type = CsvCellType.Number,
      Value = (object) this.Penalties
    },
    new CsvCell()
    {
      Name = "PenaltyYards",
      Type = CsvCellType.Number,
      Value = (object) this.PenaltyYards
    },
    new CsvCell()
    {
      Name = "TacklesForLoss",
      Type = CsvCellType.Number,
      Value = (object) this.TacklesForLoss
    },
    new CsvCell()
    {
      Name = "LongestDrive",
      Type = CsvCellType.Number,
      Value = (object) this.LongestDrive
    },
    new CsvCell()
    {
      Name = "FumbleRecoveries",
      Type = CsvCellType.Number,
      Value = (object) this.FumbleRecoveries
    },
    new CsvCell()
    {
      Name = "Interceptions",
      Type = CsvCellType.Number,
      Value = (object) this.Interceptions
    },
    new CsvCell()
    {
      Name = "ForcedFumbles",
      Type = CsvCellType.Number,
      Value = (object) this.ForcedFumbles
    },
    new CsvCell()
    {
      Name = "ScoreByQuarter",
      Type = CsvCellType.Number,
      Value = (object) this.ScoreByQuarter
    }
  };
}
