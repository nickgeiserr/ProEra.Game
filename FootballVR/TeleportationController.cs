// Decompiled with JetBrains decompiler
// Type: FootballVR.TeleportationController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using SplineMesh;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class TeleportationController : MonoBehaviour
  {
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private Color _lineColor;
    [SerializeField]
    private float _fadeDuration = 0.6f;
    [SerializeField]
    private Rect _fieldBounds;
    [SerializeField]
    private Spline _spline;
    private const float _targetHeight = 0.02f;
    private readonly RoutineHandle _fadeRoutine = new RoutineHandle();
    private readonly VariableBool Teleporting = new VariableBool();
    private readonly VariableBool LeftButtonPressed = new VariableBool();
    private readonly VariableBool RightButtonPressed = new VariableBool();
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private Transform _shootPoint;
    private Transform _shootPointRight;
    private Transform _shootPointLeft;
    private bool _initialized;
    private EHand _activeHand = EHand.Right;
    private TeleportSettings settings;
    private bool _teleporting;

    private void Awake()
    {
      this._lineRenderer.enabled = false;
      this._target.gameObject.SetActive(false);
      this.enabled = this._initialized;
      this.settings = ScriptableSingleton<VRSettings>.Instance.teleportSettings;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this.Teleporting.Link<bool>(new Action<bool>(this.HandleTeleportButton), false),
        this.LeftButtonPressed.Link<bool>((Action<bool>) (x => this.HandleButtonStateChanged(x, EHand.Left))),
        this.RightButtonPressed.Link<bool>((Action<bool>) (x => this.HandleButtonStateChanged(x, EHand.Right)))
      });
    }

    private void HandleButtonStateChanged(bool pressed, EHand hand)
    {
      if (!(bool) this.Teleporting & pressed)
      {
        this._activeHand = hand;
        this._shootPoint = hand == EHand.Right ? this._shootPointRight : this._shootPointLeft;
        this.Teleporting.SetValue(true);
      }
      else
      {
        if (!(bool) this.Teleporting || pressed || this._activeHand != hand)
          return;
        this.Teleporting.SetValue(false);
      }
    }

    private void OnDestroy()
    {
      this._linksHandler.Clear();
      this._fadeRoutine.Stop();
    }

    public void Initialize(Transform shootPointRight, Transform shootPointLeft)
    {
      if (this._initialized)
        return;
      this._shootPointRight = shootPointRight;
      this._shootPointLeft = shootPointLeft;
      this._shootPoint = shootPointRight;
      this._initialized = true;
      this.enabled = true;
    }

    private void Update()
    {
      this.RightButtonPressed.SetValue(VRInputManager.Get(VRInputManager.Button.PrimaryButton, VRInputManager.Controller.RightHand));
      this.LeftButtonPressed.SetValue(VRInputManager.Get(VRInputManager.Button.PrimaryButton, VRInputManager.Controller.LeftHand));
      if (!(bool) this.Teleporting)
        return;
      Vector3 position = this._shootPoint.position;
      Vector3 forward = this._shootPoint.forward;
      Vector3 velocity = forward * this.settings.laserPower;
      Vector3 landingPoint = AutoAim.GetLandingPoint(position, velocity, 0.0f, out float _, this.settings.gravity);
      Vector2 vector2Xz = position.ToVector2XZ();
      Vector2 intersection;
      if (this._fieldBounds.Intersects(this._fieldBounds.Contains(vector2Xz) ? vector2Xz : this._fieldBounds.center, landingPoint.ToVector2XZ(), out intersection))
      {
        landingPoint.x = intersection.x;
        landingPoint.z = intersection.y;
      }
      this._target.position = landingPoint.SetY(0.02f);
      Vector3[] positions = this.settings.useSpline ? this.GetTrajectoryCurved(position, forward, landingPoint) : this.GetTrajectoryBall(position, landingPoint);
      this._lineRenderer.positionCount = positions.Length;
      this._lineRenderer.SetPositions(positions);
    }

    private Vector3[] GetTrajectoryBall(Vector3 startPos, Vector3 targetPos)
    {
      Vector3 distance = targetPos - startPos;
      float num = distance.magnitude;
      if ((double) num > 1.0)
        num = Mathf.Pow(num, 1f / this.settings.timeFactor);
      float gravity = this.settings.gravity;
      Vector3 impulseToHitTarget = AutoAim.GetImpulseToHitTarget(distance, num, gravity);
      float flightTime = AutoAim.GetFlightTime(startPos, impulseToHitTarget, 0.02f, gravity);
      return AutoAim.GetTrajectory(startPos, impulseToHitTarget, 0.03f, flightTime, gravity);
    }

    private Vector3[] GetTrajectoryCurved(
      Vector3 startPos,
      Vector3 controllerDir,
      Vector3 targetPos,
      float step = 0.03f)
    {
      Vector3 vector3 = targetPos - startPos;
      this._spline.nodes = new List<SplineNode>(2)
      {
        new SplineNode(startPos, startPos + controllerDir * this.settings.splineCoef),
        new SplineNode(targetPos, targetPos + Vector3.down * (vector3.magnitude * this.settings.splineHeightCoef))
      };
      this._spline.RefreshCurves();
      int length = Mathf.CeilToInt(33.3333359f);
      Vector3[] trajectoryCurved = new Vector3[length];
      float t = 0.0f;
      for (int index = 0; index < length; ++index)
      {
        trajectoryCurved[index] = this._spline.GetSample(t).location;
        t = Mathf.Clamp01(t + step);
      }
      return trajectoryCurved;
    }

    private async void HandleTeleportButton(bool buttonPressed)
    {
      TeleportationController teleportationController = this;
      teleportationController._target.gameObject.SetActive(buttonPressed);
      if (buttonPressed)
      {
        if (teleportationController._teleporting)
        {
          // ISSUE: reference to a compiler-generated method
          await TaskEx.WaitUntil(new Func<bool>(teleportationController.\u003CHandleTeleportButton\u003Eb__26_0));
        }
        if (!(bool) teleportationController.Teleporting)
          return;
        teleportationController._lineRenderer.enabled = true;
        teleportationController._fadeRoutine.Stop();
        teleportationController.SetColor(teleportationController._lineColor);
        teleportationController.Update();
      }
      else
      {
        if (teleportationController._teleporting)
          return;
        teleportationController._teleporting = true;
        teleportationController._fadeRoutine.Run(teleportationController.FadeRoutine());
        await GamePlayerController.CameraFade.FadeAsync(0.15f);
        PersistentSingleton<GamePlayerController>.Instance.position = teleportationController._target.position;
        GamePlayerController.CameraFade.Clear(0.15f);
        await Task.Delay(150);
        teleportationController._teleporting = false;
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
