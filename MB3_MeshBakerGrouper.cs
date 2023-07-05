// Decompiled with JetBrains decompiler
// Type: MB3_MeshBakerGrouper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System.Collections.Generic;
using UnityEngine;

public class MB3_MeshBakerGrouper : MonoBehaviour
{
  public MB3_MeshBakerGrouperCore grouper;
  public MB3_MeshBakerGrouper.ClusterType clusterType;
  public GrouperData data = new GrouperData();
  [HideInInspector]
  public Bounds sourceObjectBounds = new Bounds(Vector3.zero, Vector3.one);

  private void OnDrawGizmosSelected()
  {
    if (this.grouper == null)
      this.grouper = this.CreateGrouper(this.clusterType, this.data);
    if (this.grouper.d == null)
      this.grouper.d = this.data;
    this.grouper.DrawGizmos(this.sourceObjectBounds);
  }

  public MB3_MeshBakerGrouperCore CreateGrouper(
    MB3_MeshBakerGrouper.ClusterType t,
    GrouperData data)
  {
    if (t == MB3_MeshBakerGrouper.ClusterType.grid)
      this.grouper = (MB3_MeshBakerGrouperCore) new MB3_MeshBakerGrouperGrid(data);
    if (t == MB3_MeshBakerGrouper.ClusterType.pie)
      this.grouper = (MB3_MeshBakerGrouperCore) new MB3_MeshBakerGrouperPie(data);
    if (t == MB3_MeshBakerGrouper.ClusterType.agglomerative)
    {
      MB3_TextureBaker component = this.GetComponent<MB3_TextureBaker>();
      List<GameObject> gos = !((Object) component != (Object) null) ? new List<GameObject>() : component.GetObjectsToCombine();
      this.grouper = (MB3_MeshBakerGrouperCore) new MB3_MeshBakerGrouperCluster(data, gos);
    }
    if (t == MB3_MeshBakerGrouper.ClusterType.none)
      this.grouper = (MB3_MeshBakerGrouperCore) new MB3_MeshBakerGrouperNone(data);
    return this.grouper;
  }

  public enum ClusterType
  {
    none,
    grid,
    pie,
    agglomerative,
  }
}
