// Decompiled with JetBrains decompiler
// Type: UDB.PoolManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class PoolManager : SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>
  {
    private static object poolManagerLockObject = new object();
    [SerializeField]
    private List<int> _homeless;
    private int uniqueID;
    private Dictionary<string, int> keyDict = new Dictionary<string, int>();
    private static PoolManager self;

    public List<int> homeless
    {
      get
      {
        if (this._homeless == null)
          this._homeless = new List<int>();
        return this._homeless;
      }
    }

    private void Awake()
    {
      if ((Object) PoolManager.self == (Object) null)
      {
        PoolManager.self = this;
        Object.DontDestroyOnLoad((Object) this);
      }
      else
        Object.DestroyImmediate((Object) this.gameObject);
    }

    private bool SpawnFromProjectPool(string prefabName)
    {
      int num1;
      if (!this.keyDict.TryGetValue(prefabName, out num1))
        return false;
      int num2 = 0;
      lock (PoolManager.poolManagerLockObject)
      {
        foreach (int num3 in this.homeless)
        {
          if (num3 == num1)
            ++num2;
        }
      }
      return num2 >= 3;
    }

    private void AddToHomeless(string prefabName)
    {
      int uniqueId;
      if (!this.keyDict.TryGetValue(prefabName, out uniqueId))
      {
        uniqueId = this.uniqueID;
        this.keyDict.Add(prefabName, uniqueId);
        ++this.uniqueID;
      }
      lock (PoolManager.poolManagerLockObject)
        this.homeless.Add(uniqueId);
    }

    public static GameObject Spawn(string prefabName)
    {
      if (DebugManager.StateForKey("PoolManager Methods"))
        Debug.Log((object) (((object) SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + prefabName));
      if (ProjectPoolManager.ContainsGroup(prefabName))
        return ProjectPoolManager.Spawn(prefabName);
      if (ScenePoolManager.ContainsGroup(prefabName))
        return ScenePoolManager.Spawn(prefabName);
      if (SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance.SpawnFromProjectPool(prefabName))
        return ProjectPoolManager.Spawn(prefabName);
      SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance.AddToHomeless(prefabName);
      return ScenePoolManager.Spawn(prefabName);
    }

    public static GameObject Spawn(GameObject prefab)
    {
      if (DebugManager.StateForKey("PoolManager Methods"))
        Debug.Log((object) (((object) SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + prefab.GetNameWithoutClone()));
      string nameWithoutClone = prefab.GetNameWithoutClone();
      if (ProjectPoolManager.ContainsGroup(nameWithoutClone))
        return ProjectPoolManager.Spawn(prefab);
      if (ScenePoolManager.ContainsGroup(nameWithoutClone))
        return ScenePoolManager.Spawn(prefab);
      if (SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance.SpawnFromProjectPool(nameWithoutClone))
        return ProjectPoolManager.Spawn(prefab);
      SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance.AddToHomeless(nameWithoutClone);
      return ScenePoolManager.Spawn(prefab);
    }

    public static void Recycle(GameObject go)
    {
      if (DebugManager.StateForKey("PoolManager Methods"))
        Debug.Log((object) (((object) SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      string nameWithoutClone = go.GetNameWithoutClone();
      if (ProjectPoolManager.ContainsGroup(nameWithoutClone))
        ProjectPoolManager.Recycle(go);
      else if (ProjectPoolManager.ContainsGroup(nameWithoutClone))
        ScenePoolManager.Recycle(go);
      else
        Object.Destroy((Object) go);
    }

    public static void PoolableObjectSpawned(int id, IPoolableObject poolableObject, GameObject go)
    {
      if (DebugManager.StateForKey("PoolManager Methods"))
        Debug.Log((object) (((object) SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      string nameWithoutClone = go.GetNameWithoutClone();
      if (ProjectPoolManager.ContainsGroup(nameWithoutClone))
        ProjectPoolManager.PoolableObjectSpawned(id, poolableObject);
      if (!ScenePoolManager.ContainsGroup(nameWithoutClone))
        return;
      ScenePoolManager.PoolableObjectSpawned(id, poolableObject);
    }

    public static void ActivatePoolable(GameObject go)
    {
      if (DebugManager.StateForKey("PoolManager Methods"))
        Debug.Log((object) (((object) SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      string nameWithoutClone = go.GetNameWithoutClone();
      if (ProjectPoolManager.ContainsGroup(nameWithoutClone))
        ProjectPoolManager.ActivatePoolable(go);
      if (!ScenePoolManager.ContainsGroup(nameWithoutClone))
        return;
      ScenePoolManager.ActivatePoolable(go);
    }

    public static void DeactivatePoolable(GameObject go)
    {
      if (DebugManager.StateForKey("PoolManager Methods"))
        Debug.Log((object) (((object) SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      string nameWithoutClone = go.GetNameWithoutClone();
      if (ProjectPoolManager.ContainsGroup(nameWithoutClone))
        ProjectPoolManager.DeactivatePoolable(go);
      if (!ScenePoolManager.ContainsGroup(nameWithoutClone))
        return;
      ScenePoolManager.DeactivatePoolable(go);
    }

    public static void RecyclePoolable(GameObject go)
    {
      if (DebugManager.StateForKey("PoolManager Methods"))
        Debug.Log((object) (((object) SerializedSingletonBehaviour<PoolManager, SerializedMonoBehaviour>.instance).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + go.GetNameWithoutClone()));
      string nameWithoutClone = go.GetNameWithoutClone();
      if (ProjectPoolManager.ContainsGroup(nameWithoutClone))
        ProjectPoolManager.RecyclePoolable(go);
      if (!ScenePoolManager.ContainsGroup(nameWithoutClone))
        return;
      ScenePoolManager.RecyclePoolable(go);
    }
  }
}
