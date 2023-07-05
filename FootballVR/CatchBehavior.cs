// Decompiled with JetBrains decompiler
// Type: FootballVR.CatchBehavior
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  public class CatchBehavior : AvatarBehaviorState
  {
    private readonly IPlaybackInfo _playbackInfo;
    private readonly Vector3 _catchPosition;
    private readonly float _catchTime;
    private static readonly Vector3 runDirection = Vector3.right;
    private static readonly float runRotation = Quaternion.LookRotation(CatchBehavior.runDirection, Vector3.up).eulerAngles.y;

    public CatchBehavior(IPlaybackInfo playbackInfo, Vector3 catchPosition, float catchTime)
    {
      this._playbackInfo = playbackInfo;
      catchPosition.y = 0.0f;
      this._catchPosition = catchPosition;
      this._catchTime = catchTime;
    }

    public override Vector3 Evaluate3DPositionMeters(float time)
    {
      float num1 = this._playbackInfo.PlayTime + time;
      if ((double) num1 < (double) this._catchTime)
      {
        Vector3 playerPosition = this.playerPosition;
        Vector3 vector3 = this._catchPosition - playerPosition;
        float magnitude = vector3.magnitude;
        if ((double) magnitude < 0.079999998211860657)
          return this._catchPosition;
        float b = 5f * time;
        return playerPosition + Mathf.Min(magnitude, b) * vector3.normalized;
      }
      float num2 = Mathf.Clamp01((float) (((double) num1 - (double) this._catchTime) / 0.800000011920929));
      return this.playerPosition + (float) ((double) time * (double) num2 * 5.0) * CatchBehavior.runDirection;
    }

    public override float EvaluateOrientation(float time) => (double) this._playbackInfo.PlayTime + (double) time > (double) this._catchTime ? CatchBehavior.runRotation : Quaternion.LookRotation(this._catchPosition - this.playerPosition, Vector3.up).eulerAngles.y;
  }
}
