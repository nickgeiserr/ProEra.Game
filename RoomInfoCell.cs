// Decompiled with JetBrains decompiler
// Type: RoomInfoCell
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.Networked;
using System;
using System.Collections.Generic;
using TB12.UI;
using TMPro;
using UnityEngine;

public class RoomInfoCell : MonoBehaviour
{
  [SerializeField]
  private TMP_Text labelFans;
  [SerializeField]
  private TMP_Text labelHost;
  [SerializeField]
  private TMP_Text labelFocus;
  [SerializeField]
  private TMP_Text labelProtected;
  [SerializeField]
  private TouchUI2DButton joinButton;
  public MultiplayerRoomsPage roomsPage;
  private RequestRoomInfo _roomInfo = new RequestRoomInfo();
  private PinView _pinView;
  private readonly LinksHandler _linksHandler = new LinksHandler();

  private void Awake()
  {
    this._linksHandler.SetLinks(new List<EventHandle>()
    {
      AppEvents.PinCodeSubmitted.Link<string>(new Action<string>(this.GoToRoomWithPIN))
    });
    this.joinButton.onClick += new System.Action(this.OnClickJoin);
  }

  private void OnDestroy() => this.joinButton.onClick -= new System.Action(this.OnClickJoin);

  private void OnClickJoin()
  {
    if (this._roomInfo.UsePassword)
      this._pinView.Show();
    else
      this.GoToRoom();
  }

  private void GoToRoomWithPIN(string pin)
  {
    Debug.Log((object) ("The submitted pin is: " + pin + " and the password is " + this._roomInfo.Password));
    if (pin == this._roomInfo.Password)
    {
      Debug.Log((object) "PIN IS CORRECT!");
      this.GoToRoom();
    }
    else
      Debug.Log((object) "PIN IS INCORRECT!");
  }

  private void GoToRoom()
  {
    Debug.Log((object) "Going to room...");
    NetworkState.requestRoomInfo = this._roomInfo;
    PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
    MultiplayerEvents.LoadMultiplayerGame.Trigger(MultiplayerManager.GetMultiplayerAppStateByID(this._roomInfo.GameTypeID, true));
  }

  public void Initialize(RequestRoomInfo roomInfo, PinView pinView)
  {
    this._roomInfo = roomInfo;
    TMP_Text labelFans = this.labelFans;
    int num = roomInfo.PlayersAmount;
    string str1 = num.ToString();
    num = roomInfo.MaxPlayersAmount;
    string str2 = num.ToString();
    string str3 = str1 + "/" + str2;
    labelFans.text = str3;
    this.labelHost.text = roomInfo.HostName;
    this.labelFocus.text = MultiplayerManager.GetMultiplayerAppStateByID(roomInfo.GameTypeID);
    this.labelProtected.text = roomInfo.UsePassword ? "Yes" : "No";
    this._pinView = pinView;
  }
}
