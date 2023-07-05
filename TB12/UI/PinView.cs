// Decompiled with JetBrains decompiler
// Type: TB12.UI.PinView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.UI;
using System;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class PinView : UIView
  {
    [SerializeField]
    private TMP_Text labelInputDisplay;
    [SerializeField]
    private TouchButton[] numbersButtons;
    [SerializeField]
    private TouchButton confirmButton;
    [SerializeField]
    private TouchButton eraseButton;
    [SerializeField]
    private TouchButton backButton;
    private string enteredValue = string.Empty;
    private int enteredCharacters;
    private const int MAX_CHARACTERS = 4;

    public override Enum ViewId { get; } = (Enum) ELockerRoomUI.kEnterPin;

    public event Action<string> OnValueChanged;

    protected override void OnInitialize()
    {
    }

    protected override void WillAppear()
    {
      this.enteredValue = "";
      this.enteredCharacters = 0;
      foreach (TouchButton numbersButton in this.numbersButtons)
      {
        if (!((UnityEngine.Object) numbersButton == (UnityEngine.Object) null))
          numbersButton.onClickInfo += new Action<TouchButton>(this.HandleNumberEnter);
      }
      this.eraseButton.onClick += new Action(this.HandleEraseButton);
      this.confirmButton.onClick += new Action(this.HandleConfirmButton);
      this.backButton.onClick += new Action(this.HandleBackButton);
      this.RefreshInputDisplay();
    }

    protected override void WillDisappear()
    {
      foreach (TouchButton numbersButton in this.numbersButtons)
      {
        if (!((UnityEngine.Object) numbersButton == (UnityEngine.Object) null))
          numbersButton.onClickInfo -= new Action<TouchButton>(this.HandleNumberEnter);
      }
      this.eraseButton.onClick -= new Action(this.HandleEraseButton);
      this.confirmButton.onClick -= new Action(this.HandleConfirmButton);
      this.backButton.onClick -= new Action(this.HandleBackButton);
      this.OnValueChanged = (Action<string>) null;
    }

    private void HandleNumberEnter(TouchButton touchButton)
    {
      if (this.enteredCharacters >= 4)
        return;
      this.enteredValue += touchButton.GetID().ToString();
      ++this.enteredCharacters;
      this.RefreshInputDisplay();
    }

    private void HandleEraseButton()
    {
      if (this.enteredValue.Length > 0)
        this.enteredValue = this.enteredValue.Remove(this.enteredValue.Length - 1, 1);
      this.enteredCharacters = Mathf.Clamp(this.enteredCharacters - 1, 0, 4);
      this.RefreshInputDisplay();
    }

    private void RefreshInputDisplay()
    {
      string str = "";
      for (int index = 0; index < 4; ++index)
        str = index < this.enteredCharacters ? str + "* " : str + "_ ";
      this.labelInputDisplay.text = str;
      if (this.enteredCharacters < 4 && this.enteredCharacters > 0)
      {
        this.confirmButton.HighlighAsSelected(false, Color.gray, Color.green);
        this.confirmButton.SetInteractible(false);
      }
      else
      {
        this.confirmButton.HighlighAsSelected(true, Color.gray, Color.green);
        this.confirmButton.SetInteractible(true);
      }
      Debug.Log((object) ("enteredValue: " + this.enteredValue + " amount " + this.enteredCharacters.ToString()));
    }

    private void HandleConfirmButton()
    {
      AppEvents.PinCodeSubmitted.Trigger(this.enteredValue);
      Action<string> onValueChanged = this.OnValueChanged;
      if (onValueChanged != null)
        onValueChanged(this.enteredValue);
      this.HandleBackButton();
    }

    private void HandleBackButton() => this.Hide();
  }
}
