// Decompiled with JetBrains decompiler
// Type: ColorImage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class ColorImage : MonoBehaviour
{
  public ColorPicker picker;
  private Image image;

  private void Awake()
  {
    this.image = this.GetComponent<Image>();
    this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
  }

  private void OnDestroy() => this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));

  private void ColorChanged(Color newColor) => this.image.color = newColor;
}
