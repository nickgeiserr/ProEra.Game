// Decompiled with JetBrains decompiler
// Type: FootballVR.PlayerAvatarHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using Framework;
using Framework.Data;
using Framework.Networked;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class PlayerAvatarHandler : PersistentSingleton<PlayerAvatarHandler>
  {
    [SerializeField]
    private PlayerAvatar _singlePlayer;
    [SerializeField]
    private PlayerAvatar _multiPlayer;
    [SerializeField]
    private string _multiPlayerPrefabName;
    [SerializeField]
    private BallThrowMechanic _throwMechanicPrefab;
    private PlayerAvatar _currentAvatar;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    public static readonly Variable<PlayerAvatarHandler.EBodyType> BodyType = new Variable<PlayerAvatarHandler.EBodyType>(PlayerAvatarHandler.EBodyType.Unknown);
    private bool _throwingInitialized;

    public PlayerAvatar CurrentAvatar => this._currentAvatar;

    protected override void Awake()
    {
      base.Awake();
      this.InitializeThrowing();
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        VREvents.PlayerPositionUpdated.Link(new System.Action(this.SyncPosition)),
        NetworkState.InRoom.Link<bool>(new Action<bool>(this.HandleMultiplayerState))
      });
    }

    private void SyncPosition() => this.CurrentAvatar.SyncBodyPosition();

    private void OnValidate()
    {
      if (!((UnityEngine.Object) this._multiPlayer != (UnityEngine.Object) null) || !((UnityEngine.Object) this._multiPlayer.gameObject != (UnityEngine.Object) null))
        return;
      this._multiPlayerPrefabName = this._multiPlayer.gameObject.name;
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      this._linksHandler.Clear();
    }

    private void InitializeThrowing()
    {
      if (this._throwingInitialized)
        return;
      GamePlayerController.PlayerTrackingRefs playerRefs = PersistentSingleton<GamePlayerController>.Instance.PlayerRefs;
      BallThrowMechanic ballThrowMechanic1 = UnityEngine.Object.Instantiate<BallThrowMechanic>(this._throwMechanicPrefab, playerRefs.leftHandAnchor);
      ballThrowMechanic1.transform.ResetTransform();
      ballThrowMechanic1.Initialize(EHand.Left);
      BallThrowMechanic ballThrowMechanic2 = UnityEngine.Object.Instantiate<BallThrowMechanic>(this._throwMechanicPrefab, playerRefs.rightHandAnchor);
      ballThrowMechanic2.transform.ResetTransform();
      ballThrowMechanic2.Initialize(EHand.Right);
      this._throwingInitialized = true;
    }

    private void HandleMultiplayerState(bool connected)
    {
      if (UnityState.quitting)
        return;
      if ((UnityEngine.Object) this._currentAvatar != (UnityEngine.Object) null)
        this._currentAvatar.Deinitialize();
      GamePlayerController.PlayerTrackingRefs playerRefs = PersistentSingleton<GamePlayerController>.Instance.PlayerRefs;
      this._currentAvatar = !connected ? UnityEngine.Object.Instantiate<PlayerAvatar>(this._singlePlayer, playerRefs.headAnchor.position.SetY(0.0f), Quaternion.identity) : (PlayerAvatar) PhotonNetwork.Instantiate(this._multiPlayerPrefabName, playerRefs.headAnchor.position.SetY(0.0f), Quaternion.identity).GetComponent<PlayerAvatarNetworked>();
      this._currentAvatar.HelmetController.transform.SetParentAndReset(playerRefs.headAnchor);
      this._currentAvatar.LeftController.transform.SetParentAndReset(playerRefs.leftHandAnchor);
      this._currentAvatar.RightController.transform.SetParentAndReset(playerRefs.rightHandAnchor);
      if (connected)
      {
        this.SetHelmetOnHead();
        this._currentAvatar.BodyController.AvatarGraphicsComponent.SetHeadState(false);
        this._currentAvatar.HelmetController.Renderer.enabled = false;
      }
      this._currentAvatar.Initialize();
      this._currentAvatar.SyncBodyPosition();
      PlayerAvatarHandler.BodyType.SetValue(connected ? PlayerAvatarHandler.EBodyType.Multiplayer : PlayerAvatarHandler.EBodyType.Singleplayer);
      if (!connected)
        return;
      this.SetHelmetOnHead();
    }

    public void SetHelmetOnHead()
    {
      Transform transform = this._currentAvatar.HelmetController.NetworkedRenderer.transform;
      transform.SetParentAndReset(this._currentAvatar.headTransform);
      transform.localPosition = new Vector3(-0.0597f, 0.0195f, 0.0f);
      transform.localEulerAngles = new Vector3(-90f, 90f, 0.0f);
    }

    public void SetupUI(HandController hand, GameObject prefab)
    {
      if ((UnityEngine.Object) prefab == (UnityEngine.Object) null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, hand.AnchorUI);
      gameObject.transform.ResetTransform();
      gameObject.transform.localScale = Vector3.one * 0.0042f;
    }

    public enum EBodyType
    {
      Unknown,
      Singleplayer,
      Multiplayer,
    }
  }
}
