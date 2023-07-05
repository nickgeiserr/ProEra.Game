// Decompiled with JetBrains decompiler
// Type: FootballVR.ThrowData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  public class ThrowData
  {
    public BallObject ball;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public Vector3 throwVector;
    public Vector3 autoAimedVector;
    public float timeStep;
    public float flightTime;
    public float maxDistance;
    public float timeToGetToTarget = -1f;
    public float accuracy;
    public IThrowTarget closestTarget;
    public bool hasTarget;

    public bool ValidFlightime() => (double) this.flightTime >= 0.0 && !float.IsNaN(this.flightTime) && (double) this.flightTime <= 30.0;
  }
}
