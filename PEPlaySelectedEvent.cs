// Decompiled with JetBrains decompiler
// Type: PEPlaySelectedEvent
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PEPlaySelectedEvent : PEGameplayEvent
{
  public int Quarter;
  public bool HomeOnOffense;
  public bool OffenseGoingNorth;
  public PlayDataOff OffenseData;
  public PlayDataDef DefenseData;
  public int Down;
  public int Distance;

  public PEPlaySelectedEvent(
    float time,
    Vector3 pos,
    int quarter,
    bool homeOff,
    bool offNorth,
    PlayDataOff offData,
    PlayDataDef defData,
    int down,
    int distance)
  {
    this.GameTime = time;
    this.BallPosition = pos;
    this.Quarter = quarter;
    this.HomeOnOffense = homeOff;
    this.OffenseGoingNorth = offNorth;
    this.OffenseData = offData;
    this.DefenseData = defData;
    this.Down = down;
    this.Distance = distance;
  }
}
