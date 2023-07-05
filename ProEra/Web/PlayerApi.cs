// Decompiled with JetBrains decompiler
// Type: ProEra.Web.PlayerApi
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Newtonsoft.Json;
using ProEra.Game.ProEra.Web;
using ProEra.Web.Models.Keycloak;
using ProEra.Web.Models.Leaderboard;
using ProEra.Web.Models.Player;
using Steamworks;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using WebSocketSharp;

namespace ProEra.Web
{
  public class PlayerApi : PersistentSingleton<PlayerApi>
  {
    private const string Platform = "windows";
    private const string HighScorePath = "HighScore";
    private const string PlayerPath = "Player";
    private const string LoginPath = "login";
    private const string RefreshPath = "refresh";
    private const string UserIdKey = "userId";
    private const string DisplayNameKey = "displayName";
    private const string PasswordKey = "password";
    private const string UsernamePath = "Username";
    private static readonly string _highScoreUriFormat = "{0}?highScoreName={1}";
    private static readonly string _bearerFormat = "Bearer {0}";
    private static readonly string _requestSucessFormat = "Get {0} score success: {1}";
    private string _displayName;

    private SaveKeycloakUserData _keycloakUserData => PersistentSingleton<SaveManager>.Instance.GetKeycloakUserData();

    public static event Action<SaveKeycloakUserData> LoginSuccess;

    public static event System.Action LoginFailure;

    public static event Action<SaveKeycloakUserData> CreateUserSuccess;

    public static event System.Action CreateUserFailure;

    public static event System.Action DeleteUserSuccess;

    public static event System.Action DeleteUserFailure;

    public static event Action<string> HighScoreChange;

    public static event System.Action SyncProEraScore;

    public bool IsLoggedIn => !string.IsNullOrEmpty(this._keycloakUserData.AuthToken);

    protected override void Awake()
    {
      base.Awake();
      this.ValidateInspectorBinding();
      if (!PersistentSingleton<SaveManager>.Exist())
        return;
      PersistentSingleton<SaveManager>.Instance.keycloakUserData.OnLoaded += new UnityAction(this.AttemptLoadCachedSession);
      this.AttemptLoadCachedSession();
    }

    public override void OnDestroy() => base.OnDestroy();

    private void ValidateInspectorBinding()
    {
    }

    public void GetHighScore(string highScoreName, Action<ListElementModel> onComplete)
    {
      if (string.IsNullOrEmpty(this._keycloakUserData.AuthToken))
        Debug.Log((object) "Attempting to GET ProEra score from player API with no auth token");
      this.StartCoroutine(this.GetHighScoreRequest(highScoreName, onComplete));
    }

    public void PutHighScore(
      Definitions.HighScore highScore,
      float value,
      Action<UnityWebRequest.Result> onComplete)
    {
      string highScoreName = Definitions.HighScoreNames[highScore];
      if (string.IsNullOrEmpty(this._keycloakUserData.AuthToken))
        Debug.LogWarning((object) "Attempting to PUT ProEra score from player API with no auth token");
      else
        this.StartCoroutine(this.PutFloatRequest(new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, "HighScore?highScoreName=" + highScoreName), value, (Action<UnityWebRequest.Result>) (result =>
        {
          Action<UnityWebRequest.Result> action = onComplete;
          if (action != null)
            action(result);
          Action<string> highScoreChange = PlayerApi.HighScoreChange;
          if (highScoreChange == null)
            return;
          highScoreChange(highScoreName);
        })));
    }

    public void Login(string username, string password) => this.StartCoroutine(this.PostLoginRequest(username, password));

    public void Login(string refreshToken) => this.StartCoroutine(this.PostRefreshRequest(refreshToken));

    public void CreateUser()
    {
      string str = SteamUser.GetSteamID().ToString();
      string personaName = SteamFriends.GetPersonaName();
      Debug.Log((object) ("CreateSteamUser: userId[" + str + "] displayName[" + personaName + "]"));
      this.CreateUser(str, personaName, str);
    }

