// Decompiled with JetBrains decompiler
// Type: FootballVR.Startup
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ClockStone;
using FootballVR.UI;
using Framework;
using Oculus.Platform;
using Oculus.Platform.Models;
using ProEra.Game.Startup;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12;
using UnityEngine;
using UnityEngine.XR;
using Vars;

namespace FootballVR
{
  public class Startup : MonoBehaviour
  {
    [SerializeField]
    private EAppPlatform _currentPlatform = EAppPlatform.Unknown;
    public EAppState QuickStartDestination;
    public string RoomName = "TestRoom";
    [SerializeField]
    private GameObject[] _gameObjects;
    [SerializeField]
    private GameObject _splashSequencePrefab;
    [SerializeField]
    private EditorUserControls _editorControlsPrefab;
    [SerializeField]
    private UIAnchoring _uiPrefab;
    [SerializeField]
    private GameObject _clientSocialPrefab;
    [SerializeField]
    private SceneAssetString _frontScreen;
    [SerializeField]
    private bool _waitForSaveManagerInit = true;
    private readonly RoutineHandle _initRoutine = new RoutineHandle();
    private readonly RoutineHandle _startupRoutine = new RoutineHandle();
    [EditorSetting(ESettingType.FeatureToggle)]
    private static bool skipIntro = false;
    public static readonly OneShotEvent InitializationComplete = new OneShotEvent();
    [HideInInspector]
    public bool isInitialized;

    public EAppPlatform CurrentPlatform
    {
      get => this._currentPlatform;
      set => this._currentPlatform = value;
    }

    private void Start()
    {
      Debug.Log((object) Microphone.devices.Length);
      Debug.Log((object) UnityEngine.Application.internetReachability.ToString());
      MessagePackStartup.Initialize();
      this.CurrentPlatform = this.CurrentPlatform != EAppPlatform.Unknown ? this._currentPlatform : this.GetPlatform();
      StartupState.PlatformInUse.SetValue(this.CurrentPlatform);
      UnityEngine.Object.Instantiate<GameObject>(this._clientSocialPrefab);
      Console.WriteLine((object) this.CurrentPlatform);
      switch (this.CurrentPlatform)
      {
        case EAppPlatform.Desktop:
          this._initRoutine.Run(this.InitializeDesktop_Platform());
          break;
        case EAppPlatform.DesktopVR:
          this._initRoutine.Run(this.InitializeDesktopVR_Platform());
          break;
        case EAppPlatform.PSVR:
        case EAppPlatform.PSVR2:
          this._initRoutine.Run(this.InitializePSVR_Platform());
          break;
        case EAppPlatform.Quest2:
          this._initRoutine.Run(this.InitializeQuest2_Platform());
          break;
        case EAppPlatform.Quest1:
          this._initRoutine.Run(this.InitializeQuest1_Platform());
          break;
      }
      this._startupRoutine.Run(this.StartupRoutine());
    }

    private void ConfigureDestination()
    {
      DestinationOptions destinationOptions = new DestinationOptions();
      destinationOptions.TimeOfDay = ETimeOfDay.Clear;
      destinationOptions.Mode = EMode.kSolo;
      if (this.QuickStartDestination != EAppState.kUnknown && UnityEngine.Application.platform == RuntimePlatform.WindowsEditor)
      {
        destinationOptions.AppState = this.QuickStartDestination;
        if (this.QuickStartDestination == EAppState.kMultiplayerLobby)
        {
          destinationOptions.ApiName = DestinationDefinitions.GetDestinationApiName(DestinationDefinitions.Destination.Multiplayer_Lobby, StartupState.PlatformInUse.Value);
          destinationOptions.LobbySessionID = this.RoomName;
          destinationOptions.Mode = EMode.kMultiplayer;
          destinationOptions.Password = string.Empty;
        }
      }
      else
      {
        if (!(StartupState.CurrentStartupDestination.Value.ApiName == destinationOptions.ApiName))
          return;
        if (PersistentSingleton<SaveManager>.Instance.SeasonModeDataExists())
        {
          destinationOptions.ApiName = DestinationDefinitions.GetDestinationApiName(DestinationDefinitions.Destination.Core_LockerRoom, StartupState.PlatformInUse.Value);
          destinationOptions.AppState = EAppState.kMainMenuActivation;
        }
        else
        {
          destinationOptions.ApiName = DestinationDefinitions.GetDestinationApiName(DestinationDefinitions.Destination.Core_Onboarding, StartupState.PlatformInUse.Value);
          destinationOptions.AppState = EAppState.kOnboardingOptions;
          AppState.GameMode = EGameMode.kOnboarding;
        }
      }
      StartupState.CurrentStartupDestination.SetValue(destinationOptions);
    }

