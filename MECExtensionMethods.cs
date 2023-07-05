// Decompiled with JetBrains decompiler
// Type: MECExtensionMethods
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MovementEffects;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class MECExtensionMethods
{
  public static IEnumerator<float> Delay(this IEnumerator<float> coroutine, float timeToDelay)
  {
    yield return Timing.WaitForSeconds(timeToDelay);
    while (coroutine.MoveNext())
      yield return coroutine.Current;
  }

  public static IEnumerator<float> Delay(this IEnumerator<float> coroutine, Func<bool> condition)
  {
    while (!condition())
      yield return 0.0f;
    while (coroutine.MoveNext())
      yield return coroutine.Current;
  }

  public static IEnumerator<float> Delay<T>(
    this IEnumerator<float> coroutine,
    T data,
    Func<T, bool> condition)
  {
    while (!condition(data))
      yield return 0.0f;
    while (coroutine.MoveNext())
      yield return coroutine.Current;
  }

  public static IEnumerator<float> CancelWith(
    this IEnumerator<float> coroutine,
    GameObject gameObject)
  {
    while ((bool) (UnityEngine.Object) gameObject && gameObject.activeInHierarchy && coroutine.MoveNext())
      yield return coroutine.Current;
  }

  public static IEnumerator<float> CancelWith(
    this IEnumerator<float> coroutine,
    GameObject gameObject1,
    GameObject gameObject2)
  {
    while ((bool) (UnityEngine.Object) gameObject1 && gameObject1.activeInHierarchy && (bool) (UnityEngine.Object) gameObject2 && gameObject2.activeInHierarchy && coroutine.MoveNext())
      yield return coroutine.Current;
  }

  public static IEnumerator<float> CancelWith(
    this IEnumerator<float> coroutine,
    GameObject gameObject1,
    GameObject gameObject2,
    GameObject gameObject3)
  {
    while ((bool) (UnityEngine.Object) gameObject1 && gameObject1.activeInHierarchy && (bool) (UnityEngine.Object) gameObject2 && gameObject2.activeInHierarchy && (bool) (UnityEngine.Object) gameObject3 && gameObject3.activeInHierarchy && coroutine.MoveNext())
      yield return coroutine.Current;
  }

  public static IEnumerator<float> CancelWith(
    this IEnumerator<float> coroutine,
    Func<bool> condition)
  {
    if (condition != null)
    {
      while (condition() && coroutine.MoveNext())
        yield return coroutine.Current;
    }
  }

  public static IEnumerator<float> Append(
    this IEnumerator<float> coroutine,
    IEnumerator<float> nextCoroutine)
  {
    while (coroutine.MoveNext())
      yield return coroutine.Current;
    if (nextCoroutine != null)
    {
      while (nextCoroutine.MoveNext())
        yield return nextCoroutine.Current;
    }
  }

  public static IEnumerator<float> Append(this IEnumerator<float> coroutine, System.Action onDone)
  {
    while (coroutine.MoveNext())
      yield return coroutine.Current;
    if (onDone != null)
      onDone();
  }

  public static IEnumerator<float> Prepend(
    this IEnumerator<float> coroutine,
    IEnumerator<float> lastCoroutine)
  {
    if (lastCoroutine != null)
    {
      while (lastCoroutine.MoveNext())
        yield return lastCoroutine.Current;
    }
    while (coroutine.MoveNext())
      yield return coroutine.Current;
  }

  public static IEnumerator<float> Prepend(this IEnumerator<float> coroutine, System.Action onStart)
  {
    if (onStart != null)
      onStart();
    while (coroutine.MoveNext())
      yield return coroutine.Current;
  }

  public static IEnumerator<float> Superimpose(
    this IEnumerator<float> coroutineA,
    IEnumerator<float> coroutineB)
  {
    return coroutineA.Superimpose(coroutineB, Timing.Instance);
  }

  public static IEnumerator<float> Superimpose(
    this IEnumerator<float> coroutineA,
    IEnumerator<float> coroutineB,
    Timing instance)
  {
    while (coroutineA != null || coroutineB != null)
    {
      if (coroutineA != null && instance.localTime >= (double) coroutineA.Current && !coroutineA.MoveNext())
        coroutineA = (IEnumerator<float>) null;
      if (coroutineB != null && instance.localTime >= (double) coroutineB.Current && !coroutineB.MoveNext())
        coroutineB = (IEnumerator<float>) null;
      if (coroutineA != null && float.IsNaN(coroutineA.Current) || coroutineB != null && float.IsNaN(coroutineB.Current))
        yield return float.NaN;
      else if (coroutineA != null && coroutineB != null)
        yield return (double) coroutineA.Current < (double) coroutineB.Current ? coroutineA.Current : coroutineB.Current;
      else if (coroutineA == null && coroutineB != null)
        yield return coroutineB.Current;
      else if (coroutineA != null)
        yield return coroutineA.Current;
    }
  }

  public static IEnumerator<float> Hijack(
    this IEnumerator<float> coroutine,
    Func<float, float> newReturn)
  {
    if (newReturn != null)
    {
      while (coroutine.MoveNext())
        yield return newReturn(coroutine.Current);
    }
  }
}
