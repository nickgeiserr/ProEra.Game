// Decompiled with JetBrains decompiler
// Type: TB12.UI.CustomizeFaceView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using FootballWorld;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TB12.UI
{
  public class CustomizeFaceView : UIView, ICircularLayoutDataSource
  {
    [SerializeField]
    private CharacterCustomizationStore _store;
    [SerializeField]
    private CircularTextItem _itemPrefab;
    [SerializeField]
    private CircularLayout _scrollLayout;
    [SerializeField]
    private TouchButton _okButton;
    private string initAvatarPresetId;
    private bool isDirty;
    private PlayerCustomization _playerCustomization;

    public override Enum ViewId { get; } = (Enum) EScreens.kChooseFace;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._itemPrefab;

    public int itemCount => this._store.GetDataCharactersPresets().Count;

    protected override void OnInitialize()
    {
      this._playerCustomization = SaveManager.GetPlayerCustomization();
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) UIHandle.Link((IButton) this._okButton, new System.Action(this.HandleConfirmButton))
      });
    }

    protected override void WillAppear()
    {
      this.isDirty = false;
      this.initAvatarPresetId = (string) this._playerCustomization.AvatarPresetId;
      this._scrollLayout.Initialize();
      int num = this._store.GetDataCharactersPresets().Keys.ToList<string>().IndexOf(this._playerCustomization.AvatarPresetId.Value);
      if (num < 0)
        return;
      this._scrollLayout.CurrentIndex = num;
    }

    protected override void DidAppear() => this._scrollLayout.OnCurrentIndexChanged += new Action<int>(this.HandleCurrentIndexChanged);

    protected override void WillDisappear()
    {
      if (this.isDirty)
      {
        this._playerCustomization.AvatarPresetId.Value = this.initAvatarPresetId;
        this._playerCustomization.SetDirty();
      }
      this._scrollLayout.OnCurrentIndexChanged -= new Action<int>(this.HandleCurrentIndexChanged);
    }

    private void HandleConfirmButton()
    {
      if (this.initAvatarPresetId != this._playerCustomization.AvatarPresetId.Value)
      {
        if (string.IsNullOrEmpty(this._playerCustomization.AvatarPresetId.Value) || string.IsNullOrWhiteSpace(this._playerCustomization.AvatarPresetId.Value) || !this._store.ExistPreset(this._playerCustomization.AvatarPresetId.Value))
          this._playerCustomization.AvatarPresetId.SetValue("default");
        this.isDirty = false;
        this._playerCustomization.AvatarCustomized.SetValue(false);
        this._playerCustomization.SetDirty();
      }
      UIDispatch.FrontScreen.DisplayView(EScreens.kCustomizeMain);
    }

    private void HandleCurrentIndexChanged(int currIndex)
    {
      KeyValuePair<string, CharacterParameters> keyValuePair = this._store.GetDataCharactersPresets().ElementAt<KeyValuePair<string, CharacterParameters>>(currIndex);
      if (keyValuePair.Value == null)
        return;
      this._playerCustomization.AvatarPresetId.SetValue(keyValuePair.Value.ID);
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      this.isDirty = true;
      KeyValuePair<string, CharacterParameters> keyValuePair = this._store.GetDataCharactersPresets().ElementAt<KeyValuePair<string, CharacterParameters>>(itemIndex);
      ((CircularTextItem) item).localizationText = keyValuePair.Value.ID;
    }
  }
}
