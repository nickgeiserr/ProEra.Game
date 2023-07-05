// Decompiled with JetBrains decompiler
// Type: TB12.UI.CustomizeNameColorView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.UI
{
  public class CustomizeNameColorView : UIView
  {
    [SerializeField]
    private FootballVR.UI.ColorPicker _colorPicker;
    [SerializeField]
    private TouchButton _okButton;
    private PlayerCustomization _playerCustomization;

    public override Enum ViewId { get; } = (Enum) EScreens.kSetNameColor;

    protected override void OnInitialize()
    {
      this._playerCustomization = SaveManager.GetPlayerCustomization();
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) UIHandle.Link((IButton) this._okButton, (System.Action) (() => UIDispatch.FrontScreen.DisplayView(EScreens.kCustomizeMain)))
      });
    }

    protected override void WillAppear() => this._colorPicker.SetColor((Color) this._playerCustomization.NameColor);

    protected override void DidAppear() => this._colorPicker.OnColorChanged += new Action<Color>(this.HandleColor);

    protected override void WillDisappear()
    {
      this._colorPicker.OnColorChanged -= new Action<Color>(this.HandleColor);
      this._playerCustomization.SetDirty();
    }

    private void HandleColor(Color color) => this._playerCustomization.NameColor.SetValue(color);
  }
}
