// Decompiled with JetBrains decompiler
// Type: StateMachine
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
  public RuntimeAnimatorController defensiveLinemen;
  public RuntimeAnimatorController defensiveBackers;
  public RuntimeAnimatorController offensiveLinemen;
  public RuntimeAnimatorController receivers;
  public RuntimeAnimatorController quaterback;
  public Animator fsmAnimator;
  public Gameboard gameboard;
  private PlayerIdentity _player;
  public static readonly HashedString DefensiveLineman = new HashedString("DL");
  public static readonly HashedString DefensiveBack = new HashedString("DB");
  public static readonly HashedString LineBacker = new HashedString("LB");
  public static readonly HashedString OffensiveLineman = new HashedString("OL");
  public static readonly HashedString TightEnd = new HashedString("TE");
  public static readonly HashedString WideReceiver = new HashedString("WR");
  public static readonly HashedString RunningBack = new HashedString("RB");
  public static readonly HashedString Quaterback = new HashedString("QB");
  public static readonly HashedString Special = new HashedString("SPEC");
  public static readonly HashedString SpeedParamHash = new HashedString("Speed");
  public static readonly HashedString IsBallCarrierParamHash = new HashedString("IsBallCarrier");
  public static readonly HashedString IsInCenterOfAttentionParamHash = new HashedString("IsInCenterOfAttention");
  public static readonly HashedString LineSetEventHash = new HashedString("line_set");
  public static readonly HashedString BallSnapEventHash = new HashedString("ball_snap");
  public static readonly HashedString ManInMotionEventHash = new HashedString("man_in_motion");
  public static readonly HashedString ShiftEventHash = new HashedString("shift");
  public static readonly HashedString HandoffEventHash = new HashedString("handoff");
  public static readonly HashedString PlayActionEventHash = new HashedString("play_action");
  public static readonly HashedString PassFowardEventHash = new HashedString("pass_forward");
  public static readonly HashedString PassArrivedEventHash = new HashedString("pass_arrived");
  public static readonly HashedString PassOutcomeCaughtEventHash = new HashedString("pass_outcome_caught");
  public static readonly HashedString TouchdownEventHash = new HashedString("touchdown");
  public static readonly HashedString TackleEventHash = new HashedString("tackle");
  public static readonly HashedString FirstContactEventHash = new HashedString("first_contact");
  public static readonly HashedString PenaltyFlagEventHash = new HashedString("penalty_flag");
  public static readonly HashedString PenaltyDeclinedEventHash = new HashedString("penalty_declined");
  private int _lastFrameUpdated = -1;

  public event Action<StateMachineBehaviour> OnStateChanged;

  public void InvokeStateChangeEvent(StateMachineBehaviour newState)
  {
    Action<StateMachineBehaviour> onStateChanged = this.OnStateChanged;
    if (onStateChanged == null)
      return;
    onStateChanged(newState);
  }

  private void OnEnable() => Gameboard.OnEventRaised += new Action<GameplayEvent>(this.OnEventRaisedHandler);

  private void OnDisable()
  {
    Gameboard.OnEventRaised -= new Action<GameplayEvent>(this.OnEventRaisedHandler);
    this.Unregister();
  }

  private void Awake()
  {
    this._player = this.GetComponent<PlayerIdentity>();
    this._player.OnIdentitySet += new System.Action(this.AssignController);
    this._player.OnIdentitySet += new System.Action(this.Unregister);
    this._player.OnIdentitySet += new System.Action(this.Register);
  }

  private void OnDestroy()
  {
    this._player.OnIdentitySet -= new System.Action(this.AssignController);
    this._player.OnIdentitySet -= new System.Action(this.Unregister);
    this._player.OnIdentitySet -= new System.Action(this.Register);
  }

  private void Register()
  {
    if (this._player.teamId == 1)
      this.gameboard.homePlayers.Add(this._player);
    else
      this.gameboard.awayPlayers.Add(this._player);
  }

  private void Unregister()
  {
    if (this.gameboard.homePlayers.Contains(this._player))
      this.gameboard.homePlayers.Remove(this._player);
    if (!this.gameboard.awayPlayers.Contains(this._player))
      return;
    this.gameboard.awayPlayers.Remove(this._player);
  }

  private void AssignController()
  {
    this.fsmAnimator.gameObject.SetActive(false);
    this.fsmAnimator.runtimeAnimatorController = (RuntimeAnimatorController) null;
    RuntimeAnimatorController animatorController = (RuntimeAnimatorController) null;
    if (!this._player.procedural)
      return;
    HashedString positionGroup = this._player.positionGroup;
    if (!positionGroup.Equals(StateMachine.LineBacker))
    {
      positionGroup = this._player.positionGroup;
      if (!positionGroup.Equals(StateMachine.DefensiveBack))
      {
        positionGroup = this._player.positionGroup;
        if (!positionGroup.Equals(StateMachine.WideReceiver))
        {
          positionGroup = this._player.positionGroup;
          if (!positionGroup.Equals(StateMachine.RunningBack))
          {
            positionGroup = this._player.positionGroup;
            if (!positionGroup.Equals(StateMachine.TightEnd))
            {
              positionGroup = this._player.positionGroup;
              if (positionGroup.Equals(StateMachine.Quaterback))
              {
                animatorController = this.quaterback;
                goto label_13;
              }
              else
              {
                positionGroup = this._player.positionGroup;
                if (positionGroup.Equals(StateMachine.OffensiveLineman))
                {
                  animatorController = this.offensiveLinemen;
                  goto label_13;
                }
                else
                  goto label_13;
              }
            }
          }
        }
        animatorController = this.receivers;
        goto label_13;
      }
    }
    animatorController = this.defensiveBackers;
label_13:
    if (!((UnityEngine.Object) animatorController != (UnityEngine.Object) null))
      return;
    this.fsmAnimator.runtimeAnimatorController = animatorController;
    this.fsmAnimator.gameObject.SetActive(true);
  }

  private void OnEventRaisedHandler(GameplayEvent gameplayEvent)
  {
    if (!this.fsmAnimator.enabled || (UnityEngine.Object) this.fsmAnimator.runtimeAnimatorController == (UnityEngine.Object) null || !this.fsmAnimator.isInitialized || Array.Find<AnimatorControllerParameter>(this.fsmAnimator.parameters, (Predicate<AnimatorControllerParameter>) (x => x.nameHash == gameplayEvent.name.Hash)) == null)
      return;
    this.fsmAnimator.SetTrigger(gameplayEvent.name.Hash);
  }

  public void UpdateValues(PlayerTelegraphy telegraphy)
  {
    if (this._lastFrameUpdated == Time.frameCount)
      return;
    this._lastFrameUpdated = Time.frameCount;
    Animator fsmAnimator1 = this.fsmAnimator;
    HashedString hashedString = StateMachine.SpeedParamHash;
    int hash1 = hashedString.Hash;
    double speed = (double) telegraphy.Speed;
    fsmAnimator1.SetFloat(hash1, (float) speed);
    Animator fsmAnimator2 = this.fsmAnimator;
    hashedString = StateMachine.IsBallCarrierParamHash;
    int hash2 = hashedString.Hash;
    int num1 = telegraphy.IsBallCarrier ? 1 : 0;
    fsmAnimator2.SetBool(hash2, num1 != 0);
    Animator fsmAnimator3 = this.fsmAnimator;
    hashedString = StateMachine.IsInCenterOfAttentionParamHash;
    int hash3 = hashedString.Hash;
    int num2 = (double) Vector3.Distance(telegraphy.transform.position, ScriptableSingleton<Gameboard>.Instance.GameAttentionCenter) < 8.0 ? 1 : 0;
    fsmAnimator3.SetBool(hash3, num2 != 0);
  }
}
