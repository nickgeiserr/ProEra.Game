// Decompiled with JetBrains decompiler
// Type: ThrowAbility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class ThrowAbility : MonoBehaviour
{
  [SerializeField]
  private AnimationEventAgent _eventAgent;
  [SerializeField]
  private NteractAgent _nteractAgent;
  [SerializeField]
  private ThrowDictionary _throwDictionary;
  [SerializeField]
  private ThrowDictionary _leftHandThrowDictionary;
  private Transform _trans;

  private void Awake() => this._trans = this.transform;

  public void ThrowBall(Vector3 targetPosition, bool leftHanded) => this._eventAgent.EnterEvent(AffineTransform.Origin, (leftHanded ? this._leftHandThrowDictionary : this._throwDictionary).GetThrow(this._trans, targetPosition, leftHanded));
}
