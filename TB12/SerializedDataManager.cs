// Decompiled with JetBrains decompiler
// Type: TB12.SerializedDataManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DataSensitiveStructs_v5;
using DDL;
using DSE;
using FootballWorld;
using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  public static class SerializedDataManager
  {
    private static bool _IsDirty = false;
    public static Action<bool> OnDirty;
    public static Action<PlayData> OnPlayLoaded;
    public static Action OnClear;
    public static int _timelineStopCount = -1;
    private static DataManager<PlayerData> _playerDataManager = new DataManager<PlayerData>();
    private static DataManager<RouteData> _routeDataManager = new DataManager<RouteData>();
    private static DataManager<BallTrajectoryData> _ballTrajDataManager = new DataManager<BallTrajectoryData>();
    private static List<float> _timelineStops = new List<float>();
    private static BallData _ballData;
    private static FirstDownYardLineData _firstDownData;
    private static TeamData _teamA;
    private static TeamData _teamB;
    private static EPlayType _playType;
    private static EPlaySource _playSource;

    public static bool IsDirty
    {
      get => SerializedDataManager._IsDirty;
      set
      {
        if (SerializedDataManager._IsDirty != value)
        {
          Action<bool> onDirty = SerializedDataManager.OnDirty;
          if (onDirty != null)
            onDirty(value);
        }
        SerializedDataManager._IsDirty = value;
      }
    }

    public static bool PlayLoaded { get; private set; }

    public static event Action<int> OnTimelineStopsCountChanged;

    public static int TimelineStopsCount
    {
      get => SerializedDataManager._timelineStopCount;
      set
      {
        SerializedDataManager._timelineStopCount = value;
        Action<int> stopsCountChanged = SerializedDataManager.OnTimelineStopsCountChanged;
        if (stopsCountChanged == null)
          return;
        stopsCountChanged(value);
      }
    }

    public static void DeserializeData(PlayData playData, UniformStore uniformStore)
    {
      SerializedDataManager.PlayLoaded = false;
      SerializedDataManager._playSource = playData.playSource;
      SerializedDataManager._playType = playData.playType;
      SerializedDataManager._playerDataManager.RecreateFromDataArray(playData.players);
      SerializedDataManager._routeDataManager.RecreateFromDataArray(playData.routes);
      SerializedDataManager._ballTrajDataManager.RecreateFromDataArray(playData.ballTrajectories);
      SerializedDataManager._timelineStops = new List<float>((IEnumerable<float>) playData.timelineStops);
      SerializedDataManager.TimelineStopsCount = SerializedDataManager._timelineStops.Count;
      SerializedDataManager._ballData = playData.ballData;
      SerializedDataManager._firstDownData = playData.firstDownYardLineData;
      SerializedDataManager._teamA = playData.teamA;
      SerializedDataManager._teamB = playData.teamB;
      List<PlayerData> datas = new List<PlayerData>();
      if (SerializedDataManager.RetrievePlayerDatas(SerializedDataManager._teamA.playerIds, ref datas) > 0)
      {
        List<string> names = new List<string>();
        List<int> numbers = new List<int>();
        foreach (PlayerData playerData in datas)
        {
          if (string.IsNullOrEmpty(playerData.playerName))
            names.Add(" ");
          else if (!playerData.playerName.Contains(" "))
          {
            names.Add(playerData.playerName.ToUpper());
          }
          else
          {
            string[] strArray = playerData.playerName.Split(' ', StringSplitOptions.None);
            names.Add(strArray[strArray.Length - 1].ToUpper());
          }
          numbers.Add(playerData.uniformNumber);
        }
        uniformStore.GetUniformConfig(SerializedDataManager._teamA.uniformId, SerializedDataManager._teamA.uniformFlags);
        UniformCapture.GenerateUniforms(1, numbers, names, SerializedDataManager._teamA.uniformFlags);
      }
      datas.Clear();
      if (SerializedDataManager.RetrievePlayerDatas(SerializedDataManager._teamB.playerIds, ref datas) > 0)
      {
        List<string> names = new List<string>();
        List<int> numbers = new List<int>();
        foreach (PlayerData playerData in datas)
        {
          if (string.IsNullOrEmpty(playerData.playerName))
            names.Add(" ");
          else if (!playerData.playerName.Contains(" "))
          {
            names.Add(playerData.playerName.ToUpper());
          }
          else
          {
            string[] strArray = playerData.playerName.Split(' ', StringSplitOptions.None);
            names.Add(strArray[strArray.Length - 1].ToUpper());
          }
          numbers.Add(playerData.uniformNumber);
        }
        uniformStore.GetUniformConfig(SerializedDataManager._teamB.uniformId, SerializedDataManager._teamB.uniformFlags);
        UniformCapture.GenerateUniforms(2, numbers, names, SerializedDataManager._teamB.uniformFlags);
      }
      RoutineRunner.StartRoutine(SerializedDataManager.ContinuePlayLoading(playData));
    }

    public static IEnumerator ContinuePlayLoading(PlayData playData)
    {
      yield return (object) new WaitForSeconds(0.1f);
      Action<PlayData> onPlayLoaded = SerializedDataManager.OnPlayLoaded;
      if (onPlayLoaded != null)
        onPlayLoaded(playData);
      SerializedDataManager.PlayLoaded = true;
    }

    public static void Clear()
    {
      Action onClear = SerializedDataManager.OnClear;
      if (onClear != null)
        onClear();
      SerializedDataManager.PlayLoaded = false;
      SerializedDataManager.IsDirty = false;
      SerializedDataManager._playerDataManager.Clear();
      SerializedDataManager._routeDataManager.Clear();
      SerializedDataManager._ballTrajDataManager.Clear();
      SerializedDataManager._timelineStops.Clear();
      SerializedDataManager.TimelineStopsCount = -1;
      SerializedDataManager._ballData = (BallData) null;
      SerializedDataManager._teamA = (TeamData) null;
      SerializedDataManager._teamB = (TeamData) null;
      SerializedDataManager._playType = EPlayType.Unknown;
    }

    public static int GetUniquePlayerID() => SerializedDataManager._playerDataManager.GetUniqueID();

    public static int GetUniqueRouteID() => SerializedDataManager._routeDataManager.GetUniqueID();

    public static int GetUniqueBallTrajectoryID() => SerializedDataManager._ballTrajDataManager.GetUniqueID();

    public static void AddNewPlayerEntry(PlayerData newPlayerData) => SerializedDataManager._playerDataManager.AddDataEntry(newPlayerData);

    public static void AddNewRouteEntry(RouteData newRouteData) => SerializedDataManager._routeDataManager.AddDataEntry(newRouteData);

    public static void AddNewBallTrajectoryEntry(BallTrajectoryData newBallTrajectoryData) => SerializedDataManager._ballTrajDataManager.AddDataEntry(newBallTrajectoryData);

    public static void RemovePlayerData(int id) => SerializedDataManager._playerDataManager.RemoveData(id);

    public static void RemoveRouteData(int id)
    {
      int[] array = SerializedDataManager.GetRoutesChainStartingWith(id).ToArray();
      int length = array.Length;
      for (int index = 0; index < length; ++index)
      {
        int key = array[index];
        SerializedDataManager._routeDataManager.RemoveData(key);
      }
      Debug.Log((object) ("Entries deleted " + length.ToString()));
    }

    public static void RemoveBallTrajectoryData(int id)
    {
      int[] array = SerializedDataManager.GetBallTrajectoriesChainStartingWith(id).ToArray();
      int length = array.Length;
      for (int index = 0; index < length; ++index)
      {
        int key = array[index];
        SerializedDataManager._ballTrajDataManager.RemoveData(key);
      }
      Debug.Log((object) ("Entries deleted " + length.ToString()));
    }

    public static Queue<int> GetWholeBallTrajectoriesChain()
    {
      Queue<int> trajectoriesChain = new Queue<int>();
      int ballTrajId = SerializedDataManager._ballData.ballTrajId;
      if (ballTrajId != 0)
        trajectoriesChain = SerializedDataManager.GetBallTrajectoriesChainStartingWith(ballTrajId);
      return trajectoriesChain;
    }

    public static void RemoveBallTrajectoriesDependentOnThePlayer(int playerID)
    {
      int[] array = SerializedDataManager.GetWholeBallTrajectoriesChain().ToArray();
      int length = array.Length;
      List<BallTrajectoryData> ballTrajectoryDataList = new List<BallTrajectoryData>();
      for (int index = 0; index < length; ++index)
      {
        BallTrajectoryData ballTrajectoryData = SerializedDataManager.RetrieveBallTrajectoryData(array[index]);
        ballTrajectoryDataList.Add(ballTrajectoryData);
      }
      for (int index = 0; index < length; ++index)
      {
        if (ballTrajectoryDataList[index].fromPlayerId == playerID || ballTrajectoryDataList[index].toPlayerId == playerID)
        {
          SerializedDataManager.RemoveBallTrajectoryData(playerID);
          break;
        }
      }
    }

    public static EPlayType GetPlayType() => SerializedDataManager._playType;

    public static EPlaySource GetPlaySource() => SerializedDataManager._playSource;

    public static bool IsNGSPlay => SerializedDataManager.GetPlaySource() == EPlaySource.kNgs;

    public static TeamData RetrieveTeamData(ETeamLetters team) => team != ETeamLetters.TeamA ? SerializedDataManager._teamB : SerializedDataManager._teamA;

    public static PlayerData RetrievePlayerData(int id) => SerializedDataManager._playerDataManager.RetrieveData(id);

    public static int RetrievePlayerDatas(List<int> ids, ref List<PlayerData> datas)
    {
      int count = ids.Count;
      for (int index = 0; index < count; ++index)
      {
        PlayerData playerData = SerializedDataManager._playerDataManager.RetrieveData(ids[index]);
        datas.Add(playerData);
      }
      return datas.Count;
    }

    public static int RetrieveRouteDatas(int[] ids, ref List<RouteData> datas)
    {
      int length = ids.Length;
      for (int index = 0; index < length; ++index)
      {
        RouteData routeData = SerializedDataManager._routeDataManager.RetrieveData(ids[index]);
        datas.Add(routeData);
      }
      return datas.Count;
    }

    public static int RetrieveBallTrajectoryDatas(int[] ids, ref List<BallTrajectoryData> datas)
    {
      int length = ids.Length;
      for (int index = 0; index < length; ++index)
      {
        BallTrajectoryData ballTrajectoryData = SerializedDataManager._ballTrajDataManager.RetrieveData(ids[index]);
        datas.Add(ballTrajectoryData);
      }
      return datas.Count;
    }

    public static void UpdatePlayerData(int[] ids, ref List<PlayerData> playerDatas)
    {
      int count = playerDatas.Count;
      for (int index = 0; index < count; ++index)
      {
        PlayerData newData = playerDatas[index];
        SerializedDataManager._playerDataManager.UpdateData(ids[index], newData);
      }
    }

    public static void UpdateRouteData(int[] ids, ref List<RouteData> routeDatas)
    {
      int count = routeDatas.Count;
      for (int index = 0; index < count; ++index)
      {
        RouteData newData = routeDatas[index];
        SerializedDataManager._routeDataManager.UpdateData(ids[index], newData);
      }
    }

    public static void UpdateBallTrajectoryData(
      int[] ids,
      ref List<BallTrajectoryData> ballTrajDatas)
    {
      int count = ballTrajDatas.Count;
      for (int index = 0; index < count; ++index)
      {
        BallTrajectoryData newData = ballTrajDatas[index];
        SerializedDataManager._ballTrajDataManager.UpdateData(ids[index], newData);
      }
    }

    public static int GetRouteChainDataArrayForPlayerId(
      int playerId,
      ref List<RouteData> routeDatas)
    {
      int[] arrayForPlayerId = SerializedDataManager.GetRouteArrayForPlayerId(playerId);
      if (arrayForPlayerId == null)
        return 0;
      int length = arrayForPlayerId.Length;
      for (int index = 0; index < length; ++index)
      {
        RouteData routeData = SerializedDataManager._routeDataManager.RetrieveData(arrayForPlayerId[index]);
        routeDatas.Add(routeData);
      }
      return routeDatas.Count;
    }

    public static int GetRouteChainDataArrayStartingFromRouteId(
      int routeId,
      ref List<RouteData> routeDatas)
    {
      if (routeId == 0)
        return 0;
      int key = routeId;
      do
      {
        RouteData routeData = SerializedDataManager._routeDataManager.RetrieveData(key);
        if (routeData != null)
        {
          routeDatas.Add(routeData);
          key = routeData.nextRouteId;
        }
        else
          break;
      }
      while (key != 0);
      return routeDatas.Count;
    }

    public static RouteData RetrieveRouteData(int id) => SerializedDataManager._routeDataManager.RetrieveData(id);

    public static BallTrajectoryData RetrieveBallTrajectoryData(int id) => SerializedDataManager._ballTrajDataManager.RetrieveData(id);

    public static BallData RetrieveBallData() => SerializedDataManager._ballData;

    public static FirstDownYardLineData RetrieveFirstDownData() => SerializedDataManager._firstDownData;

    public static void UpdateTeamData(ETeamLetters team, TeamData newTeamData)
    {
      if (team == ETeamLetters.TeamA)
        SerializedDataManager._teamA = newTeamData;
      else
        SerializedDataManager._teamB = newTeamData;
    }

    public static void AddTimelineStop(float playTime)
    {
      SerializedDataManager._timelineStops.Add(playTime);
      SerializedDataManager.TimelineStopsCount = SerializedDataManager._timelineStops.Count;
      SerializedDataManager.IsDirty = true;
    }

    public static void RemoveTimelineStop(int timelineStopIndex)
    {
      SerializedDataManager._timelineStops.RemoveAt(timelineStopIndex);
      SerializedDataManager.TimelineStopsCount = SerializedDataManager._timelineStops.Count;
      SerializedDataManager.IsDirty = true;
    }

    public static void RemoveTimelineStop(float timeStamp)
    {
      int count = SerializedDataManager._timelineStops.Count;
      for (int index = 0; index < count; ++index)
      {
        if (Mathf.Approximately(SerializedDataManager._timelineStops[index], timeStamp))
        {
          SerializedDataManager.RemoveTimelineStop(index);
          SerializedDataManager.IsDirty = true;
          break;
        }
      }
    }

    public static void RemoveAllTimelineStops()
    {
      SerializedDataManager._timelineStops.Clear();
      SerializedDataManager.TimelineStopsCount = 0;
      SerializedDataManager.IsDirty = true;
    }

    public static float[] RetrieveTimelineStopTimes() => SerializedDataManager._timelineStops.ToArray();

    public static Queue<int> GetRoutesChainStartingWith(int routeID)
    {
      Queue<int> chainStartingWith = new Queue<int>();
      if (routeID == 0)
        return chainStartingWith;
      int key = routeID;
      do
      {
        RouteData routeData = SerializedDataManager._routeDataManager.RetrieveData(key);
        if (routeData != null)
        {
          chainStartingWith.Enqueue(key);
          key = routeData.nextRouteId;
        }
        else
          break;
      }
      while (key != 0);
      return chainStartingWith;
    }

    public static bool RouteChainContainsRoute(int startId, int routeId)
    {
      if (startId == 0 || routeId == 0)
        return false;
      int key = startId;
      while (routeId != key)
      {
        RouteData routeData = SerializedDataManager._routeDataManager.RetrieveData(key);
        if (routeData != null)
        {
          key = routeData.nextRouteId;
          if (key != 0)
            continue;
        }
        return false;
      }
      return true;
    }

    public static int[] GetRouteArrayForPlayerId(int playerId)
    {
      PlayerData playerData = SerializedDataManager.RetrievePlayerData(playerId);
      if (playerData == null)
        return (int[]) null;
      return playerData.routeId <= 0 ? (int[]) null : SerializedDataManager.GetRoutesChainStartingWith(playerData.routeId).ToArray();
    }

    public static Queue<int> GetBallTrajectoriesChainStartingWith(int trajID)
    {
      Queue<int> chainStartingWith = new Queue<int>();
      if (trajID == 0)
        return chainStartingWith;
      int key = trajID;
      while (SerializedDataManager._ballTrajDataManager.ContainsData(key))
      {
        chainStartingWith.Enqueue(key);
        key = SerializedDataManager._ballTrajDataManager.RetrieveData(key).nextTrajId;
        if (key == 0)
          break;
      }
      return chainStartingWith;
    }

    public static int GetPreviousTrajectoryId(float playTime)
    {
      int previousTrajectoryId = 0;
      foreach (int id in SerializedDataManager.GetBallTrajectoriesChainStartingWith(SerializedDataManager._ballData.ballTrajId).ToArray())
      {
        BallTrajectoryData ballTrajectoryData = SerializedDataManager.RetrieveBallTrajectoryData(id);
        if ((double) playTime > (double) ballTrajectoryData.playTime[1])
          previousTrajectoryId = id;
        else
          break;
      }
      return previousTrajectoryId;
    }

    public static ETeamLetters GetPlayersTeamID(int playerID)
    {
      int count1 = SerializedDataManager._teamA.playerIds.Count;
      for (int index = 0; index < count1; ++index)
      {
        if (SerializedDataManager._teamA.playerIds[index] == playerID)
          return ETeamLetters.TeamA;
      }
      int count2 = SerializedDataManager._teamB.playerIds.Count;
      for (int index = 0; index < count2; ++index)
      {
        if (SerializedDataManager._teamB.playerIds[index] == playerID)
          return ETeamLetters.TeamB;
      }
      Debug.LogError((object) ("The specified player with ID " + playerID.ToString() + " is not found in either of the teams."));
      return ETeamLetters.Unknown;
    }

    public static int GetPlayerOwnerOfRoute(int routeID)
    {
      int playerOwnerOfRoute = 0;
      ReadOnlyArray<PlayerData> onlyDataCollection = SerializedDataManager._playerDataManager.ReadOnlyDataCollection;
      int length = onlyDataCollection.Length;
      for (int key = 0; key < length; ++key)
      {
        if (onlyDataCollection[key].routeId != 0 && SerializedDataManager.RouteChainContainsRoute(onlyDataCollection[key].routeId, routeID))
        {
          playerOwnerOfRoute = onlyDataCollection[key].id;
          break;
        }
      }
      return playerOwnerOfRoute;
    }

    public static ETeamLetters GetRoutesTeamID(int routeId) => SerializedDataManager.GetPlayersTeamID(SerializedDataManager.GetPlayerOwnerOfRoute(routeId));

    public static int RoutesCount => SerializedDataManager._routeDataManager.ReadOnlyDataCollection.Length;

    public static ReadOnlyArray<PlayerData> PlayersData => SerializedDataManager._playerDataManager.ReadOnlyDataCollection;
  }
}
