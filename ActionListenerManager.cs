// Decompiled with JetBrains decompiler
// Type: ActionListenerManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using UDB;
using UnityEngine;

public class ActionListenerManager : SingletonBehaviour<ActionListenerManager, MonoBehaviour>
{
  private static bool isFirstTime = true;
  private UserManager _instance;

  private new void Awake()
  {
    this._instance = UserManager.instance;
    if (ActionListenerManager.isFirstTime)
    {
      Object.DontDestroyOnLoad((Object) this.gameObject);
      ActionListenerManager.isFirstTime = false;
    }
    else
      Object.DestroyImmediate((Object) this.gameObject);
  }

  private void Update()
  {
    this.CheckControllerInput();
    this.CheckKeyboardInput();
    if (!InputController.Exists() || !InputController.instance.GetInputDown(0))
      return;
    NotificationCenter.Broadcast("GetMouseInputDown");
  }

  private void CheckKeyboardInput()
  {
    this.CheckAndPostKeyInput(KeyCode.A);
    this.CheckAndPostKeyInput(KeyCode.B);
    this.CheckAndPostKeyInput(KeyCode.C);
    this.CheckAndPostKeyInput(KeyCode.D);
    this.CheckAndPostKeyInput(KeyCode.E);
    this.CheckAndPostKeyInput(KeyCode.F);
    this.CheckAndPostKeyInput(KeyCode.G);
    this.CheckAndPostKeyInput(KeyCode.H);
    this.CheckAndPostKeyInput(KeyCode.I);
    this.CheckAndPostKeyInput(KeyCode.J);
    this.CheckAndPostKeyInput(KeyCode.K);
    this.CheckAndPostKeyInput(KeyCode.L);
    this.CheckAndPostKeyInput(KeyCode.M);
    this.CheckAndPostKeyInput(KeyCode.N);
    this.CheckAndPostKeyInput(KeyCode.O);
    this.CheckAndPostKeyInput(KeyCode.P);
    this.CheckAndPostKeyInput(KeyCode.Q);
    this.CheckAndPostKeyInput(KeyCode.R);
    this.CheckAndPostKeyInput(KeyCode.S);
    this.CheckAndPostKeyInput(KeyCode.T);
    this.CheckAndPostKeyInput(KeyCode.U);
    this.CheckAndPostKeyInput(KeyCode.V);
    this.CheckAndPostKeyInput(KeyCode.W);
    this.CheckAndPostKeyInput(KeyCode.X);
    this.CheckAndPostKeyInput(KeyCode.Y);
    this.CheckAndPostKeyInput(KeyCode.Z);
    this.CheckAndPostKeyInput(KeyCode.Escape);
    this.CheckAndPostKeyInput(KeyCode.Return);
    this.CheckAndPostKeyInput(KeyCode.Space);
    this.CheckAndPostKeyInput(KeyCode.LeftShift);
    this.CheckAndPostKeyInput(KeyCode.RightShift);
  }

