// Decompiled with JetBrains decompiler
// Type: OculusFoveatedRenderingManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class OculusFoveatedRenderingManager : MonoBehaviour
{
  [SerializeField]
  private OVRManager.FixedFoveatedRenderingLevel settingToUse_Manager = OVRManager.FixedFoveatedRenderingLevel.High;
  [SerializeField]
  private OVRPlugin.FixedFoveatedRenderingLevel settingToUse_Plugin = OVRPlugin.FixedFoveatedRenderingLevel.High;

  private void OnEnable() => this.EnableFoveatedRendering(true);

  public void EnableFoveatedRendering(bool enabled)
  {
    OVRPlugin.SystemHeadset systemHeadsetType = OVRPlugin.GetSystemHeadsetType();
    if ((systemHeadsetType == OVRPlugin.SystemHeadset.Oculus_Quest ? 1 : (systemHeadsetType == OVRPlugin.SystemHeadset.Oculus_Quest_2 ? 1 : 0)) == 0)
      return;
    if (enabled)
    {
      OVRManager.fixedFoveatedRenderingLevel = this.settingToUse_Manager;
      OVRPlugin.fixedFoveatedRenderingLevel = this.settingToUse_Plugin;
      OVRManager.useDynamicFixedFoveatedRendering = true;
      OVRPlugin.useDynamicFixedFoveatedRendering = true;
    }
    else
    {
      OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.Off;
      OVRPlugin.fixedFoveatedRenderingLevel = OVRPlugin.FixedFoveatedRenderingLevel.Off;
      OVRManager.useDynamicFixedFoveatedRendering = false;
      OVRPlugin.useDynamicFixedFoveatedRendering = false;
    }
    Debug.Log((object) ("<color=green>OCULUS Foveated Rendering has been " + (enabled ? "ENABLED" : "DISABLED") + "</color>"));
  }
}
