// Decompiled with JetBrains decompiler
// Type: HSVUtil
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public static class HSVUtil
{
  public static HsvColor ConvertRgbToHsv(Color color) => HSVUtil.ConvertRgbToHsv((double) (int) ((double) color.r * (double) byte.MaxValue), (double) (int) ((double) color.g * (double) byte.MaxValue), (double) (int) ((double) color.b * (double) byte.MaxValue));

  public static HsvColor ConvertRgbToHsv(double r, double b, double g)
  {
    double num1 = 0.0;
    double num2 = Math.Min(Math.Min(r, g), b);
    double num3 = Math.Max(Math.Max(r, g), b);
    double num4 = num3 - num2;
    double num5 = num3 != 0.0 ? num4 / num3 : 0.0;
    double num6;
    if (num5 == 0.0)
    {
      num6 = 360.0;
    }
    else
    {
      if (r == num3)
        num1 = (g - b) / num4;
      else if (g == num3)
        num1 = 2.0 + (b - r) / num4;
      else if (b == num3)
        num1 = 4.0 + (r - g) / num4;
      num6 = num1 * 60.0;
      if (num6 <= 0.0)
        num6 += 360.0;
    }
    return new HsvColor()
    {
      H = 360.0 - num6,
      S = num5,
      V = num3 / (double) byte.MaxValue
    };
  }

  public static Color ConvertHsvToRgb(double h, double s, double v, float alpha)
  {
    double r;
    double g;
    double b;
    if (s == 0.0)
    {
      r = v;
      g = v;
      b = v;
    }
    else
    {
      if (h == 360.0)
        h = 0.0;
      else
        h /= 60.0;
      int num1 = (int) h;
      double num2 = h - (double) num1;
      double num3 = v * (1.0 - s);
      double num4 = v * (1.0 - s * num2);
      double num5 = v * (1.0 - s * (1.0 - num2));
      switch (num1)
      {
        case 0:
          r = v;
          g = num5;
          b = num3;
          break;
        case 1:
          r = num4;
          g = v;
          b = num3;
          break;
        case 2:
          r = num3;
          g = v;
          b = num5;
          break;
        case 3:
          r = num3;
          g = num4;
          b = v;
          break;
        case 4:
          r = num5;
          g = num3;
          b = v;
          break;
        default:
          r = v;
          g = num3;
          b = num4;
          break;
      }
    }
    return new Color((float) r, (float) g, (float) b, alpha);
  }
}
