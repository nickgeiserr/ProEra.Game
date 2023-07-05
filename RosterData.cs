// Decompiled with JetBrains decompiler
// Type: RosterData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class RosterData
{
  [Key(0)]
  public PlayerData[] roster;
  [Key(1)]
  public int numberOfPlayersOnRoster;
  [Key(2)]
  public RosterType rosterType;
  [Key(3)]
  public Dictionary<string, int> defaultPlayers;

  public RosterData()
  {
  }

  public RosterData(int playersOnRoster)
  {
    this.numberOfPlayersOnRoster = playersOnRoster;
    this.roster = new PlayerData[this.numberOfPlayersOnRoster];
    this.defaultPlayers = (Dictionary<string, int>) null;
  }

  public RosterData(PlayerData[] players, Dictionary<string, int> defaultPlayers, RosterType type = RosterType.MainTeamRoster)
  {
    this.roster = players;
    this.numberOfPlayersOnRoster = this.roster.Length;
    this.rosterType = type;
    this.defaultPlayers = defaultPlayers;
  }

  public int GetNumberOfPlayers() => this.numberOfPlayersOnRoster;

  public List<int> GetNumbersOfPlayers()
  {
    List<int> numbersOfPlayers = new List<int>();
    foreach (PlayerData playerData in this.roster)
      numbersOfPlayers.Add(playerData.Number);
    return numbersOfPlayers;
  }

  public List<string> GetNamesOfPlayers()
  {
    List<string> namesOfPlayers = new List<string>();
    foreach (PlayerData playerData in this.roster)
      namesOfPlayers.Add(playerData.LastName);
    return namesOfPlayers;
  }

  public PlayerData GetPlayer(int playerIndex) => this.roster[playerIndex];

  public void SetPlayer(int playerIndex, PlayerData player)
  {
    this.roster[playerIndex] = player;
    player.IndexOnTeam = playerIndex;
  }

  public void RemovePlayerFromRoster(int playerIndex) => this.roster[playerIndex] = (PlayerData) null;

  public string GetFirstLetterAndLastName(int playerIndex) => this.GetPlayer(playerIndex).FirstName.Substring(0, 1) + ". " + this.GetPlayer(playerIndex).LastName;

  public int GetOverall(int playerIndex, Position position) => this.roster[playerIndex].GetOverall(position);

  public int GetOverall(int playerIndex) => this.roster[playerIndex].GetOverall();

  public int FindLeastNeededPlayerOnTeam(int ignorePlayersYoungerThan = 0, int[] ignoreTheseIndexes = null) => this.FindWorstPlayerAtPosition(this.FindLeastNeededBackupPosition(), ignorePlayersYoungerThan, ignoreTheseIndexes);

  public Position FindLeastNeededBackupPosition()
  {
    Position[] positionArray = new Position[10]
    {
      Position.QB,
      Position.RB,
      Position.WR,
      Position.TE,
      Position.OL,
      Position.DL,
      Position.LB,
      Position.DB,
      Position.K,
      Position.P
    };
    Position neededBackupPosition = Position.QB;
    int num1 = 0;
    for (int index = 0; index < positionArray.Length; ++index)
    {
      Position position = positionArray[index];
      int num2 = this.FindNumberOfPlayersAtPosition(position) - RosterData.GetTargetNumberOfPlayersForPosition(position);
      if (num2 > num1)
      {
        num1 = num2;
        neededBackupPosition = position;
      }
    }
    return neededBackupPosition;
  }

  public Position FindMostNeededBackupPosition()
  {
    Position[] positionArray = new Position[10]
    {
      Position.QB,
      Position.RB,
      Position.WR,
      Position.TE,
      Position.OL,
      Position.DL,
      Position.LB,
      Position.DB,
      Position.K,
      Position.P
    };
    Position neededBackupPosition = Position.QB;
    int num1 = 100;
    for (int index = 0; index < positionArray.Length; ++index)
    {
      Position position = positionArray[index];
      int num2 = this.FindNumberOfPlayersAtPosition(position) - RosterData.GetTargetNumberOfPlayersForPosition(position);
      if (num2 < num1)
      {
        num1 = num2;
        neededBackupPosition = position;
      }
    }
    return neededBackupPosition;
  }

  public static int GetNumberOfStartersForPosition(Position position)
  {
    switch (position)
    {
      case Position.QB:
        return 1;
      case Position.RB:
        return 2;
      case Position.WR:
        return 5;
      case Position.TE:
        return 2;
      case Position.OL:
        return 5;
      case Position.K:
        return 1;
      case Position.P:
        return 1;
      case Position.DL:
        return 4;
      case Position.LB:
        return 4;
      case Position.DB:
        return 6;
      default:
        Debug.Log((object) ("Unknown Starting Position Specified: " + position.ToString()));
        return 0;
    }
  }

  public static int GetTargetNumberOfPlayersForPosition(Position position)
  {
    switch (position)
    {
      case Position.QB:
        return 3;
      case Position.RB:
        return 4;
      case Position.WR:
        return 7;
      case Position.TE:
        return 3;
      case Position.OL:
        return 7;
      case Position.K:
        return 2;
      case Position.P:
        return 2;
      case Position.DL:
        return 7;
      case Position.LB:
        return 7;
      case Position.DB:
        return 7;
      default:
        Debug.Log((object) ("Unknown Starting Position Specified: " + position.ToString()));
        return 0;
    }
  }

  public int FindNumberOfOpenRosterSpots()
  {
    int ofOpenRosterSpots = 0;
    for (int index = 0; index < this.numberOfPlayersOnRoster; ++index)
    {
      if (this.roster[index] == null)
        ++ofOpenRosterSpots;
    }
    return ofOpenRosterSpots;
  }

  public int FindNumberOfFilledRosterSpots()
  {
    int filledRosterSpots = 0;
    for (int index = 0; index < this.numberOfPlayersOnRoster; ++index)
    {
      if (this.roster[index] != null)
        ++filledRosterSpots;
    }
    return filledRosterSpots;
  }

  public int FindNextOpenRosterIndex()
  {
    for (int nextOpenRosterIndex = 0; nextOpenRosterIndex < this.numberOfPlayersOnRoster; ++nextOpenRosterIndex)
    {
      if (this.roster[nextOpenRosterIndex] == null)
        return nextOpenRosterIndex;
    }
    return -1;
  }

  public int FindNumberOfUnsignedPlayers()
  {
    int ofUnsignedPlayers = 0;
    for (int index = 0; index < this.numberOfPlayersOnRoster; ++index)
    {
      if (this.roster[index] != null && this.roster[index].YearsRemainingOnContract == 0)
        ++ofUnsignedPlayers;
    }
    return ofUnsignedPlayers;
  }

  public List<PlayerData> FindUnsignedPlayers()
  {
    List<PlayerData> unsignedPlayers = new List<PlayerData>();
    for (int index = 0; index < this.numberOfPlayersOnRoster; ++index)
    {
      if (this.roster[index] != null && this.roster[index].YearsRemainingOnContract == 0)
        unsignedPlayers.Add(this.roster[index]);
    }
    return unsignedPlayers;
  }

  public Position FindAnyMissingPositionsOnRoster()
  {
    Position neededBackupPosition = this.FindMostNeededBackupPosition();
    return this.FindNumberOfPlayersAtPosition(neededBackupPosition) - RosterData.GetNumberOfStartersForPosition(neededBackupPosition) < 0 ? neededBackupPosition : Position.None;
  }

  public int FindNumberOfPlayersAtPosition(Position p)
  {
    int playersAtPosition = 0;
    for (int index = 0; index < this.numberOfPlayersOnRoster; ++index)
    {
      if (this.roster[index] != null && this.roster[index].PlayerPosition == p)
        ++playersAtPosition;
    }
    return playersAtPosition;
  }

  public int FindWorstPlayerAtPosition(
    Position p,
    int excludePlayersBelowAge = 0,
    int[] excludeTheseIndexes = null)
  {
    int num = 100;
    int playerAtPosition = 0;
    for (int playerIndex = 0; playerIndex < this.numberOfPlayersOnRoster; ++playerIndex)
    {
      bool flag = false;
      if (excludeTheseIndexes != null)
      {
        for (int index = 0; index < excludeTheseIndexes.Length; ++index)
        {
          if (playerIndex == excludeTheseIndexes[index])
          {
            flag = true;
            break;
          }
        }
      }
      if (!flag && this.roster[playerIndex] != null && this.roster[playerIndex].PlayerPosition == p && this.roster[playerIndex].Age >= excludePlayersBelowAge && this.GetOverall(playerIndex) < num)
      {
        num = this.GetOverall(playerIndex);
        playerAtPosition = playerIndex;
      }
    }
    return playerAtPosition;
  }

  public int FindWorstPlayer(int excludePlayersBelowAge = 0, int[] excludeTheseIndexes = null)
  {
    int num = 100;
    int worstPlayer = 0;
    for (int playerIndex = 0; playerIndex < this.numberOfPlayersOnRoster; ++playerIndex)
    {
      bool flag = false;
      if (excludeTheseIndexes != null)
      {
        for (int index = 0; index < excludeTheseIndexes.Length; ++index)
        {
          if (playerIndex == excludeTheseIndexes[index])
          {
            flag = true;
            break;
          }
        }
      }
      if (!flag && this.roster[playerIndex] != null && this.roster[playerIndex].Age >= excludePlayersBelowAge && this.GetOverall(playerIndex) < num)
      {
        num = this.GetOverall(playerIndex);
        worstPlayer = playerIndex;
      }
    }
    return worstPlayer;
  }

  public int FindBestPlayerAtPosition(Position p, int[] excludeTheseIndexes = null)
  {
    int num1 = 0;
    int playerAtPosition = -1;
    for (int playerIndex = 0; playerIndex < this.numberOfPlayersOnRoster; ++playerIndex)
    {
      bool flag = false;
      if (excludeTheseIndexes != null)
      {
        for (int index = 0; index < excludeTheseIndexes.Length; ++index)
        {
          if (playerIndex == excludeTheseIndexes[index])
          {
            flag = true;
            break;
          }
        }
      }
      if (this.rosterType == RosterType.Draft && this.roster[playerIndex] != null && this.roster[playerIndex].RoundDrafted > 0)
        flag = true;
      if (!flag && this.roster[playerIndex] != null && this.roster[playerIndex].PlayerPosition == p)
      {
        int num2 = this.rosterType != RosterType.Draft ? this.GetOverall(playerIndex) : this.GetPlayer(playerIndex).EstimatedOverall;
        if (num2 > num1)
        {
          num1 = num2;
          playerAtPosition = playerIndex;
        }
      }
    }
    if (playerAtPosition == -1)
    {
      for (int playerIndex = 0; playerIndex < this.numberOfPlayersOnRoster; ++playerIndex)
      {
        bool flag = false;
        if (excludeTheseIndexes != null)
        {
          for (int index = 0; index < excludeTheseIndexes.Length; ++index)
          {
            if (playerIndex == excludeTheseIndexes[index])
            {
              flag = true;
              break;
            }
          }
        }
        if (!flag && this.roster[playerIndex] != null && this.GetOverall(playerIndex, p) > num1)
        {
          num1 = this.GetOverall(playerIndex);
          playerAtPosition = playerIndex;
        }
      }
    }
    return playerAtPosition;
  }

  public int FindBestPlayer(int[] excludeTheseIndexes = null)
  {
    int num1 = 0;
    int bestPlayer = -1;
    for (int playerIndex = 0; playerIndex < this.numberOfPlayersOnRoster; ++playerIndex)
    {
      bool flag = false;
      if (excludeTheseIndexes != null)
      {
        for (int index = 0; index < excludeTheseIndexes.Length; ++index)
        {
          if (playerIndex == excludeTheseIndexes[index])
          {
            flag = true;
            break;
          }
        }
      }
      if (this.rosterType == RosterType.Draft && this.roster[playerIndex] != null && this.roster[playerIndex].RoundDrafted > 0)
        flag = true;
      if (this.GetPlayer(playerIndex) == null)
        flag = true;
      if (!flag)
      {
        int num2 = this.rosterType != RosterType.Draft ? this.GetOverall(playerIndex) : this.GetPlayer(playerIndex).EstimatedOverall;
        if (num2 > num1)
        {
          num1 = num2;
          bestPlayer = playerIndex;
        }
      }
    }
    return bestPlayer;
  }

  public void SetDraftClassData()
  {
  }
}
