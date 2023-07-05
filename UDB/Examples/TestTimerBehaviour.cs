// Decompiled with JetBrains decompiler
// Type: UDB.Examples.TestTimerBehaviour
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace UDB.Examples
{
  public class TestTimerBehaviour : MonoBehaviour
  {
    [Header("Controls")]
    public InputField DurationField;
    public Button StartTimerButton;
    public Button CancelTimerButton;
    public Button PauseTimerButton;
    public Button ResumeTimerButton;
    public Toggle IsLoopedToggle;
    public Toggle UseGameTimeToggle;
    public Slider TimescaleSlider;
    public Text NeedsRestartText;
    [Header("Stats Texts")]
    public Text TimeElapsedText;
    public Text TimeRemainingText;
    public Text PercentageCompletedText;
    public Text PercentageRemainingText;
    public Text NumberOfLoopsText;
    public Text IsCancelledText;
    public Text IsCompletedText;
    public Text IsPausedText;
    public Text IsDoneText;
    public Text UpdateText;
    private int _numLoops;
    private Timer _testTimer;

    private void Awake() => this.ResetState();

    private void ResetState()
    {
      this._numLoops = 0;
      this.CancelTestTimer();
    }

    public void StartTestTimer()
    {
      this.ResetState();
      this._testTimer = Timer.Register(this.GetDurationValue(), (System.Action) (() => ++this._numLoops), (Action<float>) (secondsElapsed => this.UpdateText.text = string.Format("Timer ran update callback: {0:F2} seconds", (object) secondsElapsed)), this.IsLoopedToggle.isOn, !this.UseGameTimeToggle.isOn);
      this.CancelTimerButton.interactable = true;
    }

    public void CancelTestTimer()
    {
      Timer.Cancel(this._testTimer);
      this.CancelTimerButton.interactable = false;
      this.NeedsRestartText.gameObject.SetActive(false);
    }

    public void PauseTestTimer() => Timer.Pause(this._testTimer);

    public void ResumeTestTimer() => Timer.Resume(this._testTimer);

    private void Update()
    {
      if (this._testTimer == null)
        return;
      Time.timeScale = this.TimescaleSlider.value;
      this._testTimer.isLooped = this.IsLoopedToggle.isOn;
      this.TimeElapsedText.text = string.Format("Time elapsed: {0:F2} seconds", (object) this._testTimer.GetTimeElapsed());
      this.TimeRemainingText.text = string.Format("Time remaining: {0:F2} seconds", (object) this._testTimer.GetTimeRemaining());
      this.PercentageCompletedText.text = string.Format("Percentage completed: {0:F4}%", (object) (float) ((double) this._testTimer.GetRatioComplete() * 100.0));
      this.PercentageRemainingText.text = string.Format("Percentage remaining: {0:F4}%", (object) (float) ((double) this._testTimer.GetRatioRemaining() * 100.0));
      this.NumberOfLoopsText.text = string.Format("# Loops: {0}", (object) this._numLoops);
      this.IsCancelledText.text = string.Format("Is Cancelled: {0}", (object) this._testTimer.isCancelled);
      this.IsCompletedText.text = string.Format("Is Completed: {0}", (object) this._testTimer.isCompleted);
      this.IsPausedText.text = string.Format("Is Paused: {0}", (object) this._testTimer.isPaused);
      this.IsDoneText.text = string.Format("Is Done: {0}", (object) this._testTimer.isDone);
      this.PauseTimerButton.interactable = !this._testTimer.isPaused;
      this.ResumeTimerButton.interactable = this._testTimer.isPaused;
      this.NeedsRestartText.gameObject.SetActive(this.ShouldShowRestartText());
    }

    private bool ShouldShowRestartText()
    {
      if (this._testTimer.isDone)
        return false;
      return this.UseGameTimeToggle.isOn && this._testTimer.usesRealTime || !this.UseGameTimeToggle.isOn && !this._testTimer.usesRealTime || (double) Mathf.Abs(this.GetDurationValue() - this._testTimer.duration) >= (double) Mathf.Epsilon;
    }

    private float GetDurationValue()
    {
      float result;
      return !float.TryParse(this.DurationField.text, out result) ? 0.0f : result;
    }
  }
}
