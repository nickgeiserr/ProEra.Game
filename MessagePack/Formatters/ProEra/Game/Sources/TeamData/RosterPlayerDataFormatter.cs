// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ProEra.Game.Sources.TeamData.RosterPlayerDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources.TeamData;
using System;

namespace MessagePack.Formatters.ProEra.Game.Sources.TeamData
{
  public sealed class RosterPlayerDataFormatter : 
    IMessagePackFormatter<RosterPlayerData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      RosterPlayerData value,
      MessagePackSerializerOptions options)
    {
      IFormatterResolver resolver = options.Resolver;
      writer.WriteArrayHeader(35);
      writer.Write(value.index);
      resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.firstName, options);
      resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.lastName, options);
      writer.Write(value.skin);
      writer.Write(value.number);
      writer.Write(value.height);
      writer.Write(value.weight);
      resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.position, options);
      writer.Write(value.speed);
      writer.Write(value.tackleBreak);
      writer.Write(value.fumble);
      writer.Write(value.catching);
      writer.Write(value.blocking);
      writer.Write(value.throwingAccuracy);
      writer.Write(value.kickingPower);
      writer.Write(value.kickingAccuracy);
      writer.Write(value.blockBreak);
      writer.Write(value.tackle);
      writer.Write(value.throwingPower);
      writer.Write(value.fitness);
      writer.Write(value.awareness);
      writer.Write(value.agility);
      writer.Write(value.cover);
      writer.Write(value.hitPower);
      writer.Write(value.endurance);
      writer.Write(value.visor);
      writer.Write(value.sleeves);
      writer.Write(value.bands);
      writer.Write(value.wraps);
      writer.Write(value.age);
      writer.Write(value.potential);
      writer.Write(value.portrait);
      writer.Write(value.discipline);
      resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.avatarId, options);
      writer.Write(value.isLeftHanded);
    }

    public RosterPlayerData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        throw new InvalidOperationException("typecode is null, struct not supported");
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      RosterPlayerData rosterPlayerData = new RosterPlayerData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            rosterPlayerData.index = reader.ReadInt32();
            break;
          case 1:
            rosterPlayerData.firstName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            rosterPlayerData.lastName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 3:
            rosterPlayerData.skin = reader.ReadInt32();
            break;
          case 4:
            rosterPlayerData.number = reader.ReadInt32();
            break;
          case 5:
            rosterPlayerData.height = reader.ReadInt32();
            break;
          case 6:
            rosterPlayerData.weight = reader.ReadInt32();
            break;
          case 7:
            rosterPlayerData.position = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 8:
            rosterPlayerData.speed = reader.ReadInt32();
            break;
          case 9:
            rosterPlayerData.tackleBreak = reader.ReadInt32();
            break;
          case 10:
            rosterPlayerData.fumble = reader.ReadInt32();
            break;
          case 11:
            rosterPlayerData.catching = reader.ReadInt32();
            break;
          case 12:
            rosterPlayerData.blocking = reader.ReadInt32();
            break;
          case 13:
            rosterPlayerData.throwingAccuracy = reader.ReadInt32();
            break;
          case 14:
            rosterPlayerData.kickingPower = reader.ReadInt32();
            break;
          case 15:
            rosterPlayerData.kickingAccuracy = reader.ReadInt32();
            break;
          case 16:
            rosterPlayerData.blockBreak = reader.ReadInt32();
            break;
          case 17:
            rosterPlayerData.tackle = reader.ReadInt32();
            break;
          case 18:
            rosterPlayerData.throwingPower = reader.ReadInt32();
            break;
          case 19:
            rosterPlayerData.fitness = reader.ReadInt32();
            break;
          case 20:
            rosterPlayerData.awareness = reader.ReadInt32();
            break;
          case 21:
            rosterPlayerData.agility = reader.ReadInt32();
            break;
          case 22:
            rosterPlayerData.cover = reader.ReadInt32();
            break;
          case 23:
            rosterPlayerData.hitPower = reader.ReadInt32();
            break;
          case 24:
            rosterPlayerData.endurance = reader.ReadInt32();
            break;
          case 25:
            rosterPlayerData.visor = reader.ReadInt32();
            break;
          case 26:
            rosterPlayerData.sleeves = reader.ReadInt32();
            break;
          case 27:
            rosterPlayerData.bands = reader.ReadInt32();
            break;
          case 28:
            rosterPlayerData.wraps = reader.ReadInt32();
            break;
          case 29:
            rosterPlayerData.age = reader.ReadInt32();
            break;
          case 30:
            rosterPlayerData.potential = reader.ReadInt32();
            break;
          case 31:
            rosterPlayerData.portrait = reader.ReadInt32();
            break;
          case 32:
            rosterPlayerData.discipline = reader.ReadInt32();
            break;
          case 33:
            rosterPlayerData.avatarId = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 34:
            rosterPlayerData.isLeftHanded = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return rosterPlayerData;
    }
  }
}
