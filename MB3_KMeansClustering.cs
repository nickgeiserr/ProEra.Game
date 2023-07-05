// Decompiled with JetBrains decompiler
// Type: MB3_KMeansClustering
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class MB3_KMeansClustering
{
  private List<MB3_KMeansClustering.DataPoint> _normalizedDataToCluster = new List<MB3_KMeansClustering.DataPoint>();
  private Vector3[] _clusters = new Vector3[0];
  private int _numberOfClusters;

  public MB3_KMeansClustering(List<GameObject> gos, int numClusters)
  {
    for (int index = 0; index < gos.Count; ++index)
    {
      if ((Object) gos[index] != (Object) null)
        this._normalizedDataToCluster.Add(new MB3_KMeansClustering.DataPoint(gos[index]));
      else
        Debug.LogWarning((object) string.Format("Object {0} in list of objects to cluster was null.", (object) index));
    }
    if (numClusters <= 0)
    {
      Debug.LogError((object) "Number of clusters must be posititve.");
      numClusters = 1;
    }
    if (this._normalizedDataToCluster.Count <= numClusters)
    {
      Debug.LogError((object) "There must be fewer clusters than objects to cluster");
      numClusters = this._normalizedDataToCluster.Count - 1;
    }
    this._numberOfClusters = numClusters;
    if (this._numberOfClusters <= 0)
      this._numberOfClusters = 1;
    this._clusters = new Vector3[this._numberOfClusters];
  }

  private void InitializeCentroids()
  {
    for (int index = 0; index < this._numberOfClusters; ++index)
      this._normalizedDataToCluster[index].Cluster = index;
    for (int numberOfClusters = this._numberOfClusters; numberOfClusters < this._normalizedDataToCluster.Count; ++numberOfClusters)
      this._normalizedDataToCluster[numberOfClusters].Cluster = Random.Range(0, this._numberOfClusters);
  }

  private bool UpdateDataPointMeans(bool force)
  {
    if (this.AnyAreEmpty(this._normalizedDataToCluster) && !force)
      return false;
    Vector3[] vector3Array = new Vector3[this._numberOfClusters];
    int[] numArray = new int[this._numberOfClusters];
    for (int index = 0; index < this._normalizedDataToCluster.Count; ++index)
    {
      int cluster = this._normalizedDataToCluster[index].Cluster;
      vector3Array[cluster] += this._normalizedDataToCluster[index].center;
      ++numArray[cluster];
    }
    for (int index = 0; index < this._numberOfClusters; ++index)
      this._clusters[index] = vector3Array[index] / (float) numArray[index];
    return true;
  }

  private bool AnyAreEmpty(List<MB3_KMeansClustering.DataPoint> data)
  {
    int[] numArray = new int[this._numberOfClusters];
    for (int index = 0; index < this._normalizedDataToCluster.Count; ++index)
      ++numArray[this._normalizedDataToCluster[index].Cluster];
    for (int index = 0; index < numArray.Length; ++index)
    {
      if (numArray[index] == 0)
        return true;
    }
    return false;
  }

  private bool UpdateClusterMembership()
  {
    bool flag = false;
    float[] distances = new float[this._numberOfClusters];
    for (int index1 = 0; index1 < this._normalizedDataToCluster.Count; ++index1)
    {
      for (int index2 = 0; index2 < this._numberOfClusters; ++index2)
        distances[index2] = this.ElucidanDistance(this._normalizedDataToCluster[index1], this._clusters[index2]);
      int num = this.MinIndex(distances);
      if (num != this._normalizedDataToCluster[index1].Cluster)
      {
        flag = true;
        this._normalizedDataToCluster[index1].Cluster = num;
      }
    }
    return flag;
  }

  private float ElucidanDistance(MB3_KMeansClustering.DataPoint dataPoint, Vector3 mean) => Vector3.Distance(dataPoint.center, mean);

  private int MinIndex(float[] distances)
  {
    int num = 0;
    double distance = (double) distances[0];
    for (int index = 0; index < distances.Length; ++index)
    {
      if ((double) distances[index] < distance)
      {
        distance = (double) distances[index];
        num = index;
      }
    }
    return num;
  }

  public List<Renderer> GetCluster(int idx, out Vector3 mean, out float size)
  {
    if (idx < 0 || idx >= this._numberOfClusters)
    {
      Debug.LogError((object) "idx is out of bounds");
      mean = Vector3.zero;
      size = 1f;
      return new List<Renderer>();
    }
    this.UpdateDataPointMeans(true);
    List<Renderer> cluster = new List<Renderer>();
    mean = this._clusters[idx];
    float num1 = 0.0f;
    for (int index = 0; index < this._normalizedDataToCluster.Count; ++index)
    {
      if (this._normalizedDataToCluster[index].Cluster == idx)
      {
        float num2 = Vector3.Distance(mean, this._normalizedDataToCluster[index].center);
        if ((double) num2 > (double) num1)
          num1 = num2;
        cluster.Add(this._normalizedDataToCluster[index].gameObject.GetComponent<Renderer>());
      }
    }
    mean = this._clusters[idx];
    size = num1;
    return cluster;
  }

  public void Cluster()
  {
    bool flag1 = true;
    bool flag2 = true;
    this.InitializeCentroids();
    int num = this._normalizedDataToCluster.Count * 1000;
    for (int index = 0; flag2 && flag1 && index < num; flag1 = this.UpdateClusterMembership())
    {
      ++index;
      flag2 = this.UpdateDataPointMeans(false);
    }
  }

  private class DataPoint
  {
    public Vector3 center;
    public GameObject gameObject;
    public int Cluster;

    public DataPoint(GameObject go)
    {
      this.gameObject = go;
      this.center = go.transform.position;
      if (!((Object) go.GetComponent<Renderer>() == (Object) null))
        return;
      Debug.LogError((object) ("Object does not have a renderer " + ((object) go)?.ToString()));
    }
  }
}
