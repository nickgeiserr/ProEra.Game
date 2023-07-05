// Decompiled with JetBrains decompiler
// Type: ProEra.GameScoreboardUI_Lightweight
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework.Data;
using ProEra.Core;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProEra
{
  public class GameScoreboardUI_Lightweight : MonoBehaviour
  {
    [SerializeField]
    [NotNull]
    private Text _quarter;
    [SerializeField]
    [NotNull]
    private Text _gameClock;
    [SerializeField]
    private GameScoreboardUI_Lightweight.TeamSection _awayTeam;
    [SerializeField]
    private GameScoreboardUI_Lightweight.TeamSection _homeTeam;
    [Space(10f)]
    [SerializeField]
    private UniformLogoStore _store;
    [SerializeField]
    private Image homeLogo;
    [SerializeField]
    private Image awayLogo;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void OnDestroy() => this._linksHandler.Clear();

    private void Awake()
    {
      List<EventHandle> links = new List<EventHandle>()
      {
        GameTimeoutState.UserTimeouts.Link<int>(new Action<int>(this.SetUserTimeouts)),
        GameTimeoutState.CompTimeouts.Link<int>(new Action<int>(this.SetCompTimeouts)),
        ScoreClockState.PenaltyVisible.Link<bool>(new Action<bool>(this.SetPenaltyVisible)),
        ScoreClockState.PlayClock.Link<int>(new Action<int>(this.SetupPlayClock)),
        ScoreClockState.PlayClockVisible.Link<bool>(new Action<bool>(this.SetupPlayClockVisibility)),
        ScoreClockState.GameClockString.Link<string>((Action<string>) (newValue => this._gameClock.text = newValue)),
        ScoreClockState.HomeDownAndDistance.Link<string>((Action<string>) (newValue => this._homeTeam._downAndDistance.text = newValue)),
        ScoreClockState.AwayDownAndDistance.Link<string>((Action<string>) (newValue => this._awayTeam._downAndDistance.text = newValue)),
        ScoreClockState.Quarter.Link<string>((Action<string>) (newValue => this._quarter.text = newValue)),
        ScoreClockState.Personnel.Link<string>(new Action<string>(this.SetPersonnel)),
        ScoreClockState.DownAndDistanceVisible.Link<bool>(new Action<bool>(this.DownAndDistanceVisibility)),
        ScoreClockState.PenaltyVisible.Link<bool>(new Action<bool>(this.PersonnelVisibility)),
        ScoreClockState.SetDownAndDistance.Link<string>(new Action<string>(this.SetDownAndDistanceHandler)),
        ScoreClockState.ShowTimeout.Link<bool>(new Action<bool>(this.ShowTimeoutHandler)),
        ScoreClockState.SetYardLine.Link(new System.Action(this.SetYardLineHandler)),
        ProEra.Game.MatchState.CurrentMatchState.Link<EMatchState>(new Action<EMatchState>(this.SetMatchState)),
        ScoreClockState.TEMP_InitializeScoreClock.Link(new System.Action(this.SetupHandler))
      };
      if (ProEra.Game.MatchState.Stats.User != null)
        links.Add(ProEra.Game.MatchState.Stats.User.VarScore.Link<int>(new Action<int>(this.SetUserScore)));
      if (ProEra.Game.MatchState.Stats.Comp != null)
        links.Add(ProEra.Game.MatchState.Stats.Comp.VarScore.Link<int>(new Action<int>(this.SetCompScore)));
      this._linksHandler.SetLinks(links);
    }

    private void SetupHandler()
    {
      ScoreClockState.Quarter.Value = MatchManager.instance.timeManager.GetQuarterString();
      ScoreClockState.PlayClockVisible.Value = false;
      ScoreClockState.DownAndDistanceVisible.SetValue(false);
      this._homeTeam._teamName.text = TeamState.GetHomeTeamAbbreviation();
      this._awayTeam._teamName.text = TeamState.GetAwayTeamAbbreviation();
    }

    private void RefreshTeamsLogo()
    {
      this.homeLogo.sprite = this._store.GetUniformLogo(PersistentData.GetHomeTeamIndex()).teamLogo;
      this.awayLogo.sprite = this._store.GetUniformLogo(PersistentData.GetAwayTeamIndex()).teamLogo;
      this._homeTeam._teamName.text = TeamState.GetHomeTeamAbbreviation();
      this._awayTeam._teamName.text = TeamState.GetAwayTeamAbbreviation();
    }

    private void SetUserTimeouts(int timeouts)
    {
      timeouts = Mathf.Clamp(timeouts, 0, 3);
      List<Image> imageList = (bool) Globals.UserIsHome ? this._homeTeam.timeouts : this._awayTeam.timeouts;
      for (int index = 0; index < timeouts; ++index)
        imageList[index].enabled = true;
      for (int index = timeouts; index < imageList.Count; ++index)
        imageList[index].enabled = false;
    }

    private void SetCompTimeouts(int timeouts)
    {
      timeouts = Mathf.Clamp(timeouts, 0, 3);
      List<Image> imageList = (bool) Globals.UserIsHome ? this._awayTeam.timeouts : this._homeTeam.timeouts;
      for (int index = 0; index < timeouts; ++index)
        imageList[index].enabled = true;
      for (int index = timeouts; index < imageList.Count; ++index)
        imageList[index].enabled = false;
    }

    private void SetPenaltyVisible(bool visible)
    {
      if (visible)
      {
        ScoreClockState.DownAndDistanceVisible.SetValue(false);
        LeanTween.alphaCanvas(ProEra.Game.MatchState.IsHomeTeamOnOffense ? this._homeTeam.penaltyCg : this._awayTeam.penaltyCg, 1f, 0.3f);
      }
      else
      {
        LeanTween.alphaCanvas(this._homeTeam.penaltyCg, 0.0f, 0.3f);
        LeanTween.alphaCanvas(this._awayTeam.penaltyCg, 0.0f, 0.3f);
      }
    }

    private void SetupPlayClock(int elapsedTime)
    {
      string str = elapsedTime > 9 ? string.Format(":{0}", (object) elapsedTime) : string.Format(":0{0}", (object) elapsedTime);
      this._homeTeam._playClock.text = str;
      this._awayTeam._playClock.text = str;
    }

    private void SetupPlayClockVisibility(bool value)
    {
      if (value)
      {
        (ProEra.Game.MatchState.IsHomeTeamOnOffense ? this._homeTeam.playClockGo : this._awayTeam.playClockGo).SetActive(true);
      }
      else
      {
        this._homeTeam.playClockGo.SetActive(false);
        this._awayTeam.playClockGo.SetActive(false);
      }
    }

    private void SetPersonnel(string obj)
    {
    }

    private void DownAndDistanceVisibility(bool obj)
    {
      this._homeTeam._downAndDistance.gameObject.SetActive(obj);
      this._homeTeam._yardLine.gameObject.SetActive(obj);
      this._awayTeam._downAndDistance.gameObject.SetActive(obj);
      this._awayTeam._yardLine.gameObject.SetActive(obj);
    }

    private void PersonnelVisibility(bool obj)
    {
    }

    private void SetDownAndDistanceHandler(string v)
    {
      ScoreClockState.DownAndDistanceVisible.SetValue(true);
      ScoreClockState.PersonnelVisible.SetValue(false);
      ScoreClockState.HomeDownAndDistance.Value = v;
      ScoreClockState.AwayDownAndDistance.Value = v;
      if (ProEra.Game.MatchState.IsHomeTeamOnOffense)
      {
        LeanTween.alphaCanvas(this._homeTeam.downAndDistanceCg, 1f, 0.3f);
        ScoreClockState.HomeDownAndDistance.Value = v;
      }
      else
      {
        LeanTween.alphaCanvas(this._awayTeam.downAndDistanceCg, 1f, 0.3f);
        ScoreClockState.AwayDownAndDistance.Value = v;
      }
    }

    private void ShowTimeoutHandler(bool userCalledTimeout)
    {
      ScoreClockState.DownAndDistanceVisible.SetValue(false);
      LeanTween.alphaCanvas(!(bool) Globals.UserIsHome ? (userCalledTimeout ? this._awayTeam.timeoutCg : this._homeTeam.timeoutCg) : (userCalledTimeout ? this._homeTeam.timeoutCg : this._awayTeam.timeoutCg), 1f, 0.3f);
    }

    private void SetYardLineHandler()
    {
      float fieldLocation = ProEra.Game.MatchState.BallOn.Value;
      float firstObjectZPos = ProEra.Game.MatchState.FirstDown.Value;
      int num = ProEra.Game.MatchState.Down.Value;
      if ((bool) ProEra.Game.MatchState.RunningPat && !(bool) PlayState.PlayOver)
        this.SetDownAndDistanceHandler("PAT");
      else if (PlayState.IsKickoff && !(bool) PlayState.PlayOver)
      {
        this.SetDownAndDistanceHandler("KICKOFF");
      }
      else
      {
        FieldState.IsBallInOpponentTerritory();
        float lineByFieldLocation = (float) Field.GetYardLineByFieldLocation(fieldLocation);
        (ProEra.Game.MatchState.IsHomeTeamOnOffense ? this._homeTeam._yardLine : this._awayTeam._yardLine).text = (double) lineByFieldLocation >= 1.0 ? lineByFieldLocation.ToString() : "IN";
        float f = Mathf.Abs(firstObjectZPos - fieldLocation) / Field.ONE_YARD;
        string str = !(bool) FieldState.FirstDownLineVisible || Field.FurtherDownfield(firstObjectZPos, Field.OFFENSIVE_GOAL_LINE) ? "GL" : ((double) f >= (double) Field.ONE_YARD ? Mathf.Max(Mathf.Round(f), 1f).ToString() : "IN");
        string v = "1st & " + str;
        switch (num)
        {
          case 2:
            v = "2nd & " + str;
            break;
          case 3:
            v = "3rd & " + str;
            break;
          case 4:
            v = "4th & " + str;
            break;
        }
        this.SetDownAndDistanceHandler(v);
      }
    }

    private void SetMatchState(EMatchState newState)
    {
      if (newState.Equals((object) EMatchState.End))
        this._linksHandler.Clear();
      try
      {
        this.RefreshTeamsLogo();
      }
      catch (NullReferenceException ex)
      {
        Debug.LogError((object) "Could not Refresh Teams Logo!");
      }
    }

    private void SetCompScore(int value) => ((bool) Globals.UserIsHome ? this._awayTeam._score : this._homeTeam._score).text = value.ToString();

    private void SetUserScore(int value) => ((bool) Globals.UserIsHome ? this._homeTeam._score : this._awayTeam._score).text = value.ToString();

    [Serializable]
    private class TeamSection
    {
      [NotNull]
      public Text _teamName;
      [NotNull]
      public Text _score;
      [NotNull]
      public Text _playClock;
      [NotNull]
      public List<Image> timeouts;
      [NotNull]
      public Text _yardLine;
      [NotNull]
      public Text _downAndDistance;
      [NotNull]
      public CanvasGroup penaltyCg;
      [NotNull]
      public CanvasGroup timeoutCg;
      [NotNull]
      public CanvasGroup downAndDistanceCg;
      [NotNull]
      public GameObject playClockGo;
    }
  }
}
