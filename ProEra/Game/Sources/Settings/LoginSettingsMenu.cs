// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.Settings.LoginSettingsMenu
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
  public class LoginSettingsMenu : MonoBehaviour
  {
    [SerializeField]
    private AccountSettingsMenu _accountSettingsMenu;
    [SerializeField]
    private TMP_InputField _usernameInput;
    [SerializeField]
    private TMP_InputField _passwordInput;
    [SerializeField]
    private TouchButton _usernameInputButton;
    [SerializeField]
    private TouchButton _passwordInputButton;
    [SerializeField]
    private TouchButton _loginButton;
    [SerializeField]
    private TextMeshProUGUI _loginStatusText;
    [SerializeField]
    private TouchButton _createAccountButton;
    [SerializeField]
    private CreateAccountSettingsMenu _createAccountSettingsMenu;

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
      PlayerApi.LoginSuccess += new Action<SaveKeycloakUserData>(this.OnAuthSuccess);
      PlayerApi.LoginFailure += new System.Action(this.OnAuthFailure);
    }

    private void OnDisable()
    {
      PlayerApi.LoginSuccess -= new Action<SaveKeycloakUserData>(this.OnAuthSuccess);
      PlayerApi.LoginFailure -= new System.Action(this.OnAuthFailure);
    }

    private void ValidateInspectorBinding()
    {
    }

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
      this._loginButton.onClick += (System.Action) (() =>
      {
        PersistentSingleton<PlayerApi>.Instance.WipeCachedSession();
        PersistentSingleton<PlayerApi>.Instance.Login(this._usernameInput.text, this._passwordInput.text);
      });
    }

    private void OnAuthSuccess(SaveKeycloakUserData userData) => this._loginStatusText.text = "Login Status: <color=green>SUCCESS</color> \nUser: " + userData.Username;

    private void OnAuthFailure() => this._loginStatusText.text = "Login Status: <color=red>FAILED</color>";
  }
}
