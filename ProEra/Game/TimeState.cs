// Decompiled with JetBrains decompiler
// Type: ProEra.Game.TimeState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Vars;

namespace ProEra.Game
{
  public static class TimeState
  {
    public static VariableInt Minutes = new VariableInt(0);
    public static VariableInt Seconds = new VariableInt(0);

    public static bool IsZero() => (int) TimeState.Minutes == 0 && (int) TimeState.Seconds == 0;

    public static void SetTime(int min, int sec)
    {
      TimeState.Minutes.Value = min;
      TimeState.Seconds.Value = sec;
    }
  }
}
