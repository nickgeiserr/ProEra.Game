// Decompiled with JetBrains decompiler
// Type: TB12.UI.CustomizeMainView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using FootballVR;
using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using ProEra.Game.Sources.SeasonMode.SeasonTablet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

namespace TB12.UI
{
  public class CustomizeMainView : UIView
  {
    [SerializeField]
    private CanvasTabManager navBar;
    [SerializeField]
    private LocalizeStringEvent NavHeaderLabel;
    [SerializeField]
    private Color _textColorUnselected;
    [SerializeField]
    private Color _textColorDisabeled;
    [SerializeField]
    private TouchButton BackButton;
    [SerializeField]
    private LocalizeStringEvent BackButtonText;
    [SerializeField]
    private TextMeshProUGUI BackButtonTextTMP;
    private SeasonLockerRoom _seasonLockerRoom;
    [SerializeField]
    private PlayerProfile _playerProfile;
    private int currentFTUETab;
    private const string LocalizationHeaderCreatePlayer = "CustomizePlayer_Header_CreatePlayer";
    private const string LocalizationHeaderEditPlayer = "Lockerroom_TextLabel_EditPlayer";
    private const string LocalizationButtonNext = "CustomizePlayer_Button_Next";
    private const string LocalizationButtonDone = "CustomizePlayer_Button_Done";

    public override Enum ViewId { get; } = (Enum) EScreens.kCustomizeMain;

    protected override void OnInitialize() => this._seasonLockerRoom = SeasonLockerRoom.Instance;

    protected override void WillAppear()
    {
      base.WillAppear();
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this.BackButton.Link(new System.Action(this.HandleBack))
      });
    }

    protected override void DidAppear()
    {
      base.DidAppear();
      VRState.LocomotionEnabled.SetValue(false);
      this._seasonLockerRoom = SeasonLockerRoom.Instance;
      this.NavHeaderLabel.StringReference.TableEntryReference = (TableEntryReference) "Lockerroom_TextLabel_EditPlayer";
      bool newCustomization = (bool) this._playerProfile.Customization.IsNewCustomization;
      this.BackButtonText.StringReference.TableEntryReference = (TableEntryReference) (newCustomization ? "CustomizePlayer_Button_Next" : "CustomizePlayer_Button_Done");
      if (newCustomization)
      {
        PlayerData startingQb = SeasonModeManager.self.userTeamData.TeamDepthChart.GetStartingQB();
        this._playerProfile.Customization.FirstName.SetValue(startingQb.FirstName);
        this._playerProfile.Customization.LastName.SetValue(startingQb.LastName);
        this._playerProfile.Customization.UniformNumber.SetValue(startingQb.Number);
        this._playerProfile.Customization.BodyType.SetValue(EBodyType.Male);
        this._seasonLockerRoom?.SetActiveCustomizationItems(true, startingQb.AvatarID);
        float num1 = 1.55f;
        float num2 = 1.92f - num1;
        float num3 = ((float) startingQb.Height * 0.0254f - num1) / num2;
        this._playerProfile.Customization.BodyHeight.SetValue(num1 + num3 * num2);
        this.NavHeaderLabel.StringReference.TableEntryReference = (TableEntryReference) "CustomizePlayer_Header_CreatePlayer";
        this.currentFTUETab = 0;
        this.navBar.SetTabsEnabled(false);
        this.SwitchTabTo(this.currentFTUETab, true, 1f);
      }
      else
      {
        this.navBar.SetTabsEnabled(true);
        this._seasonLockerRoom?.SetActiveCustomizationItems(true);
      }
    }

    private async Task SwitchTabTo(int id, bool force, float delay)
    {
      await Task.Delay(TimeSpan.FromSeconds((double) delay));
      this.navBar.SimulateTabPress(id, force);
    }

    protected override void WillDisappear()
    {
      base.WillDisappear();
      VRState.LocomotionEnabled.SetValue(true);
    }

    private void HandleBack()
    {
      int num = this.navBar.tabCount - 1;
      if ((bool) this._playerProfile.Customization.IsNewCustomization && this.currentFTUETab <= num)
      {
        ++this.currentFTUETab;
        this.navBar.SimulateTabPress(this.currentFTUETab, true);
        if (this.currentFTUETab == num)
        {
          this.BackButtonText.StringReference.TableEntryReference = (TableEntryReference) "CustomizePlayer_Button_Done";
        }
        else
        {
          if (this.currentFTUETab < num)
            return;
          this._playerProfile.SetIsLamarVO(true);
          this._playerProfile.Customization.IsNewCustomization.SetValue(false);
          this._playerProfile.Customization.SetDirty();
          AnalyticEvents.Record<PlayerCreationCompletedArgs>(new PlayerCreationCompletedArgs(this._playerProfile.Customization.AvatarCustomized.Value));
          this._seasonLockerRoom?.SetActiveCustomizationItems(false);
          UIDispatch.FrontScreen.CloseScreen();
          VRState.LocomotionEnabled.SetValue(false);
          SeasonModeManager.self.UIRequestLogin();
        }
      }
      else
      {
        this._seasonLockerRoom?.SetActiveCustomizationItems(false);
        UIDispatch.FrontScreen.CloseScreen();
        VRState.LocomotionEnabled.SetValue(true);
        this._playerProfile.Customization.IsNewCustomization.SetValue(false);
        this._playerProfile.Customization.SetDirty();
        AnalyticEvents.Record<PlayerCreationCompletedArgs>(new PlayerCreationCompletedArgs(this._playerProfile.Customization.AvatarCustomized.Value));
      }
    }

    public void OnIllegalName(bool illegal)
    {
      this.BackButton.Interactable.SetValue(!illegal);
      if ((UnityEngine.Object) this.BackButtonTextTMP != (UnityEngine.Object) null)
        this.BackButtonTextTMP.color = illegal ? this._textColorDisabeled : this._textColorUnselected;
      if ((bool) this._playerProfile.Customization.IsNewCustomization)
        return;
      this.navBar.SetTabsEnabled(!illegal);
      this.navBar.HandleTextColors();
    }
  }
}
