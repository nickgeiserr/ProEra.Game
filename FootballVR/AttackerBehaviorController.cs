// Decompiled with JetBrains decompiler
// Type: FootballVR.AttackerBehaviorController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12;
using UnityEngine;

namespace FootballVR
{
  public class AttackerBehaviorController : BehaviourController
  {
    [SerializeField]
    private Avatar _avatar;
    [SerializeField]
    private BoxCollider _collider;
    private PlayerCamera _player;
    private bool _locked;
    private Vector3 _lockedDirection;
    private float _lockedOrientation;
    private float _stopTime;
    private float _currentSpeed;
    private bool _prepState;
    public bool attackEnabled;
    private readonly RoutineHandle _attackRoutine = new RoutineHandle();
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private Vector3 _position;
    private Vector3 _targetPosition;
    private float _currentRotation;

    private CollisionSettings _collisionSettings => ScriptableSingleton<CollisionSettings>.Instance;

    public float startTime { get; set; }

    public bool collisionEnabled
    {
      get => this._collider.enabled;
      set => this._collider.enabled = value;
    }

    public float lockThreshold { get; set; }

    public float attackerSpeed { get; set; } = 5f;

    public event System.Action OnMiss;

    private void Awake()
    {
      this._player = PersistentSingleton<PlayerCamera>.Instance;
      this._locked = false;
      CollidersSettings collidersSettings = this._collisionSettings.collidersSettings;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        collidersSettings.EnemyColliderSizeX.Link<float>((Action<float>) (size => this._collider.size = this._collider.size.SetX(size))),
        collidersSettings.EnemyColliderSizeZ.Link<float>((Action<float>) (size => this._collider.size = this._collider.size.SetZ(size))),
        collidersSettings.EnemyColliderOffsetZ.Link<float>((Action<float>) (offset => this._collider.center = this._collider.center.SetZ(offset)))
      });
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      this._linksHandler.Clear();
    }

    private void OnEnable()
    {
      this._locked = false;
      this._currentSpeed = 0.0f;
      this.collisionEnabled = true;
      this._prepState = false;
      this._avatar.Outline = EOutlineMode.kDisabld;
      this.attackEnabled = false;
      this._position = this.transform.position.SetY(0.0f);
    }

    private void OnDisable() => this._attackRoutine.Stop();

    public override Vector3 Evaluate3DPositionMeters(float evalTime)
    {
      if (!this.attackEnabled)
        return base.Evaluate3DPositionMeters(evalTime);
      float num = this.playbackInfo.PlayTime + evalTime;
      if ((double) num < (double) this.startTime)
        return base.Evaluate3DPositionMeters(evalTime);
      if (!this.playbackInfo.Playing || this._locked && (double) num > (double) this._stopTime)
        return this._position;
      if (this._locked)
      {
        if ((double) num > (double) this._stopTime)
          evalTime -= num - this._stopTime;
        return this._position + this._lockedDirection * (evalTime * this._currentSpeed);
      }
      if ((double) this.playbackInfo.PlayTime < (double) this.startTime)
        evalTime -= this.startTime - this.playbackInfo.PlayTime;
      return this._position + (this._targetPosition - this._position).SetY(0.0f).normalized * (float) ((double) evalTime * ((double) this.ComputeSpeed(evalTime) + (double) this._currentSpeed) / 2.0);
    }

    public override float EvaluateOrientation(float evalTime)
    {
      if (!this.attackEnabled || (double) this.playbackInfo.PlayTime + (double) evalTime < (double) this.startTime)
        return base.EvaluateOrientation(evalTime);
      if (!this.playbackInfo.Playing || this._locked && (double) this.playbackInfo.PlayTime > (double) this._stopTime)
        return this._currentRotation;
      return this._locked ? this._lockedOrientation : Quaternion.LookRotation((this._targetPosition - this._position).SetY(0.0f), Vector3.up).eulerAngles.y;
    }

    protected override void Update()
    {
      this.UpdatePauseState();
      this._position = this.transform.position.SetY(0.0f);
      this._currentRotation = this.transform.eulerAngles.y;
      this._targetPosition = this.GetTargetPosition();
      if ((double) this.playbackInfo.PlayTime < (double) this.startTime || this._locked || !this.attackEnabled)
        return;
      if (this.playbackInfo.Playing)
        this._currentSpeed = this.ComputeSpeed(Time.deltaTime);
      float magnitude = (this._position - this._targetPosition).magnitude;
      AttackerSettings attackerSettings = this._collisionSettings.attackerSettings;
      if (!this._prepState && (double) magnitude < (double) this.lockThreshold + (double) attackerSettings.PrepareDistance)
      {
        this._prepState = true;
        this._avatar.Outline = EOutlineMode.kPreparing;
      }
      if ((double) magnitude > (double) this.lockThreshold)
        return;
      this._avatar.Outline = EOutlineMode.kAttacking;
      this._locked = true;
      this._lockedDirection = (this._targetPosition - this._position).normalized;
      this._lockedOrientation = Quaternion.LookRotation(this._lockedDirection, Vector3.up).eulerAngles.y;
      this._stopTime = this.playbackInfo.PlayTime + attackerSettings.StopTimeAfterLock;
      this._attackRoutine.Run(this.AttackRoutine(this._position, this.lockThreshold + attackerSettings.attackFinishedDistanceOffset));
    }

    private Vector3 GetTargetPosition()
    {
      AttackerSettings attackerSettings = this._collisionSettings.attackerSettings;
      return (this._player.Position + (PlayerPositionTracker.AverageMovement.SetY(0.0f) * (float) attackerSettings.PredictedFrameCount).Clamp(attackerSettings.maxPredictionDelta)).SetY(0.0f);
    }

    private IEnumerator AttackRoutine(Vector3 startPos, float distance)
    {
      AttackerBehaviorController behaviorController = this;
      while ((double) (behaviorController.transform.position.SetY(0.0f) - startPos).magnitude < (double) distance && behaviorController.collisionEnabled)
        yield return (object) null;
      if (behaviorController.collisionEnabled)
      {
        behaviorController.collisionEnabled = false;
        System.Action onMiss = behaviorController.OnMiss;
        if (onMiss != null)
          onMiss();
      }
    }

    private float ComputeSpeed(float timeDelta) => (double) this._currentSpeed > (double) this.attackerSpeed ? this._currentSpeed : Mathf.Clamp(this._currentSpeed + timeDelta * this._collisionSettings.attackerSettings.Acceleration, 0.0f, this.attackerSpeed + 0.01f);

    public override void Reset()
    {
      base.Reset();
      this._locked = false;
      this._prepState = false;
      this._currentSpeed = 0.0f;
      this._position = this.transform.position.SetY(0.0f);
      this.lockThreshold = this._collisionSettings.attackerSettings.attackerLockDistance;
      this.OnMiss = (System.Action) null;
      this._attackRoutine.Stop();
    }
  }
}
