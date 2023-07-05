// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ProEra.Game.Achievements.AchievementFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Achievements;

namespace MessagePack.Formatters.ProEra.Game.Achievements
{
  public sealed class AchievementFormatter : 
    IMessagePackFormatter<Achievement>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Achievement value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(9);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Name, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Description, options);
        resolver.GetFormatterWithVerify<AchievementTier>().Serialize(ref writer, value.Tiers, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Type, options);
        writer.Write(value._currentValue);
        writer.Write(value.CurrentTier);
        resolver.GetFormatterWithVerify<double[]>().Serialize(ref writer, value.Timestamps, options);
        resolver.GetFormatterWithVerify<string[]>().Serialize(ref writer, value.Platforms, options);
        writer.Write(value.Acknowledged);
      }
    }

    public Achievement Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Achievement) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Achievement achievement = new Achievement();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            achievement.Name = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 1:
            achievement.Description = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            achievement.Tiers = resolver.GetFormatterWithVerify<AchievementTier>().Deserialize(ref reader, options);
            break;
          case 3:
            achievement.Type = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 4:
            achievement._currentValue = reader.ReadInt32();
            break;
          case 5:
            achievement.CurrentTier = reader.ReadInt32();
            break;
          case 6:
            achievement.Timestamps = resolver.GetFormatterWithVerify<double[]>().Deserialize(ref reader, options);
            break;
          case 7:
            achievement.Platforms = resolver.GetFormatterWithVerify<string[]>().Deserialize(ref reader, options);
            break;
          case 8:
            achievement.Acknowledged = reader.ReadBoolean();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return achievement;
    }
  }
}
