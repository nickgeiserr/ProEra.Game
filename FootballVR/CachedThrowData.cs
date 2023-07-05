// Decompiled with JetBrains decompiler
// Type: FootballVR.CachedThrowData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  [Serializable]
  public class CachedThrowData
  {
    public bool hasTarget;
    public Vector3 autoAimVector;
    public List<Vector3> accelerations;
    public List<Vector3> velocities;
    public List<Vector3> positions;
  }
}
