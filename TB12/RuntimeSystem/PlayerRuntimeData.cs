// Decompiled with JetBrains decompiler
// Type: TB12.RuntimeSystem.PlayerRuntimeData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DataSensitiveStructs_v5;
using DDL;
using DSE;
using FootballVR;
using ProjectConstants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.RuntimeSystem
{
  [Serializable]
  public class PlayerRuntimeData : Data
  {
    [SerializeField]
    private int _firstRouteID;
    [SerializeField]
    private Vector2 _playerFieldPos;
    [SerializeField]
    private Vector2 _timeBounds;
    [SerializeField]
    private float _defaultOrientation;
    [SerializeField]
    private DataSensitiveStructs_v5.EPlayerRole _role;
    [SerializeField]
    private EPlayerStance _defaultStance;
    [SerializeField]
    private Queue<int> _routesRuntimeIDs = new Queue<int>();
    [SerializeField]
    private List<PlayerRuntimeData.RouteTimeStamps> _routesTimeStamps = new List<PlayerRuntimeData.RouteTimeStamps>();
    [SerializeField]
    private PositionalCurves _positionalCurves = new PositionalCurves();
    [SerializeField]
    private AnimationCurve _movementTypeCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _orientationCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _stanceCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _speedCurve = new AnimationCurve();
    [SerializeField]
    private AnimationCurve _angularSpeedCurve = new AnimationCurve();
    private PlayRuntimeData _playData;
    private RouteRuntimeData _huddleMovement;
    private float _samplingTimeStep = 0.05f;
    private bool _isInit;
    public const float SegmentLengthThreashold = 20f;
    public const float AccelerationDist = 5f;
    public const float DecelerationDist = 5f;
    public const float AccAndDecRatePerSecond = 5f;

    private PositionalCurves PositionCurves => this._positionalCurves;

    public PlayerScenario GenerateScenario()
    {
      PlayerScenario instance = ScriptableObject.CreateInstance<PlayerScenario>();
      instance.name = "PlayerRuntimeData_" + this.id.ToString();
      instance.reinit = false;
      instance.position = PositionalCurves.Scale(this._positionalCurves, 0.914411068f);
      instance.orientation = this._orientationCurve;
      instance.stance = this._stanceCurve;
      instance.legacyMovementType = this._movementTypeCurve;
      instance.startPosition = (Vector2) Vector3.zero;
      this.GeneratePossessionCurve(instance);
      instance.PostProcessCurves();
      return instance;
    }

    public void GeneratePossessionCurve(PlayerScenario scenario)
    {
      Keyframe keyframe1 = new Keyframe(-30f, 0.0f);
      scenario.possession.AddKey(keyframe1);
      scenario.possessionAnimationKeys.Add(new SerializedKeyframe(keyframe1));
      BallRuntimeData ballRuntimeData = this._playData.GetBallRuntimeData();
      bool flag = false;
      if (!ballRuntimeData.PlayerPossessionList.Contains(this.id))
        return;
      for (int index = 0; index < ballRuntimeData.BallPossessionCurveKeyframes.Length; ++index)
      {
        if (flag && Mathf.Approximately(ballRuntimeData.BallPossessionCurveKeyframes[index].value, 0.0f))
        {
          flag = false;
          EventData eventData = new EventData()
          {
            time = ballRuntimeData.BallPossessionCurveKeyframes[index].time
          };
          eventData.position = ballRuntimeData.Evaluate3DPositionMeters(eventData.time);
          Vector3 vector3 = ballRuntimeData.Evaluate3DPositionMeters(eventData.time + 0.2f) - eventData.position;
          Vector2 from = new Vector2(vector3.x, vector3.z);
          Vector2 to = new Vector2(Vector3.forward.x, Vector3.forward.z);
          eventData.orientation = Vector2.SignedAngle(from, to);
          Keyframe keyframe2 = new Keyframe(eventData.time, 0.0f);
          scenario.possession.AddKey(keyframe2);
          scenario.possessionAnimationKeys.Add(new SerializedKeyframe(keyframe2));
          scenario.eventList.Add(eventData);
        }
        else if (Mathf.Approximately(ballRuntimeData.BallPossessionCurveKeyframes[index].value, (float) this.id))
        {
          flag = true;
          EventData eventData = new EventData()
          {
            time = ballRuntimeData.BallPossessionCurveKeyframes[index].time
          };
          eventData.position = ballRuntimeData.Evaluate3DPositionMeters(eventData.time);
          Vector3 vector3 = ballRuntimeData.Evaluate3DPositionMeters(eventData.time - 0.2f) - eventData.position;
          Vector2 from = new Vector2(vector3.x, vector3.z);
          Vector2 to = new Vector2(Vector3.forward.x, Vector3.forward.z);
          eventData.orientation = Vector2.SignedAngle(from, to);
          Keyframe keyframe3 = new Keyframe(eventData.time, 1f);
          scenario.possession.AddKey(keyframe3);
          scenario.possessionAnimationKeys.Add(new SerializedKeyframe(keyframe3));
          scenario.eventList.Add(eventData);
        }
      }
    }

    public void Init(DataSensitiveStructs_v5.PlayerData playerData, PlayRuntimeData playRuntimeData)
    {
      this.id = playerData.id;
      this._playerFieldPos = playerData.placement;
      this._defaultOrientation = playerData.playerOrientation;
      this._role = playerData.role;
      this._defaultStance = playerData.stance;
      this._firstRouteID = playerData.routeId;
      this._huddleMovement = (RouteRuntimeData) null;
      this._playData = playRuntimeData;
      if (!this._isInit)
        this._isInit = true;
      this.GenerateBaseData();
    }

    public void Deinit()
    {
      this._routesRuntimeIDs.Clear();
      this._playData = (PlayRuntimeData) null;
      this._positionalCurves = (PositionalCurves) null;
      this._stanceCurve = (AnimationCurve) null;
      this._orientationCurve = (AnimationCurve) null;
      this._speedCurve = (AnimationCurve) null;
      this._angularSpeedCurve = (AnimationCurve) null;
      this._huddleMovement = (RouteRuntimeData) null;
      this._isInit = false;
    }

    public void OnPlayTimeBoundsUpdated()
    {
      this.ProcessHuddle();
      this.GenerateDerivedData();
    }

    private void GenerateBaseData()
    {
      this._routesRuntimeIDs = this._playData.GetRoutesChainStartingWith(this._firstRouteID);
      this._timeBounds = this.ConstructMergedCurves(this._playerFieldPos);
    }

    private void GenerateDerivedData()
    {
      this.DeriveSpeedCurve();
      this.DeriveStateCurve();
      this.DeriveStancesCurve();
      this.DeriveOrientationCurve();
      this.DeriveAngularSpeedCurve();
    }

    public void OnPostProcess() => this.OverrideOrientation();

    private void OverrideOrientation()
    {
      PlayMeta.PlayerInteractionData data;
      if ((UnityEngine.Object) LocalPlayLoader.LoadedPlayMeta == (UnityEngine.Object) null || !LocalPlayLoader.LoadedPlayMeta.TryGetOrientationOverride(this.id, out data))
        return;
      if ((double) this._orientationCurve.keys[0].time > (double) data.startInteractTime)
        this._orientationCurve.AddKey(data.startInteractTime - 1f, this._orientationCurve.Evaluate(data.startInteractTime));
      for (int index = 0; index < this._orientationCurve.length; ++index)
      {
        if ((double) this._orientationCurve.keys[index].time >= (double) data.startInteractTime && (double) this._orientationCurve.keys[index].time <= (double) data.endInteractTime)
        {
          this._orientationCurve.RemoveKey(index);
          --index;
        }
      }
      PlayerRuntimeData playerRuntimeData = this._playData.RetrievePlayerData(data.targetPlayerId);
      float num1 = 0.1f;
      for (float startInteractTime = data.startInteractTime; (double) startInteractTime < (double) data.endInteractTime; startInteractTime += num1)
      {
        Vector3 vector3 = this.Evaluate3DPositionYards(startInteractTime);
        float num2 = Vector3.SignedAngle(Vector3.forward, playerRuntimeData.Evaluate3DPositionYards(startInteractTime + data.evalTimeOffset) - vector3, Vector3.up);
        this._orientationCurve.AddKey(startInteractTime, num2);
      }
      List<Keyframe> keyframeList = new List<Keyframe>((IEnumerable<Keyframe>) this._orientationCurve.keys);
      PlayerRuntimeData.AdjustOrientationValuesBasedOnShortestAngles(keyframeList);
      this._orientationCurve = KeyframeUtils.CalculateConstantKeyframeTangents(keyframeList);
    }

    public bool IsInited => this._isInit;

    public float HuddleMovementDuration
    {
      get
      {
        float num1 = this._huddleMovement != null ? this._huddleMovement.endTime : 0.0f;
        double num2 = this._huddleMovement != null ? (double) this._huddleMovement.startTime : 0.0;
        float preSnapStartTime = this._playData.PreSnapStartTime;
        float num3 = this._huddleMovement != null ? Mathf.Abs(preSnapStartTime - num1) : 0.0f;
        double num4 = (double) num1;
        return Mathf.Abs((float) (num2 - num4)) + num3;
      }
    }

    public bool IsInHuddle(float playTime) => this._playData.IsInHuddle(playTime);

    public float MovementStartTime => this._timeBounds.x;

    public float MovementEndTime => this._timeBounds.y;

    public Queue<int> RoutesQueue => this._routesRuntimeIDs;

    public Vector3 Evaluate3DPositionYards(float time) => this._positionalCurves == null ? Vector3.zero : MathUtils.TransformDataCoordinatesToScenePosition(this._positionalCurves.EvaluatePosition(time));

    public Vector3 Evaluate3DPositionMeters(float time) => this.Evaluate3DPositionYards(time).YardsToMeters();

    public Vector3 Evaluate2DPositionYardsXZ(float time) => this.Evaluate3DPositionYards(time);

    public Vector3 Evaluate2DPositionMetersXZ(float time) => this.Evaluate3DPositionMeters(time);

    public Vector2 Evaluate2DPositionYards(float time) => this._positionalCurves.EvaluatePosition(time);

    public Vector2 Evaluate2DPositionMeters(float time) => this._positionalCurves.EvaluatePosition(time);

    public EObjectMovementState EvaluateMovementState(float time) => !Mathf.Approximately(this._speedCurve.Evaluate(time), 0.0f) ? EObjectMovementState.Moving : EObjectMovementState.Stationary;

    public EPlayerStance EvaluateStance(float time) => (EPlayerStance) this._stanceCurve.Evaluate(time);

    public float EvaluateSpeed(float time) => this._speedCurve.Evaluate(time);

    public float EvaluateAngularSpeed(float time) => this._angularSpeedCurve.Evaluate(time);

    public EMovementType EvaluateMovementType(float time) => (EMovementType) this._movementTypeCurve.Evaluate(time);

    public float EvaluateOrientation(float time) => this._orientationCurve.Evaluate(time);

    public float EvaluateTimeToNextStance(float time, out EPlayerStance stance)
    {
      Keyframe[] keys = this._stanceCurve.keys;
      for (int index = 0; index < keys.Length; ++index)
      {
        if ((double) time < (double) keys[index].time)
        {
          stance = (EPlayerStance) keys[index].value;
          return keys[index].time - time;
        }
      }
      stance = EPlayerStance.None;
      return -1f;
    }

    public bool ContainsRoute(int routeID) => this._routesRuntimeIDs.Contains(routeID);

    private void ProcessHuddle()
    {
      if (this._huddleMovement != null)
      {
        this.DettachRouteMovement(this._huddleMovement);
        this._huddleMovement = (RouteRuntimeData) null;
      }
      TeamRuntimeData playersTeamData = this.GetPlayersTeamData(this.id);
      if (!PlayRuntimeData.HuddleEnabled)
        return;
      float preSnapStartTime = this._playData.PreSnapStartTime;
      this._huddleMovement = PlayerRuntimeData.GenerateHuddleToFormationMovement(playersTeamData.GetPlayerHuddlePosition(this.id), this._playerFieldPos, preSnapStartTime);
      this.AttachHuddleMovement(this._huddleMovement);
    }

    private static RouteRuntimeData GenerateHuddleToFormationMovement(
      Vector2 inHuddleFieldPos,
      Vector2 inFormationPos,
      float huddleEndPlayTime)
    {
      RouteData storedRouteData = new RouteData();
      storedRouteData.id = 0;
      storedRouteData.speed = PlayEditorConfiguration.Measurements.PlayerSprintingSpeed * 0.75f;
      storedRouteData.movementType = EMovementType.RunForward;
      storedRouteData.routeStage = ERouteStage.PreSnap;
      storedRouteData.space = ESpace.World;
      storedRouteData.nextRouteId = 0;
      storedRouteData.coordinates = new List<Vector2>()
      {
        inHuddleFieldPos,
        inFormationPos
      };
      storedRouteData.timeOffsets = ToolsUtils.CalcRouteTimeStamps(storedRouteData.routeStage, storedRouteData.coordinates, storedRouteData.speed, huddleEndPlayTime - UnityEngine.Random.Range(0.0f, 1f));
      RouteRuntimeData formationMovement = new RouteRuntimeData();
      formationMovement.Init(storedRouteData, 0, (System.Type) null);
      return formationMovement;
    }

    private void DettachRouteMovement(RouteRuntimeData routeRuntime)
    {
      Keyframe[] xAxisKeyframes = routeRuntime.xAxisKeyframes;
      Keyframe[] zAxisKeyframes = routeRuntime.zAxisKeyframes;
      Keyframe[] keys1 = this.PositionCurves.xAxis.keys;
      Keyframe[] keys2 = this.PositionCurves.zAxis.keys;
      Keyframe[] fromKeys = keys1;
      this.SetPositionalCurve(KeyframeUtils.RemoveKeyframeSequence(xAxisKeyframes, fromKeys), KeyframeUtils.RemoveKeyframeSequence(zAxisKeyframes, keys2));
    }

    private void AttachHuddleMovement(RouteRuntimeData huddleToFormationMovement)
    {
      this.SetPositionalCurve(KeyframeUtils.MergeKeyframeArrays(new List<Keyframe[]>()
      {
        huddleToFormationMovement.xAxisKeyframes,
        this.PositionCurves.xAxis.keys
      }), KeyframeUtils.MergeKeyframeArrays(new List<Keyframe[]>()
      {
        huddleToFormationMovement.zAxisKeyframes,
        this.PositionCurves.zAxis.keys
      }));
      this._movementTypeCurve = KeyframeUtils.CalculateConstantKeyframeTangents(KeyframeUtils.MergeKeyframeArrays(new List<Keyframe[]>()
      {
        new Keyframe[2]
        {
          new Keyframe(huddleToFormationMovement.xAxisKeyframes[0].time, (float) huddleToFormationMovement.movementType),
          new Keyframe(huddleToFormationMovement.xAxisKeyframes[1].time, 0.0f)
        },
        this._movementTypeCurve.keys
      }));
    }

    private TeamRuntimeData GetPlayersTeamData(int playerID)
    {
      TeamRuntimeData playersTeamData = this._playData.RetrieveTeamData(ETeamLetters.TeamA);
      if (!playersTeamData.ContainsPlayerWithID(playerID))
      {
        playersTeamData = this._playData.RetrieveTeamData(ETeamLetters.TeamB);
        if (!playersTeamData.ContainsPlayerWithID(playerID))
          Debug.LogError((object) "Player is not contained in either teams!");
      }
      return playersTeamData;
    }

    private void RemoveDataHandler(int dataKey)
    {
      if (!this._routesRuntimeIDs.Contains(dataKey))
        return;
      Debug.Log((object) ("REMOVAL Curve contains key : " + dataKey.ToString()));
      Queue<int> intQueue = new Queue<int>();
      int[] array = this._routesRuntimeIDs.ToArray();
      int index1 = 0;
      for (int index2 = array[index1]; index2 != dataKey; index2 = array[index1])
      {
        intQueue.Enqueue(index2);
        ++index1;
      }
      this._routesRuntimeIDs = intQueue;
      this._firstRouteID = this._routesRuntimeIDs == null || this._routesRuntimeIDs.Count <= 0 ? 0 : this._routesRuntimeIDs.Peek();
      this.GenerateBaseData();
    }

    private static bool IsKickoffPlay
    {
      get
      {
        if (PlayRuntimeData.PlayName == null)
          return false;
        string lower = PlayRuntimeData.PlayName.ToLower();
        return lower.Contains("kickoff") || lower.Contains("kick off") || lower.Contains("kor") || SerializedDataManager.GetPlayType() == EPlayType.Kickoff;
      }
    }

    private static bool IsNgsPlay => SerializedDataManager.GetPlaySource() == EPlaySource.kNgs;

    private Vector2 ConstructMergedCurves(Vector2 offset)
    {
      int[] array = this._routesRuntimeIDs.ToArray();
      int length = array.Length;
      this._routesTimeStamps = new List<PlayerRuntimeData.RouteTimeStamps>();
      List<Keyframe[]> keyframeArraysList1 = new List<Keyframe[]>();
      List<Keyframe[]> keyframeArraysList2 = new List<Keyframe[]>();
      List<Keyframe> keys = new List<Keyframe>();
      float valueOffset1 = offset.x;
      float valueOffset2 = offset.y;
      if (length == 0)
      {
        keyframeArraysList1.Add(new Keyframe[1]
        {
          new Keyframe(0.0f, offset.x)
        });
        keyframeArraysList2.Add(new Keyframe[1]
        {
          new Keyframe(0.0f, offset.y)
        });
        keys.Add(new Keyframe(0.0f, 0.0f));
      }
      RouteRuntimeData[] routeRuntimeDataArray = new RouteRuntimeData[length];
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < length; ++index)
      {
        routeRuntimeDataArray[index] = this._playData.RetrieveRouteData(array[index]);
        if (routeRuntimeDataArray[index].stage == ERouteStage.PreSnap)
          ++num1;
        else
          ++num2;
      }
      float timeOffset1 = PlayerRuntimeData.IsKickoffPlay || PlayerRuntimeData.IsNgsPlay ? 0.0f : PlayEditorConfiguration.Whiteboard.Routes.PreSnapMovementFinishTime;
      for (int index = 0; index < num1; ++index)
      {
        float startTime = routeRuntimeDataArray[index].startTime;
        timeOffset1 += startTime;
      }
      float x = timeOffset1;
      Keyframe keyframe1 = new Keyframe(x - 1f, 0.0f);
      keys.Add(keyframe1);
      Keyframe lastKeyFrame;
      for (int index = 0; index < num1; ++index)
      {
        RouteRuntimeData routeRuntimeData = routeRuntimeDataArray[index];
        timeOffset1 += Mathf.Abs(routeRuntimeData.startTime);
        Keyframe[] xAxisKeyframes = routeRuntimeData.xAxisKeyframes;
        Keyframe[] frames = KeyframeUtils.AddKeyframesValueAndTimeOffset(xAxisKeyframes, valueOffset1, timeOffset1);
        double num3 = (double) valueOffset1;
        lastKeyFrame = xAxisKeyframes.GetLastKeyFrame();
        double num4 = (double) lastKeyFrame.value;
        valueOffset1 = (float) (num3 + num4);
        keyframeArraysList1.Add(frames);
        Keyframe[] zAxisKeyframes = routeRuntimeData.zAxisKeyframes;
        Keyframe[] keyframeArray = KeyframeUtils.AddKeyframesValueAndTimeOffset(zAxisKeyframes, valueOffset2, timeOffset1);
        double num5 = (double) valueOffset2;
        lastKeyFrame = zAxisKeyframes.GetLastKeyFrame();
        double num6 = (double) lastKeyFrame.value;
        valueOffset2 = (float) (num5 + num6);
        keyframeArraysList2.Add(keyframeArray);
        PlayerRuntimeData.RouteTimeStamps routeTimeStamps = new PlayerRuntimeData.RouteTimeStamps();
        routeTimeStamps.routeID = routeRuntimeData.id;
        routeTimeStamps.startTime = frames[0].time;
        ref PlayerRuntimeData.RouteTimeStamps local = ref routeTimeStamps;
        lastKeyFrame = frames.GetLastKeyFrame();
        double time = (double) lastKeyFrame.time;
        local.endTime = (float) time;
        this._routesTimeStamps.Add(routeTimeStamps);
        if (keys.Count > 0)
        {
          Keyframe keyframe2 = keys[keys.Count - 1];
          if ((double) keyframe2.value == 0.0 && Mathf.Approximately(keyframe2.time, routeTimeStamps.startTime))
            keys.RemoveAt(keys.Count - 1);
        }
        Keyframe keyframe3 = new Keyframe(routeTimeStamps.startTime, (float) routeRuntimeData.movementType);
        keys.Add(keyframe3);
        Keyframe keyframe4 = new Keyframe(routeTimeStamps.endTime, 0.0f);
        keys.Add(keyframe4);
      }
      float timeOffset2 = PlayerRuntimeData.IsKickoffPlay || PlayerRuntimeData.IsNgsPlay ? 0.0f : PlayEditorConfiguration.Whiteboard.Routes.PostSnapMovementStartTime;
      for (int index = num1; index < num1 + num2; ++index)
      {
        RouteRuntimeData routeRuntimeData = routeRuntimeDataArray[index];
        Keyframe[] xAxisKeyframes = routeRuntimeData.xAxisKeyframes;
        Keyframe[] frames = KeyframeUtils.AddKeyframesValueAndTimeOffset(xAxisKeyframes, valueOffset1, timeOffset2);
        lastKeyFrame = xAxisKeyframes.GetLastKeyFrame();
        float num7 = lastKeyFrame.value;
        valueOffset1 += num7;
        keyframeArraysList1.Add(frames);
        Keyframe[] zAxisKeyframes = routeRuntimeData.zAxisKeyframes;
        Keyframe[] keyframeArray = KeyframeUtils.AddKeyframesValueAndTimeOffset(zAxisKeyframes, valueOffset2, timeOffset2);
        lastKeyFrame = zAxisKeyframes.GetLastKeyFrame();
        float num8 = lastKeyFrame.value;
        valueOffset2 += num8;
        keyframeArraysList2.Add(keyframeArray);
        PlayerRuntimeData.RouteTimeStamps routeTimeStamps = new PlayerRuntimeData.RouteTimeStamps();
        routeTimeStamps.routeID = routeRuntimeData.id;
        routeTimeStamps.startTime = frames[0].time;
        ref PlayerRuntimeData.RouteTimeStamps local = ref routeTimeStamps;
        lastKeyFrame = frames.GetLastKeyFrame();
        double time = (double) lastKeyFrame.time;
        local.endTime = (float) time;
        this._routesTimeStamps.Add(routeTimeStamps);
        timeOffset2 += routeRuntimeData.TimeLength;
        if (keys.Count > 0)
        {
          Keyframe keyframe5 = keys[keys.Count - 1];
          if ((double) keyframe5.value == 0.0 && Mathf.Approximately(keyframe5.time, routeTimeStamps.startTime))
            keys.RemoveAt(keys.Count - 1);
        }
        Keyframe keyframe6 = new Keyframe(routeTimeStamps.startTime, (float) routeRuntimeData.movementType);
        keys.Add(keyframe6);
        Keyframe keyframe7 = new Keyframe(routeTimeStamps.endTime, 0.0f);
        keys.Add(keyframe7);
      }
      float y = timeOffset2;
      Keyframe[] keyframeArray1 = KeyframeUtils.MergeKeyframeArrays(keyframeArraysList1);
      Keyframe[] keyframeArray2 = KeyframeUtils.MergeKeyframeArrays(keyframeArraysList2);
      if ((PlayerRuntimeData.IsKickoffPlay || PlayerRuntimeData.IsNgsPlay) && this._routesTimeStamps.Count > 0)
      {
        bool flag = false;
        PlayerRuntimeData.RouteTimeStamps routeTimeStamps = new PlayerRuntimeData.RouteTimeStamps();
        routeTimeStamps.routeID = 0;
        routeTimeStamps.endTime = float.NegativeInfinity;
        routeTimeStamps.startTime = float.NegativeInfinity;
        for (int index = 0; index < this._routesTimeStamps.Count; ++index)
        {
          if ((double) this._routesTimeStamps[index].endTime <= 0.0 && (double) this._routesTimeStamps[index].endTime > (double) routeTimeStamps.endTime)
          {
            routeTimeStamps = this._routesTimeStamps[index];
            flag = true;
          }
        }
        if (flag && Mathf.Approximately(SerializedDataManager.RetrieveRouteData(routeTimeStamps.routeID).speed, -1f))
        {
          keyframeArray1 = PlayerRuntimeData.RemoveKeyframeAtTime(new List<Keyframe>((IEnumerable<Keyframe>) keyframeArray1), routeTimeStamps.endTime).ToArray();
          keyframeArray2 = PlayerRuntimeData.RemoveKeyframeAtTime(new List<Keyframe>((IEnumerable<Keyframe>) keyframeArray2), routeTimeStamps.endTime).ToArray();
          keys = new List<Keyframe>((IEnumerable<Keyframe>) PlayerRuntimeData.RemoveKeyframeAtTime(keys, routeTimeStamps.endTime));
        }
      }
      this._movementTypeCurve = KeyframeUtils.CalculateConstantKeyframeTangents(keys);
      this.SetPositionalCurve(PlayerRuntimeData.IsKickoffPlay || PlayerRuntimeData.IsNgsPlay ? KeyframeUtils.CalculateLinearKeyframeTangentsKeys(keyframeArray1) : keyframeArray1, PlayerRuntimeData.IsKickoffPlay || PlayerRuntimeData.IsNgsPlay ? KeyframeUtils.CalculateLinearKeyframeTangentsKeys(keyframeArray2) : keyframeArray2);
      return new Vector2(x, y);
    }

    private static List<Keyframe> RemoveKeyframeAtTime(List<Keyframe> keys, float time)
    {
      for (int index = 0; index < keys.Count; ++index)
      {
        if ((double) keys[index].time < (double) time + 0.10000000149011612 && (double) keys[index].time > (double) time - 0.10000000149011612)
        {
          keys.Remove(keys[index]);
          --index;
        }
      }
      return keys;
    }

    private void DeriveStateCurve()
    {
      float time = this.PositionCurves.StartTime - 1f;
      float endValue = this.PositionCurves.EndTime + 1f;
      Vector3 v2 = this.Evaluate3DPositionYards(time);
      float movementStartTime = this.MovementStartTime;
      int index1 = MathUtils.CountIterationsFromTo(movementStartTime, endValue, this._samplingTimeStep);
      Keyframe[] keyframeArray = new Keyframe[index1 + 1];
      for (int index2 = 0; index2 < index1; ++index2)
      {
        Vector3 v1 = this.Evaluate3DPositionYards(movementStartTime);
        EObjectMovementState eobjectMovementState = Utilities.Vector3Approx(v1, v2) ? EObjectMovementState.Stationary : EObjectMovementState.Moving;
        keyframeArray[index2] = new Keyframe(movementStartTime, (float) eobjectMovementState);
        v2 = v1;
        movementStartTime += this._samplingTimeStep;
      }
      EObjectMovementState eobjectMovementState1 = Utilities.Vector3Approx(this.Evaluate3DPositionYards(movementStartTime), v2) ? EObjectMovementState.Stationary : EObjectMovementState.Moving;
      keyframeArray[index1] = new Keyframe(movementStartTime, (float) eobjectMovementState1);
    }

    private void DeriveStancesCurve()
    {
      List<Keyframe> keys = new List<Keyframe>();
      float time1 = PlayEditorConfiguration.Whiteboard.Routes.PostSnapMovementStartTime + UnityEngine.Random.Range(0.0f, 0.3f);
      float huddleStartTime = this._playData.HuddleStartTime;
      float time2 = this._playData.PreSnapStartTime - (this._huddleMovement == null ? 0.0f : this.HuddleMovementDuration / 2f);
      Keyframe keyframe1 = new Keyframe((double) huddleStartTime < (double) time2 ? huddleStartTime : time2 - 0.1f, 1f);
      keys.Add(keyframe1);
      Keyframe keyframe2 = new Keyframe(time2, (float) this._defaultStance);
      keys.Add(keyframe2);
      for (int index = 0; index < this._routesTimeStamps.Count; ++index)
      {
        PlayerRuntimeData.RouteTimeStamps routesTimeStamp = this._routesTimeStamps[index];
        if ((double) routesTimeStamp.startTime < 0.0)
        {
          Keyframe keyframe3 = new Keyframe(UnityEngine.Random.Range(routesTimeStamp.startTime, routesTimeStamp.endTime), (float) this.PostPresnapShiftStance(routesTimeStamp.endTime));
          keys.Add(keyframe3);
        }
      }
      this._stanceCurve = KeyframeUtils.CalculateConstantKeyframeTangents(keys);
      Keyframe keyframe4 = new Keyframe(time1, (float) this.GameInProgressStance(this._role));
      keys.Add(keyframe4);
      for (int index = 0; index < this._routesTimeStamps.Count; ++index)
      {
        PlayerRuntimeData.RouteTimeStamps routesTimeStamp = this._routesTimeStamps[index];
        RouteData routeData = SerializedDataManager.RetrieveRouteData(this._routesTimeStamps[index].routeID);
        ERouteType routeType = routeData.routeType;
        EMovementType movementType = routeData.movementType;
        if ((double) routesTimeStamp.endTime > 0.0)
        {
          Keyframe keyframe5 = new Keyframe(UnityEngine.Random.Range(routesTimeStamp.startTime, routesTimeStamp.endTime), routeType == ERouteType.Block ? 17f : (movementType == EMovementType.KickStep ? 17f : 1f));
          keys.Add(keyframe5);
        }
      }
      this._stanceCurve = KeyframeUtils.CalculateConstantKeyframeTangents(keys);
    }

    private int PostPresnapShiftStance(float time)
    {
      Vector3 ballDataScenePos = this._playData.GetBallDataScenePos();
      Vector3 vector3 = this.Evaluate3DPositionYards(time);
      int num;
      if (((double) Mathf.Abs(Mathf.Abs(vector3.x) - Mathf.Abs(ballDataScenePos.x)) < 1.5 ? 1 : 0) != 0)
      {
        num = this._role == DataSensitiveStructs_v5.EPlayerRole.WideReceiver || this._role == DataSensitiveStructs_v5.EPlayerRole.RunningBack || this._role == DataSensitiveStructs_v5.EPlayerRole.FullBack || this._role == DataSensitiveStructs_v5.EPlayerRole.TightEnd || this._role == DataSensitiveStructs_v5.EPlayerRole.LineBacker || this._role == DataSensitiveStructs_v5.EPlayerRole.MiddleLineBacker || this._role == DataSensitiveStructs_v5.EPlayerRole.OutsideLineBacker || this._role == DataSensitiveStructs_v5.EPlayerRole.InsideLineBacker || this._role == DataSensitiveStructs_v5.EPlayerRole.DefensiveBack || this._role == DataSensitiveStructs_v5.EPlayerRole.CornerBack || this._role == DataSensitiveStructs_v5.EPlayerRole.FreeSafety || this._role == DataSensitiveStructs_v5.EPlayerRole.Safety || this._role == DataSensitiveStructs_v5.EPlayerRole.StrongSafety ? ((double) vector3.z - (double) ballDataScenePos.z > 0.0 ? ((double) this._defaultOrientation > 90.0 ? 3 : 4) : ((double) this._defaultOrientation > 90.0 ? 4 : 3)) : (int) this._defaultStance;
      }
      else
      {
        bool flag = (double) Mathf.Abs(Mathf.Abs(vector3.z) - Mathf.Abs(ballDataScenePos.z)) < 4.0;
        num = this._role == DataSensitiveStructs_v5.EPlayerRole.WideReceiver || this._role == DataSensitiveStructs_v5.EPlayerRole.RunningBack || this._role == DataSensitiveStructs_v5.EPlayerRole.FullBack || this._role == DataSensitiveStructs_v5.EPlayerRole.TightEnd ? (!flag ? ((double) vector3.z - (double) ballDataScenePos.z > 0.0 ? ((double) this._defaultOrientation > 90.0 ? 3 : 4) : ((double) this._defaultOrientation > 90.0 ? 4 : 3)) : 2) : (this._role == DataSensitiveStructs_v5.EPlayerRole.LineBacker || this._role == DataSensitiveStructs_v5.EPlayerRole.MiddleLineBacker || this._role == DataSensitiveStructs_v5.EPlayerRole.OutsideLineBacker || this._role == DataSensitiveStructs_v5.EPlayerRole.InsideLineBacker || this._role == DataSensitiveStructs_v5.EPlayerRole.DefensiveBack || this._role == DataSensitiveStructs_v5.EPlayerRole.CornerBack || this._role == DataSensitiveStructs_v5.EPlayerRole.FreeSafety || this._role == DataSensitiveStructs_v5.EPlayerRole.Safety || this._role == DataSensitiveStructs_v5.EPlayerRole.StrongSafety ? (((double) Mathf.Abs(Mathf.Abs(vector3.x) - Mathf.Abs(ballDataScenePos.x)) < 5.0 ? 1 : 0) == 0 ? ((double) vector3.z - (double) ballDataScenePos.z > 0.0 ? ((double) this._defaultOrientation > 90.0 ? 3 : 4) : ((double) this._defaultOrientation > 90.0 ? 4 : 3)) : 5) : (int) this._defaultStance);
      }
      return num;
    }

    private void DeriveOrientationCurve()
    {
      Keyframe[] keys1 = this.PositionCurves.xAxis.keys;
      Keyframe[] keys2 = this.PositionCurves.zAxis.keys;
      int length1 = keys1.Length;
      int length2 = length1 + 1;
      Keyframe[] keys3 = new Keyframe[length2];
      Vector3 normalized1 = Vector3.forward.normalized;
      TeamRuntimeData playersTeamData = this.GetPlayersTeamData(this.id);
      keys3[0] = !playersTeamData.StartInHuddle ? new Keyframe(keys1[0].time - 0.1f, this._defaultOrientation) : new Keyframe(keys1[0].time - 0.1f, playersTeamData.GetPlayerHuddleOrientation(this.id));
      for (int index = 0; index < length1 - 1; ++index)
      {
        Vector3 b = new Vector3(keys1[index].value, 0.0f, keys2[index].value);
        float time = keys1[index].time;
        float num1 = this._defaultOrientation;
        Vector3 a = new Vector3(keys1[index + 1].value, 0.0f, keys2[index + 1].value);
        if (!Mathf.Approximately(Vector3.Distance(a, b), 0.0f))
        {
          Vector3 normalized2 = (a - b).normalized;
          float num2 = Vector3.Angle(normalized1, normalized2);
          num1 = ((double) Mathf.Sign(Vector3.Cross(normalized1, normalized2).y) < 0.0 ? (float) ((360.0 - (double) num2) % 360.0) : num2) + PlayerRuntimeData.GetOrientationOffset((EMovementType) Mathf.RoundToInt(this._movementTypeCurve.Evaluate(time)));
        }
        keys3[index + 1] = new Keyframe(time, num1);
        b = a;
      }
      int index1 = length1 - 1;
      int index2 = length2 - 1;
      keys3[index2] = (double) keys3[index1].time <= 0.0 ? new Keyframe(keys1[index1].time + 0.1f, this._defaultOrientation) : new Keyframe(keys1[index1].time, keys3[index2 - 1].value);
      this._orientationCurve = PlayerRuntimeData.PostProcessOrientationCurve(keys3);
    }

    private void DeriveSpeedCurve()
    {
      float time1 = this.PositionCurves.StartTime - 1f;
      double num1 = (double) this.PositionCurves.EndTime + 1.0;
      float time2 = time1;
      Vector3 a = this.Evaluate3DPositionYards(time1);
      double num2 = (double) time1;
      int length = (int) ((num1 - num2) / (double) this._samplingTimeStep) + 1;
      Keyframe[] keys = new Keyframe[length];
      for (int index = 0; index < length; ++index)
      {
        Vector3 b = this.Evaluate3DPositionYards(time2);
        float num3 = Vector3.Distance(a, b) / this._samplingTimeStep;
        keys[index] = new Keyframe(time2, num3);
        time2 += this._samplingTimeStep;
        a = b;
      }
      this._speedCurve = KeyframeUtils.CalculateLinearKeyframeTangents(keys);
    }

    private void DeriveAngularSpeedCurve()
    {
      Keyframe[] keys = this._orientationCurve.keys;
      int length = keys.Length;
      List<Keyframe> keyframeList = new List<Keyframe>();
      float time1 = keys[0].time;
      float time2 = keys[keys.Length - 1].time;
      float num1 = keys[0].value;
      float num2 = 0.2f;
      for (; (double) time1 < (double) time2; time1 += num2)
      {
        double orientation = (double) this.EvaluateOrientation(time1);
        float num3 = (float) orientation - num1;
        keyframeList.Add(new Keyframe(time1 - num2, num3));
        num1 = (float) orientation;
      }
      this._angularSpeedCurve = new AnimationCurve(keyframeList.ToArray());
    }

    private void SetPositionalCurve(Keyframe[] xAxisKeyframes, Keyframe[] yAxisKeyframes) => PlayerRuntimeData.PostProcessPositionalCurves(xAxisKeyframes, yAxisKeyframes, out this._positionalCurves.xAxis, out this._positionalCurves.zAxis);

    private static float GetOrientationOffset(EMovementType movementType)
    {
      float orientationOffset;
      switch (movementType)
      {
        case EMovementType.RunForward:
          orientationOffset = 0.0f;
          break;
        case EMovementType.Backpedal:
          orientationOffset = 180f;
          break;
        case EMovementType.ShuffleLeft:
          orientationOffset = 90f;
          break;
        case EMovementType.ShuffleRight:
          orientationOffset = -90f;
          break;
        case EMovementType.KickStep:
          orientationOffset = 180f;
          break;
        default:
          orientationOffset = 0.0f;
          break;
      }
      return orientationOffset;
    }

    private EPlayerStance GameInProgressStance(DataSensitiveStructs_v5.EPlayerRole role) => role != DataSensitiveStructs_v5.EPlayerRole.Center && role != DataSensitiveStructs_v5.EPlayerRole.TightEnd && role != DataSensitiveStructs_v5.EPlayerRole.OffensiveGuard && role != DataSensitiveStructs_v5.EPlayerRole.OffensiveTackle ? (EPlayerStance) this._stanceCurve.Evaluate(-0.01f) : EPlayerStance.StanceBlock;

    public static void AdjustOrientationValuesBasedOnShortestAngles(
      List<Keyframe> orientationKeysList)
    {
      List<Keyframe> keyframeList = orientationKeysList;
      for (int index = 1; index < keyframeList.Count; ++index)
      {
        Keyframe keyframe1 = keyframeList[index - 1];
        Keyframe keyframe2 = keyframeList[index];
        int num1 = Mathf.FloorToInt(keyframe1.value / 360f);
        keyframe2.value += (float) (num1 * 360);
        if ((double) Mathf.Abs(keyframe1.value - keyframe2.value) > 180.0)
        {
          float num2 = Mathf.Sign(keyframe1.value - keyframe2.value);
          keyframe2.value += num2 * 360f;
        }
        keyframeList[index] = keyframe2;
      }
    }

    public static AnimationCurve PostProcessOrientationCurve(Keyframe[] keys)
    {
      List<Keyframe> orientationKeysList = new List<Keyframe>((IEnumerable<Keyframe>) keys);
      PlayerRuntimeData.AdjustOrientationValuesBasedOnShortestAngles(orientationKeysList);
      PlayerRuntimeData.IncertSmoothTurningKeys(orientationKeysList);
      return new AnimationCurve()
      {
        keys = orientationKeysList.ToArray()
      };
    }

    private static void IncertSmoothTurningKeys(List<Keyframe> orientationKeysList)
    {
      List<Keyframe> keyframeList = orientationKeysList;
      keyframeList[0] = KeyframeUtils.AdjustKeyframeIncomingTangentToConstant(keyframeList[0]);
      keyframeList[1] = KeyframeUtils.AdjustKeyframeOutgoingTangentToConstant(keyframeList[1]);
      float num1 = 360f;
      for (int index = 1; index < keyframeList.Count - 1; ++index)
      {
        Keyframe keyframe1 = keyframeList[index];
        Keyframe keyframe2 = keyframeList[index + 1];
        double num2 = (double) Mathf.Abs(Mathf.DeltaAngle(keyframe1.value, keyframe2.value)) / (double) num1;
        float num3 = Mathf.Abs(keyframe2.time - keyframe1.time);
        float num4 = (float) (num2 / 2.0);
        if ((double) num4 < (double) num3)
        {
          Keyframe key = new Keyframe(keyframe2.time - num4, keyframe1.value);
          key = KeyframeUtils.AdjustKeyframeIncomingTangentToConstant(key);
          keyframeList.Insert(index + 1, key);
          ++index;
        }
        keyframe2.time += num4;
        keyframeList[index + 1] = keyframe2;
      }
    }

    public static void PostProcessPositionalCurves(
      Keyframe[] keyframesX,
      Keyframe[] keyframesY,
      out AnimationCurve resultPosXCurve,
      out AnimationCurve resultPosYCurve)
    {
      int length = keyframesX.Length;
      for (int index = 1; index < length - 1; ++index)
      {
        Vector2 vector2_1 = new Vector2(keyframesX[index - 1].value, keyframesY[index - 1].value);
        Vector2 vector2_2 = new Vector2(keyframesX[index].value, keyframesY[index].value);
        Vector2 vector2_3 = new Vector2(keyframesX[index + 1].value, keyframesY[index + 1].value);
        Vector2 from = vector2_1 - vector2_2;
        Vector2 vector2_4 = vector2_2;
        Vector2 to = vector2_3 - vector2_4;
        if ((double) Mathf.Abs(Vector2.Angle(from, to)) > 100.0)
        {
          keyframesX[index] = KeyframeUtils.AdjustKeyframeTangentsToLiniar(keyframesX[index], keyframesX[index - 1], keyframesX[index + 1]);
          keyframesY[index] = KeyframeUtils.AdjustKeyframeTangentsToLiniar(keyframesY[index], keyframesY[index - 1], keyframesY[index + 1]);
        }
      }
      resultPosXCurve = new AnimationCurve()
      {
        keys = keyframesX
      };
      resultPosYCurve = new AnimationCurve()
      {
        keys = keyframesY
      };
    }

    [Serializable]
    private struct RouteTimeStamps
    {
      public int routeID;
      public float startTime;
      public float endTime;
    }
  }
}
