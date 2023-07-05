// Decompiled with JetBrains decompiler
// Type: FootballVR.Sequences.CollisionEffect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using UnityEngine;

namespace FootballVR.Sequences
{
  public class CollisionEffect : MonoBehaviour
  {
    [SerializeField]
    private Color _hitFlashColor = Color.gray;
    [SerializeField]
    private AnimationCurve _colorLerpCurve;
    private readonly AnimationCurve ShakeCurve = AnimationCurve.EaseInOut(0.0f, 1f, 1f, 0.0f);
    private readonly RoutineHandle _hitEffectRoutine = new RoutineHandle();
    private VRColorAdjustments _colorAdjustments;
    private bool _running;

    private CollisionSettings _collisionSettings => ScriptableSingleton<CollisionSettings>.Instance;

    public event System.Action OnFallDown;

    private void Awake()
    {
      this._colorAdjustments = PersistentSingleton<GamePlayerController>.Instance.ColorAdjustments;
      this._colorAdjustments.Initialize();
      this.SetPostEffects(false);
    }

    public void Stop()
    {
      if (!this._running)
        return;
      this._hitEffectRoutine.Stop();
      Transform transform = PersistentSingleton<GamePlayerController>.Instance.Rig.transform;
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      if ((UnityEngine.Object) this._colorAdjustments != (UnityEngine.Object) null)
        this._colorAdjustments.colorFilter = Color.white;
      this.SetPostEffects(false);
    }

    public void Run(bool fall = false) => this.Run(Vector3.zero, fall);

    public void Run(Vector3 pushVector, bool fall = false)
    {
      this._running = true;
      CollisionEffectSettings settings = fall ? (CollisionEffectSettings) this._collisionSettings.fallEffect : this._collisionSettings.collisionEffect;
      ControllerUtilities.InteractionHaptics2Hands(false, 0.5f);
      this._hitEffectRoutine.Run(this.HitEffectRoutine(settings));
      this._hitEffectRoutine.RunAdditive(this.FadeEffectRoutine(settings, pushVector));
    }

    public void RunWithFall(Vector3 dir) => this.RunWithFall(dir, Vector3.zero);

    public void RunWithFall(Vector3 dir, Vector3 push)
    {
      this.Run(push, true);
      this._hitEffectRoutine.RunAdditive(this.ShakeRoutine(dir));
    }

    private void SetPostEffects(bool state)
    {
      if ((UnityEngine.Object) this._colorAdjustments == (UnityEngine.Object) null)
        return;
      this._colorAdjustments.active = state;
      this._colorAdjustments.colorFilter = Color.white;
    }

    private IEnumerator ShakeRoutine(Vector3 direction)
    {
      CollisionEffect collisionEffect = this;
      FallEffect settings = collisionEffect._collisionSettings.fallEffect;
      direction = Vector3.Lerp(direction.normalized, Vector3.down, settings.LerpDownFactor);
      if (settings.shakeEnabled)
      {
        float elapsed = 0.0f;
        Transform rigTx = PersistentSingleton<GamePlayerController>.Instance.Rig.transform;
        Vector3 originalCamRotation = rigTx.rotation.eulerAngles;
        float time = 0.0f;
        float randomStart = UnityEngine.Random.Range(-1000f, 1000f);
        float distanceDamper = 1f - Mathf.Clamp01((rigTx.position - collisionEffect.transform.position).magnitude / settings.DistanceForce);
        Vector3 oldRotation = Vector3.zero;
        while ((double) elapsed < (double) settings.Duration)
        {
          elapsed += Time.deltaTime;
          float time1 = (float) ((double) elapsed / (double) settings.Duration / 2.0);
          float num1 = collisionEffect.ShakeCurve.Evaluate(time1) * distanceDamper;
          time += Time.deltaTime * num1;
          rigTx.position -= direction * (Time.deltaTime * Mathf.Sin(time * settings.Speed) * num1 * settings.Magnitude) / 2f;
          float num2 = randomStart + (float) ((double) settings.Speed * (double) time1 / 10.0);
          float num3 = (float) ((double) Mathf.PerlinNoise(num2, 0.0f) * 2.0 - 1.0);
          float num4 = (float) ((double) Mathf.PerlinNoise(1000f + num2, num2 + 1000f) * 2.0 - 1.0);
          float num5 = (float) ((double) Mathf.PerlinNoise(0.0f, num2) * 2.0 - 1.0);
          if (Quaternion.Euler(originalCamRotation + oldRotation) != rigTx.rotation)
            originalCamRotation = rigTx.rotation.eulerAngles;
          oldRotation = new Vector3(0.5f + num4, 0.3f + num3, 0.3f + num5) * (Mathf.Sin(time * settings.Speed) * num1 * settings.Magnitude * settings.RotationDamper);
          rigTx.rotation = Quaternion.Euler(originalCamRotation + oldRotation);
          yield return (object) null;
        }
        rigTx.localPosition = Vector3.zero;
        rigTx.localRotation = Quaternion.identity;
        rigTx = (Transform) null;
        originalCamRotation = new Vector3();
        oldRotation = new Vector3();
      }
      if (settings.fallEnabled)
      {
        Transform transform = PersistentSingleton<GamePlayerController>.Instance.transform;
        transform.RotateAround(transform.position, -Vector3.Cross(-direction, Vector3.up), -collisionEffect._collisionSettings.fallEffect.Angle);
        System.Action onFallDown = collisionEffect.OnFallDown;
        if (onFallDown != null)
          onFallDown();
      }
    }

    private IEnumerator FadeEffectRoutine(CollisionEffectSettings settings, Vector3 pushVector)
    {
      yield return (object) GamePlayerController.CameraFade.FadeToColor(settings.fadeOutDuration, this._hitFlashColor);
      PersistentSingleton<GamePlayerController>.Instance.position += pushVector;
      if ((double) settings.fadeDelay > 0.0)
        yield return (object) new WaitForSeconds(settings.fadeDelay);
      yield return (object) GamePlayerController.CameraFade.Clear(settings.fadeInDuration, fromBlack: false);
    }

    private IEnumerator HitEffectRoutine(CollisionEffectSettings settings)
    {
      if ((UnityEngine.Object) this._colorAdjustments == (UnityEngine.Object) null)
      {
        Debug.LogError((object) "Color Adj ref missing");
      }
      else
      {
        this.SetPostEffects(true);
        float startTime = Time.time;
        float endTime = startTime + settings.hitEffectDuration;
        while ((double) Time.time < (double) endTime)
        {
          this._colorAdjustments.colorFilter = Color.Lerp(Color.white, Color.red, this._colorLerpCurve.Evaluate(Mathf.InverseLerp(startTime, endTime, Time.time)));
          yield return (object) null;
        }
        this._colorAdjustments.colorFilter = Color.white;
        this.SetPostEffects(false);
      }
    }
  }
}
