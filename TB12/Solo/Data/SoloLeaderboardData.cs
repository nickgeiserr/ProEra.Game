// Decompiled with JetBrains decompiler
// Type: TB12.Solo.Data.SoloLeaderboardData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TB12.Solo.Data
{
  [CreateAssetMenu(menuName = "TB12/Solo/Leaderboard")]
  public class SoloLeaderboardData : ScriptableObject
  {
    private static string _baseUrl = "https://dev-tb12.platform.bytecubedlabs.co";
    private readonly Dictionary<string, List<SoloLeaderboardData.Entry>> _levelEntries = new Dictionary<string, List<SoloLeaderboardData.Entry>>();
    public Action<string> onLevelDataUpdated;
    public Action<string, SoloLeaderboardData.LeaderboardErrors> onDataError;

    public List<SoloLeaderboardData.Entry> GetEntries(string levelName)
    {
      lock (this._levelEntries)
        return this._levelEntries.ContainsKey(levelName) ? this._levelEntries[levelName] : (List<SoloLeaderboardData.Entry>) null;
    }

    public void GetLevelTop(string levelName, int count = 10)
    {
      string url = string.Format("{0}/api/leaders/{1}/{2}", (object) SoloLeaderboardData._baseUrl, (object) levelName, (object) count);
      Debug.Log((object) ("Request link: " + url));
      PersistentSingleton<RoutineRunner>.Instance.StartCoroutine(this.GetLevelTopRoutine(url, levelName));
    }

    public void SubmitScore(string levelName, string userName, int score)
    {
      if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(levelName))
        return;
      byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) new SoloLeaderboardData.Entry()
      {
        name = userName,
        score = (long) score
      }));
      UnityWebRequest req = new UnityWebRequest(SoloLeaderboardData._baseUrl + "/api/leaders/" + levelName, "POST");
      UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(bytes);
      uploadHandlerRaw.contentType = "application/json";
      req.uploadHandler = (UploadHandler) uploadHandlerRaw;
      PersistentSingleton<RoutineRunner>.Instance.StartCoroutine(this.SendRequestHACK(req));
    }

    private IEnumerator SendRequestHACK(UnityWebRequest req)
    {
      yield return (object) req.SendWebRequest();
    }

    private IEnumerator GetLevelTopRoutine(string url, string levelName)
    {
      using (UnityWebRequest req = UnityWebRequest.Get(url))
      {
        yield return (object) req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ConnectionError)
        {
          Action<string, SoloLeaderboardData.LeaderboardErrors> onDataError = this.onDataError;
          if (onDataError != null)
            onDataError(levelName, SoloLeaderboardData.LeaderboardErrors.NoConnection);
          Debug.LogError((object) req.error);
        }
        else
        {
          string body = req.downloadHandler.text;
          if (string.IsNullOrEmpty(body))
          {
            Action<string, SoloLeaderboardData.LeaderboardErrors> onDataError = this.onDataError;
            if (onDataError != null)
              onDataError(levelName, SoloLeaderboardData.LeaderboardErrors.NoData);
          }
          else
            Task.Run((Action) (() =>
            {
              SoloLeaderboardData.Entry[] collection = JsonConvert.DeserializeObject<SoloLeaderboardData.Entry[]>(body);
              if (collection == null)
                return;
              lock (this._levelEntries)
              {
                List<SoloLeaderboardData.Entry> entryList;
                if (this._levelEntries.TryGetValue(levelName, out entryList))
                {
                  entryList.Clear();
                }
                else
                {
                  entryList = new List<SoloLeaderboardData.Entry>();
                  this._levelEntries[levelName] = entryList;
                }
                entryList.AddRange((IEnumerable<SoloLeaderboardData.Entry>) collection);
                entryList.Sort((Comparison<SoloLeaderboardData.Entry>) ((x, y) => y.score.CompareTo(x.score)));
              }
              UnityMainThreadDispatcher.Enqueue(closure_1 ?? (closure_1 = (Action) (() =>
              {
                Action<string> levelDataUpdated = this.onLevelDataUpdated;
                if (levelDataUpdated == null)
                  return;
                levelDataUpdated(levelName);
              })));
            }));
        }
      }
    }

    [Serializable]
    public class Entry
    {
      public long score { get; set; }

      public string name { get; set; }
    }

    public enum LeaderboardErrors
    {
      NoConnection,
      NoData,
    }
  }
}
