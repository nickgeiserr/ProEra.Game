// Decompiled with JetBrains decompiler
// Type: PlayerAnimTuning
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PlayerAnimTuning : MonoBehaviour
{
  public static float MinLateralMovementSpeed = 0.1f;
  public static float MinForwardMovementSpeed = 0.1f;
  public static float MinStrafeDecayMovementSpeed = 0.01f;
  public static float MinForwardDecayMovementSpeed = 0.01f;
  public static float StrafeRotateToLOSSpeed = 0.2f;
  public static float QBRotateToLOSSpeed = 0.2f;
  public static float StartRunSpeed = 1f;
  public static float DampenRotationAngle = 0.5f;
  public static float DampenRotationMod = 0.35f;
  public static float ReceiverStopTurnDistance = 4f;
  public static float MinFaceTargetAngle = 60f;
  public static float DecayDirectionAngle = 0.01f;
  public static float BallInAirPuntAnimSpeed = 1.3f;
  public static float PreKickOffKickerSpeed = 1.2f;
  public static float PreKickoffOffenseSpeedMin = 0.75f;
  public static float PreKickoffOffenseSpeedMax = 0.9f;
  public static float PreKickoffDefenseSpeedMin = 0.95f;
  public static float PreKickoffDefenseSpeedMax = 1f;
  public static float SprintingOffenseSpeedMod = 1.2f;
  public static float SprintingOffenseSpeedMax = 1.3f;
  public static float SprintingDefenseSpeedMod = 1.1f;
  public static float SprintingDefenseSpeedMax = 1f;
  public static float DefMaxZDiffFromBallCarrierForStrafe = 4f;
  public static float DefMaxXDiffFromBallCarrierForStrafe = 4f;
  public static float DefCatchFacingSpeed = 0.1f;
  public static float KickBlockersFacingSpeed = 0.3f;
  public static float MinMoveDistSquared = 0.1681f;
}
