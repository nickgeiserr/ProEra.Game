// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Save_SettingsStoreFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class Save_SettingsStoreFormatter : 
    IMessagePackFormatter<Save_SettingsStore>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Save_SettingsStore value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(6);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        writer.Write(value.SfxVolume);
        writer.Write(value.BgmVolume);
        writer.Write(value.HostVoVolume);
        writer.Write(value.StadiumVolume);
        writer.Write(value.InstrumentalMusic);
      }
    }

    public Save_SettingsStore Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (Save_SettingsStore) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      Save_SettingsStore saveSettingsStore = new Save_SettingsStore();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            saveSettingsStore.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            saveSettingsStore.SfxVolume = reader.ReadSingle();
            break;
          case 2:
            saveSettingsStore.BgmVolume = reader.ReadSingle();
            break;
          case 3:
            saveSettingsStore.HostVoVolume = reader.ReadSingle();
            break;
          case 4:
            saveSettingsStore.StadiumVolume = reader.ReadSingle();
            break;
          case 5:
            saveSettingsStore.InstrumentalMusic = reader.ReadBoolean();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return saveSettingsStore;
    }
  }
}
