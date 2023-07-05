// Decompiled with JetBrains decompiler
// Type: FootballVR.CollidersSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using Vars;

namespace FootballVR
{
  [Serializable]
  public class CollidersSettings
  {
    public VariableFloat PlayerColliderSize = new VariableFloat(0.3f);
    public VariableFloat EnemyColliderSizeX = new VariableFloat(0.4f);
    public VariableFloat EnemyColliderSizeZ = new VariableFloat(1.5f);
    public VariableFloat EnemyColliderOffsetZ = new VariableFloat(0.3f);
  }
}
