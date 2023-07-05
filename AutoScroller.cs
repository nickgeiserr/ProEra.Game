// Decompiled with JetBrains decompiler
// Type: AutoScroller
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Events;

public class AutoScroller : MonoBehaviour
{
  [SerializeField]
  private Transform ScrollTarget;
  [SerializeField]
  private Vector3 ScrollDirection = Vector3.up;
  [SerializeField]
  private float ScrollSpeed = 1f;
  [SerializeField]
  private float ScrollDistance = 400f;
  [SerializeField]
  private float ScrollBeginDelay = 1f;
  [SerializeField]
  private float ScrollEndDelay = 1f;
  [SerializeField]
  private UnityEvent OnScrollEndEvent;
  private float ScrollTimer;
  private float ScrollEndTimer;

  private float ScrollProg => this.ScrollTimer * this.ScrollSpeed / this.ScrollDistance;

  private void ResetTimer() => this.ScrollTimer = -this.ScrollBeginDelay;

  private void OnEnable() => this.ResetTimer();

  private void Update()
  {
    this.ScrollTimer += Time.deltaTime;
    if ((double) this.ScrollProg <= 1.0)
    {
      this.SetTargetPosition(this.ScrollProg);
    }
    else
    {
      this.ScrollEndTimer += Time.deltaTime;
      if ((double) this.ScrollEndTimer < (double) this.ScrollEndDelay)
        return;
      this.OnScrollEndEvent?.Invoke();
      this.ScrollEndTimer = 0.0f;
      this.ResetTimer();
    }
  }

  public void SetTargetPosition(float a_val)
  {
    if ((double) a_val < 0.0)
      a_val = 0.0f;
    Vector3 normalized = this.ScrollDirection.normalized;
    this.ScrollTarget.transform.localPosition = a_val * this.ScrollDistance * normalized;
  }
}
