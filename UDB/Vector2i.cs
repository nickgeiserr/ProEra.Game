// Decompiled with JetBrains decompiler
// Type: UDB.Vector2i
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  [Serializable]
  public struct Vector2i
  {
    public const int X_INDEX = 0;
    public const int Y_INDEX = 1;
    public int x;
    public int y;

    public int this[int index]
    {
      get
      {
        if (index == 0)
          return this.x;
        if (index == 1)
          return this.y;
        throw new IndexOutOfRangeException("Invalid Vector2i index!");
      }
      set
      {
        if (index != 0)
        {
          if (index != 1)
            throw new IndexOutOfRangeException("Invalid Vector2i index!");
          this.y = value;
        }
        else
          this.x = value;
      }
    }

    public Vector2i(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public float sqrMagnitude => (float) ((double) this.x * (double) this.x + (double) this.y * (double) this.y);

    public float magnitude => Mathf.Sqrt(this.sqrMagnitude);

    public bool IsWithinBounds(Vector2i from, Vector2i to) => this.x >= from.x && this.x < to.x && this.y >= from.y && this.y < to.y;

    public void Set(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public void Scale(Vector2i scale)
    {
      this.x *= scale.x;
      this.y *= scale.y;
    }

    public static Vector2i Scale(Vector2i a, Vector2i b) => new Vector2i(a.x * b.x, a.y * b.y);

    public void RotateCW()
    {
      int x = this.x;
      this.x = this.y;
      this.y = -x;
    }

    public void RotateCCW()
    {
      int x = this.x;
      this.x = -this.y;
      this.y = x;
    }

    public static Vector2i RotateCW(Vector2i a) => new Vector2i(a.y, -a.x);

    public static Vector2i RotateCCW(Vector2i a) => new Vector2i(-a.y, a.x);

    public static void RectLoop(Vector2i from, Vector2i to, Action<Vector2i> body)
    {
      if (body == null)
        throw new ArgumentNullException(nameof (body));
      Vector2i zero = Vector2i.zero;
      for (zero.x = from.x; zero.x < to.x; ++zero.x)
      {
        for (zero.y = from.y; zero.y < to.y; ++zero.y)
          body(zero);
      }
    }

    public override string ToString() => string.Format("({0}, {1})", (object) this.x, (object) this.y);

    public static Vector2i operator +(Vector2i a, Vector2i b) => new Vector2i(a.x + b.x, a.y + b.y);

    public static Vector2i operator -(Vector2i a) => new Vector2i(-a.x, -a.y);

    public static Vector2i operator -(Vector2i a, Vector2i b) => a + -b;

    public static Vector2i operator *(int d, Vector2i a) => new Vector2i(d * a.x, d * a.y);

    public static Vector2i operator *(Vector2i a, int d) => d * a;

    public static Vector2i operator /(Vector2i a, int d) => new Vector2i(a.x / d, a.y / d);

    public static bool operator ==(Vector2i lhs, Vector2i rhs) => lhs.x == rhs.x && lhs.y == rhs.y;

    public static bool operator !=(Vector2i lhs, Vector2i rhs) => !(lhs == rhs);

    public override bool Equals(object other) => other is Vector2i vector2i && this == vector2i;

    public bool Equals(Vector2i other) => this == other;

    public override int GetHashCode() => this.x.GetHashCode() << 6 ^ this.y.GetHashCode();

    public static float Distance(Vector2i a, Vector2i b) => (a - b).magnitude;

    public static Vector2i Min(Vector2i lhs, Vector2i rhs) => new Vector2i(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));

    public static Vector2i Max(Vector2i a, Vector2i b) => new Vector2i(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y));

    public static int Dot(Vector2i lhs, Vector2i rhs) => lhs.x * rhs.x + lhs.y * rhs.y;

    public static float Magnitude(Vector2i a) => a.magnitude;

    public static float SqrMagnitude(Vector2i a) => a.sqrMagnitude;

    public static Vector2i down => new Vector2i(0, -1);

    public static Vector2i up => new Vector2i(0, 1);

    public static Vector2i left => new Vector2i(-1, 0);

    public static Vector2i right => new Vector2i(1, 0);

    public static Vector2i one => new Vector2i(1, 1);

    public static Vector2i zero => new Vector2i(0, 0);

    public static explicit operator Vector2i(Vector2 source) => new Vector2i((int) source.x, (int) source.y);

    public static implicit operator Vector2(Vector2i source) => new Vector2((float) source.x, (float) source.y);

    public static explicit operator Vector2i(Vector3 source) => new Vector2i((int) source.x, (int) source.y);

    public static implicit operator Vector3(Vector2i source) => new Vector3((float) source.x, (float) source.y, 0.0f);
  }
}
