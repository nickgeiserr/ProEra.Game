// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_MeshBakerGrouperPie
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class MB3_MeshBakerGrouperPie : MB3_MeshBakerGrouperCore
  {
    public MB3_MeshBakerGrouperPie(GrouperData data) => this.d = data;

    public override Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection)
    {
      Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
      if (this.d.pieNumSegments == 0)
      {
        Debug.LogError((object) "pieNumSegments must be greater than zero.");
        return dictionary;
      }
      if ((double) this.d.pieAxis.magnitude <= 9.9999999747524271E-07)
      {
        Debug.LogError((object) "Pie axis must have length greater than zero.");
        return dictionary;
      }
      this.d.pieAxis.Normalize();
      Quaternion rotation = Quaternion.FromToRotation(this.d.pieAxis, Vector3.up);
      Debug.Log((object) "Collecting renderers in each cell");
      foreach (GameObject gameObject in selection)
      {
        if (!((UnityEngine.Object) gameObject == (UnityEngine.Object) null))
        {
          Renderer component = gameObject.GetComponent<Renderer>();
          if (component is MeshRenderer || component is SkinnedMeshRenderer)
          {
            Vector3 vector3 = component.bounds.center - this.d.origin;
            vector3.Normalize();
            vector3 = rotation * vector3;
            float num;
            if ((double) Mathf.Abs(vector3.x) < 9.9999997473787516E-05 && (double) Mathf.Abs(vector3.z) < 9.9999997473787516E-05)
            {
              num = 0.0f;
            }
            else
            {
              num = Mathf.Atan2(vector3.x, vector3.z) * 57.29578f;
              if ((double) num < 0.0)
                num = 360f + num;
            }
            string key = "seg_" + Mathf.FloorToInt(num / 360f * (float) this.d.pieNumSegments).ToString();
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
      if ((double) this.d.pieAxis.magnitude < 0.10000000149011612 || this.d.pieNumSegments < 1)
        return;
      float magnitude = sourceObjectBounds.extents.magnitude;
      MB3_MeshBakerGrouperPie.DrawCircle(this.d.pieAxis, this.d.origin, magnitude, 24);
      Quaternion rotation = Quaternion.FromToRotation(Vector3.up, this.d.pieAxis);
      Quaternion quaternion = Quaternion.AngleAxis(180f / (float) this.d.pieNumSegments, Vector3.up);
      Vector3 vector3_1 = Vector3.forward;
      for (int index = 0; index < this.d.pieNumSegments; ++index)
      {
        Gizmos.DrawLine(this.d.origin, this.d.origin + rotation * vector3_1 * magnitude);
        Vector3 vector3_2 = quaternion * vector3_1;
        vector3_1 = quaternion * vector3_2;
      }
    }

    public static void DrawCircle(Vector3 axis, Vector3 center, float radius, int subdiv)
    {
      Quaternion quaternion = Quaternion.AngleAxis((float) (360 / subdiv), axis);
      Vector3 vector3_1 = new Vector3(axis.y, -axis.x, axis.z);
      vector3_1.Normalize();
      Vector3 vector3_2 = vector3_1 * radius;
      for (int index = 0; index < subdiv + 1; ++index)
      {
        Vector3 vector3_3 = quaternion * vector3_2;
        Gizmos.DrawLine(center + vector3_2, center + vector3_3);
        vector3_2 = vector3_3;
      }
    }
  }
}
