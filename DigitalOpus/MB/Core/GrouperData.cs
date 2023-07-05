// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.GrouperData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class GrouperData
  {
    public bool clusterOnLMIndex;
    public bool clusterByLODLevel;
    public Vector3 origin;
    public Vector3 cellSize;
    public int pieNumSegments = 4;
    public Vector3 pieAxis = Vector3.up;
    public int height = 1;
    public float maxDistBetweenClusters = 1f;
    public bool includeCellsWithOnlyOneRenderer = true;
  }
}
