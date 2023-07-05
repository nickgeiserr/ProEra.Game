// Decompiled with JetBrains decompiler
// Type: TB12.UI.ThrowGameIntro
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
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class ThrowGameIntro : UIView
  {
    [SerializeField]
    private MultiplayerStore _store;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [Header("UI Properties")]
    [SerializeField]
    private TextMeshProUGUI[] playerNames;
    [SerializeField]
    private TouchButton returnToLockerRoomBtn;
    [SerializeField]
    private TouchButton playBtn;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public override Enum ViewId { get; } = (Enum) EScreens.kMultiplayerThrowIntro;

    private void Start()
    {
      this._linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.returnToLockerRoomBtn, new System.Action(this.ReturnToLockerRoom)));
      this._linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.playBtn, new System.Action(this.PlayThrowGame)));
    }

    protected override void OnInitialize() => this._laserIsAlwaysEnabled = true;

    private void OnEnable()
    {
      PauseMenuMultiplayer._multiplayerLaserEnabled = this._laserIsAlwaysEnabled;
      NetworkState.PlayerCount.OnValueChanged += new Action<int>(this.Refresh);
    }

    private new void OnDisable()
    {
      PauseMenuMultiplayer._multiplayerLaserEnabled = false;
      NetworkState.PlayerCount.OnValueChanged -= new Action<int>(this.Refresh);
    }

    private void ReturnToLockerRoom()
    {
      NetworkState.requestRoomInfo.Clear();
      MultiplayerManager.LeaveRoom();
    }

    private void PlayThrowGame() => MultiplayerEvents.StartGame.Trigger();

    protected override void WillAppear() => this.UpdateDetails();

    private void UpdateDetails()
    {
      this.SetPlayButtonState();
      List<PlayerStatsData> playerDatas = this._store.PlayerDatas;
      int num = Mathf.Min(playerDatas.Count, 8);
      int length = this.playerNames.Length;
      for (int index = 0; index < length; ++index)
      {
        bool flag = index < num;
        this.playerNames[index].gameObject.SetActive(flag);
        if (flag)
        {
          PlayerCustomization playerCustomization;
          string str = this._playerProfile.Profiles.TryGetValue(playerDatas[index].playerId, out playerCustomization) ? (string) playerCustomization.LastName : "Player " + (index + 1).ToString();
          this.playerNames[index].text = str;
          this.playerNames[index].color = playerDatas[index].isLocalPlayer ? Common.SP_GREEN : Common.SP_WHITE;
        }
      }
    }

    public void Refresh(int pCount) => this.UpdateDetails();

    public void SetPlayButtonState() => this.playBtn.SetInteractible(PhotonNetwork.IsMasterClient);
  }
}
