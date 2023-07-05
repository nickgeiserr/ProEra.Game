// Decompiled with JetBrains decompiler
// Type: PlayerEditor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEditor : MonoBehaviour
{
  [Header("Main Section")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private Image gradientCover_Img;
  [SerializeField]
  private GameObject sectionContainer_GO;
  [SerializeField]
  private RectTransform sectionContainer_Trans;
  private int sectionIndex;
  private float activeIndicatorAnimationSpeed = 0.15f;
  [Header("Active Player Section")]
  [SerializeField]
  private Image teamLogo_Img;
  [SerializeField]
  private Image playerPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI playerDescription_Txt;
  [SerializeField]
  private TextMeshProUGUI activeTeam_Txt;
  [SerializeField]
  private TextMeshProUGUI activePosition_Txt;
  [SerializeField]
  private TextMeshProUGUI playerEditedHelp_Txt;
  [SerializeField]
  private TextMeshProUGUI teamOFF_Txt;
  [SerializeField]
  private TextMeshProUGUI teamDEF_Txt;
  [SerializeField]
  private TextMeshProUGUI TeamSPC_Txt;
  [Header("Player List Section")]
  [SerializeField]
  private TextMeshProUGUI[] categoryHeaders;
  [SerializeField]
  private PlayerEditorLine[] playerLines;
  [SerializeField]
  private RectTransform playerListScrollContents;
  [SerializeField]
  private UnityEngine.UI.Button editPersonalInfo_Btn;
  private PlayerData activePlayer;
  private Position filterPosition;
  private int[] sortedPlayers;
  private int selectedAttributeIndex;
  private bool allowMove;
  private int selectedLineIndex;
  private int activePositionIndex;
  [Header("Create Random Team Window")]
  [SerializeField]
  private CanvasGroup createRandomTeam_CG;
  [SerializeField]
  private UnityEngine.UI.Button createRandomTeamYes_Btn;
  [Header("Restore Team To Default Window")]
  [SerializeField]
  private CanvasGroup restoreTeamToDefault_CG;
  [SerializeField]
  private UnityEngine.UI.Button restoreTeamToDefaultYes_Btn;
  [Header("Edit Player Main Section")]
  [SerializeField]
  private Image editingTeamLogo_Img;
  [SerializeField]
  private Image editingPlayerPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI editingPlayerName_Txt;
  [SerializeField]
  private TextMeshProUGUI editingPlayerDescription_Txt;
  [SerializeField]
  private TextMeshProUGUI playerOVR_Txt;
  private KeyboardEditFields currentKeyboardEditField;
  [Header("Edit Personal Information")]
  [SerializeField]
  private CanvasGroup editPersonalInformation_CG;
  [SerializeField]
  private TextMeshProUGUI editField_FirstName_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_LastName_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_Portrait_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_Number_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_Position_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_Age_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_Skin_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_Height_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_Weight_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_Visor_Txt;
  [SerializeField]
  private TextMeshProUGUI editField_Sleeves_Txt;
  [SerializeField]
  private UnityEngine.UI.Button[] personalInformation_Btns;
  private int selectedEditingFieldIndex;
  [Header("Edit Attributes")]
  [SerializeField]
  private CanvasGroup editAttributes_CG;
  [SerializeField]
  private TextMeshProUGUI speed_Txt;
  [SerializeField]
  private TextMeshProUGUI awareness_Txt;
  [SerializeField]
  private TextMeshProUGUI tackleBreak_Txt;
  [SerializeField]
  private TextMeshProUGUI fumble_Txt;
  [SerializeField]
  private TextMeshProUGUI catch_Txt;
  [SerializeField]
  private TextMeshProUGUI blocking_Txt;
  [SerializeField]
  private TextMeshProUGUI throwAccuracy_Txt;
  [SerializeField]
  private TextMeshProUGUI throwPower_Txt;
  [SerializeField]
  private TextMeshProUGUI blockBreaking_Txt;
  [SerializeField]
  private TextMeshProUGUI tackle_Txt;
  [SerializeField]
  private TextMeshProUGUI hitPower_Txt;
  [SerializeField]
  private TextMeshProUGUI cover_Txt;
  [SerializeField]
  private TextMeshProUGUI discipline_Txt;
  [SerializeField]
  private TextMeshProUGUI endurance_Txt;
  [SerializeField]
  private TextMeshProUGUI agility_Txt;
  [SerializeField]
  private TextMeshProUGUI fitness_Txt;
  [SerializeField]
  private TextMeshProUGUI kickAccuracy_Txt;
  [SerializeField]
  private TextMeshProUGUI kickPower_Txt;
  [SerializeField]
  private Image speed_Img;
  [SerializeField]
  private Image awareness_Img;
  [SerializeField]
  private Image tackleBreak_Img;
  [SerializeField]
  private Image fumble_Img;
  [SerializeField]
  private Image catch_Img;
  [SerializeField]
  private Image blocking_Img;
  [SerializeField]
  private Image throwAccuracy_Img;
  [SerializeField]
  private Image throwPower_Img;
  [SerializeField]
  private Image blockBreaking_Img;
  [SerializeField]
  private Image tackle_Img;
  [SerializeField]
  private Image hitPower_Img;
  [SerializeField]
  private Image cover_Img;
  [SerializeField]
  private Image discipline_Img;
  [SerializeField]
  private Image endurance_Img;
  [SerializeField]
  private Image agility_Img;
  [SerializeField]
  private Image fitness_Img;
  [SerializeField]
  private Image kickAccuracy_Img;
  [SerializeField]
  private Image kickPower_Img;
  [SerializeField]
  private UnityEngine.UI.Button[] attribute_Btns;
  [SerializeField]
  private RectTransform attributeListScrollContents;
  private TeamData teamData;
  private int teamIndex;
  private int playersInList;
  private List<int> playerList;
  private bool wereChangesMade;
  private int beginScrollAtIndex;
  private int beginAttributeScrollAtIndex;
  private int playerSlotRowHeight;
  private int attributeRowHeight;
  private int minNumber = 1;
  private int maxNumber = 99;
  private int minWeight = 150;
  private int maxWeight = 500;
  private WaitForSeconds _clearTextWFS = new WaitForSeconds(5f);
  private WaitForSecondsRealtime _disableMove = new WaitForSecondsRealtime(0.2f);

  private void Start()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
    this.restoreTeamToDefault_CG.alpha = 0.0f;
    this.restoreTeamToDefault_CG.blocksRaycasts = false;
    this.createRandomTeam_CG.alpha = 0.0f;
    this.createRandomTeam_CG.blocksRaycasts = false;
  }

  public void Init()
  {
    this.playerList = new List<int>();
    PlayerEditorLine.guiParent = this;
    this.beginScrollAtIndex = 4;
    this.playerSlotRowHeight = 83;
    this.beginAttributeScrollAtIndex = 8;
    this.attributeRowHeight = 60;
    for (int index = 0; index < this.playerLines.Length; ++index)
    {
      if (index % 2 == 0)
        this.playerLines[index].HideDarkBackground();
      else
        this.playerLines[index].ShowDarkBackground();
    }
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow(TeamData selectedTeamData)
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    this.allowMove = true;
    BottomBarManager.instance.ShowBackButton();
    BottomBarManager.instance.SetControllerButtonGuide(10);
    this.activePositionIndex = 0;
    this.teamData = selectedTeamData;
    this.SetTeamDisplay();
    this.ShowPlayerListSection();
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.15f);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    if (this.IsRestoreTeamToDefaultWindowVisible())
      this.HideRestoreTeamToDefaultWindow();
    else if (this.IsEditPersonalInformationVisible())
    {
      if (this.wereChangesMade)
        this.SaveCurrentTeamData();
      this.HideEditPersonalInformationSection();
    }
    else if (this.IsEditAttributesVisible())
    {
      if (this.wereChangesMade)
        this.SaveCurrentTeamData();
      this.HideEditAttributesSection();
    }
    else
    {
      this.HideWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowDashboard();
    }
  }

  private void ShowPlayerListSection()
  {
    UISoundManager.instance.PlayTabSwipe();
    this.sectionIndex = 0;
    LeanTween.moveLocalX(this.sectionContainer_GO, (float) this.sectionIndex * -1f * this.sectionContainer_Trans.rect.width, this.activeIndicatorAnimationSpeed);
    BottomBarManager.instance.SetControllerButtonGuide(3);
    this.SetTeamRatings();
    this.FilterByPosition(TeamData.ALL_GENERIC_POSITIONS[this.activePositionIndex]);
  }

  private bool IsPlayerListSectionVisible() => this.sectionIndex == 0;

  private void SetTeamDisplay()
  {
    this.activeTeam_Txt.text = this.teamData.GetFullDisplayName();
    PortraitManager.self.ClearTeamPlayerPortraits();
    this.gradientCover_Img.color = this.teamData.GetPrimaryColor();
    this.teamLogo_Img.sprite = this.teamData.GetMediumLogo();
    this.editingTeamLogo_Img.sprite = this.teamData.GetMediumLogo();
  }

  private void SetTeamRatings()
  {
    this.teamOFF_Txt.text = TeamData.GetAttributeGradeFromNumber(this.teamData.GetTeamRating_OFF());
    this.teamDEF_Txt.text = TeamData.GetAttributeGradeFromNumber(this.teamData.GetTeamRating_DEF());
    this.TeamSPC_Txt.text = TeamData.GetAttributeGradeFromNumber(this.teamData.GetTeamRating_SPC());
  }

  public void ShowActivePlayer(int playerIndex)
  {
    this.activePlayer = this.teamData.GetPlayer(playerIndex);
    this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(this.activePlayer);
    this.playerName_Txt.text = this.activePlayer.FullName;
    this.playerDescription_Txt.text = this.activePlayer.PlayerPosition.ToString() + ": #" + this.activePlayer.Number.ToString() + "  /  HT: " + this.activePlayer.GetStandardHeight() + "  /  WT: " + this.activePlayer.Weight.ToString() + "  /  AGE: " + this.activePlayer.Age.ToString();
  }

  public void FilterByPosition(Position p)
  {
    this.filterPosition = p;
    this.activePosition_Txt.text = this.filterPosition.ToString();
    this.playerListScrollContents.anchoredPosition = Vector2.zero;
    int[] attributeOrder = TeamData.GetAttributeOrder(this.filterPosition);
    this.categoryHeaders[0].text = "OVR";
    for (int index1 = 1; index1 < this.categoryHeaders.Length; ++index1)
    {
      int index2 = attributeOrder[index1 - 1];
      this.categoryHeaders[index1].text = TeamData.attributeAbbreviations[index2];
    }
    int a = 0;
    this.playerList.Clear();
    for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
    {
      if (this.teamData.GetPlayer(playerIndex) != null && this.teamData.GetPlayer(playerIndex).PlayerPosition == this.filterPosition)
      {
        this.playerList.Add(playerIndex);
        ++a;
      }
    }
    this.sortedPlayers = this.playerList.ToArray();
    this.SortPlayers(this.sortedPlayers);
    int num = Mathf.Min(a, this.playerLines.Length);
    for (int index = 0; index < num; ++index)
      this.playerLines[index].SetAttributeValues(this.teamData.GetPlayer(this.sortedPlayers[index]), attributeOrder);
    this.playersInList = a - 1;
    for (int index = a; index < this.playerLines.Length; ++index)
      this.playerLines[index].ClearLine();
    if (a <= 0)
      return;
    this.selectedLineIndex = 0;
    this.playerLines[0].SelectLine();
  }

  private void SelectFirstLine()
  {
    this.playerListScrollContents.anchoredPosition = Vector2.zero;
    this.playerLines[0].SelectLine();
    this.selectedLineIndex = 0;
  }

  private void SortPlayers(int[] p)
  {
    int attributeIndex = 0;
    if (this.selectedAttributeIndex > 0)
      attributeIndex = TeamData.GetAttributeOrder(this.filterPosition)[this.selectedAttributeIndex - 1];
    for (int index1 = 0; index1 < p.Length - 1; ++index1)
    {
      int playerIndex1 = p[index1];
      int index2 = index1;
      int num1 = this.selectedAttributeIndex != 0 ? this.teamData.GetPlayer(playerIndex1).GetAttributeByIndex(attributeIndex) : this.teamData.GetPlayer(playerIndex1).GetOverall();
      for (int index3 = index1 + 1; index3 < p.Length; ++index3)
      {
        int playerIndex2 = p[index3];
        int num2 = this.selectedAttributeIndex != 0 ? this.teamData.GetPlayer(playerIndex2).GetAttributeByIndex(attributeIndex) : this.teamData.GetPlayer(playerIndex2).GetOverall();
        if (num2 > num1)
        {
          index2 = index3;
          num1 = num2;
        }
      }
      int num3 = p[index1];
      p[index1] = p[index2];
      p[index2] = num3;
    }
  }

  private IEnumerator ClearEditedPlayerHelpText()
  {
    yield return (object) this._clearTextWFS;
    this.playerEditedHelp_Txt.text = "";
  }

  public void ShowCreateRandomTeamWindow()
  {
    LeanTween.alphaCanvas(this.createRandomTeam_CG, 1f, 0.3f);
    this.createRandomTeam_CG.blocksRaycasts = true;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.createRandomTeamYes_Btn);
  }

  public void HideCreateRandomTeamWindow()
  {
    LeanTween.alphaCanvas(this.createRandomTeam_CG, 0.0f, 0.3f);
    this.createRandomTeam_CG.blocksRaycasts = false;
    this.SelectFirstLine();
  }

  private bool IsCreateRandomTeamWindowVisible() => (double) this.createRandomTeam_CG.alpha > 0.0;

  public void CreateRandomTeam()
  {
    this.teamData.CreateNewRoster();
    this.SaveCurrentTeamData();
    this.SetTeamDisplay();
    this.ShowPlayerListSection();
    this.HideCreateRandomTeamWindow();
  }

  public void SaveCurrentTeamData()
  {
  }

  private IEnumerator DelayAfterSave()
  {
    PopupLoadingScreen.self.ShowPopupLoadingScreen("Saving Team");
    yield return (object) new WaitForSeconds(3f);
    PopupLoadingScreen.self.HidePopupLoadingScreen();
  }

  public void ShowRestoreTeamToDefaultWindow()
  {
    LeanTween.alphaCanvas(this.restoreTeamToDefault_CG, 1f, 0.3f);
    this.restoreTeamToDefault_CG.blocksRaycasts = true;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.restoreTeamToDefaultYes_Btn);
  }

  public void HideRestoreTeamToDefaultWindow()
  {
    LeanTween.alphaCanvas(this.restoreTeamToDefault_CG, 0.0f, 0.3f);
    this.restoreTeamToDefault_CG.blocksRaycasts = false;
    this.SelectFirstLine();
  }

  private bool IsRestoreTeamToDefaultWindowVisible() => (double) this.restoreTeamToDefault_CG.alpha > 0.0;

  public void RestoreTeamToDefault()
  {
    this.teamData.RestoreRosterToDefault();
    this.SetTeamDisplay();
    this.ShowPlayerListSection();
    this.HideRestoreTeamToDefaultWindow();
  }

  private void SetEditingPlayerDisplay()
  {
    this.editingPlayerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(this.activePlayer);
    this.editingPlayerName_Txt.text = this.activePlayer.FullName;
    this.editingPlayerDescription_Txt.text = this.activePlayer.PlayerPosition.ToString() + ": #" + this.activePlayer.Number.ToString() + "  /  HT: " + this.activePlayer.GetStandardHeight() + "  /  WT: " + this.activePlayer.Weight.ToString() + "  /  AGE: " + this.activePlayer.Age.ToString();
    this.playerOVR_Txt.text = TeamData.GetAttributeGradeFromNumber(this.activePlayer.GetOverall());
  }

  public void OnScreenKeyboardTextAccepted(string keyboardInputValue)
  {
    if (this.currentKeyboardEditField == KeyboardEditFields.FirstName)
      this.activePlayer.FirstName = keyboardInputValue;
    else if (this.currentKeyboardEditField == KeyboardEditFields.LastName)
      this.activePlayer.LastName = keyboardInputValue;
    else if (this.currentKeyboardEditField == KeyboardEditFields.Number)
      this.activePlayer.Number = Mathf.Clamp(int.Parse(keyboardInputValue), this.minNumber, this.maxNumber);
    else if (this.currentKeyboardEditField == KeyboardEditFields.Weight)
      this.activePlayer.Weight = Mathf.Clamp(int.Parse(keyboardInputValue), this.minWeight, this.maxWeight);
    this.UpdatePersonalInformation();
  }

  public void OnScreenKeyboardClosed()
  {
    if (!this.IsEditPersonalInformationVisible())
      return;
    this.SelectPersonalInfoItem(this.selectedEditingFieldIndex);
  }

  public void ShowEditPersonalInformationSection()
  {
    this.wereChangesMade = false;
    this.editPersonalInformation_CG.alpha = 1f;
    this.editPersonalInformation_CG.blocksRaycasts = true;
    this.editAttributes_CG.alpha = 0.0f;
    this.editAttributes_CG.blocksRaycasts = false;
    UISoundManager.instance.PlayTabSwipe();
    this.sectionIndex = 1;
    this.activePlayer.PortraitID %= PortraitManager.NUMBER_OF_PLAYER_PORTRAITS_PER_SKIN_TYPE;
    this.SetEditingPlayerDisplay();
    this.SetPersonalInformationValues();
    LeanTween.moveLocalX(this.sectionContainer_GO, (float) this.sectionIndex * -1f * this.sectionContainer_Trans.rect.width, this.activeIndicatorAnimationSpeed);
    BottomBarManager.instance.SetControllerButtonGuide(3);
    this.selectedEditingFieldIndex = 0;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.personalInformation_Btns[0]);
  }

  public void HideEditPersonalInformationSection() => this.ShowPlayerListSection();

  private bool IsEditPersonalInformationVisible() => this.sectionIndex == 1 && (double) this.editPersonalInformation_CG.alpha > 0.0;

  private void UpdatePersonalInformation()
  {
    this.wereChangesMade = true;
    this.SetEditingPlayerDisplay();
    this.SetPersonalInformationValues();
  }

  private void SetPersonalInformationValues()
  {
    this.editField_FirstName_Txt.text = this.activePlayer.FirstName;
    this.editField_LastName_Txt.text = this.activePlayer.LastName;
    this.editField_Portrait_Txt.text = this.activePlayer.PortraitID.ToString();
    this.editField_Number_Txt.text = this.activePlayer.Number.ToString();
    this.editField_Position_Txt.text = this.activePlayer.PlayerPosition.ToString();
    this.editField_Age_Txt.text = this.activePlayer.Age.ToString();
    this.editField_Skin_Txt.text = this.activePlayer.SkinColor.ToString();
    this.editField_Height_Txt.text = this.activePlayer.GetStandardHeight();
    this.editField_Weight_Txt.text = this.activePlayer.Weight.ToString();
    this.editField_Visor_Txt.text = this.activePlayer.Visor == 1 ? "ON" : "OFF";
    this.editField_Sleeves_Txt.text = this.activePlayer.Sleeves == 1 ? "ON" : "OFF";
  }

  public void EditFirstName()
  {
    this.currentKeyboardEditField = KeyboardEditFields.FirstName;
    OnScreenKeyboard.instance.ShowWindow(15, false);
  }

  public void EditLastName()
  {
    this.currentKeyboardEditField = KeyboardEditFields.LastName;
    OnScreenKeyboard.instance.ShowWindow(15, false);
  }

  public void EditNumber()
  {
    this.currentKeyboardEditField = KeyboardEditFields.Number;
    OnScreenKeyboard.instance.ShowWindow(2, _allowNonNumberInput: false);
  }

  public void SetNextPosition()
  {
    int positionIndex = this.FindPositionIndex();
    if (positionIndex >= TeamData.ALL_GENERIC_POSITIONS.Count - 1)
      return;
    this.activePlayer.PlayerPosition = TeamData.ALL_GENERIC_POSITIONS[positionIndex + 1];
    this.UpdatePersonalInformation();
  }

  public void SetPreviousPosition()
  {
    int positionIndex = this.FindPositionIndex();
    if (positionIndex <= 0)
      return;
    this.activePlayer.PlayerPosition = TeamData.ALL_GENERIC_POSITIONS[positionIndex - 1];
    this.UpdatePersonalInformation();
  }

  private int FindPositionIndex()
  {
    for (int index = 0; index < TeamData.ALL_GENERIC_POSITIONS.Count; ++index)
    {
      if (this.activePlayer.PlayerPosition == TeamData.ALL_GENERIC_POSITIONS[index])
        return index;
    }
    return 0;
  }

  public void SetNextAge()
  {
    if (this.activePlayer.Age >= 50)
      return;
    ++this.activePlayer.Age;
    this.UpdatePersonalInformation();
  }

  public void SetPreviousAge()
  {
    if (this.activePlayer.Age <= 18)
      return;
    --this.activePlayer.Age;
    this.UpdatePersonalInformation();
  }

  public void SetNextSkinColor()
  {
    if (this.activePlayer.SkinColor >= 6)
      return;
    ++this.activePlayer.SkinColor;
    this.UpdatePersonalInformation();
  }

  public void SetPreviousSkinColor()
  {
    if (this.activePlayer.SkinColor <= 1)
      return;
    --this.activePlayer.SkinColor;
    this.UpdatePersonalInformation();
  }

  public void SetNextPortrait()
  {
    if (this.activePlayer.PortraitID >= PortraitManager.NUMBER_OF_PLAYER_PORTRAITS_PER_SKIN_TYPE)
      return;
    ++this.activePlayer.PortraitID;
    this.UpdatePersonalInformation();
  }

  public void SetPreviousPortrait()
  {
    if (this.activePlayer.PortraitID <= 0)
      return;
    --this.activePlayer.PortraitID;
    this.UpdatePersonalInformation();
  }

  public void SetNextHeight()
  {
    if (this.activePlayer.Height >= 96)
      return;
    ++this.activePlayer.Height;
    this.UpdatePersonalInformation();
  }

  public void SetPreviousHeight()
  {
    if (this.activePlayer.Height <= 60)
      return;
    --this.activePlayer.Height;
    this.UpdatePersonalInformation();
  }

  public void EditWeight()
  {
    this.currentKeyboardEditField = KeyboardEditFields.Weight;
    OnScreenKeyboard.instance.ShowWindow(3, _allowNonNumberInput: false);
  }

  public void ToggleVisor()
  {
    this.activePlayer.Visor = this.activePlayer.Visor != 1 ? 1 : 0;
    this.UpdatePersonalInformation();
  }

  public void ToggleSleeves()
  {
    this.activePlayer.Sleeves = this.activePlayer.Sleeves != 1 ? 1 : 0;
    this.UpdatePersonalInformation();
  }

  public void ShowEditAttributesSection()
  {
    this.wereChangesMade = false;
    this.editPersonalInformation_CG.alpha = 0.0f;
    this.editPersonalInformation_CG.blocksRaycasts = false;
    this.editAttributes_CG.alpha = 1f;
    this.editAttributes_CG.blocksRaycasts = true;
    UISoundManager.instance.PlayTabSwipe();
    this.sectionIndex = 1;
    this.SetEditingPlayerDisplay();
    this.SetAttributeValues();
    LeanTween.moveLocalX(this.sectionContainer_GO, (float) this.sectionIndex * -1f * this.sectionContainer_Trans.rect.width, this.activeIndicatorAnimationSpeed);
    BottomBarManager.instance.SetControllerButtonGuide(3);
    this.selectedEditingFieldIndex = 0;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.attribute_Btns[0]);
  }

  public void HideEditAttributesSection() => this.ShowPlayerListSection();

  private bool IsEditAttributesVisible() => this.sectionIndex == 1 && (double) this.editAttributes_CG.alpha > 0.0;

  private void UpdatePlayerAttributes()
  {
    this.wereChangesMade = true;
    this.SetEditingPlayerDisplay();
    this.SetAttributeValues();
  }

  private void SetAttributeValues()
  {
    this.speed_Txt.text = this.activePlayer.Speed.ToString();
    this.speed_Img.fillAmount = (float) this.activePlayer.Speed / 99f;
    this.awareness_Txt.text = this.activePlayer.Awareness.ToString();
    this.awareness_Img.fillAmount = (float) this.activePlayer.Awareness / 99f;
    this.tackleBreak_Txt.text = this.activePlayer.TackleBreaking.ToString();
    this.tackleBreak_Img.fillAmount = (float) this.activePlayer.TackleBreaking / 99f;
    this.fumble_Txt.text = this.activePlayer.Fumbling.ToString();
    this.fumble_Img.fillAmount = (float) this.activePlayer.Fumbling / 99f;
    this.catch_Txt.text = this.activePlayer.Catching.ToString();
    this.catch_Img.fillAmount = (float) this.activePlayer.Catching / 99f;
    this.blocking_Txt.text = this.activePlayer.Blocking.ToString();
    this.blocking_Img.fillAmount = (float) this.activePlayer.Blocking / 99f;
    this.throwAccuracy_Txt.text = this.activePlayer.ThrowAccuracy.ToString();
    this.throwAccuracy_Img.fillAmount = (float) this.activePlayer.ThrowAccuracy / 99f;
    this.throwPower_Txt.text = this.activePlayer.ThrowPower.ToString();
    this.throwPower_Img.fillAmount = (float) this.activePlayer.ThrowPower / 99f;
    this.blockBreaking_Txt.text = this.activePlayer.BlockBreaking.ToString();
    this.blockBreaking_Img.fillAmount = (float) this.activePlayer.BlockBreaking / 99f;
    this.tackle_Txt.text = this.activePlayer.Tackling.ToString();
    this.tackle_Img.fillAmount = (float) this.activePlayer.Tackling / 99f;
    this.hitPower_Txt.text = this.activePlayer.HitPower.ToString();
    this.hitPower_Img.fillAmount = (float) this.activePlayer.HitPower / 99f;
    this.cover_Txt.text = this.activePlayer.Coverage.ToString();
    this.cover_Img.fillAmount = (float) this.activePlayer.Coverage / 99f;
    this.discipline_Txt.text = this.activePlayer.Discipline.ToString();
    this.discipline_Img.fillAmount = (float) this.activePlayer.Discipline / 99f;
    this.endurance_Txt.text = this.activePlayer.Endurance.ToString();
    this.endurance_Img.fillAmount = (float) this.activePlayer.Endurance / 99f;
    this.agility_Txt.text = this.activePlayer.Agility.ToString();
    this.agility_Img.fillAmount = (float) this.activePlayer.Agility / 99f;
    this.fitness_Txt.text = this.activePlayer.Fitness.ToString();
    this.fitness_Img.fillAmount = (float) this.activePlayer.Fitness / 99f;
    this.kickAccuracy_Txt.text = this.activePlayer.KickAccuracy.ToString();
    this.kickAccuracy_Img.fillAmount = (float) this.activePlayer.KickAccuracy / 99f;
    this.kickPower_Txt.text = this.activePlayer.KickPower.ToString();
    this.kickPower_Img.fillAmount = (float) this.activePlayer.KickPower / 99f;
  }

  public void IncreaseSpeed()
  {
    if (this.activePlayer.Speed >= 99)
      return;
    ++this.activePlayer.Speed;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseSpeed()
  {
    if (this.activePlayer.Speed <= 1)
      return;
    --this.activePlayer.Speed;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseAwareness()
  {
    if (this.activePlayer.Awareness >= 99)
      return;
    ++this.activePlayer.Awareness;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseAwareness()
  {
    if (this.activePlayer.Awareness <= 1)
      return;
    --this.activePlayer.Awareness;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseTackleBreaking()
  {
    if (this.activePlayer.TackleBreaking >= 99)
      return;
    ++this.activePlayer.TackleBreaking;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseTackleBreaking()
  {
    if (this.activePlayer.TackleBreaking <= 1)
      return;
    --this.activePlayer.TackleBreaking;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseFumbling()
  {
    if (this.activePlayer.Fumbling >= 99)
      return;
    ++this.activePlayer.Fumbling;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseFumbling()
  {
    if (this.activePlayer.Fumbling <= 1)
      return;
    --this.activePlayer.Fumbling;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseBlocking()
  {
    if (this.activePlayer.Blocking >= 99)
      return;
    ++this.activePlayer.Blocking;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseBlocking()
  {
    if (this.activePlayer.Blocking <= 1)
      return;
    --this.activePlayer.Blocking;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseThrowAccuracy()
  {
    if (this.activePlayer.ThrowAccuracy >= 99)
      return;
    ++this.activePlayer.ThrowAccuracy;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseThrowAccuracy()
  {
    if (this.activePlayer.ThrowAccuracy <= 1)
      return;
    --this.activePlayer.ThrowAccuracy;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseThrowPower()
  {
    if (this.activePlayer.ThrowPower >= 99)
      return;
    ++this.activePlayer.ThrowPower;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseThrowPower()
  {
    if (this.activePlayer.ThrowPower <= 1)
      return;
    --this.activePlayer.ThrowPower;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseKickAccuracy()
  {
    if (this.activePlayer.KickAccuracy >= 99)
      return;
    ++this.activePlayer.KickAccuracy;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseKickAccuracy()
  {
    if (this.activePlayer.KickAccuracy <= 1)
      return;
    --this.activePlayer.KickAccuracy;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseKickPower()
  {
    if (this.activePlayer.KickPower >= 99)
      return;
    ++this.activePlayer.KickPower;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseKickPower()
  {
    if (this.activePlayer.KickPower <= 1)
      return;
    --this.activePlayer.KickPower;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseBlockBreaking()
  {
    if (this.activePlayer.BlockBreaking >= 99)
      return;
    ++this.activePlayer.BlockBreaking;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseBlockBreaking()
  {
    if (this.activePlayer.BlockBreaking <= 1)
      return;
    --this.activePlayer.BlockBreaking;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseTackling()
  {
    if (this.activePlayer.Tackling >= 99)
      return;
    ++this.activePlayer.Tackling;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseTackling()
  {
    if (this.activePlayer.Tackling <= 1)
      return;
    --this.activePlayer.Tackling;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseFitness()
  {
    if (this.activePlayer.Fitness >= 99)
      return;
    ++this.activePlayer.Fitness;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseFitness()
  {
    if (this.activePlayer.Fitness <= 1)
      return;
    --this.activePlayer.Fitness;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseCoverage()
  {
    if (this.activePlayer.Coverage >= 99)
      return;
    ++this.activePlayer.Coverage;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseCoverage()
  {
    if (this.activePlayer.Coverage <= 1)
      return;
    --this.activePlayer.Coverage;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseHitPower()
  {
    if (this.activePlayer.HitPower >= 99)
      return;
    ++this.activePlayer.HitPower;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseHitPower()
  {
    if (this.activePlayer.HitPower <= 1)
      return;
    --this.activePlayer.HitPower;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseEndurance()
  {
    if (this.activePlayer.Endurance >= 99)
      return;
    ++this.activePlayer.Endurance;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseEndurance()
  {
    if (this.activePlayer.Endurance <= 1)
      return;
    --this.activePlayer.Endurance;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseAgility()
  {
    if (this.activePlayer.Agility >= 99)
      return;
    ++this.activePlayer.Agility;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseAgility()
  {
    if (this.activePlayer.Agility <= 1)
      return;
    --this.activePlayer.Agility;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseDiscipline()
  {
    if (this.activePlayer.Discipline >= 99)
      return;
    ++this.activePlayer.Discipline;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseDiscipline()
  {
    if (this.activePlayer.Discipline <= 1)
      return;
    --this.activePlayer.Discipline;
    this.UpdatePlayerAttributes();
  }

  public void IncreaseCatch()
  {
    if (this.activePlayer.Catching >= 99)
      return;
    ++this.activePlayer.Catching;
    this.UpdatePlayerAttributes();
  }

  public void DecreaseCatch()
  {
    if (this.activePlayer.Catching <= 1)
      return;
    --this.activePlayer.Catching;
    this.UpdatePlayerAttributes();
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || PopupLoadingScreen.self.IsVisible() || OnScreenKeyboard.instance.IsVisible() || !ControllerManagerTitle.self.usingController || this.IsRestoreTeamToDefaultWindowVisible() || this.IsCreateRandomTeamWindowVisible())
      return;
    float num1 = UserManager.instance.LeftStickY(Player.One);
    float num2 = UserManager.instance.LeftStickX(Player.One);
    if (this.IsPlayerListSectionVisible())
    {
      if (UserManager.instance.LeftBumperWasPressed(Player.One))
        this.ShowPrevPosition();
      else if (UserManager.instance.RightBumperWasPressed(Player.One))
        this.ShowNextPosition();
      if (!this.allowMove)
        return;
      if (this.selectedLineIndex != -1)
      {
        if ((double) num1 > 0.40000000596046448)
        {
          this.StartCoroutine(this.DisableMove());
          this.SelectPrevPlayer();
        }
        else if ((double) num1 < -0.40000000596046448)
        {
          this.StartCoroutine(this.DisableMove());
          this.SelectNextPlayer();
        }
        if ((double) num2 <= 0.40000000596046448 && !UserManager.instance.Action1WasPressed(Player.One))
          return;
        this.selectedLineIndex = -1;
        ControllerManagerTitle.self.SelectUIElement((Selectable) this.editPersonalInfo_Btn);
      }
      else
      {
        if ((double) num2 >= -0.40000000596046448)
          return;
        this.SelectFirstLine();
      }
    }
    else if (this.IsEditPersonalInformationVisible())
    {
      if (!this.allowMove)
        return;
      if ((double) num1 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPreviousPersonalInfoItem();
      }
      else if ((double) num1 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextPersonalInfoItem();
      }
      if ((double) num2 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.IncreaseSelectedPersonalInfoItem();
      }
      else
      {
        if ((double) num2 >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.DecreaseSelectedPersonalInfoItem();
      }
    }
    else
    {
      if (!this.IsEditAttributesVisible() || !this.allowMove)
        return;
      if ((double) num1 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPreviousAttribute();
      }
      else if ((double) num1 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextAttribute();
      }
      if ((double) num2 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.IncreaseSelectedAttribute();
      }
      else
      {
        if ((double) num2 >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.DecreaseSelectedAttribute();
      }
    }
  }

  private void SelectNextPlayer()
  {
    if (this.selectedLineIndex >= this.playersInList)
      return;
    ++this.selectedLineIndex;
    this.playerLines[this.selectedLineIndex].SelectLine();
    UISoundManager.instance.PlayButtonToggle();
    if (this.selectedLineIndex < this.beginScrollAtIndex)
      return;
    this.playerListScrollContents.anchoredPosition = new Vector2(0.0f, (float) (this.playerSlotRowHeight * (this.selectedLineIndex - this.beginScrollAtIndex + 1)));
  }

  private void SelectPrevPlayer()
  {
    if (this.selectedLineIndex <= 0)
      return;
    --this.selectedLineIndex;
    this.playerLines[this.selectedLineIndex].SelectLine();
    UISoundManager.instance.PlayButtonToggle();
    if (this.selectedLineIndex < this.beginScrollAtIndex - 1)
      return;
    this.playerListScrollContents.anchoredPosition = new Vector2(0.0f, (float) (this.playerSlotRowHeight * (this.selectedLineIndex - this.beginScrollAtIndex + 1)));
  }

  public void ShowNextPosition()
  {
    UISoundManager.instance.PlayTabSwipe();
    if (this.activePositionIndex + 1 < TeamData.ALL_GENERIC_POSITIONS.Count)
      ++this.activePositionIndex;
    else
      this.activePositionIndex = 0;
    this.FilterByPosition(TeamData.ALL_GENERIC_POSITIONS[this.activePositionIndex]);
  }

  public void ShowPrevPosition()
  {
    UISoundManager.instance.PlayTabSwipe();
    if (this.activePositionIndex - 1 >= 0)
      --this.activePositionIndex;
    else
      this.activePositionIndex = TeamData.ALL_GENERIC_POSITIONS.Count - 1;
    this.FilterByPosition(TeamData.ALL_GENERIC_POSITIONS[this.activePositionIndex]);
  }

  private void SelectPreviousPersonalInfoItem()
  {
    if (this.selectedEditingFieldIndex <= 0)
      return;
    this.SelectPersonalInfoItem(this.selectedEditingFieldIndex - 1);
  }

  private void SelectNextPersonalInfoItem()
  {
    if (this.selectedEditingFieldIndex >= this.personalInformation_Btns.Length - 1)
      return;
    this.SelectPersonalInfoItem(this.selectedEditingFieldIndex + 1);
  }

  private void SelectPersonalInfoItem(int index)
  {
    this.selectedEditingFieldIndex = index;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.personalInformation_Btns[this.selectedEditingFieldIndex]);
  }

  private void IncreaseSelectedPersonalInfoItem()
  {
    if (this.selectedEditingFieldIndex == 3)
      this.SetNextAge();
    else if (this.selectedEditingFieldIndex == 4)
      this.SetNextSkinColor();
    else if (this.selectedEditingFieldIndex == 5)
      this.SetNextPortrait();
    else if (this.selectedEditingFieldIndex == 6)
      this.SetNextHeight();
    else if (this.selectedEditingFieldIndex == 8)
    {
      this.ToggleVisor();
    }
    else
    {
      if (this.selectedEditingFieldIndex != 9)
        return;
      this.ToggleSleeves();
    }
  }

  private void DecreaseSelectedPersonalInfoItem()
  {
    if (this.selectedEditingFieldIndex == 3)
      this.SetPreviousAge();
    else if (this.selectedEditingFieldIndex == 4)
      this.SetPreviousSkinColor();
    else if (this.selectedEditingFieldIndex == 5)
      this.SetPreviousPortrait();
    else if (this.selectedEditingFieldIndex == 6)
      this.SetPreviousHeight();
    else if (this.selectedEditingFieldIndex == 8)
    {
      this.ToggleVisor();
    }
    else
    {
      if (this.selectedEditingFieldIndex != 9)
        return;
      this.ToggleSleeves();
    }
  }

  private void SelectNextAttribute()
  {
    if (this.selectedEditingFieldIndex >= this.attribute_Btns.Length - 1)
      return;
    ++this.selectedEditingFieldIndex;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.attribute_Btns[this.selectedEditingFieldIndex]);
    if (this.selectedEditingFieldIndex < this.beginAttributeScrollAtIndex)
      return;
    this.attributeListScrollContents.anchoredPosition = new Vector2(0.0f, (float) (this.attributeRowHeight * (this.selectedEditingFieldIndex - this.beginAttributeScrollAtIndex + 1)));
  }

  private void SelectPreviousAttribute()
  {
    if (this.selectedEditingFieldIndex <= 0)
      return;
    --this.selectedEditingFieldIndex;
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.attribute_Btns[this.selectedEditingFieldIndex]);
    if (this.selectedEditingFieldIndex < this.beginAttributeScrollAtIndex - 1)
      return;
    this.attributeListScrollContents.anchoredPosition = new Vector2(0.0f, (float) (this.attributeRowHeight * (this.selectedEditingFieldIndex - this.beginAttributeScrollAtIndex + 1)));
  }

  private void IncreaseSelectedAttribute()
  {
    if (this.selectedEditingFieldIndex == 0)
      this.IncreaseSpeed();
    else if (this.selectedEditingFieldIndex == 1)
      this.IncreaseAwareness();
    else if (this.selectedEditingFieldIndex == 2)
      this.IncreaseTackleBreaking();
    else if (this.selectedEditingFieldIndex == 3)
      this.IncreaseFumbling();
    else if (this.selectedEditingFieldIndex == 4)
      this.IncreaseCatch();
    else if (this.selectedEditingFieldIndex == 5)
      this.IncreaseBlocking();
    else if (this.selectedEditingFieldIndex == 6)
      this.IncreaseThrowAccuracy();
    else if (this.selectedEditingFieldIndex == 7)
      this.IncreaseThrowPower();
    else if (this.selectedEditingFieldIndex == 8)
      this.IncreaseBlockBreaking();
    else if (this.selectedEditingFieldIndex == 9)
      this.IncreaseTackling();
    else if (this.selectedEditingFieldIndex == 10)
      this.IncreaseHitPower();
    else if (this.selectedEditingFieldIndex == 11)
      this.IncreaseCoverage();
    else if (this.selectedEditingFieldIndex == 12)
      this.IncreaseDiscipline();
    else if (this.selectedEditingFieldIndex == 13)
      this.IncreaseEndurance();
    else if (this.selectedEditingFieldIndex == 14)
      this.IncreaseAgility();
    else if (this.selectedEditingFieldIndex == 15)
      this.IncreaseFitness();
    else if (this.selectedEditingFieldIndex == 16)
    {
      this.IncreaseKickAccuracy();
    }
    else
    {
      if (this.selectedEditingFieldIndex != 17)
        return;
      this.IncreaseKickPower();
    }
  }

  private void DecreaseSelectedAttribute()
  {
    if (this.selectedEditingFieldIndex == 0)
      this.DecreaseSpeed();
    else if (this.selectedEditingFieldIndex == 1)
      this.DecreaseAwareness();
    else if (this.selectedEditingFieldIndex == 2)
      this.DecreaseTackleBreaking();
    else if (this.selectedEditingFieldIndex == 3)
      this.DecreaseFumbling();
    else if (this.selectedEditingFieldIndex == 4)
      this.DecreaseCatch();
    else if (this.selectedEditingFieldIndex == 5)
      this.DecreaseBlocking();
    else if (this.selectedEditingFieldIndex == 6)
      this.DecreaseThrowAccuracy();
    else if (this.selectedEditingFieldIndex == 7)
      this.DecreaseThrowPower();
    else if (this.selectedEditingFieldIndex == 8)
      this.DecreaseBlockBreaking();
    else if (this.selectedEditingFieldIndex == 9)
      this.DecreaseTackling();
    else if (this.selectedEditingFieldIndex == 10)
      this.DecreaseHitPower();
    else if (this.selectedEditingFieldIndex == 11)
      this.DecreaseCoverage();
    else if (this.selectedEditingFieldIndex == 12)
      this.DecreaseDiscipline();
    else if (this.selectedEditingFieldIndex == 13)
      this.DecreaseEndurance();
    else if (this.selectedEditingFieldIndex == 14)
      this.DecreaseAgility();
    else if (this.selectedEditingFieldIndex == 15)
      this.DecreaseFitness();
    else if (this.selectedEditingFieldIndex == 16)
    {
      this.DecreaseKickAccuracy();
    }
    else
    {
      if (this.selectedEditingFieldIndex != 17)
        return;
      this.DecreaseKickPower();
    }
  }

  private IEnumerator DisableMove()
  {
    this.allowMove = false;
    yield return (object) this._disableMove;
    this.allowMove = true;
  }
}
