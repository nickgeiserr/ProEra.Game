// Decompiled with JetBrains decompiler
// Type: TB12.UI.CustomizeBodyView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using FootballWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TB12.UI
{
  public class CustomizeBodyView : MonoBehaviour, ICircularLayoutDataSource
  {
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    private Slider _heightSlider;
    [SerializeField]
    private TouchToggle _femaleToggle;
    [SerializeField]
    private TouchToggle _maleToggle;
    [SerializeField]
    private TouchToggleGroup _genderGroup;
    private const string LocalizationFaceType = "CustomizePlayer_Face_Face";
    [SerializeField]
    private TouchToggle _skinnyToggle;
    [SerializeField]
    private TouchToggle _athleteToggle;
    [SerializeField]
    private TouchToggle _fatToggle;
    [SerializeField]
    private TouchToggleGroup _bodyTypeGroup;
    private CharacterCustomizationStoreBase _store;
    [SerializeField]
    private CircularTextItem _itemPrefab;
    [SerializeField]
    private CircularLayout _scrollLayout;
    private string initAvatarPresetId;
    private bool isDirty;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._itemPrefab;

    public int itemCount => this.GetStore().GetDataCharactersPresets().Count;

    private PlayerProfile _playerProfile => SaveManager.GetPlayerProfile();

    private PlayerCustomization _customization => SaveManager.GetPlayerCustomization();

    private void Start()
    {
    }

    private CharacterCustomizationStore GetStore() => this._playerProfile.Customization.BodyType.Value == EBodyType.Female ? SaveManager.GetCharacterCustomizationStoreFemale() : SaveManager.GetCharacterCustomizationStoreMale();

    private void HandleGender(EBodyType value)
    {
      Debug.Log((object) ("HandleGender() " + value.ToString()));
      this.ResetScrollLayout();
    }

    private void ResetScrollLayout()
    {
      Debug.Log((object) "ResetScrollLayout()");
      this._scrollLayout.Initialize();
      int num = this.GetStore().GetDataCharactersPresets().Keys.ToList<string>().IndexOf(this._playerProfile.Customization.AvatarPresetId.Value);
      if (num < 0)
        return;
      this._scrollLayout.CurrentIndex = num;
    }

    private void HandleSlider(float val)
    {
      float num1 = 1.55f;
      float num2 = 1.92f - num1;
      this._playerProfile.Customization.BodyHeight.SetValue(num1 + val * num2);
    }

    private void HandleGenderFemale(bool selected)
    {
      if (!selected)
        return;
      this._playerProfile.Customization.BodyType.SetValue(EBodyType.Female);
      this.HandleGender(EBodyType.Female);
    }

    private void HandleGenderMale(bool selected)
    {
      if (!selected)
        return;
      this._playerProfile.Customization.BodyType.SetValue(EBodyType.Male);
      this.HandleGender(EBodyType.Male);
    }

    private void HandleSkinnyButton(bool selected)
    {
      if (!selected)
        return;
      this._playerProfile.Customization.BodyModelId.SetValue(1);
    }

    private void HandleAthleteButton(bool selected)
    {
      if (!selected)
        return;
      this._playerProfile.Customization.BodyModelId.SetValue(0);
    }

    private void HandleFatButton(bool selected)
    {
      if (!selected)
        return;
      this._playerProfile.Customization.BodyModelId.SetValue(2);
    }

    private void OnEnable()
    {
      this.WillAppear();
      this.DidAppear();
    }

    private void OnDisable() => this.WillDisappear();

    private void WillAppear()
    {
      this.ResetScrollLayout();
      this._femaleToggle.SetId((Enum) EBodyType.Female);
      this._maleToggle.SetId((Enum) EBodyType.Male);
      float num1 = 1.55f;
      float num2 = 1.92f - num1;
      this._heightSlider.value = (this._playerProfile.Customization.BodyHeight.Value - num1) / num2;
      this._heightSlider.onValueChanged.AddListener(new UnityAction<float>(this.HandleSlider));
      this._femaleToggle.OnStateChanged += new Action<bool>(this.HandleGenderFemale);
      this._maleToggle.OnStateChanged += new Action<bool>(this.HandleGenderMale);
      this._skinnyToggle.OnStateChanged += new Action<bool>(this.HandleSkinnyButton);
      this._athleteToggle.OnStateChanged += new Action<bool>(this.HandleAthleteButton);
      this._fatToggle.OnStateChanged += new Action<bool>(this.HandleFatButton);
      this._genderGroup.Register(this._maleToggle);
      this._genderGroup.Register(this._femaleToggle);
      this._bodyTypeGroup.Register(this._athleteToggle);
      this._bodyTypeGroup.Register(this._skinnyToggle);
      this._bodyTypeGroup.Register(this._fatToggle);
      this._customization.BodyModelId.OnValueChanged += new Action<int>(this.BodyModelIdOnOnValueChanged);
      this._customization.BodyType.OnValueChanged += new Action<EBodyType>(this.HandleGender);
      this._scrollLayout.OnCurrentIndexChanged += new Action<int>(this.HandleCurrentIndexChanged);
      this.isDirty = false;
      this.initAvatarPresetId = (string) this._customization.AvatarPresetId;
      this._genderGroup.SetValueById((int) this._customization.BodyType.GetValue());
      this._bodyTypeGroup.SetValueById((int) this._customization.BodyModelId.GetValue());
    }

    private void DidAppear() => SeasonLockerRoom.Instance?.SetActiveCustomizationItems(true);

    private void BodyModelIdOnOnValueChanged(int obj)
    {
    }

    private void WillDisappear()
    {
      this._scrollLayout.OnCurrentIndexChanged -= new Action<int>(this.HandleCurrentIndexChanged);
      this._customization.BodyModelId.OnValueChanged -= new Action<int>(this.BodyModelIdOnOnValueChanged);
      this._customization.BodyType.OnValueChanged -= new Action<EBodyType>(this.HandleGender);
      this._customization.SetDirty();
    }

    private void HandleCurrentIndexChanged(int currIndex)
    {
      KeyValuePair<string, CharacterParameters> keyValuePair = this.GetStore().GetDataCharactersPresets().ElementAt<KeyValuePair<string, CharacterParameters>>(currIndex);
      if (keyValuePair.Value == null)
        return;
      this._customization.AvatarPresetId.SetValue(keyValuePair.Value.ID);
      this._customization.SetDirty();
      this._scrollLayout.gameObject.SetActive(false);
      this._scrollLayout.gameObject.SetActive(true);
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      Debug.Log((object) string.Format("rebuilding face select {0}", (object) itemIndex));
      this.isDirty = true;
      CircularTextItem circularTextItem = (CircularTextItem) item;
      circularTextItem.IsLocalized = true;
      string[] args = new string[1]{ itemIndex.ToString() };
      circularTextItem.localizationText = "CustomizePlayer_Face_Face";
      circularTextItem.SetLocalizationStringArguments(args);
    }
  }
}
