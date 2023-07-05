// Decompiled with JetBrains decompiler
// Type: MobileControllerManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using InControl;
using System.Reflection;
using UDB;
using UnityEngine;

public class MobileControllerManager : SingletonBehaviour<MobileControllerManager, MonoBehaviour>
{
  public float yThreshholdPercent;
  [Header("Controller Settings")]
  [Range(0.0f, 1f)]
  public float sensitivity = 0.1f;
  public bool oneSwipePerTouch = true;
  public bool printTouchDebugStatements;
  public bool printSwipeTouchDebugStatements;
  public bool printJoyStickTouchDebugStatements;
  private bool setOnScreenControls;
  private bool enableTouch;
  private Vector2 currentVector;
  private Vector2 beganPosition;
  private Vector2 lastPosition;
  private Vector2 currentTapPosition;
  private MobileControllerManager.MyTouchPhase currentTouchPhase;
  private int currentTouchID = -1;
  private bool swipeMessageSent;
  private bool hasCurrentTouch;

  private void OnDestroy()
  {
    Debug.Log((object) "MobileControllerManager -> OnDestroy");
    SingletonBehaviour<MobileControllerManager, MonoBehaviour>.instance = (MobileControllerManager) null;
  }

  private void Update()
  {
    if (!this.setOnScreenControls)
      this.ShowOnScreenControls(false);
    if (this.printJoyStickTouchDebugStatements)
    {
      Vector2 anologInput = this.GetAnologInput();
      if ((double) anologInput.x > 0.0 || (double) anologInput.y > 0.0)
      {
        if ((double) anologInput.x > 0.0)
          Debug.Log((object) ("joyStickDirection.x: " + anologInput.x.ToString()));
        if ((double) anologInput.y > 0.0)
          Debug.Log((object) ("joyStickDirection.y: " + anologInput.y.ToString()));
      }
    }
    if (this.currentTouchPhase == MobileControllerManager.MyTouchPhase.Ended || this.currentTouchPhase == MobileControllerManager.MyTouchPhase.Canceled)
    {
      this.currentTouchPhase = MobileControllerManager.MyTouchPhase.None;
      this.hasCurrentTouch = false;
    }
    if (this.currentTouchPhase == MobileControllerManager.MyTouchPhase.None && this.hasCurrentTouch)
      this.hasCurrentTouch = false;
    if (!this.enableTouch)
      return;
    this.SwipeUpdate();
  }

  private void SetCurrentTouchPhase(TouchPhase touchPhase)
  {
    switch (touchPhase)
    {
      case TouchPhase.Began:
        this.currentTouchPhase = MobileControllerManager.MyTouchPhase.Began;
        break;
      case TouchPhase.Moved:
        this.currentTouchPhase = MobileControllerManager.MyTouchPhase.Moved;
        break;
      case TouchPhase.Stationary:
        this.currentTouchPhase = MobileControllerManager.MyTouchPhase.Stationary;
        break;
      case TouchPhase.Ended:
        this.currentTouchPhase = MobileControllerManager.MyTouchPhase.Ended;
        break;
      case TouchPhase.Canceled:
        this.currentTouchPhase = MobileControllerManager.MyTouchPhase.Canceled;
        break;
    }
  }

  private bool IsAboveThreshhold(UnityEngine.Touch touch) => (double) touch.position.y >= (double) ((float) Screen.height * this.yThreshholdPercent);

  private TouchObject CurrentTouchObject()
  {
    TouchObject touchObject = new TouchObject();
    touchObject.exists = false;
    if (!this.hasCurrentTouch)
      return touchObject;
    UnityEngine.Touch[] touches = Input.touches;
    for (int index = 0; index < Input.touchCount; ++index)
    {
      UnityEngine.Touch touch = Input.touches[index];
      if (touch.fingerId == this.currentTouchID)
      {
        touchObject.touch = touch;
        touchObject.exists = true;
        return touchObject;
      }
    }
    return touchObject;
  }

  private void SwipeUpdate()
  {
    if (!this.hasCurrentTouch)
    {
      this.swipeMessageSent = false;
      this.TestFirstTouch();
      this.TestSecondTouch();
    }
    else
    {
      TouchObject touchObject = this.CurrentTouchObject();
      if (touchObject.exists)
      {
        UnityEngine.Touch touch = touchObject.touch;
        switch (touch.phase)
        {
          case TouchPhase.Began:
            if (!this.printTouchDebugStatements)
              break;
            Debug.Log((object) (touch.fingerId.ToString() + " TouchPhase.Began"));
            break;
          case TouchPhase.Moved:
            if (this.printTouchDebugStatements)
              Debug.Log((object) (touch.fingerId.ToString() + " TouchPhase.Moved"));
            this.TouchMoved(touch);
            break;
          case TouchPhase.Stationary:
            if (!this.printTouchDebugStatements)
              break;
            Debug.Log((object) (touch.fingerId.ToString() + " TouchPhase.Stationary"));
            break;
          case TouchPhase.Ended:
            if (this.printTouchDebugStatements)
              Debug.Log((object) (touch.fingerId.ToString() + " TouchPhase.Ended"));
            this.TouchEnded(touch);
            break;
          case TouchPhase.Canceled:
            if (this.printTouchDebugStatements)
              Debug.Log((object) (touch.fingerId.ToString() + " TouchPhase.Canceled"));
            this.hasCurrentTouch = false;
            break;
        }
      }
      else
        this.hasCurrentTouch = false;
    }
  }

