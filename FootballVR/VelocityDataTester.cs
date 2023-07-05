// Decompiled with JetBrains decompiler
// Type: FootballVR.VelocityDataTester
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FootballVR
{
  public class VelocityDataTester
  {
    private static readonly List<float> deltas = new List<float>();
    private static readonly List<float> dirDeltas = new List<float>();

    public static VelocityTestResult RunVelocityTestForData(List<CachedThrowData> data, string name)
    {
      Debug.LogError((object) ("Running velocity test for " + name));
      List<VelocityTestResult> source = new List<VelocityTestResult>();
      int[] numArray1 = new int[9]
      {
        7,
        8,
        9,
        10,
        11,
        12,
        13,
        14,
        15
      };
      foreach (int framesConsidered in numArray1)
      {
        float[] numArray2 = new float[13]
        {
          -1f,
          -0.6f,
          -0.4f,
          -0.2f,
          0.0f,
          0.1f,
          0.2f,
          0.3f,
          0.4f,
          0.5f,
          0.6f,
          0.7f,
          0.8f
        };
        foreach (float num in numArray2)
        {
          float[] numArray3 = new float[8]
          {
            0.0f,
            0.5f,
            1f,
            1.5f,
            2f,
            2.5f,
            3f,
            4f
          };
          foreach (float minThreshold in numArray3)
          {
            int[] numArray4 = new int[5]{ 3, 4, 5, 6, 7 };
            foreach (int rawDirFrameCount in numArray4)
            {
              bool[] flagArray = new bool[1]{ true };
              foreach (bool flag in flagArray)
              {
                VelocityTestResult velocityTestResult1 = VelocityDataTester.RunVelocityTest(data, framesConsidered, num, minThreshold, flag, rawDirFrameCount);
                if (velocityTestResult1 != null)
                  source.Add(velocityTestResult1);
                float[] numArray5 = new float[11]
                {
                  0.1f,
                  0.13f,
                  0.15f,
                  0.16f,
                  0.17f,
                  0.18f,
                  0.2f,
                  0.24f,
                  0.27f,
                  0.3f,
                  0.33f
                };
                foreach (float lerpVal in numArray5)
                {
                  VelocityTestResult velocityTestResult2 = VelocityDataTester.RunVelocityLerpTest(data, framesConsidered, lerpVal, minThreshold, num, flag, rawDirFrameCount);
                  if (velocityTestResult2 != null)
                    source.Add(velocityTestResult2);
                }
              }
            }
          }
        }
      }
      int num1 = Math.Min(source.Count, 24);
      Debug.Log((object) "Ordered by averageDirectionDelta");
      List<VelocityTestResult> list1 = source.OrderBy<VelocityTestResult, float>((Func<VelocityTestResult, float>) (x => x.averageDirectionDelta)).ToList<VelocityTestResult>();
      for (int index = 0; index < num1; ++index)
        Debug.LogWarning((object) list1[index]);
      Debug.Log((object) "Ordered by median dir");
      List<VelocityTestResult> list2 = list1.OrderBy<VelocityTestResult, float>((Func<VelocityTestResult, float>) (x => x.medianDirectionDelta)).ToList<VelocityTestResult>();
      for (int index = 0; index < num1; ++index)
        Debug.LogWarning((object) list2[index]);
      Debug.Log((object) "Ordered by avg cross median dir");
      List<VelocityTestResult> list3 = list2.OrderBy<VelocityTestResult, float>((Func<VelocityTestResult, float>) (x => x.averageDirectionDelta * x.medianDirectionDelta)).ToList<VelocityTestResult>();
      for (int index = 0; index < num1; ++index)
        Debug.LogWarning((object) list3[index]);
      return list3[0];
    }

    public static VelocityTestResult RunVelocityLerpTest(
      List<CachedThrowData> throwDatas,
      int framesConsidered,
      float lerpVal,
      float minThreshold,
      float endThreshold,
      bool computeMagnSeparately = false,
      int rawDirFrameCount = 4)
    {
      VelocityDataTester.deltas.Clear();
      VelocityDataTester.dirDeltas.Clear();
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      float a = 0.0f;
      float num4 = 0.0f;
      for (int index = 0; index < throwDatas.Count; ++index)
      {
        CachedThrowData throwData = throwDatas[index];
        if (throwData.hasTarget)
        {
          Vector3 vector3_1 = ThrowMath.GetLerpedVector((IReadOnlyList<Vector3>) throwData.velocities, framesConsidered, lerpVal, minThreshold, endThreshold);
          if (computeMagnSeparately)
          {
            float magnitude = ThrowMath.GetAverageVector((IReadOnlyList<Vector3>) throwData.velocities, 9, minThreshold: 2f).magnitude;
            vector3_1 = vector3_1.normalized * magnitude;
          }
          num4 += Vector3.Dot(vector3_1.normalized, throwData.autoAimVector.normalized);
          Vector3 vector3_2 = throwData.autoAimVector - vector3_1;
          num3 += Mathf.Abs(throwData.autoAimVector.magnitude - vector3_1.magnitude);
          num1 += vector3_2.magnitude;
          Vector3 vector3_3 = throwData.autoAimVector.normalized - vector3_1.normalized;
          num2 += vector3_3.magnitude;
          VelocityDataTester.dirDeltas.Add(vector3_3.magnitude);
          VelocityDataTester.deltas.Add(vector3_2.magnitude);
          a = Mathf.Max(a, vector3_2.magnitude);
        }
      }
      if (VelocityDataTester.deltas.Count == 0)
        return (VelocityTestResult) null;
      VelocityDataTester.dirDeltas.Sort();
      float dirDelta = VelocityDataTester.dirDeltas[VelocityDataTester.deltas.Count / 2];
      return new VelocityTestResult()
      {
        frameCount = framesConsidered,
        dotThreshold = endThreshold,
        averageDotProduct = num4 / (float) VelocityDataTester.deltas.Count,
        averageDelta = num1 / (float) VelocityDataTester.deltas.Count,
        averageDirectionDelta = num2 / (float) VelocityDataTester.deltas.Count,
        medianDirectionDelta = dirDelta,
        averageMagnitudeDelta = num3 / (float) VelocityDataTester.deltas.Count,
        maxDelta = a,
        minThreshold = minThreshold,
        lerped = true,
        lerpVal = lerpVal,
        rawDirFrameCount = (float) rawDirFrameCount,
        separateMagnitude = computeMagnSeparately
      };
    }

    public static VelocityTestResult RunVelocityTest(
      List<CachedThrowData> throwDatas,
      int framesConsidered,
      float dotThreshold,
      float minThreshold,
      bool computeMagnitudeSeparately = false,
      int rawDirFrameCount = 4)
    {
      VelocityDataTester.deltas.Clear();
      VelocityDataTester.dirDeltas.Clear();
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      float num4 = 0.0f;
      float a = 0.0f;
      for (int index = 0; index < throwDatas.Count; ++index)
      {
        CachedThrowData throwData = throwDatas[index];
        if (throwData.hasTarget)
        {
          Vector3 vector3_1 = ThrowMath.GetAverageVector((IReadOnlyList<Vector3>) throwData.velocities, framesConsidered, dotThreshold, minThreshold, rawDirFrameCount);
          if (computeMagnitudeSeparately)
          {
            float magnitude = ThrowMath.GetAverageVector((IReadOnlyList<Vector3>) throwData.velocities, 9, minThreshold: 2f).magnitude;
            vector3_1 = vector3_1.normalized * magnitude;
          }
          num4 += Vector3.Dot(vector3_1.normalized, throwData.autoAimVector.normalized);
          Vector3 vector3_2 = throwData.autoAimVector - vector3_1;
          num3 += Mathf.Abs(throwData.autoAimVector.magnitude - vector3_1.magnitude);
          num1 += vector3_2.magnitude;
          Vector3 vector3_3 = throwData.autoAimVector.normalized - vector3_1.normalized;
          num2 += vector3_3.magnitude;
          VelocityDataTester.dirDeltas.Add(vector3_3.magnitude);
          VelocityDataTester.deltas.Add(vector3_2.magnitude);
          a = Mathf.Max(a, vector3_2.magnitude);
        }
      }
      if (VelocityDataTester.deltas.Count == 0)
        return (VelocityTestResult) null;
      VelocityDataTester.dirDeltas.Sort();
      float dirDelta = VelocityDataTester.dirDeltas[VelocityDataTester.deltas.Count / 2];
      return new VelocityTestResult()
      {
        frameCount = framesConsidered,
        dotThreshold = dotThreshold,
        averageDelta = num1 / (float) VelocityDataTester.deltas.Count,
        averageDotProduct = num4 / (float) VelocityDataTester.deltas.Count,
        averageDirectionDelta = num2 / (float) VelocityDataTester.deltas.Count,
        medianDirectionDelta = dirDelta,
        averageMagnitudeDelta = num3 / (float) VelocityDataTester.deltas.Count,
        maxDelta = a,
        minThreshold = minThreshold,
        rawDirFrameCount = (float) rawDirFrameCount,
        separateMagnitude = computeMagnitudeSeparately
      };
    }
  }
}
