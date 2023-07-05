// Decompiled with JetBrains decompiler
// Type: RunBlock
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using ProEra.Game;
using System.Collections.Generic;
using UnityEngine;

public class RunBlock : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  private PlayerAI ownerPlayerAI;
  private RunBlockAssignment rbAssign;
  private List<Vector3> actualRoutePoints;
  private PlayerAI blockTargetPlayer;
  private Vector3 playerPos;
  private PlayersManager pm;
  private MatchManager matchManager;
  private Transform trans;
  private AnimatorCommunicator animCom;
  private float targetUpdateTime;
  private bool reachedInitBlockTarget;
  private bool completedBlockPath;
  private const float TARGETDURATION = 0.25f;

  private event System.Action OnInitBlockTargetReached;

  public override void OnStart()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.OnInitBlockTargetReached += new System.Action(this.HandleInitBlockTargetReached);
    this.rbAssign = this.ownerPlayerAI.CurrentPlayAssignment as RunBlockAssignment;
    this.actualRoutePoints = new List<Vector3>();
    this.ownerPlayerAI.animatorCommunicator.isRunningRoute = false;
    this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.Regular);
    this.ownerPlayerAI.lookForBlockTarget = true;
    this.targetUpdateTime = 0.0f;
    this.matchManager = MatchManager.instance;
    this.pm = this.matchManager.playersManager;
    this.animCom = this.ownerPlayerAI.animatorCommunicator;
    this.trans = this.ownerPlayerAI.trans;
    this.animCom.SetLocomotionStyle(ELocomotionStyle.Regular);
    if (this.rbAssign != null)
      this.ownerPlayerAI.SetPlayerRouteForAssignment((RunPathAssignment) this.rbAssign, this.actualRoutePoints, 1f, Field.OFFENSE_TOWARDS_LOS_QUATERNION, false);
    else
      this.ownerPlayerAI.animatorCommunicator.SetGoalDirection(Vector3.forward * (float) global::Game.OffensiveFieldDirection);
    this.reachedInitBlockTarget = false;
    this.completedBlockPath = false;
    this.DebugDrawRoute();
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayerAI || this.rbAssign == null || this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.RunBlock || PlayState.IsPass && global::Game.BallIsThrownOrKicked || global::Game.BS_IsOnGround || global::Game.IsOnsidesKick && global::Game.BallIsThrownOrKicked || global::Game.IsTurnover || global::Game.BS_IsInAirDeflected)
      return TaskStatus.Failure;
    if (global::Game.BS_IsInAirPass || global::Game.BS_IsOnGround)
      return TaskStatus.Success;
    if (this.ownerPlayerAI.isEngagedInBlock)
    {
      this.ownerPlayerAI.blockDuration -= this.ownerPlayerAI.AITimingInterval;
      this.ownerPlayerAI.inBlockWithScript.CheckForBlockDisengage();
      if ((double) this.ownerPlayerAI.blockDuration <= 0.0)
      {
        this.ownerPlayerAI.PlayerBlockAbility.ExitBlock();
        this.HandleInitBlockTargetReached();
      }
    }
    if (this.rbAssign != null && this.ownerPlayerAI.animatorCommunicator.atFinalGoal)
    {
      this.completedBlockPath = true;
      this.ownerPlayerAI.lookForBlockTarget = true;
    }
    this.targetUpdateTime -= this.ownerPlayerAI.AITimingInterval;
    if ((double) this.targetUpdateTime < 0.0)
    {
      this.targetUpdateTime = 0.25f;
      Vector3 position = this.trans.position;
      if ((UnityEngine.Object) this.ownerPlayerAI.initialBlockTarget != (UnityEngine.Object) null && this.reachedInitBlockTarget)
        this.animCom.SetGoal(this.ownerPlayerAI.GetPlayerPursuitAngle(this.ownerPlayerAI.initialBlockTarget), this.OnInitBlockTargetReached);
      else if (this.ownerPlayerAI.lookForBlockTarget)
      {
        this.ownerPlayerAI.FindBlockTarget();
        if ((UnityEngine.Object) this.ownerPlayerAI.blockTarget != (UnityEngine.Object) null)
          this.ownerPlayerAI.animatorCommunicator.SetGoal(this.ownerPlayerAI.GetPlayerPursuitAngle(this.ownerPlayerAI.blockTarget));
        else if (this.ownerPlayerAI.blockType == BlockType.MoveToBallHolder && (UnityEngine.Object) this.pm.ballHolder != (UnityEngine.Object) null)
        {
          if (Field.FurtherDownfield(this.ownerPlayerAI.trans.position.z, this.pm.ballHolderScript.trans.position.z))
          {
            this.ownerPlayerAI.animatorCommunicator.SetGoalDirection(Vector3.forward * (float) global::Game.OffensiveFieldDirection, 0.65f);
          }
          else
          {
            Vector3 vector3 = this.pm.ballHolderScript.trans.position + this.pm.ballHolderScript.trans.forward * Field.THREE_YARDS + new Vector3(0.0f, 0.0f, Field.ONE_YARD_FORWARD * Field.THREE_YARDS);
            float num = Mathf.Sign(this.ownerPlayerAI.trans.position.x - this.pm.ballHolderScript.trans.position.x);
            vector3.x = this.pm.ballHolderScript.trans.position.x + Field.TWO_YARDS * num;
            vector3 = PlayerAI.ClampMoveToFieldBounds(vector3);
            this.animCom.SetGoal(vector3);
            Debug.DrawLine(this.trans.position, vector3, Color.white, 0.25f);
            AIUtil.DrawDebugCross(vector3, new Vector3(0.5f, 0.5f, 0.5f), Color.white, 0.25f);
          }
        }
        else
          this.ownerPlayerAI.animatorCommunicator.SetGoalDirection(Vector3.forward * (float) global::Game.OffensiveFieldDirection, 0.8f);
      }
      else
        this.ownerPlayerAI.animatorCommunicator.SetGoalDirection(Vector3.forward * (float) global::Game.OffensiveFieldDirection, 0.65f);
    }
    return TaskStatus.Running;
  }

  private void HandleInitBlockTargetReached()
  {
    this.reachedInitBlockTarget = true;
    this.ownerPlayerAI.lookForBlockTarget = true;
    this.ownerPlayerAI.blockType = BlockType.MoveToBallHolder;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    if (!((UnityEngine.Object) this.ownerPlayerAI != (UnityEngine.Object) null))
      return;
    this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.Regular);
    this.ownerPlayerAI.EndCurrentAssignment();
    this.ownerPlayerAI.animatorCommunicator.speed = 1f;
    this.OnInitBlockTargetReached -= new System.Action(this.HandleInitBlockTargetReached);
  }

  private void DebugDrawRoute()
  {
    Vector3 start = this.ownerPlayerAI.trans.position;
    foreach (Vector3 actualRoutePoint in this.actualRoutePoints)
    {
      Debug.DrawLine(start, actualRoutePoint, Color.red, 8f);
      start = actualRoutePoint;
    }
  }
}
