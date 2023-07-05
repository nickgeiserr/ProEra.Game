// Decompiled with JetBrains decompiler
// Type: Axis.PlayLog
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using System.Text;

namespace Axis
{
  public class PlayLog
  {
    private readonly List<string> events;

    public PlayLog() => this.events = new List<string>();

    public void Clear() => this.events.Clear();

    public void Add(string newEvent) => this.events.Add(newEvent);

    public override string ToString() => this.PlayLogToString(this.events, "\n");

    private string PlayLogToString(List<string> logs, string separator)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Clear();
      foreach (string log in logs)
        stringBuilder.Append(log + separator);
      return stringBuilder.ToString();
    }
  }
}
