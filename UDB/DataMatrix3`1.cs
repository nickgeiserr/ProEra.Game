// Decompiled with JetBrains decompiler
// Type: UDB.DataMatrix3`1
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
  public sealed class DataMatrix3<T> : IDataMatrix3<T>, IEnumerable<T>, IEnumerable
  {
    private List<T> rawData;

    public DataMatrix3() => this.rawData = new List<T>();

    public DataMatrix3(Vector3i size, T padding = null)
    {
      this.rawData = new List<T>();
      this.Resize(size, padding);
    }

    public int Count => this.Size.x * this.Size.y * this.Size.z;

    public Vector3i Size { get; private set; }

    public bool IsReadOnly => false;

    public T this[int x, int y, int z]
    {
      get => this.rawData[this.PositionToIndex(new Vector3i(x, y, z))];
      set => this.rawData[this.PositionToIndex(new Vector3i(x, y, z))] = value;
    }

    public T this[Vector3i index]
    {
      get => this[index.x, index.y, index.z];
      set => this[index.x, index.y, index.z] = value;
    }

    public void Resize(Vector3i newSize, T padding = null)
    {
      this.Size = newSize;
      DataMatrix3<T>.Resize(this.rawData, this.Size.x * this.Size.y * this.Size.z, padding);
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.rawData.GetEnumerator();

    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this.rawData.GetEnumerator();

    public void Clear()
    {
      this.rawData.Clear();
      this.Resize(Vector3i.zero, default (T));
    }

    public bool Contains(T item) => this.rawData.Contains(item);

    public Vector3i IndexToPosition(int index) => new Vector3i(index % this.Size.x, index / this.Size.x % this.Size.y, index / (this.Size.x * this.Size.y));

    public int PositionToIndex(Vector3i position) => position.x + position.y * this.Size.x + position.z * this.Size.x * this.Size.y;

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
