// Decompiled with JetBrains decompiler
// Type: ProEra.Game.LaserInput
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.Networked;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12;
using TB12.UI;
using TB12.UI.Screens;
using UnityEngine;
using Vars;

namespace ProEra.Game
{
  public class LaserInput : MonoBehaviour
  {
    private bool _active;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private VRInputManager.Controller currentController;
    [SerializeField]
    private Color defaultColor = Color.white;
    [SerializeField]
    private Color clickColor = Color.cyan;
    [SerializeField]
    private Material pointerMaterial;
    [SerializeField]
    private HandController _handController;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    public LayerMask interactionMask;
    private LaserInput.PointerEventHandler pointerInHandle;
    private LaserInput.PointerEventHandler pointerOutHandle;
    private Vector3[] linePoints = new Vector3[2];
    private Collider previousContact;
    private bool wasClicked;
    private bool isGrabbed;
    private float currentrLinerendererTime;
    private float linerendererExtendTime = 0.5f;
    private float linerendererRetractTime = 0.8f;
    private float currentLaserLength;
    [SerializeField]
    private Transform _pointer;
    [SerializeField]
    private MeshRenderer _pointerRenderer;

    public bool Active
    {
      get => this._active;
      set
      {
        if (this._active != value && (UnityEngine.Object) this.lineRenderer != (UnityEngine.Object) null)
          this.lineRenderer.enabled = value;
        if (!value && (UnityEngine.Object) this.pointer != (UnityEngine.Object) null)
          this.pointer.gameObject.SetActive(false);
        this._active = value;
      }
    }

    public event LaserInput.PointerEventHandler PointerIn;

    public event LaserInput.PointerEventHandler PointerOut;

    public event LaserInput.PointerEventHandler PointerClick;

    private Transform pointer
    {
      get
      {
        if ((UnityEngine.Object) this._pointer == (UnityEngine.Object) null)
          this.CreatePointer();
        return this._pointer;
      }
    }

