// Decompiled with JetBrains decompiler
// Type: UDB.LoadingScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class LoadingScene : SingletonBehaviour<LoadingScene, MonoBehaviour>
  {
    private string sceneToLoad;
    private string sceneSwitchedFrom;
    private string originalSceneName;
    [SerializeField]
    private AnimationController loadingAnimationConroller;

    public static string sceneName => SingletonBehaviour<LoadingScene, MonoBehaviour>.instance.originalSceneName;

    private new void Awake()
    {
      this.originalSceneName = this.gameObject.scene.name;
      NotificationCenter<GenericParams>.AddListener("sceneToLoad", new Callback<GenericParams>(this.SceneToLoadCallback));
      NotificationCenter<string>.AddListener("sceneUnloaded", new Callback<string>(this.SceneUnloadedCallback));
      NotificationCenter<string>.AddListener("sceneLoaded", new Callback<string>(this.SceneLoadedCallback));
      NotificationCenter<string>.AddListener("sceneChanged", new Callback<string>(this.SceneChangedCallback));
    }

    private void OnDestroy()
    {
      NotificationCenter<GenericParams>.RemoveListener("sceneToLoad", new Callback<GenericParams>(this.SceneToLoadCallback));
      NotificationCenter<string>.RemoveListener("sceneUnloaded", new Callback<string>(this.SceneUnloadedCallback));
      NotificationCenter<string>.RemoveListener("sceneLoaded", new Callback<string>(this.SceneLoadedCallback));
      NotificationCenter<string>.RemoveListener("sceneChanged", new Callback<string>(this.SceneChangedCallback));
    }

    private void SceneToLoadCallback(GenericParams parameters)
    {
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      this.sceneToLoad = parameters.GetValue<string>("sceneToLoad");
      this.sceneSwitchedFrom = parameters.GetValue<string>("sceneSwitchedFrom");
      SceneManager.UnloadScene(this.sceneSwitchedFrom);
    }

    private void SceneUnloadedCallback(string sceneUnloaded)
    {
      if (SceneRegistry.ValueForKey(sceneUnloaded) != SceneRegistry.ValueForKey(this.sceneSwitchedFrom))
        return;
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneUnloaded));
      NotificationCenter<string>.Broadcast("activateScene", this.gameObject.scene.name);
      Task.NewTask(this.DoPlayInTransition());
    }

    private void SceneLoadedCallback(string sceneLoaded)
    {
      if (SceneRegistry.ValueForKey(sceneLoaded) != SceneRegistry.ValueForKey(this.sceneToLoad))
        return;
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneLoaded));
      Object.DontDestroyOnLoad((Object) this.gameObject);
      SceneManager.SwitchToLoadedScene(this.sceneToLoad);
    }

    private void SceneChangedCallback(string newActiveScene)
    {
      if (SceneRegistry.ValueForKey(newActiveScene) != SceneRegistry.ValueForKey(this.sceneToLoad))
        return;
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + newActiveScene));
      SceneController.SetUpScene(SceneManager.activeSceneName);
    }

    public void _LoadedSceneSetUpComplete()
    {
      if (!this.CanSwitchScenes())
        return;
      this.ActivateLoadedScene();
    }

    protected void ActivateLoadedScene()
    {
      if ((Object) this.loadingAnimationConroller != (Object) null)
        this.loadingAnimationConroller.StopAnimating();
      Task.NewTask(this.DoPlayOutTransition());
    }

    public static void LoadedSceneSetUpComplete() => SingletonBehaviour<LoadingScene, MonoBehaviour>.instance._LoadedSceneSetUpComplete();

    private IEnumerator DoPlayInTransition()
    {
      LoadingScene loadingScene = this;
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) loadingScene).GetType().Name + " at DoPlayInTransition BEGIN"));
      while (SceneTransitionPlayer.PlayInSceneTransition())
        yield return (object) null;
      if (SceneTransitionPlayer.Exists())
        SceneTransitionPlayer.Destroy();
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) loadingScene).GetType().Name + " at DoPlayInTransition FINISH"));
      if ((Object) loadingScene.loadingAnimationConroller != (Object) null)
        loadingScene.loadingAnimationConroller.StartAnimating();
      SceneManager.LoadScene(loadingScene.sceneToLoad);
    }

    private IEnumerator DoPlayOutTransition()
    {
      LoadingScene loadingScene = this;
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) loadingScene).GetType().Name + " at DoPlayOutTransition BEGIN"));
      while (SceneTransitionPlayer.PlayOutLoadingTransition())
        yield return (object) null;
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) loadingScene).GetType().Name + " at DoPlayOutTransition FINISH"));
      SerializedSceneSingletonBehaviour<SceneController, SerializedMonoBehaviour>.InstanceOfScene(SceneManager.activeSceneName).ActivateScene();
    }

    public static void DestroyLoadingSceneIfExists()
    {
      if (!SingletonBehaviour<LoadingScene, MonoBehaviour>.Exists())
        return;
      Object.Destroy((Object) SingletonBehaviour<LoadingScene, MonoBehaviour>.instance.gameObject);
    }

    public static void ClaimAsLoadingAnimation(AnimationController loadingAnimationConroller) => SingletonBehaviour<LoadingScene, MonoBehaviour>.instance.loadingAnimationConroller = loadingAnimationConroller;

    protected virtual bool CanSwitchScenes() => true;
  }
}
