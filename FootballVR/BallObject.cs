// Decompiled with JetBrains decompiler
// Type: FootballVR.BallObject
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using Framework.Data;
using System;
using System.Collections;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class BallObject : MonoBehaviour
  {
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    protected Rigidbody _rigidbody;
    [SerializeField]
    protected PlayerProfile _playerProfile;
    [SerializeField]
    protected BallGraphics _graphics;
    [SerializeField]
    protected Material _defaultBallMat;
    [SerializeField]
    private bool _vrTraining;
    [SerializeField]
    private bool _inFlight;
    [SerializeField]
    private bool _inHand;
    public System.Action OnBallDrop;
    private ReplayData replayData;
    private bool _applyPhysics = true;
    private FlightSettings _flightSettings;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _flightRoutine = new RoutineHandle();
    private static readonly WaitForFixedUpdate _waitFixedUpdate = new WaitForFixedUpdate();

    protected ThrowSettings _throwSettings => ScriptableSingleton<ThrowSettings>.Instance;

    public virtual int OwnerId => -1;

    public BallGraphics Graphics => this._graphics;

    public Collider Collider => this._collider;

    public bool inHand
    {
      get => this._inHand;
      private set
      {
        this._inHand = value;
        this._rigidbody.interpolation = this._inHand ? RigidbodyInterpolation.None : (RigidbodyInterpolation) (Variable<RigidbodyInterpolation>) this._throwSettings.BallPhysicsSettings.InterpolationMethod;
      }
    }

    public bool twoHandedGrab { get; private set; }

    public bool inFlight
    {
      get => this._inFlight;
      set
      {
        if ((UnityEngine.Object) this._throwSettings != (UnityEngine.Object) null)
          this._graphics.InFlightHighlight = value && this._throwSettings.showBallOutline;
        this._inFlight = value;
      }
    }

    public virtual bool userThrown { get; set; } = true;

    public bool applyPhysics
    {
      set
      {
        if (this._applyPhysics == value)
          return;
        this._applyPhysics = value;
        if (!value)
          this._rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        this._rigidbody.isKinematic = !value;
        if (!value)
          return;
        this._rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        this._rigidbody.useGravity = true;
        this._rigidbody.detectCollisions = true;
      }
    }

    public bool hasHitTarget { get; private set; }

    private Vector3 throwStartPos { get; set; }

    protected virtual void Awake()
    {
      this.Graphics.BallMaterial.SetValue(this._defaultBallMat);
      this._flightSettings = this._throwSettings.FlightSettings;
      this.OnValidate();
      this.ResetState();
      if (this._vrTraining)
        return;
      this._rigidbody.maxAngularVelocity = 62.831852f;
    }

    private void OnValidate()
    {
      if (!((UnityEngine.Object) this._collider == (UnityEngine.Object) null))
        return;
      this._collider = this.GetComponent<Collider>();
    }

    private void OnDisable() => this.ResetState();

    protected virtual void OnDestroy() => this._linksHandler.Clear();

    public void ResetState(bool resetTrail = true)
    {
      this._graphics.ResetState(resetTrail);
      if (!this._vrTraining)
      {
        this._rigidbody.velocity = Vector3.zero;
        this._rigidbody.angularVelocity = Vector3.zero;
        this._rigidbody.useGravity = true;
        this._collider.isTrigger = false;
        this._collider.enabled = true;
      }
      this.hasHitTarget = false;
      this._flightRoutine.Stop();
      this.twoHandedGrab = false;
      this.inFlight = false;
      this.userThrown = true;
      this.replayData = new ReplayData();
    }

    public virtual void Pick(bool twoHanded = false, bool hideTrail = true)
    {
      this.ResetState(hideTrail);
      this.inHand = true;
      this.applyPhysics = false;
      this.twoHandedGrab = twoHanded;
    }

    public virtual void Throw(
      Vector3 startPosition,
      Vector3 throwDirection,
      bool throwActivated,
      bool trailEnabled = true,
      bool hideTrail = true,
      float accuracy = 0.5f,
      int targetId = -1)
    {
      this.Release();
      this.throwStartPos = startPosition;
      this.transform.position = startPosition;
      Debug.Log((object) ("throwActivated: " + throwActivated.ToString()));
      if (throwActivated)
      {
        this._flightRoutine.Run(this.BallFlightRoutine(throwDirection, accuracy, true));
      }
      else
      {
        this._flightRoutine.Stop();
        this.CompleteBallFlight(false);
      }
      this._rigidbody.velocity = Vector3.zero;
      this._rigidbody.AddForce(throwDirection, ForceMode.Impulse);
      if ((double) throwDirection.magnitude < 0.5)
      {
        System.Action onBallDrop = this.OnBallDrop;
        if (onBallDrop != null)
          onBallDrop();
      }
      if (!this._graphics.customBallColor)
        this.ApplyTrail();
      this._graphics.Trail.TrailEnabled = trailEnabled;
      if (!(trailEnabled & hideTrail))
        return;
      this._graphics.HideTrail();
    }

    public void ActivateBallFlightRoutine(Vector3 throwDirection, float accuracy = 0.5f)
    {
      this.userThrown = false;
      this._flightRoutine.Run(this.BallFlightRoutine(throwDirection, accuracy, true));
    }

    protected virtual void ApplyTrail()
    {
      if (!((UnityEngine.Object) this._graphics.Trail != (UnityEngine.Object) null))
        return;
      this._graphics.TrailColor = (Color) this._playerProfile.Customization.TrailColor;
      this._graphics.TrailType.SetValue((EBallTrail) (Variable<EBallTrail>) this._playerProfile.Customization.TrailType);
    }

    public Vector3 ThrowToPosition(Vector3 targetPos, float flightTime, bool trailEnabled)
    {
      this.ResetState();
      Vector3 position = this.transform.position;
      Vector3 impulseToHitTarget = AutoAim.GetImpulseToHitTarget(targetPos - position, flightTime);
      this.Throw(position, impulseToHitTarget, true, trailEnabled);
      return impulseToHitTarget;
    }

    public virtual void CompleteBallFlight(bool throwSuccess = true)
    {
      if (this.hasHitTarget)
        return;
      this._graphics.Trail.TrailEmitting = false;
      this.hasHitTarget = true;
      Debug.Log((object) "Completed Ball Flight");
      if (!this.userThrown)
        return;
      float magnitude = (this.transform.position - this.throwStartPos).magnitude;
      VREvents.ThrowResult.Trigger(throwSuccess, magnitude);
    }

    public virtual void Release()
    {
      this.applyPhysics = true;
      this.transform.SetParent((Transform) null, true);
      this.inHand = false;
    }

    public Vector3 catchPosition
    {
      get
      {
        Vector3 position = this.transform.position;
        if (!this.inFlight)
          return position;
        Vector3 vector3 = Time.deltaTime * this._throwSettings.InteractionSettings.CatchPositionDelay * this._rigidbody.velocity;
        return position - vector3;
      }
    }

    private IEnumerator BallFlightRoutine(
      Vector3 trajectoryDirection,
      float accuracy = 0.5f,
      bool ignoreSpeed = false)
    {
      BallObject ballObject = this;
      if (!(trajectoryDirection == Vector3.zero))
      {
        ballObject.inFlight = true;
        Transform myTx = ballObject.transform;
        Vector3 prevPos = myTx.position;
        myTx.rotation = Quaternion.LookRotation(-trajectoryDirection);
        Debug.Log((object) ("ball start rotation: " + myTx.localEulerAngles.ToString()));
        float num1 = Mathf.Lerp(Mathf.Clamp01(trajectoryDirection.magnitude / 23f), 1f, ballObject._flightSettings.BallSpinSpeedFactor);
        float num2 = Mathf.Lerp(ballObject._flightSettings.BallRotationWobbles, ballObject._flightSettings.BallRotationWobblesMin, accuracy);
        float z = (float) ((double) ballObject._flightSettings.BallRotationsPerSecond * 360.0 * (Math.PI / 180.0)) * num1;
        float num3 = (float) ((double) ballObject._flightSettings.BallRotationWobbles * 360.0 * (Math.PI / 180.0)) * num1 * num2;
        Vector3 angularRot = new Vector3(num3, num3, z);
        int frameCount = 0;
        ballObject._collider.enabled = false;
        yield return (object) BallObject._waitFixedUpdate;
        float minHeight = 0.25f * ballObject.transform.localScale.x;
        float speed;
        do
        {
          ballObject._collider.enabled = frameCount > 2;
          trajectoryDirection.y /= ballObject._flightSettings.BallStabilizer;
          speed = trajectoryDirection.magnitude;
          if ((double) speed > 0.0099999997764825821)
            ballObject._rigidbody.rotation = Quaternion.Slerp(ballObject._rigidbody.rotation, Quaternion.LookRotation(-trajectoryDirection, myTx.up), ballObject._flightSettings.BallDirectionLerpFactor);
          ballObject._rigidbody.angularVelocity = ballObject.transform.TransformDirection(angularRot);
          Vector3 position = myTx.position;
          trajectoryDirection = position - prevPos;
          prevPos = position;
          yield return (object) BallObject._waitFixedUpdate;
          ++frameCount;
        }
        while ((ignoreSpeed || (double) speed > 0.019999999552965164) && (double) prevPos.y > (double) minHeight && !ballObject.hasHitTarget && !ballObject.inHand);
        ballObject._rigidbody.angularVelocity /= 2f;
        ballObject.inFlight = false;
        if (!ballObject.hasHitTarget && !ballObject.inHand)
          ballObject.CompleteBallFlight(false);
      }
    }

    public void DebugThrow(
      Vector3 startPosition,
      Vector3 throwDirection,
      Color color,
      bool hideTrail = true)
    {
      Debug.Log((object) nameof (DebugThrow));
      this.gameObject.SetActive(true);
      this._collider.enabled = false;
      this.userThrown = false;
      this._graphics.TrailColor = color;
      this.Throw(startPosition, throwDirection, true, hideTrail: hideTrail);
    }

    public void SetThrowReplayData(ThrowReplayData data)
    {
      this.replayData.throwData = data;
      this.replayData.PrintData();
    }

    public void SetGameReplayData(GameReplayData data)
    {
      this.replayData.gameData = data;
      this.replayData.PrintData();
    }

    public void SetReplayDataRawThrowVector(Vector3 vec) => this.replayData.rawThrowVector = vec;
  }
}
