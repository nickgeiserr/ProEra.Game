// Decompiled with JetBrains decompiler
// Type: FootballVR.Sequences.TwoHandedGrabSequence
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR.Sequences
{
  public class TwoHandedGrabSequence
  {
    private readonly HandsDataModel _data;
    private readonly RoutineHandle _grabRoutine = new RoutineHandle();
    private BallObject _ball;

    public TwoHandedGrabSequence(HandsDataModel data) => this._data = data;

    public void Run(BallObject ball, float radius)
    {
      this._ball = ball;
      this._grabRoutine.Run(this.TwoHandedGrabRoutine(radius));
    }

    private IEnumerator TwoHandedGrabRoutine(float radius)
    {
      float scale = (bool) (VariableBool) VRState.BigSizeMode ? VRState.BigSizeScale : 1f;
      if ((Object) this._ball == (Object) null)
      {
        Debug.LogError((object) "TwoHandedGrab: No ball");
      }
      else
      {
        EHandPose pose1 = TwoHandedGrabSequence.GetPose(this._ball);
        this._ball.Pick(true);
        List<HandData> HandDatas = this._data.HandDatas;
        if (HandDatas.Count < 2)
        {
          this._ball.Release();
        }
        else
        {
          foreach (HandData handData in HandDatas)
            handData.pose.Value = pose1;
          PlayerRig rig = PersistentSingleton<GamePlayerController>.Instance.Rig;
          TwoHandedPose pose = this._data.GetTwoHandedPose(pose1);
          HandController hand1 = this._data.GetHand(EHand.Left).hand;
          HandController hand2 = this._data.GetHand(EHand.Right).hand;
          if ((Object) hand1 == (Object) null || (Object) hand2 == (Object) null)
          {
            Debug.LogError((object) "Could not find left / right hand...");
            this._ball.Release();
          }
          else
          {
            Transform leftHandTx = hand1.transform;
            Transform rightHandTx = hand2.transform;
            Transform ballTx = this._ball.transform;
            pose.transform.SetParentAndReset(ballTx);
            pose.transform.localRotation = Quaternion.Euler(-90f, 0.0f, 0.0f);
            leftHandTx.SetParentAndReset(pose.LeftHand);
            rightHandTx.SetParentAndReset(pose.RightHand);
            VariableBool t1 = HandDatas[0].input.objectInteractPressed;
            VariableBool t2 = HandDatas[1].input.objectInteractPressed;
            Vector3 leftHandBallPos = leftHandTx.InverseTransformPoint(ballTx.position);
            Vector3 leftHandBallDir = leftHandTx.InverseTransformDirection(ballTx.forward);
            Vector3 leftHandBallDirUp = leftHandTx.InverseTransformDirection(ballTx.up);
            Vector3 rightHandBallPos = rightHandTx.InverseTransformPoint(ballTx.position);
            Vector3 rightHandBallDir = rightHandTx.InverseTransformDirection(ballTx.forward);
            Vector3 rightHandBallDirUp = rightHandTx.InverseTransformDirection(ballTx.up);
            radius = Mathf.Max(radius, (rig.rightControllerAnchor.position - rig.leftControllerAnchor.position).magnitude * 1.1f);
            HandData grippingHand = (HandData) null;
            while (t1.Value && t2.Value)
            {
              Vector3 rhs = rig.rightControllerAnchor.position - rig.leftControllerAnchor.position;
              if ((double) rhs.magnitude > (double) radius)
              {
                grippingHand = this._data.ActiveHand;
                break;
              }
              Vector3 vector3_1 = rig.leftControllerAnchor.TransformPoint(leftHandBallPos);
              Vector3 vector3_2 = rig.rightControllerAnchor.TransformPoint(rightHandBallPos);
              Vector3 vector3_3 = rig.leftControllerAnchor.TransformDirection(leftHandBallDir);
              Vector3 vector3_4 = rig.rightControllerAnchor.TransformDirection(rightHandBallDir);
              Vector3 vector3_5 = rig.leftControllerAnchor.TransformDirection(leftHandBallDirUp);
              Vector3 vector3_6 = rig.rightControllerAnchor.TransformDirection(rightHandBallDirUp);
              Vector3 vector3_7 = vector3_2;
              Vector3 vector3_8 = (vector3_1 + vector3_7) / 2f;
              Vector3 lhs = vector3_3.normalized + vector3_4.normalized;
              Vector3 worldUp = vector3_5.normalized + vector3_6.normalized;
              if (!((Object) ballTx == (Object) null))
              {
                ballTx.position = vector3_8;
                ballTx.SetScale(scale);
                if (this._data.settings.TwoHandedSettings.twoHandedV2)
                  worldUp = Vector3.Cross(lhs, rhs);
                ballTx.LookAt(vector3_8 + lhs, worldUp);
                yield return (object) null;
              }
              else
                break;
            }
            foreach (HandData handData in HandDatas)
            {
              if (grippingHand == null && (bool) handData.input.objectInteractPressed)
                grippingHand = handData;
              else if (handData != grippingHand)
                handData.pose.Value = EHandPose.Empty;
            }
            leftHandTx.SetParentAndReset(rig.leftControllerAnchor);
            rightHandTx.SetParentAndReset(rig.rightControllerAnchor);
            if (grippingHand == null)
              this._ball.Release();
            else
              grippingHand.CurrentObject = this._ball;
            if ((Object) pose != (Object) null)
              pose.transform.SetParentAndReset((Transform) null);
            Debug.Log((object) "Grabbed ball with two hands");
          }
        }
      }
    }

    private static EHandPose GetPose(BallObject ball)
    {
      if (!ball.inFlight)
        return EHandPose.AboveCatch;
      Transform centerEyeAnchor = PersistentSingleton<GamePlayerController>.Instance.Rig.centerEyeAnchor;
      return (double) ball.transform.position.y <= (double) centerEyeAnchor.position.y - 0.30000001192092896 ? EHandPose.BelowCatch : EHandPose.AboveCatch;
    }

    public void Stop()
    {
      if (!this._grabRoutine.running || UnityState.quitting)
        return;
      this._grabRoutine.Stop();
      PlayerRig rig = PersistentSingleton<GamePlayerController>.Instance.Rig;
      HandData hand1 = this._data.GetHand(EHand.Left);
      HandData hand2 = this._data.GetHand(EHand.Right);
      hand1.hand.transform.SetParentAndReset(rig.leftControllerAnchor);
      hand2.hand.transform.SetParentAndReset(rig.rightControllerAnchor);
      if ((Object) this._ball != (Object) null && (Object) this._ball.gameObject != (Object) null && this._ball.twoHandedGrab)
        this._ball.Release();
      this._ball = (BallObject) null;
    }

    public bool HasBall(BallObject ball) => (Object) this._ball == (Object) ball;
  }
}
