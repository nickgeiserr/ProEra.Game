// Decompiled with JetBrains decompiler
// Type: TrainingCampDebugger
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12.GameplayData;
using UnityEngine;

public class TrainingCampDebugger : MonoBehaviour
{
  public TrainingCampLevels TrainingCampLevels;
  [HideInInspector]
  public TrainingCampDebugger.TrainingCampGameMode GameMode;
  [HideInInspector]
  public TrainingCampDebugger.TrainingCampLevel Level;
  [HideInInspector]
  public GameObject EditorLevel;

  private void Start()
  {
  }

  private void Update()
  {
  }

  public enum TrainingCampGameMode
  {
    StationaryTargets,
    ThreadTheNeedle,
    MovingTargets,
    Scramble,
    PocketPresence,
  }

  public enum TrainingCampLevel
  {
    L1,
    L2,
    L3,
    L4,
    L5,
  }
}
