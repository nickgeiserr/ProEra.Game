// Decompiled with JetBrains decompiler
// Type: FootballVR.PlayerThrowTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace FootballVR
{
  public class PlayerThrowTarget : IThrowTarget
  {
    private readonly BehaviourController _controller;
    private readonly PlaybackInfo _playbackInfo;
    private readonly TimeSlot[] _openings;
    private bool _isPriority;
    private const float timeDiscount = 0.75f;

    public bool IsTargetValid(float timeOffset = 0.0f)
    {
      if (this._openings == null)
        return this._controller.EvaluatePassEligibility(this._playbackInfo.PlayTime);
      float playTime = this._playbackInfo.PlayTime;
      bool flag = false;
      for (int index = 0; index < this._openings.Length; ++index)
      {
        TimeSlot opening = this._openings[index];
        if ((double) playTime > (double) opening.startTime && (double) playTime < (double) opening.endTime)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public bool TargetValidForAI => true;

    public PlayerThrowTarget(
      BehaviourController controller,
      PlaybackInfo playbackInfo,
      TimeSlot[] openings = null)
    {
      this._controller = controller;
      this._playbackInfo = playbackInfo;
      this._openings = openings;
    }

    public PlayerThrowTarget(
      BehaviourController controller,
      PlaybackInfo playbackInfo,
      float openingStart)
      : this(controller, playbackInfo, new TimeSlot[1]
      {
        new TimeSlot(openingStart, float.MaxValue)
      })
    {
    }

    public Vector3 EvaluatePosition(float time) => this._controller.Evaluate3DPositionMeters(time * 0.75f);

    public bool ReceiveBall(EventData eventData)
    {
      this._controller.CatchBall(eventData);
      return true;
    }

    public float minCatchTime => 0.5f;

    public string TargetName => this._controller.gameObject.name;

    public float hitRange => 1f;

    public Vector3 GetHitPosition(float time, Vector3 ballPos)
    {
      float y = Mathf.Clamp(ballPos.y, 0.7f, 2.3f);
      return this.EvaluatePosition(time).SetY(y);
    }

    public void SetPriority(bool priority) => this._isPriority = priority;

    public bool IsPriorityTarget() => this._isPriority;

    public bool IsPlayer() => false;

    public void GetReplayData(out ThrowReplayData data) => data = new ThrowReplayData();

    public Vector3 GetIdealThrowTarget() => Vector3.zero;

    public void DrawRange()
    {
    }
  }
}
