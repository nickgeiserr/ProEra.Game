// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.StatSetFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class StatSetFormatter : IMessagePackFormatter<StatSet>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      StatSet value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(43);
        writer.Write(value.wins);
        writer.Write(value.losses);
        writer.Write(value.teamPlusMinus);
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
        writer.Write(value.confChampionships);
        writer.Write(value.divWins);
        writer.Write(value.divLosses);
        writer.Write(value.divChampionships);
        writer.Write(value.touchdowns);
        writer.Write(value.totalPassPlays);
        writer.Write(value.droppedPasses);
        writer.Write(value.rivalWins);
        writer.Write(value.redZoneAppearances);
        writer.Write(value.redZoneTouchdowns);
        writer.Write(value.superBowlWins);
        writer.Write(value.mvpAwards);
        writer.Write(value.superBowlMvpAwards);
        writer.Write(value.offesensivePowAwards);
        writer.Write(value.defensivePowAwards);
        writer.Write(value.playoffAppearances);
        writer.Write(value.playoffWins);
        writer.Write(value.playoffLosses);
        resolver.GetFormatterWithVerify<HashSet<int>>().Serialize(ref writer, value.teamsBeaten, options);
        resolver.GetFormatterWithVerify<HashSet<int>>().Serialize(ref writer, value.superBowlWiningTeams, options);
        writer.Write(value.touchdownPasses);
        writer.Write(value.passCompletions);
        writer.Write(value.qbInts);
      }
    }

    public StatSet Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (StatSet) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      StatSet statSet = new StatSet();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            statSet.wins = reader.ReadInt32();
            break;
          case 1:
            statSet.losses = reader.ReadInt32();
            break;
          case 2:
            statSet.teamPlusMinus = reader.ReadInt32();
            break;
          case 3:
            statSet.turnovers = reader.ReadInt32();
            break;
          case 4:
            statSet.tacklesForLoss = reader.ReadInt32();
            break;
          case 5:
            statSet.interceptions = reader.ReadInt32();
            break;
          case 6:
            statSet.forcedFumbles = reader.ReadInt32();
            break;
          case 7:
            statSet.fumbleRecoveries = reader.ReadInt32();
            break;
          case 8:
            statSet.passYards = reader.ReadInt32();
            break;
          case 9:
            statSet.rushYards = reader.ReadInt32();
            break;
          case 10:
            statSet.totalYards = reader.ReadInt32();
            break;
          case 11:
            statSet.passYardsAllowed = reader.ReadInt32();
            break;
          case 12:
            statSet.rushYardsAllowed = reader.ReadInt32();
            break;
          case 13:
            statSet.totalYardsAllowed = reader.ReadInt32();
            break;
          case 14:
            statSet.sacks = reader.ReadInt32();
            break;
          case 15:
            statSet.turnoverMargin = reader.ReadInt32();
            break;
          case 16:
            statSet.pointsScored = reader.ReadInt32();
            break;
          case 17:
            statSet.pointsAllowed = reader.ReadInt32();
            break;
          case 18:
            statSet.confWins = reader.ReadInt32();
            break;
          case 19:
            statSet.confLosses = reader.ReadInt32();
            break;
          case 20:
            statSet.confChampionships = reader.ReadInt32();
            break;
          case 21:
            statSet.divWins = reader.ReadInt32();
            break;
          case 22:
            statSet.divLosses = reader.ReadInt32();
            break;
          case 23:
            statSet.divChampionships = reader.ReadInt32();
            break;
          case 24:
            statSet.touchdowns = reader.ReadInt32();
            break;
          case 25:
            statSet.totalPassPlays = reader.ReadInt32();
            break;
          case 26:
            statSet.droppedPasses = reader.ReadInt32();
            break;
          case 27:
            statSet.rivalWins = reader.ReadInt32();
            break;
          case 28:
            statSet.redZoneAppearances = reader.ReadInt32();
            break;
          case 29:
            statSet.redZoneTouchdowns = reader.ReadInt32();
            break;
          case 30:
            statSet.superBowlWins = reader.ReadInt32();
            break;
          case 31:
            statSet.mvpAwards = reader.ReadInt32();
            break;
          case 32:
            statSet.superBowlMvpAwards = reader.ReadInt32();
            break;
          case 33:
            statSet.offesensivePowAwards = reader.ReadInt32();
            break;
          case 34:
            statSet.defensivePowAwards = reader.ReadInt32();
            break;
          case 35:
            statSet.playoffAppearances = reader.ReadInt32();
            break;
          case 36:
            statSet.playoffWins = reader.ReadInt32();
            break;
          case 37:
            statSet.playoffLosses = reader.ReadInt32();
            break;
          case 38:
            statSet.teamsBeaten = resolver.GetFormatterWithVerify<HashSet<int>>().Deserialize(ref reader, options);
            break;
          case 39:
            statSet.superBowlWiningTeams = resolver.GetFormatterWithVerify<HashSet<int>>().Deserialize(ref reader, options);
            break;
          case 40:
            statSet.touchdownPasses = reader.ReadInt32();
            break;
          case 41:
            statSet.passCompletions = reader.ReadInt32();
            break;
          case 42:
            statSet.qbInts = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return statSet;
    }
  }
}
