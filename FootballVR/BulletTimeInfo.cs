// Decompiled with JetBrains decompiler
// Type: FootballVR.BulletTimeInfo
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "BulletTimeInfo", menuName = "TB12/Core/BulletTimeInfo", order = 2)]
  public class BulletTimeInfo : ScriptableObject
  {
    public readonly AppEvent OnBulletTimeTransitionFinished = new AppEvent();
    [SerializeField]
    private float _defaultBulletTimeAmount = 0.5f;
    [SerializeField]
    private float _defaultTimeToShift = 1f;
    [SerializeField]
    private AudioClip _bulletTimeSlowdownClip;
    [SerializeField]
    private AudioClip _bulletTimeSpeedupClip;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private float _originalTimeScale;
    private float _currentTime;
    private AudioClip _bulletTimeClip;

    public float DefaultBulletTimeAmount
    {
      get => this._defaultBulletTimeAmount;
      set => this._defaultBulletTimeAmount = Mathf.Clamp01(value);
    }

    public float DefaultTimeToShift
    {
      get => this._defaultTimeToShift;
      set => this._defaultTimeToShift = value;
    }

    protected void OnEnable() => this.Reset();

    public void Reset()
    {
      this._routineHandle.Stop();
      Time.timeScale = (float) GameSettings.TimeScale;
      this._originalTimeScale = (float) GameSettings.TimeScale;
      this._currentTime = 0.0f;
      this._bulletTimeClip = (AudioClip) null;
    }

    public void EnterBulletTime() => this.RunCustomBulletTime(this.DefaultBulletTimeAmount, this.DefaultTimeToShift);

    public void ExitBulletTime() => this.RunCustomBulletTime((float) GameSettings.TimeScale, this.DefaultTimeToShift);

    public void RunCustomBulletTime(float bulletTimeAmount = 0.5f, float timeToShift = 1f, bool playAudio = true) => this._routineHandle.Run(this.BulletTimeRoutine(bulletTimeAmount, timeToShift, playAudio));

    private IEnumerator BulletTimeRoutine(float targetTimeScale, float timeToShift, bool playAudio)
    {
      if (!Time.timeScale.Equals(targetTimeScale))
      {
        this._originalTimeScale = Time.timeScale;
        this._currentTime = 0.0f;
        this._bulletTimeClip = (double) this._originalTimeScale < (double) targetTimeScale ? this._bulletTimeSlowdownClip : this._bulletTimeSpeedupClip;
        if ((double) this._currentTime <= (double) timeToShift)
        {
          this._currentTime += Time.deltaTime;
          Time.timeScale = Mathf.Lerp(this._originalTimeScale, targetTimeScale, this._currentTime / timeToShift);
          yield return (object) null;
        }
        Time.timeScale = targetTimeScale;
        this.OnBulletTimeTransitionFinished?.Trigger();
      }
    }
  }
}