  private void TestFirstTouch()
  {
    UnityEngine.Touch[] touches = Input.touches;
    if (touches.Length == 0)
      return;
    UnityEngine.Touch touch = touches[0];
    if (this.JoyStickTouch() != null)
    {
      if (touch.fingerId == this.JoyStickTouch().fingerId || touch.phase != TouchPhase.Began || !this.IsAboveThreshhold(touch))
        return;
      this.TouchBegan(touch);
    }
    else
    {
      if (touch.phase != TouchPhase.Began || !this.IsAboveThreshhold(touch))
        return;
      this.TouchBegan(touch);
    }
  }

  private void TestSecondTouch()
  {
    UnityEngine.Touch[] touches = Input.touches;
    if (touches.Length <= 1)
      return;
    UnityEngine.Touch touch = touches[1];
    if (this.JoyStickTouch() == null || touch.fingerId == this.JoyStickTouch().fingerId || touch.phase != TouchPhase.Began || !this.IsAboveThreshhold(touch))
      return;
    this.TouchBegan(touch);
  }

  private void TouchBegan(UnityEngine.Touch touch)
  {
    if (this.hasCurrentTouch)
      return;
    this.hasCurrentTouch = true;
    this.currentTouchID = touch.fingerId;
    this.SetCurrentTouchPhase(touch.phase);
    this.beganPosition = touch.position;
    this.currentTapPosition = this.beganPosition;
    this.lastPosition = this.beganPosition;
    this.currentVector = Vector2.zero;
    this.TapDownAction(touch);
    this.DefenseControls(touch);
  }

  private void TouchMoved(UnityEngine.Touch touch)
  {
    if (this.currentTouchID != touch.fingerId && this.hasCurrentTouch)
      return;
    this.SetCurrentTouchPhase(touch.phase);
    this.currentTouchID = touch.fingerId;
    Vector2 position = touch.position;
    Vector2 vector2 = position - this.lastPosition;
    if ((double) vector2.magnitude < (double) this.sensitivity || !this.CanDoSwipeAction())
      return;
    this.lastPosition = position;
    this.currentVector = vector2.normalized;
    MobileControllerManager.SwipeDirection directionForVector = this.GetSwipeDirectionForVector(this.currentVector);
    this.swipeMessageSent = true;
    this.SwipeAction(directionForVector);
  }

  private void TouchStationary(UnityEngine.Touch touch)
  {
    if (this.currentTouchID != touch.fingerId && this.hasCurrentTouch)
      return;
    this.SetCurrentTouchPhase(touch.phase);
  }

  private void TouchEnded(UnityEngine.Touch touch)
  {
    if (this.currentTouchID != touch.fingerId && this.hasCurrentTouch)
      return;
    this.hasCurrentTouch = false;
    this.SetCurrentTouchPhase(touch.phase);
    this.currentVector = Vector2.zero;
    if ((double) (this.beganPosition - touch.position).magnitude >= (double) this.sensitivity)
      return;
    this.TapAction(touch);
    this.TapUpAction(touch);
  }

  private void TouchCanceled(UnityEngine.Touch touch)
  {
    if (this.currentTouchID != touch.fingerId && this.hasCurrentTouch)
      return;
    this.SetCurrentTouchPhase(touch.phase);
  }

  private bool CanDoSwipeAction() => !this.oneSwipePerTouch || !this.swipeMessageSent;

  private MobileControllerManager.SwipeDirection GetSwipeDirectionForVector(Vector2 vector)
  {
    Vector2 vector2 = this.SnapTo(vector);
    if (vector2 == Vector2.up)
      return MobileControllerManager.SwipeDirection.Up;
    if (vector2 == Vector2.right)
      return MobileControllerManager.SwipeDirection.Right;
    if (vector2 == -Vector2.up)
      return MobileControllerManager.SwipeDirection.Down;
    return vector2 == -Vector2.right ? MobileControllerManager.SwipeDirection.Left : MobileControllerManager.SwipeDirection.None;
  }

