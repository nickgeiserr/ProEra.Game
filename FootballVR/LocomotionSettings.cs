// Decompiled with JetBrains decompiler
// Type: FootballVR.LocomotionSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Locomotion/Settings")]
  [SettingsConfig]
  public class LocomotionSettings : SingletonScriptableSettings<LocomotionSettings>
  {
    public VariableControllerKey LocomotionKey = new VariableControllerKey(EControllerKey.ButtonOne_ax);
    public bool RequireBothButtonsDown;
    public ArmSwingSettings ArmSwingSettings = new ArmSwingSettings();
    public HeadTiltSettings LeanDetectionSettings = new HeadTiltSettings();
    public ForwardSettings ForwardSettings = new ForwardSettings();
    public ThumbstickLocomotionSettings ThumbstickLocomotionSettings = new ThumbstickLocomotionSettings();
    public VariableBool ShowDebug = new VariableBool(false);
    public bool allowMoveBackwards;
    public bool maintainRunOnRelease;
    public float maintainRunDecelerationCoef = 0.5f;

    protected override void OnEnable()
    {
      base.OnEnable();
      this.ArmSwingSettings.DecelerationType.OnValueChanged += new Action<EAccelerationProfile>(this.HandleDecelerationTypeChanged);
      this.HandleDecelerationTypeChanged((EAccelerationProfile) (Variable<EAccelerationProfile>) this.ArmSwingSettings.DecelerationType);
    }

    private void OnDisable() => this.ArmSwingSettings.DecelerationType.OnValueChanged -= new Action<EAccelerationProfile>(this.HandleDecelerationTypeChanged);

    private void HandleDecelerationTypeChanged(EAccelerationProfile type) => this.ArmSwingSettings.accelProfile = this.GetAccelerationProfile(type);

    private AccelerationSettings GetAccelerationProfile(EAccelerationProfile profile)
    {
      switch (profile)
      {
        case EAccelerationProfile.V2:
          return this.ArmSwingSettings.accelerationV2;
        case EAccelerationProfile.V3:
          return this.ArmSwingSettings.accelerationV3;
        default:
          return this.ArmSwingSettings.accelerationV1;
      }
    }
  }
}
