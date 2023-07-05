// Decompiled with JetBrains decompiler
// Type: ProEra.Web.Definitions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace ProEra.Web
{
  public static class Definitions
  {
    public static Dictionary<Definitions.HighScore, string> HighScoreNames { get; } = new Dictionary<Definitions.HighScore, string>()
    {
      {
        Definitions.HighScore.ProEra,
        "ProEra"
      },
      {
        Definitions.HighScore.TwoMinuteDrill,
        "2-Minute_Drill"
      }
    };

    public enum HighScore
    {
      ProEra,
      TwoMinuteDrill,
    }
  }
}
