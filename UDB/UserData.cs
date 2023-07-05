// Decompiled with JetBrains decompiler
// Type: UDB.UserData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace UDB
{
  public abstract class UserData : MonoBehaviour, IEnumerable
  {
    public bool loadOnStart = true;
    private Dictionary<string, object> values;
    private List<UserData.Data> tempData;
    private string[] tempKeys;
    private string tempKey;
    private object tempValue;

    private void Awake()
    {
      if (!this.loadOnStart)
        return;
      this.LoadOnStart();
    }

    private void Start()
    {
    }

    public IEnumerator GetEnumerator() => this.values == null ? (IEnumerator) null : (IEnumerator) this.values.GetEnumerator();

    private UserData.Data[] LoadData(byte[] data)
    {
      UserData.Data[] dataArray = (UserData.Data[]) null;
      using (MemoryStream serializationStream = new MemoryStream(data))
        dataArray = (UserData.Data[]) new BinaryFormatter().Deserialize((Stream) serializationStream);
      return dataArray ?? new UserData.Data[0];
    }

    private void SaveData(UserData.Data[] data)
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      MemoryStream memoryStream = new MemoryStream();
      MemoryStream serializationStream = memoryStream;
      UserData.Data[] graph = data;
      binaryFormatter.Serialize((Stream) serializationStream, (object) graph);
      this.SaveRawData(memoryStream.GetBuffer());
    }

    public void Load()
    {
      byte[] data = this.LoadRawData();
      if (data == null || data.Length == 0)
        return;
      UserData.Data[] dataArray = this.LoadData(data);
      this.values = new Dictionary<string, object>();
      for (int index = 0; index < dataArray.Length; ++index)
        this.values.Add(dataArray[index].name, dataArray[index].obj);
    }

    public void Save()
    {
      if (this.values == null)
        return;
      this.tempData = new List<UserData.Data>(this.values.Count);
      this.tempKeys = this.values.Keys.ToArray<string>();
      for (int index = 0; index < this.tempKeys.Length; ++index)
      {
        this.tempKey = this.tempKeys[index];
        this.tempValue = this.values[this.tempKey];
        this.tempData.Add(new UserData.Data()
        {
          name = this.tempKey,
          obj = this.tempValue
        });
      }
      this.SaveData(this.tempData.ToArray());
    }

    public void Delete()
    {
      if (this.values != null)
        this.values.Clear();
      this.DeleteRawData();
    }

    public string[] GetKeys(Predicate<KeyValuePair<string, object>> predicate)
    {
      List<string> stringList = new List<string>(this.values.Count);
      foreach (KeyValuePair<string, object> keyValuePair in this.values)
      {
        if (predicate(keyValuePair))
          stringList.Add(keyValuePair.Key);
      }
      return stringList.ToArray();
    }

    public bool HasKey(string name) => this.values != null && this.values.ContainsKey(name);

    public System.Type GetType(string name)
    {
      object obj;
      return this.values != null && this.values.TryGetValue(name, out obj) && obj != null ? obj.GetType() : (System.Type) null;
    }

    public int GetInt(string name, int defaultValue = 0)
    {
      object obj;
      return this.values != null && this.values.TryGetValue(name, out obj) && obj is int ? Convert.ToInt32(obj) : defaultValue;
    }

    public void SetInt(string name, int value)
    {
      if (this.values == null)
        this.values = new Dictionary<string, object>();
      if (!this.values.ContainsKey(name))
        this.values.Add(name, (object) value);
      else
        this.values[name] = (object) value;
    }

    public float GetFloat(string name, float defaultValue = 0.0f)
    {
      object obj;
      return this.values != null && this.values.TryGetValue(name, out obj) && obj is float ? Convert.ToSingle(obj) : defaultValue;
    }

    public void SetFloat(string name, float value)
    {
      if (this.values == null)
        this.values = new Dictionary<string, object>();
      if (!this.values.ContainsKey(name))
        this.values.Add(name, (object) value);
      else
        this.values[name] = (object) value;
    }

    public string GetString(string name, string defaultValue = "")
    {
      object obj;
      return this.values != null && this.values.TryGetValue(name, out obj) && obj is string ? Convert.ToString(obj) : defaultValue;
    }

    public void SetString(string name, string value)
    {
      if (this.values == null)
        this.values = new Dictionary<string, object>();
      if (!this.values.ContainsKey(name))
        this.values.Add(name, (object) value);
      else
        this.values[name] = (object) value;
    }

    public void Delete(string name)
    {
      if (this.values == null)
        return;
      this.values.Remove(name);
    }

    protected virtual void LoadOnStart() => this.Load();

    protected abstract byte[] LoadRawData();

    protected abstract void SaveRawData(byte[] dat);

    protected abstract void DeleteRawData();

    [Serializable]
    public struct Data
    {
      public string name;
      public object obj;
    }

    public enum Action
    {
      Load,
      Save,
    }
  }
}
