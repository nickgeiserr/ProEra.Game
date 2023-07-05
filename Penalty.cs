// Decompiled with JetBrains decompiler
// Type: Penalty
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

public struct Penalty
{
  private string text;
  private string offenseOrDefense;
  private int yards;
  private PenaltyTime time;
  private PenaltyType type;
  private int teamIndex;
  private PenaltyDownResult penaltyResult;
  private PlayerData penalizedPlayer;

  public Penalty(
    string text,
    string offenseOrDefense,
    int yards,
    PenaltyTime time,
    PenaltyType type,
    PenaltyDownResult result,
    int teamIndex,
    PlayerData penalizedPlayer)
  {
    this.text = text;
    this.offenseOrDefense = offenseOrDefense;
    this.yards = yards;
    this.time = time;
    this.type = type;
    this.teamIndex = teamIndex;
    this.penaltyResult = result;
    this.penalizedPlayer = penalizedPlayer;
  }

  public PlayerData GetPenalizedPlayer() => this.penalizedPlayer;

  public string GetPenaltyText() => this.text;

  public int GetPlayerSkin() => this.penalizedPlayer.SkinColor;

  public string GetPlayerPosition() => this.penalizedPlayer.PlayerPosition.ToString();

  public string GetOffenseOrDefense() => this.offenseOrDefense;

  public int GetPlayerIndex() => this.penalizedPlayer.IndexOnTeam;

  public int GetPlayerPortrait() => this.penalizedPlayer.PortraitID;

  public int GetPlayerJerseyNumber() => this.penalizedPlayer.Number;

  public int GetTeamIndex() => this.teamIndex;

  public int GetPenaltyYards() => this.yards;

  public PenaltyType GetPenaltyType() => this.type;

  public PenaltyTime GetPenaltyTime() => this.time;

  public PenaltyDownResult GetPenaltyDownResult() => this.penaltyResult;

  public override string ToString() => "Penalty Details: " + this.text + " against the " + this.offenseOrDefense + " for " + this.yards.ToString() + " yards. " + this.penaltyResult.ToString();
}
