// Decompiled with JetBrains decompiler
// Type: UDB.IDataMatrix2`1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;

namespace UDB
{
  public interface IDataMatrix2<T> : IEnumerable<T>, IEnumerable
  {
    int Count { get; }

    Vector2i Size { get; }

    bool IsReadOnly { get; }

    T this[int x, int y] { get; set; }

    T this[Vector2i index] { get; set; }

    Vector2i IndexToPosition(int index);

    int PositionToIndex(Vector2i position);

    void Resize(Vector2i newSize, T padding = null);

    void Clear();

    bool Contains(T item);
  }
}
