// Decompiled with JetBrains decompiler
// Type: InputHandSelector
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class InputHandSelector : MonoBehaviour
{
  [SerializeField]
  private OVRInputModule m_InputModule;
  private PlayerRig m_CameraRig;
  private bool _initialized;
  public static bool isEnabled = true;
  private HandController currentHandController;
  private OVRInput.Controller _currentController;

  private OVRInput.Controller CurrentController
  {
    set
    {
      if (this._currentController == value || !this._initialized)
        return;
      this._currentController = value;
      int num;
      switch (value)
      {
        case OVRInput.Controller.None:
          this.m_InputModule.rayTransform = PersistentSingleton<PlayerCamera>.Instance.transform;
          return;
        case OVRInput.Controller.LTouch:
          num = 1;
          break;
        default:
          num = (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? 1 : 0;
          break;
      }
      Transform transform = num != 0 ? this.m_CameraRig.leftControllerAnchor : this.m_CameraRig.rightControllerAnchor;
      this.currentHandController = transform.GetComponentInChildren<HandController>();
      ControllerAnchor component = transform.GetComponent<ControllerAnchor>();
      if ((Object) component != (Object) null && (Object) component.targetTx != (Object) null)
        transform = component.targetTx;
      this.m_InputModule.rayTransform = transform;
    }
  }

  private void Start()
  {
    if ((Object) this.m_CameraRig == (Object) null)
      this.m_CameraRig = PersistentSingleton<GamePlayerController>.Instance.Rig;
    if ((Object) this.m_InputModule == (Object) null)
      this.m_InputModule = Object.FindObjectOfType<OVRInputModule>();
    this._initialized = true;
  }

  private void Update()
  {
    if (!XRSettings.isDeviceActive)
      this.CurrentController = OVRInput.Controller.None;
    if (!InputHandSelector.isEnabled)
      return;
    this.CurrentController = OVRInput.GetActiveController();
  }

  private void GrabStartCheck(GameObject obj)
  {
    ITouchGrabbable componentInParent = obj.GetComponentInParent<ITouchGrabbable>();
    if (componentInParent == null || !(bool) (Object) this.currentHandController)
      return;
    this.currentHandController.ClickHandleObjectInteract(componentInParent);
  }

  private void GrabEndCheck()
  {
    if (!(bool) (Object) this.currentHandController)
      return;
    this.currentHandController.StopClickGrab();
  }
}
