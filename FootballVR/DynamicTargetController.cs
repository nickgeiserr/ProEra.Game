// Decompiled with JetBrains decompiler
// Type: FootballVR.DynamicTargetController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class DynamicTargetController : MonoBehaviour
  {
    [SerializeField]
    private PhotonView _photonView;
    [SerializeField]
    private Transform _shootPoint;
    [SerializeField]
    private DynamicTarget _target;
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private Color _lineColor;
    [SerializeField]
    private float _fadeDuration = 0.6f;
    [SerializeField]
    private Rect _fieldBounds;
    [SerializeField]
    private float _multiplier = 20f;
    private readonly RoutineHandle _fadeRoutine = new RoutineHandle();
    private readonly VariableBool ThumbstickPressed = new VariableBool();
    private readonly VariableBool MovingTarget = new VariableBool();
    private bool _editorOverridePressed;

    private void Awake()
    {
      if (!this._photonView.IsMine)
      {
        Objects.SafeDestroy((UnityEngine.Object) this._lineRenderer.gameObject);
        Objects.SafeDestroy((UnityEngine.Object) this);
      }
      else
      {
        this.ThumbstickPressed.OnValueChanged += new Action<bool>(this.HandleThumbstick);
        this.MovingTarget.OnValueChanged += new Action<bool>(this.HandleOnMovingTarget);
        this._lineRenderer.enabled = false;
      }
    }

    private void OnDestroy()
    {
      this.ThumbstickPressed.OnValueChanged -= new Action<bool>(this.HandleThumbstick);
      this.MovingTarget.OnValueChanged -= new Action<bool>(this.HandleOnMovingTarget);
      this._fadeRoutine.Stop();
    }

    private void Update()
    {
      this.ThumbstickPressed.SetValue(VRInputManager.Get(VRInputManager.Button.Primary2DAxisClick, VRInputManager.Controller.LeftHand));
      if (!(bool) this.MovingTarget)
        return;
      Vector3 position = this._shootPoint.position;
      Vector3 velocity = this._shootPoint.forward * this._multiplier;
      Vector3 landingPoint = AutoAim.GetLandingPoint(position, velocity, 0.0f, out float _);
      Vector2 intersection;
      if (this._fieldBounds.Intersects(this._fieldBounds.center, new Vector2(landingPoint.x, landingPoint.z), out intersection))
      {
        landingPoint.x = intersection.x;
        landingPoint.z = intersection.y;
      }
      this._target.SetPosition(landingPoint);
      Vector3 impulseToHitTarget = AutoAim.GetImpulseToHitTarget(landingPoint - position, 1f);
      float flightTime = AutoAim.GetFlightTime(position, impulseToHitTarget, this._target.height);
      Vector3[] trajectory = AutoAim.GetTrajectory(position, impulseToHitTarget, 0.03f, flightTime);
      this._lineRenderer.positionCount = trajectory.Length;
      this._lineRenderer.SetPositions(trajectory);
    }

    private void HandleOnMovingTarget(bool moving)
    {
      if (moving)
      {
        this._lineRenderer.enabled = true;
        this._fadeRoutine.Stop();
        this.SetColor(this._lineColor);
      }
      else
        this._fadeRoutine.Run(this.FadeRoutine());
    }

    private void HandleThumbstick(bool pressed)
    {
      if (!ScriptableSingleton<VRSettings>.Instance.AllowDynamicTargets)
        return;
      if (!pressed)
        this.MovingTarget.SetValue(false);
      else if (this._target.Active)
      {
        this._target.SetState(false);
      }
      else
      {
        this._target.SetState(true);
        this.MovingTarget.SetValue(true);
      }
    }

    private IEnumerator FadeRoutine()
    {
      float startTime = Time.time;
      float endTime = Time.time + this._fadeDuration;
      Color color = this._lineColor;
      while ((double) Time.time < (double) endTime)
      {
        color.a = 1f - Mathf.InverseLerp(startTime, endTime, Time.time);
        this.SetColor(color);
        yield return (object) null;
      }
      this._lineRenderer.enabled = false;
    }

    private void SetColor(Color color)
    {
      this._lineRenderer.startColor = color;
      this._lineRenderer.endColor = color;
    }
  }
}
