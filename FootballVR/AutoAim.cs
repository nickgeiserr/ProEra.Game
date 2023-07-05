// Decompiled with JetBrains decompiler
// Type: FootballVR.AutoAim
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public static class AutoAim
  {
    private const float g = 9.85f;
    public static Action<Vector3[], Vector3, Color> OnShowPath;

    public static Vector3 GetImpulseToHitTarget(Vector3 distance, float time, float gravity = 9.85f)
    {
      if ((double) time < 0.0099999997764825821)
      {
        Debug.LogWarning((object) string.Format("invalid time = {0}. Using default time = 1", (object) time));
        time = 1f;
      }
      return distance / time + (float) ((double) gravity * (double) time / 2.0) * Vector3.up;
    }

    public static Vector3 GetImpulseToHitTargetWithArc(Vector3 distance, float arc)
    {
      arc = (float) ((double) Mathf.Clamp(arc, 0.0f, 1f) * 0.75 + 0.10000000149011612);
      float num = Mathf.Clamp(Mathf.Sqrt((float) (2.0 * (double) Mathf.Clamp(distance.SetY(0.0f).magnitude * arc, 0.2f, float.MaxValue) / 9.8500003814697266)), 0.1f, 10f);
      return AutoAim.GetImpulseToHitTarget(distance, num * 2f);
    }

    public static Vector3 GetImpulseToHitTargetWithAngle(Vector3 distance, float angle)
    {
      angle = Mathf.Clamp(angle, 10f, 75f);
      float num = Mathf.Tan((float) Math.PI / 180f * angle);
      float time = Mathf.Pow((float) (2.0 * (double) distance.SetY(0.0f).magnitude * (double) num / 9.8500003814697266), 0.3333f);
      return AutoAim.GetImpulseToHitTarget(distance, time);
    }

    public static Vector3 GetLandingPoint(
      Vector3 initialPosition,
      Vector3 velocity,
      float endHeight,
      out float time,
      float gravity = 9.85f)
    {
      time = AutoAim.GetFlightTime(initialPosition, velocity, endHeight, gravity);
      return (double) time < 0.0 ? velocity : (initialPosition + time * velocity).SetY(endHeight);
    }

    public static float GetFlightTime(
      Vector3 initialPosition,
      Vector3 velocity,
      float endHeight,
      float gravity = 9.85f)
    {
      float num1 = velocity.y / gravity;
      float num2 = (float) ((double) num1 * (double) velocity.y / 2.0) + initialPosition.y;
      if ((double) num2 < (double) endHeight)
        return -1f;
      float f = (float) (2.0 * ((double) num2 - (double) endHeight)) / gravity;
      if ((double) f < 0.0)
        return -1f;
      float num3 = Mathf.Sqrt(f);
      return (float) (((double) num1 + (double) num3) * 0.99500000476837158);
    }

    public static Vector3[] GetTrajectory(
      Vector3 initialPos,
      Vector3 velocity,
      float timeStep,
      float time,
      float gravity = 9.85f)
    {
      int length = Mathf.CeilToInt(time / timeStep);
      Vector3[] trajectory = new Vector3[length];
      for (int index = 0; index < length; ++index)
        trajectory[index] = initialPos + AutoAim.GetDistanceTraveled((float) index * timeStep, velocity, gravity);
      return trajectory;
    }

    public static Vector3 GetDistanceTraveled(float time, Vector3 initialVelocity, float gravity = 9.85f) => time * initialVelocity + (float) ((double) time * (double) time * (double) gravity / 2.0) * Vector3.down;

    public static Vector3 GetVelocityAfterTime(float time, Vector3 initialVelocity, float gravity = 9.85f) => initialVelocity + gravity * time * Vector3.down;

    public static bool HitsAnyTarget(ThrowData throwData, IEnumerable<IThrowTarget> targets)
    {
      Vector3[] trajectory = AutoAim.GetTrajectory(throwData.startPosition, throwData.throwVector, throwData.timeStep, throwData.flightTime);
      foreach (IThrowTarget target in targets)
      {
        if (target.IsTargetValid() && (double) AutoAim.GetClosestDistance(trajectory, target, throwData.maxDistance, throwData.timeStep, out float _, out Vector3 _) < (double) target.hitRange * 2.0499999523162842)
          return true;
      }
      return false;
    }

    private static void DrawTrajectory(Vector3[] points, Color color, float duration)
    {
    }

    private static bool GetImpulseToHitTargetAtSpeed(
      float ballSpeed,
      Vector3 distanceVector,
      float gravity,
      out Vector3 impulse)
    {
      impulse = Vector3.zero;
      Vector3 vector3 = distanceVector.SetY(0.0f);
      float magnitude = vector3.magnitude;
      float y = distanceVector.y;
      double num1;
      float a = (float) (num1 = -(double) gravity * (double) magnitude * (double) magnitude / (2.0 * (double) ballSpeed * (double) ballSpeed));
      float b = magnitude;
      double num2 = (double) y;
      float c = (float) (num1 - num2);
      float root2 = float.NaN;
      if (MathUtils.SolveQuadratic(a, b, c, out float _, out root2) == 0)
        return false;
      double f = (double) Mathf.Atan(root2);
      float num3 = Mathf.Sin((float) f);
      float num4 = Mathf.Cos((float) f);
      impulse = vector3.normalized * ballSpeed * num4;
      impulse.y = ballSpeed * num3;
      return true;
    }

    public static void GetClosestTargetWithoutAutoAim(
      ThrowData throwData,
      IList<IThrowTarget> targets)
    {
      float maxBallSpeed = Game.ThrowingConfig.MaxBallSpeed;
      Vector3 velocity = Vector3.ClampMagnitude(throwData.throwVector, maxBallSpeed);
      float time;
      Vector3 landingPoint = AutoAim.GetLandingPoint(throwData.startPosition, velocity, 1.2f, out time);
      IThrowTarget throwTarget = (IThrowTarget) null;
      if (targets.Count > 0)
      {
        throwTarget = targets[0];
        float num = (throwTarget.EvaluatePosition(0.0f) - landingPoint).SetY(0.0f).magnitude;
        for (int index = 1; index < targets.Count; ++index)
        {
          IThrowTarget target = targets[index];
          float magnitude = (target.EvaluatePosition(0.0f) - landingPoint).SetY(0.0f).magnitude;
          if ((double) magnitude < (double) num)
          {
            num = magnitude;
            throwTarget = target;
          }
        }
      }
      throwData.hasTarget = true;
      throwData.autoAimedVector = velocity;
      throwData.closestTarget = throwTarget;
      throwData.timeToGetToTarget = (landingPoint - throwData.startPosition).SetY(0.0f).magnitude / velocity.magnitude;
      throwData.targetPosition = landingPoint;
      throwData.flightTime = time;
    }

    public static void GetClosestTarget(
      ThrowData throwData,
      IList<IThrowTarget> targets,
      AutoAimSettings settings)
    {
      bool flag = false;
      ThrowingConfig throwingConfig = Game.ThrowingConfig;
      float num1 = throwingConfig.MaxBallSpeed;
      float num2 = (float) ((double) num1 * (double) num1 / 9.8500003814697266);
      Vector3[] trajectory = AutoAim.GetTrajectory(throwData.startPosition, throwData.throwVector, throwData.timeStep, throwData.flightTime);
      float duration = 10f;
      AutoAim.DrawTrajectory(trajectory, Color.red, duration);
      if ((double) throwData.throwVector.y < -3.0)
      {
        throwData.hasTarget = false;
      }
      else
      {
        float num3 = throwData.maxDistance;
        Transform transform = PlayerCamera.Camera.transform;
        Vector3 forward = transform.forward;
        Vector3 position1 = transform.position;
        IThrowTarget throwTarget = (IThrowTarget) null;
        float num4 = -1f;
        float num5 = -1f;
        foreach (IThrowTarget target in (IEnumerable<IThrowTarget>) targets)
        {
          Vector3 position2 = target.EvaluatePosition(0.0f);
          if (target.IsPriorityTarget())
          {
            if (throwTarget != null)
            {
              throwTarget.SetPriority(false);
              throwTarget = (IThrowTarget) null;
            }
            float num6 = Vector3.Dot(throwData.throwVector.normalized, (position2 - position1).normalized);
            Debug.Log((object) ("throwCheck: " + num6.ToString()));
            if ((double) num6 < 0.699999988079071)
              return;
            break;
          }
          float num7 = Vector3.Dot(forward, (position2 - position1).normalized);
          double num8 = (double) Vector3.Distance(position1, position2);
          if ((double) num7 > 0.949999988079071 && ((double) num7 > (double) num4 || target.IsPlayer() && (double) num7 > (double) num5))
          {
            num4 = num7;
            if (target.IsPlayer())
              num5 = num7;
            throwTarget?.SetPriority(false);
            target.SetPriority(true);
            throwTarget = target;
          }
        }
        foreach (IThrowTarget target in (IEnumerable<IThrowTarget>) targets)
        {
          Vector3 vector3_1 = throwData.throwVector;
          float flightTime1 = throwData.flightTime;
          if (target.IsPriorityTarget())
            target.DrawRange();
          if (target.IsTargetValid())
          {
            float distanceTime = -1f;
            Vector3 bestTargetPos = Vector3.zero;
            float closestDistance = AutoAim.GetClosestDistance(trajectory, target, throwData.maxDistance, throwData.timeStep, out distanceTime, out bestTargetPos);
            if ((double) closestDistance < (double) num3 || target.IsPriorityTarget())
            {
              float num9 = Vector3.Distance(bestTargetPos, throwData.startPosition);
              if ((double) num9 > (double) num2)
              {
                Debug.Log((object) "AutoAim: Target is too far away to throw to with the current max ball speed.");
              }
              else
              {
                Action<Vector3[], Vector3, Color> onShowPath1 = AutoAim.OnShowPath;
                if (onShowPath1 != null)
                  onShowPath1(trajectory, bestTargetPos, Color.red);
                float magnitude1 = AutoAim.GetImpulseToHitTarget(bestTargetPos - throwData.startPosition, distanceTime).magnitude;
                float num10 = Vector3.Distance(throwData.startPosition, bestTargetPos);
                if (target.IsPriorityTarget() && (double) num10 < (double) throwingConfig.ShortPassDist)
                  num1 = throwingConfig.MaxBallSpeedShort;
                if (target.IsPriorityTarget() && ((double) closestDistance == double.PositiveInfinity || (double) magnitude1 > (double) num1))
                {
                  int num11 = 0;
                  int autoAimPasses = GameSettings.GetDifficulty().AutoAimPasses;
                  if ((double) vector3_1.y < 0.5)
                    vector3_1.y = 0.5f;
                  double magnitude2 = (double) vector3_1.magnitude;
                  float y = vector3_1.normalized.y;
                  float num12 = (float) magnitude2;
                  ThrowData throwData1 = new ThrowData();
                  int num13 = 0;
                  float t = 0.55f;
                  while (((double) closestDistance == double.PositiveInfinity || (double) magnitude1 > (double) num1) && num11 < autoAimPasses)
                  {
                    target.IsPriorityTarget();
                    num12 *= 1.1f;
                    vector3_1 = (Vector3.Lerp(vector3_1.normalized, (target.GetIdealThrowTarget() - throwData.startPosition).normalized, t) * num12) with
                    {
                      y = y * num12
                    };
                    flightTime1 = AutoAim.GetFlightTime(throwData.startPosition, vector3_1, 0.8f);
                    trajectory = AutoAim.GetTrajectory(throwData.startPosition, vector3_1, throwData.timeStep, flightTime1);
                    ref Vector3 local = ref trajectory[0];
                    Color color = Color.Lerp(Color.black, Color.white, (float) num11 / (float) autoAimPasses);
                    AutoAim.DrawTrajectory(trajectory, color, duration);
                    closestDistance = AutoAim.GetClosestDistance(trajectory, target, throwData.maxDistance, throwData.timeStep, out distanceTime, out bestTargetPos);
                    num9 = Vector3.Distance(bestTargetPos, throwData.startPosition);
                    ++num11;
                    magnitude1 = AutoAim.GetImpulseToHitTarget(bestTargetPos - throwData.startPosition, distanceTime).magnitude;
                    if (((double) closestDistance == double.PositiveInfinity || (double) magnitude1 > (double) num1) && num11 < autoAimPasses)
                    {
                      if ((double) closestDistance == double.PositiveInfinity)
                      {
                        num12 *= (float) (1.0 + (double) throwingConfig.PowerIncreasePerDistanceFailure * (double) num13);
                        ++num13;
                      }
                      if ((double) magnitude1 > (double) num1)
                      {
                        y += throwingConfig.YVelocityIncreaseOnSpeedFailure;
                        num12 /= throwingConfig.PowerDecreaseOnSpeedFailure;
                        num13 = 0;
                      }
                      float r = Mathf.Lerp(1f, 0.0f, (float) num11 / (float) autoAimPasses);
                      Action<Vector3[], Vector3, Color> onShowPath2 = AutoAim.OnShowPath;
                      if (onShowPath2 != null)
                        onShowPath2(trajectory, bestTargetPos, new Color(r, 0.0f, 1f - r));
                      if (num11 == autoAimPasses / 2)
                      {
                        throwData1.autoAimedVector = AutoAim.GetImpulseToHitTarget(bestTargetPos - throwData.startPosition, distanceTime);
                        throwData1.closestTarget = target;
                        throwData1.timeToGetToTarget = distanceTime;
                        throwData1.targetPosition = bestTargetPos;
                        throwData1.throwVector = vector3_1;
                        throwData1.flightTime = flightTime1;
                      }
                    }
                  }
                  if ((double) distanceTime == -1.0)
                  {
                    Debug.Log((object) "BAD THROW");
                    throwData.closestTarget = throwData1.closestTarget;
                    throwData.timeToGetToTarget = throwData1.timeToGetToTarget;
                    throwData.targetPosition = throwData1.targetPosition;
                    throwData.throwVector = throwData1.throwVector;
                    throwData.flightTime = throwData1.flightTime;
                    ThrowReplayData data;
                    target.GetReplayData(out data);
                    data.timestamp = DateTime.Now.ToFileTime();
                    data.successfulThrow = false;
                    data.throwVector = throwData.throwVector;
                    data.startPosition = throwData.startPosition;
                    data.flightTime = throwData.flightTime;
                    throwData.ball.SetThrowReplayData(data);
                    throwData.ball.Graphics.SetHighlight(true);
                    break;
                  }
                }
                float num14 = (float) ((double) vector3_1.y / 9.8500003814697266 * (double) vector3_1.y / 2.0) + throwData.startPosition.y;
                ThrowReplayData data1;
                target.GetReplayData(out data1);
                data1.timestamp = DateTime.Now.ToFileTime();
                data1.successfulThrow = true;
                data1.throwVector = throwData.throwVector;
                data1.startPosition = throwData.startPosition;
                data1.flightTime = throwData.flightTime;
                throwData.ball.SetThrowReplayData(data1);
                Vector3 vector3_2 = bestTargetPos - throwData.startPosition;
                if ((double) vector3_2.magnitude >= (double) settings.MinTargetDistance || target.IsPriorityTarget())
                {
                  Action<Vector3[], Vector3, Color> onShowPath3 = AutoAim.OnShowPath;
                  if (onShowPath3 != null)
                    onShowPath3(trajectory, bestTargetPos, Color.white);
                  Vector3 vector3_3 = AutoAim.GetImpulseToHitTarget(vector3_2, distanceTime);
                  if ((double) vector3_3.magnitude > (double) num1)
                  {
                    Vector3 impulse;
                    if (AutoAim.GetImpulseToHitTargetAtSpeed(num1, vector3_2, 9.85f, out impulse))
                    {
                      Debug.Log((object) "AutoAim: Adjusted throw vec for max speed");
                      vector3_3 = impulse;
                    }
                    else
                    {
                      Debug.Log((object) "AutoAim: Tried to adjust down to max speed, but failed, so just scaled the current trajectory to max speed.");
                      vector3_3 = Vector3.ClampMagnitude(vector3_3, num1);
                    }
                  }
                  if (targets.Count > 1)
                  {
                    if ((double) Vector3.Angle(vector3_3, vector3_1) > (double) settings.MaxAngleAdjustment)
                      continue;
                  }
                  else if ((double) vector3_2.magnitude >= (double) settings.MaxPitchDistance)
                    continue;
                  float magnitude3 = vector3_3.magnitude;
                  if ((double) magnitude3 >= 0.05000000074505806 && (double) magnitude3 <= 200.0 || target.IsPriorityTarget())
                  {
                    Vector3 vector3_4 = Vector3.zero;
                    if (ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value)
                    {
                      vector3_4 = MathUtils.GetRandomVector3InBox(throwingConfig.MinRandomLaunchVelocityAdjust, throwingConfig.MaxRandomLaunchVelocityAdjust);
                      vector3_4.x *= (float) Game.OffensiveFieldDirection;
                      vector3_4.z *= (float) Game.OffensiveFieldDirection;
                      vector3_3 += vector3_4;
                    }
                    num3 = closestDistance;
                    throwData.hasTarget = true;
                    throwData.autoAimedVector = vector3_3;
                    throwData.closestTarget = target;
                    throwData.timeToGetToTarget = distanceTime;
                    throwData.targetPosition = bestTargetPos;
                    throwData.throwVector = vector3_1;
                    throwData.flightTime = flightTime1;
                    flag = true;
                    float flightTime2 = AutoAim.GetFlightTime(throwData.startPosition, vector3_1, 0.8f);
                    AutoAim.DrawTrajectory(AutoAim.GetTrajectory(throwData.startPosition, vector3_3, throwData.timeStep, flightTime2), Color.green, duration);
                    Debug.Log((object) ("closestDistance[" + num3.ToString() + "] autoAimedVector[" + vector3_3.ToString() + "] closestTarget[" + target?.ToString() + "] targetPos[" + bestTargetPos.ToString() + "] timeToGetToTarget[" + distanceTime.ToString() + "] ballSpeed[" + vector3_3.magnitude.ToString() + "]"));
                    Debug.Log((object) ("throwVector[" + throwData.throwVector.ToString() + "] throwDist[" + num9.ToString() + "] distanceTime[" + distanceTime.ToString() + "] m/s[" + (num9 / distanceTime).ToString() + "] maxHeight[" + num14.ToString() + "] variance [" + vector3_4.ToString() + "]"));
                    if (target.IsPriorityTarget())
                    {
                      if (throwTarget != null)
                      {
                        throwTarget.SetPriority(false);
                        throwTarget = (IThrowTarget) null;
                        break;
                      }
                      break;
                    }
                  }
                }
              }
            }
          }
        }
        if (!flag)
          throwData.throwVector = Vector3.ClampMagnitude(throwData.throwVector, num1);
        throwTarget?.SetPriority(false);
        throwData.accuracy = (float) (1.0 - (double) num3 / (double) throwData.maxDistance);
      }
    }

    private static float GetClosestDistance(
      Vector3[] positions,
      IThrowTarget target,
      float maxDistance,
      float timeStep,
      out float distanceTime,
      out Vector3 bestTargetPos,
      bool isThrow = true)
    {
      float closestDistance = float.PositiveInfinity;
      distanceTime = -1f;
      int length = positions.Length;
      bestTargetPos = length > 0 ? positions[0] : Vector3.zero;
      float time = 0.0f;
      for (int index = 1; index < length; ++index)
      {
        bool flag = index == length - 1;
        time += timeStep;
        if (flag || (double) time >= (double) target.minCatchTime)
        {
          Vector3 position = positions[index];
          Vector3 hitPosition = target.GetHitPosition(time, position);
          float magnitude = (hitPosition - position).magnitude;
          if (ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value)
          {
            if ((double) magnitude == 0.0)
              continue;
          }
          else if (!flag && (double) magnitude > (double) maxDistance)
            continue;
          if (flag || (double) magnitude < (double) closestDistance)
          {
            closestDistance = magnitude;
            distanceTime = time;
            bestTargetPos = hitPosition;
          }
        }
      }
      return closestDistance;
    }
  }
}
