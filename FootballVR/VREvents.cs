// Decompiled with JetBrains decompiler
// Type: FootballVR.VREvents
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using Vars;

namespace FootballVR
{
  [RuntimeState]
  public static class VREvents
  {
    public static readonly AppEvent PlayerPositionUpdated = new AppEvent();
    public static readonly AppEvent UpdateUI = new AppEvent();
    public static readonly AppEvent SpawnBall = new AppEvent();
    public static readonly AppEvent<bool, float> ThrowResult = new AppEvent<bool, float>();
    public static readonly AppEvent<bool, float> TargetHit = new AppEvent<bool, float>();
    public static readonly AppEvent GetUp = new AppEvent();
    public static readonly AppEvent PlayerHitGround = new AppEvent();
    public static readonly AppEvent<Vector3, bool> PlayerPushed = new AppEvent<Vector3, bool>();
    public static readonly AppEvent<Vector3> PlayerKnockdown = new AppEvent<Vector3>();
    public static readonly AppEvent<GameObject> PlayerCollision = new AppEvent<GameObject>();
    public static readonly AppEvent<Collider> UserCollision = new AppEvent<Collider>();
    public static readonly AppEvent AdjustPlayerHeight = new AppEvent();
    public static readonly AppEvent FootstepTaken = new AppEvent();
    public static readonly AppEvent DropBall = new AppEvent();
    public static readonly AppEvent<float, Vector3, Quaternion> BlinkMovePlayer = new AppEvent<float, Vector3, Quaternion>();
  }
}
