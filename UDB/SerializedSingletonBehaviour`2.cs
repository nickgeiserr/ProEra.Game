// Decompiled with JetBrains decompiler
// Type: UDB.SerializedSingletonBehaviour`2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class SerializedSingletonBehaviour<T, P> : SerializedMonoBehaviour
    where T : SerializedMonoBehaviour
    where P : SerializedMonoBehaviour
  {
    private static SerializedSingletonBehaviour<T, P>.InstanceState instanceState = SerializedSingletonBehaviour<T, P>.InstanceState.Uninitialized;
    private static object lockObject = new object();
    private static T _instance;

    public static T instance => SerializedSingletonBehaviour<T, P>.GetInstance();

    private static T GetInstance()
    {
      lock (SerializedSingletonBehaviour<T, P>.lockObject)
      {
        if ((UnityEngine.Object) SerializedSingletonBehaviour<T, P>._instance == (UnityEngine.Object) null)
          SerializedSingletonBehaviour<T, P>.instanceState = SerializedSingletonBehaviour<T, P>.InstanceState.Destroyed;
        if (SerializedSingletonBehaviour<T, P>.instanceState == SerializedSingletonBehaviour<T, P>.InstanceState.Initialized)
          return SerializedSingletonBehaviour<T, P>._instance;
        System.Type element = typeof (T);
        T[] objectsOfType = UnityEngine.Object.FindObjectsOfType<T>();
        if (objectsOfType.Length != 0)
        {
          SerializedSingletonBehaviour<T, P>._instance = objectsOfType[0];
          SerializedSingletonBehaviour<T, P>.UpdateInstanceState(true);
          if (objectsOfType.Length > 1)
          {
            if (DebugManager.StateForKey("Singleton Errors"))
              Debug.LogWarning((object) ("Multiple instances of singleton " + element?.ToString() + " found; destroying all but the first."));
            for (int index = 1; index < objectsOfType.Length; ++index)
              UnityEngine.Object.DestroyImmediate((UnityEngine.Object) objectsOfType[index].gameObject);
          }
          return SerializedSingletonBehaviour<T, P>._instance;
        }
        if (!(Attribute.GetCustomAttribute((MemberInfo) element, typeof (SingletonPrefabAttribute)) is SingletonPrefabAttribute customAttribute))
        {
          SerializedSingletonBehaviour<T, P>.CreateInstance();
        }
        else
        {
          string name = customAttribute.Name;
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>(name));
          if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
          {
            if (DebugManager.StateForKey("Singleton Errors"))
              Debug.LogError((object) ("Could not find prefab " + name + " for singleton of type " + element?.ToString() + "."));
            SerializedSingletonBehaviour<T, P>.CreateInstance();
          }
          else
          {
            gameObject.name = name;
            SerializedSingletonBehaviour<T, P>._instance = gameObject.GetComponent<T>();
            if ((UnityEngine.Object) SerializedSingletonBehaviour<T, P>._instance == (UnityEngine.Object) null)
            {
              if (DebugManager.StateForKey("Singleton Warnings"))
                Debug.LogWarning((object) ("There wasn't a component of type \"" + element?.ToString() + "\" inside prefab \"" + name + "\"; creating one now."));
              SerializedSingletonBehaviour<T, P>._instance = gameObject.AddComponent<T>();
              SerializedSingletonBehaviour<T, P>.UpdateInstanceState(true);
            }
          }
        }
        return SerializedSingletonBehaviour<T, P>.instance;
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
          if (!DebugManager.StateForKey("Singleton Errors"))
            return;
          Debug.LogError((object) ("Could not find object with required component " + typeof (P).Name));
          return;
        }
      }
      if (DebugManager.StateForKey("Singleton Messages"))
        Debug.Log((object) ("Creating instance of singleton component " + typeof (T).Name));
      SerializedSingletonBehaviour<T, P>._instance = gameObject.AddComponent<T>();
      SerializedSingletonBehaviour<T, P>.UpdateInstanceState(true);
    }

    private static void UpdateInstanceState(bool instanciated)
    {
      if (!(SerializedSingletonBehaviour<T, P>.instanceState == SerializedSingletonBehaviour<T, P>.InstanceState.Uninitialized & instanciated))
        return;
      SerializedSingletonBehaviour<T, P>.instanceState = SerializedSingletonBehaviour<T, P>.InstanceState.Initialized;
      ((object) SerializedSingletonBehaviour<T, P>.instance as SerializedSingletonBehaviour<T, P>).OnInstanceInit();
    }

    protected bool EnforceSingleton()
    {
      lock (SerializedSingletonBehaviour<T, P>.lockObject)
      {
        if (SerializedSingletonBehaviour<T, P>.instanceState == SerializedSingletonBehaviour<T, P>.InstanceState.Initialized)
        {
          T[] objectsOfType = UnityEngine.Object.FindObjectsOfType<T>();
          for (int index = 0; index < objectsOfType.Length; ++index)
          {
            if (objectsOfType[index].GetInstanceID() != SerializedSingletonBehaviour<T, P>._instance.GetInstanceID())
              UnityEngine.Object.DestroyImmediate((UnityEngine.Object) objectsOfType[index].gameObject);
          }
        }
      }
      return this.GetInstanceID() == SerializedSingletonBehaviour<T, P>.instance.GetInstanceID();
    }

    protected bool EnforceSingletonComponent()
    {
      lock (SerializedSingletonBehaviour<T, P>.lockObject)
      {
        if (SerializedSingletonBehaviour<T, P>.instanceState == SerializedSingletonBehaviour<T, P>.InstanceState.Initialized)
        {
          if (this.GetInstanceID() != SerializedSingletonBehaviour<T, P>._instance.GetInstanceID())
          {
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this);
            return false;
          }
        }
      }
      return true;
    }

    protected void InstancitateIfNeeded()
    {
      lock (SerializedSingletonBehaviour<T, P>.lockObject)
      {
        if (!((UnityEngine.Object) SerializedSingletonBehaviour<T, P>._instance == (UnityEngine.Object) null))
          return;
        SerializedSingletonBehaviour<T, P>._instance = this.gameObject.GetComponent<T>();
      }
    }

    public static bool Exists() => (UnityEngine.Object) SerializedSingletonBehaviour<T, P>._instance != (UnityEngine.Object) null;

    protected virtual void OnInstanceInit()
    {
    }

    private enum InstanceState
    {
      Uninitialized,
      Initialized,
      Destroyed,
    }
  }
}
