// Decompiled with JetBrains decompiler
// Type: SeasonTabletMyStatsScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Vars;

public class SeasonTabletMyStatsScreen : MonoBehaviour
{
  [SerializeField]
  private PlayerProfile _playerProfile;
  [Space(10f)]
  [Header("Player Info")]
  [SerializeField]
  private TMP_Text _playerNameText;
  [SerializeField]
  private TMP_Text _playerNumberText;
  [SerializeField]
  private Image _teamLogo;
  [Space(10f)]
  [Header("Season Stats")]
  [SerializeField]
  private LocalizeStringEvent _seasonTotals;
  [SerializeField]
  private TMP_Text _seasonStat_QBR;
  [SerializeField]
  private TMP_Text _seasonStat_Comp;
  [SerializeField]
  private TMP_Text _seasonStat_ATT;
  [SerializeField]
  private TMP_Text _seasonStat_COMPpc;
  [SerializeField]
  private TMP_Text _seasonStat_YDS;
  [SerializeField]
  private TMP_Text _seasonStat_TDS;
  [SerializeField]
  private TMP_Text _seasonStat_INT;
  [SerializeField]
  private TMP_Text _seasonStat_RushAtt;
  [SerializeField]
  private TMP_Text _seasonStat_RushYds;
  [SerializeField]
  private TMP_Text _seasonStat_RushTds;
  [Space(10f)]
  [Header("Career Stats")]
  [SerializeField]
  private TMP_Text _careerStat_QBR;
  [SerializeField]
  private TMP_Text _careerStat_Comp;
  [SerializeField]
  private TMP_Text _careerStat_ATT;
  [SerializeField]
  private TMP_Text _careerStat_COMPpc;
  [SerializeField]
  private TMP_Text _careerStat_YDS;
  [SerializeField]
  private TMP_Text _careerStat_TDS;
  [SerializeField]
  private TMP_Text _careerStat_INT;
  [SerializeField]
  private TMP_Text _careerStat_RushAtt;
  [SerializeField]
  private TMP_Text _careerStat_RushYds;
  [SerializeField]
  private TMP_Text _careerStat_RushTds;
  private VariableInt _teamIndex = new VariableInt(0);
  private SeasonModeManager _seasonManager;

  private void Start()
  {
    this._seasonManager = SeasonModeManager.self;
    this._seasonManager.OnInitComplete += new System.Action(this.Init);
    this.Init();
  }

  private void OnDestroy()
  {
    if ((bool) (UnityEngine.Object) SeasonModeManager.self)
      SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
    this.Deinitialize();
  }

  private void Init()
  {
    if (!((UnityEngine.Object) this._seasonManager != (UnityEngine.Object) null))
      return;
    this.UpdateAllStats((int) this._teamIndex);
    this._teamIndex.OnValueChanged += new Action<int>(this.UpdateAllStats);
    this._playerProfile.Customization.FirstName.OnValueChanged += new Action<string>(this.UpdatePlayerInfo_Customization);
    this._playerProfile.Customization.LastName.OnValueChanged += new Action<string>(this.UpdatePlayerInfo_Customization);
    this._playerProfile.Customization.UniformNumber.OnValueChanged += new Action<int>(this.UpdatePlayerInfo_Customization);
    this._teamIndex.SetValue(this._seasonManager.userTeamData.TeamIndex);
  }

  private void Deinitialize()
  {
    if (!((UnityEngine.Object) this._seasonManager != (UnityEngine.Object) null))
      return;
    this._teamIndex.OnValueChanged -= new Action<int>(this.UpdateAllStats);
    this._playerProfile.Customization.FirstName.OnValueChanged -= new Action<string>(this.UpdatePlayerInfo_Customization);
    this._playerProfile.Customization.LastName.OnValueChanged -= new Action<string>(this.UpdatePlayerInfo_Customization);
    this._playerProfile.Customization.UniformNumber.OnValueChanged -= new Action<int>(this.UpdatePlayerInfo_Customization);
  }

  private void UpdatePlayerInfo_Customization(string a_name) => this.UpdatePlayerInfo(this._seasonManager.GetTeamData((int) this._teamIndex));

  private void UpdatePlayerInfo_Customization(int a_number) => this.UpdatePlayerInfo(this._seasonManager.GetTeamData((int) this._teamIndex));

  private void UpdateAllStats(int a_teamIndex)
  {
    TeamData teamData = this._seasonManager.GetTeamData(a_teamIndex);
    this.UpdatePlayerInfo(teamData);
    this.UpdateSeasonStats(teamData);
    this.UpdateCareerStats(teamData);
  }

  private void UpdatePlayerInfo(TeamData a_teamData)
  {
    if (a_teamData == null)
      return;
    string str1 = string.Format("{0} {1}", (object) this._playerProfile.Customization.FirstName.Value, (object) this._playerProfile.Customization.LastName.Value);
    string str2 = string.Format("#{0}", (object) this._playerProfile.Customization.UniformNumber);
    this._playerNameText.text = str1;
    this._playerNumberText.text = str2;
    this._teamLogo.sprite = SeasonTeamDataHolder.GetTeamData()[(int) this._teamIndex].Logo;
  }

  private void UpdateSeasonStats(TeamData a_teamData)
  {
    if (a_teamData == null)
      return;
    PlayerStats currentSeasonStats = a_teamData.GetPlayer(0).CurrentSeasonStats;
    this._seasonTotals.StringReference.Arguments = (IList<object>) new string[1]
    {
      currentSeasonStats.StatYear.ToString()
    };
    this._seasonStat_QBR.text = currentSeasonStats.GetQBRating().ToString();
    this._seasonStat_Comp.text = currentSeasonStats.QBCompletions.ToString();
    this._seasonStat_ATT.text = currentSeasonStats.QBAttempts.ToString();
    this._seasonStat_COMPpc.text = currentSeasonStats.CompletionPercent.ToString("P0");
    this._seasonStat_YDS.text = currentSeasonStats.QBPassYards.ToString();
    this._seasonStat_TDS.text = currentSeasonStats.TotalTDs.ToString();
    this._seasonStat_INT.text = currentSeasonStats.QBInts.ToString();
    this._seasonStat_RushAtt.text = currentSeasonStats.RushAttempts.ToString();
    this._seasonStat_RushYds.text = currentSeasonStats.RushYards.ToString();
    this._seasonStat_RushTds.text = currentSeasonStats.RushTDs.ToString();
  }

  private void UpdateCareerStats(TeamData a_teamData)
  {
    if (a_teamData == null)
      return;
    PlayerStats totalCareerStats = a_teamData.GetPlayer(0).TotalCareerStats;
    this._careerStat_QBR.text = totalCareerStats.GetQBRating().ToString();
    this._careerStat_Comp.text = totalCareerStats.QBCompletions.ToString();
    this._careerStat_ATT.text = totalCareerStats.QBAttempts.ToString();
    this._careerStat_COMPpc.text = totalCareerStats.CompletionPercent.ToString("P0");
    this._careerStat_YDS.text = totalCareerStats.QBPassYards.ToString();
    this._careerStat_TDS.text = totalCareerStats.TotalTDs.ToString();
    this._careerStat_INT.text = totalCareerStats.QBInts.ToString();
    this._careerStat_RushAtt.text = totalCareerStats.RushAttempts.ToString();
    this._careerStat_RushYds.text = totalCareerStats.RushYards.ToString();
    this._careerStat_RushTds.text = totalCareerStats.RushTDs.ToString();
  }
}
