// Decompiled with JetBrains decompiler
// Type: FootballVR.HandPhysicsSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [Serializable]
  public class HandPhysicsSettings
  {
    public VariableBool handPhysics = new VariableBool(true);
    public VariableBool renderColliders = new VariableBool(false);
    public VariableBool continuousDetection = new VariableBool(true);
    public int framesDelayCollisionReenabled = 3;
    public EMoveType MovementMethod = EMoveType.RigidbodyMove;
    public ETargetInterpolation TargetInterpolation;
    public InterpolationMethod InterpolationMethod = new InterpolationMethod(RigidbodyInterpolation.Interpolate);
    public float lerpFactor = 1f;
  }
}
