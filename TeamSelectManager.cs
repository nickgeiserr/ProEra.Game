// Decompiled with JetBrains decompiler
// Type: TeamSelectManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using ProEra.Game;
using System.Collections;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectManager : MonoBehaviour
{
  [Header("Main")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [Header("Team Select")]
  [SerializeField]
  private TextMeshProUGUI homeSide_Txt;
  [SerializeField]
  private TextMeshProUGUI awaySide_Txt;
  [SerializeField]
  private TextMeshProUGUI awayPlayer_Txt;
  [SerializeField]
  private TextMeshProUGUI homePlayer_Txt;
  [SerializeField]
  private CanvasGroup homeTeamSelect_CG;
  [SerializeField]
  private CanvasGroup awayTeamSelect_CG;
  [SerializeField]
  private Image awayTeamBackground_Img;
  [SerializeField]
  private Image homeTeamBackground_Img;
  [SerializeField]
  private Image awayTeamLogo_Img;
  [SerializeField]
  private Image homeTeamLogo_Img;
  [SerializeField]
  private RectTransform awayTeamSection_Trans;
  [SerializeField]
  private RectTransform homeTeamSection_Trans;
  [SerializeField]
  private TextMeshProUGUI homeOFF_Txt;
  [SerializeField]
  private TextMeshProUGUI homeDEF_Txt;
  [SerializeField]
  private TextMeshProUGUI homeSPC_Txt;
  [SerializeField]
  private TextMeshProUGUI awayOFF_Txt;
  [SerializeField]
  private TextMeshProUGUI awayDEF_Txt;
  [SerializeField]
  private TextMeshProUGUI awaySPC_Txt;
  [SerializeField]
  private TextMeshProUGUI homeTeamName_Txt;
  [SerializeField]
  private TextMeshProUGUI awayTeamName_Txt;
  [SerializeField]
  private TextMeshProUGUI homeTeamCity_Txt;
  [SerializeField]
  private TextMeshProUGUI awayTeamCity_Txt;
  [SerializeField]
  private TextMeshProUGUI homePlaybookSummary_Txt;
  [SerializeField]
  private TextMeshProUGUI awayPlaybookSummary_Txt;
  [SerializeField]
  private Animator awayTeamSection_Ani;
  [SerializeField]
  private Animator homeTeamSection_Ani;
  [SerializeField]
  private GameObject switchSidesButton_GO;
  [SerializeField]
  private GameObject awayDisplayText_GO;
  [SerializeField]
  private GameObject homeDisplayText_GO;
  [HideInInspector]
  public int homeIndex;
  [HideInInspector]
  public int awayIndex;
  [HideInInspector]
  public bool homeTeamSwitched;
  private int totalNumberOfTeams;
  private bool isUserSelectingCompTeam;
  private bool isSelectingHomeTeam;
  private bool isSelectingAwayTeam;
  private TeamData selectedHomeTeam;
  private TeamData selectedAwayTeam;
  [Header("Options Window")]
  [SerializeField]
  private CanvasGroup homeOptionsWindow_CG;
  [SerializeField]
  private CanvasGroup awayOptionsWindow_CG;
  [SerializeField]
  private Animator awayOptionsEditUniform_Ani;
  [SerializeField]
  private Animator awayOptionsEditPlaybook_Ani;
  [SerializeField]
  private Animator awayOptionsReady_Ani;
  [SerializeField]
  private Animator homeOptionsEditUniform_Ani;
  [SerializeField]
  private Animator homeOptionsEditPlaybook_Ani;
  [SerializeField]
  private Animator homeOptionsReady_Ani;
  [Header("Ready Window")]
  [SerializeField]
  private CanvasGroup homeReadyWindow_CG;
  [SerializeField]
  private CanvasGroup awayReadyWindow_CG;
  [Header("Playbook Select")]
  [SerializeField]
  private CanvasGroup awayPlaybookWindow_CG;
  [SerializeField]
  private CanvasGroup homePlaybookWindow_CG;
  [SerializeField]
  private Animator awayOffensivePlaybook_Ani;
  [SerializeField]
  private Animator awayDefensivePlaybook_Ani;
  [SerializeField]
  private Animator awayPlaybookSave_Ani;
  [SerializeField]
  private Animator homeOffensivePlaybook_Ani;
  [SerializeField]
  private Animator homeDefensivePlaybook_Ani;
  [SerializeField]
  private Animator homePlaybookSave_Ani;
  [SerializeField]
  private TextMeshProUGUI homeOffensivePlaybook_Txt;
  [SerializeField]
  private TextMeshProUGUI homeDefensivePlaybook_Txt;
  [SerializeField]
  private TextMeshProUGUI awayOffensivePlaybook_Txt;
  [SerializeField]
  private TextMeshProUGUI awayDefensivePlaybook_Txt;
  private int homeOffPlaybookIndex;
  private int homeDefPlaybookIndex;
  private int awayOffPlaybookIndex;
  private int awayDefPlaybookIndex;
  [Header("Uniform Select")]
  [SerializeField]
  private CanvasGroup awayUniformWindow_CG;
  [SerializeField]
  private CanvasGroup homeUniformWindow_CG;
  [SerializeField]
  private UniformManager awayUniformManager;
  [SerializeField]
  private UniformManager homeUniformManager;
  [SerializeField]
  private TextMeshProUGUI awayUniformName_Txt;
  [SerializeField]
  private TextMeshProUGUI homeUniformName_Txt;
  [SerializeField]
  private TextMeshProUGUI awayHelmetName_Txt;
  [SerializeField]
  private TextMeshProUGUI homeHelmetName_Txt;
  [SerializeField]
  private TextMeshProUGUI awayJerseyName_Txt;
  [SerializeField]
  private TextMeshProUGUI homeJerseyName_Txt;
  [SerializeField]
  private TextMeshProUGUI awayPantsName_Txt;
  [SerializeField]
  private TextMeshProUGUI homePantsName_Txt;
  [SerializeField]
  private GameObject awayPlayer_GO;
  [SerializeField]
  private GameObject homePlayer_GO;
  [SerializeField]
  private Animator awayUniform_Ani;
  [SerializeField]
  private Animator awayHelmet_Ani;
  [SerializeField]
  private Animator awayJersey_Ani;
  [SerializeField]
  private Animator awayPants_Ani;
  [SerializeField]
  private Animator awayUniformSave_Ani;
  [SerializeField]
  private Animator homeUniform_Ani;
  [SerializeField]
  private Animator homeHelmet_Ani;
  [SerializeField]
  private Animator homeJersey_Ani;
  [SerializeField]
  private Animator homePants_Ani;
  [SerializeField]
  private Animator homeUniformSave_Ani;
  private UniformSet awayUniformSet;
  private UniformSet homeUniformSet;
  private UniformConfig awayUniformConfig;
  private UniformConfig homeUniformConfig;
  private int awayUniformIndex;
  private int awayHelmetIndex;
  private int awayJerseyIndex;
  private int awayPantIndex;
  private int homeUniformIndex;
  private int homeHelmetIndex;
  private int homeJerseyIndex;
  private int homePantIndex;
  private int awayUniformSelectedIndex;
  private int homeUniformSelectedIndex;
  private bool allowMove;
  private bool allowMoveP2;
  private WaitForSeconds _disableMove = new WaitForSeconds(0.2f);

  public void Init()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
    this.totalNumberOfTeams = AssetManager.TotalTeams;
    this.homeIndex = 0;
    this.awayIndex = this.totalNumberOfTeams - 1;
  }

  public void ShowWindow()
  {
    PopupLoadingScreen.self.ShowPopupLoadingScreen("LOADING TEAMS");
    this.StartCoroutine(this.ProceedActivate());
  }

  public void HideWindow()
  {
    this.mainWindow_CG.blocksRaycasts = false;
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  private IEnumerator ProceedActivate()
  {
    yield return (object) null;
    this.mainWindow_CG.blocksRaycasts = true;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    LeanTween.moveX(this.awayTeamSection_Trans, (float) ((double) this.awayTeamSection_Trans.rect.width * -1.0 * 0.800000011920929), 0.0f);
    LeanTween.moveX(this.homeTeamSection_Trans, this.homeTeamSection_Trans.rect.width * 0.8f, 0.0f);
    LeanTween.moveX(this.awayTeamSection_Trans, 0.0f, 0.3f);
    LeanTween.moveX(this.homeTeamSection_Trans, 0.0f, 0.3f);
    this.switchSidesButton_GO.SetActive(PersistentData.gameType == GameType.QuickMatch);
    this.SetIsSelectingAwayTeam(true);
    this.SetIsSelectingHomeTeam(PersistentData.GameMode == GameMode.PlayerVsPlayer || PersistentData.GameMode == GameMode.PlayerVsPlayerCoach);
    this.ShowHomeTeamSelect();
    this.ShowAwayTeamSelect();
    this.HideHomeReadyWindow();
    this.HideAwayReadyWindow();
    this.HideHomeOptionsWindow();
    this.HideAwayOptionsWindow();
    this.HideHomePlaybookWindow();
    this.HideAwayPlaybookWindow();
    this.HideHomeUniformWindow();
    this.HideAwayUniformWindow();
    this.allowMove = this.allowMoveP2 = true;
    this.awayUniformSet = this.homeUniformSet = (UniformSet) null;
    BottomBarManager.instance.SetControllerButtonGuide(1);
    BottomBarManager.instance.ShowBackButton();
    this.isUserSelectingCompTeam = false;
    if (PersistentData.gameType == GameType.QuickMatch)
    {
      this.homeSide_Txt.text = "HOME";
      this.awaySide_Txt.text = "AWAY";
      this.homeTeamSwitched = false;
    }
    else if (PersistentData.userIsHome)
    {
      this.homeSide_Txt.text = "AWAY";
      this.awaySide_Txt.text = "HOME";
      this.homeTeamSwitched = true;
    }
    else
    {
      this.homeTeamSwitched = false;
      this.homeSide_Txt.text = "HOME";
      this.awaySide_Txt.text = "AWAY";
    }
    if (PersistentData.Is2PMatch)
    {
      this.awayPlayer_Txt.text = "P1";
      this.homePlayer_Txt.text = "P2";
    }
    else if (PersistentData.UserCallsPlays)
    {
      this.awayPlayer_Txt.text = "P1";
      this.homePlayer_Txt.text = "CPU";
    }
    else
    {
      this.awayPlayer_Txt.text = "CPU";
      this.homePlayer_Txt.text = "CPU";
    }
    this.ShowHomeTeamInfo();
    this.ShowAwayTeamInfo();
    if (PersistentData.gameType == GameType.SeasonMode)
      this.SelectAwayTeam();
    PopupLoadingScreen.self.HidePopupLoadingScreen();
  }

  private void Update()
  {
    if (!this.IsVisible())
      return;
    this.ManageControllerInput();
  }

  public void ShowHomeTeamSelect()
  {
    LeanTween.alphaCanvas(this.homeTeamSelect_CG, 1f, 0.3f);
    this.homeTeamLogo_Img.CrossFadeAlpha(1f, 0.3f, true);
    this.homeTeamSelect_CG.blocksRaycasts = true;
  }

  public void ShowAwayTeamSelect()
  {
    LeanTween.alphaCanvas(this.awayTeamSelect_CG, 1f, 0.3f);
    this.awayTeamLogo_Img.CrossFadeAlpha(1f, 0.3f, true);
    this.awayTeamSelect_CG.blocksRaycasts = true;
    this.switchSidesButton_GO.SetActive(PersistentData.gameType == GameType.QuickMatch);
  }

  public void HideHomeTeamSelect()
  {
    LeanTween.alphaCanvas(this.homeTeamSelect_CG, 0.0f, 0.3f);
    this.homeTeamSelect_CG.blocksRaycasts = false;
  }

  public void HideAwayTeamSelect()
  {
    LeanTween.alphaCanvas(this.awayTeamSelect_CG, 0.0f, 0.3f);
    this.awayTeamSelect_CG.blocksRaycasts = false;
  }

  private void SetIsSelectingHomeTeam(bool isSelecting)
  {
    this.isSelectingHomeTeam = isSelecting;
    if (!isSelecting)
      this.homeTeamSection_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    else
      this.homeTeamSection_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  private void SetIsSelectingAwayTeam(bool isSelecting)
  {
    this.isSelectingAwayTeam = isSelecting;
    if (!isSelecting)
      this.awayTeamSection_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    else
      this.awayTeamSection_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  public void HomeTeamUp()
  {
    UISoundManager.instance.PlayButtonToggle();
    --this.homeIndex;
    if (this.homeIndex < 0)
      this.homeIndex = this.totalNumberOfTeams - 1;
    this.ShowHomeTeamInfo();
    if (this.homeIndex != this.awayIndex)
      return;
    this.HomeTeamUp();
  }

  public void HomeTeamDown()
  {
    UISoundManager.instance.PlayButtonToggle();
    ++this.homeIndex;
    if (this.homeIndex > this.totalNumberOfTeams - 1)
      this.homeIndex = 0;
    this.ShowHomeTeamInfo();
    if (this.homeIndex != this.awayIndex)
      return;
    this.HomeTeamDown();
  }

  public void AwayTeamUp()
  {
    UISoundManager.instance.PlayButtonToggle();
    --this.awayIndex;
    if (this.awayIndex < 0)
      this.awayIndex = this.totalNumberOfTeams - 1;
    this.ShowAwayTeamInfo();
    if (this.awayIndex != this.homeIndex)
      return;
    this.AwayTeamUp();
  }

  public void AwayTeamDown()
  {
    UISoundManager.instance.PlayButtonToggle();
    ++this.awayIndex;
    if (this.awayIndex > this.totalNumberOfTeams - 1)
      this.awayIndex = 0;
    this.ShowAwayTeamInfo();
    if (this.awayIndex != this.homeIndex)
      return;
    this.AwayTeamDown();
  }

  public void SetRandomHomeTeam()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.homeIndex = Random.Range(0, this.totalNumberOfTeams);
    this.ShowHomeTeamInfo();
    if (this.homeIndex != this.awayIndex)
      return;
    this.SetRandomHomeTeam();
  }

  public void SetRandomAwayTeam()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.awayIndex = Random.Range(0, this.totalNumberOfTeams);
    this.ShowAwayTeamInfo();
    if (this.awayIndex != this.homeIndex)
      return;
    this.SetRandomAwayTeam();
  }

  public void ShowHomeTeamInfo()
  {
    this.selectedHomeTeam = TeamDataCache.GetTeam(this.homeIndex);
    int[] teamRatings = this.selectedHomeTeam.GetTeamRatings();
    this.homeTeamBackground_Img.CrossFadeColor(this.selectedHomeTeam.GetPrimaryColor(), 0.2f, false, false);
    this.homeTeamLogo_Img.sprite = this.selectedHomeTeam.GetLargeLogo();
    this.homeOFF_Txt.text = TeamData.GetAttributeGradeFromNumber(teamRatings[1]);
    this.homeDEF_Txt.text = TeamData.GetAttributeGradeFromNumber(teamRatings[2]);
    this.homeSPC_Txt.text = TeamData.GetAttributeGradeFromNumber(teamRatings[3]);
    this.homeOffPlaybookIndex = Plays.self.GetOffensivePlaybookIndex(this.selectedHomeTeam.GetOffensivePlaybook());
    this.homeDefPlaybookIndex = Plays.self.GetDefensivePlaybookIndex(this.selectedHomeTeam.GetDefensivePlaybook());
    this.homeTeamCity_Txt.text = this.selectedHomeTeam.GetCity();
    this.homeTeamName_Txt.text = this.selectedHomeTeam.GetName();
    this.homePlaybookSummary_Txt.text = "OFF: " + this.selectedHomeTeam.GetOffensivePlaybook() + "  |  DEF: " + this.selectedHomeTeam.GetDefensivePlaybook();
  }

  public void ShowAwayTeamInfo()
  {
    this.selectedAwayTeam = TeamDataCache.GetTeam(this.awayIndex);
    int[] teamRatings = this.selectedAwayTeam.GetTeamRatings();
    this.awayTeamBackground_Img.CrossFadeColor(this.selectedAwayTeam.GetPrimaryColor(), 0.2f, false, false);
    this.awayTeamLogo_Img.sprite = this.selectedAwayTeam.GetLargeLogo();
    this.awayOFF_Txt.text = TeamData.GetAttributeGradeFromNumber(teamRatings[1]);
    this.awayDEF_Txt.text = TeamData.GetAttributeGradeFromNumber(teamRatings[2]);
    this.awaySPC_Txt.text = TeamData.GetAttributeGradeFromNumber(teamRatings[3]);
    this.awayOffPlaybookIndex = Plays.self.GetOffensivePlaybookIndex(this.selectedAwayTeam.GetOffensivePlaybook());
    this.awayDefPlaybookIndex = Plays.self.GetDefensivePlaybookIndex(this.selectedAwayTeam.GetDefensivePlaybook());
    this.awayTeamCity_Txt.text = this.selectedAwayTeam.GetCity();
    this.awayTeamName_Txt.text = this.selectedAwayTeam.GetName();
    this.awayPlaybookSummary_Txt.text = "OFF: " + this.selectedAwayTeam.GetOffensivePlaybook() + "  |  DEF: " + this.selectedAwayTeam.GetDefensivePlaybook();
  }

  public void SwitchHomeTeam()
  {
    if (PersistentData.gameType == GameType.SeasonMode)
      return;
    UISoundManager.instance.PlayTabSwipe();
    if (this.homeTeamSwitched)
    {
      this.homeTeamSwitched = false;
      this.homeSide_Txt.text = "HOME";
      this.awaySide_Txt.text = "AWAY";
    }
    else
    {
      this.homeTeamSwitched = true;
      this.homeSide_Txt.text = "AWAY";
      this.awaySide_Txt.text = "HOME";
    }
  }

  public void SelectHomeTeam()
  {
    this.HideHomeTeamSelect();
    this.ShowHomeOptionsWindow();
    UISoundManager.instance.PlayButtonClick();
    this.SetIsSelectingHomeTeam(false);
  }

  public void SelectAwayTeam()
  {
    this.HideAwayTeamSelect();
    this.ShowAwayOptionsWindow();
    this.switchSidesButton_GO.SetActive(false);
    UISoundManager.instance.PlayButtonClick();
    this.SetIsSelectingAwayTeam(false);
  }

  public TeamData GetSelectedHomeTeam() => this.homeTeamSwitched ? this.selectedAwayTeam : this.selectedHomeTeam;

  public TeamData GetSelectedAwayTeam() => this.homeTeamSwitched ? this.selectedHomeTeam : this.selectedAwayTeam;

  private void ShowHomeOptionsWindow()
  {
    UISoundManager.instance.PlayButtonClick();
    LeanTween.alphaCanvas(this.homeOptionsWindow_CG, 1f, 0.3f);
    this.homeOptionsWindow_CG.blocksRaycasts = true;
    this.homeTeamLogo_Img.CrossFadeAlpha(1f, 0.3f, true);
    this.homeDisplayText_GO.SetActive(true);
    if (!ControllerManagerTitle.self.usingController)
      return;
    this.homeOptionsReady_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.homeOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.homeOptionsEditUniform_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void ShowAwayOptionsWindow()
  {
    UISoundManager.instance.PlayButtonClick();
    LeanTween.alphaCanvas(this.awayOptionsWindow_CG, 1f, 0.3f);
    this.awayOptionsWindow_CG.blocksRaycasts = true;
    this.awayTeamLogo_Img.CrossFadeAlpha(1f, 0.3f, true);
    this.awayDisplayText_GO.SetActive(true);
    if (!ControllerManagerTitle.self.usingController)
      return;
    this.awayOptionsReady_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.awayOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.awayOptionsEditUniform_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void HideHomeOptionsWindow()
  {
    LeanTween.alphaCanvas(this.homeOptionsWindow_CG, 0.0f, 0.3f);
    this.homeOptionsWindow_CG.blocksRaycasts = false;
  }

  private void HideAwayOptionsWindow()
  {
    LeanTween.alphaCanvas(this.awayOptionsWindow_CG, 0.0f, 0.3f);
    this.awayOptionsWindow_CG.blocksRaycasts = false;
  }

  private bool IsAwayOptionsWindowVisible() => (double) this.awayOptionsWindow_CG.alpha > 0.0;

  private bool IsHomeOptionsWindowVisible() => (double) this.homeOptionsWindow_CG.alpha > 0.0;

  public void ShowHomeReadyWindow()
  {
    UISoundManager.instance.PlayButtonClick();
    this.HideHomeOptionsWindow();
    LeanTween.alphaCanvas(this.homeReadyWindow_CG, 1f, 0.3f);
    this.homeReadyWindow_CG.blocksRaycasts = true;
    if (!this.IsAwayTeamReady())
      return;
    this.StartCoroutine(this.ProceedToGameSetup());
  }

  public void ShowAwayReadyWindow()
  {
    UISoundManager.instance.PlayButtonClick();
    this.HideAwayOptionsWindow();
    LeanTween.alphaCanvas(this.awayReadyWindow_CG, 1f, 0.3f);
    this.awayReadyWindow_CG.blocksRaycasts = true;
    if (this.IsHomeTeamReady())
      this.StartCoroutine(this.ProceedToGameSetup());
    else if (PersistentData.gameType == GameType.SeasonMode)
    {
      this.isUserSelectingCompTeam = true;
      this.SelectHomeTeam();
    }
    else
    {
      if (PersistentData.Is2PMatch)
        return;
      this.isUserSelectingCompTeam = true;
      this.SetIsSelectingHomeTeam(true);
    }
  }

  private void HideHomeReadyWindow()
  {
    LeanTween.alphaCanvas(this.homeReadyWindow_CG, 0.0f, 0.3f);
    this.homeReadyWindow_CG.blocksRaycasts = false;
  }

  private void HideAwayReadyWindow()
  {
    LeanTween.alphaCanvas(this.awayReadyWindow_CG, 0.0f, 0.3f);
    this.awayReadyWindow_CG.blocksRaycasts = false;
  }

  private bool IsHomeTeamReady() => (double) this.homeReadyWindow_CG.alpha > 0.0;

  private bool IsAwayTeamReady() => (double) this.awayReadyWindow_CG.alpha > 0.0;

  private IEnumerator ProceedToGameSetup()
  {
    this.HideWindow();
    if (this.awayUniformSet == null)
    {
      if (this.homeTeamSwitched)
        this.LoadAwayUniformSet(this.awayIndex, "HOME");
      else
        this.LoadAwayUniformSet(this.awayIndex, "AWAY");
    }
    if (this.homeUniformSet == null)
    {
      if (this.homeTeamSwitched)
        this.LoadHomeUniformSet(this.homeIndex, "AWAY");
      else
        this.LoadHomeUniformSet(this.homeIndex, "HOME");
    }
    yield return (object) new WaitForSeconds(0.5f);
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.gameSetup.ShowWindow();
  }

  private bool IsHomePlaybookSelectVisible() => (double) this.homePlaybookWindow_CG.alpha > 0.0;

  public void ShowHomePlaybookWindow()
  {
    if (PersistentData.gameType == GameType.SeasonMode)
      return;
    UISoundManager.instance.PlayButtonClick();
    this.HideHomeOptionsWindow();
    LeanTween.alphaCanvas(this.homePlaybookWindow_CG, 1f, 0.3f);
    this.homePlaybookWindow_CG.blocksRaycasts = true;
    this.SelectHomeOffensivePlaybook(this.homeOffPlaybookIndex);
    this.SelectHomeDefensivePlaybook(this.homeDefPlaybookIndex);
    if (!ControllerManagerTitle.self.usingController)
      return;
    this.homeOffensivePlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.homeDefensivePlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.homePlaybookSave_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  public void HideHomePlaybookWindow()
  {
    LeanTween.alphaCanvas(this.homePlaybookWindow_CG, 0.0f, 0.3f);
    this.homePlaybookWindow_CG.blocksRaycasts = false;
  }

  private bool IsAwayPlaybookSelectVisible() => (double) this.awayPlaybookWindow_CG.alpha > 0.0;

  public void ShowAwayPlaybookWindow()
  {
    if (PersistentData.gameType == GameType.SeasonMode && PersistentData.watchingNonUserMatch)
      return;
    UISoundManager.instance.PlayButtonClick();
    this.HideAwayOptionsWindow();
    LeanTween.alphaCanvas(this.awayPlaybookWindow_CG, 1f, 0.3f);
    this.awayPlaybookWindow_CG.blocksRaycasts = true;
    this.SelectAwayOffensivePlaybook(this.awayOffPlaybookIndex);
    this.SelectAwayDefensivePlaybook(this.awayDefPlaybookIndex);
    if (!ControllerManagerTitle.self.usingController)
      return;
    this.awayOffensivePlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.awayDefensivePlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.awayPlaybookSave_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  public void HideAwayPlaybookWindow()
  {
    LeanTween.alphaCanvas(this.awayPlaybookWindow_CG, 0.0f, 0.3f);
    this.awayPlaybookWindow_CG.blocksRaycasts = false;
  }

  private void SelectHomeOffensivePlaybook(int playbookIndex) => this.homeOffensivePlaybook_Txt.text = Plays.self.offensivePlaybookNames[playbookIndex];

  private void SelectHomeDefensivePlaybook(int playbookIndex) => this.homeDefensivePlaybook_Txt.text = Plays.self.defensivePlaybookNames[playbookIndex];

  private void SelectAwayOffensivePlaybook(int playbookIndex) => this.awayOffensivePlaybook_Txt.text = Plays.self.offensivePlaybookNames[playbookIndex];

  private void SelectAwayDefensivePlaybook(int playbookIndex) => this.awayDefensivePlaybook_Txt.text = Plays.self.defensivePlaybookNames[playbookIndex];

  public string GetPlayer1_OffensivePlaybook() => Plays.self.offensivePlaybookNames[this.awayOffPlaybookIndex];

  public string GetPlayer1_DefensivePlaybook() => Plays.self.defensivePlaybookNames[this.awayDefPlaybookIndex];

  public string GetPlayer2_OffensivePlaybook() => Plays.self.offensivePlaybookNames[this.homeOffPlaybookIndex];

  public string GetPlayer2_DefensivePlaybook() => Plays.self.defensivePlaybookNames[this.homeDefPlaybookIndex];

  public void ShowNextHomeOffensivePlaybook()
  {
    UISoundManager.instance.PlayButtonToggle();
    if (this.homeOffPlaybookIndex >= Plays.self.offensivePlaybookNames.Count - 1)
      return;
    ++this.homeOffPlaybookIndex;
    this.SelectHomeOffensivePlaybook(this.homeOffPlaybookIndex);
  }

  public void ShowPrevHomeOffensivePlaybook()
  {
    UISoundManager.instance.PlayButtonToggle();
    if (this.homeOffPlaybookIndex <= 0)
      return;
    --this.homeOffPlaybookIndex;
    this.SelectHomeOffensivePlaybook(this.homeOffPlaybookIndex);
  }

  public void ShowNextHomeDefensivePlaybook()
  {
    UISoundManager.instance.PlayButtonToggle();
    if (this.homeDefPlaybookIndex >= Plays.self.defensivePlaybookNames.Count - 1)
      return;
    ++this.homeDefPlaybookIndex;
    this.SelectHomeDefensivePlaybook(this.homeDefPlaybookIndex);
  }

  public void ShowPrevHomeDefensivePlaybook()
  {
    UISoundManager.instance.PlayButtonToggle();
    if (this.homeDefPlaybookIndex <= 0)
      return;
    --this.homeDefPlaybookIndex;
    this.SelectHomeDefensivePlaybook(this.homeDefPlaybookIndex);
  }

  public void ShowNextAwayOffensivePlaybook()
  {
    UISoundManager.instance.PlayButtonToggle();
    if (this.awayOffPlaybookIndex >= Plays.self.offensivePlaybookNames.Count - 1)
      return;
    ++this.awayOffPlaybookIndex;
    this.SelectAwayOffensivePlaybook(this.awayOffPlaybookIndex);
  }

  public void ShowPrevAwayOffensivePlaybook()
  {
    UISoundManager.instance.PlayButtonToggle();
    if (this.awayOffPlaybookIndex <= 0)
      return;
    --this.awayOffPlaybookIndex;
    this.SelectAwayOffensivePlaybook(this.awayOffPlaybookIndex);
  }

  public void ShowNextAwayDefensivePlaybook()
  {
    UISoundManager.instance.PlayButtonToggle();
    if (this.awayDefPlaybookIndex >= Plays.self.defensivePlaybookNames.Count - 1)
      return;
    ++this.awayDefPlaybookIndex;
    this.SelectAwayDefensivePlaybook(this.awayDefPlaybookIndex);
  }

  public void ShowPrevAwayDefensivePlaybook()
  {
    UISoundManager.instance.PlayButtonToggle();
    if (this.awayDefPlaybookIndex <= 0)
      return;
    --this.awayDefPlaybookIndex;
    this.SelectAwayDefensivePlaybook(this.awayDefPlaybookIndex);
  }

  public void SelectHomePlaybooks()
  {
    this.HideHomePlaybookWindow();
    this.ShowHomeOptionsWindow();
  }

  public void SelectAwayPlaybooks()
  {
    this.HideAwayPlaybookWindow();
    this.ShowAwayOptionsWindow();
  }

  public void ShowAwayUniformWindow()
  {
    UISoundManager.instance.PlayButtonClick();
    this.HideAwayOptionsWindow();
    this.awayTeamLogo_Img.CrossFadeAlpha(0.0f, 0.3f, true);
    this.awayDisplayText_GO.SetActive(false);
    LeanTween.alphaCanvas(this.awayUniformWindow_CG, 1f, 0.3f);
    this.awayUniformWindow_CG.blocksRaycasts = true;
    this.StartCoroutine(this.Continue_ShowAwayUniformWindow());
  }

  private IEnumerator Continue_ShowAwayUniformWindow()
  {
    PopupLoadingScreen.self.ShowPopupLoadingScreen("LOADING UNIFORMS");
    yield return (object) null;
    if (this.homeTeamSwitched)
      this.LoadAwayUniformSet(this.awayIndex, "HOME");
    else
      this.LoadAwayUniformSet(this.awayIndex, "AWAY");
    PopupLoadingScreen.self.HidePopupLoadingScreen();
    this.awayPlayer_GO.SetActive(true);
    if (ControllerManagerTitle.self.usingController)
    {
      this.awayUniform_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.awayHelmet_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayJersey_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayPants_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayUniformSave_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayUniformSelectedIndex = 0;
    }
  }

  public void ShowHomeUniformWindow()
  {
    UISoundManager.instance.PlayButtonClick();
    this.HideHomeOptionsWindow();
    this.homeTeamLogo_Img.CrossFadeAlpha(0.0f, 0.3f, true);
    this.homeDisplayText_GO.SetActive(false);
    LeanTween.alphaCanvas(this.homeUniformWindow_CG, 1f, 0.3f);
    this.homeUniformWindow_CG.blocksRaycasts = true;
    this.StartCoroutine(this.Continue_ShowHomeUniformWindow());
  }

  private IEnumerator Continue_ShowHomeUniformWindow()
  {
    PopupLoadingScreen.self.ShowPopupLoadingScreen("LOADING UNIFORMS");
    yield return (object) null;
    if (this.homeTeamSwitched)
      this.LoadHomeUniformSet(this.homeIndex, "AWAY");
    else
      this.LoadHomeUniformSet(this.homeIndex, "HOME");
    PopupLoadingScreen.self.HidePopupLoadingScreen();
    this.homePlayer_GO.SetActive(true);
    if (ControllerManagerTitle.self.usingController)
    {
      this.homeUniform_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.homeHelmet_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeJersey_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homePants_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeUniformSave_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeUniformSelectedIndex = 0;
    }
  }

  private void HideAwayUniformWindow()
  {
    this.awayPlayer_GO.SetActive(false);
    LeanTween.alphaCanvas(this.awayUniformWindow_CG, 0.0f, 0.3f);
    this.awayUniformWindow_CG.blocksRaycasts = false;
  }

  private void HideHomeUniformWindow()
  {
    this.homePlayer_GO.SetActive(false);
    LeanTween.alphaCanvas(this.homeUniformWindow_CG, 0.0f, 0.3f);
    this.homeUniformWindow_CG.blocksRaycasts = false;
  }

  private bool IsAwayUniformSelectVisible() => (double) this.awayUniformWindow_CG.alpha > 0.0;

  private bool IsHomeUniformSelectVisible() => (double) this.homeUniformWindow_CG.alpha > 0.0;

  public void SelectAwayUniform()
  {
    this.SaveAwayUniformToPersistentData();
    this.HideAwayUniformWindow();
    this.ShowAwayOptionsWindow();
  }

  public void SelectHomeUniform()
  {
    this.SaveHomeUniformToPersistentData();
    this.HideHomeUniformWindow();
    this.ShowHomeOptionsWindow();
  }

  private void LoadAwayUniformSet(int teamIndex, string uniformName)
  {
    this.awayUniformSet = UniformAssetManager.GetUniformSet(teamIndex);
    this.awayUniformIndex = this.awayUniformSet.GetUniformIndexByName(uniformName);
    this.SetAwayUniform(this.awayUniformIndex);
    this.awayUniformSet.GetNumberOfUniforms();
    this.SaveAwayUniformToPersistentData();
  }

  private void LoadHomeUniformSet(int teamIndex, string uniformName)
  {
    this.homeUniformSet = UniformAssetManager.GetUniformSet(teamIndex);
    this.homeUniformIndex = this.homeUniformSet.GetUniformIndexByName(uniformName);
    this.SetHomeUniform(this.homeUniformIndex);
    this.homeUniformSet.GetNumberOfUniforms();
    this.SaveHomeUniformToPersistentData();
  }

  private void SetAwayUniform(int uniformIndex)
  {
    this.awayUniformIndex = uniformIndex;
    this.awayUniformConfig = this.awayUniformSet.GetUniformConfig(this.awayUniformIndex);
    this.awayUniformName_Txt.text = "(" + (uniformIndex + 1).ToString() + "/" + this.awayUniformSet.GetNumberOfUniforms().ToString() + ")";
    this.awayHelmetIndex = this.awayUniformSet.GetPieceForUniformSet(uniformIndex, UniformPiece.Helmets);
    int numberOfUniformPieces1 = this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets);
    this.awayHelmetName_Txt.text = "(" + (this.awayHelmetIndex + 1).ToString() + "/" + numberOfUniformPieces1.ToString() + ")";
    this.awayJerseyIndex = this.awayUniformSet.GetPieceForUniformSet(uniformIndex, UniformPiece.Jerseys);
    int numberOfUniformPieces2 = this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys);
    this.awayJerseyName_Txt.text = "(" + (this.awayJerseyIndex + 1).ToString() + "/" + numberOfUniformPieces2.ToString() + ")";
    this.awayPantIndex = this.awayUniformSet.GetPieceForUniformSet(uniformIndex, UniformPiece.Pants);
    int numberOfUniformPieces3 = this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants);
    this.awayPantsName_Txt.text = "(" + (this.awayPantIndex + 1).ToString() + "/" + numberOfUniformPieces3.ToString() + ")";
    this.awayUniformManager.SetUniform(this.awayUniformSet, this.awayUniformConfig, this.awayHelmetIndex, this.awayPantIndex);
    this.awayUniformManager.SetJersey(this.awayJerseyIndex, "AXIS", Random.Range(1, 100), UniformAssetType.TEMP);
    this.awayUniformManager.ShowArmSleeves(this.awayUniformConfig.GetArmSleevesColor());
    this.awayUniformManager.ShowHelmetVisor(this.awayUniformConfig.GetHelmetVisorColor());
  }

  private void SetHomeUniform(int uniformIndex)
  {
    this.homeUniformIndex = uniformIndex;
    this.homeUniformConfig = this.homeUniformSet.GetUniformConfig(this.homeUniformIndex);
    this.homeUniformName_Txt.text = "(" + (uniformIndex + 1).ToString() + "/" + this.homeUniformSet.GetNumberOfUniforms().ToString() + ")";
    this.homeHelmetIndex = this.homeUniformSet.GetPieceForUniformSet(uniformIndex, UniformPiece.Helmets);
    int numberOfUniformPieces1 = this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets);
    this.homeHelmetName_Txt.text = "(" + (this.homeHelmetIndex + 1).ToString() + "/" + numberOfUniformPieces1.ToString() + ")";
    this.homeJerseyIndex = this.homeUniformSet.GetPieceForUniformSet(uniformIndex, UniformPiece.Jerseys);
    int numberOfUniformPieces2 = this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys);
    this.homeJerseyName_Txt.text = "(" + (this.homeJerseyIndex + 1).ToString() + "/" + numberOfUniformPieces2.ToString() + ")";
    this.homePantIndex = this.homeUniformSet.GetPieceForUniformSet(uniformIndex, UniformPiece.Pants);
    int numberOfUniformPieces3 = this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants);
    this.homePantsName_Txt.text = "(" + (this.homePantIndex + 1).ToString() + "/" + numberOfUniformPieces3.ToString() + ")";
    this.homeUniformManager.SetUniform(this.homeUniformSet, this.homeUniformConfig, this.homeHelmetIndex, this.homePantIndex);
    this.homeUniformManager.SetJersey(this.homeJerseyIndex, "AXIS", Random.Range(1, 100), UniformAssetType.TEMP);
    this.homeUniformManager.ShowArmSleeves(this.homeUniformConfig.GetArmSleevesColor());
    this.homeUniformManager.ShowHelmetVisor(this.homeUniformConfig.GetHelmetVisorColor());
  }

  private void SaveAwayUniformToPersistentData()
  {
    this.awayUniformSet.LockInUniformPieces(this.awayUniformIndex, this.awayHelmetIndex, this.awayJerseyIndex, this.awayPantIndex);
    if (this.homeTeamSwitched)
      PersistentData.homeTeamUniform = this.awayUniformSet;
    else
      PersistentData.awayTeamUniform = this.awayUniformSet;
  }

  private void SaveHomeUniformToPersistentData()
  {
    this.homeUniformSet.LockInUniformPieces(this.homeUniformIndex, this.homeHelmetIndex, this.homeJerseyIndex, this.homePantIndex);
    if (this.homeTeamSwitched)
      PersistentData.awayTeamUniform = this.homeUniformSet;
    else
      PersistentData.homeTeamUniform = this.homeUniformSet;
  }

  private void ManageControllerInput()
  {
    if (!ControllerManagerTitle.self.usingController)
      return;
    if (UserManager.instance.Action4WasPressed(Player.One))
      this.SwitchHomeTeam();
    if (!this.isUserSelectingCompTeam)
    {
      if (this.isSelectingAwayTeam)
        this.SelectAwayTeam_Controller();
      else if (this.IsAwayOptionsWindowVisible())
        this.SelectAwayOptions_Controller();
      else if (this.IsAwayPlaybookSelectVisible())
        this.SelectAwayPlaybook_Controller();
      else if (this.IsAwayUniformSelectVisible())
        this.SelectAwayUniform_Controller();
    }
    else if (this.isSelectingHomeTeam)
      this.SelectHomeTeam_Controller();
    else if (this.IsHomeOptionsWindowVisible())
      this.SelectHomeOptions_Controller();
    else if (this.IsHomePlaybookSelectVisible())
      this.SelectHomePlaybook_Controller();
    else if (this.IsHomeUniformSelectVisible())
      this.SelectHomeUniform_Controller();
    if (!global::Game.Is2PMatch)
      return;
    if (this.isSelectingHomeTeam)
      this.SelectHomeTeamP2_Controller();
    else if (this.IsHomeOptionsWindowVisible())
      this.SelectHomeOptionsP2_Controller();
    else if (this.IsHomePlaybookSelectVisible())
    {
      this.SelectHomePlaybookP2_Controller();
    }
    else
    {
      if (!this.IsHomeUniformSelectVisible())
        return;
      this.SelectHomeUniformP2_Controller();
    }
  }

  private void SelectAwayTeam_Controller()
  {
    float num = UserManager.instance.LeftStickY(Player.One);
    if (this.allowMove)
    {
      if ((double) num > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.AwayTeamUp();
      }
      else if ((double) num < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.AwayTeamDown();
      }
    }
    if (UserManager.instance.Action3WasPressed(Player.One))
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSelect.SetRandomAwayTeam();
    if (!UserManager.instance.Action1WasPressed(Player.One))
      return;
    this.SelectAwayTeam();
  }

  private void SelectHomeTeam_Controller()
  {
    float num = UserManager.instance.LeftStickY(Player.One);
    if (this.allowMove)
    {
      if ((double) num > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.HomeTeamUp();
      }
      else if ((double) num < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.HomeTeamDown();
      }
    }
    if (UserManager.instance.Action3WasPressed(Player.One))
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSelect.SetRandomHomeTeam();
    if (!UserManager.instance.Action1WasPressed(Player.One))
      return;
    this.SelectHomeTeam();
  }

  private void SelectHomeTeamP2_Controller()
  {
    float num = UserManager.instance.LeftStickY(Player.Two);
    if (this.allowMoveP2)
    {
      if ((double) num > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.HomeTeamUp();
      }
      else if ((double) num < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.HomeTeamDown();
      }
    }
    if (UserManager.instance.Action3WasPressed(Player.Two))
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSelect.SetRandomHomeTeam();
    if (!UserManager.instance.Action1WasPressed(Player.Two))
      return;
    this.SelectHomeTeam();
  }

  private void SelectAwayOptions_Controller()
  {
    float num = UserManager.instance.LeftStickY(Player.One);
    if (this.allowMove)
    {
      if ((double) num > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.AwayOptionsUp();
      }
      else if ((double) num < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.AwayOptionsDown();
      }
    }
    if (!UserManager.instance.Action1WasPressed(Player.One))
      return;
    this.SelectActiveAwayOptionButton();
  }

  private void SelectHomeOptions_Controller()
  {
    float num = UserManager.instance.LeftStickY(Player.One);
    if (this.allowMove)
    {
      if ((double) num > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.HomeOptionsUp();
      }
      else if ((double) num < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.HomeOptionsDown();
      }
    }
    if (!UserManager.instance.Action1WasPressed(Player.One))
      return;
    this.SelectActiveHomeOptionButton();
  }

  private void SelectHomeOptionsP2_Controller()
  {
    float num = UserManager.instance.LeftStickY(Player.Two);
    if (this.allowMoveP2)
    {
      if ((double) num > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.HomeOptionsUp();
      }
      else if ((double) num < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.HomeOptionsDown();
      }
    }
    if (!UserManager.instance.Action1WasPressed(Player.Two))
      return;
    this.SelectActiveHomeOptionButton();
  }

  private void AwayOptionsUp()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.awayOptionsReady_Ani))
    {
      this.awayOptionsReady_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.awayOptionsEditPlaybook_Ani))
        return;
      this.awayOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayOptionsEditUniform_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void AwayOptionsDown()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.awayOptionsEditUniform_Ani))
    {
      this.awayOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.awayOptionsEditUniform_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.awayOptionsEditPlaybook_Ani))
        return;
      this.awayOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayOptionsReady_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void SelectActiveAwayOptionButton()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.awayOptionsReady_Ani))
      this.ShowAwayReadyWindow();
    else if (ControllerManagerTitle.IsUIElementSelected(this.awayOptionsEditPlaybook_Ani))
    {
      this.ShowAwayPlaybookWindow();
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.awayOptionsEditUniform_Ani))
        return;
      this.ShowAwayUniformWindow();
    }
  }

  private void HomeOptionsUp()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.homeOptionsReady_Ani))
    {
      this.homeOptionsReady_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.homeOptionsEditPlaybook_Ani))
        return;
      this.homeOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeOptionsEditUniform_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void HomeOptionsDown()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.homeOptionsEditUniform_Ani))
    {
      this.homeOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.homeOptionsEditUniform_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.homeOptionsEditPlaybook_Ani))
        return;
      this.homeOptionsEditPlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeOptionsReady_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void SelectActiveHomeOptionButton()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.homeOptionsReady_Ani))
      this.ShowHomeReadyWindow();
    else if (ControllerManagerTitle.IsUIElementSelected(this.homeOptionsEditPlaybook_Ani))
    {
      this.ShowHomePlaybookWindow();
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.homeOptionsEditUniform_Ani))
        return;
      this.ShowHomeUniformWindow();
    }
  }

  private void SelectAwayPlaybook_Controller()
  {
    float num1 = UserManager.instance.LeftStickX(Player.One);
    float num2 = UserManager.instance.LeftStickY(Player.One);
    if (this.allowMove)
    {
      if ((double) num1 > 0.40000000596046448)
        this.SelectNextAwayPlaybook();
      else if ((double) num1 < -0.40000000596046448)
        this.SelectPrevAwayPlaybook();
      else if ((double) num2 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectAwayPlaybookUp();
      }
      else if ((double) num2 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectAwayPlaybookDown();
      }
    }
    if (!UserManager.instance.Action1WasPressed(Player.One) || !ControllerManagerTitle.IsUIElementSelected(this.awayPlaybookSave_Ani))
      return;
    this.SelectAwayPlaybooks();
  }

  private void SelectHomePlaybook_Controller()
  {
    float num1 = UserManager.instance.LeftStickX(Player.One);
    float num2 = UserManager.instance.LeftStickY(Player.One);
    if (this.allowMove)
    {
      if ((double) num1 > 0.40000000596046448)
        this.SelectNextHomePlaybook();
      else if ((double) num1 < -0.40000000596046448)
        this.SelectPrevHomePlaybook();
      else if ((double) num2 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectHomePlaybookUp();
      }
      else if ((double) num2 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectHomePlaybookDown();
      }
    }
    if (!UserManager.instance.Action1WasPressed(Player.One) || !ControllerManagerTitle.IsUIElementSelected(this.homePlaybookSave_Ani))
      return;
    this.SelectHomePlaybooks();
  }

  private void SelectHomePlaybookP2_Controller()
  {
    float num1 = UserManager.instance.LeftStickX(Player.Two);
    float num2 = UserManager.instance.LeftStickY(Player.Two);
    if (this.allowMoveP2)
    {
      if ((double) num1 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.SelectNextHomePlaybook();
      }
      else if ((double) num1 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.SelectPrevHomePlaybook();
      }
      else if ((double) num2 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.SelectHomePlaybookUp();
      }
      else if ((double) num2 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.SelectHomePlaybookDown();
      }
    }
    if (!UserManager.instance.Action1WasPressed(Player.Two) || !ControllerManagerTitle.IsUIElementSelected(this.homePlaybookSave_Ani))
      return;
    this.SelectHomePlaybooks();
  }

  private void SelectNextAwayPlaybook()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.awayOffensivePlaybook_Ani))
    {
      UISoundManager.instance.PlayButtonToggle();
      this.StartCoroutine(this.DisableMove());
      this.ShowNextAwayOffensivePlaybook();
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.awayDefensivePlaybook_Ani))
        return;
      UISoundManager.instance.PlayButtonToggle();
      this.StartCoroutine(this.DisableMove());
      this.ShowNextAwayDefensivePlaybook();
    }
  }

  private void SelectPrevAwayPlaybook()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.awayOffensivePlaybook_Ani))
    {
      UISoundManager.instance.PlayButtonToggle();
      this.StartCoroutine(this.DisableMove());
      this.ShowPrevAwayOffensivePlaybook();
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.awayDefensivePlaybook_Ani))
        return;
      UISoundManager.instance.PlayButtonToggle();
      this.StartCoroutine(this.DisableMove());
      this.ShowPrevAwayDefensivePlaybook();
    }
  }

  private void SelectAwayPlaybookUp()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.awayPlaybookSave_Ani))
    {
      this.awayPlaybookSave_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayDefensivePlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.awayDefensivePlaybook_Ani))
        return;
      this.awayDefensivePlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayOffensivePlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void SelectAwayPlaybookDown()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.awayOffensivePlaybook_Ani))
    {
      this.awayOffensivePlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayDefensivePlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.awayDefensivePlaybook_Ani))
        return;
      this.awayDefensivePlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayPlaybookSave_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void SelectNextHomePlaybook()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.homeOffensivePlaybook_Ani))
    {
      this.StartCoroutine(this.DisableMove());
      this.ShowNextHomeOffensivePlaybook();
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.homeDefensivePlaybook_Ani))
        return;
      this.StartCoroutine(this.DisableMove());
      this.ShowNextHomeDefensivePlaybook();
    }
  }

  private void SelectPrevHomePlaybook()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.homeOffensivePlaybook_Ani))
    {
      this.StartCoroutine(this.DisableMove());
      this.ShowPrevHomeOffensivePlaybook();
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.homeDefensivePlaybook_Ani))
        return;
      this.StartCoroutine(this.DisableMove());
      this.ShowPrevHomeDefensivePlaybook();
    }
  }

  private void SelectHomePlaybookUp()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.homePlaybookSave_Ani))
    {
      this.homePlaybookSave_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeDefensivePlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.homeDefensivePlaybook_Ani))
        return;
      this.homeDefensivePlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeOffensivePlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void SelectHomePlaybookDown()
  {
    if (ControllerManagerTitle.IsUIElementSelected(this.homeOffensivePlaybook_Ani))
    {
      this.homeOffensivePlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeDefensivePlaybook_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else
    {
      if (!ControllerManagerTitle.IsUIElementSelected(this.homeDefensivePlaybook_Ani))
        return;
      this.homeDefensivePlaybook_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homePlaybookSave_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void SelectAwayUniform_Controller()
  {
    float num1 = UserManager.instance.LeftStickX(Player.One);
    float num2 = UserManager.instance.LeftStickY(Player.One);
    if (this.allowMove)
    {
      if ((double) num1 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SetNextAwayUniformPiece();
      }
      else if ((double) num1 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SetPrevAwayUniformPiece();
      }
      else if ((double) num2 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectAwayUniformSectionUp();
      }
      else if ((double) num2 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectAwayUniformSectionDown();
      }
    }
    if (!UserManager.instance.Action1WasPressed(Player.One) || !ControllerManagerTitle.IsUIElementSelected(this.awayUniformSave_Ani))
      return;
    this.SelectAwayUniform();
  }

  private void SelectHomeUniform_Controller()
  {
    float num1 = UserManager.instance.LeftStickX(Player.One);
    float num2 = UserManager.instance.LeftStickY(Player.One);
    if (this.allowMove)
    {
      if ((double) num1 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SetNextHomeUniformPiece();
      }
      else if ((double) num1 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SetPrevHomeUniformPiece();
      }
      else if ((double) num2 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectHomeUniformSectionUp();
      }
      else if ((double) num2 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectHomeUniformSectionDown();
      }
    }
    if (!UserManager.instance.Action1WasPressed(Player.One) || !ControllerManagerTitle.IsUIElementSelected(this.homeUniformSave_Ani))
      return;
    this.SelectHomeUniform();
  }

  private void SelectHomeUniformP2_Controller()
  {
    float num1 = UserManager.instance.LeftStickX(Player.Two);
    float num2 = UserManager.instance.LeftStickY(Player.Two);
    if (this.allowMoveP2)
    {
      if ((double) num1 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.SetNextHomeUniformPiece();
      }
      else if ((double) num1 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.SetPrevHomeUniformPiece();
      }
      else if ((double) num2 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.SelectHomeUniformSectionUp();
      }
      else if ((double) num2 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMoveP2());
        this.SelectHomeUniformSectionDown();
      }
    }
    if (!UserManager.instance.Action1WasPressed(Player.Two) || !ControllerManagerTitle.IsUIElementSelected(this.homeUniformSave_Ani))
      return;
    this.SelectHomeUniform();
  }

  private void SelectAwayUniformSectionUp()
  {
    if (this.awayUniformSelectedIndex == 1)
    {
      UISoundManager.instance.PlayButtonToggle();
      --this.awayUniformSelectedIndex;
      this.awayUniform_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.awayHelmet_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
    else if (this.awayUniformSelectedIndex == 2)
    {
      UISoundManager.instance.PlayButtonToggle();
      --this.awayUniformSelectedIndex;
      this.awayHelmet_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.awayJersey_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
    else if (this.awayUniformSelectedIndex == 3)
    {
      UISoundManager.instance.PlayButtonToggle();
      --this.awayUniformSelectedIndex;
      this.awayJersey_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.awayPants_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
    else
    {
      if (this.awayUniformSelectedIndex != 4)
        return;
      UISoundManager.instance.PlayButtonToggle();
      --this.awayUniformSelectedIndex;
      this.awayPants_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.awayUniformSave_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
  }

  private void SelectAwayUniformSectionDown()
  {
    if (this.awayUniformSelectedIndex == 0)
    {
      UISoundManager.instance.PlayButtonToggle();
      ++this.awayUniformSelectedIndex;
      this.awayUniform_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayHelmet_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else if (this.awayUniformSelectedIndex == 1)
    {
      UISoundManager.instance.PlayButtonToggle();
      ++this.awayUniformSelectedIndex;
      this.awayHelmet_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayJersey_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else if (this.awayUniformSelectedIndex == 2)
    {
      UISoundManager.instance.PlayButtonToggle();
      ++this.awayUniformSelectedIndex;
      this.awayJersey_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayPants_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else
    {
      if (this.awayUniformSelectedIndex != 3)
        return;
      UISoundManager.instance.PlayButtonToggle();
      ++this.awayUniformSelectedIndex;
      this.awayPants_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.awayUniformSave_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void SelectHomeUniformSectionUp()
  {
    if (this.homeUniformSelectedIndex == 1)
    {
      UISoundManager.instance.PlayButtonToggle();
      --this.homeUniformSelectedIndex;
      this.homeUniform_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.homeHelmet_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
    else if (this.homeUniformSelectedIndex == 2)
    {
      UISoundManager.instance.PlayButtonToggle();
      --this.homeUniformSelectedIndex;
      this.homeHelmet_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.homeJersey_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
    else if (this.homeUniformSelectedIndex == 3)
    {
      UISoundManager.instance.PlayButtonToggle();
      --this.homeUniformSelectedIndex;
      this.homeJersey_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.homePants_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
    else
    {
      if (this.homeUniformSelectedIndex != 4)
        return;
      UISoundManager.instance.PlayButtonToggle();
      --this.homeUniformSelectedIndex;
      this.homePants_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.homeUniformSave_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
  }

  private void SelectHomeUniformSectionDown()
  {
    if (this.homeUniformSelectedIndex == 0)
    {
      UISoundManager.instance.PlayButtonToggle();
      ++this.homeUniformSelectedIndex;
      this.homeUniform_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeHelmet_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else if (this.homeUniformSelectedIndex == 1)
    {
      UISoundManager.instance.PlayButtonToggle();
      ++this.homeUniformSelectedIndex;
      this.homeHelmet_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeJersey_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else if (this.homeUniformSelectedIndex == 2)
    {
      UISoundManager.instance.PlayButtonToggle();
      ++this.homeUniformSelectedIndex;
      this.homeJersey_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homePants_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else
    {
      if (this.homeUniformSelectedIndex != 3)
        return;
      UISoundManager.instance.PlayButtonToggle();
      ++this.homeUniformSelectedIndex;
      this.homePants_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.homeUniformSave_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
  }

  private void SetNextAwayUniformPiece()
  {
    if (this.awayUniformSelectedIndex == 0)
      this.SetNextAwayUniform();
    else if (this.awayUniformSelectedIndex == 1)
      this.SetNextAwayHelmet();
    else if (this.awayUniformSelectedIndex == 2)
    {
      this.SetNextAwayJersey();
    }
    else
    {
      if (this.awayUniformSelectedIndex != 3)
        return;
      this.SetNextAwayPants();
    }
  }

  private void SetPrevAwayUniformPiece()
  {
    if (this.awayUniformSelectedIndex == 0)
      this.SetPrevAwayUniform();
    else if (this.awayUniformSelectedIndex == 1)
      this.SetPrevAwayHelmet();
    else if (this.awayUniformSelectedIndex == 2)
    {
      this.SetPrevAwayJersey();
    }
    else
    {
      if (this.awayUniformSelectedIndex != 3)
        return;
      this.SetPrevAwayPants();
    }
  }

  private void SetNextHomeUniformPiece()
  {
    if (this.homeUniformSelectedIndex == 0)
      this.SetNextHomeUniform();
    else if (this.homeUniformSelectedIndex == 1)
      this.SetNextHomeHelmet();
    else if (this.homeUniformSelectedIndex == 2)
    {
      this.SetNextHomeJersey();
    }
    else
    {
      if (this.homeUniformSelectedIndex != 3)
        return;
      this.SetNextHomePants();
    }
  }

  private void SetPrevHomeUniformPiece()
  {
    if (this.homeUniformSelectedIndex == 0)
      this.SetPrevHomeUniform();
    else if (this.homeUniformSelectedIndex == 1)
      this.SetPrevHomeHelmet();
    else if (this.homeUniformSelectedIndex == 2)
    {
      this.SetPrevHomeJersey();
    }
    else
    {
      if (this.homeUniformSelectedIndex != 3)
        return;
      this.SetPrevHomePants();
    }
  }

  public void SetNextAwayUniform()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.SetAwayUniform(this.awayUniformIndex + 1 < this.awayUniformSet.GetNumberOfUniforms() ? this.awayUniformIndex + 1 : 0);
  }

  public void SetPrevAwayUniform()
  {
    UISoundManager.instance.PlayButtonToggle();
    int numberOfUniforms = this.awayUniformSet.GetNumberOfUniforms();
    this.SetAwayUniform(this.awayUniformIndex - 1 >= 0 ? this.awayUniformIndex - 1 : numberOfUniforms - 1);
  }

  public void SetNextHomeUniform()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.SetHomeUniform(this.homeUniformIndex + 1 < this.homeUniformSet.GetNumberOfUniforms() ? this.homeUniformIndex + 1 : 0);
  }

  public void SetPrevHomeUniform()
  {
    UISoundManager.instance.PlayButtonToggle();
    int numberOfUniforms = this.homeUniformSet.GetNumberOfUniforms();
    this.SetHomeUniform(this.homeUniformIndex - 1 >= 0 ? this.homeUniformIndex - 1 : numberOfUniforms - 1);
  }

  public void SetNextAwayHelmet()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.SetAwayCustomHelmet(this.awayHelmetIndex + 1 < this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets) ? this.awayHelmetIndex + 1 : 0);
  }

  public void SetPrevAwayHelmet()
  {
    UISoundManager.instance.PlayButtonToggle();
    int numberOfUniformPieces = this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets);
    this.SetAwayCustomHelmet(this.awayHelmetIndex - 1 >= 0 ? this.awayHelmetIndex - 1 : numberOfUniformPieces - 1);
  }

  private void SetAwayCustomHelmet(int pieceIndex)
  {
    this.awayHelmetIndex = pieceIndex;
    this.awayUniformName_Txt.text = "-";
    this.awayUniformManager.SetHelmetTexture(this.awayUniformSet.GetHelmetTexture(this.awayHelmetIndex));
    this.awayHelmetName_Txt.text = "(" + (this.awayHelmetIndex + 1).ToString() + "/" + this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets).ToString() + ")";
  }

  public void SetNextHomeHelmet()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.SetHomeCustomHelmet(this.homeHelmetIndex + 1 < this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets) ? this.homeHelmetIndex + 1 : 0);
  }

  public void SetPrevHomeHelmet()
  {
    UISoundManager.instance.PlayButtonToggle();
    int numberOfUniformPieces = this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets);
    this.SetHomeCustomHelmet(this.homeHelmetIndex - 1 >= 0 ? this.homeHelmetIndex - 1 : numberOfUniformPieces - 1);
  }

  private void SetHomeCustomHelmet(int pieceIndex)
  {
    this.homeHelmetIndex = pieceIndex;
    this.homeUniformName_Txt.text = "-";
    this.homeUniformManager.SetHelmetTexture(this.homeUniformSet.GetHelmetTexture(this.homeHelmetIndex));
    this.homeHelmetName_Txt.text = "(" + (this.homeHelmetIndex + 1).ToString() + "/" + this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets).ToString() + ")";
  }

  public void SetNextAwayJersey()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.SetAwayCustomJersey(this.awayJerseyIndex + 1 < this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys) ? this.awayJerseyIndex + 1 : 0);
  }

  public void SetPrevAwayJersey()
  {
    UISoundManager.instance.PlayButtonToggle();
    int numberOfUniformPieces = this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys);
    this.SetAwayCustomJersey(this.awayJerseyIndex - 1 >= 0 ? this.awayJerseyIndex - 1 : numberOfUniformPieces - 1);
  }

  private void SetAwayCustomJersey(int pieceIndex)
  {
    this.awayJerseyIndex = pieceIndex;
    this.awayUniformName_Txt.text = "-";
    this.awayUniformManager.SetJersey(this.awayJerseyIndex, "AXIS", Random.Range(1, 100), UniformAssetType.TEMP);
    this.awayJerseyName_Txt.text = "(" + (this.awayJerseyIndex + 1).ToString() + "/" + this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys).ToString() + ")";
  }

  public void SetNextHomeJersey()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.SetHomeCustomJersey(this.homeJerseyIndex + 1 < this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys) ? this.homeJerseyIndex + 1 : 0);
  }

  public void SetPrevHomeJersey()
  {
    UISoundManager.instance.PlayButtonToggle();
    int numberOfUniformPieces = this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys);
    this.SetHomeCustomJersey(this.homeJerseyIndex - 1 >= 0 ? this.homeJerseyIndex - 1 : numberOfUniformPieces - 1);
  }

  private void SetHomeCustomJersey(int pieceIndex)
  {
    this.homeJerseyIndex = pieceIndex;
    this.homeUniformName_Txt.text = "-";
    this.homeUniformManager.SetJersey(this.homeJerseyIndex, "AXIS", Random.Range(1, 100), UniformAssetType.TEMP);
    this.homeJerseyName_Txt.text = "(" + (this.homeJerseyIndex + 1).ToString() + "/" + this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys).ToString() + ")";
  }

  public void SetNextAwayPants()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.SetAwayCustomPants(this.awayPantIndex + 1 < this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants) ? this.awayPantIndex + 1 : 0);
  }

  public void SetPrevAwayPants()
  {
    UISoundManager.instance.PlayButtonToggle();
    int numberOfUniformPieces = this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants);
    this.SetAwayCustomPants(this.awayPantIndex - 1 >= 0 ? this.awayPantIndex - 1 : numberOfUniformPieces - 1);
  }

  private void SetAwayCustomPants(int pieceIndex)
  {
    this.awayPantIndex = pieceIndex;
    this.awayUniformName_Txt.text = "-";
    this.awayUniformManager.SetPantTexture(this.awayUniformSet.GetPantTexture(pieceIndex));
    this.awayPantsName_Txt.text = "(" + (this.awayPantIndex + 1).ToString() + "/" + this.awayUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants).ToString() + ")";
  }

  public void SetNextHomePants()
  {
    UISoundManager.instance.PlayButtonToggle();
    this.SetHomeCustomPants(this.homePantIndex + 1 < this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants) ? this.homePantIndex + 1 : 0);
  }

  public void SetPrevHomePants()
  {
    UISoundManager.instance.PlayButtonToggle();
    int numberOfUniformPieces = this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants);
    this.SetHomeCustomPants(this.homePantIndex - 1 >= 0 ? this.homePantIndex - 1 : numberOfUniformPieces - 1);
  }

  private void SetHomeCustomPants(int pieceIndex)
  {
    this.homePantIndex = pieceIndex;
    this.homeUniformName_Txt.text = "-";
    this.homeUniformManager.SetPantTexture(this.homeUniformSet.GetPantTexture(pieceIndex));
    this.homePantsName_Txt.text = "(" + (this.homePantIndex + 1).ToString() + "/" + this.homeUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants).ToString() + ")";
  }

  public IEnumerator DisableMove()
  {
    this.allowMove = false;
    yield return (object) this._disableMove;
    this.allowMove = true;
  }

  private IEnumerator DisableMoveP2()
  {
    this.allowMoveP2 = false;
    yield return (object) this._disableMove;
    this.allowMoveP2 = true;
  }

  public void ReturnToPreviousMenu(int player)
  {
    UISoundManager.instance.PlayButtonBack();
    switch (player)
    {
      case 1:
        if (PersistentData.gameType == GameType.SeasonMode)
        {
          if (this.IsAwayPlaybookSelectVisible())
          {
            this.SelectAwayPlaybooks();
            break;
          }
          if (this.IsAwayUniformSelectVisible())
          {
            this.SelectAwayUniform();
            break;
          }
          if (this.IsHomeUniformSelectVisible())
          {
            this.SelectHomeUniform();
            break;
          }
          if (this.IsHomeOptionsWindowVisible())
          {
            this.HideAwayReadyWindow();
            this.ShowAwayOptionsWindow();
            this.HideHomeOptionsWindow();
            this.ShowHomeTeamSelect();
            break;
          }
          SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.ReturnToFranchiseMode();
          break;
        }
        if (this.IsAwayPlaybookSelectVisible())
        {
          this.SelectAwayPlaybooks();
          break;
        }
        if (this.IsAwayUniformSelectVisible())
        {
          this.SelectAwayUniform();
          break;
        }
        if (this.IsAwayOptionsWindowVisible())
        {
          this.HideAwayOptionsWindow();
          this.ShowAwayTeamSelect();
          this.SetIsSelectingAwayTeam(true);
          break;
        }
        if (this.IsAwayTeamReady() && !PersistentData.Is2PMatch)
        {
          if (this.IsHomePlaybookSelectVisible())
          {
            this.SelectHomePlaybooks();
            break;
          }
          if (this.IsHomeUniformSelectVisible())
          {
            this.SelectHomeUniform();
            break;
          }
          if (this.IsHomeOptionsWindowVisible())
          {
            this.HideHomeOptionsWindow();
            this.ShowHomeTeamSelect();
            this.SetIsSelectingHomeTeam(true);
            break;
          }
          if (!this.isSelectingHomeTeam)
            break;
          this.isUserSelectingCompTeam = false;
          this.isSelectingHomeTeam = false;
          this.SetIsSelectingHomeTeam(false);
          this.HideAwayReadyWindow();
          this.ShowAwayOptionsWindow();
          break;
        }
        this.HideWindow();
        SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowWindow();
        SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowBottomSection();
        SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowPlayNowSection();
        break;
      case 2:
        if (this.IsAwayTeamReady())
        {
          this.SetIsSelectingAwayTeam(true);
          this.HideAwayReadyWindow();
          break;
        }
        if (!this.IsAwayUniformSelectVisible())
          break;
        this.SelectAwayUniform();
        break;
    }
  }
}
