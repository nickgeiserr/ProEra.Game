// Decompiled with JetBrains decompiler
// Type: FootballVR.QuickStart
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using Framework.Networked;
using Photon.Pun;
using System.Collections;
using TB12;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FootballVR
{
  public class QuickStart : PersistentSingleton<QuickStart>
  {
    [SerializeField]
    private bool _enableQuickStart;
    [SerializeField]
    private bool _disablePlayerController;
    [Header("State Data To Load")]
    public EAppState appGameState = EAppState.kAxisGame;
    public ETimeOfDay appTimeOfDay = ETimeOfDay.Clear;
    public EMode appMode = EMode.kSolo;
    [Header("Multiplayer Settings")]
    public MultiplayerManager multiplayerManagerPrefab;
    public string roomName = "Test";
    public string password = "";
    [Range(2f, 8f)]
    public int expectedPlayerCount = 8;

    public bool EnableQuickStart => this._enableQuickStart;

    public bool DisablePlayerController => this._disablePlayerController;

    protected override void Awake() => TransitionController.skipStadiumIntro = true;

    public IEnumerator RunSetupRoutine()
    {
      VRState.HandsVisible.SetValue(true);
      yield return (object) SceneManager.LoadSceneAsync("UI-Activator", LoadSceneMode.Additive);
    }

    public void QuickJoinUsingSettings()
    {
      AppState.AppMode.SetValue(EAppMode.Game);
      if (this.appMode == EMode.kMultiplayer)
        this.QuickJoinMultiplayer(this.appGameState, this.appTimeOfDay, this.roomName, this.password, this.expectedPlayerCount);
      else
        this.QuickJoinSingleplayer(this.appGameState, this.appTimeOfDay);
    }

    public void QuickJoinSingleplayer(EAppState state, ETimeOfDay timeOfDay = ETimeOfDay.Clear, string stadium = null)
    {
      AppState.AppMode.SetValue(EAppMode.Game);
      AppEvents.LoadState(state, timeOfDay);
    }

    public void QuickJoinMultiplayer(
      EAppState stateToJoin,
      ETimeOfDay timeOfDay = ETimeOfDay.Clear,
      string roomName = "Test",
      string password = "",
      int expectedPlayerCount = 8)
    {
      if ((Object) NetworkState.InstantiatedMultiplayerManager != (Object) null)
        NetworkState.InstantiatedMultiplayerManager = Object.Instantiate<MultiplayerManager>(this.multiplayerManagerPrefab, (Transform) null);
      PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "us";
      AppState.Mode.SetValue(EMode.kMultiplayer);
      MultiplayerState.ExpectedPlayerCount = expectedPlayerCount;
      NetworkState.requestRoomInfo.RoomName = roomName;
      NetworkState.requestRoomInfo.Password = password;
      NetworkState.InstantiatedMultiplayerManager.ConnectToNetwork();
      AppEvents.LoadState(stateToJoin, timeOfDay);
    }
  }
}
