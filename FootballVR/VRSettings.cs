// Decompiled with JetBrains decompiler
// Type: FootballVR.VRSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Settings/VRSettings", fileName = "VRSettings")]
  [SettingsConfig]
  public class VRSettings : SingletonScriptableSettings<VRSettings>
  {
    [NonSerialized]
    public Save_VRSettings saveVRSettings = new Save_VRSettings();
    public VariableBool LoadedGameStartupScene = new VariableBool();
    public VariableBool BypassStartup = new VariableBool(false);
    public VariableBool ControllerAnnotations = new VariableBool(true);
    public VariableFloat PlayerBodyScale = new VariableFloat(0.93f);
    public VariableFloat UIDistance = new VariableFloat(0.8f);
    public VariableBool UpdateUIHeight = new VariableBool(true);
    public VariableBool UseLeftHand = new VariableBool(false);
    public VariableBool UseVrLaser = new VariableBool(false);
    public VariableBool GripButtonThrow = new VariableBool(false);
    public VariableBool SeatedMode = new VariableBool(false);
    public VariableBool HelmetActive = new VariableBool(true);
    public VariableBool OneHandedMode = new VariableBool(false);
    public VariableInt HuddlePlayClock = new VariableInt(8);
    public VariableInt NoHuddlePlayClockOffset = new VariableInt(8);
    public VariableInt QuarterLength = new VariableInt(5);
    public VariableFloat PositionalAudio = new VariableFloat(0.6f);
    public VariableBool AlphaThrowing = new VariableBool(false);
    public float AudioSpeakThreshold = 0.01f;
    public bool AllowDynamicTargets = true;
    public VariableFloat VCVolume = new VariableFloat(1f);
    public VariableFloat MicVolume = new VariableFloat(1f);
    public VariableBool ImmersiveTackleEnabled = new VariableBool(false);
    public VariableBool AutoDropbackEnabled = new VariableBool(true);
    public CameraEffectsSettings cameraEffects;
    public TeleportSettings teleportSettings;

    [field: EditorSetting(ESettingType.Utility)]
    public static bool nonVrEditor { get; }

    [field: EditorSetting(ESettingType.FeatureToggle)]
    public static bool skipLoadingScreen { get; }

    [field: EditorSetting(ESettingType.FeatureToggle)]
    public static bool skipWipeTransition { get; }

    [field: EditorSetting(ESettingType.Utility)]
    public static bool forceHideHelmet { get; }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.BypassStartup.SetValue(false);
      this.LoadedGameStartupScene.SetValue(VRSettings.CheckIfStartupSceneIsLoaded());
    }

    public static bool CheckIfStartupSceneIsLoaded()
    {
      for (int index = 0; index < SceneManager.sceneCount; ++index)
      {
        Scene sceneAt = SceneManager.GetSceneAt(index);
        if (sceneAt.name == "GameStartup" && sceneAt.isLoaded)
          return true;
      }
      return false;
    }

    public void PrepareToLoad()
    {
      this.saveVRSettings = new Save_VRSettings();
      this.saveVRSettings.OnLoaded += new UnityAction(this.SettingsLoaded);
    }

    private void SettingsLoaded()
    {
      this.LoadedGameStartupScene.SetValue(this.saveVRSettings.LoadedGameStartupScene);
      this.BypassStartup.SetValue(this.saveVRSettings.BypassStartup);
      this.ControllerAnnotations.SetValue(this.saveVRSettings.ControllerAnnotations);
      this.PlayerBodyScale.SetValue(this.saveVRSettings.PlayerBodyScale);
      this.UIDistance.SetValue(this.saveVRSettings.UIDistance);
      this.UpdateUIHeight.SetValue(this.saveVRSettings.UpdateUIHeight);
      this.UseLeftHand.SetValue(this.saveVRSettings.UseLeftHand);
      this.UseVrLaser.SetValue(this.saveVRSettings.UseVrLaser);
      this.GripButtonThrow.SetValue(this.saveVRSettings.GripButtonThrow);
      this.SeatedMode.SetValue(this.saveVRSettings.SeatedMode);
      this.HelmetActive.SetValue(this.saveVRSettings.HelmetActive);
      this.OneHandedMode.SetValue(this.saveVRSettings.OneHandedMode);
      this.HuddlePlayClock.SetValue(this.saveVRSettings.HuddlePlayClock);
      this.NoHuddlePlayClockOffset.SetValue(this.saveVRSettings.NoHuddlePlayClockOffset);
      this.QuarterLength.SetValue(this.saveVRSettings.QuarterLength);
      this.PositionalAudio.SetValue(this.saveVRSettings.PositionalAudio);
      this.AlphaThrowing.SetValue(this.saveVRSettings.AlphaThrowing);
      this.AudioSpeakThreshold = this.saveVRSettings.AudioSpeakThreshold;
      this.AllowDynamicTargets = this.saveVRSettings.AllowDynamicTargets;
      this.VCVolume.SetValue(this.saveVRSettings.VCVolume);
      this.MicVolume.SetValue(this.saveVRSettings.MicVolume);
      this.ImmersiveTackleEnabled.SetValue(this.saveVRSettings.ImmersiveTackleEnabled);
      this.AutoDropbackEnabled.SetValue(this.saveVRSettings.AutoDropbackEnabled);
      this.cameraEffects = new CameraEffectsSettings();
      this.cameraEffects.VignetteEnabled.SetValue(this.saveVRSettings.VignetteEnabled);
      this.cameraEffects.VignetteFalloffDegrees.SetValue(this.saveVRSettings.VignetteFalloffDegrees);
      this.cameraEffects.VignetteAspectRatio.SetValue(this.saveVRSettings.VignetteAspectRatio);
      this.cameraEffects.VignetteLerpFactor = this.saveVRSettings.VignetteLerpFactor;
      this.cameraEffects.VignetteMinFov = this.saveVRSettings.VignetteMinFov;
      this.cameraEffects.VignetteMaxFov = this.saveVRSettings.VignetteMaxFov;
      this.teleportSettings = new TeleportSettings();
      this.teleportSettings.gravity = this.saveVRSettings.gravity;
      this.teleportSettings.timeFactor = this.saveVRSettings.timeFactor;
      this.teleportSettings.useSpline = this.saveVRSettings.useSpline;
      this.teleportSettings.splineCoef = this.saveVRSettings.splineCoef;
      this.teleportSettings.splineHeightCoef = this.saveVRSettings.splineHeightCoef;
      this.teleportSettings.laserPower = this.saveVRSettings.laserPower;
      this.saveVRSettings.OnLoaded -= new UnityAction(this.SettingsLoaded);
    }

    public void PrepareToSave()
    {
      this.saveVRSettings.LoadedGameStartupScene = this.LoadedGameStartupScene.Value;
      this.saveVRSettings.BypassStartup = this.BypassStartup.Value;
      this.saveVRSettings.ControllerAnnotations = this.ControllerAnnotations.Value;
      this.saveVRSettings.PlayerBodyScale = this.PlayerBodyScale.Value;
      this.saveVRSettings.UIDistance = this.UIDistance.Value;
      this.saveVRSettings.UpdateUIHeight = this.UpdateUIHeight.Value;
      this.saveVRSettings.UseLeftHand = this.UseLeftHand.Value;
      this.saveVRSettings.UseVrLaser = this.UseVrLaser.Value;
      this.saveVRSettings.GripButtonThrow = this.GripButtonThrow.Value;
      this.saveVRSettings.SeatedMode = this.SeatedMode.Value;
      this.saveVRSettings.HelmetActive = this.HelmetActive.Value;
      this.saveVRSettings.OneHandedMode = this.OneHandedMode.Value;
      this.saveVRSettings.HuddlePlayClock = this.HuddlePlayClock.Value;
      this.saveVRSettings.NoHuddlePlayClockOffset = this.NoHuddlePlayClockOffset.Value;
      this.saveVRSettings.QuarterLength = this.QuarterLength.Value;
      this.saveVRSettings.PositionalAudio = this.PositionalAudio.Value;
      this.saveVRSettings.AlphaThrowing = this.AlphaThrowing.Value;
      this.saveVRSettings.AudioSpeakThreshold = this.AudioSpeakThreshold;
      this.saveVRSettings.AllowDynamicTargets = this.AllowDynamicTargets;
      this.saveVRSettings.VCVolume = this.VCVolume.Value;
      this.saveVRSettings.MicVolume = this.MicVolume.Value;
      this.saveVRSettings.ImmersiveTackleEnabled = this.ImmersiveTackleEnabled.Value;
      this.saveVRSettings.AutoDropbackEnabled = this.AutoDropbackEnabled.Value;
      this.saveVRSettings.VignetteEnabled = this.cameraEffects.VignetteEnabled.Value;
      this.saveVRSettings.VignetteFalloffDegrees = this.cameraEffects.VignetteFalloffDegrees.Value;
      this.saveVRSettings.VignetteAspectRatio = this.cameraEffects.VignetteAspectRatio.Value;
      this.saveVRSettings.VignetteLerpFactor = this.cameraEffects.VignetteLerpFactor;
      this.saveVRSettings.VignetteMinFov = this.cameraEffects.VignetteMinFov;
      this.saveVRSettings.VignetteMaxFov = this.cameraEffects.VignetteMaxFov;
      this.saveVRSettings.gravity = this.teleportSettings.gravity;
      this.saveVRSettings.timeFactor = this.teleportSettings.timeFactor;
      this.saveVRSettings.useSpline = this.teleportSettings.useSpline;
      this.saveVRSettings.splineCoef = this.teleportSettings.splineCoef;
      this.saveVRSettings.splineHeightCoef = this.teleportSettings.splineHeightCoef;
      this.saveVRSettings.laserPower = this.teleportSettings.laserPower;
    }
  }
}
