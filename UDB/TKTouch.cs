// Decompiled with JetBrains decompiler
// Type: UDB.TKTouch
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class TKTouch
  {
    public readonly int fingerId;
    public Vector2 position;
    public Vector2 startPosition;
    public Vector2 deltaPosition;
    public float deltaTime;
    public int tapCount;
    public TouchPhase phase = TouchPhase.Ended;
    private Vector2? lastPosition;
    private double lastClickTime;
    private double multipleClickInterval = 0.2;

    public Vector2 previousPosition => this.position - this.deltaPosition;

    public TKTouch(int fingerId) => this.fingerId = fingerId;

    public override string ToString() => string.Format("[TKTouch] fingerId: {0}, phase: {1}, position: {2}", (object) this.fingerId, (object) this.phase, (object) this.position);

    public TKTouch PopulateWithTouch(Touch touch)
    {
      this.position = touch.position;
      this.deltaPosition = touch.deltaPosition;
      this.deltaTime = touch.deltaTime;
      this.tapCount = touch.tapCount;
      if (touch.phase == TouchPhase.Began)
        this.startPosition = this.position;
      this.phase = touch.phase != TouchPhase.Canceled ? touch.phase : TouchPhase.Ended;
      return this;
    }

    public TKTouch PopulateWithPosition(Vector3 currentPosition, TouchPhase touchPhase)
    {
      Vector2 vector2 = new Vector2(currentPosition.x, currentPosition.y);
      this.deltaPosition = !this.lastPosition.HasValue ? new Vector2(0.0f, 0.0f) : vector2 - this.lastPosition.Value;
      switch (touchPhase)
      {
        case TouchPhase.Began:
          this.phase = TouchPhase.Began;
          if ((double) Time.time < this.lastClickTime + this.multipleClickInterval)
            ++this.tapCount;
          else
            this.tapCount = 1;
          this.lastPosition = new Vector2?(vector2);
          this.startPosition = vector2;
          this.lastClickTime = (double) Time.time;
          break;
        case TouchPhase.Moved:
        case TouchPhase.Stationary:
          this.phase = (double) this.deltaPosition.sqrMagnitude != 0.0 ? TouchPhase.Moved : TouchPhase.Stationary;
          this.lastPosition = new Vector2?(vector2);
          break;
        case TouchPhase.Ended:
          this.phase = TouchPhase.Ended;
          this.lastPosition = new Vector2?();
          break;
      }
      this.position = vector2;
      return this;
    }

    public TKTouch populateFromMouse()
    {
      if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(0))
      {
        TouchPhase touchPhase = TouchPhase.Moved;
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonUp(0))
          touchPhase = TouchPhase.Canceled;
        else if (Input.GetMouseButtonUp(0))
          touchPhase = TouchPhase.Ended;
        else if (Input.GetMouseButtonDown(0))
          touchPhase = TouchPhase.Began;
        this.PopulateWithPosition((Vector3) new Vector2(Input.mousePosition.x, Input.mousePosition.y), touchPhase);
      }
      return this;
    }
  }
}