    private void CreateUser(string userId, string displayName, string password)
    {
      if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(password))
        Debug.LogError((object) "Attempting to create user with invalid data");
      else
        this.StartCoroutine(this.PostUserRequest(userId, displayName, password));
    }

    public void DeleteUser() => this.StartCoroutine(this.DeleteUserRequest());

    public void GetDisplayName(Action<string> onComplete)
    {
      if (this.IsLoggedIn && !string.IsNullOrEmpty(this._displayName))
        onComplete(this._displayName);
      else
        this.StartCoroutine(this.GetDisplayNameRequest(onComplete));
    }

    public void UpdateUsername(string username)
    {
      if (!this.IsLoggedIn || string.IsNullOrEmpty(username))
        return;
      this.StartCoroutine(this.PutUsernameRequest(username));
    }

    public void SyncScoreOnReturnToLockerRoom()
    {
      System.Action syncProEraScore = PlayerApi.SyncProEraScore;
      if (syncProEraScore == null)
        return;
      syncProEraScore();
    }

    private IEnumerator GetDisplayNameRequest(Action<string> onComplete)
    {
      using (UnityWebRequest request = UnityWebRequest.Get(new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, "Player/displayName")))
      {
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this._keycloakUserData.AuthToken);
        yield return (object) request.SendWebRequest();
        switch (request.result)
        {
          case UnityWebRequest.Result.Success:
            Debug.Log((object) ("Get displayName success: " + request.result.ToString()));
            this._displayName = request.downloadHandler.text;
            onComplete(this._displayName);
            break;
          case UnityWebRequest.Result.ConnectionError:
            Debug.LogError((object) ("Connection error: " + request.error));
            break;
          case UnityWebRequest.Result.ProtocolError:
            Debug.LogError((object) ("Protocol error: " + request.error));
            break;
          case UnityWebRequest.Result.DataProcessingError:
            Debug.LogError((object) ("Data processing error: " + request.error));
            break;
          default:
            Debug.LogError((object) ("request Failed: " + request.error));
            break;
        }
        if (request.result != UnityWebRequest.Result.Success)
          onComplete((string) null);
      }
    }

    private IEnumerator DeleteUserRequest()
    {
      UnityWebRequest request = UnityWebRequest.Delete(new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, "Player"));
      request.SetRequestHeader("Authorization", "Bearer " + this._keycloakUserData.AuthToken);
      yield return (object) request.SendWebRequest();
      switch (request.result)
      {
        case UnityWebRequest.Result.Success:
          Debug.Log((object) ("User successfully deleted from the database: " + this._keycloakUserData.Username));
          this.WipeCachedSession();
          this.SaveCachedSession();
          System.Action deleteUserSuccess = PlayerApi.DeleteUserSuccess;
          if (deleteUserSuccess == null)
            break;
          deleteUserSuccess();
          break;
        case UnityWebRequest.Result.ConnectionError:
          Debug.LogError((object) ("Connection error: " + request.error));
          System.Action deleteUserFailure1 = PlayerApi.DeleteUserFailure;
          if (deleteUserFailure1 == null)
            break;
          deleteUserFailure1();
          break;
        case UnityWebRequest.Result.ProtocolError:
          Debug.LogError((object) ("Protocol error: " + request.error));
          System.Action deleteUserFailure2 = PlayerApi.DeleteUserFailure;
          if (deleteUserFailure2 == null)
            break;
          deleteUserFailure2();
          break;
        case UnityWebRequest.Result.DataProcessingError:
          Debug.LogError((object) ("Data processing error: " + request.error));
          System.Action deleteUserFailure3 = PlayerApi.DeleteUserFailure;
          if (deleteUserFailure3 == null)
            break;
          deleteUserFailure3();
          break;
        default:
          Debug.LogError((object) ("request error: " + request.error));
          break;
      }
    }

    private IEnumerator PostUserRequest(string userId, string displayName, string password)
    {
      UnityWebRequest request = UnityWebRequest.Post(new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, "Player?userId=" + userId + "&displayName=" + displayName + "&password=" + password + "&platform=windows"), string.Empty);
      request.SetRequestHeader("Accept", "*/*");
      yield return (object) request.SendWebRequest();
      switch (request.result)
      {
        case UnityWebRequest.Result.Success:
          string text = request.downloadHandler.text;
          Debug.Log((object) ("User successfully created in the database!: " + text));
          AuthResponseModel authResponseModel = JsonConvert.DeserializeObject<AuthResponseModel>(text);
          this._keycloakUserData.Username = authResponseModel.Username;
          this._keycloakUserData.AuthToken = authResponseModel.AuthToken;
          this._keycloakUserData.RefreshToken = authResponseModel.RefreshToken;
          this.SaveCachedSession();
          Action<SaveKeycloakUserData> createUserSuccess = PlayerApi.CreateUserSuccess;
          if (createUserSuccess != null)
            createUserSuccess(this._keycloakUserData);
          Action<SaveKeycloakUserData> loginSuccess = PlayerApi.LoginSuccess;
          if (loginSuccess == null)
            break;
          loginSuccess(this._keycloakUserData);
          break;
        case UnityWebRequest.Result.ConnectionError:
          Debug.LogError((object) ("Connection error: " + request.error));
          System.Action createUserFailure1 = PlayerApi.CreateUserFailure;
          if (createUserFailure1 != null)
            createUserFailure1();
          System.Action loginFailure1 = PlayerApi.LoginFailure;
          if (loginFailure1 == null)
            break;
          loginFailure1();
          break;
        case UnityWebRequest.Result.ProtocolError:
          Debug.LogError((object) ("Protocol error: " + request.error));
          System.Action createUserFailure2 = PlayerApi.CreateUserFailure;
          if (createUserFailure2 != null)
            createUserFailure2();
          System.Action loginFailure2 = PlayerApi.LoginFailure;
          if (loginFailure2 == null)
            break;
          loginFailure2();
          break;
        case UnityWebRequest.Result.DataProcessingError:
          Debug.LogError((object) ("Data processing error: " + request.error));
          System.Action createUserFailure3 = PlayerApi.CreateUserFailure;
          if (createUserFailure3 != null)
            createUserFailure3();
          System.Action loginFailure3 = PlayerApi.LoginFailure;
          if (loginFailure3 == null)
            break;
          loginFailure3();
          break;
        default:
          Debug.LogError((object) ("request error: " + request.error));
          break;
      }
    }

    private IEnumerator PutUsernameRequest(string username)
    {
      UnityWebRequest request = UnityWebRequest.Put(new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, Path.Combine("Player", "Username") + "?username=" + username), string.Empty);
      request.SetRequestHeader("Accept", "*/*");
      request.SetRequestHeader("Content-Type", "application/json");
      request.SetRequestHeader("Authorization", string.Format(PlayerApi._bearerFormat, (object) this._keycloakUserData.AuthToken));
      yield return (object) request.SendWebRequest();
      switch (request.result)
      {
        case UnityWebRequest.Result.Success:
          string text = request.downloadHandler.text;
          Debug.Log((object) "username successfully updated!");
          this.SaveCachedSession();
          Action<SaveKeycloakUserData> loginSuccess = PlayerApi.LoginSuccess;
          if (loginSuccess == null)
            break;
          loginSuccess(this._keycloakUserData);
          break;
        case UnityWebRequest.Result.ConnectionError:
          Debug.LogError((object) ("Connection error: " + request.error));
          System.Action loginFailure1 = PlayerApi.LoginFailure;
          if (loginFailure1 == null)
            break;
          loginFailure1();
          break;
        case UnityWebRequest.Result.ProtocolError:
          Debug.LogError((object) ("Protocol error: " + request.error));
          System.Action loginFailure2 = PlayerApi.LoginFailure;
          if (loginFailure2 == null)
            break;
          loginFailure2();
          break;
        case UnityWebRequest.Result.DataProcessingError:
          Debug.LogError((object) ("Data processing error: " + request.error));
          System.Action loginFailure3 = PlayerApi.LoginFailure;
          if (loginFailure3 == null)
            break;
          loginFailure3();
          break;
        default:
          Debug.LogError((object) ("request error: " + request.error));
          break;
      }
    }

    private IEnumerator PostLoginRequest(string username, string password)
    {
      using (UnityWebRequest request = UnityWebRequest.Post(new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, Path.Combine("Player", "login?userId=" + username + "&password=" + password)), string.Empty))
      {
        yield return (object) request.SendWebRequest();
        this.ProcessLoginResponse(request);
      }
    }

    private IEnumerator PostRefreshRequest(string refreshToken)
    {
      using (UnityWebRequest request = UnityWebRequest.Post(new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, Path.Combine("Player", "refresh") + "?refreshToken=" + refreshToken), string.Empty))
      {
        yield return (object) request.SendWebRequest();
        this.ProcessLoginResponse(request);
      }
    }

    private IEnumerator GetHighScoreRequest(
      string highScoreName,
      Action<ListElementModel> onComplete)
    {
      using (UnityWebRequest request = UnityWebRequest.Get(new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, string.Format(PlayerApi._highScoreUriFormat, (object) "HighScore", (object) highScoreName))))
      {
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", string.Format(PlayerApi._bearerFormat, (object) this._keycloakUserData.AuthToken));
        yield return (object) request.SendWebRequest();
        switch (request.result)
        {
          case UnityWebRequest.Result.Success:
            Debug.Log((object) string.Format(PlayerApi._requestSucessFormat, (object) highScoreName, (object) request.result));
            onComplete(JsonConvert.DeserializeObject<ListElementModel>(request.downloadHandler.text));
            break;
          case UnityWebRequest.Result.ConnectionError:
            Debug.LogError((object) ("Connection error: " + request.error));
            break;
          case UnityWebRequest.Result.ProtocolError:
            Debug.LogError((object) ("Protocol error: " + request.error));
            break;
          case UnityWebRequest.Result.DataProcessingError:
            Debug.LogError((object) ("Data processing error: " + request.error));
            break;
          default:
            Debug.LogError((object) ("request Failed: " + request.error));
            break;
        }
      }
    }

    private IEnumerator PutFloatRequest(
      Uri uri,
      float value,
      Action<UnityWebRequest.Result> onComplete)
    {
      using (UnityWebRequest request = UnityWebRequest.Put(uri, JsonConvert.SerializeObject((object) new HighScoreModel()
      {
        Score = value,
        ShareWithOthers = true
      })))
      {
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this._keycloakUserData.AuthToken);
        yield return (object) request.SendWebRequest();
        string[] strArray = uri.AbsolutePath.Split('/', StringSplitOptions.None);
        int index = strArray.Length - 1;
        string str;
        switch (request.result)
        {
          case UnityWebRequest.Result.Success:
            str = strArray[index] + ":\nReceived: " + request.downloadHandler.text;
            break;
          case UnityWebRequest.Result.ProtocolError:
            str = strArray[index] + ": HTTP Error: " + request.error + strArray[index];
            break;
          case UnityWebRequest.Result.DataProcessingError:
            str = strArray[index] + ": Error: " + request.error;
            break;
          default:
            str = string.Empty;
            break;
        }
        string message = str;
        if (!string.IsNullOrEmpty(message))
          Debug.Log((object) message);
        onComplete(request.result);
      }
    }

    private static Uri GetHighScoreUri(string highScoreName) => new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, Path.Combine("HighScore", highScoreName));

    public void WipeCachedSession()
    {
      this._keycloakUserData.Clear();
      this._displayName = string.Empty;
      AppEvents.SaveKeycloak.Trigger();
    }

    private void SaveCachedSession()
    {
      AppEvents.SaveKeycloak.Trigger();
      Debug.Log((object) "Saved cached session data");
    }

    private void AttemptLoadCachedSession()
    {
      if (!string.IsNullOrEmpty(this._keycloakUserData.RefreshToken))
      {
        this.Login(this._keycloakUserData.RefreshToken);
      }
      else
      {
        if (!this._keycloakUserData.Username.IsNullOrEmpty() || !this._keycloakUserData.AuthToken.IsNullOrEmpty())
          return;
        this.CreateUser();
      }
    }

    private void ProcessLoginResponse(UnityWebRequest request)
    {
      switch (request.result)
      {
        case UnityWebRequest.Result.Success:
          string text = request.downloadHandler.text;
          Debug.Log((object) ("Received: " + text));
          Debug.Log((object) ("User successfully logged in: " + text));
          AuthResponseModel authResponseModel = JsonConvert.DeserializeObject<AuthResponseModel>(text);
          this._keycloakUserData.Username = authResponseModel.Username;
          this._keycloakUserData.AuthToken = authResponseModel.AuthToken;
          this._keycloakUserData.RefreshToken = authResponseModel.RefreshToken;
          this.SaveCachedSession();
          Action<SaveKeycloakUserData> loginSuccess = PlayerApi.LoginSuccess;
          if (loginSuccess == null)
            break;
          loginSuccess(this._keycloakUserData);
          break;
        case UnityWebRequest.Result.ConnectionError:
          Debug.LogError((object) ("Connection Error: " + request.error));
          System.Action loginFailure1 = PlayerApi.LoginFailure;
          if (loginFailure1 == null)
            break;
          loginFailure1();
          break;
        case UnityWebRequest.Result.ProtocolError:
          Debug.LogError((object) ("HTTP Error: " + request.error));
          System.Action loginFailure2 = PlayerApi.LoginFailure;
          if (loginFailure2 == null)
            break;
          loginFailure2();
          break;
        case UnityWebRequest.Result.DataProcessingError:
          Debug.LogError((object) ("Error: " + request.error));
          System.Action loginFailure3 = PlayerApi.LoginFailure;
          if (loginFailure3 == null)
            break;
          loginFailure3();
          break;
        default:
          Debug.LogError((object) ("request Error: " + request.error));
          System.Action loginFailure4 = PlayerApi.LoginFailure;
          if (loginFailure4 == null)
            break;
          loginFailure4();
          break;
      }
    }

    public void Ping(Action<bool> onComplete) => this.StartCoroutine(this.PingRequest(onComplete));

    private IEnumerator PingRequest(Action<bool> onComplete)
    {
      using (UnityWebRequest request = UnityWebRequest.Get(new Uri(PersistentSingleton<ApiConfiguration>.Instance.PlayerApiHost, Path.Combine("Player", "Ping"))))
      {
        yield return (object) request.SendWebRequest();
        switch (request.result)
        {
          case UnityWebRequest.Result.Success:
            Action<bool> action1 = onComplete;
            if (action1 != null)
            {
              action1(true);
              break;
            }
            break;
          case UnityWebRequest.Result.ConnectionError:
            Action<bool> action2 = onComplete;
            if (action2 != null)
              action2(false);
            Debug.LogError((object) ("Connection Error: " + request.error));
            break;
          case UnityWebRequest.Result.ProtocolError:
            Action<bool> action3 = onComplete;
            if (action3 != null)
              action3(false);
            Debug.LogError((object) ("HTTP Error: " + request.error));
            break;
          case UnityWebRequest.Result.DataProcessingError:
            Action<bool> action4 = onComplete;
            if (action4 != null)
              action4(false);
            Debug.LogError((object) ("Error: " + request.error));
            break;
        }
      }
    }
  }
}
