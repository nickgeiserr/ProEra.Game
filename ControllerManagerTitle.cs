// Decompiled with JetBrains decompiler
// Type: ControllerManagerTitle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerManagerTitle : MonoBehaviour
{
  public TitleScreenManager main;
  public static ControllerManagerTitle self;
  [HideInInspector]
  public bool usingController;
  [HideInInspector]
  public bool player1UsingController;
  [HideInInspector]
  public bool player2UsingController;

  private void Awake()
  {
    if (!((Object) ControllerManagerTitle.self == (Object) null))
      return;
    ControllerManagerTitle.self = this;
  }

  private void Update()
  {
    this.CheckForConnectedController();
    if (!this.usingController)
      return;
    this.ManageBackButtonPress();
  }

  private void CheckForConnectedController()
  {
    if (UserManager.instance.IsControllerAttached(Player.One) && !this.player1UsingController)
    {
      this.usingController = true;
      this.player1UsingController = true;
    }
    if (!(bool) (Object) UserManager.instance.GetUser(Player.Two) || this.player2UsingController)
      return;
    this.player2UsingController = true;
  }

  public void SelectUIElement(Selectable s)
  {
    this.DeselectCurrentUIElement();
    if (!this.usingController)
      return;
    this.StartCoroutine(this.SelectUIElementCoroutine(s));
  }

  public void DeselectCurrentUIElement() => EventSystem.current.SetSelectedGameObject((GameObject) null);

  public GameObject GetCurrentSelectedUIElement() => EventSystem.current.currentSelectedGameObject;

  private IEnumerator SelectUIElementCoroutine(Selectable s)
  {
    yield return (object) null;
    s.Select();
  }

  public static bool IsUIElementSelected(Animator animator) => animator.GetCurrentAnimatorStateInfo(0).shortNameHash == HashIDs.self.highlighted_Trigger;

  private void ManageBackButtonPress()
  {
    if (UserManager.instance.Action2WasPressed(Player.One))
    {
      if (BottomBarManager.instance.IsBackButtonVisible())
      {
        this.main.ReturnToPreviousMenu();
      }
      else
      {
        if (!this.main.mainMenu.IsVisible())
          return;
        BottomBarManager.instance.ToggleConfirmExit();
      }
    }
    else
    {
      if (!UserManager.instance.Action2WasPressed(Player.Two) || !BottomBarManager.instance.IsBackButtonVisible())
        return;
      this.main.ReturnToPreviousMenu(2);
    }
  }
}
