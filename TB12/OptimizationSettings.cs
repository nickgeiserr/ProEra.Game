// Decompiled with JetBrains decompiler
// Type: TB12.OptimizationSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using Vars;

namespace TB12
{
  [Serializable]
  public class OptimizationSettings
  {
    public VariableInt PredictedFrameCount = new VariableInt(20);
    public VariableBool AsyncTrajectoryGeneration = new VariableBool(true);
    public VariableBool UIVisible = new VariableBool(true);
  }
}
