// Decompiled with JetBrains decompiler
// Type: FootballVR.RealPlayerTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace FootballVR
{
  public class RealPlayerTarget : IThrowTarget
  {
    private readonly Transform _tx;
    private readonly int _playerId;
    private readonly MotionTracker _motionTracker;
    private bool _isPriority;

    public RealPlayerTarget(Transform tx, int playerId, MotionTracker motionTracker)
    {
      this._tx = tx;
      this._playerId = playerId;
      this._motionTracker = motionTracker;
    }

    public Vector3 EvaluatePosition(float time)
    {
      try
      {
        Vector3 position = this._tx.position;
        if ((Object) this._motionTracker != (Object) null)
          position += this._motionTracker.Velocity * time;
        return position;
      }
      catch (MissingReferenceException ex)
      {
        return Vector3.zero;
      }
    }

    public Vector3 GetHitPosition(float time, Vector3 ballPos)
    {
      try
      {
        float y = Mathf.Clamp(ballPos.y, 0.7f, 1.5f) * this._tx.lossyScale.y;
        return this.EvaluatePosition(time).SetY(y);
      }
      catch (MissingReferenceException ex)
      {
        return Vector3.zero;
      }
    }

    public bool IsTargetValid(float timeOffset) => true;

    public bool TargetValidForAI => true;

    public bool ReceiveBall(EventData eventData) => true;

    public float minCatchTime { get; } = 0.45f;

    public string TargetName => string.Format("Player {0}", (object) this.playerId);

    public float hitRange => 1f;

    public int playerId => this._playerId;

    public void SetPriority(bool priority) => this._isPriority = priority;

    public bool IsPriorityTarget() => this._isPriority;

    public bool IsPlayer() => true;

    public void GetReplayData(out ThrowReplayData data) => data = new ThrowReplayData();

    public Vector3 GetIdealThrowTarget() => this._tx.position;

    public void DrawRange()
    {
    }
  }
}
