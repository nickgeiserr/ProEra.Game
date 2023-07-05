// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.Water.WaterTile
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UnityStandardAssets.Water
{
  [ExecuteInEditMode]
  public class WaterTile : MonoBehaviour
  {
    public PlanarReflection reflection;
    public WaterBase waterBase;

    public void Start() => this.AcquireComponents();

    private void AcquireComponents()
    {
      if (!(bool) (Object) this.reflection)
        this.reflection = !(bool) (Object) this.transform.parent ? this.transform.GetComponent<PlanarReflection>() : this.transform.parent.GetComponent<PlanarReflection>();
      if ((bool) (Object) this.waterBase)
        return;
      if ((bool) (Object) this.transform.parent)
        this.waterBase = this.transform.parent.GetComponent<WaterBase>();
      else
        this.waterBase = this.transform.GetComponent<WaterBase>();
    }

    public void OnWillRenderObject()
    {
      if ((bool) (Object) this.reflection)
        this.reflection.WaterTileBeingRendered(this.transform, Camera.current);
      if (!(bool) (Object) this.waterBase)
        return;
      this.waterBase.WaterTileBeingRendered(this.transform, Camera.current);
    }
  }
}
