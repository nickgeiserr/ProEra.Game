// Decompiled with JetBrains decompiler
// Type: UIContinueSeasonOptions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12;
using TB12.UI;
using UnityEngine;

public class UIContinueSeasonOptions : UIView
{
  [SerializeField]
  private TouchButton _continueSeasonButton;
  [SerializeField]
  private TouchButton _joinNewTeamButton;

  public override Enum ViewId => (Enum) EScreens.kContinueSeasonOrNew;

  protected override void OnInitialize()
  {
    base.OnInitialize();
    LinksHandler linksHandler = this.linksHandler;
    List<EventHandle> links = new List<EventHandle>();
    TouchButton continueSeasonButton = this._continueSeasonButton;
    links.Add(continueSeasonButton != null ? (EventHandle) continueSeasonButton.Link(new System.Action(this.HandleContinueSeason)) : (EventHandle) null);
    TouchButton joinNewTeamButton = this._joinNewTeamButton;
    links.Add(joinNewTeamButton != null ? (EventHandle) joinNewTeamButton.Link(new System.Action(this.HandlePickNewTeam)) : (EventHandle) null);
    linksHandler.SetLinks(links);
  }

  protected override void OnDeinitialize()
  {
    base.OnDeinitialize();
    this.linksHandler.Clear();
  }

  private void HandleContinueSeason()
  {
    ++SeasonModeManager.self.seasonModeData.seasonNumber;
    SeasonModeManager.self.StartNextSeason();
    UIDispatch.FrontScreen.CloseScreen();
  }

  private void HandlePickNewTeam()
  {
    SeasonModeManager.self.seasonModeData.seasonNumber = 0;
    SeasonModeManager.self.UICreateNewSeason();
  }

  protected override void WillAppear()
  {
    base.WillAppear();
    VRState.LocomotionEnabled.SetValue(false);
    Vector3 vector3 = new Vector3(-999f, 0.0f, 0.0f);
    Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    VREvents.BlinkMovePlayer.Trigger(1f, vector3, quaternion);
  }

  protected override void DidAppear() => base.DidAppear();

  protected override void WillDisappear()
  {
    base.WillDisappear();
    VRState.LocomotionEnabled.SetValue(true);
    Vector3 vector3 = new Vector3(0.0f, 0.0f, 0.0f);
    Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    VREvents.BlinkMovePlayer.Trigger(1f, vector3, quaternion);
  }
}
