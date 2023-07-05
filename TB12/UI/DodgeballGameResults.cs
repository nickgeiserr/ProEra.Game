// Decompiled with JetBrains decompiler
// Type: TB12.UI.DodgeballGameResults
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using FootballVR.UI;
using Framework.Data;
using Framework.Networked;
using Framework.UI;
using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class DodgeballGameResults : UIView
  {
    [SerializeField]
    private MultiplayerStore _store;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private TextMeshProUGUI winnerText;
    [SerializeField]
    private TextMeshProUGUI loserText;
    [SerializeField]
    private TextMeshProUGUI countdownTime;
    [SerializeField]
    private TouchButton leaveLobbyBtn;
    [SerializeField]
    private TouchButton returnToLobbyBtn;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _routineHandle = new RoutineHandle();

    public override Enum ViewId { get; } = (Enum) EScreens.kMultiplayerDodgeballResults;

    private static RequestRoomInfo requestRoomInfo => NetworkState.requestRoomInfo;

    private void Awake()
    {
      this._linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.leaveLobbyBtn, new System.Action(this.LeaveLobby)));
      this._linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.returnToLobbyBtn, new System.Action(this.ReturnToLobby)));
    }

    protected override void OnInitialize() => this._laserIsAlwaysEnabled = true;

    private void LeaveLobby()
    {
      NetworkState.requestRoomInfo.Clear();
      MultiplayerManager.LeaveRoom();
    }

    private void ReturnToLobby()
    {
      DodgeballGameResults.requestRoomInfo.GameTypeID = "3";
      Debug.Log((object) "Loading Multiplayer ID: 3");
      this.winnerText.gameObject.SetActive(false);
      this.loserText.gameObject.SetActive(false);
      VRState.PauseMenu.SetValue(false);
      if (!PhotonNetwork.IsMasterClient || !((UnityEngine.Object) NetworkState.InstantiatedMultiplayerManager != (UnityEngine.Object) null))
        return;
      NetworkState.InstantiatedMultiplayerManager.UpdateRoom(DodgeballGameResults.requestRoomInfo.GameTypeID, NetworkState.requestRoomInfo.HostName, NetworkState.requestRoomInfo.Password, NetworkState.requestRoomInfo.StadiumName, (int) NetworkState.requestRoomInfo.TimeOfDay, NetworkState.requestRoomInfo.Platform);
      MultiplayerEvents.LoadMultiplayerGame.Trigger(EAppState.kMultiplayerLobby.ToString());
    }

    protected override void WillAppear()
    {
      this.winnerText.gameObject.SetActive(false);
      this.loserText.gameObject.SetActive(false);
      this.UpdateDetails(MultiplayerDodgeballFlow.HasWon);
      this.BeginReturnCountdown();
    }

    private void UpdateDetails(bool hasWon)
    {
      if (hasWon)
      {
        this.winnerText.gameObject.SetActive(true);
        this.loserText.gameObject.SetActive(false);
      }
      else
      {
        this.winnerText.gameObject.SetActive(false);
        this.loserText.gameObject.SetActive(true);
        this.winnerText.gameObject.SetActive(false);
      }
    }

    private void BeginReturnCountdown() => this._routineHandle.Run(this.BeginReturnCountdownRoutine());

    private IEnumerator BeginReturnCountdownRoutine()
    {
      WaitForSeconds wait = new WaitForSeconds(1f);
      for (int countdownToReturn = 10; countdownToReturn > 0; --countdownToReturn)
      {
        this.countdownTime.text = countdownToReturn.ToString();
        yield return (object) wait;
      }
      this.ReturnToLobby();
    }
  }
}
