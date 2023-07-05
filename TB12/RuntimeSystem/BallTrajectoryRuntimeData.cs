// Decompiled with JetBrains decompiler
// Type: TB12.RuntimeSystem.BallTrajectoryRuntimeData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DataSensitiveStructs_v5;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.RuntimeSystem
{
  [Serializable]
  public class BallTrajectoryRuntimeData : Data
  {
    public Keyframe[] xAxisKeyframes;
    public Keyframe[] zAxisKeyframes;
    public Keyframe[] yAxisKeyframes;
    public EBallFlightType flightType;
    public float startTime;
    public float endTime;
    public int fromPlayerID;
    public int toPlayerID;
    [SerializeField]
    private float _timeLenght;

    public float TimeLength => this._timeLenght;

    public int KeyCount => this.xAxisKeyframes.Length;

    public void Init(BallTrajectoryData storedBallTrajData, PlayRuntimeData runtimeDataManager)
    {
      this.id = storedBallTrajData.id;
      this.flightType = storedBallTrajData.flightType;
      List<Vector2> coordinates = storedBallTrajData.coordinates;
      List<float> playTime = storedBallTrajData.playTime;
      int count = coordinates.Count;
      this.startTime = playTime[0];
      this.endTime = playTime[count - 1];
      this._timeLenght = Mathf.Abs(this.endTime - this.startTime);
      this.fromPlayerID = storedBallTrajData.fromPlayerId;
      this.toPlayerID = storedBallTrajData.toPlayerId;
      this.xAxisKeyframes = new Keyframe[count];
      this.zAxisKeyframes = new Keyframe[count];
      this.yAxisKeyframes = new Keyframe[21];
      for (int index = 0; index < count; ++index)
      {
        Vector2 vector2 = coordinates[index];
        float time = playTime[index];
        this.xAxisKeyframes[index] = new Keyframe(time, vector2.x);
        this.zAxisKeyframes[index] = new Keyframe(time, vector2.y);
      }
      Vector3 vector3_1 = new Vector3(this.xAxisKeyframes[0].value, this.fromPlayerID > 0 ? 1.7f : 0.0f, this.zAxisKeyframes[0].value);
      Vector3 vector3_2 = new Vector3(this.xAxisKeyframes.GetLastKeyFrame().value, this.toPlayerID > 0 ? 1.7f : 0.0f, this.zAxisKeyframes.GetLastKeyFrame().value);
      Vector3 vector3_3 = (vector3_1 + vector3_2) * 0.5f - new Vector3(0.0f, 1f, 0.0f);
      Vector3 a = vector3_1 - vector3_3;
      Vector3 b = vector3_2 - vector3_3;
      float num1 = BallTrajectoryRuntimeData.BallFlightArcCoef(this.flightType);
      float t = 0.0f;
      for (int index = 0; index < 20; ++index)
      {
        float num2 = Mathf.Lerp(vector3_1.y, vector3_2.y, t);
        float num3 = ((Vector3.Slerp(a, b, t) + vector3_3).y - num2) * num1 + num2;
        this.yAxisKeyframes[index] = new Keyframe(this.startTime + this._timeLenght * t, num3);
        t += 0.05f;
      }
      this.yAxisKeyframes[20] = new Keyframe(this.endTime, vector3_2.y);
    }

    public void Deinit()
    {
      this.xAxisKeyframes = (Keyframe[]) null;
      this.zAxisKeyframes = (Keyframe[]) null;
      this.yAxisKeyframes = (Keyframe[]) null;
      this.startTime = 0.0f;
      this.endTime = 0.0f;
      this.fromPlayerID = 0;
      this.toPlayerID = 0;
    }

    public static float BallFlightArcCoef(EBallFlightType flightType) => new Dictionary<EBallFlightType, float>(7)
    {
      {
        EBallFlightType.None,
        0.5f
      },
      {
        EBallFlightType.FreeFlight,
        0.5f
      },
      {
        EBallFlightType.PassBullet,
        0.2f
      },
      {
        EBallFlightType.PassRegular,
        0.4f
      },
      {
        EBallFlightType.PassLob,
        0.6f
      },
      {
        EBallFlightType.PassPitch,
        0.3f
      },
      {
        EBallFlightType.PassShotgun,
        0.15f
      }
    }[flightType];
  }
}
