// Decompiled with JetBrains decompiler
// Type: UDB.AnimatedHorizontalLayoutGroup
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class AnimatedHorizontalLayoutGroup : AnimatedLayoutGroup
  {
    public override void CalculateLayoutInputHorizontal()
    {
      base.CalculateLayoutInputHorizontal();
      this.CalcAlongAxis(0, false);
    }

    public override void CalculateLayoutInputVertical() => this.CalcAlongAxis(1, false);

    public override void SetLayoutHorizontal()
    {
      this.SetChildrenAlongAxis(0, false);
      if (!this.resizeParentToFit)
        return;
      this.rectTransform.sizeDelta = this.childCount > 0 ? new Vector2(this.preferredWidth, this.preferredHeight) : Vector2.zero;
    }

    public override void SetLayoutVertical()
    {
      this.SetChildrenAlongAxis(1, false);
      if (!this.resizeParentToFit)
        return;
      this.rectTransform.sizeDelta = this.childCount > 0 ? new Vector2(this.preferredWidth, this.preferredHeight) : Vector2.zero;
    }
  }
}
