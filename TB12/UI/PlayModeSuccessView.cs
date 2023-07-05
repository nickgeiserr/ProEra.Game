// Decompiled with JetBrains decompiler
// Type: TB12.UI.PlayModeSuccessView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class PlayModeSuccessView : UIView
  {
    [SerializeField]
    private PlayModeStore _store;
    [SerializeField]
    private TouchButton _closeButton;
    [SerializeField]
    private TextMeshProUGUI _retryButtonText;
    [SerializeField]
    private TouchButton _retryButton;
    [SerializeField]
    private TextMeshProUGUI _nextButtonText;
    [SerializeField]
    private TouchButton _nextButton;
    [SerializeField]
    private TextMeshProUGUI _distanceText;
    [SerializeField]
    private TextMeshProUGUI _speedText;
    [SerializeField]
    private TextMeshProUGUI _titleText;

    public override Enum ViewId { get; } = (Enum) EScreens.kThrowSuccess;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._closeButton, new Action(this.CloseButtonHandler)),
      (EventHandle) UIHandle.Link((IButton) this._retryButton, new Action(this.RetryButtonHandler)),
      (EventHandle) UIHandle.Link((IButton) this._nextButton, new Action(this.NextButtonHandler))
    });

    private void OnEnable()
    {
      this._distanceText.text = string.Format("{0:0.0}ft", (object) this._store.distance);
      this._speedText.text = string.Format("{0:0.00}mph", (object) this._store.speed);
      bool touchdown = this._store.Touchdown;
      this._titleText.text = touchdown ? "TOUCHDOWN" : "SUCCESS";
      this._retryButton.gameObject.SetActive(touchdown);
      this._retryButtonText.gameObject.SetActive(touchdown);
      this._nextButton.gameObject.SetActive(!touchdown);
      this._nextButtonText.gameObject.SetActive(!touchdown);
    }

    private void CloseButtonHandler()
    {
      AppEvents.LoadMainMenu.Trigger();
      UIDispatch.HideAll();
    }

    private void RetryButtonHandler()
    {
      AppEvents.Retry.Trigger();
      UIDispatch.HideAll();
    }

    private void NextButtonHandler()
    {
      AppEvents.Continue.Trigger();
      UIDispatch.HideAll();
    }
  }
}
