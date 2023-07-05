// Decompiled with JetBrains decompiler
// Type: UnityEngine.UI.BoxSlider
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
  [AddComponentMenu("UI/BoxSlider", 35)]
  [RequireComponent(typeof (RectTransform))]
  public class BoxSlider : 
    Selectable,
    IDragHandler,
    IEventSystemHandler,
    IInitializePotentialDragHandler,
    ICanvasElement
  {
    [SerializeField]
    private RectTransform m_HandleRect;
    [Space(6f)]
    [SerializeField]
    private float m_MinValue;
    [SerializeField]
    private float m_MaxValue = 1f;
    [SerializeField]
    private bool m_WholeNumbers;
    [SerializeField]
    private float m_Value = 1f;
    [SerializeField]
    private float m_ValueY = 1f;
    [Space(6f)]
    [SerializeField]
    private BoxSlider.BoxSliderEvent m_OnValueChanged = new BoxSlider.BoxSliderEvent();
    private Transform m_HandleTransform;
    private RectTransform m_HandleContainerRect;
    private Vector2 m_Offset = Vector2.zero;
    private DrivenRectTransformTracker m_Tracker;

    public RectTransform handleRect
    {
      get => this.m_HandleRect;
      set
      {
        if (!BoxSlider.SetClass<RectTransform>(ref this.m_HandleRect, value))
          return;
        this.UpdateCachedReferences();
        this.UpdateVisuals();
      }
    }

    public float minValue
    {
      get => this.m_MinValue;
      set
      {
        if (!BoxSlider.SetStruct<float>(ref this.m_MinValue, value))
          return;
        this.Set(this.m_Value);
        this.SetY(this.m_ValueY);
        this.UpdateVisuals();
      }
    }

    public float maxValue
    {
      get => this.m_MaxValue;
      set
      {
        if (!BoxSlider.SetStruct<float>(ref this.m_MaxValue, value))
          return;
        this.Set(this.m_Value);
        this.SetY(this.m_ValueY);
        this.UpdateVisuals();
      }
    }

    public bool wholeNumbers
    {
      get => this.m_WholeNumbers;
      set
      {
        if (!BoxSlider.SetStruct<bool>(ref this.m_WholeNumbers, value))
          return;
        this.Set(this.m_Value);
        this.SetY(this.m_ValueY);
        this.UpdateVisuals();
      }
    }

    public float value
    {
      get => this.wholeNumbers ? Mathf.Round(this.m_Value) : this.m_Value;
      set => this.Set(value);
    }

    public float normalizedValue
    {
      get => Mathf.Approximately(this.minValue, this.maxValue) ? 0.0f : Mathf.InverseLerp(this.minValue, this.maxValue, this.value);
      set => this.value = Mathf.Lerp(this.minValue, this.maxValue, value);
    }

    public float valueY
    {
      get => this.wholeNumbers ? Mathf.Round(this.m_ValueY) : this.m_ValueY;
      set => this.SetY(value);
    }

    public float normalizedValueY
    {
      get => Mathf.Approximately(this.minValue, this.maxValue) ? 0.0f : Mathf.InverseLerp(this.minValue, this.maxValue, this.valueY);
      set => this.valueY = Mathf.Lerp(this.minValue, this.maxValue, value);
    }

    public BoxSlider.BoxSliderEvent onValueChanged
    {
      get => this.m_OnValueChanged;
      set => this.m_OnValueChanged = value;
    }

    private float stepSize => !this.wholeNumbers ? (float) (((double) this.maxValue - (double) this.minValue) * 0.10000000149011612) : 1f;

    protected BoxSlider()
    {
    }

    public virtual void Rebuild(CanvasUpdate executing)
    {
    }

    public void LayoutComplete()
    {
    }

    public void GraphicUpdateComplete()
    {
    }

    public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
    {
      if ((object) currentValue == null && (object) newValue == null || (object) currentValue != null && currentValue.Equals((object) newValue))
        return false;
      currentValue = newValue;
      return true;
    }

    public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
    {
      if (currentValue.Equals((object) newValue))
        return false;
      currentValue = newValue;
      return true;
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.UpdateCachedReferences();
      this.Set(this.m_Value, false);
      this.SetY(this.m_ValueY, false);
      this.UpdateVisuals();
    }

    protected override void OnDisable()
    {
      this.m_Tracker.Clear();
      base.OnDisable();
    }

    private void UpdateCachedReferences()
    {
      if ((bool) (UnityEngine.Object) this.m_HandleRect)
      {
        this.m_HandleTransform = this.m_HandleRect.transform;
        if (!((UnityEngine.Object) this.m_HandleTransform.parent != (UnityEngine.Object) null))
          return;
        this.m_HandleContainerRect = this.m_HandleTransform.parent.GetComponent<RectTransform>();
      }
      else
        this.m_HandleContainerRect = (RectTransform) null;
    }

    private void Set(float input) => this.Set(input, true);

    private void Set(float input, bool sendCallback)
    {
      float f = Mathf.Clamp(input, this.minValue, this.maxValue);
      if (this.wholeNumbers)
        f = Mathf.Round(f);
      if ((double) this.m_Value == (double) f)
        return;
      this.m_Value = f;
      this.UpdateVisuals();
      if (!sendCallback)
        return;
      this.m_OnValueChanged.Invoke(f, this.valueY);
    }

    private void SetY(float input) => this.SetY(input, true);

    private void SetY(float input, bool sendCallback)
    {
      float f = Mathf.Clamp(input, this.minValue, this.maxValue);
      if (this.wholeNumbers)
        f = Mathf.Round(f);
      if ((double) this.m_ValueY == (double) f)
        return;
      this.m_ValueY = f;
      this.UpdateVisuals();
      if (!sendCallback)
        return;
      this.m_OnValueChanged.Invoke(this.value, f);
    }

    protected override void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      this.UpdateVisuals();
    }

    private void UpdateVisuals()
    {
      this.m_Tracker.Clear();
      if (!((UnityEngine.Object) this.m_HandleContainerRect != (UnityEngine.Object) null))
        return;
      this.m_Tracker.Add((UnityEngine.Object) this, this.m_HandleRect, DrivenTransformProperties.Anchors);
      Vector2 zero = Vector2.zero;
      Vector2 one = Vector2.one;
      zero[0] = one[0] = this.normalizedValue;
      zero[1] = one[1] = this.normalizedValueY;
      this.m_HandleRect.anchorMin = zero;
      this.m_HandleRect.anchorMax = one;
    }

    private void UpdateDrag(PointerEventData eventData, Camera cam)
    {
      RectTransform handleContainerRect = this.m_HandleContainerRect;
      if (!((UnityEngine.Object) handleContainerRect != (UnityEngine.Object) null))
        return;
      Vector2 vector2_1 = handleContainerRect.rect.size;
      Vector2 localPoint;
      if ((double) vector2_1[0] <= 0.0 || !RectTransformUtility.ScreenPointToLocalPointInRectangle(handleContainerRect, eventData.position, cam, out localPoint))
        return;
      Vector2 vector2_2 = localPoint - handleContainerRect.rect.position;
      vector2_1 = vector2_2 - this.m_Offset;
      double num1 = (double) vector2_1[0];
      vector2_1 = handleContainerRect.rect.size;
      double num2 = (double) vector2_1[0];
      this.normalizedValue = Mathf.Clamp01((float) (num1 / num2));
      vector2_1 = vector2_2 - this.m_Offset;
      double num3 = (double) vector2_1[1];
      vector2_1 = handleContainerRect.rect.size;
      double num4 = (double) vector2_1[1];
      this.normalizedValueY = Mathf.Clamp01((float) (num3 / num4));
    }

    private bool MayDrag(PointerEventData eventData) => this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;

    public override void OnPointerDown(PointerEventData eventData)
    {
      if (!this.MayDrag(eventData))
        return;
      base.OnPointerDown(eventData);
      this.m_Offset = Vector2.zero;
      if ((UnityEngine.Object) this.m_HandleContainerRect != (UnityEngine.Object) null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera))
      {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out localPoint))
          this.m_Offset = localPoint;
        this.m_Offset.y = -this.m_Offset.y;
      }
      else
        this.UpdateDrag(eventData, eventData.pressEventCamera);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
      if (!this.MayDrag(eventData))
        return;
      this.UpdateDrag(eventData, eventData.pressEventCamera);
    }

    public virtual void OnInitializePotentialDrag(PointerEventData eventData) => eventData.useDragThreshold = false;

    [SpecialName]
    Transform ICanvasElement.get_transform() => this.transform;

    public enum Direction
    {
      LeftToRight,
      RightToLeft,
      BottomToTop,
      TopToBottom,
    }

    [Serializable]
    public class BoxSliderEvent : UnityEvent<float, float>
    {
    }

    private enum Axis
    {
      Horizontal,
      Vertical,
    }
  }
}
