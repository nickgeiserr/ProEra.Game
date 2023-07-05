// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.RosterTypeFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class RosterTypeFormatter : IMessagePackFormatter<RosterType>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      RosterType value,
      MessagePackSerializerOptions options)
    {
      writer.Write((int) value);
    }

    public RosterType Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      return (RosterType) reader.ReadInt32();
    }
  }
}
