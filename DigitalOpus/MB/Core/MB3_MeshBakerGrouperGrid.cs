// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_MeshBakerGrouperGrid
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class MB3_MeshBakerGrouperGrid : MB3_MeshBakerGrouperCore
  {
    public MB3_MeshBakerGrouperGrid(GrouperData d) => this.d = d;

    public override Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection)
    {
      Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
      if ((double) this.d.cellSize.x <= 0.0 || (double) this.d.cellSize.y <= 0.0 || (double) this.d.cellSize.z <= 0.0)
      {
        Debug.LogError((object) "cellSize x,y,z must all be greater than zero.");
        return dictionary;
      }
      Debug.Log((object) "Collecting renderers in each cell");
      foreach (GameObject gameObject in selection)
      {
        if (!((UnityEngine.Object) gameObject == (UnityEngine.Object) null))
        {
          Renderer component = gameObject.GetComponent<Renderer>();
          if (component is MeshRenderer || component is SkinnedMeshRenderer)
          {
            Vector3 center = component.bounds.center;
            center.x = Mathf.Floor((center.x - this.d.origin.x) / this.d.cellSize.x) * this.d.cellSize.x;
            center.y = Mathf.Floor((center.y - this.d.origin.y) / this.d.cellSize.y) * this.d.cellSize.y;
            center.z = Mathf.Floor((center.z - this.d.origin.z) / this.d.cellSize.z) * this.d.cellSize.z;
            string key = center.ToString();
            List<Renderer> rendererList;
            if (dictionary.ContainsKey(key))
            {
              rendererList = dictionary[key];
            }
            else
            {
              rendererList = new List<Renderer>();
              dictionary.Add(key, rendererList);
            }
            if (!rendererList.Contains(component))
              rendererList.Add(component);
          }
        }
      }
      return dictionary;
    }

    public override void DrawGizmos(Bounds sourceObjectBounds)
    {
      Vector3 cellSize = this.d.cellSize;
      if ((double) cellSize.x <= 9.9999997473787516E-06 || (double) cellSize.y <= 9.9999997473787516E-06 || (double) cellSize.z <= 9.9999997473787516E-06)
        return;
      Vector3 vector3_1 = sourceObjectBounds.center - sourceObjectBounds.extents;
      Vector3 origin = this.d.origin;
      origin.x %= cellSize.x;
      origin.y %= cellSize.y;
      origin.z %= cellSize.z;
      vector3_1.x = Mathf.Round(vector3_1.x / cellSize.x) * cellSize.x + origin.x;
      vector3_1.y = Mathf.Round(vector3_1.y / cellSize.y) * cellSize.y + origin.y;
      vector3_1.z = Mathf.Round(vector3_1.z / cellSize.z) * cellSize.z + origin.z;
      if ((double) vector3_1.x > (double) sourceObjectBounds.center.x - (double) sourceObjectBounds.extents.x)
        vector3_1.x -= cellSize.x;
      if ((double) vector3_1.y > (double) sourceObjectBounds.center.y - (double) sourceObjectBounds.extents.y)
        vector3_1.y -= cellSize.y;
      if ((double) vector3_1.z > (double) sourceObjectBounds.center.z - (double) sourceObjectBounds.extents.z)
        vector3_1.z -= cellSize.z;
      Vector3 vector3_2 = vector3_1;
      if (Mathf.CeilToInt((float) ((double) sourceObjectBounds.size.x / (double) cellSize.x + (double) sourceObjectBounds.size.y / (double) cellSize.y + (double) sourceObjectBounds.size.z / (double) cellSize.z)) > 200)
      {
        Gizmos.DrawWireCube(this.d.origin + cellSize / 2f, cellSize);
      }
      else
      {
        for (; (double) vector3_1.x < (double) sourceObjectBounds.center.x + (double) sourceObjectBounds.extents.x; vector3_1.x += cellSize.x)
        {
          for (vector3_1.y = vector3_2.y; (double) vector3_1.y < (double) sourceObjectBounds.center.y + (double) sourceObjectBounds.extents.y; vector3_1.y += cellSize.y)
          {
            for (vector3_1.z = vector3_2.z; (double) vector3_1.z < (double) sourceObjectBounds.center.z + (double) sourceObjectBounds.extents.z; vector3_1.z += cellSize.z)
              Gizmos.DrawWireCube(vector3_1 + cellSize / 2f, cellSize);
          }
        }
      }
    }
  }
}
