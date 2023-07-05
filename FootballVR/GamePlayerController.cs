// Decompiled with JetBrains decompiler
// Type: FootballVR.GamePlayerController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using Framework.Data;
using Framework.Networked;
using PSVR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Management;

namespace FootballVR
{
  public class GamePlayerController : PersistentSingleton<GamePlayerController>
  {
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private VRCameraFade _fade;
    [SerializeField]
    private BulletTimeInfo _bulletTime;
    [SerializeField]
    private VRColorAdjustments _colorAdjustments;
    [SerializeField]
    private PlayerRig _rig;
    [SerializeField]
    private ScreenWipeTransition _transitionPrefab;
    [SerializeField]
    private GamePlayerController.PlayerTrackingRefs _playerTracking;
    [SerializeField]
    private Rect _fieldBounds;
    [SerializeField]
    private VRHapticsController _haptics;
    [SerializeField]
    private Transform _trackingSpace;
    [SerializeField]
    private GamePlayerController.PSVRRefs _psvrRefs;
    [SerializeField]
    private GamePlayerController.QuestRefs _questTracking;
    private Transform _tx;
    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;
    private bool _collisionInitialized;
    private PlayerCollisionHandler _collisionHandler;
    private bool _isInActive;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private const float _inchesToMeters = 0.0254f;
    private readonly float _minPlayerHeight = 1.778f;
    private readonly float _maxPlayerHeight = 1.905f;
    private readonly float _seatPlayerHeight = 1.2192f;
    private const float _playerHeightFix = 0.82f;
    private bool _previousHandState;
    private Vector3 _trackingSpaceOffset;
    private Vector3 _trackingSpaceStanding;
    private bool _isSeated;
    private bool _isSeatedMode = true;
    private bool _didMoveThisFrame;
    private float _speedScale = 1f;
    private float _footstepMoveCounter;
    private const float FOOTSTEP_MOVE_ACTIVATION = 0.5f;
    private readonly Vector3 _raycastOffsetFromPlayerBase = new Vector3(0.0f, 0.1f, 0.0f);
    private bool _isFaded;
    private MotionTracker _motionTracker;
    private bool _isNoClip;

    public PlayerRig Rig => this._rig;

    public VRColorAdjustments ColorAdjustments => this._colorAdjustments;

    public static VRCameraFade CameraFade => PersistentSingleton<GamePlayerController>.Instance._fade;

    public static BulletTimeInfo BulletTime => PersistentSingleton<GamePlayerController>.Instance._bulletTime;

    public PlayerCollisionHandler CollisionHandler => this._collisionHandler;

    public GamePlayerController.PlayerTrackingRefs PlayerRefs => this._playerTracking;

    public Transform TrackingSpace => this._trackingSpace;

    public VRCameraFade CameraFader => this._fade;

    public VRHapticsController HapticsController => this._haptics;

    public bool IsInActive
    {
      get => this._isInActive;
      set => this._isInActive = value;
    }

    public bool IsNoClip
    {
      get => this._isNoClip;
      set => this._isNoClip = value;
    }

    public bool IsFaded => this._isFaded;

    public bool AvoidWallPenetration { get; set; }

    public float SpeedScale
    {
      get => this._speedScale;
      set => this._speedScale = value;
    }

    public Vector3 position
    {
      get => this._tx.position;
      set
      {
        UnityEngine.RaycastHit hitInfo;
        if (Physics.Raycast(this.transform.position + this._raycastOffsetFromPlayerBase, Vector3.down, out hitInfo, float.MaxValue))
          value.y = this._isNoClip ? value.y : hitInfo.point.y;
        else if (!this._isNoClip)
          Debug.LogError((object) "Player is not above ground");
        value.x = this._isNoClip ? value.x : Mathf.Clamp(value.x, this._fieldBounds.x, this._fieldBounds.xMax);
        value.z = this._isNoClip ? value.z : Mathf.Clamp(value.z, this._fieldBounds.y, this._fieldBounds.yMax);
        this._tx.position = value;
      }
    }

    public Quaternion rotation => this._tx.rotation;

    public Vector3 forward => this._playerTracking.headAnchor.forward;

