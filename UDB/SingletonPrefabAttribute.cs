// Decompiled with JetBrains decompiler
// Type: UDB.SingletonPrefabAttribute
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  [AttributeUsage(AttributeTargets.Class, Inherited = true)]
  public class SingletonPrefabAttribute : Attribute
  {
    public readonly string Name;

    public SingletonPrefabAttribute(string name) => this.Name = name;
  }
}
