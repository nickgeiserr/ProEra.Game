// Decompiled with JetBrains decompiler
// Type: TB12.UI.PauseMenuMultiplayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.Networked;
using Framework.UI;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vars;

namespace TB12.UI
{
  public class PauseMenuMultiplayer : UIPanel
  {
    [SerializeField]
    private TouchButton _resumeButton;
    [SerializeField]
    private TouchButton _backButton;
    [SerializeField]
    private TouchButton _helmet;
    [SerializeField]
    private TouchButton _throwGame;
    [SerializeField]
    private TouchButton _backToLobby;
    [SerializeField]
    private Slider _voiceChatSlider;
    [SerializeField]
    private Slider _micSlider;
    [SerializeField]
    private Slider _sfxSlider;
    public static bool _multiplayerLaserEnabled;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._resumeButton, (Action) (() => this.Hide())),
      (EventHandle) UIHandle.Link((IButton) this._backButton, new Action(this.BackHandle)),
      (EventHandle) UIHandle.Link((IButton) this._helmet, new Action(this.HelmetHandle)),
      (EventHandle) UIHandle.Link((IButton) this._throwGame, (AppEvent) MultiplayerEvents.StartThrowChallenge),
      (EventHandle) UIHandle.Link((IButton) this._backToLobby, (Action) (() => MultiplayerEvents.LoadMultiplayerGame.Trigger(EAppState.kMultiplayerLobby.ToString()))),
      (EventHandle) UIHandle.Link(this._sfxSlider, ScriptableSingleton<SettingsStore>.Instance.SfxVolume),
      (EventHandle) UIHandle.Link(this._voiceChatSlider, ScriptableSingleton<VRSettings>.Instance.VCVolume),
      (EventHandle) UIHandle.Link(this._micSlider, ScriptableSingleton<VRSettings>.Instance.MicVolume)
    });

    private void BackHandle()
    {
      VRState.BigSizeMode.SetValue(false);
      UIDispatch.HideAll(false);
      this.Hide();
      NetworkState.requestRoomInfo.Clear();
      MultiplayerManager.LeaveRoom();
    }

    protected override void WillAppear()
    {
      this._backToLobby.gameObject.SetActive(MultiplayerEvents.BackToLobby.enabled);
      this._throwGame.gameObject.SetActive(MultiplayerEvents.StartThrowChallenge.enabled);
      this._backToLobby.gameObject.SetActive(PhotonNetwork.IsMasterClient);
      this.SetHelmetButtonText();
    }

    protected override void WillDisappear() => VRState.PauseMenu.SetValue(false);

    private void HelmetHandle()
    {
      VRState.HelmetEnabled.Toggle();
      this.SetHelmetButtonText();
    }

    private void SetHelmetButtonText()
    {
      ButtonText component = this._helmet.GetComponent<ButtonText>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      string str = "Hemet: [" + ((bool) VRState.HelmetEnabled ? "ON" : "OFF") + "]";
      component.text = str;
    }
  }
}
