// Decompiled with JetBrains decompiler
// Type: UDB.TimerExtensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  public static class TimerExtensions
  {
    public static void AttachTimer(
      this MonoBehaviour behaviour,
      float duration,
      System.Action onComplete,
      Action<float> onUpdate = null,
      bool isLooped = false,
      bool useRealTime = false)
    {
      Timer.Register(duration, onComplete, onUpdate, isLooped, useRealTime, behaviour);
    }
  }
}
