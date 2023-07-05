// Decompiled with JetBrains decompiler
// Type: TwoMinuteDrillNewDrillPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballVR.UI;
using FootballWorld;
using Framework;
using Framework.Data;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using TB12;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class TwoMinuteDrillNewDrillPage : TabletPage
{
  [SerializeField]
  private UniformLogoStore m_uniformStore;
  [SerializeField]
  private TeamSelection_TeamDetails m_homeTeamDetails;
  [SerializeField]
  private TeamSelection_TeamDetails m_awayTeamDetails;
  [SerializeField]
  private Image m_homeLogoImage;
  [SerializeField]
  private Image m_awayLogoImage;
  [SerializeField]
  private TouchUI2DButton m_nextTeamHomeButton;
  [SerializeField]
  private TouchUI2DButton m_prevTeamHomeButton;
  [SerializeField]
  private TouchUI2DButton m_nextTeamAwayButton;
  [SerializeField]
  private TouchUI2DButton m_prevTeamAwayButton;
  [SerializeField]
  private TouchUI2DButton m_startButton;
  [SerializeField]
  private TouchUI2DButton m_backButton;
  private const int TEAM_COUNT = 32;
  private int m_currentTeamIndexHome;
  private int m_currentTeamIndexAway = 1;
  private bool _linksInitialized;
  private LinksHandler _linksHandlerObj;

  private Save_ExhibitionSettings m_exhibitionData => PersistentSingleton<SaveManager>.Instance.exhibitionSettings;

  protected LinksHandler linksHandler
  {
    get
    {
      if (this._linksInitialized)
        return this._linksHandlerObj;
      this._linksHandlerObj = new LinksHandler(false);
      this._linksInitialized = true;
      return this._linksHandlerObj;
    }
  }

  private void Awake() => this.Initialize();

  private void Initialize()
  {
    this._pageType = TabletPage.Pages.TwoMDNewGame;
    PersistentData.userIsHome = true;
    this.m_nextTeamHomeButton.onClick += new System.Action(this.SelectNextTeamHome);
    this.m_prevTeamHomeButton.onClick += new System.Action(this.SelectPrevTeamHome);
    this.m_nextTeamAwayButton.onClick += new System.Action(this.SelectNextTeamAway);
    this.m_prevTeamAwayButton.onClick += new System.Action(this.SelectPrevTeamAway);
    this.m_backButton.onClick += new System.Action(this.HandleBackButton);
    this.m_startButton.onClick += new System.Action(this.HandleStart);
    UniformLogoStore uniformLogoStore = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.GetUniformLogoStore();
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.PlayerSide.Value == ETeamUniformFlags.Home)
    {
      this.m_currentTeamIndexHome = uniformLogoStore.GetIndex(PersistentData.GetUserUniform());
      this.m_currentTeamIndexAway = uniformLogoStore.GetIndex(PersistentData.GetCompUniform());
    }
    else
    {
      this.m_currentTeamIndexHome = uniformLogoStore.GetIndex(PersistentData.GetCompUniform());
      this.m_currentTeamIndexAway = uniformLogoStore.GetIndex(PersistentData.GetUserUniform());
    }
    this.HandleHomeTeamChanged(this.m_currentTeamIndexHome);
    this.HandleAwayTeamChanged(this.m_currentTeamIndexAway);
  }

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);

  private void SelectNextTeamHome()
  {
    this.m_currentTeamIndexHome = (this.m_currentTeamIndexHome + 1) % 32;
    this.HandleHomeTeamChanged(this.m_currentTeamIndexHome);
  }

  private void SelectPrevTeamHome()
  {
    int num = this.m_currentTeamIndexHome - 1;
    this.m_currentTeamIndexHome = num < 0 ? 31 : num;
    this.HandleHomeTeamChanged(this.m_currentTeamIndexHome);
  }

  private void SelectNextTeamAway()
  {
    this.m_currentTeamIndexAway = (this.m_currentTeamIndexAway + 1) % 32;
    this.HandleAwayTeamChanged(this.m_currentTeamIndexAway);
  }

  private void SelectPrevTeamAway()
  {
    int num = this.m_currentTeamIndexAway - 1;
    this.m_currentTeamIndexAway = num < 0 ? 31 : num;
    this.HandleAwayTeamChanged(this.m_currentTeamIndexAway);
  }

  private void HandleHomeTeamChanged(int currIndex)
  {
    this.m_homeTeamDetails.SetTeam(currIndex);
    this.SetHomeTeamLogo(currIndex);
  }

  private void HandleAwayTeamChanged(int currIndex)
  {
    this.m_awayTeamDetails.SetTeam(currIndex);
    this.SetAwayTeamLogo(currIndex);
  }

  private void SetHomeTeamLogo(int currIndex) => this.m_homeLogoImage.sprite = this.m_uniformStore.GetUniformLogo(currIndex).teamLogo;

  private void SetAwayTeamLogo(int currIndex) => this.m_awayLogoImage.sprite = this.m_uniformStore.GetUniformLogo(currIndex).teamLogo;

  private void HandleStart()
  {
    PersistentData.SetUserTeam(TeamDataCache.GetTeam(this.m_homeTeamDetails.TeamID));
    PersistentData.SetCompTeam(TeamDataCache.GetTeam(this.m_awayTeamDetails.TeamID));
    PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
    AppState.SeasonMode.Value = ESeasonMode.kUnknown;
    GameplayManager.LoadLevelActivation(EGameMode.k2MD, (this.MainPage as MainMenuPage).CurrentTOD);
  }
}
