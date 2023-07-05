// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.FootballVR.EMoveTypeFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;

namespace MessagePack.Formatters.FootballVR
{
  public sealed class EMoveTypeFormatter : IMessagePackFormatter<EMoveType>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      EMoveType value,
      MessagePackSerializerOptions options)
    {
      writer.Write((int) value);
    }

    public EMoveType Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options) => (EMoveType) reader.ReadInt32();
  }
}
