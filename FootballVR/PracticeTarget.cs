// Decompiled with JetBrains decompiler
// Type: FootballVR.PracticeTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using Framework;
using Photon.Pun;
using System;
using UnityEngine;

namespace FootballVR
{
  public class PracticeTarget : MonoBehaviour, IThrowTarget
  {
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private PracticeTargetAnimator _animator;
    [SerializeField]
    private float _score;
    [SerializeField]
    private TargetHitEffect _hitEffect;
    [SerializeField]
    private int _hitSoundId = 1;
    [SerializeField]
    private float _maxHitDistance = 0.45f;
    [SerializeField]
    private bool _validForAI = true;
    [SerializeField]
    private WorldPopupText _scoreText;
    [SerializeField]
    private WorldPopupText _comboText;
    [SerializeField]
    private bool _activeForPoints = true;
    [SerializeField]
    private GameObject _activeForPointsGO;
    [SerializeField]
    private bool _alwaysOrientUITowardsCamera;
    [SerializeField]
    private Canvas _UICanvas;
    private bool _targetValid = true;
    [SerializeField]
    private bool _isGroundTarget;
    private bool _staticTarget;
    private Quaternion _parentRot;
    private Vector3 _relativePos;
    private TimeSlot _timeslot;
    private bool _hasTimeslot;
    private bool _isPriority;
    private static bool _debug;

    public event Action<bool> OnValidityChanged;

    public event System.Action OnHit;

    public static event Action<int, bool, bool, PracticeTarget> OnTargetHit;

    public WorldPopupText ScoreText => this._scoreText;

    public WorldPopupText ComboText => this._comboText;

    public float Score
    {
      get => this._score;
      set => this._score = value;
    }

    public float HitScore => this.GetHitScore();

    protected virtual float GetHitScore() => this._score;

    protected virtual float GetComboScore() => this._score * 2f;

    public bool IsTargetValid(float timeOffset = 0.0f)
    {
      if (!this._hasTimeslot || this._staticTarget)
        return this._targetValid;
      float num = Time.time - this._animator.initialTime + timeOffset;
      return (double) num < (double) this._timeslot.endTime && (double) num > (double) this._timeslot.startTime;
    }

    public bool TargetValidForAI => this._validForAI;

    public bool IsActiveForPoints => this._activeForPoints;

    public bool ReceiveBall(EventData eventData) => false;

    public float minCatchTime => 0.1f;

    public string TargetName => this.transform.parent.parent.gameObject.name + "/" + this.transform.parent.gameObject.name + "/" + this.gameObject.name;

    public float hitRange => this._maxHitDistance;

    private void Awake()
    {
      this._staticTarget = (UnityEngine.Object) this._animator == (UnityEngine.Object) null;
      this.SetUIVisibility(false);
      if (this._staticTarget)
        return;
      this._parentRot = this._animator.transform.parent.rotation;
      this._relativePos = this._animator.transform.InverseTransformPoint(this.transform.position);
    }

    protected virtual void Update()
    {
      if (!this._alwaysOrientUITowardsCamera || !((UnityEngine.Object) this._UICanvas != (UnityEngine.Object) null))
        return;
      this._UICanvas.transform.forward = PersistentSingleton<GamePlayerController>.Instance.Rig.centerEyeAnchor.forward;
    }

    private void OnEnable()
    {
      this._throwManager.RegisterTarget((IThrowTarget) this);
      this.SetUIVisibility(false);
    }

    public void SetUIVisibility(bool visible)
    {
      if ((UnityEngine.Object) this._comboText != (UnityEngine.Object) null)
        this._comboText.gameObject.SetActive(visible);
      if (!((UnityEngine.Object) this._scoreText != (UnityEngine.Object) null))
        return;
      this._scoreText.gameObject.SetActive(visible);
    }

    private void OnDisable() => this._throwManager.UnregisterTarget((IThrowTarget) this);

