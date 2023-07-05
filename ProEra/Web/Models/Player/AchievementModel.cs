// Decompiled with JetBrains decompiler
// Type: ProEra.Web.Models.Player.AchievementModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using System;

namespace ProEra.Web.Models.Player
{
  [Serializable]
  public class AchievementModel
  {
    [JsonProperty]
    public int CurrentTier { get; set; }

    [JsonProperty]
    public DateTime[] Timestamps { get; set; }

    [JsonProperty]
    public string[] Platforms { get; set; }

    [JsonProperty]
    public bool Acknowledged { get; set; }

    [JsonProperty]
    public string Name { get; set; }

    [JsonProperty]
    public string Description { get; set; }

    [JsonProperty]
    public int CurrentValue { get; set; }

    public AchievementModel()
    {
      this.CurrentTier = 0;
      this.Timestamps = Array.Empty<DateTime>();
      this.Platforms = Array.Empty<string>();
      this.Acknowledged = false;
      this.Name = string.Empty;
      this.Description = string.Empty;
      this.CurrentValue = 0;
    }
  }
}
