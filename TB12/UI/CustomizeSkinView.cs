// Decompiled with JetBrains decompiler
// Type: TB12.UI.CustomizeSkinView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using System;
using UnityEngine;

namespace TB12.UI
{
  public class CustomizeSkinView : MonoBehaviour
  {
    [SerializeField]
    private ColorPicker _colorPicker;
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    private PlayerProfile _playerProfile;

    private void Start()
    {
    }

    private void OnEnable()
    {
      this.WillAppear();
      this.DidAppear();
    }

    private void OnDisable() => this.WillDisappear();

    private void WillAppear() => this._colorPicker.SetColor(this._playerProfile.Customization.WristbandColor.Value);

    private void DidAppear() => this._colorPicker.OnColorChanged += new Action<Color>(this.HandleColor);

    private void WillDisappear()
    {
      this._colorPicker.OnColorChanged -= new Action<Color>(this.HandleColor);
      this._playerProfile.Customization.SetDirty();
    }

    private void HandleColor(Color color) => this._playerProfile.Customization.WristbandColor.SetValue(color);
  }
}