    private void OnCollisionEnter(Collision collision)
    {
      Debug.Log((object) "PracticeTarget OnCollisionEnter");
      GameObject gameObject = collision.gameObject;
      BallObject component = gameObject.GetComponent<BallObject>();
      bool flag1 = component is BallObjectNetworked && component.GetComponent<PhotonView>().IsMine;
      if (((!((UnityEngine.Object) component != (UnityEngine.Object) null) ? 0 : (!component.inHand ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
        VREvents.TargetHit.Trigger(true, this.Score);
      if (!this._staticTarget)
      {
        float num = Time.time - this._animator.initialTime;
        if (this._hasTimeslot && (double) num < (double) this._timeslot.startTime)
          return;
      }
      if (!this._isGroundTarget)
      {
        if (component.hasHitTarget)
          return;
        System.Action onHit = this.OnHit;
        if (onHit != null)
          onHit();
        if ((UnityEngine.Object) this._hitEffect != (UnityEngine.Object) null)
          this._hitEffect.PlayEffect(component.Graphics.TrailColor);
        bool flag2 = (double) this.GetMinDistance(collision) > (double) this._maxHitDistance;
        Action<int, bool, bool, PracticeTarget> onTargetHit = PracticeTarget.OnTargetHit;
        if (onTargetHit != null)
          onTargetHit(this._hitSoundId, component.userThrown, flag2, this);
        component.CompleteBallFlight();
        if (!flag1)
          return;
        PhotonNetwork.Destroy(gameObject);
      }
      else
      {
        System.Action onHit = this.OnHit;
        if (onHit != null)
          onHit();
        Action<int, bool, bool, PracticeTarget> onTargetHit = PracticeTarget.OnTargetHit;
        if (onTargetHit == null)
          return;
        onTargetHit(this._hitSoundId, true, false, this);
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      if (!this._isGroundTarget)
        return;
      System.Action onHit = this.OnHit;
      if (onHit != null)
        onHit();
      Action<int, bool, bool, PracticeTarget> onTargetHit = PracticeTarget.OnTargetHit;
      if (onTargetHit != null)
        onTargetHit(this._hitSoundId, true, false, this);
      this.gameObject.SetActive(false);
    }

    private float GetMinDistance(Collision collision)
    {
      float b = float.PositiveInfinity;
      Vector3 position = this.transform.position;
      foreach (ContactPoint contact in collision.contacts)
        b = Mathf.Min(this.transform.TransformVector(this.transform.InverseTransformVector(contact.point - position).SetZ(0.0f)).magnitude, b);
      return b;
    }

    public Vector3 EvaluatePosition(float playTime) => this._staticTarget ? this.transform.position : this._animator.EvaluatePosition(Time.time + playTime) + this._parentRot * Quaternion.Euler(this._animator.EvaluateLocalRotation(Time.time + playTime)) * this._relativePos;

    public Vector3 GetHitPosition(float time, Vector3 ballPos) => this.EvaluatePosition(time);

    public void SetActiveForPoints(bool active)
    {
      if (this._activeForPoints == active)
        return;
      this._activeForPoints = active;
      if (!(bool) (UnityEngine.Object) this._activeForPointsGO)
        return;
      this._activeForPointsGO.SetActive(active);
    }

    public void SetTargetValid(bool state)
    {
      if (this._targetValid == state)
        return;
      this._targetValid = state;
      Action<bool> onValidityChanged = this.OnValidityChanged;
      if (onValidityChanged == null)
        return;
      onValidityChanged(state);
    }

    public void SetTimeslot(TimeSlot timeSlot)
    {
      this._hasTimeslot = true;
      this._timeslot = timeSlot;
    }

    public void SetPriority(bool priority) => this._isPriority = priority;

    public bool IsPriorityTarget() => this._isPriority;

    public void GetReplayData(out ThrowReplayData data) => data = new ThrowReplayData();

    public Vector3 GetIdealThrowTarget() => this.transform.position;

    public void DrawRange()
    {
    }

    public bool IsPlayer() => false;
  }
}
