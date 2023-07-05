// Decompiled with JetBrains decompiler
// Type: TB12.UI.LeaderboardItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TB12.UI
{
  public class LeaderboardItem : RecyclingListViewItem
  {
    public TextMeshProUGUI Place;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Points;
    public Image Image;

    public void Setup(int place, int score, string text, Sprite image = null)
    {
      this.Place.text = place.ToString();
      this.Points.text = score.ToString();
      this.Name.text = text;
      this.Image.sprite = image;
    }
  }
}
