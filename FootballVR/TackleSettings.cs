// Decompiled with JetBrains decompiler
// Type: FootballVR.TackleSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace FootballVR
{
  [Serializable]
  public class TackleSettings
  {
    public float pushThreshold = 0.6f;
    public float tackleThreshold = 0.45f;
    public float handDirectionDotThreshold = 0.4f;
    public float opponentMinDirDot = -1f;
    public float minProjectedArmsVector = 0.3f;
    public float minDistance;
    public float maxDistance = 0.5f;
    public float tackleMaxDistance = 0.6f;
    public float pushAttackerForwardDuration = 0.25f;
    public float pushAttackerForwardPower = 0.02f;
    public float pushImpactIntensity = 1f;
    public float pushIntensity = 3f;
  }
}
