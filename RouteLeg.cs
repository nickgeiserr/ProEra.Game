// Decompiled with JetBrains decompiler
// Type: RouteLeg
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public struct RouteLeg
{
  public Vector3 moveVector;
  public float speedPercent;

  public RouteLeg(float x, float z, float speed)
  {
    this.moveVector.x = x;
    this.moveVector.y = 0.0f;
    this.moveVector.z = z;
    this.speedPercent = Mathf.Clamp(speed * 0.1f, 0.0f, 1f);
  }
}
