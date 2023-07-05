// Decompiled with JetBrains decompiler
// Type: FootballVR.IThrowTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  public interface IThrowTarget
  {
    Vector3 EvaluatePosition(float time);

    Vector3 GetHitPosition(float time, Vector3 ballPos);

    bool IsTargetValid(float timeOffset = 0.0f);

    bool TargetValidForAI { get; }

    bool ReceiveBall(EventData eventData);

    float minCatchTime { get; }

    string TargetName { get; }

    float hitRange { get; }

    bool IsPriorityTarget();

    bool IsPlayer();

    void SetPriority(bool priority);

    void GetReplayData(out ThrowReplayData data);

    Vector3 GetIdealThrowTarget();

    void DrawRange();
  }
}
