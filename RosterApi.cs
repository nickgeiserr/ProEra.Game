// Decompiled with JetBrains decompiler
// Type: RosterApi
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;
using ProEra.Game.ProEra.Web;
using ProEra.Game.Sources.TeamData;
using ProEra.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UDB;
using UnityEngine;
using UnityEngine.Networking;

public class RosterApi : PersistentSingleton<RosterApi>
{
  private static readonly string RosterPath = "Roster";
  private static readonly string UpdatedRostersPath = "UpdatedRosters";

  private static SaveKeycloakUserData KeycloakUserData => PersistentSingleton<SaveManager>.Instance.GetKeycloakUserData();

  public event Action<string> GetRosterSuccess;

  public bool UpdateRostersOnStart { get; set; }

  protected override void Awake()
  {
    AotHelper.EnsureType<List<RosterPlayerData>>();
    PlayerApi.LoginSuccess += (Action<SaveKeycloakUserData>) (keycloakUserData =>
    {
      if (!this.UpdateRostersOnStart)
        return;
      this.UpdateRosters((int[]) null, keycloakUserData: keycloakUserData);
    });
  }

  public void GetRoster(string cityAbbreviation)
  {
    if (!PersistentSingleton<PlayerApi>.Instance.IsLoggedIn)
      Debug.LogError((object) "Attempting to get roster while not logged in");
    else if (string.IsNullOrEmpty(cityAbbreviation) || cityAbbreviation.Length != 3)
      Debug.LogError((object) "Invalid city abbreviation format");
    else
      this.StartCoroutine(this.GetRosterRequest(cityAbbreviation));
  }

  public void UpdateRosters(
    int[] rosterVersions,
    System.Action onComplete = null,
    SaveKeycloakUserData keycloakUserData = null)
  {
    Debug.Log((object) "RosterApi: Updating rosters...");
    this.StartCoroutine(this.UpdateRosterRequest(rosterVersions, keycloakUserData, onComplete));
  }

  private IEnumerator GetRosterRequest(string cityAbbreviation)
  {
    Uri uri = new Uri(PersistentSingleton<ApiConfiguration>.Instance.RosterApiHost, Path.Combine(RosterApi.RosterPath, cityAbbreviation));
    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    {
      webRequest.SetRequestHeader("Authorization", "Bearer " + RosterApi.KeycloakUserData.AuthToken);
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
          str = strArray[index] + ": HTTP Error: " + webRequest.error + strArray[index];
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
      if (webRequest.result == UnityWebRequest.Result.Success)
      {
        Action<string> getRosterSuccess = this.GetRosterSuccess;
        if (getRosterSuccess != null)
          getRosterSuccess(webRequest.downloadHandler.text);
      }
      Debug.Log((object) ("RosterApi: converted roster: " + ((object) JsonConvert.DeserializeObject<RosterDataObject>(webRequest.downloadHandler.text))?.ToString()));
    }
  }

  private IEnumerator UpdateRosterRequest(
    int[] rosterVersions,
    SaveKeycloakUserData keycloakUserData = null,
    System.Action onComplete = null)
  {
    if (rosterVersions == null)
    {
      Debug.Log((object) "RosterApi: No versions supplied. Updating full roster list...");
      rosterVersions = new int[32];
      for (int index = 0; index < rosterVersions.Length; ++index)
        rosterVersions[index] = -1;
    }
    if (rosterVersions.Length != 32)
    {
      Debug.LogError((object) "rosterVersions must be exactly 32 elements");
    }
    else
    {
      Uri uri = new Uri(PersistentSingleton<ApiConfiguration>.Instance.RosterApiHost, Path.Combine(RosterApi.RosterPath, RosterApi.UpdatedRostersPath));
      Debug.Log((object) ("RosterApi: Updating rosters via uri: " + uri?.ToString()));
      Debug.Log((object) ("RosterApi: SaveManager auth token: " + RosterApi.KeycloakUserData.AuthToken));
      Debug.Log((object) ("RosterApi: callback auth token: " + keycloakUserData?.AuthToken));
      UnityWebRequest webRequest = new UnityWebRequest(uri, "POST");
      webRequest.SetRequestHeader("Authorization", "Bearer " + ((RosterApi.KeycloakUserData.AuthToken.IsNullOrEmpty() ? keycloakUserData?.AuthToken : RosterApi.KeycloakUserData.AuthToken) ?? string.Empty));
      webRequest.SetRequestHeader("Content-Type", "application/json");
      using (UploadHandlerRaw uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) rosterVersions))))
      {
        uploadHandler.contentType = "application/json";
        webRequest.uploadHandler = (UploadHandler) uploadHandler;
        webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        Debug.Log((object) "RosterApi: Sending Update Roster web request...");
        webRequest.SendWebRequest();
        while (!webRequest.isDone)
          yield return (object) null;
        string[] strArray = uri.AbsolutePath.Split('/', StringSplitOptions.None);
        int index1 = strArray.Length - 1;
        string str;
        switch (webRequest.result)
        {
          case UnityWebRequest.Result.Success:
            str = strArray[index1] + ":\nReceived: " + webRequest.downloadHandler.text;
            break;
          case UnityWebRequest.Result.ProtocolError:
            str = strArray[index1] + ": HTTP Error: " + webRequest.error + strArray[index1];
            break;
          case UnityWebRequest.Result.DataProcessingError:
            str = strArray[index1] + ": Error: " + webRequest.error;
            break;
          default:
            str = string.Empty;
            break;
        }
        Debug.Log((object) ("RosterApi: Update Roster web request completed: " + str));
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
          AotHelper.EnsureType<RosterPlayerData>();
          AotHelper.EnsureList<RosterPlayerData>();
          RosterDataObject[] rosterDataObjectArray = JsonConvert.DeserializeObject<RosterDataObject[]>(webRequest.downloadHandler.text);
          RosterSaveData rosterSaveData = PersistentSingleton<SaveManager>.Instance.GetRosterSaveData();
          for (int index2 = 0; index2 < rosterDataObjectArray.Length; ++index2)
            rosterSaveData.rosters[index2] = (UnityEngine.Object) rosterDataObjectArray[index2] == (UnityEngine.Object) null ? PersistentSingleton<TeamResourcesManager>.Instance.Rosters[index2].RosterFileData : rosterDataObjectArray[index2].RosterFileData;
          Debug.Log((object) "RosterApi: Overriding save data file...");
          PersistentSingleton<SaveManager>.Instance.rosterData = rosterSaveData;
          PersistentSingleton<SaveManager>.Instance.AddToSaveQueue((ISaveSync) rosterSaveData);
          System.Action action = onComplete;
          if (action != null)
            action();
        }
        else
        {
          Debug.Log((object) "RosterApi: Applying default roster data...");
          RosterDataObject[] rosters = PersistentSingleton<TeamResourcesManager>.Instance.Rosters;
          RosterSaveData rosterSaveData = PersistentSingleton<SaveManager>.Instance.GetRosterSaveData();
          for (int index3 = 0; index3 < rosters.Length; ++index3)
            rosterSaveData.rosters[index3] = rosters[index3].RosterFileData;
          System.Action action = onComplete;
          if (action != null)
            action();
        }
      }
    }
  }
}
