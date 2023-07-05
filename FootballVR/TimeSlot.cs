// Decompiled with JetBrains decompiler
// Type: FootballVR.TimeSlot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public struct TimeSlot
  {
    public float startTime;
    public float endTime;

    public TimeSlot(float start, float end)
    {
      this.startTime = start;
      this.endTime = end;
    }
  }
}
