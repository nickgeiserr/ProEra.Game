// Decompiled with JetBrains decompiler
// Type: ControllerSupport.UserManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using InControl;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerSupport
{
  public class UserManager : MonoBehaviour
  {
    public static UserManager instance;
    private const int maxUsers = 2;
    private bool isSetUp;
    private static List<User> users;
    private User keyboardUser;
    private UserActions keyboardListener;
    private UserActions joystickListener;
    public GameObject userPrefab;
    public Transform _transform;

    private void Awake()
    {
      if ((UnityEngine.Object) UserManager.instance != (UnityEngine.Object) null && (UnityEngine.Object) this.userPrefab != (UnityEngine.Object) null)
      {
        UserManager.instance = this;
        UserManager.users = new List<User>(2);
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      }
      else
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }

    private void OnDestroy() => Debug.Log((object) "UserManager -> OnDestroy");

    private void OnDisable()
    {
      if (this.isSetUp)
        InputManager.OnDeviceDetached -= new Action<InputDevice>(this.OnDeviceDetached);
      if (this.joystickListener != null)
        this.joystickListener.Destroy();
      if (this.keyboardListener == null)
        return;
      this.keyboardListener.Destroy();
    }

    private void Update()
    {
      if (!this.isSetUp)
        this.Setup();
      InputDevice activeDevice = InputManager.ActiveDevice;
      if (activeDevice.IsAttached && this.ThereIsNoUserUsingJoystick(activeDevice) && !PersistentSingleton<SaveManager>.Instance.gameSettings.ignoreControllers)
        this.CreateUser(activeDevice);
      if (!((UnityEngine.Object) this.keyboardUser == (UnityEngine.Object) null))
        return;
      this.keyboardUser = this.CreateUser((InputDevice) null);
    }

    private void Setup()
    {
      InputManager.OnDeviceDetached += new Action<InputDevice>(this.OnDeviceDetached);
      this.keyboardListener = UserActions.CreateWithKeyboardBindings();
      this.joystickListener = UserActions.CreateWithJoystickBindings();
      this.isSetUp = true;
    }

    private User FindUserUsingJoystick(InputDevice inputDevice)
    {
      int count = UserManager.users.Count;
      for (int index = 0; index < count; ++index)
      {
        User user = UserManager.users[index];
        if (user.Actions.Device == inputDevice)
          return user;
      }
      return (User) null;
    }

    private bool ThereIsNoUserUsingJoystick(InputDevice inputDevice) => (UnityEngine.Object) this.FindUserUsingJoystick(inputDevice) == (UnityEngine.Object) null;

    private void OnDeviceDetached(InputDevice inputDevice)
    {
      User userUsingJoystick = this.FindUserUsingJoystick(inputDevice);
      if (!((UnityEngine.Object) userUsingJoystick != (UnityEngine.Object) null))
        return;
      this.RemoveUser(userUsingJoystick);
    }

    private User CreateUser(InputDevice inputDevice)
    {
      if (UserManager.users.Count >= 2)
        return (User) null;
      GameObject gameObject;
      try
      {
        gameObject = UnityEngine.Object.Instantiate<GameObject>(this.userPrefab, this._transform.position, Quaternion.identity);
      }
      catch (Exception ex)
      {
        Debug.Log((object) "User Prefab not found.  This is normal if you loaded from the UniformEditor scene.");
        return (User) null;
      }
      User user = gameObject.GetComponent<User>();
      if (inputDevice == null)
      {
        user.Actions = this.keyboardListener;
      }
      else
      {
        UserActions joystickBindings = UserActions.CreateWithJoystickBindings();
        joystickBindings.Device = inputDevice;
        user.Actions = joystickBindings;
        user = this.SetRealIndex(user);
        UserManager.users.Add(user);
      }
      return user;
    }

    public void SwapUsersRealIndex()
    {
      int index1 = 0;
      int index2 = 0;
      for (int index3 = 0; index3 < UserManager.users.Count; ++index3)
      {
        if (UserManager.users[index3].Actions != this.keyboardListener && UserManager.users[index3].realUserIndex == 0)
          index1 = index3;
        else if (UserManager.users[index3].Actions != this.keyboardListener && UserManager.users[index3].realUserIndex == 1)
          index2 = index3;
      }
      UserManager.users[index1].realUserIndex = 1;
      UserManager.users[index2].realUserIndex = 0;
    }

    private User SetRealIndex(User user)
    {
      if (UserManager.users.Count > 0)
      {
        if (UserManager.users[0].realUserIndex == 0)
          user.AssignController(1);
        else
          user.AssignController(0);
      }
      return user;
    }

    public void RemoveUser(User user)
    {
      user.Actions = (UserActions) null;
      UnityEngine.Object.Destroy((UnityEngine.Object) user.gameObject);
      UserManager.users.Remove(user);
      if (UserManager.users.Count <= 0 || !((UnityEngine.Object) UserManager.users[0] != (UnityEngine.Object) null))
        return;
      UserManager.users[0].AssignController(0);
    }

    public bool IsControllerAttached(Player userIndex)
    {
      for (int index = 0; index < UserManager.users.Count; ++index)
      {
        User user = UserManager.users[index];
        if ((Player) user.realUserIndex == userIndex && user.Actions != this.keyboardListener)
          return true;
      }
      return false;
    }

    public User GetUser(Player userIndex)
    {
      if (UserManager.users.Count < 1)
        return userIndex == Player.One ? this.keyboardUser : (User) null;
      for (int index = 0; index < UserManager.users.Count; ++index)
      {
        User user = UserManager.users[index];
        if ((Player) user.realUserIndex == userIndex && user.Actions != this.keyboardListener)
          return user;
      }
      return (User) null;
    }

    public bool UserIsAttached(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Device != null && user.Actions.Device.IsAttached;
    }

    public bool Action1WasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Action1 != null && user.Actions.Action1.WasPressed;
    }

    public bool Action1IsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Action1 != null && user.Actions.Action1.IsPressed;
    }

    public bool Action2WasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Action2 != null && user.Actions.Action2.WasPressed;
    }

    public bool Action2IsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Action2 != null && user.Actions.Action2.IsPressed;
    }

    public bool Action3WasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Action3 != null && user.Actions.Action3.WasPressed;
    }

    public bool Action3IsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Action3 != null && user.Actions.Action3.IsPressed;
    }

    public bool Action4WasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Action4 != null && user.Actions.Action4.WasPressed;
    }

    public bool Action4IsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Action4 != null && user.Actions.Action4.IsPressed;
    }

    public bool StartWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Start != null && user.Actions.Start.WasPressed;
    }

    public bool MenuWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Menu != null && user.Actions.Menu.WasPressed;
    }

    public bool OptionWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Options != null && user.Actions.Options.WasPressed;
    }

    public bool BackWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.Back != null && user.Actions.Back.WasPressed;
    }

    public bool ViewWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.View != null && user.Actions.View.WasPressed;
    }

    public bool RightBumperWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RightBumper != null && user.Actions.RightBumper.WasPressed;
    }

    public bool RightBumperIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RightBumper != null && user.Actions.RightBumper.IsPressed;
    }

    public bool LeftBumperWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.LeftBumper != null && user.Actions.LeftBumper.WasPressed;
    }

    public bool LeftBumperIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.LeftBumper != null && user.Actions.LeftBumper.IsPressed;
    }

    public bool RightTriggerWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RightTrigger != null && user.Actions.RightTrigger.WasPressed;
    }

    public bool RightTriggerIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RightTrigger != null && user.Actions.RightTrigger.IsPressed;
    }

    public bool RightTriggerWasReleased(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RightTrigger != null && user.Actions.RightTrigger.WasReleased;
    }

    public bool LeftTriggerWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.LeftTrigger != null && user.Actions.LeftTrigger.WasPressed;
    }

    public bool LeftTriggerIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.LeftTrigger != null && user.Actions.LeftTrigger.IsPressed;
    }

    public bool LeftTriggerWasReleased(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.LeftTrigger != null && user.Actions.LeftTrigger.WasReleased;
    }

    public bool RightStickButtonWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RightStickButton != null && user.Actions.RightStickButton.WasPressed;
    }

    public bool RightStickButtonIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RightStickButton != null && user.Actions.RightStickButton.IsPressed;
    }

    public bool LeftStickButtonWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.LeftStickButton != null && user.Actions.LeftStickButton.WasPressed;
    }

    public bool LeftStickButtonIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.LeftStickButton != null && user.Actions.LeftStickButton.IsPressed;
    }

    public bool DPadButtonLeftWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.DPadButtonLeft != null && user.Actions.DPadButtonLeft.WasPressed;
    }

    public bool DPadButtonLeftIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.DPadButtonLeft != null && user.Actions.DPadButtonLeft.IsPressed;
    }

    public bool DPadButtonRightWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.DPadButtonRight != null && user.Actions.DPadButtonRight.WasPressed;
    }

    public bool DPadButtonRightIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.DPadButtonRight != null && user.Actions.DPadButtonRight.IsPressed;
    }

    public bool DPadButtonUpWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.DPadButtonUp != null && user.Actions.DPadButtonUp.WasPressed;
    }

    public bool DPadButtonUpIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.DPadButtonUp != null && user.Actions.DPadButtonUp.IsPressed;
    }

    public bool DPadButtonDownWasPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.DPadButtonDown != null && user.Actions.DPadButtonDown.WasPressed;
    }

    public bool DPadButtonDownIsPressed(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.DPadButtonDown != null && user.Actions.DPadButtonDown.IsPressed;
    }

    public float RightStickX(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RotateRight != null ? user.Actions.RotateRight.X : 0.0f;
    }

    public float RightStickY(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RotateRight != null ? user.Actions.RotateRight.Y : 0.0f;
    }

    public float LeftStickX(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RotateLeft != null ? user.Actions.RotateLeft.X : 0.0f;
    }

    public float LeftStickY(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.RotateLeft != null ? user.Actions.RotateLeft.Y : 0.0f;
    }

    public float ScrollWheel(Player userIndex)
    {
      User user = this.GetUser(userIndex);
      return (bool) (UnityEngine.Object) user && user.Actions != null && user.Actions.ScrollWheel != null ? user.Actions.ScrollWheel.Value : 0.0f;
    }
  }
}
