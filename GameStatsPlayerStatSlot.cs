// Decompiled with JetBrains decompiler
// Type: GameStatsPlayerStatSlot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class GameStatsPlayerStatSlot : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI[] statValues_Txt;

  public void SetName(string name) => this.playerName_Txt.text = name;

  public void SetStatValues(PlayerStats playerStats, StatCategory statCategory)
  {
    int[] statsForCategory = PlayerStats.GetStatsForCategory(statCategory);
    int num1 = Mathf.Min(this.statValues_Txt.Length, statsForCategory.Length);
    int num2 = this.statValues_Txt.Length - num1;
    int index1 = 0;
    int index2 = num2;
    while (index1 < num1)
    {
      this.statValues_Txt[index2].text = playerStats.GetStatByIndex(statsForCategory[index1]);
      ++index1;
      ++index2;
    }
  }

  public void ClearTextFields()
  {
    this.playerName_Txt.text = "";
    for (int index = 0; index < this.statValues_Txt.Length; ++index)
      this.statValues_Txt[index].text = "";
  }
}
