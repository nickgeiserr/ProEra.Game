// Decompiled with JetBrains decompiler
// Type: FootballVR.ThrowSettingsConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [SettingsConfig]
  [Serializable]
  public class ThrowSettingsConfig
  {
    public ThrowConfig ThrowConfig = new ThrowConfig();
    public AutoAimSettings AutoAimSettings = new AutoAimSettings();
    public FlightSettings FlightSettings = new FlightSettings();
    public TwoHandedSettings TwoHandedSettings = new TwoHandedSettings();
    public HandPhysicsSettings HandPhysicsSettings = new HandPhysicsSettings();
    public BallPhysicsSettings BallPhysicsSettings = new BallPhysicsSettings();
    public InteractionSettings InteractionSettings = new InteractionSettings();
    public bool ConfigEnabled = true;
  }
}
