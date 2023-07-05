// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchUI2DScrollRect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class TouchUI2DScrollRect : MonoBehaviour
  {
    [SerializeField]
    private TouchUI2DScrollRect.Direction _direction;
    [SerializeField]
    private float _scrollMultiplier = 0.02f;
    [SerializeField]
    private float _maxScrollDist = 0.8f;
    [SerializeField]
    private float _deadZone = 1f / 1000f;
    [SerializeField]
    private ScrollRect _scrollRect;
    [SerializeField]
    private bool _invertScroll;
    [SerializeField]
    private RectTransform _scrollContent;
    private Vector3 _lastHandPos;

    public bool IsTouched { get; private set; } = true;

    public float MaxScrollDistance
    {
      get => this._maxScrollDist;
      set => this._maxScrollDist = value;
    }

    private void Start()
    {
      for (Transform transform = this.transform; (Object) this._scrollContent == (Object) null && (Object) transform != (Object) null; transform = transform.GetChild(0))
        this._scrollContent = transform.Find("Content")?.GetComponent<RectTransform>();
    }

    private void OnTriggerEnter(Collider other)
    {
      if (!other.name.ToLower().Contains("index"))
        return;
      this._lastHandPos = other.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
      if (other.name.ToLower().Contains("index"))
      {
        this.IsTouched = true;
        this.TryPerformScroll(other.transform.position);
      }
      else
        this.IsTouched = false;
    }

    public void TryPerformScroll(Vector3 touchPosition)
    {
      if ((Object) this._scrollRect != (Object) null)
        this.PerformScroll(touchPosition, this._scrollRect);
      else
        this.PerformScroll(touchPosition, this._scrollContent);
      this._lastHandPos = touchPosition;
    }

    private void PerformScroll(Vector3 touchPosition, ScrollRect scrollRect)
    {
      if ((double) Vector3.Distance(touchPosition, this._lastHandPos) <= (double) this._deadZone)
        return;
      Vector3 vector3 = touchPosition - this._lastHandPos;
      if (this._direction == TouchUI2DScrollRect.Direction.Vertical)
      {
        float num = Vector3.Dot(vector3.normalized, this._scrollContent.up);
        Vector2 normalizedPosition = scrollRect.normalizedPosition;
        float y = Mathf.Clamp(normalizedPosition.y + (this._invertScroll ? -num : num) * this._scrollMultiplier, 0.0f, this._maxScrollDist);
        scrollRect.normalizedPosition = new Vector2(normalizedPosition.x, y);
      }
      if (this._direction != TouchUI2DScrollRect.Direction.Horizontal)
        return;
      float num1 = Vector3.Dot(vector3.normalized, this._scrollContent.right);
      Vector2 normalizedPosition1 = scrollRect.normalizedPosition;
      float x = Mathf.Clamp(normalizedPosition1.x + (this._invertScroll ? -num1 : num1) * this._scrollMultiplier, 0.0f, this._maxScrollDist);
      scrollRect.normalizedPosition = new Vector2(x, normalizedPosition1.y);
    }

    private void PerformScroll(Vector3 touchPosition, RectTransform scrollContent)
    {
      if ((double) Vector3.Distance(touchPosition, this._lastHandPos) <= (double) this._deadZone)
        return;
      Vector3 vector3 = touchPosition - this._lastHandPos;
      if (this._direction == TouchUI2DScrollRect.Direction.Vertical)
      {
        float num = Vector3.Dot(vector3.normalized, scrollContent.up);
        float y = Mathf.Clamp(scrollContent.anchoredPosition.y - (this._invertScroll ? -num : num) * this._scrollMultiplier, 0.0f, this._maxScrollDist);
        scrollContent.anchoredPosition = new Vector2(0.0f, y);
      }
      if (this._direction != TouchUI2DScrollRect.Direction.Horizontal)
        return;
      float num1 = Vector3.Dot(vector3.normalized, scrollContent.right);
      float x = Mathf.Clamp(scrollContent.anchoredPosition.x + (this._invertScroll ? -num1 : num1) * this._scrollMultiplier, 0.0f, this._maxScrollDist);
      scrollContent.anchoredPosition = new Vector2(x, 0.0f);
    }

    private enum Direction
    {
      Vertical,
      Horizontal,
    }
  }
}
