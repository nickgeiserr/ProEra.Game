// Decompiled with JetBrains decompiler
// Type: FootballVR.IBallThrower
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FootballVR
{
  public interface IBallThrower
  {
    Vector3 position { get; set; }

    bool isReady { get; }

    void ThrowToSpot(Vector3 targetPos, float flightTime, float throwDelay);

    event Action<Transform, Vector3, float> OnBallThrown;

    void Initialize(Vector3 targetPos, bool autoSpawnBall);

    Transform transform { get; }

    void SetHighlight(bool state);
  }
}
