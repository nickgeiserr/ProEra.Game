// Decompiled with JetBrains decompiler
// Type: PlaybookManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using FootballVR;
using ProEra.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class PlaybookManager : MonoBehaviour, IPlaybookGUI
{
  [Header("Main")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private Image teamLogo_Img;
  [SerializeField]
  private Image teamBackground_Img;
  [SerializeField]
  private Transform trans;
  [SerializeField]
  private TextMeshProUGUI titleText_Txt;
  private List<FormationData> offPlaybook;
  private List<FormationData> defPlaybook;
  [SerializeField]
  private Player player;
  private int page;
  private int playsInFormation;
  private int pagesInFormation;
  private static int playsPerPage = 3;
  private bool checkForStickInput;
  private bool checkForButtonInput;
  private TeamData teamData;
  public static bool wasGameClockRunningBefore;
  [Header("Formation Selection")]
  [SerializeField]
  private CanvasGroup formationSelectWindow_CG;
  [SerializeField]
  private PlaybookFormationItem[] playbookFormationList;
  [SerializeField]
  private PlaybookFormationIcon[] playbookFormationIcons;
  [SerializeField]
  private Scrollbar formationScrollbar;
  [SerializeField]
  private TextMeshProUGUI playsInFormation_Txt;
  [HideInInspector]
  public int currentFormation;
  [HideInInspector]
  public bool onOffense;
  [HideInInspector]
  public FormationData lastFormationUsed;
  [HideInInspector]
  public int lastFormationIndex;
  private List<FormationData> baseFormationsList;
  private List<int> subFormationCount;
  private List<int> baseFormIndexInPlaybook;
  private List<int> subFormationIndex;
  private FormationData formation;
  private int savedOffForm;
  private int formationListStartIndex;
  private int formationDisplayIndex;
  private int formationSlotIndex;
  private int baseFormationIndex;
  private int numOfBaseFormations_Off;
  private int numOfBaseFormations_Def;
  private static int numberOfFormationSlots = 5;
  [Header("Play Selection")]
  [SerializeField]
  private CanvasGroup playSelectWindow_CG;
  [SerializeField]
  private GameObject flipPlayBtn_GO;
  [SerializeField]
  private GameObject returnToFormationBtn_GO;
  [SerializeField]
  private GameObject subsBtn_GO;
  [SerializeField]
  private PlayGraphicManager[] playGraphicManagers;
  [SerializeField]
  private Scrollbar playSelectScrollbar;
  [HideInInspector]
  public bool playFlipped;
  private bool audible;
  private WaitForSecondsRealtime inputDelay_WFS;
  private string _formationText;
  private float[] xLoc;
  private float[] zLoc;
  private int[] playersInForm;
  private string[] _playNames;

  private void Start()
  {
    this.HideWindow();
    this.HideFormationSelectWindow();
    this.HidePlaySelectWindow();
  }

  public void Init(Player p)
  {
    this.player = p;
    this.playFlipped = false;
    this.savedOffForm = 3;
    this.checkForStickInput = this.checkForButtonInput = true;
    if (p == Player.One)
    {
      this.offPlaybook = Plays.self.offPlaybookP1;
      this.defPlaybook = Plays.self.defPlaybookP1;
      this.teamData = PersistentData.GetUserTeam();
    }
    else
    {
      this.offPlaybook = Plays.self.offPlaybookP2;
      this.defPlaybook = Plays.self.defPlaybookP2;
      this.teamData = PersistentData.GetCompTeam();
    }
    this.numOfBaseFormations_Off = 0;
    this.numOfBaseFormations_Def = this.defPlaybook.Count;
    this.baseFormationsList = new List<FormationData>();
    this.baseFormIndexInPlaybook = new List<int>();
    this.subFormationCount = new List<int>();
    this.subFormationIndex = new List<int>();
    this.teamLogo_Img.sprite = this.teamData.GetSmallLogo();
    this.teamBackground_Img.color = this.teamData.GetPrimaryColor();
    int num;
    for (int index1 = 0; index1 < this.offPlaybook.Count; index1 += num)
    {
      BaseFormation baseFormation = this.offPlaybook[index1].GetFormationPositions().GetBaseFormation();
      this.baseFormationsList.Add(this.offPlaybook[index1]);
      this.baseFormIndexInPlaybook.Add(index1);
      this.subFormationIndex.Add(0);
      ++this.numOfBaseFormations_Off;
      num = 1;
      for (int index2 = index1 + 1; index2 < this.offPlaybook.Count && baseFormation == this.offPlaybook[index2].GetFormationPositions().GetBaseFormation(); ++index2)
        ++num;
      this.subFormationCount.Add(num);
    }
    if (this.numOfBaseFormations_Off < 5)
      Debug.Log((object) "Offensive Playbook does not have the minimum number of base formations");
    this.lastFormationUsed = this.offPlaybook[0];
    this.lastFormationIndex = 0;
    this.formationDisplayIndex = 0;
    this.formationListStartIndex = 0;
    this.inputDelay_WFS = new WaitForSecondsRealtime(0.2f);
  }

  private void Update() => this.ManageControllerInput();

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f).setIgnoreTimeScale(true);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void ShowWindow()
  {
    if (!global::Game.UserCallsPlays || global::Game.IsNotRunningPAT && !MatchManager.instance.allowPATAfterTimeHasExpired)
      return;
    ControllerManagerGame.self.DeselectCurrentUIElement();
    if (!this.IsAudible())
    {
      MatchManager.instance.timeManager.ResetPlayClock();
      this.DetermineIfClockShouldStart_OnShowPlaybook();
    }
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetDownMarkerTexture();
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f).setIgnoreTimeScale(true);
    this.mainWindow_CG.blocksRaycasts = true;
    this.ShowFormationSelectWindow();
    this.UnFlipPlays();
    if (this.formation == Plays.self.kickoffPlays)
      this.ForceOffFormation(FormationType.Kickoff);
    else if (this.formation == Plays.self.kickReturnPlays)
      this.ForceDefFormation(FormationType.KickReturn);
    MatchManager.instance.SaveMatchStateBeforePlay();
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  private void SetFormationNames()
  {
    if (this.onOffense)
    {
      for (int index = 0; index < PlaybookManager.numberOfFormationSlots; ++index)
        this.playbookFormationList[index].formationName.text = this.baseFormationsList[index + this.formationListStartIndex].GetName();
    }
    else
    {
      for (int index = 0; index < PlaybookManager.numberOfFormationSlots; ++index)
        this.playbookFormationList[index].formationName.text = this.defPlaybook[index + this.formationListStartIndex].GetName();
    }
  }

  public void ShowFormationSelectWindow()
  {
    if (!(bool) VRState.PauseMenu.GetValue())
      ControllerManagerGame.self.DeselectCurrentUIElement();
    this.HidePlaySelectWindow();
    this.titleText_Txt.text = "SELECT A FORMATION";
    this.SetFormationNames();
    if (this.onOffense)
    {
      this.ShowOffFormation(this.baseFormationIndex);
      this.formationScrollbar.size = this.GetFormationSelectScrollbarSize();
    }
    else
    {
      this.ShowDefFormation(this.baseFormationIndex);
      this.formationScrollbar.size = this.GetFormationSelectScrollbarSize();
    }
    LeanTween.alphaCanvas(this.formationSelectWindow_CG, 1f, 0.3f).setIgnoreTimeScale(true);
    this.formationSelectWindow_CG.blocksRaycasts = true;
  }

  public void HideFormationSelectWindow()
  {
    LeanTween.alphaCanvas(this.formationSelectWindow_CG, 0.0f, 0.3f).setIgnoreTimeScale(true);
    this.formationSelectWindow_CG.blocksRaycasts = false;
  }

  public bool IsFormationSelectVisible() => (double) this.formationSelectWindow_CG.alpha == 1.0;

  public void ShowFormation(int i)
  {
    this.formationSlotIndex = i;
    if (this.onOffense)
      this.ShowOffFormation(i + this.formationListStartIndex);
    else
      this.ShowDefFormation(i + this.formationListStartIndex);
  }

  public void ShowOffFormation(int i)
  {
    int index = this.baseFormIndexInPlaybook[i] + this.subFormationIndex[i];
    if (this.subFormationCount[i] == 1)
    {
      this.playbookFormationList[i - this.formationListStartIndex].subFormationSelection.SetActive(false);
    }
    else
    {
      this.playbookFormationList[i - this.formationListStartIndex].subFormationSelection.SetActive(true);
      this.playbookFormationList[i - this.formationListStartIndex].subFormationName.text = this.offPlaybook[index].GetSubFormationName() + " (" + (this.subFormationIndex[i] + 1).ToString() + "/" + this.subFormationCount[i].ToString() + ")";
    }
    this._formationText = this.offPlaybook[index].GetFormationType() != FormationType.OffSpecial ? this.offPlaybook[index].GetSubFormationName() : "SPECIAL TEAMS";
    int playsInFormation = this.offPlaybook[index].GetNumberOfPlaysInFormation();
    this._formationText = playsInFormation != 1 ? this._formationText + "  -  " + playsInFormation.ToString() + " PLAYS" : this._formationText + "  -  " + playsInFormation.ToString() + " PLAY";
    this.playsInFormation_Txt.text = this._formationText;
    this.baseFormationIndex = i;
    this.formationDisplayIndex = index;
    this.ResetFormationItemTriggers(this.baseFormationIndex - this.formationListStartIndex);
    this.playbookFormationList[this.baseFormationIndex - this.formationListStartIndex].animator.SetTrigger(HashIDs.self.highlighted_Trigger);
    if (i < this.offPlaybook.Count - 1)
      this.SetPlayersInForm(this.offPlaybook[index]);
    else
      this.SetPlayersInForm(this.offPlaybook[this.offPlaybook.Count - 1]);
  }

  private void ResetFormationItemTriggers(int excludeIndex)
  {
    for (int index = 0; index < this.playbookFormationList.Length; ++index)
    {
      if (index != excludeIndex)
        this.playbookFormationList[index].animator.SetTrigger(HashIDs.self.normal_Trigger);
    }
  }

  public void ShowNextOffFormation()
  {
    if (this.baseFormationIndex >= this.numOfBaseFormations_Off - 1)
      return;
    if (this.baseFormationIndex - this.formationListStartIndex + 1 == PlaybookManager.numberOfFormationSlots)
      this.ScrollFormationListDown();
    this.ShowOffFormation(this.baseFormationIndex + 1);
  }

  public void ShowPrevOffFormation()
  {
    if (this.baseFormationIndex <= 0)
      return;
    if (this.baseFormationIndex - this.formationListStartIndex == 0)
      this.ScrollFormationListUp();
    this.ShowOffFormation(this.baseFormationIndex - 1);
  }

  public void SelectNextSubFormation()
  {
    if (!this.onOffense || this.subFormationIndex[this.baseFormationIndex] >= this.subFormationCount[this.baseFormationIndex] - 1)
      return;
    this.subFormationIndex[this.baseFormationIndex]++;
    this.ShowOffFormation(this.baseFormationIndex);
  }

  public void SelectPrevSubFormation()
  {
    if (!this.onOffense || this.subFormationIndex[this.baseFormationIndex] <= 0)
      return;
    this.subFormationIndex[this.baseFormationIndex]--;
    this.ShowOffFormation(this.baseFormationIndex);
  }

  public void ShowDefFormation(int i)
  {
    this.playbookFormationList[i - this.formationListStartIndex].subFormationSelection.SetActive(false);
    this.baseFormationIndex = i;
    this.formationDisplayIndex = i;
    if (i < this.defPlaybook.Count - 1)
      this.SetPlayersInForm(this.defPlaybook[i]);
    else
      this.SetPlayersInForm(this.defPlaybook[this.defPlaybook.Count - 1]);
    this.ResetFormationItemTriggers(this.baseFormationIndex - this.formationListStartIndex);
    this.playbookFormationList[this.baseFormationIndex - this.formationListStartIndex].animator.SetTrigger(HashIDs.self.highlighted_Trigger);
    this._formationText = this.defPlaybook[i].GetFormationType() != FormationType.DefSpecial ? this.defPlaybook[i].GetName() : "SPECIAL TEAMS";
    int playsInFormation = this.defPlaybook[i].GetNumberOfPlaysInFormation();
    this._formationText = playsInFormation != 1 ? this._formationText + "  -  " + playsInFormation.ToString() + " PLAYS" : this._formationText + "  -  " + playsInFormation.ToString() + " PLAY";
    this.playsInFormation_Txt.text = this._formationText;
  }

  public void ShowNextDefFormation()
  {
    if (this.formationDisplayIndex >= this.numOfBaseFormations_Def - 1)
      return;
    if (this.formationDisplayIndex - this.formationListStartIndex + 1 == PlaybookManager.numberOfFormationSlots)
      this.ScrollFormationListDown();
    this.ShowDefFormation(this.formationDisplayIndex + 1);
  }

  public void ShowPrevDefFormation()
  {
    if (this.formationDisplayIndex <= 0)
      return;
    if (this.formationDisplayIndex - this.formationListStartIndex == 0)
      this.ScrollFormationListUp();
    this.ShowDefFormation(this.formationDisplayIndex - 1);
  }

  public void ReturnToFormationSelect()
  {
    if (!this.returnToFormationBtn_GO.activeInHierarchy || this.formation == Plays.self.kickoffPlays || this.formation == Plays.self.kickReturnPlays)
      return;
    if (this.IsAudible())
      this.SetAudible(false);
    else
      this.ShowFormationSelectWindow();
  }

  public void SelectFormation()
  {
    if (this.onOffense)
      this.SelectOffFormation(this.formationDisplayIndex);
    else
      this.SelectDefFormation(this.formationDisplayIndex);
  }

  public void SelectOffFormation(int i)
  {
    this.UnFlipPlays();
    this.playFlipped = false;
    this.HideFormationSelectWindow();
    this.ShowPlaySelectWindow();
    if (i < this.offPlaybook.Count - 1)
    {
      this.formation = this.offPlaybook[i];
      this.lastFormationUsed = this.formation;
      this.lastFormationIndex = i;
    }
    else if (i == this.offPlaybook.Count - 1)
      this.formation = Plays.self.specialOffPlays;
    else if (i == this.offPlaybook.Count)
      this.formation = Plays.self.kickoffPlays;
    if (i < this.offPlaybook.Count)
      this.ShowReturnToFormationButton();
    else
      this.HideReturnToFormationButton();
    if (this.formation.GetFormationPositions().GetSubFormation() == SubFormation.None)
      this.titleText_Txt.text = this.formation.GetName();
    else
      this.titleText_Txt.text = this.formation.GetFormationPositions().GetBaseFormationString() + ":  " + this.formation.GetSubFormationName();
    this.page = 1;
    this.playsInFormation = this.formation.GetNumberOfPlaysInFormation();
    this.pagesInFormation = Mathf.CeilToInt((float) this.playsInFormation / (float) PlaybookManager.playsPerPage);
    this.playSelectScrollbar.size = this.GetPlaySelectScrollbarSize();
    this.SetPage();
    if (i < this.offPlaybook.Count - 1)
    {
      this.savedOffForm = i;
      this.ShowFlipPlayButton();
    }
    else
      this.HideFlipPlayButton();
    this.currentFormation = i;
  }

  public void SelectDefFormation(int i)
  {
    this.HideFormationSelectWindow();
    this.ShowPlaySelectWindow();
    if (i < this.defPlaybook.Count - 1)
    {
      this.formation = this.defPlaybook[i];
      this.lastFormationUsed = this.formation;
      this.lastFormationIndex = i;
    }
    else if (i == this.defPlaybook.Count - 1)
      this.formation = Plays.self.specialDefPlays;
    else if (i == this.defPlaybook.Count)
      this.formation = Plays.self.kickReturnPlays;
    if (i < this.defPlaybook.Count)
      this.ShowReturnToFormationButton();
    else
      this.HideReturnToFormationButton();
    this.titleText_Txt.text = this.formation.GetName();
    this.page = 1;
    this.playsInFormation = this.formation.GetNumberOfPlaysInFormation();
    this.pagesInFormation = Mathf.CeilToInt((float) this.playsInFormation / (float) PlaybookManager.playsPerPage);
    this.SetPage();
    this.currentFormation = i;
  }

  private void SetPlayersInForm(FormationData fd)
  {
    FormationPositions formationPositions = fd.GetFormationPositions();
    FormationType formationType = fd.GetFormationType();
    this.xLoc = formationPositions.GetXLocations();
    this.zLoc = formationPositions.GetZLocations();
    this.playersInForm = this.teamData.TeamDepthChart.GetPlayersInFormation(formationPositions);
    int num1 = -170;
    int num2 = 170;
    int num3 = -130;
    int num4 = -250;
    float num5 = -180f;
    float num6 = -50f;
    float num7 = 1.7f;
    float num8 = 1f;
    switch (formationType)
    {
      case FormationType.Offense:
        num6 = -90f;
        num8 = 1.5f;
        break;
      case FormationType.Defense:
        num6 = -210f;
        break;
      case FormationType.OffSpecial:
        num6 = -80f;
        break;
      case FormationType.DefSpecial:
        num6 = -205f;
        num8 = 1.3f;
        break;
      default:
        Debug.Log((object) "Selecting a formation that isn't in the list!");
        break;
    }
    for (int index = 0; index < this.playbookFormationIcons.Length; ++index)
    {
      float num9 = (float) (((double) this.xLoc[index] * (double) num7 - -16.0) / 32.0);
      float x = num5 + num9 * (float) (num2 - num1);
      float num10 = this.zLoc[index] / -8f * num8;
      float y = num6 + num10 * (float) (num4 - num3);
      this.playbookFormationIcons[index].trans.localPosition = new Vector3(x, y);
      this.playbookFormationIcons[index].SetPlayerNumber(this.teamData.GetPlayer(this.playersInForm[index]).Number);
      this.playbookFormationIcons[index].SetIconColor(FatigueManager.GetFatigueColor((float) this.teamData.GetPlayer(this.playersInForm[index]).Fatigue));
    }
  }

  public FormationData GetCurrentFormationData() => this.formation;

  public void ShowPlaySelectWindow()
  {
  }

  public void HidePlaySelectWindow()
  {
  }

  public bool IsPlaySelectVisible() => false;

  public void ScrollPlaySelectUp()
  {
    if (this.page <= 1)
      return;
    --this.page;
    this.SetPage();
  }

  public void ScrollPlaySelectDown()
  {
    if (this.page >= this.pagesInFormation)
      return;
    ++this.page;
    this.SetPage();
  }

  private void SetPage()
  {
    this._playNames = this.formation.GetPlayNames();
    int num1 = PlaybookManager.playsPerPage * (this.page - 1);
    int num2 = num1 + (PlaybookManager.playsPerPage - 1);
    if (this.page == this.pagesInFormation)
    {
      int num3 = this.playsInFormation % PlaybookManager.playsPerPage;
      if (num3 == 0)
        num3 = 3;
      num2 = num1 + num3 - 1;
    }
    for (int playIndex = num1; playIndex <= num2; ++playIndex)
      this.playGraphicManagers[playIndex - num1].DrawPlay(this.formation, playIndex);
    for (int index = num2 + 1; index < num1 + PlaybookManager.playsPerPage; ++index)
      this.playGraphicManagers[index - num1].Clear();
    if (this.pagesInFormation == 1)
      this.playSelectScrollbar.value = 1f;
    else
      this.playSelectScrollbar.value = this.GetPlaySelectScrollAmount();
  }

  public void SelectPlay(int i)
  {
    this.DetermineIfClockShouldStart_OnSelectPlay();
    int i1 = i + PlaybookManager.playsPerPage * (this.page - 1);
    this.checkForStickInput = true;
    this.checkForButtonInput = true;
    if (this.formation.GetPlay(i1) == null)
      return;
    MatchManager.instance.playManager.SetPlay(this.formation.GetPlay(i1), this.playFlipped, this.audible, this.onOffense);
  }

  private void DetermineIfClockShouldStart_OnShowPlaybook()
  {
    if (global::Game.IsKickoff || this.formation == Plays.self.kickoffPlays || this.formation == Plays.self.kickReturnPlays || MatchManager.runningPat)
      MatchManager.instance.timeManager.SetRunGameClock(false);
    PlaybookManager.wasGameClockRunningBefore = MatchManager.instance.timeManager.IsGameClockRunning();
    MatchManager.instance.timeManager.SetRunPlayClock(true);
    if (global::Game.IsNot2PMatch && !global::Game.IsSpectateMode && !this.onOffense)
    {
      MatchManager.instance.timeManager.SetRunPlayClock(false);
      MatchManager.instance.timeManager.SetRunGameClock(false);
    }
    if (!global::Game.IsKickoff && this.formation != Plays.self.kickoffPlays && this.formation != Plays.self.kickReturnPlays && !MatchManager.runningPat)
      return;
    MatchManager.instance.timeManager.SetRunPlayClock(false);
  }

  private void DetermineIfClockShouldStart_OnSelectPlay()
  {
    if (global::Game.IsKickoff || this.formation == Plays.self.kickoffPlays || this.formation == Plays.self.kickReturnPlays || MatchManager.runningPat)
      MatchManager.instance.timeManager.SetRunGameClock(false);
    if (global::Game.IsNot2PMatch)
    {
      MatchManager.instance.timeManager.SetRunPlayClock(true);
      if (!this.onOffense && PlaybookManager.wasGameClockRunningBefore)
        MatchManager.instance.timeManager.SetRunGameClock(true);
    }
    else
    {
      if (this.onOffense && this.IsOpposingTeamsPlaybookVisible())
      {
        MatchManager.instance.timeManager.SetRunPlayClock(false);
        MatchManager.instance.timeManager.SetRunGameClock(false);
      }
      if (!this.onOffense)
      {
        MatchManager.instance.timeManager.SetRunPlayClock(true);
        if (!this.IsOpposingTeamsPlaybookVisible() && PlaybookManager.wasGameClockRunningBefore)
          MatchManager.instance.timeManager.SetRunGameClock(true);
      }
    }
    if (!global::Game.IsKickoff && this.formation != Plays.self.kickoffPlays && this.formation != Plays.self.kickReturnPlays && !MatchManager.runningPat)
      return;
    MatchManager.instance.timeManager.SetRunPlayClock(false);
  }

  public void PickPlayUsingMike()
  {
    this.DetermineIfClockShouldStart_OnSelectPlay();
    this.checkForButtonInput = true;
    this.checkForStickInput = true;
    if (this.onOffense)
    {
      bool flip = Random.Range(0, 100) < 25;
      PlayDataOff offensivePlayForAi = MatchManager.instance.playManager.GetOffensivePlayForAI();
      for (int index = 0; index < this.offPlaybook.Count; ++index)
      {
        FormationPositions formationPositions = this.offPlaybook[index].GetFormationPositions();
        if (offensivePlayForAi.GetFormation() == formationPositions)
          this.SelectOffFormation(index);
      }
      MatchManager.instance.playManager.SetPlay((PlayData) offensivePlayForAi, flip, false, this.onOffense);
    }
    else
    {
      PlayDataDef defensivePlayForAi = MatchManager.instance.playManager.GetDefensivePlayForAI();
      for (int index = 0; index < this.defPlaybook.Count; ++index)
      {
        FormationPositions formationPositions = this.defPlaybook[index].GetFormationPositions();
        if (defensivePlayForAi.GetFormation() == formationPositions)
          this.SelectDefFormation(index);
      }
      MatchManager.instance.playManager.SetPlay((PlayData) defensivePlayForAi, false, false, this.onOffense);
    }
  }

  public List<FormationData> GetOffensivePlaybook() => this.offPlaybook;

  public List<FormationData> GetDefensivePlaybook() => this.defPlaybook;

  public FormationData GetActiveFormationInPlaybook() => this.formation;

  public void ShowReturnToFormationButton() => this.returnToFormationBtn_GO.SetActive(true);

  public void HideReturnToFormationButton() => this.returnToFormationBtn_GO.SetActive(false);

  private int GetNumberOfFormationsOnSide() => !this.onOffense ? this.numOfBaseFormations_Def : this.numOfBaseFormations_Off;

  public float GetFormationScrollAmount() => 1f / (float) (this.GetNumberOfFormationsOnSide() - PlaybookManager.numberOfFormationSlots);

  private float GetFormationSelectScrollbarSize() => this.onOffense ? 1f / (float) (this.baseFormationsList.Count - PlaybookManager.numberOfFormationSlots + 1) : 1f / (float) (this.defPlaybook.Count - PlaybookManager.numberOfFormationSlots + 1);

  private float GetPlaySelectScrollAmount() => (float) (1.0 - (double) (this.page - 1) / (double) (this.pagesInFormation - 1));

  private float GetPlaySelectScrollbarSize() => 1f / (float) this.pagesInFormation;

  private bool IsOpposingTeamsPlaybookVisible()
  {
    int num = global::Game.IsNot2PMatch ? 1 : 0;
    return false;
  }

  public bool IsAudible() => this.audible;

  public void SetAudible(bool a) => this.audible = a;

  public void OpenSubstitutions()
  {
  }

  public void ShowSubstitutionButton() => this.subsBtn_GO.SetActive(true);

  public void HideSubstitutionButton() => this.subsBtn_GO.SetActive(false);

  public void ShowFlipPlayButton() => this.flipPlayBtn_GO.SetActive(true);

  public void HideFlipPlayButton() => this.flipPlayBtn_GO.SetActive(false);

  public void ToggleFlip()
  {
    if (this.playFlipped)
    {
      this.playFlipped = false;
      for (int index = 0; index < PlaybookManager.playsPerPage; ++index)
        this.playGraphicManagers[index].UnflipPlay();
    }
    else
    {
      this.playFlipped = true;
      for (int index = 0; index < PlaybookManager.playsPerPage; ++index)
        this.playGraphicManagers[index].FlipPlay();
    }
  }

  private void UnFlipPlays()
  {
    if (!this.playFlipped)
      return;
    for (int index = 0; index < PlaybookManager.playsPerPage; ++index)
      this.playGraphicManagers[index].UnflipPlay();
    this.playFlipped = false;
  }

  public void SetToOffense()
  {
    this.formationDisplayIndex = 0;
    this.formationListStartIndex = 0;
    this.formation = (FormationData) null;
    this.onOffense = true;
    this.ShowFlipPlayButton();
    this.formationScrollbar.value = 1f;
    this.ShowOffFormation(0);
  }

  public void SetToDefense()
  {
    this.formationDisplayIndex = 0;
    this.formationListStartIndex = 0;
    this.formation = (FormationData) null;
    this.onOffense = false;
    this.HideFlipPlayButton();
    this.formationScrollbar.value = 1f;
    this.ShowDefFormation(0);
  }

  public void ForceOffFormation(FormationType formationType)
  {
    this.UnFlipPlays();
    int i = -1;
    switch (formationType)
    {
      case FormationType.OffSpecial:
        i = this.offPlaybook.Count - 1;
        this.formation = Plays.self.specialOffPlays;
        break;
      case FormationType.Kickoff:
        i = this.offPlaybook.Count;
        this.formation = Plays.self.kickoffPlays;
        break;
      default:
        Debug.Log((object) ("Calling ForceOffFormation on an invalid FormationType: " + formationType.ToString()));
        break;
    }
    this.SelectOffFormation(i);
    this.HideFormationSelectWindow();
    this.HideFlipPlayButton();
  }

  public void ForceDefFormation(FormationType formationType)
  {
    this.UnFlipPlays();
    int i = -1;
    switch (formationType)
    {
      case FormationType.DefSpecial:
        i = this.defPlaybook.Count - 1;
        this.formation = Plays.self.specialDefPlays;
        break;
      case FormationType.KickReturn:
        i = this.defPlaybook.Count;
        this.formation = Plays.self.kickReturnPlays;
        break;
      default:
        Debug.Log((object) ("Calling ForceDefFormation on an invalid FormationType: " + formationType.ToString()));
        break;
    }
    this.SelectDefFormation(i);
    this.HideFormationSelectWindow();
    this.HideFlipPlayButton();
  }

  public void ForceKickoffPlays()
  {
    MatchManager.instance.timeManager.SetRunPlayClock(false);
    this.UnFlipPlays();
    this.HideFlipPlayButton();
    this.ForceOffFormation(FormationType.Kickoff);
    this.ShowWindow();
    this.HideFormationSelectWindow();
    this.ShowPlaySelectWindow();
  }

  public void ForceKickReturnPlays()
  {
    MatchManager.instance.timeManager.SetRunPlayClock(false);
    this.HideFlipPlayButton();
    this.UnFlipPlays();
    this.ForceDefFormation(FormationType.KickReturn);
    this.ShowWindow();
    this.HideFormationSelectWindow();
    this.ShowPlaySelectWindow();
  }

  private void ScrollFormationListUp()
  {
    if (this.formationListStartIndex <= 0)
      return;
    --this.formationListStartIndex;
    this.formationScrollbar.value += this.GetFormationScrollAmount();
    this.SetFormationNames();
  }

  private void ScrollFormationListDown()
  {
    if (this.formationListStartIndex + PlaybookManager.numberOfFormationSlots >= this.GetNumberOfFormationsOnSide())
      return;
    ++this.formationListStartIndex;
    this.formationScrollbar.value -= this.GetFormationScrollAmount();
    this.SetFormationNames();
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || global::Game.HasScreenOverlay || ControllerAssignmentManager.instance.IsVisible())
      return;
    float num1 = UserManager.instance.LeftStickY(this.player);
    float num2 = UserManager.instance.LeftStickX(this.player);
    float axis = Input.GetAxis("Mouse ScrollWheel");
    if (this.IsFormationSelectVisible())
    {
      if (this.checkForStickInput)
      {
        if ((double) axis > 0.0)
        {
          this.ScrollFormationListUp();
          this.ShowFormation(this.formationSlotIndex);
        }
        else if ((double) axis < 0.0)
        {
          this.ScrollFormationListDown();
          this.ShowFormation(this.formationSlotIndex);
        }
        if ((double) num1 > 0.5)
        {
          this.StartCoroutine(this.SetStickInputDelay());
          if (this.onOffense)
            this.ShowPrevOffFormation();
          else
            this.ShowPrevDefFormation();
        }
        else if ((double) num1 < -0.5)
        {
          this.StartCoroutine(this.SetStickInputDelay());
          if (this.onOffense)
            this.ShowNextOffFormation();
          else
            this.ShowNextDefFormation();
        }
        if ((double) num2 > 0.5)
        {
          this.StartCoroutine(this.SetStickInputDelay());
          this.SelectNextSubFormation();
        }
        else if ((double) num2 < -0.5)
        {
          this.StartCoroutine(this.SetStickInputDelay());
          this.SelectPrevSubFormation();
        }
      }
      if (!this.checkForButtonInput)
        return;
      if (UserManager.instance.Action1WasPressed(this.player))
      {
        this.StartCoroutine(this.SetButtonInputDelay());
        if (this.onOffense)
          this.SelectOffFormation(this.formationDisplayIndex);
        else
          this.SelectDefFormation(this.formationDisplayIndex);
      }
      if (!this.audible && UserManager.instance.RightBumperWasPressed(this.player))
        this.OpenSubstitutions();
      if (!UserManager.instance.Action3WasPressed(this.player))
        return;
      ControllerManagerGame.playSelectedWithCont = true;
      this.PickPlayUsingMike();
    }
    else
    {
      if (!this.IsPlaySelectVisible())
        return;
      if (this.checkForStickInput)
      {
        if ((double) num1 > 0.5 || (double) axis > 0.0)
        {
          this.ScrollPlaySelectUp();
          this.StartCoroutine(this.SetStickInputDelay());
        }
        else if ((double) num1 < -0.5 || (double) axis < 0.0)
        {
          this.ScrollPlaySelectDown();
          this.StartCoroutine(this.SetStickInputDelay());
        }
      }
      if (this.flipPlayBtn_GO.activeInHierarchy && UserManager.instance.LeftBumperWasPressed(this.player))
        this.ToggleFlip();
      if (!this.checkForButtonInput)
        return;
      int i = -1;
      if (UserManager.instance.Action4WasPressed(this.player))
        i = 0;
      else if (UserManager.instance.Action3WasPressed(this.player))
        i = 1;
      else if (UserManager.instance.Action1WasPressed(this.player))
        i = 2;
      if (i > -1 && this.playGraphicManagers[i].playGraphicSection.activeInHierarchy)
      {
        if (this.player == Player.One)
          ControllerManagerGame.playSelectedWithCont = true;
        this.SelectPlay(i);
      }
      if (!this.audible && UserManager.instance.RightBumperWasPressed(this.player))
        this.OpenSubstitutions();
      if (!UserManager.instance.Action2WasPressed(this.player))
        return;
      this.ReturnToFormationSelect();
    }
  }

  private IEnumerator SetStickInputDelay()
  {
    this.checkForStickInput = false;
    yield return (object) this.inputDelay_WFS;
    this.checkForStickInput = true;
  }

  private IEnumerator SetButtonInputDelay()
  {
    this.checkForButtonInput = false;
    yield return (object) this.inputDelay_WFS;
    this.checkForButtonInput = true;
  }

  public void SetPlaybookType(EPlaybookType type)
  {
    if (type == EPlaybookType.Defense)
    {
      this.SetToDefense();
    }
    else
    {
      if (type != EPlaybookType.Offense)
        return;
      this.SetToOffense();
    }
  }

  public void ForcePlayType(EShowPlayType type)
  {
    if (type == EShowPlayType.Kickoff)
    {
      this.ForceKickoffPlays();
    }
    else
    {
      if (type != EShowPlayType.KickReturn)
        return;
      this.ForceKickReturnPlays();
    }
  }
}
