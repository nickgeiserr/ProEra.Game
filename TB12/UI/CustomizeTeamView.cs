// Decompiled with JetBrains decompiler
// Type: TB12.UI.CustomizeTeamView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballVR.UI;
using FootballWorld;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class CustomizeTeamView : UIView, ICircularLayoutDataSource
  {
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private UniformLogoStore _store;
    [SerializeField]
    private CircularIconItem _itemPrefab;
    [SerializeField]
    private CircularLayout _scrollLayout;
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    private TouchToggle _homeToggle;
    [SerializeField]
    private TouchToggle _awayToggle;
    [SerializeField]
    private TouchButton _changeNumberButton;
    [SerializeField]
    private NumpadInputPanel _numpadInputPanel;

    public override Enum ViewId { get; } = (Enum) EScreens.kChooseTeam;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._itemPrefab;

    public int itemCount => this._store.UniformCount;

    protected override void OnInitialize()
    {
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._okButton.Link((Action) (() => UIDispatch.FrontScreen.DisplayView(EScreens.kCustomizeMain))),
        (EventHandle) this._changeNumberButton.Link(new Action(this.HandleChangeNumber)),
        this._playerProfile.Customization.HomeUniform.Link<bool>(new Action<bool>(this.HandleUniformFlags)),
        this._numpadInputPanel.InputString.Link<string>(new Action<string>(this.HandleNumberChanged))
      });
      this._numpadInputPanel.Initialize();
      this._homeToggle.SetId((Enum) ETeamUniformFlags.Home);
      this._awayToggle.SetId((Enum) ETeamUniformFlags.Away);
    }

    private void HandleNumberChanged(string numberInput)
    {
      int result;
      if (!this._numpadInputPanel.IsActive || !int.TryParse(numberInput, out result))
        return;
      this._playerProfile.Customization.UniformNumber.SetValue(result);
      this._playerProfile.Customization.SetDirty();
    }

    protected override void WillAppear()
    {
      this._scrollLayout.Initialize();
      int index = this._store.GetIndex((ETeamUniformId) (Variable<ETeamUniformId>) this._playerProfile.Customization.Uniform);
      if (index >= 0)
        this._scrollLayout.CurrentIndex = index;
      this._numpadInputPanel.Initialize("Jersey Number", this._playerProfile.Customization.UniformNumber.ToString());
      this._numpadInputPanel.Show();
    }

    protected override void DidAppear()
    {
      this._scrollLayout.OnCurrentIndexChanged += new Action<int>(this.HandleCurrentIndexChanged);
      this._homeToggle.OnValueChanged += new Action<TouchToggle>(this.HandleToggleValueChanged);
      this._awayToggle.OnValueChanged += new Action<TouchToggle>(this.HandleToggleValueChanged);
    }

    protected override void WillDisappear()
    {
      this._scrollLayout.OnCurrentIndexChanged -= new Action<int>(this.HandleCurrentIndexChanged);
      this._homeToggle.OnValueChanged -= new Action<TouchToggle>(this.HandleToggleValueChanged);
      this._awayToggle.OnValueChanged -= new Action<TouchToggle>(this.HandleToggleValueChanged);
      this._playerProfile.Customization.SetDirty();
    }

    private void HandleCurrentIndexChanged(int currIndex)
    {
      UniformLogo uniformLogo = this._store.GetUniformLogo(currIndex);
      if (uniformLogo == null)
        return;
      this._playerProfile.Customization.SetUniform(uniformLogo.teamId);
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      UniformLogo uniformLogo = this._store.GetUniformLogo(itemIndex);
      ((CircularIconItem) item).Icon = uniformLogo.teamLogo;
    }

    private void HandleToggleValueChanged(TouchToggle toggle)
    {
      if (!toggle.Selected)
        return;
      this._playerProfile.Customization.HomeUniform.SetValue(toggle.id == 2);
    }

    private async void HandleChangeNumber()
    {
      CustomizeTeamView customizeTeamView = this;
      customizeTeamView.Hide();
      await Task.Delay(200);
      NumberInputRequest numberRequest = new NumberInputRequest(customizeTeamView._playerProfile.Customization.UniformNumber.ToString())
      {
        title = "Enter your uniform number"
      };
      await UIDispatch.FrontScreen.ProcessDialogRequest<NumberInputRequest>(numberRequest);
      if (!numberRequest.IsComplete)
      {
        numberRequest = (NumberInputRequest) null;
      }
      else
      {
        int result;
        if (!int.TryParse(numberRequest.inputString, out result))
        {
          numberRequest = (NumberInputRequest) null;
        }
        else
        {
          customizeTeamView._playerProfile.Customization.UniformNumber.SetValue(result);
          customizeTeamView._playerProfile.Customization.SetDirty();
          await Task.Delay(200);
          UIDispatch.FrontScreen.DisplayView(EScreens.kChooseTeam);
          numberRequest = (NumberInputRequest) null;
        }
      }
    }

    private void HandleUniformFlags(bool homeUniform)
    {
      this._homeToggle.SetState(homeUniform);
      this._awayToggle.SetState(!homeUniform);
    }
  }
}
