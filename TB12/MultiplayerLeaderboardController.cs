// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerLeaderboardController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  public class MultiplayerLeaderboardController : MonoBehaviourPunCallbacks
  {
    [SerializeField]
    private PhotonView _photonView;
    [SerializeField]
    private MultiplayerStore _store;
    [SerializeField]
    private bool _trackOnlyScore;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake()
    {
      Action<int> action = (Action<int>) (x => this.SyncPlayerData());
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this._trackOnlyScore ? this._store.Score.Link<int>(action) : this._store.BallsThrown.Link<int>(action)
      });
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void SyncPlayerData(Player targetPlayer = null)
    {
    }

    [PunRPC]
    public void SetPlayerScoreRPC(
      int newScore,
      int throwHits,
      int throwCount,
      PhotonMessageInfo info)
    {
    }

    public override void OnJoinedRoom() => this.SyncPlayerData();

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
      if (newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        return;
      this.SyncPlayerData(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
      if (otherPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        return;
      this._store.RemovePlayer(otherPlayer.ActorNumber);
    }
  }
}
