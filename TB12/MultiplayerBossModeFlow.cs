// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerBossModeFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using Framework;
using Framework.Data;
using Photon.Pun;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TB12.AppStates;
using TB12.UI;
using UnityEngine;

namespace TB12
{
  public class MultiplayerBossModeFlow : MultiplayerGameFlow
  {
    [Header("Boss Flow Settings")]
    [SerializeField]
    protected MultiplayerBossModeState state;
    [SerializeField]
    protected MultiplayerBossModeScene scene;
    [SerializeField]
    private float _bossHealthPerPlayer = 2f;
    [SerializeField]
    private float _bossBallDamageAmount = 1f;
    [SerializeField]
    private string _mapName;
    [SerializeField]
    private GameObject _spawnedMap;
    private PlayerStatsData _bossPlayer;
    private int _bossNumber = -1;
    public static bool BossIsAlive;

    protected override void Start()
    {
      this._gameState = (GameState) this.state;
      this._sceneInterface = (IMultiplayerScene) this.scene;
      base.Start();
      this.linksHandler.AddLink(MultiplayerEvents.BallHitPlayerMaster.Link<int, int, bool>(new Action<int, int, bool>(this.BallHitPlayer)));
      this.linksHandler.AddLink(MultiplayerEvents.BallHitPlayerFX.Link<int>(new Action<int>(this.PlayerHitFX)));
    }

    protected override void OnDestroy()
    {
      VRState.BigSizeMode.SetValue(false);
      base.OnDestroy();
    }

    protected override void SetupGame()
    {
      if (PhotonNetwork.IsMasterClient)
      {
        this._spawnedMap = PhotonNetwork.Instantiate(this._mapName, this.transform.position, this.transform.rotation);
        BossModeMapPositions component;
        if (this._spawnedMap.TryGetComponent<BossModeMapPositions>(out component))
        {
          Transform[] positions1;
          if (component.TryGetPlayerPositions(PhotonNetwork.PlayerList.Length - 1, out positions1))
          {
            Debug.Log((object) "Found map specific Player positions. Applying to scene...");
            this.scene.positions = positions1;
          }
          Transform[] positions2;
          if (component.TryGetBucketPositions(8, out positions2))
          {
            Debug.Log((object) "Found map specific Bucket positions. Applying to scene...");
            this.scene.SetBucketPositions(positions2);
          }
        }
      }
      base.SetupGame();
      this.photonView.RPC("SpawnIntroUI_RPC", RpcTarget.AllViaServer);
      Debug.Log((object) "Game Setup Complete!");
    }

    protected override IEnumerator StartGameRoutine()
    {
      if (PhotonNetwork.IsMasterClient)
      {
        this._bossNumber = this.store.PlayerDatas[UnityEngine.Random.Range(0, this.store.PlayerDatas.Count - 1)].playerId;
        float num = (float) this.PlayersInGame.Count * this._bossHealthPerPlayer;
        Debug.Log((object) string.Format("Boss Health is {0}", (object) num));
        this.photonView.RPC("SetPlayerToBossRPC", RpcTarget.AllViaServer, (object) this._bossNumber, (object) num);
      }
      UIDispatch.FrontScreen.HideView(EScreens.kMultiplayerBossModeIntro);
      return base.StartGameRoutine();
    }

    protected override void CheckGameState()
    {
      base.CheckGameState();
      if (!PhotonNetwork.IsMasterClient || this.store.PlayerDatas.Count <= 1 || this.CurrentState != EMultiplayerGameState.kStarted || this.IsBossAlive() && this.ArePlayersAlive())
        return;
      MultiplayerBossModeFlow.BossIsAlive = this.IsBossAlive();
      this.gamestate.SetValue(3);
    }

    private bool IsBossAlive() => this.store.GetOrCreatePlayerData(this._bossNumber).isAlive;

    private bool ArePlayersAlive() => this.store.PlayerDatas.Any<PlayerStatsData>((Func<PlayerStatsData, bool>) (playerData => !playerData.isBoss && playerData.isAlive));

