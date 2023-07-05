// Decompiled with JetBrains decompiler
// Type: FootballVR.AvatarPlaybackSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class AvatarPlaybackSettings
  {
    public float CatchUpThreshold = 0.08f;
    public float SoftCatchUpThreshold = 0.8f;
    public float SoftCatchupSpeed = 6f;
    public float CatchupLerpFactor = 0.25f;
    public float CatchUpMaxSpeed = 40f;
  }
}
