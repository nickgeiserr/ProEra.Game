// Decompiled with JetBrains decompiler
// Type: UDB.DataMatrix2`1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UDB
{
  [Serializable]
  public sealed class DataMatrix2<T> : IDataMatrix2<T>, IEnumerable<T>, IEnumerable
  {
    private List<T> rawData;

    public DataMatrix2() => this.rawData = new List<T>();

    public DataMatrix2(Vector2i size, T padding = null)
    {
      this.rawData = new List<T>();
      this.Resize(size, padding);
    }

    public int Count => this.Size.x * this.Size.y;

    public Vector2i Size { get; private set; }

    public bool IsReadOnly => false;

    public T this[int x, int y]
    {
      get => this.rawData[this.PositionToIndex(new Vector2i(x, y))];
      set => this.rawData[this.PositionToIndex(new Vector2i(x, y))] = value;
    }

    public T this[Vector2i index]
    {
      get => this[index.x, index.y];
      set => this[index.x, index.y] = value;
    }

    public void Resize(Vector2i newSize, T padding = null)
    {
      this.Size = newSize;
      DataMatrix2<T>.Resize(this.rawData, this.Size.x * this.Size.y, padding);
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.rawData.GetEnumerator();

    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this.rawData.GetEnumerator();

    public void Clear()
    {
      this.rawData.Clear();
      this.Resize(Vector2i.zero, default (T));
    }

    public bool Contains(T item) => this.rawData.Contains(item);

    public Vector2i IndexToPosition(int index) => new Vector2i(index % this.Size.x, index / this.Size.x % this.Size.y);

    public int PositionToIndex(Vector2i position) => position.x + position.y * this.Size.x;

    private static void Resize(List<T> list, int size, T padding = null)
    {
      if (list.Count > size)
      {
        list.RemoveRange(size, list.Count - size);
      }
      else
      {
        if (list.Capacity > size)
          list.Capacity = size;
        list.AddRange(Enumerable.Repeat<T>(padding, size - list.Count));
      }
    }
  }
}
