// Decompiled with JetBrains decompiler
// Type: FootballVR.VRCameraFade
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FootballVR
{
  public class VRCameraFade : MonoBehaviour
  {
    [SerializeField]
    private MeshRenderer _renderer;
    [SerializeField]
    private GameObject _vignette;
    [Header("Tackle Fade Settings")]
    public float durationToTackleFade = 0.1f;
    public float durationFromTackleFade = 0.1f;
    public Vector3 startTackleVignetteSize = new Vector3(0.2f, 0.2f, 0.2f);
    public Vector3 endTackleVignetteSize = new Vector3(0.1f, 0.1f, 0.1f);
    public float delayTackleFade;
    private int _shaderColorId;
    private MaterialPropertyBlock _mpb;
    private Color _currentColor = Color.black;
    private Vector3 _currentSize = Vector3.zero;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _fadeRoutine = new RoutineHandle();
    private readonly RoutineHandle _resizeRoutine = new RoutineHandle();

    public bool IsFadeRunning
    {
      get
      {
        RoutineHandle fadeRoutine = this._fadeRoutine;
        return fadeRoutine != null && fadeRoutine.running;
      }
    }

    public event Action<bool> OnFadeStateChanged;

    public event Action<bool> OnResizeStateChanged;

    private void Awake()
    {
      if ((UnityEngine.Object) this._renderer == (UnityEngine.Object) null)
        this._renderer = this.GetComponent<MeshRenderer>();
      this._shaderColorId = Shader.PropertyToID("_Color");
      this._mpb = new MaterialPropertyBlock();
      this._renderer.GetPropertyBlock(this._mpb);
      this._mpb.SetColor(this._shaderColorId, Color.black);
      this._renderer.SetPropertyBlock(this._mpb);
      this._renderer.sortingOrder = 3;
      this._renderer.enabled = false;
      this._vignette.SetActive(false);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        VREvents.PlayerCollision.Link<GameObject>((Action<GameObject>) (collisionObject => this.Resize_Boomerang(this.durationToTackleFade, this.durationFromTackleFade, this.startTackleVignetteSize, this.endTackleVignetteSize, this.delayTackleFade)))
      });
    }

    private void OnDestroy() => this._linksHandler.Clear();

    public bool IsActive() => this._renderer.enabled;

    public async Task FadeAsync(float duration = 0.35f, float delay = 0.0f, bool withLogo = false)
    {
      this.FadeToColor(duration, Color.black, delay, withLogo);
      await Task.Delay((int) (((double) duration + (double) delay) * 1000.0));
    }

    public Coroutine Fade(float duration = 0.35f, float delay = 0.0f, bool withLogo = false) => this.FadeToColor(duration, Color.black, delay, withLogo);

    public Coroutine Blink(float durationToFrom = 0.4f) => this.Blink(durationToFrom / 2f, durationToFrom / 2f);

    public Coroutine Blink(float durationTo = 0.2f, float durationFrom = 0.2f)
    {
      Debug.Log((object) nameof (Blink));
      Action<bool> fadeStateChanged = this.OnFadeStateChanged;
      if (fadeStateChanged != null)
        fadeStateChanged(true);
      if (!this._fadeRoutine.running)
        return this._fadeRoutine.Run(this.FadeRoutine_Boomerang(durationTo, durationFrom, Color.clear, Color.black));
      Debug.LogWarning((object) "Blink: Fade in progress, skipping");
      return this._fadeRoutine.WaitForRoutine();
    }

    public Coroutine FadeToColor(float duration, Color endColor, float delay = 0.0f, bool withLogo = false)
    {
      Debug.Log((object) nameof (FadeToColor));
      if (this._fadeRoutine.running)
      {
        Debug.LogWarning((object) "FadeToColor: Fade in progress, skipping");
        this._fadeRoutine.Stop();
      }
      Action<bool> fadeStateChanged = this.OnFadeStateChanged;
      if (fadeStateChanged != null)
        fadeStateChanged(true);
      return !withLogo || VRSettings.skipWipeTransition ? this._fadeRoutine.RunMultiYield(this.FadeRoutine(Color.clear, endColor, duration, delay, true, true)) : this._fadeRoutine.RunMultiYield(this.LogoBasedFadeToColorRoutine(duration, Color.clear, endColor, delay));
    }

    public Coroutine FadeToColorCustom(
      float duration,
      Color startColor,
      Color endColor,
      float delay = 0.0f)
    {
      Action<bool> fadeStateChanged = this.OnFadeStateChanged;
      if (fadeStateChanged != null)
        fadeStateChanged(true);
      return this._fadeRoutine.Run(this.FadeRoutine(startColor, endColor, duration, delay, true, false));
    }

    public IEnumerator FadeRoutine_Boomerang(
      float durationTo,
      float durationFrom,
      Color startColor,
      Color endColor,
      float delay = 0.0f)
    {
      yield return (object) this._fadeRoutine.RunMultiYield(this.FadeRoutine(startColor, endColor, durationTo, delay, true, false));
      yield return (object) this._fadeRoutine.RunMultiYield(this.FadeRoutine(endColor, startColor, durationFrom, delay, true, false));
    }

    private IEnumerator LogoBasedFadeToColorRoutine(
      float duration,
      Color startColor,
      Color endColor,
      float delay)
    {
      ScreenWipeTransition.StartFading();
      float startTime = Time.realtimeSinceStartup;
      while ((double) Time.realtimeSinceStartup < (double) startTime + 1.0)
        yield return (object) null;
      yield return (object) this.FadeRoutine(startColor, endColor, duration, delay, true, true);
    }

    public Coroutine Clear(float duration = 0.35f, float delay = 0.0f, bool fromBlack = true)
    {
      Debug.Log((object) nameof (Clear));
      if (this._fadeRoutine.running)
      {
        Debug.LogWarning((object) "Clear: Fade in progress, skipping");
        this._fadeRoutine.Stop();
      }
      return this._fadeRoutine.RunMultiYield(this.FadeRoutine(fromBlack ? Color.black : this._currentColor, Color.clear, duration, delay, false, false, true));
    }

    private IEnumerator FadeRoutine(
      Color startColor,
      Color endColor,
      float duration,
      float delay,
      bool jumpToStartColor,
      bool persistFading,
      bool triggerFadeCleared = false)
    {
      this._renderer.enabled = true;
      this._renderer.GetPropertyBlock(this._mpb);
      if (jumpToStartColor)
      {
        this._mpb.SetColor(this._shaderColorId, startColor);
        this._renderer.SetPropertyBlock(this._mpb);
      }
      if ((double) delay > 0.0)
        yield return (object) new WaitForSecondsRealtime(delay);
      float elapsedTime = 0.0f;
      while (true)
      {
        yield return (object) new WaitForEndOfFrame();
        elapsedTime += Time.unscaledDeltaTime;
        if ((double) elapsedTime < (double) duration)
        {
          float t = elapsedTime / duration;
          this._currentColor = Color.Lerp(startColor, endColor, t);
          this._mpb.SetColor(this._shaderColorId, this._currentColor);
          this._renderer.SetPropertyBlock(this._mpb);
          yield return (object) null;
        }
        else
          break;
      }
      if (!persistFading)
        this._renderer.enabled = !Mathf.Approximately(endColor.a, 0.0f);
      this._mpb.SetColor(this._shaderColorId, this._currentColor = endColor);
      this._renderer.SetPropertyBlock(this._mpb);
      yield return (object) new WaitForEndOfFrame();
      if (triggerFadeCleared)
      {
        Action<bool> fadeStateChanged = this.OnFadeStateChanged;
        if (fadeStateChanged != null)
          fadeStateChanged(false);
      }
      this._fadeRoutine.Finished();
    }

    public Coroutine Resize_Boomerang(
      float durationTo,
      float durationFrom,
      Vector3 startSize,
      Vector3 endSize,
      float delay = 0.0f)
    {
      Action<bool> resizeStateChanged = this.OnResizeStateChanged;
      if (resizeStateChanged != null)
        resizeStateChanged(true);
      return this._resizeRoutine.Run(this.ResizeRoutine_Boomerang(durationTo, durationFrom, startSize, endSize, delay));
    }

    public IEnumerator ResizeRoutine_Boomerang(
      float durationTo,
      float durationFrom,
      Vector3 startSize,
      Vector3 endSize,
      float delay = 0.0f)
    {
      this._vignette.SetActive(true);
      yield return (object) this._resizeRoutine.Run(this.ResizeRoutine(startSize, endSize, durationTo, delay, true, true));
      yield return (object) this._resizeRoutine.Run(this.ResizeRoutine(endSize, startSize, durationFrom, delay, true, true, true));
    }

    private IEnumerator ResizeRoutine(
      Vector3 startSize,
      Vector3 endSize,
      float duration,
      float delay,
      bool jumpToStartSize,
      bool persistSize,
      bool turnOffOnCompletion = false)
    {
      if (jumpToStartSize)
        this._vignette.transform.localScale = startSize;
      if ((double) delay > 0.0)
        yield return (object) new WaitForSecondsRealtime(delay);
      float elapsedTime = 0.0f;
      while (true)
      {
        yield return (object) new WaitForEndOfFrame();
        elapsedTime += Time.unscaledDeltaTime;
        if ((double) elapsedTime < (double) duration)
        {
          float t = elapsedTime / duration;
          this._currentSize = Vector3.Lerp(startSize, endSize, t);
          this._vignette.transform.localScale = this._currentSize;
          yield return (object) null;
        }
        else
          break;
      }
      if (!persistSize)
        this._vignette.transform.localScale = startSize;
      yield return (object) new WaitForEndOfFrame();
      this._vignette.SetActive(turnOffOnCompletion);
    }

    public struct FadeSettings
    {
      public static readonly VRCameraFade.FadeSettings Default = new VRCameraFade.FadeSettings()
      {
        Duration = 0.35f,
        Delay = 0.0f,
        FromBlack = true
      };

      public FadeSettings(float duration, float delay = 0.0f, bool fromBlack = true)
      {
        this.Duration = duration;
        this.Delay = delay;
        this.FromBlack = fromBlack;
      }

      public float Duration { get; private set; }

      public float Delay { get; private set; }

      public bool FromBlack { get; private set; }
    }
  }
}
