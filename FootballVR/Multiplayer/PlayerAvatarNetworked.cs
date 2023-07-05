// Decompiled with JetBrains decompiler
// Type: FootballVR.Multiplayer.PlayerAvatarNetworked
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.Networked;
using Framework.StateManagement;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Voice.Unity.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB12;
using TB12.AppStates;
using UnityEngine;
using Vars;

namespace FootballVR.Multiplayer
{
  public class PlayerAvatarNetworked : PlayerAvatar, IPunObservable, IInRoomCallbacks
  {
    [SerializeField]
    private AvatarGraphics _graphics;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private MultiplayerBot _autoBotPrefab;
    [SerializeField]
    private BallPrediction _ballPredictionPrefab;
    [SerializeField]
    private PhotonView _photonView;
    [SerializeField]
    private SpawnEffect _spawnEffectPrefab;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private FakeShadow _shadow;
    [SerializeField]
    private DynamicTarget _dynamicTarget;
    [SerializeField]
    private PhotonTransformView[] _transformViews;
    [SerializeField]
    private PlayerName _playerNamePrefab;
    [SerializeField]
    private PlayerSphereOfInfluence _sphereOfInfluencePrefab;
    [SerializeField]
    private HealthBarNetworked _healthBar;
    [SerializeField]
    private HealthBarNetworked _frontFacingHealthBar;
    [SerializeField]
    private Transform _autoAimTarget;
    [SerializeField]
    private Transform _headTarget;
    [SerializeField]
    private MotionTracker _motionTracker;
    [Header("DEBUG")]
    [SerializeField]
    private bool _testDamagePlayer;
    [SerializeField]
    private bool _testOutPlayer;
    private SpawnEffect _spawnEffect;
    private RealPlayerTarget _target;
    private PlayerName _playerName;
    private MonoBehaviorObjectPool<BallPrediction> _ballPredictionsPool;
    private MicAmplifier _micAmp;
    private int _cachedPlayerId;
    private bool _hasCustomizationData;
    private bool _hasCreatedUniform;
    private PlayerCustomization _customizationData;

    public bool IsMe() => this._photonView.IsMine;

    public int playerId => this._photonView.OwnerActorNr;

