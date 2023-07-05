// Decompiled with JetBrains decompiler
// Type: TB12.UI.IntelAnimationScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TB12.UI
{
  public class IntelAnimationScreen : UIView
  {
    [SerializeField]
    private TouchButton _playButton;
    [SerializeField]
    private TouchButton _pauseButton;
    [SerializeField]
    private TouchButton _stopButton;
    [SerializeField]
    private Slider _playheadSlider;
    [SerializeField]
    private IntelAnimationStore _store;

    public override Enum ViewId { get; } = (Enum) EScreens.kIntelAnimation;

    protected override void OnInitialize()
    {
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        this._store.progress.Link<float>((Action<float>) (progress => this._playheadSlider.SetValueWithoutNotify(progress))),
        (EventHandle) UIHandle.Link((IButton) this._playButton, (Action) (() => this._store.isPlaying.Value = true)),
        (EventHandle) UIHandle.Link((IButton) this._pauseButton, (Action) (() => this._store.isPlaying.Value = false)),
        (EventHandle) UIHandle.Link((IButton) this._stopButton, (Action) (() =>
        {
          this._store.isPlaying.Value = false;
          this._store.progress.Value = 0.0f;
        }))
      });
      this._playheadSlider.onValueChanged.AddListener((UnityAction<float>) (x =>
      {
        this._store.isPlaying.Value = false;
        this._store.progress.Value = x;
      }));
    }
  }
}
