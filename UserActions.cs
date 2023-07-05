// Decompiled with JetBrains decompiler
// Type: UserActions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using InControl;

public class UserActions : PlayerActionSet
{
  public PlayerAction Action1;
  public PlayerAction Action2;
  public PlayerAction Action3;
  public PlayerAction Action4;
  public PlayerAction Left;
  public PlayerAction Right;
  public PlayerAction Up;
  public PlayerAction Down;
  public PlayerAction Start;
  public PlayerAction Options;
  public PlayerAction Menu;
  public PlayerAction Back;
  public PlayerAction View;
  public PlayerAction LeftBumper;
  public PlayerAction RightBumper;
  public PlayerAction LeftTrigger;
  public PlayerAction RightTrigger;
  public PlayerAction RightStickButton;
  public PlayerAction LeftStickButton;
  public PlayerAction RightStickUp;
  public PlayerAction RightStickDown;
  public PlayerAction RightStickLeft;
  public PlayerAction RightStickRight;
  public PlayerTwoAxisAction RotateLeft;
  public PlayerTwoAxisAction RotateRight;
  public PlayerAction DPadButtonLeft;
  public PlayerAction DPadButtonRight;
  public PlayerAction DPadButtonUp;
  public PlayerAction DPadButtonDown;
  public PlayerAction ScrollWheel;

  public UserActions(bool joystick)
  {
    if (joystick)
    {
      this.Action1 = this.CreatePlayerAction(nameof (Action1));
      this.Action2 = this.CreatePlayerAction(nameof (Action2));
      this.Action3 = this.CreatePlayerAction(nameof (Action3));
      this.Action4 = this.CreatePlayerAction(nameof (Action4));
      this.Left = this.CreatePlayerAction(nameof (Left));
      this.Right = this.CreatePlayerAction(nameof (Right));
      this.Up = this.CreatePlayerAction(nameof (Up));
      this.Down = this.CreatePlayerAction(nameof (Down));
      this.Start = this.CreatePlayerAction(nameof (Start));
      this.Options = this.CreatePlayerAction(nameof (Options));
      this.Menu = this.CreatePlayerAction(nameof (Menu));
      this.Back = this.CreatePlayerAction(nameof (Back));
      this.View = this.CreatePlayerAction(nameof (View));
      this.LeftBumper = this.CreatePlayerAction(nameof (LeftBumper));
      this.RightBumper = this.CreatePlayerAction(nameof (RightBumper));
      this.LeftTrigger = this.CreatePlayerAction(nameof (LeftTrigger));
      this.RightTrigger = this.CreatePlayerAction(nameof (RightTrigger));
      this.RightStickButton = this.CreatePlayerAction(nameof (RightStickButton));
      this.LeftStickButton = this.CreatePlayerAction(nameof (LeftStickButton));
      this.RotateLeft = this.CreateTwoAxisPlayerAction(this.Left, this.Right, this.Down, this.Up);
      this.RightStickUp = this.CreatePlayerAction(nameof (RightStickUp));
      this.RightStickDown = this.CreatePlayerAction(nameof (RightStickDown));
      this.RightStickLeft = this.CreatePlayerAction(nameof (RightStickLeft));
      this.RightStickRight = this.CreatePlayerAction(nameof (RightStickRight));
      this.DPadButtonLeft = this.CreatePlayerAction(nameof (DPadButtonLeft));
      this.DPadButtonRight = this.CreatePlayerAction(nameof (DPadButtonRight));
      this.DPadButtonUp = this.CreatePlayerAction(nameof (DPadButtonUp));
      this.DPadButtonDown = this.CreatePlayerAction(nameof (DPadButtonDown));
      this.RotateRight = this.CreateTwoAxisPlayerAction(this.RightStickLeft, this.RightStickRight, this.RightStickDown, this.RightStickUp);
    }
    else
    {
      this.Left = this.CreatePlayerAction(nameof (Left));
      this.Right = this.CreatePlayerAction(nameof (Right));
      this.Up = this.CreatePlayerAction(nameof (Up));
      this.Down = this.CreatePlayerAction(nameof (Down));
      this.ScrollWheel = this.CreatePlayerAction(nameof (ScrollWheel));
      this.Action1 = this.CreatePlayerAction(nameof (Action1));
      this.RotateLeft = this.CreateTwoAxisPlayerAction(this.Left, this.Right, this.Down, this.Up);
    }
  }

