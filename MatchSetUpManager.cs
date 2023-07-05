// Decompiled with JetBrains decompiler
// Type: MatchSetUpManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UDB;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchSetUpManager : SingletonBehaviour<MatchSetUpManager, MonoBehaviour>
{
  public Material dayClearSky1;
  public Material dayOvercastSky;
  public Material nightClearSky;
  public Material nightOvercastSky;
  public GameObject rainFX;
  public GameObject snowFX;
  private string stadiumSceneName;

  private MatchManager matchManager => MatchManager.instance;

  private PlayersManager playersManager => MatchManager.instance.playersManager;

  private void Start()
  {
    if ((Object) PersistentData.stadiumSet != (Object) null)
    {
      this.stadiumSceneName = PersistentData.stadiumSet.sceneName;
      this.StartCoroutine(this.InitStadiumSceneLoading(this.stadiumSceneName));
    }
    else
      Debug.LogWarning((object) "CD: Not loading Axis stadium model");
  }

  private IEnumerator InitStadiumSceneLoading(string stadiumSceneName)
  {
    MatchSetUpManager matchSetUpManager = this;
    yield return (object) null;
    LoadingScreenManager.self.ResetLoadingBar();
    LoadingScreenManager.self.SetLoadingText("Loading Stadium Data...");
    AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(stadiumSceneName, LoadSceneMode.Additive);
    while (!asyncOperation.isDone)
    {
      LoadingScreenManager.self.SetLoadingBarFill(asyncOperation.progress);
      yield return (object) null;
    }
    matchSetUpManager.StartCoroutine(matchSetUpManager.SetSceneAsActive(stadiumSceneName));
  }

  private IEnumerator SetSceneAsActive(string stadiumSceneName)
  {
    yield return (object) null;
    UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName(stadiumSceneName));
    switch (PersistentData.timeOfDay)
    {
      case 0:
        switch (PersistentData.weather)
        {
          case 0:
            RenderSettings.skybox = this.dayClearSky1;
            break;
          case 1:
            RenderSettings.skybox = this.dayOvercastSky;
            this.TurnRainOn();
            RenderSettings.fog = true;
            break;
          case 2:
            RenderSettings.skybox = this.dayOvercastSky;
            this.TurnSnowOn();
            RenderSettings.fog = true;
            break;
          case 3:
            RenderSettings.skybox = this.dayOvercastSky;
            break;
        }
        break;
      case 1:
        switch (PersistentData.weather)
        {
          case 0:
            RenderSettings.skybox = this.nightClearSky;
            break;
          case 1:
            RenderSettings.skybox = this.nightOvercastSky;
            this.TurnRainOn();
            RenderSettings.fog = true;
            break;
          case 2:
            RenderSettings.skybox = this.nightOvercastSky;
            this.TurnSnowOn();
            RenderSettings.fog = true;
            break;
          case 3:
            RenderSettings.skybox = this.nightOvercastSky;
            break;
        }
        break;
    }
    LoadingScreenManager.self.HideWindowAfterDelay();
  }

  private void TurnRainOn()
  {
    if ((Object) PersistentData.stadiumSet != (Object) null)
    {
      if (!PersistentData.stadiumSet.allowsPrecipitation)
        return;
    }
    else
      Debug.LogWarning((object) "CD: Not checking Axis stadium weather");
    this.rainFX.SetActive(true);
  }

  private void TurnSnowOn()
  {
    if ((Object) PersistentData.stadiumSet != (Object) null)
    {
      if (!PersistentData.stadiumSet.allowsPrecipitation)
        return;
    }
    else
      Debug.LogWarning((object) "CD: Not checking Axis stadium weather");
    this.snowFX.SetActive(true);
  }
}
