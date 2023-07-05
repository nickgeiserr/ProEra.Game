// Decompiled with JetBrains decompiler
// Type: GameResults
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

public class GameResults
{
  public GameSummary HomeGameSummary { get; set; }

  public GameSummary AwayGameSummary { get; set; }

  public Award OffensivePlayerOfTheGame { get; set; }

  public Award DefensivePlayerOfTheGame { get; set; }

  public int WinningTeam => this.HomeGameSummary.TeamGameStats.Score <= this.AwayGameSummary.TeamGameStats.Score ? this.AwayGameSummary.TeamIndex : this.HomeGameSummary.TeamIndex;
}
