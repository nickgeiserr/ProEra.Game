// Decompiled with JetBrains decompiler
// Type: ProEra.Game.DevJewelCasePage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.StateManagement;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB12;
using TB12.AppStates;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProEra.Game
{
  public class DevJewelCasePage : TabletPage
  {
    [SerializeField]
    private GameStateManager _stateManager;
    [SerializeField]
    private TouchUI2DButton _btnRestartExhibition;
    [SerializeField]
    private TouchUI2DButton _btnToggleNoClip;
    [SerializeField]
    private TouchUI2DButton _btnPrevStadium;
    [SerializeField]
    private TouchUI2DButton _btnNextStadium;
    [SerializeField]
    private TouchUI2DButton _btnEnterDebugAnimationScene;
    [SerializeField]
    private GameObject _changeStadiumGroup;
    [Space(10f)]
    [SerializeField]
    private List<string> _stadiumIds = new List<string>()
    {
      "Ravens",
      "StatusPro",
      "SF49ers",
      "Bengals",
      "Broncos",
      "Buccaneers",
      "Cardinals",
      "Chiefs",
      "Colts",
      "Eagles",
      "Falcons",
      "Jaguars",
      "Jets",
      "Packers",
      "Panthers",
      "Patriots",
      "Redskins",
      "Seahawks",
      "Steelers",
      "Titans",
      "Cowboys",
      "Bills",
      "Bears",
      "Lions",
      "Raiders",
      "Texans",
      "Dolphins",
      "Practice"
    };
    [SerializeField]
    private string _debugJewelCaseSceneName = "DebugJewelCaseRoom";
    private GameObjectReference _currentStadium;
    private int _currentIndex;
    private bool _restartExhibition;
    private float _debounceTimer;
    private StadiumConfigStore m_stadiumConfigStore;

    private void Awake()
    {
      this._stateManager = UnityEngine.Object.FindObjectOfType<GameStateManager>();
      this._pageType = TabletPage.Pages.DevJewelCase;
    }

    private void OnEnable()
    {
      this._btnEnterDebugAnimationScene.onClick += new System.Action(this.HandleEnterDebugAnimationScene);
      this._btnRestartExhibition.onClick += new System.Action(this.HandleRestartExhibition);
      this._btnToggleNoClip.onClick += new System.Action(this.HandleToggleNoClip);
      this._btnPrevStadium.onClick += new System.Action(this.HandlePrevStadium);
      this._btnNextStadium.onClick += new System.Action(this.HandleNextStadium);
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
      SceneManager.sceneUnloaded += new UnityAction<Scene>(this.OnSceneUnLoaded);
      if (!(SceneManager.GetActiveScene().name != this._debugJewelCaseSceneName))
        return;
      this._changeStadiumGroup.SetActive(false);
    }

    private void OnDisable()
    {
      this._btnEnterDebugAnimationScene.onClick -= new System.Action(this.HandleEnterDebugAnimationScene);
      this._btnRestartExhibition.onClick -= new System.Action(this.HandleRestartExhibition);
      this._btnToggleNoClip.onClick -= new System.Action(this.HandleToggleNoClip);
      this._btnPrevStadium.onClick -= new System.Action(this.HandlePrevStadium);
      this._btnNextStadium.onClick -= new System.Action(this.HandleNextStadium);
      SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
      SceneManager.sceneUnloaded -= new UnityAction<Scene>(this.OnSceneUnLoaded);
    }

    private void LoadedStadium(StadiumConfigStore newConfigStore) => this.m_stadiumConfigStore = newConfigStore;

    private IEnumerator LoadStadiumRoutine(System.Action onFinished, float delay, string stadiumId)
    {
      DevJewelCasePage devJewelCasePage = this;
      if ((double) delay > 0.0)
        yield return (object) new WaitForSeconds(delay);
      Debug.Log((object) ("try to load stadium " + stadiumId));
      devJewelCasePage.m_stadiumConfigStore = (StadiumConfigStore) null;
      devJewelCasePage._stateManager.GetStadiumObject(stadiumId, ETimeOfDay.Clear, new Action<StadiumConfigStore>(devJewelCasePage.LoadedStadium));
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(devJewelCasePage.\u003CLoadStadiumRoutine\u003Eb__18_0));
      Debug.Log((object) ("Finished Loading " + stadiumId));
      yield return (object) devJewelCasePage.LoadStadiumAsync(devJewelCasePage.m_stadiumConfigStore.stadiumPrefabRef);
      devJewelCasePage._currentStadium.Value.transform.position = Vector3.zero;
      yield return (object) new WaitForEndOfFrame();
      System.Action action = onFinished;
      if (action != null)
        action();
    }

    private async Task LoadStadiumAsync(AssetReference assetReference)
    {
      GameObjectReference currentStadium = this._currentStadium;
      this._currentStadium.Value = await AddressablesData.instance.InstantiateAsync(assetReference, Vector3.zero, Quaternion.Euler(new Vector3(0.0f, 90f, 0.0f)), (Transform) null);
      this._currentStadium.AssetGUID = assetReference.AssetGUID;
    }

    private void HandleEnterDebugAnimationScene()
    {
      AppState.SeasonMode.Value = ESeasonMode.kUnknown;
      GameplayManager.LoadLevelActivation(EGameMode.kDebugAnimationSelection, ETimeOfDay.Clear);
    }

    private void HandleRestartExhibition()
    {
      if ((double) this._debounceTimer + 1.0 >= (double) Time.time)
        return;
      this._debounceTimer = Time.time;
      if (PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState.Id == EAppState.kAxisGame)
      {
        Debug.Log((object) "Exiting game");
        this._restartExhibition = true;
        AppEvents.LoadMainMenu.Trigger();
      }
      else
      {
        Debug.Log((object) "Reset game");
        AppState.SeasonMode.Value = ESeasonMode.kUnknown;
        GameplayManager.LoadLevelActivation(EGameMode.kAxisGame, ETimeOfDay.Clear);
      }
    }

    private void HandleToggleNoClip()
    {
      if ((double) this._debounceTimer + 1.0 >= (double) Time.time)
        return;
      this._debounceTimer = Time.time;
      PersistentSingleton<GamePlayerController>.Instance.IsNoClip = !PersistentSingleton<GamePlayerController>.Instance.IsNoClip;
      this._btnToggleNoClip.SetLabelText("No Clip: " + (PersistentSingleton<GamePlayerController>.Instance.IsNoClip ? "on" : "off"));
    }

    private void HandlePrevStadium()
    {
      if ((double) this._debounceTimer + 1.0 >= (double) Time.time)
        return;
      this._debounceTimer = Time.time;
      --this._currentIndex;
      if (this._currentIndex < 0)
        this._currentIndex = this._stadiumIds.Count - 1;
      this.RefreshStadium();
    }

    private void HandleNextStadium()
    {
      if ((double) this._debounceTimer + 1.0 >= (double) Time.time)
        return;
      this._debounceTimer = Time.time;
      ++this._currentIndex;
      if (this._currentIndex >= this._stadiumIds.Count)
        this._currentIndex = 0;
      this.RefreshStadium();
    }

    private void RefreshStadium()
    {
      if ((UnityEngine.Object) this._currentStadium.Value != (UnityEngine.Object) null)
      {
        AddressablesData.DestroyGameObject(this._currentStadium.Value);
        this._currentStadium = new GameObjectReference();
      }
      this.StartCoroutine(this.LoadStadiumRoutine((System.Action) (() => Debug.Log((object) ("Done Loading " + this._currentStadium.AssetGUID))), 1f, this._stadiumIds[this._currentIndex]));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
      if (this._restartExhibition)
      {
        this._restartExhibition = false;
        this.StartCoroutine(this.WaitForTransition(scene));
      }
      if (!(scene.name == this._debugJewelCaseSceneName))
        return;
      this._changeStadiumGroup.SetActive(true);
      this.RefreshStadium();
    }

    private void OnSceneUnLoaded(Scene scene)
    {
      if (!(scene.name == this._debugJewelCaseSceneName))
        return;
      if ((UnityEngine.Object) this._currentStadium.Value != (UnityEngine.Object) null)
      {
        AddressablesData.DestroyGameObject(this._currentStadium.Value);
        this._currentStadium = new GameObjectReference();
      }
      this._changeStadiumGroup.SetActive(false);
    }

    private IEnumerator WaitForTransition(Scene scene)
    {
      while (GameManager.Instance.IsTransitioning())
      {
        Debug.Log((object) "Waiting");
        yield return (object) new WaitForEndOfFrame();
      }
      Debug.Log((object) "Loading restarted exhibition");
      AppState.SeasonMode.Value = ESeasonMode.kUnknown;
      GameplayManager.LoadLevelActivation(EGameMode.kAxisGame, ETimeOfDay.Clear);
    }
  }
}
