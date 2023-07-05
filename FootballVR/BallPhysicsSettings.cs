// Decompiled with JetBrains decompiler
// Type: FootballVR.BallPhysicsSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [Serializable]
  public class BallPhysicsSettings
  {
    public VariableFloat dynamicFriction = new VariableFloat(0.5f);
    public VariableFloat staticFriction = new VariableFloat(0.6f);
    public VariableFloat bounciness = new VariableFloat(0.75f);
    public InterpolationMethod InterpolationMethod = new InterpolationMethod(RigidbodyInterpolation.None);
  }
}
