// Decompiled with JetBrains decompiler
// Type: UDB.Vector3Extensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public static class Vector3Extensions
  {
    public static Vector2 XY(this Vector3 v) => new Vector2(v.x, v.y);

    public static Vector2 XZ(this Vector3 v) => new Vector2(v.x, v.z);

    public static Vector2 YZ(this Vector3 v) => new Vector2(v.y, v.z);

    public static Vector3 WithX(this Vector3 v, float newX) => new Vector3(newX, v.y, v.z);

    public static Vector3 WithY(this Vector3 v, float newY) => new Vector3(v.x, newY, v.z);

    public static Vector3 WithZ(this Vector3 v, float newZ) => new Vector3(v.x, v.y, newZ);

    public static Vector3 Change3(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
      if (x.HasValue)
        vector.x = x.Value;
      if (y.HasValue)
        vector.y = y.Value;
      if (z.HasValue)
        vector.z = z.Value;
      return vector;
    }
  }
}
