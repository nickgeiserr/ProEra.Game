// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.InjuryFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class InjuryFormatter : IMessagePackFormatter<Injury>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Injury value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(8);
        resolver.GetFormatterWithVerify<InjuryType>().Serialize(ref writer, value.injuryType, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.recoveryTimeframe, options);
        writer.Write(value.weeksToRecover);
        writer.Write(value.playsToRecover);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.injuryCategory, options);
        writer.Write(value.weeksRemaining);
        writer.Write(value.playsRemaining);
        resolver.GetFormatterWithVerify<StartingPosition>().Serialize(ref writer, value.startingPosition, options);
      }
    }

    public Injury Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Injury) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Injury injury = new Injury();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            injury.injuryType = resolver.GetFormatterWithVerify<InjuryType>().Deserialize(ref reader, options);
            break;
          case 1:
            injury.recoveryTimeframe = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            injury.weeksToRecover = reader.ReadInt32();
            break;
          case 3:
            injury.playsToRecover = reader.ReadInt32();
            break;
          case 4:
            injury.injuryCategory = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 5:
            injury.weeksRemaining = reader.ReadInt32();
            break;
          case 6:
            injury.playsRemaining = reader.ReadInt32();
            break;
          case 7:
            injury.startingPosition = resolver.GetFormatterWithVerify<StartingPosition>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return injury;
    }
  }
}
