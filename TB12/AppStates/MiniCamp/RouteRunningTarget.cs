// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MiniCamp.RouteRunningTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.AppStates.MiniCamp
{
  public class RouteRunningTarget : PracticeTarget, IThrowTarget
  {
    public System.Action CompletePassAndHitRing;
    private PlayAssignment _routeData;
    private bool _shouldRunRoute;
    public float[] path;
    private int flipVal = -1;
    private bool reachedGoal;
    private int pathIndex = 1;
    private Vector3 target;
    private float ballHashPosition;
    private float ballOnValue;
    private BlockType blockType;
    private bool disableRotation;
    [SerializeField]
    private float baseSpeed = 5f;
    private float turnSpeed = 7f;
    private float routeSpeedModifier;
    private float _radiusOfSatisfaction = Field.ONE_HALF_YARD;
    private float _minBestTargetDist = 1f;
    private bool _shortPass;
    private Vector3 _ballDir;
    private bool _didGetNearUs;
    private bool _hitRing;
    private bool _passSuccessful;
    private float _bestDot;
    private float minHeight = 0.7f;
    private float maxHeight = 2.3f;
    private float pathSpeedModifierScale = 0.5f;
    private BallObject _ballTarget;
    private float _totalRunDistance;
    private bool _flipDirection;
    private bool _isTargetedForThrow;
    [SerializeField]
    private ThrowRing _throwRing;
    [HideInInspector]
    public float YardLineAdjustment;

    public float TotalRunDistance => this._totalRunDistance;

    public ThrowRing ThrowRingTarget => this._throwRing;

    public void SetRouteData(PlayAssignment InRouteData, bool bShouldFlip = false)
    {
      this._isTargetedForThrow = false;
      this._bestDot = -2f;
      this.pathIndex = 1;
      this._ballTarget = (BallObject) null;
      this._totalRunDistance = 0.0f;
      this.SetReachedGoal(false);
      this._routeData = InRouteData;
      this.flipVal = bShouldFlip ? -1 : 1;
      this.SetRoutePath(this._routeData.GetRoutePoints());
    }

    public void SetHitRing()
    {
      AppSounds.PlaySfx(ESfxTypes.kMiniRingSuccess);
      this._hitRing = true;
      this.SetActiveForPoints(true);
    }

    public bool CheckForSuccessfulPass()
    {
      if (this._passSuccessful)
        return true;
      if (this._hitRing)
        this._hitRing = false;
      return false;
    }

    public void ResetHitRing()
    {
      this._hitRing = false;
      this._passSuccessful = false;
      this.SetActiveForPoints(false);
    }

    public void SetThrowRingTarget(ThrowRing throwRing) => this._throwRing = throwRing;

    private void OnEnable() => this.OnHit += new System.Action(this.RouteRunningTarget_OnHit);

    private void OnDisable() => this.OnHit -= new System.Action(this.RouteRunningTarget_OnHit);

    protected override void Update()
    {
      base.Update();
      if (!this._shouldRunRoute || this.reachedGoal || this._routeData == null)
        return;
      this.UpdateOffenseMovement();
      this.FaceTarget(Time.deltaTime);
      Vector3 vector3_1 = this.target - this.transform.position;
      float x = vector3_1.x;
      float z = vector3_1.z;
      if ((double) Math.Abs(this.transform.position.x) > (double) Field.OUT_OF_BOUNDS)
      {
        this.SetReachedGoal(true);
      }
      else
      {
        Vector3 vector3_2 = new Vector3(x, 0.0f, z);
        Vector3 normalized = (this.transform.position + vector3_2 - this.transform.position).normalized;
        float num = this._isTargetedForThrow ? this.baseSpeed * 1.1f : this.baseSpeed;
        Vector3 vector3_3 = vector3_2.normalized * Time.deltaTime * (num + this.routeSpeedModifier);
        this.transform.position += vector3_3;
        this._totalRunDistance += vector3_3.magnitude;
      }
    }

    public float GetTotalRouteRunDistanceYds()
    {
      float distance = 0.0f;
      List<Vector2> vector2List = new List<Vector2>();
      if (this.path.Length != 0)
      {
        for (int index = 1; index < this.path.Length; index += 3)
        {
          vector2List.Add(new Vector2(this.path[index], this.path[index + 1]));
          if (vector2List.Count > 1)
            distance += Vector2.Distance(vector2List[vector2List.Count - 1], vector2List[vector2List.Count - 2]);
        }
      }
      return (float) Field.ConvertDistanceToYards(distance);
    }

    protected virtual void RouteRunningTarget_OnHit()
    {
      AppSounds.Play3DSfx(ESfxTypes.kMiniBallHitDummy, this.transform);
      if (!this._hitRing)
        return;
      this._hitRing = false;
      this._passSuccessful = true;
      System.Action completePassAndHitRing = this.CompletePassAndHitRing;
      if (completePassAndHitRing != null)
        completePassAndHitRing();
      this.SuccessfulPass();
    }

    protected void SuccessfulPass()
    {
      Debug.Log((object) nameof (SuccessfulPass));
      MiniCampCrowdAudioManager objectOfType = UnityEngine.Object.FindObjectOfType<MiniCampCrowdAudioManager>();
      if (!((UnityEngine.Object) objectOfType != (UnityEngine.Object) null))
        return;
      objectOfType.PlayCrowdCheer();
    }

    private void FaceTarget(float timeStep)
    {
      Vector3 forward = new Vector3(this.target.x, 0.0f, this.target.z) - this.transform.position;
      if (!(forward != Vector3.zero))
        return;
      Quaternion quaternion = Quaternion.LookRotation(forward);
      Quaternion.RotateTowards(this.transform.rotation, quaternion, this.turnSpeed * timeStep);
      Quaternion.Slerp(this.transform.rotation, quaternion, this.turnSpeed * timeStep);
      this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, this.turnSpeed * timeStep);
    }

    public void SetRoutePath(float[] p)
    {
      this.blockType = (BlockType) p[0];
      float num1 = this.transform.position.x - this.ballHashPosition;
      if ((double) num1 <= 0.75 && (double) num1 > -0.75)
        num1 = 0.0f;
      this.path = new float[p.Length];
      for (int index = 1; index < p.Length; index += 3)
      {
        float num2 = p[index];
        if ((double) num1 > 0.0 || (double) num1 == 0.0 && this.flipVal == -1)
          num2 *= -1f;
        this.path[index] = (float) Mathf.RoundToInt(num2 + num1);
        this.path[index + 1] = p[index + 1];
        this.path[index + 2] = p[index + 2];
      }
    }

    private void UpdateOffenseMovement()
    {
      if (!this.reachedGoal)
      {
        if ((UnityEngine.Object) this._ballTarget != (UnityEngine.Object) null)
        {
          this.SetTarget(this._ballTarget.transform.position);
        }
        else
        {
          if (this.path == null || this.pathIndex > this.path.Length)
            return;
          if (this.pathIndex == 1)
            this.SetNextPointInPathAsTarget();
          float num = Vector3.Distance(this.transform.position, this.target);
          Vector3 normalized = (this.target - this.transform.position).normalized;
          if ((double) num < (double) Field.THREE_YARDS && this.blockType != BlockType.PassBlockBegin && (double) Vector3.Dot(normalized, this.transform.forward) < 0.0)
            num = 0.0f;
          if ((double) num > (double) this._radiusOfSatisfaction)
            return;
          this.pathIndex += 3;
          if (this.pathIndex >= this.path.Length)
          {
            this.ResetSpeedModifier();
            if (this.blockType == BlockType.QBOnRunPlay)
            {
              this.SetTarget(this.transform.position);
              this.SetReachedGoal(true);
              this.SetDisableRotation(true);
            }
            else if (this.blockType == BlockType.PassBlockBegin)
            {
              this.blockType = BlockType.PassBlockNormal;
              this.SetTarget(this.transform.position);
              this.SetReachedGoal(true);
              this.SetDisableRotation(true);
              this.transform.rotation = Field.OFFENSE_TOWARDS_LOS_QUATERNION;
            }
            else
            {
              this.turnSpeed = 60f;
              this.SetTarget(this.transform.position);
              this.SetReachedGoal(true);
            }
          }
          else
            this.SetNextPointInPathAsTarget();
        }
      }
      else
        this.SetTarget(this.transform.position);
    }

    public void SetDisableRotation(bool _disableRotation) => this.disableRotation = _disableRotation;

    private void SetNextPointInPathAsTarget()
    {
      this.SetTarget(this.GetNextPointInPath());
      if (this.pathIndex < this.path.Length - 2)
      {
        this.SetRouteSpeedModifier(this.path[this.pathIndex + 2] * this.pathSpeedModifierScale);
      }
      else
      {
        Debug.LogError((object) "Path index out of range to set speed mod. Setting to 0");
        this.ResetSpeedModifier();
      }
      this.SetReachedGoal(false);
    }

    public void SetReachedGoal(bool _reachedGoal) => this.reachedGoal = _reachedGoal;

    public void SetTarget(Vector3 _target, bool isTargetedForThrow = false)
    {
      this._isTargetedForThrow = isTargetedForThrow;
      if (!isTargetedForThrow)
        _target.z -= this.YardLineAdjustment * (float) global::Game.OffensiveFieldDirection;
      this.target = _target;
    }

    public Vector3 GetTarget() => this.target;

    private Vector3 GetNextPointInPath()
    {
      if (this.pathIndex < this.path.Length - 2)
        return new Vector3(this.path[this.pathIndex] * Field.ONE_YARD + this.ballHashPosition, 0.0f, this.path[this.pathIndex + 1] * Field.ONE_YARD * (float) global::Game.OffensiveFieldDirection + this.ballOnValue);
      Debug.LogError((object) "Path index out of range . Setting to next point to last point in path");
      return new Vector3(this.path[this.path.Length - 3] + this.ballHashPosition, 0.0f, this.path[this.path.Length - 2] * (float) global::Game.OffensiveFieldDirection + this.ballOnValue);
    }

    private void SetRouteSpeedModifier(float spd) => this.routeSpeedModifier = spd;

    public void ResetSpeedModifier() => this.SetRouteSpeedModifier(0.0f);

    public void SetShouldRunRoute(bool InShouldRunRoute) => this._shouldRunRoute = InShouldRunRoute;

    public new void SetPriority(bool priority)
    {
    }

    public new bool IsPriorityTarget()
    {
      EligibilityManager instance = EligibilityManager.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
      {
        ReceiverUI currentTarget = instance.GetCurrentTarget();
        if ((UnityEngine.Object) currentTarget != (UnityEngine.Object) null && (UnityEngine.Object) currentTarget.GetComponent<RouteRunningTarget>() == (UnityEngine.Object) this)
        {
          if ((double) this._bestDot == -2.0 && this._didGetNearUs)
            this._minBestTargetDist += 1.2f;
          this._didGetNearUs = false;
          this._bestDot = -2f;
          return true;
        }
      }
      return false;
    }

    public new Vector3 GetIdealThrowTarget() => this.transform.position + this.transform.forward * Mathf.Lerp(this._minBestTargetDist, 13f, Vector3.Distance(PlayerCamera.Camera.transform.position, this.transform.position) / 44f);

    public new Vector3 EvaluatePosition(float time)
    {
      Vector3 position = this.transform.position;
      Vector3 axisPlayerPosition = this.GetAxisPlayerPosition(this.transform, time);
      Debug.Log((object) ("currPos[" + position.ToString() + "] movePos[" + axisPlayerPosition.ToString() + "] time[" + time.ToString() + "]"));
      return !ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? Vector3.Lerp(position, axisPlayerPosition, time / 2f) : axisPlayerPosition;
    }

    public new Vector3 GetHitPosition(float time, Vector3 ballPos)
    {
      float y1 = ballPos.y;
      if (ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value)
      {
        if ((double) y1 <= (double) this.maxHeight && (double) y1 >= (double) this.minHeight)
        {
          Transform transform = this.transform;
          Vector3 b = transform.position.SetY(0.0f);
          Vector3 a = PersistentSingleton<PlayerCamera>.Instance.Position.SetY(0.0f);
          Vector3 vector3_1 = ballPos.SetY(b.y);
          float num1 = Vector3.Distance(a, b);
          this._shortPass = true;
          Debug.Log((object) ("distanceToReceiver[" + num1.ToString() + "] qbPosition[" + a.ToString() + "] receiverPosition[" + b.ToString() + "]"));
          if ((double) num1 >= 20.0 * (double) Field.ONE_YARD)
            this._shortPass = false;
          this._ballDir = (vector3_1 - b).normalized;
          float num2 = Vector3.Dot(this._ballDir, transform.forward);
          float num3 = !this._shortPass ? 0.75f : 0.975f;
          Debug.Log((object) ("dot[" + num2.ToString() + "] _shortPass[" + this._shortPass.ToString() + "]"));
          if ((double) num2 > 0.5)
            this._didGetNearUs = true;
          if ((double) num2 > (double) this._bestDot && (double) num2 >= (double) num3)
          {
            this._bestDot = num2;
            Vector3 adjustThis = this.EvaluatePosition(time).SetY(y1);
            bool flag = (bool) FieldState.OffenseGoingNorth ? (double) adjustThis.z >= (double) b.z : (double) adjustThis.z <= (double) b.z;
            string[] strArray = new string[13]
            {
              "Valid Pos [",
              adjustThis.ToString(),
              "] ballDir[",
              this._ballDir.ToString(),
              "] receiverPos[",
              this.transform.position.ToString(),
              "] ballPos[",
              null,
              null,
              null,
              null,
              null,
              null
            };
            Vector3 vector3_2 = ballPos;
            strArray[7] = vector3_2.ToString();
            strArray[8] = "] receiverFor[";
            vector3_2 = this.transform.forward;
            strArray[9] = vector3_2.ToString();
            strArray[10] = "] bestDot[";
            strArray[11] = this._bestDot.ToString();
            strArray[12] = "]";
            Debug.Log((object) string.Concat(strArray));
            return flag || this._shortPass ? Field.AdjustToBeInbounds(adjustThis) : ballPos;
          }
        }
        return ballPos;
      }
      float y2 = Mathf.Clamp(y1, this.minHeight, this.maxHeight);
      return this.EvaluatePosition(time).SetY(y2);
    }

    private Vector3 GetAxisPlayerPosition(Transform receiver, float time)
    {
      if (this.path.Length == 0 || this.reachedGoal)
        return this.transform.position;
      float num1 = ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? 4f : 7.2f;
      float num2 = ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? 0.05f : 0.1f;
      float num3 = ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? 0.0f : 0.1f;
      Vector3 position = receiver.position;
      Vector3 b = PersistentSingleton<PlayerCamera>.Instance.Position.SetY(0.0f);
      float num4 = Vector3.Distance(position, b);
      Vector3 lhs = receiver.forward;
      if (ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value && !this._shortPass)
        lhs = this._ballDir;
      Vector3 vector3_1 = lhs * num1;
      Vector3 normalized = (position - b).normalized;
      float num5 = Vector3.Dot(lhs, normalized);
      Vector3 vector3_2 = lhs * (num5 * num4 * num2);
      Vector3 vector3_3 = lhs * (num4 * num3);
      if ((double) num5 < -0.75)
        vector3_3 *= 0.35f;
      float num6 = ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? Mathf.Max(0.5f, time * 1.3f) : 1f;
      Vector3 zero = Vector3.zero;
      Vector3 axisPlayerPosition;
      if (!ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value)
      {
        Vector3 vector3_4 = vector3_3 * num6;
        axisPlayerPosition = position + vector3_1 + vector3_4 + vector3_2;
      }
      else
      {
        Debug.Log((object) ("passTypeMultiplier[" + num6.ToString() + "]"));
        axisPlayerPosition = position + (vector3_1 + vector3_3 + vector3_2) * num6;
      }
      return axisPlayerPosition;
    }

    public void SetMoveTowardsBall(BallObject throwDataBall) => this._ballTarget = throwDataBall;
  }
}