    protected override void OnInitialize()
    {
      this.HelmetController.ShowHelmet(!this._photonView.IsMine, true);
      this._cachedPlayerId = this.playerId;
      this._motionTracker = this._autoAimTarget.GetComponent<MotionTracker>();
      if (this._photonView.IsMine)
      {
        Debug.Log((object) ("OnInitialize: ME: playerId[" + this.playerId.ToString() + "]"));
        base.OnInitialize();
        this._throwManager.HandsDataModel.OnTwoHandedGrab += (Action<BallObject>) (ball =>
        {
          if (!(ball is BallObjectNetworked))
            return;
          this.RightController.PickBall(ball);
        });
        MultiplayerManager.OnWillDisconnect += new System.Action(this.HandleWillDisconnect);
        this._bodyController.OnBallCollision += new Action<BallObject>(this.HandleBallCollision);
        this.HelmetController.OnBallCollision += new Action<BallObject>(this.HandleBallCollision);
        this.LeftController.OnBallPicked += new Action<EHand, BallObject>(this.HandleBallPicked);
        this.LeftController.OnPoseChanged += new Action<EHand, EHandPose>(this.HandlePoseChanged);
        this.LeftController.OnBallCollision += new Action<BallObject>(this.HandleBallCollision);
        this.RightController.OnBallPicked += new Action<EHand, BallObject>(this.HandleBallPicked);
        this.RightController.OnPoseChanged += new Action<EHand, EHandPose>(this.HandlePoseChanged);
        this.RightController.OnBallCollision += new Action<BallObject>(this.HandleBallCollision);
        this._linksHandler.AddLinks((IReadOnlyList<EventHandle>) new EventHandle[11]
        {
          MultiplayerEvents.PlayIntro.Link<Vector3>(new Action<Vector3>(this.HandlePlayIntro)),
          MultiplayerEvents.FireSpawnEffect.Link<Vector3>(new Action<Vector3>(this.PlaySpawnEffect)),
          MultiplayerEvents.BallWasThrown.Link<Vector3, Vector3, int>(new Action<Vector3, Vector3, int>(this.HandleBallThrown)),
          VRState.BigSizeMode.Link<bool>(new Action<bool>(this.HandleSizeMode), false),
          VRState.Muted.Link<bool>(new Action<bool>(this.HandleMuted)),
          VREvents.PlayerPositionUpdated.Link(new System.Action(this.HandlePlayerPositionChanged)),
          ScriptableSingleton<VRSettings>.Instance.MicVolume.Link<float>(new Action<float>(this.AdjustMicVolume)),
          VREvents.DropBall.Link(new System.Action(this.HandleDropBall)),
          ScriptableSingleton<VRSettings>.Instance.HelmetActive.Link<bool>(new Action<bool>(this.HandleHelmetActive)),
          MultiplayerEvents.KickPlayer.Link<int>(new Action<int>(this.HandleKickPlayer)),
          PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.Link<bool>(new Action<bool>(this.DuringTransitionCreateUniform))
        });
        this.ApplyCustomization();
        this._ballPredictionsPool = new MonoBehaviorObjectPool<BallPrediction>(this._ballPredictionPrefab, this.transform, 2);
      }
      else
      {
        Debug.Log((object) ("OnInitialize: NOT ME: playerId[" + this.playerId.ToString() + "]"));
        this._playerName = UnityEngine.Object.Instantiate<PlayerName>(this._playerNamePrefab, this.HelmetController.transform);
        this._playerName.transform.Rotate(new Vector3(0.0f, 180f, 0.0f));
        this._playerName.CheckHostIcon(this._photonView.OwnerActorNr == PhotonNetwork.MasterClient.ActorNumber);
        if (true)
          this.PlayIntroRPC(this.transform.position.SetY(0.0f), 0.3f);
        else
          this.SetAvatarVisible(false);
        this._target = new RealPlayerTarget(this._autoAimTarget, this._photonView.OwnerActorNr, this._motionTracker);
        this._throwManager.RegisterTarget((IThrowTarget) this._target);
        this._linksHandler.AddLink(ScriptableSingleton<VRSettings>.Instance.VCVolume.Link<float>(new Action<float>(this.AdjustVCVolume)));
        this._linksHandler.AddLink(PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.Link<bool>(new Action<bool>(this.DuringTransitionCreateUniform)));
        this._linksHandler.AddLink(MultiplayerEvents.UpdateNames.Link(new System.Action(this.UpdateHostIcon)));
        this.ApplyControllerGraphics(false);
      }
      UnityEngine.Object.Instantiate<PlayerSphereOfInfluence>(this._sphereOfInfluencePrefab, this.transform).Initialize(this.HelmetController.transform, this);
      this.SetAvatarVisible(true);
      this.SetBothWristbandsVisible(false);
    }

    private void HandlePlayerCollision(
      Vector3 position,
      Vector3 direction,
      int tackledPlayerId,
      bool highImpact,
      bool knockDown)
    {
      if (tackledPlayerId == PhotonNetwork.LocalPlayer.ActorNumber)
        Debug.LogError((object) "Shouldnt be able to tackle yourself..");
      else
        this._photonView.RPC("PlayerCollisionRPC", RpcTarget.All, (object) position, (object) direction, (object) tackledPlayerId, (object) highImpact, (object) knockDown);
    }

    private void HandleDropBall()
    {
      this.LeftController.DropBall();
      this.RightController.DropBall();
    }

    private void HandleBallCollision(BallObject ball)
    {
      if (!(ball is BallObjectNetworked ballObjectNetworked) || ballObjectNetworked.PhotonView.IsMine)
        return;
      PhotonNetwork.SendAllOutgoingCommands();
    }

    private void HandleWillDisconnect()
    {
      ExitGames.Client.Photon.Hashtable customProperties = this._photonView.Owner.CustomProperties;
      customProperties[(object) "visible"] = (object) false;
      PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
      if (!((UnityEngine.Object) this._dynamicTarget != (UnityEngine.Object) null))
        return;
      this._dynamicTarget.ResetState();
    }

