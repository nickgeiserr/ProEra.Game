// Decompiled with JetBrains decompiler
// Type: FootballVR.HeadTiltSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [Serializable]
  public class HeadTiltSettings
  {
    public ETiltMethod TiltMethod = ETiltMethod.HeadToDefaultPos;
    [SerializeField]
    private float _heightFlexibility = 7f;
    public float DefaultPositionHeight = 1f;
    public float SampleTime = 0.33f;
    public float HeightLerpFactor = 0.5f;
    public float LerpDownFactor = 0.03f;
    public float LerpUpFactor = 0.8f;
    public VariableInt AverageFrameCount = new VariableInt(8);
    public float MaxBodyDistance = 0.1f;
    public bool ApplyMaxDistance;
    public bool BlockForwardOffset;
    public float BodyY = -0.5f;

    public float HeightFlexibility => this._heightFlexibility / 100f;
  }
}
