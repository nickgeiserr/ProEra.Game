// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.PositionFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class PositionFormatter : IMessagePackFormatter<Position>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Position value,
      MessagePackSerializerOptions options)
    {
      writer.Write((int) value);
    }

    public Position Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options) => (Position) reader.ReadInt32();
  }
}
