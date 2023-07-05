// Decompiled with JetBrains decompiler
// Type: ProEra.Game.PlaybookState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Vars;

namespace ProEra.Game
{
  [RuntimeState]
  public static class PlaybookState
  {
    public static readonly VariableInt FormationIdx = new VariableInt(0);
    public static readonly VariableBool OnOffense = new VariableBool();
    public static readonly VariableEnum<BaseFormation> FormationBase = new VariableEnum<BaseFormation>(BaseFormation.Goalline);
    public static readonly Variable<FormationData> CurrentFormation = new Variable<FormationData>();
    public static readonly Variable<PlayData> CurrentPlay = new Variable<PlayData>();
    public static readonly VariableInt CurrentPlayIndex = new VariableInt(0);
    public static readonly VariableBool IsShown = new VariableBool();
    public static Variable<IFormationData> FormationP1 = new Variable<IFormationData>();
    public static Variable<IFormationData> FormationP2 = new Variable<IFormationData>();
    public static AppEvent ShowPlaybook = new AppEvent();
    public static AppEvent HidePlaybook = new AppEvent();
    public static AppEvent<EPlaybookType> SetPlaybookType = new AppEvent<EPlaybookType>();
  }
}
