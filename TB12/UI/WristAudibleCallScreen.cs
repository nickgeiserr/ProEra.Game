// Decompiled with JetBrains decompiler
// Type: TB12.UI.WristAudibleCallScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System;
using UnityEngine;

namespace TB12.UI
{
  public class WristAudibleCallScreen : PlayCallView
  {
    private int _playIndex;
    [SerializeField]
    private WristPlayConfirmScreen _confirmButton;

    public override Enum ViewId { get; } = (Enum) EScreens.kWristAudiblePlay;

    private WristAudibleCallScreen() => this._audible = false;

    protected override void OnInitialize()
    {
    }

    protected override void WillAppear()
    {
      this._audible = MatchManager.instance.playManager.canUserCallAudible;
      this._store.Initialize(MatchManager.instance.playManager.canUserCallAudible);
      if (MatchManager.instance.playManager.canUserCallAudible)
        this._playSelection.ShowCurrentOnly = false;
      else
        this._playSelection.ShowCurrentOnly = true;
      this._playSelection.InvalidateCache();
      this._playSelection.Rebuild();
      PlaysCircularLayout playSelection = (PlaysCircularLayout) this._playSelection;
      if ((UnityEngine.Object) playSelection != (UnityEngine.Object) null)
        playSelection.SetIndexByPlayData((PlayData) PlaybookState.CurrentPlay);
      if (MatchManager.instance.playManager.canUserCallAudible)
      {
        this._playSelection.DragEnabled = true;
        AxisGameFlow.instance.SetPlayConfirmButtonVisibility(false);
      }
      else
      {
        this._playSelection.DragEnabled = false;
        AxisGameFlow.instance.SetPlayConfirmButtonVisibility(true);
      }
    }

    protected override void DidAppear()
    {
      this._playSelection.OnCurrentIndexChanged += new Action<int>(((PlayCallView) this).HandleSelectedPlayChanged);
      this._playSelection.OnUserRelease += new System.Action(this.HandleUserRelease);
      this._playSelection.OnDragStart += new System.Action(this.HandleDragStart);
      this._confirmButton.ResetConfirmHandler();
      this._confirmButton.OnConfirmed += new System.Action(this.PlayConfirmed);
    }

    private void HandleUserRelease() => AxisGameFlow.instance.SetPlayConfirmButtonVisibility(true);

    private void HandleDragStart() => AxisGameFlow.instance.SetPlayConfirmButtonVisibility(false);

    private void PlayConfirmed()
    {
      if ((!MatchManager.instance.playManager.canUserCallAudible || MatchManager.instance.playManager.UserAudiblePlayConfirmed) && MatchManager.instance.playManager.UserHuddlePlayConfirmed || !global::Game.CurrentPlayHasUserQBOnField)
        return;
      PlaybookState.CurrentPlayIndex.Value = this._playIndex;
      MatchManager.instance.playManager.HandleUserPlayConfirmed(MatchManager.instance.playManager.canUserCallAudible);
    }

    protected override void HandleSelectedPlayChanged(int playIndex) => this._playIndex = playIndex;

    protected override void WillDisappear()
    {
      this._playSelection.OnCurrentIndexChanged -= new Action<int>(((PlayCallView) this).HandleSelectedPlayChanged);
      this._playSelection.OnUserRelease -= new System.Action(this.HandleUserRelease);
      this._playSelection.OnDragStart -= new System.Action(this.HandleDragStart);
      this._confirmButton.OnConfirmed -= new System.Action(this.PlayConfirmed);
      if (!((UnityEngine.Object) AxisGameFlow.instance != (UnityEngine.Object) null))
        return;
      AxisGameFlow.instance.SetPlayConfirmButtonVisibility(false);
    }

    public void KeyboardPlayConfirm() => this.PlayConfirmed();

    public void KeyboardAudibleScroll(int dir)
    {
      PlaysCircularLayout playSelection = (PlaysCircularLayout) this._playSelection;
      if ((UnityEngine.Object) playSelection != (UnityEngine.Object) null)
        playSelection.SetNextIndex(dir);
      AxisGameFlow.instance.SetPlayConfirmButtonVisibility(true);
    }
  }
}
