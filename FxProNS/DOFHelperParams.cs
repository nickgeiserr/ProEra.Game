// Decompiled with JetBrains decompiler
// Type: FxProNS.DOFHelperParams
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FxProNS
{
  [Serializable]
  public class DOFHelperParams
  {
    public bool UseUnityDepthBuffer = true;
    public bool AutoFocus = true;
    public LayerMask AutoFocusLayerMask = (LayerMask) -1;
    [Range(2f, 8f)]
    public float AutoFocusSpeed = 5f;
    [Range(0.01f, 1f)]
    public float FocalLengthMultiplier = 0.33f;
    public float FocalDistMultiplier = 1f;
    [Range(0.5f, 2f)]
    public float DOFBlurSize = 1f;
    public bool BokehEnabled;
    [Range(2f, 8f)]
    public float DepthCompression = 4f;
    public Camera EffectCamera;
    public Transform Target;
    [Range(0.0f, 1f)]
    public float BokehThreshold = 0.5f;
    [Range(0.5f, 5f)]
    public float BokehGain = 2f;
    [Range(0.0f, 1f)]
    public float BokehBias = 0.5f;
    public bool DoubleIntensityBlur;
  }
}
