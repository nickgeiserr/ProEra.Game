// Decompiled with JetBrains decompiler
// Type: TrainingCampMovingObjectData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TrainingCampMovingTargetData", menuName = "TB12/Data/TrainingCampMovingTargetData", order = 1)]
public class TrainingCampMovingObjectData : ScriptableObject
{
  public TrainingCampMovingObjectData.MovementData[] MovementPattern;
  public TrainingCampMovingObjectData.LoopType PatternLoopType;

  public enum Direction
  {
    Left,
    Right,
    Forward,
    Backward,
    Up,
    Down,
  }

  public enum LoopType
  {
    Loop,
    PingPong,
  }

  [Serializable]
  public struct MovementData
  {
    public TrainingCampMovingObjectData.Direction direction;
    public int distance;
    public LeanTweenType easeType;
    public float time;
  }
}
