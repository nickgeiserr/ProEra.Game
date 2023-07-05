// Decompiled with JetBrains decompiler
// Type: TB12.MaskCaptureElement
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12
{
  public class MaskCaptureElement : MonoBehaviour
  {
    [SerializeField]
    private UIGradient _gradient;
    [SerializeField]
    private UIGradient _parent;

    public void SetValues(float index)
    {
      this._gradient.m_color1 = new Color(index, 0.0f, 1f);
      this._gradient.m_color2 = new Color(index, 1f, 1f);
      this._parent.m_color1 = new Color(index, 0.0f, 0.0f);
      this._parent.m_color2 = new Color(index, 1f, 0.0f);
    }
  }
}
