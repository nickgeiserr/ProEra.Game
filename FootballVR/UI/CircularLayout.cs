// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.CircularLayout
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FootballVR.UI
{
  public class CircularLayout : 
    MonoBehaviour,
    IBeginDragHandler,
    IEventSystemHandler,
    IDragHandler,
    IEndDragHandler,
    ITouchGrabbable,
    IPointerDownHandler
  {
    [SerializeField]
    private UnityEngine.Object _dataSourceObject;
    [SerializeField]
    private RectTransform _viewport;
    [SerializeField]
    private RectTransform _textViewport;
    [SerializeField]
    private CircularLayout.ScrollType _scrollType;
    [SerializeField]
    private float _itemSize = 50f;
    [SerializeField]
    [Range(-1f, 1f)]
    private float _normalizedPosition;
    [SerializeField]
    private float _radius = 150f;
    [SerializeField]
    private float _itemAngleOffset;
    [SerializeField]
    private float _maxAngleOffset;
    [SerializeField]
    private float _maxAngleClamped;
    [SerializeField]
    private float _forwardOffset;
    [SerializeField]
    private float _forwardCoefficient = 0.7f;
    [SerializeField]
    private float _scrollSensibility = 1f;
    [SerializeField]
    private bool _fade;
    [SerializeField]
    private float _snapThreshold = 0.1f;
    [SerializeField]
    private bool _looped;
    [SerializeField]
    private float _elasticity = 0.1f;
    [SerializeField]
    private float _decelerationRate = 0.135f;
    [SerializeField]
    private float _sensibility = 5000f;
    [SerializeField]
    private bool _reversed;
    [SerializeField]
    private bool _objectsManuallyInstantiated;
    [SerializeField]
    private List<CircularLayoutItem> _items = new List<CircularLayoutItem>();
    private bool _initialized;
    private Vector2 _itemRect;
    private Vector2 _dragStartPos;
    private float _totalSize;
    private float _currentPosition;
    private float _currentSpeed;
    private int _currentIndex;
    private bool _userReleased;
    private CircularLayout.EState _state;
    protected ICircularLayoutDataSource _dataSource;
    public bool DragEnabled = true;
    public bool ShowCurrentOnly;

    private float NormalizedPosition
    {
      set
      {
        this._normalizedPosition = this._looped ? Mathf.Repeat(value, 1f) : Mathf.Clamp01(value);
        this.CurrentPosition = this._normalizedPosition * this._totalSize;
      }
    }

    private float CurrentPosition
    {
      get => this._currentPosition;
      set
      {
        this._currentPosition = this._looped ? Mathf.Repeat(value, this._totalSize) : Mathf.Clamp(value, 0.0f, this._totalSize);
        this._normalizedPosition = this._currentPosition / this._totalSize;
        this.Rebuild();
      }
    }

    private float _directionMultiplier { get; set; } = 1f;

    private bool canDrag => this.DragEnabled && this._dataSource.itemCount > 1;

    public CircularLayoutItem CurrentItem { get; private set; }

    public event Action<int> OnCurrentIndexChanged;

    public event Action<int> OnCurrentItemChanged;

    public event Action OnUserRelease;

    public event Action OnDragStart;

    public int CurrentIndex
    {
      get
      {
        if (!this._initialized)
          this.Initialize();
        return this.NormalizeIndex(this._currentIndex, this._dataSource.itemCount);
      }
      set
      {
        if (this._currentIndex == value)
          return;
        if (!this._initialized)
          this.Initialize();
        this._currentIndex = value;
        this.NormalizedPosition = (float) this._currentIndex / (float) this._dataSource.itemCount;
        Action<int> currentIndexChanged = this.OnCurrentIndexChanged;
        if (currentIndexChanged == null)
          return;
        currentIndexChanged(value);
      }
    }

    public float GetCurrentSpeed() => this._currentSpeed;

    private void OnValidate()
    {
      if (this._dataSourceObject != (UnityEngine.Object) null)
      {
        if (this._dataSourceObject is GameObject dataSourceObject)
        {
          ICircularLayoutDataSource component = dataSourceObject.GetComponent<ICircularLayoutDataSource>();
          if (component != null)
            this._dataSourceObject = (UnityEngine.Object) component;
        }
        if (!(this._dataSourceObject is ICircularLayoutDataSource))
        {
          this._dataSourceObject = (UnityEngine.Object) null;
          Debug.LogError((object) "Data source object should implement interface for ICircularLayoutDataSource");
        }
      }
      if (!this._initialized)
        return;
      this.NormalizedPosition = this._normalizedPosition;
      this.Rebuild();
      this.UpdateChildrenSize();
    }

    private void Start()
    {
      this.ShowCurrentOnly = false;
      this.Initialize();
    }

    public void Initialize()
    {
      if (this._initialized || this._dataSourceObject == (UnityEngine.Object) null || !(this._dataSourceObject is ICircularLayoutDataSource dataSourceObject))
        return;
      this.Initialize(dataSourceObject);
    }

    public void Deinitialize()
    {
      for (int index = 0; index < this._items.Count; ++index)
      {
        if (!((UnityEngine.Object) this._items[index] == (UnityEngine.Object) null))
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this._items[index].gameObject);
          UnityEngine.Object.Destroy((UnityEngine.Object) this._items[index].TextGroup.gameObject);
        }
      }
      this._items.Clear();
      this._initialized = false;
    }

    public void Initialize(ICircularLayoutDataSource dataSource)
    {
      if (this._initialized)
        this.Deinitialize();
      this._initialized = true;
      this._dataSource = dataSource;
      int num = Mathf.CeilToInt(this._maxAngleOffset / this._itemAngleOffset) * 2 + 1;
      if (this._objectsManuallyInstantiated)
      {
        for (int index = 0; index < this._items.Count; ++index)
        {
          if ((UnityEngine.Object) this._items[index].TextGroup != (UnityEngine.Object) null)
            this._items[index].TextGroup.SetParent((Transform) this._textViewport);
        }
      }
      else
      {
        for (int index = 0; index < num; ++index)
          this.BuildElement(this._dataSource.ItemPrefab);
      }
      this.UpdateChildrenSize();
      this._totalSize = (this._looped ? (float) dataSource.itemCount : (float) (dataSource.itemCount - 1)) * this._itemAngleOffset;
      this._currentPosition = this._normalizedPosition * this._totalSize;
      this.Rebuild();
    }

    public void AddItem(CircularLayoutItem a_item)
    {
      if (!this._objectsManuallyInstantiated)
        return;
      this.BuildElement(a_item);
    }

    private void BuildElement(CircularLayoutItem a_prefab)
    {
      CircularLayoutItem circularLayoutItem = UnityEngine.Object.Instantiate<CircularLayoutItem>(a_prefab, (Transform) this._viewport);
      this._items.Add(circularLayoutItem);
      if (!((UnityEngine.Object) circularLayoutItem.TextGroup != (UnityEngine.Object) null))
        return;
      circularLayoutItem.TextGroup.SetParent((Transform) this._textViewport);
    }

    public void UpdateChildrenSize()
    {
      bool flag = this._scrollType == CircularLayout.ScrollType.Horizontal;
      this._itemRect.x = flag ? this._itemSize : this.GetViewportWidth();
      this._itemRect.y = flag ? this.GetViewportHeight() : this._itemSize;
      for (int index = 0; index < this._items.Count; ++index)
      {
        this._items[index].RectTransform.sizeDelta = this._itemRect;
        if ((UnityEngine.Object) this._items[index].TextGroup != (UnityEngine.Object) null)
          this._items[index].TextGroup.sizeDelta = this._itemRect;
      }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
      int num = this.canDrag ? 1 : 0;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      int num = this.canDrag ? 1 : 0;
    }

    public void OnTouchDragStart()
    {
      if (!this.canDrag)
        return;
      this._state = CircularLayout.EState.Dragging;
      Action onDragStart = this.OnDragStart;
      if (onDragStart == null)
        return;
      onDragStart();
    }

    public void OnTouchDrag(Vector3 deltaVec, ITouchInput touchInput, bool usingLaserGrab = false)
    {
      if (!this.canDrag)
        return;
      this.ProcessDrag((Vector2) (this.transform.InverseTransformVector(deltaVec) * this._sensibility));
    }

    public void OnTouchDragEnd(Vector3 deltaVec, ITouchInput touchInput, bool usingLaserGrab = false)
    {
      if (!this.canDrag)
        return;
      this._userReleased = true;
      this.OnTouchDrag(deltaVec, touchInput, false);
      this._state = CircularLayout.EState.Sliding;
    }

    private void ProcessDrag(Vector2 deltaVec)
    {
      if (!this.canDrag)
        return;
      float num = this._scrollType == CircularLayout.ScrollType.Horizontal ? deltaVec.x : deltaVec.y;
      this.CurrentPosition += num * this._scrollSensibility * Time.unscaledDeltaTime * this._directionMultiplier;
      this._currentSpeed = Mathf.Lerp(this._currentSpeed, num * this._scrollSensibility, Time.unscaledDeltaTime * 10f);
      this._state = CircularLayout.EState.Dragging;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    private void LateUpdate()
    {
      if (!this.canDrag && this._state != CircularLayout.EState.Stopped)
      {
        this._currentSpeed = 0.0f;
        this.CurrentPosition = this._itemAngleOffset * (float) this._currentIndex;
        this._state = CircularLayout.EState.Stopped;
      }
      else
      {
        switch (this._state)
        {
          case CircularLayout.EState.Sliding:
            this._currentSpeed *= Mathf.Pow(this._decelerationRate, Time.unscaledDeltaTime);
            this.CurrentPosition += this._currentSpeed * Time.unscaledDeltaTime * this._directionMultiplier;
            if ((double) Mathf.Abs(this._currentSpeed) >= (double) this._snapThreshold)
              break;
            this._state = CircularLayout.EState.Snapping;
            break;
          case CircularLayout.EState.Snapping:
            float f = (float) this._currentIndex * this._itemAngleOffset - this._currentPosition;
            double num = (double) Mathf.Abs(f);
            this.CurrentPosition = Mathf.SmoothDamp(this._currentPosition, this._currentPosition + f, ref this._currentSpeed, this._elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
            if (num > 1.0)
              break;
            this._currentSpeed = 0.0f;
            this.CurrentPosition = this._itemAngleOffset * (float) this._currentIndex;
            this._state = CircularLayout.EState.Stopped;
            if (!this._userReleased)
              break;
            Action onUserRelease = this.OnUserRelease;
            if (onUserRelease == null)
              break;
            onUserRelease();
            break;
        }
      }
    }

    public void InvalidateCache()
    {
      this.Initialize();
      foreach (CircularLayoutItem circularLayoutItem in this._items)
        circularLayoutItem.InvalidateCache();
      this._totalSize = (this._looped ? (float) this._dataSource.itemCount : (float) (this._dataSource.itemCount - 1)) * this._itemAngleOffset;
      this._currentPosition = this._normalizedPosition * this._totalSize;
      this._currentSpeed = 0.0f;
      this._currentIndex = 0;
      this.CurrentPosition = 0.0f;
      this._state = CircularLayout.EState.Stopped;
    }

    public void Rebuild()
    {
      if (!this._initialized)
        return;
      this._directionMultiplier = this._reversed ? 1f : -1f;
      bool flag = this._scrollType == CircularLayout.ScrollType.Horizontal;
      double num1 = (double) this._currentPosition - (double) this._maxAngleOffset;
      float num2 = this._currentPosition + this._maxAngleOffset;
      double itemAngleOffset = (double) this._itemAngleOffset;
      int num3 = Mathf.CeilToInt((float) (num1 / itemAngleOffset));
      int num4 = Mathf.FloorToInt(num2 / this._itemAngleOffset);
      int count = this._items.Count;
      int index1 = Mathf.RoundToInt(this._currentPosition / this._itemAngleOffset);
      int num5 = this.NormalizeIndex(index1, this._dataSource.itemCount);
      if (this._currentIndex != num5)
      {
        Action<int> currentIndexChanged = this.OnCurrentIndexChanged;
        if (currentIndexChanged != null)
          currentIndexChanged(num5);
      }
      this._currentIndex = index1;
      int currentIndex = this._currentIndex;
      int f1 = (int) Mathf.Sign(this._currentPosition - (float) currentIndex * this._itemAngleOffset);
      for (int index2 = count - 1; index2 >= 0; --index2)
      {
        CircularLayoutItem circularLayoutItem = this._items[index2];
        int index3 = currentIndex;
        if (this._looped)
          index3 = this.NormalizeIndex(index3, this._dataSource.itemCount);
        if (currentIndex > num4 || currentIndex < num3 || index3 < 0 || index3 >= this._dataSource.itemCount || this._dataSource.itemCount == 1 && index2 != count - 1)
        {
          circularLayoutItem.alpha = 0.0f;
          currentIndex += f1;
          f1 = -(f1 + (int) Mathf.Sign((float) f1));
        }
        else
        {
          float f2 = Mathf.Clamp((float) currentIndex * this._itemAngleOffset - this._currentPosition, -this._maxAngleClamped, this._maxAngleClamped) * this._directionMultiplier;
          int itemIndex = index3;
          if (circularLayoutItem.index != itemIndex)
          {
            circularLayoutItem.index = itemIndex;
            this._dataSource.SetupItem(itemIndex, circularLayoutItem);
          }
          circularLayoutItem.alpha = !this._fade ? 1f : (float) (1.0 - (double) Mathf.Abs(f2) / (double) this._maxAngleOffset);
          float num6 = Mathf.Sin((float) (Math.PI / 180.0 * ((double) f2 + 90.0)));
          float num7 = Mathf.Cos((float) (Math.PI / 180.0 * ((double) f2 + 90.0)));
          Vector3 vector3 = new Vector3()
          {
            x = flag ? num7 * this._radius : 0.0f,
            y = flag ? 0.0f : num7 * this._radius,
            z = this._forwardOffset - num6 * this._radius * this._forwardCoefficient
          };
          Quaternion quaternion = Quaternion.Euler(new Vector3(flag ? 0.0f : -f2 * this._forwardCoefficient, flag ? f2 * this._forwardCoefficient : 0.0f, 0.0f));
          circularLayoutItem.RectTransform.localPosition = vector3;
          circularLayoutItem.RectTransform.localRotation = quaternion;
          if ((UnityEngine.Object) circularLayoutItem.TextGroup != (UnityEngine.Object) null)
          {
            circularLayoutItem.TextGroup.localPosition = vector3;
            circularLayoutItem.TextGroup.localRotation = quaternion;
          }
          circularLayoutItem.isOn = currentIndex == this._currentIndex;
          if (currentIndex == this._currentIndex)
          {
            this.CurrentItem = circularLayoutItem;
            Action<int> currentItemChanged = this.OnCurrentItemChanged;
            if (currentItemChanged != null)
              currentItemChanged(this._currentIndex);
          }
          else if (this.ShowCurrentOnly)
            circularLayoutItem.alpha = 0.0f;
          currentIndex += f1;
          f1 = -(f1 + (int) Mathf.Sign((float) f1));
        }
      }
    }

    private int NormalizeIndex(int index, int count)
    {
      if (count == 0)
        return 0;
      while (index < 0)
        index += count;
      while (index >= count)
        index -= count;
      return index;
    }

    private float GetViewportHeight() => this._viewport.rect.height;

    private float GetViewportWidth() => this._viewport.rect.width;

    private enum EState
    {
      Stopped,
      Dragging,
      Sliding,
      Snapping,
    }

    public enum ScrollType
    {
      Vertical,
      Horizontal,
    }
  }
}
