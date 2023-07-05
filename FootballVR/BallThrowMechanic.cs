// Decompiled with JetBrains decompiler
// Type: FootballVR.BallThrowMechanic
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections.Generic;
using TB12.UI;
using UnityEngine;
using UnityEngine.XR;

namespace FootballVR
{
  public class BallThrowMechanic : MonoBehaviour
  {
    [SerializeField]
    private HandsDataModel _dataModel;
    [SerializeField]
    private ThrowManager _throwManager;
    private EHand _hand;
    private readonly Vector3Cache _positionCache = new Vector3Cache(90);
    private readonly Vector3Cache _positionCacheV2 = new Vector3Cache(90);
    private readonly Vector3Cache _velocityCacheV1 = new Vector3Cache(90);
    private readonly Vector3Cache _velocityCacheV2 = new Vector3Cache(90);
    private readonly Vector3Cache _velocityCacheV3 = new Vector3Cache(90);
    private readonly Vector3Cache _velocityCacheV4 = new Vector3Cache(90);
    private readonly Vector3Cache _velocityCacheV6 = new Vector3Cache(90);
    private readonly Vector3Cache _accelerationCache = new Vector3Cache(90);
    private int _controllerTrackedFrames;
    private Vector3 prevPos = Vector3.zero;
    private Transform _rigTx;
    private HandData _handData;
    private ThrowDebugSettings _debugSettings;
    public bool IsKeyBoardControl;
    public float KeyBoardThrowPower = 1f;
    public bool IsPassPlay = true;
    public bool IsRunTossPlay;
    public bool IsThrowAllowed = true;
    private InputDevice _inputDevice;
    private int _lastFrameUpdated;
    private Vector3 _estimatedHandPos;
    private Vector3 prevFixedPos;

    private ThrowSettings _throwSettings => ScriptableSingleton<ThrowSettings>.Instance;

    public Vector3Cache positionCacheV2 => this._positionCacheV2;

    public event Action<BallObject> OnBallReleased;

    public void Initialize(EHand controller)
    {
      this._hand = controller;
      this._handData = this._dataModel.GetHand(this._hand);
      this._handData.BallThrowMechanic = this;
      this._handData.OnBallPicked += new System.Action(this.HandleBallPicked);
      this._handData.OnBallReleased += new Action<BallObject>(this.ReleaseBall);
      this._debugSettings = this._throwSettings.DebugSettings;
      this._rigTx = PersistentSingleton<GamePlayerController>.Instance.Rig.transform;
      this._inputDevice = InputDevices.GetDeviceAtXRNode(controller == EHand.Right ? XRNode.RightHand : XRNode.LeftHand);
      this.IsThrowAllowed = true;
    }

    private void OnDestroy()
    {
      if (this._handData == null)
        return;
      this._handData.OnBallPicked -= new System.Action(this.HandleBallPicked);
      this._handData.OnBallReleased -= new Action<BallObject>(this.ReleaseBall);
    }

    private void HandleBallPicked()
    {
      this._accelerationCache.Clear();
      this._velocityCacheV2.Clear();
      this._velocityCacheV1.Clear();
      this._velocityCacheV3.Clear();
      this._positionCache.Clear();
    }

    public void Update()
    {
      int frameCount = Time.frameCount;
      if (this._lastFrameUpdated == frameCount)
        return;
      float deltaTime = Time.deltaTime;
      this._lastFrameUpdated = frameCount;
      if (this.IsTracked())
        ++this._controllerTrackedFrames;
      else
        this._controllerTrackedFrames = 0;
      Vector3 pos1 = this._rigTx.InverseTransformPoint(this.transform.position);
      this._positionCache.PushValue(pos1);
      Vector3 pos2 = (pos1 - this.prevPos) / deltaTime;
      this.prevPos = pos1;
      this._velocityCacheV1.PushValue(pos2);
      Transform root = this.transform.root;
      this._velocityCacheV2.PushValue(root.TransformDirection(this.GetVelocity()));
      this._accelerationCache.PushValue(root.TransformDirection(this.GetAcceleration()) * deltaTime);
    }

    private void LateUpdate()
    {
      if (this._controllerTrackedFrames == 0)
      {
        if (this._velocityCacheV3.Count == 0)
          return;
        Vector3 vector3_1 = this._velocityCacheV3[this._velocityCacheV3.Count - 1];
        Vector3 vector3_2 = this.transform.root.TransformDirection(this.GetAcceleration());
        this._velocityCacheV3.PushValue(vector3_1 + vector3_2 * Time.deltaTime);
        if (this._throwSettings.ThrowConfig.ThrowVersion != 3)
          return;
        this._estimatedHandPos += vector3_1 * Time.deltaTime;
      }
      else
      {
        this._velocityCacheV3.PushValue(this.transform.root.TransformDirection(this.GetVelocity()));
        this._estimatedHandPos = this._rigTx.InverseTransformPoint(this.transform.position);
      }
    }

