// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TeamFileFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class TeamFileFormatter : IMessagePackFormatter<TeamFile>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      TeamFile value,
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
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.nameValuePairs, options);
      }
    }

    public TeamFile Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (TeamFile) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      TeamFile teamFile = new TeamFile();
      for (int index = 0; index < num; ++index)
      {
        if (index == 0)
          teamFile.nameValuePairs = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
        else
          reader.Skip();
      }
      --reader.Depth;
      return teamFile;
    }
  }
}
