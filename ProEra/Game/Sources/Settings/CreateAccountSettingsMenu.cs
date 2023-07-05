// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.Settings.CreateAccountSettingsMenu
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.UI;
using ProEra.Web;
using Sources.Settings;
using System;
using TB12;
using TB12.UI;
using TMPro;
using UnityEngine;

namespace ProEra.Game.Sources.Settings
{
  public class CreateAccountSettingsMenu : MonoBehaviour
  {
    [SerializeField]
    private AccountSettingsMenu _accountSettingsMenu;
    [SerializeField]
    private TMP_InputField _usernameInput;
    [SerializeField]
    private TMP_InputField _passwordInput;
    [SerializeField]
    private TMP_InputField _passwordConfirmInput;
    [SerializeField]
    private TouchButton _usernameInputButton;
    [SerializeField]
    private TouchButton _passwordInputButton;
    [SerializeField]
    private TouchButton _passwordConfirmInputButton;
    [SerializeField]
    private TouchButton _createAccountButton;
    [SerializeField]
    private TextMeshProUGUI _createAccountStatusText;
    [SerializeField]
    private TouchButton _loginScreenButton;
    [SerializeField]
    private LoginSettingsMenu _loginSettingsMenu;

    private SaveKeycloakUserData _keycloakUserData => PersistentSingleton<SaveManager>.Instance.GetKeycloakUserData();

    private void Awake()
    {
      this.ValidateInspectorBinding();
      this.BindInteractionEvents();
    }

    private void OnEnable()
    {
      this._usernameInput.text = this._keycloakUserData.Username;
      this._passwordInput.text = this._keycloakUserData.Password;
      this._passwordConfirmInput.text = this._keycloakUserData.PasswordConfirmation;
      PlayerApi.CreateUserSuccess += new Action<SaveKeycloakUserData>(this.OnCreateUserSuccess);
      PlayerApi.CreateUserFailure += new System.Action(this.OnCreateUserFailure);
    }

    private void OnDisable()
    {
      PlayerApi.CreateUserSuccess -= new Action<SaveKeycloakUserData>(this.OnCreateUserSuccess);
      PlayerApi.CreateUserFailure -= new System.Action(this.OnCreateUserFailure);
    }

    private void ValidateInspectorBinding()
    {
    }

    private void OnCreateUserSuccess(SaveKeycloakUserData userData = null) => this._createAccountStatusText.text = "Player account created: <color=green>SUCCESS</color> \nUser: " + this._keycloakUserData.Username;

    private void OnCreateUserFailure() => this._createAccountStatusText.text = "Player account creation: <color=red>FAILED</color>/";

    private void BindInteractionEvents()
    {
      this._usernameInputButton.onClick += (System.Action) (async () =>
      {
        Debug.Log((object) "Username Input Selected");
        TextInputRequest userNameRequest = new TextInputRequest(string.Empty, true)
        {
          title = "Enter Username"
        };
        await UIDispatch.FrontScreen.ProcessDialogRequest<TextInputRequest>(userNameRequest);
        if (!userNameRequest.IsComplete)
        {
          userNameRequest = (TextInputRequest) null;
        }
        else
        {
          this._keycloakUserData.Username = userNameRequest.inputString;
          UIDispatch.FrontScreen.DisplayView(EScreens.kSettings);
          userNameRequest = (TextInputRequest) null;
        }
      });
      this._passwordInputButton.onClick += (System.Action) (async () =>
      {
        TextInputRequest passwordRequest = new TextInputRequest(string.Empty, true)
        {
          title = "Enter Password"
        };
        await UIDispatch.FrontScreen.ProcessDialogRequest<TextInputRequest>(passwordRequest);
        if (!passwordRequest.IsComplete)
        {
          passwordRequest = (TextInputRequest) null;
        }
        else
        {
          this._keycloakUserData.Password = passwordRequest.inputString;
          UIDispatch.FrontScreen.DisplayView(EScreens.kSettings);
          passwordRequest = (TextInputRequest) null;
        }
      });
      this._passwordConfirmInputButton.onClick += (System.Action) (async () =>
      {
        TextInputRequest passwordRequest = new TextInputRequest(string.Empty, true)
        {
          title = "Confirm Password"
        };
        await UIDispatch.FrontScreen.ProcessDialogRequest<TextInputRequest>(passwordRequest);
        this._keycloakUserData.PasswordConfirmation = passwordRequest.inputString;
        UIDispatch.FrontScreen.DisplayView(EScreens.kSettings);
        passwordRequest = (TextInputRequest) null;
      });
      this._createAccountButton.onClick += (System.Action) (() =>
      {
        if (this._keycloakUserData.Password != this._keycloakUserData.PasswordConfirmation)
        {
          Debug.LogWarning((object) "Passwords do not match");
          this._createAccountStatusText.text = "<color=red>Passwords do not match</color>";
        }
        else
          PersistentSingleton<PlayerApi>.Instance.CreateUser();
      });
    }
  }
}
