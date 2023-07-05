// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UI.Screens.Shared.Customization.StaticKeyboardInput
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.UI;
using ProEra.FootballVR.UI.KeyboardUI;
using System;
using TMPro;
using UnityEngine;

namespace ProEra.Game.UI.Screens.Shared.Customization
{
  public class StaticKeyboardInput : MonoBehaviour
  {
    private bool _isFirstKeystroke = true;
    [SerializeField]
    private bool _clearTextOnNewEdit;
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
    private GameObject _keyboardParentObject;
    [SerializeField]
    private TouchKeyButton[] _buttons;
    [SerializeField]
    private TMP_InputField[] _linkedTextElements;
    [SerializeField]
    private int _currentTextElementIndex;
    public Action<int> OnSelectedIndexChange;
    [SerializeField]
    private int maxCharsCount = 16;
    private bool shiftPressed;

    public TMP_InputField Text => this._linkedTextElements[this.CurrentTextElementIndex];

    public int CurrentTextElementIndex
    {
      get => this._currentTextElementIndex;
      set
      {
        this._currentTextElementIndex = value % this._linkedTextElements.Length;
        Action<int> selectedIndexChange = this.OnSelectedIndexChange;
        if (selectedIndexChange != null)
          selectedIndexChange(this._currentTextElementIndex);
        this._isFirstKeystroke = this._clearTextOnNewEdit;
      }
    }

    private void Start()
    {
      this.ValidateInspectorBinding();
      this.ApplyLowerCaseFormat();
      this._okButton.onClick += new Action(this.HandleDone);
      this._backspaceButton.onClick += new Action(this.HandleBackspace);
      this._clearButton.onClick += new Action(this.HandleClear);
      this._spaceButton.onClick += new Action(this.HandleSpace);
      this._shiftButton.onClick += new Action(this.HandleShift);
    }

    private void OnEnable()
    {
      this.CurrentTextElementIndex = 0;
      this.DidAppear();
    }

    private void OnDisable()
    {
      this.WillDisappear();
      this.DidDisappear();
    }

    public bool HandleDialog(TextInputRequest request) => true;

    private void HandleDone() => ++this.CurrentTextElementIndex;

    private void HandleBackspace()
    {
      this._isFirstKeystroke = false;
      string text = this.Text.text;
      if (text.Length <= 0)
        return;
      this.Text.text = text.Substring(0, text.Length - 1);
    }

    private void HandleClear()
    {
      this._isFirstKeystroke = false;
      this.Text.text = string.Empty;
    }

    private void HandleSpace() => this.AppendText(" ");

    private void HandleShift()
    {
      this.shiftPressed = true;
      this.ApplyUpperCaseFormat();
    }

    protected void DidAppear()
    {
      foreach (TouchKeyButton button in this._buttons)
        button.OnButtonPress += new Action<string>(this.AppendText);
    }

    protected void WillDisappear()
    {
      foreach (TouchKeyButton button in this._buttons)
        button.OnButtonPress -= new Action<string>(this.AppendText);
    }

    protected void DidDisappear()
    {
    }

    private void AppendText(string txt)
    {
      if (this._clearTextOnNewEdit && this._isFirstKeystroke)
      {
        this._isFirstKeystroke = false;
        this.Text.text = string.Empty;
      }
      if (this.Text.text.Length >= this.maxCharsCount)
        return;
      this.Text.text += txt;
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

    private void ValidateInspectorBinding()
    {
    }
  }
}
