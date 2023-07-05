// Decompiled with JetBrains decompiler
// Type: LeaderboardApi
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Newtonsoft.Json;
using ProEra.Game.ProEra.Web;
using ProEra.Web.Models.Leaderboard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

public class LeaderboardApi : PersistentSingleton<LeaderboardApi>
{
  private const string Platform = "windows";
  private const string LeaderboardPath = "Leaderboard";
  private static readonly string KeysPath = Path.Combine("Leaderboard", "Keys");

  private SaveKeycloakUserData _keycloakUserData => PersistentSingleton<SaveManager>.Instance.GetKeycloakUserData();

  private static Uri LeaderboardUri => new Uri(PersistentSingleton<ApiConfiguration>.Instance.LeaderboardApiHost, "Leaderboard");

  private static Uri KeysUri => new Uri(PersistentSingleton<ApiConfiguration>.Instance.LeaderboardApiHost, LeaderboardApi.KeysPath);

  protected override void Awake()
  {
    base.Awake();
    this.ValidateInspectorBinding();
  }

  public void GetKeys(Action<List<string>> onComplete) => this.StartCoroutine(this.GetRequest(LeaderboardApi.KeysUri, (Action<string>) (json => onComplete((JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>()).Where<string>((Func<string, bool>) (x => !x.IsNullOrEmpty())).ToList<string>()))));

  public async Task GetLeaderboard(string highScoreName, Action<LeaderboardModel> onComplete)
  {
    LeaderboardApi leaderboardApi = this;
    leaderboardApi.StartCoroutine(leaderboardApi.GetRequest(LeaderboardApi.GetLeaderboardUri(highScoreName), (Action<string>) (async json =>
    {
      LeaderboardModel leaderboardModel = await Task.Run<LeaderboardModel>((Func<LeaderboardModel>) (() => JsonConvert.DeserializeObject<LeaderboardModel>(json) ?? new LeaderboardModel()));
      onComplete(leaderboardModel);
    })));
  }

  private IEnumerator GetRequest(Uri uri, Action<string> onComplete)
  {
    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    {
      webRequest.SetRequestHeader("Authorization", "Bearer " + this._keycloakUserData.AuthToken);
      yield return (object) webRequest.SendWebRequest();
      string[] strArray = uri.AbsolutePath.Split('/', StringSplitOptions.None);
      int index = strArray.Length - 1;
      string str;
      switch (webRequest.result)
      {
        case UnityWebRequest.Result.Success:
          str = strArray[index] + ":\nReceived: " + webRequest.downloadHandler.text;
          break;
        case UnityWebRequest.Result.ProtocolError:
          str = strArray[index] + ": HTTP Error: " + webRequest.error + "pages[page]";
          break;
        case UnityWebRequest.Result.DataProcessingError:
          str = strArray[index] + ": Error: " + webRequest.error;
          break;
        default:
          str = string.Empty;
          break;
      }
      string message = str;
      if (!string.IsNullOrEmpty(message))
        Debug.Log((object) message);
      onComplete(webRequest.downloadHandler.text);
    }
  }

  private static Uri GetLeaderboardUri(string highScoreName) => new Uri(PersistentSingleton<ApiConfiguration>.Instance.LeaderboardApiHost, Path.Combine("Leaderboard", highScoreName + "?platform=windows"));

  private void ValidateInspectorBinding()
  {
  }
}
