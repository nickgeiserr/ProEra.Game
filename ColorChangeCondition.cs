// Decompiled with JetBrains decompiler
// Type: ColorChangeCondition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public class ColorChangeCondition
{
  public readonly Color32 Color;
  private readonly Predicate<Color32> m_Condition;

  public bool Match(Color32 pixel) => this.m_Condition(pixel);

  public ColorChangeCondition(Predicate<Color32> filter, Color32 color)
  {
    this.Color = color;
    this.m_Condition = filter;
  }
}
