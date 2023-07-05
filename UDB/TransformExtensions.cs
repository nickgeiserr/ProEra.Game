// Decompiled with JetBrains decompiler
// Type: UDB.TransformExtensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public static class TransformExtensions
  {
    public static Transform[] GetChildren(this Transform transform)
    {
      Transform[] children = new Transform[transform.childCount];
      for (int index = 0; index < children.Length; ++index)
        children[index] = transform.GetChild(index);
      return children;
    }

    public static Bounds GetCombinedBounds(this Transform transform)
    {
      Bounds combinedBounds = new Bounds(Vector3.zero, Vector3.zero);
      MeshFilter[] componentsInChildren = transform.GetComponentsInChildren<MeshFilter>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        Bounds bounds = componentsInChildren[index].mesh.bounds with
        {
          center = componentsInChildren[index].transform.position
        };
        if ((double) combinedBounds.size.x == 0.0 && (double) combinedBounds.size.y == 0.0)
          combinedBounds = bounds;
        else
          combinedBounds.Encapsulate(bounds);
      }
      combinedBounds.center = transform.position;
      return combinedBounds;
    }

    public static Bounds GetCombinedMeshRendererBounds(this Transform transform)
    {
      Bounds meshRendererBounds = new Bounds(Vector3.zero, Vector3.zero);
      MeshRenderer[] componentsInChildren = transform.GetComponentsInChildren<MeshRenderer>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        Bounds bounds = componentsInChildren[index].bounds with
        {
          center = componentsInChildren[index].transform.position
        };
        if ((double) meshRendererBounds.size.x == 0.0 && (double) meshRendererBounds.size.y == 0.0)
          meshRendererBounds = bounds;
        else
          meshRendererBounds.Encapsulate(bounds);
      }
      meshRendererBounds.center = transform.position;
      return meshRendererBounds;
    }

    public static void Reset(this Transform transform)
    {
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      transform.localScale = Vector3.one;
    }

    public static void ScaleLocalScale(this Transform transform, float x = 1f, float y = 1f, float z = 1f) => transform.localScale = new Vector3(x * transform.localScale.x, y * transform.localScale.y, z * transform.localScale.z);

    public static void SetLocalScale(this Transform transform, float x, float y, float z) => transform.localScale = new Vector3(x, y, z);

    public static void SetPosition(this Transform transform, float? x = null, float? y = null, float? z = null) => transform.position = transform.position.Change3(x, y, z);

    public static void SetLocalPosition(this Transform transform, float? x = null, float? y = null, float? z = null) => transform.localPosition = transform.localPosition.Change3(x, y, z);

    public static void SetLocalX(this Transform transform, float x)
    {
      Vector3 localPosition = transform.localPosition with
      {
        x = x
      };
      transform.localPosition = localPosition;
    }

    public static void SetLocalY(this Transform transform, float y)
    {
      Vector3 localPosition = transform.localPosition with
      {
        y = y
      };
      transform.localPosition = localPosition;
    }

    public static void SetLocalZ(this Transform transform, float z)
    {
      Vector3 localPosition = transform.localPosition with
      {
        z = z
      };
      transform.localPosition = localPosition;
    }
  }
}
