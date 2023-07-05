// Decompiled with JetBrains decompiler
// Type: UIScrollerButton
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

public class UIScrollerButton : MonoBehaviour
{
  public ScrollButtonDirection scrollButtonDirection;
  public Scrollbar _scrollBar;

  private Scrollbar scrollBar
  {
    get
    {
      if ((Object) this._scrollBar == (Object) null)
        this._scrollBar = this.GetComponent<Scrollbar>();
      return this._scrollBar;
    }
  }

  public void ButtonPressed()
  {
    if (this.scrollButtonDirection == ScrollButtonDirection.Up)
      ;
  }
}
