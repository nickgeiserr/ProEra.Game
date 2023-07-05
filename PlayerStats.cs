// Decompiled with JetBrains decompiler
// Type: PlayerStats
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class PlayerStats : ICsvRow
{
  [Key(0)]
  public int QBCompletions;
  [Key(1)]
  public int QBAttempts;
  [Key(2)]
  public int QBPassYards;
  [Key(3)]
  public int QBPassTDs;
  [Key(4)]
  public int QBInts;
  [Key(5)]
  public int QBLongestPass;
  [Key(6)]
  public int RushAttempts;
  [Key(7)]
  public int RushYards;
  [Key(8)]
  public int RushTDs;
  [Key(9)]
  public int LongestRush;
  [Key(10)]
  public int Receptions;
  [Key(11)]
  public int ReceivingYards;
  [Key(12)]
  public int ReceivingTDs;
  [Key(13)]
  public int LongestReception;
  [Key(14)]
  public int YardsAfterCatch;
  [Key(15)]
  public int Drops;
  [Key(16)]
  public int Fumbles;
  [Key(17)]
  public int Targets;
  [Key(18)]
  public int Tackles;
  [Key(19)]
  public int Sacks;
  [Key(20)]
  public int Interceptions;
  [Key(21)]
  public int DefensiveTDs;
  [Key(22)]
  public int TacklesForLoss;
  [Key(23)]
  public int KnockDowns;
  [Key(24)]
  public int ForcedFumbles;
  [Key(25)]
  public int FumbleRecoveries;
  [Key(26)]
  public int Penalties;
  [Key(27)]
  public int PenaltyYards;
  [Key(28)]
  public int FGMade;
  [Key(29)]
  public int FGAttempted;
  [Key(30)]
  public int XPMade;
  [Key(31)]
  public int XPAttempted;
  [Key(32)]
  public int Punts;
  [Key(33)]
  public int PuntsInside20;
  [Key(34)]
  public int PuntTouchbacks;
  [Key(35)]
  public int PuntYards;
  [Key(36)]
  public int PuntReturns;
  [Key(37)]
  public int PuntReturnYards;
  [Key(38)]
  public int PuntReturnTDs;
  [Key(39)]
  public int KickReturns;
  [Key(40)]
  public int KickReturnYards;
  [Key(41)]
  public int KickReturnTDs;
  [Key(42)]
  public int QBTenYardCompletions;
  [Key(43)]
  public int QBTwentyYardCompletions;
  [Key(44)]
  public int QBThirtyYardCompletions;
  [Key(45)]
  public int QBFiftyYardCompletions;
  [Key(46)]
  public int QBSacked;
  [Key(47)]
  public int StatYear;
  [Key(48)]
  public string StatYearTeam;

  [IgnoreMember]
  public float CompletionPercent => (double) this.QBAttempts <= 0.0 ? 0.0f : (float) this.QBCompletions / (float) this.QBAttempts;

  [IgnoreMember]
  public int TotalTDs => this.QBPassTDs + this.ReceivingTDs + this.RushTDs + this.DefensiveTDs + this.KickReturnTDs + this.PuntReturnTDs;

  public PlayerStats()
  {
    this.StatYear = 0;
    this.StatYearTeam = "";
  }

  public List<CsvCell> GetCsvRow() => new List<CsvCell>()
  {
    new CsvCell()
    {
      Name = "QBCompletions",
      Type = CsvCellType.Number,
      Value = (object) this.QBCompletions
    },
    new CsvCell()
    {
      Name = "QBAttempts",
      Type = CsvCellType.Number,
      Value = (object) this.QBAttempts
    },
    new CsvCell()
    {
      Name = "QBPassYards",
      Type = CsvCellType.Number,
      Value = (object) this.QBPassYards
    },
    new CsvCell()
    {
      Name = "QBPassTDs",
      Type = CsvCellType.Number,
      Value = (object) this.QBPassTDs
    },
    new CsvCell()
    {
      Name = "QBInts",
      Type = CsvCellType.Number,
      Value = (object) this.QBInts
    },
    new CsvCell()
    {
      Name = "QBLongestPass",
      Type = CsvCellType.Number,
      Value = (object) this.QBLongestPass
    },
    new CsvCell()
    {
      Name = "RushAttempts",
      Type = CsvCellType.Number,
      Value = (object) this.RushAttempts
    },
    new CsvCell()
    {
      Name = "RushYards",
      Type = CsvCellType.Number,
      Value = (object) this.RushYards
    },
    new CsvCell()
    {
      Name = "RushTDs",
      Type = CsvCellType.Number,
      Value = (object) this.RushTDs
    },
    new CsvCell()
    {
      Name = "LongestRush",
      Type = CsvCellType.Number,
      Value = (object) this.LongestRush
    },
    new CsvCell()
    {
      Name = "Fumbles",
      Type = CsvCellType.Number,
      Value = (object) this.Fumbles
    },
    new CsvCell()
    {
      Name = "Receptions",
      Type = CsvCellType.Number,
      Value = (object) this.Receptions
    },
    new CsvCell()
    {
      Name = "ReceivingYards",
      Type = CsvCellType.Number,
      Value = (object) this.ReceivingYards
    },
    new CsvCell()
    {
      Name = "ReceivingTDs",
      Type = CsvCellType.Number,
      Value = (object) this.ReceivingTDs
    },
    new CsvCell()
    {
      Name = "LongestReception",
      Type = CsvCellType.Number,
      Value = (object) this.LongestReception
    },
    new CsvCell()
    {
      Name = "YardsAfterCatch",
      Type = CsvCellType.Number,
      Value = (object) this.YardsAfterCatch
    },
    new CsvCell()
    {
      Name = "Drops",
      Type = CsvCellType.Number,
      Value = (object) this.Drops
    },
    new CsvCell()
    {
      Name = "Targets",
      Type = CsvCellType.Number,
      Value = (object) this.Targets
    },
    new CsvCell()
    {
      Name = "Tackles",
      Type = CsvCellType.Number,
      Value = (object) this.Tackles
    },
    new CsvCell()
    {
      Name = "Sacks",
      Type = CsvCellType.Number,
      Value = (object) this.Sacks
    },
    new CsvCell()
    {
      Name = "Interceptions",
      Type = CsvCellType.Number,
      Value = (object) this.Interceptions
    },
    new CsvCell()
    {
      Name = "KnockDowns",
      Type = CsvCellType.Number,
      Value = (object) this.KnockDowns
    },
    new CsvCell()
    {
      Name = "ForcedFumbles",
      Type = CsvCellType.Number,
      Value = (object) this.ForcedFumbles
    },
    new CsvCell()
    {
      Name = "FumbleRecoveries",
      Type = CsvCellType.Number,
      Value = (object) this.FumbleRecoveries
    },
    new CsvCell()
    {
      Name = "DefensiveTDs",
      Type = CsvCellType.Number,
      Value = (object) this.DefensiveTDs
    },
    new CsvCell()
    {
      Name = "TacklesForLoss",
      Type = CsvCellType.Number,
      Value = (object) this.TacklesForLoss
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
      Name = "FGMade",
      Type = CsvCellType.Number,
      Value = (object) this.FGMade
    },
    new CsvCell()
    {
      Name = "FGAttempted",
      Type = CsvCellType.Number,
      Value = (object) this.FGAttempted
    },
    new CsvCell()
    {
      Name = "XPMade",
      Type = CsvCellType.Number,
      Value = (object) this.XPMade
    },
    new CsvCell()
    {
      Name = "XPAttempted",
      Type = CsvCellType.Number,
      Value = (object) this.XPAttempted
    },
    new CsvCell()
    {
      Name = "Punts",
      Type = CsvCellType.Number,
      Value = (object) this.Punts
    },
    new CsvCell()
    {
      Name = "PuntsInside20",
      Type = CsvCellType.Number,
      Value = (object) this.PuntsInside20
    },
    new CsvCell()
    {
      Name = "PuntTouchbacks",
      Type = CsvCellType.Number,
      Value = (object) this.PuntTouchbacks
    },
    new CsvCell()
    {
      Name = "PuntYards",
      Type = CsvCellType.Number,
      Value = (object) this.PuntYards
    },
    new CsvCell()
    {
      Name = "PuntReturns",
      Type = CsvCellType.Number,
      Value = (object) this.PuntReturns
    },
    new CsvCell()
    {
      Name = "PuntReturnYards",
      Type = CsvCellType.Number,
      Value = (object) this.PuntReturnYards
    },
    new CsvCell()
    {
      Name = "PuntReturnTDs",
      Type = CsvCellType.Number,
      Value = (object) this.PuntReturnTDs
    },
    new CsvCell()
    {
      Name = "KickReturns",
      Type = CsvCellType.Number,
      Value = (object) this.KickReturns
    },
    new CsvCell()
    {
      Name = "KickReturnYards",
      Type = CsvCellType.Number,
      Value = (object) this.KickReturnYards
    },
    new CsvCell()
    {
      Name = "KickReturnTDs",
      Type = CsvCellType.Number,
      Value = (object) this.KickReturnTDs
    },
    new CsvCell()
    {
      Name = "QBTenYardCompletions",
      Type = CsvCellType.Number,
      Value = (object) this.QBTenYardCompletions
    },
    new CsvCell()
    {
      Name = "QBTwentyYardCompletions",
      Type = CsvCellType.Number,
      Value = (object) this.QBTwentyYardCompletions
    },
    new CsvCell()
    {
      Name = "QBThirtyYardCompletions",
      Type = CsvCellType.Number,
      Value = (object) this.QBThirtyYardCompletions
    },
    new CsvCell()
    {
      Name = "QBFiftyYardCompletions",
      Type = CsvCellType.Number,
      Value = (object) this.QBFiftyYardCompletions
    },
    new CsvCell()
    {
      Name = "QBSacked",
      Type = CsvCellType.Number,
      Value = (object) this.QBSacked
    }
  };

  public static List<string> GetStatHighlights(PlayerStats playerStats, Position position)
  {
    List<string> statHighlights = new List<string>();
    switch (position)
    {
      case Position.QB:
        statHighlights.Add("COMP");
        if (playerStats.QBAttempts < 100)
          statHighlights.Add(playerStats.QBCompletions.ToString() + "/" + playerStats.QBAttempts.ToString());
        else
          statHighlights.Add(Mathf.RoundToInt((float) ((double) playerStats.QBCompletions / (double) playerStats.QBAttempts * 100.0)).ToString() + "%");
        statHighlights.Add("YDS");
        statHighlights.Add(playerStats.QBPassYards.ToString("#,##0"));
        if (playerStats.QBPassTDs > 0)
        {
          statHighlights.Add("TDS");
          statHighlights.Add(playerStats.QBPassTDs.ToString());
          break;
        }
        break;
      case Position.RB:
      case Position.FB:
        statHighlights.Add("RUSH");
        statHighlights.Add(playerStats.RushAttempts.ToString());
        statHighlights.Add("YDS");
        statHighlights.Add(playerStats.RushYards.ToString("#,##0"));
        if (playerStats.TotalTDs > 0)
        {
          statHighlights.Add("TDS");
          statHighlights.Add(playerStats.TotalTDs.ToString());
          break;
        }
        break;
      case Position.WR:
      case Position.TE:
        statHighlights.Add("REC");
        statHighlights.Add(playerStats.Receptions.ToString());
        statHighlights.Add("YDS");
        statHighlights.Add(playerStats.ReceivingYards.ToString("#,##0"));
        if (playerStats.TotalTDs > 0)
        {
          statHighlights.Add("TDS");
          statHighlights.Add(playerStats.TotalTDs.ToString());
          break;
        }
        break;
      case Position.OL:
      case Position.DL:
      case Position.LB:
      case Position.DB:
        int num1 = 0;
        int num2 = 3;
        if (playerStats.TotalTDs > 0)
        {
          statHighlights.Add("TDS");
          statHighlights.Add(playerStats.TotalTDs.ToString());
          ++num1;
        }
        if (playerStats.Interceptions > 0)
        {
          statHighlights.Add("INT");
          statHighlights.Add(playerStats.Interceptions.ToString());
          ++num1;
        }
        if (playerStats.Sacks > 0)
        {
          statHighlights.Add("SACK");
          statHighlights.Add(playerStats.Sacks.ToString());
          ++num1;
        }
        if (num1 < num2 && playerStats.TacklesForLoss > 0)
        {
          statHighlights.Add("TFL");
          statHighlights.Add(playerStats.TacklesForLoss.ToString());
          ++num1;
        }
        if (num1 < num2 && playerStats.ForcedFumbles > 0)
        {
          statHighlights.Add("FF");
          statHighlights.Add(playerStats.ForcedFumbles.ToString());
          ++num1;
        }
        if (num1 < num2 && playerStats.Tackles >= 0)
        {
          statHighlights.Add("TKL");
          statHighlights.Add(playerStats.Tackles.ToString());
          ++num1;
        }
        if (num1 < num2 && playerStats.KnockDowns > 0)
        {
          statHighlights.Add("PDEF");
          statHighlights.Add(playerStats.KnockDowns.ToString());
          int num3 = num1 + 1;
          break;
        }
        break;
      case Position.K:
        if (playerStats.FGMade > 0)
        {
          statHighlights.Add("FGS");
          statHighlights.Add(playerStats.FGMade.ToString() + "/" + playerStats.FGAttempted.ToString());
        }
        if (playerStats.XPMade > 0)
        {
          statHighlights.Add("PATS");
          statHighlights.Add(playerStats.XPMade.ToString() + "/" + playerStats.XPAttempted.ToString());
          break;
        }
        break;
      default:
        Debug.Log((object) ("Unspecified position earned an award by Position: " + position.ToString()));
        break;
    }
    return statHighlights;
  }

  public static int GetOverallMVP(RosterData roster, StatDuration statDuration, int excludedIndex = -1)
  {
    float num1 = 0.0f;
    int overallMvp = 0;
    for (int playerIndex = 0; playerIndex < roster.GetNumberOfPlayers(); ++playerIndex)
    {
      PlayerData player = roster.GetPlayer(playerIndex);
      if (playerIndex != excludedIndex && player != null)
      {
        PlayerStats playerStats = statDuration != StatDuration.CurrentGame ? player.CurrentSeasonStats : player.CurrentGameStats;
        float num2 = 0.0f + PlayerStats.GetOffensiveStatScore(playerStats) + PlayerStats.GetDefensiveStatScore(playerStats);
        if ((double) num2 > (double) num1)
        {
          num1 = num2;
          overallMvp = playerIndex;
        }
      }
    }
    return overallMvp;
  }

  public static int GetOffensiveMVP(
    RosterData roster,
    StatDuration statDuration,
    int excludedIndex = -1)
  {
    float num = 0.0f;
    int offensiveMvp = 0;
    for (int playerIndex = 0; playerIndex < roster.GetNumberOfPlayers(); ++playerIndex)
    {
      PlayerData player = roster.GetPlayer(playerIndex);
      if (playerIndex != excludedIndex && player != null)
      {
        float offensiveStatScore = PlayerStats.GetOffensiveStatScore(statDuration != StatDuration.CurrentGame ? player.CurrentSeasonStats : player.CurrentGameStats);
        if ((double) offensiveStatScore > (double) num)
        {
          num = offensiveStatScore;
          offensiveMvp = playerIndex;
        }
      }
    }
    return offensiveMvp;
  }

  public static List<int[]> GetConferenceMVP(
    int conferenceIndex,
    StatDuration statDuration,
    MVPType type,
    int returnAmount = 1)
  {
    SGD_SeasonModeData seasonModeData = SeasonModeManager.self.seasonModeData;
    SortedDictionary<float, int[]> source = new SortedDictionary<float, int[]>();
    int[] numArray;
    if (conferenceIndex > 0)
    {
      numArray = seasonModeData.GetTeamsInConference(conferenceIndex);
    }
    else
    {
      numArray = new int[seasonModeData.NumberOfTeamsInLeague];
      for (int index = 0; index < seasonModeData.NumberOfTeamsInLeague; ++index)
        numArray[index] = index;
    }
    for (int index = 0; index < numArray.Length; ++index)
    {
      RosterData mainRoster = seasonModeData.GetTeamData(numArray[index]).MainRoster;
      for (int playerIndex = 0; playerIndex < mainRoster.GetNumberOfPlayers(); ++playerIndex)
      {
        PlayerData player = mainRoster.GetPlayer(playerIndex);
        if (player != null)
        {
          PlayerStats playerStats = statDuration != StatDuration.CurrentGame ? player.CurrentSeasonStats : player.CurrentGameStats;
          float key = 0.0f;
          if (type == MVPType.Offensive || type == MVPType.Overall)
            key += PlayerStats.GetOffensiveStatScore(playerStats);
          if (type == MVPType.Defensive || type == MVPType.Overall)
            key += PlayerStats.GetDefensiveStatScore(playerStats);
          if (source.Count < returnAmount)
          {
            if (!source.ContainsKey(key))
              source.Add(key, new int[2]
              {
                index,
                playerIndex
              });
            else
              source.Add((float) ((double) key + (double) index / 100.0 + (double) playerIndex / 10000.0), new int[2]
              {
                index,
                playerIndex
              });
          }
          else if ((double) source.Keys.First<float>() < (double) key)
          {
            source.Remove(source.Keys.First<float>());
            if (!source.ContainsKey(key))
              source.Add(key, new int[2]
              {
                index,
                playerIndex
              });
            else
              source.Add((float) ((double) key + (double) index / 100.0 + (double) playerIndex / 10000.0), new int[2]
              {
                index,
                playerIndex
              });
          }
        }
      }
    }
    List<int[]> conferenceMvp = new List<int[]>();
    foreach (KeyValuePair<float, int[]> keyValuePair in source.Reverse<KeyValuePair<float, int[]>>())
      conferenceMvp.Add(keyValuePair.Value);
    return conferenceMvp;
  }

  public static int GetDefensiveMVP(
    RosterData roster,
    StatDuration statDuration,
    int excludedIndex = -1)
  {
    float num = 0.0f;
    int defensiveMvp = 0;
    for (int playerIndex = 0; playerIndex < roster.GetNumberOfPlayers(); ++playerIndex)
    {
      PlayerData player = roster.GetPlayer(playerIndex);
      if (playerIndex != excludedIndex && player != null)
      {
        float defensiveStatScore = PlayerStats.GetDefensiveStatScore(statDuration != StatDuration.CurrentGame ? player.CurrentSeasonStats : player.CurrentGameStats);
        if ((double) defensiveStatScore > (double) num)
        {
          num = defensiveStatScore;
          defensiveMvp = playerIndex;
        }
      }
    }
    return defensiveMvp;
  }

  public static int GetOffensiveMVP(PlayerStats[] playerStatsList, int excludedIndex = -1)
  {
    float num = 0.0f;
    int offensiveMvp = 0;
    for (int index = 0; index < playerStatsList.Length; ++index)
    {
      PlayerStats playerStats = playerStatsList[index];
      if (index != excludedIndex && playerStats != null)
      {
        float offensiveStatScore = PlayerStats.GetOffensiveStatScore(playerStats);
        if ((double) offensiveStatScore > (double) num)
        {
          num = offensiveStatScore;
          offensiveMvp = index;
        }
      }
    }
    return offensiveMvp;
  }

  public static int GetDefensiveMVP(PlayerStats[] playerStatsList, int excludedIndex = -1)
  {
    float num = 0.0f;
    int defensiveMvp = 0;
    for (int index = 0; index < playerStatsList.Length; ++index)
    {
      PlayerStats playerStats = playerStatsList[index];
      if (index != excludedIndex && playerStats != null)
      {
        float defensiveStatScore = PlayerStats.GetDefensiveStatScore(playerStats);
        if ((double) defensiveStatScore > (double) num)
        {
          num = defensiveStatScore;
          defensiveMvp = index;
        }
      }
    }
    return defensiveMvp;
  }

  public static int GetMVPByPosition(
    RosterData roster,
    Position p,
    StatDuration statDuration,
    int excludedIndex = -1)
  {
    float num1 = 0.0f;
    int mvpByPosition = 0;
    for (int playerIndex = 0; playerIndex < roster.GetNumberOfPlayers(); ++playerIndex)
    {
      PlayerData player = roster.GetPlayer(playerIndex);
      if (playerIndex != excludedIndex && player != null && player.PlayerPosition == p)
      {
        PlayerStats playerStats = statDuration != StatDuration.CurrentGame ? player.CurrentSeasonStats : player.CurrentGameStats;
        float num2 = 0.0f + PlayerStats.GetOffensiveStatScore(playerStats) + PlayerStats.GetDefensiveStatScore(playerStats);
        if ((double) num2 > (double) num1)
        {
          num1 = num2;
          mvpByPosition = playerIndex;
        }
      }
    }
    return mvpByPosition;
  }

  public static List<int> GetPlayersActiveInStatCategory(
    PlayerStats[] playerStats,
    StatCategory statCategory,
    int numberOfPlayersToFind)
  {
    List<int> activeInStatCategory = new List<int>();
    List<float> floatList = new List<float>();
    for (int index1 = 0; index1 < playerStats.Length; ++index1)
    {
      float activityByCategory = PlayerStats.GetStatActivityByCategory(playerStats[index1], statCategory);
      if ((double) activityByCategory > 0.0)
      {
        if (activeInStatCategory.Count < numberOfPlayersToFind)
        {
          activeInStatCategory.Add(index1);
          floatList.Add(activityByCategory);
        }
        else
        {
          float num = floatList[0];
          int index2 = 0;
          for (int index3 = 1; index3 < numberOfPlayersToFind; ++index3)
          {
            if ((double) floatList[index3] < (double) num)
            {
              index2 = index3;
              num = floatList[index3];
            }
          }
          if ((double) activityByCategory > (double) num)
          {
            activeInStatCategory[index2] = index1;
            floatList[index2] = activityByCategory;
          }
        }
      }
    }
    for (int index4 = 0; index4 < activeInStatCategory.Count - 1; ++index4)
    {
      int index5 = index4;
      float num1 = floatList[index4];
      for (int index6 = index4 + 1; index6 < activeInStatCategory.Count; ++index6)
      {
        float num2 = floatList[index6];
        if ((double) num2 > (double) num1)
        {
          index5 = index6;
          num1 = num2;
        }
      }
      int num3 = activeInStatCategory[index4];
      activeInStatCategory[index4] = activeInStatCategory[index5];
      activeInStatCategory[index5] = num3;
      float num4 = floatList[index4];
      floatList[index4] = floatList[index5];
      floatList[index5] = num4;
    }
    return activeInStatCategory;
  }

  public static int GetRookieMVP(RosterData roster, StatDuration statDuration, int excludedIndex = -1)
  {
    float num1 = 0.0f;
    int rookieMvp = 0;
    for (int playerIndex = 0; playerIndex < roster.GetNumberOfPlayers(); ++playerIndex)
    {
      PlayerData player = roster.GetPlayer(playerIndex);
      if (playerIndex != excludedIndex && player != null && player.YearsPro <= 0)
      {
        PlayerStats playerStats = statDuration != StatDuration.CurrentGame ? player.CurrentSeasonStats : player.CurrentGameStats;
        float num2 = 0.0f + PlayerStats.GetOffensiveStatScore(playerStats) + PlayerStats.GetDefensiveStatScore(playerStats);
        if ((double) num2 >= (double) num1)
        {
          num1 = num2;
          rookieMvp = playerIndex;
        }
      }
    }
    return rookieMvp;
  }

  public static float GetTotalStatScore(PlayerStats player) => PlayerStats.GetOffensiveStatScore(player) + PlayerStats.GetDefensiveStatScore(player);

  public static float GetOffensiveStatScore(PlayerStats playerStats)
  {
    float offensiveStatScore = 0.0f + (float) playerStats.QBAttempts * 0.01f + (float) playerStats.QBPassTDs * 20f + (float) playerStats.QBPassYards * 0.25f - (float) playerStats.QBInts * 5f + (float) playerStats.RushAttempts * 0.25f + (float) playerStats.RushYards * 0.5f + (float) playerStats.Receptions * 2f + (float) playerStats.ReceivingYards * 0.5f + (float) playerStats.RushTDs * 25f + (float) playerStats.ReceivingTDs * 25f + (float) playerStats.PuntReturnTDs * 25f + (float) playerStats.KickReturnTDs * 25f - (float) playerStats.Drops - (float) playerStats.Fumbles * 5f + (float) playerStats.FGMade * 7f + (playerStats.FGMade == playerStats.FGAttempted ? 7f : 0.0f) + (float) playerStats.XPMade * 4f + (playerStats.XPMade == playerStats.XPAttempted ? 4f : 0.0f) + (float) playerStats.PuntsInside20 * 7f;
    if (playerStats.QBAttempts > 5)
    {
      float num = (float) playerStats.QBCompletions / (float) playerStats.QBAttempts;
      if ((double) num > 0.949999988079071)
        offensiveStatScore += 10f;
      else if ((double) num > 0.89999997615814209)
        offensiveStatScore += 8f;
      else if ((double) num > 0.85000002384185791)
        offensiveStatScore += 6f;
      else if ((double) num > 0.800000011920929)
        offensiveStatScore += 4f;
      else if ((double) num > 0.75)
        offensiveStatScore += 2f;
      else if ((double) num > 0.699999988079071)
        ++offensiveStatScore;
    }
    return offensiveStatScore;
  }

  public static float GetStatActivityByCategory(PlayerStats playerStats, StatCategory category)
  {
    float activityByCategory = 0.0f;
    switch (category)
    {
      case StatCategory.Passing:
        activityByCategory += (float) playerStats.QBAttempts;
        break;
      case StatCategory.Rushing:
        activityByCategory += (float) playerStats.RushAttempts;
        break;
      case StatCategory.Receiving:
        activityByCategory = activityByCategory + (float) playerStats.Receptions + (float) playerStats.Targets;
        break;
      case StatCategory.Defense:
        activityByCategory = activityByCategory + (float) playerStats.Tackles + (float) playerStats.Sacks * 3f + (float) playerStats.KnockDowns + (float) playerStats.Interceptions * 10f + (float) playerStats.FumbleRecoveries * 10f + (float) playerStats.ForcedFumbles * 5f;
        break;
      case StatCategory.Kicking:
        activityByCategory = activityByCategory + (float) playerStats.Punts + (float) playerStats.FGAttempted + (float) playerStats.XPAttempted;
        break;
      case StatCategory.Returns:
        activityByCategory = activityByCategory + (float) playerStats.PuntReturns + (float) playerStats.KickReturns;
        break;
    }
    return activityByCategory;
  }

  public static float GetDefensiveStatScore(PlayerStats playerStats) => (float) (0.0 + (double) playerStats.FumbleRecoveries * 10.0 + (double) playerStats.Interceptions * 10.0 + (double) playerStats.Sacks * 8.0 + (double) playerStats.ForcedFumbles * 4.0 + (double) playerStats.KnockDowns * 2.0 + (double) playerStats.Tackles * 2.0 + (double) playerStats.DefensiveTDs * 25.0 + (double) playerStats.TacklesForLoss * 5.0);

  public static int[] GetStatsForCategory(StatCategory category)
  {
    switch (category)
    {
      case StatCategory.Passing:
        return new int[9]{ 0, 1, 2, 3, 4, 5, 6, 7, 8 };
      case StatCategory.Rushing:
        return new int[6]{ 9, 10, 11, 12, 13, 14 };
      case StatCategory.Receiving:
        return new int[8]{ 15, 16, 17, 18, 19, 20, 21, 22 };
      case StatCategory.Defense:
        return new int[8]{ 23, 24, 25, 26, 27, 28, 29, 30 };
      case StatCategory.Kicking:
        return new int[8]{ 31, 32, 33, 34, 35, 36, 37, 38 };
      case StatCategory.Returns:
        return new int[8]{ 39, 40, 41, 42, 43, 44, 45, 46 };
      default:
        Debug.Log((object) "Unknown StatCategory specified for PlayerStats.GetStatsForCategory");
        return (int[]) null;
    }
  }

  public static string GetStatAbbreviation(int statIndex)
  {
    switch (statIndex)
    {
      case 0:
        return "RAT";
      case 1:
        return "COM";
      case 2:
        return "ATT";
      case 3:
        return "PER";
      case 4:
        return "YDS";
      case 5:
        return "TDS";
      case 6:
        return "INT";
      case 7:
        return "AVG";
      case 8:
        return "LNG";
      case 9:
        return "ATT";
      case 10:
        return "YDS";
      case 11:
        return "TDS";
      case 12:
        return "AVG";
      case 13:
        return "LNG";
      case 14:
        return "FUM";
      case 15:
        return "REC";
      case 16:
        return "YDS";
      case 17:
        return "TDS";
      case 18:
        return "AVG";
      case 19:
        return "LNG";
      case 20:
        return "YAC";
      case 21:
        return "DRP";
      case 22:
        return "TRGT";
      case 23:
        return "TKL";
      case 24:
        return "SAC";
      case 25:
        return "INT";
      case 26:
        return "TFL";
      case 27:
        return "TDS";
      case 28:
        return "DP";
      case 29:
        return "FF";
      case 30:
        return "REC";
      case 31:
        return "FGM";
      case 32:
        return "FGA";
      case 33:
        return "XPM";
      case 34:
        return "XPA";
      case 35:
        return "PNTS";
      case 36:
        return "P20";
      case 37:
        return "TBK";
      case 38:
        return "PAVG";
      case 39:
        return "PRET";
      case 40:
        return "YDS";
      case 41:
        return "AVG";
      case 42:
        return "TDS";
      case 43:
        return "KRET";
      case 44:
        return "YDS";
      case 45:
        return "AVG";
      case 46:
        return "TDS";
      default:
        Debug.Log((object) ("Unknown stat index: " + statIndex.ToString()));
        return "";
    }
  }

  public static bool HasStatsInCategoryOrIsStarter(
    TeamData teamData,
    int playerIndex,
    StatCategory category,
    StatDuration statDuration)
  {
    if (teamData.GetPlayer(playerIndex) == null)
      return false;
    PlayerStats playerStats = statDuration != StatDuration.CurrentGame ? teamData.GetPlayer(playerIndex).CurrentSeasonStats : teamData.GetPlayer(playerIndex).CurrentGameStats;
    if (playerStats == null)
      return false;
    switch (category)
    {
      case StatCategory.Passing:
        return teamData.TeamDepthChart.GetStartingQBIndex() == playerIndex || playerStats.QBAttempts > 0;
      case StatCategory.Rushing:
        return teamData.TeamDepthChart.GetStartingRBIndex() == playerIndex || playerStats.RushAttempts > 0;
      case StatCategory.Receiving:
        return teamData.TeamDepthChart.GetStartingWRXIndex() == playerIndex || teamData.TeamDepthChart.GetStartingWRYIndex() == playerIndex || teamData.TeamDepthChart.GetStartingWRZIndex() == playerIndex || playerStats.Receptions > 0 || playerStats.Drops > 0 || playerStats.Targets > 0;
      case StatCategory.Kicking:
        return teamData.TeamDepthChart.GetStartingKickerIndex() == playerIndex || teamData.TeamDepthChart.GetStartingPunterIndex() == playerIndex || playerStats.FGMade > 0 || playerStats.FGAttempted > 0 || playerStats.XPMade > 0 || playerStats.XPAttempted > 0 || playerStats.Punts > 0 || playerStats.PuntsInside20 > 0 || playerStats.PuntTouchbacks > 0 || playerStats.PuntYards > 0;
      case StatCategory.Returns:
        return teamData.TeamDepthChart.GetStartingKickReturnerIndex() == playerIndex || teamData.TeamDepthChart.GetStartingPuntReturnerIndex() == playerIndex || playerStats.KickReturns > 0 || playerStats.PuntReturns > 0;
      default:
        if (teamData.TeamDepthChart.NumberOfDLUsed == 3)
        {
          if (teamData.TeamDepthChart.GetStartingLDEIndex_34() == playerIndex || teamData.TeamDepthChart.GetStartingNTIndex() == playerIndex || teamData.TeamDepthChart.GetStartingRDEIndex_34() == playerIndex)
            return true;
        }
        else if (teamData.TeamDepthChart.GetStartingLDEIndex_43() == playerIndex || teamData.TeamDepthChart.GetStartingLDTIndex() == playerIndex || teamData.TeamDepthChart.GetStartingRDTIndex() == playerIndex || teamData.TeamDepthChart.GetStartingRDEIndex_43() == playerIndex)
          return true;
        if (teamData.TeamDepthChart.NumberOfLBUsed == 3)
        {
          if (teamData.TeamDepthChart.GetStartingWLBIndex() == playerIndex || teamData.TeamDepthChart.GetStartingMLBIndex() == playerIndex || teamData.TeamDepthChart.GetStartingSLBIndex() == playerIndex)
            return true;
        }
        else if (teamData.TeamDepthChart.GetStartingLOLBIndex() == playerIndex || teamData.TeamDepthChart.GetStartingLILBIndex() == playerIndex || teamData.TeamDepthChart.GetStartingRILBIndex() == playerIndex || teamData.TeamDepthChart.GetStartingROLBIndex() == playerIndex)
          return true;
        return teamData.TeamDepthChart.GetStartingLCBIndex() == playerIndex || teamData.TeamDepthChart.GetStartingSSIndex() == playerIndex || teamData.TeamDepthChart.GetStartingFSIndex() == playerIndex || teamData.TeamDepthChart.GetStartingRCBIndex() == playerIndex || playerStats.Tackles > 0 || playerStats.Interceptions > 0 || playerStats.KnockDowns > 0 || playerStats.ForcedFumbles > 0 || playerStats.FumbleRecoveries > 0;
    }
  }

  public void AddToStats(PlayerStats stats)
  {
    this.QBCompletions += stats.QBCompletions;
    this.QBAttempts += stats.QBAttempts;
    this.QBPassYards += stats.QBPassYards;
    this.QBPassTDs += stats.QBPassTDs;
    this.QBInts += stats.QBInts;
    if (stats.QBLongestPass > this.QBLongestPass)
      this.QBLongestPass = stats.QBLongestPass;
    this.RushAttempts += stats.RushAttempts;
    this.RushYards += stats.RushYards;
    this.RushTDs += stats.RushTDs;
    if (stats.LongestRush > this.LongestRush)
      this.LongestRush = stats.LongestRush;
    this.Fumbles += stats.Fumbles;
    this.Receptions += stats.Receptions;
    this.ReceivingYards += stats.ReceivingYards;
    this.ReceivingTDs += stats.ReceivingTDs;
    if (stats.LongestReception > this.LongestReception)
      this.LongestReception = stats.LongestReception;
    this.YardsAfterCatch += stats.YardsAfterCatch;
    this.Drops += stats.Drops;
    this.Targets += stats.Targets;
    this.Tackles += stats.Tackles;
    this.Sacks += stats.Sacks;
    this.Interceptions += stats.Interceptions;
    this.KnockDowns += stats.KnockDowns;
    this.ForcedFumbles += stats.ForcedFumbles;
    this.DefensiveTDs += stats.DefensiveTDs;
    this.TacklesForLoss += stats.TacklesForLoss;
    this.FumbleRecoveries += stats.FumbleRecoveries;
    this.Penalties += stats.Penalties;
    this.PenaltyYards += stats.PenaltyYards;
    this.FGMade += stats.FGMade;
    this.FGAttempted += stats.FGAttempted;
    this.XPMade += stats.XPMade;
    this.XPAttempted += stats.XPAttempted;
    this.Punts += stats.Punts;
    this.PuntsInside20 += stats.PuntsInside20;
    this.PuntTouchbacks += stats.PuntTouchbacks;
    this.PuntYards += stats.PuntYards;
    this.PuntReturns += stats.PuntReturns;
    this.PuntReturnYards += stats.PuntReturnYards;
    this.PuntReturnTDs += stats.PuntReturnTDs;
    this.KickReturns += stats.KickReturns;
    this.KickReturnYards += stats.KickReturnYards;
    this.KickReturnTDs += stats.KickReturnTDs;
    this.QBTenYardCompletions += stats.QBTenYardCompletions;
    this.QBTwentyYardCompletions += stats.QBTwentyYardCompletions;
    this.QBThirtyYardCompletions += stats.QBThirtyYardCompletions;
    this.QBFiftyYardCompletions += stats.QBFiftyYardCompletions;
    this.QBSacked += stats.QBSacked;
  }

  public int GetQBRating() => Mathf.RoundToInt(PlayerStats.CalculatePasserRating(this.QBAttempts, this.QBCompletions, this.QBPassTDs, this.QBPassYards, this.QBInts));

  public static float CalculatePasserRating(
    int attempts,
    int completions,
    int touchdowns,
    int yards,
    int interceptions)
  {
    float passerRating = 0.0f;
    if (attempts > 0)
    {
      double num1 = (double) Mathf.Clamp((float) (((double) completions / (double) attempts - 0.30000001192092896) * 5.0), 0.0f, 2.375f);
      float num2 = Mathf.Clamp((float) ((double) yards / (double) attempts - 3.0) * 0.25f, 0.0f, 2.375f);
      float num3 = Mathf.Clamp((float) touchdowns / (float) attempts * 20f, 0.0f, 2.375f);
      float num4 = Mathf.Clamp((float) (2.375 - (double) interceptions / (double) attempts * 25.0), 0.0f, 2.375f);
      double num5 = (double) num2;
      passerRating = (float) ((num1 + num5 + (double) num3 + (double) num4) / 6.0) * 100f;
    }
    return passerRating;
  }

  public string GetStatByIndex(int statIndex)
  {
    switch (statIndex)
    {
      case 0:
        return this.GetQBRating().ToString();
      case 1:
        return this.QBCompletions.ToString();
      case 2:
        return this.QBAttempts.ToString();
      case 3:
        return this.QBAttempts != 0 ? Mathf.RoundToInt((float) ((double) this.QBCompletions / (double) this.QBAttempts * 100.0)).ToString() + "%" : "0%";
      case 4:
        return this.QBPassYards.ToString("n0");
      case 5:
        return this.QBPassTDs.ToString();
      case 6:
        return this.QBInts.ToString();
      case 7:
        return this.QBCompletions != 0 ? string.Format("{0:0.#}", (object) (float) ((double) this.QBPassYards / (double) this.QBCompletions)) : "0";
      case 8:
        return this.QBLongestPass.ToString();
      case 9:
        return this.RushAttempts.ToString();
      case 10:
        return this.RushYards.ToString("n0");
      case 11:
        return this.RushTDs.ToString();
      case 12:
        return this.RushAttempts != 0 ? string.Format("{0:0.#}", (object) (float) ((double) this.RushYards / (double) this.RushAttempts)) : "0";
      case 13:
        return this.LongestRush.ToString();
      case 14:
        return this.Fumbles.ToString();
      case 15:
        return this.Receptions.ToString();
      case 16:
        return this.ReceivingYards.ToString("n0");
      case 17:
        return this.ReceivingTDs.ToString();
      case 18:
        return this.Receptions != 0 ? string.Format("{0:0.#}", (object) (float) ((double) this.ReceivingYards / (double) this.Receptions)) : "0";
      case 19:
        return this.LongestReception.ToString();
      case 20:
        return this.YardsAfterCatch.ToString();
      case 21:
        return this.Drops.ToString();
      case 22:
        return this.Targets.ToString();
      case 23:
        return this.Tackles.ToString();
      case 24:
        return this.Sacks.ToString();
      case 25:
        return this.Interceptions.ToString();
      case 26:
        return this.TacklesForLoss.ToString();
      case 27:
        return this.DefensiveTDs.ToString();
      case 28:
        return this.KnockDowns.ToString();
      case 29:
        return this.ForcedFumbles.ToString();
      case 30:
        return this.FumbleRecoveries.ToString();
      case 31:
        return this.FGMade.ToString();
      case 32:
        return this.FGAttempted.ToString();
      case 33:
        return this.XPMade.ToString();
      case 34:
        return this.XPAttempted.ToString();
      case 35:
        return this.Punts.ToString();
      case 36:
        return this.PuntsInside20.ToString();
      case 37:
        return this.PuntTouchbacks.ToString();
      case 38:
        return this.Punts != 0 ? string.Format("{0:0.#}", (object) (float) ((double) this.PuntYards / (double) this.Punts)) : "0.0";
      case 39:
        return this.PuntReturns.ToString();
      case 40:
        return this.PuntReturnYards.ToString();
      case 41:
        return this.PuntReturns != 0 ? string.Format("{0:0.#}", (object) (float) ((double) this.PuntReturnYards / (double) this.PuntReturns)) : "0";
      case 42:
        return this.PuntReturnTDs.ToString();
      case 43:
        return this.KickReturns.ToString();
      case 44:
        return this.KickReturnYards.ToString();
      case 45:
        return this.KickReturns != 0 ? string.Format("{0:0.#}", (object) (float) ((double) this.KickReturnYards / (double) this.KickReturns)) : "0";
      case 46:
        return this.KickReturnTDs.ToString();
      default:
        Debug.Log((object) ("Unknown stat index: " + statIndex.ToString()));
        return "";
    }
  }
}
