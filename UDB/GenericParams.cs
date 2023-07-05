// Decompiled with JetBrains decompiler
// Type: UDB.GenericParams
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace UDB
{
  public class GenericParams : Dictionary<string, object>
  {
    public new object this[string key]
    {
      get
      {
        object obj;
        this.TryGetValue(key, out obj);
        return obj;
      }
      set
      {
        if (this.ContainsKey(key))
          base[key] = value;
        else
          this.Add(key, value);
      }
    }

    public GenericParams(params GenericParamArg[] args)
      : base(args.Length)
    {
      for (int index = 0; index < args.Length; ++index)
        this.Add(args[index].key, args[index].value);
    }

    public T GetValue<T>(string key) => (T) this[key];

    public bool TryGetValue<T>(string key, out T value)
    {
      object obj;
      if (this.TryGetValue(key, out obj))
      {
        value = (T) obj;
        return true;
      }
      value = default (T);
      return false;
    }
  }
}
