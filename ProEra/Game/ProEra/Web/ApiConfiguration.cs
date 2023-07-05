// Decompiled with JetBrains decompiler
// Type: ProEra.Game.ProEra.Web.ApiConfiguration
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Newtonsoft.Json;
using ProEra.Game.ProEra.Web.Models;
using System;
using UnityEngine;

namespace ProEra.Game.ProEra.Web
{
  public class ApiConfiguration : PersistentSingleton<ApiConfiguration>
  {
    private ApiHostModel _apiHostModel;

    public Uri PlayerApiHost => new Uri(this._apiHostModel.PlayerApiHost);

    public Uri LeaderboardApiHost => new Uri(this._apiHostModel.LeaderboardApiHost);

    public Uri RosterApiHost => new Uri(this._apiHostModel.RosterApiHost);

    protected override void Awake() => this._apiHostModel = JsonConvert.DeserializeObject<ApiHostModel>(Resources.Load<TextAsset>("ApiHosts").text);
  }
}
