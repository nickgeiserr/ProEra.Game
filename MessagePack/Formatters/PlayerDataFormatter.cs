// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.PlayerDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class PlayerDataFormatter : IMessagePackFormatter<PlayerData>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      PlayerData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(53);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.FirstName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.LastName, options);
        resolver.GetFormatterWithVerify<Position>().Serialize(ref writer, value.PlayerPosition, options);
        writer.Write(value.Number);
        writer.Write(value.SkinColor);
        writer.Write(value.Height);
        writer.Write(value.Weight);
        writer.Write(value.Age);
        writer.Write(value.PortraitID);
        writer.Write(value.Speed);
        writer.Write(value.Catching);
        writer.Write(value.Fumbling);
        writer.Write(value.Blocking);
        writer.Write(value.TackleBreaking);
        writer.Write(value.ThrowAccuracy);
        writer.Write(value.ThrowPower);
        writer.Write(value.KickAccuracy);
        writer.Write(value.KickPower);
        writer.Write(value.BlockBreaking);
        writer.Write(value.Tackling);
        writer.Write(value.Fitness);
        writer.Write(value.Awareness);
        writer.Write(value.Coverage);
        writer.Write(value.HitPower);
        writer.Write(value.Endurance);
        writer.Write(value.Agility);
        writer.Write(value.Potential);
        writer.Write(value.Discipline);
        writer.Write(value.Fatigue);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.AvatarID, options);
        writer.Write(value.Visor);
        writer.Write(value.Sleeves);
        writer.Write(value.Bands);
        writer.Write(value.Wraps);
        resolver.GetFormatterWithVerify<ScoutingRegion>().Serialize(ref writer, value.ScoutRegion, options);
        writer.Write(value.ScoutPercentage);
        writer.Write(value.EstimatedOverall);
        writer.Write(value.ScoutingPointsUsedOn);
        writer.Write(value.RoundDrafted);
        writer.Write(value.ContractLength);
        writer.Write(value.Salary);
        writer.Write(value.YearsRemainingOnContract);
        writer.Write(value.YearsPro);
        resolver.GetFormatterWithVerify<Injury>().Serialize(ref writer, value.CurrentInjury, options);
        resolver.GetFormatterWithVerify<List<Injury>>().Serialize(ref writer, value.AllInjuries, options);
        resolver.GetFormatterWithVerify<List<Award>>().Serialize(ref writer, value.AllAwards, options);
        resolver.GetFormatterWithVerify<PlayerStats>().Serialize(ref writer, value.CurrentGameStats, options);
        resolver.GetFormatterWithVerify<PlayerStats>().Serialize(ref writer, value.CurrentSeasonStats, options);
        resolver.GetFormatterWithVerify<PlayerStats>().Serialize(ref writer, value.TotalCareerStats, options);
        resolver.GetFormatterWithVerify<List<PlayerStats>>().Serialize(ref writer, value.CareerStats, options);
        writer.Write(value.OnUserTeam);
        writer.Write(value.IndexOnTeam);
        writer.Write(value.IsLeftHanded);
      }
    }

    public PlayerData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (PlayerData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      PlayerData playerData = new PlayerData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            playerData.FirstName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 1:
            playerData.LastName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            playerData.PlayerPosition = resolver.GetFormatterWithVerify<Position>().Deserialize(ref reader, options);
            break;
          case 3:
            playerData.Number = reader.ReadInt32();
            break;
          case 4:
            playerData.SkinColor = reader.ReadInt32();
            break;
          case 5:
            playerData.Height = reader.ReadInt32();
            break;
          case 6:
            playerData.Weight = reader.ReadInt32();
            break;
          case 7:
            playerData.Age = reader.ReadInt32();
            break;
          case 8:
            playerData.PortraitID = reader.ReadInt32();
            break;
          case 9:
            playerData.Speed = reader.ReadInt32();
            break;
          case 10:
            playerData.Catching = reader.ReadInt32();
            break;
          case 11:
            playerData.Fumbling = reader.ReadInt32();
            break;
          case 12:
            playerData.Blocking = reader.ReadInt32();
            break;
          case 13:
            playerData.TackleBreaking = reader.ReadInt32();
            break;
          case 14:
            playerData.ThrowAccuracy = reader.ReadInt32();
            break;
          case 15:
            playerData.ThrowPower = reader.ReadInt32();
            break;
          case 16:
            playerData.KickAccuracy = reader.ReadInt32();
            break;
          case 17:
            playerData.KickPower = reader.ReadInt32();
            break;
          case 18:
            playerData.BlockBreaking = reader.ReadInt32();
            break;
          case 19:
            playerData.Tackling = reader.ReadInt32();
            break;
          case 20:
            playerData.Fitness = reader.ReadInt32();
            break;
          case 21:
            playerData.Awareness = reader.ReadInt32();
            break;
          case 22:
            playerData.Coverage = reader.ReadInt32();
            break;
          case 23:
            playerData.HitPower = reader.ReadInt32();
            break;
          case 24:
            playerData.Endurance = reader.ReadInt32();
            break;
          case 25:
            playerData.Agility = reader.ReadInt32();
            break;
          case 26:
            playerData.Potential = reader.ReadInt32();
            break;
          case 27:
            playerData.Discipline = reader.ReadInt32();
            break;
          case 28:
            playerData.Fatigue = reader.ReadInt32();
            break;
          case 29:
            playerData.AvatarID = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 30:
            playerData.Visor = reader.ReadInt32();
            break;
          case 31:
            playerData.Sleeves = reader.ReadInt32();
            break;
          case 32:
            playerData.Bands = reader.ReadInt32();
            break;
          case 33:
            playerData.Wraps = reader.ReadInt32();
            break;
          case 34:
            playerData.ScoutRegion = resolver.GetFormatterWithVerify<ScoutingRegion>().Deserialize(ref reader, options);
            break;
          case 35:
            playerData.ScoutPercentage = reader.ReadInt32();
            break;
          case 36:
            playerData.EstimatedOverall = reader.ReadInt32();
            break;
          case 37:
            playerData.ScoutingPointsUsedOn = reader.ReadInt32();
            break;
          case 38:
            playerData.RoundDrafted = reader.ReadInt32();
            break;
          case 39:
            playerData.ContractLength = reader.ReadInt32();
            break;
          case 40:
            playerData.Salary = reader.ReadInt32();
            break;
          case 41:
            playerData.YearsRemainingOnContract = reader.ReadInt32();
            break;
          case 42:
            playerData.YearsPro = reader.ReadInt32();
            break;
          case 43:
            playerData.CurrentInjury = resolver.GetFormatterWithVerify<Injury>().Deserialize(ref reader, options);
            break;
          case 44:
            playerData.AllInjuries = resolver.GetFormatterWithVerify<List<Injury>>().Deserialize(ref reader, options);
            break;
          case 45:
            playerData.AllAwards = resolver.GetFormatterWithVerify<List<Award>>().Deserialize(ref reader, options);
            break;
          case 46:
            playerData.CurrentGameStats = resolver.GetFormatterWithVerify<PlayerStats>().Deserialize(ref reader, options);
            break;
          case 47:
            playerData.CurrentSeasonStats = resolver.GetFormatterWithVerify<PlayerStats>().Deserialize(ref reader, options);
            break;
          case 48:
            playerData.TotalCareerStats = resolver.GetFormatterWithVerify<PlayerStats>().Deserialize(ref reader, options);
            break;
          case 49:
            playerData.CareerStats = resolver.GetFormatterWithVerify<List<PlayerStats>>().Deserialize(ref reader, options);
            break;
          case 50:
            playerData.OnUserTeam = reader.ReadBoolean();
            break;
          case 51:
            playerData.IndexOnTeam = reader.ReadInt32();
            break;
          case 52:
            playerData.IsLeftHanded = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return playerData;
    }
  }
}
