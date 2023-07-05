// Decompiled with JetBrains decompiler
// Type: MovementEffects.Segment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MovementEffects
{
  public enum Segment
  {
    Invalid = -1, // 0xFFFFFFFF
    Update = 0,
    FixedUpdate = 1,
    LateUpdate = 2,
    SlowUpdate = 3,
    RealtimeUpdate = 4,
    EditorUpdate = 5,
    EditorSlowUpdate = 6,
    EndOfFrame = 7,
    ManualTimeframe = 8,
  }
}
