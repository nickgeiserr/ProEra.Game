// Decompiled with JetBrains decompiler
// Type: FootballVR.ThrowConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [Serializable]
  public class ThrowConfig
  {
    public int ThrowVersion = 6;
    public float MinThrowThreshold = 3f;
    public float MinMultiplier = 1f;
    public float MaxMultiplierHoriz = 5f;
    public float MaxMultiplierVert = 2.5f;
    public float MinMultiplierSpeed = 3f;
    public float MaxMultiplierSpeed = 10f;
    public float AngleCorrection = (float) new VariableFloat(-2f);
    public int FramesTracked = 7;
    public float WeightDecreaseFactor = 0.87f;
    public float PredictedVelocityFalloff = 0.9f;

    public Vector3 ApplyConfig(Vector3 throwVec)
    {
      float t = Mathf.InverseLerp(this.MinMultiplierSpeed, this.MaxMultiplierSpeed, throwVec.magnitude);
      float num1 = Mathf.Lerp(this.MinMultiplier, this.MaxMultiplierHoriz, t);
      float num2 = Mathf.Lerp(this.MinMultiplier, this.MaxMultiplierVert, t);
      throwVec = throwVec.SetY(0.0f) * num1 + num2 * throwVec.y * Vector3.up;
      return throwVec;
    }
  }
}
