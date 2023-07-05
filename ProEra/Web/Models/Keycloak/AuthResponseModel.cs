// Decompiled with JetBrains decompiler
// Type: ProEra.Web.Models.Keycloak.AuthResponseModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using System;
using System.Net;

namespace ProEra.Web.Models.Keycloak
{
  [Serializable]
  public struct AuthResponseModel
  {
    [JsonProperty]
    public HttpStatusCode ResponseStatus { get; set; }

    [JsonProperty]
    public string Username { get; set; }

    [JsonProperty]
    public string AuthToken { get; set; }

    [JsonProperty]
    public string RefreshToken { get; set; }
  }
}
