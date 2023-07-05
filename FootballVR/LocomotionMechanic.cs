// Decompiled with JetBrains decompiler
// Type: FootballVR.LocomotionMechanic
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
  public abstract class LocomotionMechanic : ScriptableObject
  {
    [SerializeField]
    protected HandsDataModel _handsDataModel;
    private bool _initialized;
    private bool _state;
    private int _keysDown;
    private VariableBool _keyPressed;
    private readonly RoutineHandle _locomotionRoutine = new RoutineHandle();
    protected CameraEffectsSettings _cameraEffects;

    protected LocomotionSettings _locomotionSettings => ScriptableSingleton<LocomotionSettings>.Instance;

    protected bool moveButtonPressed { get; private set; }

    private void OnEnable()
    {
      this._state = false;
      this._locomotionRoutine.Stop();
      this.Deinitialize();
    }

    public void SetState(bool state)
    {
      if (this._state == state)
        return;
      this._state = state;
      if (this._state)
        this.Initialize();
      this.OnStateChanged(state);
      if (this._state)
      {
        this._locomotionRoutine.Run(this.LocomotionRoutine());
        this.RegisterButton((EControllerKey) (Variable<EControllerKey>) this._locomotionSettings.LocomotionKey);
      }
      else
      {
        this._locomotionRoutine.Stop();
        this.UnregisterButton((EControllerKey) (Variable<EControllerKey>) this._locomotionSettings.LocomotionKey);
        CameraEffects.VignetteIntensity.SetValue(0.0f);
      }
    }

    private void HandleLocomotionButton(bool state)
    {
      if (state)
        ++this._keysDown;
      else
        --this._keysDown;
      this.UpdateMovementState();
    }

    private void UpdateMovementState()
    {
      bool moveButtonPressed = this.moveButtonPressed;
      this.moveButtonPressed = this._keysDown >= (this._locomotionSettings.RequireBothButtonsDown ? 2 : 1);
      if (this.moveButtonPressed != moveButtonPressed)
        this.OnMovementButtonChanged(this.moveButtonPressed);
      if (this._keysDown >= 0)
        return;
      Debug.LogError((object) "KeysDown < 0! Should never happen.");
    }

    private void Initialize()
    {
      if (this._initialized)
        return;
      this._cameraEffects = ScriptableSingleton<VRSettings>.Instance.cameraEffects;
      this._initialized = true;
      this.OnInitialize();
      this._locomotionSettings.LocomotionKey.OnValueChangedWithHistory += new Action<EControllerKey, EControllerKey>(this.HandleLocomotionKeyChanged);
    }

    private void HandleLocomotionKeyChanged(EControllerKey prevKey, EControllerKey newKey)
    {
      this.UnregisterButton(prevKey);
      this.RegisterButton(newKey);
    }

    private void RegisterButton(EControllerKey key)
    {
      this._keysDown = 0;
      foreach (HandData handData in this._handsDataModel.HandDatas)
      {
        VariableBool buttonState = handData.input.GetButtonState(key);
        if (buttonState.Value)
          ++this._keysDown;
        buttonState.OnValueChanged += new Action<bool>(this.HandleLocomotionButton);
      }
      this.UpdateMovementState();
    }

    private void UnregisterButton(EControllerKey key)
    {
      this._keysDown = 0;
      foreach (HandData handData in this._handsDataModel.HandDatas)
        handData.input.GetButtonState(key).OnValueChanged -= new Action<bool>(this.HandleLocomotionButton);
    }

    private void Deinitialize()
    {
      if (!this._initialized)
        return;
      this._keysDown = 0;
      this._initialized = false;
      this.OnDeinitialize();
      this._locomotionSettings.LocomotionKey.OnValueChangedWithHistory -= new Action<EControllerKey, EControllerKey>(this.HandleLocomotionKeyChanged);
    }

    protected abstract void OnInitialize();

    protected abstract void OnDeinitialize();

    protected abstract IEnumerator LocomotionRoutine();

    protected virtual void OnStateChanged(bool state)
    {
    }

    protected virtual void OnMovementButtonChanged(bool state)
    {
    }
  }
}
