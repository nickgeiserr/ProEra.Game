// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TB12.ProfileProgressFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using TB12;

namespace MessagePack.Formatters.TB12
{
  public sealed class ProfileProgressFormatter : 
    IMessagePackFormatter<ProfileProgress>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      ProfileProgress value,
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
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.Points);
        writer.Write(value.Level);
        writer.Write(value.Progress);
        writer.Write(value.MaxLevelCompleted);
        resolver.GetFormatterWithVerify<List<ProfileProgress.Entry>>().Serialize(ref writer, value.Datas, options);
        resolver.GetFormatterWithVerify<Dictionary<int, ProfileProgress.Entry>>().Serialize(ref writer, value.DataEntries, options);
      }
    }

    public ProfileProgress Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (ProfileProgress) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      ProfileProgress profileProgress = new ProfileProgress();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            profileProgress.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            profileProgress.Points = reader.ReadInt32();
            break;
          case 2:
            profileProgress.Level = reader.ReadInt32();
            break;
          case 3:
            profileProgress.Progress = reader.ReadSingle();
            break;
          case 4:
            profileProgress.MaxLevelCompleted = reader.ReadInt32();
            break;
          case 5:
            profileProgress.Datas = resolver.GetFormatterWithVerify<List<ProfileProgress.Entry>>().Deserialize(ref reader, options);
            break;
          case 6:
            resolver.GetFormatterWithVerify<Dictionary<int, ProfileProgress.Entry>>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return profileProgress;
    }
  }
}
