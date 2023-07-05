// Decompiled with JetBrains decompiler
// Type: TB12.ThrowGameResultsUIData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace TB12
{
  public class ThrowGameResultsUIData
  {
    public Dictionary<string, int> PlayerScores = new Dictionary<string, int>();

    public KeyValuePair<string, int> GetOrderedPlayerScores() => this.PlayerScores.OrderBy<KeyValuePair<string, int>, int>((Func<KeyValuePair<string, int>, int>) (x => x.Value)).GetEnumerator().Current;
  }
}
