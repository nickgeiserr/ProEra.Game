// Decompiled with JetBrains decompiler
// Type: FistBumpHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class FistBumpHandler : MonoBehaviour
{
  public bool Entered;

  private void OnTriggerEnter(Collider other)
  {
    if (this.Entered)
      return;
    this.Entered = true;
    FistBumpHandler component = other.GetComponent<FistBumpHandler>();
    if ((Object) component != (Object) null)
      component.Entered = true;
    GameplayStatics.PlayFirstBumpEffect((this.gameObject.transform.position + component.gameObject.transform.position) / 2f, this.gameObject.transform.rotation);
  }

  private void OnTriggerExit(Collider other) => this.Entered = false;
}
