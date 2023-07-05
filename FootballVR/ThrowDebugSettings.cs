// Decompiled with JetBrains decompiler
// Type: FootballVR.ThrowDebugSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using Vars;

namespace FootballVR
{
  [Serializable]
  public class ThrowDebugSettings
  {
    public VariableBool ShowThrowDebug = new VariableBool(false);
    public int CompareThrowVersionRed = -1;
    public int CompareThrowVersionBlue = -1;
  }
}
