// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.ColorPickerHandle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class ColorPickerHandle : TouchHandle
  {
    [SerializeField]
    private Image _image;

    public Color color
    {
      set => this._image.color = value;
    }

    public Rect rect => this._bg.rect;

    public void SetValue(float hPos, float vPos, bool silent = false)
    {
      float width = this.rect.width;
      float height = this.rect.height;
      this.SetPosition(new Vector2((hPos - 0.5f) * width, (vPos - 0.5f) * height), silent);
    }
  }
}
