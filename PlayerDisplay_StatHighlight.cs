// Decompiled with JetBrains decompiler
// Type: PlayerDisplay_StatHighlight
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay_StatHighlight : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow_GO;
  [SerializeField]
  private TextMeshProUGUI title_Txt;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
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

  public void SetTitle(string title) => this.title_Txt.text = title;

  public void SetData(
    TeamData teamData,
    PlayerStats playerStats,
    PlayerData_Basic playerData,
    bool showPositionWithName = true)
  {
    if (showPositionWithName)
      this.playerName_Txt.text = playerData.FirstInitalAndLastName + "  |  " + playerData.PlayerPosition.ToString();
    else
      this.playerName_Txt.text = playerData.FirstInitalAndLastName;
    this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(playerData.SkinColor, playerData.PortraitID);
    this.teamLogo_Img.sprite = teamData.GetSmallLogo();
    this.SetStatHighlights(playerStats, playerData.PlayerPosition);
    this.ShowWindow();
  }

  public void SetData(
    TeamData teamData,
    PlayerStats playerStats,
    PlayerData playerData,
    bool showPositionWithName = true)
  {
    if (showPositionWithName)
      this.playerName_Txt.text = playerData.FirstInitalAndLastName + "  |  " + playerData.PlayerPosition.ToString();
    else
      this.playerName_Txt.text = playerData.FirstInitalAndLastName;
    this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(playerData);
    this.teamLogo_Img.sprite = teamData.GetSmallLogo();
    this.SetStatHighlights(playerStats, playerData.PlayerPosition);
    this.ShowWindow();
  }

  private void SetStatHighlights(PlayerStats stats, Position position)
  {
    this.statDisplays_GO[0].SetActive(false);
    this.statDisplays_GO[1].SetActive(false);
    this.statDisplays_GO[2].SetActive(false);
    List<string> statHighlights = PlayerStats.GetStatHighlights(stats, position);
    int num = statHighlights.Count / 2;
    for (int index = 0; index < num; ++index)
    {
      string statTitle = statHighlights[index * 2];
      string statValue = statHighlights[index * 2 + 1];
      this.SetStatDisplay(index, statTitle, statValue);
    }
  }

  private void SetStatDisplay(int index, string statTitle, string statValue)
  {
    this.statDisplays_GO[index].SetActive(true);
    this.statTitles_Txt[index].text = statTitle;
    this.statValues_Txt[index].text = statValue;
  }
}
