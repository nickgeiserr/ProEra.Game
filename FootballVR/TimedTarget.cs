// Decompiled with JetBrains decompiler
// Type: FootballVR.TimedTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using TMPro;
using UnityEngine;

namespace FootballVR
{
  public class TimedTarget : TargetsHandler
  {
    [SerializeField]
    private GameObject _countdownObj;
    [SerializeField]
    private TextMeshProUGUI _countdownText;
    [SerializeField]
    private PracticeTarget _target;
    [SerializeField]
    private bool _hideOnHit = true;
    [SerializeField]
    private SpawnEffect _spawnEffect;
    [SerializeField]
    private bool _useCountdown;
    private bool _shown;
    private readonly RoutineHandle _countdownRoutine = new RoutineHandle();
    private bool _hasCountdownText;

    public bool Shown => this._shown;

    public PracticeTarget Target => this._target;

    private void Awake()
    {
      this._hasCountdownText = (UnityEngine.Object) this._countdownObj != (UnityEngine.Object) null && (UnityEngine.Object) this._countdownText != (UnityEngine.Object) null;
      if ((UnityEngine.Object) this._countdownObj != (UnityEngine.Object) null)
        this._countdownObj.SetActive(false);
      this._target.OnHit += new System.Action(this.HandleHit);
      this._spawnEffect.Initialize();
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) this._target != (UnityEngine.Object) null))
        return;
      this._target.OnHit -= new System.Action(this.HandleHit);
    }

    private void OnDisable()
    {
      this._countdownRoutine.Stop();
      this._shown = false;
    }

    public override void SetState(bool state)
    {
      if (this._shown == state)
        return;
      this.gameObject.SetActive(true);
      this._spawnEffect.Visible = state;
      this._shown = state;
    }

    private void HandleHit()
    {
      if (!this.Shown || !this._hideOnHit)
        return;
      this._countdownRoutine.Run(this.CountdownRoutine(0.75f, false));
      if (!this._hasCountdownText)
        return;
      this._countdownObj.SetActive(false);
    }

    public void Show() => this.SetState(true);

    public override void ShowForSeconds(float seconds, bool showText = false)
    {
      this.Show();
      this._countdownRoutine.Run(this.CountdownRoutine(seconds, showText));
    }

    public void Hide()
    {
      this.SetState(false);
      if (this._hasCountdownText)
        this._countdownObj.SetActive(false);
      this._countdownRoutine.Stop();
    }

    private IEnumerator CountdownRoutine(float delay, bool showText)
    {
      showText &= this._hasCountdownText;
      int secondsRemaining = Mathf.CeilToInt(delay);
      if (showText)
      {
        this._countdownObj.SetActive(true);
        this._countdownText.text = secondsRemaining.ToString();
      }
      while ((double) delay > 0.0)
      {
        delay -= Time.deltaTime;
        if (showText && secondsRemaining > (int) delay)
        {
          secondsRemaining = (int) delay;
          this._countdownText.text = (secondsRemaining + 1).ToString();
        }
        yield return (object) null;
      }
      this.Hide();
    }
  }
}
