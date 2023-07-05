// Decompiled with JetBrains decompiler
// Type: UDB.SingletonBehaviour`2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class SingletonBehaviour<T, P> : MonoBehaviour
    where T : MonoBehaviour
    where P : MonoBehaviour
  {
    private static SingletonBehaviour<T, P>.InstanceState instanceState = SingletonBehaviour<T, P>.InstanceState.Uninitialized;
    private static object lockObject = new object();
    private static T _instance;

    public static T instance
    {
      get => SingletonBehaviour<T, P>.GetInstance();
      set => SingletonBehaviour<T, P>._instance = value;
    }

    public void Awake()
    {
      if (!((UnityEngine.Object) SingletonBehaviour<T, P>._instance == (UnityEngine.Object) null) || !((UnityEngine.Object) this.gameObject != (UnityEngine.Object) null))
        return;
      SingletonBehaviour<T, P>._instance = this.gameObject.GetComponent<T>();
      if (!((UnityEngine.Object) SingletonBehaviour<T, P>._instance != (UnityEngine.Object) null))
        return;
      SingletonBehaviour<T, P>.instanceState = SingletonBehaviour<T, P>.InstanceState.Uninitialized;
      SingletonBehaviour<T, P>.UpdateInstanceState(true);
    }

    private static T GetInstance()
    {
      lock (SingletonBehaviour<T, P>.lockObject)
      {
        if ((UnityEngine.Object) SingletonBehaviour<T, P>._instance == (UnityEngine.Object) null)
          SingletonBehaviour<T, P>.instanceState = SingletonBehaviour<T, P>.InstanceState.Uninitialized;
        if (SingletonBehaviour<T, P>.instanceState == SingletonBehaviour<T, P>.InstanceState.Initialized)
          return SingletonBehaviour<T, P>._instance;
        System.Type element = typeof (T);
        T[] objectsOfType = UnityEngine.Object.FindObjectsOfType<T>();
        if (objectsOfType.Length != 0)
        {
          SingletonBehaviour<T, P>._instance = objectsOfType[0];
          SingletonBehaviour<T, P>.UpdateInstanceState(true);
          if (objectsOfType.Length > 1)
          {
            Debug.LogWarning((object) ("Multiple instances of singleton " + element?.ToString() + " found; destroying all but the first."));
            for (int index = 1; index < objectsOfType.Length; ++index)
              UnityEngine.Object.Destroy((UnityEngine.Object) objectsOfType[index].gameObject);
          }
          return SingletonBehaviour<T, P>._instance;
        }
        if (!(Attribute.GetCustomAttribute((MemberInfo) element, typeof (SingletonPrefabAttribute)) is SingletonPrefabAttribute customAttribute))
        {
          SingletonBehaviour<T, P>.CreateInstance();
        }
        else
        {
          string name = customAttribute.Name;
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>(name));
          if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
          {
            Debug.LogError((object) ("Could not find prefab " + name + " for singleton of type " + element?.ToString() + "."));
            SingletonBehaviour<T, P>.CreateInstance();
          }
          else
          {
            gameObject.name = name;
            SingletonBehaviour<T, P>._instance = gameObject.GetComponent<T>();
            if ((UnityEngine.Object) SingletonBehaviour<T, P>._instance == (UnityEngine.Object) null)
            {
              Debug.LogError((object) ("There wasn't a component of type \"" + element?.ToString() + "\" inside prefab \"" + name + "\"; creating one now."));
              SingletonBehaviour<T, P>._instance = gameObject.AddComponent<T>();
              SingletonBehaviour<T, P>.UpdateInstanceState(true);
            }
          }
        }
        return SingletonBehaviour<T, P>.instance;
      }
    }

    private static void CreateInstance()
    {
      GameObject gameObject;
      if (typeof (P) == typeof (MonoBehaviour))
      {
        gameObject = new GameObject();
        gameObject.name = typeof (T).Name;
      }
      else
      {
        P objectOfType = UnityEngine.Object.FindObjectOfType<P>();
        if ((bool) (UnityEngine.Object) objectOfType)
        {
          gameObject = objectOfType.gameObject;
        }
        else
        {
          Debug.LogError((object) ("Could not find object with required component " + typeof (P).Name));
          return;
        }
      }
      SingletonBehaviour<T, P>._instance = gameObject.AddComponent<T>();
      SingletonBehaviour<T, P>.UpdateInstanceState(true);
    }

    protected static void UpdateInstanceState(bool instanciated)
    {
      if (!(SingletonBehaviour<T, P>.instanceState == SingletonBehaviour<T, P>.InstanceState.Uninitialized & instanciated))
        return;
      SingletonBehaviour<T, P>.instanceState = SingletonBehaviour<T, P>.InstanceState.Initialized;
      ((object) SingletonBehaviour<T, P>.instance as SingletonBehaviour<T, P>).OnInstanceInit();
    }

    protected bool EnforceSingleton()
    {
      lock (SingletonBehaviour<T, P>.lockObject)
      {
        if (SingletonBehaviour<T, P>.instanceState == SingletonBehaviour<T, P>.InstanceState.Initialized)
        {
          T[] objectsOfType = UnityEngine.Object.FindObjectsOfType<T>();
          for (int index = 0; index < objectsOfType.Length; ++index)
          {
            if (objectsOfType[index].GetInstanceID() != SingletonBehaviour<T, P>._instance.GetInstanceID())
              UnityEngine.Object.Destroy((UnityEngine.Object) objectsOfType[index].gameObject);
          }
        }
      }
      return this.GetInstanceID() == SingletonBehaviour<T, P>.instance.GetInstanceID();
    }

    protected bool EnforceSingletonComponent()
    {
      lock (SingletonBehaviour<T, P>.lockObject)
      {
        if (SingletonBehaviour<T, P>.instanceState == SingletonBehaviour<T, P>.InstanceState.Initialized)
        {
          if (this.GetInstanceID() != SingletonBehaviour<T, P>._instance.GetInstanceID())
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) this);
            return false;
          }
        }
      }
      return true;
    }

    protected void InstancitateIfNeeded()
    {
      lock (SingletonBehaviour<T, P>.lockObject)
      {
        if (!((UnityEngine.Object) SingletonBehaviour<T, P>._instance == (UnityEngine.Object) null))
          return;
        SingletonBehaviour<T, P>._instance = this.gameObject.GetComponent<T>();
      }
    }

    public static bool Exists() => (UnityEngine.Object) SingletonBehaviour<T, P>._instance != (UnityEngine.Object) null;

    protected virtual void OnInstanceInit()
    {
    }

    private enum InstanceState
    {
      Uninitialized,
      Initialized,
      Destroyed,
    }

    public enum LogType
    {
      Error,
      Warning,
      Message,
    }
  }
}
