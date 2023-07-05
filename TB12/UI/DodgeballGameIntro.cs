// Decompiled with JetBrains decompiler
// Type: TB12.UI.DodgeballGameIntro
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
using System.Linq;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class DodgeballGameIntro : UIView
  {
    [SerializeField]
    private MultiplayerStore _store;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [Header("UI Properties")]
    [SerializeField]
    private TextMeshProUGUI[] team1Names;
    [Header("UI Properties")]
    [SerializeField]
    private TextMeshProUGUI[] team2Names;
    [SerializeField]
    private TouchButton returnToLockerRoomBtn;
    [SerializeField]
    private TouchButton playBtn;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public override Enum ViewId { get; } = (Enum) EScreens.kMultiplayerDodgeballIntro;

    private static RequestRoomInfo requestRoomInfo => NetworkState.requestRoomInfo;

    private void Start()
    {
      this._linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.returnToLockerRoomBtn, new System.Action(this.ReturnToLockerRoom)));
      this._linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.playBtn, new System.Action(this.PlayDodgeball)));
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

    private void PlayDodgeball() => MultiplayerEvents.StartGame.Trigger();

    protected override void WillAppear() => this.UpdateDetails();

    private void UpdateDetails()
    {
      this.SetPlayButtonState();
      if (!((UnityEngine.Object) this._store != (UnityEngine.Object) null) || !((UnityEngine.Object) this._playerProfile != (UnityEngine.Object) null))
        return;
      List<PlayerStatsData> list1 = this._store.PlayerDatas.Where<PlayerStatsData>((Func<PlayerStatsData, bool>) (player => player.onTeamOne)).ToList<PlayerStatsData>();
      int num1 = Mathf.Min(list1.Count, 8);
      int length1 = this.team1Names.Length;
      List<PlayerStatsData> list2 = this._store.PlayerDatas.Where<PlayerStatsData>((Func<PlayerStatsData, bool>) (player => !player.onTeamOne)).ToList<PlayerStatsData>();
      int num2 = Mathf.Min(list2.Count, 8);
      int length2 = this.team2Names.Length;
      for (int index = 0; index < length1; ++index)
      {
        bool flag = index < num1;
        this.team1Names[index].gameObject.SetActive(flag);
        if (flag)
        {
          PlayerCustomization playerCustomization;
          string str = this._playerProfile.Profiles.TryGetValue(list1[index].playerId, out playerCustomization) ? (string) playerCustomization.LastName : "Player " + (index + 1).ToString();
          this.team1Names[index].text = str;
          this.team1Names[index].color = list1[index].isLocalPlayer ? Common.SP_GREEN : Common.SP_WHITE;
        }
      }
      for (int index = 0; index < length2; ++index)
      {
        bool flag = index < num2;
        this.team2Names[index].gameObject.SetActive(flag);
        if (flag)
        {
          PlayerCustomization playerCustomization;
          string str = this._playerProfile.Profiles.TryGetValue(list2[index].playerId, out playerCustomization) ? (string) playerCustomization.LastName : "Player " + (index + 1).ToString();
          this.team2Names[index].text = str;
          this.team2Names[index].color = list2[index].isLocalPlayer ? Common.SP_GREEN : Common.SP_WHITE;
        }
      }
    }

    public void Refresh(int pCount) => this.UpdateDetails();

    public void SetPlayButtonState()
    {
      if (!((UnityEngine.Object) this.playBtn != (UnityEngine.Object) null))
        return;
      this.playBtn.SetInteractible(PhotonNetwork.IsMasterClient);
    }
  }
}