    public override void Deinitialize(bool alreadyBeingDestroyed = false)
    {
      if (this.IsMe())
        VRState.BigSizeMode.SetValue(false);
      Debug.Log((object) ("Deinitialize: _cachedPlayerId[" + this._cachedPlayerId.ToString() + "]"));
      this._linksHandler.Clear();
      this._throwManager.UnregisterTarget((IThrowTarget) this._target);
      this._target = (RealPlayerTarget) null;
      MultiplayerManager.OnWillDisconnect -= new System.Action(this.HandleWillDisconnect);
      if ((UnityEngine.Object) this._bodyController != (UnityEngine.Object) null)
        this._bodyController.OnBallCollision -= new Action<BallObject>(this.HandleBallCollision);
      if ((UnityEngine.Object) this.HelmetController != (UnityEngine.Object) null)
        this.HelmetController.OnBallCollision -= new Action<BallObject>(this.HandleBallCollision);
      if ((UnityEngine.Object) this.LeftController != (UnityEngine.Object) null)
      {
        this.LeftController.OnBallPicked -= new Action<EHand, BallObject>(this.HandleBallPicked);
        this.LeftController.OnPoseChanged -= new Action<EHand, EHandPose>(this.HandlePoseChanged);
        this.LeftController.OnBallCollision -= new Action<BallObject>(this.HandleBallCollision);
      }
      if ((UnityEngine.Object) this.RightController != (UnityEngine.Object) null)
      {
        this.RightController.OnBallPicked -= new Action<EHand, BallObject>(this.HandleBallPicked);
        this.RightController.OnPoseChanged -= new Action<EHand, EHandPose>(this.HandlePoseChanged);
        this.RightController.OnBallCollision -= new Action<BallObject>(this.HandleBallCollision);
      }
      this._uniformsStore.UnregisterPlayer(this._cachedPlayerId);
      if (this._target != null)
        this._throwManager.UnregisterTarget((IThrowTarget) this._target);
      this._ballPredictionsPool?.ClearAll();
      base.Deinitialize(alreadyBeingDestroyed);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
      if (stream.IsWriting)
        stream.SendNext((object) ((double) PhotonVoiceNetwork.Instance.PrimaryRecorder.LevelMeter.CurrentAvgAmp > (double) ScriptableSingleton<VRSettings>.Instance.AudioSpeakThreshold));
      else
        this._playerName.SpeakerEnabled = (bool) stream.ReceiveNext();
    }

    private void SetAvatarVisible(bool state, bool includeBody = true)
    {
      if (includeBody)
      {
        this.LeftController.Renderer.SetRenderState(state);
        this.RightController.Renderer.SetRenderState(state);
        if (!this._photonView.IsMine)
          this.HelmetController.SetRenderState(state, true);
        this._bodyController.SetVisible(state);
      }
      this._shadow.gameObject.SetActive(state);
    }

    public void SetBothWristbandsVisible(bool state) => this.SetWristbandsVisible(state, state);

    public void ToggleWristbands() => this.SetWristbandsVisible(!this.LeftController.Wristband.gameObject.activeSelf, !this.RightController.Wristband.gameObject.activeSelf);

    public void SetWristbandsVisible(bool leftState, bool rightState)
    {
      this.LeftController.Wristband.gameObject.SetActive(leftState);
      this.RightController.Wristband.gameObject.SetActive(rightState);
    }

    private void HandleBallThrown(Vector3 startPos, Vector3 throwVec, int targetId)
    {
      if (!ScriptableSingleton<MultiplayerSettings>.Instance.ShowThrowPredictions || targetId == PhotonNetwork.LocalPlayer.ActorNumber)
        return;
      BallPrediction ballPrediction = this._ballPredictionsPool.GetObject();
      ballPrediction.ShowTrail(startPos, throwVec);
      ballPrediction.SetWidth(0.15f);
      ballPrediction.OnDone += new Action<BallPrediction>(((ObjectPoolBase<BallPrediction>) this._ballPredictionsPool).ReturnObject);
    }

