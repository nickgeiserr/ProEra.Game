// Decompiled with JetBrains decompiler
// Type: FootballVR.AttackerSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class AttackerSettings
  {
    public float StopTimeAfterLock = 2f;
    public float Acceleration = 5f;
    public float attackFinishedDistanceOffset = -0.2f;
    public float maxPredictionDelta = 2f;
    public float PrepareDistance = 2f;
    public float attackerLockDistance = 3f;
    public int PredictedFrameCount = 5;
  }
}
