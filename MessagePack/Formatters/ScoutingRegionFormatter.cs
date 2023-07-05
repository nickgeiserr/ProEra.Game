// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ScoutingRegionFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class ScoutingRegionFormatter : 
    IMessagePackFormatter<ScoutingRegion>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      ScoutingRegion value,
      MessagePackSerializerOptions options)
    {
      writer.Write((int) value);
    }

    public ScoutingRegion Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      return (ScoutingRegion) reader.ReadInt32();
    }
  }
}
