// Decompiled with JetBrains decompiler
// Type: TB12.UI.GetUpScreen
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

namespace TB12.UI
{
  public class GetUpScreen : UIView
  {
    [SerializeField]
    private TouchButton _getUpButton;

    public override Enum ViewId { get; } = (Enum) EScreens.kGetUp;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) this._getUpButton.Link(VREvents.GetUp)
    });

    protected override void WillAppear()
    {
    }
  }
}
