// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ProEra.Game.Sources.TeamData.DefaultPlayerDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources.TeamData;

namespace MessagePack.Formatters.ProEra.Game.Sources.TeamData
{
  public sealed class DefaultPlayerDataFormatter : 
    IMessagePackFormatter<DefaultPlayerData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      DefaultPlayerData value,
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
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.position, options);
        writer.Write(value.index);
      }
    }

    public DefaultPlayerData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (DefaultPlayerData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      DefaultPlayerData defaultPlayerData = new DefaultPlayerData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            defaultPlayerData.position = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 1:
            defaultPlayerData.index = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return defaultPlayerData;
    }
  }
}
