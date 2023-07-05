// Decompiled with JetBrains decompiler
// Type: TB12.ReceiverGameScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DataSensitiveStructs_v5;
using FootballVR;
using FootballVR.AvatarSystem;
using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  public class ReceiverGameScene : MonoBehaviour
  {
    [SerializeField]
    private BallObject _ballPrefab;
    [SerializeField]
    private AvatarsManager _avatarsManager;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private RouteObject _routeObject;
    [SerializeField]
    private BallPrediction _ballPrediction;

    private ReceiverModeSettings _settings => ScriptableSingleton<ReceiverModeSettings>.Instance;

    public BallObject GameBall { get; private set; }

    public BallPrediction BallPrediction => this._ballPrediction;

    public RouteObject UserRoute => this._routeObject;

    private void Awake()
    {
      this._avatarsManager.OnAvatarsInitialized += new System.Action(this.HandleAvatarsInitialized);
      this._routeObject.gameObject.SetActive(false);
      this.GameBall = UnityEngine.Object.Instantiate<BallObject>(this._ballPrefab);
      this.GameBall.gameObject.SetActive(false);
      ScriptableSingleton<Gameboard>.Instance.football = this.GameBall.gameObject;
      SerializedDataManager.OnPlayLoaded += new Action<DataSensitiveStructs_v5.PlayData>(ScriptableSingleton<Gameboard>.Instance.ExtractPlayContext);
      this._settings.TrailWidthMultiplier.OnValueChanged += new Action<float>(this.HandleTrailWidthMultiplier);
      this.HandleTrailWidthMultiplier((float) this._settings.TrailWidthMultiplier);
    }

    private void HandleAvatarsInitialized()
    {
      this._avatarsManager.ShowAllAvatars();
      this._playbackInfo.StartPlayback();
    }

    private void HandleTrailWidthMultiplier(float width) => this._ballPrediction.SetWidth(width);

    public void DisplayUserRoute()
    {
      DataSensitiveStructs_v5.PlayerData assumedPlayerData = this._avatarsManager.GetUserAssumedPlayerData();
      if (assumedPlayerData.routeId <= 0)
      {
        Debug.LogError((object) "No routes for users player in receiver mode");
      }
      else
      {
        List<RouteData> routeDatas = new List<RouteData>();
        int startingFromRouteId = SerializedDataManager.GetRouteChainDataArrayStartingFromRouteId(assumedPlayerData.routeId, ref routeDatas);
        List<Vector2> points = new List<Vector2>();
        Vector2 zero = Vector2.zero;
        for (int index1 = 0; index1 < startingFromRouteId; ++index1)
        {
          List<Vector2> coordinates = routeDatas[index1].coordinates;
          for (int index2 = 0; index2 < coordinates.Count; ++index2)
            points.Add(coordinates[index2] + zero);
          zero += coordinates[coordinates.Count - 1];
        }
        Vector3 position = this._avatarsManager.GetUserPlayerPosition().SetY(0.02f);
        Quaternion rotation = Quaternion.Euler(new Vector3(90f, this._avatarsManager.GetUserPlayerOrientation(), 90f));
        this._routeObject.gameObject.SetActive(true);
        this._routeObject.transform.SetPositionAndRotation(position, rotation);
        this._routeObject.Initialize(points, this._settings.routeWidth, !this._settings.useRouteTipAsProjectedPosition);
      }
    }

    private void OnDestroy()
    {
      this._avatarsManager.OnAvatarsInitialized -= new System.Action(this.HandleAvatarsInitialized);
      this._settings.TrailWidthMultiplier.OnValueChanged -= new Action<float>(this.HandleTrailWidthMultiplier);
      SerializedDataManager.OnPlayLoaded -= new Action<DataSensitiveStructs_v5.PlayData>(ScriptableSingleton<Gameboard>.Instance.ExtractPlayContext);
      if (!((UnityEngine.Object) this.GameBall != (UnityEngine.Object) null) || !((UnityEngine.Object) this.GameBall.gameObject != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.GameBall.gameObject);
    }

    public void Cleanup()
    {
      this._routeObject.gameObject.SetActive(false);
      this.GameBall.gameObject.SetActive(false);
      this._ballPrediction.ResetState();
    }
  }
}
