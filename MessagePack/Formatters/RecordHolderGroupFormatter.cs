// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.RecordHolderGroupFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class RecordHolderGroupFormatter : 
    IMessagePackFormatter<RecordHolderGroup>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      RecordHolderGroup value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(60);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.QBRating, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Completions, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PassAttempts, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.CompletionPercentage, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PassYards, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PassTDs, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.ThrownInts, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.YardsPerPass, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.LongestPass, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.RushAttempts, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.RushYards, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.RushTDs, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.YardsPerRush, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.LongestRush, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Fumbles, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Receptions, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.ReceivingYards, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.ReceivingTDs, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.YardsPerCatch, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.LongestReception, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.YardsAfterCatch, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Drops, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Targets, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.TotalTDs, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Tackles, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Sacks, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Interceptions, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.TacklesForLoss, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.DefensiveTDs, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.KnockDowns, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.ForcedFumbles, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.FumbleRecoveries, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.FGMade, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.FGAttempted, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.XPMade, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.XPAttempted, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Punts, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PuntsInside20, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PuntTouchbacks, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.YardsPerPunt, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PuntReturns, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PuntReturnYards, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.YardsPerPuntReturn, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PuntReturnTDs, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.KickReturns, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.KickReturnYards, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.YardsPerKickReturn, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.KickReturnTDs, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.TotalYards, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PassYardsAllowed, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.RushYardsAllowed, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.TotalYardsAllowed, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Turnovers, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.TurnoverMargin, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PointsScored, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.PointsAllowed, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Wins, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.Losses, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.WinStreak, options);
        resolver.GetFormatterWithVerify<RecordHolder>().Serialize(ref writer, value.LossStreak, options);
      }
    }

    public RecordHolderGroup Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (RecordHolderGroup) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      RecordHolderGroup recordHolderGroup = new RecordHolderGroup();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            recordHolderGroup.QBRating = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 1:
            recordHolderGroup.Completions = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 2:
            recordHolderGroup.PassAttempts = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 3:
            recordHolderGroup.CompletionPercentage = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 4:
            recordHolderGroup.PassYards = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 5:
            recordHolderGroup.PassTDs = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 6:
            recordHolderGroup.ThrownInts = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 7:
            recordHolderGroup.YardsPerPass = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 8:
            recordHolderGroup.LongestPass = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 9:
            recordHolderGroup.RushAttempts = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 10:
            recordHolderGroup.RushYards = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 11:
            recordHolderGroup.RushTDs = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 12:
            recordHolderGroup.YardsPerRush = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 13:
            recordHolderGroup.LongestRush = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 14:
            recordHolderGroup.Fumbles = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 15:
            recordHolderGroup.Receptions = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 16:
            recordHolderGroup.ReceivingYards = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 17:
            recordHolderGroup.ReceivingTDs = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 18:
            recordHolderGroup.YardsPerCatch = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 19:
            recordHolderGroup.LongestReception = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 20:
            recordHolderGroup.YardsAfterCatch = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 21:
            recordHolderGroup.Drops = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 22:
            recordHolderGroup.Targets = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 23:
            recordHolderGroup.TotalTDs = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 24:
            recordHolderGroup.Tackles = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 25:
            recordHolderGroup.Sacks = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 26:
            recordHolderGroup.Interceptions = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 27:
            recordHolderGroup.TacklesForLoss = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 28:
            recordHolderGroup.DefensiveTDs = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 29:
            recordHolderGroup.KnockDowns = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 30:
            recordHolderGroup.ForcedFumbles = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 31:
            recordHolderGroup.FumbleRecoveries = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 32:
            recordHolderGroup.FGMade = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 33:
            recordHolderGroup.FGAttempted = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 34:
            recordHolderGroup.XPMade = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 35:
            recordHolderGroup.XPAttempted = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 36:
            recordHolderGroup.Punts = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 37:
            recordHolderGroup.PuntsInside20 = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 38:
            recordHolderGroup.PuntTouchbacks = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 39:
            recordHolderGroup.YardsPerPunt = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 40:
            recordHolderGroup.PuntReturns = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 41:
            recordHolderGroup.PuntReturnYards = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 42:
            recordHolderGroup.YardsPerPuntReturn = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 43:
            recordHolderGroup.PuntReturnTDs = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 44:
            recordHolderGroup.KickReturns = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 45:
            recordHolderGroup.KickReturnYards = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 46:
            recordHolderGroup.YardsPerKickReturn = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 47:
            recordHolderGroup.KickReturnTDs = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 48:
            recordHolderGroup.TotalYards = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 49:
            recordHolderGroup.PassYardsAllowed = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 50:
            recordHolderGroup.RushYardsAllowed = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 51:
            recordHolderGroup.TotalYardsAllowed = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 52:
            recordHolderGroup.Turnovers = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 53:
            recordHolderGroup.TurnoverMargin = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 54:
            recordHolderGroup.PointsScored = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 55:
            recordHolderGroup.PointsAllowed = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 56:
            recordHolderGroup.Wins = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 57:
            recordHolderGroup.Losses = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 58:
            recordHolderGroup.WinStreak = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          case 59:
            recordHolderGroup.LossStreak = resolver.GetFormatterWithVerify<RecordHolder>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return recordHolderGroup;
    }
  }
}
