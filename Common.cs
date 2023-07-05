// Decompiled with JetBrains decompiler
// Type: Common
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class Common
{
  public static Color SP_GREEN = new Color(0.0196f, 0.941f, 0.459f);
  public static Color SP_WHITE = new Color(0.784f, 0.827f, 0.812f);

  public static string EnumToString(string e) => e.Replace("_", " ").ToUpper();

  public static string GetOrdinalNumberFromInt(int number)
  {
    int num = number % 10;
    if (number > 10 && number < 20)
      return number.ToString() + "TH";
    switch (num)
    {
      case 1:
        return number.ToString() + "ST";
      case 2:
        return number.ToString() + "ND";
      case 3:
        return number.ToString() + "RD";
      default:
        return number.ToString() + "TH";
    }
  }
}
