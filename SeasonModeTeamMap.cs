// Decompiled with JetBrains decompiler
// Type: SeasonModeTeamMap
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;

[MessagePackObject(false)]
[Serializable]
public class SeasonModeTeamMap
{
  [Key(0)]
  public int index { get; private set; }

  [Key(1)]
  public string name { get; private set; }

  public SeasonModeTeamMap()
  {
  }

  public SeasonModeTeamMap(string s, int i)
  {
    this.name = s;
    this.index = i;
  }
}
