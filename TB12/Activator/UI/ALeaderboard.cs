// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.ALeaderboard
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12.Activator.Data;
using UnityEngine;

namespace TB12.Activator.UI
{
  public class ALeaderboard : MonoBehaviour
  {
    [SerializeField]
    private ALeaderboardData _leaderboardData;
    [SerializeField]
    private RecyclingListView _listViewBack;
    [SerializeField]
    private RecyclingListView _listViewFront;
    private bool _initialized;

    private void Awake() => this.Initialize();

    private void OnEnable()
    {
      this._leaderboardData.onDataUpdated += new System.Action(this.DataUpdatedHandler);
      this.DataUpdatedHandler();
    }

    private void OnDisable() => this._leaderboardData.onDataUpdated -= new System.Action(this.DataUpdatedHandler);

    public void Initialize()
    {
      if (this._initialized)
        return;
      if (this._listViewBack.ItemCallback == null)
        this._listViewBack.ItemCallback = new RecyclingListView.ItemDelegate(this._leaderboardData.PopulateBackItem);
      if (this._listViewFront.ItemCallback == null)
        this._listViewFront.ItemCallback = new RecyclingListView.ItemDelegate(this._leaderboardData.PopulateFrontItem);
      this.DataUpdatedHandler();
      this._initialized = true;
    }

    private void DataUpdatedHandler()
    {
      if (this._listViewBack.RowCount == this._leaderboardData.EntryCount)
      {
        this._listViewBack.Refresh();
        this._listViewFront.Refresh();
      }
      this._listViewBack.RowCount = this._leaderboardData.EntryCount;
      this._listViewFront.RowCount = this._leaderboardData.EntryCount;
    }

    public void ScrollToCurrent()
    {
      this._listViewBack.ScrollToRow(this._leaderboardData.CurrentRow);
      this._listViewFront.ScrollToRow(this._leaderboardData.CurrentRow);
    }

    public void UpdateNameAtIndex(string userName)
    {
      this._leaderboardData.UpdateCurrentName(userName);
      this._listViewBack.Refresh(this._leaderboardData.CurrentRow, 1);
      this._listViewFront.Refresh(this._leaderboardData.CurrentRow, 1);
    }

    public void UpdateTeamAtIndex(int teamIndex)
    {
      this._leaderboardData.UpdateTeam(teamIndex);
      this._listViewBack.Refresh(this._leaderboardData.CurrentRow, 1);
      this._listViewFront.Refresh(this._leaderboardData.CurrentRow, 1);
    }
  }
}
