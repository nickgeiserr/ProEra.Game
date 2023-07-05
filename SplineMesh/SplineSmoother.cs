// Decompiled with JetBrains decompiler
// Type: SplineMesh.SplineSmoother
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace SplineMesh
{
  [DisallowMultipleComponent]
  [ExecuteInEditMode]
  [RequireComponent(typeof (Spline))]
  public class SplineSmoother : MonoBehaviour
  {
    private Spline spline;
    [Range(0.0f, 1f)]
    public float curvature = 0.3f;

    private Spline Spline
    {
      get
      {
        if ((UnityEngine.Object) this.spline == (UnityEngine.Object) null)
          this.spline = this.GetComponent<Spline>();
        return this.spline;
      }
    }

    private void OnValidate() => this.SmoothAll();

    private void OnEnable()
    {
      this.Spline.NodeListChanged += new ListChangeHandler<SplineNode>(this.Spline_NodeListChanged);
      foreach (SplineNode node in this.Spline.nodes)
        node.Changed += new EventHandler(this.OnNodeChanged);
      this.SmoothAll();
    }

    private void OnDisable()
    {
      this.Spline.NodeListChanged -= new ListChangeHandler<SplineNode>(this.Spline_NodeListChanged);
      foreach (SplineNode node in this.Spline.nodes)
        node.Changed -= new EventHandler(this.OnNodeChanged);
    }

    private void Spline_NodeListChanged(object sender, ListChangedEventArgs<SplineNode> args)
    {
      if (args.newItems != null)
      {
        foreach (SplineNode newItem in args.newItems)
          newItem.Changed += new EventHandler(this.OnNodeChanged);
      }
      if (args.removedItems == null)
        return;
      foreach (SplineNode removedItem in args.removedItems)
        removedItem.Changed -= new EventHandler(this.OnNodeChanged);
    }

    private void OnNodeChanged(object sender, EventArgs e)
    {
      SplineNode node = (SplineNode) sender;
      this.SmoothNode(node);
      int num = this.Spline.nodes.IndexOf(node);
      if (num > 0)
        this.SmoothNode(this.Spline.nodes[num - 1]);
      if (num >= this.Spline.nodes.Count - 1)
        return;
      this.SmoothNode(this.Spline.nodes[num + 1]);
    }

    private void SmoothNode(SplineNode node)
    {
      int num1 = this.Spline.nodes.IndexOf(node);
      Vector3 position1 = node.Position;
      Vector3 zero = Vector3.zero;
      float num2 = 0.0f;
      if (num1 != 0)
      {
        Vector3 position2 = this.Spline.nodes[num1 - 1].Position;
        Vector3 vector3 = position1 - position2;
        num2 += vector3.magnitude;
        zero += vector3.normalized;
      }
      if (num1 != this.Spline.nodes.Count - 1)
      {
        Vector3 position3 = this.Spline.nodes[num1 + 1].Position;
        Vector3 vector3 = position1 - position3;
        num2 += vector3.magnitude;
        zero -= vector3.normalized;
      }
      float num3 = num2 * 0.5f;
      Vector3 vector3_1 = zero.normalized * num3 * this.curvature + position1;
      node.Direction = vector3_1;
    }

    private void SmoothAll()
    {
      foreach (SplineNode node in this.Spline.nodes)
        this.SmoothNode(node);
    }
  }
}
