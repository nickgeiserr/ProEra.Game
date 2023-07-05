// Decompiled with JetBrains decompiler
// Type: TB12.Utilities
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;

namespace TB12
{
  public static class Utilities
  {
    private const float yardToMeter = 0.9144f;
    private static readonly Vector3 yardToGamePosOffset = new Vector3(-24.4f, 0.0f, -21.7f);
    private static readonly Vector2 yardToGamePosOffsetVec2 = new Vector2(-24.4f, -21.7f);

    public static float YardsToMeters(float yards) => yards / 0.9144f;

    public static Vector2 YardsToMeters(Vector2 yards) => yards / 0.9144f;

    public static Vector3 YardsToGamePos(Vector2 yardPos) => new Vector3(yardPos.x, 0.0f, yardPos.y) * 0.9144f + Utilities.yardToGamePosOffset;

    public static Vector2 YardsToGamePosVec2(Vector2 yardPos)
    {
      yardPos *= 0.9144f;
      yardPos += Utilities.yardToGamePosOffsetVec2;
      return yardPos;
    }

    public static Vector3 GamePosToYard(Vector3 pos)
    {
      pos -= Utilities.yardToGamePosOffset;
      return pos / 0.9144f;
    }

    public static bool Vector3Approx(Vector3 v1, Vector3 v2) => (double) Vector3.SqrMagnitude(v1 - v2) < 0.0001;

    public static bool Vector2Approx(Vector2 v1, Vector2 v2) => (double) Vector2.SqrMagnitude(v1 - v2) < 0.0001;

    public static void Invoke(float delay, Action func) => RoutineRunner.StartRoutine(Utilities.InvokeDelayedRoutine(delay, func));

    private static IEnumerator InvokeDelayedRoutine(float delay, Action func)
    {
      yield return (object) new WaitForSeconds(delay);
      func();
    }

    public static void ResetVRPosition()
    {
    }

    public static void SetPSVRBigSizeMode(float bigSizeScale)
    {
    }
  }
}
