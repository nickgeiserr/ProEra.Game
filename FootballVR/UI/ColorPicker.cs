// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.ColorPicker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using UnityEngine;

namespace FootballVR.UI
{
  public class ColorPicker : MonoBehaviour
  {
    [SerializeField]
    private float _handleOffset = -10f;
    [SerializeField]
    private ColorPickerHandle _hueHandle;
    [SerializeField]
    private ColorPickerHandle _colorHandle;
    [SerializeField]
    private ColorPickerPreset[] _presets;
    [SerializeField]
    private Texture2D _hueTexture;
    [SerializeField]
    private Material _material;
    private Color _color;
    private static readonly int MaterialColor = Shader.PropertyToID("_Color");

    public event Action<Color> OnColorChanged;

    private void Awake()
    {
      this._hueHandle.OnPositionChanged += new System.Action(this.UpdateHueHandle);
      this._colorHandle.OnPositionChanged += new System.Action(this.UpdateColorHandle);
      foreach (ColorPickerPreset preset in this._presets)
        preset.OnSelect += new Action<Color>(this.HandlePreset);
    }

    private void OnDestroy()
    {
      this._hueHandle.OnPositionChanged -= new System.Action(this.UpdateHueHandle);
      this._colorHandle.OnPositionChanged -= new System.Action(this.UpdateColorHandle);
      this._material.SetColor(ColorPicker.MaterialColor, Color.white);
      foreach (ColorPickerPreset preset in this._presets)
        preset.OnSelect -= new Action<Color>(this.HandlePreset);
    }

    private void HandlePreset(Color color) => this.SetColor(color);

    private void UpdateHueHandle()
    {
      Vector3 vector3 = this._hueHandle.position.SetX(0.0f).SetZ(this._handleOffset);
      Rect rect = this._hueHandle.rect;
      float max = rect.height / 2f;
      vector3.y = Mathf.Clamp(vector3.y, -max, max);
      this._hueHandle.position = vector3;
      this._hueHandle.color = this._color = this._hueTexture.GetPixel(0, (int) ((float) this._hueTexture.height * (float) ((double) vector3.y / (double) rect.height + 0.5)));
      this._material.SetColor(ColorPicker.MaterialColor, this._color);
      this.UpdateColorHandle();
    }

    private void UpdateColorHandle()
    {
      Vector3 position = this._colorHandle.position;
      Rect rect = this._colorHandle.rect;
      float max1 = rect.width / 2f;
      float max2 = rect.height / 2f;
      position.x = Mathf.Clamp(position.x, -max1, max1);
      position.y = Mathf.Clamp(position.y, -max2, max2);
      this._colorHandle.position = position.SetZ(this._handleOffset);
      Vector2 vector2 = new Vector2((float) ((double) position.x / (double) rect.width + 0.5), (float) ((double) position.y / (double) rect.height + 0.5));
      Color color = Color.Lerp(Color.black, Color.Lerp(Color.white, this._color, vector2.x), vector2.y);
      this._colorHandle.color = color;
      Action<Color> onColorChanged = this.OnColorChanged;
      if (onColorChanged == null)
        return;
      onColorChanged(color);
    }

    public void SetColor(Color newColor)
    {
      HsvColor hsv = HSVUtil.ConvertRgbToHsv(newColor);
      this._colorHandle.SetValue(hsv.normalizedS, hsv.normalizedV, true);
      this._hueHandle.SetValue(0.5f, hsv.normalizedH, true);
      this.UpdateHueHandle();
      this.UpdateColorHandle();
    }
  }
}
