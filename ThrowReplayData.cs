// Decompiled with JetBrains decompiler
// Type: ThrowReplayData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

[Serializable]
public class ThrowReplayData
{
  public long timestamp;
  public bool successfulThrow;
  public Vector3 throwVector;
  public Vector3 startPosition;
  public float flightTime;
  public int targetIndex;
  public Vector3 targetPosition;

  public void PrintData() => Debug.LogError((object) JsonUtility.ToJson((object) this));
}
