// Decompiled with JetBrains decompiler
// Type: GameSetupManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using Framework;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class GameSetupManager : MonoBehaviour
{
  [Header("Main")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private RectTransform mainWindow_Trans;
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
  private TextMeshProUGUI awayTeamCity_Txt;
  [SerializeField]
  private TextMeshProUGUI homeTeamCity_Txt;
  [SerializeField]
  private TextMeshProUGUI gameSetupSummary_Txt;
  [SerializeField]
  private UnityEngine.UI.Button playGame_Btn;
  private bool isMainSectionShowing;
  private bool isGameOptionSelectShowing;
  private TeamData homeTeamData;
  private TeamData awayTeamData;
  private TeamSelectManager teamSelectManager;
  [Header("Player Comparison")]
  [SerializeField]
  private PlayerComparisonSlot[] playerComparisons;
  [SerializeField]
  private GameObject playerComparisonContainer_GO;
  [SerializeField]
  private RectTransform playerComparisonContainer_Trans;
  [SerializeField]
  private Image[] sectionIndicators_Img;
  [SerializeField]
  private TextMeshProUGUI positionComparison_Txt;
  private int playerComparisonSectionIndex;
  private int[] attributeOrder;
  private float cycleTimer;
  [Header("Expert Picks")]
  [SerializeField]
  private RectTransform expertPicks_Trans;
  [SerializeField]
  private ExpertPickSlot[] expertPicks;
  [SerializeField]
  private TextMeshProUGUI verdict_Txt;
  [Header("Main Bottom Section")]
  [SerializeField]
  private RectTransform gameSettings_Trans;
  [SerializeField]
  private TextMeshProUGUI settingsTitle_Txt;
  [SerializeField]
  private GameObject gameSettingsContainer_GO;
  [SerializeField]
  private GameObject activeIndicator_GO;
  [SerializeField]
  private RectTransform activeIndicator_Trans;
  [SerializeField]
  private RectTransform rulesBtn_Trans;
  [SerializeField]
  private RectTransform gameplayBtn_Trans;
  [SerializeField]
  private RectTransform stadiumBtn_Trans;
  [SerializeField]
  private TextMeshProUGUI rulesBtn_Txt;
  [SerializeField]
  private TextMeshProUGUI gameplayBtn_Txt;
  [SerializeField]
  private TextMeshProUGUI stadiumBtn_Txt;
  private int gameSettingsSectionIndex;
  private float activeIndicatorAnimationSpeed = 0.15f;
  [Header("Rules Settings")]
  [SerializeField]
  private string[] quarterLengthOptions;
  [SerializeField]
  private TextMeshProUGUI quarterLengthValue_Txt;
  [SerializeField]
  private string[] injuryOptions;
  [SerializeField]
  private TextMeshProUGUI injuryValue_Txt;
  [SerializeField]
  private string[] playerFatigueOptions;
  [SerializeField]
  private TextMeshProUGUI playerFatigueValue_Txt;
  [SerializeField]
  private Animator quarterLength_Ani;
  [SerializeField]
  private Animator injuries_Ani;
  [SerializeField]
  private Animator playerFatigue_Ani;
  private int quarterLengthIndex = 1;
  private int injuryIndex = 1;
  private int playerFatigueIndex = 1;
  [Header("Gameplay Settings")]
  [SerializeField]
  private string[] offDifficultyOptions;
  [SerializeField]
  private TextMeshProUGUI offDifficultyValue_Txt;
  [SerializeField]
  private string[] defDifficultyOptions;
  [SerializeField]
  private TextMeshProUGUI defDifficultyValue_Txt;
  [SerializeField]
  private Animator offDifficulty_Ani;
  [SerializeField]
  private Animator defDifficulty_Ani;
  private int offDifficultyIndex = 1;
  private int defDifficultyIndex = 1;
  [Header("Stadium Settings")]
  [SerializeField]
  private TextMeshProUGUI stadiumValue_Txt;
  [SerializeField]
  private string[] weatherTypeOptions;
  [SerializeField]
  private TextMeshProUGUI weatherTypeValue_Txt;
  [SerializeField]
  private string[] timeOfDayOptions;
  [SerializeField]
  private TextMeshProUGUI timeOfDayValue_Txt;
  [SerializeField]
  private string[] windTypeOptions;
  [SerializeField]
  private TextMeshProUGUI windTypeValue_Txt;
  [SerializeField]
  private Image stadiumPreview_Img;
  [SerializeField]
  private TextMeshProUGUI stadiumInformation_Txt;
  [SerializeField]
  private Animator stadium_Ani;
  [SerializeField]
  private Animator forecast_Ani;
  [SerializeField]
  private Animator timeOfDay_Ani;
  [SerializeField]
  private Animator wind_Ani;
  private int weatherTypeIndex;
  private int timeOfDayIndex;
  private int windTypeIndex;
  private bool stadiumHasChanged;
  private int selectedIndex;
  private bool allowMove;
  private WaitForSeconds _disableMove = new WaitForSeconds(0.2f);

  private void Update()
  {
    if (!this.IsVisible())
      return;
    this.ManageControllerSupport();
    this.CyclePlayerComparison();
  }

  public void Init()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
    this.allowMove = true;
    this.teamSelectManager = SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSelect;
    StadiumSetDatabase.GetTotalStadiums();
  }

  public void ShowWindow()
  {
    PopupLoadingScreen.self.ShowPopupLoadingScreen("LOADING GAME SETUP");
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
    PersistentData.userIsHome = this.teamSelectManager.homeTeamSwitched;
    PortraitManager.self.ClearTeamPlayerPortraits();
    this.homeTeamData = this.teamSelectManager.GetSelectedHomeTeam();
    this.awayTeamData = this.teamSelectManager.GetSelectedAwayTeam();
    if (!Game.IsSeasonMode && !PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets)
    {
      this.homeTeamData.TeamDepthChart.SetBestStartersForAllPositions();
      this.awayTeamData.TeamDepthChart.SetBestStartersForAllPositions();
    }
    if (PersistentData.userIsHome)
    {
      PersistentData.SetUserTeam(this.homeTeamData);
      PersistentData.SetCompTeam(this.awayTeamData);
    }
    else
    {
      PersistentData.SetUserTeam(this.awayTeamData);
      PersistentData.SetCompTeam(this.homeTeamData);
    }
    PersistentData.stadiumSet = StadiumSetDatabase.GetStadiumData();
    this.stadiumHasChanged = false;
    Plays.self.SetOffensivePlaybookP1(this.teamSelectManager.GetPlayer1_OffensivePlaybook());
    Plays.self.SetDefensivePlaybookP1(this.teamSelectManager.GetPlayer1_DefensivePlaybook());
    Plays.self.SetOffensivePlaybookP2(this.teamSelectManager.GetPlayer2_OffensivePlaybook());
    Plays.self.SetDefensivePlaybookP2(this.teamSelectManager.GetPlayer2_DefensivePlaybook());
    this.SetPlayerComparisonData();
    this.SetPlayerComparison(0);
    if (Game.IsSeasonMode)
    {
      this.SelectOffDifficulty(PersistentSingleton<SaveManager>.Instance.gameSettings.OffDifficulty);
      this.SelectDefDifficulty(PersistentSingleton<SaveManager>.Instance.gameSettings.DefDifficulty);
      this.SelectQuarterLength(PersistentSingleton<SaveManager>.Instance.gameSettings.QuarterLength);
      this.SelectPlayerFatigue(PersistentSingleton<SaveManager>.Instance.gameSettings.UseFatigue);
      this.SelectInjury(PersistentSingleton<SaveManager>.Instance.gameSettings.UseInjuries);
      this.SelectWeatherType(UnityEngine.Random.Range(0, this.weatherTypeOptions.Length));
      this.SelectTimeOfDay(UnityEngine.Random.Range(0, this.timeOfDayOptions.Length));
      this.SelectWindType(UnityEngine.Random.Range(0, this.windTypeOptions.Length));
      this.settingsTitle_Txt.text = "FRANCHISE";
    }
    else
    {
      this.SelectOffDifficulty(PersistentSingleton<SaveManager>.Instance.exhibitionSettings.OffDifficulty);
      this.SelectDefDifficulty(PersistentSingleton<SaveManager>.Instance.exhibitionSettings.DefDifficulty);
      this.SelectQuarterLength(PersistentSingleton<SaveManager>.Instance.exhibitionSettings.QuarterLengthIndex);
      this.SelectPlayerFatigue(PersistentSingleton<SaveManager>.Instance.exhibitionSettings.UseFatigue);
      this.SelectInjury(PersistentSingleton<SaveManager>.Instance.exhibitionSettings.UseInjuries);
      this.SelectWeatherType(PersistentSingleton<SaveManager>.Instance.exhibitionSettings.WeatherTypeIndex);
      this.SelectTimeOfDay(PersistentSingleton<SaveManager>.Instance.exhibitionSettings.TimeOfDayIndex);
      this.SelectWindType(PersistentSingleton<SaveManager>.Instance.exhibitionSettings.WindIndex);
      this.settingsTitle_Txt.text = "EXHIBITION";
    }
    StadiumSetDatabase.SetStadiumForHomeCity(this.homeTeamData.GetCity());
    this.SelectStadium();
    this.FillExpertPicks();
    PopupLoadingScreen.self.HidePopupLoadingScreen();
    this.awayTeamLogo_Img.sprite = this.awayTeamData.GetMediumLogo();
    this.homeTeamLogo_Img.sprite = this.homeTeamData.GetMediumLogo();
    this.awayTeamBackground_Img.color = this.awayTeamData.GetPrimaryColor();
    this.homeTeamBackground_Img.color = this.homeTeamData.GetPrimaryColor();
    this.awayTeamCity_Txt.text = this.awayTeamData.GetCity();
    this.homeTeamCity_Txt.text = this.homeTeamData.GetCity();
    RectTransform teamSectionTrans1 = this.awayTeamSection_Trans;
    Rect rect = this.awayTeamSection_Trans.rect;
    double to1 = (double) rect.width * -1.0 * 0.800000011920929;
    LeanTween.moveX(teamSectionTrans1, (float) to1, 0.0f);
    RectTransform teamSectionTrans2 = this.homeTeamSection_Trans;
    rect = this.homeTeamSection_Trans.rect;
    double to2 = (double) rect.width * 0.800000011920929;
    LeanTween.moveX(teamSectionTrans2, (float) to2, 0.0f);
    LeanTween.moveX(this.awayTeamSection_Trans, 0.0f, 0.3f);
    LeanTween.moveX(this.homeTeamSection_Trans, 0.0f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.ShowMainSection();
  }

  public void ShowMainSection()
  {
    this.isMainSectionShowing = true;
    this.isGameOptionSelectShowing = false;
    if (this.stadiumHasChanged)
      this.stadiumHasChanged = false;
    this.SetGameSetupSummary();
    UISoundManager.instance.PlayTabSwipe();
    LeanTween.move(this.mainWindow_Trans, (Vector3) new Vector2(0.0f, 0.0f), 0.3f).setEaseOutQuart();
    LeanTween.move(this.expertPicks_Trans, (Vector3) new Vector2(0.0f, this.expertPicks_Trans.rect.height * -1f), 0.3f).setEaseOutQuart();
    LeanTween.move(this.gameSettings_Trans, (Vector3) new Vector2(0.0f, this.gameSettings_Trans.rect.height * -1f), 0.3f).setEaseOutQuart();
    ControllerManagerTitle.self.DeselectCurrentUIElement();
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.playGame_Btn);
    BottomBarManager.instance.SetControllerButtonGuide(17);
  }

  private void SetGameSetupSummary()
  {
    Debug.LogWarning((object) "CD: Debug stadium name set here");
    this.gameSetupSummary_Txt.text = "DEBUG STADIUM NAME  /  " + this.weatherTypeOptions[PersistentData.weather].ToUpper() + "  /  " + this.quarterLengthOptions[PersistentData.quarterLength].ToUpper() + "  /  " + this.offDifficultyValue_Txt.text.ToUpper();
  }

  public void StartMatch()
  {
    this.ClearDataBeforeMatch();
    AppEvents.SaveExhibitionSettings.Trigger();
    AppEvents.SaveGameSettings.Trigger();
    StadiumSetDatabase.ClearStadiumDatabase();
    LoadingScreenManager.self.LoadScene("Game", "Loading Team and Player Data");
  }

  private void SetPlayerComparisonData()
  {
    this.playerComparisons[0].SetPlayerComparison(this.awayTeamData, this.homeTeamData, Position.QB);
    this.playerComparisons[1].SetPlayerComparison(this.awayTeamData, this.homeTeamData, Position.RB);
    this.playerComparisons[2].SetPlayerComparison(this.awayTeamData, this.homeTeamData, Position.WR);
  }

  private void CyclePlayerComparison()
  {
    if (!this.isMainSectionShowing)
      return;
    this.cycleTimer += Time.deltaTime;
    if ((double) this.cycleTimer <= 4.0)
      return;
    this.ShowNextPlayerComparison();
  }

  public void ShowNextPlayerComparison()
  {
    UISoundManager.instance.PlayTabSwipe();
    if (this.playerComparisonSectionIndex == 0)
      this.SetPlayerComparison(1);
    else if (this.playerComparisonSectionIndex == 1)
      this.SetPlayerComparison(2);
    else
      this.SetPlayerComparison(0);
  }

  public void ShowPrevPlayerComparison()
  {
    UISoundManager.instance.PlayTabSwipe();
    if (this.playerComparisonSectionIndex == 0)
      this.SetPlayerComparison(2);
    else if (this.playerComparisonSectionIndex == 1)
      this.SetPlayerComparison(0);
    else
      this.SetPlayerComparison(1);
  }

  private void SetPlayerComparison(int index)
  {
    this.cycleTimer = 0.0f;
    this.playerComparisonSectionIndex = index;
    if (this.playerComparisonSectionIndex == 0)
      this.positionComparison_Txt.text = "QB COMPARISON";
    else if (this.playerComparisonSectionIndex == 1)
      this.positionComparison_Txt.text = "RB COMPARISON";
    else
      this.positionComparison_Txt.text = "WR COMPARISON";
    LeanTween.moveLocalX(this.playerComparisonContainer_GO, this.playerComparisonContainer_Trans.rect.width * -1f * (float) index, 0.15f);
    for (int index1 = 0; index1 < this.sectionIndicators_Img.Length; ++index1)
      this.sectionIndicators_Img[index1].enabled = false;
    this.sectionIndicators_Img[this.playerComparisonSectionIndex].enabled = true;
  }

  public void ShowExpertPicksWindow()
  {
    UISoundManager.instance.PlayTabSwipe();
    ControllerManagerTitle.self.DeselectCurrentUIElement();
    this.isMainSectionShowing = false;
    LeanTween.move(this.mainWindow_Trans, (Vector3) new Vector2(0.0f, this.mainWindow_Trans.rect.height), 0.3f).setEaseOutQuart();
    LeanTween.move(this.expertPicks_Trans, (Vector3) new Vector2(0.0f, 0.0f), 0.3f).setEaseOutQuart();
    BottomBarManager.instance.SetControllerButtonGuide(14);
  }

  public void FillExpertPicks()
  {
    this.expertPicks[0].FillPick(SimulationManager.SimulateGame(this.homeTeamData, this.awayTeamData), "DANNY", 2, 34);
    this.expertPicks[1].FillPick(SimulationManager.SimulateGame(this.homeTeamData, this.awayTeamData), "JOE", 1, 98);
    this.expertPicks[2].FillPick(SimulationManager.SimulateGame(this.homeTeamData, this.awayTeamData), "DAVE", 2, 5);
    this.expertPicks[3].FillPick(SimulationManager.SimulateGame(this.homeTeamData, this.awayTeamData), "JAMES", 3, 83);
    int teamIndex1 = this.homeTeamData.TeamIndex;
    int teamIndex2 = this.awayTeamData.TeamIndex;
    int num1 = 0;
    int num2 = 0;
    for (int index = 0; index < 100; ++index)
    {
      int winningTeam = SimulationManager.SimulateGame(this.homeTeamData, this.awayTeamData).WinningTeam;
      if (teamIndex1 == winningTeam)
        ++num1;
      else
        ++num2;
    }
    if (teamIndex1 == teamIndex2)
    {
      num1 = 50;
      num2 = 50;
    }
    if (PersistentData.gameType != GameType.SeasonMode || PersistentData.watchingNonUserMatch ? PersistentData.gameType != GameType.QuickMatch || !PersistentData.UserCallsPlays || PersistentData.userIsHome : PersistentData.userIsHome)
      this.verdict_Txt.text = this.homeTeamData.GetAbbreviation() + " has a " + num1.ToString() + "% chance of winning based on 1,000 simulations";
    else
      this.verdict_Txt.text = this.awayTeamData.GetAbbreviation() + " has a " + num2.ToString() + "% chance of winning based on 1,000 simulations";
  }

  public void ShowGameSettings()
  {
    this.isMainSectionShowing = false;
    this.isGameOptionSelectShowing = true;
    LeanTween.move(this.mainWindow_Trans, (Vector3) new Vector2(0.0f, this.mainWindow_Trans.rect.height), 0.3f).setEaseOutQuart();
    LeanTween.move(this.gameSettings_Trans, (Vector3) new Vector2(0.0f, 0.0f), 0.3f).setEaseOutQuart();
    LeanTween.moveLocalX(this.gameSettingsContainer_GO, 0.0f, 0.0f);
    ControllerManagerTitle.self.DeselectCurrentUIElement();
    this.ShowRulesSection();
    BottomBarManager.instance.SetControllerButtonGuide(3);
  }

  public void ShowRulesSection()
  {
    UISoundManager.instance.PlayTabSwipe();
    this.gameSettingsSectionIndex = 0;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.rulesBtn_Trans.localPosition.x, this.activeIndicatorAnimationSpeed);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.rulesBtn_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed);
    LeanTween.moveLocalX(this.gameSettingsContainer_GO, 0.0f, this.activeIndicatorAnimationSpeed);
    this.rulesBtn_Txt.color = AxisFootballColors.brightBlue;
    this.gameplayBtn_Txt.color = Color.white;
    this.stadiumBtn_Txt.color = Color.white;
    if (!ControllerManagerTitle.self.usingController)
      return;
    this.HighlightQuarterLength();
  }

  public void ShowGameplaySection()
  {
    UISoundManager.instance.PlayTabSwipe();
    this.gameSettingsSectionIndex = 1;
    this.selectedIndex = 0;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.gameplayBtn_Trans.localPosition.x, this.activeIndicatorAnimationSpeed);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.gameplayBtn_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed);
    LeanTween.moveLocalX(this.gameSettingsContainer_GO, this.gameSettings_Trans.rect.width * -1f, this.activeIndicatorAnimationSpeed);
    this.rulesBtn_Txt.color = Color.white;
    this.gameplayBtn_Txt.color = AxisFootballColors.brightBlue;
    this.stadiumBtn_Txt.color = Color.white;
    if (!ControllerManagerTitle.self.usingController)
      return;
    this.HighlightOffensiveDifficulty();
  }

  public void ShowStadiumSection()
  {
    UISoundManager.instance.PlayTabSwipe();
    this.gameSettingsSectionIndex = 2;
    this.selectedIndex = 0;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.stadiumBtn_Trans.localPosition.x, this.activeIndicatorAnimationSpeed);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.stadiumBtn_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed);
    LeanTween.moveLocalX(this.gameSettingsContainer_GO, this.gameSettings_Trans.rect.width * -2f, this.activeIndicatorAnimationSpeed);
    this.rulesBtn_Txt.color = Color.white;
    this.gameplayBtn_Txt.color = Color.white;
    this.stadiumBtn_Txt.color = AxisFootballColors.brightBlue;
    if (!ControllerManagerTitle.self.usingController)
      return;
    this.HighlightStadiumSelect();
  }

  public void SelectNextQuarterLength()
  {
    if (this.quarterLengthIndex >= this.quarterLengthOptions.Length - 1)
      return;
    this.SelectQuarterLength(this.quarterLengthIndex + 1);
  }

  public void SelectPrevQuarterLength()
  {
    if (this.quarterLengthIndex <= 0)
      return;
    this.SelectQuarterLength(this.quarterLengthIndex - 1);
  }

  private void SelectQuarterLength(int index)
  {
    this.quarterLengthValue_Txt.text = this.quarterLengthOptions[index];
    PersistentData.quarterLength = index;
    this.quarterLengthIndex = index;
    if (Game.IsSeasonMode)
      PersistentSingleton<SaveManager>.Instance.gameSettings.QuarterLength = index;
    else
      PersistentSingleton<SaveManager>.Instance.exhibitionSettings.QuarterLengthIndex = index;
  }

  public void SelectNextPlayerFatigue()
  {
    if (this.playerFatigueIndex >= this.playerFatigueOptions.Length - 1)
      return;
    this.SelectPlayerFatigue(this.playerFatigueIndex + 1);
  }

  public void SelectPrevPlayerFatigue()
  {
    if (this.playerFatigueIndex <= 0)
      return;
    this.SelectPlayerFatigue(this.playerFatigueIndex - 1);
  }

  private void SelectPlayerFatigue(int index)
  {
    this.playerFatigueValue_Txt.text = this.playerFatigueOptions[index];
    this.playerFatigueIndex = index;
    PersistentData.playerFatigueOn = index != 0;
    if (Game.IsSeasonMode)
      PersistentSingleton<SaveManager>.Instance.gameSettings.UseFatigue = PersistentData.playerFatigueOn;
    else
      PersistentSingleton<SaveManager>.Instance.exhibitionSettings.UseFatigue = Convert.ToBoolean(index);
  }

  private void SelectPlayerFatigue(bool use)
  {
    if (use)
      this.SelectPlayerFatigue(1);
    else
      this.SelectPlayerFatigue(0);
  }

  public void SelectNextInjury()
  {
    if (this.injuryIndex >= this.injuryOptions.Length - 1)
      return;
    this.SelectInjury(this.injuryIndex + 1);
  }

  public void SelectPrevInjury()
  {
    if (this.injuryIndex <= 0)
      return;
    this.SelectInjury(this.injuryIndex - 1);
  }

  private void SelectInjury(int index)
  {
    this.injuryValue_Txt.text = this.injuryOptions[index];
    this.injuryIndex = index;
    PersistentData.injuriesOn = index != 0;
    if (Game.IsSeasonMode)
      PersistentSingleton<SaveManager>.Instance.gameSettings.UseInjuries = PersistentData.injuriesOn;
    else
      PersistentSingleton<SaveManager>.Instance.exhibitionSettings.UseInjuries = Convert.ToBoolean(index);
  }

  private void SelectInjury(bool use)
  {
    if (use)
      this.SelectInjury(1);
    else
      this.SelectInjury(0);
  }

  public void SelectNextOffDifficulty()
  {
    if (this.offDifficultyIndex >= this.offDifficultyOptions.Length - 1)
      return;
    ++this.offDifficultyIndex;
    this.SelectOffDifficulty(this.offDifficultyIndex);
  }

  public void SelectPrevOffDifficulty()
  {
    if (this.offDifficultyIndex <= 0)
      return;
    --this.offDifficultyIndex;
    this.SelectOffDifficulty(this.offDifficultyIndex);
  }

  private void SelectOffDifficulty(int index)
  {
    this.offDifficultyValue_Txt.text = this.offDifficultyOptions[index];
    this.offDifficultyIndex = index;
    if (index == 0)
      PersistentData.offDifficulty = 10;
    else if (index == 1)
      PersistentData.offDifficulty = 7;
    else if (index == 2)
      PersistentData.offDifficulty = 4;
    else if (index == 3)
      PersistentData.offDifficulty = 1;
    if (index == 0)
      PersistentData.difficulty = 10;
    else if (index == 1)
      PersistentData.difficulty = 7;
    else if (index == 2)
      PersistentData.difficulty = 4;
    else if (index == 3)
      PersistentData.difficulty = 1;
    if (Game.IsSeasonMode)
      PersistentSingleton<SaveManager>.Instance.gameSettings.OffDifficulty = index;
    else
      PersistentSingleton<SaveManager>.Instance.exhibitionSettings.OffDifficulty = index;
  }

  public void SelectNextDefDifficulty()
  {
    if (this.defDifficultyIndex >= this.defDifficultyOptions.Length - 1)
      return;
    ++this.defDifficultyIndex;
    this.SelectDefDifficulty(this.defDifficultyIndex);
  }

  public void SelectPrevDefDifficulty()
  {
    if (this.defDifficultyIndex <= 0)
      return;
    --this.defDifficultyIndex;
    this.SelectDefDifficulty(this.defDifficultyIndex);
  }

  private void SelectDefDifficulty(int index)
  {
    this.defDifficultyValue_Txt.text = this.defDifficultyOptions[index];
    this.defDifficultyIndex = index;
    if (index == 0)
      PersistentData.defDifficulty = 10;
    else if (index == 1)
      PersistentData.defDifficulty = 7;
    else if (index == 2)
      PersistentData.defDifficulty = 4;
    else if (index == 3)
      PersistentData.defDifficulty = 1;
    if (index == 0)
      PersistentData.difficulty = 10;
    else if (index == 1)
      PersistentData.difficulty = 7;
    else if (index == 2)
      PersistentData.difficulty = 4;
    else if (index == 3)
      PersistentData.difficulty = 1;
    if (Game.IsSeasonMode)
      PersistentSingleton<SaveManager>.Instance.gameSettings.DefDifficulty = index;
    else
      PersistentSingleton<SaveManager>.Instance.exhibitionSettings.DefDifficulty = index;
  }

  private void SelectStadium()
  {
    PersistentData.stadiumSet = StadiumSetDatabase.GetStadiumData();
    if ((UnityEngine.Object) PersistentData.stadiumSet != (UnityEngine.Object) null)
    {
      this.stadiumValue_Txt.text = PersistentData.stadiumSet.homeTeamStadium.ToUpper();
      this.stadiumPreview_Img.sprite = PersistentData.stadiumSet.previewImage;
      this.stadiumInformation_Txt.text = PersistentData.stadiumSet.stadiumName + "\n" + PersistentData.stadiumSet.stadiumLocation + "\nCAPACITY " + PersistentData.stadiumSet.capacity;
    }
    else
      Debug.LogWarning((object) "CD: Not loading Axis stadium data");
  }

  public void SelectNextStadium()
  {
    StadiumSetDatabase.SetNextStadium();
    this.stadiumHasChanged = true;
    this.SelectStadium();
  }

  public void SelectPrevStadium()
  {
    StadiumSetDatabase.SetPrevStadium();
    this.stadiumHasChanged = true;
    this.SelectStadium();
  }

  public void SelectNextWeather()
  {
    if (this.weatherTypeIndex >= this.weatherTypeOptions.Length - 1)
      return;
    this.SelectWeatherType(this.weatherTypeIndex + 1);
  }

  public void SelectPrevWeather()
  {
    if (this.weatherTypeIndex <= 0)
      return;
    this.SelectWeatherType(this.weatherTypeIndex - 1);
  }

  private void SelectWeatherType(int index)
  {
    this.weatherTypeValue_Txt.text = this.weatherTypeOptions[index];
    PersistentData.weather = index;
    this.weatherTypeIndex = index;
    PersistentSingleton<SaveManager>.Instance.exhibitionSettings.WeatherTypeIndex = index;
  }

  public void SelectNextTimeOfDay()
  {
    if (this.timeOfDayIndex >= this.timeOfDayOptions.Length - 1)
      return;
    this.SelectTimeOfDay(this.timeOfDayIndex + 1);
  }

  public void SelectPrevTimeOfDay()
  {
    if (this.timeOfDayIndex <= 0)
      return;
    this.SelectTimeOfDay(this.timeOfDayIndex - 1);
  }

  private void SelectTimeOfDay(int index)
  {
    this.timeOfDayValue_Txt.text = this.timeOfDayOptions[index];
    PersistentData.timeOfDay = index;
    this.timeOfDayIndex = index;
    PersistentSingleton<SaveManager>.Instance.exhibitionSettings.TimeOfDayIndex = index;
  }

  public void SelectNextWind()
  {
    if (this.windTypeIndex >= this.windTypeOptions.Length - 1)
      return;
    this.SelectWindType(this.windTypeIndex + 1);
  }

  public void SelectPrevWind()
  {
    if (this.windTypeIndex <= 0)
      return;
    this.SelectWindType(this.windTypeIndex - 1);
  }

  private void SelectWindType(int index)
  {
    this.windTypeValue_Txt.text = this.windTypeOptions[index];
    this.windTypeIndex = index;
    PersistentData.windType = index;
    PersistentSingleton<SaveManager>.Instance.exhibitionSettings.WindIndex = index;
  }

  private void ManageControllerSupport()
  {
    if (!ControllerManagerTitle.self.usingController || !this.IsVisible() || !this.allowMove)
      return;
    float h = UserManager.instance.LeftStickX(Player.One);
    float v = UserManager.instance.LeftStickY(Player.One);
    if (this.isMainSectionShowing)
    {
      if (UserManager.instance.RightBumperWasPressed(Player.One))
      {
        this.ShowNextPlayerComparison();
      }
      else
      {
        if (!UserManager.instance.LeftBumperWasPressed(Player.One))
          return;
        this.ShowPrevPlayerComparison();
      }
    }
    else
    {
      if (!this.isGameOptionSelectShowing)
        return;
      if (UserManager.instance.RightBumperWasPressed(Player.One))
        this.SelectNextGameSetupCategory();
      else if (UserManager.instance.LeftBumperWasPressed(Player.One))
        this.SelectPrevGameSetupCategory();
      if (this.gameSettingsSectionIndex == 0)
        this.ManageRulesSelectionControls(h, v);
      else if (this.gameSettingsSectionIndex == 1)
      {
        this.ManageGameplaySelectionControls(h, v);
      }
      else
      {
        if (this.gameSettingsSectionIndex != 2)
          return;
        this.ManageStadiumSelectionControls(h, v);
      }
    }
  }

  private void SelectNextGameSetupCategory()
  {
    if (this.gameSettingsSectionIndex == 0)
      this.ShowGameplaySection();
    else if (this.gameSettingsSectionIndex == 1)
      this.ShowStadiumSection();
    else
      this.ShowRulesSection();
  }

  private void SelectPrevGameSetupCategory()
  {
    if (this.gameSettingsSectionIndex == 0)
      this.ShowStadiumSection();
    else if (this.gameSettingsSectionIndex == 1)
      this.ShowRulesSection();
    else
      this.ShowGameplaySection();
  }

  private void ManageRulesSelectionControls(float h, float v)
  {
    if (this.selectedIndex == 0)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextQuarterLength();
      }
      else if ((double) h < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevQuarterLength();
      }
      if ((double) v >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.HighlightInjuries();
    }
    else if (this.selectedIndex == 1)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextInjury();
      }
      else if ((double) h < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevInjury();
      }
      if ((double) v > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.HighlightQuarterLength();
      }
      else
      {
        if ((double) v >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.HighlightPlayerFatigue();
      }
    }
    else
    {
      if (this.selectedIndex != 2)
        return;
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextPlayerFatigue();
      }
      else if ((double) h < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevPlayerFatigue();
      }
      if ((double) v <= 0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.HighlightInjuries();
    }
  }

  private void HighlightQuarterLength()
  {
    this.selectedIndex = 0;
    this.quarterLength_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.injuries_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.playerFatigue_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void HighlightInjuries()
  {
    this.selectedIndex = 1;
    this.quarterLength_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.injuries_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.playerFatigue_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void HighlightPlayerFatigue()
  {
    this.selectedIndex = 2;
    this.quarterLength_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.injuries_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.playerFatigue_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  private void ManageGameplaySelectionControls(float h, float v)
  {
    if (this.selectedIndex == 0)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextOffDifficulty();
      }
      else if ((double) h < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevOffDifficulty();
      }
      if ((double) v >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.HighlightDeffensiveDifficulty();
    }
    else
    {
      if (this.selectedIndex != 1)
        return;
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextDefDifficulty();
      }
      else if ((double) h < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevDefDifficulty();
      }
      if ((double) v <= 0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.HighlightOffensiveDifficulty();
    }
  }

  private void HighlightOffensiveDifficulty()
  {
    this.selectedIndex = 0;
    this.offDifficulty_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.defDifficulty_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void HighlightDeffensiveDifficulty()
  {
    this.selectedIndex = 1;
    this.offDifficulty_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.defDifficulty_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  private void ManageStadiumSelectionControls(float h, float v)
  {
    if (this.selectedIndex == 0)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextStadium();
      }
      else if ((double) h < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevStadium();
      }
      if ((double) v >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.HighlightForecast();
    }
    else if (this.selectedIndex == 1)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextWeather();
      }
      else if ((double) h < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevWeather();
      }
      if ((double) v < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.HighlightTimeOfDay();
      }
      else
      {
        if ((double) v <= 0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.HighlightStadiumSelect();
      }
    }
    else if (this.selectedIndex == 2)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextTimeOfDay();
      }
      else if ((double) h < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevTimeOfDay();
      }
      if ((double) v > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.HighlightForecast();
      }
      else
      {
        if ((double) v >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.HighlightWind();
      }
    }
    else
    {
      if (this.selectedIndex != 3)
        return;
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextWind();
      }
      else if ((double) h < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevWind();
      }
      if ((double) v <= 0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.HighlightTimeOfDay();
    }
  }

  private void HighlightStadiumSelect()
  {
    this.selectedIndex = 0;
    this.stadium_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.forecast_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.timeOfDay_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.wind_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void HighlightForecast()
  {
    this.selectedIndex = 1;
    this.stadium_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.forecast_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.timeOfDay_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.wind_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void HighlightTimeOfDay()
  {
    this.selectedIndex = 2;
    this.stadium_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.forecast_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.timeOfDay_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    this.wind_Ani.SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void HighlightWind()
  {
    this.selectedIndex = 3;
    this.stadium_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.forecast_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.timeOfDay_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.wind_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  private IEnumerator DisableMove()
  {
    this.allowMove = false;
    yield return (object) this._disableMove;
    this.allowMove = true;
  }

  public void ReturnToPreviousMenu()
  {
    UISoundManager.instance.PlayButtonBack();
    ControllerManagerTitle.self.DeselectCurrentUIElement();
    if (!this.isMainSectionShowing)
      this.ShowMainSection();
    else if (PersistentData.gameType == GameType.SeasonMode)
    {
      this.HideWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.ReturnToFranchiseMode();
    }
    else
    {
      this.HideWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSelect.ShowWindow();
    }
  }

  private void ClearDataBeforeMatch()
  {
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.UnloadAssetBundle("custom_endzones", true);
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.UnloadAssetBundle("fieldtextures", true);
    if (SingletonBehaviour<AssetManager, SerializedMonoBehaviour>.Exists())
    {
      AssetManager.ClearUnusedLargeTeamLogos(this.awayTeamData.TeamIndex, this.homeTeamData.TeamIndex);
      AssetManager.ClearUnusedMediumTeamLogos(this.awayTeamData.TeamIndex, this.homeTeamData.TeamIndex);
      AssetManager.ClearUnusedSmallTeamLogos(this.awayTeamData.TeamIndex, this.homeTeamData.TeamIndex);
      AssetManager.ClearUnusedTinyTeamLogos(this.awayTeamData.TeamIndex, this.homeTeamData.TeamIndex);
    }
    if (!SingletonBehaviour<PersistentData, MonoBehaviour>.Exists())
      return;
    if (PersistentData.homeTeamUniform != null)
      PersistentData.homeTeamUniform.ClearNonLockedTextures();
    if (PersistentData.awayTeamUniform == null)
      return;
    PersistentData.awayTeamUniform.ClearNonLockedTextures();
  }
}
