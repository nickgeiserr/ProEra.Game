// Decompiled with JetBrains decompiler
// Type: FootballVR.BehaviourController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MxM;
using System;
using TB12;
using UnityEngine;
using UnityEngine.Animations;

namespace FootballVR
{
  public class BehaviourController : 
    MonoBehaviour,
    ILocomotionPlanProvider,
    IReferenceProvider,
    IEventDataProvider,
    IStanceProvider,
    ILegacyMovementTypeProvider,
    ITimeToHikeProvider,
    ITimeToCatchProvider,
    ITimeToThrowProvider,
    IIsBallCarrierProvider,
    IIsQuaterbackProvider,
    IOnGrabBallCallbackInvoker,
    IPlayStateEvaluator
  {
    public IPlaybackInfo playbackInfo;
    [SerializeField]
    private PlayerIdentity _identity;
    [SerializeField]
    private MxMAnimator _mxmAnimator;
    [SerializeField]
    private GazeController _gazeController;
    [SerializeField]
    private PlayerScenario _baseScenario;
    [SerializeField]
    private bool _isQuaterback;
    [ReadOnly]
    [SerializeField]
    private PlayerScenario _scenarioDuplicate;
    private AvatarBehaviorState _state;
    private bool _customState;
    private bool _wasInit;
    protected float _cachedPlayTime;
    private Action<Transform> _onGrabBall;
    private static readonly EventData _emptyEvent = new EventData();

    public PlayerScenario Scenario
    {
      get
      {
        if (this._wasInit)
          return this._scenarioDuplicate;
        if ((UnityEngine.Object) this._baseScenario == (UnityEngine.Object) null)
          return (PlayerScenario) null;
        this._scenarioDuplicate = UnityEngine.Object.Instantiate<PlayerScenario>(this._baseScenario);
        this._wasInit = true;
        return this._scenarioDuplicate;
      }
      set
      {
        if ((UnityEngine.Object) this._scenarioDuplicate != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) this._scenarioDuplicate);
        this._baseScenario = value;
        this._scenarioDuplicate = UnityEngine.Object.Instantiate<PlayerScenario>(this._baseScenario);
        this._scenarioDuplicate.Initialize();
        this._wasInit = true;
      }
    }

    private float PlayTime => this.playbackInfo.RoundedTime;

    public virtual void Reset()
    {
      this._state = (AvatarBehaviorState) null;
      this._customState = false;
      if ((UnityEngine.Object) this.Scenario == (UnityEngine.Object) null || (UnityEngine.Object) this._mxmAnimator == (UnityEngine.Object) null)
        return;
      if (this._mxmAnimator.IsPaused)
        this._mxmAnimator.UnPause();
      if (this._mxmAnimator.MxMPlayableGraph.IsValid() && (!this._mxmAnimator.IsEventComplete || this._mxmAnimator.IsEventPlaying))
      {
        this._mxmAnimator.ForceExitEvent();
        this._mxmAnimator.ResetEventData();
      }
      Vector2 vector2 = this.Scenario.position.EvaluatePosition(this.playbackInfo.PlayTime) + this.Scenario.startPosition;
      this._mxmAnimator.transform.SetPositionAndRotation(new Vector3(vector2.x, 0.0f, vector2.y), Quaternion.Euler(0.0f, this.Scenario.orientation.Evaluate(this.playbackInfo.PlayTime), 0.0f));
      if (!this._mxmAnimator.MxMPlayableGraph.IsValid())
        return;
      this._mxmAnimator.ResetMotion();
      if (!this._mxmAnimator.MxMPlayableGraph.IsPlaying())
        return;
      this._mxmAnimator.MxMPlayableGraph.Disconnect<AnimationMixerPlayable>(this._mxmAnimator.MixerPlayable, 0);
      this._mxmAnimator.ResetPose();
    }

    private void OnEnable()
    {
      this._identity = this.GetComponent<PlayerIdentity>();
      this._identity.OnIdentitySet += new System.Action(this.OnIdentitySetHandler);
    }

    private void OnDisable() => this._identity.OnIdentitySet -= new System.Action(this.OnIdentitySetHandler);

    protected virtual void Update()
    {
      this.UpdatePauseState();
      if ((double) Mathf.Abs(this.playbackInfo.PlayTime - this._cachedPlayTime) > 1.0)
        this.Reset();
      this._cachedPlayTime = this.playbackInfo.PlayTime;
      if (!this._customState)
        return;
      this._state.playerPosition = this.transform.position;
    }

