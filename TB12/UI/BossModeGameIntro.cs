// Decompiled with JetBrains decompiler
// Type: TB12.UI.BossModeGameIntro
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
  public class BossModeGameIntro : UIView
  {
    [SerializeField]
    private MultiplayerStore _store;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private TouchButton returnToLockerRoomBtn;
    [SerializeField]
    private TouchButton playBtn;
    [Header("UI Properties")]
    [SerializeField]
    private List<TextMeshProUGUI> uiBossPlayerNames;
    [SerializeField]
    private List<TextMeshProUGUI> uiSmallPlayerNames;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public override Enum ViewId { get; } = (Enum) EScreens.kMultiplayerBossModeIntro;

    private static RequestRoomInfo requestRoomInfo => NetworkState.requestRoomInfo;

    private void Start()
    {
      this._linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.returnToLockerRoomBtn, new System.Action(this.ReturnToLockerRoom)));
      this._linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.playBtn, new System.Action(this.PlayBossMode)));
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

    private void PlayBossMode() => MultiplayerEvents.StartGame.Trigger();

    protected override void WillAppear() => this.UpdateDetails();

    private void UpdateDetails()
    {
      this.SetPlayButtonState();
      List<PlayerStatsData> list1 = this._store.PlayerDatas.Where<PlayerStatsData>((Func<PlayerStatsData, bool>) (player => player.isBoss)).ToList<PlayerStatsData>();
      int num1 = Mathf.Min(list1.Count, 8);
      int count1 = this.uiBossPlayerNames.Count;
      List<PlayerStatsData> list2 = this._store.PlayerDatas.Where<PlayerStatsData>((Func<PlayerStatsData, bool>) (player => !player.isBoss)).ToList<PlayerStatsData>();
      int num2 = Mathf.Min(list2.Count, 8);
      int count2 = this.uiSmallPlayerNames.Count;
      for (int index = 0; index < count1; ++index)
      {
        bool flag = index < num1;
        this.uiBossPlayerNames[index].gameObject.SetActive(flag);
        if (flag)
        {
          PlayerCustomization playerCustomization;
          string str = this._playerProfile.Profiles.TryGetValue(list1[index].playerId, out playerCustomization) ? (string) playerCustomization.LastName : string.Format("Player {0}", (object) (index + 1));
          this.uiBossPlayerNames[index].text = str;
          this.uiBossPlayerNames[index].color = list1[index].isLocalPlayer ? Common.SP_GREEN : Common.SP_WHITE;
        }
      }
      for (int index = 0; index < count2; ++index)
      {
        bool flag = index < num2;
        this.uiSmallPlayerNames[index].gameObject.SetActive(flag);
        if (flag)
        {
          PlayerCustomization playerCustomization;
          string str = this._playerProfile.Profiles.TryGetValue(list2[index].playerId, out playerCustomization) ? (string) playerCustomization.LastName : string.Format("Player {0}", (object) (index + 1));
          this.uiSmallPlayerNames[index].text = str;
          this.uiSmallPlayerNames[index].color = list2[index].isLocalPlayer ? Common.SP_GREEN : Common.SP_WHITE;
        }
      }
    }

    public void Refresh(int pCount) => this.UpdateDetails();

    public void SetPlayButtonState() => this.playBtn.SetInteractible(PhotonNetwork.IsMasterClient);
  }
}
