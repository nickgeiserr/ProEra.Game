﻿// Decompiled with JetBrains decompiler
// Type: PEPlayOverEvent
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PEPlayOverEvent : PEGameplayEvent
{
  public PlayEndType Type;
  public PlayerAI BallHolder;

  public PEPlayOverEvent(float time, Vector3 pos, PlayEndType type, PlayerAI ballHolder)
  {
    this.GameTime = time;
    this.BallPosition = pos;
    this.Type = type;
  }
}
