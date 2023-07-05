// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TeamSeasonDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class TeamSeasonDataFormatter : 
    IMessagePackFormatter<TeamSeasonData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      TeamSeasonData value,
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
        writer.WriteNil();
        writer.Write(value.win);
        writer.Write(value.loss);
        writer.Write(value.tie);
      }
    }

    public TeamSeasonData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (TeamSeasonData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      TeamSeasonData teamSeasonData = new TeamSeasonData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            teamSeasonData.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 2:
            teamSeasonData.win = reader.ReadInt32();
            break;
          case 3:
            teamSeasonData.loss = reader.ReadInt32();
            break;
          case 4:
            teamSeasonData.tie = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return teamSeasonData;
    }
  }
}
