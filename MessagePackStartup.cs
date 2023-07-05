// Decompiled with JetBrains decompiler
// Type: MessagePackStartup
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using MessagePack.Resolvers;
using MessagePack.Unity;
using MessagePack.Unity.Extension;
using UnityEngine;

public class MessagePackStartup
{
  private static bool serializerRegistered;

  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
  public static void Initialize()
  {
    if (MessagePackStartup.serializerRegistered)
      return;
    MessagePackSerializer.DefaultOptions = MessagePackSerializerOptions.Standard.WithResolver(CompositeResolver.Create(GeneratedResolver.Instance, (IFormatterResolver) UnityBlitResolver.Instance, (IFormatterResolver) UnityResolver.Instance, (IFormatterResolver) StandardResolver.Instance));
    MessagePackStartup.serializerRegistered = true;
  }
}
