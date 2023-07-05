// Decompiled with JetBrains decompiler
// Type: UDB.Math
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace UDB
{
  public class Math
  {
    public static bool NearlyEqual(double a, double b, double epsilon = 9.9999999747524271E-07)
    {
      double num1 = System.Math.Abs(a);
      double num2 = System.Math.Abs(b);
      double num3 = System.Math.Abs(a - b);
      if (a == b)
        return true;
      return a == 0.0 || b == 0.0 || num3 < double.MinValue ? num3 < epsilon * double.MinValue : num3 / (num1 + num2) < epsilon;
    }
  }
}
