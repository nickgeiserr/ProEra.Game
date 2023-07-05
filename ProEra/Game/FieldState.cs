// Decompiled with JetBrains decompiler
// Type: ProEra.Game.FieldState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Vars;

namespace ProEra.Game
{
  public static class FieldState
  {
    public static readonly VariableBool OffenseGoingNorth = new VariableBool();
    public static readonly VariableBool FirstDownLineVisible = new VariableBool();

    public static bool IsBallInOpponentTerritory() => (bool) FieldState.OffenseGoingNorth ? (double) MatchState.BallOn.Value >= (double) Field.MIDFIELD : (double) MatchState.BallOn.Value < (double) Field.MIDFIELD;

    public static int OffensiveFieldDirection => (bool) FieldState.OffenseGoingNorth ? 1 : -1;
  }
}
