// Decompiled with JetBrains decompiler
// Type: Award
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;

[MessagePackObject(false)]
[Serializable]
public class Award
{
  [Key(0)]
  public int teamIndex;
  [Key(1)]
  public string teamAbbreviation;
  [Key(2)]
  public string playerName;
  [Key(3)]
  public string playerFullName;
  [Key(4)]
  public int playerIndex;
  [Key(5)]
  public int playerPortrait;
  [Key(6)]
  public int skinValue;
  [Key(7)]
  public int playerNumber;
  [Key(8)]
  public AwardType awardType;
  [Key(9)]
  public Position position;
  [Key(10)]
  public string[] statHighlights;
  [Key(11)]
  public float statScore;

  public static Award NewAward(AwardType _awardType) => new Award()
  {
    statScore = 0.0f,
    awardType = _awardType
  };

  public void SetAwardData(
    TeamData team,
    PlayerStats playerStats,
    int _playerIndex,
    float _statScore,
    AwardType _awardType)
  {
    this.teamIndex = team.TeamIndex;
    this.playerIndex = _playerIndex;
    this.statScore = _statScore;
    this.awardType = _awardType;
    PlayerData player = team.GetPlayer(_playerIndex);
    this.playerName = player.FirstInitalAndLastName;
    this.playerFullName = player.FullName;
    this.playerPortrait = player.PortraitID;
    this.playerNumber = player.Number;
    this.skinValue = player.SkinColor;
    this.position = player.PlayerPosition;
    this.teamAbbreviation = team.GetAbbreviation();
    this.SetStatHighlightText(playerStats, this.position);
  }

  public void CopyAwardData(Award a, AwardType newAwardType)
  {
    this.teamIndex = a.teamIndex;
    this.teamAbbreviation = a.teamAbbreviation;
    this.playerIndex = a.playerIndex;
    this.statScore = a.statScore;
    this.playerName = a.playerName;
    this.playerFullName = a.playerFullName;
    this.playerPortrait = a.playerPortrait;
    this.skinValue = a.skinValue;
    this.position = a.position;
    this.playerNumber = a.playerNumber;
    this.statHighlights = a.statHighlights;
    this.awardType = newAwardType;
  }

  private void SetStatHighlightText(PlayerStats stats, Position position) => this.statHighlights = PlayerStats.GetStatHighlights(stats, position).ToArray();

  public bool IsOffensivePlayer() => this.position == Position.QB || this.position == Position.RB || this.position == Position.WR || this.position == Position.TE || this.position == Position.OL;
}
