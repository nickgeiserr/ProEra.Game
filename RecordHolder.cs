// Decompiled with JetBrains decompiler
// Type: RecordHolder
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;

[MessagePackObject(false)]
[Serializable]
public class RecordHolder
{
  [Key(0)]
  public string RecordName;
  [Key(1)]
  public string RecordHolderName;
  [Key(2)]
  public string RecordHolderTeam;
  [Key(3)]
  public int Year;
  [Key(4)]
  public float RecordValue;

  public static RecordHolder New(string recordName, float defaultValue = 0.0f) => new RecordHolder()
  {
    RecordName = recordName,
    RecordHolderName = "-",
    RecordHolderTeam = "-",
    Year = 0,
    RecordValue = defaultValue
  };

  public void UpdateRecord(
    string recordHolderName,
    string recordHolderTeam,
    int year,
    float recordValue)
  {
    this.RecordHolderName = recordHolderName;
    this.RecordHolderTeam = recordHolderTeam;
    this.Year = year;
    this.RecordValue = recordValue;
  }
}
