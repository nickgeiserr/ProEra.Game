// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchHandle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FootballVR.UI
{
  public class TouchHandle : 
    MonoBehaviour,
    IBeginDragHandler,
    IEventSystemHandler,
    IDragHandler,
    IEndDragHandler,
    ITouchGrabbable
  {
    [SerializeField]
    protected RectTransform _bg;
    private Vector2 _dragPos;

    public event Action OnPositionChanged;

    public Vector3 position
    {
      get => this.transform.localPosition;
      set => this.transform.localPosition = value;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    private void ProcessDrag(Vector2 deltaVec)
    {
      this.position += (Vector3) deltaVec;
      Action onPositionChanged = this.OnPositionChanged;
      if (onPositionChanged == null)
        return;
      onPositionChanged();
    }

    public void SetPosition(Vector2 pos, bool silent = false)
    {
      this.position = (Vector3) pos;
      if (silent)
        return;
      Action onPositionChanged = this.OnPositionChanged;
      if (onPositionChanged == null)
        return;
      onPositionChanged();
    }

    public void OnTouchDragStart()
    {
    }

    public void OnTouchDrag(Vector3 delta, ITouchInput touchInput, bool usingLaserGrab = false)
    {
      if (usingLaserGrab && touchInput is HandData handData)
        this.SetPosition((Vector2) this._bg.InverseTransformPoint(handData.laserDragTransform.position));
      else
        this.SetPosition((Vector2) this._bg.InverseTransformPoint(touchInput.dragPosition));
    }

    public void OnTouchDragEnd(Vector3 delta, ITouchInput touchInput, bool usingLaserGrab = false)
    {
      if (usingLaserGrab && touchInput is HandData handData)
        this.SetPosition((Vector2) this._bg.InverseTransformPoint(handData.laserDragTransform.position));
      else
        this.SetPosition((Vector2) this._bg.InverseTransformPoint(touchInput.dragPosition));
    }
  }
}
