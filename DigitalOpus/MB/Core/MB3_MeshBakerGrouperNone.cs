// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB3_MeshBakerGrouperNone
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class MB3_MeshBakerGrouperNone : MB3_MeshBakerGrouperCore
  {
    public MB3_MeshBakerGrouperNone(GrouperData d) => this.d = d;

    public override Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection)
    {
      Debug.Log((object) "Filtering into groups none");
      Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
      List<Renderer> rendererList = new List<Renderer>();
      for (int index = 0; index < selection.Count; ++index)
      {
        if ((UnityEngine.Object) selection[index] != (UnityEngine.Object) null)
          rendererList.Add(selection[index].GetComponent<Renderer>());
      }
      dictionary.Add("MeshBaker", rendererList);
      return dictionary;
    }

    public override void DrawGizmos(Bounds sourceObjectBounds)
    {
    }
  }
}
