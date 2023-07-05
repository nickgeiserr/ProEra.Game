// Decompiled with JetBrains decompiler
// Type: SeasonTabletMainScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class SeasonTabletMainScreen : MonoBehaviour
{
  [SerializeField]
  private RectTransform _scheduleHolder;
  [SerializeField]
  private GameObject _scheduleItemPrefab;
  [SerializeField]
  private TouchUI2DButton _leftButton;
  [SerializeField]
  private TouchUI2DButton _rightButton;
  private float _movementOffset = 251f;
  private int _itemsToMoveBy = 2;
  private int _currentSection;
  [SerializeField]
  private Transform _afcList;
  [SerializeField]
  private Transform _nfcList;
  [SerializeField]
  private Transform _mvpHolder;
  private bool _initialized;

  private void Start()
  {
    this._scheduleHolder.anchoredPosition = (Vector2) Vector3.zero;
    this._leftButton.onClick += new System.Action(this.HandleLeftPressed);
    this._rightButton.onClick += new System.Action(this.HandleRightPressed);
    this._leftButton.gameObject.SetActive(false);
    this._movementOffset = this._scheduleItemPrefab.GetComponent<RectTransform>().rect.width + this._scheduleHolder.GetComponent<HorizontalLayoutGroup>().spacing;
    SeasonModeManager.self.OnInitComplete += new System.Action(this.Init);
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged += new System.Action(this.OnTeamChanged);
  }

  private void OnDestroy()
  {
    if ((bool) (UnityEngine.Object) SeasonModeManager.self)
      SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
    if (!((UnityEngine.Object) SingletonBehaviour<PersistentData, MonoBehaviour>.instance != (UnityEngine.Object) null))
      return;
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged -= new System.Action(this.OnTeamChanged);
  }

  private void OnTeamChanged()
  {
    if (!this._initialized)
      return;
    this.Init();
  }

  private void Init()
  {
    SeasonModeManager self = SeasonModeManager.self;
    TeamDataStore[] teamData1 = SeasonTeamDataHolder.GetTeamData();
    List<SeasonModeGameInfo> gameResultsForWeek = self.GetGameResultsForWeek(PersistentData.seasonWeek);
    int count = gameResultsForWeek.Count;
    int childCount = this._scheduleHolder.childCount;
    bool flag = childCount > 0;
    for (int index = 0; index < count; ++index)
    {
      GameObject gameObject;
      if (flag && index < childCount)
      {
        gameObject = this._scheduleHolder.GetChild(index).gameObject;
      }
      else
      {
        gameObject = UnityEngine.Object.Instantiate<GameObject>(this._scheduleItemPrefab);
        gameObject.transform.SetParent((Transform) this._scheduleHolder);
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localPosition = Vector3.zero;
      }
      SeasonModeGameInfo seasonModeGameInfo = gameResultsForWeek[index];
      Transform transform1 = gameObject.transform.Find("HomeLogo");
      transform1.GetComponent<Image>().sprite = teamData1[seasonModeGameInfo.homeTeamIndex].Logo;
      transform1.Find("HomeAbb").GetComponent<TextMeshProUGUI>().text = seasonModeGameInfo.homeTeamAbbrev;
      TeamData teamData2 = self.seasonModeData.GetTeamData(seasonModeGameInfo.homeTeamIndex);
      transform1.Find("HomeScore").GetComponent<TextMeshProUGUI>().text = teamData2.CurrentSeasonStats.GetRecordString(TeamStatGameType.NonConference);
      Transform transform2 = gameObject.transform.Find("AwayLogo");
      transform2.GetComponent<Image>().sprite = teamData1[seasonModeGameInfo.awayTeamIndex].Logo;
      transform2.Find("AwayAbb").GetComponent<TextMeshProUGUI>().text = seasonModeGameInfo.awayTeamAbbrev;
      TeamData teamData3 = self.seasonModeData.GetTeamData(seasonModeGameInfo.awayTeamIndex);
      transform2.Find("AwayScore").GetComponent<TextMeshProUGUI>().text = teamData3.CurrentSeasonStats.GetRecordString(TeamStatGameType.NonConference);
    }
    this._rightButton.gameObject.SetActive(true);
    this._leftButton.gameObject.SetActive(true);
    try
    {
      int[] numArray1 = PlayerStats.GetConferenceMVP(1, StatDuration.CurrentSeason, MVPType.Offensive)[0];
      int[] numArray2 = PlayerStats.GetConferenceMVP(1, StatDuration.CurrentSeason, MVPType.Defensive)[0];
      int[] numArray3 = PlayerStats.GetConferenceMVP(2, StatDuration.CurrentSeason, MVPType.Offensive)[0];
      int[] numArray4 = PlayerStats.GetConferenceMVP(2, StatDuration.CurrentSeason, MVPType.Defensive)[0];
      TeamData teamData4 = self.GetTeamData(numArray1[0]);
      PlayerData player1 = teamData4.GetPlayer(numArray1[1]);
      this._afcList.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = player1.FullName;
      this._afcList.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = teamData4.GetFullDisplayName();
      TeamData teamData5 = self.GetTeamData(numArray2[0]);
      PlayerData player2 = teamData5.GetPlayer(numArray2[1]);
      this._afcList.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = player2.FullName;
      this._afcList.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = teamData5.GetFullDisplayName();
      TeamData teamData6 = self.GetTeamData(numArray3[0]);
      PlayerData player3 = teamData6.GetPlayer(numArray3[1]);
      this._nfcList.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = player3.FullName;
      this._nfcList.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = teamData6.GetFullDisplayName();
      TeamData teamData7 = self.GetTeamData(numArray4[0]);
      PlayerData player4 = teamData7.GetPlayer(numArray4[1]);
      this._nfcList.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = player4.FullName;
      this._nfcList.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = teamData7.GetFullDisplayName();
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ("Failed to get PoW data: " + ex?.ToString()));
    }
    try
    {
      List<int[]> conferenceMvp = PlayerStats.GetConferenceMVP(0, StatDuration.CurrentSeason, MVPType.Overall, 5);
      if ((UnityEngine.Object) this._mvpHolder != (UnityEngine.Object) null)
      {
        for (int index = 0; index < this._mvpHolder.childCount; ++index)
        {
          TeamData teamData8 = self.GetTeamData(conferenceMvp[index][0]);
          PlayerData player = teamData8.GetPlayer(conferenceMvp[index][1]);
          LockerTvElement component = this._mvpHolder.GetChild(index).GetComponent<LockerTvElement>();
          component.Logo.sprite = teamData1[conferenceMvp[index][0]].Logo;
          component.PlayerNameText.text = player.FullName;
          component.TeamNameText.text = teamData8.GetFullDisplayName();
          component.TeamRecordText.text = teamData8.CurrentSeasonStats.GetRecordString(TeamStatGameType.NonConference);
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ("Failed to get Awards Race data: " + ex?.ToString()));
    }
    this._initialized = true;
  }

  private void HandleLeftPressed()
  {
    --this._currentSection;
    LeanTween.moveLocalX(this._scheduleHolder.gameObject, this._movementOffset * (float) this._itemsToMoveBy * (float) -this._currentSection, 0.5f);
  }

  private void HandleRightPressed()
  {
    Debug.Log((object) nameof (HandleRightPressed));
    ++this._currentSection;
    LeanTween.moveLocalX(this._scheduleHolder.gameObject, this._movementOffset * (float) this._itemsToMoveBy * (float) -this._currentSection, 0.5f);
  }
}
