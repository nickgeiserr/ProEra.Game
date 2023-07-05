// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UI.Screens.Shared.Settings.PauseScreenV2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using Photon.Pun;
using ProEra.Game.Sources.SeasonMode.SeasonTablet;
using System;
using System.Collections.Generic;
using TB12;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;
using Vars;

namespace ProEra.Game.UI.Screens.Shared.Settings
{
  public class PauseScreenV2 : UIView
  {
    [SerializeField]
    private ThrowSettings _throwSettingsStore;
    [SerializeField]
    private CanvasTabManager _thisCanvasTabManager;
    [Space]
    [Header("General Settings")]
    [SerializeField]
    private Image _controlsImage;
    [SerializeField]
    private LocalizeSpriteEvent _controlGraphics;
    [Space]
    [Header("Gameplay Settings")]
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
    private Slider _voiceChatSlider;
    [SerializeField]
    private Slider _microphoneSlider;
    [SerializeField]
    private SettingsValueButtonUI _instrumentalEnabled;
    [Space]
    [Header("Close Btn")]
    [Space]
    [SerializeField]
    private TouchButton _closeButton;
    [SerializeField]
    private TextMeshProUGUI _closeButtonText;
    [Space]
    [Header("Resume Btn")]
    [Space]
    [SerializeField]
    private TouchButton _resumeButton;
    [Space]
    [Header("SinglePlayer - Confirmation Popups")]
    [SerializeField]
    private Image popup_ReturnToLockerRoom;
    [SerializeField]
    private TouchButton popupBtn_ReturnToLocker_Cancel;
    [SerializeField]
    private TouchButton popupBtn_ReturnToLocker_Confirm;
    [Space]
    [Header("Multiplayer")]
    [SerializeField]
    private TouchButton inviteFriendsBtn;
    [SerializeField]
    private TouchButton kickPlayersBtn;
    [SerializeField]
    private TouchButton leaveLobbyBtn;
    [SerializeField]
    private MultiplayerStore _multiplayerStore;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private TextMeshProUGUI[] _displayNames;
    [SerializeField]
    private KickPlayerListScreen _kickScreen;
    private List<EventHandle> _eventHandlers;
    private Color closeBtnTextColor;
    private bool _lastLoco;
    private const string LocalizationButtonGrip = "PauseMenuSP_UserSettings_GrabInput_Grip";
    private const string LocalizationButtonTrigger = "PauseMenuSP_UserSettings_GrabInput_Trigger";
    private const string LocalizationButtonR1L1 = "PauseMenuSP_UserSettings_GrabInput_R1L1";
    private const string LocalizationButtonR2L2 = "PauseMenuSP_UserSettings_GrabInput_R2L2";
    private const string LocalizationControllers = "SettingsGeneral_Sprite_Controls";

    public override Enum ViewId => (Enum) EScreens.kUnknown;

