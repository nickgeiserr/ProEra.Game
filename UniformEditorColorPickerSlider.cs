// Decompiled with JetBrains decompiler
// Type: UniformEditorColorPickerSlider
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

public class UniformEditorColorPickerSlider : MonoBehaviour
{
  private Color highlighted = new Color(0.0f, 1f, 1f);
  private Color normal = new Color(1f, 1f, 1f);
  public Text[] highlightTexts;

  public void ShowAsSelected()
  {
    for (int index = 0; index < this.highlightTexts.Length; ++index)
    {
      this.highlightTexts[index].color = this.highlighted;
      this.highlightTexts[index].fontStyle = FontStyle.Bold;
    }
  }

  public void ShowAsDeselected()
  {
    for (int index = 0; index < this.highlightTexts.Length; ++index)
    {
      this.highlightTexts[index].color = this.normal;
      this.highlightTexts[index].fontStyle = FontStyle.Normal;
    }
  }
}
