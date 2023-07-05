// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Save_AvatarsSettingsFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class Save_AvatarsSettingsFormatter : 
    IMessagePackFormatter<Save_AvatarsSettings>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Save_AvatarsSettings value,
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
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.CustomStateBehavior);
        writer.Write(value.PlayModeCollision);
        writer.Write(value.PlayersCollisionPhysics);
        writer.Write(value.CatchUpThreshold);
        writer.Write(value.SoftCatchUpThreshold);
        writer.Write(value.SoftCatchupSpeed);
        writer.Write(value.CatchupLerpFactor);
        writer.Write(value.CatchUpMaxSpeed);
      }
    }

    public Save_AvatarsSettings Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Save_AvatarsSettings) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Save_AvatarsSettings saveAvatarsSettings = new Save_AvatarsSettings();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveAvatarsSettings.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveAvatarsSettings.CustomStateBehavior = reader.ReadBoolean();
            break;
          case 2:
            saveAvatarsSettings.PlayModeCollision = reader.ReadBoolean();
            break;
          case 3:
            saveAvatarsSettings.PlayersCollisionPhysics = reader.ReadBoolean();
            break;
          case 4:
            saveAvatarsSettings.CatchUpThreshold = reader.ReadSingle();
            break;
          case 5:
            saveAvatarsSettings.SoftCatchUpThreshold = reader.ReadSingle();
            break;
          case 6:
            saveAvatarsSettings.SoftCatchupSpeed = reader.ReadSingle();
            break;
          case 7:
            saveAvatarsSettings.CatchupLerpFactor = reader.ReadSingle();
            break;
          case 8:
            saveAvatarsSettings.CatchUpMaxSpeed = reader.ReadSingle();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveAvatarsSettings;
    }
  }
}
