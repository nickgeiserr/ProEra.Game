// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.CustomLogoDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class CustomLogoDataFormatter : 
    IMessagePackFormatter<CustomLogoData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      CustomLogoData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(6);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.SourceLogoName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.SourceAssetBundle, options);
        resolver.GetFormatterWithVerify<float[]>().Serialize(ref writer, value.RedReplacement, options);
        resolver.GetFormatterWithVerify<float[]>().Serialize(ref writer, value.GreenReplacement, options);
        resolver.GetFormatterWithVerify<float[]>().Serialize(ref writer, value.BlueReplacement, options);
        resolver.GetFormatterWithVerify<float[]>().Serialize(ref writer, value.WhiteReplacement, options);
      }
    }

    public CustomLogoData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (CustomLogoData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      CustomLogoData customLogoData = new CustomLogoData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 1:
            resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            resolver.GetFormatterWithVerify<float[]>().Deserialize(ref reader, options);
            break;
          case 3:
            resolver.GetFormatterWithVerify<float[]>().Deserialize(ref reader, options);
            break;
          case 4:
            resolver.GetFormatterWithVerify<float[]>().Deserialize(ref reader, options);
            break;
          case 5:
            resolver.GetFormatterWithVerify<float[]>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return customLogoData;
    }
  }
}
