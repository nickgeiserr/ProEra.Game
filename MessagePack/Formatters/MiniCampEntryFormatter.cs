// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.MiniCampEntryFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class MiniCampEntryFormatter : 
    IMessagePackFormatter<MiniCampEntry>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      MiniCampEntry value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(6);
        writer.Write(value.Level);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.TeamName, options);
        writer.Write(value.EarnedStars);
        writer.Write(value.TotalStars);
        writer.Write(value.FlipOrientation);
        writer.Write(value.PersonalBest);
      }
    }

    public MiniCampEntry Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (MiniCampEntry) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      MiniCampEntry miniCampEntry = new MiniCampEntry();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            miniCampEntry.Level = reader.ReadInt32();
            break;
          case 1:
            miniCampEntry.TeamName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            miniCampEntry.EarnedStars = reader.ReadInt32();
            break;
          case 3:
            miniCampEntry.TotalStars = reader.ReadInt32();
            break;
          case 4:
            miniCampEntry.FlipOrientation = reader.ReadBoolean();
            break;
          case 5:
            miniCampEntry.PersonalBest = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return miniCampEntry;
    }
  }
}
