// Decompiled with JetBrains decompiler
// Type: UDB.SceneController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class SceneController : 
    SerializedSceneSingletonBehaviour<SceneController, SerializedMonoBehaviour>
  {
    public bool initialScene;
    public List<ISceneObject> sceneObjects = new List<ISceneObject>();
    [SerializeField]
    private ISceneSpecific sceneSpecific;
    private static object sceneControllerLockObject = new object();
    private string previousSceneName = "";

    public static void RegisterToScene(ISceneObject sceneObject, GameObject go) => SerializedSceneSingletonBehaviour<SceneController, SerializedMonoBehaviour>.InstanceOfScene(go.scene.name).RegisterToScene(sceneObject);

    public static void UnregisterFromScene(ISceneObject sceneObject, GameObject go) => SerializedSceneSingletonBehaviour<SceneController, SerializedMonoBehaviour>.InstanceOfScene(go.scene.name).UnregisterFromScene(sceneObject);

    public static void SetUpScene(string sceneName) => SerializedSceneSingletonBehaviour<SceneController, SerializedMonoBehaviour>.InstanceOfScene(sceneName).SetUpScene();

    public static void BreakDownScene(string sceneName) => SerializedSceneSingletonBehaviour<SceneController, SerializedMonoBehaviour>.InstanceOfScene(sceneName).BreakDownScene();

    private int sceneID => SceneRegistry.ValueForKey(this.sceneName);

    protected override void OnInstanceInit() => SceneRegistry.Register(this.gameObject.scene.name);

    private void SceneUnloadedCallback(string sceneUnloaded)
    {
      if (SceneRegistry.ValueForKey(sceneUnloaded) == this.sceneID && DebugManager.StateForKey("SceneController Callbacks"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if (SceneRegistry.ValueForKey(sceneUnloaded) != SceneRegistry.ValueForKey(this.previousSceneName))
        return;
      NotificationCenter<string>.Broadcast("activateScene", this.sceneName);
      this.StartCoroutine(this.DoPlayInTransition());
    }

    private void SceneLoadedCallback(string sceneLoaded)
    {
      if (SceneRegistry.ValueForKey(sceneLoaded) != this.sceneID || !DebugManager.StateForKey("SceneController Callbacks"))
        return;
      Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
    }

    private void SceneChangedCallback(string newActiveScene)
    {
      if (SceneRegistry.ValueForKey(newActiveScene) != this.sceneID)
        return;
      if (DebugManager.StateForKey("SceneController Callbacks"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      if (SceneManager.usingLoadingScene)
        return;
      this.SetUpScene();
    }

    private void SceneSetupCallback(string sceneSetupName)
    {
      if (SceneRegistry.ValueForKey(sceneSetupName) != this.sceneID)
        return;
      if (DebugManager.StateForKey("SceneController Callbacks"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneSetupName));
      if (SceneManager.usingLoadingScene)
        LoadingScene.LoadedSceneSetUpComplete();
      else
        this.ActivateScene();
    }

    private void UnloadPreviousScene()
    {
      this.previousSceneName = !SceneManager.usingLoadingScene ? SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.previousScene.name : LoadingScene.sceneName;
      SceneManager.UnloadScene(this.previousSceneName);
    }

    private void SceneStart()
    {
      if (this.sceneSpecific != null)
        this.sceneSpecific.StartScene();
      lock (SceneController.sceneControllerLockObject)
      {
        foreach (ISceneObject sceneObject in this.sceneObjects)
          sceneObject.OnSceneStart();
        this.OnSceneStart();
      }
      SceneManager.LoadingSceneUnloaded();
      SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.useLoadingScene = false;
    }

    private void SceneEnd()
    {
      if (this.sceneSpecific != null)
        this.sceneSpecific.EndScene();
      lock (SceneController.sceneControllerLockObject)
      {
        foreach (ISceneObject sceneObject in this.sceneObjects)
          sceneObject.OnSceneStart();
        this.OnSceneEnd();
      }
    }

    protected void BaseAwake() => this.InstanciateIfNeeded();

    protected void BaseOnDestroy() => this.InstanceDeinit();

    protected void BaseOnEnable()
    {
      NotificationCenter<string>.AddListener("sceneUnloaded", new Callback<string>(this.SceneUnloadedCallback));
      NotificationCenter<string>.AddListener("sceneLoaded", new Callback<string>(this.SceneLoadedCallback));
      NotificationCenter<string>.AddListener("sceneChanged", new Callback<string>(this.SceneChangedCallback));
      NotificationCenter<string>.AddListener("sceneSetup", new Callback<string>(this.SceneSetupCallback));
    }

    protected void BaseOnDisable()
    {
      NotificationCenter<string>.RemoveListener("sceneUnloaded", new Callback<string>(this.SceneUnloadedCallback));
      NotificationCenter<string>.RemoveListener("sceneLoaded", new Callback<string>(this.SceneLoadedCallback));
      NotificationCenter<string>.RemoveListener("sceneChanged", new Callback<string>(this.SceneChangedCallback));
      NotificationCenter<string>.RemoveListener("sceneSetup", new Callback<string>(this.SceneSetupCallback));
    }

    public void RegisterToScene(ISceneObject sceneObject)
    {
      lock (SceneController.sceneControllerLockObject)
        this.sceneObjects.Add(sceneObject);
    }

    public void UnregisterFromScene(ISceneObject sceneObject)
    {
      lock (SceneController.sceneControllerLockObject)
      {
        if (!this.sceneObjects.Contains(sceneObject))
          return;
        this.sceneObjects.Remove(sceneObject);
      }
    }

    public void SetUpScene() => ScenePoolManager.CreateStartupPools();

    public void BreakDownScene()
    {
    }

    public void Claim(ISceneSpecific sceneSpecific) => this.sceneSpecific = sceneSpecific;

    public void ActivateScene()
    {
      if (SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.previousScene.name.IsEmptyOrWhiteSpaceOrNull())
      {
        NotificationCenter<string>.Broadcast("activateScene", this.sceneName);
        this.StartCoroutine(this.DoPlayInTransition());
      }
      else
        this.UnloadPreviousScene();
    }

    private IEnumerator DoPlayInTransition()
    {
      SceneController sceneController = this;
      if (DebugManager.StateForKey("SceneController Callbacks"))
        Debug.Log((object) (((object) sceneController).GetType().Name + " at DoPlayInTransition BEGIN"));
      while (SceneTransitionPlayer.PlayInSceneTransition())
        yield return (object) null;
      if (SceneTransitionPlayer.Exists())
        SceneTransitionPlayer.Destroy();
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) sceneController).GetType().Name + " at DoPlayInTransition FINISH"));
      LoadingScene.DestroyLoadingSceneIfExists();
      NotificationCenter<string>.Broadcast("claimSceneController", sceneController.sceneName);
      sceneController.SceneStart();
    }

    protected virtual void OnSceneStart()
    {
    }

    protected virtual void OnSceneEnd()
    {
    }
  }
}
