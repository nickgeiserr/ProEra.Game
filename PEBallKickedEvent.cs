// Decompiled with JetBrains decompiler
// Type: PEBallKickedEvent
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PEBallKickedEvent : PEGameplayEvent
{
  public PlayerAI Kicker;
  public PlayerAI Center;
  public PlayerAI Holder;
  public bool IsPAT;

  public PEBallKickedEvent(
    float time,
    Vector3 pos,
    PlayerAI kicker,
    PlayerAI center = null,
    PlayerAI holder = null)
  {
    this.GameTime = time;
    this.BallPosition = pos;
    this.Kicker = kicker;
    this.Center = center;
    this.Holder = holder;
    this.IsPAT = (bool) ProEra.Game.MatchState.RunningPat;
  }
}
