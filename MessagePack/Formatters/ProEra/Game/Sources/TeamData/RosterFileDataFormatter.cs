// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ProEra.Game.Sources.TeamData.RosterFileDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources.TeamData;

namespace MessagePack.Formatters.ProEra.Game.Sources.TeamData
{
  public sealed class RosterFileDataFormatter : 
    IMessagePackFormatter<RosterFileData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      RosterFileData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(4);
        writer.Write(value.Version);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.CityAbbreviation, options);
        resolver.GetFormatterWithVerify<RosterPlayerData[]>().Serialize(ref writer, value.Players, options);
        resolver.GetFormatterWithVerify<DefaultPlayerFileData>().Serialize(ref writer, value.DefaultPlayerDataObject, options);
      }
    }

    public RosterFileData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (RosterFileData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      RosterFileData rosterFileData = new RosterFileData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            rosterFileData.Version = reader.ReadInt32();
            break;
          case 1:
            rosterFileData.CityAbbreviation = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            rosterFileData.Players = resolver.GetFormatterWithVerify<RosterPlayerData[]>().Deserialize(ref reader, options);
            break;
          case 3:
            rosterFileData.DefaultPlayerDataObject = resolver.GetFormatterWithVerify<DefaultPlayerFileData>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return rosterFileData;
    }
  }
}
