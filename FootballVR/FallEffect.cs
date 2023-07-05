// Decompiled with JetBrains decompiler
// Type: FootballVR.FallEffect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class FallEffect : CollisionEffectSettings
  {
    public float Angle = 90f;
    public float Duration = 0.05f;
    public float Speed = 22f;
    public float Magnitude = 2.5f;
    public float DistanceForce = 300f;
    public float RotationDamper = 3f;
    public bool shakeEnabled = true;
    public bool fallEnabled = true;
    public float LerpDownFactor;
  }
}
