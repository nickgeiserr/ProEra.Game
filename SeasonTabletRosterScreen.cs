// Decompiled with JetBrains decompiler
// Type: SeasonTabletRosterScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vars;

public class SeasonTabletRosterScreen : MonoBehaviour
{
  [SerializeField]
  private RectTransform _rosterHolder;
  [SerializeField]
  private GameObject _rosterItemPrefab;
  [SerializeField]
  private TextMeshProUGUI _leftButtonText;
  [SerializeField]
  private TextMeshProUGUI _rightButtonText;
  [SerializeField]
  private TextMeshProUGUI _titleText;
  [SerializeField]
  private Image _titleLogo;
  private VariableInt _teamIndex = new VariableInt(0);
  private SeasonModeManager _seasonManager;
  private int _leftIndex;
  private int _rightIndex;
  private int _teamCount;
  private TeamDataStore[] _teamDataStore;
  private const int LIST_ITEM_SIZE = 74;

  private void Start()
  {
    this._seasonManager = SeasonModeManager.self;
    this._seasonManager.OnInitComplete += new System.Action(this.Init);
    this._teamIndex.OnValueChanged += new Action<int>(this.UpdateRoster);
    this._leftButtonText.transform.parent.GetComponent<TouchUI2DButton>().onClick += new System.Action(this.HandleLeftClick);
    this._rightButtonText.transform.parent.GetComponent<TouchUI2DButton>().onClick += new System.Action(this.HandleRightClick);
    this._teamDataStore = SeasonTeamDataHolder.GetTeamData();
    this.Init();
  }

  private void OnDestroy()
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self)
      return;
    SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
  }

  private void Init()
  {
    if (!((UnityEngine.Object) this._seasonManager != (UnityEngine.Object) null))
      return;
    this._teamCount = this._seasonManager.seasonModeData.TeamIndexMasterList.Length - 1;
    this._teamIndex.SetValue(this._seasonManager.userTeamData.TeamIndex);
  }

  private void UpdateRoster(int teamIndex)
  {
    TeamData teamData = this._seasonManager.GetTeamData(teamIndex);
    if (teamData == null)
      return;
    RosterData mainRoster = teamData.MainRoster;
    this._titleText.text = teamData.GetName();
    this._titleLogo.sprite = this._teamDataStore[teamIndex].Logo;
    for (int index = 0; index < mainRoster.GetNumberOfPlayers(); ++index)
    {
      PlayerData player = mainRoster.GetPlayer(index);
      if (this._rosterHolder.childCount <= index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._rosterItemPrefab);
        gameObject.transform.SetParent((Transform) this._rosterHolder);
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localPosition = Vector3.zero;
      }
      Transform child = this._rosterHolder.GetChild(index);
      child.gameObject.SetActive(true);
      child.Find("Name").GetComponent<TextMeshProUGUI>().text = player.FullName + " " + player.Number.ToString();
      child.Find("Position").GetComponent<TextMeshProUGUI>().text = player.PlayerPosition.ToString();
      child.Find("Age").GetComponent<TextMeshProUGUI>().text = player.Age.ToString();
      TextMeshProUGUI component = child.Find("Height").GetComponent<TextMeshProUGUI>();
      int num = player.Height / 12;
      string str1 = num.ToString();
      num = player.Height % 12;
      string str2 = num.ToString();
      string str3 = str1 + "'" + str2 + "\"";
      component.text = str3;
      child.Find("Weight").GetComponent<TextMeshProUGUI>().text = player.Weight.ToString() + " LBS";
    }
    this._rosterHolder.sizeDelta = new Vector2(this._rosterHolder.sizeDelta.x, (float) (74 * mainRoster.GetNumberOfPlayers()));
    for (int numberOfPlayers = mainRoster.GetNumberOfPlayers(); numberOfPlayers < this._rosterHolder.childCount; ++numberOfPlayers)
      this._rosterHolder.GetChild(numberOfPlayers).gameObject.SetActive(false);
    this._leftIndex = teamIndex - 1;
    this._leftIndex = this._leftIndex < 0 ? this._teamCount : this._leftIndex;
    this._rightIndex = teamIndex + 1;
    this._rightIndex = this._rightIndex > this._teamCount ? 0 : this._rightIndex;
    this._leftButtonText.text = this._seasonManager.GetTeamData(this._leftIndex).GetName();
    this._rightButtonText.text = this._seasonManager.GetTeamData(this._rightIndex).GetName();
  }

  private void HandleLeftClick()
  {
    int num = (int) this._teamIndex - 1;
    this._teamIndex.SetValue(num < 0 ? this._teamCount : num);
  }

  private void HandleRightClick()
  {
    int num = (int) this._teamIndex + 1;
    this._teamIndex.SetValue(num > this._teamCount ? 0 : num);
  }
}
