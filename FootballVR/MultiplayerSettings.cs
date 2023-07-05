// Decompiled with JetBrains decompiler
// Type: FootballVR.MultiplayerSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "MultiplayerSettings", menuName = "TB12/Settings/MultiplayerSettings", order = 1)]
  [SettingsConfig]
  public class MultiplayerSettings : SingletonScriptableSettings<MultiplayerSettings>
  {
    public VariableFloat VoiceBoostFactor = new VariableFloat(-1f);
    public float DynamicTargetOffset = 20f;
    public bool ShowThrowPredictions;
    public VariableInt SendRate = new VariableInt(30);
    public VariableInt SerializationRate = new VariableInt(15);
    public BallFlightCompensationSettings FlightCompensationSettings;
    public BallFlightCompensationSettings HeightCompensationSettings;
    public float BallPositionSmoothFactor = 0.5f;
    public VariableBool ForceBallSyncEnabled = new VariableBool(false);
    public float SnapBallSyncFactor = 0.9f;
  }
}
