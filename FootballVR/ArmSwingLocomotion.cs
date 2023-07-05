// Decompiled with JetBrains decompiler
// Type: FootballVR.ArmSwingLocomotion
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Locomotion/ArmSwingLocomotion")]
  public class ArmSwingLocomotion : LocomotionMechanic
  {
    [SerializeField]
    private HeadTiltHandler _headTiltHandler;
    [SerializeField]
    private ForwardHandler _forwardHandler;
    private ArmSwingSettings _settings;
    private readonly List<ArmSwingController> _controllers = new List<ArmSwingController>(2);
    private PlayerRig _camRig;
    private Transform _camRigTx;
    private Transform _cameraTx;
    private float _currentSpeed;
    private GamePlayerController _player;
    private Vector3 _prevHeadPos;
    private float _headAcceleration;
    private Vector3 _forwardDir;

    public Vector3 CurrentForward => this._forwardDir;

    public float CurrentSpeed
    {
      get => this._currentSpeed;
      set => this._currentSpeed = value;
    }

    public float normalizedSpeed => this._currentSpeed / this.maxSpeed;

    public float maxSpeed => this._settings.maxSpeed;

    public static float LocomotionTimeSinceSceneLoad { get; private set; }

    protected override void OnInitialize()
    {
      this._settings = this._locomotionSettings.ArmSwingSettings;
      this._player = PersistentSingleton<GamePlayerController>.Instance;
      this._camRig = this._player.Rig;
      this._camRigTx = this._camRig.transform;
      this._cameraTx = PlayerCamera.Camera.transform;
      this._controllers.Add(new ArmSwingController(this._camRig.rightHandAnchor, this._settings));
      this._controllers.Add(new ArmSwingController(this._camRig.leftHandAnchor, this._settings));
      this._currentSpeed = 0.0f;
      this._headTiltHandler.SetState(true);
      this._forwardHandler.SetState(true);
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneManagerOnSceneLoaded);
    }

    protected override void OnDeinitialize()
    {
      this._controllers.Clear();
      this._forwardHandler.Deinitialize();
      SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneManagerOnSceneLoaded);
    }

    private void OnSceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => ArmSwingLocomotion.LocomotionTimeSinceSceneLoad = 0.0f;

    protected override void OnStateChanged(bool state)
    {
      if (UnityState.quitting)
        return;
      CameraEffects.VignetteState.SetValue(state);
      if (!state)
        return;
      this._prevHeadPos = this._cameraTx.localPosition;
      this._forwardDir = this._forwardHandler.ForwardDirection;
      this._currentSpeed = 0.0f;
      foreach (ArmSwingController controller in this._controllers)
      {
        controller.charge = this._settings.maxCharge / 2f;
        controller.Prepare();
      }
    }

    protected override void OnMovementButtonChanged(bool state)
    {
      if (!state)
        return;
      foreach (ArmSwingController controller in this._controllers)
        controller.Prepare();
    }

    private void ProcessRecharge(ArmSwingController data)
    {
      foreach (ArmSwingController controller in this._controllers)
      {
        if (controller != data)
          controller.charge = Mathf.Clamp(controller.charge + data.recharge, 0.0f, this._settings.maxCharge);
      }
    }

    protected override IEnumerator LocomotionRoutine()
    {
      ArmSwingLocomotion armSwingLocomotion = this;
      while (true)
      {
        Vector3 forwardDirection = armSwingLocomotion._forwardHandler.ForwardDirection;
        float turnAngle = armSwingLocomotion._locomotionSettings.maintainRunOnRelease ? 0.0f : Mathf.Abs(Vector3.Angle(armSwingLocomotion._forwardDir, forwardDirection));
        if (!armSwingLocomotion._locomotionSettings.maintainRunOnRelease || armSwingLocomotion.moveButtonPressed)
          armSwingLocomotion._forwardDir = forwardDirection;
        Vector3 localPosition = armSwingLocomotion._cameraTx.localPosition;
        Vector3 vector3 = armSwingLocomotion._prevHeadPos - localPosition;
        armSwingLocomotion._prevHeadPos = localPosition;
        armSwingLocomotion._headAcceleration = Mathf.Lerp(armSwingLocomotion._headAcceleration, vector3.magnitude, armSwingLocomotion._settings.headLerpCoef);
        armSwingLocomotion._headAcceleration = Mathf.Clamp(armSwingLocomotion._headAcceleration, 0.0f, armSwingLocomotion._settings.maxHeadAccel);
        if ((bool) armSwingLocomotion._locomotionSettings.ShowDebug)
          Singleton<DebugUI>.Instance.rightTextDown.text = string.Format("HeadCharge: {0:F}", (object) (float) ((double) armSwingLocomotion._headAcceleration / (double) armSwingLocomotion._settings.maxHeadAccel));
        if ((double) Mathf.Abs(armSwingLocomotion._currentSpeed) < 9.9999999747524271E-07 && !armSwingLocomotion.moveButtonPressed)
        {
          yield return (object) null;
        }
        else
        {
          Vector3 forwDir = armSwingLocomotion._camRigTx.InverseTransformDirection(armSwingLocomotion._forwardDir);
          if (armSwingLocomotion.moveButtonPressed)
            armSwingLocomotion.UpdateControllers(forwDir);
          armSwingLocomotion._currentSpeed = Mathf.Clamp(armSwingLocomotion._currentSpeed, (float) (-(double) armSwingLocomotion.maxSpeed / 2.0), armSwingLocomotion.maxSpeed);
          Vector3 delta = armSwingLocomotion._forwardDir * (armSwingLocomotion._currentSpeed * Time.deltaTime);
          ArmSwingLocomotion.LocomotionTimeSinceSceneLoad += Time.deltaTime;
          armSwingLocomotion._player.Move(delta);
          if ((bool) armSwingLocomotion._locomotionSettings.ShowDebug)
            Singleton<DebugUI>.Instance.rightTextUp.text = string.Format("Speed: {0:F}", (object) (float) ((double) armSwingLocomotion._currentSpeed / (double) armSwingLocomotion.maxSpeed));
          armSwingLocomotion.Decelerate(turnAngle);
          CameraEffects.VignetteIntensity.SetValue(Mathf.Lerp((float) CameraEffects.VignetteIntensity, armSwingLocomotion._currentSpeed / armSwingLocomotion.maxSpeed, armSwingLocomotion._cameraEffects.VignetteLerpFactor));
          yield return (object) null;
        }
      }
    }

    private void UpdateControllers(Vector3 forwDir)
    {
      float tiltAngle = this._headTiltHandler.GetTiltAngle();
      for (int index = 0; index < this._controllers.Count; ++index)
      {
        ArmSwingController controller = this._controllers[index];
        controller.Update(forwDir, (double) tiltAngle > 0.0 || !this._locomotionSettings.allowMoveBackwards);
        if ((double) Mathf.Abs(controller.acceleration) > (double) this._settings.minAccel && (!this._settings.requireLeanForward || (double) Mathf.Abs(tiltAngle) > (double) this._settings.minLeanAngle))
        {
          float acceleration = controller.acceleration;
          if (this._settings.multiplyWithHeadAccel)
            acceleration *= Mathf.Clamp(this._headAcceleration, this._settings.maxHeadAccel * 0.45f, this._settings.maxHeadAccel) * 1.1f / this._settings.maxHeadAccel;
          this._currentSpeed += acceleration;
          controller.charge = Mathf.Clamp(controller.charge - Mathf.Abs(controller.acceleration), 0.0f, this._settings.maxCharge);
        }
        if ((double) controller.recharge > 3.0 / 1000.0)
          this.ProcessRecharge(controller);
      }
      if (!(bool) this._locomotionSettings.ShowDebug)
        return;
      Singleton<DebugUI>.Instance.leftTextDown.text = string.Format("HandAccel: {0:F}", (object) (float) ((double) this._controllers[0].acceleration / (double) this._settings.maxAcceleration));
    }

    private void Decelerate(float turnAngle = 0.0f)
    {
      float num = Mathf.Abs(this.GetDeceleration(turnAngle));
      if ((double) Mathf.Abs(this._currentSpeed) > (double) num)
        this._currentSpeed = Mathf.Clamp(this._currentSpeed - num * Mathf.Sign(this._currentSpeed), (float) (-(double) this.maxSpeed / 2.0), this.maxSpeed);
      else
        this._currentSpeed = 0.0f;
    }

    private float GetDeceleration(float turnAngle)
    {
      switch (this._settings.DecelerationType.Value)
      {
        case EAccelerationProfile.V1:
          AccelerationSettings accelerationV1 = this._settings.accelerationV1;
          float deceleration1 = (float) ((double) Mathf.Abs(this._currentSpeed) * (double) Mathf.Clamp01(accelerationV1.decelerationRate * 0.1f) + (double) accelerationV1.decelerationConstant * 0.019999999552965164 + (double) accelerationV1.decelerationOnTurning * (double) turnAngle);
          if (this._locomotionSettings.maintainRunOnRelease)
            deceleration1 *= this._locomotionSettings.maintainRunDecelerationCoef;
          return deceleration1;
        case EAccelerationProfile.V2:
          AccelerationSettings accelerationV2 = this._settings.accelerationV2;
          float unscaledDeltaTime1 = Time.unscaledDeltaTime;
          return (float) ((double) Mathf.Abs(this._currentSpeed) * (1.0 - (double) Mathf.Pow(1f - accelerationV2.decelerationRate, unscaledDeltaTime1)) + (double) accelerationV2.decelerationConstant * (double) unscaledDeltaTime1 + (double) accelerationV2.decelerationOnTurning * (double) turnAngle);
        default:
          AccelerationSettings accelerationV3 = this._settings.accelerationV3;
          float unscaledDeltaTime2 = Time.unscaledDeltaTime;
          float deceleration2 = (float) ((double) Mathf.Abs(this._currentSpeed) * (double) accelerationV3.decelerationRate * (double) unscaledDeltaTime2 + (double) accelerationV3.decelerationConstant * (double) unscaledDeltaTime2 + (double) accelerationV3.decelerationOnTurning * (double) turnAngle);
          if (this._locomotionSettings.maintainRunOnRelease)
            deceleration2 *= this._locomotionSettings.maintainRunDecelerationCoef;
          return deceleration2;
      }
    }
  }
}
