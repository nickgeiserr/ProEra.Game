// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.PlayerData_BasicFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class PlayerData_BasicFormatter : 
    IMessagePackFormatter<PlayerData_Basic>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      PlayerData_Basic value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(9);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.FirstName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.LastName, options);
        resolver.GetFormatterWithVerify<Position>().Serialize(ref writer, value.PlayerPosition, options);
        writer.Write(value.Number);
        writer.Write(value.SkinColor);
        writer.Write(value.Height);
        writer.Write(value.Weight);
        writer.Write(value.Age);
        writer.Write(value.PortraitID);
      }
    }

    public PlayerData_Basic Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (PlayerData_Basic) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      PlayerData_Basic playerDataBasic = new PlayerData_Basic();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            playerDataBasic.FirstName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 1:
            playerDataBasic.LastName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            playerDataBasic.PlayerPosition = resolver.GetFormatterWithVerify<Position>().Deserialize(ref reader, options);
            break;
          case 3:
            playerDataBasic.Number = reader.ReadInt32();
            break;
          case 4:
            playerDataBasic.SkinColor = reader.ReadInt32();
            break;
          case 5:
            playerDataBasic.Height = reader.ReadInt32();
            break;
          case 6:
            playerDataBasic.Weight = reader.ReadInt32();
            break;
          case 7:
            playerDataBasic.Age = reader.ReadInt32();
            break;
          case 8:
            playerDataBasic.PortraitID = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return playerDataBasic;
    }
  }
}