    private void BallHitPlayer(int throwingPlayerId, int hitPlayerId, bool isGiant)
    {
      Debug.Log((object) ("Ball has hit Player " + hitPlayerId.ToString() + "!"));
      if (throwingPlayerId == -1)
        return;
      PlayerStatsData playerData1 = this.store.GetOrCreatePlayerData(throwingPlayerId);
      PlayerStatsData playerData2 = this.store.GetOrCreatePlayerData(hitPlayerId);
      if (hitPlayerId == throwingPlayerId || !playerData2.isBoss && !isGiant)
        return;
      if (playerData1.isBoss)
      {
        double num1 = (double) this.store.ChangeHealth(hitPlayerId, playerData2.health - this._bossBallDamageAmount);
      }
      else
      {
        double num2 = (double) this.store.ChangeHealth(hitPlayerId, playerData2.health - this.normalBallDamageAmount);
      }
    }

    protected override IEnumerator FinishGameRoutine()
    {
      MultiplayerBossModeFlow multiplayerBossModeFlow = this;
      Debug.Log((object) "Finishing Gameplay...");
      VRState.BigSizeMode.SetValue(false);
      if (PhotonNetwork.IsMasterClient)
      {
        if ((UnityEngine.Object) multiplayerBossModeFlow._spawnedMap == (UnityEngine.Object) null)
          LinqExtensions.ForEach<BossModeMapPositions>((IEnumerable<BossModeMapPositions>) UnityEngine.Object.FindObjectsOfType<BossModeMapPositions>(), (Action<BossModeMapPositions>) (x => PhotonNetwork.Destroy(x.gameObject)));
        else
          PhotonNetwork.Destroy(multiplayerBossModeFlow._spawnedMap);
      }
      // ISSUE: reference to a compiler-generated method
      yield return (object) multiplayerBossModeFlow.\u003C\u003En__0();
      if (PhotonNetwork.IsMasterClient)
      {
        MultiplayerBossModeFlow.BossIsAlive = multiplayerBossModeFlow.IsBossAlive();
        multiplayerBossModeFlow.store.bossResultsData = new BossModeResultsUIData()
        {
          IsBossAlive = multiplayerBossModeFlow.IsBossAlive()
        };
      }
      yield return (object) new WaitForSeconds(2f);
      multiplayerBossModeFlow.photonView.RPC("SpawnResultsUI_RPC", RpcTarget.AllViaServer);
    }

    private void PlayerHitFX(int hitPlayerID)
    {
      foreach (PhotonView photonView in PhotonNetwork.PhotonViewCollection)
      {
        if (photonView.OwnerActorNr == hitPlayerID)
        {
          AppSounds.Play3DSfx(ESfxTypes.kMiniBallHitPlayer, photonView.transform);
          break;
        }
      }
    }

    [PunRPC]
    public void SetPlayerToBossRPC(int bossId, float bossHealth)
    {
      Debug.Log((object) string.Format("SetPlayerToBossRPC Fired... Running SetPlayerToBoss on Player {0}", (object) bossId));
      this._bossPlayer = this.store.SetPlayerToBoss(bossId, bossHealth);
      Debug.Log((object) string.Format("Boss Player Health is now {0} and making sure that Player {1} is boss? {2}", (object) this._bossPlayer.health, (object) bossId, (object) this._bossPlayer.isBoss));
      MultiplayerEvents.SetHealth.Trigger(this._bossNumber, bossHealth);
      if (this.CompletedOrTimeoutStateActive || bossId != this.store.LocalPlayerId)
        return;
      VRState.BigSizeMode.SetValue(true);
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(this.scene.bossPosition.position, this.scene.bossPosition.rotation);
      VRState.LocomotionEnabled.SetValue(false);
      Debug.Log((object) ("MP Locomotion: " + VRState.LocomotionEnabled.Value.ToString()));
    }

    [PunRPC]
    public void SpawnIntroUI_RPC() => UIDispatch.FrontScreen.DisplayView(EScreens.kMultiplayerBossModeIntro);

    [PunRPC]
    public void SpawnResultsUI_RPC() => UIDispatch.FrontScreen.DisplayView(EScreens.kMultiplayerBossModeResults);

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
      base.OnPlayerLeftRoom(otherPlayer);
      if (!PhotonNetwork.IsMasterClient || this._bossPlayer == null || this._bossPlayer.playerId != otherPlayer.ActorNumber)
        return;
      Debug.Log((object) "Players Win!");
      this.gamestate.SetValue(4);
    }

    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
      base.OnPhotonSerializeView(stream, info);
      if (stream.IsWriting)
      {
        if (!PhotonNetwork.IsMasterClient)
          return;
        stream.SendNext((object) MultiplayerBossModeFlow.BossIsAlive);
      }
      else
        MultiplayerBossModeFlow.BossIsAlive = (bool) stream.ReceiveNext();
    }
  }
}
