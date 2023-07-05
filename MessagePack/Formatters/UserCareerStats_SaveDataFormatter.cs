// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.UserCareerStats_SaveDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class UserCareerStats_SaveDataFormatter : 
    IMessagePackFormatter<UserCareerStats.SaveData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      UserCareerStats.SaveData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(2);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        resolver.GetFormatterWithVerify<StatSet>().Serialize(ref writer, value.Stats, options);
      }
    }

    public UserCareerStats.SaveData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (UserCareerStats.SaveData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      UserCareerStats.SaveData saveData = new UserCareerStats.SaveData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveData.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveData.Stats = resolver.GetFormatterWithVerify<StatSet>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveData;
    }
  }
}
