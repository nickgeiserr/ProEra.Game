// Decompiled with JetBrains decompiler
// Type: TB12.QbData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using System;
using UnityEngine;

namespace TB12
{
  [Serializable]
  public class QbData
  {
    public string name;
    public string lastName;
    public Color skinColor = Color.white;
    public Color trailColor = Color.yellow;
    public float height = 1.8f;
    public int number;
    public ETeamUniformId team;
  }
}
