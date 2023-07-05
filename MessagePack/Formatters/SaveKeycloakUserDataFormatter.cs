// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SaveKeycloakUserDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class SaveKeycloakUserDataFormatter : 
    IMessagePackFormatter<SaveKeycloakUserData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SaveKeycloakUserData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(5);
        resolver.GetFormatterWithVerify<Dictionary<string, string>>().Serialize(ref writer, value.metaData, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Username, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Password, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.AuthToken, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.RefreshToken, options);
      }
    }

    public SaveKeycloakUserData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SaveKeycloakUserData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      SaveKeycloakUserData keycloakUserData = new SaveKeycloakUserData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            keycloakUserData.metaData = resolver.GetFormatterWithVerify<Dictionary<string, string>>().Deserialize(ref reader, options);
            break;
          case 1:
            keycloakUserData.Username = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 2:
            keycloakUserData.Password = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 3:
            keycloakUserData.AuthToken = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 4:
            keycloakUserData.RefreshToken = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return keycloakUserData;
    }
  }
}
