// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.CoachDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class CoachDataFormatter : IMessagePackFormatter<CoachData>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      CoachData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
        writer.WriteNil();
      else
        writer.WriteArrayHeader(0);
    }

    public CoachData Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (CoachData) null;
      reader.Skip();
      return new CoachData();
    }
  }
}
