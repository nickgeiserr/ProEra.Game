// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ProEra.Game.Achievements.SaveAchievementsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Achievements;
using System.Collections.Generic;

namespace MessagePack.Formatters.ProEra.Game.Achievements
{
  public sealed class SaveAchievementsFormatter : 
    IMessagePackFormatter<SaveAchievements>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SaveAchievements value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(4);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        resolver.GetFormatterWithVerify<Dictionary<string, Achievement>>().Serialize(ref writer, value.Achievements, options);
        resolver.GetFormatterWithVerify<Dictionary<string, AcknowledgeableAward>>().Serialize(ref writer, value.TeamsDefeatedByIndex, options);
        resolver.GetFormatterWithVerify<Dictionary<string, AcknowledgeableAward>>().Serialize(ref writer, value.SuperBowlAwardsByTeam, options);
      }
    }

    public SaveAchievements Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SaveAchievements) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      SaveAchievements saveAchievements = new SaveAchievements();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveAchievements.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveAchievements.Achievements = resolver.GetFormatterWithVerify<Dictionary<string, Achievement>>().Deserialize(ref reader, options);
            break;
          case 2:
            saveAchievements.TeamsDefeatedByIndex = resolver.GetFormatterWithVerify<Dictionary<string, AcknowledgeableAward>>().Deserialize(ref reader, options);
            break;
          case 3:
            saveAchievements.SuperBowlAwardsByTeam = resolver.GetFormatterWithVerify<Dictionary<string, AcknowledgeableAward>>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveAchievements;
    }
  }
}
