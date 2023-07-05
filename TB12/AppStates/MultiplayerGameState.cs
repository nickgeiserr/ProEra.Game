// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MultiplayerGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.Networked;
using Framework.StateManagement;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.AppStates
{
  public class MultiplayerGameState : GameState
  {
    private UniformStore _uniformStore;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private MultiplayerStore _store;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private RoutineHandle _levelFinishedLoading = new RoutineHandle();

    public override EAppState Id { get; } = EAppState.kMultiplayerLobby;

    public override bool clearFadeOnEntry => true;

    protected override void OnEnterState()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      StadiumController objectOfType = UnityEngine.Object.FindObjectOfType<StadiumController>();
      objectOfType.ChangeStadiumID(NetworkState.requestRoomInfo.StadiumName);
      objectOfType.LoadStadiumAndConfig(NetworkState.requestRoomInfo.TimeOfDay, NetworkState.requestRoomInfo.StadiumName);
      base.OnEnterState();
      VRState.ForceSyncThrows = true;
      MultiplayerEvents.BackToLobby.enabled = true;
      MultiplayerEvents.StartThrowChallenge.enabled = false;
      AppSounds.CrowdSound.SetValue(false);
      VRState.LocomotionEnabled.SetValue(false);
      Debug.Log((object) ("MP Locomotion: " + VRState.LocomotionEnabled.Value.ToString()));
      VRState.HelmetEnabled.SetValue(true);
      AppState.HandUI.SetValue(true);
      WorldState.CrowdEnabled.SetValue(true);
      WorldState.Raining.SetValue(false);
      this._linksHandler.AddLinks((IReadOnlyList<EventHandle>) new List<EventHandle>()
      {
        PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.Link<bool>(new Action<bool>(this.HandleStateTransition))
      });
      this._uniformStore.SetNamesAndNumbersVisibility(true);
      this._throwManager.ForceAutoAim = false;
      this._throwManager.SetCanThrowBall(true);
    }

    public override IEnumerator Load()
    {
      MultiplayerGameState multiplayerGameState = this;
      if (!(bool) NetworkState.InRoom)
        multiplayerGameState._store.ResetStore();
      // ISSUE: reference to a compiler-generated method
      yield return (object) multiplayerGameState.\u003C\u003En__0();
      if (!PhotonNetwork.InRoom)
      {
        MultiplayerManager.ConnectToRoom(NetworkState.requestRoomInfo.RoomName);
        float timeout = Time.time + 6f;
        while (!PhotonNetwork.InRoom && (double) Time.time < (double) timeout)
          yield return (object) null;
        multiplayerGameState._store.LocalPlayerId = PhotonNetwork.LocalPlayer.ActorNumber;
        AppState.HandUI.SetValue(true);
        MultiplayerState.Leaderboards.SetValue(true);
        if ((double) Time.time > (double) timeout)
          Debug.LogError((object) "Timeout connecting to room.");
      }
      if (PhotonNetwork.IsMasterClient)
        NetworkState.InstantiatedMultiplayerManager.UpdateRoom(NetworkState.InstantiatedMultiplayerManager.GetMultiplayerIDByAppState(multiplayerGameState.Id.ToString()), NetworkState.requestRoomInfo.HostName, NetworkState.requestRoomInfo.Password, NetworkState.requestRoomInfo.StadiumName, (int) NetworkState.requestRoomInfo.TimeOfDay, NetworkState.requestRoomInfo.Platform, NetworkState.requestRoomInfo.SteamLobbyID);
      AppState.GameInfoUI.SetValue(false);
      AppState.GameInfoUI.SetValue(false);
    }

    private void HandleStateTransition(bool inTransition)
    {
      if (inTransition || PhotonNetwork.CurrentRoom == null)
        return;
      string str = PhotonNetwork.CurrentRoom.CustomProperties[(object) "g"].ToString();
      if (str != NetworkState.requestRoomInfo.GameTypeID)
      {
        NetworkState.requestRoomInfo.GameTypeID = str;
        GameManager.Instance.ClientReloadMPScene();
      }
      else
        this.OnEnterStateFinished?.Trigger();
    }

    public override void WillExit()
    {
      MultiplayerState.Leaderboards.SetValue(false);
      this._throwManager.Clear();
    }

    protected override void OnExitState()
    {
      base.OnExitState();
      this._store.ResetStore();
      VRState.ForceSyncThrows = false;
      MultiplayerEvents.StartThrowChallenge.enabled = true;
      AppSounds.CrowdSound.SetValue(true);
      WorldState.CrowdEnabled.SetValue(false);
      Debug.Log((object) ("MP Locomotion: " + VRState.LocomotionEnabled.Value.ToString()));
      VRState.LocomotionEnabled.SetValue(false);
      VRState.HelmetEnabled.SetValue(false);
      VRState.BigSizeMode.SetValue(false);
      AppState.HandUI.SetValue(false);
      PersistentSingleton<GamePlayerController>.Instance.SetMovementLimits();
      if (PhotonNetwork.IsMasterClient)
        MultiplayerEvents.DeleteAllUI.Trigger();
      this._throwManager.Settings.AutoAimSettings.AutoAimEnabled = true;
      this._throwManager.ForceAutoAim = true;
      this._linksHandler.Clear();
    }
  }
}
