// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.GameManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballVR.Multiplayer;
using FootballVR.UI;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.Networked;
using Framework.StateManagement;
using Photon.Pun;
using ProEra.Game;
using ProEra.Game.Startup;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.Backend;
using TB12.UI;
using TB12.UI.Screens;
using UDB;
using UnityEngine;
using UnityEngine.Events;
using Vars;

namespace TB12.AppStates
{
  public class GameManager : MonoBehaviour
  {
    [SerializeField]
    private GameStateManager _stateManager;
    [SerializeField]
    private GameDataUpdater _gameDataUpdater;
    [SerializeField]
    private GameplayStore _multiplayerStore;
    [SerializeField]
    private GameplayStore _gameplayStore;
    [SerializeField]
    private GameEditorControls _editorControlsPrefab;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private PauseScreen _pauseScreenPrefab;
    [SerializeField]
    private ErrorScreen _errorScreenPrefab;
    [SerializeField]
    private GameObject _singlePlayerUIPrefab;
    [SerializeField]
    private GameObject _multiPlayerUIPrefab;
    [SerializeField]
    private BallTrailStore _ballTrailStore;
    [SerializeField]
    private TeamBallMatStore _ballMatStore;
    [SerializeField]
    private Transform _ballTrailPool;
    private PauseScreen _pauseScreen;
    private ErrorScreen _errorScreen;
    private StadiumConfigStore m_stadiumConfigStore;
    [EditorSetting(ESettingType.Utility)]
    private static bool forceGameMode = false;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private readonly RoutineHandle _routineStadiumHandle = new RoutineHandle();
    [SerializeField]
    private MultiplayerManager multiplayerManagerPrefab;
    public static readonly OneShotEvent GameLoadComplete = new OneShotEvent();
    public static GameManager Instance;
    private DestinationOptions multiplayerDestinationToLoad;
    private bool flagDestinationMultiplayer;
    private bool gameLoadComplete;

    private GameGraphicsSettings _graphicsSettings => ScriptableSingleton<GameGraphicsSettings>.Instance;

    public GameplayStore CurrentGameplayStore => (EMode) AppState.Mode != EMode.kSolo ? this._multiplayerStore : this._gameplayStore;

