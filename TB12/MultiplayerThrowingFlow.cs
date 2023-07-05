// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerThrowingFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
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
  public class MultiplayerThrowingFlow : MultiplayerGameFlow
  {
    [Header("Throwing Flow Settings")]
    [SerializeField]
    private int throwCount = 10;
    [SerializeField]
    private int roundsToSpawn = 6;
    [SerializeField]
    private float targetSpawnWaitTime = 10f;
    [SerializeField]
    private MultiplayerThrowingState state;
    [SerializeField]
    private MultiplayerThrowingScene scene;
    [SerializeField]
    private ThrowManager throwManager;
    private int _currentRound = 1;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    public static Dictionary<string, int> PlayerScores = new Dictionary<string, int>();

    protected override void Start()
    {
      this._gameState = (GameState) this.state;
      this._sceneInterface = (IMultiplayerScene) this.scene;
      base.Start();
      this.linksHandler.AddLinks((IReadOnlyList<EventHandle>) new EventHandle[1]
      {
        VREvents.TargetHit.Link<bool, float>(new Action<bool, float>(this.HandleTargetHit))
      });
      VREvents.ThrowResult.OnTrigger += new Action<bool, float>(this.HandleThrowResult);
      this.throwManager.OnBallThrown += new Action<BallObject, Vector3>(this.HandleBallThrown);
      this.store.AttemptsRemaining.SetValue(this.throwCount);
    }

    protected override void SetupGame()
    {
      base.SetupGame();
      this.photonView.RPC("SpawnIntroUI_RPC", RpcTarget.AllViaServer);
      Debug.Log((object) "Game Setup Complete!");
    }

    protected override void OnDestroy()
    {
      this.throwManager.OnBallThrown -= new Action<BallObject, Vector3>(this.HandleBallThrown);
      VREvents.ThrowResult.OnTrigger -= new Action<bool, float>(this.HandleThrowResult);
      base.OnDestroy();
    }

    protected override IEnumerator StartGameRoutine()
    {
      UIDispatch.FrontScreen.HideView(EScreens.kMultiplayerThrowIntro);
      yield return (object) base.StartGameRoutine();
      if (PhotonNetwork.IsMasterClient)
        this._routineHandle.Run(this.LoadTargetsRoutine());
    }

    private void HandleBallThrown(BallObject ballObject, Vector3 velocity)
    {
      if (this.store.LocalPlayerData.throwsMade < this.throwCount)
      {
        ++this.store.LocalPlayerData.throwsMade;
        this.photonView.RPC("SetThrowsRPC", RpcTarget.AllViaServer, (object) this.store.LocalPlayerId, (object) this.store.LocalPlayerData.throwsMade);
        this.store.DoThrow(velocity.magnitude);
      }
      else
        BallsContainerManager.CanSpawnBall.SetValue(false);
    }

    private void HandleThrowResult(bool targetHit, float distance)
    {
      if (targetHit)
        return;
      this.store.ComboModifier = 0;
    }

    protected override void CheckGameState()
    {
      base.CheckGameState();
      if (!PhotonNetwork.IsMasterClient || this.CompletedOrTimeoutStateActive)
        return;
      List<PlayerStatsData> playerDatas = this.store.PlayerDatas;
      int num = 0;
      for (int index = 0; index < playerDatas.Count; ++index)
      {
        if (playerDatas[index].throwsMade >= this.throwCount)
          ++num;
      }
      if (this._currentRound < this.roundsToSpawn && num < playerDatas.Count)
        return;
      this.gamestate.SetValue(3);
    }

    protected override IEnumerator FinishGameRoutine()
    {
      MultiplayerThrowingFlow multiplayerThrowingFlow = this;
      Debug.Log((object) "Finishing Gameplay...");
      multiplayerThrowingFlow.scene.HideTargets();
      yield return (object) new WaitForSeconds(1.5f);
      // ISSUE: reference to a compiler-generated method
      yield return (object) multiplayerThrowingFlow.\u003C\u003En__1();
      yield return (object) new WaitForSeconds(2f);
      if (PhotonNetwork.IsMasterClient)
        multiplayerThrowingFlow.photonView.RPC("SpawnResultsUI_RPC", RpcTarget.AllViaServer);
    }

    private void HandleTargetHit(bool throwHit, float dist)
    {
      if (this.CompletedOrTimeoutStateActive || this.store.Locked)
        return;
      if (throwHit)
      {
        this.store.Score.SetValue(this.store.Score.Value + (int) dist);
        this.photonView.RPC("RecordPointsRPC", RpcTarget.AllViaServer, (object) this.store.LocalPlayerId, (object) this.store.Score.Value);
      }
      else
        this.store.ComboModifier = 0;
    }

    private IEnumerator LoadTargetsRoutine()
    {
      MultiplayerThrowingFlow multiplayerThrowingFlow = this;
      WaitForSeconds wait = new WaitForSeconds(multiplayerThrowingFlow.targetSpawnWaitTime);
      for (int i = 0; i < multiplayerThrowingFlow.roundsToSpawn; ++i)
      {
        multiplayerThrowingFlow.photonView.RPC("LoadTargetsRPC", RpcTarget.AllViaServer, (object) multiplayerThrowingFlow.scene.GetRandomGroupId());
        Debug.Log((object) string.Format("On Round {0} of {1}", (object) multiplayerThrowingFlow._currentRound, (object) multiplayerThrowingFlow.roundsToSpawn));
        yield return (object) wait;
        ++multiplayerThrowingFlow._currentRound;
      }
    }

    [PunRPC]
    public void LoadTargetsRPC(int id)
    {
      Debug.Log((object) string.Format("LoadTargetsRPC Fired... Loading Target with ID of {0}", (object) id));
      if (this.CompletedOrTimeoutStateActive)
        return;
      this.scene.LoadTargets(id);
    }

    [PunRPC]
    public void RecordPointsRPC(int playerId, int score)
    {
      Debug.Log((object) string.Format("RecordPointsRPC Fired...Player {0} now has a score of {1}", (object) playerId, (object) score));
      if (this.CompletedOrTimeoutStateActive)
        return;
      this.store.RecordPoints(playerId, score);
    }

    [PunRPC]
    public void SetThrowsRPC(int playerId, int throws)
    {
      Debug.Log((object) string.Format("SetThrowsRPC Fired... Player {0} has made {1} throws", (object) playerId, (object) throws));
      if (this.CompletedOrTimeoutStateActive)
        return;
      this.store.SetThrowsMade(playerId, throws);
    }

    [PunRPC]
    public void SpawnIntroUI_RPC() => UIDispatch.FrontScreen.DisplayView(EScreens.kMultiplayerThrowIntro);

    [PunRPC]
    public void SpawnResultsUI_RPC() => UIDispatch.FrontScreen.DisplayView(EScreens.kMultiplayerThrowResults);

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) => base.OnPlayerLeftRoom(otherPlayer);
  }
}
