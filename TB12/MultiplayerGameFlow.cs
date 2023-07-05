// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using FootballVR;
using FootballVR.Multiplayer;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.Networked;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TB12.AppStates;
using TB12.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Vars;

namespace TB12
{
  [DisallowMultipleComponent]
  [RequireComponent(typeof (PhotonView))]
  public abstract class MultiplayerGameFlow : MonoBehaviourPunCallbacks, IPunObservable
  {
    public List<Photon.Realtime.Player> PlayersInGame = new List<Photon.Realtime.Player>();
    public VariableInt gamestate = new VariableInt(6);
    public VariableFloat countdown = new VariableFloat(0.0f);
    [Header("General Flow Settings")]
    [SerializeField]
    protected EDifficulty gameDifficulty = EDifficulty.AllStar;
    [SerializeField]
    protected float countdownTimerAmount = 60f;
    [SerializeField]
    protected int countdownToGameStartAmount = 5;
    [Header("Health Bar Settings")]
    [SerializeField]
    protected bool _displayHealthBar;
    [SerializeField]
    protected float normalPlayerHealth = 2f;
    [SerializeField]
    protected float normalBallDamageAmount = 1f;
    [Header("Ball Container Settings")]
    [SerializeField]
    private BallContainerType BallContainerTypeTospawn = BallContainerType.Delayed;
    [SerializeField]
    protected float delayOnBallBucket = 5f;
    [Header("Ball Interaction Settings")]
    [SerializeField]
    protected float timeBeforeBallGroundCheck = 1f;
    [Header("Developer References")]
    [FormerlySerializedAs("_gameplayStore")]
    [SerializeField]
    protected MultiplayerStore store;
    [SerializeField]
    protected PhotonView photonView;
    [SerializeField]
    private PlaybackInfo playbackInfo;
    [SerializeField]
    private TMP_Text _gameCountdownText;
    [SerializeField]
    private AudioSource _whistleSFX;
    [SerializeField]
    private AppSettings _appSettings;
    [SerializeField]
    private ScoreboardAnimations.BoardAnimType _boardAnimType;
    [SerializeField]
    private TeamBallMatStore _teamBallMatStore;
    [SerializeField]
    private string _ballPrefabName;
    [Space]
    [Header("DEBUG SETTINGS")]
    [SerializeField]
    private bool showDebugScreenOnStart;
    protected PlayerAvatarNetworked localAvatar;
    protected readonly LinksHandler linksHandler = new LinksHandler();
    protected readonly RoutineHandle startRoutine = new RoutineHandle();
    protected readonly RoutineHandle finishRoutine = new RoutineHandle();
    internal GameState _gameState;
    internal IMultiplayerScene _sceneInterface;
    private int playerID = -1;
    private PlayerStatsData _playerStatsData;
    private Vector3 setupPos = Vector3.zero;
    private Quaternion setupRot = Quaternion.identity;

    public EMultiplayerGameState CurrentState => (EMultiplayerGameState) this.gamestate.Value;

    public bool CompletedOrTimeoutStateActive => this.CurrentState == EMultiplayerGameState.kCompleted || this.CurrentState == EMultiplayerGameState.kTimeout;

    public bool GameInProgress => this.CurrentState != EMultiplayerGameState.kSetup && this.CurrentState != EMultiplayerGameState.kIdle;

