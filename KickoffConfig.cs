// Decompiled with JetBrains decompiler
// Type: KickoffConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class KickoffConfig : MonoBehaviour, IGameplayConfig
{
  [Header("Onside Kicks")]
  [Tooltip("The minimum X value for the onside kick's destination position")]
  [SerializeField]
  private float _minXYardsForOnsideKick;
  [Tooltip("The maximum X value for the onside kick's destination position")]
  [SerializeField]
  private float _maxXYardsForOnsideKick = 10f;
  [Tooltip("The minimum Z value for the onside kick's destination position")]
  [SerializeField]
  private float _minZYardsForOnsideKick = 10f;
  [Tooltip("The maximum Z value for the onside kick's destination position")]
  [SerializeField]
  private float _maxZYardsForOnsideKick = 15f;
  [Tooltip("Chance (from 0 to 1) that the first player to try to catch an onside kick will drop/deflect it")]
  [SerializeField]
  private float _initialDropChanceForOnsideKick = 0.5f;
  [Tooltip("Minimum velocity that the ball will have after an initial onside kick deflection.")]
  [SerializeField]
  private Vector3 _minYardsPerSecondOnsideKickDeflection = new Vector3(-8f, -8f, -8f);
  [Tooltip("Maximum velocity that the ball will have after an initial onside kick deflection.")]
  [SerializeField]
  private Vector3 _maxYardsPerSecondOnsideKickDeflection = new Vector3(3f, 8f, 3f);

  public float GetOnsideKickLandingSpotX() => Random.Range(this._minXYardsForOnsideKick, this._maxXYardsForOnsideKick);

  public float GetOnsideKickLandingSpotZ() => Random.Range(this._minZYardsForOnsideKick, this._maxZYardsForOnsideKick);

  public float InitialDropChanceForOnsideKick => this._initialDropChanceForOnsideKick;

  public Vector3 GetRandomOnsideKickDeflectionVelocity() => Field.FlipVectorByFieldDirection(Field.ONE_YARD * MathUtils.GetRandomVector3InBox(this._minYardsPerSecondOnsideKickDeflection, this._maxYardsPerSecondOnsideKickDeflection));
}
