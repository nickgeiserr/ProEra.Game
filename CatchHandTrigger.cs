// Decompiled with JetBrains decompiler
// Type: CatchHandTrigger
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UDB;
using UnityEngine;
using UnityEngine.Events;

public class CatchHandTrigger : MonoBehaviour
{
  public UnityEvent onBallInsideTrigger;

  private void OnTriggerEnter(Collider other) => this.OnTriggerEventsHandler(other);

  private void OnTriggerStay(Collider other) => this.OnTriggerEventsHandler(other);

  private void OnTriggerExit(Collider other) => this.OnTriggerEventsHandler(other);

  private void OnTriggerEventsHandler(Collider other)
  {
    if (!((Object) other.gameObject == (Object) SingletonBehaviour<BallManager, MonoBehaviour>.instance.gameObject))
      return;
    this.onBallInsideTrigger.Invoke();
  }
}
