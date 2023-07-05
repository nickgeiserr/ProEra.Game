// Decompiled with JetBrains decompiler
// Type: UDB.StringExtensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  public static class StringExtensions
  {
    public static string WithTimestamp(this string source) => string.Format("[{0:000.000}] {1}", (object) Time.realtimeSinceStartup, (object) source);

    public static int ToInt(this string source)
    {
      int result = 0;
      int.TryParse(source, out result);
      return result;
    }

    public static bool IsNullOrEmpty(this string source) => source == null || source.Length == 0;

    public static bool IsEmptyOrWhiteSpaceOrNull(this string source)
    {
      switch (source)
      {
        case null:
          return true;
        case "":
          return true;
        default:
          foreach (char c in source.ToCharArray())
          {
            if (!char.IsWhiteSpace(c))
              return false;
          }
          return true;
      }
    }

    public static void RemoveCharactersInString(this string source, string removeStr)
    {
      if (removeStr == null)
        return;
      foreach (char ch in removeStr.ToCharArray())
        source = source.Replace(ch.ToString(), "");
    }

    public static void RemoveWhitespace(this string source) => source.RemoveCharactersInString(" \r\n\t");

    public static bool IsEqual(this string source, string str) => source.Equals(str, StringComparison.Ordinal);

    public static void UppercaseFirst(this string source)
    {
      if (string.IsNullOrEmpty(source))
        return;
      source = char.ToUpper(source[0]).ToString() + source.Substring(1);
    }

    public static bool EndsWith(this string source, string suffix)
    {
      int index1 = source.Length - 1;
      int index2;
      for (index2 = suffix.Length - 1; index1 >= 0 && index2 >= 0 && (int) source[index1] == (int) suffix[index2]; --index2)
        --index1;
      if (index2 < 0 && source.Length >= suffix.Length)
        return true;
      return index1 < 0 && suffix.Length >= source.Length;
    }

    public static bool StartsWith(this string source, string prefix)
    {
      int length1 = source.Length;
      int length2 = prefix.Length;
      int index1 = 0;
      int index2;
      for (index2 = 0; index1 < length1 && index2 < length2 && (int) source[index1] == (int) prefix[index2]; ++index2)
        ++index1;
      if (index2 == length2 && length1 >= length2)
        return true;
      return index1 == length1 && length2 >= length1;
    }
  }
}
