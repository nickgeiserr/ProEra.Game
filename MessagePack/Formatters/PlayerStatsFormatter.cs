// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.PlayerStatsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class PlayerStatsFormatter : 
    IMessagePackFormatter<PlayerStats>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      PlayerStats value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(49);
        writer.Write(value.QBCompletions);
        writer.Write(value.QBAttempts);
        writer.Write(value.QBPassYards);
        writer.Write(value.QBPassTDs);
        writer.Write(value.QBInts);
        writer.Write(value.QBLongestPass);
        writer.Write(value.RushAttempts);
        writer.Write(value.RushYards);
        writer.Write(value.RushTDs);
        writer.Write(value.LongestRush);
        writer.Write(value.Receptions);
        writer.Write(value.ReceivingYards);
        writer.Write(value.ReceivingTDs);
        writer.Write(value.LongestReception);
        writer.Write(value.YardsAfterCatch);
        writer.Write(value.Drops);
        writer.Write(value.Fumbles);
        writer.Write(value.Targets);
        writer.Write(value.Tackles);
        writer.Write(value.Sacks);
        writer.Write(value.Interceptions);
        writer.Write(value.DefensiveTDs);
        writer.Write(value.TacklesForLoss);
        writer.Write(value.KnockDowns);
        writer.Write(value.ForcedFumbles);
        writer.Write(value.FumbleRecoveries);
        writer.Write(value.Penalties);
        writer.Write(value.PenaltyYards);
        writer.Write(value.FGMade);
        writer.Write(value.FGAttempted);
        writer.Write(value.XPMade);
        writer.Write(value.XPAttempted);
        writer.Write(value.Punts);
        writer.Write(value.PuntsInside20);
        writer.Write(value.PuntTouchbacks);
        writer.Write(value.PuntYards);
        writer.Write(value.PuntReturns);
        writer.Write(value.PuntReturnYards);
        writer.Write(value.PuntReturnTDs);
        writer.Write(value.KickReturns);
        writer.Write(value.KickReturnYards);
        writer.Write(value.KickReturnTDs);
        writer.Write(value.QBTenYardCompletions);
        writer.Write(value.QBTwentyYardCompletions);
        writer.Write(value.QBThirtyYardCompletions);
        writer.Write(value.QBFiftyYardCompletions);
        writer.Write(value.QBSacked);
        writer.Write(value.StatYear);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.StatYearTeam, options);
      }
    }

    public PlayerStats Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (PlayerStats) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      PlayerStats playerStats = new PlayerStats();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            playerStats.QBCompletions = reader.ReadInt32();
            break;
          case 1:
            playerStats.QBAttempts = reader.ReadInt32();
            break;
          case 2:
            playerStats.QBPassYards = reader.ReadInt32();
            break;
          case 3:
            playerStats.QBPassTDs = reader.ReadInt32();
            break;
          case 4:
            playerStats.QBInts = reader.ReadInt32();
            break;
          case 5:
            playerStats.QBLongestPass = reader.ReadInt32();
            break;
          case 6:
            playerStats.RushAttempts = reader.ReadInt32();
            break;
          case 7:
            playerStats.RushYards = reader.ReadInt32();
            break;
          case 8:
            playerStats.RushTDs = reader.ReadInt32();
            break;
          case 9:
            playerStats.LongestRush = reader.ReadInt32();
            break;
          case 10:
            playerStats.Receptions = reader.ReadInt32();
            break;
          case 11:
            playerStats.ReceivingYards = reader.ReadInt32();
            break;
          case 12:
            playerStats.ReceivingTDs = reader.ReadInt32();
            break;
          case 13:
            playerStats.LongestReception = reader.ReadInt32();
            break;
          case 14:
            playerStats.YardsAfterCatch = reader.ReadInt32();
            break;
          case 15:
            playerStats.Drops = reader.ReadInt32();
            break;
          case 16:
            playerStats.Fumbles = reader.ReadInt32();
            break;
          case 17:
            playerStats.Targets = reader.ReadInt32();
            break;
          case 18:
            playerStats.Tackles = reader.ReadInt32();
            break;
          case 19:
            playerStats.Sacks = reader.ReadInt32();
            break;
          case 20:
            playerStats.Interceptions = reader.ReadInt32();
            break;
          case 21:
            playerStats.DefensiveTDs = reader.ReadInt32();
            break;
          case 22:
            playerStats.TacklesForLoss = reader.ReadInt32();
            break;
          case 23:
            playerStats.KnockDowns = reader.ReadInt32();
            break;
          case 24:
            playerStats.ForcedFumbles = reader.ReadInt32();
            break;
          case 25:
            playerStats.FumbleRecoveries = reader.ReadInt32();
            break;
          case 26:
            playerStats.Penalties = reader.ReadInt32();
            break;
          case 27:
            playerStats.PenaltyYards = reader.ReadInt32();
            break;
          case 28:
            playerStats.FGMade = reader.ReadInt32();
            break;
          case 29:
            playerStats.FGAttempted = reader.ReadInt32();
            break;
          case 30:
            playerStats.XPMade = reader.ReadInt32();
            break;
          case 31:
            playerStats.XPAttempted = reader.ReadInt32();
            break;
          case 32:
            playerStats.Punts = reader.ReadInt32();
            break;
          case 33:
            playerStats.PuntsInside20 = reader.ReadInt32();
            break;
          case 34:
            playerStats.PuntTouchbacks = reader.ReadInt32();
            break;
          case 35:
            playerStats.PuntYards = reader.ReadInt32();
            break;
          case 36:
            playerStats.PuntReturns = reader.ReadInt32();
            break;
          case 37:
            playerStats.PuntReturnYards = reader.ReadInt32();
            break;
          case 38:
            playerStats.PuntReturnTDs = reader.ReadInt32();
            break;
          case 39:
            playerStats.KickReturns = reader.ReadInt32();
            break;
          case 40:
            playerStats.KickReturnYards = reader.ReadInt32();
            break;
          case 41:
            playerStats.KickReturnTDs = reader.ReadInt32();
            break;
          case 42:
            playerStats.QBTenYardCompletions = reader.ReadInt32();
            break;
          case 43:
            playerStats.QBTwentyYardCompletions = reader.ReadInt32();
            break;
          case 44:
            playerStats.QBThirtyYardCompletions = reader.ReadInt32();
            break;
          case 45:
            playerStats.QBFiftyYardCompletions = reader.ReadInt32();
            break;
          case 46:
            playerStats.QBSacked = reader.ReadInt32();
            break;
          case 47:
            playerStats.StatYear = reader.ReadInt32();
            break;
          case 48:
            playerStats.StatYearTeam = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return playerStats;
    }
  }
}
