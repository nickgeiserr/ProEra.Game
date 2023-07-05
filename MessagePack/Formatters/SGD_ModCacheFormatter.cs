// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SGD_ModCacheFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class SGD_ModCacheFormatter : 
    IMessagePackFormatter<SGD_ModCache>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      SGD_ModCache value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(7);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.fileName, options);
        writer.Write(value.timeStamp);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.gameVersion, options);
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.userName, options);
        writer.Write(value.markAsDeleted);
        writer.Write(value.isCacheValid);
        resolver.GetFormatterWithVerify<string[]>().Serialize(ref writer, value.ModFolders, options);
      }
    }

    public SGD_ModCache Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SGD_ModCache) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num1 = reader.ReadArrayHeader();
      string f = (string) null;
      double num2 = 0.0;
      string str1 = (string) null;
      string str2 = (string) null;
      bool flag1 = false;
      bool flag2 = false;
      string[] strArray = (string[]) null;
      for (int index = 0; index < num1; ++index)
      {
        switch (index)
        {
          case 0:
            f = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 1:
            num2 = reader.ReadDouble();
            break;
          case 2:
            str1 = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 3:
            str2 = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
            break;
          case 4:
            flag1 = reader.ReadBoolean();
            break;
          case 5:
            flag2 = reader.ReadBoolean();
            break;
          case 6:
            strArray = resolver.GetFormatterWithVerify<string[]>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      SGD_ModCache sgdModCache = new SGD_ModCache(f);
      if (num1 > 1)
      {
        sgdModCache.timeStamp = num2;
        if (num1 > 2)
        {
          sgdModCache.gameVersion = str1;
          if (num1 > 3)
          {
            sgdModCache.userName = str2;
            if (num1 > 4)
            {
              sgdModCache.markAsDeleted = flag1;
              if (num1 > 5)
              {
                sgdModCache.isCacheValid = flag2;
                if (num1 > 6)
                  sgdModCache.ModFolders = strArray;
              }
            }
          }
        }
      }
      --reader.Depth;
      return sgdModCache;
    }
  }
}
