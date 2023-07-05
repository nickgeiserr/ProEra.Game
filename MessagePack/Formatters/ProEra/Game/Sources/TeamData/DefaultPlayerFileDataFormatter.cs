// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ProEra.Game.Sources.TeamData.DefaultPlayerFileDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources.TeamData;

namespace MessagePack.Formatters.ProEra.Game.Sources.TeamData
{
  public sealed class DefaultPlayerFileDataFormatter : 
    IMessagePackFormatter<DefaultPlayerFileData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      DefaultPlayerFileData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(1);
        resolver.GetFormatterWithVerify<DefaultPlayerData[]>().Serialize(ref writer, value.DefaultPlayerCollection, options);
      }
    }

    public DefaultPlayerFileData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (DefaultPlayerFileData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      DefaultPlayerFileData defaultPlayerFileData = new DefaultPlayerFileData();
      for (int index = 0; index < num; ++index)
      {
        if (index == 0)
          defaultPlayerFileData.DefaultPlayerCollection = resolver.GetFormatterWithVerify<DefaultPlayerData[]>().Deserialize(ref reader, options);
        else
          reader.Skip();
      }
      --reader.Depth;
      return defaultPlayerFileData;
    }
  }
}
