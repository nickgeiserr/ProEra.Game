// Decompiled with JetBrains decompiler
// Type: UDB.AnimatedLayoutGroup
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

namespace UDB
{
  public abstract class AnimatedLayoutGroup : HorizontalOrVerticalLayoutGroup
  {
    public bool resizeParentToFit = true;
    [HideInInspector]
    private Transform _transform;
    [HideInInspector]
    private RectTransform _rectTransform;

    public new Transform transform
    {
      get
      {
        if ((Object) this._transform == (Object) null)
          this._transform = this.GetComponent<Transform>();
        return this._transform;
      }
    }

    public new RectTransform rectTransform
    {
      get
      {
        if ((Object) this._rectTransform == (Object) null)
          this._rectTransform = this.GetComponent<RectTransform>();
        return this._rectTransform;
      }
    }

    public int childCount
    {
      get
      {
        int childCount = 0;
        for (int index = 0; index < this.transform.childCount; ++index)
        {
          if (this.transform.GetChild(index).gameObject.activeSelf)
            ++childCount;
        }
        return childCount;
      }
    }
  }
}
