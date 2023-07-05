// Decompiled with JetBrains decompiler
// Type: UDB.ProjectPoolManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Reflection;
using UnityEngine;

namespace UDB
{
  [RequireComponent(typeof (PoolController))]
  public class ProjectPoolManager : 
    SingletonBehaviour<ProjectPoolManager, MonoBehaviour>,
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

    private new void Awake()
    {
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
      NotificationCenter<string>.Broadcast("sceneSetup", SceneManager.activeSceneName);
    }

    public static bool ContainsGroup(string prefabName) => SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.ContainsGroup(prefabName);

    public static bool ContainsGroup(GameObject prefab) => SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.ContainsGroup(prefab.GetNameWithoutClone());

    public static GameObject Spawn(string prefabName)
    {
      if (DebugManager.StateForKey("ProjectPoolManager Methods"))
        Debug.Log((object) (((object) SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance).GetType()?.ToString() + " at " + MethodBase.GetCurrentMethod().Name + ": " + prefabName));
      return SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.Spawn(prefabName);
    }

    public static GameObject Spawn(GameObject prefab)
    {
      if (DebugManager.StateForKey("ProjectPoolManager Methods"))
        Debug.Log((object) (((object) SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance).GetType()?.ToString() + " at " + MethodBase.GetCurrentMethod().Name + ": " + prefab.GetNameWithoutClone()));
      return SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.Spawn(prefab);
    }

    public static void Recycle(GameObject go)
    {
      if (DebugManager.StateForKey("ProjectPoolManager Methods"))
        Debug.Log((object) (((object) SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance).GetType()?.ToString() + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.Recycle(go);
    }

    public static void CreateStartupPools() => SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.CreateStartupPools();

    public static void PoolableObjectSpawned(int id, IPoolableObject poolableObject) => SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.PoolableObjectSpawned(id, poolableObject);

    public static void ActivatePoolable(GameObject go)
    {
      if (DebugManager.StateForKey("ProjectPoolManager Methods"))
        Debug.Log((object) (((object) SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance).GetType()?.ToString() + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.ActionOnPoolable(PoolableAction.Activate, go);
    }

    public static void DeactivatePoolable(GameObject go)
    {
      if (DebugManager.StateForKey("ProjectPoolManager Methods"))
        Debug.Log((object) (((object) SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance).GetType()?.ToString() + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.ActionOnPoolable(PoolableAction.Deactivate, go);
    }

    public static void RecyclePoolable(GameObject go)
    {
      if (DebugManager.StateForKey("ProjectPoolManager Methods"))
        Debug.Log((object) (((object) SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance).GetType()?.ToString() + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      SingletonBehaviour<ProjectPoolManager, MonoBehaviour>.instance.poolController.ActionOnPoolable(PoolableAction.Recycle, go);
    }
  }
}
