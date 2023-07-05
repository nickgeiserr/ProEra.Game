// Decompiled with JetBrains decompiler
// Type: TB12.UI.Screens.FrontScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace TB12.UI.Screens
{
  public class FrontScreen : UIScreen
  {
    public override uint id { get; } = Convert.ToUInt32((object) EScreenParent.kFront);

    protected override void Awake()
    {
      this._canvas = UIAnchoring.FrontCanvas;
      this._canvasGroup = this._canvas.GetComponent<CanvasGroup>();
      base.Awake();
      this.gameObject.SetActive(false);
    }

    protected override void OnViewWillAppear(UIView view)
    {
      Transform transform = view.transform;
      transform.SetParent(UIAnchoring.FrontCanvas.transform);
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      transform.localScale = Vector3.one;
    }

    protected override void OnViewHasDisappeared(UIView view) => view.transform.SetParent(this.transform);

    protected override async Task<UIView> OnTryViewAlternative(uint viewId)
    {
      FrontScreen frontScreen = this;
      return await UIBank.InstantiateUI(viewId, frontScreen._canvas.transform);
    }

    private void OnDisable()
    {
    }
  }
}
