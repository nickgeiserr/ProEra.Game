// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.MiniCampDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12;

namespace MessagePack.Formatters
{
  public sealed class MiniCampDataFormatter : 
    IMessagePackFormatter<MiniCampData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      MiniCampData value,
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
        resolver.GetFormatterWithVerify<EMiniCampTourType>().Serialize(ref writer, value.MiniCampTourType, options);
        resolver.GetFormatterWithVerify<EGameMode>().Serialize(ref writer, value.GameplayMode, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.MiniCampTourName, options);
        writer.Write(value.CurrentLevel);
        resolver.GetFormatterWithVerify<MiniCampEntry[]>().Serialize(ref writer, value.MiniCampEntries, options);
      }
    }

    public MiniCampData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (MiniCampData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      MiniCampData miniCampData = new MiniCampData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            miniCampData.MiniCampTourType = resolver.GetFormatterWithVerify<EMiniCampTourType>().Deserialize(ref reader, options);
            break;
          case 1:
            miniCampData.GameplayMode = resolver.GetFormatterWithVerify<EGameMode>().Deserialize(ref reader, options);
            break;
          case 2:
            miniCampData.MiniCampTourName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 3:
            miniCampData.CurrentLevel = reader.ReadInt32();
            break;
          case 4:
            miniCampData.MiniCampEntries = resolver.GetFormatterWithVerify<MiniCampEntry[]>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return miniCampData;
    }
  }
}
