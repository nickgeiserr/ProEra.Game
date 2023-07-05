// Decompiled with JetBrains decompiler
// Type: ControllerAssignmentManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerAssignmentManager : MonoBehaviour
{
  public static ControllerAssignmentManager instance;
  [SerializeField]
  private GameObject mainWindow;
  [SerializeField]
  private GameObject controller1_left;
  [SerializeField]
  private GameObject controller1_middle;
  [SerializeField]
  private GameObject controller1_right;
  [SerializeField]
  private GameObject controller2_left;
  [SerializeField]
  private GameObject controller2_middle;
  [SerializeField]
  private GameObject controller2_right;
  [SerializeField]
  private Image p1TeamBG_Img;
  [SerializeField]
  private Image p1TeamLogo_Img;
  [SerializeField]
  private Image p2TeamBG_Img;
  [SerializeField]
  private Image p2TeamLogo_Img;
  [SerializeField]
  private TextMeshProUGUI status_Txt;
  private bool allowControllerInput;
  private WaitForSecondsRealtime disableMove_WFS;

  private void Start()
  {
    if (!((Object) ControllerAssignmentManager.instance == (Object) null))
      return;
    ControllerAssignmentManager.instance = this;
    this.HideWindow();
    this.allowControllerInput = true;
    this.disableMove_WFS = new WaitForSecondsRealtime(0.2f);
  }

  private void Update() => this.ManageControllerInput();

  public bool IsVisible() => this.mainWindow.activeInHierarchy;

  public void ShowWindow()
  {
    if (this.IsVisible())
      return;
    ProEra.Game.Sources.UI.PauseWindow.ShowWindow();
    this.StartCoroutine(this.FinishShowingWindow());
  }

  private IEnumerator FinishShowingWindow()
  {
    yield return (object) null;
    yield return (object) null;
    ControllerManagerGame.self.DeselectCurrentUIElement();
    this.mainWindow.SetActive(true);
    this.HideAllController1Images();
    this.HideAllController2Images();
    if (PersistentData.userIsHome)
    {
      this.p1TeamBG_Img.color = PersistentData.GetHomeBackgroundColor();
      this.p1TeamLogo_Img.sprite = PersistentData.GetHomeSmallLogo();
      this.p2TeamBG_Img.color = PersistentData.GetAwayBackgroundColor();
      this.p2TeamLogo_Img.sprite = PersistentData.GetAwaySmallLogo();
    }
    else
    {
      this.p1TeamBG_Img.color = PersistentData.GetAwayBackgroundColor();
      this.p1TeamLogo_Img.sprite = PersistentData.GetAwaySmallLogo();
      this.p2TeamBG_Img.color = PersistentData.GetHomeBackgroundColor();
      this.p2TeamLogo_Img.sprite = PersistentData.GetHomeSmallLogo();
    }
    this.controller1_middle.SetActive(true);
    this.controller2_middle.SetActive(true);
    this.status_Txt.text = "";
  }

  public void HideWindow() => this.mainWindow.SetActive(false);

  private void HideAllController1Images()
  {
    this.controller1_left.SetActive(false);
    this.controller1_middle.SetActive(false);
    this.controller1_right.SetActive(false);
  }

  private void HideAllController2Images()
  {
    this.controller2_left.SetActive(false);
    this.controller2_middle.SetActive(false);
    this.controller2_right.SetActive(false);
  }

  private void MoveController1_Left()
  {
    if (this.controller1_middle.activeInHierarchy && !this.controller2_left.activeInHierarchy)
    {
      this.HideAllController1Images();
      this.controller1_left.SetActive(true);
    }
    else
    {
      if (!this.controller1_right.activeInHierarchy)
        return;
      this.HideAllController1Images();
      this.controller1_middle.SetActive(true);
    }
  }

  private void MoveController1_Right()
  {
    if (this.controller1_left.activeInHierarchy)
    {
      this.HideAllController1Images();
      this.controller1_middle.SetActive(true);
    }
    else
    {
      if (!this.controller1_middle.activeInHierarchy || this.controller2_right.activeInHierarchy)
        return;
      this.HideAllController1Images();
      this.controller1_right.SetActive(true);
    }
  }

  private void MoveController2_Left()
  {
    if (this.controller2_middle.activeInHierarchy && !this.controller1_left.activeInHierarchy)
    {
      this.HideAllController2Images();
      this.controller2_left.SetActive(true);
    }
    else
    {
      if (!this.controller2_right.activeInHierarchy)
        return;
      this.HideAllController2Images();
      this.controller2_middle.SetActive(true);
    }
  }

  private void MoveController2_Right()
  {
    if (this.controller2_left.activeInHierarchy)
    {
      this.HideAllController2Images();
      this.controller2_middle.SetActive(true);
    }
    else
    {
      if (!this.controller2_middle.activeInHierarchy || this.controller1_right.activeInHierarchy)
        return;
      this.HideAllController2Images();
      this.controller2_right.SetActive(true);
    }
  }

  private void ValidateControllerSetup()
  {
    if (this.controller1_middle.activeInHierarchy || this.controller2_middle.activeInHierarchy)
    {
      this.status_Txt.text = "EACH TEAM MUST HAVE A PLAYER ASSIGNED";
    }
    else
    {
      if (!this.controller1_left.activeInHierarchy)
        UserManager.instance.SwapUsersRealIndex();
      this.HideWindow();
      ProEra.Game.Sources.UI.PauseWindow.ShowWindow();
    }
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || !this.allowControllerInput)
      return;
    float num1 = UserManager.instance.LeftStickX(Player.One);
    float num2 = UserManager.instance.LeftStickX(Player.Two);
    if ((double) num1 < -0.5)
    {
      this.MoveController1_Left();
      this.StartCoroutine(this.DisableMove());
    }
    else if ((double) num1 > 0.5)
    {
      this.MoveController1_Right();
      this.StartCoroutine(this.DisableMove());
    }
    if ((double) num2 < -0.5)
    {
      this.MoveController2_Left();
      this.StartCoroutine(this.DisableMove());
    }
    else if ((double) num2 > 0.5)
    {
      this.MoveController2_Right();
      this.StartCoroutine(this.DisableMove());
    }
    if (!UserManager.instance.Action1WasPressed(Player.One) && !UserManager.instance.Action1WasPressed(Player.Two))
      return;
    this.ValidateControllerSetup();
  }

  private IEnumerator DisableMove()
  {
    this.allowControllerInput = false;
    yield return (object) this.disableMove_WFS;
    this.allowControllerInput = true;
  }
}
