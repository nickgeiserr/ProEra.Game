// Decompiled with JetBrains decompiler
// Type: FootballVR.InteractionSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using Vars;

namespace FootballVR
{
  [Serializable]
  public class InteractionSettings
  {
    public VariableFloat TriggerPressThreshold = new VariableFloat(0.5f);
    public float CatchTestDuration = 0.1f;
    public float CatchPositionDelay = 1f;
    public float InteractionRange = 0.2f;
    public float VibrationDuration = 0.2f;
    public bool VibrationEnabled = true;
    public VariableBool ShowDebug = new VariableBool();
  }
}
