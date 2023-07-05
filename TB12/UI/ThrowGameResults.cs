// Decompiled with JetBrains decompiler
// Type: TB12.UI.ThrowGameResults
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
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TB12.UI
{
  public class ThrowGameResults : UIView
  {
    [SerializeField]
    private MultiplayerStore _store;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private Image[] _images;
    [SerializeField]
    private TextMeshProUGUI[] _scoreTexts;
    [SerializeField]
    private TextMeshProUGUI[] _nameTexts;
    [SerializeField]
    private TextMeshProUGUI countdownTime;
    [SerializeField]
    private TouchButton leaveLobbyBtn;
    [SerializeField]
    private TouchButton returnToLobbyBtn;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public override Enum ViewId { get; } = (Enum) EScreens.kMultiplayerThrowResults;

    private static RequestRoomInfo requestRoomInfo => NetworkState.requestRoomInfo;

    private void Start()
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
      ThrowGameResults.requestRoomInfo.GameTypeID = "3";
      Debug.Log((object) "Loading Multiplayer ID: 3");
      VRState.PauseMenu.SetValue(false);
      if (!PhotonNetwork.IsMasterClient || !((UnityEngine.Object) NetworkState.InstantiatedMultiplayerManager != (UnityEngine.Object) null))
        return;
      NetworkState.InstantiatedMultiplayerManager.UpdateRoom(ThrowGameResults.requestRoomInfo.GameTypeID, NetworkState.requestRoomInfo.HostName, NetworkState.requestRoomInfo.Password, NetworkState.requestRoomInfo.StadiumName, (int) NetworkState.requestRoomInfo.TimeOfDay, NetworkState.requestRoomInfo.Platform);
      MultiplayerEvents.LoadMultiplayerGame.Trigger(EAppState.kMultiplayerLobby.ToString());
    }

    protected override void WillAppear()
    {
      this.UpdateDetails();
      this.BeginReturnCountdown();
    }

    private void UpdateDetails()
    {
      List<PlayerStatsData> playerDatas = this._store.PlayerDatas;
      playerDatas.Sort((Comparison<PlayerStatsData>) ((x, y) => y.score.CompareTo(x.score)));
      int num = Mathf.Min(playerDatas.Count, 8);
      int length = this._scoreTexts.Length;
      for (int index = 0; index < length; ++index)
      {
        bool flag = index < num;
        this._nameTexts[index].gameObject.SetActive(flag);
        this._scoreTexts[index].gameObject.SetActive(flag);
        this._images[index].gameObject.SetActive(flag);
        if (flag)
        {
          PlayerStatsData playerStatsData = playerDatas[index];
          PlayerCustomization playerCustomization;
          string str = this._playerProfile.Profiles.TryGetValue(playerStatsData.playerId, out playerCustomization) ? (string) playerCustomization.LastName : "Player " + (index + 1).ToString();
          this._nameTexts[index].text = str;
          this._scoreTexts[index].text = playerStatsData.score.ToString();
          this._nameTexts[index].color = playerDatas[index].isLocalPlayer ? Common.SP_GREEN : Common.SP_WHITE;
        }
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
