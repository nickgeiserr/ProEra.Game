// Decompiled with JetBrains decompiler
// Type: FootballVR.HandData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DSE;
using FootballVR.Multiplayer;
using FootballVR.UI;
using Framework;
using System;
using System.Collections;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class HandData : ITouchInput
  {
    public readonly VariableEnum<EHandPose> pose = new VariableEnum<EHandPose>(EHandPose.Empty);
    private readonly HandsDataModel _model;
    public readonly ControllerInput input;
    public readonly EHand controller;
    private readonly RoutineHandle _tryCatchRoutine = new RoutineHandle();
    private readonly RoutineHandle _carryObjectRoutine = new RoutineHandle();
    private bool _catchingEnabled = true;
    private HandController _hand;
    private BallThrowMechanic _ballThrowMechanic;
    private BallObject _currentObject;
    private readonly RoutineHandle _menuRoutine = new RoutineHandle();
    private readonly WaitForSecondsRealtime _menuHoldDelay = new WaitForSecondsRealtime(0.0f);

    public event Action<BallObject> OnBallReleased;

    public event System.Action OnBallPicked;

    public Vector3 position => !this.hasController ? Vector3.zero : this.hand.position;

    public Vector3 touchPosition => !this.hasController ? Vector3.zero : this.hand.fingerPosition;

    public Vector3 dragPosition => !this.hasController ? Vector3.zero : this.hand.dragPosition;

    public Vector3 dragRotation => !this.hasController ? Vector3.zero : this.hand.dragRotation;

    public Transform dragPivot => !this.hasController ? (Transform) null : this.hand.GetDragPivot();

    public EHand dragHand => !this.hasController ? EHand.Right : this.hand.GetDragHand();

    public Transform laserDragTransform => !this.hasController ? (Transform) null : this.hand.laserDragTransform;

    public bool hasController { get; private set; }

    public HandController hand
    {
      get => this._hand;
      private set
      {
        this.hasController = (UnityEngine.Object) value != (UnityEngine.Object) null;
        this._hand = value;
      }
    }

    public BallThrowMechanic BallThrowMechanic
    {
      get => this._ballThrowMechanic;
      set => this._ballThrowMechanic = value;
    }

    public bool hasObject { get; private set; }

    public BallObject CurrentObject
    {
      get => this._currentObject;
      set
      {
        this._tryCatchRoutine.Stop();
        this._carryObjectRoutine.Stop();
        if (this.hasObject && (UnityEngine.Object) this.CurrentObject == (UnityEngine.Object) value && (UnityEngine.Object) value != (UnityEngine.Object) null)
          return;
        if (this.hasObject && (UnityEngine.Object) this.CurrentObject != (UnityEngine.Object) null)
        {
          this.CurrentObject.Release();
          Action<BallObject> onBallReleased = this.OnBallReleased;
          if (onBallReleased != null)
            onBallReleased(this.CurrentObject);
        }
        if (!this.hasController)
          return;
        this.hasObject = (UnityEngine.Object) value != (UnityEngine.Object) null;
        if (this.hasObject)
        {
          this._hand.PickBall(value);
          System.Action onBallPicked = this.OnBallPicked;
          if (onBallPicked != null)
            onBallPicked();
          InteractionSettings interactionSettings = this._model.settings.InteractionSettings;
          if (interactionSettings.VibrationEnabled)
            ControllerUtilities.InteractionHaptics(this.controller, true, interactionSettings.VibrationDuration);
        }
        this._currentObject = value;
        if (UnityState.quitting)
          return;
        EHandPose ehandPose = !this.hasObject ? EHandPose.Empty : this.GetHandPose();
        if (this.pose.Value != ehandPose)
          this.pose.SetValue(ehandPose);
        if (!this.hasObject)
          return;
        this._carryObjectRoutine.Run(this.CarryObjectRoutine());
      }
    }

    public HandData(HandsDataModel model, EHand controller)
    {
      this._model = model;
      this.controller = controller;
      this.input = new ControllerInput(controller, this._model.settings.InteractionSettings.TriggerPressThreshold);
      this.input.objectInteractPressed.OnValueChanged += new Action<bool>(this.HandleObjectInteract);
      this.input.menuPressed.OnValueChanged += new Action<bool>(this.HandleMenuButton);
      TouchUI.TouchInputs.Add((ITouchInput) this);
    }

    private void HandleObjectInteract(bool pressed)
    {
      if (pressed && this._catchingEnabled)
      {
        if (this.hasObject)
          return;
        this._tryCatchRoutine.Run(this.TryCatchRoutine());
      }
      else
      {
        if (!this.hasObject)
          return;
        this.CurrentObject = (BallObject) null;
      }
    }

    private IEnumerator TryCatchRoutine()
    {
      float endTime = Time.time + this._model.settings.InteractionSettings.CatchTestDuration;
      while ((double) Time.time < (double) endTime && !this.PickItem())
        yield return (object) null;
    }

    private bool PickItem()
    {
      BallObject ballObject;
      if (!this._model.GetClosestValidObject(this.position, out ballObject) && !this._model.GetBallFromContainer(this.position, out ballObject) || (UnityEngine.Object) ballObject == (UnityEngine.Object) null)
        return false;
      BallObjectNetworked component;
      if (ballObject.TryGetComponent<BallObjectNetworked>(out component))
        component.ApplyBallCustomization();
      return this._model.TryGrabBall(ballObject);
    }

    private void HandleMenuButton(bool pressed)
    {
      if (pressed)
        this._menuRoutine.Run(this.HandleMenuRoutine());
      else
        this._menuRoutine.Stop();
    }

    private IEnumerator HandleMenuRoutine()
    {
      yield return (object) this._menuHoldDelay;
      if (VRState.PausePermission && !PersistentSingleton<GamePlayerController>.Instance.IsFaded)
        VRState.PauseMenu.Toggle();
    }

    private IEnumerator CarryObjectRoutine()
    {
      float holdBallUpStartTime = 0.0f;
      bool holdingBallUp = false;
      TwoHandedSettings settings = this._model.settings.TwoHandedSettings;
      while (this.hasObject)
      {
        float time = Time.time;
        bool flag = (double) this.CurrentObject.transform.position.y > 1.5;
        if (!flag)
          holdBallUpStartTime = time;
        else if (!holdingBallUp)
          holdBallUpStartTime = time;
        holdingBallUp = flag;
        EHandPose handPose = this.GetHandPose(this._model.movingSpeed, (double) time - (double) holdBallUpStartTime > (double) settings.ThrowPoseBallUpTimeThreshold);
        if (this.pose.Value != handPose)
          this.pose.SetValue(handPose);
        yield return (object) null;
      }
    }

    private EHandPose GetHandPose(float velocity = 0.0f, bool ballUp = false) => this._model.playerRole == EPlayerRole.kWideReceiver || !ballUp && (double) velocity > (double) this._model.settings.TwoHandedSettings.CarryPoseSpeedThreshold ? EHandPose.Cradle : EHandPose.QbGrip;

    public void SetHandController(HandController handController) => this.hand = handController;

    public void EnableCatching(bool enabled) => this._catchingEnabled = enabled;
  }
}