  private void CheckControllerInputForPlayer(Player player)
  {
    User user = UserManager.instance.GetUser(player);
    if ((Object) user == (Object) null || user.Actions == null)
      return;
    if (user.Actions.Start != null && user.Actions.Start.WasPressed || user.Actions.Menu != null && user.Actions.Menu.WasPressed || user.Actions.Options != null && user.Actions.Options.WasPressed)
      NotificationCenter<Player>.Broadcast("StartWasPressed", player);
    if (user.Actions.Action1 != null && user.Actions.Action1.WasPressed)
      NotificationCenter<Player>.Broadcast("Action1WasPressed", player);
    if (user.Actions.Action1 != null && user.Actions.Action1.IsPressed)
      NotificationCenter<Player>.Broadcast("Action1IsPressed", player);
    if (user.Actions.Action2 != null && user.Actions.Action2.WasPressed)
      NotificationCenter<Player>.Broadcast("Action2WasPressed", player);
    if (user.Actions.Action2 != null && user.Actions.Action2.IsPressed)
      NotificationCenter<Player>.Broadcast("Action2IsPressed", player);
    if (user.Actions.Action3 != null && user.Actions.Action3.WasPressed)
      NotificationCenter<Player>.Broadcast("Action3WasPressed", player);
    if (user.Actions.Action3 != null && user.Actions.Action3.IsPressed)
      NotificationCenter<Player>.Broadcast("Action3IsPressed", player);
    if (user.Actions.Action4 != null && user.Actions.Action4.WasPressed)
      NotificationCenter<Player>.Broadcast("Action4WasPressed", player);
    if (user.Actions.Action3 != null && user.Actions.Action3.IsPressed)
      NotificationCenter<Player>.Broadcast("Action4IsPressed", player);
    if (user.Actions.Back != null && user.Actions.Back.WasPressed)
      NotificationCenter<Player>.Broadcast("BackWasPressed", player);
    if (user.Actions.View != null && user.Actions.View.WasPressed)
      NotificationCenter<Player>.Broadcast("ViewWasPressed", player);
    if (user.Actions.RightBumper != null && user.Actions.RightBumper.WasPressed)
      NotificationCenter<Player>.Broadcast("RightBumperWasPressed", player);
    if (user.Actions.RightBumper != null && user.Actions.RightBumper.IsPressed)
      NotificationCenter<Player>.Broadcast("RightBumperWasPressed", player);
    if (user.Actions.LeftBumper != null && user.Actions.LeftBumper.WasPressed)
      NotificationCenter<Player>.Broadcast("LeftBumperWasPressed", player);
    if (user.Actions.LeftBumper != null && user.Actions.LeftBumper.IsPressed)
      NotificationCenter<Player>.Broadcast("LeftBumperIsPressed", player);
    if (user.Actions.RightTrigger != null && user.Actions.RightTrigger.WasPressed)
      NotificationCenter<Player>.Broadcast("RightTriggerWasPressed", player);
    if (user.Actions.RightTrigger != null && user.Actions.RightTrigger.IsPressed)
      NotificationCenter<Player>.Broadcast("RightTriggerIsPressed", player);
    if (user.Actions.RightTrigger != null && user.Actions.RightTrigger.WasReleased)
      NotificationCenter<Player>.Broadcast("RightTriggerWasReleased", player);
    if (user.Actions.LeftTrigger != null && user.Actions.LeftTrigger.WasPressed)
      NotificationCenter<Player>.Broadcast("LeftTriggerWasPressed", player);
    if (user.Actions.LeftTrigger != null && user.Actions.LeftTrigger.IsPressed)
      NotificationCenter<Player>.Broadcast("LeftTriggerIsPressed", player);
    if (user.Actions.LeftTrigger != null && user.Actions.LeftTrigger.WasReleased)
      NotificationCenter<Player>.Broadcast("LeftTriggerWasReleased", player);
    if (user.Actions.RightStickButton != null && user.Actions.RightStickButton.IsPressed)
      NotificationCenter<Player>.Broadcast("RightStickButtonIsPressed", player);
    if (user.Actions.RightStickButton != null && user.Actions.RightStickButton.WasPressed)
      NotificationCenter<Player>.Broadcast("RightStickButtonWasPressed", player);
    if (user.Actions.LeftStickButton != null && user.Actions.LeftStickButton.IsPressed)
      NotificationCenter<Player>.Broadcast("LeftStickButtonIsPressed", player);
    if (user.Actions.LeftStickButton != null && user.Actions.LeftStickButton.WasPressed)
      NotificationCenter<Player>.Broadcast("LeftStickButtonWasPressed", player);
    if (user.Actions.DPadButtonLeft != null && user.Actions.DPadButtonLeft.WasPressed)
      NotificationCenter<Player>.Broadcast("DPadLeftWasPressed", player);
    if (user.Actions.DPadButtonRight != null && user.Actions.DPadButtonRight.WasPressed)
      NotificationCenter<Player>.Broadcast("DPadRightWasPressed", player);
    if (user.Actions.DPadButtonRight == null || !user.Actions.DPadButtonRight.WasPressed)
      return;
    NotificationCenter<Player>.Broadcast("DPadRightWasPressed", player);
  }

  private void CheckControllerInput()
  {
    this.CheckControllerInputForPlayer(Player.One);
    this.CheckControllerInputForPlayer(Player.Two);
  }

  private void CheckAndPostKeyInput(KeyCode code)
  {
    if (Input.GetKeyDown(code))
      NotificationCenter<KeyCode>.Broadcast("KeyDown", code);
    if (!Input.GetKeyUp(code))
      return;
    NotificationCenter<KeyCode>.Broadcast("keyUp", code);
  }
}
