// Decompiled with JetBrains decompiler
// Type: OnScreenColorSelector
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using System.Collections;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenColorSelector : MonoBehaviour
{
  public static OnScreenColorSelector instance;
  [Header("Main Section")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  private TeamData selectedTeamData;
  [Header("RGB Section")]
  [SerializeField]
  private TextMeshProUGUI red_Txt;
  [SerializeField]
  private TextMeshProUGUI green_Txt;
  [SerializeField]
  private TextMeshProUGUI blue_Txt;
  [SerializeField]
  private Image red_Img;
  [SerializeField]
  private Image green_Img;
  [SerializeField]
  private Image blue_Img;
  [SerializeField]
  private TextMeshProUGUI hex_Txt;
  [SerializeField]
  private UnityEngine.UI.Button selectOnOpen_Btn;
  [SerializeField]
  private GameObject red_GO;
  [SerializeField]
  private GameObject green_GO;
  [SerializeField]
  private GameObject blue_GO;
  private bool allowMove;
  [Header("Quick Access Colors")]
  [SerializeField]
  private Image primaryColor_Img;
  [SerializeField]
  private Image secondaryColor_Img;
  [SerializeField]
  private Image alternateColor_Img;
  [Header("Color Display Section")]
  [SerializeField]
  private Image customColor_Img;
  private int red;
  private int green;
  private int blue;
  private WaitForSecondsRealtime _disableMove = new WaitForSecondsRealtime(0.2f);

  private void Awake()
  {
    if ((Object) OnScreenColorSelector.instance == (Object) null)
      OnScreenColorSelector.instance = this;
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow(TeamData teamData, Color defaultColor)
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    BottomBarManager.instance.ShowBackButton();
    BottomBarManager.instance.HideAllControllerButtonGuides();
    ControllerManagerTitle.self.DeselectCurrentUIElement();
    this.selectedTeamData = teamData;
    this.allowMove = true;
    this.primaryColor_Img.color = this.selectedTeamData.GetPrimaryColor();
    this.secondaryColor_Img.color = this.selectedTeamData.GetSecondaryColor();
    this.alternateColor_Img.color = this.selectedTeamData.GetAlternateColor();
    this.SetEditingColor(defaultColor);
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.selectOnOpen_Btn);
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.15f);
    this.mainWindow_CG.blocksRaycasts = false;
    if (SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.teamColorsEditor.IsVisible())
    {
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.teamColorsEditor.OnScreenColorSelectorClosed();
    }
    else
    {
      if (!SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.logoEditor.IsVisible())
        return;
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.logoEditor.OnScreenColorSelectorClosed();
    }
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu() => this.HideWindow();

  public void OpenHexEditor() => OnScreenKeyboard.instance.ShowWindow(7, _allowOnlyHexInput: true, startingText: "#");

  public void SelectColor(Color color)
  {
    if (SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.teamColorsEditor.IsVisible())
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.teamColorsEditor.OnScreenColorSelectorAccepted(color);
    else if (SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.logoEditor.IsVisible())
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.logoEditor.OnScreenColorSelectorAccepted(color);
    this.HideWindow();
  }

  public void AcceptColor() => this.SelectColor(this.customColor_Img.color);

  public void OnScreenKeyboardTextAccepted(string keyboardInputValue)
  {
    if (keyboardInputValue.Length < 7)
      return;
    this.SetEditingColor(TeamColorsEditor.ConvertHexToColor(keyboardInputValue));
  }

  public void OnScreenKeyboardClosed() => ControllerManagerTitle.self.SelectUIElement((Selectable) this.selectOnOpen_Btn);

  public void ChangeRedValue(int changeAmt)
  {
    this.red = Mathf.Clamp(this.red + changeAmt, 0, (int) byte.MaxValue);
    this.SetColorDisplay();
  }

  public void ChangeGreenValue(int changeAmt)
  {
    this.green = Mathf.Clamp(this.green + changeAmt, 0, (int) byte.MaxValue);
    this.SetColorDisplay();
  }

  public void ChangeBlueValue(int changeAmt)
  {
    this.blue = Mathf.Clamp(this.blue + changeAmt, 0, (int) byte.MaxValue);
    this.SetColorDisplay();
  }

  public void SelectPrimaryColor() => this.SelectColor(this.primaryColor_Img.color);

  public void SelectSecondaryColor() => this.SelectColor(this.secondaryColor_Img.color);

  public void SelectAlternateColor() => this.SelectColor(this.alternateColor_Img.color);

  public void SelectWhiteColor() => this.SelectColor(Color.white);

  public void SelectBlackColor() => this.SelectColor(Color.black);

  public void SelectGrayColor() => this.SelectColor(Color.gray);

  private void SetEditingColor(Color defaultColor)
  {
    this.red = Mathf.RoundToInt(defaultColor.r * (float) byte.MaxValue);
    this.green = Mathf.RoundToInt(defaultColor.g * (float) byte.MaxValue);
    this.blue = Mathf.RoundToInt(defaultColor.b * (float) byte.MaxValue);
    this.SetColorDisplay();
  }

  public void SetColorDisplay()
  {
    float r = (float) this.red / (float) byte.MaxValue;
    float g = (float) this.green / (float) byte.MaxValue;
    float b = (float) this.blue / (float) byte.MaxValue;
    this.red_Txt.text = this.red.ToString();
    this.green_Txt.text = this.green.ToString();
    this.blue_Txt.text = this.blue.ToString();
    this.red_Img.fillAmount = r;
    this.green_Img.fillAmount = g;
    this.blue_Img.fillAmount = b;
    this.customColor_Img.color = new Color(r, g, b);
    this.hex_Txt.text = "#" + ColorUtility.ToHtmlStringRGB(this.customColor_Img.color);
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || PopupLoadingScreen.self.IsVisible() || OnScreenKeyboard.instance.IsVisible() || !ControllerManagerTitle.self.usingController || !this.allowMove)
      return;
    float num = UserManager.instance.LeftStickX(Player.One);
    GameObject selectedUiElement = ControllerManagerTitle.self.GetCurrentSelectedUIElement();
    if ((Object) selectedUiElement == (Object) this.red_GO)
    {
      if (UserManager.instance.LeftBumperWasPressed(Player.One))
        this.ChangeRedValue(-10);
      else if (UserManager.instance.RightBumperWasPressed(Player.One))
        this.ChangeRedValue(10);
      if ((double) num < -0.5)
      {
        this.ChangeRedValue(-1);
        this.StartCoroutine(this.DisableMove());
      }
      else
      {
        if ((double) num <= 0.5)
          return;
        this.ChangeRedValue(1);
        this.StartCoroutine(this.DisableMove());
      }
    }
    else if ((Object) selectedUiElement == (Object) this.green_GO)
    {
      if (UserManager.instance.LeftBumperWasPressed(Player.One))
        this.ChangeGreenValue(-10);
      else if (UserManager.instance.RightBumperWasPressed(Player.One))
        this.ChangeGreenValue(10);
      if ((double) num < -0.5)
      {
        this.ChangeGreenValue(-1);
        this.StartCoroutine(this.DisableMove());
      }
      else
      {
        if ((double) num <= 0.5)
          return;
        this.ChangeGreenValue(1);
        this.StartCoroutine(this.DisableMove());
      }
    }
    else
    {
      if (!((Object) selectedUiElement == (Object) this.blue_GO))
        return;
      if (UserManager.instance.LeftBumperWasPressed(Player.One))
        this.ChangeBlueValue(-10);
      else if (UserManager.instance.RightBumperWasPressed(Player.One))
        this.ChangeBlueValue(10);
      if ((double) num < -0.5)
      {
        this.ChangeBlueValue(-1);
        this.StartCoroutine(this.DisableMove());
      }
      else
      {
        if ((double) num <= 0.5)
          return;
        this.ChangeBlueValue(1);
        this.StartCoroutine(this.DisableMove());
      }
    }
  }

  private IEnumerator DisableMove()
  {
    this.allowMove = false;
    yield return (object) this._disableMove;
    this.allowMove = true;
  }
}
