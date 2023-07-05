// Decompiled with JetBrains decompiler
// Type: UDB.SceneLoad
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace UDB
{
  public class SceneLoad : SerializedSingletonBehaviour<SceneLoad, SerializedMonoBehaviour>
  {
    private bool loadingScene;
    private Coroutine loadSceneRoutine;

    private void _LoadScene(string sceneName)
    {
      this.loadingScene = true;
      this.loadSceneRoutine = this.StartCoroutine(this.DoLoadSceneWithName(sceneName));
    }

    public void _StopLoadingScene() => this.StopCoroutine(this.loadSceneRoutine);

    public static bool IsLoadingScene() => SerializedSingletonBehaviour<SceneLoad, SerializedMonoBehaviour>.instance.loadingScene;

    public static void LoadScene(string sceneName) => SerializedSingletonBehaviour<SceneLoad, SerializedMonoBehaviour>.instance._LoadScene(sceneName);

    public static void StopLoadingScene() => SerializedSingletonBehaviour<SceneLoad, SerializedMonoBehaviour>.instance._StopLoadingScene();

    private IEnumerator DoLoadSceneWithName(string sceneName)
    {
      AsyncOperation sync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, SerializedSingletonBehaviour<SceneManager, SerializedMonoBehaviour>.instance.loadSceneMode);
      if (sync == null)
      {
        this.loadingScene = false;
      }
      else
      {
        while (!sync.isDone)
          yield return (object) null;
        this.loadingScene = false;
      }
    }
  }
}
