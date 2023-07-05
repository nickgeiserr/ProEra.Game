// Decompiled with JetBrains decompiler
// Type: FootballVR.RepositioningTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

namespace FootballVR
{
  public class RepositioningTarget : TargetsHandler
  {
    [SerializeField]
    private TimedTarget _timedTarget;
    [SerializeField]
    private OrientToUser _orientation;
    [SerializeField]
    private Vector3 _positionRange;
    [SerializeField]
    private float _minDistance = 3f;
    private readonly RoutineHandle _reappearRoutine = new RoutineHandle();
    private Vector3 _initialPos;

    private void Awake()
    {
      this._initialPos = this.transform.position;
      this._timedTarget.Target.OnHit += new System.Action(this.HandleHit);
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) this._timedTarget != (UnityEngine.Object) null) || !(bool) (UnityEngine.Object) this._timedTarget.Target)
        return;
      this._timedTarget.Target.OnHit -= new System.Action(this.HandleHit);
    }

    public override void SetState(bool state)
    {
      if (state)
      {
        this.gameObject.SetActive(false);
        this.transform.position = this._initialPos;
        this._orientation.Apply();
        this.gameObject.SetActive(true);
        this._timedTarget.Show();
      }
      else
      {
        this._reappearRoutine.Stop();
        this._timedTarget.Target.SetTimeslot(new TimeSlot(0.0f, 0.0f));
        this._timedTarget.Hide();
      }
    }

    public override void ShowForSeconds(float seconds, bool showText = true) => this._timedTarget.ShowForSeconds(seconds, showText);

    private void HandleHit()
    {
      if (this._reappearRoutine.running)
        return;
      this._timedTarget.Target.SetTimeslot(new TimeSlot(0.0f, 0.0f));
      this._timedTarget.ShowForSeconds(2.5f, false);
      this._reappearRoutine.Run(this.Reappear());
    }

    private IEnumerator Reappear()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      RepositioningTarget repositioningTarget = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        repositioningTarget.gameObject.SetActive(false);
        repositioningTarget.transform.position = repositioningTarget.GetRandomPos();
        repositioningTarget._orientation.Apply();
        repositioningTarget.gameObject.SetActive(true);
        repositioningTarget._timedTarget.Show();
        repositioningTarget._reappearRoutine.Stop();
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) new WaitForSeconds(3.7f);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    private Vector3 GetRandomPos()
    {
      Vector3 position = this.transform.position;
      int num = 0;
      Vector3 randomPos;
      do
      {
        ++num;
        randomPos = this._initialPos + Vector3.zero with
        {
          x = UnityEngine.Random.Range(-this._positionRange.x, this._positionRange.x),
          z = UnityEngine.Random.Range(-this._positionRange.z, this._positionRange.z)
        };
      }
      while ((double) (randomPos - position).magnitude < (double) this._minDistance && num < 10);
      return randomPos;
    }
  }
}
