// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.AwardFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class AwardFormatter : IMessagePackFormatter<Award>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Award value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(12);
        writer.Write(value.teamIndex);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.teamAbbreviation, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.playerName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.playerFullName, options);
        writer.Write(value.playerIndex);
        writer.Write(value.playerPortrait);
        writer.Write(value.skinValue);
        writer.Write(value.playerNumber);
        resolver.GetFormatterWithVerify<AwardType>().Serialize(ref writer, value.awardType, options);
        resolver.GetFormatterWithVerify<Position>().Serialize(ref writer, value.position, options);
        resolver.GetFormatterWithVerify<string[]>().Serialize(ref writer, value.statHighlights, options);
        writer.Write(value.statScore);
      }
    }

    public Award Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Award) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Award award = new Award();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            award.teamIndex = reader.ReadInt32();
            break;
          case 1:
            award.teamAbbreviation = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            award.playerName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 3:
            award.playerFullName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 4:
            award.playerIndex = reader.ReadInt32();
            break;
          case 5:
            award.playerPortrait = reader.ReadInt32();
            break;
          case 6:
            award.skinValue = reader.ReadInt32();
            break;
          case 7:
            award.playerNumber = reader.ReadInt32();
            break;
          case 8:
            award.awardType = resolver.GetFormatterWithVerify<AwardType>().Deserialize(ref reader, options);
            break;
          case 9:
            award.position = resolver.GetFormatterWithVerify<Position>().Deserialize(ref reader, options);
            break;
          case 10:
            award.statHighlights = resolver.GetFormatterWithVerify<string[]>().Deserialize(ref reader, options);
            break;
          case 11:
            award.statScore = reader.ReadSingle();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return award;
    }
  }
}
