// Decompiled with JetBrains decompiler
// Type: TB12.UI.RoomCellView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.Networked;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class RoomCellView : MonoBehaviour
  {
    [SerializeField]
    private TouchButton joinButton;
    [SerializeField]
    private TMP_Text labelFans;
    [SerializeField]
    private TMP_Text labelHost;
    [SerializeField]
    private TMP_Text labelFocus;
    [SerializeField]
    private TMP_Text labelProtected;
    private RequestRoomInfo _roomInfo = new RequestRoomInfo();
    private PinView _pinView;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake() => this._linksHandler.SetLinks(new List<EventHandle>()
    {
      AppEvents.PinCodeSubmitted.Link<string>(new Action<string>(this.GoToRoom))
    });

    private void Start() => this.joinButton.onClick += new Action(this.OnClickJoin);

    private void OnDestroy() => this.joinButton.onClick -= new Action(this.OnClickJoin);

    private void OnClickJoin()
    {
      RequestRoomInfo roomInfo = this._roomInfo;
      if (roomInfo == null || !roomInfo.UsePassword)
        return;
      this._pinView.Show();
    }

    public void GoToRoom(string pin)
    {
      if (this._roomInfo.UsePassword && pin != this._roomInfo.Password)
        return;
      NetworkState.requestRoomInfo = this._roomInfo;
      AppEvents.LoadMultiplayer.Trigger(MultiplayerManager.GetMultiplayerAppStateByID(this._roomInfo.GameTypeID, true));
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
      pinView = this._pinView;
    }
  }
}
