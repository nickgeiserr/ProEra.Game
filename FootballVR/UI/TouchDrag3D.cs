// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchDrag3D
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FootballVR.UI
{
  public class TouchDrag3D : MonoBehaviour, ITouchGrabbable, IPointerDownHandler, IEventSystemHandler
  {
    [SerializeField]
    private float resetDelay = 2f;
    [SerializeField]
    private Rigidbody rgBody;
    [SerializeField]
    private Transform lHandPivot;
    [SerializeField]
    private Transform rHandPivot;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private Vector3 _initPosition;
    private Vector3 _initRotation;
    private bool _isDrag;
    private Transform _initParent;
    private Vector3 _lHandLocalPos;
    private Vector3 _rHandLocalPos;
    private Vector3 _lHandLocalEulerAngles;
    private Vector3 _rHandLocalEulerAngles;
    public Action<ITouchInput> OnGrabObject;
    public Action<ITouchInput> OnDropObject;
    public Action<ITouchInput> OnResetObject;
    private bool isLocked;
    [SerializeField]
    private HighlightShimmerManager.ELockerRoomObjects eShimmerObjectType;

    private void Start()
    {
      Transform transform = this.transform;
      this._initPosition = transform.position;
      this._initRotation = transform.eulerAngles;
      this._initParent = transform.parent;
      if ((UnityEngine.Object) this.lHandPivot != (UnityEngine.Object) null)
      {
        this._lHandLocalPos = this.lHandPivot.localPosition;
        this._lHandLocalEulerAngles = this.lHandPivot.localEulerAngles;
      }
      if (!((UnityEngine.Object) this.rHandPivot != (UnityEngine.Object) null))
        return;
      this._rHandLocalPos = this.rHandPivot.localPosition;
      this._rHandLocalEulerAngles = this.rHandPivot.localEulerAngles;
    }

    public void Reset(ITouchInput target)
    {
      Transform transform = this.transform;
      transform.parent = this._initParent;
      transform.position = this._initPosition;
      transform.eulerAngles = this._initRotation;
      if ((UnityEngine.Object) this.rgBody != (UnityEngine.Object) null)
      {
        this.rgBody.velocity = Vector3.zero;
        if ((UnityEngine.Object) this.rgBody.transform != (UnityEngine.Object) transform)
        {
          this.rgBody.transform.localPosition = Vector3.zero;
          this.rgBody.transform.localEulerAngles = Vector3.zero;
        }
        this.rgBody.isKinematic = true;
      }
      this._isDrag = false;
      this.Lock(false);
      Action<ITouchInput> onResetObject = this.OnResetObject;
      if (onResetObject == null)
        return;
      onResetObject(target);
    }

    public void Lock(bool value) => this.isLocked = value;

    public void OnTouchDragStart()
    {
      if (this.isLocked)
        return;
      this._routineHandle.Stop();
      if (!((UnityEngine.Object) this.rgBody != (UnityEngine.Object) null))
        return;
      if ((UnityEngine.Object) this.rgBody.transform != (UnityEngine.Object) this.transform)
      {
        this.rgBody.transform.localPosition = Vector3.zero;
        this.rgBody.transform.localEulerAngles = Vector3.zero;
      }
      this.rgBody.isKinematic = true;
      HighlightShimmerManager.AddToInteractionHistory(this.eShimmerObjectType);
    }

    public void OnTouchDrag(Vector3 delta, ITouchInput touchInput, bool usingLaserGrab = false)
    {
      if (this.isLocked)
        return;
      if ((bool) ScriptableSingleton<VRSettings>.Instance.OneHandedMode && touchInput.dragHand == EHand.Left == (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
      {
        this.SetOnOffHand();
      }
      else
      {
        if (this._isDrag)
          return;
        this._isDrag = true;
        Transform transform1 = this.transform;
        if ((UnityEngine.Object) this.lHandPivot != (UnityEngine.Object) null && (UnityEngine.Object) this.rHandPivot != (UnityEngine.Object) null)
        {
          Transform transform2 = touchInput.dragHand == EHand.Left ? this.lHandPivot : this.rHandPivot;
          Vector3 localPosition = transform2.localPosition;
          Vector3 localEulerAngles = transform2.localEulerAngles;
          transform2.parent = touchInput.dragPivot;
          transform2.localPosition = localPosition;
          transform2.localEulerAngles = localEulerAngles;
          transform1.parent = transform2;
          transform1.localPosition = Vector3.zero;
          transform1.localEulerAngles = Vector3.zero;
        }
        else
        {
          transform1.parent = touchInput.dragPivot;
          transform1.localPosition = Vector3.zero;
          transform1.localEulerAngles = Vector3.zero;
        }
        Action<ITouchInput> onGrabObject = this.OnGrabObject;
        if (onGrabObject == null)
          return;
        onGrabObject(touchInput);
      }
    }

    public void OnTouchDragEnd(Vector3 delta, ITouchInput touchInput, bool usingLaserGrab = false)
    {
      if (this.isLocked)
        return;
      this._isDrag = false;
      Transform transform = this.transform;
      if ((UnityEngine.Object) this.lHandPivot != (UnityEngine.Object) null && (UnityEngine.Object) this.rHandPivot != (UnityEngine.Object) null)
      {
        transform.parent = this._initParent;
        this.lHandPivot.parent = transform;
        this.lHandPivot.localPosition = this._lHandLocalPos;
        this.lHandPivot.localEulerAngles = this._lHandLocalEulerAngles;
        this.rHandPivot.parent = transform;
        this.rHandPivot.localPosition = this._rHandLocalPos;
        this.rHandPivot.localEulerAngles = this._rHandLocalEulerAngles;
      }
      else
        transform.parent = this._initParent;
      if ((UnityEngine.Object) this.rgBody != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) this.rgBody.transform != (UnityEngine.Object) transform)
          this.rgBody.transform.localPosition = Vector3.zero;
        this.rgBody.isKinematic = false;
      }
      if ((double) this.resetDelay >= 0.0)
        this._routineHandle.Run(this.ResetAfterDelay(touchInput));
      Action<ITouchInput> onDropObject = this.OnDropObject;
      if (onDropObject == null)
        return;
      onDropObject(touchInput);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    private void OnDestroy()
    {
      this._routineHandle.Stop();
      Action<ITouchInput> onResetObject = this.OnResetObject;
      if (onResetObject == null)
        return;
      onResetObject((ITouchInput) null);
    }

    private IEnumerator ResetAfterDelay(ITouchInput target)
    {
      yield return (object) new WaitForSeconds(this.resetDelay);
      this.Reset(target);
    }

    public void SetOnOffHand()
    {
      if (!(bool) ScriptableSingleton<VRSettings>.Instance.OneHandedMode)
        return;
      ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? PlayerAvatar.Instance.RightController : PlayerAvatar.Instance.LeftController).Set3DObjectInHand((ITouchGrabbable) this);
      this.Lock(true);
    }

    public void PutMeBack() => this.Reset((ITouchInput) null);
  }
}