    private void HandleBallPicked(EHand hand, BallObject ball)
    {
      if (!(ball is BallObjectNetworked ballObjectNetworked))
        return;
      ballObjectNetworked.PhotonView.RequestOwnership();
      this._photonView.RPC("PickBallRpc", RpcTarget.Others, (object) ballObjectNetworked.PhotonView.ViewID, (object) (int) hand);
      PhotonNetwork.SendAllOutgoingCommands();
    }

    [PunRPC]
    public void PickBallRpc(int ballViewId, int handIndex, PhotonMessageInfo info)
    {
      PhotonView photonView = PhotonView.Find(ballViewId);
      if (MultiplayerManager.debugMode)
        Debug.Log((object) ("info.Sender.ActorNumber[" + info.Sender.ActorNumber.ToString() + "] photonView.Owner.ActorNumber[" + photonView.Owner.ActorNumber.ToString() + "]"));
      if ((UnityEngine.Object) photonView == (UnityEngine.Object) null || info.Sender.ActorNumber != photonView.Owner.ActorNumber)
        return;
      if (MultiplayerManager.debugMode)
        Debug.Log((object) ("PickBallRPC delay: " + (PhotonNetwork.Time - info.SentServerTime).ToString()));
      BallObject component;
      if (!photonView.TryGetComponent<BallObject>(out component))
        return;
      List<HandData> handDatas = this._throwManager.HandsDataModel.HandDatas;
      this._throwManager.HandsDataModel.StopTwoHandedCatch(component);
      foreach (HandData handData in handDatas)
      {
        if ((UnityEngine.Object) handData.CurrentObject == (UnityEngine.Object) component)
          handData.CurrentObject = (BallObject) null;
      }
      EHand ehand = (EHand) handIndex;
      if (MultiplayerManager.debugMode)
        Debug.Log((object) ("Catching ball with " + ehand.ToString() + " Hand"));
      (ehand == EHand.Right ? this.RightController : this.LeftController).PickBall(component);
    }

    private void HandlePoseChanged(EHand handId, EHandPose poseId) => this._photonView.RPC("ApplyHandPoseRPC", RpcTarget.Others, (object) (int) poseId, (object) (int) handId);

    [PunRPC]
    public void ApplyHandPoseRPC(int poseId, int handId)
    {
      EHandPose pose = (EHandPose) poseId;
      (handId == 1 ? this.RightController : this.LeftController).ApplyHandPose(pose);
    }

    private void ApplyCustomization() => this._photonView.RPC("ApplyPlayerCustomizationRPC", RpcTarget.AllBuffered, (object) JsonUtility.ToJson((object) this._playerProfile.Customization), (object) "");

    [PunRPC]
    public void ApplyPlayerCustomizationRPC(string customDataJson, string PlatformUserName)
    {
      this._customizationData = JsonUtility.FromJson<PlayerCustomization>(customDataJson);
      this._hasCustomizationData = true;
      this._customizationData.UserName.SetValue(PlatformUserName);
      this._graphics.SetStoreByGender(this._customizationData.BodyType.Value == global::EBodyType.Male ? CharacterCustomizationStore.Gender.Male : CharacterCustomizationStore.Gender.Female);
      CharacterParameters characterParameters = this._graphics.GetParams((string) this._customizationData.AvatarPresetId);
      this.HandleGloves((EGlovesId) (Variable<EGlovesId>) this._customizationData.GloveId, (ETeamUniformId) (Variable<ETeamUniformId>) this._customizationData.Uniform);
      this._playerProfile.Profiles[this._photonView.OwnerActorNr] = this._customizationData;
      this.HelmetController.ApplyCustomization((ETeamUniformId) (Variable<ETeamUniformId>) this._customizationData.Uniform);
      this._bodyController.Initialize();
      this._bodyController.AdjustPlayerHeight(this._customizationData.HeightScale);
      this._dynamicTarget.SetColor((Color) this._customizationData.TrailColor);
      this._bodyController.SetUniformData(this.playerId, this._customizationData);
      if (!this._photonView.IsMine)
        this.ApplyNameCustomization(this._customizationData);
      this._graphics.SetHead(characterParameters.GetHead());
      this.SetFace(characterParameters).ConfigureAwait(false);
    }

