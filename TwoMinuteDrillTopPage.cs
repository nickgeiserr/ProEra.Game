// Decompiled with JetBrains decompiler
// Type: TwoMinuteDrillTopPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public class TwoMinuteDrillTopPage : TabletPage
{
  [SerializeField]
  private LocalizeStringEvent _bestScoreText;
  [SerializeField]
  private TouchUI2DButton _newDrillButton;
  [SerializeField]
  private TouchUI2DButton _rankingsButton;
  [SerializeField]
  private TouchUI2DButton _challengesButton;
  [SerializeField]
  private TouchUI2DButton _backButton;

  private void Awake()
  {
    this._pageType = TabletPage.Pages.TwoMDTop;
    this._bestScoreText.StringReference.Arguments = (IList<object>) new string[1]
    {
      PersistentSingleton<SaveManager>.Instance.twoMinuteDrill.BestScore.ToString()
    };
    this._newDrillButton.onClick += new System.Action(this.HandleNewDrillButton);
    this._rankingsButton.onClick += new System.Action(this.HandleRankingsButton);
    this._challengesButton.onClick += new System.Action(this.HandleChallengeButton);
    this._backButton.onClick += new System.Action(this.HandleBackButton);
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this._newDrillButton != (UnityEngine.Object) null)
      this._newDrillButton.onClick -= new System.Action(this.HandleNewDrillButton);
    if ((UnityEngine.Object) this._rankingsButton != (UnityEngine.Object) null)
      this._rankingsButton.onClick -= new System.Action(this.HandleRankingsButton);
    if ((UnityEngine.Object) this._challengesButton != (UnityEngine.Object) null)
      this._challengesButton.onClick -= new System.Action(this.HandleChallengeButton);
    if (!((UnityEngine.Object) this._backButton != (UnityEngine.Object) null))
      return;
    this._backButton.onClick -= new System.Action(this.HandleBackButton);
  }

  public void OpenPage(TabletPage.Pages pageType) => (this.MainPage as MainMenuPage).OpenPage(pageType);

  private void HandleNewDrillButton() => this.OpenPage(TabletPage.Pages.TwoMDNewGame);

  private void HandleRankingsButton() => this.OpenPage(TabletPage.Pages.TwoMDLeaderboard);

  private void HandleChallengeButton() => this.OpenPage(TabletPage.Pages.TwoMDChallenges);

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPrevWindow();
}
