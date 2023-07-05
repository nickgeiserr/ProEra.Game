// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Startup.StartupState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Vars;

namespace ProEra.Game.Startup
{
  [RuntimeState]
  public static class StartupState
  {
    public static readonly Variable<bool> AllowDeeplinking = new Variable<bool>(true);
    public static readonly Variable<DestinationOptions> CurrentStartupDestination = new Variable<DestinationOptions>();
    public static readonly AppEvent<DestinationOptions> LoadDestination = new AppEvent<DestinationOptions>();
    public static readonly AppEvent StartupComplete = new AppEvent();
    public static readonly Variable<EAppPlatform> PlatformInUse = new Variable<EAppPlatform>();
  }
}
