// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchButtonGradientColor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class TouchButtonGradientColor : MonoBehaviour
  {
    [SerializeField]
    private TouchButton _touchButton;
    [SerializeField]
    private List<Graphic> _graphics;
    [SerializeField]
    private Gradient _gradient;
    [SerializeField]
    private AnimationCurve _curve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);

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
          Debug.LogError((object) string.Format("Removing graphics element {0} from {1} TouchButtonGradientColor", (object) index, (object) this.gameObject.name));
        }
      }
    }

    private void OnEnable() => this._touchButton.OnNormalizedPositionChanged += new Action<float>(this.HandleNormalizedPosition);

    private void OnDisable() => this._touchButton.OnNormalizedPositionChanged -= new Action<float>(this.HandleNormalizedPosition);

    private void HandleNormalizedPosition(float pressValue)
    {
      float time = this._curve.Evaluate(pressValue);
      foreach (Graphic graphic in this._graphics)
        graphic.color = this._gradient.Evaluate(time);
    }
  }
}
