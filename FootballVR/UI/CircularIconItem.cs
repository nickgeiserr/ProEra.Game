// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.CircularIconItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class CircularIconItem : CircularLayoutItem
  {
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Color _idleBgColor;
    [SerializeField]
    private Color _selectedBgColor;
    [SerializeField]
    private CanvasGroup _canvasGroup;

    public Sprite Icon
    {
      set => this._icon.sprite = value;
    }

    public override RectTransform TextGroup => (RectTransform) null;

    public override float alpha
    {
      set => this._canvasGroup.alpha = value;
    }

    protected override void OnStateChanged(bool state) => this._image.color = state ? this._selectedBgColor : this._idleBgColor;

    public override void SetVisible(bool state)
    {
      this._icon.enabled = state;
      this._image.enabled = state;
    }
  }
}
