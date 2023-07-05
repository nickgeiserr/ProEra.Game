// Decompiled with JetBrains decompiler
// Type: UDB.TouchManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class TouchManager : SingletonBehaviour<TouchManager, MonoBehaviour>
  {
    private const float pinchTurnRatio = 1.57079637f;
    private const float minTurnAngle = 0.0f;
    private const float pinchRatio = 1f;
    private const float minPinchDistance = 0.0f;
    private const float panRatio = 1f;
    private const float minPanDistance = 0.0f;
    [Header("Touch Area")]
    public EdgeInsets touchEdgeInsets = new EdgeInsets(0.0f, 0.0f);
    [Header("Touch Settings")]
    public bool touchEnabled;
    private TouchManager.Phase currentTouchPhase;
    private int currentTouchID = -1;
    private bool hasCurrentTouch;
    private Touch[] touches;
    private Touch firstTouch;
    private Touch secondTouch;
    private Touch currentTouch;
    private TouchObject currentTouchObject;
    public float turnAngleDelta;
    public float turnAngle;
    public float pinchDistanceDelta;
    public float pinchDistance;
    private static TouchManager self;
    [Header("Swipe Settings")]
    [Range(0.0f, 1f)]
    public float swipeSensitivity = 0.1f;
    public bool oneSwipePerTouch = true;
    private bool _swipeActionComplete;

    private new void Awake()
    {
      if ((Object) TouchManager.self == (Object) null)
      {
        TouchManager.self = this;
        Object.DontDestroyOnLoad((Object) this);
      }
      else
        Object.DestroyImmediate((Object) this.gameObject);
    }

    private void Update()
    {
      if (this.currentTouchPhase == TouchManager.Phase.Ended || this.currentTouchPhase == TouchManager.Phase.Canceled)
      {
        this.currentTouchPhase = TouchManager.Phase.None;
        this.hasCurrentTouch = false;
      }
      if (this.currentTouchPhase == TouchManager.Phase.None && this.hasCurrentTouch)
        this.hasCurrentTouch = false;
      if (!this.touchEnabled)
        return;
      this.TouchUpdate();
    }

    private void TouchUpdate()
    {
      this.touches = Input.touches;
      if (!this.hasCurrentTouch)
      {
        this.swipeActionComplete = false;
        this.TestFirstTouch();
        this.TestSecondTouch();
      }
      this.UpdateCurrentTouchObject();
      if (this.currentTouchObject.exists)
      {
        this.currentTouch = this.currentTouchObject.touch;
        switch (this.currentTouch.phase)
        {
          case TouchPhase.Moved:
            this.TouchMoved(this.currentTouch);
            break;
          case TouchPhase.Ended:
            this.TouchEnded(this.currentTouch);
            break;
          case TouchPhase.Canceled:
            this.TouchCanceled(this.currentTouch);
            break;
        }
      }
      else
        this.hasCurrentTouch = false;
    }

    private void TestFirstTouch()
    {
      if (this.touches.Length == 0)
        return;
      this.firstTouch = this.touches[0];
      if (this.firstTouch.phase != TouchPhase.Began || !this.InsideEdgeInsets(this.firstTouch))
        return;
      this.TouchBegan(this.firstTouch);
    }

    private void TestSecondTouch()
    {
      if (this.touches.Length <= 1)
        return;
      this.secondTouch = this.touches[1];
      if (this.secondTouch.phase != TouchPhase.Began || !this.InsideEdgeInsets(this.secondTouch))
        return;
      this.TouchBegan(this.secondTouch);
    }

    private bool InsideEdgeInsets(Touch touch)
    {
      Vector2 position = touch.position;
      float num1 = (float) Screen.height - (float) Screen.height * this.touchEdgeInsets.topPercent;
      float num2 = (float) Screen.height * this.touchEdgeInsets.bottomPercent;
      float num3 = (float) Screen.width - (float) Screen.width * this.touchEdgeInsets.leftPercent;
      float num4 = (float) Screen.width * this.touchEdgeInsets.rightPercent;
      return (double) position.y >= (double) num2 && (double) position.y <= (double) num1 && (double) position.x >= (double) num4 && (double) position.x <= (double) num3;
    }

    private void UpdateCurrentTouchObject()
    {
      this.currentTouchObject.exists = false;
      for (int index = 0; index < this.touches.Length; ++index)
      {
        if (this.touches[index].fingerId == this.currentTouchID)
        {
          this.currentTouchObject.touch = this.touches[index];
          this.currentTouchObject.exists = true;
          break;
        }
      }
    }

    private void TouchBegan(Touch touch)
    {
      if (this.hasCurrentTouch)
        return;
      if (DebugManager.StateForKey("Touch Manager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " ID : " + touch.fingerId.ToString()));
      this.hasCurrentTouch = true;
      this.currentTouchID = touch.fingerId;
      this.SetCurrentTouchPhase(touch.phase);
      NotificationCenter<Vector2>.Broadcast("InputDown", touch.position);
    }

    private void TouchMoved(Touch touch)
    {
      if (this.currentTouchID != touch.fingerId && this.hasCurrentTouch)
        return;
      this.SetCurrentTouchPhase(touch.phase);
      this.currentTouchID = touch.fingerId;
      this.CheckForSwipe(touch);
    }

    private void TouchStationary(Touch touch)
    {
      if (this.currentTouchID != touch.fingerId && this.hasCurrentTouch)
        return;
      this.SetCurrentTouchPhase(touch.phase);
    }

    private void TouchEnded(Touch touch)
    {
      if (this.currentTouchID != touch.fingerId && this.hasCurrentTouch)
        return;
      if (DebugManager.StateForKey("Touch Manager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " ID : " + touch.fingerId.ToString()));
      this.hasCurrentTouch = false;
      this.SetCurrentTouchPhase(touch.phase);
      if (this.DoSwipe(touch))
        return;
      NotificationCenter<Vector2>.Broadcast("InputUp", touch.position);
    }

    private void TouchCanceled(Touch touch)
    {
      if (this.currentTouchID != touch.fingerId && this.hasCurrentTouch)
        return;
      if (DebugManager.StateForKey("Touch Manager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " ID : " + touch.fingerId.ToString()));
      this.hasCurrentTouch = false;
      this.SetCurrentTouchPhase(touch.phase);
    }

    private void SetCurrentTouchPhase(TouchPhase touchPhase)
    {
      switch (touchPhase)
      {
        case TouchPhase.Began:
          this.currentTouchPhase = TouchManager.Phase.Began;
          break;
        case TouchPhase.Moved:
          this.currentTouchPhase = TouchManager.Phase.Moved;
          break;
        case TouchPhase.Stationary:
          this.currentTouchPhase = TouchManager.Phase.Stationary;
          break;
        case TouchPhase.Ended:
          this.currentTouchPhase = TouchManager.Phase.Ended;
          break;
        case TouchPhase.Canceled:
          this.currentTouchPhase = TouchManager.Phase.Canceled;
          break;
      }
    }

    public bool TapDown() => this.hasCurrentTouch && this.currentTouchPhase == TouchManager.Phase.Began;

    public bool TapUp() => this.currentTouchPhase == TouchManager.Phase.Ended;

    public void ToggleEnableTouches() => this.touchEnabled = !this.touchEnabled;

    public void EnableTouches() => this.touchEnabled = true;

    public void DisableTouches() => this.touchEnabled = false;

    private bool swipeActionComplete
    {
      get => this._swipeActionComplete;
      set
      {
        if (this._swipeActionComplete & value)
          Debug.Log((object) "TEST");
        this._swipeActionComplete = value;
      }
    }

    private bool DoSwipe(Touch touch)
    {
      if ((double) touch.deltaPosition.magnitude < (double) this.swipeSensitivity || !this.CanDoSwipeAction())
        return false;
      this.swipeActionComplete = true;
      this.SwipeAction(this.GetSwipeDirectionForVector(touch.deltaPosition.normalized));
      return true;
    }

    private void CheckForSwipe(Touch touch)
    {
    }

    private bool CanDoSwipeAction() => !this.oneSwipePerTouch || !this.swipeActionComplete;

    private TouchManager.SwipeDirection GetSwipeDirectionForVector(Vector2 vector)
    {
      if (this.SnapTo(vector) == Vector2.up)
        return TouchManager.SwipeDirection.Up;
      if (this.SnapTo(vector) == Vector2.right)
        return TouchManager.SwipeDirection.Right;
      if (this.SnapTo(vector) == -Vector2.up)
        return TouchManager.SwipeDirection.Down;
      return this.SnapTo(vector) == -Vector2.right ? TouchManager.SwipeDirection.Left : TouchManager.SwipeDirection.None;
    }

    private Vector2 SnapTo(Vector2 vector) => this.SnapTo(vector, 90f);

    private Vector2 SnapTo(Vector2 vector, float snapAngle)
    {
      float num = Vector2.Angle(vector, Vector2.up);
      if ((double) num < (double) snapAngle / 2.0)
        return Vector2.up * vector.magnitude;
      return (double) num > 180.0 - (double) snapAngle / 2.0 ? -Vector2.up * vector.magnitude : (Vector2) (Quaternion.AngleAxis(Mathf.Round(num / snapAngle) * snapAngle - num, Vector3.Cross((Vector3) Vector2.up, (Vector3) vector)) * (Vector3) vector);
    }

    private void SwipeAction(TouchManager.SwipeDirection swipeDirection)
    {
      if (DebugManager.StateForKey("Touch Manager Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + ": " + swipeDirection.ToString()));
      switch (swipeDirection)
      {
        case TouchManager.SwipeDirection.Left:
          NotificationCenter.Broadcast("SwipeLeft");
          break;
        case TouchManager.SwipeDirection.Right:
          NotificationCenter.Broadcast("SwipeRight");
          break;
        case TouchManager.SwipeDirection.Up:
          NotificationCenter.Broadcast("SwipeUp");
          break;
        case TouchManager.SwipeDirection.Down:
          NotificationCenter.Broadcast("SwipeDown");
          break;
      }
    }

    public enum SwipeDirection
    {
      None,
      Left,
      Right,
      Up,
      Down,
    }

    public enum Phase
    {
      Began,
      Moved,
      Stationary,
      Ended,
      Canceled,
      None,
    }
  }
}
