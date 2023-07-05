// Decompiled with JetBrains decompiler
// Type: FootballVR.Multiplayer.BallObjectNetworked
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using Framework.Data;
using Framework.Networked;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR.Multiplayer
{
  public class BallObjectNetworked : BallObject, IPunObservable, IPunOwnershipCallbacks
  {
    [SerializeField]
    private TeamBallMatStore _teamBallMatStore;
    [SerializeField]
    private PhotonView _photonView;
    [SerializeField]
    private BallObject _debugBall;
    [SerializeField]
    private bool _isMoneyBall;
    [SerializeField]
    private bool _isGiant;
    [SerializeField]
    private bool _hitGround;
    [SerializeField]
    private float _delayDestroy = 10f;
    [SerializeField]
    private bool _materialHasChanged;
    public Action<BallObjectNetworked> BallHitGround;
    private Vector3 latestPos;
    private Vector3 latestVelo;
    private Quaternion latestRot;
    private int ballMatID = -1;
    private bool _lockOwnership;
    private float _timeLastUsed;
    private float _gravityMultiplier = 1f;
    private BallFlightCompensationSettings _slowdownSettings;
    private BallFlightCompensationSettings _heightSettings;
    private readonly Vector3 _gravityVector = Vector3.down * 9.81f;
    private bool _manualFlight;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public PhotonView PhotonView => this._photonView;

    public override bool userThrown => this._photonView.IsMine;

    public bool IsMoneyBall => this._isMoneyBall;

    public bool IsGiant
    {
      get => this._isGiant;
      set => this._isGiant = value;
    }

    private MultiplayerSettings _settings => ScriptableSingleton<MultiplayerSettings>.Instance;

    private bool ManualFlight
    {
      set
      {
        if (this._manualFlight == value)
          return;
        this._manualFlight = value;
        this._rigidbody.useGravity = !this._manualFlight;
      }
    }

    protected override void Awake()
    {
      base.Awake();
      this._slowdownSettings = this._settings.FlightCompensationSettings;
      this._heightSettings = this._settings.HeightCompensationSettings;
      this.latestPos = this.transform.position;
      this.latestRot = this.transform.rotation;
      this.HandleUsed();
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        MultiplayerEvents.LoadMultiplayerGame.Link<string>((Action<string>) (s => this.CleanupBall()))
      });
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      this.BallHitGround = (Action<BallObjectNetworked>) null;
    }

    private void Update()
    {
      if ((double) this.transform.position.y <= 0.20000000298023224 && !this._hitGround)
      {
        this._hitGround = true;
        Action<BallObjectNetworked> ballHitGround = this.BallHitGround;
        if (ballHitGround != null)
          ballHitGround(this);
      }
      if ((double) this.transform.position.y < -200.0)
      {
        this.CleanupBall();
      }
      else
      {
        if ((double) Time.time - (double) this._timeLastUsed <= (double) this._delayDestroy)
          return;
        if (this.inHand || this.inFlight)
          this.HandleUsed();
        else
          this.CleanupBall();
      }
    }

    private void CleanupBall()
    {
      if (!PhotonNetwork.IsMasterClient || !((UnityEngine.Object) this._photonView != (UnityEngine.Object) null))
        return;
      this._photonView.RequestOwnership();
      PhotonNetwork.Destroy(this._photonView);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
      if (stream.IsWriting)
      {
        stream.SendNext((object) (this.latestVelo = this._rigidbody.velocity));
        stream.SendNext((object) (this.latestPos = this.transform.position));
        stream.SendNext((object) (this.latestRot = this.transform.rotation));
        stream.SendNext((object) this.Graphics.BallMaterialID.Value);
        stream.SendNext((object) this._lockOwnership);
      }
      else
      {
        this.latestVelo = (Vector3) stream.ReceiveNext();
        this.latestPos = (Vector3) stream.ReceiveNext();
        this.latestRot = (Quaternion) stream.ReceiveNext();
        int next = (int) stream.ReceiveNext();
        this._lockOwnership = (bool) stream.ReceiveNext();
        if (next != this.ballMatID)
        {
          this.ballMatID = next;
          TeamBallConfig teamBallConfig = this._teamBallMatStore.GetTeamBallConfig(this.ballMatID);
          if (teamBallConfig != null)
          {
            Material teamBallMaterial = teamBallConfig.teamBallMaterial;
            if ((UnityEngine.Object) teamBallMaterial != (UnityEngine.Object) null)
              this.Graphics.BallMaterial.SetValue(teamBallMaterial);
          }
        }
      }
      this.latestPos += this.latestVelo * Mathf.Abs((float) PhotonNetwork.Time - (float) info.SentServerTimestamp);
    }

    public override void Pick(bool twoHanded = false, bool resetTrail = true)
    {
      base.Pick(twoHanded, resetTrail);
      this.BallHitGround = (Action<BallObjectNetworked>) null;
      this.IsGiant = VRState.BigSizeMode.Value;
      this.HandleUsed();
    }

    public override void Throw(
      Vector3 startPos,
      Vector3 throwDirection,
      bool throwActivated,
      bool trailEnabled = true,
      bool hideTrail = true,
      float accuracy = 0.5f,
      int targetId = -1)
    {
      if (!this._photonView.IsMine)
        return;
      this._photonView.RPC("ThrowRPC", RpcTarget.Others, (object) startPos, (object) throwDirection, (object) throwActivated, (object) trailEnabled, (object) hideTrail, (object) accuracy, (object) targetId, (object) this.IsGiant);
      PhotonNetwork.SendAllOutgoingCommands();
      if (this._heightSettings.CompensateOnThrow)
        throwDirection = this.AdjustThrowHeight(startPos, throwDirection, (float) PhotonNetwork.GetPing() / 1000f, this._heightSettings.CompensateOnThrowMaxPercentage);
      if (this._slowdownSettings.CompensateOnThrow)
        throwDirection = this.ApplyManualThrow(startPos, throwDirection, (float) PhotonNetwork.GetPing() / 1000f, this._slowdownSettings.CompensateOnThrowMaxPercentage);
      base.Throw(startPos, throwDirection, throwActivated, trailEnabled, hideTrail, accuracy, targetId);
    }

    [PunRPC]
    public void ThrowRPC(
      Vector3 startPos,
      Vector3 throwImpulse,
      bool driveOrientation,
      bool trailEnabled,
      bool hideTrail,
      float accuracy,
      int targetId,
      bool isGiant,
      PhotonMessageInfo info)
    {
      this.IsGiant = isGiant;
      this.HandleUsed();
      float num = (float) (PhotonNetwork.Time - info.SentServerTime);
      if (MultiplayerManager.debugMode)
        Debug.Log((object) string.Format("ThrowRPC delay: {0}", (object) num));
      if (this._photonView.IsMine)
        return;
      if (VRState.ForceSyncThrows && (bool) this._settings.ForceBallSyncEnabled)
      {
        float time = num * this._settings.SnapBallSyncFactor;
        Vector3 distanceTraveled = AutoAim.GetDistanceTraveled(time, throwImpulse);
        startPos += distanceTraveled;
        throwImpulse = AutoAim.GetVelocityAfterTime(time, throwImpulse);
      }
      else if (this._slowdownSettings.CompensateOnReceive && PhotonNetwork.LocalPlayer.ActorNumber == targetId)
        throwImpulse = this.ApplyManualThrow(startPos, throwImpulse, (float) -PhotonNetwork.GetPing() / 1000f, this._slowdownSettings.CompensateOnReceiveMaxPercentage);
      base.Throw(startPos, throwImpulse, driveOrientation, trailEnabled, hideTrail, accuracy);
      MultiplayerEvents.BallWasThrown.Trigger(startPos, throwImpulse, targetId);
    }

    protected override void ApplyTrail()
    {
      if (this._photonView.IsMine)
      {
        base.ApplyTrail();
      }
      else
      {
        PlayerCustomization playerCustomization;
        if (!this._playerProfile.Profiles.TryGetValue(this._photonView.OwnerActorNr != 0 ? this._photonView.OwnerActorNr : PhotonNetwork.MasterClient.ActorNumber, out playerCustomization))
          return;
        this._graphics.TrailColor = (Color) playerCustomization.TrailColor;
        this._graphics.TrailType.SetValue((EBallTrail) (Variable<EBallTrail>) playerCustomization.TrailType);
      }
    }

    private Vector3 ApplyManualThrow(
      Vector3 startPos,
      Vector3 throwDirection,
      float timeAdjustment,
      float maxCoef)
    {
      maxCoef /= 100f;
      float flightTime = AutoAim.GetFlightTime(startPos, throwDirection, 1.2f);
      if ((double) flightTime < 0.0)
        return throwDirection;
      float num1 = flightTime * maxCoef;
      if (MultiplayerManager.debugMode)
        Debug.Log((object) string.Format("DesiredDelay {0} MaxDelay {1}", (object) timeAdjustment, (object) num1));
      if ((double) Mathf.Abs(timeAdjustment) > (double) num1)
        timeAdjustment = num1 * Mathf.Sign(timeAdjustment);
      float num2 = (float) (1.0 + (double) timeAdjustment / (double) flightTime);
      this._gravityMultiplier = (float) (1.0 / ((double) num2 * (double) num2));
      throwDirection /= num2;
      this.ManualFlight = true;
      return throwDirection;
    }

    private Vector3 AdjustThrowHeight(
      Vector3 startPos,
      Vector3 throwDirection,
      float timeAdjustment,
      float maxCoef)
    {
      maxCoef /= 100f;
      float time;
      Vector3 landingPoint = AutoAim.GetLandingPoint(startPos, throwDirection, 1.2f, out time);
      if ((double) time < 0.0)
        return throwDirection;
      float num = time * maxCoef;
      if (MultiplayerManager.debugMode)
        Debug.Log((object) string.Format("DesiredDelay {0} MaxDelay {1}", (object) timeAdjustment, (object) num));
      if ((double) Mathf.Abs(timeAdjustment) > (double) num)
        timeAdjustment = num * Mathf.Sign(timeAdjustment);
      Vector3 impulseToHitTarget = AutoAim.GetImpulseToHitTarget(landingPoint - startPos, time + timeAdjustment);
      if (!this._throwSettings.DebugMode)
        return impulseToHitTarget;
      UnityEngine.Object.Instantiate<BallObject>(this._debugBall).DebugThrow(startPos, throwDirection, Color.red);
      return impulseToHitTarget;
    }

    private void HandleUsed() => this._timeLastUsed = Time.time;

    private void FixedUpdate()
    {
      if (!this._manualFlight)
        return;
      if (!this.inFlight || this.inHand)
      {
        this.ManualFlight = false;
      }
      else
      {
        this._rigidbody.velocity += this._gravityVector * (Time.fixedDeltaTime * this._gravityMultiplier);
        if (!this._photonView.IsMine)
        {
          if (this.inFlight || this.inHand && !this.twoHandedGrab)
            return;
          this._rigidbody.position = Vector3.MoveTowards(this._rigidbody.position, this.latestPos, Time.fixedDeltaTime);
          this._rigidbody.rotation = Quaternion.RotateTowards(this._rigidbody.rotation, this.latestRot, Time.fixedDeltaTime * 100f);
          this._rigidbody.velocity = this.latestVelo;
        }
        else
        {
          this.latestPos = this.transform.position;
          this.latestRot = this.transform.rotation;
        }
      }
    }

    public override void Release()
    {
      if (!this.inHand)
        return;
      base.Release();
      if (!(bool) NetworkState.InRoom || !this._photonView.IsMine)
        return;
      this._photonView.RPC("ReleaseRPC", RpcTarget.Others);
      PhotonNetwork.SendAllOutgoingCommands();
    }

    public override void CompleteBallFlight(bool throwSuccess = true)
    {
      base.CompleteBallFlight(throwSuccess);
      this.HandleUsed();
    }

    [PunRPC]
    public void ReleaseRPC(PhotonMessageInfo info)
    {
      if (MultiplayerManager.debugMode)
        Debug.Log((object) string.Format("ReleaseRPC Sender:{0} PhotonView:{1}", (object) info.Sender.ActorNumber, (object) info.photonView));
      base.Release();
    }

    public void SetPhysics(bool state) => this._photonView.RPC("BallPhysicsRPC", RpcTarget.All, (object) state);

    [PunRPC]
    public void BallPhysicsRPC(bool state) => this.applyPhysics = state;

    public void ApplyBallCustomization() => this._photonView.RPC("ApplyBallCustomization_RPC", RpcTarget.AllViaServer, (object) this._playerProfile.Customization.MultiplayerTeamBallId.Value);

    [PunRPC]
    public void ApplyBallCustomization_RPC(int ballMatID)
    {
      if (this._materialHasChanged)
        return;
      this._materialHasChanged = true;
      Debug.Log((object) "Applying ball customization...");
      this.Graphics.BallMaterialID.SetValue(ballMatID);
    }

    void IPunOwnershipCallbacks.OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
    {
      if (!targetView.IsMine || targetView.ViewID != this._photonView.ViewID || this._lockOwnership)
        return;
      this._lockOwnership = true;
      targetView.TransferOwnership(requestingPlayer);
      PhotonNetwork.SendAllOutgoingCommands();
    }

    void IPunOwnershipCallbacks.OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner) => this._lockOwnership = false;

    void IPunOwnershipCallbacks.OnOwnershipTransferFailed(
      PhotonView targetView,
      Photon.Realtime.Player senderOfFailedRequest)
    {
    }
  }
}
