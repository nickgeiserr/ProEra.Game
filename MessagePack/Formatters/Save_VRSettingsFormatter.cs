// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Save_VRSettingsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class Save_VRSettingsFormatter : 
    IMessagePackFormatter<Save_VRSettings>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Save_VRSettings value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(36);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.LoadedGameStartupScene);
        writer.Write(value.BypassStartup);
        writer.Write(value.ControllerAnnotations);
        writer.Write(value.PlayerBodyScale);
        writer.Write(value.UIDistance);
        writer.Write(value.UpdateUIHeight);
        writer.Write(value.UseLeftHand);
        writer.Write(value.UseVrLaser);
        writer.Write(value.GripButtonThrow);
        writer.Write(value.SeatedMode);
        writer.Write(value.HelmetActive);
        writer.Write(value.OneHandedMode);
        writer.Write(value.HuddlePlayClock);
        writer.Write(value.NoHuddlePlayClockOffset);
        writer.Write(value.QuarterLength);
        writer.Write(value.PositionalAudio);
        writer.Write(value.AlphaThrowing);
        writer.Write(value.AudioSpeakThreshold);
        writer.Write(value.AllowDynamicTargets);
        writer.Write(value.VCVolume);
        writer.Write(value.MicVolume);
        writer.Write(value.ImmersiveTackleEnabled);
        writer.Write(value.AutoDropbackEnabled);
        writer.Write(value.VignetteEnabled);
        writer.Write(value.VignetteFalloffDegrees);
        writer.Write(value.VignetteAspectRatio);
        writer.Write(value.VignetteLerpFactor);
        writer.Write(value.VignetteMinFov);
        writer.Write(value.VignetteMaxFov);
        writer.Write(value.gravity);
        writer.Write(value.timeFactor);
        writer.Write(value.useSpline);
        writer.Write(value.splineCoef);
        writer.Write(value.splineHeightCoef);
        writer.Write(value.laserPower);
      }
    }

    public Save_VRSettings Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Save_VRSettings) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Save_VRSettings saveVrSettings = new Save_VRSettings();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveVrSettings.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveVrSettings.LoadedGameStartupScene = reader.ReadBoolean();
            break;
          case 2:
            saveVrSettings.BypassStartup = reader.ReadBoolean();
            break;
          case 3:
            saveVrSettings.ControllerAnnotations = reader.ReadBoolean();
            break;
          case 4:
            saveVrSettings.PlayerBodyScale = reader.ReadSingle();
            break;
          case 5:
            saveVrSettings.UIDistance = reader.ReadSingle();
            break;
          case 6:
            saveVrSettings.UpdateUIHeight = reader.ReadBoolean();
            break;
          case 7:
            saveVrSettings.UseLeftHand = reader.ReadBoolean();
            break;
          case 8:
            saveVrSettings.UseVrLaser = reader.ReadBoolean();
            break;
          case 9:
            saveVrSettings.GripButtonThrow = reader.ReadBoolean();
            break;
          case 10:
            saveVrSettings.SeatedMode = reader.ReadBoolean();
            break;
          case 11:
            saveVrSettings.HelmetActive = reader.ReadBoolean();
            break;
          case 12:
            saveVrSettings.OneHandedMode = reader.ReadBoolean();
            break;
          case 13:
            saveVrSettings.HuddlePlayClock = reader.ReadInt32();
            break;
          case 14:
            saveVrSettings.NoHuddlePlayClockOffset = reader.ReadInt32();
            break;
          case 15:
            saveVrSettings.QuarterLength = reader.ReadInt32();
            break;
          case 16:
            saveVrSettings.PositionalAudio = reader.ReadSingle();
            break;
          case 17:
            saveVrSettings.AlphaThrowing = reader.ReadBoolean();
            break;
          case 18:
            saveVrSettings.AudioSpeakThreshold = reader.ReadSingle();
            break;
          case 19:
            saveVrSettings.AllowDynamicTargets = reader.ReadBoolean();
            break;
          case 20:
            saveVrSettings.VCVolume = reader.ReadSingle();
            break;
          case 21:
            saveVrSettings.MicVolume = reader.ReadSingle();
            break;
          case 22:
            saveVrSettings.ImmersiveTackleEnabled = reader.ReadBoolean();
            break;
          case 23:
            saveVrSettings.AutoDropbackEnabled = reader.ReadBoolean();
            break;
          case 24:
            saveVrSettings.VignetteEnabled = reader.ReadBoolean();
            break;
          case 25:
            saveVrSettings.VignetteFalloffDegrees = reader.ReadSingle();
            break;
          case 26:
            saveVrSettings.VignetteAspectRatio = reader.ReadSingle();
            break;
          case 27:
            saveVrSettings.VignetteLerpFactor = reader.ReadSingle();
            break;
          case 28:
            saveVrSettings.VignetteMinFov = reader.ReadSingle();
            break;
          case 29:
            saveVrSettings.VignetteMaxFov = reader.ReadSingle();
            break;
          case 30:
            saveVrSettings.gravity = reader.ReadSingle();
            break;
          case 31:
            saveVrSettings.timeFactor = reader.ReadSingle();
            break;
          case 32:
            saveVrSettings.useSpline = reader.ReadBoolean();
            break;
          case 33:
            saveVrSettings.splineCoef = reader.ReadSingle();
            break;
          case 34:
            saveVrSettings.splineHeightCoef = reader.ReadSingle();
            break;
          case 35:
            saveVrSettings.laserPower = reader.ReadSingle();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveVrSettings;
    }
  }
}
