// Decompiled with JetBrains decompiler
// Type: FootballVR.RouteObject
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DataSensitiveStructs_v5;
using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public class RouteObject : MonoBehaviour
  {
    [SerializeField]
    private MeshLine _meshLine;
    [SerializeField]
    private SpriteRenderer _tip;
    [SerializeField]
    private Color _idleColor;
    [SerializeField]
    private Color _activeColor;
    [SerializeField]
    private Color _completeColor;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float _progress;
    [SerializeField]
    private float _appearDuration = 1f;
    [SerializeField]
    private float _tipAppearDuration = 0.3f;
    [SerializeField]
    private AnimationCurve _animationCurve;
    private RouteData _routeData;
    private MeshLine.LineSettings _meshLineSettings;
    private readonly RoutineHandle _appearRoutine = new RoutineHandle();
    private readonly RoutineHandle _tipAppearRoutine = new RoutineHandle();

    public Vector3[] WorldPoints { get; private set; }

    public float Length { get; private set; }

    private void OnValidate() => this.SetProgress(this._progress);

    private void OnDisable()
    {
      this._appearRoutine.Stop();
      this._tipAppearRoutine.Stop();
    }

    public void Initialize(List<Vector2> points, float width, bool useTipAsEnd)
    {
      this._meshLine.Reset();
      this._meshLineSettings = this._meshLine.lineSettings;
      this._meshLineSettings.width = width;
      this._appearRoutine.Run(this.AppearSequence());
      this.SetProgress(0.0f);
      this.UpdatePositions(MathUtils.TransformDataCoordinatesToScenePosition(points));
      Vector3 point = (Vector3) points[points.Count - 1];
      if (useTipAsEnd)
      {
        this._tip.transform.localPosition = point.SetZ(-0.01f);
      }
      else
      {
        this._tipAppearRoutine.Stop();
        this._tip.gameObject.SetActive(false);
      }
      this.WorldPoints = this.GetPointsInWorldSpace();
      this.Length = 0.0f;
      int num = this.WorldPoints.Length - 1;
      for (int index = 0; index < num; ++index)
        this.Length += (this.WorldPoints[index + 1] - this.WorldPoints[index]).magnitude;
    }

    private IEnumerator AppearSequence()
    {
      float startTime = Time.time;
      float endTime = startTime + this._appearDuration;
      Color color = this._idleColor;
      float alpha = this._idleColor.a;
      while ((double) Time.time < (double) endTime)
      {
        float time = Mathf.InverseLerp(startTime, endTime, Time.time);
        color.a = this._animationCurve.Evaluate(time) * alpha;
        this._meshLine.SetColor(color);
        this._tip.color = color;
        yield return (object) null;
      }
      this._meshLine.SetColor(this._idleColor);
      this._tip.color = this._idleColor;
    }

    private void UpdatePositions(List<Vector3> newPositions)
    {
      List<Vector2> points = this._meshLineSettings.points;
      points.Clear();
      int index = 0;
      for (int count = newPositions.Count; index < count; ++index)
        points.Add(new Vector2(newPositions[index].x, newPositions[index].z));
      this._meshLineSettings.points = points;
      this._meshLine.UpdateMesh(this._meshLineSettings);
    }

    private void SetColor(Color color)
    {
      this._tipAppearRoutine.Stop();
      this._meshLine.SetColor(color);
      if (!((Object) this._tip != (Object) null))
        return;
      color.a = 1f;
      this._tip.color = color;
    }

    public void SetProgress(float val) => this._meshLine.SetProgress(val);

    private Vector3[] GetPointsInWorldSpace()
    {
      List<Vector2> points = this._meshLine.Points;
      if (points == null)
      {
        Debug.LogError((object) "Route has no points assigned.");
        return new Vector3[0];
      }
      int count = points.Count;
      Vector3[] pointsInWorldSpace = new Vector3[count];
      for (int index = 0; index < count; ++index)
      {
        Vector2 position = points[index];
        pointsInWorldSpace[index] = this.transform.TransformPoint((Vector3) position);
      }
      return pointsInWorldSpace;
    }

    public void MarkComplete() => this.SetColor(this._completeColor);

    public Vector3 GetPointOnRoute(float normalizedPosition)
    {
      Vector3[] worldPoints = this.WorldPoints;
      if (worldPoints == null)
        return Vector3.zero;
      float num1 = Mathf.Clamp01(normalizedPosition) * this.Length;
      float num2 = 0.0f;
      int num3 = worldPoints.Length - 1;
      for (int index = 0; index < num3; ++index)
      {
        Vector3 vector3 = worldPoints[index + 1] - worldPoints[index];
        float magnitude = vector3.magnitude;
        if ((double) num1 > (double) num2 + (double) magnitude)
        {
          num2 += magnitude;
        }
        else
        {
          float num4 = num1 - num2;
          return worldPoints[index] + vector3.normalized * num4;
        }
      }
      return Vector3.zero;
    }

    public void Highlight() => this.SetColor(this._activeColor);

    public void SetTipPosition(Vector3 projectedPos)
    {
      this._tip.gameObject.SetActive(true);
      this._tipAppearRoutine.Run(this.TipAppearSequence());
      this._tip.transform.position = projectedPos.SetY(0.01f);
    }

    private IEnumerator TipAppearSequence()
    {
      float startTime = Time.time;
      float endTime = startTime + this._tipAppearDuration;
      Color color = this._activeColor;
      while ((double) Time.time < (double) endTime)
      {
        color.a = Mathf.InverseLerp(startTime, endTime, Time.time);
        this._tip.color = color;
        yield return (object) null;
      }
      this._tip.color = color.SetAlpha(1f);
    }
  }
}
