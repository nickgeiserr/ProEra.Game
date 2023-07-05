// Decompiled with JetBrains decompiler
// Type: BallManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using MovementEffects;
using ProEra.Game;
using System;
using System.Collections.Generic;
using TB12;
using UDB;
using UnityEngine;
using Vars;

public class BallManager : SingletonBehaviour<BallManager, MonoBehaviour>
{
  public TimeManager timeManager;
  [SerializeField]
  private Animator ballAnim;
  [SerializeField]
  private Collider ballCollider;
  [SerializeField]
  private BallObject _ballObject;
  [SerializeField]
  private Transform _ballEndPoints;
  [HideInInspector]
  public float passVelocity;
  [HideInInspector]
  public bool toss;
  [HideInInspector]
  public Vector3 passTarget;
  private Transform _trans;
  private Rigidbody _rigidbd;
  private ForwardProgressTracker _forwardProgressTracker;
  private float passDist;
  private float passAngle;
  private Transform tossTarget;
  private PlayerAI tossTargetAI;
  private Vector3 _lastCenterPosition;
  private float _timeToSnap;
  private Vector3 _cachedScale;
  private bool _hasCrossedLOS;
  private bool _isDestroying;
  private List<PlayerAI> _defenders;
  private List<int> closeDefenderScore = new List<int>();
  private List<int> nearbyDefenderScore = new List<int>();

  public EBallState ballState => Ball.State.BallState.Value;

  public bool HasCrossedLOS => this._hasCrossedLOS;

  public Vector3 LastCenterPosition => this._lastCenterPosition;

  public Transform trans
  {
    get => this._trans;
    set => this._trans = value;
  }

  public Rigidbody rigidbd
  {
    get => this._rigidbd;
    set => this._rigidbd = value;
  }

  private new void Awake()
  {
    this._trans = this.gameObject.transform;
    this._rigidbd = this.gameObject.GetComponent<Rigidbody>();
    this._rigidbd.sleepThreshold = 0.0f;
    this._cachedScale = this._trans.lossyScale;
    this._forwardProgressTracker = this.gameObject.GetComponent<ForwardProgressTracker>();
  }

  private void OnEnable()
  {
    Ball.State.Volume.OnValueChanged += new Action<float>(this.SetVolume);
    Ball.State.BallState.OnValueChanged += new Action<EBallState>(this.SetBallState);
  }

  private void OnDisable()
  {
    Ball.State.Volume.OnValueChanged -= new Action<float>(this.SetVolume);
    Ball.State.BallState.OnValueChanged -= new Action<EBallState>(this.SetBallState);
  }

  private void Start()
  {
    this.toss = false;
    Ball.State.BallState.SetValue(EBallState.PlayOver);
  }

  private void OnDestroy()
  {
    Debug.Log((object) "BallManager -> OnDestroy");
    this._isDestroying = true;
    this.StopAllCoroutines();
    Timing.KillCoroutines();
    SingletonBehaviour<BallManager, MonoBehaviour>.instance = (BallManager) null;
  }

  public void SetVolume(float vol)
  {
  }

  public async void PlayBallSound(int i)
  {
  }

