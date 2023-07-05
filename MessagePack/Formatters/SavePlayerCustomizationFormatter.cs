// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SavePlayerCustomizationFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using System.Collections.Generic;
using UnityEngine;

namespace MessagePack.Formatters
{
  public sealed class SavePlayerCustomizationFormatter : 
    IMessagePackFormatter<SavePlayerCustomization>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SavePlayerCustomization value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(21);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        resolver.GetFormatterWithVerify<ETeamUniformId>().Serialize(ref writer, value.Uniform, options);
        writer.Write(value.UniformNumber);
        writer.Write(value.HomeUniform);
        resolver.GetFormatterWithVerify<EGlovesId>().Serialize(ref writer, value.GloveId, options);
        writer.Write(value.MultiplayerTeamBallId);
        resolver.GetFormatterWithVerify<Color>().Serialize(ref writer, value.TrailColor, options);
        resolver.GetFormatterWithVerify<EBallTrail>().Serialize(ref writer, value.TrailType, options);
        resolver.GetFormatterWithVerify<Color>().Serialize(ref writer, value.NameColor, options);
        resolver.GetFormatterWithVerify<Color>().Serialize(ref writer, value.WristbandColor, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.FirstName, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.LastName, options);
        writer.Write(value.BodyHeight);
        writer.Write(value.AvatarCustomized);
        resolver.GetFormatterWithVerify<EBodyType>().Serialize(ref writer, value.BodyType, options);
        writer.Write(value.BodyModelId);
        writer.Write(value.HeadModelId);
        writer.Write(value.FaceModelId);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.AvatarPresetId, options);
        writer.Write(value.HeightScale);
        writer.Write(value.IsNewCustomization);
      }
    }

    public SavePlayerCustomization Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SavePlayerCustomization) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      SavePlayerCustomization playerCustomization = new SavePlayerCustomization();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            playerCustomization.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            playerCustomization.Uniform = resolver.GetFormatterWithVerify<ETeamUniformId>().Deserialize(ref reader, options);
            break;
          case 2:
            playerCustomization.UniformNumber = reader.ReadInt32();
            break;
          case 3:
            playerCustomization.HomeUniform = reader.ReadBoolean();
            break;
          case 4:
            playerCustomization.GloveId = resolver.GetFormatterWithVerify<EGlovesId>().Deserialize(ref reader, options);
            break;
          case 5:
            playerCustomization.MultiplayerTeamBallId = reader.ReadInt32();
            break;
          case 6:
            playerCustomization.TrailColor = resolver.GetFormatterWithVerify<Color>().Deserialize(ref reader, options);
            break;
          case 7:
            playerCustomization.TrailType = resolver.GetFormatterWithVerify<EBallTrail>().Deserialize(ref reader, options);
            break;
          case 8:
            playerCustomization.NameColor = resolver.GetFormatterWithVerify<Color>().Deserialize(ref reader, options);
            break;
          case 9:
            playerCustomization.WristbandColor = resolver.GetFormatterWithVerify<Color>().Deserialize(ref reader, options);
            break;
          case 10:
            playerCustomization.FirstName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 11:
            playerCustomization.LastName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 12:
            playerCustomization.BodyHeight = reader.ReadSingle();
            break;
          case 13:
            playerCustomization.AvatarCustomized = reader.ReadBoolean();
            break;
          case 14:
            playerCustomization.BodyType = resolver.GetFormatterWithVerify<EBodyType>().Deserialize(ref reader, options);
            break;
          case 15:
            playerCustomization.BodyModelId = reader.ReadInt32();
            break;
          case 16:
            playerCustomization.HeadModelId = reader.ReadInt32();
            break;
          case 17:
            playerCustomization.FaceModelId = reader.ReadInt32();
            break;
          case 18:
            playerCustomization.AvatarPresetId = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 19:
            playerCustomization.HeightScale = reader.ReadSingle();
            break;
          case 20:
            playerCustomization.IsNewCustomization = reader.ReadBoolean();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return playerCustomization;
    }
  }
}