    protected void UpdatePauseState()
    {
      if (this._mxmAnimator.IsEventPlaying && !this.playbackInfo.Playing)
        this._mxmAnimator.Pause();
      else
        this._mxmAnimator.UnPause();
    }

    protected virtual void OnDestroy()
    {
      if (!((UnityEngine.Object) this._scenarioDuplicate != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this._scenarioDuplicate);
    }

    private void OnIdentitySetHandler() => this.SetQuarterback(this._identity.positionGroup.Equals(ConstantHashedStrings.Quaterback));

    public void SetQuarterback(bool isQuaterback) => this._isQuaterback = isQuaterback;

    public void GoTo(float timeDelay, Vector2 relativePosition, float speed)
    {
      float time = this.PlayTime + timeDelay;
      Vector2 position = this.Scenario.position.EvaluatePosition(time);
      float num = Vector2.Distance(position, relativePosition) / speed;
      for (int index = 0; index < this.Scenario.position.xAxis.keys.Length; ++index)
      {
        if ((double) this.Scenario.position.xAxis.keys[index].time > (double) time)
        {
          this.Scenario.position.xAxis.RemoveKey(index);
          this.Scenario.position.zAxis.RemoveKey(index);
          this.Scenario.orientation.RemoveKey(index);
          --index;
        }
      }
      this.Scenario.position.AddPosition(time, position);
      this.Scenario.position.AddPosition(time + num, relativePosition);
      float orientationY = AnimUtils.CalculateOrientationY((Vector3) (relativePosition - position));
      this.Scenario.orientation.AddKey(time, orientationY);
      this.Scenario.PostProcessCurves();
    }

    public void CatchBall(EventData eventData)
    {
      if (!Mathf.Approximately(this.Scenario.possession.Evaluate(eventData.time), 0.0f))
        return;
      if (ScriptableSingleton<AvatarsSettings>.Instance.CustomStateBehavior)
      {
        this._customState = true;
        CatchBehavior catchBehavior = new CatchBehavior(this.playbackInfo, eventData.position, eventData.time);
        catchBehavior.playerPosition = this.transform.position;
        this._state = (AvatarBehaviorState) catchBehavior;
      }
      this.Scenario.possession.AddKey(new Keyframe(eventData.time, 1f));
      this.Scenario.possession = KeyframeUtils.CalculateConstantKeyframeTangents(this.Scenario.possession.keys);
      this.Scenario.possessionAnimationKeys = this.Scenario.possession.SerializableKeys();
      this.Scenario.eventList.Add(eventData);
    }

    public void ThrowBall(EventData eventData)
    {
      if (!Mathf.Approximately(this.Scenario.possession.Evaluate(eventData.time), 1f))
        return;
      this.Scenario.possession.AddKey(new Keyframe(eventData.time, 0.0f));
      this.Scenario.possession = KeyframeUtils.CalculateConstantKeyframeTangents(this.Scenario.possession.keys);
      this.Scenario.possessionAnimationKeys = this.Scenario.possession.SerializableKeys();
      this.Scenario.eventList.Add(eventData);
    }

    public void HikeBall(EventData eventData)
    {
      this.Scenario.possession.AddKey(new Keyframe(eventData.time, -1f));
      this.Scenario.possession = KeyframeUtils.CalculateConstantKeyframeTangents(this.Scenario.possession.keys);
      this.Scenario.possessionAnimationKeys = this.Scenario.possession.SerializableKeys();
      this.Scenario.eventList.Add(eventData);
    }

    public void GrabBall(Transform ball)
    {
      this.Scenario.possession.AddKey(new Keyframe(this.playbackInfo.PlayTime, 1f));
      this.Scenario.possession = KeyframeUtils.CalculateConstantKeyframeTangents(this.Scenario.possession.keys);
      this.Scenario.possessionAnimationKeys = this.Scenario.possession.SerializableKeys();
      Action<Transform> onGrabBall = this.OnGrabBall;
      if (onGrabBall == null)
        return;
      onGrabBall(ball);
    }

    public void AssumeOrientation(float absoluteAngle) => this.Scenario.orientation.AddKey(new Keyframe(this.PlayTime, absoluteAngle));

    public void SetGazeTarget(Transform target) => this._gazeController.SetTarget(target);

    public void InitializeScenario(PlayerScenario scenario)
    {
      if ((UnityEngine.Object) this._scenarioDuplicate != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this._scenarioDuplicate);
      this._scenarioDuplicate = scenario;
      this._scenarioDuplicate.Initialize();
      this._wasInit = true;
    }

    public Vector3 GetPosition() => this.Evaluate3DPositionMeters(0.0f);

    public Quaternion GetRotation() => Quaternion.Euler(this.GetRotationEuler());

    public Vector3 GetRotationEuler() => new Vector3(0.0f, this.GetRotationY(), 0.0f);

    public float GetRotationY() => this.EvaluateOrientation(0.0f);

    public virtual Vector3 Evaluate3DPositionMeters(float evalTime) => !this._customState ? MathUtils.TransformDataCoordinatesToScenePosition(this.Scenario.position.EvaluatePosition(this.PlayTime + (this.playbackInfo.Playing ? evalTime : 0.0f)) + this.Scenario.startPosition) : this._state.Evaluate3DPositionMeters(evalTime);

    public virtual float EvaluateOrientation(float evalTime) => !this._customState ? this.Scenario.orientation.Evaluate(this.PlayTime + (this.playbackInfo.Playing ? evalTime : 0.0f)) : this._state.EvaluateOrientation(evalTime);

    public Action<Transform> OnGrabBall
    {
      get => this._onGrabBall;
      set => this._onGrabBall = value;
    }

    public bool IsQuaterback => this._isQuaterback;

    public bool IsInStance => this._mxmAnimator.IsIdle && this.EvaluateStance(this.playbackInfo.PlayTime) > 1;

    public virtual int EvaluateStance(float evalTime) => (int) this.Scenario.stance.Evaluate(this.PlayTime + evalTime);

    public virtual int EvaluateMovementType(float evalTime) => (int) this.Scenario.legacyMovementType.Evaluate(this.PlayTime + evalTime);

    public float TimeToCatch => this.EvaluateTimeUntilCatch(this.PlayTime);

    public float TimeToThrow => this.EvaluateTimeUntilThrow(this.PlayTime);

    public float TimeToHike => this.EvaluateTimeUntilHike(this.PlayTime);

    public EventData EvaluateUpcomingEvent(float time)
    {
      EventData upcomingEvent = BehaviourController._emptyEvent;
      for (int index = 0; index < this.Scenario.eventList.Count; ++index)
      {
        EventData eventData = this.Scenario.eventList[index];
        if ((double) eventData.time >= (double) time)
          upcomingEvent = (double) upcomingEvent.time > (double) eventData.time ? eventData : upcomingEvent;
      }
      return upcomingEvent;
    }

    public EventData EvaluateUpcomingEvent() => this.EvaluateUpcomingEvent(this.playbackInfo.PlayTime);

    public bool IsBallCarrier => Mathf.FloorToInt(this.Scenario.possession.Evaluate(this.playbackInfo.PlayTime)) == 1;

    private float EvaluateTimeUntilCatch(float time)
    {
      for (int index = 0; index < this.Scenario.possessionAnimationKeys.Count; ++index)
      {
        if ((double) this.Scenario.possessionAnimationKeys[index].time > (double) time && (double) this.Scenario.possessionAnimationKeys[index].time > 0.0 && Mathf.Approximately(this.Scenario.possessionAnimationKeys[index].value, 1f))
          return this.Scenario.possessionAnimationKeys[index].time - time;
      }
      return float.PositiveInfinity;
    }

    private float EvaluateTimeUntilThrow(float time)
    {
      for (int index = 0; index < this.Scenario.possessionAnimationKeys.Count; ++index)
      {
        if ((double) this.Scenario.possessionAnimationKeys[index].time > (double) time && (double) this.Scenario.possessionAnimationKeys[index].time > 0.0 && Mathf.Approximately(this.Scenario.possessionAnimationKeys[index].value, 0.0f))
          return this.Scenario.possessionAnimationKeys[index].time - time;
      }
      return float.PositiveInfinity;
    }

    private float EvaluateTimeUntilHike(float time)
    {
      for (int index = 0; index < this.Scenario.possessionAnimationKeys.Count; ++index)
      {
        if ((double) this.Scenario.possessionAnimationKeys[index].time > (double) time && Mathf.Approximately(this.Scenario.possessionAnimationKeys[index].value, -1f))
          return this.Scenario.possessionAnimationKeys[index].time - time;
      }
      return float.PositiveInfinity;
    }

    public bool EvaluatePassEligibility(float time) => (double) this.Scenario.eligibility.Evaluate(time) > 0.0;

    public float GetPlayTime() => this.playbackInfo.PlayTime;
  }
}
