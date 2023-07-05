// Decompiled with JetBrains decompiler
// Type: TB12.RuntimeSystem.BallRuntimeData
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
  public class BallRuntimeData : Data
  {
    private PlayRuntimeData _runtimeDataManager;
    [SerializeField]
    private int _firstTrajectoryID;
    [SerializeField]
    private Queue<int> _trajectoriesRuntimeIDs = new Queue<int>();
    [SerializeField]
    private AnimationCurve _xAxisAnimationCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _yAxisAnimationCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _zAxisAnimationCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _movementStateCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _ballPossessionCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _xQuatAnimationCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _yQuatAnimationCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _zQuatAnimationCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _wQuatAnimationCurve = new AnimationCurve();
    [SerializeField]
    private Vector3 _ballInitialFieldPos;
    [SerializeField]
    private Vector2 _timeBounds;
    [SerializeField]
    private List<int> _ballPossessingPlayers = new List<int>();
    private Keyframe[] _ballPossessionCurveKeyframes;

    public float MovementStartTime => this._timeBounds.x;

    public float MovementEndTime => this._timeBounds.y;

    public Queue<int> TrajectoryQueue => this._trajectoriesRuntimeIDs;

    public Vector3 BallInitialFieldPos => this._ballInitialFieldPos;

    public List<int> PlayerPossessionList => this._ballPossessingPlayers;

    public Keyframe[] BallPossessionCurveKeyframes => this._ballPossessionCurveKeyframes;

    public float EvaluateTimeSinceLastPosessionChange(float time)
    {
      float playerInPossession = (float) this.EvaluatePlayerInPossession(time);
      Keyframe keyframe = new Keyframe(float.NegativeInfinity, -1f);
      for (int index = 0; index < this._ballPossessionCurveKeyframes.Length; ++index)
      {
        if ((double) this._ballPossessionCurveKeyframes[index].value != (double) playerInPossession && (double) this._ballPossessionCurveKeyframes[index].time > (double) keyframe.time && (double) this._ballPossessionCurveKeyframes[index].time < (double) time)
          keyframe = this._ballPossessionCurveKeyframes[index];
      }
      return !Mathf.Approximately(keyframe.value, float.NegativeInfinity) ? time - keyframe.time : Mathf.Abs(keyframe.value);
    }

    public float EvaluateTimeToNextPosessionChange(float time)
    {
      float playerInPossession = (float) this.EvaluatePlayerInPossession(time);
      Keyframe keyframe = new Keyframe(float.PositiveInfinity, -1f);
      for (int index = 0; index < this._ballPossessionCurveKeyframes.Length; ++index)
      {
        if ((double) this._ballPossessionCurveKeyframes[index].value != (double) playerInPossession && (double) this._ballPossessionCurveKeyframes[index].time < (double) keyframe.time && (double) this._ballPossessionCurveKeyframes[index].time > (double) time)
          keyframe = this._ballPossessionCurveKeyframes[index];
      }
      return !Mathf.Approximately(keyframe.value, float.PositiveInfinity) ? keyframe.time - time : keyframe.value;
    }

    public float EvaluateTimeToNextCatch(int playerID, float time)
    {
      for (int index = 0; index < this._ballPossessionCurveKeyframes.Length; ++index)
      {
        if ((double) this._ballPossessionCurveKeyframes[index].time > (double) time && (double) this._ballPossessionCurveKeyframes[index].value == (double) playerID)
          return this._ballPossessionCurveKeyframes[index].time - time;
      }
      return float.PositiveInfinity;
    }

    public float EvaluateTimeToNextCatchByAnyone(float time)
    {
      for (int index = 0; index < this._ballPossessionCurveKeyframes.Length; ++index)
      {
        if ((double) this._ballPossessionCurveKeyframes[index].time > (double) time && (double) this._ballPossessionCurveKeyframes[index].value != 0.0)
          return this._ballPossessionCurveKeyframes[index].time - time;
      }
      return float.PositiveInfinity;
    }

    public float EvaluateTimeSincePrevCatch(int playerID, float time)
    {
      for (int index = this._ballPossessionCurveKeyframes.Length - 1; index >= 0; --index)
      {
        if ((double) this._ballPossessionCurveKeyframes[index].time < (double) time && (double) this._ballPossessionCurveKeyframes[index].value == (double) playerID)
          return time - this._ballPossessionCurveKeyframes[index].time;
      }
      return float.PositiveInfinity;
    }

    public float EvaluateTimeToNextThrow(int playerID, float time)
    {
      int playerInPossession = this.EvaluatePlayerInPossession(time);
      for (int index = 0; index < this._ballPossessionCurveKeyframes.Length; ++index)
      {
        if ((double) this._ballPossessionCurveKeyframes[index].time > (double) time && (double) this._ballPossessionCurveKeyframes[index].value != (double) playerID && playerInPossession == playerID)
          return this._ballPossessionCurveKeyframes[index].time - time;
        playerInPossession = this.EvaluatePlayerInPossession(this._ballPossessionCurveKeyframes[index].time);
      }
      return float.PositiveInfinity;
    }

    public float EvaluateTimeSincePrevThrow(int playerID, float time)
    {
      for (int index = this._ballPossessionCurveKeyframes.Length - 1; index >= 0; --index)
      {
        if ((double) this._ballPossessionCurveKeyframes[index].time < (double) time && (double) this._ballPossessionCurveKeyframes[index].value != (double) playerID && this.EvaluatePlayerInPossession(this._ballPossessionCurveKeyframes[index].time - 0.01f) == playerID)
          return time - this._ballPossessionCurveKeyframes[index].time;
      }
      return float.PositiveInfinity;
    }

    public Vector3 Evaluate2DPositionYardsXZ(float time) => new Vector3(this._xAxisAnimationCurve.Evaluate(time), 0.0f, this._zAxisAnimationCurve.Evaluate(time));

    public Vector3 Evaluate2DPositionMetersXZ(float time) => this.Evaluate2DPositionYardsXZ(time).YardsToMeters();

    public Vector2 Evaluate2DPositionYards(float time) => new Vector2(this._xAxisAnimationCurve.Evaluate(time), this._zAxisAnimationCurve.Evaluate(time));

    public Vector2 Evaluate2DPositionMeters(float time) => this.Evaluate2DPositionYards(time).YardsToMeters();

    public int EvaluatePlayerInPossession(float time) => (int) this._ballPossessionCurve.Evaluate(time);

    public Vector3 Evaluate3DPositionYards(float time) => new Vector3(this._xAxisAnimationCurve.Evaluate(time), this._yAxisAnimationCurve.Evaluate(time), this._zAxisAnimationCurve.Evaluate(time));

    public Vector3 Evaluate3DPositionMeters(float time) => this.Evaluate3DPositionYards(time).YardsToMeters();

    public float EvaluateOrientation(float time) => this.Evaluate3DEulerRot(time).y;

    public Vector3 Evaluate3DEulerRot(float time) => this.Evaluate3DQuatRot(time).eulerAngles;

    public Quaternion Evaluate3DQuatRot(float time) => new Quaternion(this._xQuatAnimationCurve.Evaluate(time), this._yQuatAnimationCurve.Evaluate(time), this._zQuatAnimationCurve.Evaluate(time), this._wQuatAnimationCurve.Evaluate(time));

    public void Init(BallData ballData, PlayRuntimeData runtimeDataManager)
    {
      this._runtimeDataManager = runtimeDataManager;
      this._ballInitialFieldPos = MathUtils.TransformDataCoordinatesToScenePosition(ballData.placement);
      this._firstTrajectoryID = ballData.ballTrajId;
      if (this._firstTrajectoryID != 0)
      {
        this._trajectoriesRuntimeIDs = this._runtimeDataManager.GetBallTrajectoriesChainStartingWith(this._firstTrajectoryID);
        this._timeBounds = this.ConstructMergedCurves();
        this.ConstructStateCurve();
        this.ConstructRotationCurves();
        this.GeneratePossessionInfo();
      }
      else
        this.InitDefaults();
    }

    private void InitDefaults()
    {
      Keyframe keyframe1 = new Keyframe(0.0f, this._ballInitialFieldPos.x);
      Keyframe keyframe2 = new Keyframe(0.0f, this._ballInitialFieldPos.y);
      Keyframe keyframe3 = new Keyframe(0.0f, this._ballInitialFieldPos.z);
      Keyframe keyframe4 = new Keyframe(0.0f, 0.0f);
      Keyframe keyframe5 = new Keyframe(0.0f, 0.0f);
      this._xAxisAnimationCurve = new AnimationCurve(new Keyframe[1]
      {
        keyframe1
      });
      this._xAxisAnimationCurve.preWrapMode = WrapMode.ClampForever;
      this._xAxisAnimationCurve.postWrapMode = WrapMode.ClampForever;
      this._yAxisAnimationCurve = new AnimationCurve(new Keyframe[1]
      {
        keyframe2
      });
      this._yAxisAnimationCurve.preWrapMode = WrapMode.ClampForever;
      this._yAxisAnimationCurve.postWrapMode = WrapMode.ClampForever;
      this._zAxisAnimationCurve = new AnimationCurve(new Keyframe[1]
      {
        keyframe3
      });
      this._zAxisAnimationCurve.preWrapMode = WrapMode.ClampForever;
      this._zAxisAnimationCurve.postWrapMode = WrapMode.ClampForever;
      this._xQuatAnimationCurve = new AnimationCurve(new Keyframe[1]
      {
        new Keyframe(0.0f, 0.0f)
      });
      this._yQuatAnimationCurve = new AnimationCurve(new Keyframe[1]
      {
        new Keyframe(0.0f, 0.0f)
      });
      this._zQuatAnimationCurve = new AnimationCurve(new Keyframe[1]
      {
        new Keyframe(0.0f, 0.0f)
      });
      this._wQuatAnimationCurve = new AnimationCurve(new Keyframe[1]
      {
        new Keyframe(0.0f, 0.0f)
      });
      this._movementStateCurve = new AnimationCurve(new Keyframe[1]
      {
        keyframe4
      });
      this._ballPossessionCurve = new AnimationCurve(new Keyframe[1]
      {
        keyframe5
      });
      this._ballPossessionCurveKeyframes = new Keyframe[0];
      this._timeBounds = new Vector2(0.0f, 0.0f);
      this._ballPossessingPlayers = new List<int>();
    }

    public void Deinit()
    {
      this._trajectoriesRuntimeIDs.Clear();
      this._runtimeDataManager = (PlayRuntimeData) null;
      this._xAxisAnimationCurve = (AnimationCurve) null;
      this._zAxisAnimationCurve = (AnimationCurve) null;
      this._yAxisAnimationCurve = (AnimationCurve) null;
      this._movementStateCurve = (AnimationCurve) null;
      this._ballPossessionCurve = (AnimationCurve) null;
      this._ballPossessionCurveKeyframes = (Keyframe[]) null;
      this._ballPossessingPlayers = (List<int>) null;
    }

    public int GetCurrentOrNextTrajectoryId(float playTime)
    {
      int nextTrajectoryId = 0;
      if (this._trajectoriesRuntimeIDs.Count > 0)
      {
        int[] array = this._trajectoriesRuntimeIDs.ToArray();
        int length = array.Length;
        for (int index = 0; index < length; ++index)
        {
          BallTrajectoryRuntimeData trajectoryRuntimeData = this._runtimeDataManager.RetrieveBallTrajectoryData(array[index]);
          if ((double) playTime >= (double) trajectoryRuntimeData.startTime && (double) playTime <= (double) trajectoryRuntimeData.endTime || (double) playTime < (double) trajectoryRuntimeData.startTime)
          {
            nextTrajectoryId = array[index];
            break;
          }
        }
      }
      return nextTrajectoryId;
    }

    public int GetPreviousTrajectoryId(float playTime)
    {
      int previousTrajectoryId = 0;
      if (this._trajectoriesRuntimeIDs.Count > 0)
      {
        int[] array = this._trajectoriesRuntimeIDs.ToArray();
        int length = array.Length;
        for (int index = 0; index < length; ++index)
        {
          int id = array[index];
          BallTrajectoryRuntimeData trajectoryRuntimeData = this._runtimeDataManager.RetrieveBallTrajectoryData(id);
          if ((double) playTime > (double) trajectoryRuntimeData.endTime)
            previousTrajectoryId = id;
          else
            break;
        }
      }
      return previousTrajectoryId;
    }

    private void RemoveDataHandler(int dataKey)
    {
      if (!this._trajectoriesRuntimeIDs.Contains(dataKey))
        return;
      Queue<int> intQueue = new Queue<int>();
      int[] array = this._trajectoriesRuntimeIDs.ToArray();
      int index1 = 0;
      for (int index2 = array[index1]; index2 != dataKey; index2 = array[index1])
      {
        intQueue.Enqueue(index2);
        ++index1;
      }
      this._trajectoriesRuntimeIDs = intQueue;
      if (this._trajectoriesRuntimeIDs.Count > 0)
      {
        this._timeBounds = this.ConstructMergedCurves();
        this.ConstructStateCurve();
        this.ConstructRotationCurves();
        this.GeneratePossessionInfo();
      }
      else
        this.InitDefaults();
    }

    private void UpdateDataHandler(int dataKey)
    {
      if (this._trajectoriesRuntimeIDs.Count <= 0)
        return;
      this._trajectoriesRuntimeIDs = this._runtimeDataManager.GetBallTrajectoriesChainStartingWith(this._trajectoriesRuntimeIDs.Peek());
      if (!this._trajectoriesRuntimeIDs.Contains(dataKey))
        return;
      this._timeBounds = this.ConstructMergedCurves();
      this.ConstructStateCurve();
      this.ConstructRotationCurves();
      this.GeneratePossessionInfo();
    }

    private void GeneratePossessionInfo()
    {
      int[] array = this._trajectoriesRuntimeIDs.ToArray();
      int length = array.Length;
      BallTrajectoryRuntimeData[] trajectoryRuntimeDataArray = new BallTrajectoryRuntimeData[length];
      for (int index = 0; index < length; ++index)
        trajectoryRuntimeDataArray[index] = this._runtimeDataManager.RetrieveBallTrajectoryData(array[index]);
      List<Keyframe> keyframeList = new List<Keyframe>();
      this._ballPossessingPlayers = new List<int>();
      for (int index = 0; index < length; ++index)
      {
        BallTrajectoryRuntimeData trajectoryRuntimeData = trajectoryRuntimeDataArray[index];
        keyframeList.Add(new Keyframe(trajectoryRuntimeData.startTime, 0.0f));
        keyframeList.Add(new Keyframe(trajectoryRuntimeData.endTime, (float) trajectoryRuntimeData.toPlayerID));
        if (trajectoryRuntimeData.fromPlayerID != 0 && !this._ballPossessingPlayers.Contains(trajectoryRuntimeData.fromPlayerID))
          this._ballPossessingPlayers.Add(trajectoryRuntimeData.fromPlayerID);
        if (trajectoryRuntimeData.toPlayerID != 0 && !this._ballPossessingPlayers.Contains(trajectoryRuntimeData.toPlayerID))
          this._ballPossessingPlayers.Add(trajectoryRuntimeData.toPlayerID);
      }
      this._ballPossessionCurve = KeyframeUtils.CalculateConstantKeyframeTangents(keyframeList.ToArray());
      this._ballPossessionCurveKeyframes = this._ballPossessionCurve.keys;
    }

    private Vector2 ConstructMergedCurves()
    {
      int[] array = this._trajectoriesRuntimeIDs.ToArray();
      int length = array.Length;
      BallTrajectoryRuntimeData[] trajectoryRuntimeDataArray = new BallTrajectoryRuntimeData[length];
      for (int index = 0; index < length; ++index)
        trajectoryRuntimeDataArray[index] = this._runtimeDataManager.RetrieveBallTrajectoryData(array[index]);
      List<Keyframe[]> keyframeArraysList1 = new List<Keyframe[]>();
      List<Keyframe[]> keyframeArraysList2 = new List<Keyframe[]>();
      List<Keyframe[]> keyframeArraysList3 = new List<Keyframe[]>();
      for (int index = 0; index < length; ++index)
      {
        BallTrajectoryRuntimeData trajectoryRuntimeData = trajectoryRuntimeDataArray[index];
        keyframeArraysList1.Add(trajectoryRuntimeData.xAxisKeyframes);
        keyframeArraysList2.Add(trajectoryRuntimeData.zAxisKeyframes);
        keyframeArraysList3.Add(trajectoryRuntimeData.yAxisKeyframes);
      }
      Keyframe[] keys1 = KeyframeUtils.MergeKeyframeArrays(keyframeArraysList1);
      Keyframe[] keys2 = KeyframeUtils.MergeKeyframeArrays(keyframeArraysList2);
      Keyframe[] keys3 = KeyframeUtils.MergeKeyframeArrays(keyframeArraysList3);
      this._xAxisAnimationCurve = KeyframeUtils.CalculateLinearKeyframeTangents(keys1);
      this._xAxisAnimationCurve.preWrapMode = WrapMode.ClampForever;
      this._xAxisAnimationCurve.postWrapMode = WrapMode.ClampForever;
      this._zAxisAnimationCurve = KeyframeUtils.CalculateLinearKeyframeTangents(keys2);
      this._zAxisAnimationCurve.preWrapMode = WrapMode.ClampForever;
      this._zAxisAnimationCurve.postWrapMode = WrapMode.ClampForever;
      this._yAxisAnimationCurve = KeyframeUtils.CalculateLinearKeyframeTangents(keys3);
      this._yAxisAnimationCurve.preWrapMode = WrapMode.ClampForever;
      this._yAxisAnimationCurve.postWrapMode = WrapMode.ClampForever;
      return new Vector2(this._xAxisAnimationCurve.keys[0].time, this._xAxisAnimationCurve.keys.GetLastKeyFrame().time);
    }

    private void ConstructStateCurve()
    {
      float step = 0.016f;
      float time = this.MovementStartTime - step;
      float endValue = this.MovementEndTime + step;
      Vector3 v2 = this.Evaluate2DPositionYardsXZ(time);
      float movementStartTime = this.MovementStartTime;
      int index1 = MathUtils.CountIterationsFromTo(movementStartTime, endValue, step);
      Keyframe[] keys = new Keyframe[index1 + 1];
      for (int index2 = 0; index2 < index1; ++index2)
      {
        Vector3 v1 = this.Evaluate2DPositionYardsXZ(movementStartTime);
        EObjectMovementState eobjectMovementState = Utilities.Vector3Approx(v1, v2) ? EObjectMovementState.Stationary : EObjectMovementState.Moving;
        keys[index2] = new Keyframe(movementStartTime, (float) eobjectMovementState);
        v2 = v1;
        movementStartTime += step;
      }
      EObjectMovementState eobjectMovementState1 = Utilities.Vector3Approx(this.Evaluate2DPositionYardsXZ(movementStartTime), v2) ? EObjectMovementState.Stationary : EObjectMovementState.Moving;
      keys[index1] = new Keyframe(movementStartTime, (float) eobjectMovementState1);
      this._movementStateCurve = new AnimationCurve()
      {
        keys = keys
      };
      this._movementStateCurve = KeyframeUtils.CalculateConstantKeyframeTangents(keys);
    }

    private void ConstructRotationCurves()
    {
      float step = 0.1f;
      int length = MathUtils.CountIterationsFromTo(this._timeBounds.x, this._timeBounds.y, step);
      Keyframe[] keyframeArray1 = new Keyframe[length];
      Keyframe[] keyframeArray2 = new Keyframe[length];
      Keyframe[] keyframeArray3 = new Keyframe[length];
      Keyframe[] keyframeArray4 = new Keyframe[length];
      Vector3 vector3_1 = this.Evaluate3DPositionMeters(this._timeBounds.x - 0.1f);
      float x = this._timeBounds.x;
      for (int index = 0; index < length; ++index)
      {
        Vector3 vector3_2 = this.Evaluate3DPositionMeters(x + step);
        Vector3 vector3_3 = vector3_2 - vector3_1;
        Quaternion quaternion = Quaternion.identity;
        if (!Mathf.Approximately(vector3_3.magnitude, 0.0f))
          quaternion = Quaternion.LookRotation(vector3_3.normalized);
        keyframeArray1[index] = new Keyframe(x, quaternion.x);
        keyframeArray2[index] = new Keyframe(x, quaternion.y);
        keyframeArray3[index] = new Keyframe(x, quaternion.z);
        keyframeArray4[index] = new Keyframe(x, quaternion.w);
        vector3_1 = vector3_2;
        x += step;
      }
      this._xQuatAnimationCurve = new AnimationCurve(keyframeArray1);
      this._yQuatAnimationCurve = new AnimationCurve(keyframeArray2);
      this._zQuatAnimationCurve = new AnimationCurve(keyframeArray3);
      this._wQuatAnimationCurve = new AnimationCurve(keyframeArray4);
    }
  }
}
