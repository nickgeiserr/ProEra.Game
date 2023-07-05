// Decompiled with JetBrains decompiler
// Type: PETackleEvent
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PETackleEvent : PEGameplayEvent
{
  public PlayerAI Tackler;
  public PlayerAI BallHolder;
  public bool UserTackled;

  public PETackleEvent(
    float time,
    Vector3 pos,
    PlayerAI tack,
    PlayerAI ballHolder,
    bool userTackled)
  {
    this.GameTime = time;
    this.BallPosition = pos;
    this.Tackler = tack;
    this.BallHolder = ballHolder;
    this.UserTackled = userTackled;
  }
}
