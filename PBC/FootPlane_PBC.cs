// Decompiled with JetBrains decompiler
// Type: PBC.FootPlane_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class FootPlane_PBC : MonoBehaviour
  {
    private AdvancedFootIK_PBC footIK;
    public Transform character;

    private void OnEnable()
    {
      this.footIK = this.character.GetComponent<AdvancedFootIK_PBC>();
      this.transform.position = this.footIK.predictPlanePos;
      this.transform.rotation = Quaternion.LookRotation(-this.footIK.predictTargetPlane, Vector3.forward);
    }

    private void Update()
    {
      this.transform.position = this.footIK.predictPlanePos;
      this.transform.rotation = Quaternion.LookRotation(-this.footIK.predictTargetPlane, Vector3.forward);
      Debug.DrawRay(this.footIK.predictPlanePos, -this.footIK.raycastDown_Hat, Color.magenta);
    }
  }
}
