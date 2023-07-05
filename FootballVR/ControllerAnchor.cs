// Decompiled with JetBrains decompiler
// Type: FootballVR.ControllerAnchor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class ControllerAnchor : MonoBehaviour
  {
    public Transform targetTx;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake() => this._linksHandler.SetLinks(new List<EventHandle>()
    {
      EventHandle.Link<bool>((Variable<bool>) VRState.HandsVisible, new Action<bool>(this.HandleState))
    });

    private void HandleState(bool state) => this.gameObject.SetActive(state);

    private void OnDestroy() => this._linksHandler.Clear();
  }
}
