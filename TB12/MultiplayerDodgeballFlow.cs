// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerDodgeballFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using Framework.Data;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.AppStates;
using TB12.UI;
using UnityEngine;

namespace TB12
{
  public class MultiplayerDodgeballFlow : MultiplayerGameFlow
  {
    [SerializeField]
    private MultiplayerDodgeballState state;
    [SerializeField]
    private MultiplayerDodgeballScene scene;
    private bool teamOneAlive = true;
    private bool teamTwoAlive = true;
    private int teamOneCount;
    private int teamTwoCount;
    private int spawnDistanceFromPlayer = 2;
    [Header("Asset References")]
    [SerializeField]
    private GameObject p_MultiplayerDodgeballStats;
    [SerializeField]
    private GameObject p_MultiplayerDodgeballIntroUi;
    [SerializeField]
    private GameObject _offsideBoxForTeamOne;
    [SerializeField]
    private GameObject _offsideBoxForTeamTwo;
    private const float standingHeightOffset = 0.33f;
    private const float distSpawnFromPlayer = 0.85f;
    private float _gameStartTime;
    public static bool HasWon;

    protected override void Start()
    {
      this._gameState = (GameState) this.state;
      this._sceneInterface = (IMultiplayerScene) this.scene;
      base.Start();
      this.linksHandler.AddLink(MultiplayerEvents.BallHitPlayerMaster.Link<int, int, bool>(new Action<int, int, bool>(this.PlayerIsOut)));
      this.linksHandler.AddLink(MultiplayerEvents.BallHitPlayerFX.Link<int>(new Action<int>(this.PlayerHitFX)));
      this.linksHandler.AddLink(MultiplayerEvents.PlayerIsOutFX.Link(new System.Action(this.PlayerOutFX)));
      this.linksHandler.AddLink(MultiplayerEvents.BallWasCaught.Link<int, int, bool>(new Action<int, int, bool>(this.PlayerIsOut)));
    }

    protected override void SetupGame()
    {
      base.SetupGame();
      for (int index = 0; index < this.PlayersInGame.Count; ++index)
        this.photonView.RPC("SetToTeamRPC", RpcTarget.AllViaServer, (object) this.PlayersInGame[index].ActorNumber, (object) (index % 2 != 0));
      this.photonView.RPC("SpawnIntroUI_RPC", RpcTarget.AllViaServer);
      Debug.Log((object) "Game Setup Complete!");
    }

    protected override IEnumerator StartGameRoutine()
    {
      this._gameStartTime = Time.time;
      UIDispatch.FrontScreen.HideView(EScreens.kMultiplayerDodgeballIntro);
      this._offsideBoxForTeamOne.SetActive(true);
      this._offsideBoxForTeamTwo.SetActive(true);
      return base.StartGameRoutine();
    }

    protected override void CheckGameState()
    {
      if (!PhotonNetwork.IsMasterClient)
        return;
      base.CheckGameState();
      List<PlayerStatsData> playerDatas = this.store.PlayerDatas;
      if (this.store.PlayerDatas.Count <= 0 || this.CurrentState != EMultiplayerGameState.kStarted)
        return;
      this.teamOneAlive = false;
      this.teamTwoAlive = false;
      for (int index = 0; index < playerDatas.Count; ++index)
      {
        if ((double) playerDatas[index].health > 0.0)
        {
          if (playerDatas[index].onTeamOne)
            this.teamOneAlive = true;
          else
            this.teamTwoAlive = true;
        }
      }
      if (!this.teamOneAlive)
      {
        this.gamestate.SetValue(3);
      }
      else
      {
        if (this.teamTwoAlive)
          return;
        this.gamestate.SetValue(3);
      }
    }

    protected override IEnumerator FinishGameRoutine()
    {
      MultiplayerDodgeballFlow multiplayerDodgeballFlow = this;
      Debug.Log((object) "Finishing Gameplay...");
      multiplayerDodgeballFlow._offsideBoxForTeamOne.SetActive(false);
      multiplayerDodgeballFlow._offsideBoxForTeamTwo.SetActive(false);
      // ISSUE: reference to a compiler-generated method
      yield return (object) multiplayerDodgeballFlow.\u003C\u003En__0();
      yield return (object) new WaitForSeconds(2f);
      if (PhotonNetwork.IsMasterClient)
        multiplayerDodgeballFlow.photonView.RPC("SpawnResultsUI_RPC", RpcTarget.AllViaServer, (object) multiplayerDodgeballFlow.teamOneAlive, (object) multiplayerDodgeballFlow.teamTwoAlive);
    }

    private void PlayerIsOut(int thrownPlayerId, int hitPlayerId, bool isGiant)
    {
      Debug.Log((object) "Ball has hit!");
      PlayerStatsData playerData = this.store.GetOrCreatePlayerData(hitPlayerId);
      if (thrownPlayerId != -1)
      {
        if (!(this.store.GetOrCreatePlayerData(thrownPlayerId).onTeamOne != playerData.onTeamOne | isGiant))
          return;
        MultiplayerEvents.PlayerIsOutFX.Trigger();
        double zero = (double) this.store.SetHealthToZero(hitPlayerId);
        this.CheckGameState();
      }
      else
      {
        MultiplayerEvents.PlayerIsOutFX.Trigger();
        double zero = (double) this.store.SetHealthToZero(hitPlayerId);
        this.CheckGameState();
      }
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

    private void PlayerOutFX() => AppSounds.PlaySfx(ESfxTypes.kMiniOutCallout);

    [PunRPC]
    public void SetToTeamRPC(int playerID, bool onTeamOne)
    {
      Debug.Log((object) "SetToTeamRPC Fired...");
      if (!((UnityEngine.Object) this.store != (UnityEngine.Object) null))
        return;
      this.store.SetToTeamOne(playerID, onTeamOne);
    }

    [PunRPC]
    public void SpawnIntroUI_RPC() => UIDispatch.FrontScreen.DisplayView(EScreens.kMultiplayerDodgeballIntro);

    [PunRPC]
    public void SpawnResultsUI_RPC(bool isTeamOneAlive, bool isTeamTwoAlive)
    {
      MultiplayerDodgeballFlow.HasWon = ((!(this.store.LocalPlayerData.onTeamOne & isTeamOneAlive) ? 0 : (!isTeamTwoAlive ? 1 : 0)) | (((this.store.LocalPlayerData.onTeamOne ? 0 : (!isTeamOneAlive ? 1 : 0)) & (isTeamTwoAlive ? 1 : 0)) != 0 ? 1 : 0)) != 0;
      UIDispatch.FrontScreen.DisplayView(EScreens.kMultiplayerDodgeballResults);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
      base.OnPlayerEnteredRoom(newPlayer);
      if (this.GameInProgress || this.CompletedOrTimeoutStateActive)
        return;
      this.SetupGame();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
      base.OnPlayerLeftRoom(otherPlayer);
      if (!PhotonNetwork.IsMasterClient)
        return;
      for (int index = 0; index < this.store.PlayerDatas.Count; ++index)
      {
        if (this.store.PlayerDatas[index].onTeamOne)
          ++this.teamOneCount;
        else
          ++this.teamTwoCount;
      }
      if (this.teamOneCount == 0)
      {
        this.teamOneAlive = false;
        this.teamTwoAlive = true;
        this.gamestate.SetValue(3);
      }
      else
      {
        if (this.teamTwoCount != 0)
          return;
        this.teamOneAlive = true;
        this.teamTwoAlive = false;
        this.gamestate.SetValue(3);
      }
    }
  }
}
