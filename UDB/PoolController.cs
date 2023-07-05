// Decompiled with JetBrains decompiler
// Type: UDB.PoolController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class PoolController : SerializedCachedMonoBehaviour
  {
    private IPoolManager manager;
    [SerializeField]
    protected PoolController.ObjectPoolStatus currentPoolStatus;
    public PoolController.StartupPoolMode startupPoolMode;
    public PoolTemplate[] startupTemplates;
    [SerializeField]
    private Dictionary<string, ObjectPool> _objectPools;
    [SerializeField]
    private Dictionary<string, string> _keyPoolMap;
    [SerializeField]
    private Dictionary<int, IPoolableObject> _spawnedInterfaces;
    private bool initialized;

    public Dictionary<string, ObjectPool> objectPools
    {
      get
      {
        if (this._objectPools == null)
          this._objectPools = new Dictionary<string, ObjectPool>();
        return this._objectPools;
      }
      private set => this._objectPools = value;
    }

    public Dictionary<string, string> keyPoolMap
    {
      get
      {
        if (this._keyPoolMap == null)
          this._keyPoolMap = new Dictionary<string, string>();
        return this._keyPoolMap;
      }
      private set => this._keyPoolMap = value;
    }

    public Dictionary<int, IPoolableObject> spawnedInterfaces
    {
      get
      {
        if (this._spawnedInterfaces == null)
          this._spawnedInterfaces = new Dictionary<int, IPoolableObject>();
        return this._spawnedInterfaces;
      }
      set => this._spawnedInterfaces = value;
    }

    public bool initializeOnAwake => !this.initialized && this.startupPoolMode == PoolController.StartupPoolMode.Awake;

    public bool initializeOnStart => !this.initialized && this.startupPoolMode == PoolController.StartupPoolMode.Start;

    public bool initializeOnInit => !this.initialized && this.startupPoolMode == PoolController.StartupPoolMode.Init;

    private ObjectPool PoolForGameObject(GameObject go)
    {
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      string group = "";
      if (!this.keyPoolMap.TryGetValue(go.GetNameWithoutClone(), out group))
        group = go.GetNameWithoutClone();
      return this.PoolForGroup(group);
    }

    private ObjectPool PoolForGroup(string group)
    {
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      ObjectPool objectPool = (ObjectPool) null;
      if (!this.objectPools.TryGetValue(group, out objectPool))
      {
        objectPool = ObjectPool.Generate(group, this.transform);
        objectPool.AssignToController(this);
        this.objectPools.Add(group, objectPool);
        Debug.Log((object) ("Creating group " + group));
      }
      return objectPool;
    }

    private IPoolableObject PoolableObjectForGameObject(GameObject go)
    {
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      IPoolableObject poolableObject;
      if (this.spawnedInterfaces.TryGetValue(go.GetInstanceID(), out poolableObject))
        return poolableObject;
      if (DebugManager.StateForKey("PoolController Warnings"))
        Debug.LogWarning((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Poolable Interface was not found for id"));
      return (IPoolableObject) null;
    }

    public void Claim(IPoolManager manager) => this.manager = manager;

    public void CreateStartupPools() => this.Initialize();

    public void Initialize()
    {
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      this.initialized = true;
      this.StartCoroutine(this.CreateStartupPoolsCoroutine());
    }

    public bool ContainsGroup(string gameObjectName)
    {
      string key = "";
      if (!this.keyPoolMap.TryGetValue(gameObjectName, out key))
        key = gameObjectName;
      return this.objectPools.ContainsKey(key);
    }

    public GameObject Spawn(GameObject go)
    {
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      return this.PoolForGameObject(go).Spawn(go);
    }

    public GameObject Spawn(string goName)
    {
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      string str;
      if (this.keyPoolMap.TryGetValue(goName, out str))
        return this.objectPools[str].Spawn(goName, str);
      if (DebugManager.StateForKey("PoolController Warnings"))
        Debug.LogWarning((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " No group found: " + goName));
      return (GameObject) null;
    }

    public void MapKeyToGroup(string key, string group)
    {
      if (this.keyPoolMap.TryGetValue(key, out string _))
      {
        this.keyPoolMap.Remove(key);
        if (DebugManager.StateForKey("PoolController Methods"))
          Debug.LogWarning((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " Pool already made for key: " + key));
      }
      this.keyPoolMap.Add(key, group);
    }

    public void Recycle(GameObject go)
    {
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      this.PoolForGameObject(go).Recycle(go);
    }

    public void RecycleAll()
    {
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      foreach (KeyValuePair<string, ObjectPool> objectPool in this.objectPools)
        objectPool.Value.RecycleAllObjects();
    }

    public void PoolableObjectSpawned(int id, IPoolableObject poolableObject)
    {
      if (this.spawnedInterfaces.TryGetValue(id, out IPoolableObject _))
        Debug.Log((object) "IPoolableObject is already spawned");
      else
        this.spawnedInterfaces.Add(id, poolableObject);
    }

    public void ActionOnPoolable(PoolableAction poolableAction, GameObject go)
    {
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + poolableAction.ToString() + ": " + go.GetNameWithoutClone()));
      if (go == null)
        return;
      IPoolableObject poolableObject = this.PoolableObjectForGameObject(go);
      if (poolableObject == null)
        return;
      switch (poolableAction)
      {
        case PoolableAction.Recycle:
          Debug.Log((object) (((object) this).GetType().Name + " RecyclePoolable: " + go.GetNameWithoutClone()));
          poolableObject.OnPoolableRecycle();
          break;
        case PoolableAction.Activate:
          Debug.Log((object) (((object) this).GetType().Name + " ActivatePoolable: " + go.GetNameWithoutClone()));
          poolableObject.OnPoolableActivate();
          break;
        case PoolableAction.Deactivate:
          Debug.Log((object) (((object) this).GetType().Name + " DeactivatePoolable: " + go.GetNameWithoutClone()));
          poolableObject.OnPoolableDeactivate();
          break;
      }
    }

    private IEnumerator CreateStartupPoolsCoroutine()
    {
      PoolController poolController = this;
      if (DebugManager.StateForKey("PoolController Methods"))
        Debug.Log((object) (((object) poolController).GetType().Name + " at CreateStartupPoolsCoroutine"));
      if (poolController.currentPoolStatus == PoolController.ObjectPoolStatus.NotLoaded)
      {
        poolController.currentPoolStatus = PoolController.ObjectPoolStatus.Loading;
        if (poolController.startupTemplates != null && poolController.startupTemplates.Length != 0)
        {
          for (int i = 0; i < poolController.startupTemplates.Length; ++i)
          {
            PoolTemplate startupTemplate = poolController.startupTemplates[i];
            string group = startupTemplate.group.Length <= 0 ? startupTemplate.prefab.GetNameWithoutClone() : startupTemplate.group;
            ObjectPool objectPool = poolController.PoolForGroup(group);
            objectPool.ApplyPoolTemplate(startupTemplate);
            while (objectPool.loadPoolWithObjectsCoroutine != null)
              yield return (object) null;
            objectPool = (ObjectPool) null;
          }
        }
        poolController.currentPoolStatus = PoolController.ObjectPoolStatus.Loaded;
      }
      if (poolController.manager != null)
        poolController.manager.OnPoolFinishedSetup();
    }

    public enum StartupPoolMode
    {
      Init,
      Awake,
      Start,
      CallManually,
    }

    public enum ObjectPoolStatus
    {
      NotLoaded,
      Loading,
      Loaded,
    }
  }
}
