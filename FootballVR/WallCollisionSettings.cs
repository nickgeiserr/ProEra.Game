// Decompiled with JetBrains decompiler
// Type: FootballVR.WallCollisionSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class WallCollisionSettings
  {
    public float KnockSpeedThreshold = 0.2f;
    public float FallSpeedThreshold = 1f;
    public float Pushback = 1f;
    public float FallPushback = 1f;
    public float movementRaycastDistanceMultiplier = 1f;
    public float MinOffset = 0.2f;
  }
}
