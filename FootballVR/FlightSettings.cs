// Decompiled with JetBrains decompiler
// Type: FootballVR.FlightSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class FlightSettings
  {
    public float BallRotationsPerSecond = 5f;
    public float BallRotationWobbles = 1.2f;
    public float BallRotationWobblesMin = 0.7f;
    public float BallDirectionLerpFactor = 0.5f;
    public float BallSpinSpeedFactor = 0.5f;
    public float BallStabilizer = 1.5f;
    public const float maxRealisticThrowSpeed = 23f;
    public float HikeFlightTime = 0.45f;
  }
}
