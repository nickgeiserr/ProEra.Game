// Decompiled with JetBrains decompiler
// Type: TB12.GameplayData.GameLevel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;

namespace TB12.GameplayData
{
  [Serializable]
  public class GameLevel
  {
    public int id;
    public string name;
    public string description;
    public string[] qb;
    public string environment;
    public string stadium;
    public string levelType;
    public string levelId;

    public ETimeOfDay GetTimeOfDay()
    {
      switch (this.environment.ToLower().Trim())
      {
        case "clear":
          return ETimeOfDay.Clear;
        case "dusk":
          return ETimeOfDay.Dusk;
        case "dawn":
          return ETimeOfDay.Dawn;
        case "overcast":
          return ETimeOfDay.Overcast;
        case "night":
          return ETimeOfDay.Night;
        default:
          return ETimeOfDay.Clear;
      }
    }

    public string QbName => this.qb == null || this.qb.Length == 0 || this.qb[0] == null ? string.Empty : this.qb[0];
  }
}
