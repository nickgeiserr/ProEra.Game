// Decompiled with JetBrains decompiler
// Type: FootballVR.HandController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using FootballWorld;
using Framework;
using System;
using System.Collections;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class HandController : MonoBehaviour
  {
    [SerializeField]
    private HandsDataModel _handsDataModel;
    [SerializeField]
    private EHand _handness;
    [SerializeField]
    private HandRenderer _handRenderer;
    [SerializeField]
    private Transform _fingerTx;
    [SerializeField]
    private Transform _fingerTouchPoint;
    [SerializeField]
    private Transform _dragPointTx;
    [SerializeField]
    private Transform _uiAnchorPoint;
    [SerializeField]
    private Collider _handOpenCollider;
    [SerializeField]
    private Collider _fistCollider;
    [SerializeField]
    private Collider _fistTrigger;
    [SerializeField]
    private UserInteractCollider _interactCollider;
    [SerializeField]
    private GameObject _topWristGO;
    [SerializeField]
    private GameObject _bottomWristGO;
    [SerializeField]
    private PlayCallWristband _wristband;
    public Transform laserDragTransform;
    private HandData _data;
    private bool _ClickGrab;
    private readonly RoutineHandle _interactRoutine = new RoutineHandle();
    private InteractionSettings _interactionSettings;
    private ITouchGrabbable grabbedObj;
    private readonly Collider[] nearbyObjects = new Collider[100];

    public HandRenderer Renderer => this._handRenderer;

    public Transform attachPoint => this._handRenderer.attachPoint;

    public Vector3 position => this.transform.position;

    public Vector3 fingerPosition => this._fingerTx.position;

    public Vector3 dragPosition => this._dragPointTx.position;

    public Vector3 dragRotation => this._dragPointTx.eulerAngles;

    public Transform AnchorUI => this._uiAnchorPoint;

    public GameObject TopWristGO => this._topWristGO;

    public GameObject BottomWristGO => this._bottomWristGO;

    public PlayCallWristband Wristband => this._wristband;

    public event Action<EHand, BallObject> OnBallPicked;

    public event Action<EHand, EHandPose> OnPoseChanged;

    public event Action<BallObject> OnBallCollision;

    private void Awake()
    {
      this._handRenderer.Fist.OnValueChanged += new Action<bool>(this.HandleFist);
      this.HandleFist(false);
    }

    private void HandleFist(bool fist)
    {
      this._fistCollider.enabled = fist;
      this._fistTrigger.enabled = fist;
      this._handOpenCollider.enabled = !fist;
    }

    public void Initialize()
    {
      this._interactionSettings = this._handsDataModel.settings.InteractionSettings;
      this._data = this._handsDataModel.RegisterHand(this, this._handness);
      this._data.pose.OnValueChanged += new Action<EHandPose>(this.ApplyHandPose);
      this._data.input.objectInteractPressed.OnValueChanged += new Action<bool>(this.HandleObjectInteract);
      this._interactCollider.OnBallCollision += new Action<BallObject>(this.HandleBallCollision);
    }

    private void OnDestroy()
    {
      if ((UnityEngine.Object) this._interactCollider != (UnityEngine.Object) null)
      {
        this._interactCollider.OnBallCollision -= new Action<BallObject>(this.HandleBallCollision);
        Objects.SafeDestroy((UnityEngine.Object) this._interactCollider.gameObject);
      }
      if ((UnityEngine.Object) this._handRenderer != (UnityEngine.Object) null)
        this._handRenderer.Fist.OnValueChanged -= new Action<bool>(this.HandleFist);
      if (this._data == null)
        return;
      this._data.CurrentObject = (BallObject) null;
      this._data.pose.OnValueChanged -= new Action<EHandPose>(this.ApplyHandPose);
      this._data.input.objectInteractPressed.OnValueChanged -= new Action<bool>(this.HandleObjectInteract);
      this._handsDataModel.UnregisterHand(this);
    }

    public bool ClickHandleObjectInteract(ITouchGrabbable obj)
    {
      this._interactRoutine.Run(this.ClickInteractionRoutine(obj));
      return true;
    }

    private void HandleObjectInteract(bool pressed)
    {
      ITouchGrabbable closestObj;
      if (!pressed || !this.GetClosestDraggableObject(this.dragPosition, out closestObj))
        return;
      this._interactRoutine.Run(this.InteractionRoutine(closestObj));
    }

    public void Set3DObjectInHand(ITouchGrabbable obj)
    {
      if (this.grabbedObj?.ToString() == "null")
        this.grabbedObj = (ITouchGrabbable) null;
      this.DropCurrentItem();
      obj.OnTouchDragStart();
      obj.OnTouchDrag(this.position, (ITouchInput) this._data);
      this.grabbedObj = obj;
    }

    private bool GetClosestDraggableObject(Vector3 pos, out ITouchGrabbable closestObj)
    {
      closestObj = (ITouchGrabbable) null;
      float num1 = float.MaxValue;
      int num2 = 0;
      if (this._interactionSettings != null)
        num2 = Physics.OverlapSphereNonAlloc(pos, this._interactionSettings.InteractionRange, this.nearbyObjects, (int) WorldConstants.Layers.UI);
      for (int index = 0; index < num2; ++index)
      {
        Collider nearbyObject = this.nearbyObjects[index];
        ITouchGrabbable componentInParent = nearbyObject.GetComponentInParent<ITouchGrabbable>();
        if (componentInParent != null)
        {
          float sqrMagnitude = (nearbyObject.transform.position - pos).sqrMagnitude;
          if ((double) sqrMagnitude < (double) num1)
          {
            closestObj = componentInParent;
            num1 = sqrMagnitude;
          }
        }
      }
      return closestObj != null;
    }

    private IEnumerator InteractionRoutine(ITouchGrabbable obj)
    {
      Vector3 prevPos = this.position;
      yield return (object) null;
      VariableBool triggerPressed = this._data.input.objectInteractPressed;
      obj.OnTouchDragStart();
      this.grabbedObj = obj;
      if (obj.GetType() == typeof (TouchDrag3D))
        this.ApplyHandPose(EHandPose.Cradle);
      while (triggerPressed.Value)
      {
        yield return (object) null;
        Vector3 position = this.position;
        obj.OnTouchDrag(position - prevPos, (ITouchInput) this._data);
        prevPos = position;
      }
      Vector3 delta = this.position - prevPos;
      if (obj.GetType() == typeof (TouchDrag3D))
        this.ApplyHandPose(EHandPose.Empty);
      obj.OnTouchDragEnd(delta, (ITouchInput) this._data);
      if (obj == this.grabbedObj)
        this.grabbedObj = (ITouchGrabbable) null;
    }

    private IEnumerator ClickInteractionRoutine(ITouchGrabbable obj)
    {
      Vector3 prevPos = this.position;
      this._ClickGrab = true;
      yield return (object) null;
      obj.OnTouchDragStart();
      this.grabbedObj = obj;
      if (obj.GetType() == typeof (TouchDrag3D))
        this.ApplyHandPose(EHandPose.Cradle);
      while (this._ClickGrab)
      {
        yield return (object) null;
        Vector3 position = this.position;
        obj.OnTouchDrag(position - prevPos, (ITouchInput) this._data, true);
        prevPos = position;
      }
      Vector3 delta = this.position - prevPos;
      if (obj.GetType() == typeof (TouchDrag3D))
        this.ApplyHandPose(EHandPose.Empty);
      obj.OnTouchDragEnd(delta, (ITouchInput) this._data, true);
      if (obj == this.grabbedObj)
        this.grabbedObj = (ITouchGrabbable) null;
    }

    public void StopClickGrab() => this._ClickGrab = false;

    private void HandleBallCollision(BallObject ballObject)
    {
      Action<BallObject> onBallCollision = this.OnBallCollision;
      if (onBallCollision == null)
        return;
      onBallCollision(ballObject);
    }

    public void ApplyHandPose(EHandPose pose)
    {
      this._interactCollider.transform.position = this.transform.position;
      this._interactCollider.SetState(pose == EHandPose.Empty);
      this._handRenderer.SetHandPose(pose);
      Action<EHand, EHandPose> onPoseChanged = this.OnPoseChanged;
      if (onPoseChanged == null)
        return;
      onPoseChanged(this._handness, pose);
    }

    public void PickBall(BallObject ball)
    {
      ball.Pick();
      ball.transform.SetParentAndReset(this.attachPoint);
      Action<EHand, BallObject> onBallPicked = this.OnBallPicked;
      if (onBallPicked == null)
        return;
      onBallPicked(this._handness, ball);
    }

    public Transform GetDragPivot() => this._dragPointTx;

    public EHand GetDragHand() => this._handness;

    public Transform GetFingerTouchPoint() => this._fingerTouchPoint;

    public bool IsGrabbing() => this._interactRoutine.running;

    public ITouchGrabbable GetGrabbedObject() => this.grabbedObj;

    public HandData GetHandData() => this._data;

    public bool IsDominantHand() => (UnityEngine.Object) this._handsDataModel != (UnityEngine.Object) null && this._handsDataModel.ActiveHand != null && (UnityEngine.Object) this._handsDataModel.ActiveHand.hand == (UnityEngine.Object) this;

    public void SetIsDominantHand(bool IsDominantHand)
    {
      if (!(bool) (UnityEngine.Object) this._wristband)
        return;
      this._wristband.gameObject.SetActive(!IsDominantHand);
    }

    public void SetWristbandColliderEnabled(bool isEnabled)
    {
      if (!((UnityEngine.Object) this._wristband != (UnityEngine.Object) null))
        return;
      SphereCollider component = this._wristband.GetComponent<SphereCollider>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.enabled = isEnabled;
    }

    public void DropCurrentItem()
    {
      if (this.grabbedObj == null)
        return;
      ((TouchDrag3D) this.grabbedObj).PutMeBack();
      this.grabbedObj = (ITouchGrabbable) null;
    }

    public void DropBall() => this._data.CurrentObject = (BallObject) null;
  }
}
