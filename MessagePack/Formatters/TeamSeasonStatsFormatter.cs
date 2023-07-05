// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TeamSeasonStatsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class TeamSeasonStatsFormatter : 
    IMessagePackFormatter<TeamSeasonStats>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      TeamSeasonStats value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(34);
        writer.Write(value.wins);
        writer.Write(value.losses);
        writer.Write(value.teamPlusMinus);
        writer.Write(value.streak);
        writer.Write(value.turnovers);
        writer.Write(value.tacklesForLoss);
        writer.Write(value.interceptions);
        writer.Write(value.forcedFumbles);
        writer.Write(value.fumbleRecoveries);
        writer.Write(value.passYards);
        writer.Write(value.rushYards);
        writer.Write(value.totalYards);
        writer.Write(value.passYardsAllowed);
        writer.Write(value.rushYardsAllowed);
        writer.Write(value.totalYardsAllowed);
        writer.Write(value.sacks);
        writer.Write(value.turnoverMargin);
        writer.Write(value.pointsScored);
        writer.Write(value.pointsAllowed);
        writer.Write(value.confWins);
        writer.Write(value.confLosses);
        writer.Write(value.divWins);
        writer.Write(value.divLosses);
        writer.Write(value.touchdowns);
        writer.Write(value.totalPassPlays);
        writer.Write(value.droppedPasses);
        writer.Write(value.rivalWins);
        writer.Write(value.redZoneAppearances);
        writer.Write(value.redZoneTouchdowns);
        writer.Write(value.IsSuperbowlWinner);
        resolver.GetFormatterWithVerify<HashSet<int>>().Serialize(ref writer, value.TeamsBeaten, options);
        writer.Write(value.TeamIndex);
        writer.Write(value.IsConferenceChampion);
        writer.Write(value.qbInts);
      }
    }

    public TeamSeasonStats Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (TeamSeasonStats) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      TeamSeasonStats teamSeasonStats = new TeamSeasonStats();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            teamSeasonStats.wins = reader.ReadInt32();
            break;
          case 1:
            teamSeasonStats.losses = reader.ReadInt32();
            break;
          case 2:
            teamSeasonStats.teamPlusMinus = reader.ReadInt32();
            break;
          case 3:
            teamSeasonStats.streak = reader.ReadInt32();
            break;
          case 4:
            teamSeasonStats.turnovers = reader.ReadInt32();
            break;
          case 5:
            teamSeasonStats.tacklesForLoss = reader.ReadInt32();
            break;
          case 6:
            teamSeasonStats.interceptions = reader.ReadInt32();
            break;
          case 7:
            teamSeasonStats.forcedFumbles = reader.ReadInt32();
            break;
          case 8:
            teamSeasonStats.fumbleRecoveries = reader.ReadInt32();
            break;
          case 9:
            teamSeasonStats.passYards = reader.ReadInt32();
            break;
          case 10:
            teamSeasonStats.rushYards = reader.ReadInt32();
            break;
          case 11:
            teamSeasonStats.totalYards = reader.ReadInt32();
            break;
          case 12:
            teamSeasonStats.passYardsAllowed = reader.ReadInt32();
            break;
          case 13:
            teamSeasonStats.rushYardsAllowed = reader.ReadInt32();
            break;
          case 14:
            teamSeasonStats.totalYardsAllowed = reader.ReadInt32();
            break;
          case 15:
            teamSeasonStats.sacks = reader.ReadInt32();
            break;
          case 16:
            teamSeasonStats.turnoverMargin = reader.ReadInt32();
            break;
          case 17:
            teamSeasonStats.pointsScored = reader.ReadInt32();
            break;
          case 18:
            teamSeasonStats.pointsAllowed = reader.ReadInt32();
            break;
          case 19:
            teamSeasonStats.confWins = reader.ReadInt32();
            break;
          case 20:
            teamSeasonStats.confLosses = reader.ReadInt32();
            break;
          case 21:
            teamSeasonStats.divWins = reader.ReadInt32();
            break;
          case 22:
            teamSeasonStats.divLosses = reader.ReadInt32();
            break;
          case 23:
            teamSeasonStats.touchdowns = reader.ReadInt32();
            break;
          case 24:
            teamSeasonStats.totalPassPlays = reader.ReadInt32();
            break;
          case 25:
            teamSeasonStats.droppedPasses = reader.ReadInt32();
            break;
          case 26:
            teamSeasonStats.rivalWins = reader.ReadInt32();
            break;
          case 27:
            teamSeasonStats.redZoneAppearances = reader.ReadInt32();
            break;
          case 28:
            teamSeasonStats.redZoneTouchdowns = reader.ReadInt32();
            break;
          case 29:
            teamSeasonStats.IsSuperbowlWinner = reader.ReadBoolean();
            break;
          case 30:
            teamSeasonStats.TeamsBeaten = resolver.GetFormatterWithVerify<HashSet<int>>().Deserialize(ref reader, options);
            break;
          case 31:
            teamSeasonStats.TeamIndex = reader.ReadInt32();
            break;
          case 32:
            teamSeasonStats.IsConferenceChampion = reader.ReadBoolean();
            break;
          case 33:
            teamSeasonStats.qbInts = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return teamSeasonStats;
    }
  }
}
