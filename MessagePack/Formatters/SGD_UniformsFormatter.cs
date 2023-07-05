// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SGD_UniformsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class SGD_UniformsFormatter : 
    IMessagePackFormatter<SGD_Uniforms>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SGD_Uniforms value,
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
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.fileName, options);
        writer.Write(value.timeStamp);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.gameVersion, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.userName, options);
        writer.Write(value.markAsDeleted);
        resolver.GetFormatterWithVerify<List<UniformConfig>>().Serialize(ref writer, value.uniforms, options);
      }
    }

    public SGD_Uniforms Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SGD_Uniforms) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num1 = reader.ReadArrayHeader();
      string f = (string) null;
      double num2 = 0.0;
      string str1 = (string) null;
      string str2 = (string) null;
      bool flag = false;
      List<UniformConfig> uniformConfigList = (List<UniformConfig>) null;
      for (int index = 0; index < num1; ++index)
      {
        switch (index)
        {
          case 0:
            f = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 1:
            num2 = reader.ReadDouble();
            break;
          case 2:
            str1 = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 3:
            str2 = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 4:
            flag = reader.ReadBoolean();
            break;
          case 5:
            uniformConfigList = resolver.GetFormatterWithVerify<List<UniformConfig>>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      SGD_Uniforms sgdUniforms = new SGD_Uniforms(f);
      if (num1 > 1)
      {
        sgdUniforms.timeStamp = num2;
        if (num1 > 2)
        {
          sgdUniforms.gameVersion = str1;
          if (num1 > 3)
          {
            sgdUniforms.userName = str2;
            if (num1 > 4)
            {
              sgdUniforms.markAsDeleted = flag;
              if (num1 > 5)
                sgdUniforms.uniforms = uniformConfigList;
            }
          }
        }
      }
      --reader.Depth;
      return sgdUniforms;
    }
  }
}
