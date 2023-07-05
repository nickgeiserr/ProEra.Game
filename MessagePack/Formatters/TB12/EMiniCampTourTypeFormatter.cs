// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TB12.EMiniCampTourTypeFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12;

namespace MessagePack.Formatters.TB12
{
  public sealed class EMiniCampTourTypeFormatter : 
    IMessagePackFormatter<EMiniCampTourType>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      EMiniCampTourType value,
      MessagePackSerializerOptions options)
    {
      writer.Write((int) value);
    }

    public EMiniCampTourType Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      return (EMiniCampTourType) reader.ReadInt32();
    }
  }
}
