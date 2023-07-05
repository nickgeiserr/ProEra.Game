// Decompiled with JetBrains decompiler
// Type: BallPossessionAbility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public class BallPossessionAbility : MonoBehaviour
{
  public bool inBallPossession;
  [SerializeField]
  private BallPossessionAbility.BallPossessionPose centerPreSnapPose;
  [SerializeField]
  private BallPossessionAbility.BallPossessionPose fieldGoalHolderPose;
  [SerializeField]
  private BallPossessionAbility.BallPossessionPose _rightyCarrierCradlePose;
  [SerializeField]
  private BallPossessionAbility.BallPossessionPose _leftyCarrierCradlePose;
  [SerializeField]
  private BallPossessionAbility.BallPossessionPose _rightyQuarterbackPassingPose;
  [SerializeField]
  private BallPossessionAbility.BallPossessionPose _leftyQuarterbackPassingPose;
  [SerializeField]
  private BallPossessionAbility.BallPossessionPose _rightyQuarterbackReceiveSnapPose;
  [SerializeField]
  private BallPossessionAbility.BallPossessionPose _leftyQuarterbackReceiveSnapPose;
  private bool _handoffTimeReached;
  [SerializeField]
  [ReadOnly]
  private BallPossessionAbility.BallPossessionPose _currentPosessionPose;
  private PlayerAI _playerAI;
  private BallManager _ballManager;

  private BallPossessionAbility.BallPossessionPose CarrierCradlePose => this._playerAI.LeftHanded ? this._leftyCarrierCradlePose : this._rightyCarrierCradlePose;

  private BallPossessionAbility.BallPossessionPose QuarterbackPassingPose => this._playerAI.LeftHanded ? this._leftyQuarterbackPassingPose : this._rightyQuarterbackPassingPose;

  private BallPossessionAbility.BallPossessionPose QuarterbackReceiveSnapPose => this._playerAI.LeftHanded ? this._leftyQuarterbackReceiveSnapPose : this._rightyQuarterbackReceiveSnapPose;

  public void Awake() => PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);

  public void OnDestroy() => PEGameplayEventManager.OnEventOccurred -= new Action<PEGameplayEvent>(this.HandleGameEvent);

  public void Initiate(PlayerAI playerAI, BallManager ballManager)
  {
    this._playerAI = playerAI;
    this._ballManager = ballManager;
    this._handoffTimeReached = false;
  }

  private void SetCurrentPossessionPose(BallPossessionAbility.BallPossessionPose pose)
  {
    this._currentPosessionPose = pose;
    this._ballManager.SetParent(pose.ballParent);
    this._ballManager.trans.localPosition = pose.localPosition;
    this._ballManager.trans.localRotation = pose.localRotation;
  }

  private void HandleGameEvent(PEGameplayEvent e)
  {
    switch (e)
    {
      case PEHandoffTimeReachedEvent _:
        this._handoffTimeReached = true;
        break;
      case PEPlayOverEvent _:
        this._handoffTimeReached = false;
        break;
    }
  }

  private void Update()
  {
    if (!this.inBallPossession || Game.CurrentPlayHasUserQBOnField && Game.UserControlsQB && this._playerAI.IsQB())
      return;
    if (this._ballManager.ballState == EBallState.PlayersHands)
    {
      if (this._playerAI.IsQB())
      {
        if (this._playerAI.GetCurrentAssignId() == EPlayAssignmentId.OffPlayInitiation && !this._handoffTimeReached && !this._playerAI.LeftHanded)
          this.SetCurrentPossessionPose(this.QuarterbackReceiveSnapPose);
        else if (Game.IsFG)
          this.SetCurrentPossessionPose(this.fieldGoalHolderPose);
        else if (this._playerAI.animatorCommunicator.LocomotioStyle == ELocomotionStyle.QuaterbackStrafe)
          this.SetCurrentPossessionPose(this.QuarterbackPassingPose);
        else
          this.SetCurrentPossessionPose(this.CarrierCradlePose);
      }
      else
        this.SetCurrentPossessionPose(this.CarrierCradlePose);
    }
    else
    {
      if (this._ballManager.ballState != EBallState.InCentersHandsBeforeSnap)
        return;
      this.SetCurrentPossessionPose(this.centerPreSnapPose);
    }
  }

  [Serializable]
  public class BallPossessionPose
  {
    public Transform ballParent;
    public Vector3 localPosition;
    public Quaternion localRotation;
  }
}
