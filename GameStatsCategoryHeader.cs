// Decompiled with JetBrains decompiler
// Type: GameStatsCategoryHeader
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class GameStatsCategoryHeader : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI statCategoryTitle_Txt;
  [SerializeField]
  private TextMeshProUGUI[] statCategories_Txt;

  public void SetStatCategories(string categoryTitle, StatCategory statCategory)
  {
    this.statCategoryTitle_Txt.text = categoryTitle;
    int[] statsForCategory = PlayerStats.GetStatsForCategory(statCategory);
    for (int index = 0; index < this.statCategories_Txt.Length; ++index)
      this.statCategories_Txt[index].text = "";
    int num1 = Mathf.Min(this.statCategories_Txt.Length, statsForCategory.Length);
    int num2 = this.statCategories_Txt.Length - num1;
    int index1 = 0;
    int index2 = num2;
    while (index1 < num1)
    {
      this.statCategories_Txt[index2].text = PlayerStats.GetStatAbbreviation(statsForCategory[index1]);
      ++index1;
      ++index2;
    }
  }
}
