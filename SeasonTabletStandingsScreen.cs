// Decompiled with JetBrains decompiler
// Type: SeasonTabletStandingsScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeasonTabletStandingsScreen : MonoBehaviour
{
  [SerializeField]
  private Transform _divisionHolder;

  private void Start() => SeasonModeManager.self.OnInitComplete += new System.Action(this.Init);

  private void OnDestroy()
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self)
      return;
    SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
  }

  private void Init()
  {
    if ((UnityEngine.Object) this._divisionHolder == (UnityEngine.Object) null)
      return;
    SeasonModeManager self = SeasonModeManager.self;
    SGD_SeasonModeData seasonModeData = self.seasonModeData;
    TeamDataStore[] teamData1 = SeasonTeamDataHolder.GetTeamData();
    for (int conference = 1; conference < 3; ++conference)
    {
      for (int divisionIndex = 0; divisionIndex < 4; ++divisionIndex)
      {
        int[] sortedTeamsInDivision = self.GetSortedTeamsInDivision(conference, divisionIndex + 1);
        Transform child1 = this._divisionHolder.GetChild((conference - 1) * 4 + divisionIndex);
        string conferenceName = seasonModeData.GetConferenceName(conference - 1);
        string divisionName = seasonModeData.GetDivisionName(divisionIndex);
        child1.Find("Header/Division").GetComponent<TextMeshProUGUI>().text = conferenceName + " " + divisionName;
        for (int index = 0; index < sortedTeamsInDivision.Length; ++index)
        {
          TeamData teamData2 = self.GetTeamData(sortedTeamsInDivision[index]);
          TeamSeasonStats currentSeasonStats = teamData2.CurrentSeasonStats;
          Transform child2 = child1.GetChild(index + 1);
          child2.Find("Rank").GetComponent<TextMeshProUGUI>().text = (index + 1).ToString();
          child2.Find("Name").GetComponent<TextMeshProUGUI>().text = teamData2.GetName();
          child2.Find("Logo_BG/Logo").GetComponent<Image>().sprite = teamData1[teamData2.TeamIndex].Logo;
          Transform transform = child2.Find("Stats");
          transform.Find("Rec").GetComponent<TextMeshProUGUI>().text = currentSeasonStats.GetRecordString(TeamStatGameType.NonConference);
          transform.Find("Div").GetComponent<TextMeshProUGUI>().text = currentSeasonStats.GetRecordString(TeamStatGameType.Division);
          transform.Find("Stk").GetComponent<TextMeshProUGUI>().text = currentSeasonStats.GetStreakString();
        }
      }
    }
  }
}
