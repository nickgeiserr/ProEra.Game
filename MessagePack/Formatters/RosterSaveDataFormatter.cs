// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.RosterSaveDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources.TeamData;
using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class RosterSaveDataFormatter : 
    IMessagePackFormatter<RosterSaveData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      RosterSaveData value,
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
        resolver.GetFormatterWithVerify<RosterFileData[]>().Serialize(ref writer, value.rosters, options);
      }
    }

    public RosterSaveData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (RosterSaveData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      RosterSaveData rosterSaveData = new RosterSaveData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            rosterSaveData.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            rosterSaveData.rosters = resolver.GetFormatterWithVerify<RosterFileData[]>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return rosterSaveData;
    }
  }
}