  public void SetBallState(EBallState newState)
  {
    this.ballAnim.SetInteger(HashIDs.self.ballStateInt, (int) newState);
    switch (newState)
    {
      case EBallState.InCentersHandsBeforeSnap:
        this._ballObject.Pick();
        this.DisableCollider();
        this.rigidbd.constraints = RigidbodyConstraints.FreezeAll;
        this.SetDrag(0.0f);
        break;
      case EBallState.PlayersHands:
        this._ballObject.Pick();
        this.DisableCollider();
        this.rigidbd.constraints = RigidbodyConstraints.FreezeAll;
        if (global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB && global::Game.CurrentPlayHasUserQBOnField)
          PersistentSingleton<GamePlayerController>.Instance.HapticsController.SendHapticPulseFromBothNodes(0.9f, 0.3f);
        this.SetDrag(0.0f);
        break;
      case EBallState.InAirPass:
        this.rigidbd.constraints = RigidbodyConstraints.None;
        Timing.RunCoroutine(this.AvoidCollisions((double) Vector3.Distance(this.trans.position, this.passTarget) > (double) Field.FOUR_YARDS ? Field.TWO_YARDS : Field.ONE_YARD_TWO_FEET));
        this.SetDrag(0.0f);
        if (!this._hasCrossedLOS || !global::Game.IsPlayActive)
          break;
        MatchManager.instance.EndPlay(PlayEndType.Incomplete);
        break;
      case EBallState.InAirToss:
        if (global::Game.IsPlayerOneOnOffense && (UnityEngine.Object) MatchManager.instance.playersManager.ballHolderScript != (UnityEngine.Object) null && MatchManager.instance.playersManager.ballHolderScript.IsQB())
        {
          Debug.LogWarning((object) " Why is the ball holder still the QB when the ball state be being set to InAirToss");
          this.rigidbd.constraints = RigidbodyConstraints.None;
        }
        else
        {
          Timing.RunCoroutine(this.AvoidCollisions(Field.TWO_YARDS));
          this.rigidbd.constraints = RigidbodyConstraints.None;
          Timing.RunCoroutine(this._MovingBall());
        }
        this.SetDrag(0.0f);
        break;
      case EBallState.OnGround:
        this.EnableCollider();
        this.rigidbd.constraints = RigidbodyConstraints.None;
        AppSounds.Play3DSfx(ESfxTypes.kBallBounce, this.trans.position);
        this.SetDrag(1f);
        break;
      case EBallState.Kick:
        Timing.RunCoroutine(this.AvoidCollisions(Field.SIX_YARDS));
        this.rigidbd.constraints = RigidbodyConstraints.None;
        this.SetDrag(0.0f);
        break;
      case EBallState.Fumble:
        this.EnableCollider();
        this.rigidbd.constraints = RigidbodyConstraints.None;
        this.SetDrag(1f);
        break;
      case EBallState.PlayOver:
        if ((UnityEngine.Object) this.trans.parent == (UnityEngine.Object) null)
        {
          this.EnableCollider();
          this.rigidbd.constraints = RigidbodyConstraints.None;
        }
        this.SetDrag(0.0f);
        break;
      case EBallState.InAirSnap:
        if (global::Game.IsPlayerOneOnOffense && global::Game.UserControlsQB)
        {
          this.EnableCollider();
          this.rigidbd.constraints = RigidbodyConstraints.FreezeAll;
        }
        this.SetDrag(0.0f);
        break;
      case EBallState.InAirDeflected:
        this.rigidbd.constraints = RigidbodyConstraints.None;
        this.SetDrag(0.0f);
        break;
      case EBallState.OnTee:
        this.DisableCollider();
        this.rigidbd.constraints = RigidbodyConstraints.FreezeAll;
        this.SetDrag(0.0f);
        break;
      case EBallState.InAirDrop:
        if (global::Game.IsPlayerOneOnOffense)
        {
          this.rigidbd.constraints = RigidbodyConstraints.None;
          this.EnableCollider();
          this.SetDrag(0.0f);
          break;
        }
        Debug.LogError((object) "Error! BallState InAirDrop is only valid in ProEra mode");
        throw new NotImplementedException();
    }
  }

  public void ResetColliderSize()
  {
  }

  public void EnableCollider() => this.ballCollider.enabled = true;

  public void DisableCollider() => this.ballCollider.enabled = false;

  private IEnumerator<float> AvoidCollisions(float dist)
  {
    Vector3 startPoint = this.trans.position;
    this.DisableCollider();
    Vector3 yOffset = new Vector3(0.0f, Field.ONE_YARD_FIFTEEN_INCHES, 0.0f);
    while ((double) Vector3.Distance(startPoint, this.trans.position) < (double) dist && (!((UnityEngine.Object) this.tossTarget != (UnityEngine.Object) null) || (double) Vector3.Distance(this.trans.position, this.tossTarget.position + yOffset) >= (double) Field.NINE_INCHES))
      yield return 0.0f;
    this.EnableCollider();
  }

