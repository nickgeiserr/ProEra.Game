// Decompiled with JetBrains decompiler
// Type: TitleScreenManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : SingletonBehaviour<TitleScreenManager, MonoBehaviour>
{
  [SerializeField]
  private GraphicRaycaster graphicRaycaster;
  [Header("Start Screen")]
  [SerializeField]
  private StartScreenManager startScreen;
  [Header("Main Menu")]
  public MainMenuManager mainMenu;
  [Header("Game Setup")]
  public GameSetupManager gameSetup;
  [Header("Team Select")]
  public TeamSelectManager teamSelect;
  [Header("Options Menu")]
  public OptionsMenuManager optionsMenu;
  [Header("Franchise Team Select")]
  [Header("Team Suite Manager")]
  public TeamSuiteManager teamSuiteManager;
  [Header("Play Editor")]
  public PlayEditor playEditor;
  [Header("Xbox")]
  public XboxEngagementManager xboxEngagementManager;

  private void Start() => this.StartCoroutine(this.RunStartupSequence());

  private IEnumerator RunStartupSequence()
  {
    TitleScreenManager titleScreenManager = this;
    titleScreenManager.mainMenu.Init();
    yield return (object) null;
    LoadingScreenManager.self.SetLoadingText("Initializing Title Screen 8%");
    LoadingScreenManager.self.SetLoadingBarFill(0.08f);
    titleScreenManager.teamSuiteManager.Init();
    titleScreenManager.playEditor.Init();
    titleScreenManager.teamSelect.Init();
    titleScreenManager.gameSetup.Init();
    titleScreenManager.optionsMenu.Init();
    yield return (object) null;
    LoadingScreenManager.self.SetLoadingText("Initializing Title Screen 26%");
    LoadingScreenManager.self.SetLoadingBarFill(0.26f);
    BottomBarManager.instance.Init();
    yield return (object) null;
    LoadingScreenManager.self.SetLoadingText("Initializing Title Screen 55%");
    LoadingScreenManager.self.SetLoadingBarFill(0.55f);
    if (!titleScreenManager.startScreen.IsVisible())
      titleScreenManager.mainMenu.ShowWindow();
    Cursor.visible = true;
    titleScreenManager.graphicRaycaster.enabled = true;
    yield return (object) null;
    LoadingScreenManager.self.SetLoadingText("Initializing Title Screen 62%");
    LoadingScreenManager.self.SetLoadingBarFill(0.62f);
    if (PersistentData.simulateWeek || PersistentData.showFranchise)
    {
      titleScreenManager.startScreen.HideWindow();
      titleScreenManager.mainMenu.HideWindow();
      BottomBarManager.instance.ShowBackButton();
      BottomBarManager.instance.ShowWindow();
    }
    else if (PersistentData.previousScene != "" && PersistentData.previousScene != "Splash Screen")
    {
      titleScreenManager.startScreen.HideWindow();
      titleScreenManager.mainMenu.ShowWindow();
    }
    yield return (object) null;
    LoadingScreenManager.self.SetLoadingText("Initializing Title Screen 79%");
    LoadingScreenManager.self.SetLoadingBarFill(0.79f);
    titleScreenManager.StartCoroutine(titleScreenManager.BeginBackgroundMusic());
    yield return (object) null;
    LoadingScreenManager.self.SetLoadingText("Initializing Title Screen 91%");
    LoadingScreenManager.self.SetLoadingBarFill(0.91f);
    if (PersistentData.previousScene != "" && PersistentData.previousScene != "Splash Screen")
    {
      UniformWriter.ClearTextureCache();
      yield return (object) null;
      LoadingScreenManager.self.SetLoadingText("Initializing Title Screen 99%");
      LoadingScreenManager.self.SetLoadingBarFill(0.99f);
      yield return (object) null;
      LoadingScreenManager.self.HideWindowAfterDelay(80);
    }
    else
      LoadingScreenManager.self.HideWindow();
  }

  private IEnumerator BeginBackgroundMusic()
  {
    yield return (object) new WaitForSeconds(1f);
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.Escape) || PopupLoadingScreen.self.screenIsLoading)
      return;
    if (BottomBarManager.instance.IsConfirmExitVisible())
      BottomBarManager.instance.HideConfirmExit();
    else if (BottomBarManager.instance.IsBackButtonVisible())
    {
      this.ReturnToPreviousMenu();
    }
    else
    {
      if (!this.mainMenu.IsVisible())
        return;
      BottomBarManager.instance.ToggleConfirmExit();
    }
  }

  public void ReturnToPreviousMenu(int player = 1)
  {
    if (WarningWindowManager.instance.IsVisible())
      WarningWindowManager.instance.ReturnToPreviousMenu();
    else if (OnScreenKeyboard.instance.IsVisible())
      OnScreenKeyboard.instance.ReturnToPreviousMenu();
    else if (OnScreenColorSelector.instance.IsVisible())
      OnScreenColorSelector.instance.ReturnToPreviousMenu();
    else if (this.mainMenu.IsVisible())
      this.mainMenu.ReturnToPreviousMenu();
    else if (this.teamSelect.IsVisible())
      this.teamSelect.ReturnToPreviousMenu(player);
    else if (this.gameSetup.IsVisible())
      this.gameSetup.ReturnToPreviousMenu();
    else if (this.optionsMenu.IsVisible())
      this.optionsMenu.ReturnToPreviousMenu();
    else if (this.teamSuiteManager.IsVisible())
      this.teamSuiteManager.ReturnToPreviousMenu();
    else if (this.playEditor.IsVisible())
    {
      this.playEditor.ReturnToPreviousMenu();
    }
    else
    {
      UISoundManager.instance.PlayButtonBack();
      BottomBarManager.instance.ShowQuitButton();
      this.mainMenu.ShowWindow();
    }
  }

  public void ReturnToFranchiseMode()
  {
    this.teamSelect.HideWindow();
    PersistentData.simulateWeek = false;
  }
}
