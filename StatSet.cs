// Decompiled with JetBrains decompiler
// Type: StatSet
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using System.Collections.Generic;

[MessagePackObject(false)]
[Serializable]
public class StatSet
{
  [Key(0)]
  public int wins;
  [Key(1)]
  public int losses;
  [Key(2)]
  public int teamPlusMinus;
  [Key(3)]
  public int turnovers;
  [Key(4)]
  public int tacklesForLoss;
  [Key(5)]
  public int interceptions;
  [Key(6)]
  public int forcedFumbles;
  [Key(7)]
  public int fumbleRecoveries;
  [Key(8)]
  public int passYards;
  [Key(9)]
  public int rushYards;
  [Key(10)]
  public int totalYards;
  [Key(11)]
  public int passYardsAllowed;
  [Key(12)]
  public int rushYardsAllowed;
  [Key(13)]
  public int totalYardsAllowed;
  [Key(14)]
  public int sacks;
  [Key(15)]
  public int turnoverMargin;
  [Key(16)]
  public int pointsScored;
  [Key(17)]
  public int pointsAllowed;
  [Key(18)]
  public int confWins;
  [Key(19)]
  public int confLosses;
  [Key(20)]
  public int confChampionships;
  [Key(21)]
  public int divWins;
  [Key(22)]
  public int divLosses;
  [Key(23)]
  public int divChampionships;
  [Key(24)]
  public int touchdowns;
  [Key(25)]
  public int totalPassPlays;
  [Key(26)]
  public int droppedPasses;
  [Key(27)]
  public int rivalWins;
  [Key(28)]
  public int redZoneAppearances;
  [Key(29)]
  public int redZoneTouchdowns;
  [Key(30)]
  public int superBowlWins;
  [Key(31)]
  public int mvpAwards;
  [Key(32)]
  public int superBowlMvpAwards;
  [Key(33)]
  public int offesensivePowAwards;
  [Key(34)]
  public int defensivePowAwards;
  [Key(35)]
  public int playoffAppearances;
  [Key(36)]
  public int playoffWins;
  [Key(37)]
  public int playoffLosses;
  [Key(38)]
  public HashSet<int> teamsBeaten = new HashSet<int>();
  [Key(39)]
  public HashSet<int> superBowlWiningTeams = new HashSet<int>();
  [Key(40)]
  public int touchdownPasses;
  [Key(41)]
  public int passCompletions;
  [Key(42)]
  public int qbInts;

  [IgnoreMember]
  public int Games => this.wins + this.losses;

  [IgnoreMember]
  public int TotalPlayerOfTheWeekAwards => this.offesensivePowAwards + this.defensivePowAwards;

  public void Add(StatSet other)
  {
  }

  public static StatSet operator +(StatSet a, StatSet b)
  {
    a.wins += b.wins;
    a.losses += b.losses;
    a.teamPlusMinus += b.teamPlusMinus;
    a.turnovers += b.turnovers;
    a.tacklesForLoss += b.tacklesForLoss;
    a.interceptions += b.interceptions;
    a.forcedFumbles += b.forcedFumbles;
    a.fumbleRecoveries += b.fumbleRecoveries;
    a.passYards += b.passYards;
    a.rushYards += b.rushYards;
    a.totalYards += b.totalYards;
    a.passYardsAllowed += b.passYardsAllowed;
    a.rushYardsAllowed += b.rushYardsAllowed;
    a.totalYardsAllowed += b.totalYardsAllowed;
    a.sacks += b.sacks;
    a.turnoverMargin += b.turnoverMargin;
    a.pointsScored += b.pointsScored;
    a.pointsAllowed += b.pointsAllowed;
    a.confWins += b.confWins;
    a.confLosses += b.confLosses;
    a.confChampionships += b.confChampionships;
    a.divWins += b.divWins;
    a.divLosses += b.divLosses;
    a.divChampionships += b.divChampionships;
    a.touchdowns += b.touchdowns;
    a.totalPassPlays += b.totalPassPlays;
    a.droppedPasses += b.droppedPasses;
    a.rivalWins += b.rivalWins;
    a.redZoneAppearances += b.redZoneAppearances;
    a.redZoneTouchdowns += b.redZoneTouchdowns;
    a.superBowlWins += b.superBowlWins;
    a.mvpAwards += b.mvpAwards;
    a.superBowlMvpAwards += b.superBowlMvpAwards;
    a.offesensivePowAwards += b.offesensivePowAwards;
    a.defensivePowAwards += b.defensivePowAwards;
    a.playoffAppearances += b.playoffAppearances;
    a.playoffWins += b.playoffWins;
    a.playoffLosses += b.playoffLosses;
    a.touchdownPasses += b.touchdownPasses;
    a.passCompletions += b.passCompletions;
    a.qbInts += b.qbInts;
    a.teamsBeaten.UnionWith((IEnumerable<int>) b.teamsBeaten);
    a.superBowlWiningTeams.UnionWith((IEnumerable<int>) b.superBowlWiningTeams);
    return a;
  }
}
