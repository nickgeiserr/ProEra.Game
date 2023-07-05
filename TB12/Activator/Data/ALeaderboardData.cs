// Decompiled with JetBrains decompiler
// Type: TB12.Activator.Data.ALeaderboardData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB12.Activator.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace TB12.Activator.Data
{
  [CreateAssetMenu(menuName = "TB12/Activator/Leaderboard")]
  public class ALeaderboardData : ScriptableObject
  {
    [SerializeField]
    private TeamLogoData _teamLogoData;
    [SerializeField]
    private List<ALeaderboardData.Entry> _entries;
    private static string _leaderboardBackendUrl = "https://dev-tb12.platform.bytecubedlabs.co";
    private static string _postUrl = ALeaderboardData._leaderboardBackendUrl + "/api/leaderboard";
    private static string _top10Url = ALeaderboardData._leaderboardBackendUrl + "/api/leaderboard/top/10";
    private int _currentEntryIndex = -1;
    public System.Action onDataUpdated;

    public int CurrentRow => this._currentEntryIndex;

    public int EntryCount => this._entries.Count;

    public string Date
    {
      get
      {
        if (this._entries != null && this._currentEntryIndex >= 0 && this._currentEntryIndex < this._entries.Count)
          return this._entries[this._currentEntryIndex].dateTime;
        Debug.LogError((object) string.Format("Couldnt find entry with index {0}", (object) this._currentEntryIndex));
        return (string) null;
      }
    }

    private void OnEnable()
    {
      if (this._entries == null)
        this._entries = new List<ALeaderboardData.Entry>();
      this._currentEntryIndex = -1;
    }

    public void ResetHighlight() => this._currentEntryIndex = -1;

    public void InsertAndHighlight(int score, EGameMode gameMode)
    {
      int index1 = 0;
      for (int index2 = this._entries.Count - 1; index2 >= 0; --index2)
      {
        if (score >= this._entries[index2].score)
          index1 = index2;
      }
      ALeaderboardData.EMode emode = ALeaderboardData.EMode.Unknown;
      switch (gameMode)
      {
        case EGameMode.kThrow:
        case EGameMode.kPass:
          emode = ALeaderboardData.EMode.Throw;
          break;
        case EGameMode.kCatch:
          emode = ALeaderboardData.EMode.Catch;
          break;
        case EGameMode.kAgility:
          emode = ALeaderboardData.EMode.Agility;
          break;
      }
      ALeaderboardData.Entry entry = new ALeaderboardData.Entry()
      {
        score = score,
        name = "___",
        team = -1,
        dateTime = DateTime.Now.ToString("h:mm tt | dd MMM yyyy"),
        type = emode
      };
      this._entries.Insert(index1, entry);
      this._currentEntryIndex = index1;
    }

    public void UpdateCurrentName(string userName) => this._entries[this.CurrentRow].name = userName;

    public void UpdateTeam(int teamIndex) => this._entries[this.CurrentRow].team = teamIndex;

    public void PushHighscore()
    {
      ALeaderboardData.Entry entry = this._entries[this._currentEntryIndex];
      WWWForm formData = new WWWForm();
      formData.AddBinaryData("files", new byte[2], "image");
      formData.AddField("score", entry.score);
      formData.AddField("team", entry.team);
      formData.AddField("name", entry.name);
      formData.AddField("date", entry.dateTime);
      formData.AddField("type", (int) entry.type);
      UnityWebRequest.Post(ALeaderboardData._postUrl, formData).SendWebRequest();
    }

    public void GetTop10() => PersistentSingleton<RoutineRunner>.Instance.StartCoroutine(this.DoGetTop10());

    private IEnumerator DoGetTop10()
    {
      ALeaderboardData aleaderboardData1 = this;
      using (UnityWebRequest req = UnityWebRequest.Get(ALeaderboardData._top10Url))
      {
        yield return (object) req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
          Debug.LogError((object) req.error);
        }
        else
        {
          ALeaderboardData aleaderboardData = aleaderboardData1;
          string str = req.downloadHandler.text;
          if (!string.IsNullOrEmpty(str))
            Task.Run((System.Action) (() =>
            {
              try
              {
                ALeaderboardData.Entry[] collection = JsonConvert.DeserializeObject<ALeaderboardData.Entry[]>(str);
                if (collection == null)
                  return;
                aleaderboardData._entries.Clear();
                aleaderboardData._entries.AddRange((IEnumerable<ALeaderboardData.Entry>) collection);
                // ISSUE: reference to a compiler-generated method
                UnityMainThreadDispatcher.Enqueue(new System.Action(aleaderboardData.\u003CDoGetTop10\u003Eb__22_1));
              }
              catch (Exception ex)
              {
                Debug.LogError((object) ("DoGetTop10: " + ex.Message));
              }
            }));
        }
      }
    }

    public void PopulateBackItem(RecyclingListViewItem viewItem, int row)
    {
      ActivatorLeaderboardItemBack leaderboardItemBack = viewItem as ActivatorLeaderboardItemBack;
      if ((UnityEngine.Object) leaderboardItemBack == (UnityEngine.Object) null)
        return;
      ALeaderboardData.Entry entry = this._entries[row];
      Sprite sprite = entry.team < 0 ? (Sprite) null : this._teamLogoData.Sprites[entry.team];
      leaderboardItemBack.Setup((int) entry.type, row == this._currentEntryIndex, sprite);
    }

    public void PopulateFrontItem(RecyclingListViewItem viewItem, int row)
    {
      ActivatorLeaderboardItemFront leaderboardItemFront = viewItem as ActivatorLeaderboardItemFront;
      if ((UnityEngine.Object) leaderboardItemFront == (UnityEngine.Object) null)
        return;
      ALeaderboardData.Entry entry = this._entries[row];
      leaderboardItemFront.Setup(row + 1, entry.score, entry.name, row == this._currentEntryIndex);
    }

    [ContextMenu("SORT DATA")]
    private void SORT_DATA() => this._entries = this._entries.OrderByDescending<ALeaderboardData.Entry, int>((Func<ALeaderboardData.Entry, int>) (x => x.score)).ToList<ALeaderboardData.Entry>();

    public enum EMode
    {
      Unknown,
      Catch,
      Throw,
      Agility,
    }

    [Serializable]
    private class Entry
    {
      public int score { get; set; }

      public int team { get; set; }

      public string name { get; set; }

      public string dateTime { get; set; }

      public ALeaderboardData.EMode type { get; set; }
    }
  }
}
