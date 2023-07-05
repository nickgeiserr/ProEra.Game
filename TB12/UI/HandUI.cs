// Decompiled with JetBrains decompiler
// Type: TB12.UI.HandUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class HandUI : UIPanel
  {
    [SerializeField]
    private TouchButton _spawnBallButton;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    protected override void OnInitialize() => this._linksHandler.SetLinks(new List<EventHandle>()
    {
      EventHandle.Link<bool>((Variable<bool>) AppState.HandUI, new Action<bool>(this.HandleState)),
      (EventHandle) UIHandle.Link((IButton) this._spawnBallButton, VREvents.SpawnBall)
    });

    private void HandleState(bool state)
    {
      if (state)
        this.Show();
      else
        this.Hide();
    }

    protected override void OnDeinitialize() => this._linksHandler?.Clear();
  }
}
