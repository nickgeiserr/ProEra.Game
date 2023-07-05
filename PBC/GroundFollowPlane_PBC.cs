// Decompiled with JetBrains decompiler
// Type: PBC.GroundFollowPlane_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class GroundFollowPlane_PBC : MonoBehaviour
  {
    private AdvancedFootIK_PBC footIK;
    public Transform character;

    private void OnEnable()
    {
      this.GetComponent<Collider>().enabled = false;
      if (!(bool) (Object) this.character)
      {
        Debug.LogWarning((object) ("No character assigned to script GroundFollowPlane on " + this.name + ".\n"));
        this.enabled = false;
      }
      else if (!(bool) (Object) (this.footIK = this.character.GetComponent<AdvancedFootIK_PBC>()))
      {
        Debug.LogWarning((object) ("The GroundFollowPlane script on " + this.name + " cannot find an AdvancedFootIK script on " + this.character.name + ".\n"));
        this.enabled = false;
      }
      else
      {
        this.transform.position = this.footIK.transformTarget;
        this.transform.rotation = Quaternion.LookRotation(-this.footIK.destinationNormal_Hat, Vector3.forward);
      }
    }

    private void Update()
    {
      this.transform.position = this.footIK.transformTarget;
      this.transform.rotation = Quaternion.LookRotation(-this.footIK.destinationNormal_Hat, Vector3.forward);
    }
  }
}
