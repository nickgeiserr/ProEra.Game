// Decompiled with JetBrains decompiler
// Type: ProEra.Web.Models.Player.PlayerModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using System;

namespace ProEra.Web.Models.Player
{
  [Serializable]
  public class PlayerModel
  {
    [JsonProperty]
    public string AuthSystemId { get; set; }

    public string PlayerId { get; set; }

    [JsonProperty]
    public string PlayerName { get; set; }

    [JsonProperty]
    public string[] FriendList { get; set; }

    [JsonProperty]
    public string[] PendingFriendList { get; set; }

    [JsonProperty]
    public string Platform { get; set; }

    [JsonProperty]
    public ProEraDataModel ProEra { get; set; }

    public PlayerModel()
    {
      this.AuthSystemId = string.Empty;
      this.PlayerId = string.Empty;
      this.PlayerName = string.Empty;
      this.FriendList = Array.Empty<string>();
      this.PendingFriendList = Array.Empty<string>();
      this.Platform = string.Empty;
      this.ProEra = new ProEraDataModel();
    }
  }
}
