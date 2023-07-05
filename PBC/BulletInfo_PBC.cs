// Decompiled with JetBrains decompiler
// Type: PBC.BulletInfo_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class BulletInfo_PBC
  {
    public Vector3 impulse;
    public RaycastHit raycastHit;
    public Vector3 localHit;

    public BulletInfo_PBC(
      Vector3 bulletImpulse,
      RaycastHit bulletRaycastHit,
      Vector3 bulletLocalHit)
    {
      this.impulse = bulletImpulse;
      this.raycastHit = bulletRaycastHit;
      this.localHit = bulletLocalHit;
    }
  }
}
