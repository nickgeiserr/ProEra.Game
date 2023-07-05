// Decompiled with JetBrains decompiler
// Type: ColorSliderImage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (RawImage))]
[ExecuteInEditMode]
public class ColorSliderImage : MonoBehaviour
{
  public ColorPicker picker;
  public ColorValues type;
  public Slider.Direction direction;
  private RawImage image;

  private RectTransform rectTransform => this.transform as RectTransform;

  private void Awake()
  {
    this.image = this.GetComponent<RawImage>();
    this.RegenerateTexture();
  }

  private void OnEnable()
  {
    if (!((UnityEngine.Object) this.picker != (UnityEngine.Object) null) || !Application.isPlaying)
      return;
    this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
    this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
  }

  private void OnDisable()
  {
    if (!((UnityEngine.Object) this.picker != (UnityEngine.Object) null))
      return;
    this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
    this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) this.image.texture != (UnityEngine.Object) null))
      return;
    UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.image.texture);
  }

  private void ColorChanged(Color newColor)
  {
    switch (this.type)
    {
      case ColorValues.R:
      case ColorValues.G:
      case ColorValues.B:
      case ColorValues.Saturation:
      case ColorValues.Value:
        this.RegenerateTexture();
        break;
    }
  }

  private void HSVChanged(float hue, float saturation, float value)
  {
    switch (this.type)
    {
      case ColorValues.R:
      case ColorValues.G:
      case ColorValues.B:
      case ColorValues.Saturation:
      case ColorValues.Value:
        this.RegenerateTexture();
        break;
    }
  }

  private void RegenerateTexture()
  {
    Color32 color32 = (Color32) ((UnityEngine.Object) this.picker != (UnityEngine.Object) null ? this.picker.CurrentColor : Color.black);
    float num = (UnityEngine.Object) this.picker != (UnityEngine.Object) null ? this.picker.H : 0.0f;
    float s = (UnityEngine.Object) this.picker != (UnityEngine.Object) null ? this.picker.S : 0.0f;
    float v = (UnityEngine.Object) this.picker != (UnityEngine.Object) null ? this.picker.V : 0.0f;
    bool flag1 = this.direction == Slider.Direction.BottomToTop || this.direction == Slider.Direction.TopToBottom;
    bool flag2 = this.direction == Slider.Direction.TopToBottom || this.direction == Slider.Direction.RightToLeft;
    int length;
    switch (this.type)
    {
      case ColorValues.R:
      case ColorValues.G:
      case ColorValues.B:
      case ColorValues.A:
        length = (int) byte.MaxValue;
        break;
      case ColorValues.Hue:
        length = 360;
        break;
      case ColorValues.Saturation:
      case ColorValues.Value:
        length = 100;
        break;
      default:
        throw new NotImplementedException("");
    }
    Texture2D texture2D = !flag1 ? new Texture2D(length, 1) : new Texture2D(1, length);
    texture2D.hideFlags = HideFlags.DontSave;
    Color32[] colors = new Color32[length];
    switch (this.type)
    {
      case ColorValues.R:
        for (byte r = 0; (int) r < length; ++r)
          colors[flag2 ? length - 1 - (int) r : (int) r] = new Color32(r, color32.g, color32.b, byte.MaxValue);
        break;
      case ColorValues.G:
        for (byte g = 0; (int) g < length; ++g)
          colors[flag2 ? length - 1 - (int) g : (int) g] = new Color32(color32.r, g, color32.b, byte.MaxValue);
        break;
      case ColorValues.B:
        for (byte b = 0; (int) b < length; ++b)
          colors[flag2 ? length - 1 - (int) b : (int) b] = new Color32(color32.r, color32.g, b, byte.MaxValue);
        break;
      case ColorValues.A:
        for (byte index = 0; (int) index < length; ++index)
          colors[flag2 ? length - 1 - (int) index : (int) index] = new Color32(index, index, index, byte.MaxValue);
        break;
      case ColorValues.Hue:
        for (int h = 0; h < length; ++h)
          colors[flag2 ? length - 1 - h : h] = (Color32) HSVUtil.ConvertHsvToRgb((double) h, 1.0, 1.0, 1f);
        break;
      case ColorValues.Saturation:
        for (int index = 0; index < length; ++index)
          colors[flag2 ? length - 1 - index : index] = (Color32) HSVUtil.ConvertHsvToRgb((double) num * 360.0, (double) index / (double) length, (double) v, 1f);
        break;
      case ColorValues.Value:
        for (int index = 0; index < length; ++index)
          colors[flag2 ? length - 1 - index : index] = (Color32) HSVUtil.ConvertHsvToRgb((double) num * 360.0, (double) s, (double) index / (double) length, 1f);
        break;
      default:
        throw new NotImplementedException("");
    }
    texture2D.SetPixels32(colors);
    texture2D.Apply();
    if ((UnityEngine.Object) this.image.texture != (UnityEngine.Object) null)
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.image.texture);
    this.image.texture = (Texture) texture2D;
    switch (this.direction)
    {
      case Slider.Direction.LeftToRight:
      case Slider.Direction.RightToLeft:
        this.image.uvRect = new Rect(0.0f, 0.0f, 1f, 2f);
        break;
      case Slider.Direction.BottomToTop:
      case Slider.Direction.TopToBottom:
        this.image.uvRect = new Rect(0.0f, 0.0f, 2f, 1f);
        break;
    }
  }
}
