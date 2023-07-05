// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchButtonGradientEffect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class TouchButtonGradientEffect : MonoBehaviour
  {
    [SerializeField]
    private TouchButtonGradientEffect.ETriggerType _eventType;
    [SerializeField]
    private TouchButton _touchButton;
    [SerializeField]
    private float _duration = 1f;
    [SerializeField]
    private List<Graphic> _graphics;
    [SerializeField]
    private Gradient _gradient;
    [SerializeField]
    private AnimationCurve _curve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
    private readonly RoutineHandle _effectRoutine = new RoutineHandle();

    public static event System.Action PlaySpawnEvent;

    private void OnValidate()
    {
      if (!((UnityEngine.Object) this._touchButton == (UnityEngine.Object) null))
        return;
      this._touchButton = this.GetComponent<TouchButton>();
    }

    private void Start()
    {
      for (int index = 0; index < this._graphics.Count; ++index)
      {
        if (!((UnityEngine.Object) this._graphics[index] != (UnityEngine.Object) null))
        {
          this._graphics.RemoveAt(index);
          --index;
          Debug.LogError((object) string.Format("Removing graphics element {0} from {1} TouchButtonGradientEffect", (object) index, (object) this.gameObject.name));
        }
      }
    }

    private void OnEnable()
    {
      this._touchButton.onClick += new System.Action(this.HandleClick);
      if (this._eventType == TouchButtonGradientEffect.ETriggerType.OnAppear)
        this._effectRoutine.Run(this.ClickEffect());
      TouchButtonGradientEffect.PlaySpawnEvent += new System.Action(this.HandlePlaySpawnEffect);
    }

    private void OnDisable()
    {
      this._effectRoutine.Stop();
      this._touchButton.onClick -= new System.Action(this.HandleClick);
      TouchButtonGradientEffect.PlaySpawnEvent -= new System.Action(this.HandlePlaySpawnEffect);
    }

    private void HandleClick()
    {
      if (this._eventType != TouchButtonGradientEffect.ETriggerType.OnPress)
        return;
      this._effectRoutine.Run(this.ClickEffect());
    }

    private IEnumerator ClickEffect()
    {
      float time = 0.0f;
      if ((double) this._duration == 0.0)
        this._duration = 0.01f;
      for (; (double) time < (double) this._duration; time += Time.unscaledDeltaTime)
      {
        this.ApplyColor(time / this._duration);
        yield return (object) null;
      }
      this.ApplyColor(1f);
    }

    private void ApplyColor(float time)
    {
      float time1 = this._curve.Evaluate(time);
      foreach (Graphic graphic in this._graphics)
      {
        if ((UnityEngine.Object) graphic != (UnityEngine.Object) null)
          graphic.color = this._gradient.Evaluate(time1);
      }
    }

    public void ResetColor() => this.ApplyColor(0.0f);

    public static void PlaySpawnEffect()
    {
      System.Action playSpawnEvent = TouchButtonGradientEffect.PlaySpawnEvent;
      if (playSpawnEvent == null)
        return;
      playSpawnEvent();
    }

    private void HandlePlaySpawnEffect()
    {
      if (this._eventType != TouchButtonGradientEffect.ETriggerType.OnAppear)
        return;
      this._effectRoutine.Run(this.ClickEffect());
    }

    public enum ETriggerType
    {
      OnPress,
      OnAppear,
    }
  }
}
