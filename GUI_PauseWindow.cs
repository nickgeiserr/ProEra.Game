// Decompiled with JetBrains decompiler
// Type: GUI_PauseWindow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using Framework;
using ProEra.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI_PauseWindow : MonoBehaviour, IPauseWindow
{
  [Header("Main Window")]
  [SerializeField]
  private CanvasGroup parentWindow_CG;
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private UnityEngine.UI.Button resumeGame_Btn;
  [SerializeField]
  private TextMeshProUGUI resumeGame_Txt;
  [SerializeField]
  private GameObject sectionContainer_GO;
  [SerializeField]
  private RectTransform sectionContainer_Trans;
  [SerializeField]
  private RectTransform gameButton_Trans;
  [SerializeField]
  private RectTransform statsButton_Trans;
  [SerializeField]
  private RectTransform settingsButton_Trans;
  [SerializeField]
  private RectTransform controlsButton_Trans;
  [SerializeField]
  private UnityEngine.UI.Button game_Btn;
  [SerializeField]
  private UnityEngine.UI.Button stats_Btn;
  [SerializeField]
  private UnityEngine.UI.Button settings_Btn;
  [SerializeField]
  private UnityEngine.UI.Button controls_Btn;
  [SerializeField]
  private GameObject activeIndicator_GO;
  [SerializeField]
  private RectTransform activeIndicator_Trans;
  [SerializeField]
  private TextMeshProUGUI startSecondHalf_Txt;
  private int sectionIndex;
  private GUI_PauseWindow.PauseWindowState pauseWindowState;
  [Header("Game Section - Drive Stats")]
  [SerializeField]
  private Image offensiveTeamLogo_Img;
  [SerializeField]
  private TextMeshProUGUI plays_Txt;
  [SerializeField]
  private TextMeshProUGUI yards_Txt;
  [SerializeField]
  private TextMeshProUGUI timeOfPossession_Txt;
  [SerializeField]
  private TextMeshProUGUI firstDowns_Txt;
  [SerializeField]
  private TextMeshProUGUI rushPlays_Txt;
  [SerializeField]
  private TextMeshProUGUI passPlays_Txt;
  [SerializeField]
  private TextMeshProUGUI rushYards_Txt;
  [SerializeField]
  private TextMeshProUGUI passYards_Txt;
  [SerializeField]
  private UnityEngine.UI.Button callTimeout_Btn;
  [SerializeField]
  private UnityEngine.UI.Button depthChart_Btn;
  [SerializeField]
  private UnityEngine.UI.Button quitGame_Btn;
  [SerializeField]
  private GameObject callTimeoutButton_GO;
  [SerializeField]
  private GameObject quitGameButton_GO;
  private Navigation resumeGameBtn_Nav;
  private Navigation depthChartBtn_Nav;
  [Header("Game Section - Box Score")]
  [SerializeField]
  private TextMeshProUGUI[] homeTeamBoxScore_Txt;
  [SerializeField]
  private TextMeshProUGUI[] awayTeamBoxScore_Txt;
  [SerializeField]
  private Image homeTeamBoxScore_Img;
  [SerializeField]
  private Image awayTeamBoxScore_Img;
  [SerializeField]
  private GameObject homePossessionIndicator_GO;
  [SerializeField]
  private GameObject awayPossessionIndicator_GO;
  [SerializeField]
  private TextMeshProUGUI gameStatusSummary_Txt;
  [Header("Stats Section")]
  [SerializeField]
  private GameObject statsTitle_GO;
  [SerializeField]
  private PauseWindowPlayerDisplay homeQBDisplay;
  [SerializeField]
  private PauseWindowPlayerDisplay awayQBDisplay;
  [SerializeField]
  private UnityEngine.UI.Button gameStats_Btn;
  [Header("Settings Section")]
  [SerializeField]
  private GameObject settingsTitle_GO;
  [SerializeField]
  private UnityEngine.UI.Button audioSettings_Btn;
  [Header("Controls Section")]
  [SerializeField]
  private GameObject controlsTitle_GO;
  [SerializeField]
  private TextMeshProUGUI controlLayoutTitle_Txt;
  [SerializeField]
  private GameObject controlLayoutContainer_GO;
  [SerializeField]
  private GameObject[] controlLayoutIndicator_GO;
  private string[] controlLayoutTitles;
  private int controlLayoutIndex;
  private bool allowMove;
  private WaitForSecondsRealtime disableMove_WFS;
  private bool mouseWasShown = true;

  private void Awake() => ProEra.Game.Sources.UI.PauseWindow = (IPauseWindow) this;

  public void Init()
  {
    this.controlLayoutIndex = 0;
    this.parentWindow_CG.alpha = 0.0f;
    this.parentWindow_CG.blocksRaycasts = false;
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
    this.disableMove_WFS = new WaitForSecondsRealtime(0.2f);
    this.startSecondHalf_Txt.text = "";
    this.controlLayoutTitles = new string[5]
    {
      "OFFENSE - BEFORE SNAP",
      "OFFENSE - RUN CONTROLS",
      "OFFENSE - PASS CONTROLS",
      "DEFENSE - BEFORE SNAP",
      "DEFENSE - ACTIVE PLAY"
    };
    this.resumeGameBtn_Nav = new Navigation();
    this.depthChartBtn_Nav = new Navigation();
    this.resumeGameBtn_Nav.mode = Navigation.Mode.Explicit;
    this.depthChartBtn_Nav.mode = Navigation.Mode.Explicit;
  }

  public void ShowWindow()
  {
    GUIManager.instance.ShowParentPauseWindow();
    GUIManager.instance.SetAllowPlaySelect(false);
    this.allowMove = true;
    this.mouseWasShown = Cursor.visible;
    Cursor.visible = true;
    if (global::Game.IsSpectateMode)
    {
      this.callTimeoutButton_GO.SetActive(false);
      this.resumeGameBtn_Nav.selectOnRight = (Selectable) this.depthChart_Btn;
      this.resumeGame_Btn.navigation = this.resumeGameBtn_Nav;
      this.depthChartBtn_Nav.selectOnLeft = (Selectable) this.resumeGame_Btn;
      this.depthChartBtn_Nav.selectOnRight = (Selectable) this.quitGame_Btn;
      this.depthChart_Btn.navigation = this.depthChartBtn_Nav;
    }
    this.mainWindow_CG.blocksRaycasts = true;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f).setIgnoreTimeScale(true);
    Globals.PauseGame.SetValue(true);
    BottomBarManager.instance.ShowWindow();
    BottomBarManager.instance.ShowBackButton();
    this.ShowGameSection();
  }

  public void HideWindow()
  {
    Cursor.visible = this.mouseWasShown;
    GUIManager.instance.SetPlaySelectDelay();
    this.mainWindow_CG.blocksRaycasts = false;
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f).setIgnoreTimeScale(true);
    ControllerManagerGame.self.DeselectCurrentUIElement();
    BottomBarManager.instance.HideWindow();
    Globals.PauseGame.SetValue(false);
  }

  public bool IsParentWindowVisible() => (double) this.parentWindow_CG.alpha > 0.0;

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public bool IsGameSectionActive() => this.sectionIndex == 0;

  public void HideAllTitleText()
  {
    this.statsTitle_GO.SetActive(false);
    this.settingsTitle_GO.SetActive(false);
    this.controlsTitle_GO.SetActive(false);
  }

  public void ReturnToPreviousMenu()
  {
    if (this.pauseWindowState == GUI_PauseWindow.PauseWindowState.EndOfGame)
      return;
    this.ResumeGame();
  }

  private void Update() => this.ManageControllerSupport();

  public void SetHalftimeDisplay()
  {
    this.ShowWindow();
    this.pauseWindowState = GUI_PauseWindow.PauseWindowState.Halftime;
    this.SetResumeGameButtonText("START SECOND HALF");
    this.callTimeoutButton_GO.SetActive(false);
    this.resumeGameBtn_Nav.selectOnRight = (Selectable) this.depthChart_Btn;
    this.resumeGame_Btn.navigation = this.resumeGameBtn_Nav;
    this.depthChartBtn_Nav.selectOnLeft = (Selectable) this.resumeGame_Btn;
    this.depthChartBtn_Nav.selectOnRight = (Selectable) this.quitGame_Btn;
    this.depthChart_Btn.navigation = this.depthChartBtn_Nav;
  }

  public void SetResumeGameButtonText(string t) => this.resumeGame_Txt.text = t;

  private IEnumerator ForceSecondHalfStart()
  {
    GUI_PauseWindow guiPauseWindow = this;
    int timeToSecondHalf = 15;
    for (int i = 0; i < timeToSecondHalf; ++i)
    {
      guiPauseWindow.startSecondHalf_Txt.text = "SECOND HALF BEGINS IN: " + (timeToSecondHalf - i).ToString();
      yield return (object) guiPauseWindow.StartCoroutine(CoroutineUtil.WaitForRealSeconds(1f));
    }
    guiPauseWindow.startSecondHalf_Txt.text = "";
    if (guiPauseWindow.IsVisible() && guiPauseWindow.sectionIndex == 0 && guiPauseWindow.pauseWindowState == GUI_PauseWindow.PauseWindowState.Halftime)
      guiPauseWindow.ResumeGame();
  }

  public void SetEndOfGameDisplay()
  {
    this.ShowWindow();
    this.callTimeoutButton_GO.SetActive(false);
    this.quitGameButton_GO.SetActive(false);
    this.resumeGameBtn_Nav.selectOnRight = (Selectable) this.depthChart_Btn;
    this.resumeGame_Btn.navigation = this.resumeGameBtn_Nav;
    this.depthChartBtn_Nav.selectOnLeft = (Selectable) this.resumeGame_Btn;
    this.depthChart_Btn.navigation = this.depthChartBtn_Nav;
    this.pauseWindowState = GUI_PauseWindow.PauseWindowState.EndOfGame;
    this.SetResumeGameButtonText("END GAME");
  }

  public void ShowGameSection()
  {
    this.HideAllTitleText();
    if ((Object) UISoundManager.instance != (Object) null)
      UISoundManager.instance.PlayTabSwipe();
    this.sectionIndex = 0;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.gameButton_Trans.localPosition.x, 0.15f).setIgnoreTimeScale(true);
    LeanTween.size(this.activeIndicator_Trans, new Vector2(this.gameButton_Trans.rect.width, this.activeIndicator_Trans.rect.height), 0.15f).setIgnoreTimeScale(true);
    LeanTween.moveLocalX(this.sectionContainer_GO, (float) ((double) this.sectionContainer_Trans.rect.width * (double) this.sectionIndex * -1.0), 0.15f).setIgnoreTimeScale(true);
    this.game_Btn.interactable = false;
    this.stats_Btn.interactable = true;
    this.settings_Btn.interactable = true;
    this.controls_Btn.interactable = true;
    this.ShowControlLayout(0);
    this.SetDriveStats();
    this.SetBoxScore();
    BottomBarManager.instance.SetControllerButtonGuide(3);
    GUIManager.instance.SelectUIItem((Selectable) this.resumeGame_Btn);
  }

  private void SetDriveStats()
  {
    this.offensiveTeamLogo_Img.sprite = !ProEra.Game.MatchState.IsHomeTeamOnOffense ? PersistentData.GetAwaySmallLogo() : PersistentData.GetHomeSmallLogo();
    this.plays_Txt.text = ProEra.Game.MatchState.Stats.CurrentDrivePlays.ToString();
    this.yards_Txt.text = ProEra.Game.MatchState.Stats.DriveTotalYards.ToString();
    int num = ProEra.Game.MatchState.Stats.DriveTimeInSeconds % 60;
    this.timeOfPossession_Txt.text = string.Format("{0:00}:{1:00}", (object) (ProEra.Game.MatchState.Stats.DriveTimeInSeconds / 60 % 60), (object) num);
    this.firstDowns_Txt.text = ProEra.Game.MatchState.Stats.DriveFirstDowns.ToString();
    this.rushPlays_Txt.text = ProEra.Game.MatchState.Stats.DriveRunPlays.ToString();
    this.passPlays_Txt.text = ProEra.Game.MatchState.Stats.DrivePassPlays.ToString();
    this.rushYards_Txt.text = ProEra.Game.MatchState.Stats.DriveRunYards.ToString();
    this.passYards_Txt.text = ProEra.Game.MatchState.Stats.DrivePassYards.ToString();
  }

  private void SetBoxScore()
  {
    this.homeTeamBoxScore_Img.sprite = PersistentData.GetHomeTeamData().GetSmallLogo();
    this.awayTeamBoxScore_Img.sprite = PersistentData.GetAwayTeamData().GetSmallLogo();
    for (int index = 0; index < 4; ++index)
    {
      if (PersistentData.userIsHome)
      {
        this.homeTeamBoxScore_Txt[index].text = ProEra.Game.MatchState.Stats.User.ScoreByQuarter[index].ToString();
        this.awayTeamBoxScore_Txt[index].text = ProEra.Game.MatchState.Stats.Comp.ScoreByQuarter[index].ToString();
      }
      else
      {
        this.awayTeamBoxScore_Txt[index].text = ProEra.Game.MatchState.Stats.User.ScoreByQuarter[index].ToString();
        this.homeTeamBoxScore_Txt[index].text = ProEra.Game.MatchState.Stats.Comp.ScoreByQuarter[index].ToString();
      }
    }
    if (PersistentData.userIsHome)
    {
      this.homeTeamBoxScore_Txt[4].text = ProEra.Game.MatchState.Stats.User.Score.ToString();
      this.awayTeamBoxScore_Txt[4].text = ProEra.Game.MatchState.Stats.Comp.Score.ToString();
    }
    else
    {
      this.homeTeamBoxScore_Txt[4].text = ProEra.Game.MatchState.Stats.Comp.Score.ToString();
      this.awayTeamBoxScore_Txt[4].text = ProEra.Game.MatchState.Stats.User.Score.ToString();
    }
    if (ProEra.Game.MatchState.IsHomeTeamOnOffense)
    {
      this.homePossessionIndicator_GO.SetActive(true);
      this.awayPossessionIndicator_GO.SetActive(false);
    }
    else
    {
      this.homePossessionIndicator_GO.SetActive(false);
      this.awayPossessionIndicator_GO.SetActive(true);
    }
    string str = (!FieldState.IsBallInOpponentTerritory() ? PersistentData.GetOffensiveTeamData().GetAbbreviation() : PersistentData.GetDefensiveTeamData().GetAbbreviation()) + " " + Field.GetYardLineByFieldLocation(ProEra.Game.MatchState.BallOn.Value).ToString();
  }

  public void ResumeGame()
  {
    this.HideWindow();
    GUIManager.instance.HideParentPauseWindow();
    if (this.pauseWindowState == GUI_PauseWindow.PauseWindowState.Halftime)
    {
      this.pauseWindowState = GUI_PauseWindow.PauseWindowState.NormalPause;
      this.SetResumeGameButtonText("RESUME GAME");
      this.startSecondHalf_Txt.text = "";
      GUIManager.instance.StartSecondHalf();
      this.callTimeoutButton_GO.SetActive(true);
      this.resumeGameBtn_Nav.selectOnRight = (Selectable) this.callTimeout_Btn;
      this.resumeGame_Btn.navigation = this.resumeGameBtn_Nav;
      this.depthChartBtn_Nav.selectOnLeft = (Selectable) this.callTimeout_Btn;
      this.depthChartBtn_Nav.selectOnRight = (Selectable) this.quitGame_Btn;
      this.depthChart_Btn.navigation = this.depthChartBtn_Nav;
    }
    else
    {
      if (this.pauseWindowState != GUI_PauseWindow.PauseWindowState.EndOfGame)
        return;
      GUIManager.instance.EndGame();
    }
  }

  public void CallTimeout()
  {
    if (global::Game.IsSpectateMode || this.pauseWindowState == GUI_PauseWindow.PauseWindowState.Halftime || this.pauseWindowState == GUI_PauseWindow.PauseWindowState.EndOfGame)
      return;
    if (GlobalsUtils.IsFirstPlayerNavigatingMenu())
      TimeoutManager.CallTimeOut(Team.Player1);
    else
      TimeoutManager.CallTimeOut(Team.Player2);
    this.ResumeGame();
  }

  public void ShowDepthChart()
  {
    this.HideWindow();
    ProEra.Game.Sources.UI.DepthChart.ShowWindow();
  }

  public void QuitGame() => this.mainWindow_CG.interactable = false;

  public void ShowStatsSection()
  {
    this.HideAllTitleText();
    this.statsTitle_GO.SetActive(true);
    UISoundManager.instance.PlayTabSwipe();
    this.sectionIndex = 1;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.statsButton_Trans.localPosition.x, 0.15f).setIgnoreTimeScale(true);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.statsButton_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    LeanTween.size(activeIndicatorTrans, to, 0.15f).setIgnoreTimeScale(true);
    LeanTween.moveLocalX(this.sectionContainer_GO, (float) ((double) this.sectionContainer_Trans.rect.width * (double) this.sectionIndex * -1.0), 0.15f).setIgnoreTimeScale(true);
    this.game_Btn.interactable = true;
    this.stats_Btn.interactable = false;
    this.settings_Btn.interactable = true;
    this.controls_Btn.interactable = true;
    TeamData homeTeamData = PersistentData.GetHomeTeamData();
    TeamData awayTeamData = PersistentData.GetAwayTeamData();
    this.homeQBDisplay.SetPlayerDisplay(homeTeamData, homeTeamData.TeamDepthChart.GetStartingQB());
    this.awayQBDisplay.SetPlayerDisplay(awayTeamData, awayTeamData.TeamDepthChart.GetStartingQB());
    this.ShowControlLayout(0);
    GUIManager.instance.SelectUIItem((Selectable) this.gameStats_Btn);
  }

  public void ShowGameStats()
  {
    this.HideWindow();
    ProEra.Game.Sources.UI.GameStats.ShowWindow();
  }

  public void ShowPlayerStats()
  {
    this.HideWindow();
    ProEra.Game.Sources.UI.PlayerStats.ShowWindow();
  }

  public void ShowSettingsSection()
  {
    this.HideAllTitleText();
    this.settingsTitle_GO.SetActive(true);
    UISoundManager.instance.PlayTabSwipe();
    this.sectionIndex = 2;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.settingsButton_Trans.localPosition.x, 0.15f).setIgnoreTimeScale(true);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.settingsButton_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    LeanTween.size(activeIndicatorTrans, to, 0.15f).setIgnoreTimeScale(true);
    LeanTween.moveLocalX(this.sectionContainer_GO, (float) ((double) this.sectionContainer_Trans.rect.width * (double) this.sectionIndex * -1.0), 0.15f).setIgnoreTimeScale(true);
    this.game_Btn.interactable = true;
    this.stats_Btn.interactable = true;
    this.settings_Btn.interactable = false;
    this.controls_Btn.interactable = true;
    this.ShowControlLayout(0);
    GUIManager.instance.SelectUIItem((Selectable) this.audioSettings_Btn);
  }

  public void ShowAudioOptions()
  {
    this.HideWindow();
    ProEra.Game.Sources.UI.OptionsMenu.ShowWindow();
    ProEra.Game.Sources.UI.OptionsMenu.ShowAudioOptions();
    Globals.PauseGame.SetValue(true);
  }

  public void ShowVideoOptions()
  {
    this.HideWindow();
    ProEra.Game.Sources.UI.OptionsMenu.ShowWindow();
    ProEra.Game.Sources.UI.OptionsMenu.ShowVideoOptions();
    Globals.PauseGame.SetValue(true);
  }

  public void ShowGameOptions()
  {
    this.HideWindow();
    ProEra.Game.Sources.UI.OptionsMenu.ShowWindow();
    ProEra.Game.Sources.UI.OptionsMenu.ShowGameOptions();
    Globals.PauseGame.SetValue(true);
  }

  public void ShowPenaltyOptions()
  {
    this.HideWindow();
    ProEra.Game.Sources.UI.OptionsMenu.ShowWindow();
    ProEra.Game.Sources.UI.OptionsMenu.ShowPenaltyOptions();
    Globals.PauseGame.SetValue(true);
  }

  public void ShowOptions()
  {
    ProEra.Game.Sources.UI.PauseWindow.HideWindow();
    ProEra.Game.Sources.UI.OptionsMenu.ShowWindow();
    Cursor.visible = true;
  }

  public void HideOptions()
  {
    ProEra.Game.Sources.UI.PauseWindow.ShowWindow();
    AppEvents.SaveGameSettings.Trigger();
  }

  public void ShowControlsSection()
  {
    this.HideAllTitleText();
    this.controlsTitle_GO.SetActive(true);
    UISoundManager.instance.PlayTabSwipe();
    this.sectionIndex = 3;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.controlsButton_Trans.localPosition.x, 0.15f).setIgnoreTimeScale(true);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.controlsButton_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    LeanTween.size(activeIndicatorTrans, to, 0.15f).setIgnoreTimeScale(true);
    LeanTween.moveLocalX(this.sectionContainer_GO, (float) ((double) this.sectionContainer_Trans.rect.width * (double) this.sectionIndex * -1.0), 0.15f).setIgnoreTimeScale(true);
    this.game_Btn.interactable = true;
    this.stats_Btn.interactable = true;
    this.settings_Btn.interactable = true;
    this.controls_Btn.interactable = false;
    ControllerManagerGame.self.DeselectCurrentUIElement();
    this.ShowControlLayout(0);
  }

  private void ShowControlLayout(int index)
  {
    this.controlLayoutIndex = index;
    this.controlLayoutTitle_Txt.text = this.controlLayoutTitles[index];
    LeanTween.moveLocalX(this.controlLayoutContainer_GO, (float) ((double) this.sectionContainer_Trans.rect.width * (double) this.controlLayoutIndex * -1.0), 0.15f).setIgnoreTimeScale(true);
    for (int index1 = 0; index1 < this.controlLayoutIndicator_GO.Length; ++index1)
      this.controlLayoutIndicator_GO[index1].SetActive(false);
    this.controlLayoutIndicator_GO[this.controlLayoutIndex].SetActive(true);
  }

  public void ShowNextControlLayout()
  {
    if (this.controlLayoutIndex >= this.controlLayoutTitles.Length - 1)
      return;
    UISoundManager.instance.PlayTabSwipe();
    this.ShowControlLayout(this.controlLayoutIndex + 1);
  }

  public void ShowPrevControlLayout()
  {
    if (this.controlLayoutIndex <= 0)
      return;
    UISoundManager.instance.PlayTabSwipe();
    this.ShowControlLayout(this.controlLayoutIndex - 1);
  }

  private void ManageControllerSupport()
  {
    if (!this.IsVisible() || !ControllerManagerGame.usingController || !this.allowMove)
      return;
    Player userIndex = Globals.MenuPlayer.Value;
    float h = UserManager.instance.LeftStickX(userIndex);
    double num = (double) UserManager.instance.LeftStickY(userIndex);
    if (UserManager.instance.RightBumperWasPressed(userIndex))
      this.SelectNextCategory();
    else if (UserManager.instance.LeftBumperWasPressed(userIndex))
      this.SelectPreviousCategory();
    if (UserManager.instance.StartWasPressed(userIndex) || UserManager.instance.OptionWasPressed(userIndex) || UserManager.instance.MenuWasPressed(userIndex) || UserManager.instance.BackWasPressed(userIndex) || UserManager.instance.ViewWasPressed(userIndex))
      this.ResumeGame();
    if (this.sectionIndex != 3)
      return;
    this.ManageControlLayoutSectionControls(h);
  }

  private void SelectNextCategory()
  {
    if (this.sectionIndex == 0)
      this.ShowStatsSection();
    else if (this.sectionIndex == 1)
      this.ShowSettingsSection();
    else if (this.sectionIndex == 2 && this.controlsButton_Trans.gameObject.activeInHierarchy)
      this.ShowControlsSection();
    else
      this.ShowGameSection();
  }

  private void SelectPreviousCategory()
  {
    if (this.sectionIndex == 3)
      this.ShowSettingsSection();
    else if (this.sectionIndex == 2)
      this.ShowStatsSection();
    else if (this.sectionIndex == 1)
      this.ShowGameSection();
    else if (this.sectionIndex == 0 && this.controlsButton_Trans.gameObject.activeInHierarchy)
      this.ShowControlsSection();
    else
      this.ShowSettingsSection();
  }

  private void ManageControlLayoutSectionControls(float h)
  {
    if ((double) h < -0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.ShowPrevControlLayout();
    }
    else
    {
      if ((double) h <= 0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ShowNextControlLayout();
    }
  }

  private IEnumerator DisableMove()
  {
    this.allowMove = false;
    yield return (object) this.disableMove_WFS;
    this.allowMove = true;
  }

  private enum PauseWindowState
  {
    NormalPause,
    Halftime,
    EndOfGame,
  }
}
