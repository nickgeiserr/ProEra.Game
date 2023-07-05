// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UI.Screens.Shared.Settings.SettingsScreenV2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using ProEra.Game.Sources.SeasonMode.SeasonTablet;
using ProEra.Web;
using System;
using System.Collections.Generic;
using TB12;
using TB12.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;
using Vars;

namespace ProEra.Game.UI.Screens.Shared.Settings
{
  public class SettingsScreenV2 : UIView
  {
    [SerializeField]
    private ThrowSettings _throwSettingsStore;
    [SerializeField]
    private CanvasTabManagerConfig _canvasTabManagerConfig;
    [SerializeField]
    private CanvasTabManager _thisCanvasTabManager;
    [Space]
    [Header("General Settings")]
    [SerializeField]
    private TouchButton _difficultySelectionButton;
    [SerializeField]
    private LocalizeStringEvent _difficultyText;
    [SerializeField]
    private TouchButton _difficultyEasyButton;
    [SerializeField]
    private TouchButton _difficultyMediumButton;
    [SerializeField]
    private TouchButton _difficultyHardButton;
    [SerializeField]
    private TouchButton _newTeamButton;
    [SerializeField]
    private GameObject _newTeamConfirmPopup;
    [SerializeField]
    private TouchButton _newTeamConfirmButton;
    [SerializeField]
    private TouchButton _newTeamCancelButton;
    [SerializeField]
    private Image _controlsImage;
    [SerializeField]
    private LocalizeSpriteEvent _controlGraphics;
    [Space]
    [Header("Gameplay Settings")]
    [SerializeField]
    private SettingsValueButtonUI _quarterLengthSetting;
    [SerializeField]
    private SettingsValueButtonUI _huddleClockSetting;
    [SerializeField]
    private SettingsValueButtonUI _huddleRunOffSetting;
    [SerializeField]
    private SettingsValueButtonUI _playModeIsSeated;
    [SerializeField]
    private SettingsValueButtonUI _immersiveTackleEnabled;
    [SerializeField]
    private SettingsValueButtonUI _autoDropBackEnabled;
    [SerializeField]
    private SettingsValueButtonUI _autoHandoffEnabled;
    [Space]
    [Header("User Settings")]
    [SerializeField]
    private SettingsValueButtonUI _vrLaserPointerEnabled;
    [SerializeField]
    private SettingsValueButtonUI _dominantHandIsLeft;
    [SerializeField]
    private SettingsValueButtonUI _passAssistEnabled;
    [SerializeField]
    private SettingsValueButtonUI _helmetEffectEnabled;
    [SerializeField]
    private SettingsValueButtonUI _oneHandedMode;
    [SerializeField]
    private SettingsValueButtonUI _grabWithGrips;
    [SerializeField]
    private LocalizeStringEvent localizeGripText;
    [SerializeField]
    private LocalizeStringEvent localizeTriggerText;
    [Space]
    [Header("Sound Settings")]
    [SerializeField]
    private Slider _sfxSlider;
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private Slider _coachSlider;
    [SerializeField]
    private Slider _stadiumSlider;
    [SerializeField]
    private SettingsValueButtonUI _instrumentalEnabled;
    [Space]
    [Header("Account Settings")]
    [SerializeField]
    private SettingsValueButtonUI _accountLinkButton;
    [SerializeField]
    private GameObject _accountNoNetworkError;
    [SerializeField]
    private TextMeshProUGUI _activeUserText;
    [SerializeField]
    private string _inactiveUserText = "no account linked";
    [SerializeField]
    private Color _activeUserTextColor = Color.green;
    [SerializeField]
    private Color _inactiveUserTextColor = Color.red;
    private const string LocalizationDropdownRookie = "SettingsGeneral_Dropdown_Rookie";
    private const string LocalizationDropdownPro = "SettingsGeneral_Dropdown_Pro";
    private const string LocalizationDropdownAllPro = "SettingsGeneral_Dropdown_AllPro";
    private const string LocalizationButtonGrip = "SettingsUser_Button_UseGrip";
    private const string LocalizationButtonTrigger = "SettingsUser_Button_UseTrigger";
    private const string LocalizationButtonR1L1 = "SettingsGeneral_Button_R1L1";
    private const string LocalizationButtonR2L2 = "SettingsGeneral_Button_R2L2";
    private const string LocalizationControllers = "SettingsGeneral_Sprite_Controls";
    private Color _defaultTabTextColor = new Color(0.7843137f, 0.827451f, 0.8117647f, 1f);
    [Space]
    [SerializeField]
    private TouchButton _closeButton;
    private List<EventHandle> _eventHandlers;

    public override Enum ViewId => (Enum) EScreens.kSettings;

