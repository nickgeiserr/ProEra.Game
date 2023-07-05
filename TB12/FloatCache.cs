// Decompiled with JetBrains decompiler
// Type: TB12.FloatCache
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  public class FloatCache : 
    IReadOnlyList<float>,
    IEnumerable<float>,
    IEnumerable,
    IReadOnlyCollection<float>
  {
    private int _capacity;
    private float[] _values;
    private int _currIndex;
    private int _count;

    public FloatCache(int size) => this.SetSize(size);

    public void PushValue(float val)
    {
      this._values[this._currIndex] = val;
      if (this._count < this._capacity)
        ++this._count;
      ++this._currIndex;
      if (this._currIndex < this._capacity)
        return;
      this._currIndex = 0;
    }

    public void Clear()
    {
      this._currIndex = 0;
      this._count = 0;
    }

    public void SetSize(int size)
    {
      this.Clear();
      this._capacity = size;
      this._values = new float[size];
    }

    public IEnumerator<float> GetEnumerator()
    {
      for (int i = 0; i < this._count; ++i)
        yield return this[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public int Count => this._count;

    public float this[int index] => this._values[this.remapIndex(index)];

    private int remapIndex(int index)
    {
      if (index < 0 || index >= this._count)
        Debug.LogError((object) string.Format("Invalid index accessed {0}", (object) index));
      int num = this._currIndex + index;
      if (num >= this._count)
        num -= this._count;
      return num;
    }

    public float AverageValue()
    {
      float num = 0.0f;
      for (int index = 0; index < this._count; ++index)
        num += this[index];
      return num / (float) this._count;
    }

    public void FillValues(float val)
    {
      while (this._count < this._capacity)
        this.PushValue(val);
    }
  }
}
