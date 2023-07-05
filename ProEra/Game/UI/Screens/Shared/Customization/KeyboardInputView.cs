// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UI.Screens.Shared.Customization.KeyboardInputView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using ProEra.FootballVR.UI.KeyboardUI;
using System;
using System.Collections.Generic;
using TB12;
using TMPro;
using UnityEngine;

namespace ProEra.Game.UI.Screens.Shared.Customization
{
  public class KeyboardInputView : UIView, IDialogHandler<TextInputRequest>
  {
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    private TouchButton _backspaceButton;
    [SerializeField]
    private TouchButton _clearButton;
    [SerializeField]
    private TouchButton _cancelButton;
    [SerializeField]
    private TouchButton _spaceButton;
    [SerializeField]
    private TouchButton _shiftButton;
    [SerializeField]
    private TouchKeyButton[] _buttons;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _titleText;
    private bool _firstPreset = true;
    private const int maxCharsCount = 16;
    private TextInputRequest _request;
    private bool shiftPressed;

    public override Enum ViewId { get; } = (Enum) EScreens.kKeyboardInput;

    public TextMeshProUGUI Text
    {
      get => this._text;
      set => this._text = value;
    }

    protected override void OnInitialize()
    {
      this.ApplyLowerCaseFormat();
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._okButton.Link(new Action(this.HandleDone)),
        (EventHandle) this._backspaceButton.Link(new Action(this.HandleBackspace)),
        (EventHandle) this._clearButton.Link((Action) (() => this._text.text = string.Empty)),
        (EventHandle) this._spaceButton.Link((Action) (() => this.AppendText(" "))),
        (EventHandle) this._shiftButton.Link(new Action(this.HandleShift)),
        (EventHandle) this._cancelButton.Link((Action) (() => this.Hide()))
      });
    }

    public bool HandleDialog(TextInputRequest request)
    {
      this._request = request;
      this._text.text = request.inputString ?? string.Empty;
      this._titleText.text = request.title;
      this._titleText.transform.parent.gameObject.SetActive(!string.IsNullOrEmpty(request.title));
      this._cancelButton.gameObject.SetActive(request.canCancel);
      return true;
    }

    private void HandleDone()
    {
      this._request.inputString = this._text.text;
      if (!this._request.Complete())
        return;
      this.Hide();
    }

    private void HandleBackspace()
    {
      string text = this._text.text;
      if (text.Length <= 0)
        return;
      this._text.text = text.Substring(0, text.Length - 1);
    }

    private void HandleShift()
    {
      this.shiftPressed = true;
      this.ApplyUpperCaseFormat();
    }

    protected override void DidAppear()
    {
      foreach (TouchKeyButton button in this._buttons)
        button.OnButtonPress += new Action<string>(this.AppendText);
    }

    protected override void WillDisappear()
    {
      foreach (TouchKeyButton button in this._buttons)
        button.OnButtonPress -= new Action<string>(this.AppendText);
    }

    protected override void DidDisappear()
    {
      this._request?.Cancel();
      this._request = (TextInputRequest) null;
    }

    private void AppendText(string txt)
    {
      if (this._firstPreset)
      {
        this._text.text = string.Empty;
        this._firstPreset = false;
      }
      if (this._text.text.Length >= 16)
        return;
      this._text.text += txt;
      if (!this.shiftPressed)
        return;
      this.shiftPressed = false;
      this.ApplyLowerCaseFormat();
    }

    private void ApplyLowerCaseFormat()
    {
      foreach (TouchKeyButton button in this._buttons)
        button.ButtonText = button.ButtonText.ToLowerInvariant();
    }

    private void ApplyUpperCaseFormat()
    {
      foreach (TouchKeyButton button in this._buttons)
        button.ButtonText = button.ButtonText.ToUpperInvariant();
    }

    [ContextMenu("GetButtonRefs")]
    public void GetButtonRefs() => this._buttons = this.GetComponentsInChildren<TouchKeyButton>();
  }
}
