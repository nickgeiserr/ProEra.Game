// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.CircularTextItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class CircularTextItem : CircularLayoutItem
  {
    [SerializeField]
    private Image _image;
    [SerializeField]
    private LocalizeStringEvent _localizationEvent;
    [SerializeField]
    private TMP_Text _tmpText;
    [SerializeField]
    private RectTransform _textGroup;
    [SerializeField]
    private Color _idleBgColor;
    [SerializeField]
    private Color _idleTextColor;
    [SerializeField]
    private Color _selectedBgColor;
    [SerializeField]
    private Color _selectedTextColor;
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private CanvasGroup _textCanvasGroup;
    [SerializeField]
    private bool _isLocalized;

    public bool IsLocalized
    {
      get => this._isLocalized;
      set => this._isLocalized = value;
    }

    public string localizationText
    {
      get => this._tmpText.text;
      set
      {
        if (this.IsLocalized && (Object) this._localizationEvent != (Object) null)
        {
          this._localizationEvent.StringReference.TableEntryReference = (TableEntryReference) value;
        }
        else
        {
          if (!((Object) this._tmpText != (Object) null))
            return;
          this._tmpText.text = value;
        }
      }
    }

    public void SetLocalizationStringArguments(string[] args)
    {
      if (!((Object) this._localizationEvent != (Object) null))
        return;
      this._localizationEvent.StringReference.Arguments = (IList<object>) args;
    }

    public override RectTransform TextGroup => this._textGroup;

    public override float alpha
    {
      set
      {
        this._canvasGroup.alpha = value;
        this._textCanvasGroup.alpha = value;
      }
    }

    protected override void OnStateChanged(bool state)
    {
      if ((Object) this._image != (Object) null)
        this._image.color = state ? this._selectedBgColor : this._idleBgColor;
      if (!((Object) this._tmpText != (Object) null))
        return;
      this._tmpText.color = state ? this._selectedTextColor : this._idleTextColor;
    }

    public override void SetVisible(bool state)
    {
      this._tmpText.enabled = state;
      this._image.enabled = state;
    }

    private void OnDestroy()
    {
      if (!((Object) this._textGroup != (Object) null) || !((Object) this._textGroup.gameObject != (Object) null))
        return;
      Object.Destroy((Object) this._textGroup.gameObject);
    }
  }
}
