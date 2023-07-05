// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchSliderHandle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class TouchSliderHandle : MonoBehaviour, ITouchGrabbable
  {
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    protected RectTransform _bg;

    public void SetPosition(Vector2 pos) => this._slider.value = pos.x / this._bg.rect.width;

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
