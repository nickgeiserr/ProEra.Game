// Decompiled with JetBrains decompiler
// Type: AsyncExtensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Threading.Tasks;

public static class AsyncExtensions
{
  public static async void SafeFireAndForget(
    this Task task,
    bool continueOnCapturedContext = true,
    Action<Exception> onException = null)
  {
    try
    {
      await task.ConfigureAwait(continueOnCapturedContext);
    }
    catch (Exception ex) when (onException != null)
    {
      onException(ex);
    }
  }
}
