// Decompiled with JetBrains decompiler
// Type: FootballVR.HandsDataModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DSE;
using FootballVR.Multiplayer;
using FootballVR.Sequences;
using Framework;
using Framework.Networked;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Managers/HandsDataModel", fileName = "HandsDataModel")]
  public class HandsDataModel : ScriptableSingleton<HandsDataModel>
  {
    [SerializeField]
    private TwoHandedPose[] _poses;
    [SerializeField]
    private LayerMask _interactablesLayer;
    [SerializeField]
    private ArmSwingLocomotion _locomotion;
    [SerializeField]
    public BallObject defaultBall;
    [SerializeField]
    public float ballContainerPickRadius = 0.3f;
    private const string miniCampJugMachineBallTag = "MiniCampJugMachineBall";
    private TwoHandedGrabSequence _twoHandedSequence;
    private readonly Dictionary<int, TwoHandedPose> _posesDictionary = new Dictionary<int, TwoHandedPose>();
    private readonly RoutineHandle _updateRoutine = new RoutineHandle();
    private bool _enabled = true;
    private bool _initialized;
    private readonly Collider[] nearbyObjects = new Collider[100];

    public ThrowSettings settings => ScriptableSingleton<ThrowSettings>.Instance;

    public float catchRadius { get; set; } = 0.4f;

    public float catchRadiusOneHand { get; set; } = 0.25f;

    public EPlayerRole playerRole { get; set; } = EPlayerRole.kQuarterBack;

    public float movingSpeed => this._locomotion.CurrentSpeed;

    public HandData ActiveHand
    {
      get
      {
        EHand ehand = (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? EHand.Left : EHand.Right;
        foreach (HandData handData in this.HandDatas)
        {
          if (handData.controller == ehand)
            return handData;
        }
        return (HandData) null;
      }
    }

    public List<HandData> HandDatas { get; } = new List<HandData>();

    public event System.Action OnBallPicked;

    public static event System.Action OnBallCaught;

    public event Action<BallObject> OnTwoHandedGrab;

    public HandData RegisterHand(HandController hand, EHand controller)
    {
      this.Initialize();
      HandData hand1 = this.GetHand(controller);
      if (hand1 != null)
      {
        hand1.SetHandController(hand);
        this.UpdateActiveHand();
        return hand1;
      }
      Debug.LogError((object) string.Format("HandData for {0} not found.", (object) controller));
      return (HandData) null;
    }

    public void UnregisterHand(HandController handController)
    {
      for (int index = 0; index < this.HandDatas.Count; ++index)
      {
        if ((UnityEngine.Object) this.HandDatas[index].hand == (UnityEngine.Object) handController)
          this.HandDatas[index].SetHandController((HandController) null);
      }
    }

    public void SetInputState(bool state, bool force = false)
    {
      if (!force && this._enabled == state)
        return;
      this._enabled = state;
      if (state)
        this._updateRoutine.Run(this.InputUpdateRoutine());
      else
        this._updateRoutine.Stop();
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.Deinitialize();
    }

    public void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
      this.HandDatas.Clear();
      this.InitializeHand(EHand.Right);
      this.InitializeHand(EHand.Left);
      this.SetInputState(true, true);
      foreach (KeyValuePair<int, TwoHandedPose> poses in this._posesDictionary)
      {
        if ((UnityEngine.Object) poses.Value != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) poses.Value.gameObject);
      }
      this._posesDictionary.Clear();
      this._twoHandedSequence = new TwoHandedGrabSequence(this);
    }

    private void Deinitialize()
    {
      foreach (HandData handData in this.HandDatas)
      {
        if (handData != null && (UnityEngine.Object) handData.hand != (UnityEngine.Object) null)
          handData.OnBallPicked -= this.OnBallPicked;
      }
      this.HandDatas.Clear();
      this._initialized = false;
      this.playerRole = EPlayerRole.kQuarterBack;
    }

    private HandData InitializeHand(EHand controller)
    {
      HandData handData = new HandData(this, controller);
      this.HandDatas.Add(handData);
      handData.OnBallPicked += this.OnBallPicked;
      return handData;
    }

    public void ResetHandsState()
    {
      this._twoHandedSequence?.Stop();
      foreach (HandData handData in this.HandDatas)
        handData.CurrentObject = (BallObject) null;
    }

    public void StopTwoHandedCatch(BallObject ball)
    {
      if (this._twoHandedSequence == null || !this._twoHandedSequence.HasBall(ball))
        return;
      this._twoHandedSequence.Stop();
    }

    private IEnumerator InputUpdateRoutine()
    {
      while (true)
      {
        foreach (HandData handData in this.HandDatas)
          handData.input.Update();
        if ((bool) this.settings.InteractionSettings.ShowDebug)
        {
          DebugUI instance = Singleton<DebugUI>.Instance;
          TextMeshProUGUI leftTextDown = instance.leftTextDown;
          float num = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
          string str1 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          leftTextDown.text = str1;
          TextMeshProUGUI rightTextDown = instance.rightTextDown;
          num = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
          string str2 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          rightTextDown.text = str2;
        }
        yield return (object) null;
      }
    }

    public HandData GetHand(EHand controller)
    {
      this.Initialize();
      foreach (HandData handData in this.HandDatas)
      {
        if (handData.controller == controller)
          return handData;
      }
      return this.InitializeHand(controller);
    }

    public TwoHandedPose GetTwoHandedPose(EHandPose handPose)
    {
      int key = (int) (handPose - 2);
      if (key < 0)
      {
        Debug.LogWarning((object) "TwoHandedPose: Index < 0");
        key = 0;
      }
      if (key >= this._poses.Length)
      {
        Debug.LogWarning((object) "TwoHandedPose: Index >= count");
        key = this._poses.Length - 1;
      }
      TwoHandedPose twoHandedPose;
      if (this._posesDictionary.TryGetValue(key, out twoHandedPose))
      {
        if ((UnityEngine.Object) twoHandedPose != (UnityEngine.Object) null && (UnityEngine.Object) twoHandedPose.gameObject != (UnityEngine.Object) null)
          return twoHandedPose;
        Debug.LogWarning((object) "Two-hand pose found in dictionary is null, or has been destroyed. Recreating..");
      }
      twoHandedPose = UnityEngine.Object.Instantiate<TwoHandedPose>(this._poses[key]);
      this._posesDictionary[key] = twoHandedPose;
      return twoHandedPose;
    }

    public bool TryGetMidPoint(out Vector3 midPoint)
    {
      midPoint = Vector3.zero;
      int count = this.HandDatas.Count;
      if (count == 0)
        return false;
      foreach (HandData handData in this.HandDatas)
        midPoint += handData.position;
      midPoint /= (float) count;
      return true;
    }

    public bool TryGrabBall(BallObject ballObject, float radiusMultiplier = 1f)
    {
      if ((UnityEngine.Object) ballObject == (UnityEngine.Object) null || ballObject.CompareTag("MiniCampJugMachineBall"))
        return false;
      Debug.Log((object) "Trying to grab ball");
      Vector3 catchPosition = ballObject.catchPosition;
      if ((bool) NetworkState.InRoom)
        radiusMultiplier = 1.5f;
      if ((bool) NetworkState.InRoom && (bool) (VariableBool) VRState.BigSizeMode)
        radiusMultiplier *= VRState.BigSizeScale;
      float radius = this.catchRadius * radiusMultiplier;
      if (this.CheckDoubleHandCatch(ballObject, catchPosition, radius))
      {
        System.Action onBallCaught = HandsDataModel.OnBallCaught;
        if (onBallCaught != null)
          onBallCaught();
        foreach (HandData handData in this.HandDatas)
          handData.CurrentObject = (BallObject) null;
        this._twoHandedSequence.Run(ballObject, radius);
        Action<BallObject> onTwoHandedGrab = this.OnTwoHandedGrab;
        if (onTwoHandedGrab != null)
          onTwoHandedGrab(ballObject);
        if (!(bool) NetworkState.InRoom)
          return true;
        BallObjectNetworked ballObjectNetworked = (BallObjectNetworked) ballObject;
        MultiplayerEvents.BallWasCaught.Trigger(PhotonNetwork.LocalPlayer.ActorNumber, ballObjectNetworked.PhotonView.OwnerActorNr, VRState.BigSizeMode.Value);
        Debug.Log((object) string.Format("BallWasCaught Fired! Caught by Player:{0} Thrown by Player {1} Giant? : {2}", (object) PhotonNetwork.LocalPlayer.ActorNumber, (object) ballObjectNetworked.PhotonView.OwnerActorNr, (object) VRState.BigSizeMode.Value));
        return true;
      }
      HandData handOnBall;
      if (!this.CheckCatch(catchPosition, this.catchRadiusOneHand * radiusMultiplier, out handOnBall))
        return false;
      handOnBall.CurrentObject = ballObject;
      if (!(bool) NetworkState.InRoom)
        return true;
      BallObjectNetworked ballObjectNetworked1 = (BallObjectNetworked) ballObject;
      MultiplayerEvents.BallWasCaught.Trigger(PhotonNetwork.LocalPlayer.ActorNumber, ballObjectNetworked1.PhotonView.OwnerActorNr, VRState.BigSizeMode.Value);
      return true;
    }

    public bool GetClosestValidObject(
      Vector3 pos,
      out BallObject closestBall,
      float radiusMultiplier = 1f)
    {
      closestBall = (BallObject) null;
      float num1 = float.MaxValue;
      bool flag = false;
      int num2 = Physics.OverlapSphereNonAlloc(pos, this.catchRadius * radiusMultiplier, this.nearbyObjects, (int) this._interactablesLayer);
      for (int index = 0; index < num2; ++index)
      {
        Collider nearbyObject = this.nearbyObjects[index];
        BallObject component;
        if (nearbyObject.TryGetComponentInParent<BallObject>(out component))
        {
          float sqrMagnitude = (nearbyObject.transform.position - pos).sqrMagnitude;
          if ((double) sqrMagnitude < (double) num1 || !flag && component.inFlight)
          {
            flag = component.inFlight;
            closestBall = component;
            num1 = sqrMagnitude;
          }
        }
      }
      return (UnityEngine.Object) closestBall != (UnityEngine.Object) null;
    }

    public bool GetBallFromContainer(Vector3 position, out BallObject ball)
    {
      ball = (BallObject) null;
      int num = Physics.OverlapSphereNonAlloc(position, this.ballContainerPickRadius, this.nearbyObjects, (int) this._interactablesLayer);
      for (int index = 0; index < num; ++index)
      {
        BallsContainer component;
        if (this.nearbyObjects[index].TryGetComponent<BallsContainer>(out component))
        {
          ball = component.SpawnBall(position);
          return true;
        }
      }
      return false;
    }

    private bool CheckDoubleHandCatch(BallObject ball, Vector3 catchPos, float radius)
    {
      if (!VRUtils.BothHandsConnected)
        return false;
      int num = 0;
      foreach (HandData handData in this.HandDatas)
      {
        if ((handData.input.TriggerActivated(this.settings.InteractionSettings.CatchTestDuration) || handData.hasObject) && (!handData.hasObject || !((UnityEngine.Object) handData.CurrentObject != (UnityEngine.Object) ball)) && (double) (handData.position - catchPos).magnitude <= (double) radius)
          ++num;
      }
      return num == 2;
    }

    private bool CheckCatch(Vector3 catchPos, float radius, out HandData handOnBall)
    {
      float num = float.MaxValue;
      handOnBall = (HandData) null;
      foreach (HandData handData in this.HandDatas)
      {
        if ((bool) handData.input.objectInteractPressed && !handData.hasObject)
        {
          float magnitude = (handData.position - catchPos).magnitude;
          if ((double) magnitude <= (double) radius && (double) magnitude <= (double) num)
          {
            num = magnitude;
            handOnBall = handData;
          }
        }
      }
      return handOnBall != null;
    }

    [ContextMenu("Reset catch settings")]
    private void ResetCatchSettings()
    {
      this.catchRadius = 0.4f;
      this.catchRadiusOneHand = 0.25f;
    }

    public void UpdateActiveHand()
    {
      foreach (HandData handData in this.HandDatas)
      {
        if ((bool) (UnityEngine.Object) handData.hand)
          handData.hand.SetIsDominantHand(handData == this.ActiveHand);
      }
    }
  }
}
