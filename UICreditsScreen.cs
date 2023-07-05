// Decompiled with JetBrains decompiler
// Type: UICreditsScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework.UI;
using System;
using TB12;
using TB12.UI;
using UnityEngine;

public class UICreditsScreen : UIView
{
  [SerializeField]
  private TouchButton _backButton;

  public override Enum ViewId => (Enum) EScreens.kCredits;

  private void OnEnable()
  {
    if (!(bool) (UnityEngine.Object) this._backButton)
      return;
    this._backButton.onClick += new System.Action(this.CloseFrontScreen);
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (!(bool) (UnityEngine.Object) this._backButton)
      return;
    this._backButton.onClick -= new System.Action(this.CloseFrontScreen);
  }

  protected override void DidAppear()
  {
    base.DidAppear();
    VRState.LocomotionEnabled.SetValue(false);
  }

  protected override void DidDisappear()
  {
    base.DidDisappear();
    VRState.LocomotionEnabled.SetValue(true);
  }

  public void CloseFrontScreen()
  {
    VRState.LocomotionEnabled.SetValue(true);
    UIDispatch.FrontScreen.CloseScreen();
  }
}
