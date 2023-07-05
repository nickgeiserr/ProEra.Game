// Decompiled with JetBrains decompiler
// Type: TB12.PlayImageItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using TMPro;
using UnityEngine;
using Vars;

namespace TB12
{
  public class PlayImageItem : CircularLayoutItem
  {
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private TextMeshProUGUI _audibleText;
    private Variable<PlayData> _playData = new Variable<PlayData>();
    private bool _bAudibleText = true;
    private const float MIN_ALPHA = 0.01f;

    public Variable<PlayData> playData => this._playData;

    public SpriteRenderer spriteRenderer => this._spriteRenderer;

    public override RectTransform TextGroup => (RectTransform) null;

    public override float alpha
    {
      set => this.AdjustAlpha(value);
    }

    private void AdjustAlpha(float value)
    {
      if ((double) value <= 0.0099999997764825821)
      {
        this.SetVisible(false);
      }
      else
      {
        this.SetVisible(true);
        this._spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, value);
      }
    }

    public override void SetVisible(bool state) => this._spriteRenderer.enabled = state;

    public void SetAudibleText(bool isAudible)
    {
      if (!((Object) this._audibleText != (Object) null))
        return;
      if (isAudible)
      {
        if (!this._bAudibleText)
          this._audibleText.SetText("AUDIBLE");
        this._bAudibleText = true;
      }
      else
      {
        if (this._bAudibleText)
          this._audibleText.SetText("PLAY CALL");
        this._bAudibleText = false;
      }
    }
  }
}
