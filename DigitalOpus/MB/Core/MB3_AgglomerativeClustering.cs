// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_AgglomerativeClustering
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class MB3_AgglomerativeClustering
  {
    public List<MB3_AgglomerativeClustering.item_s> items = new List<MB3_AgglomerativeClustering.item_s>();
    public MB3_AgglomerativeClustering.ClusterNode[] clusters;
    public bool wasCanceled;
    private const int MAX_PRIORITY_Q_SIZE = 2048;

    private float euclidean_distance(Vector3 a, Vector3 b) => Vector3.Distance(a, b);

    public bool agglomerate(ProgressUpdateCancelableDelegate progFunc)
    {
      this.wasCanceled = true;
      if (progFunc != null)
        this.wasCanceled = progFunc("Filling Priority Queue:", 0.0f);
      if (this.items.Count <= 1)
      {
        this.clusters = new MB3_AgglomerativeClustering.ClusterNode[0];
        return false;
      }
      this.clusters = new MB3_AgglomerativeClustering.ClusterNode[this.items.Count * 2 - 1];
      for (int index = 0; index < this.items.Count; ++index)
        this.clusters[index] = new MB3_AgglomerativeClustering.ClusterNode(this.items[index], index);
      int count = this.items.Count;
      List<MB3_AgglomerativeClustering.ClusterNode> unclustered = new List<MB3_AgglomerativeClustering.ClusterNode>();
      for (int index = 0; index < count; ++index)
      {
        this.clusters[index].isUnclustered = true;
        unclustered.Add(this.clusters[index]);
      }
      int h = 0;
      new Stopwatch().Start();
      float num1 = 0.0f;
      long num2 = GC.GetTotalMemory(false) / 1000000L;
      PriorityQueue<float, MB3_AgglomerativeClustering.ClusterDistance> pq = new PriorityQueue<float, MB3_AgglomerativeClustering.ClusterDistance>();
      int num3 = 0;
      while (unclustered.Count > 1)
      {
        int num4 = 0;
        ++h;
        if (pq.Count == 0)
        {
          ++num3;
          long num5 = GC.GetTotalMemory(false) / 1000000L;
          if (progFunc != null)
            this.wasCanceled = progFunc("Refilling Q:" + ((float) (this.items.Count - unclustered.Count) * 100f / (float) this.items.Count).ToString() + " unclustered:" + unclustered.Count.ToString() + " inQ:" + pq.Count.ToString() + " usedMem:" + num5.ToString(), (float) ((double) (this.items.Count - unclustered.Count) / (double) this.items.Count));
          num1 = this._RefillPriorityQWithSome(pq, unclustered, this.clusters, progFunc);
          if (pq.Count == 0)
            break;
        }
        KeyValuePair<float, MB3_AgglomerativeClustering.ClusterDistance> keyValuePair = pq.Dequeue();
        while (!keyValuePair.Value.a.isUnclustered || !keyValuePair.Value.b.isUnclustered)
        {
          if (pq.Count == 0)
          {
            ++num3;
            long num6 = GC.GetTotalMemory(false) / 1000000L;
            if (progFunc != null)
              this.wasCanceled = progFunc("Creating clusters:" + ((float) (this.items.Count - unclustered.Count) * 100f / (float) this.items.Count).ToString() + " unclustered:" + unclustered.Count.ToString() + " inQ:" + pq.Count.ToString() + " usedMem:" + num6.ToString(), (float) ((double) (this.items.Count - unclustered.Count) / (double) this.items.Count));
            num1 = this._RefillPriorityQWithSome(pq, unclustered, this.clusters, progFunc);
            if (pq.Count == 0)
              break;
          }
          keyValuePair = pq.Dequeue();
          ++num4;
        }
        ++count;
        MB3_AgglomerativeClustering.ClusterNode aa = new MB3_AgglomerativeClustering.ClusterNode(keyValuePair.Value.a, keyValuePair.Value.b, count - 1, h, keyValuePair.Key, this.clusters);
        unclustered.Remove(keyValuePair.Value.a);
        unclustered.Remove(keyValuePair.Value.b);
        keyValuePair.Value.a.isUnclustered = false;
        keyValuePair.Value.b.isUnclustered = false;
        int index1 = count - 1;
        if (index1 == this.clusters.Length)
          UnityEngine.Debug.LogError((object) "how did this happen");
        this.clusters[index1] = aa;
        unclustered.Add(aa);
        aa.isUnclustered = true;
        for (int index2 = 0; index2 < unclustered.Count - 1; ++index2)
        {
          float key = this.euclidean_distance(aa.centroid, unclustered[index2].centroid);
          if ((double) key < (double) num1)
            pq.Add(new KeyValuePair<float, MB3_AgglomerativeClustering.ClusterDistance>(key, new MB3_AgglomerativeClustering.ClusterDistance(aa, unclustered[index2])));
        }
        if (!this.wasCanceled)
        {
          long num7 = GC.GetTotalMemory(false) / 1000000L;
          if (progFunc != null)
            this.wasCanceled = progFunc("Creating clusters:" + ((float) (this.items.Count - unclustered.Count) * 100f / (float) this.items.Count).ToString() + " unclustered:" + unclustered.Count.ToString() + " inQ:" + pq.Count.ToString() + " usedMem:" + num7.ToString(), (float) ((double) (this.items.Count - unclustered.Count) / (double) this.items.Count));
        }
        else
          break;
      }
      if (progFunc != null)
        this.wasCanceled = progFunc("Finished clustering:", 100f);
      return !this.wasCanceled;
    }

    private float _RefillPriorityQWithSome(
      PriorityQueue<float, MB3_AgglomerativeClustering.ClusterDistance> pq,
      List<MB3_AgglomerativeClustering.ClusterNode> unclustered,
      MB3_AgglomerativeClustering.ClusterNode[] clusters,
      ProgressUpdateCancelableDelegate progFunc)
    {
      List<float> array = new List<float>(2048);
      for (int index1 = 0; index1 < unclustered.Count; ++index1)
      {
        for (int index2 = index1 + 1; index2 < unclustered.Count; ++index2)
          array.Add(this.euclidean_distance(unclustered[index1].centroid, unclustered[index2].centroid));
        this.wasCanceled = progFunc("Refilling Queue Part A:", (float) index1 / ((float) unclustered.Count * 2f));
        if (this.wasCanceled)
          return 10f;
      }
      if (array.Count == 0)
        return 1E+11f;
      float num = MB3_AgglomerativeClustering.NthSmallestElement<float>(array, 2048);
      for (int index3 = 0; index3 < unclustered.Count; ++index3)
      {
        for (int index4 = index3 + 1; index4 < unclustered.Count; ++index4)
        {
          int idx1 = unclustered[index3].idx;
          int idx2 = unclustered[index4].idx;
          float key = this.euclidean_distance(unclustered[index3].centroid, unclustered[index4].centroid);
          if ((double) key <= (double) num)
            pq.Add(new KeyValuePair<float, MB3_AgglomerativeClustering.ClusterDistance>(key, new MB3_AgglomerativeClustering.ClusterDistance(clusters[idx1], clusters[idx2])));
        }
        this.wasCanceled = progFunc("Refilling Queue Part B:", (float) (unclustered.Count + index3) / ((float) unclustered.Count * 2f));
        if (this.wasCanceled)
          return 10f;
      }
      return num;
    }

    public int TestRun(List<GameObject> gos)
    {
      List<MB3_AgglomerativeClustering.item_s> itemSList = new List<MB3_AgglomerativeClustering.item_s>();
      for (int index = 0; index < gos.Count; ++index)
        itemSList.Add(new MB3_AgglomerativeClustering.item_s()
        {
          go = gos[index],
          coord = gos[index].transform.position
        });
      this.items = itemSList;
      if (this.items.Count > 0)
        this.agglomerate((ProgressUpdateCancelableDelegate) null);
      return 0;
    }

    public static void Main()
    {
      List<float> array = new List<float>();
      array.AddRange((IEnumerable<float>) new float[10]
      {
        19f,
        18f,
        17f,
        16f,
        15f,
        10f,
        11f,
        12f,
        13f,
        14f
      });
      UnityEngine.Debug.Log((object) "Loop quick select 10 times.");
      UnityEngine.Debug.Log((object) MB3_AgglomerativeClustering.NthSmallestElement<float>(array, 0));
    }

    public static T NthSmallestElement<T>(List<T> array, int n) where T : IComparable<T>
    {
      if (n < 0)
        n = 0;
      if (n > array.Count - 1)
        n = array.Count - 1;
      if (array.Count == 0)
        throw new ArgumentException("Array is empty.", nameof (array));
      return array.Count == 1 ? array[0] : MB3_AgglomerativeClustering.QuickSelectSmallest<T>(array, n)[n];
    }

    private static List<T> QuickSelectSmallest<T>(List<T> input, int n) where T : IComparable<T>
    {
      List<T> array = input;
      int num1 = 0;
      int num2 = input.Count - 1;
      int pivotIndex = n;
      System.Random random = new System.Random();
      while (num2 > num1)
      {
        int num3 = MB3_AgglomerativeClustering.QuickSelectPartition<T>(array, num1, num2, pivotIndex);
        if (num3 != n)
        {
          if (num3 > n)
            num2 = num3 - 1;
          else
            num1 = num3 + 1;
          pivotIndex = random.Next(num1, num2);
        }
        else
          break;
      }
      return array;
    }

    private static int QuickSelectPartition<T>(
      List<T> array,
      int startIndex,
      int endIndex,
      int pivotIndex)
      where T : IComparable<T>
    {
      T other = array[pivotIndex];
      MB3_AgglomerativeClustering.Swap<T>(array, pivotIndex, endIndex);
      for (int index = startIndex; index < endIndex; ++index)
      {
        if (array[index].CompareTo(other) <= 0)
        {
          MB3_AgglomerativeClustering.Swap<T>(array, index, startIndex);
          ++startIndex;
        }
      }
      MB3_AgglomerativeClustering.Swap<T>(array, endIndex, startIndex);
      return startIndex;
    }

    private static void Swap<T>(List<T> array, int index1, int index2)
    {
      if (index1 == index2)
        return;
      T obj = array[index1];
      array[index1] = array[index2];
      array[index2] = obj;
    }

    [Serializable]
    public class ClusterNode
    {
      public MB3_AgglomerativeClustering.item_s leaf;
      public MB3_AgglomerativeClustering.ClusterNode cha;
      public MB3_AgglomerativeClustering.ClusterNode chb;
      public int height;
      public float distToMergedCentroid;
      public Vector3 centroid;
      public int[] leafs;
      public int idx;
      public bool isUnclustered = true;

      public ClusterNode(MB3_AgglomerativeClustering.item_s ii, int index)
      {
        this.leaf = ii;
        this.idx = index;
        this.leafs = new int[1];
        this.leafs[0] = index;
        this.centroid = ii.coord;
        this.height = 0;
      }

      public ClusterNode(
        MB3_AgglomerativeClustering.ClusterNode a,
        MB3_AgglomerativeClustering.ClusterNode b,
        int index,
        int h,
        float dist,
        MB3_AgglomerativeClustering.ClusterNode[] clusters)
      {
        this.cha = a;
        this.chb = b;
        this.idx = index;
        this.leafs = new int[a.leafs.Length + b.leafs.Length];
        Array.Copy((Array) a.leafs, (Array) this.leafs, a.leafs.Length);
        Array.Copy((Array) b.leafs, 0, (Array) this.leafs, a.leafs.Length, b.leafs.Length);
        Vector3 zero = Vector3.zero;
        for (int index1 = 0; index1 < this.leafs.Length; ++index1)
          zero += clusters[this.leafs[index1]].centroid;
        this.centroid = zero / (float) this.leafs.Length;
        this.height = h;
        this.distToMergedCentroid = dist;
      }
    }

    [Serializable]
    public class item_s
    {
      public GameObject go;
      public Vector3 coord;
    }

    public class ClusterDistance
    {
      public MB3_AgglomerativeClustering.ClusterNode a;
      public MB3_AgglomerativeClustering.ClusterNode b;

      public ClusterDistance(
        MB3_AgglomerativeClustering.ClusterNode aa,
        MB3_AgglomerativeClustering.ClusterNode bb)
      {
        this.a = aa;
        this.b = bb;
      }
    }
  }
}
