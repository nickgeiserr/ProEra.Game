// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TeamDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class TeamDataFormatter : IMessagePackFormatter<TeamData>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      TeamData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(22);
        writer.WriteNil();
        writer.WriteNil();
        writer.WriteNil();
        writer.WriteNil();
        writer.WriteNil();
        writer.WriteNil();
        resolver.GetFormatterWithVerify<DepthChart>().Serialize(ref writer, value.SavedTeamDepthChart, options);
        resolver.GetFormatterWithVerify<CoachData[]>().Serialize(ref writer, value.CoachingStaff, options);
        resolver.GetFormatterWithVerify<RosterData>().Serialize(ref writer, value.MainRoster, options);
        resolver.GetFormatterWithVerify<RosterData>().Serialize(ref writer, value.PracticeSquad, options);
        resolver.GetFormatterWithVerify<TeamSeasonStats>().Serialize(ref writer, value.CurrentSeasonStats, options);
        resolver.GetFormatterWithVerify<TeamFile>().Serialize(ref writer, value.SavedTeamFile, options);
        resolver.GetFormatterWithVerify<TeamPlayCalling>().Serialize(ref writer, value.PlayCalling, options);
        resolver.GetFormatterWithVerify<TeamGameStats>().Serialize(ref writer, value.CurrentGameStats, options);
        resolver.GetFormatterWithVerify<List<TeamSeasonStats>>().Serialize(ref writer, value.AllSeasonStats, options);
        resolver.GetFormatterWithVerify<CustomLogoData>().Serialize(ref writer, value.CustomLogo, options);
        writer.WriteNil();
        writer.WriteNil();
        writer.WriteNil();
        writer.WriteNil();
        writer.WriteNil();
        writer.Write(value.TeamIndex);
      }
    }

    public TeamData Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (TeamData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      TeamData teamData = new TeamData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 6:
            teamData.SavedTeamDepthChart = resolver.GetFormatterWithVerify<DepthChart>().Deserialize(ref reader, options);
            break;
          case 7:
            teamData.CoachingStaff = resolver.GetFormatterWithVerify<CoachData[]>().Deserialize(ref reader, options);
            break;
          case 8:
            teamData.MainRoster = resolver.GetFormatterWithVerify<RosterData>().Deserialize(ref reader, options);
            break;
          case 9:
            teamData.PracticeSquad = resolver.GetFormatterWithVerify<RosterData>().Deserialize(ref reader, options);
            break;
          case 10:
            teamData.CurrentSeasonStats = resolver.GetFormatterWithVerify<TeamSeasonStats>().Deserialize(ref reader, options);
            break;
          case 11:
            teamData.SavedTeamFile = resolver.GetFormatterWithVerify<TeamFile>().Deserialize(ref reader, options);
            break;
          case 12:
            teamData.PlayCalling = resolver.GetFormatterWithVerify<TeamPlayCalling>().Deserialize(ref reader, options);
            break;
          case 13:
            teamData.CurrentGameStats = resolver.GetFormatterWithVerify<TeamGameStats>().Deserialize(ref reader, options);
            break;
          case 14:
            teamData.AllSeasonStats = resolver.GetFormatterWithVerify<List<TeamSeasonStats>>().Deserialize(ref reader, options);
            break;
          case 15:
            teamData.CustomLogo = resolver.GetFormatterWithVerify<CustomLogoData>().Deserialize(ref reader, options);
            break;
          case 21:
            teamData.TeamIndex = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return teamData;
    }
  }
}
