// Decompiled with JetBrains decompiler
// Type: FootballVR.Vector3Cache
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public class Vector3Cache : 
    IReadOnlyList<Vector3>,
    IEnumerable<Vector3>,
    IEnumerable,
    IReadOnlyCollection<Vector3>
  {
    private int _capacity;
    private Vector3[] _values;
    private int _currIndex;
    private int _count;

    public Vector3Cache(int size) => this.SetSize(size);

    public void PushValue(Vector3 pos)
    {
      this._values[this._currIndex] = pos;
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
      this._values = new Vector3[size];
    }

    public IEnumerator<Vector3> GetEnumerator()
    {
      for (int i = 0; i < this._count; ++i)
        yield return this[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public int Count => this._count;

    public Vector3 this[int index] => this._values[this.remapIndex(index)];

    private int remapIndex(int index)
    {
      if (index < 0 || index >= this._count)
        Debug.LogError((object) string.Format("Invalid index accessed {0}", (object) index));
      int num = this._currIndex + index;
      if (num >= this._count)
        num -= this._count;
      return num;
    }

    public Vector3 AverageValue()
    {
      Vector3 zero = Vector3.zero;
      for (int index = 0; index < this._count; ++index)
        zero += this[index];
      return zero / (float) this._count;
    }

    public void FillValues(Vector3 vector)
    {
      while (this._count < this._capacity)
        this.PushValue(vector);
    }
  }
}
