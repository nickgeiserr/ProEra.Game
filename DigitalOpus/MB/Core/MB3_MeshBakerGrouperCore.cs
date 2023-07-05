// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_MeshBakerGrouperCore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public abstract class MB3_MeshBakerGrouperCore
  {
    public GrouperData d;

    public abstract Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection);

    public abstract void DrawGizmos(Bounds sourceObjectBounds);

    public void DoClustering(MB3_TextureBaker tb, MB3_MeshBakerGrouper grouper)
    {
      Dictionary<string, List<Renderer>> dictionary1 = this.FilterIntoGroups(tb.GetObjectsToCombine());
      if (this.d.clusterOnLMIndex)
      {
        Dictionary<string, List<Renderer>> dictionary2 = new Dictionary<string, List<Renderer>>();
        foreach (string key1 in dictionary1.Keys)
        {
          Dictionary<int, List<Renderer>> dictionary3 = this.GroupByLightmapIndex(dictionary1[key1]);
          foreach (int key2 in dictionary3.Keys)
          {
            string key3 = key1 + "-LM-" + key2.ToString();
            dictionary2.Add(key3, dictionary3[key2]);
          }
        }
        dictionary1 = dictionary2;
      }
      if (this.d.clusterByLODLevel)
      {
        Dictionary<string, List<Renderer>> dictionary4 = new Dictionary<string, List<Renderer>>();
        foreach (string key4 in dictionary1.Keys)
        {
          foreach (Renderer renderer in dictionary1[key4])
          {
            Renderer r = renderer;
            if (!((UnityEngine.Object) r == (UnityEngine.Object) null))
            {
              bool flag = false;
              LODGroup componentInParent = r.GetComponentInParent<LODGroup>();
              if ((UnityEngine.Object) componentInParent != (UnityEngine.Object) null)
              {
                LOD[] loDs = componentInParent.GetLODs();
                for (int index = 0; index < loDs.Length; ++index)
                {
                  if ((UnityEngine.Object) Array.Find<Renderer>(loDs[index].renderers, (Predicate<Renderer>) (x => (UnityEngine.Object) x == (UnityEngine.Object) r)) != (UnityEngine.Object) null)
                  {
                    flag = true;
                    string key5 = string.Format("{0}_LOD{1}", (object) key4, (object) index);
                    List<Renderer> rendererList;
                    if (!dictionary4.TryGetValue(key5, out rendererList))
                    {
                      rendererList = new List<Renderer>();
                      dictionary4.Add(key5, rendererList);
                    }
                    if (!rendererList.Contains(r))
                      rendererList.Add(r);
                  }
                }
              }
              if (!flag)
              {
                string key6 = string.Format("{0}_LOD0", (object) key4);
                List<Renderer> rendererList;
                if (!dictionary4.TryGetValue(key6, out rendererList))
                {
                  rendererList = new List<Renderer>();
                  dictionary4.Add(key6, rendererList);
                }
                if (!rendererList.Contains(r))
                  rendererList.Add(r);
              }
            }
          }
        }
        dictionary1 = dictionary4;
      }
      int num = 0;
      foreach (string key in dictionary1.Keys)
      {
        List<Renderer> gaws = dictionary1[key];
        if (gaws.Count > 1 || grouper.data.includeCellsWithOnlyOneRenderer)
          this.AddMeshBaker(tb, key, gaws);
        else
          ++num;
      }
      Debug.Log((object) string.Format("Found {0} cells with Renderers. Not creating bakers for {1} because there is only one mesh in the cell. Creating {2} bakers.", (object) dictionary1.Count, (object) num, (object) (dictionary1.Count - num)));
    }

    private Dictionary<int, List<Renderer>> GroupByLightmapIndex(List<Renderer> gaws)
    {
      Dictionary<int, List<Renderer>> dictionary = new Dictionary<int, List<Renderer>>();
      for (int index = 0; index < gaws.Count; ++index)
      {
        List<Renderer> rendererList;
        if (dictionary.ContainsKey(gaws[index].lightmapIndex))
        {
          rendererList = dictionary[gaws[index].lightmapIndex];
        }
        else
        {
          rendererList = new List<Renderer>();
          dictionary.Add(gaws[index].lightmapIndex, rendererList);
        }
        rendererList.Add(gaws[index]);
      }
      return dictionary;
    }

    private void AddMeshBaker(MB3_TextureBaker tb, string key, List<Renderer> gaws)
    {
      int num = 0;
      for (int index = 0; index < gaws.Count; ++index)
      {
        UnityEngine.Mesh mesh = MB_Utility.GetMesh(gaws[index].gameObject);
        if ((UnityEngine.Object) mesh != (UnityEngine.Object) null)
          num += mesh.vertexCount;
      }
      GameObject gameObject = new GameObject("MeshBaker-" + key);
      gameObject.transform.position = Vector3.zero;
      MB3_MeshBakerCommon mb3MeshBakerCommon;
      if (num >= (int) ushort.MaxValue)
      {
        mb3MeshBakerCommon = (MB3_MeshBakerCommon) gameObject.AddComponent<MB3_MultiMeshBaker>();
        mb3MeshBakerCommon.useObjsToMeshFromTexBaker = false;
      }
      else
      {
        mb3MeshBakerCommon = (MB3_MeshBakerCommon) gameObject.AddComponent<MB3_MeshBaker>();
        mb3MeshBakerCommon.useObjsToMeshFromTexBaker = false;
      }
      mb3MeshBakerCommon.textureBakeResults = tb.textureBakeResults;
      mb3MeshBakerCommon.transform.parent = tb.transform;
      for (int index = 0; index < gaws.Count; ++index)
        mb3MeshBakerCommon.GetObjectsToCombine().Add(gaws[index].gameObject);
    }
  }
}
