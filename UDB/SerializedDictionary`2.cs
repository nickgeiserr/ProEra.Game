// Decompiled with JetBrains decompiler
// Type: UDB.SerializedDictionary`2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  [Serializable]
  public class SerializedDictionary<TKey, TValue> : 
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable
    where TKey : IEquatable<TKey>
  {
    [SerializeField]
    private List<TKey> _keysList = new List<TKey>();
    [SerializeField]
    private List<TValue> _valuesList = new List<TValue>();

    public List<TKey> keysList
    {
      get => this._keysList;
      set => this._keysList = value;
    }

    public List<TValue> valuesList
    {
      get => this._valuesList;
      set => this._valuesList = value;
    }

    public void Add(TKey key, TValue data)
    {
      if (!this.ContainsKey(key))
      {
        this.keysList.Add(key);
        this.valuesList.Add(data);
      }
      else
        this.SetValue(key, data);
    }

    public void Remove(TKey key)
    {
      this.valuesList.Remove(this.GetValue(key));
      this.keysList.Remove(key);
    }

    public bool ContainsKey(TKey key) => this.ConvertToDictionary().ContainsKey(key);

    public bool ContainsValue(TValue data) => this.ConvertToDictionary().ContainsValue(data);

    public void Clear()
    {
      if (this.keysList.Count > 0)
        this.keysList.Clear();
      if (this.valuesList.Count <= 0)
        return;
      this.valuesList.Clear();
    }

    public void SetValue(TKey key, TValue data)
    {
      int index1 = 0;
      for (int index2 = 0; index2 < this.keysList.Count; ++index2)
      {
        if (this.keysList[index2].Equals(key))
          index1 = index2;
      }
      this.valuesList[index1] = data;
    }

    public TKey GetKey(TKey key)
    {
      for (int index = 0; index < this.keysList.Count; ++index)
      {
        if (this.keysList[index].Equals(key))
          return this.keysList[index];
      }
      return default (TKey);
    }

    public TValue GetValue(TKey key)
    {
      int index1 = 0;
      for (int index2 = 0; index2 < this.keysList.Count; ++index2)
      {
        if (this.keysList[index2].Equals(key))
          index1 = index2;
      }
      return this.valuesList[index1];
    }

    public Dictionary<TKey, TValue> ConvertToDictionary()
    {
      Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
      try
      {
        for (int index = 0; index < this.keysList.Count; ++index)
          dictionary.Add(this.keysList[index], this.valuesList[index]);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) "KeysList.Count is not equal to ValuesList.Count. It shouldn't happen!");
      }
      return dictionary;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      for (int i = 0; i < this.keysList.Count; ++i)
        yield return new KeyValuePair<TKey, TValue>(this.keysList[i], this.valuesList[i]);
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
