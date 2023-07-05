// Decompiled with JetBrains decompiler
// Type: TB12.UI.MultiplayerDebugScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework.Data;
using Framework.Networked;
using Framework.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TB12.UI
{
  public class MultiplayerDebugScreen : UIView, IConnectionCallbacks, IMatchmakingCallbacks
  {
    [Header("Developer References")]
    [SerializeField]
    private MultiplayerStore _store;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private PlayerDebugPanel _debugPanel;
    [SerializeField]
    private Transform _playerStatsDataPanelTransform;
    [Header("UI Links")]
    [SerializeField]
    private Image _connectedLight;
    [SerializeField]
    private Image _inRoomLight;
    [SerializeField]
    private TMP_Text _currentIDText;
    [SerializeField]
    private TMP_Text _currentNameText;
    [SerializeField]
    private TMP_Text _currentRoomText;
    [SerializeField]
    private TMP_Text _currentTimeText;
    [SerializeField]
    private TMP_Text _currentHostText;
    [SerializeField]
    private TMP_Text _currentPingText;
    [SerializeField]
    private TMP_Text _currentGamestateText;
    [SerializeField]
    private TMP_Text _currentPlayerCountText;
    [SerializeField]
    private List<PlayerDebugPanel> _playerDebugPanelList = new List<PlayerDebugPanel>();
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public override Enum ViewId { get; } = (Enum) EScreens.kMultiplayerDebug;

    private void Start()
    {
      this.SetLight(this._inRoomLight, true);
      this.SetText(this._currentIDText, "ID: " + PhotonNetwork.LocalPlayer.ActorNumber.ToString());
      this.SetText(this._currentNameText, "Player");
      this.SetText(this._currentRoomText, PhotonNetwork.CurrentRoom.Name);
      this.SetText(this._currentTimeText, "TIME LEFT: 0");
      this.SetText(this._currentHostText, "Host ID: " + PhotonNetwork.MasterClient.ActorNumber.ToString());
      this.SetLight(this._connectedLight, PhotonNetwork.IsConnectedAndReady);
      this.SetLight(this._inRoomLight, PhotonNetwork.InRoom);
      Debug.Log((object) "Set initial values for debug screen");
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this._store.OnDataChanged.Link(new System.Action(this.HandleDataChanged))
      });
      Debug.Log((object) "Set dynamic links for debug screen changes");
      foreach (string registeredRoomVariable in NetworkedEventsHandler.RegisteredRoomVariables)
        Debug.Log((object) ("<b><color=green>" + registeredRoomVariable + "</color></b>"));
    }

    private void LateUpdate() => this.SetText(this._currentPingText, "Ping: " + PhotonNetwork.GetPing().ToString());

    public void OnConnected() => this.SetLight(this._connectedLight, true);

    public void OnDisconnected(DisconnectCause cause) => this.ResetScreen();

    public void OnJoinedRoom()
    {
      this.SetLight(this._inRoomLight, true);
      this.SetText(this._currentRoomText, "Room: " + PhotonNetwork.CurrentRoom.Name);
      this.SetText(this._currentHostText, PhotonNetwork.MasterClient.ActorNumber.ToString());
      this.SetText(this._currentPlayerCountText, string.Format("Player Count: {0}", (object) this._store.PlayerDatas.Count));
      this.BuildPlayerDebugPanels();
    }

    public void OnLeftRoom()
    {
      this.SetLight(this._inRoomLight, false);
      this.SetText(this._currentRoomText, string.Empty);
      this.SetText(this._currentHostText, string.Empty);
    }

    private void OnDestroy() => this._linksHandler.Clear();

    public void SetLight(Image lightToChange, bool state) => lightToChange.color = state ? Color.green : Color.red;

    public void SetText(TMP_Text textToChange, string text) => textToChange.text = text;

    private void HandleState(bool state) => this.gameObject.SetActive(state);

    private void HandleDataChanged() => this.UpdateData();

    private void OnEnable() => this.BuildPlayerDebugPanels();

    private void BuildPlayerDebugPanels()
    {
      this.ClearDebugPanels();
      if (this._store.PlayerDatas.Count == 0)
        return;
      this.SetText(this._currentPlayerCountText, string.Format("Player Count: {0}", (object) this._store.PlayerDatas.Count));
      foreach (PlayerStatsData playerData in this._store.PlayerDatas)
      {
        PlayerDebugPanel component;
        if (UnityEngine.Object.Instantiate<GameObject>(this._debugPanel.gameObject, this._playerStatsDataPanelTransform).TryGetComponent<PlayerDebugPanel>(out component))
        {
          Debug.Log((object) string.Format("Created debug panel for Player {0}", (object) playerData.playerId));
          component.UpdateWithData(playerData);
          this._playerDebugPanelList.Add(component);
        }
      }
    }

    private void UpdateData()
    {
      if (this._store.PlayerDatas.Count != this._playerDebugPanelList.Count)
      {
        this.BuildPlayerDebugPanels();
      }
      else
      {
        if (this._playerDebugPanelList.Count == 0)
          return;
        for (int index = 0; index < this._playerDebugPanelList.Count; ++index)
          this._playerDebugPanelList[index].UpdateWithData(this._store.GetOrCreatePlayerData(int.Parse(this._playerDebugPanelList[index].ID)));
      }
    }

    private void ClearDebugPanels()
    {
      if (this._playerDebugPanelList.Count == 0)
        return;
      foreach (Component playerDebugPanel in this._playerDebugPanelList)
        UnityEngine.Object.Destroy((UnityEngine.Object) playerDebugPanel.gameObject);
      this._playerDebugPanelList.Clear();
    }

    public void ResetScreen()
    {
      this.SetLight(this._connectedLight, false);
      this.SetLight(this._inRoomLight, false);
      this.SetText(this._currentHostText, string.Empty);
      this.SetText(this._currentRoomText, string.Empty);
      this.ClearDebugPanels();
    }

    public void OnConnectedToMaster()
    {
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnCreatedRoom()
    {
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
    }
  }
}
