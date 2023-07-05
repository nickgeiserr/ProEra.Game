// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ConferenceFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class ConferenceFormatter : IMessagePackFormatter<Conference>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      Conference value,
      MessagePackSerializerOptions options)
    {
      writer.Write((int) value);
    }

    public Conference Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      return (Conference) reader.ReadInt32();
    }
  }
}
