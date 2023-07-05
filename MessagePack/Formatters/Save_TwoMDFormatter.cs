// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Save_TwoMDFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class Save_TwoMDFormatter : IMessagePackFormatter<Save_TwoMD>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Save_TwoMD value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(2);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.BestScore);
      }
    }

    public Save_TwoMD Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Save_TwoMD) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Save_TwoMD saveTwoMd = new Save_TwoMD();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveTwoMd.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveTwoMd.BestScore = reader.ReadSingle();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveTwoMd;
    }
  }
}
