// Decompiled with JetBrains decompiler
// Type: TB12.PlayGraphicItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using TMPro;
using UnityEngine;
using Vars;

namespace TB12
{
  public class PlayGraphicItem : CircularLayoutItem
  {
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private PlayGraphicRenderer _graphicRenderer;
    [SerializeField]
    private TextMeshProUGUI _playName;
    private Variable<PlayData> _playData = new Variable<PlayData>();

    public PlayGraphicRenderer Renderer => this._graphicRenderer;

    public string Text
    {
      set => this._playName.text = value;
    }

    public Variable<PlayData> playData => this._playData;

    public override RectTransform TextGroup => (RectTransform) null;

    public override float alpha
    {
      set => this._canvasGroup.alpha = value;
    }

    public override void SetVisible(bool state) => this._canvas.enabled = state;
  }
}
