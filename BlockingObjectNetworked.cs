// Decompiled with JetBrains decompiler
// Type: BlockingObjectNetworked
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using Framework.Data;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingObjectNetworked : MonoBehaviour
{
  [SerializeField]
  private Renderer _renderer;
  [SerializeField]
  private Collider _collider;
  [SerializeField]
  private PhotonView _photonView;
  [SerializeField]
  private bool _allowRespawn;
  [SerializeField]
  private SpawnEffect _spawnEffect;
  private readonly LinksHandler _linksHandler = new LinksHandler();
  private readonly RoutineHandle _routineHandle = new RoutineHandle();
  private WaitForSeconds wait;

  public float RespawnDelay { get; set; } = 5f;

  private void Awake()
  {
    this._linksHandler.AddLinks((IReadOnlyList<EventHandle>) new EventHandle[1]
    {
      MultiplayerEvents.LoadMultiplayerGame.Link<string>((Action<string>) (obj =>
      {
        if (!PhotonNetwork.IsMasterClient)
          return;
        PhotonNetwork.Destroy(this.gameObject);
      }))
    });
    this._spawnEffect.Initialize();
  }

  private void Start()
  {
    this.wait = new WaitForSeconds(this.RespawnDelay);
    this._spawnEffect.Visible = true;
  }

  private void OnCollisionEnter(Collision other)
  {
    Debug.Log((object) "Blocking Dummy hit!");
    if (!PhotonNetwork.IsMasterClient)
      return;
    BallObjectNetworked component;
    if (other.transform.TryGetComponent<BallObjectNetworked>(out component))
    {
      Debug.Log((object) "Blocking Dummy hit by ball!");
      if (component.IsGiant)
      {
        Debug.Log((object) "Blocking Dummy hit by GIANT ball!");
        this._photonView.RPC("SetStateRPC", RpcTarget.AllViaServer, (object) false);
        if (this._allowRespawn)
          this._routineHandle.Run(this.RespawnRoutine());
      }
    }
    PhotonNetwork.Destroy(component.gameObject);
    Debug.Log((object) "Ball destroyed by masterclient!");
  }

  [PunRPC]
  public void SetStateRPC(bool state)
  {
    Debug.Log((object) ("Blocking dummy state is: " + state.ToString()));
    this._spawnEffect.Visible = state;
    this._collider.enabled = state;
  }

  private IEnumerator RespawnRoutine()
  {
    yield return (object) this.wait;
    this._photonView.RPC("SetStateRPC", RpcTarget.AllViaServer, (object) true);
  }
}
