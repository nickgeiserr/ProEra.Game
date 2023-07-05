// Decompiled with JetBrains decompiler
// Type: PrecisionPassingRouteRunnerData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

[Serializable]
public class PrecisionPassingRouteRunnerData
{
  public int RouteSegmentIndexToSpawnRing;
  public float PercentToSpawnRingWithinRouteSegment;
  public float YOffset;
  public float XOffSet;
  public float ZOffset;
  public float Scale = 1f;
  public int MedalIndex;

  public string PrintDebug() => string.Format("RouteSegmentIndex: {0} | PercentToSpawnRingWithinRouteSegment: {1} | Offset: ({2}, {3}, {4}) | Scale {5}", (object) this.RouteSegmentIndexToSpawnRing, (object) this.PercentToSpawnRingWithinRouteSegment, (object) this.XOffSet, (object) this.YOffset, (object) this.ZOffset, (object) this.Scale);
}
