// Decompiled with JetBrains decompiler
// Type: PlayerLimbCollision
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PlayerLimbCollision : MonoBehaviour
{
  public PlayerCollisions playerCollisions;
  public int myIndex;

  private void Start()
  {
  }

  public void Initialize(int index, PlayerCollisions p)
  {
    this.myIndex = index;
    this.playerCollisions = p;
  }

  private void OnCollisionEnter(Collision collisionInfo) => this.playerCollisions.EnterCollision(collisionInfo, this.myIndex);

  private void OnCollisionExit(Collision collisionInfo)
  {
    if (collisionInfo.gameObject.layer != 12 && collisionInfo.gameObject.layer != 14)
      return;
    this.playerCollisions.OutCollision(collisionInfo, this.myIndex);
  }
}
