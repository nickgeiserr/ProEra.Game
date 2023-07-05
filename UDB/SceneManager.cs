// Decompiled with JetBrains decompiler
// Type: UDB.SceneManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UDB
{
  [RequireComponent(typeof (SceneLoad))]
  [RequireComponent(typeof (SceneUnload))]
  public class SceneManager : SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>
  {
    private Coroutine sceneLoadRoutine;
    private Coroutine sceneSwitchRoutine;
    public bool sceneLoadRoutineActive;
    public bool scenesLoadRoutineActive;
    public bool sceneUnloadRoutineActive;
    public bool scenesUnloadRoutineActive;
    public bool sceneSwitchRoutineActive;
    private bool _isLoading;
    private Queue<string> scenesToUnload = new Queue<string>();
    private Queue<string> scenesToLoad = new Queue<string>();
    private List<string> scenesLoaded = new List<string>();
    private Scene _previousScene;
    [SerializeField]
    private LoadSceneMode _loadSceneMode = LoadSceneMode.Additive;
    private string sceneToLoad;
    private string sceneLoadedFrom;
    public bool useLoadingScene;
    public bool loadingSceneIsActive;
    public string loadingSceneName = "Loading";
    private Transform holder;
    private const string holderName = "SceneHolder";

    private bool canLoadNextScene => !this.sceneLoadRoutineActive;

    private bool canLoadScene => !this.scenesUnloadRoutineActive && !this.sceneUnloadRoutineActive;

    private bool canUnloadNextScene => !this.sceneUnloadRoutineActive;

    private bool canUnloadScene => !this.sceneLoadRoutineActive && !this.scenesLoadRoutineActive;

    private bool canSwitchScene => !this.sceneLoadRoutineActive && this.canUnloadNextScene && this.canLoadScene && this.canUnloadScene;

    private bool IsSceneLoaded(string sceneName) => UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).IsValid();

    private void PreSwitchScene(string sceneName)
    {
      this.isLoading = true;
      NotificationCenter<string>.Broadcast("sceneLoadBegin", sceneName);
    }

    private void PostSwitchScene(string sceneName)
    {
      this._ActivateScene(sceneName);
      this.isLoading = false;
      this.SetSceneSwitchRoutineToNull();
      NotificationCenter<string>.Broadcast("sceneChanged", sceneName);
    }

    private void SetScenesUnloadRoutineToNull() => this.scenesUnloadRoutineActive = false;

    private void StartScenesUnloadRoutine(IEnumerator routine)
    {
      this.scenesUnloadRoutineActive = true;
      this.StartCoroutine(routine);
    }

    private void SetSceneUnloadRoutineToNull() => this.sceneUnloadRoutineActive = false;

    private void StartSceneUnloadRoutine(IEnumerator routine)
    {
      this.sceneUnloadRoutineActive = true;
      this.StartCoroutine(routine);
    }

    private void SetScenesLoadRoutineToNull() => this.scenesLoadRoutineActive = false;

    private void StartScenesLoadRoutine()
    {
      this.scenesLoadRoutineActive = true;
      this.StartCoroutine(this.DoLoadScenes());
    }

    private void SetSceneLoadRoutineToNull()
    {
      this.sceneLoadRoutineActive = false;
      this.sceneLoadRoutine = (Coroutine) null;
    }

    private void StartSceneLoadRoutine(IEnumerator routine)
    {
      this.sceneLoadRoutineActive = true;
      this.sceneLoadRoutine = this.StartCoroutine(routine);
    }

    private void StopSceneLoadRoutine()
    {
      this.StopCoroutine(this.sceneLoadRoutine);
      this.SetSceneLoadRoutineToNull();
    }

    private void SetSceneSwitchRoutineToNull()
    {
      this.sceneSwitchRoutineActive = false;
      this.sceneSwitchRoutine = (Coroutine) null;
    }

    private void StartSceneSwitchRoutine(IEnumerator routine)
    {
      this.sceneSwitchRoutineActive = true;
      this.sceneSwitchRoutine = this.StartCoroutine(routine);
    }

    private void StopSceneSwitchRoutine()
    {
      this.StopCoroutine(this.sceneSwitchRoutine);
      this.SetSceneSwitchRoutineToNull();
    }

    private IEnumerator DoSwitchScene(string sceneName)
    {
      SceneManager sceneManager = this;
      if (DebugManager.StateForKey("SceneManager Actions"))
        Debug.Log((object) (((object) sceneManager).GetType().Name + " at DoSwitchScene " + sceneName));
      sceneManager.PreSwitchScene(sceneName);
      while (SceneTransitionPlayer.PlayOutSceneTransition())
        yield return (object) null;
      while (!sceneManager.canSwitchScene)
        yield return (object) null;
      if (sceneManager.loadSceneMode == LoadSceneMode.Single)
      {
        SceneUnload.UnloadScene(sceneManager.currentScene.name);
        while (SceneUnload.IsUnloadingScene())
          yield return (object) null;
      }
      if (!sceneManager.IsSceneLoaded(sceneName))
      {
        AsyncOperation sync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, sceneManager.loadSceneMode);
        if (sync == null)
        {
          sceneManager.isLoading = false;
          yield break;
        }
        else
        {
          while (!sync.isDone)
            yield return (object) null;
          sync = (AsyncOperation) null;
        }
      }
      else
        yield return (object) null;
      sceneManager.PostSwitchScene(sceneName);
    }

    private IEnumerator DoLoadScenes()
    {
      SceneManager sceneManager = this;
      if (DebugManager.StateForKey("SceneManager Actions"))
        Debug.Log((object) (((object) sceneManager).GetType().Name + " at DoLoadScenes"));
      while (sceneManager.scenesToLoad.Count > 0)
      {
        while (!sceneManager.canLoadNextScene)
          yield return (object) null;
        sceneManager.StartSceneLoadRoutine(sceneManager.DoLoadScene(sceneManager.scenesToLoad.Dequeue()));
      }
      sceneManager.SetScenesLoadRoutineToNull();
    }

    private IEnumerator DoUnloadLoadedScenes()
    {
      SceneManager sceneManager = this;
      if (DebugManager.StateForKey("SceneManager Actions"))
        Debug.Log((object) (((object) sceneManager).GetType().Name + " at DoUnloadLoadedScenes"));
      while (sceneManager.scenesToUnload.Count > 0)
      {
        while (sceneManager.sceneUnloadRoutineActive)
          yield return (object) null;
        sceneManager.StartSceneUnloadRoutine(sceneManager.DoUnloadScene(sceneManager.scenesToUnload.Dequeue()));
      }
      sceneManager.SetScenesUnloadRoutineToNull();
    }

    private IEnumerator DoLoadScene(string sceneName)
    {
      SceneManager sceneManager = this;
      if (DebugManager.StateForKey("SceneManager Actions"))
        Debug.Log((object) (((object) sceneManager).GetType().Name + " at DoLoadScene " + sceneName));
      while (!sceneManager.canLoadScene)
        yield return (object) null;
      SceneLoad.LoadScene(sceneName);
      while (SceneLoad.IsLoadingScene())
        yield return (object) null;
      sceneManager._AddToLoadedScenes(sceneName);
      sceneManager.SetSceneLoadRoutineToNull();
      NotificationCenter<string>.Broadcast("sceneLoaded", sceneName);
    }

    private IEnumerator DoUnloadScene(string sceneNameToUnload)
    {
      SceneManager sceneManager = this;
      if (DebugManager.StateForKey("SceneManager Actions"))
        Debug.Log((object) (((object) sceneManager).GetType().Name + " at DoUnloadScene " + sceneNameToUnload));
      SceneUnload.UnloadScene(sceneNameToUnload);
      if (SceneUnload.IsUnloadingScene())
        yield return (object) null;
      sceneManager.SetSceneUnloadRoutineToNull();
    }

    public bool isLoading
    {
      get => this._isLoading;
      private set
      {
        if (DebugManager.StateForKey("SceneManager Messages"))
          Debug.Log((object) (((object) this).GetType().Name + " isLoading = " + value.ToString()));
        this._isLoading = value;
      }
    }

    public Scene currentScene => UnityEngine.SceneManagement.SceneManager.GetActiveScene();

    public Scene previousScene
    {
      get => this._previousScene;
      private set => this._previousScene = value;
    }

    public LoadSceneMode loadSceneMode
    {
      get => this._loadSceneMode;
      set
      {
        if (this.isLoading)
        {
          if (!DebugManager.StateForKey("SceneManager Errors"))
            return;
          Debug.LogError((object) "Current scene is still loading, can't set mode.");
        }
        else
          this._loadSceneMode = value;
      }
    }

    private void _AddToLoadedScenes(string loadedSceneName)
    {
      if (this.scenesLoaded.Contains(loadedSceneName))
        return;
      this.scenesLoaded.Add(loadedSceneName);
    }

    private void ClearLoadedSceneData()
    {
      if (DebugManager.StateForKey("SceneManager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      this.scenesLoaded.Clear();
      this.scenesToLoad.Clear();
      if (!this.sceneLoadRoutineActive)
        return;
      this.StopSceneLoadRoutine();
    }

    private void LoadSceneAction(string sceneName)
    {
      if (DebugManager.StateForKey("SceneManager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneName));
      this.scenesToLoad.Enqueue(sceneName);
      if (this.scenesLoadRoutineActive)
        return;
      this.StartScenesLoadRoutine();
    }

    private void SwtichToSceneAction(string sceneName)
    {
      if (DebugManager.StateForKey("SceneManager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneName));
      this.SwtichToAppropiateScene(sceneName);
    }

    private void SwtichToLoadedSceneAction(string sceneName)
    {
      if (!this.scenesLoaded.Contains(sceneName))
        return;
      if (DebugManager.StateForKey("SceneManager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneName));
      this.SwtichToAppropiateScene(sceneName);
    }

    private void SwtichToAppropiateScene(string sceneName)
    {
      if (DebugManager.StateForKey("SceneManager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneName));
      if (this.loadLoadingScene)
      {
        this.LoadLoadingScene(sceneName);
      }
      else
      {
        if (this.sceneSwitchRoutineActive)
          return;
        this.StartSceneSwitchRoutine(this.DoSwitchScene(sceneName));
      }
    }

    private void UnloadSceneAction(string sceneName)
    {
      if (DebugManager.StateForKey("SceneManager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneName));
      this.scenesToUnload.Enqueue(sceneName);
      if (this.scenesUnloadRoutineActive)
        return;
      this.StartScenesUnloadRoutine(this.DoUnloadLoadedScenes());
    }

    private void UnloadLoadedSceneAction(string sceneName)
    {
      if (DebugManager.StateForKey("SceneManager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneName));
      if (this.scenesLoaded.Contains(sceneName))
        this.scenesLoaded.Remove(sceneName);
      if (this.scenesToLoad.Contains(sceneName))
      {
        Queue<string> stringQueue = new Queue<string>();
        while (this.scenesToLoad.Count > 0)
        {
          string str = this.scenesToLoad.Dequeue();
          if (str != sceneName)
            stringQueue.Enqueue(str);
        }
        this.scenesToLoad = stringQueue;
      }
      this.scenesToUnload.Enqueue(sceneName);
      if (this.scenesUnloadRoutineActive)
        return;
      this.StartScenesUnloadRoutine(this.DoUnloadLoadedScenes());
    }

    private void UnloadLoadedScenesAction()
    {
      for (int index = 0; index < this.scenesLoaded.Count; ++index)
        this.scenesToUnload.Enqueue(this.scenesLoaded[index]);
      this.ClearLoadedSceneData();
      if (this.scenesUnloadRoutineActive)
        return;
      this.StartScenesUnloadRoutine(this.DoUnloadLoadedScenes());
    }

    protected void SceneAction(string sceneName, SceneManager.SceneActionType sceneAction)
    {
      string str = ((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " " + sceneAction.ToString();
      if (this.isLoading && sceneAction != SceneManager.SceneActionType.UnloadAllLoaded)
      {
        Debug.LogError((object) (str + " : Current scene is still loading, can't load: " + sceneName));
      }
      else
      {
        switch (sceneAction)
        {
          case SceneManager.SceneActionType.Load:
            this.LoadSceneAction(sceneName);
            break;
          case SceneManager.SceneActionType.SwitchTo:
            this.SwtichToSceneAction(sceneName);
            break;
          case SceneManager.SceneActionType.SwitchToLoaded:
            this.SwtichToLoadedSceneAction(sceneName);
            break;
          case SceneManager.SceneActionType.Unload:
            this.UnloadSceneAction(sceneName);
            break;
          case SceneManager.SceneActionType.UnloadLoaded:
            this.UnloadLoadedSceneAction(sceneName);
            break;
          case SceneManager.SceneActionType.UnloadAllLoaded:
            this.UnloadLoadedScenesAction();
            break;
        }
      }
    }

    protected void _Reload()
    {
      if (DebugManager.StateForKey("SceneManager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      Scene currentScene = this.currentScene;
      if (string.IsNullOrEmpty(currentScene.name))
        return;
      currentScene = this.currentScene;
      SceneManager.LoadScene(currentScene.name);
    }

    protected bool _ActivateScene(string sceneName)
    {
      Debug.Log((object) "D4: Not activating axis scene..");
      return false;
    }

    private bool loadLoadingScene => this.useLoadingScene && !this.loadingSceneIsActive;

    private void LoadLoadingScene(string sceneName)
    {
      if (DebugManager.StateForKey("SceneManager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneName));
      this.sceneToLoad = sceneName;
      this.sceneLoadedFrom = SceneManager.activeSceneName;
      this.StartCoroutine(this.DoLoadLoadingScene());
    }

    private void _LoadingSceneUnloaded() => this.loadingSceneIsActive = false;

    private IEnumerator DoLoadLoadingScene()
    {
      SceneManager sceneManager = this;
      if (DebugManager.StateForKey("SceneManager Actions"))
        Debug.Log((object) (((object) sceneManager).GetType().Name + " at DoLoadLoadingScene"));
      if (!sceneManager.sceneSwitchRoutineActive)
        sceneManager.StartSceneSwitchRoutine(sceneManager.DoSwitchScene(sceneManager.loadingSceneName));
      while (sceneManager.sceneSwitchRoutineActive)
        yield return (object) null;
      sceneManager.loadingSceneIsActive = sceneManager.loadLoadingScene;
      if (sceneManager.loadingSceneIsActive)
        NotificationCenter<GenericParams>.Broadcast("sceneToLoad", new GenericParams(new GenericParamArg[2]
        {
          new GenericParamArg("sceneSwitchedFrom", (object) sceneManager.sceneLoadedFrom),
          new GenericParamArg("sceneToLoad", (object) sceneManager.sceneToLoad)
        }));
    }

    private void GenerateHolder()
    {
      if (!((Object) this.holder == (Object) null))
        return;
      GameObject target = GameObject.Find("SceneHolder");
      if ((Object) target == (Object) null)
        target = new GameObject("SceneHolder");
      Object.DontDestroyOnLoad((Object) target);
      this.holder = target.transform;
    }

    public void StoreAddObject(Transform trans)
    {
      this.GenerateHolder();
      trans.parent = this.holder;
      trans.gameObject.SetActive(false);
    }

    public Transform StoreGetObject(string name)
    {
      this.GenerateHolder();
      return this.holder.Find(name);
    }

    public void StoreDestroyObject(string name)
    {
      Transform transform = this.StoreGetObject(name);
      if (!(bool) (Object) transform)
        return;
      Object.Destroy((Object) transform.gameObject);
    }

    public static bool usingLoadingScene => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.useLoadingScene;

    public static string activeSceneName => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

    public static void StartUseLoadingScene() => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.useLoadingScene = true;

    public static void StopUseLoadingScene() => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.useLoadingScene = false;

    public static void SwitchToScene(string sceneName) => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.SceneAction(sceneName, SceneManager.SceneActionType.SwitchTo);

    public static void SwitchToLoadedScene(string sceneName) => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.SceneAction(sceneName, SceneManager.SceneActionType.SwitchToLoaded);

    public static void Reload() => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance._Reload();

    public static bool AcitvateScene(string sceneName) => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance._ActivateScene(sceneName);

    public static void LoadScene(string sceneName) => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.SceneAction(sceneName, SceneManager.SceneActionType.Load);

    public static void UnloadScene(string sceneName) => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.SceneAction(sceneName, SceneManager.SceneActionType.Unload);

    public static void UnloadLoadedScene(string sceneName) => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.SceneAction(sceneName, SceneManager.SceneActionType.UnloadLoaded);

    public static void UnloadLoadedScenes() => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.SceneAction(string.Empty, SceneManager.SceneActionType.UnloadAllLoaded);

    public static void AddToLoadedScenes(string loadedSceneName) => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance._AddToLoadedScenes(loadedSceneName);

    public static void LoadingSceneUnloaded() => SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance._LoadingSceneUnloaded();

    protected enum SceneActionType
    {
      Load,
      SwitchTo,
      SwitchToLoaded,
      Unload,
      UnloadLoaded,
      UnloadAllLoaded,
    }
  }
}
