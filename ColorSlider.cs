// Decompiled with JetBrains decompiler
// Type: ColorSlider
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (Slider))]
public class ColorSlider : MonoBehaviour
{
  public ColorPicker hsvpicker;
  public ColorValues type;
  private Slider slider;
  private bool listen = true;

  private void Awake()
  {
    this.slider = this.GetComponent<Slider>();
    this.hsvpicker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
    this.hsvpicker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
    this.slider.onValueChanged.AddListener(new UnityAction<float>(this.SliderChanged));
  }

  private void OnDestroy()
  {
    this.hsvpicker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
    this.hsvpicker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
    this.slider.onValueChanged.RemoveListener(new UnityAction<float>(this.SliderChanged));
  }

  private void ColorChanged(Color newColor)
  {
    this.listen = false;
    switch (this.type)
    {
      case ColorValues.R:
        this.slider.normalizedValue = newColor.r;
        break;
      case ColorValues.G:
        this.slider.normalizedValue = newColor.g;
        break;
      case ColorValues.B:
        this.slider.normalizedValue = newColor.b;
        break;
      case ColorValues.A:
        this.slider.normalizedValue = newColor.a;
        break;
    }
  }

  private void HSVChanged(float hue, float saturation, float value)
  {
    this.listen = false;
    switch (this.type)
    {
      case ColorValues.Hue:
        this.slider.normalizedValue = hue;
        break;
      case ColorValues.Saturation:
        this.slider.normalizedValue = saturation;
        break;
      case ColorValues.Value:
        this.slider.normalizedValue = value;
        break;
    }
  }

  private void SliderChanged(float newValue)
  {
    if (this.listen)
    {
      newValue = this.slider.normalizedValue;
      this.hsvpicker.AssignColor(this.type, newValue);
    }
    this.listen = true;
  }
}