    protected virtual void Start()
    {
      Debug.Log((object) "Running Start Links...");
      if (this.photonView == null)
        this.photonView = PhotonView.Get((Component) this);
      if (PhotonNetwork.IsMasterClient)
      {
        foreach (Component component in UnityEngine.Object.FindObjectsOfType<BallObjectNetworked>())
          PhotonNetwork.Destroy(component.gameObject);
      }
      this.linksHandler.AddLink(this.store.OnDataChanged.Link(new System.Action(this.CheckGameState)));
      this.linksHandler.AddLink(this.store.OnHealthChanged.Link<int, float>(new Action<int, float>(this.SyncPlayerHealth)));
      this.linksHandler.AddLink(this._gameState.OnEnterStateFinished.Link((System.Action) (() =>
      {
        ScoreboardAnimations.HideCurrentBoard();
        ScoreboardAnimations.DisplayBoard(this._boardAnimType);
        if (!PersistentSingleton<PlayerAvatarHandler>.Instance.CurrentAvatar.Initialize())
          return;
        this.SetupLocalAvatar();
        if (!PhotonNetwork.IsMasterClient)
          return;
        this.gamestate.SetValue(0);
      })));
      this.linksHandler.AddLink(MultiplayerEvents.StartGame.Link((System.Action) (() => this.gamestate.SetValue(1))));
      this.linksHandler.AddLink(this.playbackInfo.OnPlaybackFinished.Link((System.Action) (() =>
      {
        if (!PhotonNetwork.IsMasterClient)
          return;
        this.gamestate.SetValue(4);
      })));
      this.gamestate.OnValueChanged += new Action<int>(this.HandleGameStateChange);
      this.linksHandler.AddLink(this.playbackInfo.OnRoundedTick.Link<float>(new Action<float>(((Variable<float>) this.countdown).SetValue)));
      this.linksHandler.AddLink(EventHandle.Link<int, Vector3>((AppEvent<int, Vector3>) MultiplayerEvents.SpawnBall, new Action<int, Vector3>(this.SpawnBallAtPos)));
    }

    private void HandleGameStateChange(int gameStateInt)
    {
      if (!PhotonNetwork.IsMasterClient)
        return;
      this.ParseGameStateFlow();
    }

    [PunRPC]
    public void GameStateChanged_RPC(EMultiplayerGameState newGameState)
    {
      Console.WriteLine("GameStateChanged_RPC : " + newGameState.ToString());
      this.gamestate.SetValue((int) newGameState);
      this.ParseGameStateFlow();
    }

    protected virtual void OnDestroy()
    {
      this.playbackInfo.StopPlayback();
      this.gamestate.OnValueChanged -= new Action<int>(this.HandleGameStateChange);
      this.linksHandler.Clear();
    }

