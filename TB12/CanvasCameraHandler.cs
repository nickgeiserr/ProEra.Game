// Decompiled with JetBrains decompiler
// Type: TB12.CanvasCameraHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12
{
  public class CanvasCameraHandler : MonoBehaviour
  {
    [SerializeField]
    private Canvas _canvas;

    private void OnValidate()
    {
      if (!((Object) this._canvas == (Object) null))
        return;
      this._canvas = this.GetComponent<Canvas>();
    }

    private void Awake()
    {
      if (!((Object) this._canvas != (Object) null))
        return;
      this._canvas.worldCamera = PlayerCamera.Camera;
    }
  }
}
