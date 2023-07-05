// Decompiled with JetBrains decompiler
// Type: MultiplayerRoomsPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.Networked;
using Photon.Realtime;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections.Generic;
using TB12.UI;
using UnityEngine;

public class MultiplayerRoomsPage : TabletPage, ILobbyCallbacks
{
  [SerializeField]
  private GameObject prefabListCell;
  [SerializeField]
  private TouchUI2DButton searchButton;
  [SerializeField]
  private TouchUI2DButton backButton;
  [SerializeField]
  private Transform parentListCells;
  [SerializeField]
  private MultiplayerLobbyStore _lobbyStore;
  [SerializeField]
  private PinView pinView;
  private List<RoomInfoCell> cells = new List<RoomInfoCell>();

  private void Awake()
  {
    this._pageType = TabletPage.Pages.MultiplayerJoin;
    if ((UnityEngine.Object) this.searchButton != (UnityEngine.Object) null)
      this.searchButton.onClick += new System.Action(this.HandleSearchButton);
    this.backButton.onClick += new System.Action(this.HandleBackButton);
  }

  private void OnEnable() => this.RefreshRoomsList();

  private void OnDisable() => this.ClearList();

  private void ClearList()
  {
    foreach (RoomInfoCell cell in this.cells)
    {
      if (!((UnityEngine.Object) cell == (UnityEngine.Object) null))
        UnityEngine.Object.Destroy((UnityEngine.Object) cell.gameObject);
    }
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this.searchButton != (UnityEngine.Object) null)
      this.searchButton.onClick -= new System.Action(this.HandleSearchButton);
    if (!((UnityEngine.Object) this.backButton != (UnityEngine.Object) null))
      return;
    this.backButton.onClick -= new System.Action(this.HandleBackButton);
  }

  public void RefreshRoomsList()
  {
    this.ClearList();
    this.cells = new List<RoomInfoCell>();
    foreach (RoomInfo topRoom in NetworkState.InstantiatedMultiplayerManager.GetTopRooms(200))
    {
      if (topRoom != null && (RuntimePlatform) topRoom.CustomProperties[(object) "a"] == Application.platform)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabListCell, Vector3.zero, Quaternion.identity);
        gameObject.transform.SetParent(this.parentListCells);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localEulerAngles = Vector3.zero;
        RoomInfoCell component = gameObject.GetComponent<RoomInfoCell>();
        component.roomsPage = this;
        component.Initialize(new RequestRoomInfo()
        {
          RoomName = topRoom.Name,
          MaxPlayersAmount = Convert.ToInt32(topRoom.MaxPlayers),
          PlayersAmount = topRoom.PlayerCount,
          HostName = (string) topRoom.CustomProperties[(object) "n"],
          GameTypeID = (string) topRoom.CustomProperties[(object) "g"],
          Password = (string) topRoom.CustomProperties[(object) "p"],
          StadiumName = (string) topRoom.CustomProperties[(object) "s"],
          TimeOfDay = (ETimeOfDay) topRoom.CustomProperties[(object) "t"]
        }, this.pinView);
        this.cells.Add(component);
      }
    }
  }

  public void OpenJoinMultiplayerLoadingPage() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.JoinMultiplayerLoading);

  private void HandleSearchButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MutiplayerFilter);

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MultiplayerMain);

  public void OnJoinedLobby()
  {
  }

  public void OnLeftLobby()
  {
  }

  public void OnRoomListUpdate(List<RoomInfo> roomList)
  {
    this._lobbyStore.UpdateRoomList(roomList);
    this.RefreshRoomsList();
  }

  public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
  {
  }
}
