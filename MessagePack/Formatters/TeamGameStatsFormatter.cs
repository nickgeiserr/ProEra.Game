// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TeamGameStatsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class TeamGameStatsFormatter : 
    IMessagePackFormatter<TeamGameStats>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      TeamGameStats value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(33);
        writer.Write(value.PlayerScore);
        writer.Write(value.RushYards);
        writer.Write(value.PassYards);
        writer.Write(value.Sacks);
        writer.Write(value.DroppedPasses);
        writer.Write(value.Turnovers);
        writer.Write(value.ThirdDownAtt);
        writer.Write(value.ThirdDownSuc);
        writer.Write(value.Ints);
        writer.Write(value.ConsecutiveRunPlays);
        writer.Write(value.ConsecutivePassPlays);
        writer.Write(value.TotalRunPlays);
        writer.Write(value.TotalPassPlays);
        writer.Write(value.Penalties);
        writer.Write(value.PenaltyYards);
        writer.Write(value.TacklesForLoss);
        writer.Write(value.LongestDrive);
        writer.Write(value.FumbleRecoveries);
        writer.Write(value.Interceptions);
        writer.Write(value.ForcedFumbles);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.ScoreByQuarter, options);
        writer.Write(value.TotalPossessionTime);
        writer.Write(value.VFormationSatisfied);
        writer.Write(value.MaxDriveFirstDowns);
        writer.Write(value.RedZoneAppearances);
        writer.Write(value.RedZoneTouchdowns);
        writer.Write(value.RushTDs);
        writer.Write(value.RushFiveYards);
        writer.Write(value.TwoPointConversions);
        writer.Write(value.Touchdowns);
        writer.Write(value.TotalFirstDowns);
        resolver.GetFormatterWithVerify<List<DriveEndType>>().Serialize(ref writer, value.PreviousDriveResults, options);
        resolver.GetFormatterWithVerify<Dictionary<string, float>>().Serialize(ref writer, value.YardGainByPlayType, options);
      }
    }

    public TeamGameStats Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (TeamGameStats) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      TeamGameStats teamGameStats = new TeamGameStats();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            teamGameStats.PlayerScore = reader.ReadInt32();
            break;
          case 1:
            teamGameStats.RushYards = reader.ReadInt32();
            break;
          case 2:
            teamGameStats.PassYards = reader.ReadInt32();
            break;
          case 3:
            teamGameStats.Sacks = reader.ReadInt32();
            break;
          case 4:
            teamGameStats.DroppedPasses = reader.ReadInt32();
            break;
          case 5:
            teamGameStats.Turnovers = reader.ReadInt32();
            break;
          case 6:
            teamGameStats.ThirdDownAtt = reader.ReadInt32();
            break;
          case 7:
            teamGameStats.ThirdDownSuc = reader.ReadInt32();
            break;
          case 8:
            teamGameStats.Ints = reader.ReadInt32();
            break;
          case 9:
            teamGameStats.ConsecutiveRunPlays = reader.ReadInt32();
            break;
          case 10:
            teamGameStats.ConsecutivePassPlays = reader.ReadInt32();
            break;
          case 11:
            teamGameStats.TotalRunPlays = reader.ReadInt32();
            break;
          case 12:
            teamGameStats.TotalPassPlays = reader.ReadInt32();
            break;
          case 13:
            teamGameStats.Penalties = reader.ReadInt32();
            break;
          case 14:
            teamGameStats.PenaltyYards = reader.ReadInt32();
            break;
          case 15:
            teamGameStats.TacklesForLoss = reader.ReadInt32();
            break;
          case 16:
            teamGameStats.LongestDrive = reader.ReadInt32();
            break;
          case 17:
            teamGameStats.FumbleRecoveries = reader.ReadInt32();
            break;
          case 18:
            teamGameStats.Interceptions = reader.ReadInt32();
            break;
          case 19:
            teamGameStats.ForcedFumbles = reader.ReadInt32();
            break;
          case 20:
            resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 21:
            teamGameStats.TotalPossessionTime = reader.ReadSingle();
            break;
          case 22:
            teamGameStats.VFormationSatisfied = reader.ReadBoolean();
            break;
          case 23:
            teamGameStats.MaxDriveFirstDowns = reader.ReadInt32();
            break;
          case 24:
            teamGameStats.RedZoneAppearances = reader.ReadInt32();
            break;
          case 25:
            teamGameStats.RedZoneTouchdowns = reader.ReadInt32();
            break;
          case 26:
            teamGameStats.RushTDs = reader.ReadInt32();
            break;
          case 27:
            teamGameStats.RushFiveYards = reader.ReadInt32();
            break;
          case 28:
            teamGameStats.TwoPointConversions = reader.ReadInt32();
            break;
          case 29:
            teamGameStats.Touchdowns = reader.ReadInt32();
            break;
          case 30:
            teamGameStats.TotalFirstDowns = reader.ReadInt32();
            break;
          case 31:
            teamGameStats.PreviousDriveResults = resolver.GetFormatterWithVerify<List<DriveEndType>>().Deserialize(ref reader, options);
            break;
          case 32:
            teamGameStats.YardGainByPlayType = resolver.GetFormatterWithVerify<Dictionary<string, float>>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return teamGameStats;
    }
  }
}
