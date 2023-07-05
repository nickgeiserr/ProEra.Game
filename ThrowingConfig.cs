// Decompiled with JetBrains decompiler
// Type: ThrowingConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public class ThrowingConfig : MonoBehaviour, IGameplayConfig
{
  [Header("Trajectory")]
  [Tooltip("The fastest a player can throw the ball (in miles per hour).")]
  [SerializeField]
  private float MaxBallSpeedMPH = 48f;
  [Tooltip("The fastest a player can throw the ball for short routes(in miles per hour).")]
  [SerializeField]
  private float MaxShortBallSpeedMPH = 40f;
  [Tooltip("The dist to target to use the short pass max speed).")]
  [SerializeField]
  public float ShortPassDist = 20f;
  [Tooltip("The auto aim code will assume we want a target height between Min Target Height and Max Target Height.")]
  public float MinTargetHeight = 0.7f;
  [Tooltip("The auto aim code will assume we want a target height between Min Target Height and Max Target Height.")]
  public float MaxTargetHeight = 2.3f;
  [Tooltip("When an auto aim candidate is too short, scale the power by this much for each iteration that was too short.")]
  public float PowerIncreasePerDistanceFailure = 0.05f;
  [Tooltip("When an auto aim candidate is too fast, divide the power by this much.")]
  public float PowerDecreaseOnSpeedFailure = 1.15f;
  [Tooltip("When an auto aim candidate is too fast, add this to the Y Velocity.")]
  public float YVelocityIncreaseOnSpeedFailure = 0.025f;
  [Header("Accuracy")]
  [Tooltip("If the receiver is within this many degrees of facing the passer, scale the distance adjustment.")]
  public float MaxDegreesReceiverFacingToPasserForScaling = 41.4f;
  [Tooltip("If the receiver is facing the passer, scale the distance adjustment by this much.")]
  public float LeadingDistanceAdjustReceiverFacingPasser = 0.35f;
  [Header("AlphaThrowing")]
  [Tooltip("If the receiver is closer to the passer than this, we will use different criteria to see whether a potential passing target location is valid.")]
  [SerializeField]
  private float MaxYardsForShortPassAlpha = 20f;
  [Tooltip("For a short pass, don't consider a potential location valid unless the receiver is within this many degrees of facing the target location.")]
  public float MaxDegreesFromReceiverFacingToBallShortAlpha = 12.84f;
  [Tooltip("For a non-short pass, don't consider a potential location valid unless the receiver is within this many degrees of facing the target location.")]
  public float MaxDegreesFromReceiverFacingToBallAlpha = 41.4f;
  [Tooltip("If the receiver is within this many degrees of facing the target location, consider it to have \"come near\" the receiver, so the receiver is a potential target.")]
  public float MaxDegreesFromReceiverFacingToBallNearAlpha = 60f;
  [Tooltip("Lead the receiver this far toward the target location, in addition to other leading that is done")]
  public float AlphaLeadingBaseMultipler = 5f;
  [Tooltip("Lead the receiver extra if he's running away from or toward the passer.  (Distance * Dot product of vector from QB and vector to ball * this)")]
  public float AlphaLeadingDotMultiplier = 0.05f;
  [Tooltip("For every meter between the passer and the receiver, lead this much in the direction the receiver is running.")]
  public float AlphaLeadingReceiverDistMultiplier;
  [Tooltip("Multiply the total leading by this number * time the ball will be in the air.")]
  public float AlphaLeadingPassTypeMultiplier = 1.3f;
  [Tooltip("Once the launch velocity is calculated, add a random adjustment to each component of the velocity in the range (-min,max)")]
  public Vector3 MinRandomLaunchVelocityAdjust = Vector3.zero;
  [Tooltip("Once the launch velocity is calculated, add a random adjustment to each component of the velocity in the range (-min,max)")]
  public Vector3 MaxRandomLaunchVelocityAdjust = Vector3.zero;
  [Header("Non-Alpha Throwing")]
  [Tooltip("Lead the receiver this far toward the target location, in addition to other leading that is done")]
  public float LeadingBaseMultipler = 9f;
  [Tooltip("Lead the receiver extra if he's running away from or toward the passer.  (Distance * Dot product of vector from QB and vector to ball * this)")]
  public float LeadingDotMultiplier = 0.1f;
  [Tooltip("For every meter between the passer and the receiver, lead this much in the direction the receiver is running.")]
  public float LeadingReceiverDistMultiplier = 0.1f;
  [Header("AI Quarterback")]
  [Tooltip("A QB with this accuracy or lower will have the highest possible variance")]
  public int AccuracyRatingForMaxAIAccuracyVariance = 70;
  [Tooltip("A QB with this accuracy or lower will have the lowest possible variance")]
  public int AccuracyRatingForMinAIAccuracyVariance = 99;
  [Tooltip("Minimum variance for a QB throw (the random offset that is within one meter of the ideal spot will be multiplied by this)")]
  public float MaxAIAccuracyVariance = 7f;
  [Tooltip("Minimum variance for a QB throw (the random offset that is within one meter of the ideal spot will be multiplied by this)")]
  public float MinAIAccuracyVariance = 3f;
  [Tooltip("We apply a multiplier to the accuracy variance based on distance.  The multiplier is the distance to the receiver divided by this value.")]
  public float AIAccuracyVarianceMultiplierDistance = 15f;

  public float MaxBallSpeed => this.MaxBallSpeedMPH * Field.ONE_MILE_PER_HOUR;

  public float MaxBallSpeedShort => this.MaxShortBallSpeedMPH * Field.ONE_MILE_PER_HOUR;

  [HideInInspector]
  public float MinDotReceiverFacingToPasserForScaling => Mathf.Cos(this.MaxDegreesReceiverFacingToPasserForScaling * ((float) Math.PI / 180f));

  public float MaxDistanceForShortPassAlpha => Field.ConvertYardsToDistance(this.MaxYardsForShortPassAlpha);

  [HideInInspector]
  public float MinDotFromReceiverFacingToBallShortAlpha => Mathf.Cos(this.MaxDegreesFromReceiverFacingToBallShortAlpha * ((float) Math.PI / 180f));

  [HideInInspector]
  public float MinDotFromReceiverFacingToBallAlpha => Mathf.Cos(this.MaxDegreesFromReceiverFacingToBallAlpha * ((float) Math.PI / 180f));

  [HideInInspector]
  public float MinDotFromReceiverFacingToBallNearAlpha => Mathf.Cos(this.MaxDegreesFromReceiverFacingToBallNearAlpha * ((float) Math.PI / 180f));
}
