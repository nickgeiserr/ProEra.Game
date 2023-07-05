// Decompiled with JetBrains decompiler
// Type: FootballVR.ThrowMath
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public static class ThrowMath
  {
    private static readonly AccelerationProfile DefaultProfile = new AccelerationProfile();
    public static int dotFound = 0;
    public static int staticFound = 0;
    public static int notFound = 0;

    public static Vector3 GetVectorBasedOnAcceleration(
      IReadOnlyList<Vector3> accelerations,
      out int startIndex,
      AccelerationProfile profile = null)
    {
      if (profile == null)
        profile = ThrowMath.DefaultProfile;
      startIndex = ThrowMath.GetThrowStartIndex(accelerations, profile);
      startIndex = Mathf.Clamp(startIndex, 0, accelerations.Count - 5);
      Vector3[] vector3Array = new Vector3[accelerations.Count - startIndex];
      vector3Array[0] = Vector3.zero;
      for (int index = 1; index < vector3Array.Length; ++index)
        vector3Array[index] = vector3Array[index - 1] + accelerations[startIndex + index];
      Vector3 zero = Vector3.zero;
      startIndex = vector3Array.Length - 14;
      if (startIndex < 0)
        startIndex = 0;
      for (int index = startIndex; index < vector3Array.Length; ++index)
        zero += vector3Array[index];
      return zero / 14f;
    }

    private static int GetThrowStartIndex(
      IReadOnlyList<Vector3> accelerations,
      AccelerationProfile profile)
    {
      int count = accelerations.Count;
      Vector3 rhs = Vector3.zero;
      int max = Mathf.Clamp(count - profile.rawDirectionFramesOffset, 0, count);
      for (int index = Mathf.Clamp(max - profile.rawDirectionFrames, 0, max); index < max; ++index)
        rhs += accelerations[index];
      rhs = rhs.normalized;
      int num = 0;
      for (int index = count - profile.minStartIndexOffset; index >= 0; --index)
      {
        Vector3 acceleration = accelerations[index];
        if ((double) Vector3.Dot(acceleration.normalized, rhs) < (double) profile.dotMinimum)
        {
          ++ThrowMath.dotFound;
          return index + profile.dotFramesOffset;
        }
        acceleration = accelerations[index];
        if ((double) acceleration.sqrMagnitude < (double) profile.staticThreshold)
          ++num;
        else
          num = 0;
        if (num >= profile.staticFrameCount)
        {
          ++ThrowMath.staticFound;
          return index + profile.staticFrameOffset;
        }
      }
      ++ThrowMath.notFound;
      return 0;
    }

    private static Vector3 GetVectorSum(
      IReadOnlyList<Vector3> values,
      out int countConsidered,
      int count = -1,
      float endThreshold = -1f,
      float minThreshold = 4f)
    {
      if (count == -1)
        count = values.Count;
      countConsidered = 0;
      int count1 = values.Count;
      int num = Mathf.Clamp(count1 - count, 0, values.Count);
      Vector3 zero = Vector3.zero;
      for (int index = num; index < count1; ++index)
      {
        if ((double) values[index].sqrMagnitude >= (double) minThreshold && (index <= count1 - 3 || (double) Vector3.Dot(values[index].normalized, zero.normalized) >= (double) endThreshold))
        {
          ++countConsidered;
          zero += values[index];
        }
      }
      return zero;
    }

    public static Vector3 GetThrowVector(
      IReadOnlyList<Vector3> values,
      int frameCount = 8,
      float endThreshold = -1f,
      float minThreshold = 0.0001f)
    {
      return ThrowMath.GetAverageVector(values, 10, minThreshold: 0.5f).normalized * ThrowMath.GetAverageVector(values, 9, minThreshold: 0.5f).magnitude;
    }

    public static Vector3 GetAverageVector(
      IReadOnlyList<Vector3> values,
      int frameCount = 8,
      float dotThreshold = -1f,
      float minThreshold = 0.0001f,
      int rawDirFrameCount = 4)
    {
      frameCount = Mathf.Clamp(frameCount, 1, values.Count);
      int countConsidered;
      Vector3 vectorSum = ThrowMath.GetVectorSum(values, out countConsidered, frameCount, dotThreshold, minThreshold);
      if (countConsidered > 0)
        vectorSum /= (float) countConsidered;
      return vectorSum;
    }

    public static Vector3 GetLerpedVector(
      IReadOnlyList<Vector3> values,
      int count,
      float lerpFactor,
      float minThreshold,
      float endThreshold)
    {
      if (count == -1)
        count = Mathf.Min(values.Count, 2);
      int count1 = values.Count;
      int num = Mathf.Clamp(count1 - count, 0, values.Count);
      Vector3 a = values[0];
      for (int index = num + 1; index < count1; ++index)
      {
        if ((double) values[index].sqrMagnitude >= (double) minThreshold && (index <= count1 - 3 || (double) Vector3.Dot(values[index].normalized, a.normalized) >= (double) endThreshold))
          a = Vector3.Lerp(a, values[index], lerpFactor);
      }
      return a;
    }

    public static Vector3 GetWeightedVector(
      IReadOnlyList<Vector3> values,
      int count,
      float decreaseFactor)
    {
      if (count == -1)
        count = Mathf.Min(values.Count, 2);
      decreaseFactor = Mathf.Clamp01(decreaseFactor);
      int count1 = values.Count;
      int num1 = Mathf.Clamp(count1 - count, 0, values.Count);
      float num2 = 1f;
      float num3 = 0.0f;
      Vector3 zero = Vector3.zero;
      for (int index = num1; index < count1; ++index)
      {
        zero += values[index] * num2;
        num3 += num2;
        num2 *= decreaseFactor;
      }
      return zero / num3;
    }

    public static Vector3 EstimateHandPosition(
      IReadOnlyList<Vector3> trackedVelocities,
      IReadOnlyList<Vector3> accelerations,
      Vector3 lastKnownPosition,
      int throwStartIndex)
    {
      float deltaTime = Time.deltaTime;
      int num1 = -1;
      for (int index = trackedVelocities.Count - 1; index >= 0; --index)
      {
        if ((double) trackedVelocities[index].sqrMagnitude > 0.05000000074505806)
        {
          num1 = index;
          break;
        }
      }
      int num2 = Mathf.Max(num1, throwStartIndex);
      Vector3[] vector3Array = new Vector3[accelerations.Count - num2];
      vector3Array[0] = num1 == -1 ? Vector3.zero : trackedVelocities[num1];
      for (int index = 1; index < vector3Array.Length; ++index)
      {
        vector3Array[index] = vector3Array[index - 1] + accelerations[num2 + index];
        lastKnownPosition += vector3Array[index] * deltaTime;
      }
      return lastKnownPosition;
    }
  }
}
