// Decompiled with JetBrains decompiler
// Type: TB12.UI.LeaderboardScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12.Activator.UI;
using TB12.GameplayData;
using TB12.Solo.Data;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class LeaderboardScreen : UIView
  {
    [SerializeField]
    private GameLevelsStore _levelsStore;
    [SerializeField]
    private SoloLeaderboardData _soloLeaderboardData;
    [SerializeField]
    private RecyclingListView _listView;
    [SerializeField]
    private TextMeshProUGUI _headerText;
    private readonly List<SoloLeaderboardData.Entry> _soloData = new List<SoloLeaderboardData.Entry>();

    public override Enum ViewId { get; } = (Enum) EScreens.kLeaderboards;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      EventHandle.Link<GameLevel>(this._levelsStore.CurrentLevel, new Action<GameLevel>(this.CurrentLevelChangedHandler))
    });

    private void CurrentLevelChangedHandler(GameLevel level)
    {
      this._listView.RowCount = 0;
      this._soloData.Clear();
      this._headerText.text = "Level " + level.name;
      this._soloLeaderboardData.GetLevelTop(ASummaryScreen.GetLeaderURLByGameType(AppState.GameMode));
    }

    protected override void WillAppear()
    {
      if (this._listView.ItemCallback == null)
        this._listView.ItemCallback = new RecyclingListView.ItemDelegate(this.PopulateItem);
      this._soloLeaderboardData.onLevelDataUpdated += new Action<string>(this.SoloLevelUpdatedHandler);
    }

    protected override void WillDisappear() => this._soloLeaderboardData.onLevelDataUpdated -= new Action<string>(this.SoloLevelUpdatedHandler);

    private void PopulateItem(RecyclingListViewItem viewItem, int row)
    {
      LeaderboardItem leaderboardItem = viewItem as LeaderboardItem;
      if ((UnityEngine.Object) leaderboardItem == (UnityEngine.Object) null)
        return;
      SoloLeaderboardData.Entry entry = this._soloData[row];
      leaderboardItem.Setup(row + 1, Convert.ToInt32(entry.score), entry.name);
    }

    private void SoloLevelUpdatedHandler(string levelName)
    {
      this._listView.RowCount = 0;
      if (this._listView.ItemCallback == null)
        this._listView.ItemCallback = new RecyclingListView.ItemDelegate(this.PopulateItem);
      this._soloData.Clear();
      List<SoloLeaderboardData.Entry> entries = this._soloLeaderboardData.GetEntries(levelName);
      if (entries == null)
      {
        Debug.LogError((object) ("No entries found for " + levelName + ". Aborting.."));
      }
      else
      {
        this._soloData.AddRange((IEnumerable<SoloLeaderboardData.Entry>) entries);
        this._listView.RowCount = this._soloData.Count;
      }
    }
  }
}
