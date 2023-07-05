// Decompiled with JetBrains decompiler
// Type: PassBlock
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class PassBlock : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  private PlayerAI ownerPlayerAI;
  private PassBlockAssignment playerRoute;
  private List<Vector3> actualRoutePoints;
  private int currentLeg;
  private BlockStage currentBlockStage;
  private PlayerAI blockTargetPlayer;
  private Vector3 playerPos;
  private Vector3 QBPos;
  private float[] defenderBlockScores = new float[11];
  private bool movingToInterceptTarget;
  private bool bShowBlockDebug = true;
  private const float DROPBACKFACEANGDELTA = 11.25f;
  private const float PassDropBackBlockTargetAngle = 45f;
  private const float PassBlockTargetAngle = 90f;
  private const float IntercepDefPosToQB = 0.75f;
  private const float PassBlockSpeedUp = 1.3f;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.playerRoute = this.ownerPlayerAI.CurrentPlayAssignment as PassBlockAssignment;
    this.actualRoutePoints = new List<Vector3>();
    this.ownerPlayerAI.animatorCommunicator.isRunningRoute = false;
    this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.BlockingStrafe);
    this.ownerPlayerAI.lookForBlockTarget = true;
    this.ownerPlayerAI.blockType = BlockType.PassBlockBegin;
    this.ownerPlayerAI.blockTargetScore = -1000f;
    this.movingToInterceptTarget = false;
    if (this.playerRoute != null)
    {
      this.ownerPlayerAI.SetPlayerRouteForAssignment((RunPathAssignment) this.playerRoute, this.actualRoutePoints, 1f, Quaternion.Euler(0.0f, Mathf.Clamp((float) (this.ownerPlayerAI.indexInFormation - 2) * 11.25f, -30f, 30f), 0.0f) * Field.OFFENSE_TOWARDS_LOS_QUATERNION, false);
      this.currentLeg = 0;
      this.currentBlockStage = BlockStage.Dropback;
      this.ownerPlayerAI.animatorCommunicator.speed = 1.3f;
    }
    else
      this.ownerPlayerAI.animatorCommunicator.Stop();
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI || this.playerRoute == null || this.ownerPlayerAI.GetCurrentAssignId() != EPlayAssignmentId.PassBlock || global::Game.BallIsThrownOrKicked || global::Game.BS_IsOnGround || global::Game.IsOnsidesKick && global::Game.BallIsThrownOrKicked || global::Game.IsTurnover || global::Game.BS_IsInAirDeflected)
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
        return TaskStatus.Success;
      }
    }
    else
    {
      this.playerPos = this.ownerPlayerAI.trans.position;
      this.QBPos = MatchManager.instance.playersManager.GetCurrentQB().trans.position;
      if (this.ownerPlayerAI.lookForBlockTarget)
        this.GetPassBlockTarget();
      if ((Object) this.ownerPlayerAI.blockTarget != (Object) null)
      {
        this.blockTargetPlayer = this.ownerPlayerAI.blockTarget;
        Vector3 position1 = this.blockTargetPlayer.trans.position;
        float num1 = Vector3.Distance(this.playerPos, position1);
        float num2 = Vector3.Distance(this.QBPos, position1);
        Vector3 vector3 = this.QBPos - position1;
        float num3 = Vector3.Distance(this.QBPos, this.playerPos);
        Vector3 position2 = position1 + vector3 * 0.75f;
        Vector3 b = position1 + this.blockTargetPlayer.animatorCommunicator.velocity * this.ownerPlayerAI.AITimingInterval * 2f;
        float num4 = Vector3.Distance(this.QBPos, b);
        if (this.currentBlockStage == BlockStage.Dropback)
        {
          this.ownerPlayerAI.blockType = BlockType.PassBlockBegin;
          if (this.ownerPlayerAI.animatorCommunicator.GoalReached)
            ++this.currentLeg;
          if (this.ownerPlayerAI.animatorCommunicator.atFinalGoal)
            this.currentBlockStage = BlockStage.MaintainPocket;
        }
        else if (this.currentBlockStage == BlockStage.MaintainPocket)
        {
          this.ownerPlayerAI.blockType = BlockType.PassBlockNormal;
          if ((double) num1 < (double) PlayerAI.PASS_BLOCK_LOOKFORBLOCK_DIST_AT_DEPTH * (double) Field.ONE_YARD || Field.FurtherDownfield(this.QBPos.z, b.z) || (double) num2 <= (double) num3 || (double) num4 <= (double) num3)
          {
            this.movingToInterceptTarget = true;
            this.ownerPlayerAI.animatorCommunicator.SetGoal(position2, 1f);
          }
        }
      }
      else if (this.currentBlockStage == BlockStage.MaintainPocket && (double) Vector3.Distance(this.playerPos, this.actualRoutePoints[this.actualRoutePoints.Count - 1]) > (double) Field.ONE_YARD)
        this.ownerPlayerAI.animatorCommunicator.SetGoal(this.actualRoutePoints[this.actualRoutePoints.Count - 1], 0.3f, Field.OFFENSE_TOWARDS_LOS_QUATERNION);
    }
    return TaskStatus.Running;
  }

  public override void OnEnd()
  {
    base.OnEnd();
    if ((Object) this.ownerPlayerAI != (Object) null)
      this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.Regular);
    this.ownerPlayerAI.EndCurrentAssignment();
    this.ownerPlayerAI.animatorCommunicator.speed = 1f;
  }

  public void GetPassBlockTarget()
  {
    Vector3 position = this.ownerPlayerAI.trans.position;
    double blockTargetAngle1 = (double) global::Game.PassBlockConfig.maxPassBlockTargetAngle;
    if (this.currentBlockStage == BlockStage.Dropback)
    {
      double blockTargetAngle2 = (double) global::Game.PassBlockConfig.maxPassDropBackBlockTargetAngle;
    }
    List<PlayerAI> defensivePlayers = global::Game.DefensivePlayers;
    float a = global::Game.PassBlockConfig.maxDistToBlockTarget;
    if (!this.ownerPlayerAI.IsLineman() && Field.FurtherDownfield((float) ProEra.Game.MatchState.BallOn, position.z))
    {
      a = global::Game.PassBlockConfig.maxDistToBlockTargetBlockingBack;
      double blockTargetAngle3 = (double) global::Game.PassBlockConfig.maxPassBlockTargetAngle;
    }
    PlayerAI playerAi1 = this.ownerPlayerAI.blockTarget;
    float num1 = -1000f;
    for (int index = 0; index < 11; ++index)
    {
      PlayerAI playerAi2 = defensivePlayers[index];
      float num2 = Vector3.Distance(position, playerAi2.trans.position);
      if ((Object) playerAi2 == (Object) null || playerAi2.isEngagedInBlock || (Object) playerAi2.mainGO == (Object) this.ownerPlayerAI.inBlockWith || playerAi2.isTackling || (double) num2 > (double) a)
      {
        this.defenderBlockScores[index] = -1000f;
      }
      else
      {
        Vector3 b = playerAi2.trans.position + playerAi2.animatorCommunicator.velocity * this.ownerPlayerAI.AITimingInterval;
        double num3 = (double) Vector3.Distance(position, b);
        double num4 = (double) Vector3.Angle(this.ownerPlayerAI.trans.forward, playerAi2.trans.position - position);
        float num5 = Mathf.Abs(position.x - playerAi2.trans.position.x);
        float num6 = Mathf.Abs(position.x - b.x);
        float num7 = Mathf.InverseLerp(a, 0.0f, num2) * global::Game.PassBlockConfig.distScoreWeight;
        float num8 = Mathf.InverseLerp(global::Game.PassBlockConfig.maxXDistToBlockTarget, 0.0f, num5) * global::Game.PassBlockConfig.xDistScoreWeight;
        float num9 = Mathf.InverseLerp(global::Game.PassBlockConfig.maxXDistToBlockTarget, 0.0f, num6) * global::Game.PassBlockConfig.xDistPredictedScoreWeight;
        float num10 = Field.FurtherDownfield((float) ProEra.Game.MatchState.BallOn, playerAi2.trans.position.z) ? global::Game.PassBlockConfig.defPosCrossLosScore : 0.0f;
        float num11 = Field.FurtherDownfield((float) ProEra.Game.MatchState.BallOn, b.z) ? global::Game.PassBlockConfig.defPredPosCrossLosScore : 0.0f;
        float num12 = 0.0f;
        float num13;
        if ((double) num11 > 0.0 && (double) Mathf.Abs(b.x - MatchManager.instance.ballHashPosition) > (double) Field.TWO_YARDS)
        {
          if (Field.MoreLeftOf(this.ownerPlayerAI.GetPlayStartPosition().x, MatchManager.instance.ballHashPosition - Field.TWO_YARDS * (float) global::Game.OffensiveFieldDirection))
          {
            if (Field.MoreLeftOf(b.x, position.x))
              num13 = num12 + global::Game.PassBlockConfig.defEdgeRushScore;
          }
          else if (Field.MoreRightOf(this.ownerPlayerAI.GetPlayStartPosition().x, MatchManager.instance.ballHashPosition + Field.TWO_YARDS * (float) global::Game.OffensiveFieldDirection) && Field.MoreRightOf(b.x, position.x))
            num13 = num12 + global::Game.PassBlockConfig.defEdgeRushScore;
        }
        this.defenderBlockScores[index] = num7 + num8 + num9 + num10 + num11;
        float num14 = (Object) playerAi2.blockerAssignedToThisDefender != (Object) null ? this.defenderBlockScores[index] - playerAi2.blockerAssignedToThisDefender.blockTargetScore : global::Game.PassBlockConfig.minScoreDifForTargetSwitch + 1f;
        if ((double) this.defenderBlockScores[index] > (double) num1 && (double) num14 > (double) global::Game.PassBlockConfig.minScoreDifForTargetSwitch)
        {
          playerAi1 = playerAi2;
          num1 = this.defenderBlockScores[index];
        }
      }
    }
    if (!((Object) playerAi1 != (Object) null))
      return;
    if ((Object) playerAi1.blockerAssignedToThisDefender != (Object) null && (Object) playerAi1.blockerAssignedToThisDefender != (Object) this.ownerPlayerAI)
    {
      playerAi1.blockerAssignedToThisDefender.blockTarget = (PlayerAI) null;
      playerAi1.blockerAssignedToThisDefender.blockTargetScore = -1000f;
    }
    this.ownerPlayerAI.blockTarget = playerAi1;
    this.ownerPlayerAI.blockTargetScore = num1;
    this.ownerPlayerAI.blockTarget.distanceToBlocker = Vector3.Distance(this.ownerPlayerAI.trans.position, playerAi1.trans.position);
    this.ownerPlayerAI.blockTarget.blockerAssignedToThisDefender = this.ownerPlayerAI;
  }

  public override void OnDrawGizmos()
  {
  }
}
