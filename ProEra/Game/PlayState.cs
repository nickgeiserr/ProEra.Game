// Decompiled with JetBrains decompiler
// Type: ProEra.Game.PlayState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Vars;

namespace ProEra.Game
{
  public static class PlayState
  {
    public static readonly VariableBool PlayOver = new VariableBool(false);
    public static readonly Variable<global::PlayType> PlayType = new Variable<global::PlayType>();
    public static readonly VariableBool HuddleBreak = new VariableBool(false);

    public static bool IsKickoff => PlayState.PlayType.Value == global::PlayType.Kickoff;

    public static bool IsPunt => PlayState.PlayType.Value == global::PlayType.Punt;

    public static bool IsPuntOrKickoff => PlayState.IsPunt || PlayState.IsKickoff;

    public static bool IsRun => PlayState.PlayType.Value == global::PlayType.Run;

    public static bool IsPass => PlayState.PlayType.Value == global::PlayType.Pass;

    public static bool IsRunOrPass => PlayState.IsRun || PlayState.IsPass;
  }
}
