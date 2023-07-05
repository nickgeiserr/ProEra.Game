// Decompiled with JetBrains decompiler
// Type: TB12.AppSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;
using Vars;

namespace TB12
{
  [CreateAssetMenu(fileName = "AppSettings", menuName = "TB12/Settings/AppSettings", order = 1)]
  [SettingsConfig]
  public class AppSettings : SingletonScriptableSettings<AppSettings>
  {
    public DifficultySettings DifficultySettings = new DifficultySettings();
    public GameModeVar GameMode = new GameModeVar(EAppMode.Game);
    public VariableBool Announcer = new VariableBool(false);
    public OptimizationSettings OptimizationSettings = new OptimizationSettings();

    protected override void OnEnable() => base.OnEnable();

    public DifficultySetting GetDifficultySetting(EDifficulty difficulty)
    {
      switch (difficulty)
      {
        case EDifficulty.Pro:
          return this.DifficultySettings.Pro;
        case EDifficulty.AllStar:
          return this.DifficultySettings.AllStar;
        default:
          return this.DifficultySettings.Rookie;
      }
    }
  }
}
