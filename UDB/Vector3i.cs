// Decompiled with JetBrains decompiler
// Type: UDB.Vector3i
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  [Serializable]
  public struct Vector3i
  {
    public const int X_INDEX = 0;
    public const int Y_INDEX = 1;
    public const int Z_INDEX = 2;
    public int x;
    public int y;
    public int z;
    private static int[,] loopCoords = new int[6, 3]
    {
      {
        2,
        1,
        0
      },
      {
        2,
        0,
        1
      },
      {
        0,
        2,
        1
      },
      {
        1,
        2,
        0
      },
      {
        1,
        0,
        2
      },
      {
        0,
        1,
        2
      }
    };

    public int this[int index]
    {
      get
      {
        switch (index)
        {
          case 0:
            return this.x;
          case 1:
            return this.y;
          case 2:
            return this.z;
          default:
            throw new IndexOutOfRangeException("Invalid Vector3i index!");
        }
      }
      set
      {
        switch (index)
        {
          case 0:
            this.x = value;
            break;
          case 1:
            this.y = value;
            break;
          case 2:
            this.z = value;
            break;
          default:
            throw new IndexOutOfRangeException("Invalid Vector3i index!");
        }
      }
    }

    public Vector3i(int x, int y)
    {
      this.x = x;
      this.y = y;
      this.z = 0;
    }

    public Vector3i(int x, int y, int z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public float sqrMagnitude => (float) (this.x * this.x + this.y * this.y + this.z * this.z);

    public float magnitude => Mathf.Sqrt(this.sqrMagnitude);

    public bool IsWithinBounds(Vector3i from, Vector3i to) => this.x >= from.x && this.x < to.x && this.y >= from.y && this.y < to.y && this.z >= from.z && this.z < to.z;

    public void Set(int x, int y, int z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public void Scale(Vector3i scale)
    {
      this.x *= scale.x;
      this.y *= scale.y;
      this.z *= scale.z;
    }

    public static Vector3i Scale(Vector3i a, Vector3i b) => new Vector3i(a.x * b.x, a.y * b.y, a.z * b.z);

    public void RotateCW(int axis)
    {
      switch (axis)
      {
        case 0:
          int y = this.y;
          this.y = -this.z;
          this.z = y;
          break;
        case 1:
          int x1 = this.x;
          this.x = this.z;
          this.z = -x1;
          break;
        case 2:
          int x2 = this.x;
          this.x = -this.y;
          this.y = x2;
          break;
      }
    }

    public void RotateCCW(int axis)
    {
      switch (axis)
      {
        case 0:
          int y = this.y;
          this.y = this.z;
          this.z = -y;
          break;
        case 1:
          int x1 = this.x;
          this.x = -this.z;
          this.z = x1;
          break;
        case 2:
          int x2 = this.x;
          this.x = this.y;
          this.y = -x2;
          break;
      }
    }

    private static int GetCoord(Vector3i.LoopOrder loopOrder, int loopLevel) => Vector3i.loopCoords[(int) loopOrder, loopLevel];

    public static void CubeLoop(Vector3i from, Vector3i to, Action<Vector3i> body)
    {
      if (body == null)
        throw new ArgumentNullException(nameof (body));
      Vector3i zero = Vector3i.zero;
      for (zero.x = from.x; zero.x < to.x; ++zero.x)
      {
        for (zero.y = from.y; zero.y < to.y; ++zero.y)
        {
          for (zero.z = from.z; zero.z < to.z; ++zero.z)
            body(zero);
        }
      }
    }

    public override string ToString() => string.Format("({0}, {1}, {2})", (object) this.x, (object) this.y, (object) this.z);

    public static Vector3i operator +(Vector3i a, Vector3i b) => new Vector3i(a.x + b.x, a.y + b.y, a.z + b.z);

    public static Vector3i operator -(Vector3i a) => new Vector3i(-a.x, -a.y, -a.z);

    public static Vector3i operator -(Vector3i a, Vector3i b) => a + -b;

    public static Vector3i operator *(int d, Vector3i a) => new Vector3i(d * a.x, d * a.y, d * a.z);

    public static Vector3i operator *(Vector3i a, int d) => d * a;

    public static Vector3i operator /(Vector3i a, int d) => new Vector3i(a.x / d, a.y / d, a.z / d);

    public static bool operator ==(Vector3i lhs, Vector3i rhs) => lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;

    public static bool operator !=(Vector3i lhs, Vector3i rhs) => !(lhs == rhs);

    public override bool Equals(object other) => other is Vector3i vector3i && this == vector3i;

    public bool Equals(Vector3i other) => this == other;

    public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;

    public static float Distance(Vector3i a, Vector3i b) => (a - b).magnitude;

    public static Vector3i Min(Vector3i lhs, Vector3i rhs) => new Vector3i(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));

    public static Vector3i Max(Vector3i lhs, Vector3i rhs) => new Vector3i(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));

    public static int Dot(Vector3i lhs, Vector3i rhs) => lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;

    public static Vector3i Cross(Vector3i lhs, Vector3i rhs) => new Vector3i(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);

    public static float Magnitude(Vector3i a) => a.magnitude;

    public static float SqrMagnitude(Vector3i a) => a.sqrMagnitude;

    public static Vector3i back => new Vector3i(0, 0, -1);

    public static Vector3i forward => new Vector3i(0, 0, 1);

    public static Vector3i down => new Vector3i(0, -1, 0);

    public static Vector3i up => new Vector3i(0, 1, 0);

    public static Vector3i left => new Vector3i(-1, 0, 0);

    public static Vector3i right => new Vector3i(1, 0, 0);

    public static Vector3i one => new Vector3i(1, 1, 1);

    public static Vector3i zero => new Vector3i(0, 0, 0);

    public static explicit operator Vector3i(Vector3 source) => new Vector3i((int) source.x, (int) source.y, (int) source.z);

    public static implicit operator Vector3(Vector3i source) => new Vector3((float) source.x, (float) source.y, (float) source.z);

    public enum LoopOrder
    {
      ZYX,
      ZXY,
      XZY,
      YZX,
      YXZ,
      XYZ,
    }
  }
}