  public void BallCatchAnalysis(PlayerAI playerAI)
  {
    Debug.LogWarning((object) ("Ball Catch for " + playerAI.name));
    if ((UnityEngine.Object) MatchManager.instance.playersManager.ballHolder != (UnityEngine.Object) null)
    {
      Debug.LogWarning((object) (playerAI.name + " can't catch ball, there is a possessor already."));
    }
    else
    {
      if (PlayState.IsPass && (global::Game.BallIsThrownOrKicked || global::Game.BS_IsInAirPass) && playerAI.onOffense && playerAI.indexInFormation == 5)
        return;
      int num1 = global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[6].indexOnTeam : MatchManager.instance.playersManager.curCompScriptRef[6].indexOnTeam;
      PlayerData playerData = global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.userTeamData.GetPlayer(num1) : MatchManager.instance.playersManager.compTeamData.GetPlayer(num1);
      PlayerStats playerStats = global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playManager.userTeamCurrentPlayStats.players[num1] : MatchManager.instance.playManager.compTeamCurrentPlayStats.players[num1];
      float distance = this.trans.position.z - MatchManager.instance.savedLineOfScrim;
      if (MatchManager.instance.onsideKick && !(bool) PlayState.PlayOver)
      {
        KickoffConfig kickoffConfig = global::Game.KickoffConfig;
        bool flag = true;
        if (Ball.State.BallState.Value == EBallState.Kick && (double) UnityEngine.Random.Range(0.0f, 1f) <= (double) kickoffConfig.InitialDropChanceForOnsideKick)
        {
          playerAI.hasAttemptedCatch = true;
          this.SetVelocity(kickoffConfig.GetRandomOnsideKickDeflectionVelocity());
          Ball.State.BallState.SetValue(EBallState.InAirDeflected);
          PlayerAI.NotifyAllPlayersOfDeflectedPass();
          MatchManager.instance.playManager.deflectedPassTimer = MatchManager.instance.playTimer;
          flag = false;
        }
        if (!flag)
          return;
        playerAI.RecoverBallOnKickoff();
      }
      else if ((global::Game.BS_IsInAirPass || global::Game.BS_IsInAirDeflected) && !playerAI.droppedPass)
      {
        if (PlayState.IsPass)
        {
          Debug.Log((object) string.Format("{0} {1} ({2} caught ball!", (object) playerAI.firstName, (object) playerAI.lastName, (object) playerAI.playerPosition));
          bool interception = false;
          if (global::Game.IsPlayerOneOnDefense && playerAI.onUserTeam)
          {
            MatchManager.instance.playManager.AddToCurrentPlayLog(playerAI.firstName + " " + playerAI.lastName + " (" + playerAI.playerPosition.ToString() + ") intercepted the ball at the " + Field.GetYardLineStringByFieldLocation(playerAI.trans.position.z) + " yard line");
            ++MatchManager.instance.playManager.userTeamCurrentPlayStats.players[playerAI.indexOnTeam].Interceptions;
            ++MatchManager.instance.playManager.compTeamCurrentPlayStats.players[MatchManager.instance.playersManager.curCompScriptRef[5].indexOnTeam].QBInts;
            ++ProEra.Game.MatchState.Stats.User.Interceptions;
            if ((UnityEngine.Object) playerAI.mainGO == (UnityEngine.Object) MatchManager.instance.playersManager.userPlayer)
              ++ProEra.Game.MatchState.Stats.User.Ints;
            interception = true;
          }
          else if (global::Game.IsPlayerOneOnOffense && !playerAI.onUserTeam)
          {
            if (MatchManager.instance.soundOn)
            {
              int num2 = PersistentData.userIsHome ? 1 : 0;
            }
            MatchManager.instance.playManager.AddToCurrentPlayLog(playerAI.firstName + " " + playerAI.lastName + " (" + playerAI.playerPosition.ToString() + ") intercepted the ball at the " + Field.GetYardLineStringByFieldLocation(playerAI.trans.position.z) + " yard line");
            ++MatchManager.instance.playManager.compTeamCurrentPlayStats.players[playerAI.indexOnTeam].Interceptions;
            ++MatchManager.instance.playManager.userTeamCurrentPlayStats.players[MatchManager.instance.playersManager.curUserScriptRef[5].indexOnTeam].QBInts;
            ++ProEra.Game.MatchState.Stats.Comp.Interceptions;
            if ((UnityEngine.Object) MatchManager.instance.playersManager.userPlayerP2 != (UnityEngine.Object) null && (UnityEngine.Object) MatchManager.instance.playersManager.userPlayerP2 == (UnityEngine.Object) playerAI.mainGO)
              ++ProEra.Game.MatchState.Stats.Comp.Ints;
            interception = true;
          }
          PEGameplayEventManager.RecordBallCaughtEvent(MatchManager.instance.timeManager.GetGameClockTimer(), this.transform.position, playerAI, interception);
          MatchManager.instance.playersManager.SetBallHolder(playerAI.mainGO, playerAI.onUserTeam);
          this.PlayBallSound(2);
        }
        else
        {
          if (!PlayState.IsPass)
            return;
          Debug.LogWarning((object) "BallManager: Deflected");
          if (global::Game.IsPlayerOneOnOffense && !playerAI.onUserTeam && !playerAI.hasDeflectedPass)
          {
            playerAI.hasDeflectedPass = true;
            MatchManager.instance.playManager.deflectedPassTimer = MatchManager.instance.playTimer;
            MatchManager.instance.playManager.AddToCurrentPlayLog(playerAI.firstName + " " + playerAI.lastName + " (" + playerAI.playerPosition.ToString() + ") deflected the pass at the " + Field.GetYardLineStringByFieldLocation(playerAI.trans.position.z) + " yard line");
            ++MatchManager.instance.playManager.compTeamCurrentPlayStats.players[playerAI.indexOnTeam].KnockDowns;
            MatchManager.instance.playersManager.tackler = playerAI;
          }
          else if (global::Game.IsPlayerOneOnDefense && playerAI.onUserTeam && !playerAI.hasDeflectedPass)
          {
            playerAI.hasDeflectedPass = true;
            MatchManager.instance.playManager.deflectedPassTimer = MatchManager.instance.playTimer;
            MatchManager.instance.playManager.AddToCurrentPlayLog(playerAI.firstName + " " + playerAI.lastName + " (" + playerAI.playerPosition.ToString() + ") deflected the pass at the " + Field.GetYardLineStringByFieldLocation(playerAI.trans.position.z) + " yard line");
            ++MatchManager.instance.playManager.userTeamCurrentPlayStats.players[playerAI.indexOnTeam].KnockDowns;
            MatchManager.instance.playersManager.tackler = playerAI;
          }
          if (global::Game.IsPlayerOneOnOffense && playerAI.onUserTeam)
          {
            this.StartCoroutine(playerAI.SetDroppedPass());
            ++ProEra.Game.MatchState.Stats.User.DroppedPasses;
            MatchManager.instance.playManager.AddToCurrentPlayLog(playerAI.firstName + " " + playerAI.lastName + " (" + playerAI.playerPosition.ToString() + ") dropped the pass at the " + Field.GetYardLineStringByFieldLocation(playerAI.trans.position.z) + " yard line");
            ++MatchManager.instance.playManager.userTeamCurrentPlayStats.players[playerAI.indexOnTeam].Drops;
          }
          else if (global::Game.IsPlayerOneOnDefense && !playerAI.onUserTeam)
          {
            this.StartCoroutine(playerAI.SetDroppedPass());
            ++ProEra.Game.MatchState.Stats.Comp.DroppedPasses;
            MatchManager.instance.playManager.AddToCurrentPlayLog(playerAI.firstName + " " + playerAI.lastName + " (" + playerAI.playerPosition.ToString() + ") dropped the pass at the " + Field.GetYardLineStringByFieldLocation(playerAI.trans.position.z) + " yard line");
            ++MatchManager.instance.playManager.compTeamCurrentPlayStats.players[playerAI.indexOnTeam].Drops;
          }
          Ball.State.BallState.SetValue(EBallState.InAirDeflected);
          PlayerAI.NotifyAllPlayersOfDeflectedPass();
        }
      }
      else if (global::Game.BS_IsInAirToss)
      {
        Debug.LogWarning((object) "Game.BS_IsInAirToss");
        MatchManager.instance.playersManager.SetBallHolder(playerAI.mainGO, playerAI.onUserTeam);
      }
      else
      {
        if (global::Game.BS_IsInAirSnap)
          return;
        if (global::Game.BS_IsKick)
        {
          if (PlayState.IsPunt)
          {
            MatchManager.instance.playManager.AddToCurrentPlayLog(playerData.FirstName + " " + playerData.LastName + " punted the ball");
            ++playerStats.Punts;
            playerStats.PuntYards += Field.ConvertDistanceToYards(distance);
            if (Field.FurtherDownfield(this.trans.position.z, Field.OPPONENT_TWENTY_YARD_LINE))
              ++playerStats.PuntsInside20;
          }
          if (UnityEngine.Random.Range(0, 100) < 0)
          {
            MatchManager.instance.playManager.deflectedPassTimer = MatchManager.instance.playTimer;
            this.SetVelocity(new Vector3(this.rigidbd.velocity.x / 5f, this.rigidbd.velocity.y / 5f, (float) (-(double) this.rigidbd.velocity.z / 5.0)));
            Ball.State.BallState.SetValue(EBallState.InAirDeflected);
            PlayerAI.NotifyAllPlayersOfDeflectedPass();
          }
          else
            MatchManager.instance.playersManager.SetBallHolder(playerAI.mainGO, playerAI.onUserTeam);
        }
        else
        {
          if (!global::Game.BS_IsOnGround && !global::Game.BS_IsFumble)
            return;
          playerAI.PickUpBallFromGround();
        }
      }
    }
  }

