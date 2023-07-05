// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SavedGameDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack.Internal;
using System;

namespace MessagePack.Formatters
{
  public sealed class SavedGameDataFormatter : 
    IMessagePackFormatter<SavedGameData>,
    IMessagePackFormatter
  {
    private static unsafe ReadOnlySpan<byte> GetSpan_fileName() => new ReadOnlySpan<byte>((void*) &\u003CPrivateImplementationDetails\u003E.CEB9A35B9CFD4066D3986FD5F0FD6FFD08FB581874BB41D3DC6BFBD7A0E39B16, 9);

    private static unsafe ReadOnlySpan<byte> GetSpan_timeStamp() => new ReadOnlySpan<byte>((void*) &\u003CPrivateImplementationDetails\u003E.CEC20EAE07B2B6964E9DBEC9ECE4AF37DB26CD8D452AE9DA519955DEFB59125B, 10);

    private static unsafe ReadOnlySpan<byte> GetSpan_gameVersion() => new ReadOnlySpan<byte>((void*) &\u003CPrivateImplementationDetails\u003E.C040D96EA0C655575D4F4276948DC8E02FFD02F2840C3BC0FC5FFF612FB06E03, 9);

    private static unsafe ReadOnlySpan<byte> GetSpan_userName() => new ReadOnlySpan<byte>((void*) &\u003CPrivateImplementationDetails\u003E.\u0038647371E53882394DC4A92F81F02DBEE6E9411D0FFFB8D14E7D508C3F2DE5D2F, 9);

    private static unsafe ReadOnlySpan<byte> GetSpan_markAsDeleted() => new ReadOnlySpan<byte>((void*) &\u003CPrivateImplementationDetails\u003E.\u0034CD51A20D913AB1FCDA45748BBCC8A2E77CFA3B754043882B333040E12064BCD, 9);

    public void Serialize(
      ref MessagePackWriter writer,
      SavedGameData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteMapHeader(5);
        writer.WriteRaw(SavedGameDataFormatter.GetSpan_fileName());
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.fileName, options);
        writer.WriteRaw(SavedGameDataFormatter.GetSpan_timeStamp());
        writer.Write(value.timeStamp);
        writer.WriteRaw(SavedGameDataFormatter.GetSpan_gameVersion());
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.gameVersion, options);
        writer.WriteRaw(SavedGameDataFormatter.GetSpan_userName());
        resolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.userName, options);
        writer.WriteRaw(SavedGameDataFormatter.GetSpan_markAsDeleted());
        writer.Write(value.markAsDeleted);
      }
    }

    public SavedGameData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (SavedGameData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadMapHeader();
      SavedGameData savedGameData = new SavedGameData();
      for (int index = 0; index < num; ++index)
      {
        ReadOnlySpan<byte> span = CodeGenHelpers.ReadStringSpan(ref reader);
        switch (span.Length)
        {
          case 8:
            switch (AutomataKeyGen.GetKey(ref span))
            {
              case 7308604759629130086:
                savedGameData.fileName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                continue;
              case 7308604759846777717:
                savedGameData.userName = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                continue;
              case 7310579611359014772:
                savedGameData.markAsDeleted = reader.ReadBoolean();
                continue;
              case 8318822883449987431:
                savedGameData.gameVersion = resolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                continue;
              default:
                goto label_4;
            }
          case 9:
            if (MemoryExtensions.SequenceEqual<byte>(span, SavedGameDataFormatter.GetSpan_timeStamp().Slice(1)))
            {
              savedGameData.timeStamp = reader.ReadDouble();
              break;
            }
            goto default;
          default:
label_4:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return savedGameData;
    }
  }
}
