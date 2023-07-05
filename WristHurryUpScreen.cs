// Decompiled with JetBrains decompiler
// Type: WristHurryUpScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.UI;
using System;
using TB12;
using UnityEngine;

public class WristHurryUpScreen : UIView
{
  [SerializeField]
  private TouchButton _hurryupButton;
  [SerializeField]
  private Color _hurryupDisabledColor;
  [SerializeField]
  private Color _hurryupEnabledColor;

  public override Enum ViewId { get; } = (Enum) EScreens.kWristHurryUp;

  public TouchButton HurryUpButton => this._hurryupButton;

  public Color HurryUpDisabledColor => this._hurryupDisabledColor;

  public Color HurryUpEnabledColor => this._hurryupEnabledColor;

  protected override void DidAppear()
  {
    base.DidAppear();
    this.HurryUpButton.onClick += new System.Action(this.HurryUpButtonOnClick);
    if (!(bool) (UnityEngine.Object) this._hurryupButton)
      return;
    bool selected = Game.CanUserRunHurryUp();
    this._hurryupButton.HighlighAsSelected(selected, this._hurryupDisabledColor, this._hurryupEnabledColor);
    this._hurryupButton.SetInteractible(selected);
  }

  private void HurryUpButtonOnClick()
  {
    AppSounds.PlayVO(EVOTypes.kHurryUp);
    MatchManager.instance.playManager.RunHurryUp();
    this._hurryupButton.SetInteractible(false);
    this.HurryUpButton.onClick -= new System.Action(this.HurryUpButtonOnClick);
  }

  protected override void DidDisappear()
  {
    base.DidDisappear();
    this.HurryUpButton.onClick -= new System.Action(this.HurryUpButtonOnClick);
  }
}
