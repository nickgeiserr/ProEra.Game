// Decompiled with JetBrains decompiler
// Type: Analytics.AnalyticEvents
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

namespace Analytics
{
  public static class AnalyticEvents
  {
    public static void Record<TEventArgs>(TEventArgs args) where TEventArgs : AnalyticEventArgs
    {
      AnalyticsService.Instance.CustomData(args.EventName, (IDictionary<string, object>) args.Parameters);
      Debug.Log((object) ("Generated Analytic Event: " + args.EventName));
    }
  }
}
