// Decompiled with JetBrains decompiler
// Type: ProEra.FootballVR.UI.KeyboardUI.TouchKeyButton
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System;
using UnityEngine;

namespace ProEra.FootballVR.UI.KeyboardUI
{
  public class TouchKeyButton : MonoBehaviour
  {
    [SerializeField]
    private TouchButton _button;
    [SerializeField]
    private FootballVR.UI.ButtonText _buttonText;

    public event Action<string> OnButtonPress;

    public string ButtonText
    {
      get => this._buttonText.text;
      set => this._buttonText.text = value;
    }

    public TouchButton Button => this._button;

    private void OnEnable() => this._button.onClick += new Action(this.HandleClick);

    private void OnDisable() => this._button.onClick -= new Action(this.HandleClick);

    private void HandleClick()
    {
      Action<string> onButtonPress = this.OnButtonPress;
      if (onButtonPress == null)
        return;
      onButtonPress(this._buttonText.text);
    }
  }
}
