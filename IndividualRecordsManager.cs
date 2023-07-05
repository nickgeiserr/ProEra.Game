// Decompiled with JetBrains decompiler
// Type: IndividualRecordsManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using UnityEngine;

public class IndividualRecordsManager : MonoBehaviour
{
  [Header("Main")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private GameObject activeIndicator_GO;
  [SerializeField]
  private RectTransform activeIndicator_Trans;
  [SerializeField]
  private GameObject tabsContainer_GO;
  [SerializeField]
  private RectTransform tabsContainer_Trans;
  [SerializeField]
  private RectTransform individualGameButton_Trans;
  [SerializeField]
  private RectTransform individualSeasonButton_Trans;
  [SerializeField]
  private RectTransform individualCareerButton_Trans;
  [SerializeField]
  private UnityEngine.UI.Button individualGame_Btn;
  [SerializeField]
  private UnityEngine.UI.Button individualSeason_Btn;
  [SerializeField]
  private UnityEngine.UI.Button individualCareer_Btn;
  [Header("Individual Game")]
  [SerializeField]
  private IndividualRecordItem[] individualGameRecordItems_Off;
  [SerializeField]
  private IndividualRecordItem[] individualGameRecordItems_Def;
  [SerializeField]
  private IndividualRecordItem[] individualGameRecordItems_Spc;
  [SerializeField]
  private RectTransform scrollContents_IndividualGameRecords;
  [Header("Individual Season")]
  [SerializeField]
  private IndividualRecordItem[] individualSeasonRecordItems_Off;
  [SerializeField]
  private IndividualRecordItem[] individualSeasonRecordItems_Def;
  [SerializeField]
  private IndividualRecordItem[] individualSeasonRecordItems_Spc;
  [SerializeField]
  private RectTransform scrollContents_IndividualSeasonRecords;
  [Header("Individual Career")]
  [SerializeField]
  private IndividualCareerRecordItem[] individualCareerRecordItems_Off;
  [SerializeField]
  private IndividualCareerRecordItem[] individualCareerRecordItems_Def;
  [SerializeField]
  private IndividualCareerRecordItem[] individualCareerRecordItems_Spc;
  [SerializeField]
  private RectTransform scrollContents_IndividualCareerRecords;
  private int tabSectionIndex;
  private float scrollAmount;
  private float activeIndicatorAnimationSpeed = 0.15f;
  private SGD_SeasonModeData seasonModeData;

  private void Start()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void Init()
  {
    this.seasonModeData = SeasonModeManager.self.seasonModeData;
    this.scrollAmount = 300f;
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    BottomBarManager.instance.SetControllerButtonGuide(21);
    this.ShowIndividualGameRecordsTab();
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.15f);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu() => this.HideWindow();

  public void ShowIndividualGameRecordsTab()
  {
    this.scrollContents_IndividualGameRecords.anchoredPosition = Vector2.zero;
    UISoundManager.instance.PlayTabSwipe();
    this.tabSectionIndex = 0;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.individualGameButton_Trans.localPosition.x, this.activeIndicatorAnimationSpeed);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.individualGameButton_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed);
    LeanTween.moveLocalX(this.tabsContainer_GO, (float) this.tabSectionIndex * -1f * this.tabsContainer_Trans.rect.width, this.activeIndicatorAnimationSpeed);
    this.individualGame_Btn.interactable = false;
    this.individualSeason_Btn.interactable = true;
    this.individualCareer_Btn.interactable = true;
    this.SetIndividualGameRecordsData();
  }

  public void ShowIndividualSeasonRecordsTab()
  {
    this.scrollContents_IndividualSeasonRecords.anchoredPosition = Vector2.zero;
    UISoundManager.instance.PlayTabSwipe();
    this.tabSectionIndex = 1;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.individualSeasonButton_Trans.localPosition.x, this.activeIndicatorAnimationSpeed);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.individualSeasonButton_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed);
    LeanTween.moveLocalX(this.tabsContainer_GO, (float) this.tabSectionIndex * -1f * this.tabsContainer_Trans.rect.width, this.activeIndicatorAnimationSpeed);
    this.individualGame_Btn.interactable = true;
    this.individualSeason_Btn.interactable = false;
    this.individualCareer_Btn.interactable = true;
    this.SetIndividualSeasonRecordsData();
  }

  public void ShowIndividualCareerRecordsTab()
  {
    this.scrollContents_IndividualCareerRecords.anchoredPosition = Vector2.zero;
    UISoundManager.instance.PlayTabSwipe();
    this.tabSectionIndex = 2;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.individualCareerButton_Trans.localPosition.x, this.activeIndicatorAnimationSpeed);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.individualCareerButton_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed);
    LeanTween.moveLocalX(this.tabsContainer_GO, (float) this.tabSectionIndex * -1f * this.tabsContainer_Trans.rect.width, this.activeIndicatorAnimationSpeed);
    this.individualGame_Btn.interactable = true;
    this.individualSeason_Btn.interactable = true;
    this.individualCareer_Btn.interactable = false;
    this.SetIndividualCareerRecordsData();
  }

  private bool IsIndividualGameRecordsTabVisible() => this.tabSectionIndex == 0;

  private bool IsIndividualSeasonRecordsTabVisible() => this.tabSectionIndex == 1;

  private bool IsIndividualCareerRecordsTabVisible() => this.tabSectionIndex == 2;

  private void SelectNextTab()
  {
    if (this.tabSectionIndex == 0)
    {
      this.ShowIndividualSeasonRecordsTab();
    }
    else
    {
      if (this.tabSectionIndex != 1)
        return;
      this.ShowIndividualCareerRecordsTab();
    }
  }

  private void SelectPreviousTab()
  {
    if (this.tabSectionIndex == 2)
    {
      this.ShowIndividualSeasonRecordsTab();
    }
    else
    {
      if (this.tabSectionIndex != 1)
        return;
      this.ShowIndividualGameRecordsTab();
    }
  }

  private void SetIndividualGameRecordsData()
  {
    this.individualGameRecordItems_Off[0].SetData(this.seasonModeData.IndividualGameRecords_League.Completions);
    this.individualGameRecordItems_Off[1].SetData(this.seasonModeData.IndividualGameRecords_League.PassAttempts);
    this.individualGameRecordItems_Off[2].SetData(this.seasonModeData.IndividualGameRecords_League.PassYards);
    this.individualGameRecordItems_Off[3].SetData(this.seasonModeData.IndividualGameRecords_League.PassTDs);
    this.individualGameRecordItems_Off[4].SetData(this.seasonModeData.IndividualGameRecords_League.ThrownInts);
    this.individualGameRecordItems_Off[5].SetData(this.seasonModeData.IndividualGameRecords_League.RushAttempts);
    this.individualGameRecordItems_Off[6].SetData(this.seasonModeData.IndividualGameRecords_League.RushYards);
    this.individualGameRecordItems_Off[7].SetData(this.seasonModeData.IndividualGameRecords_League.RushTDs);
    this.individualGameRecordItems_Off[8].SetData(this.seasonModeData.IndividualGameRecords_League.Fumbles);
    this.individualGameRecordItems_Off[9].SetData(this.seasonModeData.IndividualGameRecords_League.Receptions);
    this.individualGameRecordItems_Off[10].SetData(this.seasonModeData.IndividualGameRecords_League.ReceivingYards);
    this.individualGameRecordItems_Off[11].SetData(this.seasonModeData.IndividualGameRecords_League.ReceivingTDs);
    this.individualGameRecordItems_Off[12].SetData(this.seasonModeData.IndividualGameRecords_League.YardsAfterCatch);
    this.individualGameRecordItems_Off[13].SetData(this.seasonModeData.IndividualGameRecords_League.Drops);
    this.individualGameRecordItems_Off[14].SetData(this.seasonModeData.IndividualGameRecords_League.Targets);
    this.individualGameRecordItems_Off[15].SetData(this.seasonModeData.IndividualGameRecords_League.TotalTDs);
    this.individualGameRecordItems_Def[0].SetData(this.seasonModeData.IndividualGameRecords_League.Tackles);
    this.individualGameRecordItems_Def[1].SetData(this.seasonModeData.IndividualGameRecords_League.Sacks);
    this.individualGameRecordItems_Def[2].SetData(this.seasonModeData.IndividualGameRecords_League.Interceptions);
    this.individualGameRecordItems_Def[3].SetData(this.seasonModeData.IndividualGameRecords_League.TacklesForLoss);
    this.individualGameRecordItems_Def[4].SetData(this.seasonModeData.IndividualGameRecords_League.DefensiveTDs);
    this.individualGameRecordItems_Def[5].SetData(this.seasonModeData.IndividualGameRecords_League.KnockDowns);
    this.individualGameRecordItems_Def[6].SetData(this.seasonModeData.IndividualGameRecords_League.ForcedFumbles);
    this.individualGameRecordItems_Def[7].SetData(this.seasonModeData.IndividualGameRecords_League.FumbleRecoveries);
    this.individualGameRecordItems_Spc[0].SetData(this.seasonModeData.IndividualGameRecords_League.FGMade);
    this.individualGameRecordItems_Spc[1].SetData(this.seasonModeData.IndividualGameRecords_League.FGAttempted);
    this.individualGameRecordItems_Spc[2].SetData(this.seasonModeData.IndividualGameRecords_League.Punts);
    this.individualGameRecordItems_Spc[3].SetData(this.seasonModeData.IndividualGameRecords_League.PuntReturnYards);
    this.individualGameRecordItems_Spc[4].SetData(this.seasonModeData.IndividualGameRecords_League.KickReturnYards);
  }

  private void SetIndividualSeasonRecordsData()
  {
    this.individualSeasonRecordItems_Off[0].SetData(this.seasonModeData.IndividualSeasonRecords_League.QBRating);
    this.individualSeasonRecordItems_Off[0].SetToDecimalDisplay();
    this.individualSeasonRecordItems_Off[1].SetData(this.seasonModeData.IndividualSeasonRecords_League.Completions);
    this.individualSeasonRecordItems_Off[2].SetData(this.seasonModeData.IndividualSeasonRecords_League.PassAttempts);
    this.individualSeasonRecordItems_Off[3].SetData(this.seasonModeData.IndividualSeasonRecords_League.CompletionPercentage);
    this.individualSeasonRecordItems_Off[3].SetToPercentageDisplay();
    this.individualSeasonRecordItems_Off[4].SetData(this.seasonModeData.IndividualSeasonRecords_League.PassYards);
    this.individualSeasonRecordItems_Off[5].SetData(this.seasonModeData.IndividualSeasonRecords_League.PassTDs);
    this.individualSeasonRecordItems_Off[6].SetData(this.seasonModeData.IndividualSeasonRecords_League.ThrownInts);
    this.individualSeasonRecordItems_Off[7].SetData(this.seasonModeData.IndividualSeasonRecords_League.YardsPerPass);
    this.individualSeasonRecordItems_Off[7].SetToDecimalDisplay();
    this.individualSeasonRecordItems_Off[8].SetData(this.seasonModeData.IndividualSeasonRecords_League.RushAttempts);
    this.individualSeasonRecordItems_Off[9].SetData(this.seasonModeData.IndividualSeasonRecords_League.RushYards);
    this.individualSeasonRecordItems_Off[10].SetData(this.seasonModeData.IndividualSeasonRecords_League.RushTDs);
    this.individualSeasonRecordItems_Off[11].SetData(this.seasonModeData.IndividualSeasonRecords_League.YardsPerRush);
    this.individualSeasonRecordItems_Off[11].SetToDecimalDisplay();
    this.individualSeasonRecordItems_Off[12].SetData(this.seasonModeData.IndividualSeasonRecords_League.Fumbles);
    this.individualSeasonRecordItems_Off[13].SetData(this.seasonModeData.IndividualSeasonRecords_League.Receptions);
    this.individualSeasonRecordItems_Off[14].SetData(this.seasonModeData.IndividualSeasonRecords_League.ReceivingYards);
    this.individualSeasonRecordItems_Off[15].SetData(this.seasonModeData.IndividualSeasonRecords_League.ReceivingTDs);
    this.individualSeasonRecordItems_Off[16].SetData(this.seasonModeData.IndividualSeasonRecords_League.YardsPerCatch);
    this.individualSeasonRecordItems_Off[16].SetToDecimalDisplay();
    this.individualSeasonRecordItems_Off[17].SetData(this.seasonModeData.IndividualSeasonRecords_League.YardsAfterCatch);
    this.individualSeasonRecordItems_Off[18].SetData(this.seasonModeData.IndividualSeasonRecords_League.Drops);
    this.individualSeasonRecordItems_Off[19].SetData(this.seasonModeData.IndividualSeasonRecords_League.Targets);
    this.individualSeasonRecordItems_Off[20].SetData(this.seasonModeData.IndividualSeasonRecords_League.TotalTDs);
    this.individualSeasonRecordItems_Def[0].SetData(this.seasonModeData.IndividualSeasonRecords_League.Tackles);
    this.individualSeasonRecordItems_Def[1].SetData(this.seasonModeData.IndividualSeasonRecords_League.Sacks);
    this.individualSeasonRecordItems_Def[2].SetData(this.seasonModeData.IndividualSeasonRecords_League.Interceptions);
    this.individualSeasonRecordItems_Def[3].SetData(this.seasonModeData.IndividualSeasonRecords_League.TacklesForLoss);
    this.individualSeasonRecordItems_Def[4].SetData(this.seasonModeData.IndividualSeasonRecords_League.DefensiveTDs);
    this.individualSeasonRecordItems_Def[5].SetData(this.seasonModeData.IndividualSeasonRecords_League.KnockDowns);
    this.individualSeasonRecordItems_Def[6].SetData(this.seasonModeData.IndividualSeasonRecords_League.ForcedFumbles);
    this.individualSeasonRecordItems_Def[7].SetData(this.seasonModeData.IndividualSeasonRecords_League.FumbleRecoveries);
    this.individualSeasonRecordItems_Spc[0].SetData(this.seasonModeData.IndividualSeasonRecords_League.FGMade);
    this.individualSeasonRecordItems_Spc[1].SetData(this.seasonModeData.IndividualSeasonRecords_League.FGAttempted);
    this.individualSeasonRecordItems_Spc[2].SetData(this.seasonModeData.IndividualSeasonRecords_League.XPMade);
    this.individualSeasonRecordItems_Spc[3].SetData(this.seasonModeData.IndividualSeasonRecords_League.XPAttempted);
    this.individualSeasonRecordItems_Spc[4].SetData(this.seasonModeData.IndividualSeasonRecords_League.Punts);
    this.individualSeasonRecordItems_Spc[5].SetData(this.seasonModeData.IndividualSeasonRecords_League.PuntsInside20);
    this.individualSeasonRecordItems_Spc[6].SetData(this.seasonModeData.IndividualSeasonRecords_League.PuntTouchbacks);
    this.individualSeasonRecordItems_Spc[7].SetData(this.seasonModeData.IndividualSeasonRecords_League.YardsPerPunt);
    this.individualSeasonRecordItems_Spc[7].SetToDecimalDisplay();
    this.individualSeasonRecordItems_Spc[8].SetData(this.seasonModeData.IndividualSeasonRecords_League.PuntReturns);
    this.individualSeasonRecordItems_Spc[9].SetData(this.seasonModeData.IndividualSeasonRecords_League.PuntReturnYards);
    this.individualSeasonRecordItems_Spc[10].SetData(this.seasonModeData.IndividualSeasonRecords_League.YardsPerPuntReturn);
    this.individualSeasonRecordItems_Spc[10].SetToDecimalDisplay();
    this.individualSeasonRecordItems_Spc[11].SetData(this.seasonModeData.IndividualSeasonRecords_League.PuntReturnTDs);
    this.individualSeasonRecordItems_Spc[12].SetData(this.seasonModeData.IndividualSeasonRecords_League.KickReturns);
    this.individualSeasonRecordItems_Spc[13].SetData(this.seasonModeData.IndividualSeasonRecords_League.KickReturnYards);
    this.individualSeasonRecordItems_Spc[14].SetData(this.seasonModeData.IndividualSeasonRecords_League.YardsPerKickReturn);
    this.individualSeasonRecordItems_Spc[14].SetToDecimalDisplay();
    this.individualSeasonRecordItems_Spc[15].SetData(this.seasonModeData.IndividualSeasonRecords_League.KickReturnTDs);
  }

  private void SetIndividualCareerRecordsData()
  {
    this.individualCareerRecordItems_Off[0].SetData(this.seasonModeData.IndividualCareerRecords_League.QBRating);
    this.individualCareerRecordItems_Off[0].SetToDecimalDisplay();
    this.individualCareerRecordItems_Off[1].SetData(this.seasonModeData.IndividualCareerRecords_League.Completions);
    this.individualCareerRecordItems_Off[2].SetData(this.seasonModeData.IndividualCareerRecords_League.PassAttempts);
    this.individualCareerRecordItems_Off[3].SetData(this.seasonModeData.IndividualCareerRecords_League.CompletionPercentage);
    this.individualCareerRecordItems_Off[3].SetToPercentageDisplay();
    this.individualCareerRecordItems_Off[4].SetData(this.seasonModeData.IndividualCareerRecords_League.PassYards);
    this.individualCareerRecordItems_Off[5].SetData(this.seasonModeData.IndividualCareerRecords_League.PassTDs);
    this.individualCareerRecordItems_Off[6].SetData(this.seasonModeData.IndividualCareerRecords_League.ThrownInts);
    this.individualCareerRecordItems_Off[7].SetData(this.seasonModeData.IndividualCareerRecords_League.YardsPerPass);
    this.individualCareerRecordItems_Off[7].SetToDecimalDisplay();
    this.individualCareerRecordItems_Off[8].SetData(this.seasonModeData.IndividualCareerRecords_League.LongestPass);
    this.individualCareerRecordItems_Off[9].SetData(this.seasonModeData.IndividualCareerRecords_League.RushAttempts);
    this.individualCareerRecordItems_Off[10].SetData(this.seasonModeData.IndividualCareerRecords_League.RushYards);
    this.individualCareerRecordItems_Off[11].SetData(this.seasonModeData.IndividualCareerRecords_League.RushTDs);
    this.individualCareerRecordItems_Off[12].SetData(this.seasonModeData.IndividualCareerRecords_League.YardsPerRush);
    this.individualCareerRecordItems_Off[12].SetToDecimalDisplay();
    this.individualCareerRecordItems_Off[13].SetData(this.seasonModeData.IndividualCareerRecords_League.LongestRush);
    this.individualCareerRecordItems_Off[14].SetData(this.seasonModeData.IndividualCareerRecords_League.Fumbles);
    this.individualCareerRecordItems_Off[15].SetData(this.seasonModeData.IndividualCareerRecords_League.Receptions);
    this.individualCareerRecordItems_Off[16].SetData(this.seasonModeData.IndividualCareerRecords_League.ReceivingYards);
    this.individualCareerRecordItems_Off[17].SetData(this.seasonModeData.IndividualCareerRecords_League.ReceivingTDs);
    this.individualCareerRecordItems_Off[18].SetData(this.seasonModeData.IndividualCareerRecords_League.YardsPerCatch);
    this.individualCareerRecordItems_Off[18].SetToDecimalDisplay();
    this.individualCareerRecordItems_Off[19].SetData(this.seasonModeData.IndividualCareerRecords_League.LongestReception);
    this.individualCareerRecordItems_Off[20].SetData(this.seasonModeData.IndividualCareerRecords_League.YardsAfterCatch);
    this.individualCareerRecordItems_Off[21].SetData(this.seasonModeData.IndividualCareerRecords_League.Drops);
    this.individualCareerRecordItems_Off[22].SetData(this.seasonModeData.IndividualCareerRecords_League.Targets);
    this.individualCareerRecordItems_Off[23].SetData(this.seasonModeData.IndividualCareerRecords_League.TotalTDs);
    this.individualCareerRecordItems_Def[0].SetData(this.seasonModeData.IndividualCareerRecords_League.Tackles);
    this.individualCareerRecordItems_Def[1].SetData(this.seasonModeData.IndividualCareerRecords_League.Sacks);
    this.individualCareerRecordItems_Def[2].SetData(this.seasonModeData.IndividualCareerRecords_League.Interceptions);
    this.individualCareerRecordItems_Def[3].SetData(this.seasonModeData.IndividualCareerRecords_League.TacklesForLoss);
    this.individualCareerRecordItems_Def[4].SetData(this.seasonModeData.IndividualCareerRecords_League.DefensiveTDs);
    this.individualCareerRecordItems_Def[5].SetData(this.seasonModeData.IndividualCareerRecords_League.KnockDowns);
    this.individualCareerRecordItems_Def[6].SetData(this.seasonModeData.IndividualCareerRecords_League.ForcedFumbles);
    this.individualCareerRecordItems_Def[7].SetData(this.seasonModeData.IndividualCareerRecords_League.FumbleRecoveries);
    this.individualCareerRecordItems_Spc[0].SetData(this.seasonModeData.IndividualCareerRecords_League.FGMade);
    this.individualCareerRecordItems_Spc[1].SetData(this.seasonModeData.IndividualCareerRecords_League.FGAttempted);
    this.individualCareerRecordItems_Spc[2].SetData(this.seasonModeData.IndividualCareerRecords_League.XPMade);
    this.individualCareerRecordItems_Spc[3].SetData(this.seasonModeData.IndividualCareerRecords_League.XPAttempted);
    this.individualCareerRecordItems_Spc[4].SetData(this.seasonModeData.IndividualCareerRecords_League.Punts);
    this.individualCareerRecordItems_Spc[5].SetData(this.seasonModeData.IndividualCareerRecords_League.PuntsInside20);
    this.individualCareerRecordItems_Spc[6].SetData(this.seasonModeData.IndividualCareerRecords_League.PuntTouchbacks);
    this.individualCareerRecordItems_Spc[7].SetData(this.seasonModeData.IndividualCareerRecords_League.YardsPerPunt);
    this.individualCareerRecordItems_Spc[7].SetToDecimalDisplay();
    this.individualCareerRecordItems_Spc[8].SetData(this.seasonModeData.IndividualCareerRecords_League.PuntReturns);
    this.individualCareerRecordItems_Spc[9].SetData(this.seasonModeData.IndividualCareerRecords_League.PuntReturnYards);
    this.individualCareerRecordItems_Spc[10].SetData(this.seasonModeData.IndividualCareerRecords_League.YardsPerPuntReturn);
    this.individualCareerRecordItems_Spc[10].SetToDecimalDisplay();
    this.individualCareerRecordItems_Spc[11].SetData(this.seasonModeData.IndividualCareerRecords_League.PuntReturnTDs);
    this.individualCareerRecordItems_Spc[12].SetData(this.seasonModeData.IndividualCareerRecords_League.KickReturns);
    this.individualCareerRecordItems_Spc[13].SetData(this.seasonModeData.IndividualCareerRecords_League.KickReturnYards);
    this.individualCareerRecordItems_Spc[14].SetData(this.seasonModeData.IndividualCareerRecords_League.YardsPerKickReturn);
    this.individualCareerRecordItems_Spc[14].SetToDecimalDisplay();
    this.individualCareerRecordItems_Spc[15].SetData(this.seasonModeData.IndividualCareerRecords_League.KickReturnTDs);
  }

  public void CheckForBrokenRecords()
  {
    this.seasonModeData.IndividualGameRecords_League.CheckForBrokenRecords_IndividualGameLeague();
    this.seasonModeData.IndividualSeasonRecords_League.CheckForBrokenRecords_IndividualSeasonLeague();
    this.seasonModeData.IndividualCareerRecords_League.CheckForBrokenRecords_IndividualCareerLeague();
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || !ControllerManagerTitle.self.usingController)
      return;
    if (UserManager.instance.RightBumperWasPressed(Player.One))
      this.SelectNextTab();
    else if (UserManager.instance.LeftBumperWasPressed(Player.One))
      this.SelectPreviousTab();
    float num1 = UserManager.instance.RightStickY(Player.One);
    float num2 = UserManager.instance.LeftStickY(Player.One);
    if (this.IsIndividualGameRecordsTabVisible())
    {
      if ((double) num1 > 0.40000000596046448 || (double) num2 > 0.40000000596046448)
      {
        this.scrollContents_IndividualGameRecords.anchoredPosition -= Vector2.up * this.scrollAmount * Time.unscaledDeltaTime;
      }
      else
      {
        if ((double) num1 >= -0.40000000596046448 && (double) num2 >= -0.40000000596046448)
          return;
        this.scrollContents_IndividualGameRecords.anchoredPosition += Vector2.up * this.scrollAmount * Time.unscaledDeltaTime;
      }
    }
    else if (this.IsIndividualSeasonRecordsTabVisible())
    {
      if ((double) num1 > 0.40000000596046448 || (double) num2 > 0.40000000596046448)
      {
        this.scrollContents_IndividualSeasonRecords.anchoredPosition -= Vector2.up * this.scrollAmount * Time.unscaledDeltaTime;
      }
      else
      {
        if ((double) num1 >= -0.40000000596046448 && (double) num2 >= -0.40000000596046448)
          return;
        this.scrollContents_IndividualSeasonRecords.anchoredPosition += Vector2.up * this.scrollAmount * Time.unscaledDeltaTime;
      }
    }
    else
    {
      if (!this.IsIndividualCareerRecordsTabVisible())
        return;
      if ((double) num1 > 0.40000000596046448 || (double) num2 > 0.40000000596046448)
      {
        this.scrollContents_IndividualCareerRecords.anchoredPosition -= Vector2.up * this.scrollAmount * Time.unscaledDeltaTime;
      }
      else
      {
        if ((double) num1 >= -0.40000000596046448 && (double) num2 >= -0.40000000596046448)
          return;
        this.scrollContents_IndividualCareerRecords.anchoredPosition += Vector2.up * this.scrollAmount * Time.unscaledDeltaTime;
      }
    }
  }
}
