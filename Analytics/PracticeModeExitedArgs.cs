// Decompiled with JetBrains decompiler
// Type: Analytics.PracticeModeExitedArgs
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace Analytics
{
  public class PracticeModeExitedArgs : AnalyticEventArgs
  {
    public PracticeModeExitedArgs(
      float timeSpentInScene,
      float fistLocomotionTime,
      float thumbLocomotionTime)
    {
      this.TimeSpentInScene = timeSpentInScene;
      this.FistLocomotionTime = fistLocomotionTime;
      this.ThumbLocomotionTime = thumbLocomotionTime;
    }

    public override string EventName => AnalyticEventName.PracticeModeExited.ServiceName();

    public float TimeSpentInScene { get; set; }

    public float FistLocomotionTime { get; set; }

    public float ThumbLocomotionTime { get; set; }

    public override Dictionary<string, object> Parameters => new Dictionary<string, object>()
    {
      {
        "TimeSpentInScene",
        (object) this.TimeSpentInScene
      },
      {
        "FistLocomotionTime",
        (object) this.FistLocomotionTime
      },
      {
        "ThumbLocomotionTime",
        (object) this.ThumbLocomotionTime
      }
    };
  }
}
