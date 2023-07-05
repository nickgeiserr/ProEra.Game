// Decompiled with JetBrains decompiler
// Type: BallPhysicsSimulation
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vars;

public class BallPhysicsSimulation : MonoBehaviour
{
  private Rigidbody _ballRigidbody;
  private static PositionAnimationCurves _simulatedTrajectory = new PositionAnimationCurves();
  private static float _simulatedTrajectoryLastTime;
  [SerializeField]
  private static float _minimumLookAheadTime = 0.5f;
  private static Rigidbody _cachedDummySimulatedRigidbody;

  public static Vector3 EvaluatePosition(float time) => BallPhysicsSimulation._simulatedTrajectory.Evaluate(time);

  public static bool GetLastTimeAtHeight(
    Rigidbody ballRigidBody,
    float targetHeight,
    out float timeAtHeight)
  {
    AnimationCurve curveY = BallPhysicsSimulation._simulatedTrajectory.curveY;
    if (curveY.length == 0 || (double) curveY[curveY.length - 1].value > (double) targetHeight)
    {
      BallPhysicsSimulation.SimulateBallTrajectory((EBallState) (Variable<EBallState>) Ball.State.BallState, ballRigidBody, (PhysicsSimulation.SimulationEndCondition) ((rigidBody, _) => (double) rigidBody.transform.position.y < (double) targetHeight && (double) rigidBody.velocity.y <= 0.0));
      curveY = BallPhysicsSimulation._simulatedTrajectory.curveY;
    }
    bool flag = false;
    Keyframe keyframe1 = new Keyframe();
    Keyframe keyframe2 = new Keyframe();
    for (int index = curveY.length - 2; index > 0; --index)
    {
      Keyframe keyframe3 = curveY[index];
      Keyframe keyframe4 = curveY[index + 1];
      if ((double) keyframe3.value >= (double) targetHeight != (double) keyframe4.value >= (double) targetHeight)
      {
        flag = true;
        keyframe1 = (double) keyframe3.value > (double) keyframe4.value ? keyframe3 : keyframe4;
        keyframe2 = (double) keyframe3.value < (double) keyframe4.value ? keyframe3 : keyframe4;
        break;
      }
    }
    if (!flag)
    {
      timeAtHeight = -1f;
      return false;
    }
    float num = (float) (((double) targetHeight - (double) keyframe2.value) / ((double) keyframe1.value - (double) keyframe2.value));
    timeAtHeight = keyframe2.time + num * (keyframe1.time - keyframe2.time);
    return true;
  }

  private void Awake()
  {
    this._ballRigidbody = this.GetComponent<Rigidbody>();
    PhysicsSimulation.InitiatePhysicsScene();
    if (!((UnityEngine.Object) BallPhysicsSimulation._cachedDummySimulatedRigidbody == (UnityEngine.Object) null))
      return;
    BallPhysicsSimulation._cachedDummySimulatedRigidbody = PhysicsSimulation.CreateDummyRigidbody(this.name);
  }

  private void OnEnable() => Ball.State.BallState.OnValueChanged += new Action<EBallState>(this.SimulateBallTrajectory);

  private void OnDisable() => Ball.State.BallState.OnValueChanged -= new Action<EBallState>(this.SimulateBallTrajectory);

  private void LateUpdate()
  {
    if ((double) BallPhysicsSimulation._simulatedTrajectoryLastTime >= (double) Time.time + (double) BallPhysicsSimulation._minimumLookAheadTime)
      return;
    this.SimulateBallTrajectory((EBallState) (Variable<EBallState>) Ball.State.BallState);
  }

  private bool DefaultSimulationEndCondition(Rigidbody simulatedRigidBody, float simulationTime) => (double) simulatedRigidBody.transform.position.y <= 0.0 || (double) simulationTime >= (double) BallPhysicsSimulation._minimumLookAheadTime * 2.0;

  private void SimulateBallTrajectory(EBallState ballState) => BallPhysicsSimulation.SimulateBallTrajectory(ballState, this._ballRigidbody, new PhysicsSimulation.SimulationEndCondition(this.DefaultSimulationEndCondition));

  private static void SimulateBallTrajectory(
    EBallState ballState,
    Rigidbody ballRigidBody,
    PhysicsSimulation.SimulationEndCondition endCondition)
  {
    if (ballRigidBody.isKinematic || !BallPhysicsSimulation.SuitableBallState(ballState) || (double) ballRigidBody.velocity.sqrMagnitude <= (double) Mathf.Epsilon)
      return;
    PhysicsSimulation.SimulateRigidbody(ballRigidBody, BallPhysicsSimulation._cachedDummySimulatedRigidbody, 0.05f, endCondition, out BallPhysicsSimulation._simulatedTrajectory);
    BallPhysicsSimulation._simulatedTrajectoryLastTime = ((IEnumerable<Keyframe>) BallPhysicsSimulation._simulatedTrajectory.curveX.keys).Last<Keyframe>().time;
  }

  private static bool SuitableBallState(EBallState state) => state == EBallState.Kick || state == EBallState.InAirDeflected || state == EBallState.InAirPass || state == EBallState.InAirToss;
}
