// Decompiled with JetBrains decompiler
// Type: ProEra.Web.Models.Player.HighScoreModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using System;

namespace ProEra.Web.Models.Player
{
  [Serializable]
  public class HighScoreModel
  {
    [JsonProperty("score")]
    public float Score { get; set; }

    [JsonProperty("shareWithOthers")]
    public bool ShareWithOthers { get; set; }

    public HighScoreModel()
    {
      this.Score = 0.0f;
      this.ShareWithOthers = true;
    }
  }
}
