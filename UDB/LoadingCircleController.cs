// Decompiled with JetBrains decompiler
// Type: UDB.LoadingCircleController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class LoadingCircleController : AnimationController
  {
    private RectTransform _rectTransform;
    private float rotateSpeed = 200f;

    private RectTransform rectTransform
    {
      get
      {
        if ((Object) this._rectTransform == (Object) null)
          this._rectTransform = this.GetComponent<RectTransform>();
        return this._rectTransform;
      }
    }

    private void Update()
    {
      if (!this.isAnimating)
        return;
      this.rectTransform.Rotate(0.0f, 0.0f, this.rotateSpeed * Time.deltaTime);
    }

    protected override void AwakeAction()
    {
      if (!SingletonBehaviour<LoadingScene, MonoBehaviour>.Exists())
        return;
      LoadingScene.ClaimAsLoadingAnimation((AnimationController) this);
    }
  }
}
