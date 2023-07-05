// Decompiled with JetBrains decompiler
// Type: UDB.ScenePoolManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  [RequireComponent(typeof (PoolController))]
  [RequireComponent(typeof (PoolController))]
  public class ScenePoolManager : 
    SerializedSceneSingletonBehaviour<ScenePoolManager, SerializedMonoBehaviour>,
    IPoolManager
  {
    private PoolController _poolController;

    protected PoolController poolController
    {
      get
      {
        if (this._poolController == null)
          this._poolController = this.GetComponent<PoolController>();
        return this._poolController;
      }
    }

    private void Awake()
    {
      this.InstanciateIfNeeded();
      this.poolController.Claim((IPoolManager) this);
      if (!this.poolController.initializeOnAwake)
        return;
      this.poolController.Initialize();
    }

    private void Start()
    {
      if (!this.poolController.initializeOnStart)
        return;
      this.poolController.Initialize();
    }

    protected override void OnInstanceInit()
    {
      if (!this.poolController.initializeOnInit)
        return;
      this.poolController.Initialize();
    }

    public void OnPoolFinishedSetup()
    {
      if (DebugManager.StateForKey("IPoolManager Overrides"))
        Debug.Log((object) (((object) this).GetType()?.ToString() + " at " + MethodBase.GetCurrentMethod().Name));
      NotificationCenter<string>.Broadcast("sceneSetup", this.sceneName);
    }

    private static ScenePoolManager currentScenePoolManager => SerializedSceneSingletonBehaviour<ScenePoolManager, SerializedMonoBehaviour>.InstanceOfScene(SceneManager.activeSceneName);

    private static PoolController currentScenePoolController => (Object) ScenePoolManager.currentScenePoolManager != (Object) null ? ScenePoolManager.currentScenePoolManager.poolController : (PoolController) null;

    public static bool ContainsGroup(string prefabName) => (Object) ScenePoolManager.currentScenePoolController != (Object) null && ScenePoolManager.currentScenePoolController.ContainsGroup(prefabName);

    public static bool ContainsGroup(GameObject prefab) => (Object) ScenePoolManager.currentScenePoolController != (Object) null && ScenePoolManager.currentScenePoolController.ContainsGroup(prefab.GetNameWithoutClone());

    public static GameObject Spawn(string prefabName)
    {
      if (DebugManager.StateForKey("ScenePoolManager Methods"))
        Debug.Log((object) ("ScenePoolManager at " + MethodBase.GetCurrentMethod().Name + ": " + prefabName));
      return (Object) ScenePoolManager.currentScenePoolController != (Object) null ? ScenePoolManager.currentScenePoolController.Spawn(prefabName) : (GameObject) null;
    }

    public static GameObject Spawn(GameObject prefab)
    {
      if (DebugManager.StateForKey("ScenePoolManager Methods"))
        Debug.Log((object) ("ScenePoolManager at " + MethodBase.GetCurrentMethod().Name + ": " + prefab.GetNameWithoutClone()));
      return (Object) ScenePoolManager.currentScenePoolController != (Object) null ? ScenePoolManager.currentScenePoolController.Spawn(prefab) : (GameObject) null;
    }

    public static void Recycle(GameObject go)
    {
      if (DebugManager.StateForKey("ScenePoolManager Methods"))
        Debug.Log((object) ("ScenePoolManager at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      if (!((Object) ScenePoolManager.currentScenePoolController != (Object) null))
        return;
      ScenePoolManager.currentScenePoolController.Recycle(go);
    }

    public static void CreateStartupPools()
    {
      if (!((Object) ScenePoolManager.currentScenePoolController != (Object) null))
        return;
      ScenePoolManager.currentScenePoolController.CreateStartupPools();
    }

    public static void PoolableObjectSpawned(int id, IPoolableObject poolableObject)
    {
      if (!((Object) ScenePoolManager.currentScenePoolController != (Object) null))
        return;
      ScenePoolManager.currentScenePoolController.PoolableObjectSpawned(id, poolableObject);
    }

    public static void ActivatePoolable(GameObject go)
    {
      if (DebugManager.StateForKey("ScenePoolManager Methods"))
        Debug.Log((object) ("ScenePoolManager at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      if (!((Object) ScenePoolManager.currentScenePoolController != (Object) null))
        return;
      ScenePoolManager.currentScenePoolController.ActionOnPoolable(PoolableAction.Activate, go);
    }

    public static void DeactivatePoolable(GameObject go)
    {
      if (DebugManager.StateForKey("ScenePoolManager Methods"))
        Debug.Log((object) ("ScenePoolManager at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      if (!((Object) ScenePoolManager.currentScenePoolController != (Object) null))
        return;
      ScenePoolManager.currentScenePoolController.ActionOnPoolable(PoolableAction.Deactivate, go);
    }

    public static void RecyclePoolable(GameObject go)
    {
      if (DebugManager.StateForKey("ScenePoolManager Methods"))
        Debug.Log((object) ("ScenePoolManager at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      if (!((Object) ScenePoolManager.currentScenePoolController != (Object) null))
        return;
      ScenePoolManager.currentScenePoolController.ActionOnPoolable(PoolableAction.Recycle, go);
    }
  }
}
