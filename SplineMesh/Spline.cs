// Decompiled with JetBrains decompiler
// Type: SplineMesh.Spline
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace SplineMesh
{
  [DisallowMultipleComponent]
  [ExecuteInEditMode]
  public class Spline : MonoBehaviour
  {
    public List<SplineNode> nodes = new List<SplineNode>();
    [HideInInspector]
    public List<CubicBezierCurve> curves = new List<CubicBezierCurve>();
    public float Length;
    [SerializeField]
    private bool isLoop;
    [HideInInspector]
    public UnityEvent CurveChanged = new UnityEvent();
    private SplineNode start;
    private SplineNode end;

    public bool IsLoop
    {
      get => this.isLoop;
      set
      {
        this.isLoop = value;
        this.updateLoopBinding();
      }
    }

    public event ListChangeHandler<SplineNode> NodeListChanged;

    private void Reset()
    {
      this.nodes.Clear();
      this.curves.Clear();
      this.AddNode(new SplineNode(new Vector3(5f, 0.0f, 0.0f), new Vector3(5f, 0.0f, -3f)));
      this.AddNode(new SplineNode(new Vector3(10f, 0.0f, 0.0f), new Vector3(10f, 0.0f, 3f)));
      this.RaiseNodeListChanged(new ListChangedEventArgs<SplineNode>()
      {
        type = ListChangeType.clear
      });
      this.UpdateAfterCurveChanged();
    }

    private void OnEnable() => this.RefreshCurves();

    public ReadOnlyCollection<CubicBezierCurve> GetCurves() => this.curves.AsReadOnly();

    private void RaiseNodeListChanged(ListChangedEventArgs<SplineNode> args)
    {
      if (this.NodeListChanged == null)
        return;
      this.NodeListChanged((object) this, args);
    }

    private void UpdateAfterCurveChanged()
    {
      this.Length = 0.0f;
      foreach (CubicBezierCurve curve in this.curves)
        this.Length += curve.Length;
      this.CurveChanged.Invoke();
    }

    public CurveSample GetSample(float t)
    {
      int nodeIndexForTime = this.GetNodeIndexForTime(t);
      return this.curves[nodeIndexForTime].GetSample(t - (float) nodeIndexForTime);
    }

    public CubicBezierCurve GetCurve(float t) => this.curves[this.GetNodeIndexForTime(t)];

    private int GetNodeIndexForTime(float t)
    {
      if ((double) t < 0.0 || (double) t > (double) (this.nodes.Count - 1))
        throw new ArgumentException(string.Format("Time must be between 0 and last node index ({0}). Given time was {1}.", (object) (this.nodes.Count - 1), (object) t));
      int nodeIndexForTime = Mathf.FloorToInt(t);
      if (nodeIndexForTime == this.nodes.Count - 1)
        --nodeIndexForTime;
      return nodeIndexForTime;
    }

    public void RefreshCurves()
    {
      this.curves.Clear();
      for (int index = 0; index < this.nodes.Count - 1; ++index)
      {
        CubicBezierCurve cubicBezierCurve = new CubicBezierCurve(this.nodes[index], this.nodes[index + 1]);
        cubicBezierCurve.Changed.AddListener(new UnityAction(this.UpdateAfterCurveChanged));
        this.curves.Add(cubicBezierCurve);
      }
      this.RaiseNodeListChanged(new ListChangedEventArgs<SplineNode>()
      {
        type = ListChangeType.clear
      });
      this.UpdateAfterCurveChanged();
    }

    public CurveSample GetSampleAtDistance(float d)
    {
      if ((double) d < 0.0 || (double) d > (double) this.Length)
        throw new ArgumentException(string.Format("Distance must be between 0 and spline length ({0}). Given distance was {1}.", (object) this.Length, (object) d));
      foreach (CubicBezierCurve curve in this.curves)
      {
        if ((double) d > (double) curve.Length && (double) d < (double) curve.Length + 9.9999997473787516E-05)
          d = curve.Length;
        if ((double) d <= (double) curve.Length)
          return curve.GetSampleAtDistance(d);
        d -= curve.Length;
      }
      throw new Exception("Something went wrong with GetSampleAtDistance.");
    }

    public void AddNode(SplineNode node)
    {
      this.nodes.Add(node);
      if (this.nodes.Count != 1)
      {
        CubicBezierCurve cubicBezierCurve = new CubicBezierCurve(this.nodes[this.nodes.IndexOf(node) - 1], node);
        cubicBezierCurve.Changed.AddListener(new UnityAction(this.UpdateAfterCurveChanged));
        this.curves.Add(cubicBezierCurve);
      }
      this.RaiseNodeListChanged(new ListChangedEventArgs<SplineNode>()
      {
        type = ListChangeType.Add,
        newItems = new List<SplineNode>() { node }
      });
      this.UpdateAfterCurveChanged();
      this.updateLoopBinding();
    }

    public void InsertNode(int index, SplineNode node)
    {
      SplineNode splineNode = index != 0 ? this.nodes[index - 1] : throw new Exception("Can't insert a node at index 0");
      SplineNode node1 = this.nodes[index];
      this.nodes.Insert(index, node);
      this.curves[index - 1].ConnectEnd(node);
      CubicBezierCurve cubicBezierCurve = new CubicBezierCurve(node, node1);
      cubicBezierCurve.Changed.AddListener(new UnityAction(this.UpdateAfterCurveChanged));
      this.curves.Insert(index, cubicBezierCurve);
      this.RaiseNodeListChanged(new ListChangedEventArgs<SplineNode>()
      {
        type = ListChangeType.Insert,
        newItems = new List<SplineNode>() { node },
        insertIndex = index
      });
      this.UpdateAfterCurveChanged();
      this.updateLoopBinding();
    }

    public void RemoveNode(SplineNode node)
    {
      int index = this.nodes.IndexOf(node);
      if (this.nodes.Count <= 2)
        throw new Exception("Can't remove the node because a spline needs at least 2 nodes.");
      CubicBezierCurve cubicBezierCurve = index == this.nodes.Count - 1 ? this.curves[index - 1] : this.curves[index];
      if (index != 0 && index != this.nodes.Count - 1)
      {
        SplineNode node1 = this.nodes[index + 1];
        this.curves[index - 1].ConnectEnd(node1);
      }
      this.nodes.RemoveAt(index);
      cubicBezierCurve.Changed.RemoveListener(new UnityAction(this.UpdateAfterCurveChanged));
      this.curves.Remove(cubicBezierCurve);
      this.RaiseNodeListChanged(new ListChangedEventArgs<SplineNode>()
      {
        type = ListChangeType.Remove,
        removedItems = new List<SplineNode>() { node },
        removeIndex = index
      });
      this.UpdateAfterCurveChanged();
      this.updateLoopBinding();
    }

    private void updateLoopBinding()
    {
      if (this.start != null)
        this.start.Changed -= new EventHandler(this.StartNodeChanged);
      if (this.end != null)
        this.end.Changed -= new EventHandler(this.EndNodeChanged);
      if (this.isLoop)
      {
        this.start = this.nodes[0];
        this.end = this.nodes[this.nodes.Count - 1];
        this.start.Changed += new EventHandler(this.StartNodeChanged);
        this.end.Changed += new EventHandler(this.EndNodeChanged);
        this.StartNodeChanged((object) null, (EventArgs) null);
      }
      else
      {
        this.start = (SplineNode) null;
        this.end = (SplineNode) null;
      }
    }

    private void StartNodeChanged(object sender, EventArgs e)
    {
      this.end.Changed -= new EventHandler(this.EndNodeChanged);
      this.end.Position = this.start.Position;
      this.end.Direction = this.start.Direction;
      this.end.Roll = this.start.Roll;
      this.end.Scale = this.start.Scale;
      this.end.Up = this.start.Up;
      this.end.Changed += new EventHandler(this.EndNodeChanged);
    }

    private void EndNodeChanged(object sender, EventArgs e)
    {
      this.start.Changed -= new EventHandler(this.StartNodeChanged);
      this.start.Position = this.end.Position;
      this.start.Direction = this.end.Direction;
      this.start.Roll = this.end.Roll;
      this.start.Scale = this.end.Scale;
      this.start.Up = this.end.Up;
      this.start.Changed += new EventHandler(this.StartNodeChanged);
    }

    public CurveSample GetProjectionSample(Vector3 pointToProject)
    {
      CurveSample projectionSample1 = new CurveSample();
      float num1 = float.MaxValue;
      float num2 = 0.0f;
      foreach (CubicBezierCurve curve in this.curves)
      {
        CurveSample projectionSample2 = curve.GetProjectionSample(pointToProject);
        Vector3 vector3;
        if (curve == this.curves[0])
        {
          projectionSample1 = projectionSample2;
          projectionSample1.timeInSpline = (projectionSample1.distanceInCurve + num2) / this.Length;
          vector3 = projectionSample2.location - pointToProject;
          num1 = vector3.sqrMagnitude;
          num2 += curve.Length;
        }
        else
        {
          vector3 = projectionSample2.location - pointToProject;
          float sqrMagnitude = vector3.sqrMagnitude;
          if ((double) sqrMagnitude < (double) num1)
          {
            num1 = sqrMagnitude;
            projectionSample1 = projectionSample2;
            projectionSample1.timeInSpline = (projectionSample1.distanceInCurve + num2) / this.Length;
          }
          num2 += curve.Length;
        }
      }
      return projectionSample1;
    }
  }
}
