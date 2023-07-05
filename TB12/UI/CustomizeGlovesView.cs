// Decompiled with JetBrains decompiler
// Type: TB12.UI.CustomizeGlovesView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using FootballWorld;
using System;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class CustomizeGlovesView : MonoBehaviour, ICircularLayoutDataSource
  {
    [SerializeField]
    private GlovesConfigStore _store;
    [SerializeField]
    private CircularTextItem _itemPrefab;
    [SerializeField]
    private CircularLayout _scrollLayout;
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    private PlayerProfile _playerProfile;
    private PlayerCustomization _customization;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._itemPrefab;

    public int itemCount => this._store.glovesCount;

    private void Start()
    {
    }

    private void WillAppear()
    {
      this._scrollLayout.Initialize();
      int configId = this._store.GetConfigId((EGlovesId) (Variable<EGlovesId>) this._playerProfile.Customization.GloveId);
      if (configId < 0)
        return;
      this._scrollLayout.CurrentIndex = configId;
    }

    private void DidAppear() => this._scrollLayout.OnCurrentIndexChanged += new Action<int>(this.HandleCurrentIndexChanged);

    private void WillDisappear()
    {
      this._scrollLayout.OnCurrentIndexChanged -= new Action<int>(this.HandleCurrentIndexChanged);
      this._playerProfile.Customization.SetDirty();
    }

    private void OnEnable()
    {
      this.WillAppear();
      this.DidAppear();
    }

    private void OnDisable() => this.WillDisappear();

    private void HandleCurrentIndexChanged(int currIndex)
    {
      GlovesConfig glovesConfig = this._store.GetGlovesConfig(currIndex);
      if (glovesConfig == null)
        return;
      this._playerProfile.Customization.GloveId.SetValue(glovesConfig.Id);
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      GlovesConfig glovesConfig = this._store.GetGlovesConfig(itemIndex);
      CircularTextItem circularTextItem = (CircularTextItem) item;
      circularTextItem.IsLocalized = true;
      circularTextItem.localizationText = glovesConfig.name;
    }
  }
}