  private bool DetermineCatch(PlayerAI recScript)
  {
    if (recScript.isEngagedInBlock || (double) this.trans.position.z > (double) Field.NORTH_BACK_OF_ENDZONE || (double) this.trans.position.z < (double) Field.SOUTH_BACK_OF_ENDZONE || (double) Mathf.Abs(this.trans.position.x) > (double) Field.OUT_OF_BOUNDS || (bool) ProEra.Game.MatchState.RunningPat && !recScript.onOffense)
      return false;
    if ((double) ProEra.Game.MatchState.BallOn.Value >= (double) this.trans.position.z)
      return true;
    if ((double) MatchManager.instance.playTimer - (double) MatchManager.instance.playManager.droppedPassTimer <= 0.20000000298023224)
      return false;
    int receiverScore = (int) ((double) recScript.catching * 0.699999988079071 + (double) recScript.awareness * 0.30000001192092896);
    int dropChance = (int) (-0.40000000596046448 * (double) receiverScore + 35.0);
    if ((double) MatchManager.instance.playTimer - (double) MatchManager.instance.playManager.deflectedPassTimer <= 3.0)
      dropChance *= Mathf.RoundToInt(1.25f);
    int num;
    if (recScript.onOffense)
    {
      int difficultyModifier = 0;
      if (global::Game.IsPlayerOneOnOffense)
        difficultyModifier = 10 - PersistentData.offDifficulty;
      this._defenders = !global::Game.IsPlayerOneOnDefense ? MatchManager.instance.playersManager.curCompScriptRef : MatchManager.instance.playersManager.curUserScriptRef;
      num = Mathf.Min(90, this.GetDropChanceBasedOnNearbyOpponents(recScript, this._defenders, receiverScore, dropChance, difficultyModifier));
    }
    else
    {
      if (recScript.hasAttemptedCatch)
        return false;
      int difficultyModifier = 0;
      if (global::Game.IsPlayerOneOnOffense)
        difficultyModifier = Mathf.RoundToInt((float) (10 - PersistentData.offDifficulty) / 2f);
      this._defenders = !global::Game.IsPlayerOneOnDefense ? MatchManager.instance.playersManager.curUserScriptRef : MatchManager.instance.playersManager.curCompScriptRef;
      num = Mathf.Min(98, this.GetDropChanceBasedOnNearbyOpponents(recScript, this._defenders, receiverScore, dropChance, difficultyModifier));
      recScript.hasAttemptedCatch = true;
    }
    return UnityEngine.Random.Range(0, 125) > num;
  }

