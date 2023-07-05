// Decompiled with JetBrains decompiler
// Type: TB12.UI.SuccessScreen
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
using Vars;

namespace TB12.UI
{
  public class SuccessScreen : UIView
  {
    [SerializeField]
    private Transform _retryOnlyTx;
    [SerializeField]
    private Transform _retryNextTx;
    [SerializeField]
    private TouchButton _retryButton;
    [SerializeField]
    private TouchButton _backButton;
    [SerializeField]
    private TouchButton _nextPlayButton;
    [SerializeField]
    private ThrowSuccessPanel _throwSuccessPanel;
    [SerializeField]
    private PassSuccessPanel _passSuccessPanel;
    [SerializeField]
    private CatchSuccessPanel _catchSuccessPanel;
    [SerializeField]
    private TrainingCampSuccessPanel _trainingCampSuccessPanel;
    private List<SuccessPanel> _panels = new List<SuccessPanel>();

    public override Enum ViewId { get; } = (Enum) EScreens.kCompletedSuccessfully;

    protected override void OnInitialize()
    {
      this._panels = new List<SuccessPanel>()
      {
        (SuccessPanel) this._throwSuccessPanel,
        (SuccessPanel) this._passSuccessPanel,
        (SuccessPanel) this._catchSuccessPanel,
        (SuccessPanel) this._trainingCampSuccessPanel
      };
      foreach (UIPanel panel in this._panels)
        panel.Initialize();
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) UIHandle.Link((IButton) this._backButton, AppEvents.LoadMainMenu),
        (EventHandle) UIHandle.Link((IButton) this._retryButton, (AppEvent) AppEvents.Retry),
        (EventHandle) UIHandle.Link((IButton) this._nextPlayButton, AppEvents.Continue)
      });
    }

    private SuccessPanel GetPanel()
    {
      switch (AppState.GameMode)
      {
        case EGameMode.kCatch:
          return (SuccessPanel) this._catchSuccessPanel;
        case EGameMode.kTrainingCamp:
          return (SuccessPanel) this._trainingCampSuccessPanel;
        case EGameMode.kPass:
          return (SuccessPanel) this._passSuccessPanel;
        default:
          return (SuccessPanel) this._throwSuccessPanel;
      }
    }

    protected override void WillAppear()
    {
      SuccessPanel panel = this.GetPanel();
      if ((UnityEngine.Object) panel != (UnityEngine.Object) null)
        panel.Show();
      this.SetupButtons(panel.canContinue);
    }

    protected override void WillDisappear()
    {
      foreach (UIPanel panel in this._panels)
        panel.Hide();
    }

    private void SetupButtons(bool canContinue)
    {
      this._nextPlayButton.gameObject.SetActive(canContinue);
      this._retryButton.transform.SetParentAndReset(canContinue ? this._retryNextTx : this._retryOnlyTx);
    }

    [ContextMenu("Continue")]
    private void TriggerContinueButton() => this._nextPlayButton.SimulateClick();
  }
}
