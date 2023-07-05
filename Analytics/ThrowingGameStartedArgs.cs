// Decompiled with JetBrains decompiler
// Type: Analytics.ThrowingGameStartedArgs
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace Analytics
{
  public class ThrowingGameStartedArgs : AnalyticEventArgs
  {
    public override string EventName => AnalyticEventName.ThrowingGameStarted.ServiceName();

    public override Dictionary<string, object> Parameters => new Dictionary<string, object>();
  }
}
