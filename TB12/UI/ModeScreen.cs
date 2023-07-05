// Decompiled with JetBrains decompiler
// Type: TB12.UI.ModeScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.UI;
using System;
using UnityEngine;

namespace TB12.UI
{
  public class ModeScreen : UIView
  {
    [SerializeField]
    private TouchButton _soloButton;
    [SerializeField]
    private TouchButton _partyButton;
    [SerializeField]
    private TouchButton _activationButton;
    private EMode _mode;

    public override Enum ViewId { get; } = (Enum) EScreens.kSelectMode;

    protected override void DidAppear()
    {
      this._mode = EMode.kUnknown;
      this._soloButton.onClick += new Action(this.SoloHandler);
      this._partyButton.onClick += new Action(this.PartyHandler);
      this._activationButton.onClick += new Action(this.ActivationHandler);
    }

    protected override void DidDisappear()
    {
      this._soloButton.onClick -= new Action(this.SoloHandler);
      this._partyButton.onClick -= new Action(this.PartyHandler);
      this._activationButton.onClick -= new Action(this.ActivationHandler);
      if (this._mode != EMode.kUnknown)
        AppState.Mode.SetValue(this._mode);
      AppEvents.LoadMainMenu.Trigger();
    }

    [ContextMenu("SOLO")]
    private void SoloHandler() => this.Hide();

    [ContextMenu("PARTY")]
    private void PartyHandler() => this.Hide();

    [ContextMenu("ACTIVATION")]
    private void ActivationHandler() => this.Hide();
  }
}
