// Decompiled with JetBrains decompiler
// Type: UDB.IntVectorArrayExtensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  internal static class IntVectorArrayExtensions
  {
    public static T GetValue<T>(this T[,] array, Vector2i index)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[,]>(array);
      return array[index.x, index.y];
    }

    public static void SetValue<T>(this T[,] array, T newValue, Vector2i index)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[,]>(array);
      array[index.x, index.y] = newValue;
    }

    public static Vector2i GetSize<T>(this T[,] array)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[,]>(array);
      return new Vector2i(array.GetLength(0), array.GetLength(1));
    }

    public static T GetValue<T>(this T[][] array, Vector2i index)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[][]>(array);
      return array[index.x][index.y];
    }

    public static void SetValue<T>(this T[][] array, T newValue, Vector2i index)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[][]>(array);
      array[index.x][index.y] = newValue;
    }

    public static T GetValue<T>(this T[,,] array, Vector3i index)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[,,]>(array);
      return array[index.x, index.y, index.z];
    }

    public static void SetValue<T>(this T[,,] array, T newValue, Vector3i index)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[,,]>(array);
      array[index.x, index.y, index.z] = newValue;
    }

    public static Vector3i GetSize<T>(this T[,,] array)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[,,]>(array);
      return new Vector3i(array.GetLength(0), array.GetLength(1), array.GetLength(2));
    }

    public static T GetValue<T>(this T[][][] array, Vector3i index)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[][][]>(array);
      return array[index.x][index.y][index.z];
    }

    public static void SetValue<T>(this T[][][] array, T newValue, Vector3i index)
    {
      IntVectorArrayExtensions.CheckArrayArgumentNotNull<T[][][]>(array);
      array[index.x][index.y][index.z] = newValue;
    }

    private static void CheckArrayArgumentNotNull<T>(T array)
    {
      if ((object) array == null)
        throw new ArgumentNullException(nameof (array));
    }
  }
}
