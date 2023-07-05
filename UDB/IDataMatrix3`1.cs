// Decompiled with JetBrains decompiler
// Type: UDB.IDataMatrix3`1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;

namespace UDB
{
  public interface IDataMatrix3<T> : IEnumerable<T>, IEnumerable
  {
    int Count { get; }

    Vector3i Size { get; }

    bool IsReadOnly { get; }

    T this[int x, int y, int z] { get; set; }

    T this[Vector3i index] { get; set; }

    Vector3i IndexToPosition(int index);

    int PositionToIndex(Vector3i position);

    void Resize(Vector3i newSize, T padding = null);

    void Clear();

    bool Contains(T item);
  }
}
