// Decompiled with JetBrains decompiler
// Type: TeamSuiteManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class TeamSuiteManager : MonoBehaviour
{
  [Header("Main Section")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private Image gradientCover_Img;
  [Header("Create / Edit Select Window")]
  [SerializeField]
  private CanvasGroup createEditSelectWindow_CG;
  [SerializeField]
  private UnityEngine.UI.Button createNewTeam_Btn;
  [Header("Select Team To Edit Window")]
  [SerializeField]
  private CanvasGroup selectTeamToEditWindow_CG;
  [SerializeField]
  private TextMeshProUGUI pageDisplay_Txt;
  [SerializeField]
  private TeamSuiteTeamSelectItem[] teamSelectItems;
  private int pageIndex;
  [Header("Team Suite Dashboard")]
  [SerializeField]
  private CanvasGroup dashboard_CG;
  [SerializeField]
  private TextMeshProUGUI dashboardTeam_Txt;
  [SerializeField]
  private UnityEngine.UI.Button editDetails_Btn;
  [SerializeField]
  private Image dashboardTeamLogo_Img;
  [Header("Editors")]
  public TeamDetailsEditor teamDetailsEditor;
  public TeamColorsEditor teamColorsEditor;
  public PlayerEditor playerEditor;
  public LogoEditor logoEditor;
  public FieldEditor fieldEditor;
  private TeamData selectedTeamData;

  private void Start()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void Init()
  {
    this.HideCreateEditSelectWindow();
    this.HideSelectTeamToEditWindow();
    this.HideDashboard();
    this.teamDetailsEditor.Init();
    this.teamColorsEditor.Init();
    this.playerEditor.Init();
    this.logoEditor.Init();
    this.fieldEditor.Init();
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    BottomBarManager.instance.SetControllerButtonGuide(3);
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.15f);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    if (this.IsSelectTeamToEditWindowVisible())
    {
      this.HideSelectTeamToEditWindow();
      this.ShowCreateEditSelctWindow();
    }
    else if (this.IsCreateEditSelectWindowVisible() || this.IsDashboardVisible())
    {
      this.HideDashboard();
      this.HideCreateEditSelectWindow();
      this.HideWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowBottomSection();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowCreateSection();
    }
    else if (this.teamDetailsEditor.IsVisible())
      this.teamDetailsEditor.ReturnToPreviousMenu();
    else if (this.teamColorsEditor.IsVisible())
      this.teamColorsEditor.ReturnToPreviousMenu();
    else if (this.playerEditor.IsVisible())
      this.playerEditor.ReturnToPreviousMenu();
    else if (this.logoEditor.IsVisible())
    {
      this.logoEditor.ReturnToPreviousMenu();
    }
    else
    {
      if (!this.fieldEditor.IsVisible())
        return;
      this.fieldEditor.ReturnToPreviousMenu();
    }
  }

  public void ShowCreateEditSelctWindow()
  {
    LeanTween.alphaCanvas(this.createEditSelectWindow_CG, 1f, 0.3f);
    this.createEditSelectWindow_CG.blocksRaycasts = true;
    BottomBarManager.instance.ShowBackButton();
    BottomBarManager.instance.SetControllerButtonGuide(3);
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.createNewTeam_Btn);
  }

  private void HideCreateEditSelectWindow()
  {
    LeanTween.alphaCanvas(this.createEditSelectWindow_CG, 0.0f, 0.3f);
    this.createEditSelectWindow_CG.blocksRaycasts = false;
  }

  private bool IsCreateEditSelectWindowVisible() => (double) this.createEditSelectWindow_CG.alpha > 0.0;

  public void Select_CreateNewTeam()
  {
  }

  public void Select_EditExistingTeam()
  {
    this.HideCreateEditSelectWindow();
    this.ShowSelectTeamToEditWindow();
  }

  public void ShowSelectTeamToEditWindow()
  {
    LeanTween.alphaCanvas(this.selectTeamToEditWindow_CG, 1f, 0.3f);
    this.selectTeamToEditWindow_CG.blocksRaycasts = true;
    this.SetPage(0);
  }

  public void HideSelectTeamToEditWindow()
  {
    LeanTween.alphaCanvas(this.selectTeamToEditWindow_CG, 0.0f, 0.3f);
    this.selectTeamToEditWindow_CG.blocksRaycasts = false;
  }

  public bool IsSelectTeamToEditWindowVisible() => (double) this.selectTeamToEditWindow_CG.alpha > 0.0;

  public void SelectTeamForEditing(int teamIndex)
  {
    this.selectedTeamData = TeamDataCache.GetTeam(teamIndex);
    this.HideSelectTeamToEditWindow();
    this.ShowDashboard();
  }

  public void SetPage(int _pageIndex)
  {
    int num1 = 8;
    int totalTeams = AssetManager.TotalTeams;
    int num2 = Mathf.CeilToInt((float) totalTeams / (float) num1);
    if (_pageIndex >= num2 || _pageIndex < 0)
      return;
    this.pageIndex = _pageIndex;
    this.pageDisplay_Txt.text = "PAGE " + (this.pageIndex + 1).ToString() + "/" + num2.ToString();
    int num3 = this.pageIndex * num1;
    int num4 = Mathf.Min(num3 + num1, totalTeams) - 1 - num3 + 1;
    for (int index = 0; index < num4; ++index)
      this.teamSelectItems[index].SetTeamData(TeamDataCache.GetTeam(num3 + index));
    for (int index = num4; index < this.teamSelectItems.Length; ++index)
      this.teamSelectItems[index].HideWindow();
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.teamSelectItems[0].GetButton());
  }

  public void SetNextPage() => this.SetPage(this.pageIndex + 1);

  public void SetPreviousPage() => this.SetPage(this.pageIndex - 1);

  public void ShowDashboard()
  {
    LeanTween.alphaCanvas(this.dashboard_CG, 1f, 0.3f);
    this.dashboard_CG.blocksRaycasts = true;
    this.dashboardTeam_Txt.text = this.selectedTeamData.GetFullDisplayName();
    this.dashboardTeamLogo_Img.sprite = this.selectedTeamData.GetLargeLogo();
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.editDetails_Btn);
  }

  public void HideDashboard()
  {
    LeanTween.alphaCanvas(this.dashboard_CG, 0.0f, 0.3f);
    this.dashboard_CG.blocksRaycasts = false;
  }

  public bool IsDashboardVisible() => (double) this.dashboard_CG.alpha > 0.0;

  public void Select_EditDetails()
  {
    this.HideDashboard();
    this.teamDetailsEditor.ShowWindow(this.selectedTeamData);
  }

  public void Select_EditColors()
  {
    this.HideDashboard();
    this.teamColorsEditor.ShowWindow(this.selectedTeamData);
  }

  public void Select_EditLogo()
  {
    this.HideDashboard();
    this.logoEditor.ShowWindow(this.selectedTeamData);
  }

  public void Select_EditUniforms() => MonoBehaviour.print((object) "Selecting Edit Uniforms");

  public void Select_EditRoster()
  {
    this.HideDashboard();
    this.playerEditor.ShowWindow(this.selectedTeamData);
  }

  public void Select_EditField()
  {
    this.HideDashboard();
    this.fieldEditor.ShowWindow(this.selectedTeamData);
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || PopupLoadingScreen.self.IsVisible() || OnScreenKeyboard.instance.IsVisible() || !ControllerManagerTitle.self.usingController || !this.IsSelectTeamToEditWindowVisible())
      return;
    if (UserManager.instance.RightBumperWasPressed(Player.One))
      this.SetNextPage();
    if (!UserManager.instance.LeftBumperWasPressed(Player.One))
      return;
    this.SetPreviousPage();
  }
}
