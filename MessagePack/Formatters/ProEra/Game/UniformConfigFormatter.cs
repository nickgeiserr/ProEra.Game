// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ProEra.Game.UniformConfigFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;

namespace MessagePack.Formatters.ProEra.Game
{
  public sealed class UniformConfigFormatter : 
    IMessagePackFormatter<UniformConfig>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      UniformConfig value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(18);
        writer.Write(value.IsCustomBaseUniform);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.UniformSetName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.HelmetName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.JerseyName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.PantName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.NumberFontIndex, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.NumberFillColor, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.NumberOutline1Color, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.NumberOutline2Color, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.LetterFontIndex, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.LetterFillColor, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.LetterOutlineColor, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.VisorColor, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.ArmSleeveColor, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.ArmBandColor, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.HasSleeveNumber, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.HasShoulderNumber, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.HelmetType, options);
      }
    }

    public UniformConfig Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (UniformConfig) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      UniformConfig uniformConfig = new UniformConfig();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            uniformConfig.IsCustomBaseUniform = reader.ReadBoolean();
            break;
          case 1:
            uniformConfig.UniformSetName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            uniformConfig.HelmetName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 3:
            uniformConfig.JerseyName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 4:
            uniformConfig.PantName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 5:
            uniformConfig.NumberFontIndex = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 6:
            uniformConfig.NumberFillColor = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 7:
            uniformConfig.NumberOutline1Color = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 8:
            uniformConfig.NumberOutline2Color = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 9:
            uniformConfig.LetterFontIndex = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 10:
            uniformConfig.LetterFillColor = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 11:
            uniformConfig.LetterOutlineColor = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 12:
            uniformConfig.VisorColor = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 13:
            uniformConfig.ArmSleeveColor = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 14:
            uniformConfig.ArmBandColor = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 15:
            uniformConfig.HasSleeveNumber = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 16:
            uniformConfig.HasShoulderNumber = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 17:
            uniformConfig.HelmetType = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return uniformConfig;
    }
  }
}
