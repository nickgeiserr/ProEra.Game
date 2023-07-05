// Decompiled with JetBrains decompiler
// Type: UDB.SplashScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class SplashScene : SingletonBehaviour<SplashScene, MonoBehaviour>
  {
    public string mainMenuScene;
    public AudioClip splashSFX;

    private void Start()
    {
    }

    private void OnEnable() => NotificationCenter<string>.AddListener("sceneSetup", new Callback<string>(this.SceneSetUp));

    private void OnDisable() => NotificationCenter<string>.RemoveListener("sceneSetup", new Callback<string>(this.SceneSetUp));

    private void SceneSetUp(string sceneSetup)
    {
      if (!sceneSetup.IsEqual(this.gameObject.scene.name))
        return;
      SpawnedSFXPlayer.Play2DAudioClip(this.splashSFX, new SFXPlayer.OnSFXFinish(this.OnSFXFinished));
    }

    private void OnSFXFinished(string sfxName, SFXPlayer sfxPlayer)
    {
      sfxPlayer.sfxFinishCallback -= new SFXPlayer.OnSFXFinish(this.OnSFXFinished);
      SceneManager.SwitchToScene(this.mainMenuScene);
    }
  }
}
