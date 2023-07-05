// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_MeshBakerGrouperKMeans
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class MB3_MeshBakerGrouperKMeans : MB3_MeshBakerGrouperCore
  {
    public int numClusters = 4;
    public Vector3[] clusterCenters = new Vector3[0];
    public float[] clusterSizes = new float[0];

    public MB3_MeshBakerGrouperKMeans(GrouperData data) => this.d = data;

    public override Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection)
    {
      Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
      List<GameObject> gos = new List<GameObject>();
      int numClusters = 20;
      foreach (GameObject gameObject1 in selection)
      {
        if (!((UnityEngine.Object) gameObject1 == (UnityEngine.Object) null))
        {
          GameObject gameObject2 = gameObject1;
          Renderer component = gameObject2.GetComponent<Renderer>();
          if (component is MeshRenderer || component is SkinnedMeshRenderer)
            gos.Add(gameObject2);
        }
      }
      if (gos.Count > 0 && numClusters > 0 && numClusters < gos.Count)
      {
        MB3_KMeansClustering kmeansClustering = new MB3_KMeansClustering(gos, numClusters);
        kmeansClustering.Cluster();
        this.clusterCenters = new Vector3[numClusters];
        this.clusterSizes = new float[numClusters];
        for (int idx = 0; idx < numClusters; ++idx)
        {
          List<Renderer> cluster = kmeansClustering.GetCluster(idx, out this.clusterCenters[idx], out this.clusterSizes[idx]);
          if (cluster.Count > 0)
            dictionary.Add("Cluster_" + idx.ToString(), cluster);
        }
      }
      return dictionary;
    }

    public override void DrawGizmos(Bounds sceneObjectBounds)
    {
      if (this.clusterCenters == null || this.clusterSizes == null || this.clusterCenters.Length != this.clusterSizes.Length)
        return;
      for (int index = 0; index < this.clusterSizes.Length; ++index)
        Gizmos.DrawWireSphere(this.clusterCenters[index], this.clusterSizes[index]);
    }
  }
}
