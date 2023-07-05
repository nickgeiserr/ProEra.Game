// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UI.Screens.Shared.Customization.CustomizeNameView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using TB12.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace ProEra.Game.UI.Screens.Shared.Customization
{
  public class CustomizeNameView : MonoBehaviour
  {
    [SerializeField]
    private bool _saveInstantly;
    [SerializeField]
    private TMP_InputField _firstNameTextInput;
    [SerializeField]
    private TMP_InputField _lastNameTextInput;
    [SerializeField]
    private TMP_Text _blacklistMessage;
    [SerializeField]
    private Image _selectedImage;
    [SerializeField]
    private StaticKeyboardInput _keyboard;
    [SerializeField]
    private LocalizeStringEvent _fieldSelectButtonText;
    [SerializeField]
    private CustomizeMainView _MainView;
    private const string LocalizationButtonUp = "CustomizePlayer_Button_Up";
    private const string LocalizationButtonDown = "CustomizePlayer_Button_Down";

    private PlayerProfile _playerProfile => SaveManager.GetPlayerProfile();

    private bool _isValid => !((UnityEngine.Object) this._playerProfile == (UnityEngine.Object) null) && !((UnityEngine.Object) this._firstNameTextInput == (UnityEngine.Object) null) && !((UnityEngine.Object) this._lastNameTextInput == (UnityEngine.Object) null) && !((UnityEngine.Object) this._selectedImage == (UnityEngine.Object) null) && !((UnityEngine.Object) this._keyboard == (UnityEngine.Object) null);

    public bool isFilled => this._isValid && this._firstNameTextInput.text.Length > 0 && this._lastNameTextInput.text.Length > 0;

    private void Start()
    {
      if (!this._isValid)
        return;
      if (this._saveInstantly)
      {
        this._firstNameTextInput.onValueChanged.AddListener(new UnityAction<string>(this.SaveFirstAndLastName));
        this._lastNameTextInput.onValueChanged.AddListener(new UnityAction<string>(this.SaveFirstAndLastName));
      }
      else
      {
        this._firstNameTextInput.onEndEdit.AddListener(new UnityAction<string>(this.SaveFirstAndLastName));
        this._lastNameTextInput.onEndEdit.AddListener(new UnityAction<string>(this.SaveFirstAndLastName));
      }
    }

    private void OnEnable()
    {
      if (!this._isValid)
        return;
      int num = (bool) this._playerProfile.Customization.IsNewCustomization ? 1 : 0;
      string firstName = (string) this._playerProfile.Customization.FirstName;
      string lastName = (string) this._playerProfile.Customization.LastName;
      this._firstNameTextInput.SetTextWithoutNotify(firstName);
      this._lastNameTextInput.SetTextWithoutNotify(lastName);
      this.SaveFirstAndLastName("");
      this.HandleInputFieldSelection(0);
      this._keyboard.OnSelectedIndexChange += new Action<int>(this.HandleInputFieldSelection);
    }

    private void OnDisable() => this.SaveFirstAndLastName("");

    private void OnDestroy()
    {
      if (!this._isValid)
        return;
      this._firstNameTextInput.onValueChanged.RemoveAllListeners();
      this._lastNameTextInput.onValueChanged.RemoveAllListeners();
      this._firstNameTextInput.onEndEdit.RemoveAllListeners();
      this._lastNameTextInput.onEndEdit.RemoveAllListeners();
      this._keyboard.OnSelectedIndexChange -= new Action<int>(this.HandleInputFieldSelection);
    }

    private void SaveFirstAndLastName(string s)
    {
      if (!this._isValid)
        return;
      this._playerProfile.Customization.FirstName.SetValue(this._firstNameTextInput.text);
      this._playerProfile.Customization.LastName.SetValue(this._lastNameTextInput.text);
      if (string.IsNullOrEmpty(this._playerProfile.Customization.FirstName.Value) || string.IsNullOrWhiteSpace(this._playerProfile.Customization.FirstName.Value))
        this._playerProfile.Customization.FirstName.SetValue("Troy");
      if (string.IsNullOrEmpty(this._playerProfile.Customization.LastName.Value) || string.IsNullOrWhiteSpace(this._playerProfile.Customization.LastName.Value))
        this._playerProfile.Customization.LastName.SetValue("Jones");
      this._playerProfile.Customization.SetDirty();
    }

    private void HandleInputFieldSelection(int index)
    {
      if (!this._isValid)
        return;
      Vector3 vector3 = Vector3.forward * -4f;
      if (index != 0)
      {
        if (index != 1)
          return;
        this._selectedImage.transform.localPosition = this._lastNameTextInput.GetComponent<RectTransform>().localPosition + vector3;
        this._fieldSelectButtonText.StringReference.TableEntryReference = (TableEntryReference) "CustomizePlayer_Button_Up";
      }
      else
      {
        this._selectedImage.rectTransform.localPosition = this._firstNameTextInput.GetComponent<RectTransform>().localPosition + vector3;
        this._fieldSelectButtonText.StringReference.TableEntryReference = (TableEntryReference) "CustomizePlayer_Button_Down";
      }
    }

    public void HandleProfaneName(bool nameIsProfane)
    {
      this._blacklistMessage.enabled = nameIsProfane;
      this._MainView.OnIllegalName(nameIsProfane);
    }
  }
}
