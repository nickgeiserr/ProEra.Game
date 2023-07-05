// Decompiled with JetBrains decompiler
// Type: MultiplayerMainPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using FootballVR.UI;
using Framework;
using Framework.Networked;
using Photon.Realtime;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiplayerMainPage : TabletPage
{
  [SerializeField]
  private TouchUI2DButton quickJoinRoomButton;
  [SerializeField]
  private TouchUI2DButton joinRoomButton;
  [SerializeField]
  private TouchUI2DButton createRoomButton;
  [SerializeField]
  private TouchUI2DButton serverButton;
  [SerializeField]
  private TouchUI2DButton backButton;
  [SerializeField]
  private MultiplayerLobbyStore _lobbyStore;
  private List<RoomInfo> roomAttemptCandidates = new List<RoomInfo>();

  private void Awake()
  {
    this._pageType = TabletPage.Pages.MultiplayerMain;
    this.quickJoinRoomButton.onClick += new System.Action(this.HandleQuickJoinButton);
    this.joinRoomButton.onClick += new System.Action(this.HandleJoinRoomButton);
    this.createRoomButton.onClick += new System.Action(this.HandleCreateRoomButton);
    this.serverButton.onClick += new System.Action(this.HandleServerButton);
    this.backButton.onClick += new System.Action(this.HandleBackButton);
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this.quickJoinRoomButton != (UnityEngine.Object) null)
      this.quickJoinRoomButton.onClick -= new System.Action(this.HandleQuickJoinButton);
    if ((UnityEngine.Object) this.joinRoomButton != (UnityEngine.Object) null)
      this.joinRoomButton.onClick -= new System.Action(this.HandleJoinRoomButton);
    if ((UnityEngine.Object) this.createRoomButton != (UnityEngine.Object) null)
      this.createRoomButton.onClick -= new System.Action(this.HandleCreateRoomButton);
    if ((UnityEngine.Object) this.serverButton != (UnityEngine.Object) null)
      this.serverButton.onClick -= new System.Action(this.HandleServerButton);
    if (!((UnityEngine.Object) this.backButton != (UnityEngine.Object) null))
      return;
    this.backButton.onClick -= new System.Action(this.HandleBackButton);
  }

  private void OnEnable() => PersistentSingleton<BallsContainerManager>.Instance.IsNetworked = true;

  private void HandleJoinRoomButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MultiplayerJoin);

  private void HandleQuickJoinButton()
  {
    foreach (RoomInfo roomInfo in (IEnumerable<RoomInfo>) this._lobbyStore.Rooms.OrderBy<RoomInfo, byte>((Func<RoomInfo, byte>) (x => x.MaxPlayers)).ThenBy<RoomInfo, int>((Func<RoomInfo, int>) (x => x.PlayerCount)))
    {
      if (Application.platform == (RuntimePlatform) roomInfo.CustomProperties[(object) "a"])
      {
        RequestRoomInfo requestRoomInfo = new RequestRoomInfo();
        requestRoomInfo.RoomName = roomInfo.Name;
        requestRoomInfo.MaxPlayersAmount = Convert.ToInt32(roomInfo.MaxPlayers);
        requestRoomInfo.PlayersAmount = roomInfo.PlayerCount;
        requestRoomInfo.HostName = (string) roomInfo.CustomProperties[(object) "n"];
        requestRoomInfo.GameTypeID = (string) roomInfo.CustomProperties[(object) "g"];
        requestRoomInfo.Password = (string) roomInfo.CustomProperties[(object) "p"];
        requestRoomInfo.StadiumName = (string) roomInfo.CustomProperties[(object) "s"];
        requestRoomInfo.TimeOfDay = (ETimeOfDay) roomInfo.CustomProperties[(object) "t"];
        requestRoomInfo.Platform = (int) roomInfo.CustomProperties[(object) "a"];
        requestRoomInfo.SteamLobbyID = ulong.Parse(roomInfo.CustomProperties[(object) "sli"].ToString());
        if (!requestRoomInfo.UsePassword && requestRoomInfo.PlayersAmount < requestRoomInfo.MaxPlayersAmount)
        {
          NetworkState.requestRoomInfo = requestRoomInfo;
          PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
          MultiplayerEvents.LoadMultiplayerGame.Trigger(MultiplayerManager.GetMultiplayerAppStateByID(requestRoomInfo.GameTypeID, true));
          break;
        }
      }
    }
  }

  private void HandleCreateRoomButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MultiplayerCreate);

  private void HandleServerButton()
  {
  }

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);
}
