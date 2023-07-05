// Decompiled with JetBrains decompiler
// Type: UDB.MainMenuScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class MainMenuScene : SerializedMonoBehaviour, ISceneSpecific
  {
    public Canvas _canvas;
    public string _sceneName;

    public Canvas canvas
    {
      get
      {
        if ((Object) this._canvas == (Object) null)
          this._canvas = this.GetComponent<Canvas>();
        return this._canvas;
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

    private void OnEnable() => NotificationCenter<string>.AddListener("claimSceneController", new Callback<string>(this.ClaimSceneControllerCallback));

    private void OnDisable() => NotificationCenter<string>.RemoveListener("claimSceneController", new Callback<string>(this.ClaimSceneControllerCallback));

    private void ClaimSceneControllerCallback(string sceneName)
    {
      if (!sceneName.IsEqual(this.sceneName))
        return;
      if (DebugManager.StateForKey("LoadingSceneController Callbacks"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + sceneName));
      SerializedSceneSingletonBehaviour<SceneController, SerializedMonoBehaviour>.InstanceOfScene(sceneName).Claim((ISceneSpecific) this);
    }

    public void StartScene() => this.canvas.enabled = true;

    public void EndScene() => this.canvas.enabled = false;
  }
}
