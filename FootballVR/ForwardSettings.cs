// Decompiled with JetBrains decompiler
// Type: FootballVR.ForwardSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vars;

namespace FootballVR
{
  [Serializable]
  public class ForwardSettings
  {
    public VariableDirection ForwardDirection = new VariableDirection(EForwardDirection.Head);
    public VariableFloat ForwardLerpCoef = new VariableFloat(0.03f);
    public VariableBool DebugForward = new VariableBool(false);
    public VariableInt FramesTracked = new VariableInt(24);
    public float swingLerpFactor = 0.3f;
    [SerializeField]
    private float _minSwingStrength = 0.1f;
    public bool ignoreRecoverySwipes;
    public bool useCamForw = true;
    [FormerlySerializedAs("swingDirLerpFactor")]
    public float swingSumLerpFactor = 0.3f;
    public float lerpWithSwingDirFactor;
    public float lerpWithGazeFactor;

    public float minStep => this._minSwingStrength / 100f;
  }
}
