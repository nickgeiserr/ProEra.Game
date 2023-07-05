// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Globals
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Vars;

namespace ProEra.Game
{
  public static class Globals
  {
    public static readonly VariableBool UserIsHome = new VariableBool();
    public static readonly VariableBool PauseGame = new VariableBool(false);
    public static readonly VariableBool GameOver = new VariableBool(false);
    public static readonly Variable<Player> MenuPlayer = new Variable<Player>();
    public static readonly VariableBool ReplayMode = new VariableBool(false);
  }
}
