// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TB12.ProfileProgress_EntryFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12;

namespace MessagePack.Formatters.TB12
{
  public sealed class ProfileProgress_EntryFormatter : 
    IMessagePackFormatter<ProfileProgress.Entry>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      ProfileProgress.Entry value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        writer.WriteArrayHeader(3);
        writer.Write(value.LevelId);
        writer.Write(value.Star);
        writer.Write(value.Score);
      }
    }

    public ProfileProgress.Entry Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (ProfileProgress.Entry) null;
      options.Security.DepthStep(ref reader);
      int num1 = reader.ReadArrayHeader();
      int id = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index = 0; index < num1; ++index)
      {
        switch (index)
        {
          case 0:
            id = reader.ReadInt32();
            break;
          case 1:
            num2 = reader.ReadInt32();
            break;
          case 2:
            num3 = reader.ReadInt32();
            break;
          default:
            reader.Skip();
            break;
        }
      }
      ProfileProgress.Entry entry = new ProfileProgress.Entry(id);
      if (num1 > 1)
      {
        entry.Star = num2;
        if (num1 > 2)
          entry.Score = num3;
      }
      --reader.Depth;
      return entry;
    }
  }
}