  private int GetDropChanceBasedOnNearbyOpponents(
    PlayerAI player,
    List<PlayerAI> opponents,
    int receiverScore,
    int dropChance,
    int difficultyModifier)
  {
    Vector3 position = player.trans.position;
    int num1 = 0;
    int num2 = 0;
    this.closeDefenderScore.Clear();
    this.nearbyDefenderScore.Clear();
    for (int index = 0; index < opponents.Count; ++index)
    {
      float num3 = Vector3.Distance(position, opponents[index].trans.position);
      int num4 = !player.onOffense ? (int) ((double) opponents[index].catching * 0.699999988079071 + (double) opponents[index].awareness * 0.30000001192092896) + 45 : (int) ((double) opponents[index].coverage * 0.699999988079071 + (double) opponents[index].awareness * 0.30000001192092896);
      if ((double) num3 < (double) MatchManager.instance.playersManager.playersCloseDistance)
      {
        ++num1;
        this.closeDefenderScore.Add(num4);
      }
      else if ((double) num3 < (double) MatchManager.instance.playersManager.playersNearbyDistance)
      {
        ++num2;
        this.nearbyDefenderScore.Add(num4);
      }
    }
    for (int index = 0; index < this.nearbyDefenderScore.Count; ++index)
      dropChance += Mathf.Max(10 * this.nearbyDefenderScore.Count, this.nearbyDefenderScore[index] + difficultyModifier - receiverScore);
    for (int index = 0; index < this.closeDefenderScore.Count; ++index)
      dropChance += Mathf.Max(34 * this.closeDefenderScore.Count, this.closeDefenderScore[index] + difficultyModifier - receiverScore);
    if (!player.onOffense && (double) player.trans.position.z - (double) MatchManager.instance.savedLineOfScrim < 5.0 * (double) Field.ONE_YARD)
      dropChance = Mathf.Clamp(dropChance, 95, 100);
    return dropChance;
  }

  public void Snap()
  {
    global::Game.OffensiveCenter.BallPossession.inBallPossession = false;
    Ball.State.BallState.SetValue(EBallState.InAirSnap);
    this._hasCrossedLOS = false;
  }

  public Vector3 BallisticVel(Vector3 target, float angle)
  {
    Vector3 vector3_1 = new Vector3(target.x, Field.TWO_YARDS, target.z) - this.trans.position;
    float y = vector3_1.y;
    vector3_1.y = 0.0f;
    float magnitude = vector3_1.magnitude;
    float f = angle * ((float) System.Math.PI / 180f);
    vector3_1.y = magnitude * Mathf.Tan(f);
    this.passVelocity = Mathf.Sqrt((magnitude + y / Mathf.Tan(f)) * Physics.gravity.magnitude / Mathf.Sin(2f * f));
    Vector3 vector3_2 = this.passVelocity * vector3_1.normalized;
    if (global::Game.BS_IsInAirToss)
      vector3_2 *= 0.65f;
    return vector3_2;
  }

  public void Kick(Vector3 target, float kickAngle)
  {
    this.PlayBallSound(1);
    this.rigidbd.useGravity = true;
    this.rigidbd.detectCollisions = true;
    this.passAngle = kickAngle;
    if (MatchManager.instance.onsideKick)
      this.passAngle = 20f;
    this.passTarget = target;
    Timing.RunCoroutine(this.ReleaseBall());
  }

