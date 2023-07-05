// Decompiled with JetBrains decompiler
// Type: UDB.ArrayExtensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  public static class ArrayExtensions
  {
    public static void Shuffle<T>(this T[] source)
    {
      int num = 0;
      for (int length = source.Length; num < length; ++num)
      {
        int index = UnityEngine.Random.Range(num, length);
        object obj1 = source.GetValue(num);
        object obj2 = source.GetValue(index);
        source.SetValue(obj2, num);
        source.SetValue(obj1, index);
      }
    }

    public static T[] RemoveAt<T>(this T[] source, int index)
    {
      T[] destinationArray = new T[source.Length - 1];
      if (index > 0)
        Array.Copy((Array) source, 0, (Array) destinationArray, 0, index);
      if (index < source.Length - 1)
        Array.Copy((Array) source, index + 1, (Array) destinationArray, index, source.Length - index - 1);
      return destinationArray;
    }

    public static void RemoveAt<T>(ref T[] source, int index)
    {
      T[] destinationArray = new T[source.Length - 1];
      if (index > 0)
        Array.Copy((Array) source, 0, (Array) destinationArray, 0, index);
      if (index < source.Length - 1)
        Array.Copy((Array) source, index + 1, (Array) destinationArray, index, source.Length - index - 1);
      source = destinationArray;
    }

    public static void InsertAfter<T>(ref T[] source, int index, T val)
    {
      if (index == source.Length - 1)
      {
        Array.Resize<T>(ref source, source.Length + 1);
        source[source.Length - 1] = val;
      }
      else
      {
        T[] destinationArray = new T[source.Length + 1];
        Array.Copy((Array) source, (Array) destinationArray, index + 1);
        destinationArray[index + 1] = val;
        Array.Copy((Array) source, index + 1, (Array) destinationArray, index + 2, source.Length - (index + 1));
        source = destinationArray;
      }
    }

    public static T PickRandom<T>(this T[] source)
    {
      if (source == null || source.Length == 0)
        return default (T);
      int index = UnityEngine.Random.Range(0, source.Length);
      return source[index];
    }

    public static bool InRange<T>(this T[] source, int index) => source != null && index >= 0 && index < source.Length;
  }
}
