// Decompiled with JetBrains decompiler
// Type: TimeStopper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class TimeStopper : MonoBehaviour
{
  private void Start()
  {
    if (Application.isEditor)
      return;
    Object.Destroy((Object) this.gameObject);
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.V))
      return;
    Time.timeScale = (double) Time.timeScale < 0.10000000149011612 ? 1f : 0.0f;
  }
}
