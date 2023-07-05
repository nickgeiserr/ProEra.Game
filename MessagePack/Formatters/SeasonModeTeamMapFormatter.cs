// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SeasonModeTeamMapFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class SeasonModeTeamMapFormatter : 
    IMessagePackFormatter<SeasonModeTeamMap>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SeasonModeTeamMap value,
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
        writer.Write(value.index);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.name, options);
      }
    }

    public SeasonModeTeamMap Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SeasonModeTeamMap) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      SeasonModeTeamMap seasonModeTeamMap = new SeasonModeTeamMap();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            reader.ReadInt32();
            break;
          case 1:
            resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return seasonModeTeamMap;
    }
  }
}
