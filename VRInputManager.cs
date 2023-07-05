// Decompiled with JetBrains decompiler
// Type: VRInputManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class VRInputManager : MonoBehaviour
{
  private static VRInputManager Instance;
  [SerializeField]
  private InputActionAsset _ActionAsset;
  private static readonly Dictionary<int, InputAction> _leftController = new Dictionary<int, InputAction>(16);
  private static readonly Dictionary<int, InputAction> _rightController = new Dictionary<int, InputAction>(16);
  private readonly Enum[] Buttons = new Enum[20]
  {
    (Enum) VRInputManager.Button.TriggerPress,
    (Enum) VRInputManager.Button.TriggerTouch,
    (Enum) VRInputManager.Button.PrimaryButton,
    (Enum) VRInputManager.Button.PrimaryTouch,
    (Enum) VRInputManager.Button.Menu,
    (Enum) VRInputManager.Button.Primary2DAxisClick,
    (Enum) VRInputManager.Button.Primary2DAxisTouch,
    (Enum) VRInputManager.Button.Secondary2DAxisClick,
    (Enum) VRInputManager.Button.Secondary2DAxisTouch,
    (Enum) VRInputManager.Button.GripPress,
    (Enum) VRInputManager.Button.SecondaryButton,
    (Enum) VRInputManager.Button.SecondaryTouch,
    (Enum) VRInputManager.Button.Bumper,
    (Enum) VRInputManager.Button.PSVRSquare,
    (Enum) VRInputManager.Button.PSVRTriangle,
    (Enum) VRInputManager.Axis1D.Trigger,
    (Enum) VRInputManager.Axis1D.Grip,
    (Enum) VRInputManager.Axis2D.Primary2DAxis,
    (Enum) VRInputManager.Axis2D.Secondary2DAxis,
    (Enum) VRInputManager.Button.MoveButton
  };
  public static bool LeftLaserInput = false;
  public static bool RightLaserInput = false;

  private void Awake() => VRInputManager.Instance = this;

  private void OnDestroy() => VRInputManager.Instance = (VRInputManager) null;

  private void Start()
  {
    this._ActionAsset.Enable();
    this.Initialize();
  }

  public void Initialize()
  {
    this.Initialize(VRInputManager._leftController, VRInputManager.Controller.LeftHand);
    this.Initialize(VRInputManager._rightController, VRInputManager.Controller.RightHand);
  }

  private void Initialize(Dictionary<int, InputAction> dict, VRInputManager.Controller controller)
  {
    foreach (Enum button in this.Buttons)
      dict[Convert.ToInt32((object) button)] = VRInputManager.Instance._ActionAsset.FindAction(string.Format("{0}/{1}", (object) controller, (object) button), false);
  }

  public static void SetHaptic(EHand eHand)
  {
    if ((UnityEngine.Object) VRInputManager.Instance == (UnityEngine.Object) null)
      return;
    PersistentSingleton<GamePlayerController>.Instance.HapticsController.SendHapticPulseFromNode(eHand == EHand.Left ? XRNode.LeftHand : XRNode.RightHand, 0.5f, 0.05f);
  }

  public static InputAction GetAction(
    VRInputManager.Button button,
    VRInputManager.Controller controller)
  {
    return VRInputManager.GetAction((int) button, controller);
  }

  public static InputAction GetAction(
    VRInputManager.Axis1D axis,
    VRInputManager.Controller controller)
  {
    return VRInputManager.GetAction((int) axis, controller);
  }

  public static InputAction GetAction(
    VRInputManager.Axis2D axis,
    VRInputManager.Controller controller)
  {
    return VRInputManager.GetAction((int) axis, controller);
  }

  private static InputAction GetAction(int actionId, VRInputManager.Controller controller)
  {
    InputAction inputAction;
    return !(controller == VRInputManager.Controller.LeftHand ? VRInputManager._leftController : VRInputManager._rightController).TryGetValue(actionId, out inputAction) ? (InputAction) null : inputAction;
  }

  public static bool Get(VRInputManager.Button button, VRInputManager.Controller controller)
  {
    InputAction action = VRInputManager.GetAction(button, controller);
    if (action?.activeControl != null)
    {
      if (action.activeControl.valueType == typeof (bool))
        return action.ReadValue<bool>();
      if (action.activeControl.valueType == typeof (float))
        return (double) action.ReadValue<float>() > 0.5;
    }
    return false;
  }

  public static bool GetDown(VRInputManager.Button button, VRInputManager.Controller controller)
  {
    InputAction action = VRInputManager.GetAction(button, controller);
    return action != null && action.WasPressedThisFrame();
  }

  public static float Get(VRInputManager.Axis1D axis, VRInputManager.Controller controller)
  {
    InputAction action = VRInputManager.GetAction(axis, controller);
    return action == null ? 0.0f : action.ReadValue<float>();
  }

  public static Vector2 Get(VRInputManager.Axis2D axis, VRInputManager.Controller controller)
  {
    InputAction action = VRInputManager.GetAction(axis, controller);
    return action == null ? Vector2.zero : action.ReadValue<Vector2>();
  }

  public static bool BothHandsConnected() => InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).isValid && InputDevices.GetDeviceAtXRNode(XRNode.RightHand).isValid;

  public enum Controller
  {
    LeftHand,
    RightHand,
    HMD,
  }

  public enum Button
  {
    TriggerPress,
    TriggerTouch,
    PrimaryButton,
    PrimaryTouch,
    Menu,
    Primary2DAxisClick,
    Primary2DAxisTouch,
    Secondary2DAxisClick,
    Secondary2DAxisTouch,
    GripPress,
    SecondaryButton,
    SecondaryTouch,
    Bumper,
    MoveButton,
    PSVRSquare,
    PSVRTriangle,
  }

  public enum Axis1D
  {
    Trigger = 101, // 0x00000065
    Grip = 103, // 0x00000067
  }

  public enum Axis2D
  {
    Primary2DAxis = 1001, // 0x000003E9
    Secondary2DAxis = 1002, // 0x000003EA
  }
}
