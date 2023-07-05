// Decompiled with JetBrains decompiler
// Type: Axis.CsvExport
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using System.Text;

namespace Axis
{
  public class CsvExport
  {
    private readonly string delimeter;
    private readonly List<CsvRow> rows;

    public CsvExport(string delimeter = ",")
    {
      this.delimeter = delimeter;
      this.rows = new List<CsvRow>();
    }

    public void AddRow(List<CsvCell> cells) => this.rows.Add(new CsvRow()
    {
      Cells = cells
    });

    public void WriteToFile(string folderUnderMods, string filenameWithExtension)
    {
      if (this.rows.Count == 0)
        return;
      List<string> stringList = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.rows[0].Cells.Count; ++index)
      {
        CsvCell cell = this.rows[0].Cells[index];
        stringBuilder.Append(cell.Name);
        if (index != this.rows[0].Cells.Count - 1)
          stringBuilder.Append(this.delimeter);
      }
      stringList.Add(stringBuilder.ToString());
      stringBuilder.Length = 0;
      for (int index1 = 0; index1 < this.rows.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.rows[index1].Cells.Count; ++index2)
        {
          CsvCell cell = this.rows[index1].Cells[index2];
          stringBuilder.Append(cell.ToString());
          if (index2 != this.rows[index1].Cells.Count - 1)
            stringBuilder.Append(this.delimeter);
        }
        stringList.Add(stringBuilder.ToString());
        stringBuilder.Length = 0;
      }
      ModManager.SaveLinesToTextFile(folderUnderMods, filenameWithExtension, stringList.ToArray());
    }
  }
}
