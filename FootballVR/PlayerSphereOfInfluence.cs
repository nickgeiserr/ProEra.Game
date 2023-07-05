// Decompiled with JetBrains decompiler
// Type: FootballVR.PlayerSphereOfInfluence
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using Framework.Data;
using Photon.Pun;
using System;
using UnityEngine;

namespace FootballVR
{
  public class PlayerSphereOfInfluence : MonoBehaviour
  {
    [SerializeField]
    private float _ballGroundCheckTime = 1f;
    [SerializeField]
    private PlayerAvatarNetworked _playerAvatar;
    [SerializeField]
    private Rigidbody _rigidbody;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private Transform _headTx;

    private void Awake()
    {
      if ((UnityEngine.Object) this._headTx == (UnityEngine.Object) null)
        this.enabled = false;
      this._linksHandler.AddLink(MultiplayerEvents.BallGroundCheckTimer.Link<float>((Action<float>) (time => this._ballGroundCheckTime = time)));
    }

    public void Initialize(Transform headTx, PlayerAvatarNetworked playerAvatarNetworked)
    {
      this._playerAvatar = playerAvatarNetworked;
      this._headTx = headTx;
      this._rigidbody.isKinematic = true;
      this.enabled = true;
    }

    private void Update()
    {
      Vector3 position = this._headTx.position;
      position.y -= 0.5f;
      this._rigidbody.position = position;
    }

    private void OnCollisionEnter(Collision coll)
    {
      BallObjectNetworked component;
      if (!coll.gameObject.TryGetComponent<BallObjectNetworked>(out component) || this._playerAvatar.playerId == component.PhotonView.OwnerActorNr || component.inHand || !component.inFlight)
        return;
      Debug.Log((object) "Hit by ball!");
      if (component.hasHitTarget)
        return;
      Debug.Log((object) "Ball Hit Target!");
      component.CompleteBallFlight(false);
      MultiplayerEvents.BallHitPlayerFX.Trigger(this._playerAvatar.playerId);
      if (!PhotonNetwork.IsMasterClient)
        return;
      if ((double) this._ballGroundCheckTime <= 0.0)
        MultiplayerEvents.BallHitPlayerMaster.Trigger(component.PhotonView.Owner.ActorNumber, this._playerAvatar.playerId, component.IsGiant);
      else
        component.BallHitGround += new Action<BallObjectNetworked>(this.BallHitGround);
    }

    private void BallHitGround(BallObjectNetworked ball) => MultiplayerEvents.BallHitPlayerMaster.Trigger(ball.PhotonView.Owner.ActorNumber, this._playerAvatar.playerId, ball.IsGiant);

    public int GetPlayerID() => this._playerAvatar.playerId;
  }
}
