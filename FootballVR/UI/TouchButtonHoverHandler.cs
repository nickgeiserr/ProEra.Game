// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchButtonHoverHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class TouchButtonHoverHandler : MonoBehaviour
  {
    [SerializeField]
    private TouchButton _touchButton;
    [SerializeField]
    private List<Graphic> _graphics;
    [SerializeField]
    private Color _enterColor = Color.white;
    [SerializeField]
    private float _expansionFactor = 1.2f;
    [SerializeField]
    private float _effectDuration = 0.2f;
    private Color[] _originalColors;
    private EventHandle handle;
    private readonly RoutineHandle _effectRoutine = new RoutineHandle();

    private void OnValidate()
    {
      if (!((UnityEngine.Object) this._touchButton == (UnityEngine.Object) null))
        return;
      this._touchButton = this.GetComponent<TouchButton>();
    }

    private void Awake()
    {
      this._touchButton.Initialize();
      this.handle = this._touchButton.Highlighted.Link<bool>(new Action<bool>(this.HandleHighlight));
      this._originalColors = new Color[this._graphics.Count];
      int index = 0;
      foreach (Graphic graphic in this._graphics)
      {
        if ((UnityEngine.Object) graphic != (UnityEngine.Object) null)
          this._originalColors[index] = graphic.color;
        ++index;
      }
    }

    private void OnEnable()
    {
      this.handle.SetState(true);
      this._touchButton.Interactable.OnValueChanged += new Action<bool>(this.ForceClearHighlight);
    }

    private void OnDisable()
    {
      this.handle.SetState(false);
      this._touchButton.Interactable.OnValueChanged -= new Action<bool>(this.ForceClearHighlight);
    }

    private void OnDestroy()
    {
      this.handle?.Dispose();
      this._effectRoutine.Stop();
    }

    private void HandleHighlight(bool highlighted)
    {
      if (!(bool) this._touchButton.Interactable)
        return;
      this._effectRoutine.Run(this.EffectRoutine(highlighted));
    }

    private IEnumerator EffectRoutine(bool highlighted)
    {
      if ((double) this._effectDuration == 0.0)
        this._effectDuration = 0.01f;
      float endTime = this._effectDuration;
      float time = 0.0f;
      float startExpansion = this._touchButton.Expansion;
      float endExpansion = highlighted ? this._expansionFactor : 1f;
      if (!Mathf.Approximately(startExpansion, endExpansion))
      {
        for (; (double) time < (double) endTime; time += Time.unscaledDeltaTime)
        {
          this._touchButton.Expansion = Mathf.Lerp(startExpansion, endExpansion, time / this._effectDuration);
          int index = 0;
          foreach (Graphic graphic in this._graphics)
          {
            if ((UnityEngine.Object) graphic != (UnityEngine.Object) null)
              graphic.color = Color.Lerp(graphic.color, highlighted ? this._enterColor : this._originalColors[index], time / this._effectDuration);
            ++index;
          }
          yield return (object) null;
        }
        this._touchButton.Expansion = endExpansion;
      }
    }

    private void ForceClearHighlight(bool val)
    {
      if (!val)
        return;
      this.ForceClearHighlight();
    }

    public void ForceClearHighlight()
    {
      int count = this._graphics.Count;
      for (int index = 0; index < count; ++index)
      {
        Graphic graphic = this._graphics[index];
        if ((UnityEngine.Object) graphic != (UnityEngine.Object) null)
          graphic.color = this._originalColors[index];
      }
    }
  }
}
