// Decompiled with JetBrains decompiler
// Type: FootballVR.CollisionEffectSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class CollisionEffectSettings
  {
    public float hitEffectDuration = 2f;
    public float fadeOutDuration = 0.04f;
    public float fadeDelay = -1f;
    public float fadeInDuration = 1.1f;
    public const float VibrationDuration = 0.5f;
  }
}
