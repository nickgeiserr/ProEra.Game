// Decompiled with JetBrains decompiler
// Type: ThrowRing
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using System.Collections;
using TB12.AppStates.MiniCamp;
using UnityEngine;

public class ThrowRing : MonoBehaviour
{
  [HideInInspector]
  public RouteRunningTarget routeRunningTarget;
  [SerializeField]
  private Animation _anim;
  [SerializeField]
  private Animator _animator;
  [SerializeField]
  private GameObject RingMesh;
  [SerializeField]
  private Color GoldColor;
  [SerializeField]
  private Color SilverColor;
  [SerializeField]
  private Color BronzeColor;
  private PrecisionPassingGameFlow.ThrowAward _award;
  private Rigidbody _rigidBody;
  [SerializeField]
  private bool _isTargeted;
  private float _desiredRotationRollRate;
  private float _rotationRollRate;
  [SerializeField]
  private float _rotationRollRateLerp = 0.3f;

  public PrecisionPassingGameFlow.ThrowAward Award => this._award;

  private void Start() => this._rigidBody = this.GetComponent<Rigidbody>();

  private void Update()
  {
    if (this._isTargeted)
    {
      if ((double) Math.Abs(this._rotationRollRate - this._desiredRotationRollRate) > 0.0099999997764825821)
        this._rotationRollRate = Mathf.Lerp(this._rotationRollRate, this._desiredRotationRollRate, this._rotationRollRateLerp);
      this.RingMesh.transform.eulerAngles = new Vector3(this.RingMesh.transform.eulerAngles.x, this.RingMesh.transform.eulerAngles.y, this.RingMesh.transform.eulerAngles.z + this._rotationRollRate * Time.deltaTime);
    }
    else
      this.RingMesh.transform.eulerAngles = new Vector3(this.RingMesh.transform.eulerAngles.x, this.RingMesh.transform.eulerAngles.y, Mathf.Lerp(this.RingMesh.transform.eulerAngles.z, 0.0f, 0.1f));
  }

  public Color GetColor(PrecisionPassingGameFlow.ThrowAward medalColor)
  {
    if (medalColor == PrecisionPassingGameFlow.ThrowAward.Gold)
      return this.GoldColor;
    return medalColor == PrecisionPassingGameFlow.ThrowAward.Silver ? this.SilverColor : this.BronzeColor;
  }

  public void SetMedalColor(int medalIndex)
  {
    this._award = (PrecisionPassingGameFlow.ThrowAward) medalIndex;
    this.RingMesh.GetComponent<Renderer>().material.SetColor("_Tint", this.GetColor(this._award));
  }

  public void SetScale(float scale) => this.transform.localScale = Vector3.one * scale;

  public void SetRouteRunningTarget(RouteRunningTarget target)
  {
    if (!((UnityEngine.Object) this.routeRunningTarget == (UnityEngine.Object) null))
      return;
    this.routeRunningTarget = target;
    this.routeRunningTarget.CompletePassAndHitRing += new System.Action(this.OnCompletePassAndHitRing);
  }

  public void ResetState()
  {
    this._animator.Rebind();
    this._animator.Update(0.0f);
    this.routeRunningTarget.ResetHitRing();
    this._isTargeted = false;
    this._rigidBody.detectCollisions = true;
    this._desiredRotationRollRate = 0.0f;
  }

  private void OnDisable()
  {
    if (!((UnityEngine.Object) this.routeRunningTarget != (UnityEngine.Object) null))
      return;
    this.routeRunningTarget.CompletePassAndHitRing -= new System.Action(this.OnCompletePassAndHitRing);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!other.TryGetComponent<BallObject>(out BallObject _))
      return;
    this.routeRunningTarget.SetHitRing();
  }

  private void OnCompletePassAndHitRing()
  {
    this._animator.SetTrigger("Hit");
    PrecisionPassingGameFlow objectOfType = UnityEngine.Object.FindObjectOfType<PrecisionPassingGameFlow>();
    if ((UnityEngine.Object) objectOfType != (UnityEngine.Object) null)
      objectOfType.UpdateThrowAwards(this._award);
    this.StartCoroutine(this.DisableAfterAnimation());
  }

  private IEnumerator DisableAfterAnimation()
  {
    yield return (object) new WaitForSeconds(2f);
    this._rigidBody.detectCollisions = false;
  }

  public void SetTargeted(bool isTargeted)
  {
    this._isTargeted = isTargeted;
    this._desiredRotationRollRate = 360f;
  }
}