    private void InputDevicesOndeviceConnected(InputDevice obj) => Debug.Log((object) ("Device connected: " + obj.name));

    private IEnumerator InitializeDesktop_Platform()
    {
      yield return (object) null;
      this.AfterInitializePlatform();
    }

    private IEnumerator StartupRoutine()
    {
      FootballVR.Startup startup = this;
      WaitForSeconds delay = new WaitForSeconds(0.1f);
      GamePlayerController player = PersistentSingleton<GamePlayerController>.Instance;
      foreach (GameObject gameObject in startup._gameObjects)
      {
        gameObject.transform.SetParent((Transform) null);
        gameObject.SetActive(true);
      }
      if (!FootballVR.Startup.skipIntro && !(bool) ScriptableSingleton<VRSettings>.Instance.BypassStartup && (bool) (UnityEngine.Object) startup._splashSequencePrefab)
      {
        GameObject splash = UnityEngine.Object.Instantiate<GameObject>(startup._splashSequencePrefab);
        yield return (object) new WaitUntil((Func<bool>) (() => SplashManager.Finished));
        UnityEngine.Object.Destroy((UnityEngine.Object) splash);
        splash = (GameObject) null;
      }
      if ((bool) (UnityEngine.Object) SingletonMonoBehaviour<AudioController>.DoesInstanceExist())
        SingletonMonoBehaviour<AudioController>.Instance.ValidateCategoriesEarly();
      if (SaveManager.bIsInitialized || !startup._waitForSaveManagerInit)
      {
        PersistentSingleton<SaveManager>.Instance.StartLoadingSeasonModeData();
      }
      else
      {
        yield return (object) new WaitUntil((Func<bool>) (() => SaveManager.bIsInitialized));
        PersistentSingleton<SaveManager>.Instance.StartLoadingSeasonModeData();
      }
      yield return (object) new WaitForSeconds(2f);
      if ((bool) (UnityEngine.Object) startup._uiPrefab && (bool) (UnityEngine.Object) player)
      {
        Debug.Log((object) ("Startup Instantiate UI Prefab " + startup._uiPrefab.name));
        UnityEngine.Object.Instantiate<UIAnchoring>(startup._uiPrefab, player.transform).transform.ResetTransform();
      }
      if ((bool) ScriptableSingleton<VRSettings>.Instance.BypassStartup)
      {
        yield return (object) GamePlayerController.CameraFade.Fade();
        TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.Loading, true);
        yield return (object) GamePlayerController.CameraFade.Clear();
        yield return (object) delay;
      }
      startup.ConfigureDestination();
      FootballVR.Startup.InitializationComplete.Trigger();
      StartupState.StartupComplete.Trigger();
      yield return (object) delay;
      startup.isInitialized = true;
      if (StartupState.CurrentStartupDestination.Value.AppState != EAppState.kMainMenuActivation)
        startup.StartCoroutine(PersistentSingleton<LevelManager>.Instance.LoadGameplay(startup._frontScreen));
    }