  public static UserActions CreateWithKeyboardBindings()
  {
    UserActions keyboardBindings = new UserActions(false);
    keyboardBindings.Up.AddDefaultBinding(Key.UpArrow);
    keyboardBindings.Down.AddDefaultBinding(Key.DownArrow);
    keyboardBindings.Left.AddDefaultBinding(Key.LeftArrow);
    keyboardBindings.Right.AddDefaultBinding(Key.RightArrow);
    keyboardBindings.Left.AddDefaultBinding(Key.A);
    keyboardBindings.Down.AddDefaultBinding(Key.S);
    keyboardBindings.Right.AddDefaultBinding(Key.D);
    keyboardBindings.Up.AddDefaultBinding(Key.W);
    keyboardBindings.Action1.AddDefaultBinding(Key.Return);
    return keyboardBindings;
  }

  public static UserActions CreateWithJoystickBindings()
  {
    UserActions joystickBindings = new UserActions(true);
    joystickBindings.Action1.AddDefaultBinding(InputControlType.Action1);
    joystickBindings.Action2.AddDefaultBinding(InputControlType.Action2);
    joystickBindings.Action3.AddDefaultBinding(InputControlType.Action3);
    joystickBindings.Action4.AddDefaultBinding(InputControlType.Action4);
    joystickBindings.Up.AddDefaultBinding(InputControlType.LeftStickUp);
    joystickBindings.Down.AddDefaultBinding(InputControlType.LeftStickDown);
    joystickBindings.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
    joystickBindings.Right.AddDefaultBinding(InputControlType.LeftStickRight);
    joystickBindings.Up.AddDefaultBinding(InputControlType.DPadUp);
    joystickBindings.Down.AddDefaultBinding(InputControlType.DPadDown);
    joystickBindings.Left.AddDefaultBinding(InputControlType.DPadLeft);
    joystickBindings.Right.AddDefaultBinding(InputControlType.DPadRight);
    joystickBindings.Start.AddDefaultBinding(InputControlType.Start);
    joystickBindings.Options.AddDefaultBinding(InputControlType.Options);
    joystickBindings.Menu.AddDefaultBinding(InputControlType.Menu);
    joystickBindings.Back.AddDefaultBinding(InputControlType.Back);
    joystickBindings.View.AddDefaultBinding(InputControlType.View);
    joystickBindings.LeftBumper.AddDefaultBinding(InputControlType.LeftBumper);
    joystickBindings.RightBumper.AddDefaultBinding(InputControlType.RightBumper);
    joystickBindings.LeftTrigger.AddDefaultBinding(InputControlType.LeftTrigger);
    joystickBindings.RightTrigger.AddDefaultBinding(InputControlType.RightTrigger);
    joystickBindings.RightStickButton.AddDefaultBinding(InputControlType.RightStickButton);
    joystickBindings.LeftStickButton.AddDefaultBinding(InputControlType.LeftStickButton);
    joystickBindings.RightStickUp.AddDefaultBinding(InputControlType.RightStickUp);
    joystickBindings.RightStickDown.AddDefaultBinding(InputControlType.RightStickDown);
    joystickBindings.RightStickLeft.AddDefaultBinding(InputControlType.RightStickLeft);
    joystickBindings.RightStickRight.AddDefaultBinding(InputControlType.RightStickRight);
    joystickBindings.DPadButtonLeft.AddDefaultBinding(InputControlType.DPadLeft);
    joystickBindings.DPadButtonRight.AddDefaultBinding(InputControlType.DPadRight);
    joystickBindings.DPadButtonUp.AddDefaultBinding(InputControlType.DPadUp);
    joystickBindings.DPadButtonDown.AddDefaultBinding(InputControlType.DPadDown);
    return joystickBindings;
  }
}
