// Decompiled with JetBrains decompiler
// Type: TeamSelectRatings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

public class TeamSelectRatings : MonoBehaviour
{
  public Image[] homeRating;
  public Image[] awayRating;
  public Text homeValue;
  public Text awayValue;

  public void SetHomeRating(short rating)
  {
    int dots = this.ConvertToDots(rating);
    for (int index = 0; index < this.homeRating.Length; ++index)
      this.homeRating[index].enabled = false;
    for (int index = 0; index < dots; ++index)
      this.homeRating[index].enabled = true;
    this.homeValue.text = rating.ToString();
  }

  public void SetAwayRating(short rating)
  {
    int dots = this.ConvertToDots(rating);
    for (int index = 0; index < this.awayRating.Length; ++index)
      this.awayRating[index].enabled = false;
    for (int index = 0; index < dots; ++index)
      this.awayRating[index].enabled = true;
    this.awayValue.text = rating.ToString();
  }

  private int ConvertToDots(short rating)
  {
    rating -= (short) 69;
    return Mathf.FloorToInt((float) rating / 5f);
  }
}
