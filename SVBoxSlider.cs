// Decompiled with JetBrains decompiler
// Type: SVBoxSlider
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (BoxSlider), typeof (RawImage))]
[ExecuteInEditMode]
public class SVBoxSlider : MonoBehaviour
{
  public ColorPicker picker;
  private BoxSlider slider;
  private RawImage image;
  private float lastH = -1f;
  private bool listen = true;

  public RectTransform rectTransform => this.transform as RectTransform;

  private void Awake()
  {
    this.slider = this.GetComponent<BoxSlider>();
    this.image = this.GetComponent<RawImage>();
    this.RegenerateSVTexture();
  }

  private void OnEnable()
  {
    if (!Application.isPlaying || !((Object) this.picker != (Object) null))
      return;
    this.slider.onValueChanged.AddListener(new UnityAction<float, float>(this.SliderChanged));
    this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
  }

  private void OnDisable()
  {
    if (!((Object) this.picker != (Object) null))
      return;
    this.slider.onValueChanged.RemoveListener(new UnityAction<float, float>(this.SliderChanged));
    this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
  }

  private void OnDestroy()
  {
    if (!((Object) this.image.texture != (Object) null))
      return;
    Object.DestroyImmediate((Object) this.image.texture);
  }

  private void SliderChanged(float saturation, float value)
  {
    if (this.listen)
    {
      this.picker.AssignColor(ColorValues.Saturation, saturation);
      this.picker.AssignColor(ColorValues.Value, value);
    }
    this.listen = true;
  }

  private void HSVChanged(float h, float s, float v)
  {
    if ((double) this.lastH != (double) h)
    {
      this.lastH = h;
      this.RegenerateSVTexture();
    }
    if ((double) s != (double) this.slider.normalizedValue)
    {
      this.listen = false;
      this.slider.normalizedValue = s;
    }
    if ((double) v == (double) this.slider.normalizedValueY)
      return;
    this.listen = false;
    this.slider.normalizedValueY = v;
  }

  private void RegenerateSVTexture()
  {
    double h = (Object) this.picker != (Object) null ? (double) this.picker.H * 360.0 : 0.0;
    if ((Object) this.image.texture != (Object) null)
      Object.DestroyImmediate((Object) this.image.texture);
    Texture2D texture2D = new Texture2D(100, 100);
    texture2D.hideFlags = HideFlags.DontSave;
    for (int x = 0; x < 100; ++x)
    {
      Color32[] colors = new Color32[100];
      for (int index = 0; index < 100; ++index)
        colors[index] = (Color32) HSVUtil.ConvertHsvToRgb(h, (double) x / 100.0, (double) index / 100.0, 1f);
      texture2D.SetPixels32(x, 0, 1, 100, colors);
    }
    texture2D.Apply();
    this.image.texture = (Texture) texture2D;
  }
}
