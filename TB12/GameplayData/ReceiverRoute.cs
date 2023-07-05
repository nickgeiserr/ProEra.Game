// Decompiled with JetBrains decompiler
// Type: TB12.GameplayData.ReceiverRoute
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.GameplayData
{
  [Serializable]
  public class ReceiverRoute
  {
    public string name;
    public Vector2 startPoint;
    public List<Vector2> points = new List<Vector2>()
    {
      Vector2.zero
    };
    public List<bool> receiverOpen = new List<bool>();
  }
}
