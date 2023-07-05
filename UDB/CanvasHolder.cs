// Decompiled with JetBrains decompiler
// Type: UDB.CanvasHolder
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class CanvasHolder : MonoBehaviour
  {
    public bool enableCanvasOnActive;
    public Canvas _canvasComponent;

    public Canvas canvasComponent
    {
      get
      {
        if ((Object) this._canvasComponent == (Object) null)
          this._canvasComponent = this.GetComponent<Canvas>();
        return this._canvasComponent;
      }
    }

    private void Awake()
    {
      if (!SceneTransitionPlayer.Exists() || !((Object) this.canvasComponent != (Object) null) || !this.enableCanvasOnActive)
        return;
      this.canvasComponent.enabled = false;
    }

    private void OnEnable() => NotificationCenter<string>.AddListener("activateScene", new Callback<string>(this.SceneActivatedCallback));

    private void OnDisable() => NotificationCenter<string>.RemoveListener("activateScene", new Callback<string>(this.SceneActivatedCallback));

    private void SceneActivatedCallback(string activatedSceneName)
    {
      if (!activatedSceneName.Equals(this.gameObject.scene.name))
        return;
      this.AcivateScene();
    }

    private void AcivateScene()
    {
      if (DebugManager.StateForKey("Canvas Holder Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + this.gameObject.scene.name));
      if (!((Object) this.canvasComponent != (Object) null))
        return;
      this.canvasComponent.enabled = true;
    }
  }
}
