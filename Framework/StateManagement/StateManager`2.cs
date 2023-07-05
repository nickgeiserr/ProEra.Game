// Decompiled with JetBrains decompiler
// Type: Framework.StateManagement.StateManager`2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework.Data;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12;
using TB12.AppStates;
using UnityEngine;
using Vars;

namespace Framework.StateManagement
{
  public class StateManager<StateId, State> : PersistentSingleton<StateManager<StateId, State>>
    where StateId : Enum
    where State : BaseState<StateId>
  {
    [SerializeField]
    private List<State> _appStates;
    [SerializeField]
    private float _minLoadingTime = 0.5f;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private StadiumConfigsStore m_stadiumConfigStore;
    public Action<State> OnFinished;
    private const float DefaultFadeTime = 0.35f;
    private const float DefaultFadeDelay = 0.2f;
    private readonly RoutineHandle _transitionCoroutineHandle = new RoutineHandle();
    private bool _isWaitingToTransition;

    public State activeState { get; private set; }

    public VariableBool InTransition { get; } = new VariableBool();

    public event System.Action OnCameraFadeComplete;

    public void SetStadiumConfigsStoreUsedForLoading(StadiumConfigsStore newStore) => this.m_stadiumConfigStore = newStore;

    public void LoadState(StateId newState, ETimeOfDay timeOfDay, string sceneId)
    {
      if ((UnityEngine.Object) this.activeState != (UnityEngine.Object) null && object.Equals((object) this.activeState.Id, (object) newState))
      {
        Debug.LogWarning((object) "Trying to enter in same state!");
      }
      else
      {
        State state = this.GetState(newState);
        if ((UnityEngine.Object) state == (UnityEngine.Object) null)
          Debug.LogError((object) ("State " + newState?.ToString() + " not found."));
        else if ((UnityEngine.Object) this.activeState == (UnityEngine.Object) state)
        {
          Debug.LogWarning((object) "Trying to enter in same state! (ref check)");
        }
        else
        {
          Debug.Log((object) ("Changing state to " + newState?.ToString()));
          this._transitionCoroutineHandle.Stop();
          this._transitionCoroutineHandle.Run(this.TransitionRoutine(state, timeOfDay, sceneId));
        }
      }
    }

    private bool IsMultiplayerState(StateId stateId) => object.Equals((object) this.activeState.Id, (object) EAppState.kMultiplayerLobby) || object.Equals((object) this.activeState.Id, (object) EAppState.kMultiplayerBossModeGame) || object.Equals((object) this.activeState.Id, (object) EAppState.kMultiplayerDodgeball) || object.Equals((object) this.activeState.Id, (object) EAppState.kMultiplayerThrowGame);

    public static bool IsMultiplayerCurrentState()
    {
      State activeState = PersistentSingleton<StateManager<StateId, State>>.Instance.activeState;
      return object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerLobby) || object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerBossModeGame) || object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerDodgeball) || object.Equals((object) activeState.Id, (object) EAppState.kMultiplayerThrowGame);
    }

    private IEnumerator WaitingToTransition(State state, ETimeOfDay timeOfDay, string sceneId)
    {
      this._isWaitingToTransition = true;
      while ((bool) this.InTransition)
      {
        Console.WriteLine("StateManager - WaitingToTransition - Waiting...");
        yield return (object) new WaitForSeconds(0.2f);
      }
      yield return (object) GamePlayerController.CameraFade.Clear();
      yield return (object) new WaitForSeconds(0.5f);
      yield return (object) GamePlayerController.CameraFade.Fade(0.2f);
      Console.WriteLine("StateManager - WaitingToTransition - Transitioning...");
      this._isWaitingToTransition = false;
      this._transitionCoroutineHandle.Stop();
      this._transitionCoroutineHandle.Run(this.TransitionRoutine(state, timeOfDay, sceneId));
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      this._linksHandler.Clear();
    }

    private IEnumerator TransitionRoutine(State state, ETimeOfDay timeOfDay, string sceneId)
    {
      StateManager<StateId, State> stateManager = this;
      Debug.Log((object) ("TransitionRoutine state " + ((object) state)?.ToString() + " sceneID " + sceneId));
      stateManager.InTransition.SetValue(true);
      State previousState = stateManager.activeState;
      bool shouldLoadStadium = false;
      stateManager.activeState = state;
      stateManager.m_stadiumConfigStore = (StadiumConfigsStore) null;
      if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
        PhotonNetwork.IsMessageQueueRunning = false;
      bool mpTransition = object.Equals((object) stateManager.activeState.Id, (object) EAppState.kMultiplayerLobby) || object.Equals((object) stateManager.activeState.Id, (object) EAppState.kMultiplayerBossModeGame) || object.Equals((object) stateManager.activeState.Id, (object) EAppState.kMultiplayerDodgeball) || object.Equals((object) stateManager.activeState.Id, (object) EAppState.kMultiplayerThrowGame);
      if ((UnityEngine.Object) previousState != (UnityEngine.Object) null)
      {
        previousState.WillExit();
        int num = !((UnityEngine.Object) stateManager.activeState != (UnityEngine.Object) null) || !stateManager.activeState.showLoadingScreen ? 0 : (stateManager.activeState.showTransition ? 1 : 0);
        if (!PersistentSingleton<GamePlayerController>.Instance.IsInActive)
          yield return (object) GamePlayerController.CameraFade.Fade(0.8f, withLogo: true);
        if (object.Equals((object) previousState.Id, (object) EAppState.kAxisGame) && previousState.SaveSeasonDuringLoading)
        {
          AppEvents.SaveSeasonMode.Trigger();
          previousState.SaveSeasonDuringLoading = false;
        }
        try
        {
          previousState.Exit();
        }
        catch (Exception ex)
        {
          Debug.LogError((object) string.Format("Failed to exit {0}", (object) previousState.Id));
          Debug.LogException(ex);
        }
        if (!mpTransition && (bool) (UnityEngine.Object) stateManager.activeState != object.Equals((object) stateManager.activeState.Id, (object) EAppState.kHeroMoment) && (bool) (UnityEngine.Object) stateManager.activeState != object.Equals((object) stateManager.activeState.Id, (object) EAppState.kTunnel))
          AddressablesData.ReleaseAllObjects();
        yield return (object) new WaitForEndOfFrame();
        yield return (object) previousState.Unload();
        yield return (object) new WaitForSeconds(0.2f);
        if (VRState.debug)
          Debug.Log((object) ("Unloaded " + previousState.Id?.ToString()));
      }
      PersistentSingleton<GamePlayerController>.Instance.ResetPosition();
      yield return (object) stateManager.OnNewStateLoad();
      WorldState.TimeOfDay.SetValue(timeOfDay);
      if ((UnityEngine.Object) stateManager.activeState != (UnityEngine.Object) null)
      {
        WorldState.Raining.SetValue(stateManager.activeState.allowRain && (double) UnityEngine.Random.value < 0.20000000298023224);
        float startTime = Time.time;
        if (stateManager.activeState.showLoadingScreen && !(bool) TransitionScreenController.Transitioning)
        {
          TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.Loading, true);
          yield return (object) GamePlayerController.CameraFade.Clear();
        }
        SceneAssetString scene;
        if (stateManager.TryGetEnvironmentScene(sceneId, stateManager.activeState, out scene))
        {
          yield return (object) PersistentSingleton<LevelManager>.Instance.LoadEnvironment(scene);
        }
        else
        {
          if (stateManager.activeState.AlwaysUnloadEnvironment)
            yield return (object) PersistentSingleton<LevelManager>.Instance.UnloadCurrentEnvironment();
          else if (mpTransition && AppState.GameMode != EGameMode.kMultiplayer)
          {
            AppState.GameMode = EGameMode.kMultiplayer;
            PersistentSingleton<LevelManager>.Instance.UnloadCurrentStadium();
          }
          switch (AppState.GameMode)
          {
            case EGameMode.kAxisGame:
            case EGameMode.k2MD:
            case EGameMode.kHeroMoment:
              shouldLoadStadium = true;
              break;
          }
        }
        yield return (object) stateManager.activeState.Load();
        PhotonNetwork.IsMessageQueueRunning = true;
        try
        {
          stateManager.activeState.Enter();
          Debug.Log((object) ("Successfully entered state " + stateManager.activeState.Id?.ToString()));
        }
        catch (Exception ex)
        {
          Debug.LogError((object) ("Failed to enter " + stateManager.activeState.Id?.ToString() + ":"));
          Debug.LogException(ex);
          GameManager.Instance.ResetNetworkState();
          stateManager.InTransition.SetValue(false);
          AppEvents.LoadMainMenu.Trigger();
          yield break;
        }
        if ((UnityEngine.Object) stateManager.m_stadiumConfigStore != (UnityEngine.Object) null)
        {
          // ISSUE: reference to a compiler-generated method
          yield return (object) new WaitUntil(new Func<bool>(stateManager.\u003CTransitionRoutine\u003Eb__25_0));
          if ((UnityEngine.Object) stateManager.m_stadiumConfigStore.GetLoadedConfig() != (UnityEngine.Object) null)
            yield return (object) PersistentSingleton<LevelManager>.Instance.LoadEnvironment(stateManager.m_stadiumConfigStore.GetLoadedConfig().stadiumPrefabRef, stateManager.m_stadiumConfigStore.GetTimeOfDayScene(WorldState.TimeOfDay.Value));
        }
        if (shouldLoadStadium)
          yield return (object) UnityEngine.Object.FindObjectOfType<GameManager>().ResetStadiumRoutine();
        if (stateManager.activeState.showLoadingScreen)
        {
          if (!VRSettings.skipLoadingScreen)
          {
            while ((double) Time.time - (double) startTime < (double) stateManager._minLoadingTime)
              yield return (object) null;
          }
          yield return (object) GamePlayerController.CameraFade.Fade();
        }
        Action<State> onFinished = stateManager.OnFinished;
        if (onFinished != null)
          onFinished(stateManager.activeState);
        TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.Loading, false);
        stateManager.InTransition.SetValue(false);
        if (stateManager.activeState.clearFadeOnEntry)
        {
          yield return (object) new WaitForSeconds(0.5f);
          VRCameraFade.FadeSettings settings;
          if (stateManager.activeState.HasCameraFadeOverride(out settings))
            yield return (object) GamePlayerController.CameraFade.Clear(settings.Duration, settings.Delay, settings.FromBlack);
          else
            yield return (object) GamePlayerController.CameraFade.Clear(delay: 0.2f);
        }
      }
      else
      {
        stateManager.InTransition.SetValue(false);
        yield return (object) GamePlayerController.CameraFade.Clear();
      }
      if (!object.Equals((object) stateManager.activeState.Id, (object) EAppState.kHeroMoment) && !object.Equals((object) stateManager.activeState.Id, (object) EAppState.kTunnel) && !object.Equals((object) stateManager.activeState.Id, (object) EAppState.kPSVRSafety) && !object.Equals((object) stateManager.activeState.Id, (object) EAppState.kOnboardingOptions))
        TransitionScreenController.CheckForNetworkMessages();
      System.Action cameraFadeComplete = stateManager.OnCameraFadeComplete;
      if (cameraFadeComplete != null)
        cameraFadeComplete();
      yield return (object) null;
    }

    public virtual bool TryGetEnvironmentScene(
      string sceneId,
      State state,
      out SceneAssetString scene)
    {
      if (string.IsNullOrEmpty(sceneId))
      {
        ref SceneAssetString local = ref scene;
        SceneGroupBundle sceneBundle = state.SceneBundle;
        SceneAssetString sceneAssetString = sceneBundle != null ? sceneBundle.EnvironmentScene : new SceneAssetString();
        local = sceneAssetString;
      }
      else
        scene = new SceneAssetString();
      return scene.IsValid();
    }

    private State GetState(StateId id)
    {
      foreach (State appState in this._appStates)
      {
        if ((UnityEngine.Object) appState != (UnityEngine.Object) null && object.Equals((object) appState.Id, (object) id))
          return appState;
      }
      return default (State);
    }

    public void ForceReloadState()
    {
      PersistentSingleton<LevelManager>.Instance.ForceReloadLevel();
      this._transitionCoroutineHandle.Stop();
      this._transitionCoroutineHandle.Run(this.TransitionRoutine(this.activeState, ETimeOfDay.Night, (string) null));
    }

    protected virtual IEnumerator OnNewStateLoad()
    {
      yield return (object) new WaitForEndOfFrame();
    }
  }
}
