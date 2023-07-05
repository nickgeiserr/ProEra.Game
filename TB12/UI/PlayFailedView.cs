// Decompiled with JetBrains decompiler
// Type: TB12.UI.PlayFailedView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class PlayFailedView : UIView
  {
    [SerializeField]
    private PlayModeStore _store;
    [SerializeField]
    private TouchButton _closeButton;
    [SerializeField]
    private TouchButton _retryButton;
    [SerializeField]
    private TextMeshProUGUI _headerText;
    [SerializeField]
    private TextMeshProUGUI _bodyText;

    public override Enum ViewId { get; } = (Enum) EScreens.kPlayFailed;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._closeButton, new Action(this.HandleBackButton)),
      (EventHandle) UIHandle.Link((IButton) this._retryButton, (AppEvent) AppEvents.Retry)
    });

    protected override void WillAppear()
    {
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game && (AppState.GameMode == EGameMode.kThrow || AppState.GameMode == EGameMode.kCatch))
      {
        this._headerText.text = "Level Failed";
        this._bodyText.text = "I bet you'll make it next time! Try it again.";
      }
      else if (AppState.GameMode == EGameMode.kTrainingCamp)
      {
        this._headerText.text = "Level Failed";
        this._bodyText.text = "I bet you'll make it next time! Try it again.";
      }
      else
      {
        switch (this._store.Result)
        {
          case EPlayResult.kHikeMissed:
            this._headerText.text = "HIKE MISSED";
            this._bodyText.text = "Press both index buttons when the ball is near your hands to catch it.";
            break;
          case EPlayResult.kSacked:
            this._headerText.text = "SACKED";
            this._bodyText.text = "Try to make the pass faster to avoid the tackle.";
            break;
          case EPlayResult.kPassMissed:
            this._headerText.text = "PASS MISSED";
            this._bodyText.text = "I bet you'll make it next time! Try it again.";
            break;
          case EPlayResult.kRunTooSlow:
            this._headerText.text = "TOO SLOW";
            this._bodyText.text = "Lean forward and swing your arms quicker for a higher speed!";
            break;
          default:
            this._headerText.text = "INCOMPLETE PASS";
            this._bodyText.text = "I bet you'll make it next time! Try it again.";
            break;
        }
      }
    }

    private void HandleBackButton()
    {
      if (AppState.GameMode != EGameMode.kTrainingCamp)
        AppEvents.LoadMainMenu.Trigger();
      else
        UIDispatch.FrontScreen.DisplayView(EScreens.kTrainingCampModeSelect);
    }
  }
}
