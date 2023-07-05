// Decompiled with JetBrains decompiler
// Type: TeamStatRankDisplay
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamStatRankDisplay : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI title_Txt;
  [SerializeField]
  private TextMeshProUGUI rankValue_Txt;
  [SerializeField]
  private TextMeshProUGUI rankChange_Txt;
  [SerializeField]
  private Image upArrow_Img;
  [SerializeField]
  private Image downArrow_Img;
  [SerializeField]
  private Image noChange_Img;

  public void SetData(string title, int currentRank, int previousRank)
  {
    this.title_Txt.text = title;
    this.rankValue_Txt.text = Common.GetOrdinalNumberFromInt(currentRank + 1);
    this.upArrow_Img.enabled = false;
    this.downArrow_Img.enabled = false;
    this.noChange_Img.enabled = false;
    if (currentRank < previousRank)
    {
      this.upArrow_Img.enabled = true;
      this.rankChange_Txt.text = "+" + (previousRank - currentRank).ToString();
    }
    else if (currentRank == previousRank)
    {
      this.noChange_Img.enabled = true;
      this.rankChange_Txt.text = "0";
    }
    else
    {
      this.downArrow_Img.enabled = true;
      this.rankChange_Txt.text = "-" + (currentRank - previousRank).ToString();
    }
  }
}
