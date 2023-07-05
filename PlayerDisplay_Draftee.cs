// Decompiled with JetBrains decompiler
// Type: PlayerDisplay_Draftee
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay_Draftee : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow_GO;
  [SerializeField]
  private GameObject drafteeBanner_GO;
  [SerializeField]
  private GameObject teamBanner_GO;
  [SerializeField]
  private Image teamBanner_Img;
  [SerializeField]
  private TextMeshProUGUI teamAbbrev_Txt;
  [SerializeField]
  private TextMeshProUGUI playerNumber_Txt;
  [SerializeField]
  private Image playerPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI playerInfo_Txt;
  [SerializeField]
  private TextMeshProUGUI overallTitle_Txt;
  [SerializeField]
  private TextMeshProUGUI overallValue_Txt;

  public void ShowWindow() => this.mainWindow_GO.SetActive(true);

  public void HideWindow() => this.mainWindow_GO.SetActive(false);

  public void SetReplacementPlayerData(PlayerData player, bool showEstimatedOverall, TeamData team = null)
  {
    if (team == null)
    {
      this.drafteeBanner_GO.SetActive(true);
      this.teamBanner_GO.SetActive(false);
    }
    else
    {
      this.drafteeBanner_GO.SetActive(false);
      this.teamBanner_GO.SetActive(true);
      this.teamBanner_Img.color = team.GetPrimaryColor();
      this.teamAbbrev_Txt.text = team.GetAbbreviation();
      this.playerNumber_Txt.text = player.Number.ToString();
    }
    this.playerName_Txt.text = player.FirstName.ToUpper() + "\n" + player.LastName.ToUpper();
    this.playerInfo_Txt.text = "POS: " + player.PlayerPosition.ToString() + "  /  AGE: " + player.Age.ToString();
    this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player);
    if (showEstimatedOverall)
    {
      this.overallTitle_Txt.text = "ESTIMATED\nOVERALL";
      this.overallValue_Txt.text = TeamData.GetAttributeGradeFromNumber(player.EstimatedOverall);
    }
    else
    {
      this.overallTitle_Txt.text = "OVERALL";
      this.overallValue_Txt.text = TeamData.GetAttributeGradeFromNumber(player.GetOverall());
    }
    this.ShowWindow();
  }

  public void SetUndraftedPlayerData(TeamData teamData, int overallPickNumber, int pickInRound)
  {
    this.drafteeBanner_GO.SetActive(true);
    this.teamBanner_GO.SetActive(false);
    this.playerPortrait_Img.sprite = teamData.GetSmallLogo();
    this.playerName_Txt.text = "OVERALL PICK  #" + (overallPickNumber + 1).ToString();
    this.playerInfo_Txt.text = "PICK IN ROUND  #" + (pickInRound + 1).ToString();
    this.overallTitle_Txt.text = "";
    this.overallValue_Txt.text = "";
    this.ShowWindow();
  }

  public void SetDraftedPlayerData(PlayerData player, int overallPickNumber)
  {
    this.drafteeBanner_GO.SetActive(true);
    this.teamBanner_GO.SetActive(false);
    if (player == null)
    {
      this.playerPortrait_Img.enabled = false;
      this.playerName_Txt.text = "";
      this.playerInfo_Txt.text = "";
      this.overallTitle_Txt.text = "";
      this.overallValue_Txt.text = "";
    }
    else
    {
      this.playerPortrait_Img.enabled = true;
      this.playerName_Txt.text = player.FirstName.ToUpper() + "\n" + player.LastName.ToUpper();
      this.playerInfo_Txt.text = "POS: " + player.PlayerPosition.ToString() + "  /  PICK #" + (overallPickNumber + 1).ToString();
      this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player);
      this.overallTitle_Txt.text = "OVERALL";
      this.overallValue_Txt.text = TeamData.GetAttributeGradeFromNumber(player.GetOverall());
    }
    this.ShowWindow();
  }

  public void ShowUnknownPickPosition(TeamData teamData)
  {
    this.drafteeBanner_GO.SetActive(true);
    this.teamBanner_GO.SetActive(false);
    this.playerPortrait_Img.sprite = teamData.GetSmallLogo();
    this.playerName_Txt.text = "PICK TBD";
    this.playerInfo_Txt.text = "";
    this.overallTitle_Txt.text = "";
    this.overallValue_Txt.text = "";
    this.ShowWindow();
  }
}
