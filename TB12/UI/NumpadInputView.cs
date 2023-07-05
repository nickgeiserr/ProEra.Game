// Decompiled with JetBrains decompiler
// Type: TB12.UI.NumpadInputView
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
  public class NumpadInputView : UIView, IDialogHandler<NumberInputRequest>
  {
    [SerializeField]
    private TouchButton _cancelButton;
    [SerializeField]
    private NumpadInputPanel _numpadPanel;
    private NumberInputRequest _request;

    public override Enum ViewId { get; } = (Enum) EScreens.kNumberInput;

    protected override void OnInitialize()
    {
      this._numpadPanel.Initialize();
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._cancelButton.Link((Action) (() => this.Hide()))
      });
    }

    public bool HandleDialog(NumberInputRequest request)
    {
      this._request = request;
      this._numpadPanel.Initialize(request.title, request.inputString);
      this._cancelButton.gameObject.SetActive(request.canCancel);
      return true;
    }

    private void HandleComplete(string inputString)
    {
      this._request.inputString = inputString;
      if (!this._request.Complete())
        return;
      this.Hide();
    }

    protected override void WillAppear()
    {
      this._numpadPanel.OnComplete += new Action<string>(this.HandleComplete);
      this._numpadPanel.Show();
    }

    protected override void WillDisappear() => this._numpadPanel.OnComplete -= new Action<string>(this.HandleComplete);

    protected override void DidDisappear()
    {
      this._request?.Cancel();
      this._request = (NumberInputRequest) null;
    }
  }
}
