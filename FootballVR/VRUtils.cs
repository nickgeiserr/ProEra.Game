// Decompiled with JetBrains decompiler
// Type: FootballVR.VRUtils
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine.XR;

namespace FootballVR
{
  public static class VRUtils
  {
    public static string OPENVR_UNITY_NAME_STR = "OpenXR Display";

    public static VRUtils.DeviceType GetDeviceType()
    {
      string name = InputDevices.GetDeviceAtXRNode(XRNode.RightHand).name;
      if (name == null)
        return VRUtils.DeviceType.Oculus;
      if (name.Contains("Index"))
        return VRUtils.DeviceType.Index;
      return name.Contains("Vive") ? VRUtils.DeviceType.Vive : VRUtils.DeviceType.Oculus;
    }

    public static bool ViveConnected { get; } = XRSettings.loadedDeviceName == VRUtils.OPENVR_UNITY_NAME_STR;

    public static bool BothHandsConnected => VRInputManager.BothHandsConnected();

    public enum DeviceType
    {
      Oculus,
      PSVR2,
      Vive,
      Index,
      PSVR,
    }
  }
}
