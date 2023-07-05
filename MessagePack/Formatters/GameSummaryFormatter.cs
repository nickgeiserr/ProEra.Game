// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.GameSummaryFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class GameSummaryFormatter : 
    IMessagePackFormatter<GameSummary>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      GameSummary value,
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
        writer.Write(value.TeamIndex);
        resolver.GetFormatterWithVerify<TeamGameStats>().Serialize(ref writer, value.TeamGameStats, options);
        resolver.GetFormatterWithVerify<PlayerData_Basic[]>().Serialize(ref writer, value.PlayerData, options);
        resolver.GetFormatterWithVerify<PlayerStats[]>().Serialize(ref writer, value.PlayerStats, options);
      }
    }

    public GameSummary Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (GameSummary) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      GameSummary gameSummary = new GameSummary();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            gameSummary.TeamIndex = reader.ReadInt32();
            break;
          case 1:
            gameSummary.TeamGameStats = resolver.GetFormatterWithVerify<TeamGameStats>().Deserialize(ref reader, options);
            break;
          case 2:
            gameSummary.PlayerData = resolver.GetFormatterWithVerify<PlayerData_Basic[]>().Deserialize(ref reader, options);
            break;
          case 3:
            gameSummary.PlayerStats = resolver.GetFormatterWithVerify<PlayerStats[]>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return gameSummary;
    }
  }
}
