// Decompiled with JetBrains decompiler
// Type: TB12.UI.MultiplayerRoomSelector
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.Networked;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.UI
{
  public class MultiplayerRoomSelector : UIView, ICircularLayoutDataSource
  {
    [SerializeField]
    private CircularTextItem _itemPrefab;
    [SerializeField]
    private CircularLayout _scrollLayout;
    [SerializeField]
    private TouchButton _okButton;
    public Action<int> OnSelectedIndex;

    public override Enum ViewId { get; } = (Enum) ELockerRoomUI.kSearchRoomFilter;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._itemPrefab;

    public int itemCount => 4;

    protected override void OnInitialize()
    {
      this._laserIsAlwaysEnabled = true;
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) UIHandle.Link((IButton) this._okButton, (Action) (() => this.Hide()))
      });
    }

    protected override void WillAppear()
    {
      this._scrollLayout.Initialize();
      this._scrollLayout.CurrentIndex = 0;
    }

    protected override void DidAppear()
    {
      this._scrollLayout.OnCurrentIndexChanged += new Action<int>(this.HandleCurrentIndexChanged);
      Action<int> onSelectedIndex = this.OnSelectedIndex;
      if (onSelectedIndex == null)
        return;
      onSelectedIndex(0);
    }

    protected override void WillDisappear() => this._scrollLayout.OnCurrentIndexChanged -= new Action<int>(this.HandleCurrentIndexChanged);

    private void HandleCurrentIndexChanged(int currIndex)
    {
      Action<int> onSelectedIndex = this.OnSelectedIndex;
      if (onSelectedIndex == null)
        return;
      onSelectedIndex(currIndex);
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item) => ((CircularTextItem) item).localizationText = MultiplayerManager.GetMultiplayerAppStateByID(itemIndex.ToString());
  }
}
