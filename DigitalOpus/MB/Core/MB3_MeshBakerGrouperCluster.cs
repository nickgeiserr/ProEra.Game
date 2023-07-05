// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_MeshBakerGrouperCluster
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class MB3_MeshBakerGrouperCluster : MB3_MeshBakerGrouperCore
  {
    public MB3_AgglomerativeClustering cluster;
    private float _lastMaxDistBetweenClusters;
    public float _ObjsExtents = 10f;
    public float _minDistBetweenClusters = 1f / 1000f;
    private List<MB3_AgglomerativeClustering.ClusterNode> _clustersToDraw = new List<MB3_AgglomerativeClustering.ClusterNode>();
    private float[] _radii;

    public MB3_MeshBakerGrouperCluster(GrouperData data, List<GameObject> gos) => this.d = data;

    public override Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection)
    {
      Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
      for (int index1 = 0; index1 < this._clustersToDraw.Count; ++index1)
      {
        MB3_AgglomerativeClustering.ClusterNode clusterNode = this._clustersToDraw[index1];
        List<Renderer> rendererList = new List<Renderer>();
        for (int index2 = 0; index2 < clusterNode.leafs.Length; ++index2)
        {
          Renderer component = this.cluster.clusters[clusterNode.leafs[index2]].leaf.go.GetComponent<Renderer>();
          switch (component)
          {
            case MeshRenderer _:
            case SkinnedMeshRenderer _:
              rendererList.Add(component);
              break;
          }
        }
        dictionary.Add("Cluster_" + index1.ToString(), rendererList);
      }
      return dictionary;
    }

    public void BuildClusters(List<GameObject> gos, ProgressUpdateCancelableDelegate progFunc)
    {
      if (gos.Count == 0)
      {
        Debug.LogWarning((object) "No objects to cluster. Add some objects to the list of Objects To Combine.");
      }
      else
      {
        if (this.cluster == null)
          this.cluster = new MB3_AgglomerativeClustering();
        List<MB3_AgglomerativeClustering.item_s> itemSList = new List<MB3_AgglomerativeClustering.item_s>();
        for (int i = 0; i < gos.Count; i++)
        {
          if ((UnityEngine.Object) gos[i] != (UnityEngine.Object) null && itemSList.Find((Predicate<MB3_AgglomerativeClustering.item_s>) (x => (UnityEngine.Object) x.go == (UnityEngine.Object) gos[i])) == null)
          {
            Renderer component = gos[i].GetComponent<Renderer>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              switch (component)
              {
                case MeshRenderer _:
                case SkinnedMeshRenderer _:
                  itemSList.Add(new MB3_AgglomerativeClustering.item_s()
                  {
                    go = gos[i],
                    coord = component.bounds.center
                  });
                  continue;
                default:
                  continue;
              }
            }
          }
        }
        this.cluster.items = itemSList;
        this.cluster.agglomerate(progFunc);
        if (this.cluster.wasCanceled)
          return;
        float smallest;
        float largest;
        this._BuildListOfClustersToDraw(progFunc, out smallest, out largest);
        this.d.maxDistBetweenClusters = Mathf.Lerp(smallest, largest, 0.9f);
      }
    }

    private void _BuildListOfClustersToDraw(
      ProgressUpdateCancelableDelegate progFunc,
      out float smallest,
      out float largest)
    {
      this._clustersToDraw.Clear();
      if (this.cluster.clusters == null)
      {
        smallest = 1f;
        largest = 10f;
      }
      else
      {
        if (progFunc != null)
        {
          int num1 = progFunc("Building Clusters To Draw A:", 0.0f) ? 1 : 0;
        }
        List<MB3_AgglomerativeClustering.ClusterNode> clusterNodeList = new List<MB3_AgglomerativeClustering.ClusterNode>();
        largest = 1f;
        smallest = 1E+07f;
        for (int index = 0; index < this.cluster.clusters.Length; ++index)
        {
          MB3_AgglomerativeClustering.ClusterNode cluster = this.cluster.clusters[index];
          if ((double) cluster.distToMergedCentroid <= (double) this.d.maxDistBetweenClusters)
          {
            if (this.d.includeCellsWithOnlyOneRenderer)
              this._clustersToDraw.Add(cluster);
            else if (cluster.leaf == null)
              this._clustersToDraw.Add(cluster);
          }
          if ((double) cluster.distToMergedCentroid > (double) largest)
            largest = cluster.distToMergedCentroid;
          if (cluster.height > 0 && (double) cluster.distToMergedCentroid < (double) smallest)
            smallest = cluster.distToMergedCentroid;
        }
        if (progFunc != null)
        {
          int num2 = progFunc("Building Clusters To Draw B:", 0.0f) ? 1 : 0;
        }
        for (int index = 0; index < this._clustersToDraw.Count; ++index)
        {
          clusterNodeList.Add(this._clustersToDraw[index].cha);
          clusterNodeList.Add(this._clustersToDraw[index].chb);
        }
        for (int index = 0; index < clusterNodeList.Count; ++index)
          this._clustersToDraw.Remove(clusterNodeList[index]);
        this._radii = new float[this._clustersToDraw.Count];
        if (progFunc != null)
        {
          int num3 = progFunc("Building Clusters To Draw C:", 0.0f) ? 1 : 0;
        }
        for (int index1 = 0; index1 < this._radii.Length; ++index1)
        {
          MB3_AgglomerativeClustering.ClusterNode clusterNode = this._clustersToDraw[index1];
          Bounds bounds = new Bounds(clusterNode.centroid, Vector3.one);
          for (int index2 = 0; index2 < clusterNode.leafs.Length; ++index2)
          {
            Renderer component = this.cluster.clusters[clusterNode.leafs[index2]].leaf.go.GetComponent<Renderer>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              bounds.Encapsulate(component.bounds);
          }
          this._radii[index1] = bounds.extents.magnitude;
        }
        if (progFunc != null)
        {
          int num4 = progFunc("Building Clusters To Draw D:", 0.0f) ? 1 : 0;
        }
        this._ObjsExtents = largest + 1f;
        this._minDistBetweenClusters = Mathf.Lerp(smallest, 0.0f, 0.9f);
        if ((double) this._ObjsExtents >= 2.0)
          return;
        this._ObjsExtents = 2f;
      }
    }

    public override void DrawGizmos(Bounds sceneObjectBounds)
    {
      if (this.cluster == null || this.cluster.clusters == null)
        return;
      if ((double) this._lastMaxDistBetweenClusters != (double) this.d.maxDistBetweenClusters)
      {
        this._BuildListOfClustersToDraw((ProgressUpdateCancelableDelegate) null, out float _, out float _);
        this._lastMaxDistBetweenClusters = this.d.maxDistBetweenClusters;
      }
      for (int index = 0; index < this._clustersToDraw.Count; ++index)
      {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this._clustersToDraw[index].centroid, this._radii[index]);
      }
    }
  }
}
