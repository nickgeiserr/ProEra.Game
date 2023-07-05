// Decompiled with JetBrains decompiler
// Type: ZoneCoverageConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class ZoneCoverageConfig : MonoBehaviour, IGameplayConfig
{
  [Tooltip("If no one is in their zone, players will move toward their home positions if they're farther away than this.")]
  [SerializeField]
  private float MaxYardsFromHomePosition = 2f;
  [Tooltip("The defender will try to cover a receiver by getting this many yards downfield of him.")]
  [SerializeField]
  private float DownfieldYardsFromTargetPosition = 3f;
  [Tooltip("The defender will try to shade this many yards to the side of the receiver he's targeting.")]
  [SerializeField]
  private float XOffsetFromReceiverInYards = 0.5f;
  [Tooltip("A deep zone defender will always try to position himself at least this far from the back of the end zone.")]
  [SerializeField]
  private float MinYardsFromBackOfEndZoneDeep = 5f;
  [Tooltip("A short zone defender will always try to position himself at least this far from the back of the end zone.")]
  [SerializeField]
  private float MinYardsFromBackOfEndZoneShort = 10f;
  [Tooltip("When predicting the receiver's future position, the defender will look this many seconds into the future.")]
  public float ReceiverPredictionTimeInSeconds = 0.5f;
  [Tooltip("The frequency at which zone coverage AI will update.")]
  public float UpdateIntervalSeconds = 0.24f;

  public float MinDistanceFromBackOfEndzoneDeep => Field.ConvertYardsToDistance(this.MinYardsFromBackOfEndZoneDeep);

  public float MostDownfieldCoveragePositionDeep => Field.OFFENSIVE_BACK_OF_ENDZONE - this.MinDistanceFromBackOfEndzoneDeep * (float) Game.OffensiveFieldDirection;

  public float MinDistanceFromBackOfEndzoneShort => Field.ConvertYardsToDistance(this.MinYardsFromBackOfEndZoneShort);

  public float MostDownfieldCoveragePositionShort => Field.OFFENSIVE_BACK_OF_ENDZONE - this.MinDistanceFromBackOfEndzoneShort * (float) Game.OffensiveFieldDirection;

  public float DownfieldDistanceFromTargetPosition => Field.ConvertYardsToDistance(this.DownfieldYardsFromTargetPosition);

  public float MaxDistanceFromHomePosition => Field.ConvertYardsToDistance(this.MaxYardsFromHomePosition);

  public float XOffsetFromReceiver => Field.ConvertYardsToDistance(this.XOffsetFromReceiverInYards);
}
