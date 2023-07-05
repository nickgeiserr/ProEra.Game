// Decompiled with JetBrains decompiler
// Type: MiniCampData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using TB12;

[MessagePackObject(false)]
[Serializable]
public class MiniCampData
{
  [Key(0)]
  public EMiniCampTourType MiniCampTourType;
  [Key(1)]
  public EGameMode GameplayMode;
  [Key(2)]
  public string MiniCampTourName = string.Empty;
  [Key(3)]
  public int CurrentLevel = 1;
  [Key(4)]
  public MiniCampEntry[] MiniCampEntries;

  public MiniCampEntry GetEntryByLevel(int level) => ((IEnumerable<MiniCampEntry>) this.MiniCampEntries).FirstOrDefault<MiniCampEntry>((Func<MiniCampEntry, bool>) (miniCampEntry => miniCampEntry.Level == level));

  public void SetEntry(MiniCampEntry entry)
  {
    for (int index = 0; index < this.MiniCampEntries.Length; ++index)
    {
      if (entry.Level == this.MiniCampEntries[index].Level)
      {
        this.MiniCampEntries[index] = entry;
        break;
      }
    }
  }
}
