// Decompiled with JetBrains decompiler
// Type: UDB.TransformElement
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  [Flags]
  public enum TransformElement
  {
    None = 0,
    Position = 1,
    Rotation = 2,
    Scale = 4,
    All = Scale | Rotation | Position, // 0x00000007
  }
}
