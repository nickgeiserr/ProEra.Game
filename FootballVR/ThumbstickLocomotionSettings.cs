// Decompiled with JetBrains decompiler
// Type: FootballVR.ThumbstickLocomotionSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class ThumbstickLocomotionSettings
  {
    public float acceleration = 5.2f;
    public float decelerationRate = 17f;
    public float decelerationConstant = 66f;
    public float maxSpeed = 5f;
    public float buttonPressThreshold = 0.02f;
    public float sprintAccelerationMultiplier = 2f;
    public float autoDropBackSpeed = 5f;
  }
}
