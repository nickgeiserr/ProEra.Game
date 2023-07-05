// Decompiled with JetBrains decompiler
// Type: SplineMesh.ListChangedEventArgs`1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;

namespace SplineMesh
{
  public class ListChangedEventArgs<T> : EventArgs
  {
    public ListChangeType type;
    public List<T> newItems;
    public List<T> removedItems;
    public int insertIndex;
    public int removeIndex;
  }
}
