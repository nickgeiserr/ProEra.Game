// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.UserCareerStatsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class UserCareerStatsFormatter : 
    IMessagePackFormatter<UserCareerStats>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      UserCareerStats value,
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
        resolver.GetFormatterWithVerify<StatSet>().Serialize(ref writer, value.Stats, options);
      }
    }

    public UserCareerStats Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (UserCareerStats) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      UserCareerStats userCareerStats = new UserCareerStats();
      for (int index = 0; index < num; ++index)
      {
        if (index == 0)
          userCareerStats.Stats = resolver.GetFormatterWithVerify<StatSet>().Deserialize(ref reader, options);
        else
          reader.Skip();
      }
      --reader.Depth;
      return userCareerStats;
    }
  }
}
