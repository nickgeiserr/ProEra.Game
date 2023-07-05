// Decompiled with JetBrains decompiler
// Type: FootballVR.ArmSwingSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FootballVR
{
  [Serializable]
  public class ArmSwingSettings
  {
    public float maxSpeed = 9f;
    public VariableAccelerationProfile DecelerationType = new VariableAccelerationProfile(EAccelerationProfile.V3);
    public AccelerationSettings accelerationV1;
    public AccelerationSettings accelerationV2;
    public AccelerationSettings accelerationV3;
    public bool multiplyWithHeadAccel;
    public float headLerpCoef = 0.5f;
    [SerializeField]
    private float _maxHeadAccel = 0.1f;
    public float forwardCoef = 0.9f;
    public float verticalCoef = 0.4f;
    public float backwardsVerticalCoef = 0.3f;
    public float rechargeCoefficient = 1.25f;
    [SerializeField]
    private float _minAccel = 0.1f;
    public float maxAcceleration = 0.4f;
    public float maxCharge = 0.06f;
    public float swingLerpCoef = 0.35f;
    public bool _requireLeanForward = true;
    public float minLeanAngle = 0.5f;

    public AccelerationSettings accelProfile { get; set; }

    public float maxHeadAccel => this._maxHeadAccel / 100f;

    public float minAccel => this._minAccel / 100f;

    public bool requireLeanForward => this._requireLeanForward;
  }
}
