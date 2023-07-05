// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SGD_TeamDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class SGD_TeamDataFormatter : 
    IMessagePackFormatter<SGD_TeamData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SGD_TeamData value,
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
        resolver.GetFormatterWithVerify<TeamData>().Serialize(ref writer, value.teamData, options);
      }
    }

    public SGD_TeamData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SGD_TeamData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num1 = reader.ReadArrayHeader();
      string f = (string) null;
      double num2 = 0.0;
      string str1 = (string) null;
      string str2 = (string) null;
      bool flag = false;
      TeamData teamData = (TeamData) null;
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
            teamData = resolver.GetFormatterWithVerify<TeamData>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      SGD_TeamData sgdTeamData = new SGD_TeamData(f);
      if (num1 > 1)
      {
        sgdTeamData.timeStamp = num2;
        if (num1 > 2)
        {
          sgdTeamData.gameVersion = str1;
          if (num1 > 3)
          {
            sgdTeamData.userName = str2;
            if (num1 > 4)
            {
              sgdTeamData.markAsDeleted = flag;
              if (num1 > 5)
                sgdTeamData.teamData = teamData;
            }
          }
        }
      }
      --reader.Depth;
      return sgdTeamData;
    }
  }
}