  public void Throw(Vector3 target, float passD, bool bulletPass, int throwPower)
  {
    this.rigidbd.useGravity = true;
    this.rigidbd.detectCollisions = true;
    this.passTarget = target;
    this.passDist = passD;
    this.passAngle = 32f;
    float num1 = 0.45f;
    if (bulletPass)
      num1 = 0.35f;
    float num2 = (float) Mathf.RoundToInt(this.passDist / (Field.ONE_YARD * 5f));
    float num3 = (float) (0.039999999105930328 + (double) (100 - throwPower) * 0.00050000002374872565);
    this.passAngle *= num1 + num2 * num3;
    this.passAngle = Mathf.Clamp(this.passAngle, 15f, 45f);
    if (PlayState.PlayType.Value == PlayType.Run)
      this.passAngle = 25f;
    Timing.RunCoroutine(this.ReleaseBall());
  }

  public void Toss(PlayerAI trgt)
  {
    this.tossTarget = trgt.trans;
    this.toss = true;
    this.rigidbd.detectCollisions = true;
    Timing.RunCoroutine(this.ReleaseBall());
  }

  public void FumbleBall()
  {
    this.SetParent((Transform) null);
    this.rigidbd.useGravity = true;
    this.rigidbd.detectCollisions = true;
    this.rigidbd.isKinematic = false;
    Ball.State.BallState.SetValue(EBallState.Fumble);
    this.ballAnim.SetInteger(HashIDs.self.ballStateInt, 6);
  }

  public void VRThrow(Vector3 target, Vector3 throwVector)
  {
    this.passTarget = target;
    this.BallisticVel(this.passTarget, 360f - Quaternion.LookRotation(throwVector).eulerAngles.x);
  }

  private void FixedUpdate()
  {
    if (this._isDestroying)
      return;
    if (this._rigidbd.IsSleeping())
      this._rigidbd.WakeUp();
    Ball.State.BallPosition = this.trans.position;
    switch (Ball.State.BallState.Value)
    {
      case EBallState.InCentersHandsBeforeSnap:
        this._lastCenterPosition = this.trans.position;
        break;
      case EBallState.OnGround:
        this.ClampVelocity(13f);
        break;
    }
    if (this._hasCrossedLOS || !global::Game.BallHolderIsNotNull || !Field.FurtherDownfield(this.trans.position.z, ProEra.Game.MatchState.BallOn.Value))
      return;
    this._hasCrossedLOS = true;
  }

  public void SetVelocity(Vector3 v) => this.rigidbd.velocity = v;

  public void SetParent(Transform p)
  {
    this.trans.parent = p;
    if (!((UnityEngine.Object) p == (UnityEngine.Object) null))
      return;
    this.trans.localScale = this._cachedScale;
    this._ballObject.Release();
  }

  public void SetPosition(Vector3 p)
  {
    this.trans.position = p;
    Ball.State.BallPosition = p;
  }

  public void SetRotation(float y) => this.trans.eulerAngles = new Vector3(0.0f, y, 0.0f);

  public void SetAngularVelocity(Vector3 v) => this.rigidbd.angularVelocity = v;

  public void SetAngularVelocity()
  {
    FlightSettings flightSettings = ScriptableSingleton<ThrowSettings>.Instance.FlightSettings;
    float num1 = Mathf.Lerp(Mathf.Clamp01(this.rigidbd.velocity.magnitude / 23f), 1f, flightSettings.BallSpinSpeedFactor);
    float ballRotationWobbles = flightSettings.BallRotationWobbles;
    float z = (float) ((double) flightSettings.BallRotationsPerSecond * 360.0 * (System.Math.PI / 180.0)) * num1;
    float num2 = (float) ((double) flightSettings.BallRotationWobbles * 360.0 * (System.Math.PI / 180.0)) * num1 * ballRotationWobbles;
    Debug.Log((object) ("trans.localEulerAngles[" + this.trans.localEulerAngles.ToString() + "]wobble[" + num2.ToString() + "] rotSpeed[" + z.ToString() + "]"));
    this.SetAngularVelocity(this.trans.TransformDirection(new Vector3(num2, num2, z)));
  }

