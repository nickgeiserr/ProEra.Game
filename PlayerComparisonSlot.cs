// Decompiled with JetBrains decompiler
// Type: PlayerComparisonSlot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComparisonSlot : MonoBehaviour
{
  [SerializeField]
  private Image awayPlayerPortrait_Img;
  [SerializeField]
  private Image homePlayerPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI awayPlayerName_Txt;
  [SerializeField]
  private TextMeshProUGUI homePlayerName_Txt;
  [SerializeField]
  private TextMeshProUGUI awayPlayerNumber_Txt;
  [SerializeField]
  private TextMeshProUGUI homePlayerNumber_Txt;
  [SerializeField]
  private Image awayPlayerTeamBackground_Img;
  [SerializeField]
  private Image homePlayerTeamBackground_Img;
  [SerializeField]
  private Image awayPlayerTeamLogo_Img;
  [SerializeField]
  private Image homePlayerTeamLogo_Img;
  [SerializeField]
  private TextMeshProUGUI[] awayAttributeTitles_Txt;
  [SerializeField]
  private TextMeshProUGUI[] homeAttributeTitles_Txt;
  [SerializeField]
  private TextMeshProUGUI[] awayAttributeValues_Txt;
  [SerializeField]
  private TextMeshProUGUI[] homeAttributeValues_Txt;

  public void SetPlayerComparison(TeamData awayTeamData, TeamData homeTeamData, Position p)
  {
    PlayerData player1;
    PlayerData player2;
    switch (p)
    {
      case Position.QB:
        player1 = awayTeamData.TeamDepthChart.GetStartingQB();
        player2 = homeTeamData.TeamDepthChart.GetStartingQB();
        break;
      case Position.RB:
        player1 = awayTeamData.TeamDepthChart.GetStartingRB();
        player2 = homeTeamData.TeamDepthChart.GetStartingRB();
        break;
      default:
        player1 = awayTeamData.TeamDepthChart.GetStartingWRX();
        player2 = homeTeamData.TeamDepthChart.GetStartingWRX();
        break;
    }
    int[] attributeOrder = TeamData.GetAttributeOrder(p);
    this.awayPlayerTeamLogo_Img.sprite = awayTeamData.GetSmallLogo();
    this.awayPlayerTeamBackground_Img.color = awayTeamData.GetPrimaryColor();
    this.awayPlayerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player1);
    this.awayPlayerNumber_Txt.text = player1.Number.ToString();
    this.awayPlayerName_Txt.text = player1.FullName;
    this.homePlayerTeamLogo_Img.sprite = homeTeamData.GetSmallLogo();
    this.homePlayerTeamBackground_Img.color = homeTeamData.GetPrimaryColor();
    this.homePlayerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player2);
    this.homePlayerNumber_Txt.text = player2.Number.ToString();
    this.homePlayerName_Txt.text = player2.FullName;
    for (int index = 0; index < 4; ++index)
    {
      this.awayAttributeTitles_Txt[index].text = TeamData.attributeAbbreviations[attributeOrder[index]];
      this.homeAttributeTitles_Txt[index].text = TeamData.attributeAbbreviations[attributeOrder[index]];
      this.awayAttributeValues_Txt[index].text = TeamData.GetAttributeGradeFromNumber(player1.GetAttributeByIndex(attributeOrder[index])).ToString();
      this.homeAttributeValues_Txt[index].text = TeamData.GetAttributeGradeFromNumber(player2.GetAttributeByIndex(attributeOrder[index])).ToString();
    }
  }
}
