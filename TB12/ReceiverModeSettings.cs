// Decompiled with JetBrains decompiler
// Type: TB12.ReceiverModeSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;
using Vars;

namespace TB12
{
  [CreateAssetMenu(fileName = "ReceiverModeSettings", menuName = "TB12/Settings/ReceiverModeSettings", order = 1)]
  [SettingsConfig]
  public class ReceiverModeSettings : SingletonScriptableSettings<ReceiverModeSettings>
  {
    public float throwMaxTime = 5f;
    public float completionOffset = 0.7f;
    public float ballFlightTime = 1.3f;
    public float progressThreshold = 0.8f;
    public float throwProjectionCoef = 0.5f;
    public float routeWidth = 1.3f;
    public float catchHeight = 1.65f;
    public float warningDelay = 0.2f;
    public bool useRouteTipAsProjectedPosition = true;
    public bool showPredictionTrail = true;
    public VariableFloat TrailWidthMultiplier = new VariableFloat(0.1f);
  }
}