  private void ClampVelocity(float maxVelocity)
  {
    if ((double) Mathf.Abs(this.rigidbd.velocity.z) > (double) maxVelocity)
    {
      float num = Mathf.Abs(this.rigidbd.velocity.z) / maxVelocity;
      this.SetVelocity(new Vector3(this.rigidbd.velocity.x / num, this.rigidbd.velocity.y / num, this.rigidbd.velocity.z / num));
    }
    if ((double) Mathf.Abs(this.rigidbd.velocity.x) > (double) maxVelocity)
    {
      float num = Mathf.Abs(this.rigidbd.velocity.x) / maxVelocity;
      this.SetVelocity(new Vector3(this.rigidbd.velocity.x / num, this.rigidbd.velocity.y / num, this.rigidbd.velocity.z / num));
    }
    if ((double) Mathf.Abs(this.rigidbd.velocity.y) <= (double) maxVelocity)
      return;
    float num1 = Mathf.Abs(this.rigidbd.velocity.y) / maxVelocity;
    this.SetVelocity(new Vector3(this.rigidbd.velocity.x / num1, this.rigidbd.velocity.y / num1, this.rigidbd.velocity.z / num1));
  }

  public void FreezeAfterPlay()
  {
    this.SetVelocity(Vector3.zero);
    this.SetAngularVelocity(Vector3.zero);
    this.SetParent((Transform) null);
    if (global::Game.BS_OnTee)
    {
      this.SetPosition(SingletonBehaviour<FieldManager, MonoBehaviour>.instance.tee.transform.position + Vector3.up * Ball.BALL_ON_TEE_HEIGHT);
      this.trans.eulerAngles = new Vector3(70f * (float) global::Game.OffensiveFieldDirection, 0.0f, 0.0f);
    }
    else
    {
      this.trans.eulerAngles = Vector3.forward;
      this.SetPosition(new Vector3(MatchManager.instance.ballHashPosition + Field.SIX_INCHES * (float) global::Game.OffensiveFieldDirection, Ball.BALL_ON_GROUND_HEIGHT, ProEra.Game.MatchState.BallOn.Value));
    }
  }

