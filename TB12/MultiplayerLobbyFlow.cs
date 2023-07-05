// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerLobbyFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using Framework;
using Framework.Data;
using Photon.Pun;
using Photon.Voice.PUN;
using System;
using System.Collections.Generic;
using TB12.AppStates;
using UnityEngine;

namespace TB12
{
  public class MultiplayerLobbyFlow : MultiplayerGameFlow
  {
    [Header("Freeplay Flow Settings")]
    [SerializeField]
    protected MultiplayerLobbyState state;
    [SerializeField]
    protected MultiplayerLobbyScene scene;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private ThrowManager _throwManager;

    protected override void Start()
    {
      this._gameState = (GameState) this.state;
      this._sceneInterface = (IMultiplayerScene) this.scene;
      base.Start();
      this.linksHandler.AddLinks((IReadOnlyList<EventHandle>) new List<EventHandle>()
      {
        EventHandle.Link(VREvents.SpawnBall, new System.Action(this.SpawnBall))
      });
      this._throwManager.OnBallThrown += new Action<BallObject, Vector3>(this.HandleBallThrown);
    }

    protected override void OnDestroy()
    {
      this._throwManager.OnBallThrown -= new Action<BallObject, Vector3>(this.HandleBallThrown);
      base.OnDestroy();
    }

    protected override void SetupGame()
    {
      PhotonVoiceNetwork.Instance.enabled = true;
      this.localAvatar = (PlayerAvatarNetworked) PersistentSingleton<PlayerAvatarHandler>.Instance.CurrentAvatar;
      MultiplayerEvents.DisplayHealthBar.Trigger(false);
      MultiplayerEvents.PlayIntro.Trigger(this.localAvatar.HelmetController.transform.position);
      this.localAvatar.RegisterTarget();
      AppState.HandUI.SetValue(true);
      VRState.LocomotionEnabled.SetValue(true);
      Debug.Log((object) ("MP Locomotion: " + VRState.LocomotionEnabled.Value.ToString()));
    }

    private void SpawnBall()
    {
      Transform transform = PersistentSingleton<PlayerCamera>.Instance.transform;
      Vector3 vector3 = transform.position + transform.forward * 0.6f;
      MultiplayerEvents.SpawnBall.Trigger((int) this._playerProfile.Customization.MultiplayerTeamBallId, vector3);
    }

    private void HandleBallThrown(BallObject arg1, Vector3 throwVector) => this.store.DoThrow(throwVector.magnitude);

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
      Debug.Log((object) ("Player " + newPlayer.ActorNumber.ToString() + " has entered the room!"));
      if (!PhotonNetwork.IsMasterClient)
        return;
      this.SetupPlayer(this.PlayersInGame.Count + 1, newPlayer);
    }

    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
  }
}
