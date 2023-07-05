// Decompiled with JetBrains decompiler
// Type: PlayerDisplay_Award
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay_Award : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow_GO;
  [SerializeField]
  private TextMeshProUGUI awardTitle_Txt;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI playerInfo_Txt;
  [SerializeField]
  private Image teamLogo_Img;
  [SerializeField]
  private Image playerPortrait_Img;
  [SerializeField]
  private GameObject[] statDisplays_GO;
  [SerializeField]
  private TextMeshProUGUI[] statTitles_Txt;
  [SerializeField]
  private TextMeshProUGUI[] statValues_Txt;

  public void ShowWindow() => this.mainWindow_GO.SetActive(true);

  public void HideWindow() => this.mainWindow_GO.SetActive(false);

  public void ShowOnlyAwardTitle(AwardType awardType)
  {
    this.awardTitle_Txt.text = Common.EnumToString(awardType.ToString());
    this.playerName_Txt.text = "";
    this.playerInfo_Txt.text = "";
    this.teamLogo_Img.enabled = false;
    this.playerPortrait_Img.enabled = false;
    this.statDisplays_GO[0].SetActive(false);
    this.statDisplays_GO[1].SetActive(false);
    this.statDisplays_GO[2].SetActive(false);
  }

  public void SetData(Award award)
  {
    this.awardTitle_Txt.text = Common.EnumToString(award.awardType.ToString());
    this.playerName_Txt.text = award.playerFullName;
    this.playerInfo_Txt.text = award.position.ToString() + "  |  #" + award.playerNumber.ToString() + "  |  " + award.teamAbbreviation;
    this.teamLogo_Img.enabled = true;
    this.playerPortrait_Img.enabled = true;
    this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(award.skinValue, award.playerPortrait);
    this.statDisplays_GO[0].SetActive(false);
    this.statDisplays_GO[1].SetActive(false);
    this.statDisplays_GO[2].SetActive(false);
    int num = award.statHighlights.Length / 2;
    for (int index = 0; index < num; ++index)
    {
      string statHighlight1 = award.statHighlights[index * 2];
      string statHighlight2 = award.statHighlights[index * 2 + 1];
      this.SetStatDisplay(index, statHighlight1, statHighlight2);
    }
    this.ShowWindow();
  }

  private void SetStatDisplay(int index, string statTitle, string statValue)
  {
    this.statDisplays_GO[index].SetActive(true);
    this.statTitles_Txt[index].text = statTitle;
    this.statValues_Txt[index].text = statValue;
  }
}