  private Vector2 SnapTo(Vector2 vector)
  {
    float snapAngle = 90f;
    return this.SnapTo(vector, snapAngle);
  }

  private Vector2 SnapTo(Vector2 vector, float snapAngle)
  {
    float num = Vector2.Angle(vector, Vector2.up);
    if ((double) num < (double) snapAngle / 2.0)
      return Vector2.up * vector.magnitude;
    return (double) num > 180.0 - (double) snapAngle / 2.0 ? -Vector2.up * vector.magnitude : (Vector2) (Quaternion.AngleAxis(Mathf.Round(num / snapAngle) * snapAngle - num, Vector3.Cross((Vector3) Vector2.up, (Vector3) vector)) * (Vector3) vector);
  }

  private void SwipeAction(
    MobileControllerManager.SwipeDirection swipeDirection)
  {
    if (swipeDirection == MobileControllerManager.SwipeDirection.Up)
    {
      if (this.printSwipeTouchDebugStatements)
        Debug.Log((object) "Swipe Up");
      NotificationCenter.Broadcast("SwipeUp");
    }
    if (swipeDirection == MobileControllerManager.SwipeDirection.Down)
    {
      if (this.printSwipeTouchDebugStatements)
        Debug.Log((object) "Swipe Down");
      NotificationCenter.Broadcast("SwipeDown");
    }
    if (swipeDirection == MobileControllerManager.SwipeDirection.Right)
    {
      if (this.printSwipeTouchDebugStatements)
        Debug.Log((object) "Swipe Right");
      NotificationCenter.Broadcast("SwipeRight");
    }
    if (swipeDirection != MobileControllerManager.SwipeDirection.Left)
      return;
    if (this.printSwipeTouchDebugStatements)
      Debug.Log((object) "Swipe Left");
    NotificationCenter.Broadcast("SwipeLeft");
  }

  private void DefenseControls(UnityEngine.Touch touch)
  {
    if ((double) touch.position.x > (double) Screen.width / 2.0)
      NotificationCenter.Broadcast("CycleDefensePlayerRight");
    else
      NotificationCenter.Broadcast("CycleDefensePlayerLeft");
  }

  private void TapAction(UnityEngine.Touch touch)
  {
    if (!this.printSwipeTouchDebugStatements)
      return;
    Vector2 position = touch.position;
    if ((double) position.x <= 0.0 && (double) position.y <= 0.0)
      return;
    if ((double) position.x > 0.0)
      Debug.Log((object) ("tapPosition.x: " + position.x.ToString()));
    if ((double) position.y <= 0.0)
      return;
    Debug.Log((object) ("tapPosition.y: " + position.y.ToString()));
  }

  private void TapDownAction(UnityEngine.Touch touch) => NotificationCenter<Vector2>.Broadcast("InputDown", touch.position);

  private void TapUpAction(UnityEngine.Touch touch) => NotificationCenter<Vector2>.Broadcast("InputUp", touch.position);

  private InControl.Touch JoyStickTouch() => (InControl.Touch) null;

  public Vector2 TapTouchPosition()
  {
    Vector2 zero = Vector2.zero;
    return this.hasCurrentTouch ? this.currentTapPosition : Vector2.zero;
  }

  public bool TapDown() => this.hasCurrentTouch && this.currentTouchPhase == MobileControllerManager.MyTouchPhase.Began;

  public bool TapUp() => this.currentTouchPhase == MobileControllerManager.MyTouchPhase.Ended;

  public void ToggleEnableTouches() => this.enableTouch = !this.enableTouch;

  public void EnableTouches(bool enable) => this.enableTouch = enable;

  public void ToggleOnScreenControls() => this.ShowOnScreenControls(!InControl.TouchManager.ControlsEnabled);

  public void ShowOnScreenControls(bool show)
  {
    if (!show)
      this.TouchDevice().ClearInputState();
    try
    {
      InControl.TouchManager.ControlsEnabled = show;
      this.enableTouch = show;
      this.setOnScreenControls = true;
    }
    catch
    {
      Debug.Log((object) ("ERROR: " + ((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name));
    }
  }

  public Vector2 GetAnologInput() => (Vector2) this.TouchDevice().Direction;

  private InputDevice TouchDevice()
  {
    InputDevice activeDevice = InputManager.ActiveDevice;
    if (activeDevice != InputDevice.Null && activeDevice != InControl.TouchManager.Device)
      InControl.TouchManager.ControlsEnabled = false;
    return activeDevice;
  }

  public enum SwipeDirection
  {
    None,
    Left,
    Right,
    Up,
    Down,
  }

  public enum MyTouchPhase
  {
    Began,
    Moved,
    Stationary,
    Ended,
    Canceled,
    None,
  }
}
