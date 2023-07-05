// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.DDL.UniformData.ETeamUniformIdFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;

namespace MessagePack.Formatters.DDL.UniformData
{
  public sealed class ETeamUniformIdFormatter : 
    IMessagePackFormatter<ETeamUniformId>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      ETeamUniformId value,
      MessagePackSerializerOptions options)
    {
      writer.Write((uint) value);
    }

    public ETeamUniformId Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      return (ETeamUniformId) reader.ReadUInt32();
    }
  }
}
