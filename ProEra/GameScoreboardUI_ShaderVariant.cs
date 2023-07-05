// Decompiled with JetBrains decompiler
// Type: ProEra.GameScoreboardUI_ShaderVariant
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework.Data;
using ProEra.Game;
using System;
using System.Collections.Generic;
using TB12;
using UnityEngine;

namespace ProEra
{
  public class GameScoreboardUI_ShaderVariant : MonoBehaviour
  {
    [SerializeField]
    private Material ScoreboardMat;
    [SerializeField]
    private UniformLogoStore _store;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void OnDestroy() => this._linksHandler.Clear();

    private void Awake() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

    private void Init()
    {
      Debug.Log((object) "Scoreboard awake");
      List<EventHandle> links = new List<EventHandle>()
      {
        GameTimeoutState.UserTimeouts.Link<int>(new Action<int>(this.SetUserTimeouts)),
        GameTimeoutState.CompTimeouts.Link<int>(new Action<int>(this.SetCompTimeouts)),
        ScoreClockState.PenaltyVisible.Link<bool>(new Action<bool>(this.SetPenaltyVisible)),
        ScoreClockState.PlayClock.Link<int>(new Action<int>(this.SetupPlayClock)),
        ScoreClockState.PlayClockVisible.Link<bool>(new Action<bool>(this.SetupPlayClockVisibility)),
        ScoreClockState.GameClockString.Link<string>(new Action<string>(this.SetGameClock)),
        ScoreClockState.Quarter.Link<string>(new Action<string>(this.SetQuarter)),
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

    public void Reinitialize()
    {
      this._linksHandler.Clear();
      this.Init();
    }

    private void SetupHandler()
    {
      ScoreClockState.Quarter.Value = MatchManager.instance.timeManager.GetQuarterString();
      ScoreClockState.PlayClockVisible.Value = false;
      ScoreClockState.DownAndDistanceVisible.SetValue(false);
      this.ScoreboardMat.SetFloat("_TeamNameHome", (float) (PersistentData.GetHomeTeamIndex() + 1));
      this.ScoreboardMat.SetFloat("_TeamNameAway", (float) (PersistentData.GetAwayTeamIndex() + 1));
    }

    private void RefreshTeamsLogo()
    {
      this.ScoreboardMat.SetTexture("_HomeTeamLogo", (Texture) this._store.GetUniformLogo(PersistentData.GetHomeTeamIndex()).teamLogo.texture);
      this.ScoreboardMat.SetTexture("_AwayTeamLogo", (Texture) this._store.GetUniformLogo(PersistentData.GetAwayTeamIndex()).teamLogo.texture);
      this.ScoreboardMat.SetFloat("_TeamNameHome", (float) (PersistentData.GetHomeTeamIndex() + 1));
      this.ScoreboardMat.SetFloat("_TeamNameAway", (float) (PersistentData.GetAwayTeamIndex() + 1));
    }

    private void SetUserTimeouts(int timeouts)
    {
      if ((bool) Globals.UserIsHome)
        this.ScoreboardMat.SetFloat("_TOLHome", (float) timeouts);
      else
        this.ScoreboardMat.SetFloat("_TOLAway", (float) timeouts);
    }

    private void SetCompTimeouts(int timeouts)
    {
      if ((bool) Globals.UserIsHome)
        this.ScoreboardMat.SetFloat("_TOLAway", (float) timeouts);
      else
        this.ScoreboardMat.SetFloat("_TOLHome", (float) timeouts);
    }

    private void SetPenaltyVisible(bool visible)
    {
    }

    private void SetupPlayClock(int elapsedTime) => this.ScoreboardMat.SetFloat("_PlayClockSeconds", (float) elapsedTime);

    private void SetGameClock(string elapsedTime)
    {
      this.ScoreboardMat.SetFloat("_GameClockMinutes", (float) ScoreClockState._minutes);
      this.ScoreboardMat.SetFloat("_GameClockSeconds", (float) ScoreClockState._seconds);
    }

    private void SetQuarter(string quarterString)
    {
      if (!MatchManager.Exists())
        return;
      this.ScoreboardMat.SetFloat("_Quarter", (float) MatchManager.instance.timeManager.GetQuarter());
    }

    private void SetupPlayClockVisibility(bool value)
    {
    }

    private void SetPersonnel(string obj)
    {
    }

    private void DownAndDistanceVisibility(bool obj)
    {
    }

    private void PersonnelVisibility(bool obj)
    {
    }

    private void SetDownAndDistanceHandler(string v)
    {
      if (v == "PAT")
        return;
      int num = v == "KICKOFF" ? 1 : 0;
    }

    private void ShowTimeoutHandler(bool userCalledTimeout) => ScoreClockState.DownAndDistanceVisible.SetValue(false);

    private void SetYardLineHandler()
    {
      float firstObjectZPos = ProEra.Game.MatchState.FirstDown.Value;
      if ((bool) ProEra.Game.MatchState.RunningPat && !(bool) PlayState.PlayOver)
        this.SetDownAndDistanceHandler("PAT");
      else if (PlayState.IsKickoff && !(bool) PlayState.PlayOver)
      {
        this.SetDownAndDistanceHandler("KICKOFF");
      }
      else
      {
        float f = Mathf.Abs(firstObjectZPos - ProEra.Game.MatchState.BallOn.Value) / Field.ONE_YARD;
        if (ProEra.Game.MatchState.IsHomeTeamOnOffense)
        {
          this.ScoreboardMat.SetFloat("_DownHome", (float) ProEra.Game.MatchState.Down.Value);
          if ((double) f < (double) Field.ONE_YARD)
            this.ScoreboardMat.SetFloat("_YadlineLineHome", Mathf.Max(Mathf.Round(f), -1f));
          else if (!(bool) FieldState.FirstDownLineVisible || Field.FurtherDownfield(firstObjectZPos, Field.OFFENSIVE_GOAL_LINE))
            this.ScoreboardMat.SetFloat("_YadlineLineHome", Mathf.Max(Mathf.Round(f), 0.0f));
          else
            this.ScoreboardMat.SetFloat("_YadlineLineHome", Mathf.Max(Mathf.Round(f), 1f));
          this.ScoreboardMat.SetInt("_ShowYardLineHome", 1);
          this.ScoreboardMat.SetInt("_ShowYardLineAway", 0);
          this.ScoreboardMat.SetInt("_POSSESIONSWITCHAWAY", 1);
        }
        else
        {
          this.ScoreboardMat.SetFloat("_DownAway", (float) ProEra.Game.MatchState.Down.Value);
          if ((double) f < (double) Field.ONE_YARD)
            this.ScoreboardMat.SetFloat("_YadlineLineAway", Mathf.Max(Mathf.Round(f), -1f));
          else if (!(bool) FieldState.FirstDownLineVisible || Field.FurtherDownfield(firstObjectZPos, Field.OFFENSIVE_GOAL_LINE))
            this.ScoreboardMat.SetFloat("_YadlineLineAway", Mathf.Max(Mathf.Round(f), 0.0f));
          else
            this.ScoreboardMat.SetFloat("_YadlineLineAway", Mathf.Max(Mathf.Round(f), 1f));
          this.ScoreboardMat.SetInt("_ShowYardLineHome", 0);
          this.ScoreboardMat.SetInt("_ShowYardLineAway", 1);
          this.ScoreboardMat.SetInt("_POSSESIONSWITCHAWAY", 0);
        }
      }
    }

    private void SetMatchState(EMatchState newState)
    {
      if (newState.Equals((object) EMatchState.End) && AppState.GameMode != EGameMode.k2MD)
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

    private void SetUserScore(int value)
    {
      if ((bool) Globals.UserIsHome)
        this.ScoreboardMat.SetFloat("_TeamScoreHome", (float) value);
      else
        this.ScoreboardMat.SetFloat("_TeamScoreAway", (float) value);
    }

    private void SetCompScore(int value)
    {
      if ((bool) Globals.UserIsHome)
        this.ScoreboardMat.SetFloat("_TeamScoreAway", (float) value);
      else
        this.ScoreboardMat.SetFloat("_TeamScoreHome", (float) value);
    }
  }
}
