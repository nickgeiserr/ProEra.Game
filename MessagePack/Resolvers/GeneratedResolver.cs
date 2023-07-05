// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.GeneratedResolver
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack.Formatters;

namespace MessagePack.Resolvers
{
  public class GeneratedResolver : IFormatterResolver
  {
    public static readonly IFormatterResolver Instance = (IFormatterResolver) new GeneratedResolver();

    private GeneratedResolver()
    {
    }

    public IMessagePackFormatter<T> GetFormatter<T>() => GeneratedResolver.FormatterCache<T>.Formatter;

    private static class FormatterCache<T>
    {
      internal static readonly IMessagePackFormatter<T> Formatter;

      static FormatterCache()
      {
        object formatter = GeneratedResolverGetFormatterHelper.GetFormatter(typeof (T));
        if (formatter == null)
          return;
        GeneratedResolver.FormatterCache<T>.Formatter = (IMessagePackFormatter<T>) formatter;
      }
    }
  }
}
