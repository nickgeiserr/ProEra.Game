﻿// Decompiled with JetBrains decompiler
// Type: PEBallThrownEvent
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PEBallThrownEvent : PEGameplayEvent
{
  public bool IsUser;
  public PlayerAI Thrower;
  public PlayerAI IntendedReceiver;

  public PEBallThrownEvent(float time, Vector3 pos, PlayerAI thrower, PlayerAI rec, bool user)
  {
    this.GameTime = time;
    this.BallPosition = pos;
    this.IntendedReceiver = rec;
    this.Thrower = thrower;
    this.IsUser = user;
  }
}
