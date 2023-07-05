// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Save_MiniCampFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class Save_MiniCampFormatter : 
    IMessagePackFormatter<Save_MiniCamp>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Save_MiniCamp value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(5);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        resolver.GetFormatterWithVerify<MiniCampData>().Serialize(ref writer, value.SelectedMiniCamp, options);
        resolver.GetFormatterWithVerify<MiniCampEntry>().Serialize(ref writer, value.SelectedEntry, options);
        resolver.GetFormatterWithVerify<MiniCampData[]>().Serialize(ref writer, value.MiniCamps, options);
        writer.Write(value.LoadedTemplate);
      }
    }

    public Save_MiniCamp Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Save_MiniCamp) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Save_MiniCamp saveMiniCamp = new Save_MiniCamp();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveMiniCamp.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveMiniCamp.SelectedMiniCamp = resolver.GetFormatterWithVerify<MiniCampData>().Deserialize(ref reader, options);
            break;
          case 2:
            saveMiniCamp.SelectedEntry = resolver.GetFormatterWithVerify<MiniCampEntry>().Deserialize(ref reader, options);
            break;
          case 3:
            saveMiniCamp.MiniCamps = resolver.GetFormatterWithVerify<MiniCampData[]>().Deserialize(ref reader, options);
            break;
          case 4:
            saveMiniCamp.LoadedTemplate = reader.ReadBoolean();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveMiniCamp;
    }
  }
}