    private void FixedUpdate()
    {
      if (!this._handData.hasController)
        return;
      if (this._handData.hasObject && (UnityEngine.Object) this._handData.CurrentObject != (UnityEngine.Object) null)
        this._positionCacheV2.PushValue(this._rigTx.InverseTransformPoint(this._handData.CurrentObject.transform.position));
      if (this.IsTracked() && !VRUtils.ViveConnected)
      {
        if (this._velocityCacheV4.Count > 0)
        {
          Vector3 vector3 = this._velocityCacheV4[this._velocityCacheV4.Count - 1];
          Vector3 acceleration = this.GetAcceleration();
          float fixedDeltaTime = Time.fixedDeltaTime;
          float num = Mathf.Clamp01(this._throwSettings.ThrowConfig.PredictedVelocityFalloff);
          this._velocityCacheV4.PushValue(vector3 * num + acceleration * fixedDeltaTime);
          if (this._throwSettings.ThrowConfig.ThrowVersion != 3)
            this._estimatedHandPos += vector3 * fixedDeltaTime;
        }
      }
      else
      {
        this._velocityCacheV4.PushValue(this.GetVelocity());
        this._estimatedHandPos = this._rigTx.InverseTransformPoint(this.transform.position);
      }
      Vector3 vector3_1 = this._rigTx.InverseTransformPoint(this._handData.hand.attachPoint.position);
      this._velocityCacheV6.PushValue((vector3_1 - this.prevFixedPos) / Time.fixedDeltaTime);
      this.prevFixedPos = vector3_1;
    }

    private void ReleaseBall(BallObject ball)
    {
      if (this.IsThrowAllowed)
      {
        Action<BallObject> onBallReleased = this.OnBallReleased;
        if (onBallReleased != null)
          onBallReleased(ball);
        if (!this._handData.hasObject || !this._handData.hasController)
          return;
        this.Update();
        if (this._debugSettings.CompareThrowVersionRed >= 0)
        {
          Vector3 throwVector = this.GetThrowVector(this._debugSettings.CompareThrowVersionRed);
          this.ThrowComparisonBall(ball.transform.position, throwVector, Color.red);
        }
        if (this._debugSettings.CompareThrowVersionBlue >= 0)
        {
          Vector3 throwVector = this.GetThrowVector(this._debugSettings.CompareThrowVersionBlue);
          this.ThrowComparisonBall(ball.transform.position, throwVector, Color.blue);
        }
        if (!this.IsTracked() && this._controllerTrackedFrames == 0 && !VRUtils.ViveConnected)
          this.HandleThrowFromBehind(ball);
        if (this._throwSettings.DebugMode)
        {
          Vector3 throwVector1 = this.GetThrowVector(4);
          Vector3 throwVector2 = this.GetThrowVector(6);
          Debug.Log((object) (string.Format("V4: {0} : {1}\r\n", (object) throwVector1.magnitude, (object) throwVector1) + string.Format("V6: {0} : {1}", (object) throwVector2.magnitude, (object) throwVector2)));
        }
        Vector3 throwVector3 = this.GetThrowVector(this._throwSettings.ThrowConfig.ThrowVersion);
        if (this.IsKeyBoardControl)
        {
          Transform transform = PlayerCamera.Camera.transform;
          if (this.IsPassPlay)
          {
            throwVector3 = (transform.forward + transform.up * 0.2f).normalized * Mathf.Clamp(this.KeyBoardThrowPower, 3f, 40f);
          }
          else
          {
            if (!this.IsRunTossPlay)
              return;
            throwVector3 = (transform.forward * 0.3f + transform.right * 0.8f + transform.up * 0.2f).normalized * 3.5f;
          }
        }
        Vector3 throwVector4 = this.AdjustThrowVector(throwVector3);
        this.ThrowBall(ball, throwVector4);
      }
      else
        Debug.Log((object) "Ball Released but Throw not allowed");
    }

    private void HandleThrowFromBehind(BallObject ball)
    {
      if (this._throwSettings.DebugMode)
        GameplayUI.ShowText("Throw from behind!");
      Vector3 vector3 = this._estimatedHandPos + (this._handData.hand.attachPoint.position - this.transform.position) - ball.transform.position;
      if ((double) vector3.magnitude > 0.20000000298023224)
        vector3 = vector3.normalized * 0.2f;
      ball.transform.position += vector3;
    }

    public Vector3 AdjustThrowVector(Vector3 throwVector)
    {
      float angleCorrection = this._throwSettings.ThrowConfig.AngleCorrection;
      if (this._hand != EHand.Right)
        angleCorrection *= -1f;
      Vector2 vector2 = new Vector2(throwVector.x, throwVector.z).Rotate(angleCorrection);
      throwVector = new Vector3(vector2.x, throwVector.y, vector2.y);
      return throwVector;
    }

