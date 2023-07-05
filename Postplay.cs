// Decompiled with JetBrains decompiler
// Type: Postplay
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Postplay : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  private PlayerAI ownerPlayerAI;
  private Color distColor = Color.white;
  private bool hasFinalGoal;
  private float timeInTask;
  private float postPlayDelayTime;
  private float huddleDelayTime;
  private float subDelayTime;
  private System.Action HuddleReached;
  private System.Action SidelineReached;
  private bool IsAtHuddle;
  private bool IsAtSideline;
  private bool isBeingSubbed;
  private bool startedPostPlay;

  public override void OnStart()
  {
    this.HuddleReached += new System.Action(this.OnHuddleReached);
    this.SidelineReached += new System.Action(this.OnSidelineReached);
    if (!(bool) (UnityEngine.Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.postPlayDelayTime = UnityEngine.Random.Range(0.1f, 0.5f);
    this.subDelayTime = UnityEngine.Random.Range(0.0f, 1f);
    this.huddleDelayTime = 0.0f;
    this.timeInTask = 0.0f;
    this.startedPostPlay = false;
    this.IsAtHuddle = false;
    this.IsAtSideline = false;
    this.ownerPlayerAI.animatorCommunicator.SetStance(0);
    this.ownerPlayerAI.animatorCommunicator.atHuddlePosition = false;
    this.ownerPlayerAI.animatorCommunicator.atPlayPosition = false;
    this.ownerPlayerAI.hasShiftedStartingPosition = false;
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayerAI || !Game.IsPlayOver)
      return TaskStatus.Failure;
    if (this.ownerPlayerAI.animatorCommunicator.atHuddlePosition)
    {
      this.IsAtHuddle = true;
      this.ownerPlayerAI.animatorCommunicator.Stop();
    }
    if (!this.IsAtHuddle && !this.IsAtSideline)
    {
      this.timeInTask += this.ownerPlayerAI.AITimingInterval;
      if (!this.startedPostPlay && (double) this.timeInTask >= (double) this.postPlayDelayTime)
      {
        this.startedPostPlay = true;
        this.ownerPlayerAI.PlayerBlockAbility.ExitBlock();
        this.ownerPlayerAI.DisableColliders();
        this.ownerPlayerAI.hasShiftedStartingPosition = false;
        this.ownerPlayerAI.animatorCommunicator.atHuddlePosition = false;
        this.ownerPlayerAI.animatorCommunicator.atPlayPosition = false;
        this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.Regular);
        this.hasFinalGoal = false;
        this.timeInTask = 0.0f;
        this.IsAtHuddle = false;
        this.IsAtSideline = false;
        this.isBeingSubbed = false;
      }
      if (this.startedPostPlay && (!this.hasFinalGoal || this.ownerPlayerAI.isBeingSubbed != this.isBeingSubbed) && (this.ownerPlayerAI.iAnimQuerries.IsInInactivePlayLocomotion() || this.ownerPlayerAI.iAnimQuerries.IsInLocomotion() || this.ownerPlayerAI.iAnimQuerries.IsInIdle()))
      {
        if (this.ownerPlayerAI.isBeingSubbed)
        {
          if (!this.isBeingSubbed && this.hasFinalGoal && (double) this.timeInTask >= (double) this.subDelayTime + 1.0)
            this.timeInTask = 0.0f;
          if ((double) this.timeInTask >= (double) this.subDelayTime)
          {
            float num = (Game.PostPlayConfig.DefaultOobYardsFromSideline + UnityEngine.Random.Range(Game.PostPlayConfig.minYardsFromOOBTargetToStop, Game.PostPlayConfig.maxYardsFromOOBTargetToStop)) * Field.ONE_YARD;
            Vector3 position = new Vector3(PersistentData.userIsHome && this.ownerPlayerAI.onUserTeam || !PersistentData.userIsHome && !this.ownerPlayerAI.onUserTeam ? Field.OUT_OF_BOUNDS + num : -Field.OUT_OF_BOUNDS - num, 0.0f, Mathf.Clamp(UnityEngine.Random.Range(this.ownerPlayerAI.trans.position.z - Field.TEN_YARDS, this.ownerPlayerAI.trans.position.z + Field.TEN_YARDS), Field.THIRTY_YARD_LINE, -Field.THIRTY_YARD_LINE));
            this.isBeingSubbed = true;
            float effortCeiling01 = UnityEngine.Random.Range(0.65f, 0.8f);
            this.ownerPlayerAI.animatorCommunicator.SetGoal(position, effortCeiling01);
          }
          this.hasFinalGoal = true;
        }
        else if (!this.ownerPlayerAI.animatorCommunicator.atHuddlePosition)
        {
          Vector3 huddlePosition = this.ownerPlayerAI.GetHuddlePosition();
          Vector3 position = this.ownerPlayerAI.trans.position;
          if (this.ownerPlayerAI.onUserTeam)
          {
            PlayerAI playerToAvoid = this.ownerPlayerAI.FindPlayerToAvoid(Game.PostPlayConfig.maxDistanceFromPlayerForAvoidance, Game.PostPlayConfig.forwardVecToPlayerVecDotThreshold, bStopOnFirstFound: true, bUseCullDist: true);
            Vector3 forward = huddlePosition - position;
            if ((UnityEngine.Object) playerToAvoid != (UnityEngine.Object) null)
            {
              if (!this.ownerPlayerAI.animatorCommunicator.IsInIdle())
              {
                this.ownerPlayerAI.animatorCommunicator.Stop();
                this.HuddleReached();
                this.hasFinalGoal = true;
              }
              else
                this.hasFinalGoal = false;
            }
            else if ((double) forward.sqrMagnitude > 2.25)
              this.ownerPlayerAI.animatorCommunicator.SetGoalDirection((Quaternion.LookRotation(forward, Vector3.up) * Vector3.forward * Field.FIVE_YARDS).normalized, 0.3f);
            if ((double) Vector3.Distance(huddlePosition, position) < (double) Game.PostPlayConfig.maxTargetDistanceToStop)
            {
              this.ownerPlayerAI.animatorCommunicator.Stop();
              this.HuddleReached();
              this.hasFinalGoal = true;
            }
          }
          else
          {
            float num = Vector3.Distance(huddlePosition, position);
            this.ownerPlayerAI.animatorCommunicator.SetGoal(huddlePosition, (double) num < (double) Game.PostPlayConfig.MaxDistanceFromTargetToWalk ? 0.3f : 0.65f, this.ownerPlayerAI.GetHuddleRotation(), this.HuddleReached);
            this.hasFinalGoal = true;
          }
        }
      }
    }
    else
      this.ownerPlayerAI.animatorCommunicator.Stop();
    return TaskStatus.Running;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    this.HuddleReached -= new System.Action(this.OnHuddleReached);
    this.SidelineReached -= new System.Action(this.OnSidelineReached);
  }

  private void OnHuddleReached()
  {
    this.ownerPlayerAI.PlayerInHuddle();
    this.IsAtHuddle = true;
  }

  private void OnSidelineReached() => this.IsAtSideline = true;
}
