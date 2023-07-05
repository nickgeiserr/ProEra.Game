// Decompiled with JetBrains decompiler
// Type: ProEra.Game.PlayStatsUtils
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace ProEra.Game
{
  public static class PlayStatsUtils
  {
    public static bool HasValidQbReference() => (int) PlayStats.QbIndexReference != -1;

    public static bool HasValidOffPlayerReference() => (int) PlayStats.OffPlayerReference != -1;

    public static bool HasValidDefPlayerReference() => (int) PlayStats.DefPlayerReference != -1;
  }
}
