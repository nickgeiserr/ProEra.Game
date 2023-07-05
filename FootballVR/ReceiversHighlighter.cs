// Decompiled with JetBrains decompiler
// Type: FootballVR.ReceiversHighlighter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public class ReceiversHighlighter : MonoBehaviour
  {
    [SerializeField]
    private TargetReticle _reticlePrefab;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    private readonly RoutineHandle _highlightRoutine = new RoutineHandle();
    private ManagedList<TargetReticle> _aimTargets;
    private bool _initialized;
    private readonly TimeSlot _emptyTimeSlot = new TimeSlot(0.0f, 0.0f);

    private void Awake() => this.Initialize();

    public void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
      Transform transform = new GameObject("PlayerAimObjects").transform;
      transform.SetParent(this.transform);
      this._aimTargets = new ManagedList<TargetReticle>((IObjectPool<TargetReticle>) new MonoBehaviorObjectPool<TargetReticle>(this._reticlePrefab, transform, 1));
      this.enabled = false;
    }

    private void OnDestroy() => this.Stop();

    public void Stop()
    {
      this._highlightRoutine.Stop();
      this._aimTargets.SetCount(0);
    }

    public void HighlightReceivers(
      List<Transform> targets,
      TimeSlot[] receiverOpenings,
      float height = -1f)
    {
      this._highlightRoutine.Run(this.HighlightReceiversRoutine(targets, receiverOpenings, height));
    }

    public void HighlightReceivers(List<Transform> targets, float minValidTime, float height = -1f) => this._highlightRoutine.Run(this.HighlightReceiversRoutine(targets, new TimeSlot[1]
    {
      new TimeSlot(minValidTime, float.MaxValue)
    }, height));

    private IEnumerator HighlightReceiversRoutine(
      List<Transform> targets,
      TimeSlot[] receiverOpenings,
      float height)
    {
      this.Initialize();
      Transform camTx = PlayerCamera.Camera.transform;
      this._aimTargets.SetCount(targets.Count);
      bool overrideHeight = (double) height > 0.0;
      for (int index = 0; index < targets.Count; ++index)
      {
        this._aimTargets[index].gameObject.SetActive(true);
        Vector3 position = targets[index].transform.position;
        if (overrideHeight)
          position.y = height;
        Vector3 vector3 = position - camTx.position;
        this._aimTargets[index].transform.position = position - vector3.normalized * 0.1f;
        this._aimTargets[index].transform.LookAt(position + vector3, Vector3.up);
      }
      while (true)
      {
        float playTime = this._playbackInfo.PlayTime;
        TimeSlot timeSlot;
        EReticle state = this.GetTimeSlot(playTime, receiverOpenings, out timeSlot) ? ((double) playTime - (double) timeSlot.startTime >= 1.0 ? EReticle.Green : EReticle.Yellow) : EReticle.Red;
        for (int index = 0; index < targets.Count; ++index)
        {
          this._aimTargets[index].SetState(state);
          Vector3 position = targets[index].transform.position;
          if (overrideHeight)
            position.y = height;
          Vector3 vector3 = position - camTx.position;
          this._aimTargets[index].transform.LookAt(position + vector3, Vector3.up);
          this._aimTargets[index].transform.position = Vector3.Lerp(this._aimTargets[index].transform.position, position - vector3.normalized * 0.1f, 0.5f);
        }
        yield return (object) null;
      }
    }

    private bool GetTimeSlot(float playTime, TimeSlot[] timeSlots, out TimeSlot timeSlot)
    {
      for (int index = 0; index < timeSlots.Length; ++index)
      {
        timeSlot = timeSlots[index];
        if ((double) playTime > (double) timeSlot.startTime && (double) playTime < (double) timeSlot.endTime)
          return true;
      }
      timeSlot = this._emptyTimeSlot;
      return false;
    }

    public void HighlightReceivers(IReadOnlyList<Avatar> avatars) => this._highlightRoutine.Run(this.HighlightReceiversRoutine(avatars));

    private IEnumerator HighlightReceiversRoutine(IReadOnlyList<Avatar> avatars)
    {
      this.Initialize();
      if (avatars != null)
      {
        Transform camTx = PlayerCamera.Camera.transform;
        List<Transform> playersTx = new List<Transform>(avatars.Count);
        for (int index = 0; index < avatars.Count; ++index)
          playersTx.Add(avatars[index].transform);
        this._aimTargets.SetCount(playersTx.Count);
        float[] maxTimes = new float[avatars.Count];
        for (int index1 = 0; index1 < playersTx.Count; ++index1)
        {
          this._aimTargets[index1].gameObject.SetActive(true);
          Vector3 vector3_1 = playersTx[index1].transform.position.SetY(1.2f);
          Vector3 vector3_2 = vector3_1 - camTx.position;
          this._aimTargets[index1].transform.position = vector3_1 - vector3_2.normalized * 0.1f;
          this._aimTargets[index1].transform.LookAt(vector3_1 + vector3_2, Vector3.up);
          float num1 = 0.0f;
          int num2 = 0;
          Keyframe[] keys = avatars[index1].behaviourController.Scenario.eligibility.keys;
          for (int index2 = 0; index2 < keys.Length; ++index2)
          {
            Keyframe keyframe = keys[index2];
            if ((double) keyframe.value > 0.0 && (double) keyframe.time > (double) num1)
            {
              num1 = keyframe.time;
              num2 = index2;
            }
          }
          maxTimes[index1] = keys.Length <= num2 + 1 ? num1 : keys[num2 + 1].time;
        }
        while (true)
        {
          float playTime = this._playbackInfo.PlayTime;
          for (int index = 0; index < playersTx.Count; ++index)
          {
            if ((double) playTime > (double) maxTimes[index])
              this._aimTargets[index].SetState(EReticle.Faded);
            else if (avatars[index].behaviourController.EvaluatePassEligibility(playTime))
            {
              this._aimTargets[index].SetState(EReticle.Green);
            }
            else
            {
              bool flag = avatars[index].behaviourController.EvaluatePassEligibility(playTime + 1f) || avatars[index].behaviourController.EvaluatePassEligibility(playTime + 0.5f);
              this._aimTargets[index].SetState(flag ? EReticle.Yellow : EReticle.Red);
            }
            Vector3 vector3_3 = playersTx[index].transform.position.SetY(1.2f);
            Vector3 vector3_4 = vector3_3 - camTx.position;
            this._aimTargets[index].transform.LookAt(vector3_3 + vector3_4, Vector3.up);
            this._aimTargets[index].transform.position = Vector3.Lerp(this._aimTargets[index].transform.position, vector3_3 - vector3_4.normalized * 0.1f, 0.5f);
          }
          yield return (object) null;
        }
      }
    }
  }
}
