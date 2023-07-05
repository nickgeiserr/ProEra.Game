// Decompiled with JetBrains decompiler
// Type: ProEra.Web.Models.HighScoreModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using System;

namespace ProEra.Web.Models
{
  [Serializable]
  public class HighScoreModel
  {
    [JsonProperty]
    public float Score { get; set; }

    [JsonProperty]
    public bool ShareWithOthers { get; set; }
  }
}
