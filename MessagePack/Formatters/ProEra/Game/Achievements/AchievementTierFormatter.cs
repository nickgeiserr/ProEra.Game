// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ProEra.Game.Achievements.AchievementTierFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Achievements;

namespace MessagePack.Formatters.ProEra.Game.Achievements
{
  public sealed class AchievementTierFormatter : 
    IMessagePackFormatter<AchievementTier>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      AchievementTier value,
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
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value._levels, options);
        writer.Write(value.Count);
      }
    }

    public AchievementTier Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (AchievementTier) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num1 = reader.ReadArrayHeader();
      int[] levels = (int[]) null;
      int num2 = 0;
      for (int index = 0; index < num1; ++index)
      {
        switch (index)
        {
          case 0:
            levels = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 1:
            num2 = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      AchievementTier achievementTier = new AchievementTier(levels);
      if (num1 > 1)
        achievementTier.Count = num2;
      --reader.Depth;
      return achievementTier;
    }
  }
}
