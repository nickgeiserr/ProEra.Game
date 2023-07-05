// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.RosterDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class RosterDataFormatter : IMessagePackFormatter<RosterData>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      RosterData value,
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
        resolver.GetFormatterWithVerify<PlayerData[]>().Serialize(ref writer, value.roster, options);
        writer.Write(value.numberOfPlayersOnRoster);
        resolver.GetFormatterWithVerify<RosterType>().Serialize(ref writer, value.rosterType, options);
        resolver.GetFormatterWithVerify<Dictionary<string, int>>().Serialize(ref writer, value.defaultPlayers, options);
      }
    }

    public RosterData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (RosterData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      RosterData rosterData = new RosterData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            rosterData.roster = resolver.GetFormatterWithVerify<PlayerData[]>().Deserialize(ref reader, options);
            break;
          case 1:
            rosterData.numberOfPlayersOnRoster = reader.ReadInt32();
            break;
          case 2:
            rosterData.rosterType = resolver.GetFormatterWithVerify<RosterType>().Deserialize(ref reader, options);
            break;
          case 3:
            rosterData.defaultPlayers = resolver.GetFormatterWithVerify<Dictionary<string, int>>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return rosterData;
    }
  }
}
