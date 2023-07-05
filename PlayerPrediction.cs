// Decompiled with JetBrains decompiler
// Type: PlayerPrediction
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public static class PlayerPrediction
{
  public static Vector3 PredictPosition(PlayerAI player, float timeInSeconds)
  {
    MotionTracker motionTracker = player.motionTracker;
    return (Object) motionTracker == (Object) null ? player.transform.position : player.transform.position + motionTracker.Velocity * timeInSeconds;
  }
}
