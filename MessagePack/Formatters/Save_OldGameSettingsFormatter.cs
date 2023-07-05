// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Save_OldGameSettingsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class Save_OldGameSettingsFormatter : 
    IMessagePackFormatter<Save_OldGameSettings>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Save_OldGameSettings value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(7);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.TimeScale);
        writer.Write(value.OffenseGoingNorth);
        writer.Write(value.PlayerOnField);
        writer.Write(value.DifficultyLevel);
        writer.Write(value.AutoDropBackBulletTimeSpeed);
        writer.Write(value.HandoffBulletTimeSpeed);
      }
    }

    public Save_OldGameSettings Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Save_OldGameSettings) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Save_OldGameSettings saveOldGameSettings = new Save_OldGameSettings();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveOldGameSettings.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveOldGameSettings.TimeScale = reader.ReadSingle();
            break;
          case 2:
            saveOldGameSettings.OffenseGoingNorth = reader.ReadBoolean();
            break;
          case 3:
            saveOldGameSettings.PlayerOnField = reader.ReadBoolean();
            break;
          case 4:
            saveOldGameSettings.DifficultyLevel = reader.ReadInt32();
            break;
          case 5:
            saveOldGameSettings.AutoDropBackBulletTimeSpeed = reader.ReadSingle();
            break;
          case 6:
            saveOldGameSettings.HandoffBulletTimeSpeed = reader.ReadSingle();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveOldGameSettings;
    }
  }
}