    private async Task SetFace(CharacterParameters characterParameters)
    {
      AvatarGraphics avatarGraphics = this._graphics;
      avatarGraphics.SetFace(await characterParameters.GetFace(this._graphics.charactersStore));
      avatarGraphics = (AvatarGraphics) null;
    }

    private void HandlePlayIntro(Vector3 position)
    {
      ExitGames.Client.Photon.Hashtable customProperties = this._photonView.Owner.CustomProperties;
      customProperties[(object) "visible"] = (object) true;
      PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
      this._photonView.RPC("PlayIntroRPC", RpcTarget.Others, (object) position, (object) -1f);
      PhotonNetwork.SendAllOutgoingCommands();
    }

    [PunRPC]
    public void PlayIntroRPC(Vector3 position, float delay) => RoutineRunner.StartRoutine(this.IntroEffectRoutine(position, delay));

    private IEnumerator IntroEffectRoutine(Vector3 position, float delay)
    {
      PlayerAvatarNetworked playerAvatarNetworked = this;
      playerAvatarNetworked.SetAvatarVisible(false, false);
      if ((double) delay > 0.0)
      {
        yield return (object) new WaitForSeconds(delay);
        position = playerAvatarNetworked.HelmetController.transform.position.SetY(0.0f);
      }
      playerAvatarNetworked._bodyController.SetVisible(true);
      if ((UnityEngine.Object) playerAvatarNetworked._spawnEffect == (UnityEngine.Object) null)
        playerAvatarNetworked.PlaySpawnEffect(position);
      else
        Debug.LogError((object) "Trying to play spawnEffect for avatar when it already played / is playing");
      yield return (object) new WaitForSeconds(0.7f);
      playerAvatarNetworked.SetAvatarVisible(true);
    }

    public void PlaySpawnEffect(Vector3 position)
    {
      this._spawnEffect = UnityEngine.Object.Instantiate<SpawnEffect>(this._spawnEffectPrefab, this.transform);
      this._spawnEffect.transform.position = position.SetY(0.02f);
      this._spawnEffect.transform.localScale = Vector3.one * 0.5f;
      this._spawnEffect.Visible = true;
    }

    private void ApplyNameCustomization(PlayerCustomization customizationData) => this._playerName.SetName(!string.IsNullOrEmpty(customizationData.LastName.Value) ? (string) customizationData.LastName : "Player " + this._photonView.Owner.UserId);

    private void HandleSizeMode(bool bigSize)
    {
      if ((UnityEngine.Object) this._photonView != (UnityEngine.Object) null && this._photonView.isActiveAndEnabled)
        this._photonView.RPC("ApplySize", RpcTarget.AllBuffered, (object) (float) (bigSize ? (double) VRState.BigSizeScale : 1.0));
      else
        this.ApplySize(1f);
      VREvents.UpdateUI.Trigger();
    }

    [PunRPC]
    public void ApplySize(float size)
    {
      Vector3 vector3 = size * Vector3.one;
      this.transform.localScale = vector3;
      if (this._photonView.IsMine)
      {
        PersistentSingleton<GamePlayerController>.Instance.transform.localScale = vector3;
      }
      else
      {
        foreach (PhotonTransformView transformView in this._transformViews)
          transformView.SnapPosition();
      }
    }

    [PunRPC]
    public void PlayerCollisionRPC(
      Vector3 position,
      Vector3 impactVector,
      int tackledPlayerId,
      bool highImpact,
      bool knockDown)
    {
      int num = knockDown ? 1 : 0;
      if (PhotonNetwork.LocalPlayer.ActorNumber != tackledPlayerId)
        return;
      if (highImpact | knockDown)
        VREvents.PlayerPushed.Trigger(impactVector, knockDown);
      else
        PersistentSingleton<GamePlayerController>.Instance.position += impactVector;
    }

