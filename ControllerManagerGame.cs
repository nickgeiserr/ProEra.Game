// Decompiled with JetBrains decompiler
// Type: ControllerManagerGame
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using ProEra.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerManagerGame : MonoBehaviour
{
  public static ControllerManagerGame self;
  public FeedbackManager feedbackManagerScript;
  public static bool usingController;
  public static bool playSelectedWithCont;
  private GUIManager gui;
  private bool isInitialized;

  private void Awake()
  {
    ControllerManagerGame.usingController = (Object) UserManager.instance != (Object) null && UserManager.instance.UserIsAttached(Player.One);
    if (!((Object) ControllerManagerGame.self == (Object) null))
      return;
    ControllerManagerGame.self = this;
  }

  private void Start()
  {
    ControllerManagerGame.playSelectedWithCont = false;
    Globals.MenuPlayer.SetValue(Player.One);
    this.gui = GUIManager.instance;
    if (ControllerManagerGame.usingController)
      this.SetupUsingController();
    this.isInitialized = true;
  }

  private void SetupUsingController()
  {
  }

  private void Update()
  {
    if (!this.isInitialized || (Object) UserManager.instance == (Object) null)
      return;
    this.CheckForConnectedController();
    this.CheckForBackButtonPress();
    if (!ControllerManagerGame.usingController)
      return;
    double num1 = (double) UserManager.instance.LeftStickX(Player.One);
    double num2 = (double) UserManager.instance.LeftStickY(Player.One);
    this.CheckForTimeout();
    this.CheckForPause();
  }

  private void CheckForBackButtonPress()
  {
    if (UserManager.instance.Action2WasPressed(Player.One))
    {
      GUIManager.instance.ReturnToPreviousMenu();
    }
    else
    {
      if (!UserManager.instance.Action2WasPressed(Player.Two))
        return;
      GUIManager.instance.ReturnToPreviousMenu(2);
    }
  }

  private void CheckForTimeout()
  {
    if (!global::Game.UserCallsPlays)
      return;
    if (ProEra.Game.Sources.UI.PrePlayWindowP1.IsVisible() && UserManager.instance.LeftBumperIsPressed(Player.One) && UserManager.instance.RightBumperWasPressed(Player.One))
    {
      TimeoutManager.CallTimeOut(Team.Player1);
    }
    else
    {
      if (!global::Game.Is2PMatch || !ProEra.Game.Sources.UI.PrePlayWindowP2.IsVisible() || !UserManager.instance.LeftBumperIsPressed(Player.Two) || !UserManager.instance.RightBumperWasPressed(Player.Two))
        return;
      TimeoutManager.CallTimeOut(Team.Player2);
    }
  }

  private void CheckForPause()
  {
    if (global::Game.IsPlayActive || global::Game.HasScreenOverlay)
      return;
    this.CheckForPlayerPause(Player.One);
    if (!global::Game.Is2PMatch)
      return;
    this.CheckForPlayerPause(Player.Two);
  }

  private void CheckForPlayerPause(Player userIndex)
  {
    if (!UserManager.instance.StartWasPressed(userIndex) && !UserManager.instance.OptionWasPressed(userIndex) && !UserManager.instance.MenuWasPressed(userIndex) && !UserManager.instance.BackWasPressed(userIndex) && !UserManager.instance.ViewWasPressed(userIndex))
      return;
    Globals.MenuPlayer.SetValue(userIndex);
  }

  private void CheckForConnectedController()
  {
    if (!((Object) UserManager.instance != (Object) null) || !UserManager.instance.UserIsAttached(Player.One) || ControllerManagerGame.usingController)
      return;
    this.SetupUsingController();
  }

  public void SelectUIElement(Selectable s)
  {
    this.DeselectCurrentUIElement();
    if (!ControllerManagerGame.usingController)
      return;
    this.StartCoroutine(this.SelectUIElementCoroutine(s));
  }

  public void DeselectCurrentUIElement()
  {
    EventSystem current = EventSystem.current;
    if (!((Object) current != (Object) null))
      return;
    current.SetSelectedGameObject((GameObject) null);
  }

  public GameObject GetCurrentSelectedUIElement() => EventSystem.current.currentSelectedGameObject;

  private IEnumerator SelectUIElementCoroutine(Selectable s)
  {
    yield return (object) null;
    s.Select();
  }

  public static bool IsUIElementSelected(Animator animator) => animator.GetCurrentAnimatorStateInfo(0).shortNameHash == HashIDs.self.highlighted_Trigger;
}
