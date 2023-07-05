// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.RecordHolderFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class RecordHolderFormatter : 
    IMessagePackFormatter<RecordHolder>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      RecordHolder value,
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
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.RecordName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.RecordHolderName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.RecordHolderTeam, options);
        writer.Write(value.Year);
        writer.Write(value.RecordValue);
      }
    }

    public RecordHolder Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (RecordHolder) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      RecordHolder recordHolder = new RecordHolder();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            recordHolder.RecordName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 1:
            recordHolder.RecordHolderName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            recordHolder.RecordHolderTeam = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 3:
            recordHolder.Year = reader.ReadInt32();
            break;
          case 4:
            recordHolder.RecordValue = reader.ReadSingle();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return recordHolder;
    }
  }
}
