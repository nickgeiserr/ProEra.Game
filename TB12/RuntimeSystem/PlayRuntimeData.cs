// Decompiled with JetBrains decompiler
// Type: TB12.RuntimeSystem.PlayRuntimeData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DataSensitiveStructs_v5;
using DDL;
using DDL.UniformData;
using FootballVR;
using FootballWorld;
using Framework;
using ProjectConstants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.RuntimeSystem
{
  [CreateAssetMenu(fileName = "PlayRuntimeData", menuName = "TB12/Core/PlayRuntimeData", order = 1)]
  public class PlayRuntimeData : ScriptableObject
  {
    public Action<ExplodedPlayData> OnPlayLoaded;
    public System.Action OnPlayClosed;
    public System.Action PostTimeBoundsUpdated;
    public static bool OverrideUniforms;
    [Header("Data Management Variables")]
    private readonly DataManager<PlayerRuntimeData> _playersRuntime = new DataManager<PlayerRuntimeData>();
    private readonly DataManager<RouteRuntimeData> _routesRuntime = new DataManager<RouteRuntimeData>();
    private readonly DataManager<BallTrajectoryRuntimeData> _ballTrajRuntime = new DataManager<BallTrajectoryRuntimeData>();
    private BallRuntimeData _ballRuntimeData;
    private TeamRuntimeData _teamARuntimeData;
    private TeamRuntimeData _teamBRuntimeData;
    [Header("Runtime Cache Variables")]
    [SerializeField]
    private float _preSnapStartTime;
    [SerializeField]
    private UniformStore _uniformStore;
    [SerializeField]
    private float _postSnapEndTime;
    [SerializeField]
    private float _huddleDuration;
    [SerializeField]
    private PlaybackInfo PlaybackInfo;
    private bool _timeBoundsUpdateScheduled;
    public static string PlayName;
    public static bool HuddleEnabled;
    private Coroutine _trackingRoutineInstance;

    public bool PlayLoaded { get; private set; }

    public BallRuntimeData GetBallRuntimeData() => this._ballRuntimeData;

    public Vector3 GetBallDataScenePos() => MathUtils.TransformDataCoordinatesToScenePosition(SerializedDataManager.RetrieveBallData().placement);

    public float PreSnapStartTime => this._preSnapStartTime - Timeline.AdditiveTimeBeforePreSnap;

    public float PostSnapEndTime => this._postSnapEndTime + Timeline.AdditiveTimeAfterPostSnap;

    public float HuddleStartTime
    {
      get
      {
        float timeBeforeHuddle = Timeline.AdditiveTimeBeforeHuddle;
        return this.PreSnapStartTime - Mathf.Abs((double) this._huddleDuration > 0.0 ? this._huddleDuration + timeBeforeHuddle : 0.0f);
      }
    }

    public float InitialFormationTime => this.PreSnapStartTime + Timeline.AdditiveTimeBeforePreSnap / 2f;

    public bool IsHuddlePresent => (double) this._huddleDuration > 0.0;

    public bool IsCurrentlyInHuddle => this.IsInHuddle(this.PlaybackInfo.PlayTime);

    public bool IsInHuddle(float time) => (double) this.PreSnapStartTime - (double) this.HuddleStartTime > 0.0 && (double) time >= (double) this.HuddleStartTime && (double) time < (double) this.InitialFormationTime;

    public Queue<int> GetRoutesChainStartingWith(int routeID) => SerializedDataManager.GetRoutesChainStartingWith(routeID);

    public Queue<int> GetBallTrajectoriesChainStartingWith(int trajID) => SerializedDataManager.GetBallTrajectoriesChainStartingWith(trajID);

    public PlayerRuntimeData GetPlayerOwnerOfRoute(int routeID) => this._playersRuntime.RetrieveData(this.GetPlayerOwnerOfRouteId(routeID));

    public int GetPlayerOwnerOfRouteId(int routeID)
    {
      int key = routeID;
      do
      {
        RouteRuntimeData routeRuntimeData = this._routesRuntime.RetrieveData(key);
        key = routeRuntimeData.anchorId;
        if (routeRuntimeData.anchorType == typeof (PlayerData))
          return key;
      }
      while (key > 0);
      return -1;
    }

    public TeamRuntimeData GetPlayersTeamData(int playerID)
    {
      TeamRuntimeData playersTeamData = (TeamRuntimeData) null;
      if (this._teamARuntimeData.ContainsPlayerWithID(playerID))
        playersTeamData = this._teamARuntimeData;
      else if (this._teamBRuntimeData.ContainsPlayerWithID(playerID))
        playersTeamData = this._teamBRuntimeData;
      return playersTeamData;
    }

    public ETeamLetters GetPlayersTeamID(int playerID)
    {
      if (this._teamARuntimeData.ContainsPlayerWithID(playerID))
        return ETeamLetters.TeamA;
      if (this._teamBRuntimeData.ContainsPlayerWithID(playerID))
        return ETeamLetters.TeamB;
      Debug.LogError((object) ("Could not find team id for player " + playerID.ToString()));
      return ETeamLetters.Unknown;
    }

    public ETeamLetters GetRoutesTeamID(int routeID) => this.GetPlayersTeamID(this.GetPlayerOwnerOfRouteId(routeID));

    public RouteRuntimeData RetrieveRouteData(int id) => this._routesRuntime.RetrieveData(id);

    public PlayerRuntimeData RetrievePlayerData(int id) => this._playersRuntime.RetrieveData(id);

    public TeamRuntimeData RetrieveTeamData(ETeamLetters team) => team != ETeamLetters.TeamA ? this._teamBRuntimeData : this._teamARuntimeData;

    public BallTrajectoryRuntimeData RetrieveBallTrajectoryData(int id) => this._ballTrajRuntime.RetrieveData(id);

    private void OnEnable()
    {
    }

    public void Initialize()
    {
      SerializedDataManager.OnPlayLoaded += new Action<DataSensitiveStructs_v5.PlayData>(this.HandlePlayLoaded);
      SerializedDataManager.OnClear += new System.Action(this.Clear);
      this.StartScheduledEventTracker();
    }

    public void Deinitialize()
    {
      SerializedDataManager.OnPlayLoaded -= new Action<DataSensitiveStructs_v5.PlayData>(this.HandlePlayLoaded);
      SerializedDataManager.OnClear -= new System.Action(this.Clear);
      this.StopScheduledEventTracker();
      this.Clear();
    }

    private void StartScheduledEventTracker()
    {
      this.StopScheduledEventTracker();
      this._trackingRoutineInstance = RoutineRunner.StartRoutine(this.ScheduledEventsTrackingRoutine());
    }

    private void StopScheduledEventTracker()
    {
      if (this._trackingRoutineInstance == null)
        return;
      RoutineRunner.StopRoutine(this._trackingRoutineInstance);
    }

    private IEnumerator ScheduledEventsTrackingRoutine()
    {
      WaitForEndOfFrame waitEndOfFrame = new WaitForEndOfFrame();
      while (true)
      {
        System.Action timeBoundsUpdated;
        do
        {
          do
          {
            yield return (object) waitEndOfFrame;
          }
          while (!this._timeBoundsUpdateScheduled);
          this._timeBoundsUpdateScheduled = false;
          this.UpdateTimeBounds();
          timeBoundsUpdated = this.PostTimeBoundsUpdated;
        }
        while (timeBoundsUpdated == null);
        timeBoundsUpdated();
      }
    }

    private void ScheduleTimeBoundsUpdate() => this._timeBoundsUpdateScheduled = true;

    private void HandlePlayLoaded(DataSensitiveStructs_v5.PlayData playData)
    {
      this.LoadPlay(playData);
      this.ScheduleTimeBoundsUpdate();
    }

    private void Clear()
    {
      this.PlayLoaded = false;
      ReadOnlyArray<RouteRuntimeData> onlyDataCollection1 = this._routesRuntime.ReadOnlyDataCollection;
      int length1 = onlyDataCollection1.Length;
      for (int key = 0; key < length1; ++key)
        this._routesRuntime.RetrieveData(onlyDataCollection1[key].id).Deinit();
      ReadOnlyArray<PlayerRuntimeData> onlyDataCollection2 = this._playersRuntime.ReadOnlyDataCollection;
      int length2 = onlyDataCollection2.Length;
      for (int key = 0; key < length2; ++key)
        this._playersRuntime.RetrieveData(onlyDataCollection2[key].id).Deinit();
      this._teamARuntimeData?.Deinit();
      this._teamBRuntimeData?.Deinit();
      this._playersRuntime.Clear();
      this._routesRuntime.Clear();
      this._ballTrajRuntime.Clear();
      this._teamARuntimeData = (TeamRuntimeData) null;
      this._teamBRuntimeData = (TeamRuntimeData) null;
      System.Action onPlayClosed = this.OnPlayClosed;
      if (onPlayClosed == null)
        return;
      onPlayClosed();
    }

    private void LoadPlay(DataSensitiveStructs_v5.PlayData playData)
    {
      PlayRuntimeData.HuddleEnabled = playData.visibilitySettings.misc_huddle == 1;
      List<RouteData> routes = playData.routes;
      int count1 = routes.Count;
      for (int index = 0; index < count1; ++index)
      {
        RouteRuntimeData newEntry = new RouteRuntimeData();
        newEntry.Init(routes[index], -1, (System.Type) null);
        this._routesRuntime.AddDataEntry(newEntry);
      }
      for (int index = 0; index < count1; ++index)
      {
        int nextRouteId = routes[index].nextRouteId;
        if (nextRouteId != 0)
        {
          RouteRuntimeData routeRuntimeData = this._routesRuntime.RetrieveData(nextRouteId);
          routeRuntimeData.anchorId = routes[index].id;
          routeRuntimeData.anchorType = typeof (RouteData);
        }
      }
      List<DataSensitiveStructs_v5.PlayerData> players = playData.players;
      int count2 = players.Count;
      for (int index = 0; index < count2; ++index)
      {
        PlayerRuntimeData newEntry = new PlayerRuntimeData();
        newEntry.Init(players[index], this);
        this._playersRuntime.AddDataEntry(newEntry);
        int routeId = players[index].routeId;
        if (routeId != 0)
        {
          RouteRuntimeData routeRuntimeData = this._routesRuntime.RetrieveData(routeId);
          routeRuntimeData.anchorId = players[index].id;
          routeRuntimeData.anchorType = typeof (PlayerData);
        }
      }
      this._teamARuntimeData = new TeamRuntimeData();
      DataSensitiveStructs_v5.TeamData teamA = playData.teamA;
      this._teamARuntimeData.Init(teamA, ETeamLetters.TeamA, this);
      this._teamBRuntimeData = new TeamRuntimeData();
      DataSensitiveStructs_v5.TeamData teamB = playData.teamB;
      this._teamBRuntimeData.Init(teamB, ETeamLetters.TeamB, this);
      List<BallTrajectoryData> ballTrajectories = playData.ballTrajectories;
      int count3 = ballTrajectories.Count;
      for (int index = 0; index < count3; ++index)
      {
        BallTrajectoryRuntimeData newEntry = new BallTrajectoryRuntimeData();
        newEntry.Init(ballTrajectories[index], this);
        this._ballTrajRuntime.AddDataEntry(newEntry);
      }
      this._ballRuntimeData = new BallRuntimeData();
      this._ballRuntimeData.Init(playData.ballData, this);
      this.UpdateTimeBounds();
      FootballWorld.UniformConfig uniformConfig1;
      FootballWorld.UniformConfig uniformConfig2;
      if (PlayRuntimeData.OverrideUniforms)
      {
        uniformConfig1 = this._uniformStore.GetUniformConfig(ETeamUniformId.Dolphins, ETeamUniformFlags.Away);
        uniformConfig2 = this._uniformStore.GetUniformConfig(ETeamUniformId.Ravens, ETeamUniformFlags.Home);
      }
      else
      {
        uniformConfig1 = this._uniformStore.GetUniformConfig(teamA.uniformId, teamA.uniformFlags);
        uniformConfig2 = this._uniformStore.GetUniformConfig(teamB.uniformId, teamB.uniformFlags);
      }
      this.load_textures(new ExplodedPlayData()
      {
        PlayData = playData,
        TeamAUniformConfig = uniformConfig1,
        TeamBUniformConfig = uniformConfig2
      });
    }

    private void load_textures(ExplodedPlayData pd)
    {
      pd.TeamATexture = pd.TeamAUniformConfig.BasemapAlternative;
      pd.TeamBTexture = pd.TeamBUniformConfig.BasemapAlternative;
      Action<ExplodedPlayData> onPlayLoaded = this.OnPlayLoaded;
      if (onPlayLoaded != null)
        onPlayLoaded(pd);
      this.PlayLoaded = true;
    }

    private void UpdateTimeBounds()
    {
      this._preSnapStartTime = 0.0f;
      this._postSnapEndTime = 0.0f;
      ReadOnlyArray<PlayerRuntimeData> onlyDataCollection = this._playersRuntime.ReadOnlyDataCollection;
      int length = onlyDataCollection.Length;
      for (int key = 0; key < length; ++key)
      {
        float movementStartTime = onlyDataCollection[key].MovementStartTime;
        this._preSnapStartTime = (double) movementStartTime < (double) this._preSnapStartTime ? movementStartTime : this._preSnapStartTime;
        float movementEndTime = onlyDataCollection[key].MovementEndTime;
        this._postSnapEndTime = (double) movementEndTime > (double) this._postSnapEndTime ? movementEndTime : this._postSnapEndTime;
      }
      this._preSnapStartTime = Mathf.Min(this._preSnapStartTime, 0.0f);
      this._postSnapEndTime = Mathf.Max(this._postSnapEndTime, 0.0f);
      this._huddleDuration = 0.0f;
      for (int key = 0; key < length; ++key)
      {
        onlyDataCollection[key].OnPlayTimeBoundsUpdated();
        float movementDuration = onlyDataCollection[key].HuddleMovementDuration;
        this._huddleDuration = (double) this._huddleDuration < (double) movementDuration ? movementDuration : this._huddleDuration;
      }
      this.PlaybackInfo.Setup(this.HuddleStartTime, this.PostSnapEndTime);
      for (int key = 0; key < length; ++key)
      {
        onlyDataCollection[key].OnPostProcess();
        float movementDuration = onlyDataCollection[key].HuddleMovementDuration;
        this._huddleDuration = (double) this._huddleDuration < (double) movementDuration ? movementDuration : this._huddleDuration;
      }
    }
  }
}
