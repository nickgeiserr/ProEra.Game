// Decompiled with JetBrains decompiler
// Type: Analytics.HeroMomentCompletedArgs
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace Analytics
{
  public class HeroMomentCompletedArgs : AnalyticEventArgs
  {
    public HeroMomentCompletedArgs(bool heroMomentSucceeded) => this.HeroMomentSucceeded = heroMomentSucceeded;

    public bool HeroMomentSucceeded { get; set; }

    public override Dictionary<string, object> Parameters => new Dictionary<string, object>()
    {
      {
        "HeroMomentSucceeded",
        (object) this.HeroMomentSucceeded
      }
    };

    public override string EventName => AnalyticEventName.HeroMomentCompleted.ServiceName();
  }
}
