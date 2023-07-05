// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.ColorPickerPreset
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FootballVR.UI
{
  public class ColorPickerPreset : MonoBehaviour
  {
    [SerializeField]
    private Color _color;
    [SerializeField]
    private TouchButton _button;

    public event Action<Color> OnSelect;

    private void Awake() => this._button.onClick += new Action(this.HandleClick);

    private void OnDestroy() => this._button.onClick -= new Action(this.HandleClick);

    private void HandleClick()
    {
      Action<Color> onSelect = this.OnSelect;
      if (onSelect == null)
        return;
      onSelect(this._color);
    }
  }
}
