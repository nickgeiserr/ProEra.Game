// Decompiled with JetBrains decompiler
// Type: ProEra.Web.Models.Player.ProEraDataModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ProEra.Web.Models.Player
{
  [Serializable]
  public class ProEraDataModel
  {
    [JsonProperty]
    public AvatarDnaModel AvatarDna { get; set; }

    [JsonProperty]
    public AchievementModel[] Achievements { get; set; }

    [JsonProperty]
    public Dictionary<string, HighScoreModel> HighScores { get; set; }

    public ProEraDataModel()
    {
      this.AvatarDna = new AvatarDnaModel();
      this.Achievements = Array.Empty<AchievementModel>();
      this.HighScores = new Dictionary<string, HighScoreModel>()
      {
        {
          "ProEra",
          new HighScoreModel()
        }
      };
    }
  }
}
