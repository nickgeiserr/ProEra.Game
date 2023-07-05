// Decompiled with JetBrains decompiler
// Type: ColorPresets
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPresets : MonoBehaviour
{
  public ColorPicker picker;
  public GameObject[] presets;
  public Image createPresetImage;

  private void Awake() => this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));

  public void CreatePresetButton()
  {
    for (int index = 0; index < this.presets.Length; ++index)
    {
      if (!this.presets[index].activeSelf)
      {
        this.presets[index].SetActive(true);
        this.presets[index].GetComponent<Image>().color = this.picker.CurrentColor;
        break;
      }
    }
  }

  public void PresetSelect(Image sender) => this.picker.CurrentColor = sender.color;

  private void ColorChanged(Color color) => this.createPresetImage.color = color;
}
