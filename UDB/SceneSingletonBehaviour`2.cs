// Decompiled with JetBrains decompiler
// Type: UDB.SceneSingletonBehaviour`2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class SceneSingletonBehaviour<T, P> : MonoBehaviour
    where T : MonoBehaviour
    where P : MonoBehaviour
  {
    [SerializeField]
    private static Dictionary<string, T> _instances;
    private string _sceneName = "";

    public static Dictionary<string, T> instances
    {
      get
      {
        if (SceneSingletonBehaviour<T, P>._instances == null)
          SceneSingletonBehaviour<T, P>._instances = new Dictionary<string, T>();
        return SceneSingletonBehaviour<T, P>._instances;
      }
    }

    public string sceneName
    {
      get
      {
        if (this._sceneName.IsEmptyOrWhiteSpaceOrNull())
          this._sceneName = this.gameObject.scene.name;
        return this._sceneName;
      }
    }

    private static T GetInstance()
    {
      System.Type element = typeof (T);
      T instance;
      if (!(Attribute.GetCustomAttribute((MemberInfo) element, typeof (SingletonPrefabAttribute)) is SingletonPrefabAttribute customAttribute))
      {
        instance = SceneSingletonBehaviour<T, P>.CreateInstance();
      }
      else
      {
        string name = customAttribute.Name;
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>(name));
        if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        {
          if (DebugManager.StateForKey("SceneSingleton Errors"))
            Debug.LogError((object) ("Could not find prefab " + name + " for singleton of type " + element?.ToString() + "."));
          instance = SceneSingletonBehaviour<T, P>.CreateInstance();
        }
        else
        {
          gameObject.name = name;
          instance = gameObject.GetComponent<T>();
          if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
          {
            if (DebugManager.StateForKey("SceneSingleton Warnings"))
              Debug.LogWarning((object) ("There wasn't a component of type \"" + element?.ToString() + "\" inside prefab \"" + name + "\"; creating one now."));
            instance = gameObject.AddComponent<T>();
            SceneSingletonBehaviour<T, P>.UpdateInstanceState(instance, true);
          }
        }
      }
      return instance;
    }

    private static T CreateInstance()
    {
      GameObject gameObject = (GameObject) null;
      if (typeof (P) == typeof (MonoBehaviour))
      {
        gameObject = new GameObject();
        gameObject.name = typeof (T).Name;
      }
      else
      {
        P objectOfType = UnityEngine.Object.FindObjectOfType<P>();
        if ((bool) (UnityEngine.Object) objectOfType)
          gameObject = objectOfType.gameObject;
        else if (DebugManager.StateForKey("SceneSingleton Errors"))
          Debug.LogError((object) (MethodBase.GetCurrentMethod().Name + ": Could not find object with required component " + typeof (P).Name));
      }
      if (DebugManager.StateForKey("SceneSingleton Methods"))
        Debug.Log((object) (MethodBase.GetCurrentMethod().Name + ": Creating instance of singleton component " + typeof (T).Name));
      T instance = gameObject.AddComponent<T>();
      SceneSingletonBehaviour<T, P>.UpdateInstanceState(instance, true);
      return instance;
    }

    private static void UpdateInstanceState(T instance, bool instanciated)
    {
      if (instanciated)
        ((object) instance as SceneSingletonBehaviour<T, P>).InstanceInit();
      else
        ((object) instance as SceneSingletonBehaviour<T, P>).InstanceDeinit();
    }

    protected void InstanciateIfNeeded()
    {
      if (SceneSingletonBehaviour<T, P>.instances.ContainsKey(this.sceneName))
        return;
      if (DebugManager.StateForKey("SceneSingleton Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": Creating instance for scene " + this.sceneName));
      SceneSingletonBehaviour<T, P>.instances.Add(this.sceneName, this as T);
      this.OnInstanceInit();
    }

    protected void InstanceInit()
    {
      if (SceneSingletonBehaviour<T, P>.instances.ContainsKey(this.sceneName))
      {
        if (!DebugManager.StateForKey("SceneSingleton Warnings"))
          return;
        Debug.LogWarning((object) (this.sceneName + ": " + ((object) this).GetType().Name + ": for scene " + this.sceneName + " already exists"));
      }
      else
      {
        if (DebugManager.StateForKey("SceneSingleton Methods"))
          Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": Creating instance for scene " + this.sceneName));
        SceneSingletonBehaviour<T, P>.instances.Add(this.sceneName, this as T);
        this.OnInstanceInit();
      }
    }

    protected void InstanceDeinit()
    {
      if (SceneSingletonBehaviour<T, P>.instances.ContainsKey(this.sceneName))
      {
        if (DebugManager.StateForKey("SceneSingleton Methods"))
          Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": Removing instance for scene " + this.sceneName));
        SceneSingletonBehaviour<T, P>.instances.Remove(this.sceneName);
        this.OnInstanceDeinit();
      }
      else
      {
        if (!DebugManager.StateForKey("SceneSingleton Warnings"))
          return;
        Debug.LogWarning((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": Cannont remove instance for scene " + this.sceneName + " because it does not  exists"));
      }
    }

    public static T InstanceOfScene(string sceneName)
    {
      T obj;
      return SceneSingletonBehaviour<T, P>.instances.TryGetValue(sceneName, out obj) ? obj : SceneSingletonBehaviour<T, P>.GetInstance();
    }

    protected virtual void OnInstanceInit()
    {
    }

    protected virtual void OnInstanceDeinit()
    {
    }
  }
}
