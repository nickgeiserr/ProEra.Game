// Decompiled with JetBrains decompiler
// Type: TB12.AiTimingStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.XR;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Stores/AiTimingStore", fileName = "AiTimingStore")]
  [AppStore]
  public class AiTimingStore : ScriptableObject
  {
    private const float defaultAITiming = 0.167f;
    public float AiActionsInterval_SteamVR = 0.167f;
    public float AiActionsInterval_PSVR = 0.167f;
    public float AiActionsInterval_Quest1 = 0.167f;
    public float AiActionsInterval_Quest2 = 0.167f;

    public int GetAIUpdatedPerFrame() => Mathf.Max(22 / Mathf.CeilToInt(this.GetCurrentPlatformInterval() / 0.0166666675f), 2);

    public void Reset()
    {
      this.AiActionsInterval_SteamVR = 0.167f;
      this.AiActionsInterval_PSVR = 0.167f;
      this.AiActionsInterval_Quest1 = 0.167f;
      this.AiActionsInterval_Quest2 = 0.167f;
    }

    public float GetCurrentPlatformInterval()
    {
      string loadedDeviceName = XRSettings.loadedDeviceName;
      if (loadedDeviceName.Contains("PSVR") || loadedDeviceName.Contains("PlayStation") || loadedDeviceName.Contains("Playstation"))
        return this.AiActionsInterval_PSVR;
      if (loadedDeviceName.Contains("oculus") || loadedDeviceName.Contains("Oculus") || loadedDeviceName.Contains("Quest"))
        return loadedDeviceName.Contains("1") ? this.AiActionsInterval_Quest1 : this.AiActionsInterval_Quest2;
      loadedDeviceName.Contains("OpenXR");
      return this.AiActionsInterval_SteamVR;
    }
  }
}
