// Decompiled with JetBrains decompiler
// Type: MiniGameRankingsPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections.Generic;
using TB12;
using TB12.Activator.UI;
using TB12.Solo.Data;
using TMPro;
using UnityEngine;

public class MiniGameRankingsPage : TabletPage
{
  [SerializeField]
  private GameObject _listItem;
  [SerializeField]
  private SoloLeaderboardData _soloLeaderboardData;
  [SerializeField]
  private TextMeshProUGUI _headerText;
  [SerializeField]
  private RectTransform _contentHolder;
  [SerializeField]
  private TextMeshProUGUI _statusText;
  [SerializeField]
  private TouchUI2DButton _backButton;
  private EGameMode _miniGameType;
  private string[] _headerNames = new string[2]
  {
    "Passing Challenge",
    "Agility Drills"
  };

  private void OnDisable()
  {
    this._contentHolder.DestroyAllChildren();
    if ((UnityEngine.Object) this._backButton != (UnityEngine.Object) null)
      this._backButton.onClick -= new System.Action(this.HandleBackButton);
    if (!((UnityEngine.Object) this._soloLeaderboardData != (UnityEngine.Object) null))
      return;
    this._soloLeaderboardData.onLevelDataUpdated -= new Action<string>(this.UpdateLeaderboard);
    this._soloLeaderboardData.onDataError -= new Action<string, SoloLeaderboardData.LeaderboardErrors>(this.HandleDataError);
  }

  private void OnEnable()
  {
    this._backButton.onClick += new System.Action(this.HandleBackButton);
    this._soloLeaderboardData.onLevelDataUpdated += new Action<string>(this.UpdateLeaderboard);
    this._soloLeaderboardData.onDataError += new Action<string, SoloLeaderboardData.LeaderboardErrors>(this.HandleDataError);
    this._statusText.text = "Loading";
    this._statusText.gameObject.SetActive(true);
    this._miniGameType = (this.MainPage as MainMenuPage).MiniGameType;
    TextMeshProUGUI headerText = this._headerText;
    string str;
    switch (this._miniGameType)
    {
      case EGameMode.kThrow:
        str = this._headerNames[0];
        break;
      case EGameMode.kAgility:
        str = this._headerNames[1];
        break;
      case EGameMode.kPass:
        str = this._headerNames[0];
        break;
      default:
        str = this._headerText.text;
        break;
    }
    headerText.text = str;
    this._soloLeaderboardData.GetLevelTop(ASummaryScreen.GetLeaderURLByGameType(this._miniGameType));
  }

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MiniGamesRankingMenu);

  private void UpdateLeaderboard(string levelName)
  {
    List<SoloLeaderboardData.Entry> entries = this._soloLeaderboardData.GetEntries(levelName);
    if (entries == null || entries.Count == 0)
    {
      Debug.LogError((object) ("No entries found for " + levelName + ". Aborting.."));
      this._statusText.text = "No scores for this minigame found";
      for (int index = 0; index < this._contentHolder.childCount; ++index)
        this._contentHolder.GetChild(index).gameObject.SetActive(false);
    }
    else
    {
      this._statusText.gameObject.SetActive(false);
      int num = -1;
      for (int index1 = 0; index1 < entries.Count; ++index1)
      {
        if (this._contentHolder.childCount <= index1)
          UnityEngine.Object.Instantiate<GameObject>(this._listItem, (Transform) this._contentHolder).GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.1f);
        Transform child = this._contentHolder.GetChild(index1);
        child.gameObject.SetActive(true);
        int index2 = index1 + 1;
        string str;
        if (index2 < entries.Count)
        {
          if (entries[index1].score == entries[index2].score)
          {
            if (num == -1)
              num = index1;
            str = "T" + index2.ToString();
          }
          else if (num != -1)
          {
            str = "T" + (num + 1).ToString();
            num = -1;
          }
          else
            str = index2.ToString();
        }
        else
          str = index2.ToString();
        child.Find("Rank").GetComponent<TextMeshProUGUI>().text = str;
        child.Find("Name").GetComponent<TextMeshProUGUI>().text = entries[index1].name;
        child.Find("Points").GetComponent<TextMeshProUGUI>().text = entries[index1].score.ToString();
      }
      for (int count = entries.Count; count < this._contentHolder.childCount; ++count)
        this._contentHolder.GetChild(count).gameObject.SetActive(false);
    }
  }

  public void HandleDataError(string levelName, SoloLeaderboardData.LeaderboardErrors error)
  {
    if (error != SoloLeaderboardData.LeaderboardErrors.NoConnection)
    {
      if (error != SoloLeaderboardData.LeaderboardErrors.NoData)
        return;
      this._statusText.text = "No scores for this minigame found";
    }
    else
      this._statusText.text = "Could not connect to the leaderboard server";
  }
}
