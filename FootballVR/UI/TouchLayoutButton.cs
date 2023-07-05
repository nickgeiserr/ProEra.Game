// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchLayoutButton
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FootballVR.UI
{
  public class TouchLayoutButton : MonoBehaviour
  {
    [SerializeField]
    private TouchButton _button;
    [SerializeField]
    private ButtonText _buttonText;
    public string id;

    public event Action<string> OnButtonPress;

    public ButtonText ButtonText => this._buttonText;

    public TouchButton Button => this._button;

    private void OnEnable() => this._button.onClick += new Action(this.HandleClick);

    private void OnDisable() => this._button.onClick -= new Action(this.HandleClick);

    private void HandleClick()
    {
      bool flag = !string.IsNullOrEmpty(this.id);
      Action<string> onButtonPress = this.OnButtonPress;
      if (onButtonPress == null)
        return;
      onButtonPress(flag ? this.id : this._buttonText.text);
    }
  }
}