  private IEnumerator<float> _MovingBall()
  {
    if (!((UnityEngine.Object) this.tossTarget == (UnityEngine.Object) null))
    {
      float distCollisionOccurs = 0.33f;
      EBallState curState = (EBallState) (Variable<EBallState>) Ball.State.BallState;
      float ballSpeed = 18.2f;
      Vector3 yOffset = Vector3.zero;
      Vector3 xOffset = Vector3.zero;
      if (PlayState.IsRun && (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.InAirToss)
        distCollisionOccurs = Field.ONE_YARD + Field.ONE_FOOT;
      else if (PlayState.IsPunt)
      {
        ballSpeed = 27.3f;
        distCollisionOccurs = 0.5f;
      }
      else if (global::Game.IsFG)
      {
        ballSpeed = 21.84f;
        distCollisionOccurs = 0.4f;
      }
      if (curState == EBallState.InAirSnap)
      {
        yOffset = Vector3.zero;
        this.SetRotation(0.0f);
      }
      float lastDistance = 100f;
      while (global::Game.IsPlayActive && (EBallState) (Variable<EBallState>) Ball.State.BallState == curState)
      {
        bool flag = false;
        if (curState == EBallState.InAirSnap)
          this.SetPosition(this.trans.position + (this.tossTarget.position + xOffset + yOffset - this.trans.position).normalized * Time.deltaTime * ballSpeed);
        float num = Vector3.Distance(this.trans.position, this.tossTarget.position);
        if ((EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.InAirToss && (double) lastDistance < (double) num)
          flag = true;
        lastDistance = num;
        if ((double) num < (double) distCollisionOccurs | flag)
        {
          AIUtil.DrawDebugCross(this.tossTarget.position, new Vector3(0.3f, 0.3f, 0.5f), Color.green, 3f);
          if (curState == EBallState.InAirSnap)
          {
            if (PlayState.IsPunt)
            {
              MatchManager.instance.playersManager.SetBallHolder(global::Game.Punter.mainGO, global::Game.Punter.onUserTeam);
              break;
            }
            if (global::Game.IsFG)
            {
              MatchManager.instance.playersManager.SetBallHolder(global::Game.Holder.mainGO, global::Game.Holder.onUserTeam);
              break;
            }
            MatchManager.instance.playersManager.SetBallHolder(global::Game.OffensiveQB.mainGO, global::Game.OffensiveQB.onUserTeam);
            break;
          }
          MatchManager.instance.playersManager.SetBallHolder(MatchManager.instance.playManager.handOffTargetIndex, global::Game.IsPlayerOneOnOffense);
          break;
        }
        Debug.DrawLine(this.trans.position, this.tossTarget.position, Color.white, 0.02f);
        yield return 0.0f;
      }
    }
  }

  private IEnumerator<float> ReleaseBall()
  {
    BallManager ballManager = this;
    yield return Timing.SwitchCoroutine(Segment.LateUpdate);
    if ((EBallState) (Variable<EBallState>) Ball.State.BallState != EBallState.PlayersHands && !global::Game.IsPlayInactive && ballManager.ballState != EBallState.InAirSnap)
    {
      ballManager.trans.rotation = Field.DEFENSE_TOWARDS_LOS_QUATERNION;
      if (PlayState.IsPass && ballManager.passTarget != Vector3.zero)
      {
        ballManager.SetVelocity(ballManager.BallisticVel(ballManager.passTarget, ballManager.passAngle));
        Quaternion quaternion = Quaternion.LookRotation(new Vector3(ballManager.passTarget.x, 0.0f, ballManager.passTarget.z) - ballManager.trans.position);
        ballManager.SetRotation(quaternion.eulerAngles.y);
        ballManager._ballObject.ActivateBallFlightRoutine(ballManager.transform.forward);
      }
      if (PlayState.IsRun && (UnityEngine.Object) ballManager.tossTarget != (UnityEngine.Object) null && (EBallState) (Variable<EBallState>) Ball.State.BallState == EBallState.InAirToss)
      {
        float num1 = 18.2f;
        Vector3 vector3 = Vector3.up * Field.ONE_YARD_SIX_INCHES;
        float num2 = Vector3.Distance(global::Game.OffensiveQB.trans.position, ballManager.tossTarget.position) * 1.165f;
        Vector3 forward = ballManager.tossTarget.position + ballManager.tossTarget.forward * num2 + vector3 - ballManager.trans.position;
        Vector3 v = forward.normalized * num1;
        ballManager.SetVelocity(v);
        Quaternion quaternion = Quaternion.LookRotation(forward);
        ballManager.SetRotation(quaternion.eulerAngles.y);
      }
      if ((PlayState.IsPuntOrKickoff || global::Game.IsFG) && ballManager.passTarget != Vector3.zero)
      {
        if (global::Game.IsFG)
          ballManager.SetPosition(ballManager.trans.position + Vector3.up * Field.ONE_YARD);
        ballManager.SetVelocity(ballManager.BallisticVel(ballManager.passTarget, ballManager.passAngle));
        Quaternion quaternion = Quaternion.LookRotation(new Vector3(ballManager.passTarget.x, 0.0f, ballManager.passTarget.z) - ballManager.trans.position);
        ballManager.SetRotation(quaternion.y);
        ballManager.SetAngularVelocity(ballManager.transform.right * -30f);
      }
      ballManager._ballObject.Release();
    }
  }

  public float GetForwardProgressLine() => !((UnityEngine.Object) this._forwardProgressTracker != (UnityEngine.Object) null) ? this.GetClosestPointToEndzone() : this._forwardProgressTracker.forwardProgressLine;

  public float GetClosestPointToEndzone()
  {
    int index1 = 0;
    for (int index2 = 1; index2 < this._ballEndPoints.childCount; ++index2)
    {
      if (Field.FurtherDownfield(this._ballEndPoints.GetChild(index2).position.z, this._ballEndPoints.GetChild(index1).position.z))
        index1 = index2;
    }
    return this._ballEndPoints.GetChild(index1).position.z;
  }

  public bool IsPastOffensiveGoalLine()
  {
    float offensiveGoalLine = Field.OFFENSIVE_GOAL_LINE;
    for (int index = 0; index < this._ballEndPoints.childCount; ++index)
    {
      if (Field.FurtherDownfield(this._ballEndPoints.GetChild(index).position.z, offensiveGoalLine))
        return true;
    }
    return false;
  }

  public bool WasDeflected => (double) MatchManager.instance.playManager.deflectedPassTimer > 0.0;

  public Vector3 GetClosestPointToPosition(Vector3 pos)
  {
    int index1 = 0;
    float num1 = float.PositiveInfinity;
    for (int index2 = 1; index2 < this._ballEndPoints.childCount; ++index2)
    {
      float num2 = Vector3.Distance(pos, this._ballEndPoints.GetChild(index2).position);
      if ((double) num2 < (double) num1)
      {
        index1 = index2;
        num1 = num2;
      }
    }
    return this._ballEndPoints.GetChild(index1).position;
  }

  public void DropBallAfterPlay(bool playOver)
  {
    if (!playOver || !((UnityEngine.Object) this.trans.parent != (UnityEngine.Object) null))
      return;
    Debug.Log((object) nameof (DropBallAfterPlay));
    this.SetParent((Transform) null);
    this.rigidbd.useGravity = true;
    this.rigidbd.detectCollisions = true;
    this.rigidbd.interpolation = RigidbodyInterpolation.Interpolate;
    this.rigidbd.constraints = RigidbodyConstraints.None;
    this.EnableCollider();
    this.SetDrag(1f);
  }

  private void SetDrag(float newDrag)
  {
    this.rigidbd.drag = newDrag;
    this.rigidbd.angularDrag = newDrag;
  }
}