    private void HandleMuted(bool muted) => this._photonView.RPC("SetMutedStateRPC", RpcTarget.Others, (object) muted);

    private void HandlePlayerPositionChanged() => this._photonView.RPC("SnapPositionRPC", RpcTarget.Others, (object) this.HelmetController.transform.position, (object) this.RightController.transform.position, (object) this.LeftController.transform.position);

    [PunRPC]
    public void SetMutedStateRPC(bool muted)
    {
      if (!(bool) (UnityEngine.Object) this._playerName)
        return;
      this._playerName.SetMuted(muted);
    }

    [PunRPC]
    public void SnapPositionRPC(Vector3 headPos, Vector3 rPos, Vector3 lPos)
    {
      if (this._transformViews.Length <= 2)
        return;
      this._transformViews[0].SnapPosition(headPos);
      this._transformViews[1].SnapPosition(rPos);
      this._transformViews[2].SnapPosition(lPos);
    }

    public void RegisterTarget() => this._photonView.RPC("RegisterTargetRPC", RpcTarget.Others);

    [PunRPC]
    public void RegisterTargetRPC()
    {
      Debug.Log((object) ("RegisterTargetRPC _photonView.OwnerActorNr[" + this._photonView.OwnerActorNr.ToString() + "]"));
      this._target = new RealPlayerTarget(this._autoAimTarget, this._photonView.OwnerActorNr, this._motionTracker);
      this._throwManager.RegisterTarget((IThrowTarget) this._target);
    }

    [PunRPC]
    public void UnregisterTargetRPC()
    {
      Debug.Log((object) nameof (UnregisterTargetRPC));
      this._throwManager.UnregisterTarget((IThrowTarget) this._target);
      this._target = (RealPlayerTarget) null;
    }

    private void AdjustVCVolume(float volume) => this._audioSource.volume = volume;

    private void AdjustMicVolume(float volume)
    {
      if ((UnityEngine.Object) this._micAmp == (UnityEngine.Object) null)
      {
        Recorder recorderInUse = this._audioSource.GetComponent<PhotonVoiceView>().RecorderInUse;
        if (!((UnityEngine.Object) recorderInUse != (UnityEngine.Object) null))
          return;
        this._micAmp = recorderInUse.gameObject.GetComponent<MicAmplifier>();
      }
      this._micAmp.AmplificationFactor = volume;
    }

    private void HandleHelmetActive(bool active)
    {
      this.HelmetController.ShowHelmet(active);
      this.HelmetController.SetRenderState(active && !VRSettings.forceHideHelmet);
    }

    private void HandleKickPlayer(int playerID)
    {
      if (playerID != PhotonNetwork.LocalPlayer.ActorNumber)
        return;
      PersistentSingleton<BallsContainerManager>.Instance.Clear();
      VRState.PauseMenu.Toggle();
      AppEvents.LoadMainMenu.Trigger();
    }

    public void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
    }

    public void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
    }

    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
    }

    public void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
    }

    public void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
      MultiplayerEvents.UpdateNames.Trigger();
      this.UpdateHostIcon();
    }

    private void UpdateHostIcon()
    {
      if (!((UnityEngine.Object) this._playerName != (UnityEngine.Object) null))
        return;
      this._playerName.CheckHostIcon(this._photonView.OwnerActorNr == PhotonNetwork.MasterClient.ActorNumber);
    }

    public void DuringTransitionCreateUniform(bool InTransition)
    {
    }

    public IEnumerator ApplyUniformInLoadScreen()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      PlayerAvatarNetworked playerAvatarNetworked = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        Console.WriteLine("ApplyUniformInLoadScreen : Done");
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      Console.WriteLine("ApplyUniformInLoadScreen : DelayStart");
      playerAvatarNetworked._bodyController.SetUniformData(playerAvatarNetworked.playerId, playerAvatarNetworked._customizationData);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) new WaitForSeconds(0.5f);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }
}
