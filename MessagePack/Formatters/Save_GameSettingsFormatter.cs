// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Save_GameSettingsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class Save_GameSettingsFormatter : 
    IMessagePackFormatter<Save_GameSettings>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Save_GameSettings value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(58);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.GameSFXVolume);
        writer.Write(value.AnnouncerVolume);
        writer.Write(value.GameOptionsMusicVolume);
        writer.Write(value.MenuMusicVolume);
        writer.Write(value.SoundOn);
        writer.Write(value.MusicOn);
        writer.Write(value.AnnouncerOn);
        writer.Write(value.GraphicsQuality);
        writer.Write(value.ResolutionWidth);
        writer.Write(value.ResolutionHeight);
        writer.Write(value.ignoreControllers);
        writer.Write(value.runInBackground);
        writer.Write(value.SensitivitySliderP1);
        writer.Write(value.SensitivitySliderP2);
        writer.Write(value.LeadReceiverValueP1);
        writer.Write(value.LeadReceiverValueP2);
        writer.Write(value.GlobalTimeScale);
        writer.Write(value.ButtonPassingP1);
        writer.Write(value.ButtonPassingP2);
        writer.Write(value.StickPushP1);
        writer.Write(value.StickPushP2);
        writer.Write(value.AcceleratedClockValue);
        writer.Write(value.ButtonStyle);
        writer.Write(value.ClockSpeed);
        writer.Write(value.ControllerIconStyle);
        writer.Write(value.TimeBetweenPlays);
        writer.Write(value.CameraHeightZoom);
        writer.Write(value.CameraDepthZoom);
        writer.Write(value.ShowSidelinePlayers);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.GameLanguage, options);
        writer.Write(value.DefDifficulty);
        writer.Write(value.OffDifficulty);
        writer.Write(value.QuarterLength);
        writer.Write(value.UseFatigue);
        writer.Write(value.UseInjuries);
        writer.Write(value.TwoMinuteWarningEnabled);
        writer.Write(value.TutorialEnabled);
        writer.Write(value.InjuryFrequency);
        writer.Write(value.PenaltyFrequency);
        writer.Write(value.DelayOfGame);
        writer.Write(value.FalseStart);
        writer.Write(value.OffensiveHolding);
        writer.Write(value.OffensivePassInterference);
        writer.Write(value.DefensivePassInterference);
        writer.Write(value.Facemask);
        writer.Write(value.IllegalForwardPass);
        writer.Write(value.Offsides);
        writer.Write(value.Encroachment);
        writer.Write(value.KickoffOutOfBounds);
        writer.Write(value.IntentionalGrounding);
        writer.Write(value._useBaseAssets);
        writer.Write(value._useModAssets);
        writer.Write(value._logGamesToFile);
        writer.Write(value._exportGameStatsToFile);
        writer.Write(value._isFullscreeMode);
        writer.Write(value._lockerInteractionHistory);
        writer.Write(value._didCompleteTutorial);
      }
    }

    public Save_GameSettings Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Save_GameSettings) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Save_GameSettings saveGameSettings = new Save_GameSettings();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveGameSettings.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveGameSettings.GameSFXVolume = reader.ReadSingle();
            break;
          case 2:
            saveGameSettings.AnnouncerVolume = reader.ReadSingle();
            break;
          case 3:
            saveGameSettings.GameOptionsMusicVolume = reader.ReadSingle();
            break;
          case 4:
            saveGameSettings.MenuMusicVolume = reader.ReadSingle();
            break;
          case 5:
            saveGameSettings.SoundOn = reader.ReadBoolean();
            break;
          case 6:
            saveGameSettings.MusicOn = reader.ReadBoolean();
            break;
          case 7:
            saveGameSettings.AnnouncerOn = reader.ReadBoolean();
            break;
          case 8:
            saveGameSettings.GraphicsQuality = reader.ReadInt32();
            break;
          case 9:
            saveGameSettings.ResolutionWidth = reader.ReadInt32();
            break;
          case 10:
            saveGameSettings.ResolutionHeight = reader.ReadInt32();
            break;
          case 11:
            saveGameSettings.ignoreControllers = reader.ReadBoolean();
            break;
          case 12:
            saveGameSettings.runInBackground = reader.ReadBoolean();
            break;
          case 13:
            saveGameSettings.SensitivitySliderP1 = reader.ReadSingle();
            break;
          case 14:
            saveGameSettings.SensitivitySliderP2 = reader.ReadSingle();
            break;
          case 15:
            saveGameSettings.LeadReceiverValueP1 = reader.ReadSingle();
            break;
          case 16:
            saveGameSettings.LeadReceiverValueP2 = reader.ReadSingle();
            break;
          case 17:
            saveGameSettings.GlobalTimeScale = reader.ReadSingle();
            break;
          case 18:
            saveGameSettings.ButtonPassingP1 = reader.ReadBoolean();
            break;
          case 19:
            saveGameSettings.ButtonPassingP2 = reader.ReadBoolean();
            break;
          case 20:
            saveGameSettings.StickPushP1 = reader.ReadBoolean();
            break;
          case 21:
            saveGameSettings.StickPushP2 = reader.ReadBoolean();
            break;
          case 22:
            saveGameSettings.AcceleratedClockValue = reader.ReadInt32();
            break;
          case 23:
            saveGameSettings.ButtonStyle = reader.ReadInt32();
            break;
          case 24:
            saveGameSettings.ClockSpeed = reader.ReadInt32();
            break;
          case 25:
            saveGameSettings.ControllerIconStyle = reader.ReadInt32();
            break;
          case 26:
            saveGameSettings.TimeBetweenPlays = reader.ReadInt32();
            break;
          case 27:
            saveGameSettings.CameraHeightZoom = reader.ReadSingle();
            break;
          case 28:
            saveGameSettings.CameraDepthZoom = reader.ReadSingle();
            break;
          case 29:
            saveGameSettings.ShowSidelinePlayers = reader.ReadBoolean();
            break;
          case 30:
            saveGameSettings.GameLanguage = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 31:
            saveGameSettings.DefDifficulty = reader.ReadInt32();
            break;
          case 32:
            saveGameSettings.OffDifficulty = reader.ReadInt32();
            break;
          case 33:
            saveGameSettings.QuarterLength = reader.ReadInt32();
            break;
          case 34:
            saveGameSettings.UseFatigue = reader.ReadBoolean();
            break;
          case 35:
            saveGameSettings.UseInjuries = reader.ReadBoolean();
            break;
          case 36:
            saveGameSettings.TwoMinuteWarningEnabled = reader.ReadBoolean();
            break;
          case 37:
            saveGameSettings.TutorialEnabled = reader.ReadBoolean();
            break;
          case 38:
            saveGameSettings.InjuryFrequency = reader.ReadInt32();
            break;
          case 39:
            saveGameSettings.PenaltyFrequency = reader.ReadSingle();
            break;
          case 40:
            saveGameSettings.DelayOfGame = reader.ReadBoolean();
            break;
          case 41:
            saveGameSettings.FalseStart = reader.ReadBoolean();
            break;
          case 42:
            saveGameSettings.OffensiveHolding = reader.ReadBoolean();
            break;
          case 43:
            saveGameSettings.OffensivePassInterference = reader.ReadBoolean();
            break;
          case 44:
            saveGameSettings.DefensivePassInterference = reader.ReadBoolean();
            break;
          case 45:
            saveGameSettings.Facemask = reader.ReadBoolean();
            break;
          case 46:
            saveGameSettings.IllegalForwardPass = reader.ReadBoolean();
            break;
          case 47:
            saveGameSettings.Offsides = reader.ReadBoolean();
            break;
          case 48:
            saveGameSettings.Encroachment = reader.ReadBoolean();
            break;
          case 49:
            saveGameSettings.KickoffOutOfBounds = reader.ReadBoolean();
            break;
          case 50:
            saveGameSettings.IntentionalGrounding = reader.ReadBoolean();
            break;
          case 51:
            saveGameSettings._useBaseAssets = reader.ReadBoolean();
            break;
          case 52:
            saveGameSettings._useModAssets = reader.ReadBoolean();
            break;
          case 53:
            saveGameSettings._logGamesToFile = reader.ReadBoolean();
            break;
          case 54:
            saveGameSettings._exportGameStatsToFile = reader.ReadBoolean();
            break;
          case 55:
            saveGameSettings._isFullscreeMode = reader.ReadBoolean();
            break;
          case 56:
            saveGameSettings._lockerInteractionHistory = reader.ReadInt32();
            break;
          case 57:
            saveGameSettings._didCompleteTutorial = reader.ReadBoolean();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveGameSettings;
    }
  }
}
