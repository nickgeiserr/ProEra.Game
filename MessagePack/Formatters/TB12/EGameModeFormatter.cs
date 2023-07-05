// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TB12.EGameModeFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12;

namespace MessagePack.Formatters.TB12
{
  public sealed class EGameModeFormatter : IMessagePackFormatter<EGameMode>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      EGameMode value,
      MessagePackSerializerOptions options)
    {
      writer.Write((uint) value);
    }

    public EGameMode Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options) => (EGameMode) reader.ReadUInt32();
  }
}
