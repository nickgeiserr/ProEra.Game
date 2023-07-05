// Decompiled with JetBrains decompiler
// Type: BottomBarManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using System.Collections;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarManager : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow_GO;
  [SerializeField]
  private CanvasGroup bottomBarCover_CG;
  [SerializeField]
  private GameObject quitButton_GO;
  [SerializeField]
  private GameObject backButton_GO;
  [SerializeField]
  private GameObject[] controllerButtonsGuide;
  [SerializeField]
  private CanvasGroup controllerButtonGuide_CG;
  [SerializeField]
  private CanvasGroup confirmExit_CG;
  [SerializeField]
  private UnityEngine.UI.Button yes_Btn;
  [SerializeField]
  private UnityEngine.UI.Button no_Btn;
  public static BottomBarManager instance;
  private bool inTitleScreen;
  private WaitForSecondsRealtime buttonFadeTime_WFS;

  private void Awake() => BottomBarManager.instance = this;

  public void Init()
  {
    if ((Object) ControllerManagerGame.self != (Object) null)
    {
      this.inTitleScreen = false;
      this.HideWindow();
    }
    else
      this.inTitleScreen = true;
    this.bottomBarCover_CG.alpha = 0.0f;
    this.buttonFadeTime_WFS = new WaitForSecondsRealtime(0.2f);
  }

  private void Update() => this.ManageControllerSupport();

  public void ShowWindow() => this.mainWindow_GO.SetActive(true);

  public void HideWindow() => this.mainWindow_GO.SetActive(false);

  public bool IsVisible() => this.mainWindow_GO.activeInHierarchy;

  public void ShowBackButton()
  {
    this.backButton_GO.SetActive(true);
    this.HideQuitButton();
  }

  public void HideBackButton() => this.backButton_GO.SetActive(false);

  public bool IsBackButtonVisible() => this.backButton_GO.activeInHierarchy;

  public void ShowQuitButton()
  {
    this.quitButton_GO.SetActive(true);
    this.HideBackButton();
  }

  private void HideQuitButton() => this.quitButton_GO.SetActive(false);

  public bool IsQuitButtonVisible() => this.quitButton_GO.activeInHierarchy;

  public void ShowGradientCover() => LeanTween.alphaCanvas(this.bottomBarCover_CG, 1f, 0.3f).setIgnoreTimeScale(true);

  public void HideGradientCover() => LeanTween.alphaCanvas(this.bottomBarCover_CG, 0.0f, 0.3f).setIgnoreTimeScale(true);

  public void ReturnToPreviousMenu()
  {
    if (this.inTitleScreen)
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.ReturnToPreviousMenu();
    else
      GUIManager.instance.ReturnToPreviousMenu();
  }

  public void ToggleConfirmExit()
  {
    if (this.IsConfirmExitVisible())
      this.HideConfirmExit();
    else
      this.ShowConfirmExit();
  }

  public void ShowConfirmExit()
  {
    this.confirmExit_CG.blocksRaycasts = true;
    LeanTween.alphaCanvas(this.confirmExit_CG, 1f, 0.3f).setIgnoreTimeScale(true);
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.yes_Btn);
  }

  public void HideConfirmExit()
  {
    this.confirmExit_CG.blocksRaycasts = false;
    LeanTween.alphaCanvas(this.confirmExit_CG, 0.0f, 0.3f).setIgnoreTimeScale(true);
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.HighlightFranchiseMode();
  }

  public bool IsConfirmExitVisible() => (double) this.confirmExit_CG.alpha > 0.0;

  public void QuitGame() => Application.Quit();

  public void SetControllerButtonGuide(int buttonGuideIndex)
  {
    this.HideGradientCover();
    if (!this.IsUsingController())
      return;
    this.StartCoroutine(this.BeginButtonGuideFade(buttonGuideIndex));
  }

  public void HideAllControllerButtonGuides()
  {
    for (int index = 0; index < this.controllerButtonsGuide.Length; ++index)
      this.controllerButtonsGuide[index].SetActive(false);
  }

  private bool IsUsingController() => this.inTitleScreen ? ControllerManagerTitle.self.usingController : ControllerManagerGame.usingController;

  private IEnumerator BeginButtonGuideFade(int buttonGuideIndex)
  {
    LeanTween.alphaCanvas(this.controllerButtonGuide_CG, 0.0f, 0.15f).setIgnoreTimeScale(true);
    yield return (object) this.buttonFadeTime_WFS;
    this.HideAllControllerButtonGuides();
    this.controllerButtonsGuide[buttonGuideIndex].SetActive(true);
    LeanTween.alphaCanvas(this.controllerButtonGuide_CG, 1f, 0.15f).setIgnoreTimeScale(true);
  }

  private void ManageControllerSupport()
  {
    if (!this.inTitleScreen || !this.IsQuitButtonVisible() || WarningWindowManager.instance.IsVisible() || !UserManager.instance.StartWasPressed(Player.One) && !UserManager.instance.OptionWasPressed(Player.One) && !UserManager.instance.MenuWasPressed(Player.One) && !UserManager.instance.BackWasPressed(Player.One) && !UserManager.instance.ViewWasPressed(Player.One))
      return;
    this.ToggleConfirmExit();
  }
}
