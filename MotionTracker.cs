// Decompiled with JetBrains decompiler
// Type: MotionTracker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MotionTracker : MonoBehaviour
{
  private Vector3 currentVelocity;
  private Vector3 currentAcceleration;
  private Vector3 previousPosition;
  private Vector3 previousVelocity;

  public Vector3 Velocity => this.currentVelocity;

  public Vector3 Acceleration => this.currentAcceleration;

  private void LateUpdate()
  {
    float deltaTime = Time.deltaTime;
    Vector3 vector3 = (this.transform.position - this.previousPosition) / deltaTime;
    this.previousVelocity = this.currentVelocity;
    this.currentVelocity = vector3;
    this.previousPosition = this.transform.position;
    this.currentAcceleration = (vector3 - this.previousVelocity) / deltaTime;
  }
}
