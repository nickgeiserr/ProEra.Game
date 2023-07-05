// Decompiled with JetBrains decompiler
// Type: ProEra.Web.Models.Player.SocialNetworkIdModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;

namespace ProEra.Web.Models.Player
{
  public class SocialNetworkIdModel
  {
    [JsonProperty]
    public string Steam { get; set; }

    [JsonProperty]
    public string Facebook { get; set; }

    [JsonProperty]
    public string Playstation { get; set; }
  }
}
