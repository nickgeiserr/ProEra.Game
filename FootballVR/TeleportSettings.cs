// Decompiled with JetBrains decompiler
// Type: FootballVR.TeleportSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class TeleportSettings
  {
    public float gravity = 5f;
    public float timeFactor = 4f;
    public bool useSpline;
    public float splineCoef = 1.2f;
    public float splineHeightCoef = 0.3f;
    public float laserPower = 20f;
  }
}
