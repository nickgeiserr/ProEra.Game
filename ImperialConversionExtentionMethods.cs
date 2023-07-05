// Decompiled with JetBrains decompiler
// Type: ImperialConversionExtentionMethods
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public static class ImperialConversionExtentionMethods
{
  public const float MeterToYardsScaleFactor = 1.0936f;

  public static float MetersToYards(this float metricFloat) => metricFloat * 1.0936f;

  public static float YardsToMeters(this float yardFloat) => yardFloat / 1.0936f;

  public static Vector2 MetersToYards(this Vector2 metricVector) => metricVector * 1.0936f;

  public static Vector2 YardsToMeters(this Vector2 yardVector) => yardVector / 1.0936f;

  public static Vector3 MetersToYards(this Vector3 metricVector) => metricVector * 1.0936f;

  public static Vector3 YardsToMeters(this Vector3 yardVector) => yardVector / 1.0936f;

  public static List<Vector3> MetersToYards(this List<Vector3> metricVectorList)
  {
    List<Vector3> yards = new List<Vector3>();
    for (int index = 0; index < metricVectorList.Count; ++index)
      yards.Add(metricVectorList[index].MetersToYards());
    return yards;
  }

  public static List<Vector3> YardsToMeters(this List<Vector3> yardVectorList)
  {
    List<Vector3> meters = new List<Vector3>();
    for (int index = 0; index < yardVectorList.Count; ++index)
      meters.Add(yardVectorList[index].YardsToMeters());
    return meters;
  }

  public static Vector3 GetLocalPositionYards(this Transform transform) => transform.localPosition.MetersToYards();

  public static Vector3 GetPositionYards(this Transform transform) => transform.position.MetersToYards();

  public static void SetPositionYards(this Transform transform, Vector3 yardsVector) => transform.position = yardsVector.YardsToMeters();

  public static void SetLocalPositionYards(this Transform transform, Vector3 yardsVector) => transform.localPosition = yardsVector.YardsToMeters();
}
