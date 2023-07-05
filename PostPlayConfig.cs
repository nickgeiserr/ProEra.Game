// Decompiled with JetBrains decompiler
// Type: PostPlayConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PostPlayConfig : MonoBehaviour, IGameplayConfig
{
  [Tooltip("If a player is too far away, don't consider for avoidance.")]
  [SerializeField]
  private float maxFeetFromPlayerForAvoidance = 4f;
  [Tooltip("If close enough to target, stop.")]
  [SerializeField]
  private float maxFeetFromTargetToStop = 3f;
  [Tooltip("If a player is too close to my peripheral, don't consider for avoidance.")]
  [Range(0.0f, 90f)]
  [SerializeField]
  private float maxDegreesFromMovingVectorForAvoidance = 75f;
  [Tooltip("Sideline out of bounds target.")]
  public float minYardsFromOOBTargetToStop = -0.5f;
  public float maxYardsFromOOBTargetToStop = 2f;
  [Tooltip("Players will run the specified number of yards into the sideline.")]
  public float DefaultOobYardsFromSideline = 3f;
  [Tooltip("How far from target for not walking.")]
  [SerializeField]
  private float MaxYardsFromTargetToWalk = 7f;

  public float maxDistanceFromPlayerForAvoidance => Field.ONE_FOOT * this.maxFeetFromPlayerForAvoidance;

  public float maxTargetDistanceToStop => Field.ONE_FOOT * this.maxFeetFromTargetToStop;

  public float forwardVecToPlayerVecDotThreshold => (float) (1.0 - (double) this.maxDegreesFromMovingVectorForAvoidance / 90.0);

  public float MaxDistanceFromTargetToWalk => this.MaxYardsFromTargetToWalk * Field.ONE_YARD;
}
