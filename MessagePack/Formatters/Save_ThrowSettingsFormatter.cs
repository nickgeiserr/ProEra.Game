// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Save_ThrowSettingsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System.Collections.Generic;
using UnityEngine;

namespace MessagePack.Formatters
{
  public sealed class Save_ThrowSettingsFormatter : 
    IMessagePackFormatter<Save_ThrowSettings>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Save_ThrowSettings value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(49);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.ThrowVersion);
        writer.Write(value.MinThrowThreshold);
        writer.Write(value.MinMultiplier);
        writer.Write(value.MaxMultiplierHoriz);
        writer.Write(value.MaxMultiplierVert);
        writer.Write(value.MinMultiplierSpeed);
        writer.Write(value.MaxMultiplierSpeed);
        writer.Write(value.AngleCorrection);
        writer.Write(value.FramesTracked);
        writer.Write(value.WeightDecreaseFactor);
        writer.Write(value.PredictedVelocityFalloff);
        writer.Write(value.AutoAimEnabled);
        writer.Write(value.MaxAngleAdjustment);
        writer.Write(value.MinTargetDistance);
        writer.Write(value.MaxPitchDistance);
        writer.Write(value.BallRotationsPerSecond);
        writer.Write(value.BallRotationWobbles);
        writer.Write(value.BallRotationWobblesMin);
        writer.Write(value.BallDirectionLerpFactor);
        writer.Write(value.BallSpinSpeedFactor);
        writer.Write(value.BallStabilizer);
        writer.Write(value.HikeFlightTime);
        writer.Write(value.twoHandedV2);
        writer.Write(value.CarryPoseSpeedThreshold);
        writer.Write(value.ThrowPoseBallUpTimeThreshold);
        writer.Write(value.handPhysics);
        writer.Write(value.renderColliders);
        writer.Write(value.continuousDetection);
        writer.Write(value.framesDelayCollisionReenabled);
        resolver.GetFormatterWithVerify<EMoveType>().Serialize(ref writer, value.MovementMethod, options);
        resolver.GetFormatterWithVerify<ETargetInterpolation>().Serialize(ref writer, value.TargetInterpolation, options);
        resolver.GetFormatterWithVerify<RigidbodyInterpolation>().Serialize(ref writer, value.InterpolationMethod_Hand, options);
        writer.Write(value.lerpFactor);
        writer.Write(value.dynamicFriction);
        writer.Write(value.staticFriction);
        writer.Write(value.bounciness);
        resolver.GetFormatterWithVerify<RigidbodyInterpolation>().Serialize(ref writer, value.InterpolationMethod_Ball, options);
        writer.Write(value.TriggerPressThreshold);
        writer.Write(value.CatchTestDuration);
        writer.Write(value.CatchPositionDelay);
        writer.Write(value.InteractionRange);
        writer.Write(value.VibrationDuration);
        writer.Write(value.VibrationEnabled);
        writer.Write(value.ShowDebug);
        writer.Write(value.ShowThrowDebug);
        writer.Write(value.CompareThrowVersionRed);
        writer.Write(value.CompareThrowVersionBlue);
        writer.Write(value.showBallOutline);
      }
    }

    public Save_ThrowSettings Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Save_ThrowSettings) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Save_ThrowSettings saveThrowSettings = new Save_ThrowSettings();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveThrowSettings.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveThrowSettings.ThrowVersion = reader.ReadInt32();
            break;
          case 2:
            saveThrowSettings.MinThrowThreshold = reader.ReadSingle();
            break;
          case 3:
            saveThrowSettings.MinMultiplier = reader.ReadSingle();
            break;
          case 4:
            saveThrowSettings.MaxMultiplierHoriz = reader.ReadSingle();
            break;
          case 5:
            saveThrowSettings.MaxMultiplierVert = reader.ReadSingle();
            break;
          case 6:
            saveThrowSettings.MinMultiplierSpeed = reader.ReadSingle();
            break;
          case 7:
            saveThrowSettings.MaxMultiplierSpeed = reader.ReadSingle();
            break;
          case 8:
            saveThrowSettings.AngleCorrection = reader.ReadSingle();
            break;
          case 9:
            saveThrowSettings.FramesTracked = reader.ReadInt32();
            break;
          case 10:
            saveThrowSettings.WeightDecreaseFactor = reader.ReadSingle();
            break;
          case 11:
            saveThrowSettings.PredictedVelocityFalloff = reader.ReadSingle();
            break;
          case 12:
            saveThrowSettings.AutoAimEnabled = reader.ReadBoolean();
            break;
          case 13:
            saveThrowSettings.MaxAngleAdjustment = reader.ReadSingle();
            break;
          case 14:
            saveThrowSettings.MinTargetDistance = reader.ReadSingle();
            break;
          case 15:
            saveThrowSettings.MaxPitchDistance = reader.ReadSingle();
            break;
          case 16:
            saveThrowSettings.BallRotationsPerSecond = reader.ReadSingle();
            break;
          case 17:
            saveThrowSettings.BallRotationWobbles = reader.ReadSingle();
            break;
          case 18:
            saveThrowSettings.BallRotationWobblesMin = reader.ReadSingle();
            break;
          case 19:
            saveThrowSettings.BallDirectionLerpFactor = reader.ReadSingle();
            break;
          case 20:
            saveThrowSettings.BallSpinSpeedFactor = reader.ReadSingle();
            break;
          case 21:
            saveThrowSettings.BallStabilizer = reader.ReadSingle();
            break;
          case 22:
            saveThrowSettings.HikeFlightTime = reader.ReadSingle();
            break;
          case 23:
            saveThrowSettings.twoHandedV2 = reader.ReadBoolean();
            break;
          case 24:
            saveThrowSettings.CarryPoseSpeedThreshold = reader.ReadSingle();
            break;
          case 25:
            saveThrowSettings.ThrowPoseBallUpTimeThreshold = reader.ReadSingle();
            break;
          case 26:
            saveThrowSettings.handPhysics = reader.ReadBoolean();
            break;
          case 27:
            saveThrowSettings.renderColliders = reader.ReadBoolean();
            break;
          case 28:
            saveThrowSettings.continuousDetection = reader.ReadBoolean();
            break;
          case 29:
            saveThrowSettings.framesDelayCollisionReenabled = reader.ReadInt32();
            break;
          case 30:
            saveThrowSettings.MovementMethod = resolver.GetFormatterWithVerify<EMoveType>().Deserialize(ref reader, options);
            break;
          case 31:
            saveThrowSettings.TargetInterpolation = resolver.GetFormatterWithVerify<ETargetInterpolation>().Deserialize(ref reader, options);
            break;
          case 32:
            saveThrowSettings.InterpolationMethod_Hand = resolver.GetFormatterWithVerify<RigidbodyInterpolation>().Deserialize(ref reader, options);
            break;
          case 33:
            saveThrowSettings.lerpFactor = reader.ReadSingle();
            break;
          case 34:
            saveThrowSettings.dynamicFriction = reader.ReadSingle();
            break;
          case 35:
            saveThrowSettings.staticFriction = reader.ReadSingle();
            break;
          case 36:
            saveThrowSettings.bounciness = reader.ReadSingle();
            break;
          case 37:
            saveThrowSettings.InterpolationMethod_Ball = resolver.GetFormatterWithVerify<RigidbodyInterpolation>().Deserialize(ref reader, options);
            break;
          case 38:
            saveThrowSettings.TriggerPressThreshold = reader.ReadSingle();
            break;
          case 39:
            saveThrowSettings.CatchTestDuration = reader.ReadSingle();
            break;
          case 40:
            saveThrowSettings.CatchPositionDelay = reader.ReadSingle();
            break;
          case 41:
            saveThrowSettings.InteractionRange = reader.ReadSingle();
            break;
          case 42:
            saveThrowSettings.VibrationDuration = reader.ReadSingle();
            break;
          case 43:
            saveThrowSettings.VibrationEnabled = reader.ReadBoolean();
            break;
          case 44:
            saveThrowSettings.ShowDebug = reader.ReadBoolean();
            break;
          case 45:
            saveThrowSettings.ShowThrowDebug = reader.ReadBoolean();
            break;
          case 46:
            saveThrowSettings.CompareThrowVersionRed = reader.ReadInt32();
            break;
          case 47:
            saveThrowSettings.CompareThrowVersionBlue = reader.ReadInt32();
            break;
          case 48:
            saveThrowSettings.showBallOutline = reader.ReadBoolean();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveThrowSettings;
    }
  }
}
