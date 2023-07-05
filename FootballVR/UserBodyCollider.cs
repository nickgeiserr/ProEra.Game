// Decompiled with JetBrains decompiler
// Type: FootballVR.UserBodyCollider
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class UserBodyCollider : MonoBehaviour
  {
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private CapsuleCollider _collider;
    [SerializeField]
    private Vector3 _offset;
    private Transform _player;
    private Transform _cameraTx;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private const string attackerTag = "Attacker";

    public bool CollisionEnabled
    {
      set => this._collider.enabled = value;
    }

    public event Action<Collider> OnCollision;

    private void OnTriggerEnter(Collider other)
    {
      if (!other.CompareTag("Attacker"))
        return;
      AttackerBehaviorController component;
      if (other.TryGetComponent<AttackerBehaviorController>(out component))
        component.collisionEnabled = false;
      else
        Debug.LogError((object) "Couldnt find attacker behavior on trigger enter");
      Action<Collider> onCollision = this.OnCollision;
      if (onCollision == null)
        return;
      onCollision(other);
    }

    public void Awake()
    {
      this._player = PersistentSingleton<GamePlayerController>.Instance.transform;
      this._cameraTx = PlayerCamera.Camera.transform;
      CollisionSettings instance = ScriptableSingleton<CollisionSettings>.Instance;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        EventHandle.Link<float>((Variable<float>) instance.collidersSettings.PlayerColliderSize, (Action<float>) (size => this._collider.radius = size / 2f)),
        EventHandle.Link<bool>((Variable<bool>) instance.CollisionEnabled, (Action<bool>) (state => this._collider.enabled = state))
      });
      this._collider.isTrigger = true;
      this._rigidbody.isKinematic = true;
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void Update()
    {
      this._rigidbody.position = this._cameraTx.position + this._offset;
      this._rigidbody.rotation = Quaternion.LookRotation(this._cameraTx.forward.SetY(0.0f), this._player.up);
    }
  }
}
