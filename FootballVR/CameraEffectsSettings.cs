// Decompiled with JetBrains decompiler
// Type: FootballVR.CameraEffectsSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using Vars;

namespace FootballVR
{
  [Serializable]
  public class CameraEffectsSettings
  {
    public VariableBool VignetteEnabled = new VariableBool(true);
    public VariableFloat VignetteFalloffDegrees = new VariableFloat(20f);
    public VariableFloat VignetteAspectRatio = new VariableFloat(1f);
    public float VignetteLerpFactor = 0.2f;
    public float VignetteMinFov = 55f;
    public float VignetteMaxFov = 105f;
  }
}
