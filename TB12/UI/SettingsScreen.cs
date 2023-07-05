// Decompiled with JetBrains decompiler
// Type: TB12.UI.SettingsScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vars;

namespace TB12.UI
{
  public class SettingsScreen : UIView
  {
    [SerializeField]
    private TextMeshProUGUI _versionText;
    [SerializeField]
    private TextMeshProUGUI _activeHandText;
    [SerializeField]
    private TextMeshProUGUI _laserActiveText;
    [SerializeField]
    private TextMeshProUGUI _throwButtonText;
    [SerializeField]
    private TextMeshProUGUI _noHuddleButtonText;
    [SerializeField]
    private TextMeshProUGUI _seatedButtonText;
    [SerializeField]
    private TextMeshProUGUI _quarterText;
    [SerializeField]
    private TextMeshProUGUI _helmetText;
    [SerializeField]
    private TouchButton _closeButton;
    [SerializeField]
    private TouchButton _activeHandButton;
    [SerializeField]
    private TouchButton _laserActiveButton;
    [SerializeField]
    private TouchButton _resetButton;
    [SerializeField]
    private TouchButton _newSeasonButton;
    [SerializeField]
    private TouchButton _throwButton;
    [SerializeField]
    private TouchButton _noHuddleButton;
    [SerializeField]
    private TouchButton _seatedButton;
    [SerializeField]
    private TouchButton _quarterButton;
    [SerializeField]
    private TouchButton _helmetButton;
    [SerializeField]
    private Slider _sfxSlider;
    [SerializeField]
    private Slider _bgmSlider;
    [SerializeField]
    private Slider _hostVoSlider;
    [SerializeField]
    private Slider _timeScaleSlider;

    public override Enum ViewId { get; } = (Enum) EScreens.kSettings;

    protected override void OnInitialize()
    {
      VRSettings instance = ScriptableSingleton<VRSettings>.Instance;
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) UIHandle.Link(this._sfxSlider, ScriptableSingleton<SettingsStore>.Instance.SfxVolume),
        (EventHandle) UIHandle.Link(this._bgmSlider, ScriptableSingleton<SettingsStore>.Instance.BgmVolume),
        (EventHandle) UIHandle.Link(this._hostVoSlider, ScriptableSingleton<SettingsStore>.Instance.HostVoVolume),
        (EventHandle) UIHandle.Link(this._timeScaleSlider, GameSettings.TimeScale),
        (EventHandle) UIHandle.Link((IButton) this._resetButton, new System.Action(this.ResetHandler)),
        (EventHandle) UIHandle.Link((IButton) this._newSeasonButton, new System.Action(this.NewSeasonHandler)),
        (EventHandle) UIHandle.Link((IButton) this._closeButton, (System.Action) (() => UIDispatch.FrontScreen.CloseScreen())),
        (EventHandle) UIHandle.Link((IButton) this._activeHandButton, new System.Action(instance.UseLeftHand.Toggle)),
        EventHandle.Link<bool>((Variable<bool>) instance.UseLeftHand, new Action<bool>(this.ActiveHandChangedHandler)),
        (EventHandle) UIHandle.Link((IButton) this._laserActiveButton, new System.Action(instance.UseVrLaser.Toggle)),
        EventHandle.Link<bool>((Variable<bool>) instance.UseVrLaser, new Action<bool>(this.LaserEnableHandler)),
        (EventHandle) UIHandle.Link((IButton) this._throwButton, new System.Action(instance.GripButtonThrow.Toggle)),
        EventHandle.Link<bool>((Variable<bool>) instance.GripButtonThrow, new Action<bool>(this.ThrowButtonChangedHandler)),
        (EventHandle) UIHandle.Link((IButton) this._noHuddleButton, new System.Action(this.UpdateNoHuddleValue)),
        EventHandle.Link<int>((Variable<int>) instance.NoHuddlePlayClockOffset, new Action<int>(this.NoHuddleButtonChangedHandler)),
        (EventHandle) UIHandle.Link((IButton) this._quarterButton, new System.Action(this.UpdateQuarterLength)),
        EventHandle.Link<int>((Variable<int>) instance.QuarterLength, new Action<int>(this.QuarterLengthButtonChangedHandler)),
        (EventHandle) UIHandle.Link((IButton) this._seatedButton, new System.Action(instance.SeatedMode.Toggle)),
        EventHandle.Link<bool>((Variable<bool>) instance.SeatedMode, new Action<bool>(this.SeatedButtonChangedHandler)),
        (EventHandle) UIHandle.Link((IButton) this._helmetButton, new System.Action(instance.HelmetActive.Toggle)),
        EventHandle.Link<bool>((Variable<bool>) instance.HelmetActive, new Action<bool>(this.HelmetButtonChangedHandler))
      });
      this._sfxSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateSFXVolume));
      this._bgmSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateBGMVolume));
      this._hostVoSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateVOVolume));
      this._timeScaleSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateTimeScale));
    }

    protected override void WillAppear() => this._versionText.text = "VERSION: " + Application.version;

    protected override void WillDisappear()
    {
    }

    private void UpdateBGMVolume(float value) => AppSounds.UpdateBGMVolume.Trigger(value);

    private void UpdateVOVolume(float value) => AppSounds.UpdateVOVolume.Trigger(value);

    private void UpdateSFXVolume(float value) => AppSounds.UpdateSFXVolume.Trigger(value);

    private void UpdateTimeScale(float value) => GameSettings.UpdateTimescaleHandler(value);

    private void ActiveHandChangedHandler(bool leftHand)
    {
      this._activeHandText.text = leftHand ? "LEFT HAND" : "RIGHT HAND";
      ScriptableSingleton<HandsDataModel>.Instance.UpdateActiveHand();
    }

    private void LaserEnableHandler(bool enable) => this._laserActiveText.text = enable ? "LASER: ON" : "LASER: OFF";

    private void ResetHandler() => ScriptableSingleton<SettingsStore>.Instance.ResetStore();

    private void NewSeasonHandler() => SeasonModeManager.self.UICreateNewSeason();

    private void ThrowButtonChangedHandler(bool gripButton) => this._throwButtonText.text = gripButton ? "GRIP" : "TRIGGER";

    private void SeatedButtonChangedHandler(bool seated) => this._seatedButtonText.text = seated ? "SEATED" : "STANDING";

    private void UpdateNoHuddleValue()
    {
      VRSettings instance = ScriptableSingleton<VRSettings>.Instance;
      ++instance.NoHuddlePlayClockOffset.Value;
      if (instance.NoHuddlePlayClockOffset.Value <= 20)
        return;
      instance.NoHuddlePlayClockOffset.Value = 0;
    }

    private void NoHuddleButtonChangedHandler(int value) => this._noHuddleButtonText.text = "No-Huddle\nClock Run Off: " + value.ToString();

    private void UpdateQuarterLength()
    {
      VRSettings instance = ScriptableSingleton<VRSettings>.Instance;
      ++instance.QuarterLength.Value;
      if (instance.QuarterLength.Value <= 15)
        return;
      instance.QuarterLength.Value = 1;
    }

    private void QuarterLengthButtonChangedHandler(int value) => this._quarterText.text = "QUARTER LENGTH: " + value.ToString();

    private void HelmetButtonChangedHandler(bool active) => this._helmetText.text = active ? "HELMET ON" : "HELMET OFF";
  }
}
