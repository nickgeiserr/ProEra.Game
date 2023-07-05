// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SGD_TeamChangesFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class SGD_TeamChangesFormatter : 
    IMessagePackFormatter<SGD_TeamChanges>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SGD_TeamChanges value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(7);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.fileName, options);
        writer.Write(value.timeStamp);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.gameVersion, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.userName, options);
        writer.Write(value.markAsDeleted);
        resolver.GetFormatterWithVerify<SGD_Uniforms[]>().Serialize(ref writer, value.customUniforms, options);
        resolver.GetFormatterWithVerify<SGD_TeamData[]>().Serialize(ref writer, value.teamDataOverride, options);
      }
    }

    public SGD_TeamChanges Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SGD_TeamChanges) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num1 = reader.ReadArrayHeader();
      string f = (string) null;
      double num2 = 0.0;
      string str1 = (string) null;
      string str2 = (string) null;
      bool flag = false;
      SGD_Uniforms[] sgdUniformsArray = (SGD_Uniforms[]) null;
      SGD_TeamData[] sgdTeamDataArray = (SGD_TeamData[]) null;
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
            sgdUniformsArray = resolver.GetFormatterWithVerify<SGD_Uniforms[]>().Deserialize(ref reader, options);
            break;
          case 6:
            sgdTeamDataArray = resolver.GetFormatterWithVerify<SGD_TeamData[]>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      SGD_TeamChanges sgdTeamChanges = new SGD_TeamChanges(f);
      if (num1 > 1)
      {
        sgdTeamChanges.timeStamp = num2;
        if (num1 > 2)
        {
          sgdTeamChanges.gameVersion = str1;
          if (num1 > 3)
          {
            sgdTeamChanges.userName = str2;
            if (num1 > 4)
            {
              sgdTeamChanges.markAsDeleted = flag;
              if (num1 > 5)
              {
                sgdTeamChanges.customUniforms = sgdUniformsArray;
                if (num1 > 6)
                  sgdTeamChanges.teamDataOverride = sgdTeamDataArray;
              }
            }
          }
        }
      }
      --reader.Depth;
      return sgdTeamChanges;
    }
  }
}
