// Decompiled with JetBrains decompiler
// Type: SplineMesh.SplineNode
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace SplineMesh
{
  [Serializable]
  public class SplineNode
  {
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private Vector3 up = Vector3.up;
    [SerializeField]
    private Vector2 scale = Vector2.one;
    [SerializeField]
    private float roll;

    public Vector3 Position
    {
      get => this.position;
      set
      {
        if (this.position.Equals(value))
          return;
        this.position.x = value.x;
        this.position.y = value.y;
        this.position.z = value.z;
        if (this.Changed == null)
          return;
        this.Changed((object) this, EventArgs.Empty);
      }
    }

    public Vector3 Direction
    {
      get => this.direction;
      set
      {
        if (this.direction.Equals(value))
          return;
        this.direction.x = value.x;
        this.direction.y = value.y;
        this.direction.z = value.z;
        if (this.Changed == null)
          return;
        this.Changed((object) this, EventArgs.Empty);
      }
    }

    public Vector3 Up
    {
      get => this.up;
      set
      {
        if (this.up.Equals(value))
          return;
        this.up.x = value.x;
        this.up.y = value.y;
        this.up.z = value.z;
        if (this.Changed == null)
          return;
        this.Changed((object) this, EventArgs.Empty);
      }
    }

    public Vector2 Scale
    {
      get => this.scale;
      set
      {
        if (this.scale.Equals(value))
          return;
        this.scale.x = value.x;
        this.scale.y = value.y;
        if (this.Changed == null)
          return;
        this.Changed((object) this, EventArgs.Empty);
      }
    }

    public float Roll
    {
      get => this.roll;
      set
      {
        if ((double) this.roll == (double) value)
          return;
        this.roll = value;
        if (this.Changed == null)
          return;
        this.Changed((object) this, EventArgs.Empty);
      }
    }

    public SplineNode(Vector3 position, Vector3 direction)
    {
      this.Position = position;
      this.Direction = direction;
    }

    [HideInInspector]
    public event EventHandler Changed;
  }
}
