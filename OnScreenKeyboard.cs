// Decompiled with JetBrains decompiler
// Type: OnScreenKeyboard
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenKeyboard : MonoBehaviour
{
  public static OnScreenKeyboard instance;
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private UnityEngine.UI.Button selectOnOpen_Btn;
  [SerializeField]
  private TextMeshProUGUI inputText_Txt;
  private int maxCharacters;
  private bool allowNumberInput;
  private bool allowNonNumberInput;
  private bool allowOnlyHexInput;

  private void Awake()
  {
    if ((Object) OnScreenKeyboard.instance == (Object) null)
      OnScreenKeyboard.instance = this;
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  private void Update() => this.ManageKeyboardInput();

  public void ShowWindow(
    int _maxCharacters = 25,
    bool _allowNumberInput = true,
    bool _allowNonNumberInput = true,
    bool _allowOnlyHexInput = false,
    string startingText = "")
  {
    this.maxCharacters = _maxCharacters;
    this.allowNumberInput = _allowNumberInput;
    this.allowNonNumberInput = _allowNonNumberInput;
    this.allowOnlyHexInput = _allowOnlyHexInput;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    this.inputText_Txt.text = startingText;
    BottomBarManager.instance.HideAllControllerButtonGuides();
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.selectOnOpen_Btn);
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = false;
    if (SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.playerEditor.IsVisible())
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.playerEditor.OnScreenKeyboardClosed();
    else if (SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.teamDetailsEditor.IsVisible())
    {
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.teamDetailsEditor.OnScreenKeyboardClosed();
    }
    else
    {
      if (!OnScreenColorSelector.instance.IsVisible())
        return;
      OnScreenColorSelector.instance.OnScreenKeyboardClosed();
    }
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu() => this.HideWindow();

  public void AddCharacter(string c)
  {
    if (!this.allowNonNumberInput || this.inputText_Txt.text.Length >= this.maxCharacters)
      return;
    if (this.allowOnlyHexInput)
    {
      bool flag = false;
      if (c == "A" || c == "B" || c == "C" || c == "D" || c == "E" || c == "F")
        flag = true;
      if (!flag)
        return;
    }
    this.inputText_Txt.text += c;
  }

  public void AddNumber(string n)
  {
    if (!this.allowNumberInput || this.inputText_Txt.text.Length >= this.maxCharacters)
      return;
    this.inputText_Txt.text += n;
  }

  public void SendBackspace()
  {
    if (this.inputText_Txt.text.Length <= 0 || this.inputText_Txt.text.Length == 1 && this.inputText_Txt.text.Substring(0, 1) == "#")
      return;
    this.inputText_Txt.text = this.inputText_Txt.text.Remove(this.inputText_Txt.text.Length - 1);
  }

  public void SendSpace() => this.AddCharacter(" ");

  public void Cancel() => this.HideWindow();

  public void Accept()
  {
    if (this.inputText_Txt.text == "")
      return;
    if (SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.playerEditor.IsVisible())
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.playerEditor.OnScreenKeyboardTextAccepted(this.inputText_Txt.text);
    else if (SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.teamDetailsEditor.IsVisible())
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.teamDetailsEditor.OnScreenKeyboardTextAccepted(this.inputText_Txt.text);
    else if (OnScreenColorSelector.instance.IsVisible())
      OnScreenColorSelector.instance.OnScreenKeyboardTextAccepted(this.inputText_Txt.text);
    this.HideWindow();
  }

  public void ManageKeyboardInput()
  {
    if (!this.IsVisible() || ControllerManagerTitle.self.usingController)
      return;
    if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
      this.AddNumber("0");
    else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
      this.AddNumber("1");
    else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
      this.AddNumber("2");
    else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
      this.AddNumber("3");
    else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
      this.AddNumber("4");
    else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
      this.AddNumber("5");
    else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
      this.AddNumber("6");
    else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
      this.AddNumber("7");
    else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
      this.AddNumber("8");
    else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
      this.AddNumber("9");
    else if (Input.GetKeyDown(KeyCode.A))
      this.AddCharacter("A");
    else if (Input.GetKeyDown(KeyCode.B))
      this.AddCharacter("B");
    else if (Input.GetKeyDown(KeyCode.C))
      this.AddCharacter("C");
    else if (Input.GetKeyDown(KeyCode.D))
      this.AddCharacter("D");
    else if (Input.GetKeyDown(KeyCode.E))
      this.AddCharacter("E");
    else if (Input.GetKeyDown(KeyCode.F))
      this.AddCharacter("F");
    else if (Input.GetKeyDown(KeyCode.G))
      this.AddCharacter("G");
    else if (Input.GetKeyDown(KeyCode.H))
      this.AddCharacter("H");
    else if (Input.GetKeyDown(KeyCode.I))
      this.AddCharacter("I");
    else if (Input.GetKeyDown(KeyCode.J))
      this.AddCharacter("J");
    else if (Input.GetKeyDown(KeyCode.K))
      this.AddCharacter("K");
    else if (Input.GetKeyDown(KeyCode.L))
      this.AddCharacter("L");
    else if (Input.GetKeyDown(KeyCode.M))
      this.AddCharacter("M");
    else if (Input.GetKeyDown(KeyCode.N))
      this.AddCharacter("N");
    else if (Input.GetKeyDown(KeyCode.O))
      this.AddCharacter("O");
    else if (Input.GetKeyDown(KeyCode.P))
      this.AddCharacter("P");
    else if (Input.GetKeyDown(KeyCode.Q))
      this.AddCharacter("Q");
    else if (Input.GetKeyDown(KeyCode.R))
      this.AddCharacter("R");
    else if (Input.GetKeyDown(KeyCode.S))
      this.AddCharacter("S");
    else if (Input.GetKeyDown(KeyCode.T))
      this.AddCharacter("T");
    else if (Input.GetKeyDown(KeyCode.U))
      this.AddCharacter("U");
    else if (Input.GetKeyDown(KeyCode.V))
      this.AddCharacter("V");
    else if (Input.GetKeyDown(KeyCode.W))
      this.AddCharacter("W");
    else if (Input.GetKeyDown(KeyCode.X))
      this.AddCharacter("X");
    else if (Input.GetKeyDown(KeyCode.Y))
      this.AddCharacter("Y");
    else if (Input.GetKeyDown(KeyCode.Z))
    {
      this.AddCharacter("Z");
    }
    else
    {
      if (!Input.GetKeyDown(KeyCode.KeypadEnter) && !Input.GetKey(KeyCode.Return))
        return;
      this.Accept();
    }
  }
}
