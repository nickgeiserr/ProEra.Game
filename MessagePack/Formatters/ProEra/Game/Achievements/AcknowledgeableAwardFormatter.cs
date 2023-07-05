// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ProEra.Game.Achievements.AcknowledgeableAwardFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Achievements;

namespace MessagePack.Formatters.ProEra.Game.Achievements
{
  public sealed class AcknowledgeableAwardFormatter : 
    IMessagePackFormatter<AcknowledgeableAward>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      AcknowledgeableAward value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(3);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Name, options);
        writer.Write(value.Acknowledged);
        writer.Write(value.HasBeenAwarded);
      }
    }

    public AcknowledgeableAward Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (AcknowledgeableAward) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      AcknowledgeableAward acknowledgeableAward = new AcknowledgeableAward();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            acknowledgeableAward.Name = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 1:
            acknowledgeableAward.Acknowledged = reader.ReadBoolean();
            break;
          case 2:
            acknowledgeableAward.HasBeenAwarded = reader.ReadBoolean();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return acknowledgeableAward;
    }
  }
}
