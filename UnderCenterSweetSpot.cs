// Decompiled with JetBrains decompiler
// Type: UnderCenterSweetSpot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class UnderCenterSweetSpot : MonoBehaviour
{
  private List<HandData> _handList;
  private bool _ball;

  private void Start() => this._handList = new List<HandData>();

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer != LayerMask.NameToLayer("UserAvatar"))
      return;
    HandController componentInParent = other.GetComponentInParent<HandController>();
    if (!((Object) componentInParent != (Object) null))
      return;
    this._handList.Add(componentInParent.GetHandData());
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.layer != LayerMask.NameToLayer("UserAvatar"))
      return;
    HandController componentInParent = other.GetComponentInParent<HandController>();
    if (!((Object) componentInParent != (Object) null))
      return;
    this._handList.Remove(componentInParent.GetHandData());
  }

  private void OnDisable() => this._handList.Clear();

  public bool HasHand(HandData hand) => this._handList.Contains(hand);

  public bool HasBall()
  {
    float num = Vector3.Distance(SingletonBehaviour<BallManager, MonoBehaviour>.instance.GetClosestPointToPosition(this.transform.position), this.transform.position);
    Debug.Log((object) ("HasBall: " + num.ToString()));
    return (double) num <= 0.550000011920929;
  }
}
