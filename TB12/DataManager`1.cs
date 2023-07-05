// Decompiled with JetBrains decompiler
// Type: TB12.DataManager`1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  [Serializable]
  public class DataManager<DataType> where DataType : Data
  {
    [SerializeField]
    protected CachedDictionary<int, DataType> _dataDictionary = new CachedDictionary<int, DataType>();

    public ReadOnlyArray<DataType> ReadOnlyDataCollection => this._dataDictionary.ValuesReadonlyCollection;

    public int GetUniqueID() => DataManager<DataType>.GetNewUniqueKeyForList(this._dataDictionary);

    public bool ContainsData(int key) => this._dataDictionary.ContainsKey(key);

    public void RecreateFromDataArray(List<DataType> dataArray)
    {
      this._dataDictionary.Clear();
      if (dataArray == null)
        return;
      int count = dataArray.Count;
      for (int index = 0; index < count; ++index)
        this._dataDictionary.Add(dataArray[index].id, dataArray[index]);
    }

    public DataType RetrieveData(int key)
    {
      if (this._dataDictionary.ContainsKey(key))
        return this._dataDictionary[key];
      Debug.LogError((object) ("No object with id " + key.ToString() + " found."));
      return default (DataType);
    }

    public void UpdateData(int key, DataType newData)
    {
      newData.id = key;
      this.RemoveData(key);
      this.AddDataEntry(newData);
    }

    public virtual void RemoveData(int key) => this._dataDictionary.Remove(key);

    public void AddDataEntry(DataType newEntry) => this._dataDictionary.Add(newEntry.id, newEntry);

    public void Clear() => this._dataDictionary.Clear();

    private static int GetNewUniqueKeyForList(CachedDictionary<int, DataType> dictionary)
    {
      int key;
      do
      {
        key = UnityEngine.Random.Range(0, int.MaxValue);
      }
      while (dictionary.ContainsKey(key));
      return key;
    }
  }
}
