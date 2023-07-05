// Decompiled with JetBrains decompiler
// Type: ProEra.Game.GameTimeoutState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Vars;

namespace ProEra.Game
{
  public static class GameTimeoutState
  {
    public static VariableInt UserTimeouts = new VariableInt(0);
    public static VariableInt CompTimeouts = new VariableInt(0);
    public static VariableBool TimeoutCalledP1 = new VariableBool(false);
    public static VariableBool TimeoutCalledP2 = new VariableBool(false);

    public static int OffenseTimeouts => !MatchState.IsPlayerOneOnOffense ? GameTimeoutState.CompTimeouts.Value : GameTimeoutState.UserTimeouts.Value;

    public static int DefenseTimeouts => !MatchState.IsPlayerOneOnDefense ? GameTimeoutState.CompTimeouts.Value : GameTimeoutState.UserTimeouts.Value;

    public static bool NoTimeoutCalled() => !(bool) GameTimeoutState.TimeoutCalledP1 && !(bool) GameTimeoutState.TimeoutCalledP2;
  }
}