    protected override void OnInitialize()
    {
      this._throwSettingsStore = ScriptableSingleton<ThrowSettings>.Instance;
      this._eventHandlers = new List<EventHandle>();
      this.InitializePlatformGraphics();
      if ((UnityEngine.Object) this._autoDropBackEnabled != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(this.InitializeAutoDropBackSettings());
      if ((UnityEngine.Object) this._autoHandoffEnabled != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(this.InitializeAutoHandoffSettings());
      if ((UnityEngine.Object) this._vrLaserPointerEnabled != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(this.InitializeVrLaserSettings());
      if ((UnityEngine.Object) this._helmetEffectEnabled != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(this.InitializeHelmetEffectSettings());
      if ((UnityEngine.Object) this._oneHandedMode != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(this.InitializeOneHandedModeSettings());
      if ((UnityEngine.Object) this._playModeIsSeated != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(this.InitializeSeatedPlaymodeSettings());
      if ((UnityEngine.Object) this._grabWithGrips != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(this.InitializeGrabWithGripsSettings());
      if ((UnityEngine.Object) this._immersiveTackleEnabled != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(this.InitializeImmersiveTackleSettings());
      if ((UnityEngine.Object) this._sfxSlider != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(PauseScreenV2.InitializeSettingSlider(this._sfxSlider, ScriptableSingleton<SettingsStore>.Instance.SfxVolume, new UnityAction<float>(this.UpdateSfxVolume)));
      if ((UnityEngine.Object) this._musicSlider != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(PauseScreenV2.InitializeSettingSlider(this._musicSlider, ScriptableSingleton<SettingsStore>.Instance.BgmVolume, new UnityAction<float>(this.UpdateBGMVolume)));
      if ((bool) (UnityEngine.Object) this._coachSlider)
        this._eventHandlers.AddRange(PauseScreenV2.InitializeSettingSlider(this._coachSlider, ScriptableSingleton<SettingsStore>.Instance.HostVoVolume, new UnityAction<float>(this.UpdateVoVolume)));
      if ((bool) (UnityEngine.Object) this._stadiumSlider)
        this._eventHandlers.AddRange(PauseScreenV2.InitializeSettingSlider(this._stadiumSlider, ScriptableSingleton<SettingsStore>.Instance.StadiumVolume, new UnityAction<float>(this.UpdateStadiumVolume)));
      if ((bool) (UnityEngine.Object) this._voiceChatSlider)
        this._eventHandlers.AddRange(PauseScreenV2.InitializeSettingSlider(this._voiceChatSlider, ScriptableSingleton<VRSettings>.Instance.VCVolume, new UnityAction<float>(this.UpdateMicrophoneVolume)));
      if ((bool) (UnityEngine.Object) this._microphoneSlider)
        this._eventHandlers.AddRange(PauseScreenV2.InitializeSettingSlider(this._microphoneSlider, ScriptableSingleton<VRSettings>.Instance.MicVolume, new UnityAction<float>(this.UpdateMicrophoneVolume)));
      if ((UnityEngine.Object) this._instrumentalEnabled != (UnityEngine.Object) null)
        this._eventHandlers.AddRange(this.InitializeInstrumentalSettings());
      if ((UnityEngine.Object) this._closeButton != (UnityEngine.Object) null)
      {
        this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this._closeButton, new System.Action(this.CloseMenuHandler)));
        this.closeBtnTextColor = this._closeButtonText.color;
      }
      if ((bool) (UnityEngine.Object) this._resumeButton)
        this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this._resumeButton, new System.Action(this.ResumeButtonHandler)));
      if ((UnityEngine.Object) this.popupBtn_ReturnToLocker_Cancel != (UnityEngine.Object) null)
        this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this.popupBtn_ReturnToLocker_Cancel, new System.Action(this.ClosePopup_ReturnToLockerConfirmation)));
      if ((UnityEngine.Object) this.popupBtn_ReturnToLocker_Confirm != (UnityEngine.Object) null)
        this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this.popupBtn_ReturnToLocker_Confirm, new System.Action(this.ReturnPlayerToLockerRoom)));
      if ((UnityEngine.Object) this.inviteFriendsBtn != (UnityEngine.Object) null)
        this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this.inviteFriendsBtn, new System.Action(GroupPresenceManager.Instance.OpenInvitePanel)));
      if ((UnityEngine.Object) this.kickPlayersBtn != (UnityEngine.Object) null)
        this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this.kickPlayersBtn, new System.Action(this.SpawnKickPlayerScreen)));
      if ((UnityEngine.Object) this.leaveLobbyBtn != (UnityEngine.Object) null)
        this._eventHandlers.Add((EventHandle) UIHandle.Link((IButton) this.leaveLobbyBtn, new System.Action(this.CloseMenuHandler)));
      if ((UnityEngine.Object) this._multiplayerStore != (UnityEngine.Object) null)
      {
        this._multiplayerStore.OnDataChanged.Link(new System.Action(this.UpdateDisplayNames));
        this.UpdateDisplayNames();
      }
      this.linksHandler.SetLinks(this._eventHandlers);
    }

    protected override void WillAppear()
    {
      this.UpdateSettingsElements();
      this.UpdateDisplayNames();
      this._thisCanvasTabManager.SetTabsEnabled(true);
      this._lastLoco = VRState.LocomotionEnabled.Value;
      Debug.Log((object) ("PauseMenu: _lastLoco[" + this._lastLoco.ToString() + "]"));
      VRState.LocomotionEnabled.SetValue(false);
    }

    private void OnEnable() => this._closeButtonText.color = this.closeBtnTextColor;

    protected override void WillDisappear()
    {
      if (PersistentSingleton<SaveManager>.Exist())
      {
        ScriptableSingleton<SettingsStore>.Instance.PrepareToSave();
        PersistentSingleton<SaveManager>.Instance.AddToSaveQueue((ISaveSync) ScriptableSingleton<SettingsStore>.Instance.saveSettingsStore);
        ScriptableSingleton<VRSettings>.Instance.PrepareToSave();
        PersistentSingleton<SaveManager>.Instance.AddToSaveQueue((ISaveSync) ScriptableSingleton<VRSettings>.Instance.saveVRSettings);
        ScriptableSingleton<GameSettings>.Instance.PrepareToSave();
        PersistentSingleton<SaveManager>.Instance.AddToSaveQueue((ISaveSync) ScriptableSingleton<GameSettings>.Instance.saveOldGameSettings);
      }
      VRState.LocomotionEnabled.SetValue(this._lastLoco);
    }

    private void OnDestroy() => this.linksHandler.Clear();

    private void InitializePlatformGraphics()
    {
      if (!(bool) (UnityEngine.Object) this._controlGraphics)
        return;
      this._controlGraphics.AssetReference.TableEntryReference = (TableEntryReference) ("SettingsGeneral_Sprite_Controls" + VRUtils.GetDeviceType().ToString());
    }

    private void ResumeButtonHandler() => VRState.PauseMenu.Toggle();

    private void CloseMenuHandler()
    {
      if (!((UnityEngine.Object) this.popup_ReturnToLockerRoom != (UnityEngine.Object) null))
        return;
      this.popup_ReturnToLockerRoom.gameObject.SetActive(true);
    }

    public void CloseAllPopups() => this.ClosePopup_ReturnToLockerConfirmation();

    private void ClosePopup_ReturnToLockerConfirmation()
    {
      if (!((UnityEngine.Object) this.popup_ReturnToLockerRoom != (UnityEngine.Object) null))
        return;
      this.popup_ReturnToLockerRoom.gameObject.SetActive(false);
    }

    private void ReturnPlayerToLockerRoom()
    {
      if ((UnityEngine.Object) this.popup_ReturnToLockerRoom != (UnityEngine.Object) null)
        this.popup_ReturnToLockerRoom.gameObject.SetActive(false);
      PersistentSingleton<BallsContainerManager>.Instance.Clear();
      VRState.PauseMenu.Toggle();
      AppEvents.UserRequestedMainMenu.Trigger();
      AppEvents.LoadMainMenu.Trigger();
    }

    private void SpawnKickPlayerScreen()
    {
      if (!PhotonNetwork.IsMasterClient)
        return;
      this._kickScreen.gameObject.SetActive(true);
      this._kickScreen.InjectData(this._multiplayerStore.GetCurrentLobbyPlayers(), this._playerProfile.Profiles);
    }

    private void UpdateDisplayNames()
    {
      if (!((UnityEngine.Object) this._multiplayerStore != (UnityEngine.Object) null))
        return;
      List<int> currentLobbyPlayers = this._multiplayerStore.GetCurrentLobbyPlayers();
      if (currentLobbyPlayers == null)
        return;
      for (int index = 0; index < currentLobbyPlayers.Count; ++index)
      {
        PlayerCustomization playerCustomization;
        if (this._playerProfile.Profiles.TryGetValue(currentLobbyPlayers[index], out playerCustomization))
          this._displayNames[index].text = (string) playerCustomization.LastName;
        else
          this._displayNames[index].text = "UNKNOWN";
      }
      for (int count = currentLobbyPlayers.Count; count < this._displayNames.Length; ++count)
        this._displayNames[count].text = "";
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
      this.localizeGripText.StringReference.TableEntryReference = (TableEntryReference) "PauseMenuSP_UserSettings_GrabInput_Grip";
      this.localizeTriggerText.StringReference.TableEntryReference = (TableEntryReference) "PauseMenuSP_UserSettings_GrabInput_Trigger";
    }

    private IEnumerable<EventHandle> InitializeImmersiveTackleSettings()
    {
      bool isEnabled = (bool) ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.GetValue();
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._immersiveTackleEnabled.IncrementValueButton, new System.Action(PauseScreenV2.EnableImmersiveTackle));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._immersiveTackleEnabled.DecrementValueButton, new System.Action(PauseScreenV2.DisableImmersiveTackle));
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
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._autoDropBackEnabled.IncrementValueButton, new System.Action(PauseScreenV2.EnableAutoDropBack));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._autoDropBackEnabled.DecrementValueButton, new System.Action(PauseScreenV2.DisableAutoDropBack));
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
      UIHandle uiHandle1 = UIHandle.Link((IButton) this._autoHandoffEnabled.IncrementValueButton, new System.Action(PauseScreenV2.EnableAutoHandoff));
      UIHandle uiHandle2 = UIHandle.Link((IButton) this._autoHandoffEnabled.DecrementValueButton, new System.Action(PauseScreenV2.DisableAutoHandoff));
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
      if (!isEnabled)
        this._instrumentalEnabled.ValueText[0].color = Color.black;
      else
        this._instrumentalEnabled.ValueText[1].color = Color.black;
    }

    public void UpdateSettingsElements()
    {
      VRSettings instance = ScriptableSingleton<VRSettings>.Instance;
      bool isSeated = (bool) instance.SeatedMode.GetValue();
      bool isEnabled1 = (bool) instance.UseVrLaser.GetValue();
      bool isEnabled2 = (bool) instance.HelmetActive.GetValue();
      bool isEnabled3 = (bool) instance.ImmersiveTackleEnabled.GetValue();
      bool isEnabled4 = (bool) instance.AutoDropbackEnabled.GetValue();
      bool isEnabled5 = (bool) instance.GripButtonThrow.GetValue();
      bool isEnabled6 = (bool) VRState.AutoHandoffEnabled.GetValue();
      bool instrumentalMusic = (bool) ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic;
      this.UpdateSliderValues();
      if ((UnityEngine.Object) this._vrLaserPointerEnabled != (UnityEngine.Object) null)
        this.HandleVrLaserEnableChange(isEnabled1);
      if ((UnityEngine.Object) this._helmetEffectEnabled != (UnityEngine.Object) null)
        this.HandleHelmetEffectChange(isEnabled2);
      if ((UnityEngine.Object) this._playModeIsSeated != (UnityEngine.Object) null)
        this.HandleSeatedPlaymodeChange(isSeated);
      if ((UnityEngine.Object) this._immersiveTackleEnabled != (UnityEngine.Object) null)
        this.HandleImmersiveTackleChange(isEnabled3);
      if ((UnityEngine.Object) this._autoDropBackEnabled != (UnityEngine.Object) null)
        this.HandleAutoDropBackChange(isEnabled4);
      if ((UnityEngine.Object) this._autoHandoffEnabled != (UnityEngine.Object) null)
        this.HandleAutoHandoffChange(isEnabled6);
      if ((UnityEngine.Object) this._grabWithGrips != (UnityEngine.Object) null)
        this.HandleGrabWithGripsChange(isEnabled5);
      if (!((UnityEngine.Object) this._instrumentalEnabled != (UnityEngine.Object) null))
        return;
      this.HandleInstrumentalChange(instrumentalMusic);
    }

    private static IEnumerable<EventHandle> InitializeSettingSlider(
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
      if ((UnityEngine.Object) ScriptableSingleton<SettingsStore>.Instance != (UnityEngine.Object) null)
      {
        float input1 = (float) ScriptableSingleton<SettingsStore>.Instance.BgmVolume.GetValue();
        float input2 = (float) ScriptableSingleton<SettingsStore>.Instance.HostVoVolume.GetValue();
        float input3 = (float) ScriptableSingleton<SettingsStore>.Instance.SfxVolume.GetValue();
        float input4 = (float) ScriptableSingleton<SettingsStore>.Instance.StadiumVolume.GetValue();
        if ((UnityEngine.Object) this._musicSlider != (UnityEngine.Object) null)
          this._musicSlider.SetValueWithoutNotify(input1);
        if ((UnityEngine.Object) this._coachSlider != (UnityEngine.Object) null)
          this._coachSlider.SetValueWithoutNotify(input2);
        if ((UnityEngine.Object) this._sfxSlider != (UnityEngine.Object) null)
          this._sfxSlider.SetValueWithoutNotify(input3);
        if (!((UnityEngine.Object) this._stadiumSlider != (UnityEngine.Object) null))
          return;
        this._stadiumSlider.SetValueWithoutNotify(input4);
      }
      else
        Debug.LogError((object) "_settingsStore is null in PauseScreenV2->UpdateSliderValues");
    }

    private void UpdateMicrophoneVolume(float value)
    {
    }
  }
}
