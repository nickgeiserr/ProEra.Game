// Decompiled with JetBrains decompiler
// Type: UDB.AnimatedVerticalLayoutGroup
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class AnimatedVerticalLayoutGroup : AnimatedLayoutGroup
  {
    public override void CalculateLayoutInputHorizontal()
    {
      base.CalculateLayoutInputHorizontal();
      this.CalcAlongAxis(0, true);
    }

    public override void CalculateLayoutInputVertical() => this.CalcAlongAxis(1, true);

    public override void SetLayoutHorizontal()
    {
      this.SetChildrenAlongAxis(0, true);
      if (!this.resizeParentToFit)
        return;
      this.rectTransform.sizeDelta = this.childCount > 0 ? new Vector2(this.preferredWidth, this.preferredHeight) : Vector2.zero;
    }

    public override void SetLayoutVertical()
    {
      this.SetChildrenAlongAxis(1, true);
      if (!this.resizeParentToFit)
        return;
      this.rectTransform.sizeDelta = this.childCount > 0 ? new Vector2(this.preferredWidth, this.preferredHeight) : Vector2.zero;
    }
  }
}