    public Vector3 GetThrowVector(int throwVersion)
    {
      switch (throwVersion)
      {
        case 1:
          return ThrowMath.GetThrowVector((IReadOnlyList<Vector3>) this._velocityCacheV1);
        case 2:
          return this._controllerTrackedFrames <= 2 ? ThrowMath.GetVectorBasedOnAcceleration((IReadOnlyList<Vector3>) this._accelerationCache, out int _) : ThrowMath.GetThrowVector((IReadOnlyList<Vector3>) this._velocityCacheV2);
        case 3:
          return ThrowMath.GetThrowVector((IReadOnlyList<Vector3>) this._velocityCacheV3);
        case 4:
          return this.transform.root.TransformDirection(ThrowMath.GetAverageVector((IReadOnlyList<Vector3>) this._velocityCacheV4, this._throwSettings.ThrowConfig.FramesTracked, minThreshold: -1f));
        case 7:
          Vector3 throwVector1 = this.GetThrowVector(4);
          Vector3 vector3_1 = throwVector1.SetY(0.0f);
          Vector3 throwVector2 = this.GetThrowVector(6);
          return (Vector3.Lerp(vector3_1.normalized, throwVector2.SetY(0.0f).normalized, 0.5f).normalized * vector3_1.magnitude).SetY(throwVector1.y);
        case 8:
          Vector3 throwVector3 = this.GetThrowVector(4);
          Vector3 throwVector4 = this.GetThrowVector(6);
          return Vector3.Lerp(throwVector3.normalized, throwVector4.normalized, 0.5f).normalized * throwVector3.magnitude;
        case 9:
          return this.transform.root.TransformDirection(ThrowMath.GetWeightedVector((IReadOnlyList<Vector3>) this._velocityCacheV4, this._throwSettings.ThrowConfig.FramesTracked, this._throwSettings.ThrowConfig.WeightDecreaseFactor));
        case 10:
          return !this.IsTracked() && this._controllerTrackedFrames == 0 && !VRUtils.ViveConnected ? this.GetThrowVector(9) : ThrowMath.GetWeightedVector((IReadOnlyList<Vector3>) this._velocityCacheV6, this._throwSettings.ThrowConfig.FramesTracked, this._throwSettings.ThrowConfig.WeightDecreaseFactor);
        case 11:
          Vector3 b = this.transform.root.TransformDirection(this.GetAcceleration());
          return Vector3.Lerp(this.GetThrowVector(10), b, 0.3f);
        case 12:
          Vector3 vector3_2 = ThrowMath.GetAverageVector((IReadOnlyList<Vector3>) this._velocityCacheV4, 3, minThreshold: -1f).SetY(0.0f);
          Vector3 throwVector5 = this.GetThrowVector(6);
          Vector3 throwVector6 = this.GetThrowVector(4);
          return (Vector3.Lerp(vector3_2.normalized, throwVector5.SetY(0.0f).normalized, 0.5f).normalized * throwVector6.magnitude).SetY(throwVector6.y);
        default:
          return !this.IsTracked() && this._controllerTrackedFrames == 0 && !VRUtils.ViveConnected ? this.GetThrowVector(4) : this._rigTx.TransformVector(ThrowMath.GetAverageVector((IReadOnlyList<Vector3>) this._velocityCacheV6, this._throwSettings.ThrowConfig.FramesTracked));
      }
    }

    public BallObject ThrowComparisonBall(
      Vector3 pos,
      Vector3 throwVec,
      Color color,
      bool hideTrail = true)
    {
      throwVec = this._throwSettings.ThrowConfig.ApplyConfig(throwVec);
      BallObject ballObject = UnityEngine.Object.Instantiate<BallObject>(this._dataModel.defaultBall, pos, Quaternion.identity);
      ballObject.DebugThrow(pos, throwVec, color, hideTrail);
      return ballObject;
    }

    public void ThrowBall(BallObject ball, Vector3 throwVector)
    {
      ball.SetReplayDataRawThrowVector(throwVector);
      float f = this.transform.lossyScale.x;
      if ((double) f > 1.0)
      {
        f = Mathf.Sqrt(f);
        throwVector /= f;
      }
      InteractionSettings interactionSettings = this._throwSettings.InteractionSettings;
      if (interactionSettings.VibrationEnabled)
        ControllerUtilities.InteractionHaptics(this._hand, false, interactionSettings.VibrationDuration);
      bool flag = (double) throwVector.magnitude >= (double) this._throwSettings.ThrowConfig.MinThrowThreshold * (double) f;
      int targetId = -1;
      float accuracy = 0.0f;
      if (flag)
      {
        ThrowData throwData = this._throwManager.ProcessThrow(ball, throwVector);
        if (throwData != null)
        {
          throwVector = throwData.throwVector;
          if (throwData.hasTarget && throwData.closestTarget is RealPlayerTarget closestTarget)
            targetId = closestTarget.playerId;
          accuracy = throwData.accuracy;
        }
      }
      ball.Throw(ball.transform.position, throwVector, flag, flag, accuracy: accuracy, targetId: targetId);
    }

    private bool IsTracked()
    {
      bool flag;
      return !this._inputDevice.TryGetFeatureValue(CommonUsages.isTracked, out flag) | flag;
    }

    private Vector3 GetVelocity()
    {
      Vector3 vector3;
      return !this._inputDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out vector3) ? Vector3.zero : vector3;
    }

    private Vector3 GetAcceleration()
    {
      Vector3 vector3;
      return !this._inputDevice.TryGetFeatureValue(CommonUsages.deviceAcceleration, out vector3) ? Vector3.zero : vector3;
    }
  }
}
