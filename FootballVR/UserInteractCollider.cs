// Decompiled with JetBrains decompiler
// Type: FootballVR.UserInteractCollider
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public class UserInteractCollider : MonoBehaviour
  {
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private MeshRenderer[] _renderers;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private bool _enabled = true;
    private bool _state;
    private Vector3 _pos;
    private Quaternion _rot;
    private HandPhysicsSettings settings;

    public event Action<BallObject> OnBallCollision;

    private ThrowSettings _throwSettings => ScriptableSingleton<ThrowSettings>.Instance;

    private void Awake()
    {
      this._rigidbody.isKinematic = true;
      this._rigidbody.useGravity = false;
      this.transform.SetParent(this.transform.root);
      this.gameObject.SetActive(false);
      this._state = false;
      this.settings = this._throwSettings.HandPhysicsSettings;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this.settings.handPhysics.Link<bool>(new Action<bool>(this.HandlePhysicsState)),
        this.settings.continuousDetection.Link<bool>(new Action<bool>(this.HandleContinousDetection)),
        this.settings.renderColliders.Link<bool>(new Action<bool>(this.HandleDebugThrowColliders)),
        this.settings.InterpolationMethod.Link<RigidbodyInterpolation>((Action<RigidbodyInterpolation>) (x => this._rigidbody.interpolation = x))
      });
    }

    private void OnDestroy()
    {
      this._linksHandler.Clear();
      this._routineHandle.Stop();
    }

    private void OnEnable() => this.SyncPos();

    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.layer == (int) WorldConstants.Layers.Interactables)
        return;
      BallObject component = other.gameObject.GetComponent<BallObject>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      Action<BallObject> onBallCollision = this.OnBallCollision;
      if (onBallCollision == null)
        return;
      onBallCollision(component);
    }

    private void HandleDebugThrowColliders(bool state)
    {
      if (this._renderers == null)
        return;
      foreach (Renderer renderer in this._renderers)
        renderer.enabled = state;
    }

    private void HandlePhysicsState(bool state) => this.ApplyState(state && this._enabled);

    private void HandleContinousDetection(bool continuosEnabled) => this._rigidbody.collisionDetectionMode = continuosEnabled ? CollisionDetectionMode.ContinuousSpeculative : CollisionDetectionMode.Discrete;

    private void FixedUpdate() => this.UpdatePosition();

    public void SetState(bool state)
    {
      this._enabled = state;
      this.ApplyState(this._enabled && (bool) this.settings.handPhysics);
    }

    private void ApplyState(bool state)
    {
      if (this._state == state)
        return;
      this._routineHandle.Stop();
      this._state = state;
      int collisionReenabled = this.settings.framesDelayCollisionReenabled;
      if (state && collisionReenabled > 0)
      {
        this._routineHandle.Run(this.DelayedRoutine(collisionReenabled));
      }
      else
      {
        if (state)
          this.SyncPos();
        this.gameObject.SetActive(state);
      }
    }

    private IEnumerator DelayedRoutine(int delay)
    {
      UserInteractCollider interactCollider = this;
      for (int i = 0; i < delay; ++i)
        yield return (object) null;
      interactCollider.SyncPos();
      interactCollider.gameObject.SetActive(true);
    }

    private void SyncPos()
    {
      if (this._throwSettings.DebugMode)
        Debug.Log((object) string.Format("Rigidbody:{0} tx:{1} target:{2}", (object) this._rigidbody.position, (object) this.transform.position, (object) this._target.transform.position));
      this._pos = this._target.position;
      this._rot = this._target.rotation;
      this._rigidbody.position = this._pos;
      this._rigidbody.rotation = this._rot;
      this.transform.position = this._pos;
      this.transform.rotation = this._rot;
    }

    private void GetTargetValues(out Vector3 pos, out Quaternion rot)
    {
      switch (this.settings.TargetInterpolation)
      {
        case ETargetInterpolation.RigidbodyPos:
          pos = Vector3.Lerp(this._rigidbody.position, this._target.position, this.settings.lerpFactor);
          rot = Quaternion.Slerp(this._rigidbody.rotation, this._target.rotation, this.settings.lerpFactor);
          break;
        case ETargetInterpolation.CachedPos:
          pos = this._pos = Vector3.Lerp(this._pos, this._target.position, this.settings.lerpFactor);
          rot = this._rot = Quaternion.Slerp(this._rot, this._target.rotation, this.settings.lerpFactor);
          break;
        default:
          pos = this._target.position;
          rot = this._target.rotation;
          break;
      }
    }

    private void UpdatePosition()
    {
      Vector3 pos;
      Quaternion rot;
      this.GetTargetValues(out pos, out rot);
      switch (this.settings.MovementMethod)
      {
        case EMoveType.Transform:
          this.transform.position = pos;
          this.transform.rotation = rot;
          break;
        case EMoveType.RigidbodyPos:
          this._rigidbody.position = pos;
          this._rigidbody.rotation = rot;
          break;
        case EMoveType.RigidbodyVelo:
          this._rigidbody.velocity = (pos - this._rigidbody.position) / Time.fixedDeltaTime;
          float angle;
          Vector3 axis;
          (rot * Quaternion.Inverse(this._rigidbody.rotation)).ToAngleAxis(out angle, out axis);
          Vector3 vector3 = angle * ((float) Math.PI / 180f) * axis;
          this._rigidbody.angularVelocity = rot * (vector3 / Time.deltaTime);
          break;
        case EMoveType.RigidbodyForce:
          break;
        default:
          this._rigidbody.MovePosition(pos);
          this._rigidbody.MoveRotation(rot);
          break;
      }
    }
  }
}
