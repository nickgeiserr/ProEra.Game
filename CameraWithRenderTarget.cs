// Decompiled with JetBrains decompiler
// Type: CameraWithRenderTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class CameraWithRenderTarget : MonoBehaviour
{
  [SerializeField]
  private RenderTexture renderTexture;
  private Camera _camera;

  private void Awake()
  {
    this._camera = this.GetComponent<Camera>();
    if (!((Object) this._camera != (Object) null))
      return;
    this._camera.targetTexture = this.renderTexture;
  }

  private void OnDestroy()
  {
    if (!((Object) this._camera != (Object) null))
      return;
    this._camera.targetTexture = (RenderTexture) null;
    this._camera = (Camera) null;
  }
}
