// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.GameStates.LockerRoom.MainMenu.TabletPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12;
using UnityEngine;

namespace ProEra.Game.Sources.GameStates.LockerRoom.MainMenu
{
  public class TabletPage : MonoBehaviour, ITabletPageBase
  {
    [SerializeField]
    protected TabletPage.Pages _pageType;
    protected TabletPage MainPage;
    protected EGameMode miniGameType;
    [SerializeField]
    protected bool showTODToggle;

    public void OpenWindow()
    {
      if ((bool) (Object) this.MainPage && this.MainPage is MainMenuPage)
        (this.MainPage as MainMenuPage).EnableTODToggle(this.showTODToggle);
      this.gameObject.SetActive(true);
    }

    public void CloseWindow() => this.gameObject.SetActive(false);

    public TabletPage.Pages GetPageType() => this._pageType;

    public void RegisterMainPage(TabletPage value) => this.MainPage = value;

    public enum Pages
    {
      Unknown,
      Main,
      MiniCamp,
      Settings,
      MultiplayerMain,
      MultiplayerLoading,
      MultiplayerCreate,
      MultiplayerJoin,
      MultiplayerPin,
      MutiplayerFilter,
      MiniGamesRankingMenu,
      MiniGamesRanking,
      GameStats,
      DevConsole,
      DevSettings,
      DevGameplay,
      DevMultiplayer,
      DevAI,
      DevAudio,
      DevSocial,
      DevGraphics,
      DevJewelCase,
      DevAchievements,
      MiniCampProgression,
      ExhibitionTeamSelect,
      TwoMDTop,
      TwoMDNewGame,
      TwoMDLeaderboard,
      TwoMDChallenges,
      LeaderboardDisplay,
      LeaderboardMenu,
      CreateMultiplayerLoading,
      JoinMultiplayerLoading,
      OpenLeaderBoardLoading,
    }
  }
}
