// Decompiled with JetBrains decompiler
// Type: UDB.DebugManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class DebugManager : SerializedSingletonBehaviour<DebugManager, SerializedMonoBehaviour>
  {
    private static DebugManager self;
    public DebugPrefences debugPrefences;
    private Dictionary<string, bool> _debugKeys;
    public static bool defaultState = true;
    [SerializeField]
    public Dictionary<string, bool> debugMap = new Dictionary<string, bool>()
    {
      {
        "SceneManager Actions",
        false
      },
      {
        "SceneManager Methods",
        false
      },
      {
        "SceneManager Messages",
        false
      },
      {
        "SceneManager.Vault Methods",
        false
      },
      {
        "SceneManager Warnings",
        true
      },
      {
        "SceneManager Errors",
        true
      },
      {
        "SceneController Methods",
        false
      },
      {
        "SceneController Callbacks",
        false
      },
      {
        "SceneController Errors",
        true
      },
      {
        "LoadingSceneController Methods",
        false
      },
      {
        "LoadingSceneController Callbacks",
        false
      },
      {
        "LoadingSceneController Errors",
        true
      },
      {
        "Singleton Errors",
        true
      },
      {
        "Singleton Warnings",
        true
      },
      {
        "Singleton Messages",
        false
      },
      {
        "SceneSingleton Errors",
        true
      },
      {
        "SceneSingleton Warnings",
        true
      },
      {
        "SceneSingleton Methods",
        false
      },
      {
        "ObjectPool Errors",
        true
      },
      {
        "ObjectPool Warnings",
        true
      },
      {
        "ObjectPool Methods",
        false
      },
      {
        "PoolController Warnings",
        true
      },
      {
        "PoolController Methods",
        false
      },
      {
        "PoolController Co-Routines",
        false
      },
      {
        "PoolManager Methods",
        false
      },
      {
        "ProjectPoolManager Methods",
        false
      },
      {
        "IPoolManager Overrides",
        false
      },
      {
        "ScenePoolManager Methods",
        false
      },
      {
        "SceneTransition Methods",
        false
      },
      {
        "TransitionObjectKeys Methods",
        false
      },
      {
        "TransitionObjectKeys Warnings",
        true
      },
      {
        "SceneTransitionObject Methods",
        false
      },
      {
        "SceneTransitionObject Warnings",
        true
      },
      {
        "Transition Callbacks",
        false
      },
      {
        "Transition Methods",
        false
      },
      {
        "Transition Messages",
        false
      },
      {
        "Transition Co-Routines",
        false
      },
      {
        "Transition Render Methods",
        false
      },
      {
        "Canvas Holder Methods",
        false
      },
      {
        "Camera Holder Methods",
        false
      },
      {
        "Touch Manager Methods",
        false
      },
      {
        "Touch Manager Message",
        false
      },
      {
        "Code Profiler Message",
        false
      },
      {
        "Profiler Recording Message",
        false
      }
    };

    private Dictionary<string, bool> debugKeys
    {
      get
      {
        if (this._debugKeys == null)
          this._debugKeys = new Dictionary<string, bool>();
        return this._debugKeys;
      }
      set => this._debugKeys = value;
    }

    private void Awake()
    {
      if ((Object) DebugManager.self == (Object) null)
      {
        DebugManager.self = this;
        Object.DontDestroyOnLoad((Object) this);
      }
      else
        Object.DestroyImmediate((Object) this.gameObject);
    }

    public static void SetDefaultState(bool state) => DebugManager.defaultState = state;

    public static void SetStateForKey(string debugKey, bool keyState) => SerializedSingletonBehaviour<DebugManager, SerializedMonoBehaviour>.instance.debugMap.Update<string, bool>(debugKey, keyState);

    public static bool StateForKey(string debugKey)
    {
      if (!SerializedSingletonBehaviour<DebugManager, SerializedMonoBehaviour>.instance.debugMap.ContainsKey(debugKey))
        DebugManager.SetStateForKey(debugKey, DebugManager.defaultState);
      switch (SerializedSingletonBehaviour<DebugManager, SerializedMonoBehaviour>.instance.debugPrefences)
      {
        case DebugPrefences.Custom:
          return SerializedSingletonBehaviour<DebugManager, SerializedMonoBehaviour>.instance.debugMap[debugKey];
        case DebugPrefences.All:
          return true;
        case DebugPrefences.None:
          return false;
        default:
          return SerializedSingletonBehaviour<DebugManager, SerializedMonoBehaviour>.instance.debugMap[debugKey];
      }
    }
  }
}
