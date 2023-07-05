// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.AResultEnterTeamView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12.Activator.Data;
using UnityEngine;

namespace TB12.Activator.UI
{
  public class AResultEnterTeamView : UIPanel, ICircularLayoutDataSource
  {
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    private CircularLayout _layout;
    [SerializeField]
    private TeamLogoData _teamLogoData;
    [SerializeField]
    private CircularIconItem _itemPrefab;
    public Action onConfirmed;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._itemPrefab;

    public int itemCount => this._teamLogoData.Sprites.Length;

    public int TeamIndex => this._layout.CurrentIndex;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._okButton, new Action(this.OkHandler))
    });

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      if (itemIndex < 0 || itemIndex >= this.itemCount)
        Debug.LogError((object) string.Format("Bad index {0}", (object) itemIndex));
      else
        ((CircularIconItem) item).Icon = this._teamLogoData.Sprites[itemIndex];
    }

    protected override void DidDisappear()
    {
      Action onConfirmed = this.onConfirmed;
      if (onConfirmed == null)
        return;
      onConfirmed();
    }

    [ContextMenu("OK")]
    private void OkHandler() => this.Hide();
  }
}