    protected override void OnInitialize()
    {
      PlayerApi.CreateUserSuccess += new Action<SaveKeycloakUserData>(this.HandleAccountLinkToggle);
      PlayerApi.DeleteUserSuccess += new System.Action(this.HandleAccountLinkToggle);
      this._throwSettingsStore = ScriptableSingleton<ThrowSettings>.Instance;
      this._eventHandlers = new List<EventHandle>();
      this.InitializePlatformGraphics();
      this._eventHandlers.AddRange(this.InitializeDifficultyDropdown());
      this._eventHandlers.AddRange(this.InitializeQuarterLengthSettings());
      this._eventHandlers.AddRange(this.InitializeHuddleTimeSettings());
      this._eventHandlers.AddRange(this.InitializeNoHuddleRunoffTimeSettings());
      this._eventHandlers.AddRange(this.InitializeAutoDropBackSettings());
      this._eventHandlers.AddRange(this.InitializeAutoHandoffSettings());
      this._eventHandlers.AddRange(this.InitializeVrLaserSettings());
      this._eventHandlers.AddRange(this.InitializeDominantHandSettings());
      this._eventHandlers.AddRange(this.InitializePassAssistSettings());
      this._eventHandlers.AddRange(this.InitializeSeatedPlaymodeSettings());
      this._eventHandlers.AddRange(this.InitializeHelmetEffectSettings());
      this._eventHandlers.AddRange(this.InitializeOneHandedModeSettings());
      this._eventHandlers.AddRange(this.InitializeImmersiveTackleSettings());
      this._eventHandlers.AddRange(this.InitializeGrabWithGripsSettings());
      this._eventHandlers.AddRange(this.InitializeSettingSlider(this._sfxSlider, ScriptableSingleton<SettingsStore>.Instance.SfxVolume, new UnityAction<float>(this.UpdateSfxVolume)));
      this._eventHandlers.AddRange(this.InitializeSettingSlider(this._musicSlider, ScriptableSingleton<SettingsStore>.Instance.BgmVolume, new UnityAction<float>(this.UpdateBGMVolume)));
      this._eventHandlers.AddRange(this.InitializeSettingSlider(this._coachSlider, ScriptableSingleton<SettingsStore>.Instance.HostVoVolume, new UnityAction<float>(this.UpdateVoVolume)));
      this._eventHandlers.AddRange(this.InitializeSettingSlider(this._stadiumSlider, ScriptableSingleton<SettingsStore>.Instance.StadiumVolume, new UnityAction<float>(this.UpdateStadiumVolume)));
      this._eventHandlers.AddRange(this.InitializeInstrumentalSettings());
      this._eventHandlers.AddRange(this.InitializeAccountLinkSettings());
      this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this._newTeamButton, new System.Action(this.ShowNewTeamConfirmPopup)));
      this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this._newTeamCancelButton, new System.Action(this.HideNewTeamConfirmPopup)));
      this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this._newTeamConfirmButton, new System.Action(this.NewSeasonHandler)));
      this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this._closeButton, new System.Action(this.CloseMenuHandler)));
      this.linksHandler.SetLinks(this._eventHandlers);
    }

    protected override void WillAppear()
    {
      int num = (bool) SaveManager.GetPlayerCustomization().IsNewCustomization ? 1 : 0;
      this._thisCanvasTabManager.SetTabsEnabled(true);
      if ((UnityEngine.Object) this._thisCanvasTabManager != (UnityEngine.Object) null)
        this._thisCanvasTabManager.SimulateTabPress(0, true);
      else
        Debug.LogError((object) "ERROR! _thisCanvasTab is null in SettingsScreenV2.WillAppear");
      VRState.LocomotionEnabled.SetValue(false);
      this.LoadAllSettings();
      this.UpdateSettingsElements();
    }

    protected override void WillDisappear()
    {
      this.SaveAllSettings();
      VRState.LocomotionEnabled.SetValue(true);
    }

    private void LoadAllSettings()
    {
      if (!PersistentSingleton<SaveManager>.Exist())
        return;
      ScriptableSingleton<SettingsStore>.Instance.PrepareToLoad();
      PersistentSingleton<SaveManager>.Instance.AddToLoadQueue((ISaveSync) ScriptableSingleton<SettingsStore>.Instance.saveSettingsStore);
      ScriptableSingleton<VRSettings>.Instance.PrepareToLoad();
      PersistentSingleton<SaveManager>.Instance.AddToLoadQueue((ISaveSync) ScriptableSingleton<VRSettings>.Instance.saveVRSettings);
      ScriptableSingleton<GameSettings>.Instance.PrepareToLoad();
      PersistentSingleton<SaveManager>.Instance.AddToLoadQueue((ISaveSync) ScriptableSingleton<GameSettings>.Instance.saveOldGameSettings);
    }

    private void SaveAllSettings()
    {
      if (!PersistentSingleton<SaveManager>.Exist())
        return;
      ScriptableSingleton<SettingsStore>.Instance.PrepareToSave();
      PersistentSingleton<SaveManager>.Instance.AddToSaveQueue((ISaveSync) ScriptableSingleton<SettingsStore>.Instance.saveSettingsStore);
      ScriptableSingleton<VRSettings>.Instance.PrepareToSave();
      PersistentSingleton<SaveManager>.Instance.AddToSaveQueue((ISaveSync) ScriptableSingleton<VRSettings>.Instance.saveVRSettings);
      ScriptableSingleton<GameSettings>.Instance.PrepareToSave();
      PersistentSingleton<SaveManager>.Instance.AddToSaveQueue((ISaveSync) ScriptableSingleton<GameSettings>.Instance.saveOldGameSettings);
    }

    private void OnDestroy() => this.linksHandler.Clear();

    private void InitializePlatformGraphics()
    {
      if (!(bool) (UnityEngine.Object) this._controlGraphics)
        return;
      this._controlGraphics.AssetReference.TableEntryReference = (TableEntryReference) ("SettingsGeneral_Sprite_Controls" + VRUtils.GetDeviceType().ToString());
    }

    private void ShowNewTeamConfirmPopup() => this._newTeamConfirmPopup.gameObject.SetActive(true);

    private void HideNewTeamConfirmPopup() => this._newTeamConfirmPopup.gameObject.SetActive(false);

    private void NewSeasonHandler() => SeasonModeManager.self.UICreateNewSeason();

    private void CloseMenuHandler()
    {
      if ((bool) SaveManager.GetPlayerCustomization().IsNewCustomization && !PersistentSingleton<PlayerApi>.Instance.IsLoggedIn)
        return;
      UIDispatch.FrontScreen.CloseScreen();
    }

    private IEnumerable<EventHandle> InitializeDifficultyDropdown()
    {
      int newLevel = (int) GameSettings.DifficultyLevel.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._difficultySelectionButton, new System.Action(this.ToggleShowDifficultySelection));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._difficultyEasyButton, new System.Action(this.SetDifficultyEasy));
      UIHandle uiHandle3 = UIHandle.Link((IButton) this._difficultyMediumButton, new System.Action(this.SetDifficultyMed));
      UIHandle uiHandle4 = UIHandle.Link((IButton) this._difficultyHardButton, new System.Action(this.SetDifficultyHard));
      EventHandle eventHandle = GameSettings.DifficultyLevel.Link<int>(new Action<int>(this.HandleDifficultyChange));
      this.HandleDifficultyChange(newLevel);
      return (IEnumerable<EventHandle>) new EventHandle[5]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        (EventHandle) uiHandle3,
        (EventHandle) uiHandle4,
        eventHandle
      };
    }

    private void SetDifficultyEasy()
    {
      GameSettings.UpdateDifficultyHandler(0);
      this.HandleDifficultyChange(0);
      this.HideDifficultySelection();
    }

    private void SetDifficultyMed()
    {
      GameSettings.UpdateDifficultyHandler(1);
      this.HandleDifficultyChange(1);
      this.HideDifficultySelection();
    }

    private void SetDifficultyHard()
    {
      GameSettings.UpdateDifficultyHandler(2);
      this.HandleDifficultyChange(2);
      this.HideDifficultySelection();
    }

    private void ToggleShowDifficultySelection()
    {
      bool flag = !this._difficultyEasyButton.gameObject.activeSelf;
      this._difficultyEasyButton.gameObject.SetActive(flag);
      this._difficultyMediumButton.gameObject.SetActive(flag);
      this._difficultyHardButton.gameObject.SetActive(flag);
    }

    private void HideDifficultySelection()
    {
      this._difficultyEasyButton.gameObject.SetActive(false);
      this._difficultyMediumButton.gameObject.SetActive(false);
      this._difficultyHardButton.gameObject.SetActive(false);
    }

    private void HandleDifficultyChange(int newLevel)
    {
      string gameDifficulty = newLevel > 1 ? "SettingsGeneral_Dropdown_AllPro" : (newLevel > 0 ? "SettingsGeneral_Dropdown_Pro" : "SettingsGeneral_Dropdown_Rookie");
      this._difficultyText.StringReference.TableEntryReference = (TableEntryReference) gameDifficulty;
      AnalyticEvents.Record<DifficultySelectedArgs>(new DifficultySelectedArgs(gameDifficulty));
    }

    private IEnumerable<EventHandle> InitializeQuarterLengthSettings()
    {
      int newValue = (int) ScriptableSingleton<VRSettings>.Instance.QuarterLength.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._quarterLengthSetting.IncrementValueButton, new System.Action(this.IncreaseQuarterLength));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._quarterLengthSetting.DecrementValueButton, new System.Action(this.DecreaseQuarterLength));
      EventHandle eventHandle = EventHandle.Link<int>((Variable<int>) ScriptableSingleton<VRSettings>.Instance.QuarterLength, new Action<int>(this.HandleQuarterLengthChange));
      this.HandleQuarterLengthChange(newValue);
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void IncreaseQuarterLength()
    {
      int num = (int) ScriptableSingleton<VRSettings>.Instance.QuarterLength.GetValue() + 1;
      if (num > 15)
        return;
      ScriptableSingleton<VRSettings>.Instance.QuarterLength.SetValue(num);
      AnalyticEvents.Record<QuarterLengthSelectedArgs>(new QuarterLengthSelectedArgs((float) (int) ScriptableSingleton<VRSettings>.Instance.QuarterLength.GetValue()));
    }

    private void DecreaseQuarterLength()
    {
      int num = (int) ScriptableSingleton<VRSettings>.Instance.QuarterLength.GetValue() - 1;
      if (num < 1)
        return;
      ScriptableSingleton<VRSettings>.Instance.QuarterLength.SetValue(num);
      AnalyticEvents.Record<QuarterLengthSelectedArgs>(new QuarterLengthSelectedArgs((float) (int) ScriptableSingleton<VRSettings>.Instance.QuarterLength.GetValue()));
    }

    private void HandleQuarterLengthChange(int newValue)
    {
      int num = (int) ScriptableSingleton<VRSettings>.Instance.QuarterLength.GetValue();
      this._quarterLengthSetting.IncrementValueButton.SetInteractible(true);
      this._quarterLengthSetting.DecrementValueButton.SetInteractible(true);
      this._quarterLengthSetting.ValueText[0].text = num.ToString();
    }

    private IEnumerable<EventHandle> InitializeHuddleTimeSettings()
    {
      int newValue = (int) ScriptableSingleton<VRSettings>.Instance.HuddlePlayClock.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._huddleClockSetting.IncrementValueButton, new System.Action(this.IncreaseHuddleTime));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._huddleClockSetting.DecrementValueButton, new System.Action(this.DecreaseHuddleTime));
      EventHandle eventHandle = EventHandle.Link<int>((Variable<int>) ScriptableSingleton<VRSettings>.Instance.HuddlePlayClock, new Action<int>(this.HandleHuddleTimeChange));
      this.HandleHuddleTimeChange(newValue);
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void IncreaseHuddleTime()
    {
      int num = (int) ScriptableSingleton<VRSettings>.Instance.HuddlePlayClock.GetValue() + 1;
      if (num > 15)
        return;
      ScriptableSingleton<VRSettings>.Instance.HuddlePlayClock.SetValue(num);
    }

    private void DecreaseHuddleTime()
    {
      int num = (int) ScriptableSingleton<VRSettings>.Instance.HuddlePlayClock.GetValue() - 1;
      if (num < 1)
        return;
      ScriptableSingleton<VRSettings>.Instance.HuddlePlayClock.SetValue(num);
    }

    private void HandleHuddleTimeChange(int newValue)
    {
      int num = (int) ScriptableSingleton<VRSettings>.Instance.HuddlePlayClock.GetValue();
      this._huddleClockSetting.IncrementValueButton.SetInteractible(true);
      this._huddleClockSetting.DecrementValueButton.SetInteractible(true);
      this._huddleClockSetting.ValueText[0].text = num.ToString();
    }

    private IEnumerable<EventHandle> InitializeNoHuddleRunoffTimeSettings()
    {
      int newValue = (int) ScriptableSingleton<VRSettings>.Instance.NoHuddlePlayClockOffset.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._huddleRunOffSetting.IncrementValueButton, new System.Action(this.IncreaseNoHuddleRunoffTime));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._huddleRunOffSetting.DecrementValueButton, new System.Action(this.DecreaseNoHuddleRunoffTime));
      EventHandle eventHandle = EventHandle.Link<int>((Variable<int>) ScriptableSingleton<VRSettings>.Instance.NoHuddlePlayClockOffset, new Action<int>(this.HandleNoHuddleRunoffTimeChange));
      this.HandleNoHuddleRunoffTimeChange(newValue);
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void IncreaseNoHuddleRunoffTime()
    {
      int num = (int) ScriptableSingleton<VRSettings>.Instance.NoHuddlePlayClockOffset.GetValue() + 1;
      if (num > 20)
        return;
      ScriptableSingleton<VRSettings>.Instance.NoHuddlePlayClockOffset.SetValue(num);
    }

    private void DecreaseNoHuddleRunoffTime()
    {
      int num = (int) ScriptableSingleton<VRSettings>.Instance.NoHuddlePlayClockOffset.GetValue() - 1;
      if (num < 0)
        return;
      ScriptableSingleton<VRSettings>.Instance.NoHuddlePlayClockOffset.SetValue(num);
    }

    private void HandleNoHuddleRunoffTimeChange(int newValue)
    {
      int num = (int) ScriptableSingleton<VRSettings>.Instance.NoHuddlePlayClockOffset.GetValue();
      this._huddleRunOffSetting.IncrementValueButton.SetInteractible(true);
      this._huddleRunOffSetting.IncrementValueButton.SetInteractible(true);
      this._huddleRunOffSetting.ValueText[0].text = num.ToString();
    }

    private IEnumerable<EventHandle> InitializeVrLaserSettings()
    {
      bool isEnabled = (bool) ScriptableSingleton<VRSettings>.Instance.UseVrLaser.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._vrLaserPointerEnabled.IncrementValueButton, new System.Action(this.EnableVrLaser));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._vrLaserPointerEnabled.DecrementValueButton, new System.Action(this.DisableVrLaser));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.UseVrLaser, new Action<bool>(this.HandleVrLaserEnableChange));
      this.HandleVrLaserEnableChange(isEnabled);
      if (isEnabled)
        this._vrLaserPointerEnabled.ValueText[0].color = Color.black;
      else
        this._vrLaserPointerEnabled.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void EnableVrLaser() => ScriptableSingleton<VRSettings>.Instance.UseVrLaser.SetValue(true);

    private void DisableVrLaser() => ScriptableSingleton<VRSettings>.Instance.UseVrLaser.SetValue(false);

    private void HandleVrLaserEnableChange(bool isEnabled)
    {
      this._vrLaserPointerEnabled.IncrementValueButton.SetInteractible(!isEnabled);
      this._vrLaserPointerEnabled.DecrementValueButton.SetInteractible(isEnabled);
    }

    private IEnumerable<EventHandle> InitializePassAssistSettings()
    {
      bool assistEnabled = !(bool) ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.GetValue();
      if ((UnityEngine.Object) this._passAssistEnabled != (UnityEngine.Object) null)
      {
        UIHandle uiHandle1 = UIHandle.Link((IButton) this._passAssistEnabled.IncrementValueButton, new System.Action(this.SetPassAssistTrue));
        UIHandle uiHandle2 = UIHandle.Link((IButton) this._passAssistEnabled.DecrementValueButton, new System.Action(this.SetPassAssistFalse));
        EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.AlphaThrowing, new Action<bool>(this.HandlePassaAssistChange));
        this.HandlePassaAssistChange(assistEnabled);
        if (assistEnabled)
        {
          this._passAssistEnabled.ValueText[0].color = Color.black;
          this._passAssistEnabled.ValueText[1].color = Color.white;
        }
        else
        {
          this._passAssistEnabled.ValueText[0].color = Color.white;
          this._passAssistEnabled.ValueText[1].color = Color.black;
        }
        return (IEnumerable<EventHandle>) new EventHandle[3]
        {
          (EventHandle) uiHandle1,
          (EventHandle) uiHandle2,
          eventHandle
        };
      }
      Debug.LogError((object) "ERROR! _passAsseistEnabled is null in SettingsScreenV2.InitializePassAssestSettings");
      return (IEnumerable<EventHandle>) null;
    }

    private void SetPassAssistTrue()
    {
      ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.SetValue(false);
      this.HandlePassaAssistChange(true);
    }

    private void SetPassAssistFalse()
    {
      ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.SetValue(true);
      this.HandlePassaAssistChange(false);
    }

    private void HandlePassaAssistChange(bool assistEnabled)
    {
      if ((UnityEngine.Object) this._passAssistEnabled != (UnityEngine.Object) null)
      {
        this._passAssistEnabled.IncrementValueButton.SetInteractible(!assistEnabled);
        this._passAssistEnabled.DecrementValueButton.SetInteractible(assistEnabled);
        if (assistEnabled)
        {
          this._passAssistEnabled.ValueText[0].color = Color.black;
          this._passAssistEnabled.ValueText[1].color = this._defaultTabTextColor;
        }
        else
        {
          this._passAssistEnabled.ValueText[0].color = this._defaultTabTextColor;
          this._passAssistEnabled.ValueText[1].color = Color.black;
        }
        AnalyticEvents.Record<PassAssistChangedArgs>(new PassAssistChangedArgs(assistEnabled));
      }
      else
        Debug.LogError((object) "ERROR! _passAsseistEnabled is null in SettingsScreenV2.HandlePassaAssistChange");
    }

    private IEnumerable<EventHandle> InitializeDominantHandSettings()
    {
      bool isLeftHand = (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._dominantHandIsLeft.IncrementValueButton, new System.Action(this.SetDominantHandLeft));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._dominantHandIsLeft.DecrementValueButton, new System.Action(this.SetDominantHandNotLeft));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.UseLeftHand, new Action<bool>(this.HandleDominantHandChange));
      this.HandleDominantHandChange(isLeftHand);
      if (isLeftHand)
        this._dominantHandIsLeft.ValueText[0].color = Color.black;
      else
        this._dominantHandIsLeft.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void SetDominantHandLeft() => ScriptableSingleton<VRSettings>.Instance.UseLeftHand.SetValue(true);

    private void SetDominantHandNotLeft() => ScriptableSingleton<VRSettings>.Instance.UseLeftHand.SetValue(false);

    private void HandleDominantHandChange(bool isLeftHand)
    {
      this._dominantHandIsLeft.IncrementValueButton.SetInteractible(!isLeftHand);
      this._dominantHandIsLeft.DecrementValueButton.SetInteractible(isLeftHand);
      ScriptableSingleton<HandsDataModel>.Instance.UpdateActiveHand();
    }

    private IEnumerable<EventHandle> InitializeSeatedPlaymodeSettings()
    {
      bool isSeated = (bool) ScriptableSingleton<VRSettings>.Instance.SeatedMode.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._playModeIsSeated.IncrementValueButton, new System.Action(this.SetPlayModeSeated));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._playModeIsSeated.DecrementValueButton, new System.Action(this.SetPlayModeStanding));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.SeatedMode, new Action<bool>(this.HandleSeatedPlaymodeChange));
      this.HandleSeatedPlaymodeChange(isSeated);
      if (isSeated)
        this._playModeIsSeated.ValueText[0].color = Color.black;
      else
        this._playModeIsSeated.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void SetPlayModeSeated() => ScriptableSingleton<VRSettings>.Instance.SeatedMode.SetValue(true);

    private void SetPlayModeStanding() => ScriptableSingleton<VRSettings>.Instance.SeatedMode.SetValue(false);

    private void HandleSeatedPlaymodeChange(bool isSeated)
    {
      this._playModeIsSeated.IncrementValueButton.SetInteractible(!isSeated);
      this._playModeIsSeated.DecrementValueButton.SetInteractible(isSeated);
      AnalyticEvents.Record<SeatedModeChangedArgs>(new SeatedModeChangedArgs(isSeated));
    }

    private IEnumerable<EventHandle> InitializeAccountLinkSettings()
    {
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._accountLinkButton.IncrementValueButton, new System.Action(this.LinkAccount));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._accountLinkButton.DecrementValueButton, new System.Action(this.UnlinkAccount));
      this.HandleAccountLinkToggle();
      return (IEnumerable<EventHandle>) new UIHandle[2]
      {
        uiHandle1,
        uiHandle2
      };
    }

    private void LinkAccount()
    {
      PersistentSingleton<PlayerApi>.Instance.CreateUser();
      this.HandleAccountLinkToggle();
    }

    private void UnlinkAccount()
    {
      PersistentSingleton<PlayerApi>.Instance.DeleteUser();
      this.HandleAccountLinkToggle();
    }

    private void HandleAccountLinkToggle(SaveKeycloakUserData _) => this.HandleAccountLinkToggle();

    private void HandleAccountLinkToggle() => this.HandleAccountLinkToggleWithNetCheck();

    private void HandleAccountLinkNetCheck(bool a_isConnected)
    {
      this._accountLinkButton.gameObject.SetActive(a_isConnected);
      this._accountNoNetworkError.SetActive(!a_isConnected);
      if (a_isConnected)
        PersistentSingleton<PlayerApi>.Instance.GetDisplayName((Action<string>) (username =>
        {
          if (string.IsNullOrEmpty(username))
          {
            this._activeUserText.text = this._inactiveUserText;
            this._activeUserText.color = this._inactiveUserTextColor;
          }
          else
          {
            this._activeUserText.text = username;
            this._activeUserText.color = this._activeUserTextColor;
          }
        }));
      MonoBehaviour.print((object) a_isConnected);
    }

    private void HandleAccountLinkToggleWithNetCheck()
    {
      PersistentSingleton<PlayerApi>.Instance.Ping(new Action<bool>(this.HandleAccountLinkNetCheck));
      bool flag = (UnityEngine.Object) PersistentSingleton<PlayerApi>.Instance != (UnityEngine.Object) null && PersistentSingleton<PlayerApi>.Instance.IsLoggedIn;
      this._accountLinkButton.IncrementValueButton.SetInteractible(!flag);
      this._accountLinkButton.DecrementValueButton.SetInteractible(flag);
      if (flag)
        this._accountLinkButton.ValueText[0].color = Color.black;
      else
        this._accountLinkButton.ValueText[1].color = Color.black;
    }

    private IEnumerable<EventHandle> InitializeHelmetEffectSettings()
    {
      bool isEnabled = (bool) ScriptableSingleton<VRSettings>.Instance.HelmetActive.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._helmetEffectEnabled.IncrementValueButton, new System.Action(this.EnableHelmetEffect));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._helmetEffectEnabled.DecrementValueButton, new System.Action(this.DisableHelmetEffect));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.HelmetActive, new Action<bool>(this.HandleHelmetEffectChange));
      this.HandleHelmetEffectChange(isEnabled);
      if (isEnabled)
        this._helmetEffectEnabled.ValueText[0].color = Color.black;
      else
        this._helmetEffectEnabled.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void EnableHelmetEffect() => ScriptableSingleton<VRSettings>.Instance.HelmetActive.SetValue(true);

    private void DisableHelmetEffect() => ScriptableSingleton<VRSettings>.Instance.HelmetActive.SetValue(false);

    private void HandleHelmetEffectChange(bool isEnabled)
    {
      this._helmetEffectEnabled.IncrementValueButton.SetInteractible(!isEnabled);
      this._helmetEffectEnabled.DecrementValueButton.SetInteractible(isEnabled);
    }

    private IEnumerable<EventHandle> InitializeOneHandedModeSettings()
    {
      bool isEnabled = (bool) ScriptableSingleton<VRSettings>.Instance.OneHandedMode.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._oneHandedMode.IncrementValueButton, new System.Action(this.EnableOneHandedMode));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._oneHandedMode.DecrementValueButton, new System.Action(this.DisableOneHandedMode));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.OneHandedMode, new Action<bool>(this.HandleOneHandedModeChange));
      this.HandleOneHandedModeChange(isEnabled);
      if (isEnabled)
        this._oneHandedMode.ValueText[0].color = Color.black;
      else
        this._oneHandedMode.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void EnableOneHandedMode() => ScriptableSingleton<VRSettings>.Instance.OneHandedMode.SetValue(true);

    private void DisableOneHandedMode() => ScriptableSingleton<VRSettings>.Instance.OneHandedMode.SetValue(false);

    private void HandleOneHandedModeChange(bool isEnabled)
    {
      this._oneHandedMode.IncrementValueButton.SetInteractible(!isEnabled);
      this._oneHandedMode.DecrementValueButton.SetInteractible(isEnabled);
    }

    private IEnumerable<EventHandle> InitializeGrabWithGripsSettings()
    {
      bool isEnabled = (bool) ScriptableSingleton<VRSettings>.Instance.GripButtonThrow.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._grabWithGrips.IncrementValueButton, new System.Action(this.EnableGrabWithGrips));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._grabWithGrips.DecrementValueButton, new System.Action(this.DisableGrabWithGrips));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.GripButtonThrow, new Action<bool>(this.HandleGrabWithGripsChange));
      this.HandleGrabWithGripsChange(isEnabled);
      if (isEnabled)
        this._grabWithGrips.ValueText[0].color = Color.black;
      else
        this._grabWithGrips.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void EnableGrabWithGrips() => ScriptableSingleton<VRSettings>.Instance.GripButtonThrow.SetValue(true);

    private void DisableGrabWithGrips() => ScriptableSingleton<VRSettings>.Instance.GripButtonThrow.SetValue(false);

    private void HandleGrabWithGripsChange(bool isEnabled)
    {
      this._grabWithGrips.IncrementValueButton.SetInteractible(!isEnabled);
      this._grabWithGrips.DecrementValueButton.SetInteractible(isEnabled);
      if (!((UnityEngine.Object) this.localizeGripText != (UnityEngine.Object) null) || !((UnityEngine.Object) this.localizeTriggerText != (UnityEngine.Object) null))
        return;
      this.localizeGripText.StringReference.TableEntryReference = (TableEntryReference) "SettingsUser_Button_UseGrip";
      this.localizeTriggerText.StringReference.TableEntryReference = (TableEntryReference) "SettingsUser_Button_UseTrigger";
    }

    private IEnumerable<EventHandle> InitializeImmersiveTackleSettings()
    {
      bool isEnabled = (bool) ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._immersiveTackleEnabled.IncrementValueButton, new System.Action(SettingsScreenV2.EnableImmersiveTackle));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._immersiveTackleEnabled.DecrementValueButton, new System.Action(SettingsScreenV2.DisableImmersiveTackle));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled, new Action<bool>(this.HandleImmersiveTackleChange));
      this.HandleImmersiveTackleChange(isEnabled);
      if (isEnabled)
        this._immersiveTackleEnabled.ValueText[0].color = Color.black;
      else
        this._immersiveTackleEnabled.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private static void EnableImmersiveTackle() => ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.SetValue(true);

    private static void DisableImmersiveTackle() => ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.SetValue(false);

    private void HandleImmersiveTackleChange(bool isEnabled)
    {
      this._immersiveTackleEnabled.IncrementValueButton.SetInteractible(!isEnabled);
      this._immersiveTackleEnabled.DecrementValueButton.SetInteractible(isEnabled);
    }

    private IEnumerable<EventHandle> InitializeAutoDropBackSettings()
    {
      bool isEnabled = (bool) ScriptableSingleton<VRSettings>.Instance.AutoDropbackEnabled.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._autoDropBackEnabled.IncrementValueButton, new System.Action(SettingsScreenV2.EnableAutoDropBack));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._autoDropBackEnabled.DecrementValueButton, new System.Action(SettingsScreenV2.DisableAutoDropBack));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.AutoDropbackEnabled, new Action<bool>(this.HandleAutoDropBackChange));
      this.HandleAutoDropBackChange(isEnabled);
      if (isEnabled)
        this._autoDropBackEnabled.ValueText[0].color = Color.black;
      else
        this._autoDropBackEnabled.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private static void EnableAutoDropBack() => ScriptableSingleton<VRSettings>.Instance.AutoDropbackEnabled.SetValue(true);

    private static void DisableAutoDropBack() => ScriptableSingleton<VRSettings>.Instance.AutoDropbackEnabled.SetValue(false);

    private void HandleAutoDropBackChange(bool isEnabled)
    {
      this._autoDropBackEnabled.IncrementValueButton.SetInteractible(!isEnabled);
      this._autoDropBackEnabled.DecrementValueButton.SetInteractible(isEnabled);
    }

    private IEnumerable<EventHandle> InitializeAutoHandoffSettings()
    {
      bool isEnabled = (bool) VRState.AutoHandoffEnabled.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._autoHandoffEnabled.IncrementValueButton, new System.Action(SettingsScreenV2.EnableAutoHandoff));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._autoHandoffEnabled.DecrementValueButton, new System.Action(SettingsScreenV2.DisableAutoHandoff));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) VRState.AutoHandoffEnabled, new Action<bool>(this.HandleAutoHandoffChange));
      this.HandleAutoHandoffChange(isEnabled);
      if (isEnabled)
        this._autoHandoffEnabled.ValueText[0].color = Color.black;
      else
        this._autoHandoffEnabled.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private static void EnableAutoHandoff() => VRState.AutoHandoffEnabled.SetValue(true);

    private static void DisableAutoHandoff() => VRState.AutoHandoffEnabled.SetValue(false);

    private void HandleAutoHandoffChange(bool isEnabled)
    {
      this._autoHandoffEnabled.IncrementValueButton.SetInteractible(!isEnabled);
      this._autoHandoffEnabled.DecrementValueButton.SetInteractible(isEnabled);
    }

    private IEnumerable<EventHandle> InitializeInstrumentalSettings()
    {
      Debug.Log((object) ("InitializeInstrumentalSettings: _settingsStore.InstrumentalMusic: " + ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic?.ToString()));
      bool instrumentalMusic = (bool) ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic;
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._instrumentalEnabled.IncrementValueButton, new System.Action(this.EnableInstrumental));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._instrumentalEnabled.DecrementValueButton, new System.Action(this.DisableInstrumental));
      EventHandle eventHandle = EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic, new Action<bool>(this.HandleInstrumentalChange));
      this.HandleInstrumentalChange(instrumentalMusic);
      if (!instrumentalMusic)
        this._instrumentalEnabled.ValueText[0].color = Color.black;
      else
        this._instrumentalEnabled.ValueText[1].color = Color.black;
      return (IEnumerable<EventHandle>) new EventHandle[3]
      {
        (EventHandle) uiHandle1,
        (EventHandle) uiHandle2,
        eventHandle
      };
    }

    private void EnableInstrumental()
    {
      Debug.Log((object) nameof (EnableInstrumental));
      ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic.SetValue(false);
      AppSounds.UpdateInstrumental.Trigger(false);
    }

    private void DisableInstrumental()
    {
      Debug.Log((object) nameof (DisableInstrumental));
      ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic.SetValue(true);
      AppSounds.UpdateInstrumental.Trigger(true);
    }

    private void HandleInstrumentalChange(bool isEnabled)
    {
      Debug.Log((object) ("HandleInstrumentalChange: isEnabled: " + isEnabled.ToString()));
      this._instrumentalEnabled.IncrementValueButton.SetInteractible(isEnabled);
      this._instrumentalEnabled.DecrementValueButton.SetInteractible(!isEnabled);
    }

    public void RefreshAllSettingsVisuals() => this.UpdateSettingsElements();

    private void UpdateSettingsElements()
    {
      VRSettings instance = ScriptableSingleton<VRSettings>.Instance;
      int newLevel = (int) GameSettings.DifficultyLevel.GetValue();
      int newValue1 = (int) instance.QuarterLength.GetValue();
      int newValue2 = (int) instance.HuddlePlayClock.GetValue();
      int newValue3 = (int) instance.NoHuddlePlayClockOffset.GetValue();
      bool isSeated = (bool) instance.SeatedMode.GetValue();
      bool isEnabled1 = (bool) instance.UseVrLaser.GetValue();
      bool isEnabled2 = (bool) instance.OneHandedMode.GetValue();
      bool isEnabled3 = (bool) instance.GripButtonThrow.GetValue();
      bool assistEnabled = !(bool) instance.AlphaThrowing.GetValue();
      bool isLeftHand = (bool) instance.UseLeftHand.GetValue();
      bool isEnabled4 = (bool) instance.HelmetActive.GetValue();
      bool isEnabled5 = (bool) instance.ImmersiveTackleEnabled.GetValue();
      bool isEnabled6 = (bool) instance.AutoDropbackEnabled.GetValue();
      bool isEnabled7 = (bool) VRState.AutoHandoffEnabled.GetValue();
      bool instrumentalMusic = (bool) ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic;
      this.HandleDifficultyChange(newLevel);
      this.HandleQuarterLengthChange(newValue1);
      this.HandleHuddleTimeChange(newValue2);
      this.HandleNoHuddleRunoffTimeChange(newValue3);
      this.HandleSeatedPlaymodeChange(isSeated);
      this.HandleVrLaserEnableChange(isEnabled1);
      this.HandleOneHandedModeChange(isEnabled2);
      this.HandleGrabWithGripsChange(isEnabled3);
      this.HandlePassaAssistChange(assistEnabled);
      this.HandleDominantHandChange(isLeftHand);
      this.HandleHelmetEffectChange(isEnabled4);
      this.HandleImmersiveTackleChange(isEnabled5);
      this.HandleAutoDropBackChange(isEnabled6);
      this.HandleAutoHandoffChange(isEnabled7);
      this.HandleInstrumentalChange(instrumentalMusic);
      this.HandleAccountLinkToggle();
      this.UpdateSliderValues();
    }

    private IEnumerable<EventHandle> InitializeSettingSlider(
      Slider slider,
      VariableFloat variable,
      UnityAction<float> onValueChange)
    {
      slider.onValueChanged.AddListener(onValueChange);
      return (IEnumerable<EventHandle>) new UIHandle[1]
      {
        UIHandle.Link(slider, variable)
      };
    }

    private void UpdateBGMVolume(float value)
    {
      ScriptableSingleton<SettingsStore>.Instance.BgmVolume.SetValue(value);
      AppSounds.UpdateBGMVolume.Trigger(value);
    }

    private void UpdateVoVolume(float value)
    {
      ScriptableSingleton<SettingsStore>.Instance.HostVoVolume.SetValue(value);
      AppSounds.UpdateVOVolume.Trigger(value);
    }

    private void UpdateSfxVolume(float value)
    {
      ScriptableSingleton<SettingsStore>.Instance.SfxVolume.SetValue(value);
      AppSounds.UpdateSFXVolume.Trigger(value);
    }

    private void UpdateStadiumVolume(float value)
    {
      ScriptableSingleton<SettingsStore>.Instance.StadiumVolume.SetValue(value);
      AppSounds.UpdateStadiumVolume.Trigger(value);
    }

    private void UpdateSliderValues()
    {
      float input1 = (float) ScriptableSingleton<SettingsStore>.Instance.BgmVolume.GetValue();
      float input2 = (float) ScriptableSingleton<SettingsStore>.Instance.HostVoVolume.GetValue();
      float input3 = (float) ScriptableSingleton<SettingsStore>.Instance.SfxVolume.GetValue();
      float input4 = (float) ScriptableSingleton<SettingsStore>.Instance.StadiumVolume.GetValue();
      this._musicSlider.SetValueWithoutNotify(input1);
      this._coachSlider.SetValueWithoutNotify(input2);
      this._sfxSlider.SetValueWithoutNotify(input3);
      this._stadiumSlider.SetValueWithoutNotify(input4);
    }
  }
}
