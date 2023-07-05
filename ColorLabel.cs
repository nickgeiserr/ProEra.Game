// Decompiled with JetBrains decompiler
// Type: ColorLabel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class ColorLabel : MonoBehaviour
{
  public ColorPicker picker;
  public ColorValues type;
  public string prefix = "R: ";
  public float minValue;
  public float maxValue = (float) byte.MaxValue;
  public int precision;
  private Text label;

  private void Awake() => this.label = this.GetComponent<Text>();

  private void OnEnable()
  {
    if (!Application.isPlaying || !((Object) this.picker != (Object) null))
      return;
    this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
    this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
  }

  private void OnDestroy()
  {
    if (!((Object) this.picker != (Object) null))
      return;
    this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
    this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
  }

  private void ColorChanged(Color color) => this.UpdateValue();

  private void HSVChanged(float hue, float sateration, float value) => this.UpdateValue();

  private void UpdateValue()
  {
    if ((Object) this.picker == (Object) null)
      this.label.text = this.prefix + "-";
    else
      this.label.text = this.prefix + this.ConvertToDisplayString(this.minValue + this.picker.GetValue(this.type) * (this.maxValue - this.minValue));
  }

  private string ConvertToDisplayString(float value) => this.precision > 0 ? value.ToString("f " + this.precision.ToString()) : Mathf.FloorToInt(value).ToString();
}
