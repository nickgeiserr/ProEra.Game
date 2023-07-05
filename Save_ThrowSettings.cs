// Decompiled with JetBrains decompiler
// Type: Save_ThrowSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[MessagePackObject(false)]
[Serializable]
public class Save_ThrowSettings : ISaveSync
{
  [IgnoreMember]
  public static string FileName = nameof (Save_ThrowSettings);
  [IgnoreMember]
  private bool isDirty;
  [IgnoreMember]
  public UnityAction OnLoaded;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public int ThrowVersion = 6;
  [Key(2)]
  public float MinThrowThreshold = 3f;
  [Key(3)]
  public float MinMultiplier = 0.25f;
  [Key(4)]
  public float MaxMultiplierHoriz = 2.5f;
  [Key(5)]
  public float MaxMultiplierVert = 2.4f;
  [Key(6)]
  public float MinMultiplierSpeed = 1f;
  [Key(7)]
  public float MaxMultiplierSpeed = 1.5f;
  [Key(8)]
  public float AngleCorrection = -2f;
  [Key(9)]
  public int FramesTracked = 7;
  [Key(10)]
  public float WeightDecreaseFactor = 0.87f;
  [Key(11)]
  public float PredictedVelocityFalloff = 0.9f;
  [Key(12)]
  public bool AutoAimEnabled = true;
  [Key(13)]
  public float MaxAngleAdjustment = 60f;
  [Key(14)]
  public float MinTargetDistance = 50f;
  [Key(15)]
  public float MaxPitchDistance = 9.144f;
  [Key(16)]
  public float BallRotationsPerSecond = 5f;
  [Key(17)]
  public float BallRotationWobbles = 0.9f;
  [Key(18)]
  public float BallRotationWobblesMin = 0.4f;
  [Key(19)]
  public float BallDirectionLerpFactor = 0.5f;
  [Key(20)]
  public float BallSpinSpeedFactor = 0.5f;
  [Key(21)]
  public float BallStabilizer = 1.5f;
  [Key(22)]
  public float HikeFlightTime = 0.45f;
  [Key(23)]
  public bool twoHandedV2;
  [Key(24)]
  public float CarryPoseSpeedThreshold = 2f;
  [Key(25)]
  public float ThrowPoseBallUpTimeThreshold = 0.5f;
  [Key(26)]
  public bool handPhysics = true;
  [Key(27)]
  public bool renderColliders;
  [Key(28)]
  public bool continuousDetection = true;
  [Key(29)]
  public int framesDelayCollisionReenabled = 3;
  [Key(30)]
  public EMoveType MovementMethod = EMoveType.RigidbodyMove;
  [Key(31)]
  public ETargetInterpolation TargetInterpolation;
  [Key(32)]
  public RigidbodyInterpolation InterpolationMethod_Hand = RigidbodyInterpolation.Interpolate;
  [Key(33)]
  public float lerpFactor = 0.5f;
  [Key(34)]
  public float dynamicFriction = 0.5f;
  [Key(35)]
  public float staticFriction = 0.6f;
  [Key(36)]
  public float bounciness = 0.25f;
  [Key(37)]
  public RigidbodyInterpolation InterpolationMethod_Ball = RigidbodyInterpolation.Interpolate;
  [Key(38)]
  public float TriggerPressThreshold = 0.5f;
  [Key(39)]
  public float CatchTestDuration = 0.1f;
  [Key(40)]
  public float CatchPositionDelay = 1f;
  [Key(41)]
  public float InteractionRange = 0.1f;
  [Key(42)]
  public float VibrationDuration = 0.2f;
  [Key(43)]
  public bool VibrationEnabled = true;
  [Key(44)]
  public bool ShowDebug;
  [Key(45)]
  public bool ShowThrowDebug;
  [Key(46)]
  public int CompareThrowVersionRed = -1;
  [Key(47)]
  public int CompareThrowVersionBlue = -1;
  [Key(48)]
  public bool showBallOutline;

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value) => this.isDirty = value;

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<Save_ThrowSettings> deserializedData = SaveIO.LoadAsync<Save_ThrowSettings>(Path.Combine(SaveIO.DefaultFolderPath, Save_ThrowSettings.FileName));
    Save_ThrowSettings saveThrowSettings = await deserializedData;
    if (deserializedData.Result != null)
      this.CopyData(deserializedData.Result);
    UnityAction onLoaded = this.OnLoaded;
    if (onLoaded == null)
    {
      deserializedData = (Task<Save_ThrowSettings>) null;
    }
    else
    {
      onLoaded();
      deserializedData = (Task<Save_ThrowSettings>) null;
    }
  }

  private void CopyData(Save_ThrowSettings loadedData)
  {
    this.metaData = loadedData.metaData;
    this.ThrowVersion = loadedData.ThrowVersion;
    this.MinThrowThreshold = loadedData.MinThrowThreshold;
    this.MinMultiplier = loadedData.MinMultiplier;
    this.MaxMultiplierHoriz = loadedData.MaxMultiplierHoriz;
    this.MaxMultiplierVert = loadedData.MaxMultiplierVert;
    this.MinMultiplierSpeed = loadedData.MinMultiplierSpeed;
    this.MaxMultiplierSpeed = loadedData.MaxMultiplierSpeed;
    this.AngleCorrection = loadedData.AngleCorrection;
    this.FramesTracked = loadedData.FramesTracked;
    this.WeightDecreaseFactor = loadedData.WeightDecreaseFactor;
    this.PredictedVelocityFalloff = loadedData.PredictedVelocityFalloff;
    this.AutoAimEnabled = loadedData.AutoAimEnabled;
    this.MaxAngleAdjustment = loadedData.MaxAngleAdjustment;
    this.MinTargetDistance = loadedData.MinTargetDistance;
    this.MaxPitchDistance = loadedData.MaxPitchDistance;
    this.BallRotationsPerSecond = loadedData.BallRotationsPerSecond;
    this.BallRotationWobbles = loadedData.BallRotationWobbles;
    this.BallRotationWobblesMin = loadedData.BallRotationWobblesMin;
    this.BallDirectionLerpFactor = loadedData.BallDirectionLerpFactor;
    this.BallSpinSpeedFactor = loadedData.BallSpinSpeedFactor;
    this.BallStabilizer = loadedData.BallStabilizer;
    this.HikeFlightTime = loadedData.HikeFlightTime;
    this.twoHandedV2 = loadedData.twoHandedV2;
    this.CarryPoseSpeedThreshold = loadedData.CarryPoseSpeedThreshold;
    this.ThrowPoseBallUpTimeThreshold = loadedData.ThrowPoseBallUpTimeThreshold;
    this.handPhysics = loadedData.handPhysics;
    this.renderColliders = loadedData.renderColliders;
    this.continuousDetection = loadedData.continuousDetection;
    this.framesDelayCollisionReenabled = loadedData.framesDelayCollisionReenabled;
    this.MovementMethod = loadedData.MovementMethod;
    this.TargetInterpolation = loadedData.TargetInterpolation;
    this.InterpolationMethod_Hand = loadedData.InterpolationMethod_Hand;
    this.lerpFactor = loadedData.lerpFactor;
    this.dynamicFriction = loadedData.dynamicFriction;
    this.staticFriction = loadedData.staticFriction;
    this.bounciness = loadedData.bounciness;
    this.InterpolationMethod_Ball = loadedData.InterpolationMethod_Ball;
    this.TriggerPressThreshold = loadedData.TriggerPressThreshold;
    this.CatchTestDuration = loadedData.CatchTestDuration;
    this.CatchPositionDelay = loadedData.CatchPositionDelay;
    this.InteractionRange = loadedData.InteractionRange;
    this.VibrationDuration = loadedData.VibrationDuration;
    this.VibrationEnabled = loadedData.VibrationEnabled;
    this.ShowDebug = loadedData.ShowDebug;
    this.ShowThrowDebug = loadedData.ShowThrowDebug;
    this.CompareThrowVersionRed = loadedData.CompareThrowVersionRed;
    this.CompareThrowVersionBlue = loadedData.CompareThrowVersionBlue;
    this.showBallOutline = loadedData.showBallOutline;
  }

  public async Task Save()
  {
    Save_ThrowSettings objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<Save_ThrowSettings>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, Save_ThrowSettings.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
