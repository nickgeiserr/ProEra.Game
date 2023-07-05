// Decompiled with JetBrains decompiler
// Type: PlayerDisplay_RetiredPlayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay_RetiredPlayer : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow;
  [SerializeField]
  private Image playerPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI playerInfo_Txt;
  [SerializeField]
  private GameObject[] statDisplays_GO;
  [SerializeField]
  private TextMeshProUGUI[] statTitles_Txt;
  [SerializeField]
  private TextMeshProUGUI[] statValues_Txt;
  private PlayerData player;

  public void ShowWindow() => this.mainWindow.SetActive(true);

  public void HideWindow() => this.mainWindow.SetActive(false);

  public void SetData(PlayerData p)
  {
    this.player = p;
    this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(this.player);
    this.playerName_Txt.text = this.player.FirstName + "\n" + this.player.LastName;
    this.playerInfo_Txt.text = "POS: " + this.player.PlayerPosition.ToString() + "  /  AGE: " + this.player.Age.ToString();
    this.SetCareerStatHighlights(this.player);
    this.ShowWindow();
  }

  private void SetCareerStatHighlights(PlayerData player)
  {
    this.statDisplays_GO[0].SetActive(false);
    this.statDisplays_GO[1].SetActive(false);
    this.statDisplays_GO[2].SetActive(false);
    List<string> statHighlights = PlayerStats.GetStatHighlights(player.TotalCareerStats, player.PlayerPosition);
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

  public void SelectPlayer()
  {
  }
}
