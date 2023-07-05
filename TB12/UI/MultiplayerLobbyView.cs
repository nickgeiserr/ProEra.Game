// Decompiled with JetBrains decompiler
// Type: TB12.UI.MultiplayerLobbyView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.Networked;
using Framework.UI;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class MultiplayerLobbyView : UIView
  {
    [Header("Main menu")]
    [Space(10f)]
    [SerializeField]
    private GameObject _mainMenuPanel;
    [SerializeField]
    private TouchButton _customizeButton;
    [SerializeField]
    private TouchButton _createRoomMenuButton;
    [SerializeField]
    private TouchButton _joinRoomMenuButton;
    [SerializeField]
    private ButtonText _serverButtonText;
    [SerializeField]
    private TouchButton _selectServerButton;
    [SerializeField]
    private TouchButton _adjustHeightButton;
    [Header("Create room menu")]
    [Space(10f)]
    [SerializeField]
    private GameObject _createRoomMenuPanel;
    [SerializeField]
    private TouchButton _createRoomButton;
    [SerializeField]
    private TouchButton _pinButton;
    [SerializeField]
    private TouchButton[] _lobbyFocusButton;
    [SerializeField]
    private TouchButton[] _lobbySizeButton;
    [SerializeField]
    private TMP_Text labelLobbySize;
    [Header("Select room menu")]
    [Space(10f)]
    [SerializeField]
    private GameObject _selectRoomMenuPanel;
    [SerializeField]
    private GameObject P_lobbyCell;
    [SerializeField]
    private Transform parentLobbyCells;
    [SerializeField]
    private MultiplayerLobbyStore _lobbyStore;
    [SerializeField]
    private TouchButton _searchRoomButton;
    private List<RoomCellView> listLobbyCells = new List<RoomCellView>();
    [Space(10f)]
    [SerializeField]
    private TouchButton[] _backButtons;
    [SerializeField]
    private PinView _pinView;
    private int prevLobbyFocus;
    private int prevSizeButton;

    public override Enum ViewId { get; } = (Enum) ELockerRoomUI.kMultiplayer;

    private static RequestRoomInfo requestRoomInfo => NetworkState.requestRoomInfo;

    protected override void OnInitialize()
    {
      this._laserIsAlwaysEnabled = true;
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) UIHandle.Link((IButton) this._createRoomMenuButton, new Action(this.HandleCreateRoomMenu)),
        (EventHandle) UIHandle.Link((IButton) this._joinRoomMenuButton, new Action(this.HandleJoinRoomMenu)),
        (EventHandle) UIHandle.Link((IButton) this._selectServerButton, new Action(this.HandleServerButton)),
        (EventHandle) UIHandle.Link((IButton) this._customizeButton, new Action(this.HandleCustomize)),
        (EventHandle) UIHandle.Link((IButton) this._adjustHeightButton, new Action(this.HandleHeightAdjustment)),
        (EventHandle) UIHandle.Link((IButton) this._createRoomButton, new Action(this.HandleCreateRoom)),
        (EventHandle) UIHandle.Link((IButton) this._pinButton, new Action(this.HandlePinButton)),
        (EventHandle) UIHandle.Link((IButton) this._searchRoomButton, new Action(this.HandleSearchRoom))
      });
      foreach (TouchButton backButton in this._backButtons)
      {
        if (!((UnityEngine.Object) backButton == (UnityEngine.Object) null))
          this.linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) backButton, new Action(this.HandleBack)));
      }
      this.prevLobbyFocus = Convert.ToInt32(MultiplayerLobbyView.requestRoomInfo.GameTypeID);
      this.prevSizeButton = MultiplayerLobbyView.requestRoomInfo.MaxPlayersAmount;
      if (this._lobbyFocusButton.Length > this.prevLobbyFocus)
        this._lobbyFocusButton[this.prevLobbyFocus]?.HighlighAsSelected(true, Color.white, Color.yellow);
      if (this._lobbySizeButton.Length <= this.prevSizeButton)
        return;
      this._lobbySizeButton[this.prevSizeButton]?.HighlighAsSelected(true, Color.white, Color.yellow);
    }

    private void HandleServerButton()
    {
      if (NetworkState.RegionData == null)
        return;
      UIDispatch.LockerRoomScreen.DisplayView(ELockerRoomUI.kSelectServer);
    }

    private void HandleJoinRoomMenu()
    {
      this._selectRoomMenuPanel.SetActive(true);
      this._mainMenuPanel.SetActive(false);
      this.RefreshRoomsList();
    }

    private void HandleCreateRoomMenu()
    {
      this._createRoomMenuPanel.SetActive(true);
      this._mainMenuPanel.SetActive(false);
    }

    private void HandlePinButton() => UIDispatch.LockerRoomScreen.DisplayDialog(ELockerRoomUI.kEnterPin);

    public void OnAddedPin(string value) => NetworkState.requestRoomInfo.Password = value;

    private void HandleRoomSize(TouchButton target)
    {
      this._lobbySizeButton[this.prevSizeButton].HighlighAsSelected(false, Color.white, Color.yellow);
      this.prevSizeButton = target.GetID() - 2;
      this._lobbySizeButton[this.prevSizeButton].HighlighAsSelected(true, Color.white, Color.yellow);
      int num = Mathf.Clamp(target.GetID(), 2, 8);
      this.RefreshLobbySizeLabel(num);
      MultiplayerLobbyView.requestRoomInfo.MaxPlayersAmount = num;
    }

    private void HandleLobbyFocus(TouchButton target)
    {
      this._lobbyFocusButton[this.prevLobbyFocus].HighlighAsSelected(false, Color.white, Color.yellow);
      this.prevLobbyFocus = target.GetID();
      this._lobbyFocusButton[this.prevLobbyFocus].HighlighAsSelected(true, Color.white, Color.yellow);
      MultiplayerLobbyView.requestRoomInfo.GameTypeID = target.GetID().ToString();
    }

    private void HandleSearchRoom() => UIDispatch.LockerRoomScreen.DisplayDialog(ELockerRoomUI.kSearchRoom);

    private void HandleCreateRoom() => AppEvents.LoadMultiplayer.Trigger(MultiplayerManager.GetMultiplayerAppStateByID(NetworkState.requestRoomInfo.GameTypeID, true));

    private void HandleCustomize()
    {
      this.Hide();
      UIDispatch.LockerRoomScreen.DisplayView(ELockerRoomUI.kCustomizeMain);
    }

    private void HandleHeightAdjustment() => VREvents.AdjustPlayerHeight.Trigger();

    protected override void WillAppear()
    {
      foreach (TouchButton touchButton in this._lobbyFocusButton)
      {
        if (!((UnityEngine.Object) touchButton == (UnityEngine.Object) null))
          touchButton.onClickInfo += new Action<TouchButton>(this.HandleLobbyFocus);
      }
      foreach (TouchButton touchButton in this._lobbySizeButton)
      {
        if (!((UnityEngine.Object) touchButton == (UnityEngine.Object) null))
          touchButton.onClickInfo += new Action<TouchButton>(this.HandleRoomSize);
      }
      this._serverButtonText.text = "Change Server\r\n[" + NetworkState.Region.ToUpperInvariant() + "]";
      MultiplayerLobbyView.requestRoomInfo.MaxPlayersAmount = MultiplayerLobbyView.requestRoomInfo.MaxPlayersAmount <= 2 ? 2 : MultiplayerLobbyView.requestRoomInfo.MaxPlayersAmount;
      this.RefreshLobbySizeLabel(MultiplayerLobbyView.requestRoomInfo.MaxPlayersAmount);
      this.prevSizeButton = MultiplayerLobbyView.requestRoomInfo.MaxPlayersAmount;
      this._lobbySizeButton[this.prevSizeButton].HighlighAsSelected(true, Color.white, Color.yellow);
    }

    protected override void WillDisappear()
    {
      foreach (TouchButton touchButton in this._lobbyFocusButton)
      {
        if (!((UnityEngine.Object) touchButton == (UnityEngine.Object) null))
          touchButton.onClickInfo -= new Action<TouchButton>(this.HandleLobbyFocus);
      }
      foreach (TouchButton touchButton in this._lobbySizeButton)
      {
        if (!((UnityEngine.Object) touchButton == (UnityEngine.Object) null))
          touchButton.onClickInfo -= new Action<TouchButton>(this.HandleRoomSize);
      }
    }

    private void HandleBack()
    {
      if (this._mainMenuPanel.activeSelf)
      {
        AppState.Mode.SetValue(EMode.kUnknown);
        AppEvents.LoadMainMenu.Trigger();
      }
      if (this._createRoomMenuPanel.activeSelf)
      {
        this._createRoomMenuPanel.SetActive(false);
        this._mainMenuPanel.SetActive(true);
      }
      if (!this._selectRoomMenuPanel.activeSelf)
        return;
      this._selectRoomMenuPanel.SetActive(false);
      this._mainMenuPanel.SetActive(true);
    }

    public void RefreshLobbySizeLabel(int value) => this.labelLobbySize.text = "Lobby size: " + value.ToString();

    public void RefreshRoomsList()
    {
      foreach (RoomCellView listLobbyCell in this.listLobbyCells)
      {
        if ((UnityEngine.Object) listLobbyCell != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) listLobbyCell.gameObject);
      }
      this.listLobbyCells = new List<RoomCellView>();
      foreach (RoomInfo room in (IEnumerable<RoomInfo>) this._lobbyStore.Rooms)
      {
        if (room != null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.P_lobbyCell, Vector3.zero, Quaternion.identity);
          gameObject.transform.SetParent(this.parentLobbyCells);
          gameObject.transform.localPosition = Vector3.zero;
          gameObject.transform.localScale = Vector3.one;
          gameObject.transform.localEulerAngles = Vector3.zero;
          RoomCellView component = gameObject.GetComponent<RoomCellView>();
          component.Initialize(new RequestRoomInfo()
          {
            RoomName = room.Name,
            MaxPlayersAmount = Convert.ToInt32(room.MaxPlayers),
            PlayersAmount = room.PlayerCount,
            HostName = (string) room.CustomProperties[(object) "n"],
            GameTypeID = (string) room.CustomProperties[(object) "g"],
            Password = (string) room.CustomProperties[(object) "p"],
            StadiumName = (string) room.CustomProperties[(object) "s"],
            TimeOfDay = (ETimeOfDay) room.CustomProperties[(object) "t"]
          }, this._pinView);
          this.listLobbyCells.Add(component);
        }
      }
    }
  }
}
