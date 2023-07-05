// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.ActivatorLeaderboardItemBack
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

namespace TB12.Activator.UI
{
  public class ActivatorLeaderboardItemBack : RecyclingListViewItem
  {
    [Space]
    [SerializeField]
    private Color _bgIdleColor1;
    [SerializeField]
    private Color _bgIdleColor2;
    [SerializeField]
    private Color _bgActiveColor1;
    [SerializeField]
    private Color _bgActiveColor2;
    [Space]
    [SerializeField]
    private Sprite _catchIcon;
    [SerializeField]
    private Sprite _throwIcon;
    [SerializeField]
    private Sprite _agileIcon;
    [SerializeField]
    private Image _bgImage01;
    [SerializeField]
    private Image _bgImage02;
    [SerializeField]
    private Image _ballImage;
    public Image Image;
    public Image Logo;

    public void Setup(int type, bool highlight = false, Sprite image = null)
    {
      this.Image.sprite = image;
      this.Image.enabled = (Object) image != (Object) null;
      this._ballImage.enabled = highlight;
      this._bgImage01.color = highlight ? this._bgActiveColor1 : this._bgIdleColor1;
      this._bgImage02.color = highlight ? this._bgActiveColor2 : this._bgIdleColor2;
      if (type == 0)
      {
        this.Logo.enabled = false;
      }
      else
      {
        this.Logo.enabled = true;
        if (type == 1)
          this.Logo.sprite = this._catchIcon;
        else if (type == 2)
        {
          this.Logo.sprite = this._throwIcon;
        }
        else
        {
          if (type != 3)
            return;
          this.Logo.sprite = this._agileIcon;
        }
      }
    }
  }
}
