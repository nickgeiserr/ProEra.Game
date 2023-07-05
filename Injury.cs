// Decompiled with JetBrains decompiler
// Type: Injury
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;

[MessagePackObject(false)]
[Serializable]
public class Injury
{
  [Key(0)]
  public InjuryType injuryType;
  [Key(1)]
  public string recoveryTimeframe;
  [Key(2)]
  public int weeksToRecover;
  [Key(3)]
  public int playsToRecover;
  [Key(4)]
  public string injuryCategory;
  [Key(5)]
  public int weeksRemaining;
  [Key(6)]
  public int playsRemaining;
  [Key(7)]
  public StartingPosition startingPosition;

  public Injury()
  {
  }

  public Injury(
    InjuryType _injuryType,
    string _injuryCategory,
    StartingPosition _startingPosition,
    string _recoveryTimeframe,
    int _weeksToRecover,
    int _playsToRecover)
  {
    this.injuryType = _injuryType;
    this.injuryCategory = _injuryCategory;
    this.startingPosition = _startingPosition;
    this.recoveryTimeframe = _recoveryTimeframe;
    this.weeksToRecover = _weeksToRecover;
    this.playsToRecover = _playsToRecover;
    this.weeksRemaining = this.weeksToRecover;
    this.playsRemaining = this.playsToRecover;
  }
}
