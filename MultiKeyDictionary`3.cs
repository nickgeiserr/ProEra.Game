// Decompiled with JetBrains decompiler
// Type: MultiKeyDictionary`3
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class MultiKeyDictionary<K, L, V>
{
  internal readonly Dictionary<K, V> baseDictionary = new Dictionary<K, V>();
  internal readonly Dictionary<L, K> subDictionary = new Dictionary<L, K>();
  internal readonly Dictionary<K, L> primaryToSubkeyMapping = new Dictionary<K, L>();
  private ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();

  public V this[L subKey]
  {
    get
    {
      V val;
      if (this.TryGetValue(subKey, out val))
        return val;
      throw new KeyNotFoundException("sub key not found: " + subKey.ToString());
    }
  }

  public V this[K primaryKey]
  {
    get
    {
      V val;
      if (this.TryGetValue(primaryKey, out val))
        return val;
      throw new KeyNotFoundException("primary key not found: " + primaryKey.ToString());
    }
  }

  public void Associate(L subKey, K primaryKey)
  {
    this.readerWriterLock.EnterUpgradeableReadLock();
    try
    {
      if (!this.baseDictionary.ContainsKey(primaryKey))
        throw new KeyNotFoundException(string.Format("The base dictionary does not contain the key '{0}'", (object) primaryKey));
      if (this.primaryToSubkeyMapping.ContainsKey(primaryKey))
      {
        this.readerWriterLock.EnterWriteLock();
        try
        {
          if (this.subDictionary.ContainsKey(this.primaryToSubkeyMapping[primaryKey]))
            this.subDictionary.Remove(this.primaryToSubkeyMapping[primaryKey]);
          this.primaryToSubkeyMapping.Remove(primaryKey);
        }
        finally
        {
          this.readerWriterLock.ExitWriteLock();
        }
      }
      this.subDictionary[subKey] = primaryKey;
      this.primaryToSubkeyMapping[primaryKey] = subKey;
    }
    finally
    {
      this.readerWriterLock.ExitUpgradeableReadLock();
    }
  }

  public bool TryGetValue(L subKey, out V val)
  {
    val = default (V);
    this.readerWriterLock.EnterReadLock();
    try
    {
      K key;
      if (this.subDictionary.TryGetValue(subKey, out key))
        return this.baseDictionary.TryGetValue(key, out val);
    }
    finally
    {
      this.readerWriterLock.ExitReadLock();
    }
    return false;
  }

  public bool TryGetValue(K primaryKey, out V val)
  {
    this.readerWriterLock.EnterReadLock();
    try
    {
      return this.baseDictionary.TryGetValue(primaryKey, out val);
    }
    finally
    {
      this.readerWriterLock.ExitReadLock();
    }
  }

  public bool ContainsKey(L subKey) => this.TryGetValue(subKey, out V _);

  public bool ContainsKey(K primaryKey) => this.TryGetValue(primaryKey, out V _);

  public void Remove(K primaryKey)
  {
    this.readerWriterLock.EnterWriteLock();
    try
    {
      if (this.primaryToSubkeyMapping.ContainsKey(primaryKey))
      {
        if (this.subDictionary.ContainsKey(this.primaryToSubkeyMapping[primaryKey]))
          this.subDictionary.Remove(this.primaryToSubkeyMapping[primaryKey]);
        this.primaryToSubkeyMapping.Remove(primaryKey);
      }
      this.baseDictionary.Remove(primaryKey);
    }
    finally
    {
      this.readerWriterLock.ExitWriteLock();
    }
  }

  public void Remove(L subKey)
  {
    this.readerWriterLock.EnterWriteLock();
    try
    {
      this.baseDictionary.Remove(this.subDictionary[subKey]);
      this.primaryToSubkeyMapping.Remove(this.subDictionary[subKey]);
      this.subDictionary.Remove(subKey);
    }
    finally
    {
      this.readerWriterLock.ExitWriteLock();
    }
  }

  public void Add(K primaryKey, V val)
  {
    this.readerWriterLock.EnterWriteLock();
    try
    {
      this.baseDictionary.Add(primaryKey, val);
    }
    finally
    {
      this.readerWriterLock.ExitWriteLock();
    }
  }

  public void Add(K primaryKey, L subKey, V val)
  {
    this.Add(primaryKey, val);
    this.Associate(subKey, primaryKey);
  }

  public V[] CloneValues()
  {
    this.readerWriterLock.EnterReadLock();
    try
    {
      V[] array = new V[this.baseDictionary.Values.Count];
      this.baseDictionary.Values.CopyTo(array, 0);
      return array;
    }
    finally
    {
      this.readerWriterLock.ExitReadLock();
    }
  }

  public List<V> Values
  {
    get
    {
      this.readerWriterLock.EnterReadLock();
      try
      {
        return this.baseDictionary.Values.ToList<V>();
      }
      finally
      {
        this.readerWriterLock.ExitReadLock();
      }
    }
  }

  public K[] ClonePrimaryKeys()
  {
    this.readerWriterLock.EnterReadLock();
    try
    {
      K[] array = new K[this.baseDictionary.Keys.Count];
      this.baseDictionary.Keys.CopyTo(array, 0);
      return array;
    }
    finally
    {
      this.readerWriterLock.ExitReadLock();
    }
  }

  public L[] CloneSubKeys()
  {
    this.readerWriterLock.EnterReadLock();
    try
    {
      L[] array = new L[this.subDictionary.Keys.Count];
      this.subDictionary.Keys.CopyTo(array, 0);
      return array;
    }
    finally
    {
      this.readerWriterLock.ExitReadLock();
    }
  }

  public void Clear()
  {
    this.readerWriterLock.EnterWriteLock();
    try
    {
      this.baseDictionary.Clear();
      this.subDictionary.Clear();
      this.primaryToSubkeyMapping.Clear();
    }
    finally
    {
      this.readerWriterLock.ExitWriteLock();
    }
  }

  public int Count
  {
    get
    {
      this.readerWriterLock.EnterReadLock();
      try
      {
        return this.baseDictionary.Count;
      }
      finally
      {
        this.readerWriterLock.ExitReadLock();
      }
    }
  }

  public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
  {
    this.readerWriterLock.EnterReadLock();
    try
    {
      return (IEnumerator<KeyValuePair<K, V>>) this.baseDictionary.GetEnumerator();
    }
    finally
    {
      this.readerWriterLock.ExitReadLock();
    }
  }
}
