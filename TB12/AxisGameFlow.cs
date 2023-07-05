// Decompiled with JetBrains decompiler
// Type: TB12.AxisGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using FootballVR;
using Framework;
using Framework.Data;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.AppStates;
using TB12.Sequences;
using TB12.UI;
using UDB;
using UnityEngine;
using Vars;

namespace TB12
{
  public class AxisGameFlow : MonoBehaviour
  {
    public static AxisGameFlow instance;
    [SerializeField]
    protected AxisGameState _state;
    [SerializeField]
    protected AxisGameScene _scene;
    [SerializeField]
    protected HandsDataModel _handsDataModel;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private PlayersManager _playersManager;
    [SerializeField]
    private Transform[] _sideLinePosition;
    [SerializeField]
    private AxisGameFlow.QBFollowTypes _followType;
    private bool _isOnOffense;
    private PlayerAI _qbAI;
    private Transform _currentFollowTx;
    private bool _doRotationReset;
    private readonly RoutineHandle _movePlayerRoutine = new RoutineHandle();
    protected bool _showTutorialScreen;
    private bool _canFumble;
    protected bool _gameStarted;
    protected bool _stopQBFollow;
    private HandData _snapHand;
    private float _ballHeldTime;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private ControllerInput rightHandInput;
    private ControllerInput leftHandInput;
    private EPlayEndType _lastPlayEndType;
    private EPlayState _currentPlayState;
    private WristAudibleCallScreen wristUI;
    protected bool _didThrowBall;
    private Rect pocketBounds;
    protected bool _ballHasBeenInSweetSpot;
    protected bool _ballHasLeftSweetSpot;
    protected bool _idealHandoffTimeReached;
    protected bool _isInHuddle;
    private bool _transitionActive;
    [EditorSetting(ESettingType.Debug)]
    private static bool debug;
    [EditorSetting(ESettingType.Debug)]
    private static bool pause;
    [HideInInspector]
    public bool isWristPlayCallVisible;
    [Header("Tackle Settings")]
    [Range(0.1f, 1f)]
    [SerializeField]
    private float tackleTimeSlowAmount = 0.4f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float tackleFallSpeed = 0.5f;
    [Range(0.0f, 1.5f)]
    [SerializeField]
    private float tackleRumbleStrength = 1f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float tackleRumbleDuration = 0.2f;
    [Header("Dropback Settings")]
    [Range(0.0f, 1f)]
    [SerializeField]
    private float dropbackDuration = 0.6f;
    [Range(0.0f, 0.5f)]
    [SerializeField]
    private float dropbackTimeScale = 0.25f;
    [Header("Handoff Settings")]
    [Range(0.0f, 1.5f)]
    [SerializeField]
    private float HANDOFF_DIST = 2f;
    [Range(-0.25f, 0.25f)]
    [SerializeField]
    private float HANDOFF_ANGLE_DOT = -0.17f;
    [Header("Player Bounds")]
    [Tooltip("When the user is on the sideline, keep his Z position within this distance of midfield")]
    [Range(0.0f, 100f)]
    [SerializeField]
    private float MAX_Z_OFFSET_WHEN_ON_SIDELINE = 16f;
    protected readonly AxisHikeSequence _ballHikeSequence = new AxisHikeSequence();
    private readonly RoutineHandle _tacklehandle = new RoutineHandle();

