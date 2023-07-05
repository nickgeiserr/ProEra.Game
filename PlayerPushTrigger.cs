// Decompiled with JetBrains decompiler
// Type: PlayerPushTrigger
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class PlayerPushTrigger : MonoBehaviour
{
  private static Dictionary<GameObject, PlayerPushTrigger> _gameObjectToPlayerPushDict = new Dictionary<GameObject, PlayerPushTrigger>();
  public float pushSpeed = 1f;
  [SerializeField]
  private Rigidbody _rigidbody;
  private GameObject _gameObject;
  private static Vector3 pushAxis = new Vector3(1f, 0.0f, 1f);
  private bool _canBePushed = true;

  public bool CanBePushed
  {
    get => this._canBePushed;
    set => this._canBePushed = value;
  }

  private void Awake() => this._gameObject = this.gameObject;

  private void OnEnable() => PlayerPushTrigger._gameObjectToPlayerPushDict.Add(this._gameObject, this);

  private void OnDisable() => PlayerPushTrigger._gameObjectToPlayerPushDict.Remove(this._gameObject);

  private void OnTriggerEnter(Collider other) => this.PushColliderAway(other);

  private void OnTriggerStay(Collider other) => this.PushColliderAway(other);

  private void PushColliderAway(Collider other)
  {
    GameObject gameObject = other.gameObject;
    PlayerPushTrigger playerPushTrigger;
    if ((Object) gameObject == (Object) this._gameObject || !PlayerPushTrigger._gameObjectToPlayerPushDict.TryGetValue(gameObject, out playerPushTrigger) || !playerPushTrigger.CanBePushed)
      return;
    Rigidbody rigidbody = playerPushTrigger._rigidbody;
    Vector3 position1 = this._rigidbody.position;
    Vector3 position2 = rigidbody.position;
    Vector3 position3 = Vector3.MoveTowards(position2, position2 + (position2 - position1).normalized, this.pushSpeed * Time.fixedDeltaTime);
    position3.Scale(PlayerPushTrigger.pushAxis);
    rigidbody.MovePosition(position3);
  }
}