    private void Awake()
    {
      GameManager.Instance = this;
      if (SaveManager.bIsInitialized)
        this.LoadSettings();
      else
        SaveManager.OnInitialized += new UnityAction(this.LoadSettings);
      AppSettings instance = ScriptableSingleton<AppSettings>.Instance;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        AppEvents.LoadMainMenu.Link(new System.Action(this.HandleLoadMainMenu)),
        AppEvents.RequestLoadState.Link<EAppState, ETimeOfDay, string>(new Action<EAppState, ETimeOfDay, string>(((StateManager<EAppState, GameState>) this._stateManager).LoadState)),
        this._stateManager.InTransition.Link<bool>(new Action<bool>(this.HandleStateTransition)),
        AppEvents.Retry.Link(new System.Action(this.ResetCollisionState)),
        instance.OptimizationSettings.UIVisible.Link<bool>(new Action<bool>(this.HandleUI)),
        MultiplayerEvents.LoadMultiplayerGame.Link<string>(new Action<string>(this.HandleLoadMultiplayer)),
        DevControls.LoadIntelAnimation.Link(new System.Action(this.HandleIntelAnimation)),
        PlayerCollisionHandler.IsDown.Link<bool>(new Action<bool>(this.HandlePlayerDown)),
        this._graphicsSettings.CrowdEnabled.Link<bool>(new Action<bool>(this.HandleCrowdState)),
        WorldState.CrowdEnabled.Link<bool>(new Action<bool>(this.HandleCrowdState)),
        PlayerAvatarHandler.BodyType.Link<PlayerAvatarHandler.EBodyType>(new Action<PlayerAvatarHandler.EBodyType>(this.HandleBodyType)),
        AppEvents.LoadAISimGameHub.Link(new System.Action(this.HandleLoadAISimGameHub)),
        NetworkState.Connected.Link<bool>(new Action<bool>(this.NetworkStateConnected))
      });
      PracticeTarget.OnTargetHit += new Action<int, bool, bool, PracticeTarget>(this.HandleTargetHit);
      HandsDataModel.OnBallCaught += new System.Action(this.HandleBallCaught);
      TouchButton.OnButtonClicked += new Action<uint>(this.HandleButtonClicked);
      AppState.AppMode.SetValue(GameManager.forceGameMode ? EAppMode.Game : (EAppMode) (Variable<EAppMode>) instance.GameMode);
      FootballVR.Startup.InitializationComplete.Link(new System.Action(this.HandleInitializationComplete));
      this._playbackInfo.pauseBeforeHike = true;
      this._ballTrailStore.SetPoolTransform(this._ballTrailPool);
      this._ballTrailPool.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this._ballMatStore != (UnityEngine.Object) null))
        return;
      this._ballMatStore.selectedTeamBall = (int) SaveManager.GetPlayerProfile().Customization.MultiplayerTeamBallId;
    }

    private void LoadSettings()
    {
      if (!PersistentSingleton<SaveManager>.Exist())
        return;
      ScriptableSingleton<SettingsStore>.Instance.PrepareToLoad();
      PersistentSingleton<SaveManager>.Instance.AddToLoadQueue((ISaveSync) ScriptableSingleton<SettingsStore>.Instance.saveSettingsStore);
      ScriptableSingleton<VRSettings>.Instance.PrepareToLoad();
      PersistentSingleton<SaveManager>.Instance.AddToLoadQueue((ISaveSync) ScriptableSingleton<VRSettings>.Instance.saveVRSettings);
      ScriptableSingleton<GameSettings>.Instance.PrepareToLoad();
      PersistentSingleton<SaveManager>.Instance.AddToLoadQueue((ISaveSync) ScriptableSingleton<GameSettings>.Instance.saveOldGameSettings);
      ScriptableSingleton<AvatarsSettings>.Instance.PrepareToLoad();
      PersistentSingleton<SaveManager>.Instance.AddToLoadQueue((ISaveSync) ScriptableSingleton<AvatarsSettings>.Instance.saveAvatarSettings);
    }

    public void TurnOnImmersiveTackleInMiniCamp()
    {
    }

    public void TurnOffImmersiveTackleInMiniCamp() => ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.SetValue(false);

    private void HandleInitializationComplete() => this._routineHandle.Run(this.LoadGame());

    public IEnumerator LoadGame()
    {
      this.ResetCollisionState();
      if ((bool) (UnityEngine.Object) this._pauseScreenPrefab)
      {
        this._pauseScreen = UnityEngine.Object.Instantiate<PauseScreen>(this._pauseScreenPrefab, UIAnchoring.PauseMenuCanvas.transform);
        this._pauseScreen.Initialize();
        VRState.PauseMenu.OnValueChanged += new Action<bool>(this._pauseScreen.HandlePauseState);
      }
      if ((bool) (UnityEngine.Object) this._errorScreenPrefab)
      {
        this._errorScreen = UnityEngine.Object.Instantiate<ErrorScreen>(this._errorScreenPrefab, UIAnchoring.PauseMenuCanvas.transform);
        this._errorScreen.Initialize();
        VRState.ErrorOccurred.OnValueChanged += new Action<bool>(this._errorScreen.HandleErrorState);
      }
      GameManager.GameLoadComplete.Trigger();
      this.gameLoadComplete = true;
      this._routineHandle.Run(this.GoToDestination((DestinationOptions) StartupState.CurrentStartupDestination));
      yield return (object) null;
    }

    private async void Start()
    {
    }

    private void OnDestroy()
    {
      ScriptableSingleton<SettingsStore>.Instance.Deinitialize();
      this._linksHandler.Clear();
      PracticeTarget.OnTargetHit -= new Action<int, bool, bool, PracticeTarget>(this.HandleTargetHit);
      HandsDataModel.OnBallCaught -= new System.Action(this.HandleBallCaught);
      TouchButton.OnButtonClicked -= new Action<uint>(this.HandleButtonClicked);
      VRState.PauseMenu.OnValueChanged -= new Action<bool>(this._pauseScreen.HandlePauseState);
      VRState.ErrorOccurred.OnValueChanged -= new Action<bool>(this._errorScreen.HandleErrorState);
      FootballVR.Startup.InitializationComplete.Unlink(new System.Action(AppEvents.LoadMainMenu.Trigger));
      this._routineHandle.Stop();
      this._routineStadiumHandle.Stop();
    }

    private void HandleButtonClicked(uint soundID) => AppSounds.PlaySfx((ESfxTypes) soundID);

    private void HandleStateTransition(bool inTransition)
    {
      if (inTransition)
      {
        VRState.PauseMenu.SetValue(false);
        VRState.PausePermission = false;
        AppEvents.Retry.enabled = false;
      }
      else
      {
        GameState activeState = this._stateManager.activeState;
        bool flag = (UnityEngine.Object) activeState != (UnityEngine.Object) null;
        VRState.PausePermission = flag && activeState.allowPause;
        AppEvents.Retry.enabled = flag && activeState.allowRetry;
      }
    }

    public bool IsTransitioning() => (bool) (UnityEngine.Object) this._stateManager && (bool) this._stateManager.InTransition;

    private void HandleBallCaught() => AppSounds.PlaySfx(ESfxTypes.kCatchBall);

    private void HandleTargetHit(
      int soundId,
      bool userThrown,
      bool nearMiss,
      PracticeTarget target)
    {
      GameplayStore gameplayStore = (EMode) AppState.Mode == EMode.kMultiplayer ? this._multiplayerStore : this._gameplayStore;
      this.StartCoroutine(this.PlaySoundsRoutine(soundId, target.transform));
      if (!target.IsActiveForPoints)
        return;
      int num1 = (int) ((double) target.HitScore * (double) AppState.Difficulty.ThrowPointsMultiplier);
      if (!((UnityEngine.Object) gameplayStore != (UnityEngine.Object) null))
        return;
      if (nearMiss)
        num1 /= 2;
      if (!userThrown)
      {
        gameplayStore.HandleOpponentHit();
      }
      else
      {
        if (gameplayStore.Locked)
          return;
        if ((UnityEngine.Object) target.ScoreText != (UnityEngine.Object) null)
          target.ScoreText.Display(string.Format("+{0}pts", (object) num1), 1.5f);
        if (AppState.GameMode == EGameMode.kMiniCampPrecisionPassing)
          MiniGameScoreState.ComboModifier = 1;
        else
          ++gameplayStore.ComboModifier;
        int num2 = AppState.IsInMiniCamp() ? MiniGameScoreState.ComboModifier : gameplayStore.ComboModifier;
        int num3 = num1 * num2;
        if (num2 > 1)
          target.ComboText.Display("x" + num2.ToString(), 1.5f);
        if (AppState.IsInMiniCamp())
          MiniGameScoreState.AccumulateScore(num3);
        else
          gameplayStore.AccumulateScore(num3);
      }
    }

    private IEnumerator PlaySoundsRoutine(int soundId, Transform target)
    {
      AppSounds.Play3DSfx(ESfxTypes.kTargetHit, target);
      yield return (object) new WaitForSeconds(0.4f);
      AppSounds.PlayPracticeSound((EPracticeSoundType) soundId, target);
    }

    private void HandleCrowdState(bool crowdEnabled)
    {
      bool flag = (bool) WorldState.CrowdEnabled && (bool) this._graphicsSettings.CrowdEnabled;
      AppSounds.AmbienceSound.SetValue(flag && (bool) AppSounds.CrowdSound);
    }

    public void ResetStadium(System.Action onFinished = null, float delay = 0.0f) => this._routineStadiumHandle.Run(this.ResetStadiumRoutine(onFinished, delay));

    public IEnumerator ResetStadiumRoutine(System.Action onFinished = null, float delay = 0.0f)
    {
      GameManager gameManager = this;
      Debug.Log((object) nameof (ResetStadiumRoutine));
      if ((double) delay > 0.0)
        yield return (object) new WaitForSeconds(delay);
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
      bool isExhibitionGame = AppState.SeasonMode.Value == ESeasonMode.kUnknown;
      string stadiumId = !SeasonModeManager.self.IsFourthRoundOfNFLPlayoffs() || isExhibitionGame ? Enum.GetName(typeof (ETeamUniformId), (object) SingletonBehaviour<PersistentData, MonoBehaviour>.instance.HomeTeamUniform.Value) : Enum.GetName(typeof (ETeamUniformId), (object) ETeamUniformId.Cardinals);
      if (AppState.GameMode == EGameMode.kOnboarding)
        stadiumId = "Practice";
      Debug.Log((object) ("try to load stadium " + stadiumId));
      gameManager.m_stadiumConfigStore = (StadiumConfigStore) null;
      gameManager._stateManager.GetStadiumObject(stadiumId, WorldState.TimeOfDay.Value, new Action<StadiumConfigStore>(gameManager.LoadedStadium));
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(gameManager.\u003CResetStadiumRoutine\u003Eb__46_0));
      SeasonModeManager self = SeasonModeManager.self;
      if (!isExhibitionGame)
      {
        if (self.IsFirstRoundOfPlayoffs())
          gameManager.m_stadiumConfigStore.gameRound = new StadiumController.SeasonModeGameRound?(StadiumController.SeasonModeGameRound.WildCard);
        else if (self.IsSecondRoundOfPlayoffs())
          gameManager.m_stadiumConfigStore.gameRound = new StadiumController.SeasonModeGameRound?(StadiumController.SeasonModeGameRound.DivisionalRound);
        else if (self.IsThirdRoundOfPlayoffs())
          gameManager.m_stadiumConfigStore.gameRound = new StadiumController.SeasonModeGameRound?(StadiumController.SeasonModeGameRound.ConferenceChampionship);
        else if (self.IsFourthRoundOfNFLPlayoffs())
          gameManager.m_stadiumConfigStore.gameRound = new StadiumController.SeasonModeGameRound?(StadiumController.SeasonModeGameRound.SuperBowl);
      }
      Debug.Log((object) ("Finished Loading " + stadiumId));
      yield return (object) PersistentSingleton<LevelManager>.Instance.LoadEnvironment(gameManager.m_stadiumConfigStore.stadiumPrefabRef, gameManager._stateManager.GetTimeOfDayScene(gameManager.m_stadiumConfigStore.id));
      yield return (object) new WaitForEndOfFrame();
      if (onFinished != null)
      {
        System.Action action = onFinished;
        if (action != null)
          action();
      }
    }

    private void LoadedStadium(StadiumConfigStore newConfigStore)
    {
      StadiumController.SeasonModeGameRound seasonModeGameRound = StadiumController.SeasonModeGameRound.RegularSeason;
      if (AppState.SeasonMode.Value != ESeasonMode.kUnknown)
      {
        if (SeasonModeManager.self.IsFirstRoundOfPlayoffs())
          seasonModeGameRound = StadiumController.SeasonModeGameRound.WildCard;
        else if (SeasonModeManager.self.IsSecondRoundOfPlayoffs())
          seasonModeGameRound = StadiumController.SeasonModeGameRound.DivisionalRound;
        else if (SeasonModeManager.self.IsThirdRoundOfPlayoffs())
          seasonModeGameRound = StadiumController.SeasonModeGameRound.ConferenceChampionship;
        else if (SeasonModeManager.self.IsFourthRoundOfNFLPlayoffs())
          seasonModeGameRound = StadiumController.SeasonModeGameRound.SuperBowl;
      }
      newConfigStore.gameRound = new StadiumController.SeasonModeGameRound?(seasonModeGameRound);
      this.m_stadiumConfigStore = newConfigStore;
    }

    private void HandlePlayerDown(bool isDown)
    {
      if (isDown)
        UIDispatch.FrontScreen.DisplayView(EScreens.kGetUp);
      else
        UIDispatch.FrontScreen.HideView(EScreens.kGetUp);
    }

    private void HandleLoadMainMenu()
    {
      StartupState.CurrentStartupDestination.SetValue(new DestinationOptions()
      {
        ApiName = DestinationDefinitions.GetDestinationApiName(DestinationDefinitions.Destination.Core_LockerRoom, StartupState.PlatformInUse.Value),
        AppState = EAppState.kMainMenuActivation,
        Mode = EMode.kSolo,
        TimeOfDay = ETimeOfDay.Clear
      });
      this._routineHandle.Run(this.GoToDestination((DestinationOptions) StartupState.CurrentStartupDestination));
    }

    private IEnumerator GoToDestination(DestinationOptions destination)
    {
      GameState activeState = this._stateManager.activeState;
      bool? nullable1 = new bool?((bool) SaveManager.GetPlayerProfile()?.Customization?.IsNewCustomization);
      if (PhotonNetwork.IsConnectedAndReady)
        MultiplayerManager.LeaveRoom();
      if (destination.Mode == EMode.kMultiplayer)
      {
        if (nullable1.HasValue)
        {
          bool? nullable2 = nullable1;
          bool flag = true;
          if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue))
            goto label_14;
        }
        Debug.Log((object) "User tried to join a MP game before creating a character");
        VRState.ErrorOccurred.SetValue(true);
        VRState.HandsVisible.SetValue(true);
        if ((UnityEngine.Object) PersistentSingleton<StateManager<EAppState, GameState>>.Instance != (UnityEngine.Object) null && (UnityEngine.Object) PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState != (UnityEngine.Object) null)
        {
          switch (PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState.Id)
          {
            case EAppState.kMainMenuActivation:
              yield break;
            case EAppState.kOnboarding:
              yield break;
            case EAppState.kOnboardingOptions:
              yield break;
            case EAppState.kTunnel:
              yield break;
            case EAppState.kHeroMoment:
              yield break;
            default:
              destination = this.SetOnboardingDestination(destination);
              break;
          }
        }
        else
          destination = this.SetOnboardingDestination(destination);
      }