    private void Start()
    {
      this.Active = this.CheckForActivity();
      this.lineRenderer.enabled = this.Active;
      if ((UnityEngine.Object) this._handController != (UnityEngine.Object) null)
        this._handController.laserDragTransform = this.pointer;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.UseLeftHand, new Action<bool>(this.ActiveHandChangedHandler)),
        EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<VRSettings>.Instance.UseVrLaser, new Action<bool>(this.ActiveLaserChangedHandler))
      });
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void ActiveHandChangedHandler(bool leftHand)
    {
      VRInputManager.LeftLaserInput = leftHand;
      VRInputManager.RightLaserInput = !leftHand;
    }

    private void ActiveLaserChangedHandler(bool laserEnabled) => this.Active = laserEnabled;

    private void CreatePointer()
    {
      if (!((UnityEngine.Object) this._pointer == (UnityEngine.Object) null))
        return;
      GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
      this._pointer = primitive.transform;
      this._pointerRenderer = primitive.GetComponent<MeshRenderer>();
      this._pointerRenderer.material = this.pointerMaterial;
      this._pointer.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    private void Update()
    {
      if (!VRState.InterationWithUI.Value)
        return;
      bool flag = this.IsVRClickDown();
      this.CheckForLaserActivity(this.CheckForActivity());
      this.HandleLaserIsGrabbing(flag);
      if (this.CheckLaserShouldBeOff())
      {
        this.Active = false;
      }
      else
      {
        bool a_triggerClick = this.CheckVRClick(flag);
        if (!this.Active)
          return;
        float a_defaultDistance = 0.12f;
        float a_dist = 2f;
        UnityEngine.RaycastHit a_hit;
        this.PerformLaserRaycast(this.transform.position, this.transform.forward, out a_hit, a_dist, (LayerMask) this.interactionMask.value);
        this.HandleLaserReticle(a_hit);
        this.HandleLaserHover(a_hit);
        this.HandleLaserTriggers(a_hit, a_triggerClick, flag);
        this.HandleLaserLineRenderer(a_hit, a_defaultDistance);
      }
    }

    private void HandleLaserIsGrabbing(bool a_triggerHold)
    {
      if (!this.isGrabbed || a_triggerHold)
        return;
      this.GrabEndCheck();
    }

    private void CheckForLaserActivity(bool a_hasActivity)
    {
      if (this.Active && !a_hasActivity)
        this.Active = false;
      if (!this.Active & a_hasActivity)
        this.Active = true;
      if (this.Active)
        return;
      if ((UnityEngine.Object) this.pointer != (UnityEngine.Object) null && this.pointer.gameObject.activeSelf)
        this.pointer.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this.lineRenderer != (UnityEngine.Object) null) || !this.lineRenderer.enabled)
        return;
      this.lineRenderer.enabled = false;
    }

    private bool CheckLaserShouldBeOff() => !PauseScreen.isPaused && !UIPanel.laserIsAlwaysEnabled && !PauseMenuMultiplayer._multiplayerLaserEnabled && !MinicampIntroUi._minicampMenuIsOpen && !(bool) PlaybookState.IsShown.GetValue() && ((bool) NetworkState.InRoom || AppState.IsInMiniCamp() || AppState.InFTUETunnel || AppState.InCoinToss || MatchManager.Exists() && global::Game.CurrentPlayHasUserQBOnField);

    private bool PerformLaserRaycast(
      Vector3 a_pos,
      Vector3 a_dir,
      out UnityEngine.RaycastHit a_hit,
      float a_dist,
      LayerMask a_layer)
    {
      bool flag = false;
      if (Physics.Raycast(a_pos, a_dir, out a_hit, a_dist, (int) a_layer))
        flag = (UnityEngine.Object) a_hit.collider != (UnityEngine.Object) null;
      return flag;
    }

    private void HandleLaserHover(UnityEngine.RaycastHit a_hit)
    {
      int num = (UnityEngine.Object) a_hit.collider != (UnityEngine.Object) null ? 1 : 0;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = (UnityEngine.Object) this.previousContact != (UnityEngine.Object) null;
      bool flag4 = num == 0 & flag3;
      if (num != 0)
      {
        flag2 = (UnityEngine.Object) this.previousContact != (UnityEngine.Object) a_hit.collider;
        flag1 = flag3 & flag2;
      }
      if (flag1 | flag4)
      {
        this.OnPointerOut(new LaserInput.PointerEventArgs()
        {
          distance = 0.0f,
          target = this.previousContact
        });
        this.previousContact = (Collider) null;
      }
      if (!flag2)
        return;
      this.OnPointerIn(new LaserInput.PointerEventArgs()
      {
        distance = a_hit.distance,
        target = a_hit.collider
      });
      this.previousContact = a_hit.collider;
    }

    private void SetupLaserHighlightHandlers(object a_sender, LaserInput.PointerEventArgs args1)
    {
      Collider target = args1.target;
      TouchButton buttonTarget = target.GetComponent<TouchButton>();
      TouchUI2DButton buttonTarget2d = target.GetComponent<TouchUI2DButton>();
      TouchUI2DScrollRect component = target.GetComponent<TouchUI2DScrollRect>();
      LockerRoomObjEvent componentInParent = target.GetComponentInParent<LockerRoomObjEvent>();
      this.pointerInHandle = (LaserInput.PointerEventHandler) null;
      this.pointerOutHandle = (LaserInput.PointerEventHandler) null;
      this.pointerInHandle = (LaserInput.PointerEventHandler) ((sender, args2) => { });
      this.pointerOutHandle = (LaserInput.PointerEventHandler) ((sender, args3) =>
      {
        this.PointerIn -= this.pointerInHandle;
        this.PointerOut -= this.pointerOutHandle;
      });
      if ((UnityEngine.Object) buttonTarget != (UnityEngine.Object) null)
      {
        this.pointerInHandle += (LaserInput.PointerEventHandler) ((sender, args4) => buttonTarget.SetLaserHighlight(true));
        this.pointerOutHandle += (LaserInput.PointerEventHandler) ((sender, args5) => buttonTarget.SetLaserHighlight(false));
      }
      if ((UnityEngine.Object) buttonTarget2d != (UnityEngine.Object) null)
      {
        this.pointerInHandle += (LaserInput.PointerEventHandler) ((sender, args6) => buttonTarget2d.SetLaserHovering(true));
        this.pointerOutHandle += (LaserInput.PointerEventHandler) ((sender, args7) => buttonTarget2d.SetLaserHovering(false));
      }
      int num1 = (UnityEngine.Object) componentInParent != (UnityEngine.Object) null ? 1 : 0;
      int num2 = (UnityEngine.Object) component != (UnityEngine.Object) null ? 1 : 0;
      this.PointerIn += this.pointerInHandle;
      this.PointerOut += this.pointerOutHandle;
    }

    private void HandleLaserReticle(UnityEngine.RaycastHit a_hit)
    {
      if ((UnityEngine.Object) a_hit.collider != (UnityEngine.Object) null)
      {
        this.pointer.gameObject.SetActive(true);
        this.pointer.transform.position = a_hit.point;
        float num1 = 0.8f;
        float num2 = Mathf.Lerp(0.01f, 0.15f, Mathf.Clamp((float) (((double) a_hit.distance - 0.30000001192092896) / 3.0), 0.0f, 1f)) * num1;
        this.pointer.localScale = new Vector3(num2, num2, num2);
        this.pointer.forward = this.transform.forward;
      }
      else
      {
        if (this.pointer.gameObject.activeSelf)
          this.pointer.gameObject.SetActive(false);
        if (!((UnityEngine.Object) this._pointerRenderer != (UnityEngine.Object) null))
          return;
        this._pointerRenderer.material.color = this.defaultColor;
      }
    }

    private void HandleLaserTriggers(UnityEngine.RaycastHit a_hit, bool a_triggerClick, bool a_triggerHold)
    {
      if (!((UnityEngine.Object) a_hit.collider != (UnityEngine.Object) null))
        return;
      Collider collider = a_hit.collider;
      TouchButton component1 = collider.GetComponent<TouchButton>();
      TouchUI2DButton component2 = collider.GetComponent<TouchUI2DButton>();
      TouchUI2DScrollRect component3 = collider.GetComponent<TouchUI2DScrollRect>();
      LockerRoomObjEvent componentInParent = collider.GetComponentInParent<LockerRoomObjEvent>();
      if (a_triggerClick)
      {
        if (!this.isGrabbed && (UnityEngine.Object) collider.gameObject != (UnityEngine.Object) null)
          this.GrabStartCheck(collider.gameObject);
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.HandlePressStateChanged(true);
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          component2.OnClick(this._handController);
        if ((UnityEngine.Object) componentInParent != (UnityEngine.Object) null)
        {
          componentInParent.SimulateClick();
          this.AdjustLasersOnGrab();
        }
        this.OnPointerClick(new LaserInput.PointerEventArgs()
        {
          distance = a_hit.distance,
          target = collider
        });
      }
      if (!a_triggerHold || !((UnityEngine.Object) component3 != (UnityEngine.Object) null))
        return;
      component3.TryPerformScroll(a_hit.point);
    }

    private void HandleLaserLineRenderer(UnityEngine.RaycastHit a_hit, float a_defaultDistance)
    {
      float num1;
      if ((UnityEngine.Object) a_hit.collider != (UnityEngine.Object) null)
      {
        float min = 0.05f;
        this.currentrLinerendererTime += Time.deltaTime / this.linerendererExtendTime;
        this.currentrLinerendererTime = Mathf.Clamp01(this.currentrLinerendererTime);
        float num2 = Mathf.Clamp(a_hit.distance - min, min, a_hit.distance - min);
        num1 = a_defaultDistance + num2 * this.currentrLinerendererTime;
        this.lineRenderer.material.color = this.clickColor;
        if ((UnityEngine.Object) this._pointerRenderer != (UnityEngine.Object) null)
          this._pointerRenderer.material.color = this.clickColor;
      }
      else
      {
        this.currentrLinerendererTime -= Time.deltaTime / this.linerendererRetractTime;
        this.currentrLinerendererTime = Mathf.Clamp01(this.currentrLinerendererTime);
        num1 = a_defaultDistance + this.currentLaserLength * this.currentrLinerendererTime;
        this.lineRenderer.material.color = this.defaultColor;
        if ((UnityEngine.Object) this._pointerRenderer != (UnityEngine.Object) null)
          this._pointerRenderer.material.color = this.defaultColor;
      }
      this.SetLineWidth(num1);
    }

    private bool IsInLayerMask(LayerMask target, LayerMask layerMask) => (layerMask.value & 1 << (int) target) > 0;

    private bool CheckForActivity()
    {
      if (!(bool) ScriptableSingleton<VRSettings>.Instance.UseVrLaser)
        return false;
      if (!VRInputManager.LeftLaserInput && !VRInputManager.RightLaserInput)
        VRInputManager.RightLaserInput = !(VRInputManager.LeftLaserInput = (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand);
      return this.currentController == VRInputManager.Controller.LeftHand && VRInputManager.LeftLaserInput || this.currentController == VRInputManager.Controller.RightHand && VRInputManager.RightLaserInput;
    }

    private bool CheckVRClick(bool isDown)
    {
      if (this.wasClicked & isDown)
        return false;
      this.wasClicked = isDown;
      return isDown;
    }

    private bool IsVRClickDown() => (double) VRInputManager.Get(VRInputManager.Axis1D.Trigger, this.currentController) > 0.5;

    private void SetLineWidth(float value)
    {
      if (Mathf.Approximately(this.linePoints[1].z, value))
        return;
      this.currentLaserLength = value;
      this.linePoints[1] = new Vector3(0.0f, 0.0f, this.currentLaserLength);
      this.lineRenderer.SetPositions(this.linePoints);
    }

    private void OnPointerIn(LaserInput.PointerEventArgs e)
    {
      if (!this.pointer.gameObject.activeSelf)
        this.pointer.gameObject.SetActive(true);
      this.SetupLaserHighlightHandlers((object) this, e);
      LaserInput.PointerEventHandler pointerIn = this.PointerIn;
      if (pointerIn == null)
        return;
      pointerIn((object) this, e);
    }

    private void OnPointerOut(LaserInput.PointerEventArgs e)
    {
      if (this.pointer.gameObject.activeSelf)
        this.pointer.gameObject.SetActive(false);
      LaserInput.PointerEventHandler pointerOut = this.PointerOut;
      if (pointerOut == null)
        return;
      pointerOut((object) this, e);
    }

    private void OnPointerClick(LaserInput.PointerEventArgs e)
    {
      LaserInput.PointerEventHandler pointerClick = this.PointerClick;
      if (pointerClick == null)
        return;
      pointerClick((object) this, e);
    }

    private bool GrabStartCheck(GameObject obj)
    {
      if ((UnityEngine.Object) obj != (UnityEngine.Object) null)
      {
        ITouchGrabbable componentInParent = obj.GetComponentInParent<ITouchGrabbable>();
        ITouchGrabbable touchGrabbable = componentInParent == null ? obj.GetComponent<ITouchGrabbable>() : componentInParent;
        if (touchGrabbable != null && (UnityEngine.Object) this._handController != (UnityEngine.Object) null)
        {
          if (this.currentController == VRInputManager.Controller.RightHand)
          {
            if ((UnityEngine.Object) PlayerAvatar.GetLeftHand() != (UnityEngine.Object) null && PlayerAvatar.GetLeftHand().GetGrabbedObject() == touchGrabbable)
              return false;
          }
          else if ((UnityEngine.Object) PlayerAvatar.GetRightHand() != (UnityEngine.Object) null && PlayerAvatar.GetRightHand().GetGrabbedObject() == touchGrabbable)
            return false;
          this.isGrabbed = this._handController.ClickHandleObjectInteract(touchGrabbable);
          return true;
        }
      }
      return false;
    }

    private void GrabEndCheck()
    {
      if ((bool) (UnityEngine.Object) this._handController)
        this._handController.StopClickGrab();
      this.isGrabbed = false;
      this.AdjustLasersOnGrab();
    }

    private void AdjustLasersOnGrab()
    {
      int num = (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? 1 : 0;
      bool flag1 = this.currentController == VRInputManager.Controller.LeftHand;
      bool flag2 = this.currentController == VRInputManager.Controller.RightHand;
      if (num != 0)
      {
        if (!flag1)
          return;
        VRInputManager.LeftLaserInput = !this.isGrabbed;
        VRInputManager.RightLaserInput = this.isGrabbed;
      }
      else
      {
        if (!flag2)
          return;
        VRInputManager.LeftLaserInput = this.isGrabbed;
        VRInputManager.RightLaserInput = !this.isGrabbed;
      }
    }

    public delegate void PointerEventHandler(object sender, LaserInput.PointerEventArgs e);

    public struct PointerEventArgs
    {
      public float distance;
      public Collider target;
    }
  }
}
