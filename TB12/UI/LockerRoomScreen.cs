// Decompiled with JetBrains decompiler
// Type: TB12.UI.LockerRoomScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace TB12.UI
{
  public class LockerRoomScreen : UIScreen
  {
    public override uint id { get; } = Convert.ToUInt32((object) EScreenParent.kLockerRoom);

    protected override void Awake()
    {
      this._canvas = this.GetComponent<Canvas>();
      this._canvasGroup = this._canvas.GetComponent<CanvasGroup>();
      base.Awake();
    }

    protected override void OnViewWillAppear(UIView view)
    {
      if (!this._displaying)
        return;
      VREvents.UpdateUI.Trigger();
    }

    private void OnDisable()
    {
    }

    protected override async Task<UIView> OnTryViewAlternative(uint viewId)
    {
      LockerRoomScreen lockerRoomScreen = this;
      return await UIBank.InstantiateUI(viewId, lockerRoomScreen._canvas.transform);
    }
  }
}
