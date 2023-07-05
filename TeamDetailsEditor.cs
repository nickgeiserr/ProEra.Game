// Decompiled with JetBrains decompiler
// Type: TeamDetailsEditor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class TeamDetailsEditor : MonoBehaviour
{
  [Header("Main Section")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private GameObject teamCity_GO;
  [SerializeField]
  private GameObject teamName_GO;
  [SerializeField]
  private GameObject teamAbbreviation_GO;
  [SerializeField]
  private GameObject accept_GO;
  [SerializeField]
  private TextMeshProUGUI teamCity_Txt;
  [SerializeField]
  private TextMeshProUGUI teamName_Txt;
  [SerializeField]
  private TextMeshProUGUI teamAbbreviation_Txt;
  [SerializeField]
  private UnityEngine.UI.Button selectOnOpen_Btn;
  [SerializeField]
  private Image teamLogo_Img;
  private TeamData selectedTeamData;
  private TeamDetailsEditFields editingField;

  private void Start()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void Init() => this.editingField = TeamDetailsEditFields.None;

  private void Update() => this.ManageControllerInput();

  public void ShowWindow(TeamData editingTeam)
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    this.selectedTeamData = editingTeam;
    this.teamLogo_Img.sprite = this.selectedTeamData.GetLargeLogo();
    this.SetTeamInformation();
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.selectOnOpen_Btn);
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.15f);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowDashboard();
  }

  private void SetTeamInformation()
  {
    this.teamCity_Txt.text = this.selectedTeamData.GetCity().ToUpper();
    this.teamName_Txt.text = this.selectedTeamData.GetName().ToUpper();
    this.teamAbbreviation_Txt.text = this.selectedTeamData.GetAbbreviation().ToUpper();
  }

  public void SelectEdit_City()
  {
    this.editingField = TeamDetailsEditFields.TeamCity;
    OnScreenKeyboard.instance.ShowWindow(_allowNumberInput: false);
  }

  public void SelectEdit_Name()
  {
    this.editingField = TeamDetailsEditFields.TeamName;
    OnScreenKeyboard.instance.ShowWindow(_allowNumberInput: false);
  }

  public void SelectEdit_Abbreviation()
  {
    this.editingField = TeamDetailsEditFields.Abbreviation;
    OnScreenKeyboard.instance.ShowWindow(3, false);
  }

  public void OnScreenKeyboardTextAccepted(string keyboardInputValue)
  {
    if (this.editingField == TeamDetailsEditFields.TeamCity)
      this.selectedTeamData.SetCity(keyboardInputValue);
    else if (this.editingField == TeamDetailsEditFields.TeamName)
      this.selectedTeamData.SetName(keyboardInputValue);
    else if (this.editingField == TeamDetailsEditFields.Abbreviation)
      this.selectedTeamData.SetAbbreviation(keyboardInputValue);
    this.SetTeamInformation();
  }

  public void OnScreenKeyboardClosed() => ControllerManagerTitle.self.SelectUIElement((Selectable) this.selectOnOpen_Btn);

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || PopupLoadingScreen.self.IsVisible() || OnScreenKeyboard.instance.IsVisible() || !ControllerManagerTitle.self.usingController)
      return;
    GameObject selectedUiElement = ControllerManagerTitle.self.GetCurrentSelectedUIElement();
    if (!UserManager.instance.Action1WasPressed(Player.One))
      return;
    if ((Object) selectedUiElement == (Object) this.teamCity_GO)
      this.SelectEdit_City();
    else if ((Object) selectedUiElement == (Object) this.teamName_GO)
      this.SelectEdit_Name();
    else if ((Object) selectedUiElement == (Object) this.teamAbbreviation_GO)
    {
      this.SelectEdit_Abbreviation();
    }
    else
    {
      if (!((Object) selectedUiElement == (Object) this.accept_GO))
        return;
      this.ReturnToPreviousMenu();
    }
  }
}
