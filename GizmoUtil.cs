// Decompiled with JetBrains decompiler
// Type: GizmoUtil
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class GizmoUtil
{
  private static readonly Vector3[] circleVertexList = new Vector3[25]
  {
    new Vector3(0.0f, 0.0f, 1f),
    new Vector3(0.2588f, 0.0f, 0.9659f),
    new Vector3(0.5f, 0.0f, 0.866f),
    new Vector3(0.7071f, 0.0f, 0.7071f),
    new Vector3(0.866f, 0.0f, 0.5f),
    new Vector3(0.9659f, 0.0f, 0.2588f),
    new Vector3(1f, 0.0f, 0.0f),
    new Vector3(0.9659f, 0.0f, -0.2588f),
    new Vector3(0.866f, 0.0f, -0.5f),
    new Vector3(0.7071f, 0.0f, -0.7071f),
    new Vector3(0.5f, 0.0f, -0.866f),
    new Vector3(0.2588f, 0.0f, -0.9659f),
    new Vector3(0.0f, 0.0f, -1f),
    new Vector3(-0.2588f, 0.0f, -0.9659f),
    new Vector3(-0.5f, 0.0f, -0.866f),
    new Vector3(-0.7071f, 0.0f, -0.7071f),
    new Vector3(-0.866f, 0.0f, -0.5f),
    new Vector3(-0.9659f, 0.0f, -0.2588f),
    new Vector3(-1f, 0.0f, -0.0f),
    new Vector3(-0.9659f, 0.0f, 0.2588f),
    new Vector3(-0.866f, 0.0f, 0.5f),
    new Vector3(-0.7071f, 0.0f, 0.7071f),
    new Vector3(-0.5f, 0.0f, 0.866f),
    new Vector3(-0.2588f, 0.0f, 0.9659f),
    new Vector3(0.0f, 0.0f, 1f)
  };

  public static void DrawCircleGizmo(Vector3 center, float radius)
  {
    Vector3 from = GizmoUtil.circleVertexList[0] * radius + center;
    int length = GizmoUtil.circleVertexList.Length;
    for (int index = 1; index < length; ++index)
      Gizmos.DrawLine(from, from = GizmoUtil.circleVertexList[index] * radius + center);
  }

  public static void DrawCircleGizmo(Vector3 center, float radius, Color color)
  {
    Gizmos.color = color;
    GizmoUtil.DrawCircleGizmo(center, radius);
  }

  public static void DrawOvalGizmo(Vector3 center, Vector3 size)
  {
    Vector3 b = size / 2f;
    Vector3 from = Vector3.Scale(GizmoUtil.circleVertexList[0], b) + center;
    int length = GizmoUtil.circleVertexList.Length;
    for (int index = 1; index < length; ++index)
      Gizmos.DrawLine(from, from = Vector3.Scale(GizmoUtil.circleVertexList[index], b) + center);
  }

  public static void DrawOvalGizmo(Vector3 center, Vector3 size, Color color)
  {
    Gizmos.color = color;
    GizmoUtil.DrawOvalGizmo(center, size);
  }

  public static void DrawRectGizmo(float top, float left, float bottom, float right)
  {
    Vector3 vector3_1 = new Vector3(left, 0.0f, bottom);
    Vector3 vector3_2 = new Vector3(right, 0.0f, bottom);
    Vector3 vector3_3 = new Vector3(right, 0.0f, top);
    Vector3 vector3_4 = new Vector3(left, 0.0f, top);
    Gizmos.DrawLine(vector3_1, vector3_2);
    Gizmos.DrawLine(vector3_2, vector3_3);
    Gizmos.DrawLine(vector3_3, vector3_4);
    Gizmos.DrawLine(vector3_4, vector3_1);
  }

  public static void DrawRectGizmo(Rect rect)
  {
    Vector3 vector3_1 = new Vector3(rect.xMin, 0.0f, rect.yMin);
    Vector3 vector3_2 = new Vector3(rect.xMax, 0.0f, rect.yMin);
    Vector3 vector3_3 = new Vector3(rect.xMax, 0.0f, rect.yMax);
    Vector3 vector3_4 = new Vector3(rect.xMin, 0.0f, rect.yMax);
    Gizmos.DrawLine(vector3_1, vector3_2);
    Gizmos.DrawLine(vector3_2, vector3_3);
    Gizmos.DrawLine(vector3_3, vector3_4);
    Gizmos.DrawLine(vector3_4, vector3_1);
  }

  public static void DrawRectGizmo(Rect rect, Color color)
  {
    Gizmos.color = color;
    GizmoUtil.DrawRectGizmo(rect);
  }
}
