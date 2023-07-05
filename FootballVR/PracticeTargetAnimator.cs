// Decompiled with JetBrains decompiler
// Type: FootballVR.PracticeTargetAnimator
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  public class PracticeTargetAnimator : MonoBehaviour
  {
    [SerializeField]
    private float _timeOffset;
    [SerializeField]
    [HideInInspector]
    private bool _animatePosition;
    [SerializeField]
    [HideInInspector]
    private bool _animateRotation;
    [SerializeField]
    [HideInInspector]
    public bool baked;
    [SerializeField]
    [HideInInspector]
    private AnimationCurve[] _positionCurves;
    [SerializeField]
    [HideInInspector]
    private AnimationCurve[] _rotationCurves;
    private float _initialTime;

    public float initialTime => this._initialTime;

    private void OnEnable() => this._initialTime = Time.time;

    private void Update() => this.UpdateForTime(Time.time);

    private void UpdateForTime(float time)
    {
      if (this._animatePosition)
        this.transform.localPosition = this.EvaluateLocalPosition(time);
      if (!this._animateRotation)
        return;
      this.transform.localRotation = Quaternion.Euler(this.EvaluateLocalRotation(time));
    }

    public void SetupCurves(
      bool animPos,
      bool animRot,
      AnimationCurve[] posCurves,
      AnimationCurve[] rotCurves)
    {
      this._animatePosition = animPos;
      this._animateRotation = animRot;
      this._positionCurves = animPos ? posCurves : (AnimationCurve[]) null;
      this._rotationCurves = animRot ? rotCurves : (AnimationCurve[]) null;
    }

    private Vector3 EvaluateLocalPosition(float time)
    {
      if (!this._animatePosition)
        return this.transform.localPosition;
      time -= this._initialTime;
      time += this._timeOffset;
      return this._positionCurves.Length < 3 ? Vector3.zero : new Vector3(this._positionCurves[0].Evaluate(time), this._positionCurves[1].Evaluate(time), this._positionCurves[2].Evaluate(time));
    }

    public Vector3 EvaluateLocalRotation(float time)
    {
      if (!this._animateRotation)
        return this.transform.localRotation.eulerAngles;
      time -= this._initialTime;
      time += this._timeOffset;
      return this._rotationCurves.Length < 3 ? Vector3.zero : new Vector3(this._rotationCurves[0].Evaluate(time), this._rotationCurves[1].Evaluate(time), this._rotationCurves[2].Evaluate(time));
    }

    public Vector3 EvaluatePosition(float playTime) => this.transform.parent.TransformPoint(this.EvaluateLocalPosition(playTime));
  }
}
