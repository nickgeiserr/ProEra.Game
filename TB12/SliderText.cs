// Decompiled with JetBrains decompiler
// Type: TB12.SliderText
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TB12
{
  public class SliderText : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private float _multiplier = 100f;
    [SerializeField]
    private bool _percentage = true;

    private void Awake()
    {
      this._slider.onValueChanged.AddListener(new UnityAction<float>(this.HandleValueChanged));
      this.HandleValueChanged(this._slider.value);
    }

    private void OnDestroy() => this._slider.onValueChanged.RemoveListener(new UnityAction<float>(this.HandleValueChanged));

    private void HandleValueChanged(float value)
    {
      if (this._percentage)
        this._text.text = string.Format("{0}%", (object) Mathf.CeilToInt(value * this._multiplier));
      else
        this._text.text = string.Format("{0}", (object) (float) ((double) value * (double) this._multiplier));
    }
  }
}
