// Decompiled with JetBrains decompiler
// Type: TB12.TeamSelectionView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballVR.UI;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12.AppStates;
using TB12.UI;
using UDB;
using UnityEngine;

namespace TB12
{
  public class TeamSelectionView : UIView, ICircularLayoutDataSource
  {
    [SerializeField]
    private CircularIconItem _prefab;
    [SerializeField]
    private CircularLayout _homeTeamSelection;
    [SerializeField]
    private CircularLayout _awayTeamSelection;
    [SerializeField]
    private TouchToggle _homeSideToggle;
    [SerializeField]
    private TouchToggle _awaySideToggle;
    [SerializeField]
    private TouchToggleGroup _sideSelectionGroup;
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    private TouchButton _backButton;
    [SerializeField]
    private ButtonText _homeSideToggleText;
    [SerializeField]
    private ButtonText _awaySideToggleText;
    [SerializeField]
    private TeamSelection_TeamDetails _homeTeamDetails;
    [SerializeField]
    private TeamSelection_TeamDetails _awayTeamDetails;
    private bool isAppear;
    private int homeTeamIndexSelected;
    private int awayTeamIndexSelected;

    public override Enum ViewId { get; } = (Enum) EScreens.kTeamSelection;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._prefab;

    public int itemCount => SingletonBehaviour<PersistentData, MonoBehaviour>.instance.GetUniformLogoStore().UniformCount;

    protected override void OnInitialize()
    {
      this._homeSideToggle.id = 2;
      this._awaySideToggle.id = 1;
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._okButton.Link(new System.Action(this.HandleOk)),
        (EventHandle) this._backButton.Link(AppEvents.LoadMainMenu),
        (EventHandle) this._sideSelectionGroup.Link<ETeamUniformFlags>(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.PlayerSide),
        (EventHandle) this._homeTeamSelection.Link(new Action<int>(this.HandleHomeTeamChanged)),
        (EventHandle) this._awayTeamSelection.Link(new Action<int>(this.HandleAwayTeamChanged)),
        SingletonBehaviour<PersistentData, MonoBehaviour>.instance.HomeTeamUniform.Link<ETeamUniformId>(new Action<ETeamUniformId>(this._homeTeamDetails.SetTeam)),
        SingletonBehaviour<PersistentData, MonoBehaviour>.instance.AwayTeamUniform.Link<ETeamUniformId>(new Action<ETeamUniformId>(this._awayTeamDetails.SetTeam)),
        SingletonBehaviour<PersistentData, MonoBehaviour>.instance.PlayerSide.Link<ETeamUniformFlags>(new Action<ETeamUniformFlags>(this.HandlePlayerSideChanged))
      });
    }

    protected override void DidAppear() => this.isAppear = true;

    protected override void WillDisappear() => this.isAppear = false;

    private void HandlePlayerSideChanged(ETeamUniformFlags playerSide)
    {
      bool flag = playerSide == ETeamUniformFlags.Home;
      PersistentData.userIsHome = flag;
      if (flag)
      {
        PersistentData.SetUserTeam(TeamDataCache.GetTeam(this._homeTeamSelection.CurrentIndex));
        PersistentData.SetCompTeam(TeamDataCache.GetTeam(this._awayTeamSelection.CurrentIndex));
      }
      else
      {
        PersistentData.SetUserTeam(TeamDataCache.GetTeam(this._awayTeamSelection.CurrentIndex));
        PersistentData.SetCompTeam(TeamDataCache.GetTeam(this._homeTeamSelection.CurrentIndex));
      }
      this._homeSideToggleText.text = flag ? "P1" : "CPU";
      this._awaySideToggleText.text = flag ? "CPU" : "P1";
    }

    protected override void WillAppear()
    {
      UniformLogoStore uniformLogoStore = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.GetUniformLogoStore();
      if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.PlayerSide.Value == ETeamUniformFlags.Home)
      {
        this._homeTeamSelection.CurrentIndex = uniformLogoStore.GetIndex(PersistentData.GetUserUniform());
        this._awayTeamSelection.CurrentIndex = uniformLogoStore.GetIndex(PersistentData.GetCompUniform());
      }
      else
      {
        this._homeTeamSelection.CurrentIndex = uniformLogoStore.GetIndex(PersistentData.GetCompUniform());
        this._awayTeamSelection.CurrentIndex = uniformLogoStore.GetIndex(PersistentData.GetUserUniform());
      }
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      UniformLogo uniformLogo = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.GetUniformLogoStore().GetUniformLogo(itemIndex);
      ((CircularIconItem) item).Icon = uniformLogo.teamLogo;
    }

    private void HandleHomeTeamChanged(int currIndex)
    {
      if (!this.isAppear)
        return;
      if (PersistentData.userIsHome)
        PersistentData.SetUserTeam(TeamDataCache.GetTeam(currIndex));
      else
        PersistentData.SetCompTeam(TeamDataCache.GetTeam(currIndex));
    }

    private void HandleAwayTeamChanged(int currIndex)
    {
      if (!this.isAppear)
        return;
      if (PersistentData.userIsHome)
        PersistentData.SetCompTeam(TeamDataCache.GetTeam(currIndex));
      else
        PersistentData.SetUserTeam(TeamDataCache.GetTeam(currIndex));
    }

    private void HandleOk()
    {
      UIDispatch.FrontScreen.HideView(EScreens.kTeamSelection);
      UnityEngine.Object.FindObjectOfType<GameManager>().ResetStadium((System.Action) (() =>
      {
        AppState.GameInfoUI.ForceValue(true);
        AppSounds.StopSfx(ESfxTypes.kTunnel, 1);
        AppSounds.StopMusic.Trigger();
        AppSounds.AmbienceSound.ForceValue(true);
        AppSounds.AMBIENT_MOD = 0.0f;
        AppSounds.AdjustAmbientVolume(AppSounds.AMBIENT_LOWEST);
        MatchManager.instance.playersManager.CreateTeamPlayers(1);
        MatchManager.instance.playersManager.CreateTeamPlayers(2);
        if (AppState.GameMode == EGameMode.kAxisGame)
        {
          UIDispatch.FrontScreen.DisplayView(EScreens.kCoinToss);
          PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(new Vector3(0.0f, 180f, 0.0f)));
          if ((bool) (UnityEngine.Object) OnFieldCanvas.Instance)
            OnFieldCanvas.Instance.Init();
        }
        else
        {
          UIDispatch.FrontScreen.HideView(EScreens.kTeamSelection);
          MatchManager.instance.CallStart();
        }
        GamePlayerController.CameraFade.Clear(1f);
      }));
    }
  }
}
