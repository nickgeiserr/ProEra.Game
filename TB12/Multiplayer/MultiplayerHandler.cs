// Decompiled with JetBrains decompiler
// Type: TB12.Multiplayer.MultiplayerHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Data;
using Framework.Networked;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.Multiplayer
{
  public class MultiplayerHandler : MonoBehaviour
  {
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Start()
    {
      NetworkedEventsHandler.Initialize();
      MultiplayerSettings instance = ScriptableSingleton<MultiplayerSettings>.Instance;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        instance.SendRate.Link<int>((Action<int>) (rate => PhotonNetwork.SendRate = rate)),
        instance.SerializationRate.Link<int>((Action<int>) (rate => PhotonNetwork.SerializationRate = rate))
      });
      MultiplayerManager.OnErrorDisconnect += new Action(AppEvents.LoadMainMenu.Trigger);
    }

    private void OnDestroy()
    {
      this._linksHandler.Clear();
      MultiplayerManager.OnErrorDisconnect -= new Action(AppEvents.LoadMainMenu.Trigger);
    }
  }
}
