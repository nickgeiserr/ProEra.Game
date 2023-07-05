// Decompiled with JetBrains decompiler
// Type: ProEra.Game.AxisReceiver
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using UnityEngine;

namespace ProEra.Game
{
  public class AxisReceiver : IThrowTarget
  {
    private readonly PlayerAI _playerAI;
    private Vector3 _ballDir;
    private float _bestDot;
    private float _minBestTargetDist;
    private bool _didGetNearUs;
    private bool _shortPass;
    public static Action<Transform> OnDrawRange;

    public AxisReceiver(PlayerAI playerAI)
    {
      this._playerAI = playerAI;
      this._minBestTargetDist = 1f;
      this._bestDot = -2f;
    }

    public Vector3 EvaluatePosition(float time)
    {
      Vector3 position = this._playerAI.transform.position;
      Vector3 axisPlayerPosition = this.GetAxisPlayerPosition(this._playerAI.trans, time);
      return !ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? Vector3.Lerp(position, axisPlayerPosition, time / 2f) : axisPlayerPosition;
    }

    private Vector3 GetAxisPlayerPosition(Transform receiver, float time)
    {
      ThrowingConfig throwingConfig = global::Game.ThrowingConfig;
      float num1 = ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? throwingConfig.AlphaLeadingBaseMultipler : throwingConfig.LeadingBaseMultipler;
      float num2 = ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? throwingConfig.AlphaLeadingDotMultiplier : throwingConfig.LeadingDotMultiplier;
      float num3 = ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? throwingConfig.AlphaLeadingReceiverDistMultiplier : throwingConfig.LeadingReceiverDistMultiplier;
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
      if ((double) num5 < -(double) throwingConfig.MinDotReceiverFacingToPasserForScaling)
        vector3_3 *= throwingConfig.LeadingDistanceAdjustReceiverFacingPasser;
      Vector3 zero = Vector3.zero;
      Vector3 axisPlayerPosition;
      if (!ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value)
      {
        axisPlayerPosition = position + vector3_1 + vector3_3 + vector3_2;
      }
      else
      {
        float num6 = Mathf.Max(0.5f, time * throwingConfig.AlphaLeadingPassTypeMultiplier);
        Debug.Log((object) ("passTypeMultiplier[" + num6.ToString() + "]"));
        axisPlayerPosition = position + (vector3_1 + vector3_3 + vector3_2) * num6;
      }
      return axisPlayerPosition;
    }

    public Vector3 GetHitPosition(float time, Vector3 ballPos)
    {
      ThrowingConfig throwingConfig = global::Game.ThrowingConfig;
      float y1 = ballPos.y;
      if (ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value)
      {
        if ((double) throwingConfig.MinTargetHeight <= (double) y1 && (double) y1 <= (double) throwingConfig.MaxTargetHeight)
        {
          Transform transform = this._playerAI.transform;
          Vector3 b = transform.position.SetY(0.0f);
          Vector3 a = PersistentSingleton<PlayerCamera>.Instance.Position.SetY(0.0f);
          Vector3 vector3_1 = ballPos.SetY(b.y);
          float num1 = Vector3.Distance(a, b);
          this._shortPass = true;
          string[] strArray1 = new string[7]
          {
            "distanceToReceiver[",
            num1.ToString(),
            "] qbPosition[",
            a.ToString(),
            "] receiverPosition[",
            null,
            null
          };
          Vector3 vector3_2 = b;
          strArray1[5] = vector3_2.ToString();
          strArray1[6] = "]";
          Debug.Log((object) string.Concat(strArray1));
          if ((double) num1 >= (double) throwingConfig.MaxDistanceForShortPassAlpha)
            this._shortPass = false;
          vector3_2 = vector3_1 - b;
          this._ballDir = vector3_2.normalized;
          float num2 = Vector3.Dot(this._ballDir, transform.forward);
          float num3 = !this._shortPass ? throwingConfig.MinDotFromReceiverFacingToBallAlpha : throwingConfig.MinDotFromReceiverFacingToBallShortAlpha;
          Debug.Log((object) ("dot[" + num2.ToString() + "] _shortPass[" + this._shortPass.ToString() + "]"));
          if ((double) num2 > (double) throwingConfig.MinDotFromReceiverFacingToBallNearAlpha)
            this._didGetNearUs = true;
          if ((double) num2 > (double) this._bestDot && (double) num2 >= (double) num3)
          {
            this._bestDot = num2;
            Vector3 adjustThis = this.EvaluatePosition(time).SetY(y1);
            bool flag = true;
            string[] strArray2 = new string[13];
            strArray2[0] = "Valid Pos [";
            vector3_2 = adjustThis;
            strArray2[1] = vector3_2.ToString();
            strArray2[2] = "] ballDir[";
            vector3_2 = this._ballDir;
            strArray2[3] = vector3_2.ToString();
            strArray2[4] = "] receiverPos[";
            vector3_2 = this._playerAI.transform.position;
            strArray2[5] = vector3_2.ToString();
            strArray2[6] = "] ballPos[";
            vector3_2 = ballPos;
            strArray2[7] = vector3_2.ToString();
            strArray2[8] = "] receiverFor[";
            vector3_2 = this._playerAI.transform.forward;
            strArray2[9] = vector3_2.ToString();
            strArray2[10] = "] bestDot[";
            strArray2[11] = this._bestDot.ToString();
            strArray2[12] = "]";
            Debug.Log((object) string.Concat(strArray2));
            return flag || this._shortPass ? Field.AdjustToBeInbounds(adjustThis) : ballPos;
          }
        }
        return ballPos;
      }
      float y2 = Mathf.Clamp(y1, throwingConfig.MinTargetHeight, throwingConfig.MaxTargetHeight);
      return this.EvaluatePosition(time).SetY(y2);
    }

    public bool IsTargetValid(float timeOffset = 0.0f) => true;

    public bool TargetValidForAI => true;

    public bool IsPlayer() => false;

    public bool ReceiveBall(EventData eventData) => true;

    public float minCatchTime => 0.5f;

    public string TargetName => this._playerAI.transform.parent.gameObject.name;

    public float hitRange => 1f;

    public PlayerAI GetPlayerScript() => this._playerAI;

    public void SetPriority(bool priority)
    {
    }

    public bool IsPriorityTarget()
    {
      EligibilityManager instance = EligibilityManager.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
      {
        ReceiverUI currentTarget = instance.GetCurrentTarget();
        if ((UnityEngine.Object) currentTarget != (UnityEngine.Object) null && (UnityEngine.Object) currentTarget.GetComponent<PlayerAI>() == (UnityEngine.Object) this._playerAI)
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

    public void GetReplayData(out ThrowReplayData data)
    {
      data = new ThrowReplayData();
      data.targetIndex = this._playerAI.indexInFormation;
      data.targetPosition = this._playerAI.trans.position;
    }

    public Vector3 GetIdealThrowTarget() => this._playerAI.trans.position + this._playerAI.trans.forward * Mathf.Lerp(this._minBestTargetDist, 13f, Vector3.Distance(PlayerCamera.Camera.transform.position, this._playerAI.transform.position) / 44f);

    public void DrawRange()
    {
      Action<Transform> onDrawRange = AxisReceiver.OnDrawRange;
      if (onDrawRange == null)
        return;
      onDrawRange(this._playerAI.trans);
    }
  }
}
