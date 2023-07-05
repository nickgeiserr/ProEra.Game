// Decompiled with JetBrains decompiler
// Type: VRHapticsController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.XR;

public class VRHapticsController : MonoBehaviour
{
  [SerializeField]
  private XRNode _leftHandNode = XRNode.LeftHand;
  [SerializeField]
  private XRNode _rightHandNode = XRNode.LeftHand;

  public void SendHapticPulseFromNode(XRNode node, float amplitude, float duration)
  {
    InputDevice deviceAtXrNode = InputDevices.GetDeviceAtXRNode(node);
    HapticCapabilities capabilities;
    if (!deviceAtXrNode.TryGetHapticCapabilities(out capabilities) || !capabilities.supportsImpulse)
      return;
    uint channel = 0;
    deviceAtXrNode.SendHapticImpulse(channel, amplitude, duration);
  }

  public void SendHapticPulseFromBothNodes(float amplitude, float duration)
  {
    InputDevice deviceAtXrNode1 = InputDevices.GetDeviceAtXRNode(this._leftHandNode);
    InputDevice deviceAtXrNode2 = InputDevices.GetDeviceAtXRNode(this._rightHandNode);
    HapticCapabilities capabilities;
    if (deviceAtXrNode1.TryGetHapticCapabilities(out capabilities) && capabilities.supportsImpulse)
    {
      uint channel = 0;
      deviceAtXrNode1.SendHapticImpulse(channel, amplitude, duration);
    }
    if (!deviceAtXrNode2.TryGetHapticCapabilities(out capabilities) || !capabilities.supportsImpulse)
      return;
    uint channel1 = 0;
    deviceAtXrNode2.SendHapticImpulse(channel1, amplitude, duration);
  }
}
