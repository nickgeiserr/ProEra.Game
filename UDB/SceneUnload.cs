// Decompiled with JetBrains decompiler
// Type: UDB.SceneUnload
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

namespace UDB
{
  public class SceneUnload : SingletonBehaviour<SceneUnload, MonoBehaviour>
  {
    private bool unloadingScene;
    private Coroutine unloadSceneRoutine;

    private void _UnloadScene(string sceneName)
    {
      this.unloadingScene = true;
      this.unloadSceneRoutine = this.StartCoroutine(this.DoUnloadSceneWithName(sceneName));
    }

    public void _StopUnloadingScene() => this.StopCoroutine(this.unloadSceneRoutine);

    public static bool IsUnloadingScene() => SingletonBehaviour<SceneUnload, MonoBehaviour>.instance.unloadingScene;

    public static void UnloadScene(string sceneName) => SingletonBehaviour<SceneUnload, MonoBehaviour>.instance._UnloadScene(sceneName);

    public static void StopUnloadingScene() => SingletonBehaviour<SceneUnload, MonoBehaviour>.instance._StopUnloadingScene();

    private IEnumerator DoUnloadSceneWithName(string sceneName)
    {
      if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).IsValid())
      {
        AsyncOperation sync = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName));
        while (!sync.isDone)
          yield return (object) null;
        NotificationCenter<string>.Broadcast("sceneUnloaded", sceneName);
        sync = (AsyncOperation) null;
      }
      this.unloadingScene = false;
    }
  }
}
