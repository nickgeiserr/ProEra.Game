// Decompiled with JetBrains decompiler
// Type: WristDropItemScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.UI;
using System;
using TB12;
using UnityEngine;

public class WristDropItemScreen : UIView
{
  [SerializeField]
  private TouchButton _dropItemButton;

  public override Enum ViewId { get; } = (Enum) EScreens.kWristDropItem;

  public TouchButton DropButton => this._dropItemButton;

  public event System.Action OnConfirmed;

  protected override void WillAppear()
  {
  }

  protected override void DidAppear()
  {
    if (!(bool) (UnityEngine.Object) this._dropItemButton)
      return;
    this._dropItemButton.onClick += new System.Action(this.ConfirmButtonOnClick);
    this.OnConfirmed += new System.Action(((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? (Component) PersistentSingleton<GamePlayerController>.Instance.PlayerRefs.rightHandAnchor : (Component) PersistentSingleton<GamePlayerController>.Instance.PlayerRefs.leftHandAnchor).GetComponentInChildren<HandController>().DropCurrentItem);
  }

  private void ConfirmButtonOnClick()
  {
    System.Action onConfirmed = this.OnConfirmed;
    if (onConfirmed != null)
      onConfirmed();
    this.OnConfirmed = (System.Action) null;
    this.WaitAndHide(0.5f);
  }

  protected override void DidDisappear()
  {
    base.DidDisappear();
    this.DropButton.onClick -= new System.Action(this.ConfirmButtonOnClick);
  }
}
