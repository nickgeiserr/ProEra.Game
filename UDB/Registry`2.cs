// Decompiled with JetBrains decompiler
// Type: UDB.Registry`2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class Registry<TKey, TStored> : Dictionary<TKey, TStored>
  {
    public void Register(TKey key, TStored obj)
    {
      if (this.ContainsKey(key))
        Debug.LogWarning((object) ("Overriding stored object for key: " + key?.ToString()));
      this[key] = obj;
    }

    public TStored GetValue(TKey key)
    {
      if (this.ContainsKey(key))
        return this[key];
      Debug.LogError((object) ("Failed to find key: " + key?.ToString() + " in the registry!"));
      return default (TStored);
    }
  }
}