    private IEnumerator InitializePSVR_Platform()
    {
      bool useVrLaser = (bool) ScriptableSingleton<VRSettings>.Instance.UseVrLaser;
      int num = (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? 1 : 0;
      VRInputManager.LeftLaserInput = (num & (useVrLaser ? 1 : 0)) != 0;
      VRInputManager.RightLaserInput = num == 0 & useVrLaser;
      yield return (object) null;
      this.AfterInitializePlatform();
    }

    private IEnumerator InitializeQuest2_Platform()
    {
      bool useVrLaser = (bool) ScriptableSingleton<VRSettings>.Instance.UseVrLaser;
      int num = (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? 1 : 0;
      VRInputManager.LeftLaserInput = (num & (useVrLaser ? 1 : 0)) != 0;
      VRInputManager.RightLaserInput = num == 0 & useVrLaser;
      yield return (object) null;
      this.AfterInitializePlatform();
    }

    private IEnumerator InitializeQuest1_Platform()
    {
      bool useVrLaser = (bool) ScriptableSingleton<VRSettings>.Instance.UseVrLaser;
      int num = (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? 1 : 0;
      VRInputManager.LeftLaserInput = (num & (useVrLaser ? 1 : 0)) != 0;
      VRInputManager.RightLaserInput = num == 0 & useVrLaser;
      yield return (object) null;
      this.AfterInitializePlatform();
    }

    private IEnumerator InitializeDesktopVR_Platform()
    {
      FootballVR.Startup startup = this;
      if (!VRUtils.ViveConnected)
        Core.AsyncInitialize().OnComplete(new Message<PlatformInitialize>.Callback(startup.HandleInitializeComplete));
      else
        LaserPointer.LaserBehavior = LaserPointer.LaserBeamBehavior.Off;
      yield return (object) null;
      startup.AfterInitializePlatform();
    }

    private void AfterInitializePlatform()
    {
      Debug.Log((object) ("Loaded device:" + XRSettings.loadedDeviceName));
      List<InputDevice> inputDeviceList = new List<InputDevice>();
      InputDevices.GetDevices(inputDeviceList);
      foreach (InputDevice inputDevice in inputDeviceList)
        Debug.Log((object) ("InputDevice: " + inputDevice.name + ", " + inputDevice.manufacturer));
      InputDevices.deviceConnected += new Action<InputDevice>(this.InputDevicesOndeviceConnected);
      Debug.Log((object) ("Headset: " + InputDevices.GetDeviceAtXRNode(XRNode.Head).name));
    }

    private void HandleInitializeComplete(Message<PlatformInitialize> message)
    {
      if (message == null)
        Debug.LogError((object) "Null msg (GPC)");
      else if (message.IsError)
        Debug.LogError((object) ("Error on platform initialize: " + message.GetError().Message));
      else
        Debug.Log((object) "Oculus platform initialized!");
    }

    private EAppPlatform GetPlatform()
    {
      EAppPlatform platform = EAppPlatform.Unknown;
      switch (UnityEngine.Application.platform)
      {
        case RuntimePlatform.WindowsPlayer:
          platform = XRSettings.isDeviceActive ? EAppPlatform.DesktopVR : EAppPlatform.Desktop;
          break;
        case RuntimePlatform.WindowsEditor:
          platform = EAppPlatform.DesktopVR;
          break;
        case RuntimePlatform.Android:
          platform = this.GetQuestType();
          break;
        case RuntimePlatform.PS4:
          platform = EAppPlatform.PSVR;
          break;
        case RuntimePlatform.PS5:
          platform = EAppPlatform.PSVR2;
          break;
      }
      return platform;
    }

    private EAppPlatform GetQuestType()
    {
      EAppPlatform questType = EAppPlatform.Unknown;
      if (UnityEngine.Application.platform == RuntimePlatform.Android)
      {
        switch (OVRPlugin.GetSystemHeadsetType())
        {
          case OVRPlugin.SystemHeadset.Oculus_Quest:
            questType = EAppPlatform.Quest1;
            break;
          case OVRPlugin.SystemHeadset.Oculus_Quest_2:
            questType = EAppPlatform.Quest2;
            break;
        }
      }
      return questType;
    }

    private void OnDestroy()
    {
      this._startupRoutine.Stop();
      InputDevices.deviceConnected -= new Action<InputDevice>(this.InputDevicesOndeviceConnected);
    }
  }
}