    public override void OnJoinedRoom() => base.OnJoinedRoom();

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
      Debug.Log((object) string.Format("Player {0} has entered the room!", (object) newPlayer.ActorNumber));
      if ((int) PhotonNetwork.CurrentRoom.PlayerCount == NetworkState.requestRoomInfo.MaxPlayersAmount && PhotonNetwork.IsMasterClient)
        PhotonNetwork.CurrentRoom.IsOpen = false;
      if (PhotonNetwork.IsMasterClient && !this.GameInProgress)
      {
        this.PlayersInGame.Add(newPlayer);
        this.SetupPlayer(this.PlayersInGame.Count + 1, newPlayer);
      }
      else
        this.photonView.RPC("PlacePlayerAtLocalSidelineRPC", newPlayer, (object) 0);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
      Debug.Log((object) string.Format("Player {0} has left the room!", (object) otherPlayer.ActorNumber));
      if (otherPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        return;
      if (PhotonNetwork.IsMasterClient)
      {
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.DestroyPlayerObjects(otherPlayer);
      }
      this.PlayersInGame.Remove(otherPlayer);
      this.store.RemovePlayer(otherPlayer.ActorNumber);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
      Debug.Log((object) "MGF: OnMasterClientSwitched");
      base.OnMasterClientSwitched(newMasterClient);
      if (newMasterClient.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
      {
        if (this.CompletedOrTimeoutStateActive)
          MultiplayerEvents.BackToLobby.Trigger();
      }
      else
      {
        Debug.Log((object) "Sending Player Data to new Master Client!");
        this.photonView.RPC("SyncPlayerDataRPC", RpcTarget.AllViaServer, (object) this.store.LocalPlayerData.playerId, (object) this.store.LocalPlayerData.health, (object) this.store.LocalPlayerData.score, (object) this.store.LocalPlayerData.onTeamOne, (object) this.store.LocalPlayerData.isBoss);
      }
      if (!(bool) (UnityEngine.Object) this.localAvatar)
        return;
      this.localAvatar.OnMasterClientSwitched(newMasterClient);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
      Debug.Log((object) ("DisconnectCause: " + cause.ToString()));
      base.OnDisconnected(cause);
      PersistentSingleton<BallsContainerManager>.Instance.Clear();
      VRState.BigSizeMode.SetValue(false);
      AppEvents.LoadMainMenu.Trigger();
    }

    protected virtual void ParseGameStateFlow()
    {
      if (PhotonNetwork.IsMasterClient)
        this.photonView.RPC("GameStateChanged_RPC", RpcTarget.Others, (object) this.CurrentState);
      switch (this.CurrentState)
      {
        case EMultiplayerGameState.kSetup:
          Debug.Log((object) "<b>Setting Up Game...</b>");
          this.SetupGame();
          break;
        case EMultiplayerGameState.kStarting:
          Debug.Log((object) "<b>Starting Game...</b>");
          this.startRoutine.Run(this.StartGameRoutine());
          break;
        case EMultiplayerGameState.kStarted:
          AnalyticEvents.Record<MultiplayerGameStartedArgs>(new MultiplayerGameStartedArgs());
          Debug.Log((object) "<b>Started Game...</b>");
          break;
        case EMultiplayerGameState.kCompleted:
          AnalyticEvents.Record<MultiplayerGameCompletedArgs>(new MultiplayerGameCompletedArgs(Time.timeSinceLevelLoad, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
          Debug.Log((object) "<b>Completed Game...</b>");
          this.finishRoutine.Run(this.FinishGameRoutine());
          break;
        case EMultiplayerGameState.kTimeout:
          AnalyticEvents.Record<MultiplayerGameCompletedArgs>(new MultiplayerGameCompletedArgs(Time.timeSinceLevelLoad, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
          Debug.Log((object) "<b>Game Timed Out...</b>");
          this.finishRoutine.Run(this.FinishGameRoutine());
          break;
      }
    }

    protected virtual void SetupGame()
    {
      Debug.Log((object) "<b>Running Game Setup...</b>");
      PhotonVoiceNetwork.Instance.enabled = true;
      AppState.HandUI.SetValue(false);
      MultiplayerEvents.BallGroundCheckTimer.Trigger(this.timeBeforeBallGroundCheck);
      MultiplayerEvents.DisplayHealthBar.Trigger(this._displayHealthBar);
      if (PhotonNetwork.IsMasterClient)
      {
        this.countdown.SetValue(this.countdownTimerAmount);
        this.PlayersInGame.AddRange((IEnumerable<Photon.Realtime.Player>) this.ShufflePlayers());
        for (int index = 0; index < this.PlayersInGame.Count; ++index)
          this.SetupPlayer(index, this.PlayersInGame[index]);
      }
      if ((UnityEngine.Object) this.localAvatar != (UnityEngine.Object) null && (UnityEngine.Object) this.localAvatar.HelmetController != (UnityEngine.Object) null)
      {
        MultiplayerEvents.FireSpawnEffect.Trigger(this.localAvatar.HelmetController.transform.position);
      }
      else
      {
        this.SetupLocalAvatar();
        if ((UnityEngine.Object) this.localAvatar != (UnityEngine.Object) null && (UnityEngine.Object) this.localAvatar.HelmetController != (UnityEngine.Object) null)
          MultiplayerEvents.FireSpawnEffect.Trigger(this.localAvatar.HelmetController.transform.position);
      }
      ScriptableSingleton<HandsDataModel>.Instance.catchRadius = this._appSettings.GetDifficultySetting(this.gameDifficulty).CatchRadius;
      ScriptableSingleton<HandsDataModel>.Instance.catchRadiusOneHand = this._appSettings.GetDifficultySetting(this.gameDifficulty).CatchRadiusOneHand;
      if (this.CurrentState == EMultiplayerGameState.kSetup)
        return;
      this.ParseGameStateFlow();
    }

    protected virtual IEnumerator StartGameRoutine()
    {
      AppSounds.PlaySfx(ESfxTypes.kMiniStartWhistle);
      if (PhotonNetwork.IsMasterClient)
        PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(this._sceneInterface.bucketPositions, 200, this.BallContainerTypeTospawn, this.delayOnBallBucket);
      BallsContainerManager.CanSpawnBall.SetValue(false);
      this.playbackInfo.Setup(0.0f, this.countdown.Value);
      if (this.CurrentState == EMultiplayerGameState.kStarting)
      {
        Debug.Log((object) "Step 1");
        this._gameCountdownText.enabled = true;
        WaitForSeconds wait = new WaitForSeconds(1f);
        int countdownToStart = this.countdownToGameStartAmount;
        Debug.Log((object) string.Format("Step 2: {0}", (object) countdownToStart));
        for (; countdownToStart > 0; --countdownToStart)
        {
          Debug.Log((object) string.Format("Step 3: {0}", (object) countdownToStart));
          this._gameCountdownText.text = countdownToStart.ToString();
          yield return (object) wait;
        }
        this._gameCountdownText.enabled = false;
        yield return (object) new WaitForSeconds(0.2f);
        Debug.Log((object) string.Format("Step 4: {0}", (object) countdownToStart));
        GameplayUI.ShowText_Go_Localized();
        this._whistleSFX.Play();
        yield return (object) new WaitForSeconds(0.2f);
        wait = (WaitForSeconds) null;
      }
      VRState.LocomotionEnabled.SetValue(!this.store.LocalPlayerData.isBoss);
      Debug.Log((object) ("MP Locomotion: " + VRState.LocomotionEnabled.Value.ToString()));
      BallsContainerManager.CanSpawnBall.SetValue(true);
      if (PhotonNetwork.IsMasterClient)
        this.gamestate.SetValue(2);
      this.playbackInfo.StartPlayback();
      if (this.showDebugScreenOnStart)
        UIDispatch.FrontScreen.DisplayView(EScreens.kMultiplayerDebug);
      yield return (object) null;
    }

    protected virtual void CheckGameState() => Debug.Log((object) "Checking Game State...");

    protected virtual void SyncPlayerHealth(int playerID, float newHealth)
    {
      if (!PhotonNetwork.IsMasterClient)
        return;
      if ((double) newHealth <= 0.0)
      {
        int gameId = this.store.GetOrCreatePlayerData(playerID).gameId;
        this.photonView.RPC("PlacePlayerAtLocalSidelineRPC", this.PlayersInGame[gameId], (object) gameId);
      }
      this.photonView.RPC("SyncPlayerHealthRPC", RpcTarget.AllViaServer, (object) playerID, (object) newHealth);
    }

    protected virtual IEnumerator FinishGameRoutine()
    {
      AppSounds.PlaySfx(ESfxTypes.kMiniStopWhistle);
      VRState.BigSizeMode.SetValue(false);
      if (PhotonNetwork.IsMasterClient)
        this.playbackInfo.Reset();
      PersistentSingleton<BallsContainerManager>.Instance.Clear();
      ScoreboardAnimations.HideCurrentBoard();
      yield return (object) null;
    }

    private void SetupLocalAvatar()
    {
      if (!((UnityEngine.Object) PersistentSingleton<PlayerAvatarHandler>.Instance != (UnityEngine.Object) null) || !(PersistentSingleton<PlayerAvatarHandler>.Instance.CurrentAvatar is PlayerAvatarNetworked))
        return;
      this.localAvatar = (PlayerAvatarNetworked) PersistentSingleton<PlayerAvatarHandler>.Instance.CurrentAvatar;
      if (!((UnityEngine.Object) this.localAvatar != (UnityEngine.Object) null))
        return;
      this.localAvatar.RegisterTarget();
    }

    protected void SetupPlayer(int gameID, Photon.Realtime.Player player)
    {
      this.playerID = player.ActorNumber;
      Debug.Log((object) ("Setting up Player " + this.playerID.ToString()));
      bool flag = this is MultiplayerLobbyFlow;
      if (this.GameInProgress)
      {
        flag = false;
        if (this._sceneInterface.sidelinePostions.Length > gameID)
        {
          this.setupPos = this._sceneInterface.localSidelineTransform.position;
          this.setupRot = Quaternion.Euler(this._sceneInterface.localSidelineTransform.eulerAngles);
        }
        else
        {
          this.setupPos = this._sceneInterface.sidelinePostions[0].position;
          Debug.LogError((object) "MultiplayerGameFlow - SetupPlayer - Sideline Positions IS NOT LONG ENOUGH");
        }
      }
      else
      {
        if (this._sceneInterface.positions.Length > gameID)
        {
          this.setupPos = this._sceneInterface.positions[gameID].position;
          this.setupRot = Quaternion.Euler(this._sceneInterface.positions[gameID].eulerAngles);
        }
        else
        {
          this.setupPos = this._sceneInterface.positions[0].position;
          Debug.LogError((object) "MultiplayerGameFlow - SetupPlayer - Positions IS NOT LONG ENOUGH");
        }
        if (this.CompletedOrTimeoutStateActive || this.GameInProgress)
          return;
        this._playerStatsData = this.store.GetOrCreatePlayerData(this.playerID);
        Debug.Log((object) string.Format("Created PlayerStatsData for Player {0}", (object) this.playerID));
        this.photonView.RPC("SyncPlayerDataRPC", RpcTarget.AllBufferedViaServer, (object) this._playerStatsData.playerId, (object) this._playerStatsData.health, (object) this._playerStatsData.score, (object) this._playerStatsData.onTeamOne, (object) this._playerStatsData.isBoss, (object) (this._playerStatsData.gameId = gameID));
      }
      this.photonView.RPC("PlacePlayerAtPosAndRot", player, (object) this.setupPos, (object) this.setupRot, (object) flag);
    }

    public Photon.Realtime.Player[] ShufflePlayers()
    {
      System.Random rnd = new System.Random();
      return ((IEnumerable<Photon.Realtime.Player>) PhotonNetwork.PlayerList).OrderBy<Photon.Realtime.Player, int>((Func<Photon.Realtime.Player, int>) (x => rnd.Next())).ToArray<Photon.Realtime.Player>();
    }

    private void SpawnBallAtPos(int ballMatID, Vector3 pos)
    {
      GameObject gameObject = PhotonNetwork.Instantiate(this._ballPrefabName, pos, Quaternion.identity);
      BallObjectNetworked component = (UnityEngine.Object) gameObject != (UnityEngine.Object) null ? gameObject.GetComponent<BallObjectNetworked>() : (BallObjectNetworked) null;
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "Failed to create ball");
      }
      else
      {
        Debug.Log((object) "Ball Created!");
        component.SetPhysics(false);
        component.Graphics.BallMaterialID.SetValue(ballMatID);
        TeamBallConfig teamBallConfig = this._teamBallMatStore.GetTeamBallConfig(ballMatID);
        if (teamBallConfig == null)
          return;
        Material teamBallMaterial = teamBallConfig.teamBallMaterial;
        if ((UnityEngine.Object) teamBallMaterial == (UnityEngine.Object) null)
          return;
        component.Graphics.BallMaterial.SetValue(teamBallMaterial);
      }
    }

    [PunRPC]
    public void PlacePlayerAtPosAndRot(Vector3 pos, Quaternion rot, bool canMove = false)
    {
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(pos, rot);
      VRState.LocomotionEnabled.SetValue(canMove);
      Debug.Log((object) ("MP Locomotion: " + VRState.LocomotionEnabled.Value.ToString()));
    }

    [PunRPC]
    public void PlacePlayerAtLocalSidelineRPC(int gameId = 0)
    {
      VREvents.DropBall.Trigger();
      this._sceneInterface.localSidelineTransform = this._sceneInterface.sidelinePostions[gameId];
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(this._sceneInterface.localSidelineTransform.position, this._sceneInterface.localSidelineTransform.rotation);
      VRState.LocomotionEnabled.SetValue(false);
      Debug.Log((object) ("MP Locomotion: " + VRState.LocomotionEnabled.Value.ToString()));
    }

    [PunRPC]
    public void SyncPlayerDataRPC(
      int playerId,
      float health,
      int score,
      bool onTeamOne,
      bool isBoss,
      int gameId)
    {
      Debug.Log((object) string.Format("SyncPlayerDataRPC Fired... Receiving Player Data of Player {0}", (object) playerId));
      if (this.CompletedOrTimeoutStateActive)
        return;
      this.store.SetPlayerData(playerId, health, score, onTeamOne, isBoss, gameId);
      MultiplayerEvents.SetHealth.Trigger(playerId, health);
    }

    [PunRPC]
    public void SyncPlayerHealthRPC(int playerId, float health)
    {
      Debug.Log((object) string.Format("SyncPlayerHealthRPC Fired... Receiving Health of Player {0} with a health of {1}", (object) playerId, (object) health));
      this.store.GetOrCreatePlayerData(playerId).health = health;
      MultiplayerEvents.UpdateHealth.Trigger(playerId, health);
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
      if (stream.IsWriting)
      {
        if (!PhotonNetwork.IsMasterClient)
          return;
        stream.SendNext((object) this.countdown.Value);
      }
      else
        this.countdown.SetValue((float) stream.ReceiveNext());
    }
  }
}
