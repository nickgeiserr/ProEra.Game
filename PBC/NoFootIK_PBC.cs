// Decompiled with JetBrains decompiler
// Type: PBC.NoFootIK_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class NoFootIK_PBC : FootIKBaseClass_PBC
  {
    private float raycastLength = 1.5f;
    private LayerMask layerMask;
    private RaycastHit raycastHit;
    private bool userNeedsToFixStuff;
    [SerializeField]
    private string[] ignoreLayers = new string[1]{ "Water" };

    private void Awake()
    {
      this.SetIgnoreLayers();
      this.userNeedsToFixStuff = !this.WeHaveAllTheStuff();
    }

    public override void DoFootIK()
    {
      if (this.userNeedsToFixStuff)
        return;
      if (!Physics.Raycast(this.transform.position - this.raycastDown_Hat * this.raycastLength * 0.5f, this.raycastDown_Hat, out this.raycastHit, this.raycastLength, (int) this.layerMask))
      {
        this.transformTarget = this.transform.position + this.raycastDown_Hat * this.raycastLength * 0.5f;
        this.elevation = Vector3.Dot(this.transform.position - this.transformTarget, -this.raycastDown_Hat);
        this.nRaw_Hat = -this.raycastDown_Hat;
      }
      else
      {
        this.transformTarget = this.raycastHit.point;
        this.elevation = Vector3.Dot(this.transform.position - this.raycastHit.point, -this.raycastDown_Hat);
        this.nRaw_Hat = this.raycastHit.normal;
      }
      this.grounded = (double) this.elevation < 0.05000000074505806;
      this.iAmStandingOn = this.raycastHit.transform;
    }

    private bool WeHaveAllTheStuff()
    {
      if (!Physics.Raycast(this.transform.position + this.transform.up, this.raycastDown_Hat, out this.raycastHit, 500f, (int) this.layerMask))
      {
        Debug.LogWarning((object) ("Bad spawn of " + this.name + "\nSpawn character closer to ground or platform.\n"));
        return false;
      }
      this.iAmStandingOn = this.raycastHit.transform;
      if (this.transform.root.GetComponentsInChildren<FootIKBaseClass_PBC>().Length > 1)
      {
        Debug.Log((object) ("To many FootIK scripts on " + this.name + ".\nRemove the NoFootIK script.\n"));
        return false;
      }
      foreach (Collider componentsInChild in this.transform.root.GetComponentsInChildren<Collider>())
      {
        bool flag = false;
        foreach (string ignoreLayer in this.ignoreLayers)
        {
          if (componentsInChild.gameObject.layer.Equals(LayerMask.NameToLayer(ignoreLayer)))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          Debug.LogWarning((object) ("Layer for all colliders on " + componentsInChild.name + " must be set to an ignored layer\n"));
      }
      return true;
    }

    private void SetIgnoreLayers()
    {
      foreach (string ignoreLayer in this.ignoreLayers)
        this.layerMask = (LayerMask) ((int) this.layerMask | 1 << LayerMask.NameToLayer(ignoreLayer));
      this.layerMask = (LayerMask) ~(int) this.layerMask;
    }
  }
}
