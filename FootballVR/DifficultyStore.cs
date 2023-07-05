// Decompiled with JetBrains decompiler
// Type: FootballVR.DifficultyStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Stores/Difficulty Store")]
  [AppStore]
  public class DifficultyStore : ScriptableSettings
  {
    public VariableFloat GameSpeed = new VariableFloat(0.8f);
    public VariableInt AutoAimPasses = new VariableInt(10);
    public VariableBool UnderCenterAutoDropBackPass = new VariableBool(true);
    public VariableBool UnderCenterAutoDropBackRun = new VariableBool(true);
    public VariableBool UnderCenterBulletTimePass = new VariableBool(true);
    public VariableBool UnderCenterBulletTimeRun = new VariableBool(true);
    public VariableBool ShotgunBulletTimePass = new VariableBool(true);
    public VariableBool ShotgunBulletTimeRun = new VariableBool(true);
    public VariableBool OpponentCanIntercept = new VariableBool(false);
    public VariableBool OpponentCanBlitz = new VariableBool(false);
    public VariableBool DelayOfGame = new VariableBool(false);
  }
}
