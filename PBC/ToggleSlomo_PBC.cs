// Decompiled with JetBrains decompiler
// Type: PBC.ToggleSlomo_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class ToggleSlomo_PBC : MonoBehaviour
  {
    public float slomoOnKeyN = 0.3f;
    private bool slomo;

    private void Update()
    {
      if (!Input.GetKeyDown(KeyCode.N))
        return;
      if (!this.slomo)
      {
        Time.timeScale = this.slomoOnKeyN;
        this.slomo = true;
      }
      else
      {
        Time.timeScale = 1f;
        this.slomo = false;
      }
    }
  }
}
