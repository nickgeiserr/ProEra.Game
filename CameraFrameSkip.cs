// Decompiled with JetBrains decompiler
// Type: CameraFrameSkip
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class CameraFrameSkip : MonoBehaviour
{
  [SerializeField]
  private Camera _camera;
  [SerializeField]
  private int _skipFrames = 2;

  private void Start()
  {
    if (!((Object) this._camera != (Object) null))
      return;
    this._camera.enabled = false;
  }

  private void LateUpdate()
  {
    if ((Object) this._camera == (Object) null || Time.frameCount % this._skipFrames != 0)
      return;
    this._camera.Render();
  }
}
