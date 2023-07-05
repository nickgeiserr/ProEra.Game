// Decompiled with JetBrains decompiler
// Type: FootballVR.GameSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using System;
using UnityEngine;
using UnityEngine.Events;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "GameSettings", menuName = "TB12/Settings/GameSettings", order = 1)]
  [SettingsConfig]
  public class GameSettings : SingletonScriptableSettings<GameSettings>
  {
    [NonSerialized]
    public Save_OldGameSettings saveOldGameSettings = new Save_OldGameSettings();
    public static VariableFloat TimeScale = new VariableFloat(0.9f);
    public static VariableBool OffenseGoingNorth = new VariableBool(true);
    public static VariableBool PlayerOnField = new VariableBool(false);
    public static VariableInt DifficultyLevel = new VariableInt(1);
    public float AutoDropBackBulletTimeSpeed = 0.5f;
    public float HandoffBulletTimeSpeed = 0.125f;
    public VariableFloat _timescale = new VariableFloat(0.9f);
    public VariableInt _difficultyLevel = new VariableInt(1);
    public readonly LinksHandler _linksHandler = new LinksHandler();
    [SerializeField]
    private DifficultySetting[] _difficultySettings;
    private int _initCount;
    private const int LINKS_COUNT = 2;

    protected override void OnEnable()
    {
      base.OnEnable();
      this._initCount = 0;
      ScriptableSettings.Initialiation.Link(new System.Action(this.FinishedInitializingSettings));
    }

    private void FinishedInitializingSettings()
    {
      ScriptableSingleton<GameSettings>.Instance._linksHandler.AddLink(ScriptableSingleton<GameSettings>.Instance._timescale.Link<float>(new Action<float>(this.InitialTimescaleUpdate)));
      ScriptableSingleton<GameSettings>.Instance._linksHandler.AddLink(ScriptableSingleton<GameSettings>.Instance._difficultyLevel.Link<int>(new Action<int>(this.InitialDifficultyUpdate)));
    }

    private void InitializationCleanup()
    {
      ++this._initCount;
      if (this._initCount < 2)
        return;
      ScriptableSingleton<GameSettings>.Instance._linksHandler.Clear();
    }

    public static void UpdateTimescaleHandler(float value)
    {
      Time.timeScale = value;
      ScriptableSingleton<GameSettings>.Instance._timescale.SetValue(value);
      GameSettings.TimeScale.SetValue(value);
    }

    public void InitialTimescaleUpdate(float value)
    {
      this.InitializationCleanup();
      Time.timeScale = value;
      ScriptableSingleton<GameSettings>.Instance._timescale.SetValue(value);
      GameSettings.TimeScale.SetValue(value);
    }

    public static void UpdateDifficultyHandler(int value)
    {
      ScriptableSingleton<GameSettings>.Instance._difficultyLevel.SetValue(value);
      GameSettings.DifficultyLevel.SetValue(value);
      float gameSpeed = GameSettings.GetDifficulty().GameSpeed;
      Time.timeScale = gameSpeed;
      ScriptableSingleton<GameSettings>.Instance._timescale.SetValue(gameSpeed);
      GameSettings.TimeScale.SetValue(gameSpeed);
    }

    public void InitialDifficultyUpdate(int value)
    {
      this.InitializationCleanup();
      ScriptableSingleton<GameSettings>.Instance._difficultyLevel.SetValue(value);
      GameSettings.DifficultyLevel.SetValue(value);
      float gameSpeed = GameSettings.GetDifficulty().GameSpeed;
      Time.timeScale = gameSpeed;
      ScriptableSingleton<GameSettings>.Instance._timescale.SetValue(gameSpeed);
      GameSettings.TimeScale.SetValue(gameSpeed);
    }

    public static DifficultySetting GetDifficulty() => ScriptableSingleton<GameSettings>.Instance._difficultySettings[GameSettings.DifficultyLevel.Value];

    public void PrepareToLoad()
    {
      this.saveOldGameSettings = new Save_OldGameSettings();
      this.saveOldGameSettings.OnLoaded += new UnityAction(this.SettingsLoaded);
    }

    private void SettingsLoaded()
    {
      GameSettings.TimeScale.SetValue(this.saveOldGameSettings.TimeScale);
      GameSettings.OffenseGoingNorth.SetValue(this.saveOldGameSettings.OffenseGoingNorth);
      GameSettings.PlayerOnField.SetValue(this.saveOldGameSettings.PlayerOnField);
      GameSettings.DifficultyLevel.SetValue(this.saveOldGameSettings.DifficultyLevel);
      this.AutoDropBackBulletTimeSpeed = this.saveOldGameSettings.AutoDropBackBulletTimeSpeed;
      this.HandoffBulletTimeSpeed = this.saveOldGameSettings.HandoffBulletTimeSpeed;
      this._timescale.SetValue(this.saveOldGameSettings.TimeScale);
      this._difficultyLevel.SetValue(this.saveOldGameSettings.DifficultyLevel);
      this.saveOldGameSettings.OnLoaded -= new UnityAction(this.SettingsLoaded);
    }

    public void PrepareToSave()
    {
      this.saveOldGameSettings.TimeScale = GameSettings.TimeScale.Value;
      this.saveOldGameSettings.OffenseGoingNorth = GameSettings.OffenseGoingNorth.Value;
      this.saveOldGameSettings.PlayerOnField = GameSettings.PlayerOnField.Value;
      this.saveOldGameSettings.DifficultyLevel = GameSettings.DifficultyLevel.Value;
      this.saveOldGameSettings.AutoDropBackBulletTimeSpeed = this.AutoDropBackBulletTimeSpeed;
      this.saveOldGameSettings.HandoffBulletTimeSpeed = this.HandoffBulletTimeSpeed;
    }
  }
}
