// Decompiled with JetBrains decompiler
// Type: FootballVR.ThrowSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "ThrowSettings", menuName = "TB12/Settings/ThrowSettings", order = 1)]
  [SettingsConfig]
  public class ThrowSettings : SingletonScriptableSettings<ThrowSettings>
  {
    [SerializeField]
    private ThrowSettingsConfig _highPassAssistConfig = new ThrowSettingsConfig();
    [SerializeField]
    private ThrowSettingsConfig _lowPassAssistConfig = new ThrowSettingsConfig();
    [SerializeField]
    private ThrowSettingsConfig _noPassAssistConfig = new ThrowSettingsConfig();
    public ThrowDebugSettings DebugSettings = new ThrowDebugSettings();
    public bool showBallOutline;
    public bool DebugMode;

    public ThrowConfig ThrowConfig => this.GetActiveThrowSettingsConfig().ThrowConfig;

    public AutoAimSettings AutoAimSettings => this.GetActiveThrowSettingsConfig().AutoAimSettings;

    public FlightSettings FlightSettings => this.GetActiveThrowSettingsConfig().FlightSettings;

    public TwoHandedSettings TwoHandedSettings => this.GetActiveThrowSettingsConfig().TwoHandedSettings;

    public HandPhysicsSettings HandPhysicsSettings => this.GetActiveThrowSettingsConfig().HandPhysicsSettings;

    public BallPhysicsSettings BallPhysicsSettings => this.GetActiveThrowSettingsConfig().BallPhysicsSettings;

    public InteractionSettings InteractionSettings => this.GetActiveThrowSettingsConfig().InteractionSettings;

    protected override void OnEnable()
    {
      this.DebugMode = false;
      base.OnEnable();
    }

    private ThrowSettingsConfig GetActiveThrowSettingsConfig() => ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value && this._noPassAssistConfig.ConfigEnabled ? this._noPassAssistConfig : this._highPassAssistConfig;
  }
}
