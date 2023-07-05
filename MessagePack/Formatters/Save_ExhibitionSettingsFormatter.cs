// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Save_ExhibitionSettingsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class Save_ExhibitionSettingsFormatter : 
    IMessagePackFormatter<Save_ExhibitionSettings>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Save_ExhibitionSettings value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(9);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.WeatherTypeIndex);
        writer.Write(value.TimeOfDayIndex);
        writer.Write(value.WindIndex);
        writer.Write(value.DefDifficulty);
        writer.Write(value.OffDifficulty);
        writer.Write(value.QuarterLengthIndex);
        writer.Write(value.UseFatigue);
        writer.Write(value.UseInjuries);
      }
    }

    public Save_ExhibitionSettings Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Save_ExhibitionSettings) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Save_ExhibitionSettings exhibitionSettings = new Save_ExhibitionSettings();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            exhibitionSettings.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            exhibitionSettings.WeatherTypeIndex = reader.ReadInt32();
            break;
          case 2:
            exhibitionSettings.TimeOfDayIndex = reader.ReadInt32();
            break;
          case 3:
            exhibitionSettings.WindIndex = reader.ReadInt32();
            break;
          case 4:
            exhibitionSettings.DefDifficulty = reader.ReadInt32();
            break;
          case 5:
            exhibitionSettings.OffDifficulty = reader.ReadInt32();
            break;
          case 6:
            exhibitionSettings.QuarterLengthIndex = reader.ReadInt32();
            break;
          case 7:
            exhibitionSettings.UseFatigue = reader.ReadBoolean();
            break;
          case 8:
            exhibitionSettings.UseInjuries = reader.ReadBoolean();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return exhibitionSettings;
    }
  }
}
