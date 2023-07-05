// Decompiled with JetBrains decompiler
// Type: UDB.ObjectPool
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class ObjectPool : SerializedCachedMonoBehaviour
  {
    public Coroutine loadPoolWithObjectsCoroutine;
    [SerializeField]
    private List<PoolTemplate> _poolTemplates;
    [SerializeField]
    private List<GameObject> _recycleBin;
    [SerializeField]
    private List<GameObject> _trashCan;
    [SerializeField]
    private Dictionary<string, List<GameObject>> _spawnedObjects;
    [SerializeField]
    private Dictionary<string, List<GameObject>> _pooledObjects;
    [SerializeField]
    private Dictionary<string, List<GameObject>> _poolableObjects;
    [SerializeField]
    private Dictionary<string, GameObject> _groupPrefabMap;
    private Quaternion rotation;
    private PoolController poolController;

    public string group { get; private set; }

    public List<PoolTemplate> poolTemplates
    {
      get
      {
        if (this._poolTemplates == null)
          this._poolTemplates = new List<PoolTemplate>();
        return this._poolTemplates;
      }
      set => this._poolTemplates = value;
    }

    private List<GameObject> recycleBin
    {
      get
      {
        if (this._recycleBin == null)
          this._recycleBin = new List<GameObject>();
        return this._recycleBin;
      }
      set => this._recycleBin = value;
    }

    private List<GameObject> trashCan
    {
      get
      {
        if (this._trashCan == null)
          this._trashCan = new List<GameObject>();
        return this._trashCan;
      }
      set => this._trashCan = value;
    }

    public Dictionary<string, List<GameObject>> spawnedObjects
    {
      get
      {
        if (this._spawnedObjects == null)
          this._spawnedObjects = new Dictionary<string, List<GameObject>>();
        return this._spawnedObjects;
      }
      set => this._spawnedObjects = value;
    }

    public Dictionary<string, List<GameObject>> pooledObjects
    {
      get
      {
        if (this._pooledObjects == null)
          this._pooledObjects = new Dictionary<string, List<GameObject>>();
        return this._pooledObjects;
      }
      set => this._pooledObjects = value;
    }

    public Dictionary<string, List<GameObject>> poolableObjects
    {
      get
      {
        if (this._poolableObjects == null)
          this._poolableObjects = new Dictionary<string, List<GameObject>>();
        return this._poolableObjects;
      }
      set => this._poolableObjects = value;
    }

    public Dictionary<string, GameObject> groupPrefabMap
    {
      get
      {
        if (this._groupPrefabMap == null)
          this._groupPrefabMap = new Dictionary<string, GameObject>();
        return this._groupPrefabMap;
      }
      set => this._groupPrefabMap = value;
    }

    private void Awake() => this.rotation = this.transform.rotation;

    private void AddToSpawned(string goName, GameObject go)
    {
      List<GameObject> gameObjectList;
      if (!this.spawnedObjects.TryGetValue(goName, out gameObjectList))
        gameObjectList = new List<GameObject>();
      gameObjectList.Add(go);
      this.spawnedObjects[goName] = gameObjectList;
    }

    private void AddToPooled(string goName, GameObject go)
    {
      List<GameObject> gameObjectList;
      if (!this.pooledObjects.TryGetValue(goName, out gameObjectList))
        gameObjectList = new List<GameObject>();
      gameObjectList.Add(go);
      this.pooledObjects[goName] = gameObjectList;
    }

    private void AddToPoolable(string goName, GameObject go)
    {
      List<GameObject> gameObjectList;
      if (!this.poolableObjects.TryGetValue(goName, out gameObjectList))
        gameObjectList = new List<GameObject>();
      if (gameObjectList.Contains(go))
        return;
      gameObjectList.Add(go);
      this.poolableObjects[goName] = gameObjectList;
    }

    public static ObjectPool Generate(string group, Transform parent)
    {
      Transform transform = new GameObject().transform;
      transform.name = group;
      transform.gameObject.SetActive(true);
      transform.SetParent(parent, false);
      ObjectPool objectPool = transform.gameObject.AddComponent<ObjectPool>();
      objectPool.group = group;
      return objectPool;
    }

    public void AssignToController(PoolController poolController) => this.poolController = poolController;

    public void ApplyPoolTemplate(PoolTemplate poolTemplate)
    {
      if ((Object) poolTemplate.prefab == (Object) null)
      {
        if (!DebugManager.StateForKey("ObjectPool Warnings"))
          return;
        Debug.LogWarning((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ":  Cant load prefabs for pool"));
      }
      else
      {
        this.poolTemplates.Add(poolTemplate);
        string key = poolTemplate.prefab.GetNameWithoutClone();
        if (poolTemplate.group.Length > 0)
          key = poolTemplate.group;
        this.groupPrefabMap.Add(key, poolTemplate.prefab);
        this.poolController.MapKeyToGroup(poolTemplate.prefab.name, this.group);
        this.loadPoolWithObjectsCoroutine = this.StartCoroutine(this.LoadPoolWithObjects(poolTemplate.prefab, poolTemplate.size));
      }
    }

    public GameObject Spawn(string goName, string groupName) => this.pooledObjects.ContainsKey(goName) && this.groupPrefabMap.ContainsKey(groupName) ? this.Spawn(this.groupPrefabMap[groupName]) : (GameObject) null;

    public GameObject Spawn(GameObject prefab)
    {
      if (DebugManager.StateForKey("ObjectPool Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + prefab.GetNameWithoutClone()));
      string nameWithoutClone = prefab.GetNameWithoutClone();
      GameObject gameObject = (GameObject) null;
      List<GameObject> gameObjectList;
      if (this.pooledObjects.TryGetValue(nameWithoutClone, out gameObjectList) && gameObjectList.Count > 0)
      {
        gameObject = gameObjectList[0];
        gameObjectList.Remove(gameObject);
        this.pooledObjects[nameWithoutClone] = gameObjectList;
        gameObject.transform.SetParent((Transform) null, false);
      }
      if ((Object) gameObject == (Object) null)
      {
        gameObject = Object.Instantiate<GameObject>(prefab);
        gameObject.GetComponent<IPoolableObject>()?.OnPoolableSpawned();
      }
      if ((Object) gameObject != (Object) null)
      {
        gameObject.SetActive(false);
        this.AddToSpawned(gameObject.GetNameWithoutClone(), gameObject);
        NotificationCenter<int>.Broadcast("objectSpawned", gameObject.GetInstanceID());
      }
      return gameObject;
    }

    public void Recycle(GameObject go)
    {
      if (DebugManager.StateForKey("ObjectPool Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.name));
      bool flag1 = false;
      bool flag2 = false;
      List<GameObject> gameObjectList1;
      if (this.poolableObjects.TryGetValue(go.GetNameWithoutClone(), out gameObjectList1) && gameObjectList1.Contains(go))
      {
        flag2 = true;
        flag1 = true;
      }
      if (!flag1)
      {
        List<GameObject> gameObjectList2;
        if (this.spawnedObjects.TryGetValue(go.GetNameWithoutClone(), out gameObjectList2))
        {
          if (gameObjectList2.Contains(go))
            gameObjectList2.Remove(go);
          flag2 = true;
        }
        else
          Object.Destroy((Object) go);
      }
      if (!flag2)
        return;
      this.AddToPooled(go.GetNameWithoutClone(), go);
      this.AddToPoolable(go.GetNameWithoutClone(), go);
      go.transform.parent = this.transform;
      go.SetActive(false);
      this.poolController.ActionOnPoolable(PoolableAction.Recycle, go);
    }

    public void RecycleAllObjects()
    {
      if (DebugManager.StateForKey("ObjectPool Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
      this.trashCan.Clear();
      this.recycleBin.Clear();
      foreach (string key in this.poolableObjects.Keys)
      {
        List<GameObject> gameObjectList1;
        List<GameObject> gameObjectList2;
        if (this.poolableObjects.TryGetValue(key, out gameObjectList1) && this.pooledObjects.TryGetValue(key, out gameObjectList2))
        {
          foreach (GameObject gameObject in gameObjectList1)
          {
            if (!gameObjectList2.Contains(gameObject))
              this.recycleBin.Add(gameObject);
          }
        }
      }
      foreach (string key in this.spawnedObjects.Keys)
      {
        bool flag = this.poolableObjects.ContainsKey(key);
        List<GameObject> gameObjectList;
        if (this.spawnedObjects.TryGetValue(key, out gameObjectList))
        {
          foreach (GameObject gameObject in gameObjectList)
          {
            if (flag)
              this.recycleBin.Add(gameObject);
            else
              this.trashCan.Add(gameObject);
          }
        }
      }
      int count1 = this.recycleBin.Count;
      for (int index = 0; index < count1; ++index)
        this.Recycle(this.recycleBin[index]);
      int count2 = this.trashCan.Count;
      for (int index = 0; index < count2; ++index)
        Object.Destroy((Object) this.trashCan[index]);
      this.trashCan.Clear();
      this.recycleBin.Clear();
    }

    private IEnumerator LoadPoolWithObjects(GameObject prefab, int initialPoolSize)
    {
      ObjectPool objectPool = this;
      string goName = prefab.GetNameWithoutClone();
      List<GameObject> list;
      if (!objectPool.pooledObjects.TryGetValue(goName, out list))
        list = new List<GameObject>();
      List<GameObject> poolableList;
      if (!objectPool.poolableObjects.TryGetValue(goName, out poolableList))
        poolableList = new List<GameObject>();
      if (initialPoolSize > 0)
      {
        bool active = prefab.activeSelf;
        prefab.SetActive(false);
        while (list.Count < initialPoolSize)
        {
          GameObject go = Object.Instantiate<GameObject>(prefab);
          yield return (object) null;
          Transform trans = go.transform;
          yield return (object) null;
          trans.parent = objectPool.transform;
          yield return (object) null;
          trans.localPosition = Vector3.zero;
          trans.localRotation = objectPool.rotation;
          list.Add(go);
          poolableList.Add(go);
          go = (GameObject) null;
          trans = (Transform) null;
        }
        prefab.SetActive(active);
      }
      objectPool.pooledObjects.Add(goName, list);
      objectPool.poolableObjects.Add(goName, poolableList);
      objectPool.loadPoolWithObjectsCoroutine = (Coroutine) null;
    }
  }
}
