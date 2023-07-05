// Decompiled with JetBrains decompiler
// Type: TB12.LeaderboardData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Data/Leaderboard")]
  public class LeaderboardData : ScriptableSettings
  {
    [SerializeField]
    private List<LeaderboardData.Entry> _entries = new List<LeaderboardData.Entry>();
    public Action DataUpdated;

    public bool BelongsToPass(int score) => score > 10;

    public void Insert(int score, string challenge, string userName, EGameMode gameMode)
    {
      LeaderboardData.Entry byName = this.GetByName(userName);
      if (byName == null)
      {
        LeaderboardData.Entry entry = new LeaderboardData.Entry()
        {
          UserName = userName
        };
        switch (gameMode)
        {
          case EGameMode.kCatch:
            entry.TotalScore = entry.Catching.Add(score, challenge);
            break;
          case EGameMode.kPass:
            entry.TotalScore = entry.Passing.Add(score, challenge);
            break;
        }
        this._entries.Add(entry);
      }
      else
      {
        LeaderboardData.EntryDetails entryDetails = gameMode != EGameMode.kPass ? byName.Catching : byName.Passing;
        int index = entryDetails.Challenges.IndexOf(challenge);
        if (index >= 0)
        {
          byName.TotalScore -= entryDetails.Score[index];
          entryDetails.TotalScore -= entryDetails.Score[index];
          entryDetails.Score[index] = Mathf.Max(entryDetails.Score[index], score);
          entryDetails.TotalScore += entryDetails.Score[index];
          byName.TotalScore += entryDetails.Score[index];
        }
        else
          byName.TotalScore += entryDetails.Add(score, challenge);
      }
      Action dataUpdated = this.DataUpdated;
      if (dataUpdated == null)
        return;
      dataUpdated();
    }

    private LeaderboardData.Entry GetByName(string userName)
    {
      for (int index = 0; index < this._entries.Count; ++index)
      {
        if (this._entries[index].UserName == userName)
          return this._entries[index];
      }
      return (LeaderboardData.Entry) null;
    }

    public void GetEntries(ELeaderboardType mode, ref List<LeaderboardData.DisplayEntry> result)
    {
      result.Clear();
      switch (mode)
      {
        case ELeaderboardType.kAll:
          using (List<LeaderboardData.Entry>.Enumerator enumerator = this._entries.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              LeaderboardData.Entry current = enumerator.Current;
              result.Add(new LeaderboardData.DisplayEntry()
              {
                Name = current.UserName,
                Score = current.TotalScore
              });
            }
            break;
          }
        case ELeaderboardType.kPass:
          using (List<LeaderboardData.Entry>.Enumerator enumerator = this._entries.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              LeaderboardData.Entry current = enumerator.Current;
              result.Add(new LeaderboardData.DisplayEntry()
              {
                Name = current.UserName,
                Score = current.Passing.TotalScore
              });
            }
            break;
          }
        case ELeaderboardType.kCatch:
          using (List<LeaderboardData.Entry>.Enumerator enumerator = this._entries.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              LeaderboardData.Entry current = enumerator.Current;
              result.Add(new LeaderboardData.DisplayEntry()
              {
                Name = current.UserName,
                Score = current.Catching.TotalScore
              });
            }
            break;
          }
      }
      result = result.OrderByDescending<LeaderboardData.DisplayEntry, int>((Func<LeaderboardData.DisplayEntry, int>) (x => x.Score)).ToList<LeaderboardData.DisplayEntry>();
    }

    [ContextMenu("RECALC TOTALS")]
    private void RECALC_TOTALS()
    {
      foreach (LeaderboardData.Entry entry in this._entries)
      {
        entry.Passing.TotalScore = 0;
        foreach (int num in entry.Passing.Score)
          entry.Passing.TotalScore += num;
        entry.Catching.TotalScore = 0;
        foreach (int num in entry.Catching.Score)
          entry.Catching.TotalScore += num;
        entry.TotalScore = entry.Passing.TotalScore + entry.Catching.TotalScore;
      }
    }

    public class DisplayEntry
    {
      public int Score;
      public string Name;
    }

    [Serializable]
    public class EntryDetails
    {
      public int TotalScore;
      public List<int> Score = new List<int>();
      public List<string> Challenges = new List<string>();

      public int Add(int score, string challenge)
      {
        this.Challenges.Add(challenge);
        this.Score.Add(score);
        this.TotalScore += score;
        return this.TotalScore;
      }
    }

    [Serializable]
    public class Entry
    {
      public string UserName;
      public int TotalScore;
      public LeaderboardData.EntryDetails Passing = new LeaderboardData.EntryDetails();
      public LeaderboardData.EntryDetails Catching = new LeaderboardData.EntryDetails();
    }
  }
}
