// Decompiled with JetBrains decompiler
// Type: TB12.UI.CustomizeTeamBallView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using FootballWorld;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.UI
{
  public class CustomizeTeamBallView : MonoBehaviour, ICircularLayoutDataSource
  {
    private const bool devMode = false;
    [SerializeField]
    private PlayerProfile _playerProfile;
    private PlayerCustomization _customization;
    [SerializeField]
    private TeamBallMatStore _store;
    [SerializeField]
    private CircularTextItem _itemPrefab;
    [SerializeField]
    private CircularLayout _scrollLayout;
    [SerializeField]
    private TouchButton _okButton;
    private List<TeamBallConfig> _unlockedSelection = new List<TeamBallConfig>();

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._itemPrefab;

    public int itemCount => this._store.teamBallCount;

    private void Start()
    {
    }

    private void OnEnable()
    {
      this.WillAppear();
      this.DidAppear();
    }

    private void OnDisable() => this.WillDisappear();

    private void WillAppear()
    {
      this._scrollLayout.Deinitialize();
      this.PopulateUnlockedSelections();
      this._scrollLayout.Initialize();
      int configId = this._store.GetConfigId((int) this._playerProfile.Customization.MultiplayerTeamBallId);
      if (configId >= 0)
        this._scrollLayout.CurrentIndex = configId;
      else
        this._scrollLayout.CurrentIndex = 0;
    }

    private void DidAppear() => this._scrollLayout.OnCurrentIndexChanged += new Action<int>(this.HandleCurrentIndexChanged);

    private void WillDisappear()
    {
      this._scrollLayout.OnCurrentIndexChanged -= new Action<int>(this.HandleCurrentIndexChanged);
      this._playerProfile.Customization.SetDirty();
    }

    private void HandleCurrentIndexChanged(int currIndex)
    {
      TeamBallConfig teamBallConfig = this._unlockedSelection[currIndex % this._unlockedSelection.Count];
      if (teamBallConfig == null)
        return;
      int teamId = (int) teamBallConfig.TeamId;
      this._playerProfile.Customization.MultiplayerTeamBallId.SetValue(teamId);
      this._store.selectedTeamBall = teamId;
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      CircularTextItem circularTextItem = (CircularTextItem) item;
      circularTextItem.IsLocalized = false;
      int index = itemIndex;
      if (itemIndex >= this._unlockedSelection.Count)
        index = itemIndex - (int) Mathf.Floor((float) (itemIndex / this._unlockedSelection.Count)) * this._unlockedSelection.Count;
      TeamBallConfig teamBallConfig = this._unlockedSelection[index];
      int teamId = (int) teamBallConfig.TeamId;
      circularTextItem.localizationText = teamBallConfig.TeamId.ToString();
    }

    private void PopulateUnlockedSelections()
    {
      this._unlockedSelection.Clear();
      this._unlockedSelection.Add(this._store.GetTeamBallConfig(ETeamBallID.Duke));
      this._unlockedSelection.Add(this._store.GetTeamBallConfig(ETeamBallID.StatusPro));
      int teamBallCount = this._store.teamBallCount;
      for (int index = 0; index < teamBallCount; ++index)
      {
        TeamBallConfig teamBallConfig = this._store.GetTeamBallConfig(index);
        int teamId = (int) teamBallConfig.TeamId;
        if ((teamId < 0 ? 0 : (SeasonModeManager.self.CareerTeamsBeaten.Contains(teamId) ? 1 : 0)) != 0)
          this._unlockedSelection.Add(teamBallConfig);
      }
      foreach (TeamBallConfig teamBallConfig in this._unlockedSelection)
        this._scrollLayout.AddItem((CircularLayoutItem) this._itemPrefab);
    }
  }
}
