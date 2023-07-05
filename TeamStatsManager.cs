// Decompiled with JetBrains decompiler
// Type: TeamStatsManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class TeamStatsManager : MonoBehaviour
{
  private TeamStatCategory activeStatCategory;
  private int activeCategoryIndex;
  private bool sortingDescOrder;
  private int showingTier;
  private bool reversedSort;
  private SeasonModeManager seasonMode;
  private SGD_SeasonModeData seasonModeData;
  private int userTeamIndex;
  private int[] _sortedTeams;
  private int[] _rankedTeams;

  private void Start()
  {
  }

  public void Init()
  {
    this.seasonMode = SeasonModeManager.self;
    this.seasonModeData = this.seasonMode.seasonModeData;
    this.userTeamIndex = this.seasonModeData.UserTeamIndex;
  }

  private void Update()
  {
  }

  public void ShowWindow()
  {
    this.showingTier = 0;
    this.SortTeamsByStat(TeamStatCategory.Total_Yards);
  }

  public void ChangeTier()
  {
    TeamStatCategory activeStatCategory = this.activeStatCategory;
    this.activeStatCategory = TeamStatCategory.None;
    this.SortTeamsByStat(activeStatCategory);
  }

  public int[] OrderTeamsByStat(
    TeamStatCategory statCategory,
    bool sortInDescOrder,
    int filterByTier)
  {
    if (filterByTier == 0)
    {
      this._sortedTeams = new int[this.seasonModeData.NumberOfTeamsInLeague];
      for (int index = 0; index < this._sortedTeams.Length; ++index)
        this._sortedTeams[index] = this.seasonModeData.TeamIndexMasterList[index];
    }
    else
    {
      int[] teamsInConference = this.seasonModeData.GetTeamsInConference(filterByTier);
      this._sortedTeams = new int[this.seasonModeData.NumberOfTeamsPerConference];
      for (int index = 0; index < this._sortedTeams.Length; ++index)
        this._sortedTeams[index] = teamsInConference[index];
    }
    for (int index1 = 0; index1 < this._sortedTeams.Length - 1; ++index1)
    {
      int index2 = index1;
      int categoryValue = this.seasonMode.GetTeamData(this._sortedTeams[index1]).CurrentSeasonStats.GetCategoryValue(statCategory);
      for (int index3 = index1 + 1; index3 < this._sortedTeams.Length; ++index3)
      {
        int sortedTeam = this._sortedTeams[index3];
        if (sortInDescOrder)
        {
          if (categoryValue < this.seasonMode.GetTeamData(sortedTeam).CurrentSeasonStats.GetCategoryValue(statCategory))
          {
            index2 = index3;
            categoryValue = this.seasonMode.GetTeamData(sortedTeam).CurrentSeasonStats.GetCategoryValue(statCategory);
          }
        }
        else if (categoryValue > this.seasonMode.GetTeamData(sortedTeam).CurrentSeasonStats.GetCategoryValue(statCategory))
        {
          index2 = index3;
          categoryValue = this.seasonMode.GetTeamData(sortedTeam).CurrentSeasonStats.GetCategoryValue(statCategory);
        }
      }
      int sortedTeam1 = this._sortedTeams[index1];
      this._sortedTeams[index1] = this._sortedTeams[index2];
      this._sortedTeams[index2] = (int) (short) sortedTeam1;
    }
    return this._sortedTeams;
  }

  public void SortTeamsByStats(int statCategory) => this.SortTeamsByStat((TeamStatCategory) statCategory);

  public void SortTeamsByStat(TeamStatCategory statCategory)
  {
    bool flag;
    if (statCategory == this.activeStatCategory)
    {
      this.reversedSort = !this.reversedSort;
      flag = !this.sortingDescOrder;
    }
    else
    {
      flag = true;
      this.reversedSort = false;
      if (statCategory == TeamStatCategory.Total_Yards_Allowed || statCategory == TeamStatCategory.Pass_Defense || statCategory == TeamStatCategory.Rush_Defense || statCategory == TeamStatCategory.Points_Allowed)
        flag = false;
    }
    this.sortingDescOrder = flag;
    this.activeStatCategory = statCategory;
    this.activeCategoryIndex = (int) this.activeStatCategory;
    this._rankedTeams = this.OrderTeamsByStat(statCategory, this.sortingDescOrder, this.showingTier);
    for (int index = 0; index < this._rankedTeams.Length; ++index)
    {
      if (this.reversedSort)
      {
        int length = this._rankedTeams.Length;
      }
    }
  }

  public int GetTeamRankInCategory(int teamIndex, TeamStatCategory statCategory)
  {
    bool sortInDescOrder = true;
    if (statCategory == TeamStatCategory.Total_Yards_Allowed || statCategory == TeamStatCategory.Pass_Defense || statCategory == TeamStatCategory.Rush_Defense || statCategory == TeamStatCategory.Points_Allowed)
      sortInDescOrder = false;
    int[] numArray = this.OrderTeamsByStat(statCategory, sortInDescOrder, 0);
    for (int teamRankInCategory = 0; teamRankInCategory < numArray.Length; ++teamRankInCategory)
    {
      if (numArray[teamRankInCategory] == teamIndex)
        return teamRankInCategory;
    }
    Debug.Log((object) ("Team not found in list. GetTeamRankInCategory () of TeamStatsManager.cs. Team Index: " + teamIndex.ToString()));
    return -1;
  }

  public void SetPreviousWeekTeamRanks()
  {
    this.seasonModeData.previousWeekTeamRanks[0] = this.GetTeamRankInCategory(this.userTeamIndex, TeamStatCategory.Total_Yards);
    this.seasonModeData.previousWeekTeamRanks[1] = this.GetTeamRankInCategory(this.userTeamIndex, TeamStatCategory.Pass_Yards);
    this.seasonModeData.previousWeekTeamRanks[2] = this.GetTeamRankInCategory(this.userTeamIndex, TeamStatCategory.Rush_Yards);
    this.seasonModeData.previousWeekTeamRanks[3] = this.GetTeamRankInCategory(this.userTeamIndex, TeamStatCategory.Total_Yards_Allowed);
    this.seasonModeData.previousWeekTeamRanks[4] = this.GetTeamRankInCategory(this.userTeamIndex, TeamStatCategory.Pass_Defense);
    this.seasonModeData.previousWeekTeamRanks[5] = this.GetTeamRankInCategory(this.userTeamIndex, TeamStatCategory.Rush_Defense);
  }
}
