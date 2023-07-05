// Decompiled with JetBrains decompiler
// Type: TB12.OpponentData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using Vars;

namespace TB12
{
  [Serializable]
  public class OpponentData
  {
    public VariableInt Score = new VariableInt(0);
    public VariableInt BallsThrown = new VariableInt(0);
    public VariableInt BallsHit = new VariableInt(0);
    public VariableInt TotalAttempts = new VariableInt(0);

    public void ResetData(int totalAttempts = 0)
    {
      this.Score.SetValue(0);
      this.BallsThrown.SetValue(0);
      this.BallsHit.SetValue(0);
      this.TotalAttempts.Value = totalAttempts;
    }
  }
}
