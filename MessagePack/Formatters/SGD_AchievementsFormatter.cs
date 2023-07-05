// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SGD_AchievementsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class SGD_AchievementsFormatter : 
    IMessagePackFormatter<SGD_Achievements>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SGD_Achievements value,
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
        writer.Write(value.numberOfAchievements);
        resolver.GetFormatterWithVerify<bool[]>().Serialize(ref writer, value.achievementEarnedStatus, options);
      }
    }

    public SGD_Achievements Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SGD_Achievements) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num1 = reader.ReadArrayHeader();
      string f = (string) null;
      double num2 = 0.0;
      string str1 = (string) null;
      string str2 = (string) null;
      bool flag = false;
      int num3 = 0;
      bool[] flagArray = (bool[]) null;
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
            num3 = reader.ReadInt32();
            break;
          case 6:
            flagArray = resolver.GetFormatterWithVerify<bool[]>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      SGD_Achievements sgdAchievements = new SGD_Achievements(f);
      if (num1 > 1)
      {
        sgdAchievements.timeStamp = num2;
        if (num1 > 2)
        {
          sgdAchievements.gameVersion = str1;
          if (num1 > 3)
          {
            sgdAchievements.userName = str2;
            if (num1 > 4)
            {
              sgdAchievements.markAsDeleted = flag;
              if (num1 > 5)
              {
                sgdAchievements.numberOfAchievements = num3;
                if (num1 > 6)
                  sgdAchievements.achievementEarnedStatus = flagArray;
              }
            }
          }
        }
      }
      --reader.Depth;
      return sgdAchievements;
    }
  }
}
