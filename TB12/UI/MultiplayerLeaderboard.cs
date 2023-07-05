// Decompiled with JetBrains decompiler
// Type: TB12.UI.MultiplayerLeaderboard
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballWorld;
using Framework.Data;
using System;
using System.Collections.Generic;
using TB12.Activator.UI;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class MultiplayerLeaderboard : MonoBehaviour
  {
    [SerializeField]
    private MultiplayerStore _store;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private UniformLogoStore _uniformStore;
    [SerializeField]
    private RecyclingListView _listViewBack;
    [SerializeField]
    private RecyclingListView _listViewFront;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake()
    {
      if (this._listViewBack.ItemCallback == null)
        this._listViewBack.ItemCallback = new RecyclingListView.ItemDelegate(this.PopulateBackItem);
      if (this._listViewFront.ItemCallback == null)
        this._listViewFront.ItemCallback = new RecyclingListView.ItemDelegate(this.PopulateFrontItem);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        EventHandle.Link<bool>((Variable<bool>) MultiplayerState.Leaderboards, new Action<bool>(this.HandleState)),
        this._store.OnDataChanged.Link(new System.Action(this.HandleDataChanged))
      });
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void HandleState(bool state) => this.gameObject.SetActive(state);

    private void HandleDataChanged()
    {
      if (this._listViewBack.RowCount == this._store.PlayerDatas.Count)
      {
        this._listViewBack.Refresh();
        this._listViewFront.Refresh();
      }
      else
        this.RebuildData();
    }

    private void PopulateFrontItem(RecyclingListViewItem viewItem, int row)
    {
      ActivatorLeaderboardItemFront leaderboardItemFront = viewItem as ActivatorLeaderboardItemFront;
      if ((UnityEngine.Object) leaderboardItemFront == (UnityEngine.Object) null)
        return;
      PlayerStatsData playerData = this._store.PlayerDatas[row];
      PlayerCustomization playerCustomization;
      if (!this._playerProfile.Profiles.TryGetValue(playerData.playerId, out playerCustomization))
        Debug.LogError((object) "Couldn't find play customizaton profile");
      else
        leaderboardItemFront.Setup(row + 1, playerData.score, (string) playerCustomization.LastName, playerData.playerId == this._store.LocalPlayerId);
    }

    private void PopulateBackItem(RecyclingListViewItem viewItem, int row)
    {
      ActivatorLeaderboardItemBack leaderboardItemBack = viewItem as ActivatorLeaderboardItemBack;
      if ((UnityEngine.Object) leaderboardItemBack == (UnityEngine.Object) null)
        return;
      PlayerStatsData playerData = this._store.PlayerDatas[row];
      PlayerCustomization playerCustomization;
      if (!this._playerProfile.Profiles.TryGetValue(playerData.playerId, out playerCustomization))
      {
        Debug.LogError((object) "Couldn't find play customizaton profile");
      }
      else
      {
        UniformLogo uniformLogo = this._uniformStore.GetUniformLogo((ETeamUniformId) (Variable<ETeamUniformId>) playerCustomization.Uniform);
        Sprite image = uniformLogo != null ? uniformLogo.teamLogo : this._uniformStore.GetUniformLogo(ETeamUniformId.Ravens).teamLogo;
        leaderboardItemBack.Setup(0, playerData.playerId == this._store.LocalPlayerId, image);
      }
    }

    private void OnEnable() => this.RebuildData();

    public void RebuildData()
    {
      this._listViewBack.RowCount = 0;
      this._listViewBack.RowCount = this._store.PlayerDatas.Count;
      this._listViewFront.RowCount = 0;
      this._listViewFront.RowCount = this._store.PlayerDatas.Count;
    }
  }
}
