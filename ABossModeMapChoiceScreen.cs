// Decompiled with JetBrains decompiler
// Type: ABossModeMapChoiceScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using Photon.Pun;
using System;
using System.Collections.Generic;
using TB12;
using UnityEngine;

public class ABossModeMapChoiceScreen : UIView
{
  [SerializeField]
  private MultiplayerStore _store;
  [SerializeField]
  private TouchButton _mapAButton;
  [SerializeField]
  private TouchButton _mapBButton;
  [SerializeField]
  private TouchButton _mapCButton;

  public override Enum ViewId { get; } = (Enum) EScreens.kBossModeMapChoice;

  protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
  {
    (EventHandle) UIHandle.Link((IButton) this._mapAButton, (System.Action) (() =>
    {
      if (!PhotonNetwork.IsMasterClient)
        return;
      MultiplayerEvents.LoadMap.Trigger(0);
    })),
    (EventHandle) UIHandle.Link((IButton) this._mapBButton, (System.Action) (() =>
    {
      if (!PhotonNetwork.IsMasterClient)
        return;
      MultiplayerEvents.LoadMap.Trigger(1);
    })),
    (EventHandle) UIHandle.Link((IButton) this._mapCButton, (System.Action) (() =>
    {
      if (!PhotonNetwork.IsMasterClient)
        return;
      MultiplayerEvents.LoadMap.Trigger(2);
    }))
  });

  protected override void WillAppear()
  {
  }

  protected override void DidAppear()
  {
  }

  protected override void WillDisappear()
  {
  }
}
