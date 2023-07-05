// Decompiled with JetBrains decompiler
// Type: FootballVR.ControllerInput
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class ControllerInput
  {
    private readonly EHand _hand;
    private readonly VRInputManager.Controller _controller;
    private readonly VariableFloat _triggerDownThreshold;
    private float _lastTimeTriggerPressed;
    private float _buttonOneHeldTime;
    private float _timeUntilHeld = 1f;

    private VariableBool triggerPressed { get; } = new VariableBool();

    public VariableBool gripPressed { get; } = new VariableBool();

    public VariableBool buttonOne { get; } = new VariableBool();

    public VariableBool buttonOneHeld { get; } = new VariableBool();

    public VariableBool buttonTwo { get; } = new VariableBool();

    public VariableBool moveButton { get; } = new VariableBool();

    public VariableBool menuPressed { get; } = new VariableBool();

    public VariableBool thumbstickPressed { get; } = new VariableBool();

    public VariableBool psvrSquareButton { get; } = new VariableBool();

    public VariableBool psvrTriangleButton { get; } = new VariableBool();

    public Vector2 thumbstickVector { get; set; }

    public VariableBool objectInteractPressed { get; } = new VariableBool();

    public VariableBool sprintPressed { get; } = new VariableBool();

    public bool TriggerActivated(float maxTime, float minTime = 0.0f)
    {
      if ((double) VRInputManager.Get(VRInputManager.Axis1D.Trigger, this._controller) > (double) this._triggerDownThreshold.Value)
        return true;
      return (double) Time.time - (double) this._lastTimeTriggerPressed < (double) maxTime && (double) Time.time >= (double) this._lastTimeTriggerPressed + (double) minTime;
    }

    public ControllerInput(EHand controller, VariableFloat triggerDownThreshold)
    {
      this._hand = controller;
      this._controller = controller == EHand.Right ? VRInputManager.Controller.RightHand : VRInputManager.Controller.LeftHand;
      this._triggerDownThreshold = triggerDownThreshold;
      this.triggerPressed.OnValueChanged += new Action<bool>(this.HandleTriggerChanged);
      ScriptableSingleton<VRSettings>.Instance.GripButtonThrow.OnValueChanged += new Action<bool>(this.OnControlsChange);
      this.OnControlsChange((bool) ScriptableSingleton<VRSettings>.Instance.GripButtonThrow);
    }

    public void OnControlsChange(bool grip)
    {
      this.triggerPressed.ClearValueEvents();
      this.gripPressed.ClearValueEvents();
      this.triggerPressed.OnValueChanged += new Action<bool>(this.HandleTriggerChanged);
      if (!(bool) ScriptableSingleton<VRSettings>.Instance.GripButtonThrow)
      {
        this.triggerPressed.OnValueChanged += new Action<bool>(this.UpdateObjectInteract);
        this.gripPressed.OnValueChanged += new Action<bool>(this.UpdateSprint);
      }
      else
      {
        this.triggerPressed.OnValueChanged += new Action<bool>(this.UpdateSprint);
        this.gripPressed.OnValueChanged += new Action<bool>(this.UpdateObjectInteract);
      }
    }

    private void HandleTriggerChanged(bool pressed)
    {
      if (!pressed)
        return;
      this._lastTimeTriggerPressed = Time.time;
    }

    private void UpdateObjectInteract(bool pressed) => this.objectInteractPressed.SetValue(pressed);

    private void UpdateSprint(bool pressed) => this.sprintPressed.SetValue(pressed);

    public void Update()
    {
      try
      {
        this.thumbstickVector = VRInputManager.Get(VRInputManager.Axis2D.Primary2DAxis, this._controller);
        this.thumbstickPressed.Value = VRInputManager.Get(VRInputManager.Button.Primary2DAxisClick, this._controller);
        this.triggerPressed.Value = (double) VRInputManager.Get(VRInputManager.Axis1D.Trigger, this._controller) > (double) this._triggerDownThreshold.Value;
        this.menuPressed.Value = VRInputManager.Get(VRInputManager.Button.Menu, this._controller);
        if (VRUtils.GetDeviceType() == VRUtils.DeviceType.Index)
          this.menuPressed.Value = VRInputManager.GetDown(VRInputManager.Button.SecondaryButton, this._controller);
        this.gripPressed.Value = (double) VRInputManager.Get(VRInputManager.Axis1D.Grip, this._controller) > (double) this._triggerDownThreshold.Value;
        this.buttonOne.Value = VRInputManager.Get(VRInputManager.Button.PrimaryButton, this._controller);
        this.buttonTwo.Value = VRInputManager.Get(VRInputManager.Button.SecondaryButton, this._controller);
        this.moveButton.Value = VRInputManager.Get(VRInputManager.Button.MoveButton, this._controller);
        this.psvrSquareButton.Value = VRInputManager.Get(VRInputManager.Button.PSVRSquare, this._controller);
        this.psvrTriangleButton.Value = VRInputManager.Get(VRInputManager.Button.PSVRTriangle, this._controller);
        if (this.buttonOne.Value)
          this._buttonOneHeldTime += Time.deltaTime;
        else
          this._buttonOneHeldTime = 0.0f;
        this.buttonOneHeld.Value = (double) this._buttonOneHeldTime >= (double) this._timeUntilHeld;
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
    }

    public VariableBool GetButtonState(EControllerKey controllerKey)
    {
      switch (controllerKey)
      {
        case EControllerKey.Grip:
          return this.gripPressed;
        case EControllerKey.Trigger:
          return this.triggerPressed;
        case EControllerKey.ButtonOne_ax:
          return this.buttonOne;
        case EControllerKey.ButtonTwo_by:
          return this.buttonTwo;
        case EControllerKey.MoveButton:
          return this.moveButton;
        default:
          Debug.LogError((object) string.Format("{0} not handled. Returning grip..", (object) controllerKey));
          return this.gripPressed;
      }
    }

    public static IEnumerator WaitDoubleTrigger()
    {
      float num1;
      double num2;
      do
      {
        yield return (object) null;
        num2 = (double) VRInputManager.Get(VRInputManager.Axis1D.Trigger, VRInputManager.Controller.RightHand);
        num1 = VRInputManager.Get(VRInputManager.Axis1D.Trigger, VRInputManager.Controller.LeftHand);
      }
      while (num2 <= 0.89999997615814209 || (double) num1 <= 0.89999997615814209);
    }

    public static bool IsSprintInputPressed(ControllerInput.InputCheckType type)
    {
      if (!(bool) ScriptableSingleton<VRSettings>.Instance.GripButtonThrow)
      {
        switch (type)
        {
          case ControllerInput.InputCheckType.Left:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.LeftHand);
          case ControllerInput.InputCheckType.Right:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand);
          case ControllerInput.InputCheckType.Either:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.LeftHand) || VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand);
          case ControllerInput.InputCheckType.Both:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.LeftHand) && VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand);
        }
      }
      else
      {
        switch (type)
        {
          case ControllerInput.InputCheckType.Left:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.LeftHand);
          case ControllerInput.InputCheckType.Right:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.RightHand);
          case ControllerInput.InputCheckType.Either:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.LeftHand) || VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.RightHand);
          case ControllerInput.InputCheckType.Both:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.LeftHand) && VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.RightHand);
        }
      }
      return false;
    }

    public bool IsSprintInputPressed()
    {
      if (!(bool) ScriptableSingleton<VRSettings>.Instance.GripButtonThrow)
      {
        switch (this._hand)
        {
          case EHand.Right:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand);
          case EHand.Left:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.LeftHand);
        }
      }
      else
      {
        switch (this._hand)
        {
          case EHand.Right:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.RightHand);
          case EHand.Left:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.LeftHand);
        }
      }
      return false;
    }

    public static bool IsBallInputPressed(ControllerInput.InputCheckType type)
    {
      if (!(bool) ScriptableSingleton<VRSettings>.Instance.GripButtonThrow)
      {
        switch (type)
        {
          case ControllerInput.InputCheckType.Left:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.LeftHand);
          case ControllerInput.InputCheckType.Right:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.RightHand);
          case ControllerInput.InputCheckType.Either:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.LeftHand) || VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.RightHand);
          case ControllerInput.InputCheckType.Both:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.LeftHand) && VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.RightHand);
        }
      }
      else
      {
        switch (type)
        {
          case ControllerInput.InputCheckType.Left:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.LeftHand);
          case ControllerInput.InputCheckType.Right:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand);
          case ControllerInput.InputCheckType.Either:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.LeftHand) || VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand);
          case ControllerInput.InputCheckType.Both:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.LeftHand) && VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand);
        }
      }
      return false;
    }

    public bool IsBallInputPressed()
    {
      if (!(bool) ScriptableSingleton<VRSettings>.Instance.GripButtonThrow)
      {
        switch (this._hand)
        {
          case EHand.Right:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.RightHand);
          case EHand.Left:
            return VRInputManager.Get(VRInputManager.Button.TriggerPress, VRInputManager.Controller.LeftHand);
        }
      }
      else
      {
        switch (this._hand)
        {
          case EHand.Right:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.RightHand);
          case EHand.Left:
            return VRInputManager.Get(VRInputManager.Button.GripPress, VRInputManager.Controller.LeftHand);
        }
      }
      return false;
    }

    public enum InputCheckType
    {
      Left,
      Right,
      Either,
      Both,
    }
  }
}
