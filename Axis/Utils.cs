// Decompiled with JetBrains decompiler
// Type: Axis.Utils
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Axis
{
  public static class Utils
  {
    public static Color ConvertToColor(string s)
    {
      s = s.Trim('\t', ' ');
      string[] strArray = s.Split(","[0], StringSplitOptions.None);
      return new Color((float) int.Parse(strArray[0]) / (float) byte.MaxValue, (float) int.Parse(strArray[1]) / (float) byte.MaxValue, (float) int.Parse(strArray[2]) / (float) byte.MaxValue);
    }

    public static string ConvertColorToRGB(Color c)
    {
      float num1 = (float) Mathf.RoundToInt(c.r * (float) byte.MaxValue);
      float num2 = (float) Mathf.RoundToInt(c.g * (float) byte.MaxValue);
      float num3 = (float) Mathf.RoundToInt(c.b * (float) byte.MaxValue);
      return num1.ToString() + ", " + num2.ToString() + ", " + num3.ToString();
    }

    public static string TrimString(string s) => s.Trim('\t', ' ', '\r', '\n', '"');

    public static string RemoveWhitespace(string s)
    {
      s = s.Replace("\t", "").Replace(" ", "").Replace("\r", "").Replace("\n", "");
      return s;
    }

    public static Dictionary<string, string> ReadProperties(string contents)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (string str in contents.Split('\n', StringSplitOptions.None))
      {
        string[] strArray = str.Split('=', StringSplitOptions.None);
        dictionary.Add(Utils.TrimString(strArray[0]), Utils.TrimString(strArray[1]));
      }
      return dictionary;
    }

    public static string[] SplitTextFile(string contents) => contents.Split("\n"[0], StringSplitOptions.None);

    public static string[] SplitAndTrimTextFile(string contents)
    {
      string[] strArray = contents.Split(new char[1]
      {
        "\n"[0]
      }, StringSplitOptions.RemoveEmptyEntries);
      for (int index = 0; index < strArray.Length; ++index)
        strArray[index] = Utils.TrimString(strArray[index]);
      return strArray;
    }
  }
}
