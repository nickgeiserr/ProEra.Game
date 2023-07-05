// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.ActivatorLeaderboardItemFront
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

namespace TB12.Activator.UI
{
  public class ActivatorLeaderboardItemFront : RecyclingListViewItem
  {
    [SerializeField]
    private Color _firstPlaceTextColor;
    [SerializeField]
    private Color _simpleTextColor;
    [SerializeField]
    private Color _activeTextColor;
    [SerializeField]
    private bool _applyFirstPlaceColor = true;
    public TextMeshProUGUI Place;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Points;

    public void Setup(int place, int score, string text, bool highlight = false)
    {
      Color color = highlight ? this._activeTextColor : this._simpleTextColor;
      switch (place)
      {
        case 1:
          this.Place.text = "1<sup>st</sup>";
          if (this._applyFirstPlaceColor)
          {
            color = this._firstPlaceTextColor;
            break;
          }
          break;
        case 2:
          this.Place.text = "2<sup>nd</sup>";
          break;
        case 3:
          this.Place.text = "3<sup>rd</sup>";
          break;
        default:
          this.Place.text = place.ToString();
          break;
      }
      this.Points.text = score.ToString();
      this.Name.text = text;
      this.Place.color = this.Name.color = this.Points.color = color;
    }
  }
}