    public Vector3 velocity => (UnityEngine.Object) this._motionTracker != (UnityEngine.Object) null ? this._motionTracker.Velocity : Vector3.zero;

    protected override void Awake()
    {
      base.Awake();
      this._tx = this.transform;
      this._defaultPosition = this._tx.position;
      this._defaultRotation = this._tx.rotation;
      this._motionTracker = this.GetComponent<MotionTracker>();
      this.UpdateOneHandedSettings(false);
      UnityEngine.Object.Instantiate<ScreenWipeTransition>(this._transitionPrefab, this._playerTracking.headAnchor);
      this._trackingSpaceOffset = this._trackingSpace.localPosition;
      this._trackingSpaceStanding = this._trackingSpaceOffset;
      this.HandleBodyHeight(this._playerProfile.Customization.BodyHeight.Value);
    }

    private void Start()
    {
      ScriptableSettings.Initialiation.Link(new System.Action(this.FinishedInitializingSettings));
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        VREvents.BlinkMovePlayer.Link<float, Vector3, Quaternion>(new Action<float, Vector3, Quaternion>(this.HandleBlinkMovePlayer))
      });
      if (!((UnityEngine.Object) this._fade != (UnityEngine.Object) null))
        return;
      this._fade.OnFadeStateChanged += new Action<bool>(this.HandleFadeStateChanged);
    }

    private void FinishedInitializingSettings()
    {
      ScriptableSingleton<VRSettings>.Instance.SeatedMode.OnValueChanged += new Action<bool>(this.HandleSeatedChange);
      ScriptableSingleton<VRSettings>.Instance.UseLeftHand.OnValueChanged += new Action<bool>(this.HandleDominantHandChange);
      this.HandleDominantHandChange(false);
      this.HandleSeatedChange(ScriptableSingleton<VRSettings>.Instance.SeatedMode.Value);
      ScriptableSingleton<VRSettings>.Instance.OneHandedMode.OnValueChanged += new Action<bool>(this.UpdateOneHandedSettings);
      WorldState.HighDefGrass.OnValueChanged += new Action<GameObject>(this.UpdateFollowGrass);
      this._playerProfile.Customization.BodyHeight.OnValueChanged += new Action<float>(this.HandleBodyHeight);
      this.HandleBodyHeight(this._playerProfile.Customization.BodyHeight.Value);
    }

    private void HandleBlinkMovePlayer(
      float blinkTime,
      Vector3 newPlayerPosition,
      Quaternion newPlayerRotation)
    {
      this._routineHandle.Run(this.DoMovementTransition(blinkTime, newPlayerPosition, newPlayerRotation));
    }

    private void HandleSeatedChange(bool seated)
    {
      if (!seated)
        this.HandleBodyHeight(this._playerProfile.Customization.BodyHeight.Value);
      this._isSeated = seated;
    }

    private void HandleBodyHeight(float value)
    {
      this._trackingSpaceStanding = new Vector3(0.0f, value / 1.92f - 0.82f, 0.0f);
      this._trackingSpace.localPosition = this._trackingSpaceStanding;
    }

    private IEnumerator DoMovementTransition(
      float blinkTime,
      Vector3 newPlayerPosition,
      Quaternion newPlayerRotation)
    {
      yield return (object) GamePlayerController.CameraFade.Fade(blinkTime / 2f);
      this.SetPositionAndRotation(newPlayerPosition, newPlayerRotation);
      VREvents.PlayerPositionUpdated.Trigger();
      yield return (object) GamePlayerController.CameraFade.Clear(blinkTime / 2f);
    }

    private IEnumerator DoRigFollow(Transform objectToFollow, bool lazyFollow = true, float followTime = 0.0f)
    {
      GamePlayerController playerController = this;
      Transform transform = UnityEngine.Object.Instantiate<GameObject>(new GameObject("TEMP_RIGFOLLOWOBJECT"), playerController.transform.position, playerController.transform.rotation).transform;
      float num = 5f;
      Vector3 vector3 = (playerController.transform.position - transform.position).normalized * num;
      playerController.transform.position = vector3;
      while (true)
      {
        playerController.SetPositionAndRotation(objectToFollow.position, objectToFollow.rotation);
        yield return (object) null;
      }
    }

    private void Update()
    {
      if (this._isSeated && this._isSeatedMode)
      {
        this._trackingSpaceOffset.y = this._seatPlayerHeight - 0.82f;
        this._trackingSpace.localPosition = this._trackingSpaceOffset;
      }
      else
        this._trackingSpace.localPosition = this._trackingSpaceStanding;
    }

    private void LateUpdate() => this._didMoveThisFrame = false;

    private void DebugControlCheckIfSwitchThrowMode()
    {
    }

    private IEnumerator DebugControlShowText()
    {
      TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.DebugThrowing, true);
      yield return (object) new WaitForSeconds(1f);
      TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.DebugThrowing, false);
    }

    public void OnStateChange(bool isMainMenu)
    {
      if (!this._isSeatedMode)
      {
        this.HandleBodyHeight(this._playerProfile.Customization.BodyHeight.Value);
      }
      else
      {
        if (!this._isSeated)
          return;
        this._trackingSpaceOffset.y = this._seatPlayerHeight - 0.82f;
        this._trackingSpace.localPosition = this._trackingSpaceOffset;
      }
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      if ((UnityEngine.Object) this._fade != (UnityEngine.Object) null)
        this._fade.OnFadeStateChanged -= new Action<bool>(this.HandleFadeStateChanged);
      if (!((UnityEngine.Object) this._playerProfile != (UnityEngine.Object) null) || this._playerProfile.Customization == null)
        return;
      this._playerProfile.Customization.BodyHeight.OnValueChanged -= new Action<float>(this.HandleBodyHeight);
    }

    private void HandleFadeStateChanged(bool fadeState) => this._isFaded = fadeState;

    public void ResetPosition()
    {
      this._tx.position = this._defaultPosition;
      this._tx.rotation = this._defaultRotation;
    }

    public void ResetRotation() => this._tx.rotation = this._defaultRotation;

    public void ResetToDefault()
    {
      Debug.Log((object) "Reset To Default");
      this.ResetPosition();
      this.ResetRotation();
    }

    public void CachePositionInLockerRoom()
    {
      this._defaultPosition = this._tx.position;
      this._defaultRotation = this._tx.rotation;
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion quat)
    {
      this._tx.SetPositionAndRotation(pos, quat);
      VREvents.PlayerPositionUpdated.Trigger();
    }

    public void SetPosition(Vector3 pos)
    {
      this._tx.position = pos;
      VREvents.PlayerPositionUpdated.Trigger();
    }

    public void Move(Vector3 delta)
    {
      if (this.AvoidWallPenetration && !Physics.Raycast(PersistentSingleton<GamePlayerController>.Instance.PlayerRefs.headAnchor.position, Vector3.down, out UnityEngine.RaycastHit _, float.MaxValue, (int) WorldConstants.Layers.Field) || !this._collisionInitialized || !this._collisionHandler.TryMove(delta) || this._didMoveThisFrame)
        return;
      this._didMoveThisFrame = true;
      this.position += delta * this._speedScale;
      this._footstepMoveCounter += Mathf.Abs(delta.x) + Mathf.Abs(delta.z);
      if ((double) this._footstepMoveCounter < 0.5)
        return;
      this._footstepMoveCounter = 0.0f;
      VREvents.FootstepTaken.Trigger();
    }

    public void InitializeCollision(PlayerCollisionHandler collisionHandler)
    {
      this._collisionHandler = collisionHandler;
      this._collisionInitialized = true;
    }

    public void SetMovementLimits(int minYardLine = -11, int maxYardLine = 111)
    {
    }

    public void SetNewBoundingRect(Rect bounds) => this._fieldBounds = bounds;

    private void OnDrawGizmos() => Debug.DrawLine(new Vector3(this._fieldBounds.x, 0.5f, this._fieldBounds.y), new Vector3(this._fieldBounds.x + this._fieldBounds.width, 0.5f, this._fieldBounds.y + this._fieldBounds.height), Color.green);

    private void HandleDominantHandChange(bool left)
    {
      ScriptableSingleton<HandsDataModel>.Instance.UpdateActiveHand();
      this.UpdateOneHandedSettings((bool) ScriptableSingleton<VRSettings>.Instance.OneHandedMode);
    }

    public void UpdateOneHandedSettings(bool active)
    {
      int num = (bool) ScriptableSingleton<VRSettings>.Instance.OneHandedMode ? ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? 1 : 0) : -1;
      for (int index = 0; index < this._questTracking.PoseDrivers.Length; ++index)
      {
        UnityEngine.InputSystem.XR.TrackedPoseDriver poseDriver = this._questTracking.PoseDrivers[index];
        if (index == num)
        {
          poseDriver.enabled = false;
          this.AdjustOneHandedOffHandTransform(GamePlayerController.EOneHandedModes.LockerRoom);
          poseDriver.GetComponent<DampedTargetMover>().enabled = true;
        }
        else
        {
          poseDriver.enabled = true;
          poseDriver.GetComponent<DampedTargetMover>().enabled = false;
        }
      }
    }

    public void AdjustOneHandedOffHandTransform(GamePlayerController.EOneHandedModes mode)
    {
      if (!(bool) ScriptableSingleton<VRSettings>.Instance.OneHandedMode)
        return;
      Transform transform = this._questTracking.PoseDrivers[(bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? 1 : 0].transform;
      switch (mode)
      {
        case GamePlayerController.EOneHandedModes.LockerRoom:
          if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
          {
            Vector3 vector3 = new Vector3(0.355f, 0.8f, 0.634f);
            Quaternion identity = Quaternion.identity;
            transform.localPosition = vector3;
            transform.localRotation = identity;
            break;
          }
          Vector3 vector3_1 = new Vector3(-0.355f, 0.8f, 0.634f);
          Quaternion identity1 = Quaternion.identity;
          transform.localPosition = vector3_1;
          transform.localRotation = identity1;
          break;
        case GamePlayerController.EOneHandedModes.OnField:
          if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
          {
            Vector3 vector3_2 = new Vector3(-0.323f, 0.747f, 0.599f);
            Quaternion quaternion = Quaternion.Euler(29.948f, 276.92f, -256.336f);
            transform.localPosition = vector3_2;
            transform.localRotation = quaternion;
            break;
          }
          Vector3 vector3_3 = new Vector3(0.323f, 0.747f, 0.599f);
          Quaternion quaternion1 = Quaternion.Euler(13.224f, 87.183f, -102.138f);
          transform.localPosition = vector3_3;
          transform.localRotation = quaternion1;
          break;
      }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
      if (!hasFocus)
      {
        this._previousHandState = VRState.HandsVisible.Value;
        if (VRState.PausePermission)
          VRState.PauseMenu.SetValue(true);
        VRState.HandsVisible.SetValue(false);
        if ((bool) NetworkState.InRoom)
          return;
        Time.timeScale = 0.0f;
      }
      else
      {
        VRState.HandsVisible.SetValue(this._previousHandState);
        if (!(bool) VRState.PauseMenu && !(bool) NetworkState.InRoom)
          Time.timeScale = (double) (float) GameSettings.TimeScale > 0.0 ? (float) GameSettings.TimeScale : 1f;
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
          return;
        PSVRManager.instance.SetupHMDDevice();
      }
    }

    public void UpdateFollowGrass(GameObject grass)
    {
      if (!((UnityEngine.Object) grass != (UnityEngine.Object) null))
        return;
      grass.transform.SetParent(this._tx);
    }

    public enum EOneHandedModes
    {
      LockerRoom,
      OnField,
    }

    [Serializable]
    public class PlayerTrackingRefs
    {
      public Transform leftHandAnchor;
      public Transform rightHandAnchor;
      public Transform headAnchor;
    }

    [Serializable]
    public class PSVRRefs
    {
      public UnityEngine.InputSystem.XR.TrackedPoseDriver[] VRTrackedPoseDriversToDisable;
      public UnityEngine.SpatialTracking.TrackedPoseDriver PSVRHeadTrackedPoseDriverToEnable;
      public TrackedPlayStationDevices PSTrackedDevicesToEnable;
    }

    [Serializable]
    public class QuestRefs
    {
      public UnityEngine.InputSystem.XR.TrackedPoseDriver[] PoseDrivers;
      public InputAction[] PositionTracking;
      public InputAction[] RotationTracking;
    }
  }
}
