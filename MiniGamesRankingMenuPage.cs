// Decompiled with JetBrains decompiler
// Type: MiniGamesRankingMenuPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using TB12;
using UnityEngine;

public class MiniGamesRankingMenuPage : TabletPage
{
  [SerializeField]
  private TouchUI2DButton _passChallengeButton;
  [SerializeField]
  private TouchUI2DButton _agilityDrillButton;
  [SerializeField]
  private TouchUI2DButton _backButton;

  private void Awake()
  {
    this._pageType = TabletPage.Pages.MiniGamesRankingMenu;
    this._passChallengeButton.onClick += new System.Action(this.HandlePassChallengeButton);
    this._agilityDrillButton.onClick += new System.Action(this.HandleAgilityDrillButton);
    this._backButton.onClick += new System.Action(this.HandleBackButton);
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this._passChallengeButton != (UnityEngine.Object) null)
      this._passChallengeButton.onClick -= new System.Action(this.HandlePassChallengeButton);
    if ((UnityEngine.Object) this._agilityDrillButton != (UnityEngine.Object) null)
      this._agilityDrillButton.onClick -= new System.Action(this.HandleAgilityDrillButton);
    if (!((UnityEngine.Object) this._backButton != (UnityEngine.Object) null))
      return;
    this._backButton.onClick -= new System.Action(this.HandleBackButton);
  }

  private void HandleAgilityDrillButton()
  {
    MainMenuPage mainPage = this.MainPage as MainMenuPage;
    mainPage.MiniGameType = EGameMode.kAgility;
    mainPage.OpenPage(TabletPage.Pages.MiniGamesRanking);
  }

  private void HandlePassChallengeButton()
  {
    MainMenuPage mainPage = this.MainPage as MainMenuPage;
    mainPage.MiniGameType = EGameMode.kThrow;
    mainPage.OpenPage(TabletPage.Pages.MiniGamesRanking);
  }

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MiniCamp);
}
