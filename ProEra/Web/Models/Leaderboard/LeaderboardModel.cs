// Decompiled with JetBrains decompiler
// Type: ProEra.Web.Models.Leaderboard.LeaderboardModel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using System;

namespace ProEra.Web.Models.Leaderboard
{
  [Serializable]
  public class LeaderboardModel
  {
    [JsonProperty]
    public string Name { get; set; }

    [JsonProperty]
    public uint SteamLeaderboardID { get; set; }

    [JsonProperty]
    public uint PlaystationLeaderboardID { get; set; }

    [JsonProperty]
    public string ReportID { get; set; }

    [JsonProperty]
    public DateTime ReportTime { get; set; }

    [JsonProperty]
    public LeaderboardModel.ScoreValueType ScoreType { get; set; }

    [JsonProperty]
    public LeaderboardModel.SocialNetwork SocialNetworkFilter { get; set; }

    [JsonProperty]
    public ListElementModel[] HighScores { get; set; }

    public enum ScoreValueType
    {
      DistanceInFoot,
      DistanceInMeters,
      Percentage,
      Point,
      TimeInMillisecond,
      TimeInSecond,
    }

    public enum SocialNetwork
    {
      Facebook,
      Playstation,
      Steam,
    }
  }
}
