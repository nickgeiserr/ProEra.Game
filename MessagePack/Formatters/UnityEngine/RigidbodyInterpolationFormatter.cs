// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.UnityEngine.RigidbodyInterpolationFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace MessagePack.Formatters.UnityEngine
{
  public sealed class RigidbodyInterpolationFormatter : 
    IMessagePackFormatter<RigidbodyInterpolation>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      RigidbodyInterpolation value,
      MessagePackSerializerOptions options)
    {
      writer.Write((int) value);
    }

    public RigidbodyInterpolation Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      return (RigidbodyInterpolation) reader.ReadInt32();
    }
  }
}
