// Decompiled with JetBrains decompiler
// Type: TB12.SeasonTeamSelectionView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12.UI;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

namespace TB12
{
  public class SeasonTeamSelectionView : UIView, ICircularLayoutDataSource
  {
    [SerializeField]
    private CircularSeasonTeamItem _prefab;
    [SerializeField]
    private TeamDataStore[] _store;
    [SerializeField]
    private CircularLayout _teamSelection;
    [SerializeField]
    private TouchButton _joinTeamButton;
    [SerializeField]
    private Image[] _keyPlayerSprites;
    [SerializeField]
    private TextMeshProUGUI[] _keyPlayerNames;
    [SerializeField]
    private TextMeshProUGUI[] _keyPlayerPositions;
    private RoutineHandle newSeasonRoutine = new RoutineHandle();
    private TeamDataStore _currentTeam;
    private PlayerProfile _playerProfile;
    private bool isCircularLayoutSelected = true;
    private bool isCreateNewSeason;

    public override Enum ViewId { get; } = (Enum) EScreens.kSeasonTeamSelect;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._prefab;

    public int itemCount => this._store.Length;

    protected override void OnInitialize()
    {
      this._playerProfile = SaveManager.GetPlayerProfile();
      this._store = SeasonTeamDataHolder.GetTeamData();
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._joinTeamButton.Link(new System.Action(this.HandleJoinTeam))
      });
    }

    protected override void WillAppear()
    {
      base.WillAppear();
      this._teamSelection.OnCurrentIndexChanged += new Action<int>(this.HandleTeamChanged);
      this._teamSelection.OnDragStart += new System.Action(this.HandleStartDrag);
      this._teamSelection.OnUserRelease += new System.Action(this.HandleEndDrag);
      VRState.LocomotionEnabled.SetValue(false);
      Vector3 vector3 = new Vector3(-3f, 0.0f, 0.0f);
      Quaternion quaternion = Quaternion.Euler(0.0f, 45f, 0.0f);
      VREvents.BlinkMovePlayer.Trigger(1f, vector3, quaternion);
      int num = 0;
      SeasonModeManager.self.seasonModeData.UserTeamIndex = num;
      PersistentData.SetUserTeam(TeamDataCache.GetTeam(num));
      this.HandleTeamChanged(num);
      System.Action onUserTeamChanged = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged;
      if (onUserTeamChanged == null)
        return;
      onUserTeamChanged();
    }

    protected override void WillDisappear()
    {
      base.WillDisappear();
      this._teamSelection.OnCurrentIndexChanged -= new Action<int>(this.HandleTeamChanged);
      this._teamSelection.OnDragStart -= new System.Action(this.HandleStartDrag);
      this._teamSelection.OnUserRelease -= new System.Action(this.HandleEndDrag);
      VRState.LocomotionEnabled.SetValue(true);
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      TeamDataStore teamDataStore = this._store[itemIndex];
      CircularSeasonTeamItem circularSeasonTeamItem = (CircularSeasonTeamItem) item;
      circularSeasonTeamItem.Icon = teamDataStore.Logo;
      circularSeasonTeamItem.TeamName = teamDataStore.TeamName;
      circularSeasonTeamItem.OffensivePower = teamDataStore.OffensivePower.ToString();
      circularSeasonTeamItem.DefensivePower = teamDataStore.DefensivePower.ToString();
    }

    private void HandleTeamChanged(int currIndex)
    {
      this._currentTeam = this._store[currIndex];
      PlayersToWatchData[] playersToWatch = this._currentTeam.PlayersToWatch;
      if (playersToWatch != null)
      {
        for (int index = 0; index < playersToWatch.Length; ++index)
        {
          this._keyPlayerSprites[index].sprite = playersToWatch[index].PlayerIcon;
          this._keyPlayerNames[index].text = playersToWatch[index].Name;
          this._keyPlayerPositions[index].text = playersToWatch[index].Position;
        }
      }
      SeasonModeManager.self.seasonModeData.UserTeamIndex = this._currentTeam.TeamDataIndex;
      System.Action onUserTeamChanged = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged;
      if (onUserTeamChanged == null)
        return;
      onUserTeamChanged();
    }

    private void HandleStartDrag() => this.isCircularLayoutSelected = false;

    private void HandleEndDrag() => this.isCircularLayoutSelected = true;

    private void HandleJoinTeam()
    {
      if (!this.isCircularLayoutSelected || this.isCreateNewSeason)
        return;
      this.isCreateNewSeason = true;
      TeamData team = TeamDataCache.GetTeam(this._currentTeam.TeamDataIndex);
      AnalyticEvents.Record<TeamChosenArgs>(new TeamChosenArgs(team.GetFullDisplayName()));
      PersistentData.SetUserTeam(team);
      SeasonModeManager.self.seasonModeData.UserTeamIndex = this._currentTeam.TeamDataIndex;
      PersistentData.userIsHome = true;
      this._playerProfile.Customization.SetUniform(PersistentData.GetUserUniform());
      this.CreateNewSeason().SafeFireAndForget();
    }

    private async System.Threading.Tasks.Task CreateNewSeason()
    {
      await GamePlayerController.CameraFade.FadeAsync();
      TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.Loading, true);
      await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.34999999403953552));
      SeasonModeManager.self.CreateNewSeasonMode();
      SeasonModeManager.self.StartNewSeasonMode();
      if ((bool) this._playerProfile.Customization.IsNewCustomization)
        this._playerProfile.NewProfile();
      PersistentData.SetUserTeam(TeamDataCache.GetTeam(this._currentTeam.TeamDataIndex));
      this._playerProfile.Customization.SetUniform(PersistentData.GetUserUniform());
      AppEvents.SaveSeasonMode.Trigger();
      if ((bool) this._playerProfile.Customization.IsNewCustomization)
        await UIDispatch.DisplayCAP(false);
      else
        UIDispatch.FrontScreen.CloseScreen();
      await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.34999999403953552));
      TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.Loading, false);
      GamePlayerController.CameraFade.Clear(delay: 0.2f);
      this.isCreateNewSeason = false;
    }
  }
}
