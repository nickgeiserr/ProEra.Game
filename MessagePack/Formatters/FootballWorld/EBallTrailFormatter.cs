﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.FootballWorld.EBallTrailFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;

namespace MessagePack.Formatters.FootballWorld
{
  public sealed class EBallTrailFormatter : IMessagePackFormatter<EBallTrail>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      EBallTrail value,
      MessagePackSerializerOptions options)
    {
      writer.Write((int) value);
    }

    public EBallTrail Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      return (EBallTrail) reader.ReadInt32();
    }
  }
}
