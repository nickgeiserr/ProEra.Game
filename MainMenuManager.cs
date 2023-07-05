// Decompiled with JetBrains decompiler
// Type: MainMenuManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using Framework;
using System.Collections;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [Header("Top Section")]
  [SerializeField]
  private RectTransform topSection_Trans;
  [SerializeField]
  private TextMeshProUGUI gameVersion_Txt;
  [SerializeField]
  private Image playerDisplay_Img;
  [SerializeField]
  private Image gradientCover_Img;
  [SerializeField]
  private CanvasGroup playerDisplay_CG;
  [SerializeField]
  private UnityEngine.UI.Button franchiseMode_Btn;
  [SerializeField]
  private UnityEngine.UI.Button playNow_Btn;
  [SerializeField]
  private UnityEngine.UI.Button create_Btn;
  [SerializeField]
  private UnityEngine.UI.Button settings_Btn;
  [SerializeField]
  private Sprite[] playerDisplaySprites;
  [SerializeField]
  private Color[] playerTeamColors;
  [Header("Bottom Section")]
  [SerializeField]
  private RectTransform bottomSection_Trans;
  [SerializeField]
  private RectTransform playNowButton_Trans;
  [SerializeField]
  private RectTransform createButton_Trans;
  [SerializeField]
  private RectTransform settingsButton_Trans;
  [SerializeField]
  private UnityEngine.UI.Button playNowButton_Btn;
  [SerializeField]
  private UnityEngine.UI.Button createButton_Btn;
  [SerializeField]
  private UnityEngine.UI.Button settingsButton_Btn;
  [SerializeField]
  private GameObject activeIndicator_GO;
  [SerializeField]
  private RectTransform activeIndicator_Trans;
  [SerializeField]
  private GameObject bottomMenuContainer_GO;
  [SerializeField]
  private UnityEngine.UI.Button playNow_FirstSelect;
  [SerializeField]
  private UnityEngine.UI.Button create_FirstSelect;
  [SerializeField]
  private UnityEngine.UI.Button settings_FirstSelect;
  [SerializeField]
  private GameObject upArrowBtn_GO;
  private int activeMenuButtonIndex;
  private bool allowControllerInput;
  private bool topSectionBeingShown;
  private int bottomMenuSectionIndex;
  private float activeIndicatorAnimationSpeed = 0.15f;
  private WaitForSecondsRealtime disableMove_WFS;

  private void Start() => this.ShowTopSection();

  public void Init()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.disableMove_WFS = new WaitForSecondsRealtime(0.2f);
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = true;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.gameVersion_Txt.text = SaveIO.gameVersion;
    this.allowControllerInput = true;
    this.ShowTopSection();
    BottomBarManager.instance.SetControllerButtonGuide(16);
    BottomBarManager.instance.ShowQuitButton();
  }

  public void HideWindow()
  {
    this.mainWindow_CG.blocksRaycasts = false;
    ControllerManagerTitle.self.DeselectCurrentUIElement();
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    UISoundManager.instance.PlayButtonBack();
    if (this.topSectionBeingShown)
      return;
    this.ShowTopSection();
  }

  public void ShowTopSection()
  {
    UISoundManager.instance.PlayTabSwipe();
    this.topSectionBeingShown = true;
    LeanTween.move(this.topSection_Trans, (Vector3) new Vector2(0.0f, 0.0f), 0.3f).setEaseOutQuart();
    LeanTween.move(this.bottomSection_Trans, (Vector3) new Vector2(0.0f, this.bottomSection_Trans.rect.height * -1f), 0.3f).setEaseOutQuart();
    BottomBarManager.instance.SetControllerButtonGuide(16);
    BottomBarManager.instance.ShowQuitButton();
    this.HighlightFranchiseMode();
  }

  public void HighlightFranchiseMode()
  {
    this.activeMenuButtonIndex = 0;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.franchiseMode_Btn);
    this.ShowPlayerDisplay(this.activeMenuButtonIndex);
  }

  public void HighlightPlayNow()
  {
    this.activeMenuButtonIndex = 1;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.playNow_Btn);
    this.ShowPlayerDisplay(this.activeMenuButtonIndex);
  }

  public void HighlightCreate()
  {
    this.activeMenuButtonIndex = 2;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.create_Btn);
    this.ShowPlayerDisplay(this.activeMenuButtonIndex);
  }

  public void HighlightSettings()
  {
    this.activeMenuButtonIndex = 3;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.settings_Btn);
    this.ShowPlayerDisplay(this.activeMenuButtonIndex);
  }

  public void SelectFranchiseMode()
  {
    if (PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets && !PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets && ModManager.self.GetCountOfTeamFolders() < 36)
      WarningWindowManager.instance.ShowWindow("YOU MUST HAVE BASE ASSETS ENABLED OR AT LEAST 36 CUSTOM TEAMS TO PLAY FRANCHISE MODE.");
    else
      this.HideWindow();
  }

  public void SelectPlayNow()
  {
    this.ShowBottomSection();
    this.ShowPlayNowSection();
  }

  public void SelectCreate()
  {
    this.ShowBottomSection();
    this.ShowCreateSection();
  }

  public void SelectSettings()
  {
    this.ShowBottomSection();
    this.ShowSettingsSection();
  }

  public void ShowPlayerDisplay(int index)
  {
    this.activeMenuButtonIndex = index;
    this.StartCoroutine(this.BeingPlayerTransition(index));
  }

  private IEnumerator BeingPlayerTransition(int index)
  {
    this.gradientCover_Img.CrossFadeColor(this.playerTeamColors[index], 0.1f, true, true);
    LeanTween.alphaCanvas(this.playerDisplay_CG, 0.0f, 0.03f);
    yield return (object) new WaitForSeconds(0.03f);
    this.playerDisplay_Img.sprite = this.playerDisplaySprites[index];
    LeanTween.alphaCanvas(this.playerDisplay_CG, 1f, 0.03f);
  }

  public void ShowBottomSection()
  {
    PersistentData.gameType = GameType.QuickMatch;
    this.topSectionBeingShown = false;
    LeanTween.move(this.topSection_Trans, (Vector3) new Vector2(0.0f, this.topSection_Trans.rect.height), 0.3f).setEaseOutQuart();
    LeanTween.move(this.bottomSection_Trans, (Vector3) new Vector2(0.0f, 0.0f), 0.3f).setEaseOutQuart();
    LeanTween.moveLocalX(this.bottomMenuContainer_GO, 0.0f, 0.0f);
    this.ShowPlayNowSection();
    BottomBarManager.instance.SetControllerButtonGuide(3);
    BottomBarManager.instance.ShowBackButton();
  }

  public void ShowPlayNowSection()
  {
    UISoundManager.instance.PlayTabSwipe();
    this.bottomMenuSectionIndex = 0;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.playNowButton_Trans.localPosition.x, this.activeIndicatorAnimationSpeed);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.playNowButton_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed);
    LeanTween.moveLocalX(this.bottomMenuContainer_GO, (float) this.bottomMenuSectionIndex * -1f * this.bottomSection_Trans.rect.width, this.activeIndicatorAnimationSpeed);
    this.playNowButton_Btn.interactable = false;
    this.createButton_Btn.interactable = true;
    this.settingsButton_Btn.interactable = true;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.playNow_FirstSelect);
  }

  public void ShowCreateSection()
  {
    UISoundManager.instance.PlayTabSwipe();
    this.bottomMenuSectionIndex = 1;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.createButton_Trans.localPosition.x, this.activeIndicatorAnimationSpeed);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.createButton_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed);
    LeanTween.moveLocalX(this.bottomMenuContainer_GO, (float) this.bottomMenuSectionIndex * -1f * this.bottomSection_Trans.rect.width, this.activeIndicatorAnimationSpeed);
    this.playNowButton_Btn.interactable = true;
    this.createButton_Btn.interactable = false;
    this.settingsButton_Btn.interactable = true;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.create_FirstSelect);
  }

  public void ShowSettingsSection()
  {
    UISoundManager.instance.PlayTabSwipe();
    this.bottomMenuSectionIndex = 2;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.settingsButton_Trans.localPosition.x, this.activeIndicatorAnimationSpeed);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.settingsButton_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed);
    LeanTween.moveLocalX(this.bottomMenuContainer_GO, (float) this.bottomMenuSectionIndex * -1f * this.bottomSection_Trans.rect.width, this.activeIndicatorAnimationSpeed);
    this.playNowButton_Btn.interactable = true;
    this.createButton_Btn.interactable = true;
    this.settingsButton_Btn.interactable = false;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.settings_FirstSelect);
  }

  public void SelectExhibition()
  {
    PersistentData.SetGameMode(GameMode.PlayerVsAI);
    this.StartCoroutine(this.ProceedToTeamSelect());
  }

  public void SelectCoachMode()
  {
    PersistentData.SetGameMode(GameMode.Coach);
    this.StartCoroutine(this.ProceedToTeamSelect());
  }

  public void SelectLocal2P()
  {
    PersistentData.SetGameMode(GameMode.PlayerVsPlayer);
    this.StartCoroutine(this.ProceedToTeamSelect());
  }

  public void SelectSpectate()
  {
    PersistentData.SetGameMode(GameMode.Spectate);
    this.StartCoroutine(this.ProceedToTeamSelect());
  }

  public void Select2PCoachMode()
  {
    PersistentData.SetGameMode(GameMode.PlayerVsPlayerCoach);
    this.StartCoroutine(this.ProceedToTeamSelect());
  }

  private IEnumerator ProceedToTeamSelect()
  {
    this.HideWindow();
    yield return (object) new WaitForSeconds(0.3f);
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSelect.ShowWindow();
  }

  public void ShowAudioOptions()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowAudioOptions();
  }

  public void ShowVideoOptions()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowVideoOptions();
  }

  public void ShowGameOptions()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowGameOptions();
  }

  public void ShowPenaltyOptions()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowPenaltyOptions();
  }

  public void ShowDataOptions()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.optionsMenu.ShowDataOptions();
  }

  public void ShowUniformEditor()
  {
    PersistentData.homeTeamUniform = UniformAssetManager.GetUniformSet(0);
    LoadingScreenManager.self.LoadScene("Uniform Editor", "Loading Uniform Editor");
  }

  public void ShowPlayEditor()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.playEditor.ShowWindow();
  }

  public void ShowTeamSuiteManager()
  {
    this.HideWindow();
    TeamDataCache.ClearTeamDataCache();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowCreateEditSelctWindow();
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || !ControllerManagerTitle.self.usingController || !this.allowControllerInput || BottomBarManager.instance.IsConfirmExitVisible() || WarningWindowManager.instance.IsVisible())
      return;
    float num1 = UserManager.instance.LeftStickX(Player.One);
    float num2 = UserManager.instance.LeftStickY(Player.One);
    if (this.topSectionBeingShown)
    {
      if ((double) num1 < -0.5)
      {
        this.SelectLeft_TopMenu();
        this.StartCoroutine(this.DisableMove());
      }
      else if ((double) num1 > 0.5)
      {
        this.SelectRight_TopMenu();
        this.StartCoroutine(this.DisableMove());
      }
      if ((double) num2 >= -0.5)
        return;
      this.ShowBottomSection();
      this.StartCoroutine(this.DisableMove());
    }
    else
    {
      if ((double) num2 > 0.5 && this.IsTopMenuButtonSelected())
      {
        this.ShowTopSection();
        this.StartCoroutine(this.DisableMove());
      }
      if (UserManager.instance.RightBumperWasPressed(Player.One))
        this.SelectRight_BottomMenu();
      if (!UserManager.instance.LeftBumperWasPressed(Player.One))
        return;
      this.SelectLeft_BottomMenu();
    }
  }

  private void SelectLeft_TopMenu()
  {
    if (this.activeMenuButtonIndex == 0)
      this.HighlightSettings();
    else if (this.activeMenuButtonIndex == 1)
      this.HighlightFranchiseMode();
    else if (this.activeMenuButtonIndex == 2)
    {
      this.HighlightPlayNow();
    }
    else
    {
      if (this.activeMenuButtonIndex != 3)
        return;
      this.HighlightCreate();
    }
  }

  private void SelectRight_TopMenu()
  {
    if (this.activeMenuButtonIndex == 0)
      this.HighlightPlayNow();
    else if (this.activeMenuButtonIndex == 1)
      this.HighlightCreate();
    else if (this.activeMenuButtonIndex == 2)
    {
      this.HighlightSettings();
    }
    else
    {
      if (this.activeMenuButtonIndex != 3)
        return;
      this.HighlightFranchiseMode();
    }
  }

  private void SelectLeft_BottomMenu()
  {
    if (this.bottomMenuSectionIndex == 2)
      this.ShowCreateSection();
    else if (this.bottomMenuSectionIndex == 1)
      this.ShowPlayNowSection();
    else
      this.ShowSettingsSection();
  }

  private void SelectRight_BottomMenu()
  {
    if (this.bottomMenuSectionIndex == 0)
      this.ShowCreateSection();
    else if (this.bottomMenuSectionIndex == 1)
      this.ShowSettingsSection();
    else
      this.ShowPlayNowSection();
  }

  private bool IsTopMenuButtonSelected() => (Object) ControllerManagerTitle.self.GetCurrentSelectedUIElement() == (Object) this.upArrowBtn_GO;

  private IEnumerator DisableMove()
  {
    this.allowControllerInput = false;
    yield return (object) this.disableMove_WFS;
    this.allowControllerInput = true;
  }
}
