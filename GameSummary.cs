// Decompiled with JetBrains decompiler
// Type: GameSummary
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;

[MessagePackObject(false)]
[Serializable]
public class GameSummary
{
  [Key(0)]
  public int TeamIndex;
  [Key(1)]
  public TeamGameStats TeamGameStats;
  [Key(2)]
  public PlayerData_Basic[] PlayerData;
  [Key(3)]
  public global::PlayerStats[] PlayerStats;

  public void RecordPlayerStats(TeamData team)
  {
    this.PlayerStats = new global::PlayerStats[team.GetNumberOfPlayersOnRoster()];
    this.PlayerData = new PlayerData_Basic[team.GetNumberOfPlayersOnRoster()];
    for (int playerIndex = 0; playerIndex < this.PlayerStats.Length; ++playerIndex)
    {
      if (team.GetPlayer(playerIndex) != null)
      {
        this.PlayerStats[playerIndex] = team.GetPlayer(playerIndex).CurrentGameStats;
        this.PlayerData[playerIndex] = team.GetPlayer(playerIndex).CreatePlayerData_Basic();
      }
    }
  }
}
