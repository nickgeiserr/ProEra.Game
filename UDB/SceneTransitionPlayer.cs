// Decompiled with JetBrains decompiler
// Type: UDB.SceneTransitionPlayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class SceneTransitionPlayer : TransitionPlayer
  {
    public static SceneTransitionPlayer instance;
    private static bool startInTransition;
    private static bool startOutTransition;
    public Camera _toCamera;
    public Camera _fromCamera;

    public static bool isPlayingInTransition => SceneTransitionPlayer.instance.playingInTransition;

    public static bool isPlayingOutTransition => SceneTransitionPlayer.instance.playingOutTransition;

    public static Camera toCamera => SceneTransitionPlayer.instance._toCamera;

    public static Camera fromCamera => SceneTransitionPlayer.instance._fromCamera;

    private void Awake()
    {
      if ((Object) SceneTransitionPlayer.instance == (Object) null)
      {
        SceneTransitionPlayer.instance = this;
        Object.DontDestroyOnLoad((Object) this);
      }
      else
        Object.DestroyImmediate((Object) this.gameObject);
    }

    private void _Attach(Transform transfrom) => transfrom.SetParent(this.transform);

    private static void CreateIfNecessary()
    {
      if (SceneTransitionPlayer.Exists())
        return;
      SceneTransitionPlayer.Create();
    }

    public static bool Exists() => (Object) SceneTransitionPlayer.instance != (Object) null;

    public static void Create()
    {
      Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      if (DebugManager.StateForKey("SceneTransitionObject Methods"))
        Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      if (SceneTransitionPlayer.Exists())
      {
        if (!DebugManager.StateForKey("SceneTransitionObject Warnings"))
          return;
        Debug.LogWarning((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name + ": Instance already created"));
      }
      else
      {
        GameObject gameObject = new GameObject();
        gameObject.name = nameof (SceneTransitionPlayer);
        SceneTransitionPlayer.instance = gameObject.AddComponent<SceneTransitionPlayer>();
      }
    }

    public static void WaitOutSceneTransition() => SceneTransitionPlayer.instance.WaitOutTransition();

    public static void WaitInSceneTransition() => SceneTransitionPlayer.instance.WaitInTransition();

    public static void PrepareOutSceneTransition() => SceneTransitionPlayer.instance.PrepareOutTransition();

    public static void PrepareInSceneTransition() => SceneTransitionPlayer.instance.PrepareInTransition();

    public static bool PlayOutLoadingTransition()
    {
      if (!SceneTransitionPlayer.startOutTransition)
      {
        NotificationCenter.Broadcast("AddOutLoadingTransitions");
        SceneTransitionPlayer.instance.PlayOutTransition();
        SceneTransitionPlayer.startOutTransition = true;
      }
      if (!SceneTransitionPlayer.instance.playingOutTransition && SceneTransitionPlayer.startOutTransition)
        SceneTransitionPlayer.startOutTransition = false;
      return SceneTransitionPlayer.isPlayingOutTransition;
    }

    public static bool PlayOutSceneTransition()
    {
      if (!SceneTransitionPlayer.startOutTransition)
      {
        NotificationCenter.Broadcast("AddOutSceneTransitions");
        SceneTransitionPlayer.instance.PlayOutTransition();
        SceneTransitionPlayer.startOutTransition = true;
      }
      if (!SceneTransitionPlayer.instance.playingOutTransition && SceneTransitionPlayer.startOutTransition)
        SceneTransitionPlayer.startOutTransition = false;
      return SceneTransitionPlayer.isPlayingOutTransition;
    }

    public static bool PlayInSceneTransition()
    {
      if (!SceneTransitionPlayer.startInTransition)
      {
        SceneTransitionPlayer.instance.PlayInTransition();
        SceneTransitionPlayer.startInTransition = true;
      }
      if (!SceneTransitionPlayer.instance.playingInTransition && SceneTransitionPlayer.startInTransition)
        SceneTransitionPlayer.startInTransition = false;
      return SceneTransitionPlayer.isPlayingInTransition;
    }

    public static void Attach(Transform transfrom)
    {
      Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      if (DebugManager.StateForKey("SceneTransitionObject Methods"))
        Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      SceneTransitionPlayer.CreateIfNecessary();
      SceneTransitionPlayer.instance._Attach(transfrom);
    }

    public static void AttachFromCamera(Camera camera)
    {
      Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      if (DebugManager.StateForKey("SceneTransitionObject Methods"))
        Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      SceneTransitionPlayer.CreateIfNecessary();
      SceneTransitionPlayer.instance._fromCamera = camera;
    }

    public static void AttachToCamera(Camera camera)
    {
      Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      if (DebugManager.StateForKey("SceneTransitionObject Methods"))
        Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      SceneTransitionPlayer.CreateIfNecessary();
      SceneTransitionPlayer.instance._toCamera = camera;
    }

    public static void AddAsInSceneTransition(Transition transition)
    {
      Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      SceneTransitionPlayer.CreateIfNecessary();
      SceneTransitionPlayer.instance.AddAsInTransition(transition);
    }

    public static void AddAsOutSceneTransition(Transition transition)
    {
      Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      SceneTransitionPlayer.CreateIfNecessary();
      SceneTransitionPlayer.instance.AddAsOutTransition(transition);
    }

    public static void RemoveInSceneTransition(Transition transition)
    {
      Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      SceneTransitionPlayer.CreateIfNecessary();
      SceneTransitionPlayer.instance.RemoveInTransition(transition);
    }

    public static void RemoveOutSceneTransition(Transition transition)
    {
      Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      SceneTransitionPlayer.CreateIfNecessary();
      SceneTransitionPlayer.instance.RemoveOutTransition(transition);
    }

    public static void Destroy()
    {
      Debug.Log((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name));
      if (SceneTransitionPlayer.Exists())
      {
        Object.DestroyImmediate((Object) SceneTransitionPlayer.instance.gameObject);
      }
      else
      {
        if (!DebugManager.StateForKey("SceneTransitionObject Warnings"))
          return;
        Debug.LogWarning((object) ("SceneTransitionPlayer at " + MethodBase.GetCurrentMethod().Name + ": Cant destroy becasue instance isnt created"));
      }
    }
  }
}
