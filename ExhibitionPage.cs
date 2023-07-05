// Decompiled with JetBrains decompiler
// Type: ExhibitionPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

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

public class ExhibitionPage : TabletPage
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
  private TouchUI2DButton m_switchHomeButton;
  [SerializeField]
  private TouchUI2DButton m_switchAwayButton;
  [SerializeField]
  private TouchUI2DButton m_startButton;
  [SerializeField]
  private TouchUI2DButton m_backButton;
  private const int TEAM_COUNT = 32;
  private int m_currentTeamIndexHome;
  private int m_currentTeamIndexAway = 1;
  private bool m_userIsHome = true;
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
    this._pageType = TabletPage.Pages.ExhibitionTeamSelect;
    this.m_nextTeamHomeButton.onClick += new System.Action(this.SelectNextTeamHome);
    this.m_prevTeamHomeButton.onClick += new System.Action(this.SelectPrevTeamHome);
    this.m_nextTeamAwayButton.onClick += new System.Action(this.SelectNextTeamAway);
    this.m_prevTeamAwayButton.onClick += new System.Action(this.SelectPrevTeamAway);
    this.m_switchHomeButton.onClick += new System.Action(this.SwitchPlayerToHome);
    this.m_switchAwayButton.onClick += new System.Action(this.SwitchPlayerToAway);
    this.m_backButton.onClick += new System.Action(this.HandleBackButton);
    this.m_startButton.onClick += new System.Action(this.HandleStart);
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.GetUniformLogoStore();
    this.m_currentTeamIndexHome = SeasonModeManager.self.userTeamData.TeamIndex;
    do
    {
      this.m_currentTeamIndexAway = UnityEngine.Random.Range(0, 32);
    }
    while (this.m_currentTeamIndexHome == this.m_currentTeamIndexAway);
    this.SwitchPlayerToHome();
    this.HandleHomeTeamChanged(this.m_currentTeamIndexHome);
    this.HandleAwayTeamChanged(this.m_currentTeamIndexAway);
  }

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);

  private void SelectNextTeamHome()
  {
    this.UpdateTeamIndex(true, true);
    this.HandleHomeTeamChanged(this.m_currentTeamIndexHome);
  }

  private void SelectPrevTeamHome()
  {
    this.UpdateTeamIndex(true, false);
    this.HandleHomeTeamChanged(this.m_currentTeamIndexHome);
  }

  private void SelectNextTeamAway()
  {
    this.UpdateTeamIndex(false, true);
    this.HandleAwayTeamChanged(this.m_currentTeamIndexAway);
  }

  private void SelectPrevTeamAway()
  {
    this.UpdateTeamIndex(false, false);
    this.HandleAwayTeamChanged(this.m_currentTeamIndexAway);
  }

  private void UpdateTeamIndex(bool home, bool pos)
  {
    do
    {
      int num1 = home ? this.m_currentTeamIndexHome : this.m_currentTeamIndexAway;
      int num2;
      if (pos)
      {
        int num3 = num1 - 1;
        num2 = num3 < 0 ? 31 : num3;
      }
      else
        num2 = (num1 + 1) % 32;
      if (home)
        this.m_currentTeamIndexHome = num2;
      else
        this.m_currentTeamIndexAway = num2;
    }
    while (this.m_currentTeamIndexHome == this.m_currentTeamIndexAway);
  }

  private void HandleHomeTeamChanged(int currIndex)
  {
    this.m_homeTeamDetails.SetTeam(currIndex);
    this.SetHomeTeamLogo();
  }

  private void HandleAwayTeamChanged(int currIndex)
  {
    this.m_awayTeamDetails.SetTeam(currIndex);
    this.SetAwayTeamLogo();
  }

  private void SetHomeTeamLogo() => this.m_homeLogoImage.sprite = this.m_uniformStore.GetUniformLogo(this.m_currentTeamIndexHome).teamLogo;

  private void SetAwayTeamLogo() => this.m_awayLogoImage.sprite = this.m_uniformStore.GetUniformLogo(this.m_currentTeamIndexAway).teamLogo;

  private void SwitchPlayerToHome() => this.HandlePlayerSideChanged(true);

  private void SwitchPlayerToAway() => this.HandlePlayerSideChanged(false);

  private void HandlePlayerSideChanged(bool isHome)
  {
    this.m_userIsHome = isHome;
    this.m_switchHomeButton.GetComponent<UnityEngine.UI.Button>().interactable = this.m_switchHomeButton.enabled = !isHome;
    this.m_switchAwayButton.GetComponent<UnityEngine.UI.Button>().interactable = this.m_switchAwayButton.enabled = isHome;
    this.m_switchHomeButton.SetLabelTextColor(!isHome ? Color.white : Color.black);
    this.m_switchAwayButton.SetLabelTextColor(isHome ? Color.white : Color.black);
  }

  private void HandleStart()
  {
    PersistentData.userIsHome = this.m_userIsHome;
    if (this.m_userIsHome)
    {
      PersistentData.SetUserTeam(TeamDataCache.GetTeam(this.m_currentTeamIndexHome));
      PersistentData.SetCompTeam(TeamDataCache.GetTeam(this.m_currentTeamIndexAway));
    }
    else
    {
      PersistentData.SetUserTeam(TeamDataCache.GetTeam(this.m_currentTeamIndexAway));
      PersistentData.SetCompTeam(TeamDataCache.GetTeam(this.m_currentTeamIndexHome));
    }
    PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
    AppState.SeasonMode.Value = ESeasonMode.kUnknown;
    GameplayManager.LoadLevelActivation(EGameMode.kAxisGame, (this.MainPage as MainMenuPage).CurrentTOD);
  }
}
