// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Startup.DestinationOptions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using TB12;

namespace ProEra.Game.Startup
{
  [Serializable]
  public struct DestinationOptions
  {
    public string ApiName;
    public EAppState AppState;
    public EMode Mode;
    public ETimeOfDay TimeOfDay;
    public string LobbySessionID;
    public string MatchSessionID;
    public string Password;
    public ulong ExpectedPlayerCount;
    public string StadiumName;
  }
}
