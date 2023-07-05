// Decompiled with JetBrains decompiler
// Type: FootballVR.TargetReticle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using TMPro;
using UnityEngine;

namespace FootballVR
{
  public class TargetReticle : MonoBehaviour
  {
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private TextMeshProUGUI _text;
    private readonly int spotState = Animator.StringToHash("Spotted");
    private readonly int redState = Animator.StringToHash("Red");
    private readonly int yellowState = Animator.StringToHash("Yellow");
    private readonly int greenState = Animator.StringToHash("Green");
    private const float _defaultAlpha = 0.85f;
    private const float _fadeDuration = 0.5f;
    private readonly RoutineHandle _fadingRoutine = new RoutineHandle();
    private bool _faded = true;
    private EReticle _state;

    private void OnEnable()
    {
      this._animator.ResetTrigger(this.redState);
      this._animator.ResetTrigger(this.yellowState);
      this._animator.ResetTrigger(this.greenState);
      this._state = EReticle.Faded;
      this._text.enabled = false;
      this._canvasGroup.alpha = 0.0f;
      this._faded = true;
    }

    private void OnDestroy() => this._fadingRoutine.Stop();

    public void SetSpotState(bool spotted) => this._animator.SetBool(this.spotState, spotted);

    public void SetState(EReticle state)
    {
      if (this._state == state)
        return;
      if (this._faded && state != EReticle.Faded)
        this._fadingRoutine.Run(this.FadeRoutine(false));
      this._state = state;
      switch (state)
      {
        case EReticle.Red:
          this._animator.SetTrigger(this.redState);
          this._text.text = string.Empty;
          break;
        case EReticle.Yellow:
          this._animator.SetTrigger(this.yellowState);
          this._text.text = "READY";
          break;
        case EReticle.Green:
          this._animator.SetTrigger(this.greenState);
          this._text.text = "NOW";
          break;
        case EReticle.Faded:
          this._animator.SetTrigger(this.redState);
          this._fadingRoutine.Run(this.FadeRoutine(true));
          break;
        default:
          Debug.LogError((object) "Bad state for pass reticle. Setting to red..");
          this.SetState(EReticle.Red);
          break;
      }
      this._text.enabled = state != 0;
    }

    private IEnumerator FadeRoutine(bool fade)
    {
      float time = Time.time;
      float endTime = time + 0.5f;
      float start = fade ? endTime : time;
      float end = fade ? time : endTime;
      while ((double) Time.time < (double) endTime)
      {
        yield return (object) null;
        this._canvasGroup.alpha = Mathf.InverseLerp(start, end, Time.time) * 0.85f;
      }
      this._faded = fade;
    }
  }
}
