// Decompiled with JetBrains decompiler
// Type: StatValue
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

[Serializable]
public struct StatValue
{
  public string Value;

  public static implicit operator StatValue(string rhs) => new StatValue()
  {
    Value = rhs
  };

  public static implicit operator string(StatValue rhs) => rhs.Value;
}
