// Decompiled with JetBrains decompiler
// Type: FootballVR.Vector2Extension
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FootballVR
{
  public static class Vector2Extension
  {
    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
      float num1 = Mathf.Sin(degrees * ((float) Math.PI / 180f));
      float num2 = Mathf.Cos(degrees * ((float) Math.PI / 180f));
      float x = v.x;
      float y = v.y;
      v.x = (float) ((double) num2 * (double) x - (double) num1 * (double) y);
      v.y = (float) ((double) num1 * (double) x + (double) num2 * (double) y);
      return v;
    }
  }
}
