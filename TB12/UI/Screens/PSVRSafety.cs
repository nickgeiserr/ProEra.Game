// Decompiled with JetBrains decompiler
// Type: TB12.UI.Screens.PSVRSafety
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.UI.Screens
{
  public class PSVRSafety : UIView
  {
    [SerializeField]
    private TouchButton _acceptPSVRSafetyButton;
    [SerializeField]
    private GameObject _PSVRSafetyMenu;
    private readonly RoutineHandle _inviteTravelRoutine = new RoutineHandle();

    public override Enum ViewId => (Enum) EScreens.kPSVRSafety;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) this._acceptPSVRSafetyButton.Link(new System.Action(this.HandlePSVRSafteyAccepting))
    });

    private void OnDestroy() => this.linksHandler.Clear();

    private void HandlePSVRSafteyAccepting() => this.HandleCleanup();

    private void HandleCleanup()
    {
      UIDispatch.FrontScreen.HideView(EScreens.kPSVRSafety);
      AppEvents.LoadMainMenu.Trigger();
    }
  }
}
