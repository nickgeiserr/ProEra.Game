// Decompiled with JetBrains decompiler
// Type: ColorPickerTester
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;

public class ColorPickerTester : MonoBehaviour
{
  public Renderer renderer;
  public ColorPicker picker;

  private void Start()
  {
    this.picker.onValueChanged.AddListener((UnityAction<Color>) (color => this.renderer.material.color = color));
    this.renderer.material.color = this.picker.CurrentColor;
  }

  private void Update()
  {
  }
}
