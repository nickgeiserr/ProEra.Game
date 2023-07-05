// Decompiled with JetBrains decompiler
// Type: Axis.CsvCell
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace Axis
{
  public class CsvCell
  {
    public CsvCellType Type { get; set; }

    public string Name { get; set; }

    public object Value { get; set; }

    public override string ToString() => this.Type == CsvCellType.String ? "\"" + this.Value.ToString() + "\"" : this.Value.ToString();
  }
}
