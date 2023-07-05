// Decompiled with JetBrains decompiler
// Type: TB12.UI.Screens.HeroMomentFailure
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.UI.Screens
{
  public class HeroMomentFailure : UIView
  {
    [SerializeField]
    private TouchButton _retryButton;
    [SerializeField]
    private TouchButton _mainMenuButton;

    public override Enum ViewId { get; } = (Enum) EScreens.kHeroMomentFailure;

    protected override void OnInitialize()
    {
      this._laserIsAlwaysEnabled = true;
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._retryButton.Link(new Action(this.HandleRetry)),
        (EventHandle) this._mainMenuButton.Link(AppEvents.LoadMainMenu)
      });
    }

    private void HandleRetry()
    {
      UIDispatch.FrontScreen.HideView(EScreens.kHeroMomentFailure);
      ((HeroMomentGameFlow) AxisGameFlow.instance).ResetGame();
    }
  }
}
