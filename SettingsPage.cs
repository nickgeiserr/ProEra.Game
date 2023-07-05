// Decompiled with JetBrains decompiler
// Type: SettingsPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using UnityEngine;

public class SettingsPage : TabletPage
{
  [SerializeField]
  private TouchUI2DButton newSeasonButton;
  [SerializeField]
  private TouchUI2DButton backButton;

  private void Awake()
  {
    this._pageType = TabletPage.Pages.Settings;
    this.backButton.onClick += new System.Action(this.HandleBackButton);
    this.newSeasonButton.onClick += new System.Action(this.HandleNewSeasonButton);
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this.backButton != (UnityEngine.Object) null)
      this.backButton.onClick -= new System.Action(this.HandleBackButton);
    if (!((UnityEngine.Object) this.newSeasonButton != (UnityEngine.Object) null))
      return;
    this.newSeasonButton.onClick -= new System.Action(this.HandleNewSeasonButton);
  }

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);

  private void HandleNewSeasonButton()
  {
    (this.MainPage as MainMenuPage).PutMeBack();
    SeasonModeManager.self.UICreateNewSeason();
  }
}
