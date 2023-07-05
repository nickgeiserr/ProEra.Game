// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.ShaderTextureProperty
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;

namespace DigitalOpus.MB.Core
{
  [Serializable]
  public class ShaderTextureProperty
  {
    public string name;
    public bool isNormalMap;

    public ShaderTextureProperty(string n, bool norm)
    {
      this.name = n;
      this.isNormalMap = norm;
    }

    public static string[] GetNames(List<ShaderTextureProperty> props)
    {
      string[] names = new string[props.Count];
      for (int index = 0; index < names.Length; ++index)
        names[index] = props[index].name;
      return names;
    }
  }
}
