// Decompiled with JetBrains decompiler
// Type: TB12.UI.NumpadInputPanel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using Framework.UI;
using ProEra.FootballVR.UI.KeyboardUI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class NumpadInputPanel : UIPanel
  {
    [SerializeField]
    private TextMeshProUGUI[] _titleText;
    [SerializeField]
    private TextMeshProUGUI[] _text;
    [SerializeField]
    private TouchKeyButton[] _buttons;
    private const int maxCharsCount = 2;

    public event Action<string> OnComplete;

    public VariableString InputString { get; } = new VariableString(string.Empty);

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      this.InputString.Link<string>(new Action<string>(this.HandleInputString))
    });

    public void Initialize(string title, string defaultString)
    {
      this.InputString.SetValue(defaultString ?? string.Empty);
      foreach (TextMeshProUGUI textMeshProUgui in this._titleText)
      {
        textMeshProUgui.text = title;
        textMeshProUgui.transform.parent.gameObject.SetActive(!string.IsNullOrEmpty(title));
      }
    }

    protected override void DidAppear()
    {
      foreach (TouchKeyButton button in this._buttons)
        button.OnButtonPress += new Action<string>(this.HandleButton);
    }

    protected override void WillDisappear()
    {
      foreach (TouchKeyButton button in this._buttons)
        button.OnButtonPress -= new Action<string>(this.HandleButton);
    }

    private void HandleButton(string txt)
    {
      switch (txt)
      {
        case "Clear":
          this.InputString.SetValue(string.Empty);
          break;
        case "OK":
          Action<string> onComplete = this.OnComplete;
          if (onComplete == null)
            break;
          onComplete((string) this.InputString);
          break;
        default:
          this.AppendText(txt);
          break;
      }
    }

    private void HandleInputString(string text)
    {
      if (string.IsNullOrEmpty(text))
        text = "0";
      foreach (TMP_Text tmpText in this._text)
        tmpText.text = text;
    }

    private void AppendText(string txt)
    {
      if (this.InputString.Value.Length >= 2)
        return;
      this.InputString.Value += txt;
    }

    [ContextMenu("GetButtonRefs")]
    private void GetButtonRefs() => this._buttons = this.GetComponentsInChildren<TouchKeyButton>();
  }
}
