// Decompiled with JetBrains decompiler
// Type: FootballVR.LoopedTimedTargets
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public class LoopedTimedTargets : TargetsHandler
  {
    [SerializeField]
    private TimedTarget[] _targets;
    [SerializeField]
    private ReceiversHighlighter _highlighter;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private float _minValidTime = 4f;
    [SerializeField]
    private float _delay = 11f;
    private readonly RoutineHandle _loopRoutine = new RoutineHandle();
    private bool _hiding;

    private void OnEnable() => this._hiding = false;

    private void OnDestroy() => this._loopRoutine.Stop();

    private void OnDisable() => this._loopRoutine.Stop();

    public override void SetState(bool state)
    {
      if (state)
      {
        this._hiding = false;
        this._loopRoutine.Run(this.LoopRoutine());
      }
      else
      {
        this._loopRoutine.Stop();
        this.HideAll();
      }
    }

    public override void ShowForSeconds(float seconds, bool showText = true) => this._hiding = true;

    private void HideAll()
    {
      this._highlighter.Stop();
      foreach (TimedTarget target in this._targets)
      {
        target.Target.SetTargetValid(false);
        target.Hide();
      }
    }

    private IEnumerator LoopRoutine()
    {
      foreach (Component target in this._targets)
        target.gameObject.SetActive(false);
      yield return (object) new WaitForSeconds(1f);
      Queue<TimedTarget> targetQueue = new Queue<TimedTarget>((IEnumerable<TimedTarget>) this._targets);
      while (!this._hiding)
      {
        TimedTarget timedTarget = targetQueue.Dequeue();
        targetQueue.Enqueue(timedTarget);
        timedTarget.gameObject.SetActive(true);
        timedTarget.ShowForSeconds(this._delay, false);
        TimeSlot timeslot = new TimeSlot(this._playbackInfo.PlayTime + this._minValidTime, (float) ((double) this._playbackInfo.PlayTime + (double) this._delay - 1.0));
        timedTarget.Target.SetTimeslot(new TimeSlot(this._minValidTime, this._delay));
        yield return (object) new WaitForSeconds(1.2f);
        ReceiversHighlighter highlighter = this._highlighter;
        List<Transform> targets = new List<Transform>();
        targets.Add(timedTarget.Target.transform);
        TimeSlot[] receiverOpenings = new TimeSlot[1]
        {
          timeslot
        };
        highlighter.HighlightReceivers(targets, receiverOpenings);
        if ((double) this._minValidTime > 1.2000000476837158)
          yield return (object) new WaitForSeconds(this._minValidTime - 1.2f);
        timedTarget.Target.SetTargetValid(true);
        yield return (object) new WaitForSeconds(this._delay - this._minValidTime);
        this._highlighter.Stop();
        yield return (object) new WaitForSeconds(1.6f);
        timedTarget.Target.SetTargetValid(false);
        timedTarget.gameObject.SetActive(false);
        timedTarget = (TimedTarget) null;
      }
    }
  }
}
