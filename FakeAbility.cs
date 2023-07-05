// Decompiled with JetBrains decompiler
// Type: FakeAbility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public class FakeAbility : MonoBehaviour
{
  private PlayerAI _ownerPlayer;
  private PlayerAI _fakeTarget;
  private float _fakeTimerCurValue;
  private float _fakeTimerMaxValue;
  public float fakeChance = 0.25f;
  public float fakeTime = 1f;

  private void Awake()
  {
    this._ownerPlayer = this.GetComponent<PlayerAI>();
    this.Reset();
    PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);
  }

  private void Reset()
  {
    this._fakeTarget = (PlayerAI) null;
    this._fakeTimerCurValue = 0.0f;
    this._fakeTimerMaxValue = 0.0f;
  }

  private void OnDestroy() => PEGameplayEventManager.OnEventOccurred -= new Action<PEGameplayEvent>(this.HandleGameEvent);

  public bool IsFakedOut() => (double) this._fakeTimerCurValue > 0.0;

  public PlayerAI GetFakeTarget(PlayerAI targetIfNotFaked) => !this.IsFakedOut() ? targetIfNotFaked : this._fakeTarget;

  private void Update()
  {
    if (!this.IsFakedOut())
      return;
    this._fakeTimerCurValue -= Time.deltaTime;
    if ((double) this._fakeTimerCurValue > 0.0)
      return;
    this._fakeTimerCurValue = 0.0f;
    this._fakeTarget = (PlayerAI) null;
  }

  private void HandleGameEvent(PEGameplayEvent e)
  {
    if ((UnityEngine.Object) this._ownerPlayer == (UnityEngine.Object) null)
      return;
    switch (e)
    {
      case PEBallHandoffEvent ballHandoffEvent:
        if (this._ownerPlayer.onOffense || !ballHandoffEvent.IsFake || (double) UnityEngine.Random.Range(0.0f, 1f) >= (double) this.fakeChance)
          break;
        this._fakeTarget = ballHandoffEvent.Receiver;
        this._fakeTimerMaxValue = this.fakeTime;
        this._fakeTimerCurValue = this._fakeTimerMaxValue;
        break;
      case PEPlayOverEvent _:
        this.Reset();
        break;
    }
  }

  private void OnDrawGizmos()
  {
    if (!this.IsFakedOut())
      return;
    Vector3 vector3 = Vector3.Lerp(this.transform.position, this._fakeTarget.transform.position, this._fakeTimerCurValue / this._fakeTimerMaxValue);
    Gizmos.color = Color.magenta;
    Gizmos.DrawLine(this.transform.position, vector3);
    Gizmos.color = Color.black;
    Gizmos.DrawLine(vector3, this._fakeTarget.transform.position);
  }
}
