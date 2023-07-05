// Decompiled with JetBrains decompiler
// Type: SeasonTabletStatsScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeasonTabletStatsScreen : MonoBehaviour
{
  [SerializeField]
  private GameObject _mainPage;
  [SerializeField]
  private Transform[] _statSections;
  [SerializeField]
  private TouchUI2DButton[] _jumpToSectionButtons;
  [SerializeField]
  private TouchUI2DButton[] _showFullListButtons;
  [SerializeField]
  private SeasonTabletCanvasManager _canvasManager;
  [SerializeField]
  private GameObject[] _statPopups;
  [SerializeField]
  private Transform[] _statPopupContent;
  [SerializeField]
  private GameObject[] _statPopupItemPrefabs;
  [SerializeField]
  private TouchUI2DButton[] _statPopupBackButton;
  [SerializeField]
  private PlayerProfile _playerProfile;
  [SerializeField]
  private TouchUI2DButton[] _nextPageButtons;
  [SerializeField]
  private TouchUI2DButton[] _previousPageButtons;
  [SerializeField]
  private TextMeshProUGUI[] _pageText;
  private LeagueLeaders _ll;
  private SeasonModeManager _seasonMode;
  private SGD_SeasonModeData _seasonModeData;
  private LeagueLeaders.Leaders _leaders;
  private TeamDataStore[] _store;
  private RoutineHandle waitToHidePage = new RoutineHandle();
  private const int FARAWAY_Y = -10000;
  private const int MAX_PAGE_COUNT = 40;
  private int _currentPageNum;
  private int _maxPageNum;
  private List<LeagueLeaders.LeagueLeaderItem> _llList;
  private int _sectionIndex;
  private int _macroSection;

  private void Awake()
  {
    this.ShowPage(0);
    this._ll = this.GetComponent<LeagueLeaders>();
    this._jumpToSectionButtons[0].onClick += new System.Action(this.JumpToOffSection);
    this._jumpToSectionButtons[1].onClick += new System.Action(this.JumpToDefSection);
    this._jumpToSectionButtons[2].onClick += new System.Action(this.JumpToSpSection);
    foreach (TouchUI2DButton showFullListButton in this._showFullListButtons)
      showFullListButton.onClickInfo += new Action<TouchUI2DButton>(this.HandleFullStatsPopup);
    int length = this._statPopupBackButton.Length;
    for (int index = 0; index < length; ++index)
      this._statPopupBackButton[index].onClick += new System.Action(this.HandleFullStatsPopupBack);
    for (int index = 0; index < this._nextPageButtons.Length; ++index)
      this._nextPageButtons[index].onClick += new System.Action(this.HandleNextPage);
    for (int index = 0; index < this._previousPageButtons.Length; ++index)
      this._previousPageButtons[index].onClick += new System.Action(this.HandlePreviousPage);
    SeasonModeManager.self.OnInitComplete += new System.Action(this.Init);
  }

  private void Start()
  {
  }

  private void ShowPage(int page)
  {
    if (page == 0)
      this._canvasManager.ShowPage(SeasonTabletCanvasManager.ESeasonTabletCanvas.Stats);
    else
      this._canvasManager.ShowPage((SeasonTabletCanvasManager.ESeasonTabletCanvas) (page + 3));
  }

  private void OnDestroy()
  {
    this.waitToHidePage?.Stop();
    SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
  }

  private void Init()
  {
    this._store = SeasonTeamDataHolder.GetTeamData();
    this._ll.Init();
    this._seasonMode = SeasonModeManager.self;
    this._seasonModeData = this._seasonMode.seasonModeData;
    if (this._seasonModeData.currentWeek - 1 < 1)
    {
      this._mainPage.SetActive(false);
    }
    else
    {
      this._mainPage.SetActive(true);
      for (int index1 = 0; index1 < this._statSections.Length; ++index1)
      {
        Transform transform = this._statSections[index1].Find("Content Layout");
        LeagueLeaders.Leaders leaders;
        switch (index1)
        {
          case 0:
            leaders = this._ll.GetPassingLeaders();
            break;
          case 1:
            leaders = this._ll.GetRushingLeaders();
            break;
          case 2:
            leaders = this._ll.GetReceivingLeaders();
            break;
          case 3:
            leaders = this._ll.GetTackleLeaders();
            break;
          case 4:
            leaders = this._ll.GetINTLeaders();
            break;
          case 5:
            leaders = this._ll.GetSackLeaders();
            break;
          case 6:
            leaders = this._ll.GetFGLeaders();
            break;
          case 7:
            leaders = this._ll.GetPuntLeaders();
            break;
          default:
            leaders = this._leaders;
            break;
        }
        this._leaders = leaders;
        for (int index2 = 0; index2 < this._leaders.value.Length; ++index2)
        {
          PlayerData player = this._seasonMode.GetTeamData(this._leaders.teamIndex[index2]).GetPlayer(this._leaders.playerIndex[index2]);
          Transform child = transform.GetChild(index2);
          for (int index3 = 1; index3 < child.childCount; ++index3)
          {
            switch (index3)
            {
              case 1:
                child.GetChild(index3).GetComponent<Image>().sprite = this._store[this._leaders.teamIndex[index2]].Logo;
                break;
              case 2:
                child.GetChild(index3).GetComponent<TextMeshProUGUI>().text = player.FullName + " " + player.Number.ToString();
                break;
              case 3:
                child.GetChild(index3).GetComponent<TextMeshProUGUI>().text = this._leaders.value[index2];
                break;
            }
          }
        }
      }
    }
  }

  private void HandleFullStatsPopupBack() => this.waitToHidePage.Run(this.WaitToHidePage());

  private IEnumerator WaitToHidePage()
  {
    yield return (object) new WaitForSeconds(0.1f);
    this.ShowPage(0);
  }

  private void HandleFullStatsPopup(TouchUI2DButton button)
  {
    this._sectionIndex = button.GetID();
    this._macroSection = -1;
    switch (this._sectionIndex)
    {
      case 0:
        this._macroSection = 0;
        break;
      case 1:
        this._macroSection = 1;
        break;
      case 2:
        this._macroSection = 2;
        break;
      case 3:
      case 4:
      case 5:
        this._macroSection = 3;
        break;
      case 6:
      case 7:
        this._macroSection = 4;
        break;
    }
    this._llList = this._ll.FilterPlayersByCategory(this._macroSection);
    this._currentPageNum = 0;
    this._maxPageNum = this._llList.Count / 40 + (this._llList.Count % 40 > 0 ? 1 : 0);
    switch (this._sectionIndex)
    {
      case 0:
        this._ll.SortByQBTotalPassYards(ref this._llList);
        this.SetPassingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 1:
        this._ll.SortByRushYards(ref this._llList);
        this.SetRushingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 2:
        this._ll.SortByRecYards(ref this._llList);
        this.SetReceivingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 3:
        this._ll.SortByTackles(ref this._llList);
        this.SetDefenseStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 4:
        this._ll.SortByInts(ref this._llList);
        this.SetDefenseStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 5:
        this._ll.SortBySacks(ref this._llList);
        this.SetDefenseStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 6:
        this._ll.SortByFGPercentage(ref this._llList);
        this.SetKickingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 7:
        this._ll.SortByPuntAverage(ref this._llList);
        this.SetKickingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
    }
    this._pageText[this._macroSection].gameObject.SetActive(true);
    this._nextPageButtons[this._macroSection].gameObject.SetActive(true);
    this._previousPageButtons[this._macroSection].gameObject.SetActive(false);
    if (this._maxPageNum == 1)
    {
      this._pageText[this._macroSection].gameObject.SetActive(false);
      this._nextPageButtons[this._macroSection].gameObject.SetActive(false);
    }
    this._pageText[this._macroSection].text = string.Format("{0}/{1}", (object) (this._currentPageNum + 1), (object) this._maxPageNum);
    RectTransform component = this._statPopupContent[this._macroSection].GetComponent<RectTransform>();
    component.sizeDelta = new Vector2(component.sizeDelta.x, (float) ((this._currentPageNum < this._maxPageNum - 1 ? 40 : this._llList.Count % 40) * 80));
    this.ShowPage(this._macroSection + 1);
  }

  private void SetPassingStats(
    IReadOnlyList<LeagueLeaders.LeagueLeaderItem> leaders,
    int macroSection)
  {
    int num1 = this._currentPageNum * 40;
    for (int index1 = 0; index1 < Mathf.Min(leaders.Count - num1, 40); ++index1)
    {
      TeamData teamData = this._seasonMode.GetTeamData(leaders[num1 + index1].teamIndex);
      PlayerData player = teamData.GetPlayer(leaders[num1 + index1].playerIndex);
      PlayerStats currentSeasonStats = player.CurrentSeasonStats;
      if (index1 >= this._statPopupContent[macroSection].childCount)
        UnityEngine.Object.Instantiate<GameObject>(this._statPopupItemPrefabs[macroSection], this._statPopupContent[macroSection]);
      Transform child = this._statPopupContent[macroSection].GetChild(index1);
      child.gameObject.SetActive(true);
      for (int index2 = 0; index2 < 12; ++index2)
      {
        TextMeshProUGUI component = child.GetChild(1).GetChild(index2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMeshProUgui = component;
        int num2;
        string str;
        float num3;
        switch (index2)
        {
          case 0:
            num2 = num1 + index1 + 1;
            str = num2.ToString();
            break;
          case 1:
            str = leaders[num1 + index1].teamIndex == this._seasonModeData.UserTeamIndex ? this._playerProfile.FirstInitialAndLastName + " (" + teamData.GetAbbreviation() + ")" : player.FirstInitalAndLastName + " (" + teamData.GetAbbreviation() + ")";
            break;
          case 2:
            str = player.PlayerPosition.ToString();
            break;
          case 3:
            num2 = currentSeasonStats.GetQBRating();
            str = num2.ToString();
            break;
          case 4:
            str = currentSeasonStats.QBCompletions.ToString();
            break;
          case 5:
            str = currentSeasonStats.QBAttempts.ToString();
            break;
          case 6:
            str = currentSeasonStats.QBPassYards.ToString();
            break;
          case 7:
            num3 = (float) ((double) currentSeasonStats.QBCompletions / (double) currentSeasonStats.QBAttempts * 100.0);
            str = num3.ToString("00.#") + "%";
            break;
          case 8:
            str = currentSeasonStats.QBPassTDs.ToString();
            break;
          case 9:
            str = currentSeasonStats.QBInts.ToString();
            break;
          case 10:
            num3 = (float) currentSeasonStats.QBPassYards / (float) currentSeasonStats.QBCompletions;
            str = num3.ToString("00.#");
            break;
          case 11:
            str = currentSeasonStats.QBLongestPass.ToString();
            break;
          default:
            str = component.text;
            break;
        }
        textMeshProUgui.text = str;
      }
    }
    for (int index = Mathf.Min(leaders.Count - num1, 40); index < 40; ++index)
    {
      if (this._statPopupContent[macroSection].childCount > index)
        this._statPopupContent[macroSection].GetChild(index).gameObject.SetActive(false);
    }
  }

  private void SetRushingStats(
    IReadOnlyList<LeagueLeaders.LeagueLeaderItem> leaders,
    int macroSection)
  {
    int num = this._currentPageNum * 40;
    Debug.Log((object) ("offset: " + num.ToString()));
    for (int index1 = 0; index1 < Mathf.Min(leaders.Count - num, 40); ++index1)
    {
      TeamData teamData = this._seasonMode.GetTeamData(leaders[num + index1].teamIndex);
      PlayerData player = teamData.GetPlayer(leaders[num + index1].playerIndex);
      PlayerStats currentSeasonStats = player.CurrentSeasonStats;
      if (index1 >= this._statPopupContent[macroSection].childCount)
        UnityEngine.Object.Instantiate<GameObject>(this._statPopupItemPrefabs[macroSection], this._statPopupContent[macroSection]);
      Transform child = this._statPopupContent[macroSection].GetChild(index1);
      child.gameObject.SetActive(true);
      for (int index2 = 0; index2 < 9; ++index2)
      {
        TextMeshProUGUI component = child.GetChild(1).GetChild(index2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMeshProUgui = component;
        string str;
        switch (index2)
        {
          case 0:
            str = (num + index1 + 1).ToString();
            break;
          case 1:
            str = leaders[num + index1].teamIndex != this._seasonModeData.UserTeamIndex || player.PlayerPosition != Position.QB ? player.FirstInitalAndLastName + " (" + teamData.GetAbbreviation() + ")" : this._playerProfile.FirstInitialAndLastName + " (" + teamData.GetAbbreviation() + ")";
            break;
          case 2:
            str = player.PlayerPosition.ToString();
            break;
          case 3:
            str = currentSeasonStats.RushAttempts.ToString();
            break;
          case 4:
            str = currentSeasonStats.RushYards.ToString();
            break;
          case 5:
            str = currentSeasonStats.RushAttempts > 0 ? ((float) currentSeasonStats.RushYards / (float) currentSeasonStats.RushAttempts).ToString("00.#") : "0";
            break;
          case 6:
            str = currentSeasonStats.LongestRush.ToString();
            break;
          case 7:
            str = currentSeasonStats.RushTDs.ToString();
            break;
          case 8:
            str = currentSeasonStats.Fumbles.ToString();
            break;
          default:
            str = component.text;
            break;
        }
        textMeshProUgui.text = str;
      }
    }
    for (int index = Mathf.Min(leaders.Count - num, 40); index < 40; ++index)
    {
      if (this._statPopupContent[macroSection].childCount > index)
        this._statPopupContent[macroSection].GetChild(index).gameObject.SetActive(false);
    }
  }

  private void SetReceivingStats(
    IReadOnlyList<LeagueLeaders.LeagueLeaderItem> leaders,
    int macroSection)
  {
    int num = this._currentPageNum * 40;
    for (int index1 = 0; index1 < Mathf.Min(leaders.Count - num, 40); ++index1)
    {
      TeamData teamData = this._seasonMode.GetTeamData(leaders[num + index1].teamIndex);
      PlayerData player = teamData.GetPlayer(leaders[num + index1].playerIndex);
      PlayerStats currentSeasonStats = player.CurrentSeasonStats;
      if (index1 >= this._statPopupContent[macroSection].childCount)
        UnityEngine.Object.Instantiate<GameObject>(this._statPopupItemPrefabs[macroSection], this._statPopupContent[macroSection]);
      Transform child = this._statPopupContent[macroSection].GetChild(index1);
      child.gameObject.SetActive(true);
      for (int index2 = 0; index2 < 11; ++index2)
      {
        TextMeshProUGUI component = child.GetChild(1).GetChild(index2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMeshProUgui = component;
        string str;
        switch (index2)
        {
          case 0:
            str = (num + index1 + 1).ToString();
            break;
          case 1:
            str = player.FirstInitalAndLastName + " (" + teamData.GetAbbreviation() + ")";
            break;
          case 2:
            str = player.PlayerPosition.ToString();
            break;
          case 3:
            str = currentSeasonStats.Receptions.ToString();
            break;
          case 4:
            str = currentSeasonStats.Targets.ToString();
            break;
          case 5:
            str = currentSeasonStats.ReceivingYards.ToString();
            break;
          case 6:
            str = ((float) ((double) currentSeasonStats.ReceivingYards / (double) currentSeasonStats.Receptions * 100.0)).ToString("00.#");
            break;
          case 7:
            str = currentSeasonStats.LongestReception.ToString();
            break;
          case 8:
            str = currentSeasonStats.ReceivingTDs.ToString();
            break;
          case 9:
            str = currentSeasonStats.Drops.ToString();
            break;
          case 10:
            str = currentSeasonStats.Fumbles.ToString();
            break;
          default:
            str = component.text;
            break;
        }
        textMeshProUgui.text = str;
      }
    }
    for (int index = Mathf.Min(leaders.Count - num, 40); index < 40; ++index)
    {
      if (this._statPopupContent[macroSection].childCount > index)
        this._statPopupContent[macroSection].GetChild(index).gameObject.SetActive(false);
    }
  }

  private void SetDefenseStats(
    IReadOnlyList<LeagueLeaders.LeagueLeaderItem> leaders,
    int macroSection)
  {
    int num = this._currentPageNum * 40;
    for (int index1 = 0; index1 < Mathf.Min(leaders.Count - num, 40); ++index1)
    {
      TeamData teamData = this._seasonMode.GetTeamData(leaders[num + index1].teamIndex);
      PlayerData player = teamData.GetPlayer(leaders[num + index1].playerIndex);
      PlayerStats currentSeasonStats = player.CurrentSeasonStats;
      if (index1 >= this._statPopupContent[macroSection].childCount)
        UnityEngine.Object.Instantiate<GameObject>(this._statPopupItemPrefabs[macroSection], this._statPopupContent[macroSection]);
      Transform child = this._statPopupContent[macroSection].GetChild(index1);
      child.gameObject.SetActive(true);
      for (int index2 = 0; index2 < 10; ++index2)
      {
        TextMeshProUGUI component = child.GetChild(1).GetChild(index2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMeshProUgui = component;
        string str;
        switch (index2)
        {
          case 0:
            str = (num + index1 + 1).ToString();
            break;
          case 1:
            str = player.FirstInitalAndLastName + " (" + teamData.GetAbbreviation() + ")";
            break;
          case 2:
            str = player.PlayerPosition.ToString();
            break;
          case 3:
            str = currentSeasonStats.Tackles.ToString();
            break;
          case 4:
            str = currentSeasonStats.Sacks.ToString();
            break;
          case 5:
            str = currentSeasonStats.Interceptions.ToString();
            break;
          case 6:
            str = currentSeasonStats.Drops.ToString();
            break;
          case 7:
            str = currentSeasonStats.ForcedFumbles.ToString();
            break;
          case 8:
            str = currentSeasonStats.Receptions.ToString();
            break;
          case 9:
            str = currentSeasonStats.DefensiveTDs.ToString();
            break;
          default:
            str = component.text;
            break;
        }
        textMeshProUgui.text = str;
      }
    }
    for (int index = Mathf.Min(leaders.Count - num, 40); index < 40; ++index)
    {
      if (this._statPopupContent[macroSection].childCount > index)
        this._statPopupContent[macroSection].GetChild(index).gameObject.SetActive(false);
    }
  }

  private void SetKickingStats(
    IReadOnlyList<LeagueLeaders.LeagueLeaderItem> leaders,
    int macroSection)
  {
    int num = this._currentPageNum * 40;
    for (int index1 = 0; index1 < Mathf.Min(leaders.Count - num, 40); ++index1)
    {
      TeamData teamData = this._seasonMode.GetTeamData(leaders[num + index1].teamIndex);
      PlayerData player = teamData.GetPlayer(leaders[num + index1].playerIndex);
      PlayerStats currentSeasonStats = player.CurrentSeasonStats;
      if (index1 >= this._statPopupContent[macroSection].childCount)
        UnityEngine.Object.Instantiate<GameObject>(this._statPopupItemPrefabs[macroSection], this._statPopupContent[macroSection]);
      Transform child = this._statPopupContent[macroSection].GetChild(index1);
      child.gameObject.SetActive(true);
      for (int index2 = 0; index2 < 10; ++index2)
      {
        TextMeshProUGUI component = child.GetChild(1).GetChild(index2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMeshProUgui = component;
        string str;
        switch (index2)
        {
          case 0:
            str = (num + index1 + 1).ToString();
            break;
          case 1:
            str = player.FirstInitalAndLastName + " (" + teamData.GetAbbreviation() + ")";
            break;
          case 2:
            str = player.PlayerPosition.ToString();
            break;
          case 3:
            str = currentSeasonStats.FGMade.ToString();
            break;
          case 4:
            str = currentSeasonStats.FGAttempted.ToString();
            break;
          case 5:
            str = currentSeasonStats.XPMade.ToString();
            break;
          case 6:
            str = currentSeasonStats.Punts.ToString();
            break;
          case 7:
            str = currentSeasonStats.PuntsInside20.ToString();
            break;
          case 8:
            str = currentSeasonStats.PuntTouchbacks.ToString();
            break;
          case 9:
            str = (currentSeasonStats.Punts > 0 ? (float) currentSeasonStats.PuntYards / (float) currentSeasonStats.Punts : 0.0f).ToString((IFormatProvider) CultureInfo.InvariantCulture);
            break;
          default:
            str = component.text;
            break;
        }
        textMeshProUgui.text = str;
      }
    }
    for (int index = Mathf.Min(leaders.Count - num, 40); index < 40; ++index)
    {
      if (this._statPopupContent[macroSection].childCount > index)
        this._statPopupContent[macroSection].GetChild(index).gameObject.SetActive(false);
    }
  }

  private void JumpToOffSection()
  {
    RectTransform component = this._statSections[0].transform.parent.GetComponent<RectTransform>();
    component.anchoredPosition = new Vector2(component.anchoredPosition.x, 0.0f);
  }

  private void JumpToDefSection()
  {
    RectTransform component = this._statSections[0].transform.parent.GetComponent<RectTransform>();
    component.anchoredPosition = new Vector2(component.anchoredPosition.x, 1715f);
  }

  private void JumpToSpSection()
  {
    RectTransform component = this._statSections[0].transform.parent.GetComponent<RectTransform>();
    component.anchoredPosition = new Vector2(component.anchoredPosition.x, 3380f);
  }

  public void HandlePreviousPage()
  {
    --this._currentPageNum;
    this._currentPageNum = Mathf.Max(this._currentPageNum, 0);
    this._nextPageButtons[this._macroSection].gameObject.SetActive(true);
    this._previousPageButtons[this._macroSection].gameObject.SetActive(true);
    if (this._currentPageNum == 0)
      this._previousPageButtons[this._macroSection].gameObject.SetActive(false);
    this.UpdateCurrentPage();
  }

  public void HandleNextPage()
  {
    ++this._currentPageNum;
    this._currentPageNum = Mathf.Min(this._currentPageNum, this._maxPageNum - 1);
    this._nextPageButtons[this._macroSection].gameObject.SetActive(true);
    this._previousPageButtons[this._macroSection].gameObject.SetActive(true);
    if (this._currentPageNum == this._maxPageNum - 1)
      this._nextPageButtons[this._macroSection].gameObject.SetActive(false);
    this.UpdateCurrentPage();
  }

  private void UpdateCurrentPage()
  {
    switch (this._sectionIndex)
    {
      case 0:
        this.SetPassingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 1:
        this.SetRushingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 2:
        this.SetReceivingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 3:
        this.SetDefenseStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 4:
        this.SetDefenseStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 5:
        this.SetDefenseStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 6:
        this.SetKickingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
      case 7:
        this.SetKickingStats((IReadOnlyList<LeagueLeaders.LeagueLeaderItem>) this._llList, this._macroSection);
        break;
    }
    this._pageText[this._macroSection].text = string.Format("{0}/{1}", (object) (this._currentPageNum + 1), (object) this._maxPageNum);
    RectTransform component = this._statPopupContent[this._macroSection].GetComponent<RectTransform>();
    component.sizeDelta = new Vector2(component.sizeDelta.x, (float) ((this._currentPageNum < this._maxPageNum - 1 ? 40 : this._llList.Count % 40) * 80));
  }
}
