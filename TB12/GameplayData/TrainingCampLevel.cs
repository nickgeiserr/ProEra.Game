// Decompiled with JetBrains decompiler
// Type: TB12.GameplayData.TrainingCampLevel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace TB12.GameplayData
{
  [Serializable]
  public class TrainingCampLevel
  {
    public string id;
    public GameObject levelPrefab;
    public int time = 75;
    public int targetsToWin = 5;

    public virtual bool DidPassTraining(GameplayStore store) => (int) store.BallsHitTarget >= this.targetsToWin;
  }
}