    protected virtual void Awake()
    {
      AxisGameFlow.instance = this;
      PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);
      MatchManager.instance.OnQBPositionChange += new System.Action(this.MovePlayerCamera);
      MatchManager.instance.OnUserPenaltyCalled += new System.Action(this.HandleUserPenaltyCalled);
      MatchManager.instance.OnAllowUserHurryUp += new Action<bool>(this.SetHurryUpVisibility);
      MatchManager.instance.OnAllowUserTimeout += new Action<bool>(this.SetTimeoutVisibility);
      MatchManager.instance.playManager.OnUserPlayCallStarted += new System.Action(this.HandlePlayCallStarted);
      MatchManager.instance.playManager.OnUserPlayCallMade += new System.Action(this.HandlePlayCallMade);
      MatchManager.instance.playManager.OnUserPlayCallEnded += new System.Action(this.HandlePlayCallEnded);
      MatchManager.instance.OnSnapAllowed += new System.Action(this.HandleReadyForPlay);
      MatchManager.instance.OnPlayReset += new System.Action(this.HandlePlayReset);
      SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance.OnUserTimeOutCalled += new System.Action(this.HandleUserTimeoutCalled);
      PlayerAI.OnUserQBInHuddle += new System.Action(this.HandleUserQBInHuddle);
      this.rightHandInput = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right).input;
      this.leftHandInput = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left).input;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this.rightHandInput.objectInteractPressed.Link<bool>(new Action<bool>(this.HandleTriggerPress)),
        this.leftHandInput.objectInteractPressed.Link<bool>(new Action<bool>(this.HandleTriggerPress)),
        Ball.State.BallState.Link<EBallState>(new Action<EBallState>(this.HandleBallState)),
        this.rightHandInput.buttonOneHeld.Link<bool>(new Action<bool>(this.HandleButtonOneHeld)),
        this.leftHandInput.buttonOneHeld.Link<bool>(new Action<bool>(this.HandleButtonOneHeld)),
        ProEra.Game.MatchState.CurrentMatchState.Link<EMatchState>(new Action<EMatchState>(this.HandleMatchStateChanged)),
        AppEvents.UserRequestedMainMenu.Link(new System.Action(this.HandleUserRequestedMainMenu))
      });
      foreach (HandData handData in this._handsDataModel.HandDatas)
      {
        handData.OnBallPicked += new System.Action(this.HandleBallPicked);
        handData.OnBallReleased += new Action<BallObject>(this.HandleBallReleased);
      }
      HandController hand = ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right) : ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left)).hand;
      if ((UnityEngine.Object) hand != (UnityEngine.Object) null && (UnityEngine.Object) hand.TopWristGO != (UnityEngine.Object) null)
        this.wristUI = hand.TopWristGO.transform.GetChild(0).gameObject.GetComponent<WristAudibleCallScreen>();
      this._throwManager.OnThrowProcessed += new Action<ThrowData>(this.HandleThrowProcessed);
      this._ballHikeSequence.OnHikeComplete += new System.Action(this.HandleHikeComplete);
      BallManager instance = SingletonBehaviour<BallManager, MonoBehaviour>.instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      BallObject component = instance.GetComponent<BallObject>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      component.OnBallDrop += new System.Action(this.OnBallDrop);
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0.0f, 180f, 0.0f));
      this._isOnOffense = global::Game.IsPlayerOneOnOffense;
      this._throwManager.SetCanThrowBall(false);
      this.pocketBounds = new Rect();
    }

    protected virtual void OnDestroy()
    {
      this._linksHandler.Clear();
      this._movePlayerRoutine.Stop();
      this._throwManager.OnThrowProcessed -= new Action<ThrowData>(this.HandleThrowProcessed);
      PEGameplayEventManager.OnEventOccurred -= new Action<PEGameplayEvent>(this.HandleGameEvent);
      MainGameTablet.SelfDestroy();
      VRState.PlayerOneDropbackActive = false;
      GamePlayerController.BulletTime.ExitBulletTime();
      if (!UnityState.quitting)
      {
        foreach (HandData handData in this._handsDataModel.HandDatas)
        {
          handData.EnableCatching(true);
          handData.OnBallPicked -= new System.Action(this.HandleBallPicked);
          handData.OnBallReleased -= new Action<BallObject>(this.HandleBallReleased);
        }
        if (MatchManager.Exists())
        {
          MatchManager.instance.OnQBPositionChange -= new System.Action(this.MovePlayerCamera);
          MatchManager.instance.playManager.OnUserPlayCallStarted -= new System.Action(this.HandlePlayCallStarted);
          MatchManager.instance.playManager.OnUserPlayCallMade -= new System.Action(this.HandlePlayCallMade);
          MatchManager.instance.playManager.OnUserPlayCallEnded -= new System.Action(this.HandlePlayCallEnded);
          MatchManager.instance.OnSnapAllowed -= new System.Action(this.HandleReadyForPlay);
          MatchManager.instance.OnUserPenaltyCalled -= new System.Action(this.HandleUserPenaltyCalled);
          MatchManager.instance.OnAllowUserHurryUp -= new Action<bool>(this.SetHurryUpVisibility);
          MatchManager.instance.OnAllowUserTimeout -= new Action<bool>(this.SetTimeoutVisibility);
        }
        if (SingletonBehaviour<TimeoutManager, MonoBehaviour>.Exists())
          SingletonBehaviour<TimeoutManager, MonoBehaviour>.instance.OnUserTimeOutCalled += new System.Action(this.HandleUserTimeoutCalled);
        PlayerAI.OnUserQBInHuddle -= new System.Action(this.HandleUserQBInHuddle);
      }
      AxisGameFlow.instance = (AxisGameFlow) null;
    }

    protected virtual void HandleGameEvent(PEGameplayEvent e)
    {
      switch (e)
      {
        case PEPlayOverEvent pePlayOverEvent:
          this.OnPlayComplete(pePlayOverEvent.Type.ToString());
          break;
        case PEBallHikedEvent _:
          this.OnBallSnapped();
          break;
        case PEBallKickedEvent _:
          ScoreboardAnimations.SetActiveBroadcastView(true);
          break;
        case PEQuarterEndEvent peQuarterEndEvent:
          this.OnQuarterEnd(peQuarterEndEvent.Quarter);
          break;
        case PEMovePlayersToHuddleEvent _:
          this._isInHuddle = true;
          break;
        case PEBreakHuddleEvent _:
          this._isInHuddle = false;
          break;
        case PEAudibleCalledEvent _:
          this.OnAudibleCalled();
          break;
        case PEHandoffAbortedEvent _:
        case PEBallHandoffEvent _:
          this.OnHandoffEnded();
          break;
        case PEHandoffTimeReachedEvent _:
          this._idealHandoffTimeReached = true;
          break;
      }
    }

    private void HandleBallReleased(BallObject ball)
    {
      if ((UnityEngine.Object) MatchManager.instance.playersManager.ballHolderScript == (UnityEngine.Object) null)
        return;
      if (global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB && MatchManager.instance.playersManager.ballHolderScript.IsQB() && global::Game.PlayIsNotOver)
      {
        PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
        if (Ball.State.BallState.Value == EBallState.InAirDrop)
          return;
        if (savedOffPlay.GetPlayType() == global::PlayType.Pass)
        {
          Ball.State.BallState.SetValue(EBallState.InAirPass);
        }
        else
        {
          if (savedOffPlay.GetPlayType() != global::PlayType.Run)
            return;
          if (global::Game.IsPitchPlay || global::Game.IsQBKeeper)
            Ball.State.BallState.SetValue(EBallState.InAirPass);
          else
            Ball.State.BallState.SetValue(EBallState.InAirDrop);
        }
      }
      else
        Ball.State.BallState.SetValue(EBallState.InAirDrop);
    }

    private void HandleBallState(EBallState state)
    {
      Debug.Log((object) ("HandleBallState: " + state.ToString()));
      if (!global::Game.IsPlayerOneOnOffense || !global::Game.UserControlsQB)
        return;
      if ((UnityEngine.Object) this._qbAI == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "_qbAI is null!! -- it should already be set");
        this._qbAI = global::Game.OffensiveQB;
      }
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      if (savedOffPlay.GetPlayType() != global::PlayType.Pass && savedOffPlay.GetPlayType() != global::PlayType.Run)
        return;
      BallManager instance = SingletonBehaviour<BallManager, MonoBehaviour>.instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      BallObject component = instance.GetComponent<BallObject>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      switch (state)
      {
        case EBallState.PlayersHands:
          if (component.inHand)
          {
            this._throwManager.SetCanThrowBall(true);
            break;
          }
          component.CompleteBallFlight();
          break;
        case EBallState.InAirSnap:
          this._ballHikeSequence.RunHikeSequence(component, this._snapHand);
          this._ballHeldTime = Time.time;
          break;
        default:
          if (state == EBallState.OnGround && this._canFumble)
          {
            if (this._didThrowBall && (double) Field.FindDifference(component.transform.position.z, global::Game.OffensiveQB.trans.position.z) >= 0.0)
              break;
            this.ActivateFumble();
            break;
          }
          switch (state)
          {
            case EBallState.InCentersHandsBeforeSnap:
              if (this._showTutorialScreen)
              {
                UIDispatch.FrontScreen.DisplayView(EScreens.kBasicFTUE);
                this._showTutorialScreen = false;
              }
              Vector3 position = this._qbAI.trans.position;
              if (!savedOffPlay.IsUnderCenterPlay())
              {
                this.pocketBounds = new Rect(position.x - Field.ONE_YARD * (float) global::Game.OffensiveFieldDirection, position.z - Field.ONE_YARD * (float) global::Game.OffensiveFieldDirection, Field.TWO_YARDS * (float) global::Game.OffensiveFieldDirection, Field.TWO_YARDS * (float) global::Game.OffensiveFieldDirection);
              }
              else
              {
                this.pocketBounds = new Rect(position.x - Field.ONE_HALF_YARD * (float) global::Game.OffensiveFieldDirection, position.z - Field.ONE_FOOT * (float) global::Game.OffensiveFieldDirection, Field.ONE_YARD * (float) global::Game.OffensiveFieldDirection, Field.TWO_FEET * (float) global::Game.OffensiveFieldDirection);
                Vector3 playStartPosition = global::Game.OffensiveCenter.GetPlayStartPosition();
                this._scene.SetSweetSpotPosition(playStartPosition + new Vector3(0.0f, 0.75f, (float) (((double) position.z - (double) playStartPosition.z) * 0.75)));
              }
              VRState.LocomotionEnabled.SetValue(true);
              return;
            case EBallState.PlayOver:
              this._throwManager.SetCanThrowBall(false);
              HandData hand1 = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left);
              HandData hand2 = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right);
              hand1.CurrentObject = (BallObject) null;
              hand2.CurrentObject = (BallObject) null;
              return;
            case EBallState.InAirDrop:
              bool flag = false;
              if (global::Game.PlayIsNotOver && global::Game.PlayHasHandoff && this._scene.BallInSweetSpot())
              {
                this._playersManager.HandOffBall(false);
                flag = true;
              }
              if (flag)
                return;
              this._playersManager.BallHolderReleaseBall();
              this._canFumble = true;
              return;
            default:
              return;
          }
      }
    }

    private void OnBallDrop()
    {
      if (!global::Game.IsPlayerOneOnOffense)
        return;
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      if (savedOffPlay.GetPlayType() != global::PlayType.Pass && savedOffPlay.GetPlayType() != global::PlayType.Run)
        return;
      Ball.State.BallState.SetValue(EBallState.InAirDrop);
      Debug.Log((object) nameof (OnBallDrop));
      this._canFumble = true;
      GameplayUI.HidePointer();
    }

    private void ActivateFumble()
    {
      if (!this._canFumble)
        return;
      this._canFumble = false;
      this._playersManager.ActivateFumble();
      if ((UnityEngine.Object) this._playersManager.ballHolderScript != (UnityEngine.Object) null && (UnityEngine.Object) this._playersManager.ballHolderScript.animatorCommunicator != (UnityEngine.Object) null)
        this._playersManager.ballHolderScript.animatorCommunicator.hasBall = false;
      if ((UnityEngine.Object) this._playersManager.ballHolderScript != (UnityEngine.Object) null)
        ScoreboardAnimations.PlayAnimation(ScoreboardAnimations.BoardAnimType.Fumble, this._playersManager.ballHolderScript.teamIndex);
      this._playersManager.BallHolderReleaseBall();
      ScoreboardAnimations.PlayAnimation(ScoreboardAnimations.BoardAnimType.Fumble, global::Game.IsPlayerOneOnOffense ? this._playersManager.userTeamData.TeamIndex : this._playersManager.compTeamData.TeamIndex);
    }

    private void HandleBallPicked()
    {
      Debug.Log((object) ("HandleBallPicked: " + Ball.State.BallState?.ToString()));
      if ((EBallState) (Variable<EBallState>) Ball.State.BallState != EBallState.InAirSnap)
      {
        MatchManager.instance.playersManager.SetBallHolder(this._qbAI.mainGO, this._qbAI.onUserTeam);
      }
      else
      {
        this._ballHeldTime = Time.time - this._ballHeldTime;
        PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
        PlayDataDef savedDefPlay = MatchManager.instance.playManager.savedDefPlay;
        SingletonBehaviour<BallManager, MonoBehaviour>.instance.GetComponent<BallObject>().SetGameReplayData(new GameReplayData()
        {
          snapTime = this._ballHeldTime,
          offFormation = savedOffPlay.GetFormation().GetBaseFormationString(),
          offPlayType = savedOffPlay.GetPlayName(),
          defFormation = savedDefPlay.GetFormation().GetBaseFormationString(),
          defPlayType = savedDefPlay.GetPlayName(),
          startFieldPosition = ProEra.Game.MatchState.BallOn.Value,
          startHash = MatchManager.instance.ballHashPosition,
          offenseNorth = global::Game.OffenseGoingNorth
        });
      }
      Ball.State.BallState.SetValue(EBallState.PlayersHands);
    }

    private void HandleTriggerPress(bool pressed)
    {
      if (!global::Game.IsPlayerOneOnOffense || !global::Game.UserControlsQB)
        return;
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      if (savedOffPlay.GetPlayType() != global::PlayType.Pass && savedOffPlay.GetPlayType() != global::PlayType.Run)
        return;
      bool flag = this.pocketBounds.Contains((Vector3) new Vector2(PersistentSingleton<GamePlayerController>.Instance.position.x, PersistentSingleton<GamePlayerController>.Instance.position.z), true);
      if (!pressed || !MatchManager.instance.IsSnapAllowed() || !(bool) this.rightHandInput.objectInteractPressed && !(bool) this.leftHandInput.objectInteractPressed)
        return;
      this._snapHand = (bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand ? ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left) : ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right);
      HandData handData = this._snapHand.controller == EHand.Left ? ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right) : ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left);
      if (savedOffPlay.IsUnderCenterPlay())
      {
        if (!this._scene.IsInSweetSpot(this._snapHand))
          return;
        this.SnapBall(savedOffPlay);
      }
      else
      {
        if (!flag || handData.hand.Wristband.IsOverlappingHand)
          return;
        this.SnapBall(savedOffPlay);
      }
    }

    private void SnapBall(PlayDataOff pDataOff)
    {
      if (global::Game.PlayHasHandoff)
        this._scene.SetSweetSpotPosition(this._playersManager.curUserScriptRef[pDataOff.GetHandoffTarget()].transform, true, new Vector3(0.0f, 1f, (float) (0.37099999189376831 * (global::Game.OffenseGoingNorth ? 1.0 : -1.0))));
      else
        this._scene.DeactivateSweetSpot();
      MatchManager.instance.SnapBall(this._snapHand.input.thumbstickVector);
    }

    private void HandleButtonOneHeld(bool held)
    {
      if (!held)
        return;
      MatchManager.instance.playManager.RunHurryUp();
    }

    public void MovePlayerCamera()
    {
      Debug.Log((object) nameof (MovePlayerCamera));
      if (!this._gameStarted || this._stopQBFollow)
        return;
      this._isOnOffense = global::Game.IsPlayerOneOnOffense;
      this._doRotationReset = true;
      if (this._lastPlayEndType == EPlayEndType.kIncomplete && global::Game.IsPlayerOneOnOffense && (int) ProEra.Game.MatchState.Down != 4)
        this._doRotationReset = false;
      if (global::Game.UserControlsQB && this._isOnOffense && global::Game.IsNotKickoff && global::Game.IsNotFG && !global::Game.IsPunt)
      {
        this._qbAI = global::Game.OffensiveQB;
        this._qbAI.IsTackledEvent += new PlayerAI.OnIsTackledEvent(this.HandleQBTackle);
        this._currentFollowTx = this._qbAI.trans;
        this._qbAI.HidePlayerAvatar();
      }
      else
      {
        if ((bool) (UnityEngine.Object) this._qbAI)
          this._qbAI.ShowPlayerAvatar();
        this._currentFollowTx = this.GetSidelinePosition();
      }
      if (this._followType != AxisGameFlow.QBFollowTypes.OnCall)
        return;
      this._movePlayerRoutine.Run(this.DoMovementTransition());
    }

    private IEnumerator DoMovementTransition()
    {
      VRState.LocomotionEnabled.SetValue(false);
      if ((UnityEngine.Object) this._qbAI != (UnityEngine.Object) null && (UnityEngine.Object) this._currentFollowTx == (UnityEngine.Object) this._qbAI.trans)
      {
        PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(this._currentFollowTx.position with
        {
          y = 0.0f
        }, global::Game.OffenseGoingNorth ? Quaternion.Euler(0.0f, 0.0f, 0.0f) : Quaternion.Euler(0.0f, 180f, 0.0f));
        if (PEGameplayEventManager.PlayerOnSideline.Value)
          SingletonBehaviour<FieldManager, MonoBehaviour>.instance.ToggleSidelineOfficialsForSide(PersistentData.userIsHome, true);
        PEGameplayEventManager.PlayerOnSideline.SetValue(false);
        PersistentSingleton<GamePlayerController>.Instance.AdjustOneHandedOffHandTransform(GamePlayerController.EOneHandedModes.OnField);
        VRState.HelmetEnabled.SetValue(ScriptableSingleton<VRSettings>.Instance.HelmetActive.Value);
        MainGameTablet.Hide();
        this._handsDataModel.GetHand(EHand.Left).EnableCatching(true);
        this._handsDataModel.GetHand(EHand.Right).EnableCatching(true);
      }
      else
      {
        PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
        if (!(bool) PEGameplayEventManager.PlayerOnSideline)
        {
          Vector3 position = this._currentFollowTx.position;
          position.z = Mathf.Clamp(position.z, -this.MAX_Z_OFFSET_WHEN_ON_SIDELINE, this.MAX_Z_OFFSET_WHEN_ON_SIDELINE);
          PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(position, this._currentFollowTx.rotation);
          this.SetTimeoutVisibility(false);
        }
        PEGameplayEventManager.PlayerOnSideline.SetValue(true);
        VRState.LocomotionEnabled.SetValue(true);
        VRState.HelmetEnabled.SetValue(false);
        MainGameTablet.Show();
        this._handsDataModel.GetHand(EHand.Left).EnableCatching(false);
        this._handsDataModel.GetHand(EHand.Right).EnableCatching(false);
        PersistentSingleton<GamePlayerController>.Instance.AdjustOneHandedOffHandTransform(GamePlayerController.EOneHandedModes.LockerRoom);
        SingletonBehaviour<FieldManager, MonoBehaviour>.instance.ToggleSidelineOfficialsForSide(PersistentData.userIsHome, false);
      }
      yield return (object) null;
    }

    private void LateUpdate()
    {
      PersistentSingleton<GamePlayerController>.Instance.SpeedScale = 1f;
      if (global::Game.IsPlayActive)
      {
        PlayerAI ballHolderScript = MatchManager.instance.playersManager.ballHolderScript;
        if (EligibilityManager.IsActive && (UnityEngine.Object) ballHolderScript != (UnityEngine.Object) null && ballHolderScript.indexInFormation == 5 && (double) Field.ConvertDistanceToYards(Field.FindDifference(ballHolderScript.trans.position.z, ProEra.Game.MatchState.BallOn.Value)) > 0.0)
          EligibilityManager.Instance.TurnOffReceiverUI();
        if ((!global::Game.PlayHasHandoff || !global::Game.CurrentPlayHasUserQBOnField ? 0 : (this._scene.SweetSpotIsActive() ? 1 : 0)) != 0)
        {
          bool flag = this._scene.BallInSweetSpot();
          this._ballHasBeenInSweetSpot |= flag;
          if ((!this._ballHasBeenInSweetSpot || this._ballHasLeftSweetSpot ? 0 : (!flag ? 1 : 0)) != 0)
          {
            if (global::Game.CanFakeHandoff)
              this._playersManager.TriggerFakeHandoff();
            this._ballHasLeftSweetSpot = true;
          }
          if ((!global::Game.IsRun || !this._idealHandoffTimeReached || this._ballHasBeenInSweetSpot && global::Game.CanFakeHandoff || !((UnityEngine.Object) ballHolderScript != (UnityEngine.Object) null) ? 0 : ((UnityEngine.Object) ballHolderScript != (UnityEngine.Object) global::Game.HandoffTarget ? 1 : 0)) != 0 && (double) Vector3.Distance(ballHolderScript.transform.position, global::Game.HandoffTarget.transform.position) > (double) global::Game.HandoffConfig.MaxDistanceFromTargetToAbortHandoff | Field.FurtherDownfield(ballHolderScript.transform.position.z, (float) ProEra.Game.MatchState.BallOn))
            this._playersManager.TriggerAbortedHandoff();
        }
        if (!global::Game.IsDesignedQBRun && global::Game.BallHolderIsUser)
        {
          UserMovementConfig userMovementConfig = global::Game.UserMovementConfig;
          Vector3 position = PersistentSingleton<GamePlayerController>.Instance.position;
          float num1 = 1f;
          if (!global::Game.OffensiveQB.IsTackled)
          {
            foreach (PlayerAI playerAi in this._playersManager.Defense)
            {
              Vector3 lhs = position - playerAi.trans.position;
              if ((double) Vector3.Dot(lhs, playerAi.trans.forward) > 0.0 && (double) lhs.sqrMagnitude < (double) userMovementConfig.MaxDistanceToOpponentForAutoTackle * (double) userMovementConfig.MaxDistanceToOpponentForAutoTackle)
              {
                playerAi.ForceTacklePlayer(global::Game.OffensiveQB);
                break;
              }
            }
          }
          if (!global::Game.OffensiveQB.IsTackled && PlayerAI.IsQBBetweenTackles() && Field.FurtherDownfield((float) ProEra.Game.MatchState.BallOn, position.z) && (double) PersistentSingleton<GamePlayerController>.Instance.velocity.z * (double) global::Game.OffensiveFieldDirection > 0.0)
          {
            foreach (PlayerAI playerAi in this._playersManager.Offense)
            {
              if (!((UnityEngine.Object) playerAi == (UnityEngine.Object) global::Game.OffensiveQB) && (double) (playerAi.trans.position - position).sqrMagnitude < (double) userMovementConfig.MaxDistanceToTeammateForSlowdown * (double) userMovementConfig.MaxDistanceToTeammateForSlowdown)
              {
                num1 *= userMovementConfig.TeammateSlowdownSpeedScale;
                break;
              }
            }
          }
          float num2 = MathUtils.MapRange(Field.GetDistanceDownfield(position.z, (float) ProEra.Game.MatchState.BallOn), 0.0f, userMovementConfig.DistancePastLOSForMaxSlowdown, 1f, userMovementConfig.SpeedScaleAtMaxDistanceFromLOS);
          PersistentSingleton<GamePlayerController>.Instance.SpeedScale = num1 * num2;
        }
      }
      else if (!this._transitionActive)
      {
        if (this._isInHuddle && MatchManager.instance.playManager.canUserCallAudible && global::Game.CurrentPlayHasUserQBOnField)
        {
          MatchManager.instance.playManager.canUserCallAudible = false;
          this.SetWristPlayCallVisibility(true);
        }
        else if (global::Game.CurrentPlayHasUserQBOnField && MatchManager.instance.IsQBReadyForSnap() && !MatchManager.instance.playManager.canUserCallAudible && AppState.GameMode != EGameMode.kOnboarding && AppState.GameMode != EGameMode.kHeroMoment && AppState.GameMode != EGameMode.kPracticeMode)
        {
          MatchManager.instance.playManager.canUserCallAudible = true;
          this.SetWristPlayCallVisibility(true);
        }
      }
      if ((EMatchState) (Variable<EMatchState>) ProEra.Game.MatchState.CurrentMatchState != EMatchState.End && (bool) PEGameplayEventManager.PlayerOnSideline && this._gameStarted && (UnityEngine.Object) this._currentFollowTx != (UnityEngine.Object) null && !SingletonBehaviour<PersistentData, MonoBehaviour>.instance.ignoreSidelineBoundary)
      {
        Vector3 position = PersistentSingleton<GamePlayerController>.Instance.position;
        double num = (double) Mathf.Sign(position.z);
        position.z = Mathf.Clamp(position.z, -this.MAX_Z_OFFSET_WHEN_ON_SIDELINE, this.MAX_Z_OFFSET_WHEN_ON_SIDELINE);
        position.x = (double) position.x <= 0.0 ? Mathf.Clamp(position.x, this._currentFollowTx.position.x - 4f, this._currentFollowTx.position.x - 2f) : Mathf.Clamp(position.x, this._currentFollowTx.position.x + 2f, this._currentFollowTx.position.x + 4f);
        PersistentSingleton<GamePlayerController>.Instance.SetPosition(position);
      }
      if ((UnityEngine.Object) this._currentFollowTx != (UnityEngine.Object) null && this._followType != AxisGameFlow.QBFollowTypes.OnCall)
      {
        PersistentSingleton<GamePlayerController>.Instance.SetPosition(this._currentFollowTx.position);
        this._doRotationReset = false;
        VREvents.PlayerPositionUpdated.Trigger();
      }
      if (!((UnityEngine.Object) this._qbAI != (UnityEngine.Object) null))
        return;
      this._qbAI.LockQBToPlayer(PersistentSingleton<GamePlayerController>.Instance.position, Quaternion.LookRotation(PersistentSingleton<GamePlayerController>.Instance.forward.SetY(0.0f)));
    }

    protected void UpdatePointerState()
    {
      bool flag = false;
      if (this._isOnOffense)
      {
        PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
        if (savedOffPlay != null && savedOffPlay.GetPlayType() == global::PlayType.Run)
        {
          PlayerAI playerAi = this._playersManager.curUserScriptRef[savedOffPlay.GetHandoffTarget()];
          if ((UnityEngine.Object) playerAi != (UnityEngine.Object) this._playersManager.GetCurrentSnapTarget())
          {
            GameplayUI.PointTo(playerAi.headJoint, "");
            flag = true;
          }
        }
      }
      if (flag)
        return;
      GameplayUI.HidePointer();
    }

    protected void OnBallSnapped()
    {
      List<PlayerAI> curUserScriptRef = MatchManager.instance.playersManager.curUserScriptRef;
      ScoreboardAnimations.SetActiveBroadcastView(true);
      if (global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB)
      {
        PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
        if (savedOffPlay.GetPlayType() == global::PlayType.Pass || savedOffPlay.GetPlayType() == global::PlayType.Run)
        {
          this._state.UpdateBounds();
          UIDispatch.FrontScreen.HideView(EScreens.kBasicFTUE);
          this._throwManager.Clear();
          if (savedOffPlay.GetPlayType() == global::PlayType.Pass)
          {
            for (int index = 0; index < 11; ++index)
            {
              if (curUserScriptRef[index].blockType == BlockType.None && curUserScriptRef[index].indexInFormation > 5)
                this._throwManager.RegisterTarget((IThrowTarget) new AxisReceiver(curUserScriptRef[index]));
            }
          }
          else if (savedOffPlay.GetPlayType() == global::PlayType.Run)
          {
            this._canFumble = true;
            int handoffTarget = savedOffPlay.GetHandoffTarget();
            PlayerAI playerAI = curUserScriptRef[handoffTarget];
            this._throwManager.RegisterTarget((IThrowTarget) new AxisReceiver(playerAI));
            EligibilityManager.Instance.SetCurrentTarget(playerAI.GetComponent<ReceiverUI>());
          }
        }
        this.SetWristPlayCallVisibility(false);
        MatchManager.instance.playManager.canUserCallAudible = false;
      }
      this.SetTimeoutVisibility(false);
    }

    private void HandleHikeComplete() => this.SetBallToPlayer();

    private void HandleReadyForPlay()
    {
      Debug.Log((object) "snapAllowed set--ball ready for play");
      PEGameplayEventManager.RecordReadyToHikeEvent();
    }

    private void SetBallToPlayer()
    {
      if (!global::Game.IsPlayerOneOnOffense || !global::Game.UserControlsQB)
        return;
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      if (savedOffPlay.GetPlayType() != global::PlayType.Pass && savedOffPlay.GetPlayType() != global::PlayType.Run)
        return;
      BallManager instance = SingletonBehaviour<BallManager, MonoBehaviour>.instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      BallObject component = instance.GetComponent<BallObject>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      HandData hand = this._handsDataModel.GetHand(EHand.Right);
      if ((bool) this._handsDataModel.GetHand(EHand.Left).input.objectInteractPressed && !(bool) hand.input.objectInteractPressed)
        hand = this._handsDataModel.GetHand(EHand.Left);
      hand.CurrentObject = component;
      MatchManager.instance.playersManager.SetBallHolder(this._qbAI.mainGO, this._qbAI.onUserTeam);
      if (ProEra.Game.PlayState.IsRun)
        hand.OnBallReleased += new Action<BallObject>(this.Hand_OnBallReleased);
      FootballVR.DifficultySetting difficulty = GameSettings.GetDifficulty();
      bool flag1 = savedOffPlay.IsUnderCenterPlay() && global::Game.IsPass && difficulty.UnderCenterAutoDropBackPass;
      bool flag2 = savedOffPlay.IsUnderCenterPlay() && global::Game.IsRun && difficulty.UnderCenterAutoDropBackRun;
      if (ScriptableSingleton<VRSettings>.Instance.AutoDropbackEnabled.Value && flag1 | flag2)
        this.StartCoroutine(this.BeginDropback());
      else if (!global::Game.IsRun)
        GamePlayerController.BulletTime.ExitBulletTime();
      if ((bool) hand.input.objectInteractPressed)
        return;
      hand.CurrentObject = (BallObject) null;
    }

    private IEnumerator BeginDropback()
    {
      VRState.PlayerOneDropbackActive = true;
      yield return (object) new WaitForSeconds(this.dropbackDuration);
      VRState.PlayerOneDropbackActive = false;
      if (!global::Game.IsRun)
        GamePlayerController.BulletTime.ExitBulletTime();
    }

    private void Hand_OnBallReleased(BallObject obj)
    {
      GamePlayerController.BulletTime.ExitBulletTime();
      LeanTween.cancel(PersistentSingleton<GamePlayerController>.Instance.gameObject);
      this._doRotationReset = true;
    }

    private void OnAudibleCalled()
    {
      this.UpdatePointerState();
      EligibilityManager.Instance.TurnOffReceiverUI();
      EligibilityManager.Instance.UpdateEligibleReceivers();
    }

    private void OnHandoffEnded()
    {
      GameplayUI.HidePointer();
      this._scene.DeactivateSweetSpot();
      GamePlayerController.BulletTime.ExitBulletTime();
    }

    private void OnPlayComplete(string type)
    {
      this.SetTimeoutVisibility(true);
      this._currentPlayState = EPlayState.kPostPlay;
      EPlayEndType eplayEndType = (EPlayEndType) Enum.Parse(typeof (EPlayEndType), "k" + type);
      this._lastPlayEndType = eplayEndType;
      Debug.Log((object) ("OnPlayComplete: " + eplayEndType.ToString()));
      int teamIndex = global::Game.IsPlayerOneOnOffense && !MatchManager.turnover || !global::Game.IsPlayerOneOnOffense && MatchManager.turnover ? this._playersManager.userTeamData.TeamIndex : this._playersManager.compTeamData.TeamIndex;
      switch (eplayEndType)
      {
        case EPlayEndType.kTouchdown:
          if (!(bool) ProEra.Game.MatchState.RunningPat)
            ScoreboardAnimations.PlayAnimation(ScoreboardAnimations.BoardAnimType.TouchDown, teamIndex);
          if (global::Game.IsKickoff)
          {
            ENteractHealthStatusCode healthStatusCode = NteractManager.DoHealthCheck();
            string message = "NteractManager health status code: " + healthStatusCode.ToString();
            if (healthStatusCode == ENteractHealthStatusCode.Healthy)
              Debug.Log((object) message);
            else
              Debug.LogError((object) message);
            AnalyticEvents.Record<NteractHealthCheckArgs>(new NteractHealthCheckArgs((int) healthStatusCode));
            break;
          }
          break;
        case EPlayEndType.kMadeFG:
          if (!(bool) ProEra.Game.MatchState.RunningPat)
          {
            ScoreboardAnimations.PlayAnimation(ScoreboardAnimations.BoardAnimType.FieldGoal, teamIndex);
            break;
          }
          break;
      }
      if ((UnityEngine.Object) this._qbAI != (UnityEngine.Object) null)
        this._qbAI.animatorCommunicator.applyRootMotion = true;
      this._canFumble = false;
      this._didThrowBall = false;
      GameplayUI.HidePointer();
      this._idealHandoffTimeReached = false;
      this._ballHasBeenInSweetSpot = false;
      this._ballHasLeftSweetSpot = false;
      ScoreboardAnimations.SetActiveBroadcastView(false);
    }

    private void OnQuarterEnd(int quarter)
    {
      Transform sidelinePosition = this.GetSidelinePosition();
      VREvents.BlinkMovePlayer.Trigger(1f, sidelinePosition.position.SetZ(0.0f), (double) sidelinePosition.position.x > 0.0 ? Quaternion.Euler(0.0f, 270f, 0.0f) : Quaternion.Euler(0.0f, 90f, 0.0f));
      this._stopQBFollow = true;
      MatchManager.instance.playManager.SetPlayCallingEnabled(false);
      this._isInHuddle = false;
      this._transitionActive = true;
      this.SetPlayConfirmButtonVisibility(false);
      this.SetWristPlayCallVisibility(false);
      this.SetTimeoutVisibility(false);
      this.SetHurryUpVisibility(false);
      this.StartCoroutine(this.ResumePlayerMovement());
    }

    protected Transform GetSidelinePosition() => this._sideLinePosition[PersistentData.userIsHome ? 0 : 1];

    private IEnumerator DoFadePlayerCamera(float time = 0.35f)
    {
      yield return (object) GamePlayerController.CameraFade.Fade(time);
    }

    private IEnumerator DoClearPlayerCamera(float time = 0.35f)
    {
      yield return (object) GamePlayerController.CameraFade.Clear(time);
    }

    private void DoBlinkPlayerCamera(float time = 1f) => GamePlayerController.CameraFade.Blink(time);

    private IEnumerator ResumePlayerMovement()
    {
      yield return (object) new WaitForSeconds(10f);
      this._stopQBFollow = false;
      MatchManager.instance.playManager.SetPlayCallingEnabled(true);
      this.MovePlayerCamera();
      this._transitionActive = false;
    }

    private void HandleThrowProcessed(ThrowData throwData)
    {
      this._didThrowBall = true;
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      if (!throwData.hasTarget)
      {
        Debug.Log((object) "No target receiver--throwing ball away");
        if (savedOffPlay.GetPlayType() == global::PlayType.Pass)
        {
          this._playersManager.intendedReceiver = (PlayerAI) null;
          this._playersManager.passDestination = throwData.targetPosition;
          this._playersManager.passVelocity = throwData.throwVector;
          this._playersManager.ExecuteThrow(true);
        }
        else if (savedOffPlay.GetPlayType() == global::PlayType.Run)
        {
          PlayerAI playerAi = this._playersManager.curUserScriptRef[savedOffPlay.GetHandoffTarget()];
          GameplayUI.HidePointer();
        }
      }
      if (throwData.closestTarget is AxisReceiver closestTarget)
      {
        Debug.Log((object) "Throw successful");
        if (savedOffPlay.GetPlayType() == global::PlayType.Pass)
        {
          this._playersManager.intendedReceiver = closestTarget.GetPlayerScript();
          this._playersManager.passDestination = throwData.targetPosition;
          this._playersManager.passVelocity = throwData.throwVector;
          this._playersManager.ExecuteThrow(true);
        }
        else if (savedOffPlay.GetPlayType() == global::PlayType.Run)
        {
          PlayerAI playerAi = this._playersManager.curUserScriptRef[savedOffPlay.GetHandoffTarget()];
          this._playersManager.HandOffBall(false);
          GameplayUI.HidePointer();
        }
        if (!AxisGameFlow.debug)
          return;
        Time.timeScale = 0.2f;
      }
      else
      {
        Debug.Log((object) "Closest target not axisReceiver !!");
        this._playersManager.intendedReceiver = (PlayerAI) null;
        this._playersManager.passDestination = throwData.targetPosition;
        this._playersManager.passVelocity = throwData.throwVector;
        this._playersManager.ExecuteThrow(true);
      }
    }

    protected virtual void HandlePlayCallStarted()
    {
      VRState.LocomotionEnabled.SetValue(false);
      PEGameplayEventManager.RecordShowPlaybookEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position);
      ScoreClockState.SetYardLine.Trigger();
    }

    protected virtual void HandlePlayCallMade()
    {
      PEGameplayEventManager.RecordPlayCallMadeEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position);
      this.SetWristPlayCallVisibility(true);
    }

    protected virtual void HandlePlayCallEnded()
    {
      GameSettings.OffenseGoingNorth.SetValue(global::Game.OffenseGoingNorth);
      this.UpdatePointerState();
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      PEGameplayEventManager.RecordPlaySelectedEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, MatchManager.instance.timeManager.GetQuarter(), global::Game.IsHomeTeamOnOffense, global::Game.OffenseGoingNorth, savedOffPlay, MatchManager.instance.playManager.savedDefPlay, MatchManager.down, ProEra.Game.MatchState.FirstDown.Value, ProEra.Game.MatchState.BallOn.Value);
      MatchManager.instance.timeManager.SetRunPlayClock(true);
      this.SetWristPlayCallVisibility(false);
    }

    protected virtual void HandleUserQBInHuddle() => MatchManager.instance.playersManager.ForcePlayersToHuddleAfterPlay();

    private void HandleUserTimeoutCalled()
    {
    }

    private void HandleUserPenaltyCalled()
    {
    }

    private void HandlePlayReset()
    {
      this.SetWristPlayCallVisibility(false);
      this.SetTimeoutVisibility(false);
      this.SetHurryUpVisibility(false);
      this._scene.DeactivateSweetSpot();
    }

    public bool IsTackling => this._tacklehandle.running;

    protected virtual void HandleQBTackle(bool isTackled)
    {
      if (!(!(bool) ProEra.Game.MatchState.Turnover & isTackled) || this.IsTackling)
        return;
      this._tacklehandle.Run(this.HandleQBTackleRoutine());
      this._throwManager.SetCanThrowBall(false);
    }

    private IEnumerator HandleQBTackleRoutine()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      AxisGameFlow axisGameFlow = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      Debug.Log((object) "Tackle Feedback Started!");
      VREvents.PlayerCollision.Trigger(MatchManager.instance.playersManager.tackler.gameObject);
      VRState.LocomotionEnabled.SetValue(false);
      PersistentSingleton<GamePlayerController>.Instance.HapticsController.SendHapticPulseFromBothNodes(axisGameFlow.tackleRumbleStrength, axisGameFlow.tackleRumbleDuration);
      Debug.Log((object) "Tackle Feedback Finished!");
      axisGameFlow._qbAI.IsTackledEvent -= new PlayerAI.OnIsTackledEvent(axisGameFlow.HandleQBTackle);
      axisGameFlow._tacklehandle.Stop();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) null;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected virtual void HandleMatchStateChanged(EMatchState state)
    {
      Debug.Log((object) ("OnMatchStateChanged: " + state.ToString()));
      if (state == EMatchState.Beginning)
        return;
      this._gameStarted = true;
    }

    protected virtual void HandleUserRequestedMainMenu()
    {
      if (AppState.SeasonMode.Value <= ESeasonMode.kUnknown)
        return;
      int seasonWins = SeasonModeManager.self.seasonModeData.seasonWins;
      int seasonLosses = SeasonModeManager.self.seasonModeData.seasonLosses;
      AnalyticEvents.Record<SeasonGameExitedEarlyArgs>(new SeasonGameExitedEarlyArgs(seasonWins + seasonLosses, seasonWins, seasonLosses, ProEra.Game.MatchState.Stats.User.Score, ProEra.Game.MatchState.Stats.Comp.Score, MatchManager.gameLength, (int) GameSettings.DifficultyLevel, !ScriptableSingleton<ThrowSettings>.Instance.AutoAimSettings.UseMinimalPassAssistSettings, (float) (1.0 - (double) MatchManager.instance.timeManager.GetTotalSecondsRemainingInGame() / (double) (MatchManager.gameLength * 4))));
    }

    public void SetWristPlayCallVisibility(bool isVisible)
    {
      if (!global::Game.IsPlayerOneOnOffense || !global::Game.UserControlsQB || !global::Game.CurrentPlayHasUserQBOnField)
        return;
      if (isVisible)
      {
        if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
          UIDispatch.RightTopWristScreen.DisplayView(EScreens.kWristAudiblePlay);
        else
          UIDispatch.LeftTopWristScreen.DisplayView(EScreens.kWristAudiblePlay);
        this.isWristPlayCallVisible = true;
      }
      else
      {
        if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
          UIDispatch.RightTopWristScreen.HideView(EScreens.kWristAudiblePlay);
        else
          UIDispatch.LeftTopWristScreen.HideView(EScreens.kWristAudiblePlay);
        this.isWristPlayCallVisible = false;
      }
    }

    public void SetPlayConfirmButtonVisibility(bool isVisible)
    {
      if (AppState.GameMode == EGameMode.kHeroMoment)
        return;
      if (isVisible && global::Game.IsPlayerOneOnOffense)
      {
        if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
          UIDispatch.RightTopWristScreen2.DisplayView(EScreens.kWristPlayConfirm);
        else
          UIDispatch.LeftTopWristScreen2.DisplayView(EScreens.kWristPlayConfirm);
      }
      else if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
        UIDispatch.RightTopWristScreen2.HideView(EScreens.kWristPlayConfirm);
      else
        UIDispatch.LeftTopWristScreen2.HideView(EScreens.kWristPlayConfirm);
    }

    public void SetHurryUpVisibility(bool isVisible)
    {
      if (AppState.GameMode == EGameMode.kPracticeMode || AppState.GameMode == EGameMode.kHeroMoment || !global::Game.UserControlsQB)
        return;
      if (isVisible && global::Game.IsPlayerOneOnOffense)
      {
        if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
          UIDispatch.RightTopWristScreen.DisplayView(EScreens.kWristHurryUp);
        else
          UIDispatch.LeftTopWristScreen.DisplayView(EScreens.kWristHurryUp);
      }
      else if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
        UIDispatch.RightTopWristScreen.HideView(EScreens.kWristHurryUp);
      else
        UIDispatch.LeftTopWristScreen.HideView(EScreens.kWristHurryUp);
    }

    public void SetTimeoutVisibility(bool isVisible)
    {
      if (AppState.GameMode == EGameMode.kPracticeMode || AppState.GameMode == EGameMode.kHeroMoment || AppState.GameMode == EGameMode.kOnboarding || !global::Game.UserControlsQB)
        return;
      if (isVisible && global::Game.CurrentPlayHasUserQBOnField)
      {
        if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
          UIDispatch.RightBottomWristScreen.DisplayView(EScreens.kWristTimeout);
        else
          UIDispatch.LeftBottomWristScreen.DisplayView(EScreens.kWristTimeout);
      }
      else if ((bool) ScriptableSingleton<VRSettings>.Instance.UseLeftHand)
        UIDispatch.RightBottomWristScreen.HideView(EScreens.kWristTimeout);
      else
        UIDispatch.LeftBottomWristScreen.HideView(EScreens.kWristTimeout);
    }

    public bool CheckKeyBoardHandoff()
    {
      bool flag = false;
      PlayerAI playerAi = this._playersManager.curUserScriptRef[MatchManager.instance.playManager.savedOffPlay.GetHandoffTarget()];
      Vector3 position1 = playerAi.trans.position;
      Vector3 position2 = PersistentSingleton<GamePlayerController>.Instance.transform.position;
      double num1 = (double) Vector3.Distance(position1, position2);
      float num2 = Vector3.Dot((position2 - position1).normalized, playerAi.trans.forward);
      double num3 = (double) Field.ONE_YARD * (double) this.HANDOFF_DIST;
      if (num1 < num3 && (double) num2 > (double) this.HANDOFF_ANGLE_DOT)
        flag = true;
      return flag;
    }

    private enum QBFollowTypes
    {
      OnCall,
      FollowBody,
      FollowHead,
    }
  }
}