label_14:
      if (destination.Mode == EMode.kMultiplayer)
      {
        NetworkState.requestRoomInfo = new RequestRoomInfo();
        Debug.Log((object) "Preparing to send Player to Multiplayer Destination...");
        this.flagDestinationMultiplayer = true;
        if ((UnityEngine.Object) NetworkState.InstantiatedMultiplayerManager == (UnityEngine.Object) null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<MultiplayerManager>(this.multiplayerManagerPrefab).gameObject;
          UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) gameObject);
          Debug.Log((object) "Created a new Multiplayer Manager");
          NetworkState.InstantiatedMultiplayerManager = gameObject.GetComponent<MultiplayerManager>();
        }
        NetworkState.InstantiatedMultiplayerManager.ConnectToNetwork();
        NetworkState.requestRoomInfo.RoomName = destination.LobbySessionID;
        NetworkState.requestRoomInfo.MaxPlayersAmount = 8;
        NetworkState.requestRoomInfo.GameTypeID = DestinationDefinitions.GetMultiplayerID(destination.ApiName);
        NetworkState.requestRoomInfo.Password = destination.Password;
        NetworkState.requestRoomInfo.StadiumName = destination.StadiumName;
        NetworkState.requestRoomInfo.TimeOfDay = destination.TimeOfDay;
        this.multiplayerDestinationToLoad = destination;
        while (!PhotonNetwork.IsConnectedAndReady)
          yield return (object) null;
        Debug.Log((object) "Player is ready to go to Multiplayer Destination!");
        if ((bool) NetworkState.Connected)
        {
          this.flagDestinationMultiplayer = false;
          MultiplayerEvents.LoadMultiplayerGame.Trigger(this.multiplayerDestinationToLoad.AppState.ToString());
        }
      }
      else
        AppEvents.LoadState(destination.AppState, destination.TimeOfDay);
      VRState.BigSizeMode.SetValue(false);
      this.ResetCollisionState();
    }

    private void NetworkStateConnected(bool connected)
    {
      GameState activeState = this._stateManager.activeState;
      if (!connected || !this.flagDestinationMultiplayer)
        return;
      this.flagDestinationMultiplayer = false;
      MultiplayerEvents.LoadMultiplayerGame.Trigger(this.multiplayerDestinationToLoad.AppState.ToString());
    }

    private DestinationOptions SetOnboardingDestination(DestinationOptions destination)
    {
      destination.ApiName = DestinationDefinitions.GetDestinationApiName(DestinationDefinitions.Destination.Core_Onboarding, StartupState.PlatformInUse.Value);
      destination.AppState = EAppState.kOnboardingOptions;
      destination.Mode = EMode.kSolo;
      AppState.GameMode = EGameMode.kOnboarding;
      return destination;
    }

    private void HandleLoadAISimGameHub()
    {
      EAppState state = EAppState.kAISimGameInit;
      GameState activeState = this._stateManager.activeState;
      if ((UnityEngine.Object) activeState == (UnityEngine.Object) null || activeState.Id != state)
        AppEvents.LoadState(state);
      this.ResetCollisionState();
    }

    private void ResetCollisionState()
    {
      if (!((UnityEngine.Object) PersistentSingleton<GamePlayerController>.Instance.CollisionHandler != (UnityEngine.Object) null))
        return;
      PersistentSingleton<GamePlayerController>.Instance.CollisionHandler.ResetState();
    }

    public void ResetNetworkState() => this.flagDestinationMultiplayer = false;

    private void HandleUI(bool state) => UIDispatch.SetScreensVisible(state);

    public void ClientReloadMPScene() => this.HandleLoadMultiplayer(MultiplayerManager.GetMultiplayerAppStateByID(NetworkState.requestRoomInfo.GameTypeID, true));

    private void HandleLoadMultiplayer(string multiplayerToLoad)
    {
      UIDispatch.HideAll();
      Debug.Log((object) multiplayerToLoad);
      PersistentSingleton<BallsContainerManager>.Instance.Clear();
      AppState.Mode.Value = EMode.kMultiplayer;
      NetworkState.requestRoomInfo.PrintInfo();
      WorldState.TimeOfDay.SetValue(NetworkState.requestRoomInfo.TimeOfDay);
      AppEvents.LoadState((EAppState) Enum.Parse(typeof (EAppState), multiplayerToLoad), WorldState.TimeOfDay.Value);
    }

    private void HandleIntelAnimation()
    {
      UIDispatch.HideAll();
      AppEvents.LoadState(EAppState.kIntelAnimation, ETimeOfDay.Clear);
    }

    private void HandleBodyType(PlayerAvatarHandler.EBodyType bodyType)
    {
      if (bodyType == PlayerAvatarHandler.EBodyType.Unknown)
        return;
      PlayerAvatarHandler instance = PersistentSingleton<PlayerAvatarHandler>.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      PlayerAvatar currentAvatar = instance.CurrentAvatar;
      if ((UnityEngine.Object) currentAvatar == (UnityEngine.Object) null || bodyType != PlayerAvatarHandler.EBodyType.Multiplayer)
        return;
      instance.SetupUI(currentAvatar.LeftController, this._multiPlayerUIPrefab);
    }

    public void GoToDeeplinkDestination()
    {
      VRState.PauseMenu.SetValue(false);
      if (!this.gameLoadComplete)
        return;
      this._routineHandle.Run(this.GoToDestination((DestinationOptions) StartupState.CurrentStartupDestination));
    }
  }
}
