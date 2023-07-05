// Decompiled with JetBrains decompiler
// Type: StateFaceMovementDirectionConfiguration
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

[CreateAssetMenu(fileName = "StateFaceMovementDirectionConfiguration", menuName = "State Configurations/Face Movement Direction")]
public class StateFaceMovementDirectionConfiguration : ScriptableObject
{
  [SerializeField]
  private float _timeAhead = 0.1f;
  [SerializeField]
  private float _distanceThreshold = 0.2f;

  public float TimeAhead => this._timeAhead;

  public float DistanceThreshold => this._distanceThreshold;
}
