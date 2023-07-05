// Decompiled with JetBrains decompiler
// Type: AnimatorCommunicator
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class AnimatorCommunicator : LocomotionAgentController, IAnimatorQuerries
{
  private Dictionary<int, List<int>> axisToProEraStances = new Dictionary<int, List<int>>()
  {
    {
      -1,
      new List<int>() { 0 }
    },
    {
      0,
      new List<int>() { 13, 14, 1, 2 }
    },
    {
      1,
      new List<int>() { 3 }
    },
    {
      2,
      new List<int>() { 9 }
    },
    {
      3,
      new List<int>() { 8 }
    },
    {
      4,
      new List<int>() { 5, 12 }
    },
    {
      5,
      new List<int>() { 11 }
    },
    {
      6,
      new List<int>() { 6 }
    },
    {
      7,
      new List<int>() { 4 }
    },
    {
      8,
      new List<int>() { 10 }
    },
    {
      9,
      new List<int>() { 8 }
    },
    {
      10,
      new List<int>() { 8 }
    },
    {
      11,
      new List<int>() { 8 }
    },
    {
      12,
      new List<int>() { 8 }
    },
    {
      13,
      new List<int>() { 7 }
    },
    {
      14,
      new List<int>() { 15 }
    }
  };
  [SerializeField]
  private Rigidbody _rigidbody;
  [SerializeField]
  private AnimationEventAgent _eventAgent;
  [SerializeField]
  private NteractAgent _nteractAgent;
  public bool atPlayPosition;
  private bool _onOffense;

  private bool IsAnimatedByAuxiliarySystem => this._nteractAgent.IsInsideInteraction || this._eventAgent.IsInsideEvent;

  public Vector3 velocity => this._animator.velocity;

  public float speed
  {
    get => this._animator.speed;
    set => this._animator.speed = value;
  }

  public bool applyRootMotion
  {
    get => this._animator.applyRootMotion;
    set => this._animator.applyRootMotion = value;
  }

  public float CurrentEffort => this._animator.GetFloat(LocomotionAgentController.HashIDs.currentEffortFloat);

  public bool atHuddlePosition
  {
    get => this._animator.GetBool(LocomotionAgentController.HashIDs.atHuddlePositionBool);
    set => this.SetBoolParameter(LocomotionAgentController.HashIDs.atHuddlePositionBool, value);
  }

  public int celebrationType
  {
    set => this.SetIntParameter(LocomotionAgentController.HashIDs.celebrationCategoryInt, value);
  }

  public int randomCelebrationIndex
  {
    set => this.SetIntParameter(LocomotionAgentController.HashIDs.randomCelebrationIndex, value);
  }

  public float randomCelebrationOffset
  {
    set => this.SetFloatParameter(LocomotionAgentController.HashIDs.randomCelebrationOffset, value);
  }

  public bool mirrorCelebration
  {
    set => this.SetBoolParameter(LocomotionAgentController.HashIDs.mirrorCelebration, value);
  }

  public bool onOffense
  {
    set
    {
      this._onOffense = value;
      this.SetBoolParameter(LocomotionAgentController.HashIDs.onOffenseBool, value);
    }
  }

  public bool hasBall
  {
    get => this._animator.GetBool(LocomotionAgentController.HashIDs.hasBallBool);
    set => this.SetBoolParameter(LocomotionAgentController.HashIDs.hasBallBool, value);
  }

  public int indexInFormation
  {
    set => this.SetIntParameter(LocomotionAgentController.HashIDs.indexInFormation, value);
  }

  public bool isPlayActive
  {
    get => this._animator.GetBool(LocomotionAgentController.HashIDs.isPlayActive);
    set => this.SetBoolParameter(LocomotionAgentController.HashIDs.isPlayActive, value);
  }

  public bool isRunningRoute
  {
    get => this._animator.GetBool(LocomotionAgentController.HashIDs.isRunningRouteBool);
    set => this.SetBoolParameter(LocomotionAgentController.HashIDs.isRunningRouteBool, value);
  }

  public void SetStance(PlayerAI ownerPlayerAI, int stanceId)
  {
    switch (stanceId)
    {
      case 0:
        stanceId = !ownerPlayerAI.onOffense ? this.axisToProEraStances[stanceId][0] : (!ownerPlayerAI.IsCenter() ? this.axisToProEraStances[stanceId][1] : (!Game.IsPunt ? this.axisToProEraStances[stanceId][2] : this.axisToProEraStances[stanceId][3]));
        break;
      case 4:
        stanceId = !Game.IsFG ? this.axisToProEraStances[stanceId][0] : this.axisToProEraStances[stanceId][1];
        break;
      default:
        stanceId = this.axisToProEraStances[stanceId][0];
        break;
    }
    if (Field.IsObjectOutOfBounds(ownerPlayerAI.trans.position))
      stanceId = 0;
    this.SetStance(stanceId);
  }

  public bool IsQBInPocket() => this._animator.GetInteger(LocomotionAgentController.HashIDs.stanceInt) == 3 && Game.IsPass && !Game.IsPlayAction && Game.BallIsNotThrownOrKicked;

  public bool IsQBMovingInPocket() => (double) this._animator.GetFloat(LocomotionAgentController.HashIDs.directStrafeEffortFloat) != 0.0 || (double) this._animator.GetFloat(LocomotionAgentController.HashIDs.lateralStrafeEffortFloat) != 0.0;

  public bool IsQBInPocketIdle() => this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.qbHasBallIdleState && (double) Mathf.Abs(this._animator.GetFloat(LocomotionAgentController.HashIDs.directStrafeEffortFloat)) < (double) PlayerAnimTuning.MinLateralMovementSpeed && (double) Mathf.Abs(this._animator.GetFloat(LocomotionAgentController.HashIDs.lateralStrafeEffortFloat)) < (double) PlayerAnimTuning.MinForwardMovementSpeed;

  public bool IsSprinting() => (double) this._animator.velocity.magnitude > 5.0;

  public bool IsStrafing() => this._animator.GetBool(LocomotionAgentController.HashIDs.isStrafingBool);

  public bool AllowPivot() => this._animator.GetBool(LocomotionAgentController.HashIDs.allowPivotBool);

  public bool IsInLocomotion() => (double) this._animator.velocity.magnitude > 1.0;

  public bool IsInInactivePlayLocomotion() => this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.inactivePlayLocomotion;

  public bool IsInDefenderStrafeIdle() => this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.defenderStrafeState && (double) Mathf.Abs(this._animator.GetFloat(LocomotionAgentController.HashIDs.lateralStrafeEffortFloat)) < (double) PlayerAnimTuning.MinLateralMovementSpeed && (double) Mathf.Abs(this._animator.GetFloat(LocomotionAgentController.HashIDs.directStrafeEffortFloat)) < (double) PlayerAnimTuning.MinForwardMovementSpeed;

  public bool IsInIdle() => (double) this._animator.velocity.magnitude < 0.25;

  public bool IsInPivot() => this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.pivotLeft135State || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.pivotRight135State || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.pivotLeft90State || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.pivotRight90State || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.pivotLeft45State || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.pivotRight45State || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.idleToRunLeftState || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.idleToRunRightState;

  public bool IsInPassBlockDropback() => this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.passBlockLoop_LT || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.passBlockLoop_LG || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.passBlockLoop_RG || this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.passBlockLoop_RT;

  public bool IsInQBUnderCenter()
  {
    int num1 = this._animator.GetInteger(LocomotionAgentController.HashIDs.stanceInt) == 3 ? 1 : 0;
    bool flag1 = Mathf.Approximately(0.0f, this._animator.GetFloat(LocomotionAgentController.HashIDs.goalEffortFloat));
    bool flag2 = (double) this._animator.velocity.magnitude < 0.10000000149011612;
    int num2 = flag1 ? 1 : 0;
    return (num1 & num2 & (flag2 ? 1 : 0)) != 0;
  }

  public bool IsInQBShotgunIdle()
  {
    int num1 = this._animator.GetInteger(LocomotionAgentController.HashIDs.stanceInt) == 4 ? 1 : 0;
    bool flag1 = Mathf.Approximately(0.0f, this._animator.GetFloat(LocomotionAgentController.HashIDs.goalEffortFloat));
    bool flag2 = (double) this._animator.velocity.magnitude < 0.10000000149011612;
    int num2 = flag1 ? 1 : 0;
    return (num1 & num2 & (flag2 ? 1 : 0)) != 0;
  }

  public bool IsQBReadyForSnap() => this.IsInQBUnderCenter() || this.IsInQBShotgunIdle();

  public bool IsInQBIdle() => this._animator.GetCurrentAnimatorStateInfo(0).fullPathHash == LocomotionAgentController.HashIDs.qbHasBallIdleState;

  public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex) => this._animator.GetCurrentAnimatorStateInfo(layerIndex);

  public int BodyType
  {
    get => this._animator.GetInteger(LocomotionAgentController.HashIDs.bodyType);
    set => this.SetIntParameter(LocomotionAgentController.HashIDs.bodyType, value);
  }

  public bool IsPrePlay
  {
    get => this._animator.GetBool(LocomotionAgentController.HashIDs.isPrePlay);
    set => this.SetBoolParameter(LocomotionAgentController.HashIDs.isPrePlay, value);
  }

  public bool IsLeftSideOfBall
  {
    get => this._animator.GetBool(LocomotionAgentController.HashIDs.leftSideOfBallBool);
    set => this.SetBoolParameter(LocomotionAgentController.HashIDs.leftSideOfBallBool, value);
  }

  public override void Reset(Vector3 position, Quaternion rotation, bool skipReset = false)
  {
    if (this._nteractAgent.IsInsideInteraction)
    {
      this._nteractAgent.ExitInteraction();
      this._nteractAgent.Blender.Reset();
    }
    if (this._eventAgent.IsInsideEvent)
    {
      this._eventAgent.ExitEvent();
      this._eventAgent.Blender.Reset();
    }
    this._rigidbody.velocity = Vector3.zero;
    this._rigidbody.angularVelocity = Vector3.zero;
    base.Reset(position, rotation, skipReset);
  }

  protected override void Update()
  {
    bool flag = Time.frameCount % 2 == 0;
    if (this._onOffense & flag || !this._onOffense && !flag)
      base.Update();
    this.SetBoolParameter(LocomotionAgentController.HashIDs.isAnimatedByAuxiliarySystem, this.IsAnimatedByAuxiliarySystem);
  }
}
