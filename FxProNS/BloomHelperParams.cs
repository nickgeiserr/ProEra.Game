// Decompiled with JetBrains decompiler
// Type: FxProNS.BloomHelperParams
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FxProNS
{
  [Serializable]
  public class BloomHelperParams
  {
    public EffectsQuality Quality;
    public Color BloomTint = Color.white;
    [Range(0.0f, 0.99f)]
    public float BloomThreshold = 0.8f;
    [Range(0.0f, 3f)]
    public float BloomIntensity = 1.5f;
    [Range(0.01f, 3f)]
    public float BloomSoftness = 0.5f;
  }
}
