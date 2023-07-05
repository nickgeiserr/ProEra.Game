// Decompiled with JetBrains decompiler
// Type: PenaltyManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class PenaltyManager : SingletonBehaviour<PenaltyManager, MonoBehaviour>
{
  private static int BASE_PLAYER_DISCIPLINE = 75;
  private float DISCIPLINE_PERCENT_INCREASE = 0.004f;
  private Penalty noPenalty;
  private Penalty penaltyOnPlay;
  private List<Penalty> allPenaltiesOnPlay;
  [HideInInspector]
  public bool isPenaltyOnPlay;
  [HideInInspector]
  public bool isPenaltyAccepted;
  [HideInInspector]
  public int downIfAccept;
  [HideInInspector]
  public int downIfDecline;
  [HideInInspector]
  public static float PREPLAY_PENALTY_CHECKTIME;
  [HideInInspector]
  public static float MIDPLAY_PENALTY_CHECKTIME;
  public float RANGE_HOLDING;
  public float RANGE_OFFSIDES;
  public float RANGE_FALSESTART;
  public float RANGE_FACEMASK;
  public float RANGE_OFF_PI;
  public float RANGE_DEF_PI;
  private List<Penalty> _prePlayPenalties = new List<Penalty>();
  private List<Penalty> _randomPrePlayPenalties = new List<Penalty>();
  private List<Penalty> homeTeamPotentialPenalties = new List<Penalty>();
  private List<Penalty> awayTeamPotentialPenalties = new List<Penalty>();
  private List<Penalty> _ballInAirPenalties = new List<Penalty>();
  private List<Penalty> _randomBallInAirPenalties = new List<Penalty>();
  private List<Penalty> _duringPlayPenalty = new List<Penalty>();
  private List<Penalty> _randomDuringPlayPenalties = new List<Penalty>();
  private List<Penalty> _randomAfterPlayPenalty = new List<Penalty>();
  private List<Penalty> _onSnapPenalties = new List<Penalty>();
  private List<Penalty> _randomOnSnapPenalties = new List<Penalty>();

  private float BasePlayerPenaltyPercentage => PersistentSingleton<SaveManager>.Instance.gameSettings.PenaltyFrequency / 1000f;

  public bool OptDelayOfGame => PersistentSingleton<SaveManager>.Instance.gameSettings.DelayOfGame;

  public bool OptFalseStart => PersistentSingleton<SaveManager>.Instance.gameSettings.FalseStart;

  public bool OptOffensiveHolding => PersistentSingleton<SaveManager>.Instance.gameSettings.OffensiveHolding;

  public bool OptDefenseOffsides => PersistentSingleton<SaveManager>.Instance.gameSettings.Offsides;

  public bool OptOffensivePassInterference => PersistentSingleton<SaveManager>.Instance.gameSettings.OffensivePassInterference;

  public bool OptDefensivePassInterference => PersistentSingleton<SaveManager>.Instance.gameSettings.DefensivePassInterference;

  public bool OptFacemask => PersistentSingleton<SaveManager>.Instance.gameSettings.Facemask;

  public bool OptIllegalForwardPass => PersistentSingleton<SaveManager>.Instance.gameSettings.IllegalForwardPass;

  public bool OptEncroachment => PersistentSingleton<SaveManager>.Instance.gameSettings.Encroachment;

  public bool OptKickoffOutOfBounds => PersistentSingleton<SaveManager>.Instance.gameSettings.KickoffOutOfBounds;

  public bool OptIntentionalGrounding => PersistentSingleton<SaveManager>.Instance.gameSettings.IntentionalGrounding;

  public void Start()
  {
    this.noPenalty = new Penalty("NONE", "NONE", 0, PenaltyTime.None, PenaltyType.None, PenaltyDownResult.Repeat, -1, new PlayerData());
    PenaltyManager.PREPLAY_PENALTY_CHECKTIME = (float) UnityEngine.Random.Range(7, 25);
    PenaltyManager.MIDPLAY_PENALTY_CHECKTIME = 0.3f;
    this.RANGE_HOLDING = 22f;
    this.RANGE_OFFSIDES = 5f;
    this.RANGE_FALSESTART = 8f;
    this.RANGE_FACEMASK = 1f;
    this.RANGE_OFF_PI = 1f;
    this.RANGE_DEF_PI = 3f;
    this.allPenaltiesOnPlay = new List<Penalty>();
    this.CheckForRangePenaltyOverrides();
  }

  private void OnDestroy()
  {
    Debug.Log((object) "PenaltyManager -> OnDestroy");
    this.StopAllCoroutines();
    SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance = (PenaltyManager) null;
  }

  public void CheckForRangePenaltyOverrides()
  {
    Dictionary<string, string> dictionary = ModManager.LoadNameValuePairTextFile("Settings/PENALTY.TXT");
    float? nullable;
    if (dictionary.ContainsKey("HOLDING"))
    {
      nullable = ModManager.ParseFloat(dictionary["HOLDING"]);
      this.RANGE_HOLDING = (float) ((double) nullable ?? (double) this.RANGE_HOLDING);
    }
    if (dictionary.ContainsKey("OFFSIDES"))
    {
      nullable = ModManager.ParseFloat(dictionary["OFFSIDES"]);
      this.RANGE_OFFSIDES = (float) ((double) nullable ?? (double) this.RANGE_OFFSIDES);
    }
    if (dictionary.ContainsKey("FALSESTART"))
    {
      nullable = ModManager.ParseFloat(dictionary["FALSESTART"]);
      this.RANGE_FALSESTART = (float) ((double) nullable ?? (double) this.RANGE_FALSESTART);
    }
    if (dictionary.ContainsKey("FACEMASK"))
    {
      nullable = ModManager.ParseFloat(dictionary["FACEMASK"]);
      this.RANGE_FACEMASK = (float) ((double) nullable ?? (double) this.RANGE_FACEMASK);
    }
    if (dictionary.ContainsKey("OFF_PI"))
    {
      nullable = ModManager.ParseFloat(dictionary["OFF_PI"]);
      this.RANGE_OFF_PI = (float) ((double) nullable ?? (double) this.RANGE_OFF_PI);
    }
    if (!dictionary.ContainsKey("DEF_PI"))
      return;
    nullable = ModManager.ParseFloat(dictionary["DEF_PI"]);
    this.RANGE_DEF_PI = (float) ((double) nullable ?? (double) this.RANGE_DEF_PI);
  }

  public Penalty GetPenaltyOnPlay() => !this.penaltyOnPlay.Equals((object) null) ? this.penaltyOnPlay : this.noPenalty;

  public List<Penalty> GetAllPenaltiesOnPlay()
  {
    if (this.allPenaltiesOnPlay.Count > 0)
      return this.allPenaltiesOnPlay;
    this.allPenaltiesOnPlay.Add(this.noPenalty);
    return this.allPenaltiesOnPlay;
  }

  public void SetPenaltyOnPlay(Penalty p)
  {
    MonoBehaviour.print((object) ("Setting Penalty On Play: " + p.ToString()));
    this.penaltyOnPlay = p;
  }

  public void CallPenalty(PenaltyType type, int teamIndex, PlayerData player)
  {
    if (type != PenaltyType.DelayOfGame)
    {
      Debug.LogWarning((object) (" Error -- attempt to call unsupported penalty for " + Enum.GetName(typeof (PenaltyType), (object) type)));
    }
    else
    {
      PEGameplayEventManager.RecordPenaltyEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, type);
      MatchManager.instance.playersManager.StopAISnapBallCoroutine();
      switch (type - 4)
      {
        case PenaltyType.None:
          if (!this.OptEncroachment)
            break;
          this.isPenaltyOnPlay = true;
          this.penaltyOnPlay = new Penalty("Encroachment", "Defense", 5, PenaltyTime.PrePlay, PenaltyType.Encroachment, PenaltyDownResult.Repeat, teamIndex, player);
          break;
        case PenaltyType.PassInterference:
          if (!this.OptDelayOfGame)
            break;
          this.isPenaltyOnPlay = true;
          this.penaltyOnPlay = new Penalty("Delay of Game", "Offense", -5, PenaltyTime.PrePlay, PenaltyType.DelayOfGame, PenaltyDownResult.Repeat, teamIndex, player);
          break;
        case PenaltyType.Encroachment:
          if (!this.OptIllegalForwardPass)
            break;
          this.isPenaltyOnPlay = true;
          this.penaltyOnPlay = new Penalty("Illegal Forward Pass", "Offense", -5, PenaltyTime.DuringPlay, PenaltyType.IllegalForwardPass, PenaltyDownResult.Repeat, teamIndex, player);
          break;
        case PenaltyType.Offsides:
          if (!this.OptKickoffOutOfBounds)
            break;
          this.isPenaltyOnPlay = true;
          this.penaltyOnPlay = new Penalty("Kickoff Out of Bounds", "Offense", 0, PenaltyTime.DuringPlay, PenaltyType.KickoffOutOfBounds, PenaltyDownResult.FirstDown, teamIndex, player);
          break;
        case PenaltyType.Facemask:
          if (!this.OptIntentionalGrounding)
            break;
          this.isPenaltyOnPlay = true;
          this.penaltyOnPlay = new Penalty("Intentional Grounding", "Offense", -10, PenaltyTime.DuringPlay, PenaltyType.IntentionalGrounding, PenaltyDownResult.LossOfDown, teamIndex, player);
          break;
      }
    }
  }

  public bool IsPenaltyBeforePlay(int teamOnOffense) => false;

  private Penalty CheckForPrePlayPenalty(TeamData team, int teamOnOffense)
  {
    this._prePlayPenalties.Clear();
    for (int playerIndex = 0; playerIndex < team.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      Penalty randomPrePlayPenalty = this.GetRandomPrePlayPenalty(team.GetPlayer(playerIndex), team.TeamIndex, teamOnOffense);
      if (randomPrePlayPenalty.GetPenaltyType() != PenaltyType.None)
        this._prePlayPenalties.Add(randomPrePlayPenalty);
    }
    return this._prePlayPenalties.Count > 0 ? this._prePlayPenalties[UnityEngine.Random.Range(0, this._prePlayPenalties.Count)] : this.noPenalty;
  }

  private Penalty GetRandomPrePlayPenalty(PlayerData player, int teamToCheck, int teamOnOffense)
  {
    this._randomPrePlayPenalties.Clear();
    Position playerPosition = player.PlayerPosition;
    if (this.PlayerOnField(player.IndexOnTeam))
    {
      if (teamToCheck == teamOnOffense && TeamData.LIST_OF_OFFENSIVE_POSITIONS.Contains(playerPosition) && !PlayState.IsKickoff)
      {
        switch (playerPosition)
        {
          case Position.OL:
            if ((double) UnityEngine.Random.Range(0.0f, this.RANGE_FALSESTART) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptFalseStart)
            {
              this._randomPrePlayPenalties.Add(new Penalty("False Start", "Offense", -5, PenaltyTime.PrePlay, PenaltyType.FalseStart, PenaltyDownResult.Repeat, teamToCheck, player));
              break;
            }
            break;
        }
      }
      else if (teamToCheck != teamOnOffense && TeamData.LIST_OF_DEFENSIVE_POSITIONS.Contains(playerPosition) && playerPosition != Position.DL && playerPosition != Position.LB)
        ;
    }
    return this._randomPrePlayPenalties.Count > 0 ? this._randomPrePlayPenalties[UnityEngine.Random.Range(0, this._randomPrePlayPenalties.Count)] : this.noPenalty;
  }

  public bool IsPenaltyWhileBallInAir(int teamOnOffense) => false;

  private List<Penalty> CheckForBallInAirPenalty(TeamData team, int teamOnOffense)
  {
    this._ballInAirPenalties.Clear();
    for (int playerIndex = 0; playerIndex < team.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      Penalty ballInAirPenalty = this.GetRandomBallInAirPenalty(team.GetPlayer(playerIndex), team.TeamIndex, teamOnOffense);
      if (ballInAirPenalty.GetPenaltyType() != PenaltyType.None)
        this._ballInAirPenalties.Add(ballInAirPenalty);
    }
    return this._ballInAirPenalties;
  }

  private Penalty GetRandomBallInAirPenalty(PlayerData player, int teamToCheck, int teamOnOffense)
  {
    this._randomBallInAirPenalties.Clear();
    Position playerPosition = player.PlayerPosition;
    if (this.PlayerOnField(player.IndexOnTeam))
    {
      if (teamToCheck == teamOnOffense && TeamData.LIST_OF_OFFENSIVE_POSITIONS.Contains(playerPosition) && !PlayState.IsKickoff)
      {
        switch (playerPosition)
        {
          case Position.RB:
          case Position.WR:
          case Position.TE:
            if ((double) UnityEngine.Random.Range(0.0f, this.RANGE_OFF_PI) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptOffensivePassInterference)
            {
              this._randomBallInAirPenalties.Add(new Penalty("Pass Interference", "Offense", -10, PenaltyTime.PostPlay, PenaltyType.PassInterference, PenaltyDownResult.Repeat, teamToCheck, player));
              break;
            }
            break;
        }
      }
      else if (teamToCheck != teamOnOffense && TeamData.LIST_OF_DEFENSIVE_POSITIONS.Contains(playerPosition) && (playerPosition == Position.DL || playerPosition == Position.LB || playerPosition == Position.DB) && (double) UnityEngine.Random.Range(0.0f, this.RANGE_DEF_PI) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptDefensivePassInterference)
        this._randomBallInAirPenalties.Add(new Penalty("Pass Interference", "Defense", 15, PenaltyTime.PostPlay, PenaltyType.PassInterference, PenaltyDownResult.FirstDown, teamToCheck, player));
    }
    return this._randomBallInAirPenalties.Count > 0 ? this._randomBallInAirPenalties[UnityEngine.Random.Range(0, this._randomBallInAirPenalties.Count)] : this.noPenalty;
  }

  public bool IsPenaltyDuringPlay(PlayType playType, int teamOnOffense) => false;

  private Penalty CheckForDuringPlayPenalty(TeamData team, PlayType playType, int teamOnOffense)
  {
    this._duringPlayPenalty.Clear();
    for (int playerIndex = 0; playerIndex < team.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      Penalty duringPlayPenalty = this.GetRandomDuringPlayPenalty(team.GetPlayer(playerIndex), team.TeamIndex, playType, teamOnOffense);
      if (duringPlayPenalty.GetPenaltyType() != PenaltyType.None)
        this._duringPlayPenalty.Add(duringPlayPenalty);
    }
    return this._duringPlayPenalty.Count > 0 ? this._duringPlayPenalty[UnityEngine.Random.Range(0, this._duringPlayPenalty.Count)] : this.noPenalty;
  }

  private Penalty GetRandomDuringPlayPenalty(
    PlayerData player,
    int teamToCheck,
    PlayType playType,
    int teamOnOffense)
  {
    this._randomDuringPlayPenalties.Clear();
    Position playerPosition = player.PlayerPosition;
    if (this.PlayerOnField(player.IndexOnTeam) && teamToCheck == teamOnOffense && TeamData.LIST_OF_OFFENSIVE_POSITIONS.Contains(playerPosition) && !PlayState.IsKickoff)
    {
      switch (playerPosition)
      {
        case Position.RB:
          if (playType == PlayType.Pass && (double) UnityEngine.Random.Range(0.0f, this.RANGE_HOLDING) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptOffensiveHolding)
          {
            this._randomDuringPlayPenalties.Add(new Penalty("Holding", "Offense", -10, PenaltyTime.DuringPlay, PenaltyType.Holding, PenaltyDownResult.Repeat, teamToCheck, player));
            break;
          }
          break;
        case Position.WR:
          if (playType == PlayType.Run && (double) UnityEngine.Random.Range(0.0f, this.RANGE_HOLDING) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptOffensiveHolding)
          {
            this._randomDuringPlayPenalties.Add(new Penalty("Holding", "Offense", -10, PenaltyTime.DuringPlay, PenaltyType.Holding, PenaltyDownResult.Repeat, teamToCheck, player));
            break;
          }
          break;
        case Position.TE:
          if (playType == PlayType.Run && (double) UnityEngine.Random.Range(0.0f, this.RANGE_HOLDING) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptOffensiveHolding)
          {
            this._randomDuringPlayPenalties.Add(new Penalty("Holding", "Offense", -10, PenaltyTime.DuringPlay, PenaltyType.Holding, PenaltyDownResult.Repeat, teamToCheck, player));
            break;
          }
          break;
        case Position.OL:
          if ((double) UnityEngine.Random.Range(0.0f, this.RANGE_HOLDING) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptOffensiveHolding)
          {
            this._randomDuringPlayPenalties.Add(new Penalty("Holding", "Offense", -10, PenaltyTime.DuringPlay, PenaltyType.Holding, PenaltyDownResult.Repeat, teamToCheck, player));
            break;
          }
          break;
      }
    }
    return this._randomDuringPlayPenalties.Count > 0 ? this._randomDuringPlayPenalties[UnityEngine.Random.Range(0, this._randomDuringPlayPenalties.Count)] : this.noPenalty;
  }

  public bool IsPenaltyAfterPlay(
    PlayerAI ballCarrier,
    PlayerAI defender,
    int teamOnOffense,
    PlayType playType)
  {
    return false;
  }

  private Penalty GetRandomAfterPlayPenalty(
    PlayerData player,
    int teamToCheck,
    int teamOnOffense,
    PlayType playType)
  {
    this._randomAfterPlayPenalty.Clear();
    Position playerPosition = player.PlayerPosition;
    if (this.PlayerOnField(player.IndexOnTeam))
    {
      if (teamToCheck == teamOnOffense && TeamData.LIST_OF_OFFENSIVE_POSITIONS.Contains(playerPosition) && !PlayState.IsKickoff)
      {
        switch (playerPosition)
        {
        }
      }
      else if (teamToCheck != teamOnOffense && TeamData.LIST_OF_DEFENSIVE_POSITIONS.Contains(playerPosition))
      {
        switch (playerPosition)
        {
          case Position.DL:
            if ((double) UnityEngine.Random.Range(0.0f, this.RANGE_FACEMASK) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptFacemask)
            {
              this._randomAfterPlayPenalty.Add(new Penalty("Facemask", "Defense", 5, PenaltyTime.PostPlay, PenaltyType.Facemask, PenaltyDownResult.LossOfDown, teamToCheck, player));
              this._randomAfterPlayPenalty.Add(new Penalty("Facemask", "Defense", 10, PenaltyTime.PostPlay, PenaltyType.Facemask, PenaltyDownResult.LossOfDown, teamToCheck, player));
              this._randomAfterPlayPenalty.Add(new Penalty("Facemask", "Defense", 15, PenaltyTime.PostPlay, PenaltyType.Facemask, PenaltyDownResult.LossOfDown, teamToCheck, player));
              break;
            }
            break;
          case Position.LB:
            if ((double) UnityEngine.Random.Range(0.0f, this.RANGE_FACEMASK) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptFacemask)
            {
              this._randomAfterPlayPenalty.Add(new Penalty("Facemask", "Defense", 5, PenaltyTime.PostPlay, PenaltyType.Facemask, PenaltyDownResult.LossOfDown, teamToCheck, player));
              this._randomAfterPlayPenalty.Add(new Penalty("Facemask", "Defense", 10, PenaltyTime.PostPlay, PenaltyType.Facemask, PenaltyDownResult.LossOfDown, teamToCheck, player));
              this._randomAfterPlayPenalty.Add(new Penalty("Facemask", "Defense", 15, PenaltyTime.PostPlay, PenaltyType.Facemask, PenaltyDownResult.LossOfDown, teamToCheck, player));
              break;
            }
            break;
          case Position.DB:
            if ((double) UnityEngine.Random.Range(0.0f, this.RANGE_FACEMASK) < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptFacemask)
            {
              this._randomAfterPlayPenalty.Add(new Penalty("Facemask", "Defense", 5, PenaltyTime.PostPlay, PenaltyType.Facemask, PenaltyDownResult.LossOfDown, teamToCheck, player));
              this._randomAfterPlayPenalty.Add(new Penalty("Facemask", "Defense", 10, PenaltyTime.PostPlay, PenaltyType.Facemask, PenaltyDownResult.LossOfDown, teamToCheck, player));
              this._randomAfterPlayPenalty.Add(new Penalty("Facemask", "Defense", 15, PenaltyTime.PostPlay, PenaltyType.Facemask, PenaltyDownResult.LossOfDown, teamToCheck, player));
              break;
            }
            break;
        }
      }
    }
    return this._randomAfterPlayPenalty.Count > 0 ? this._randomAfterPlayPenalty[UnityEngine.Random.Range(0, this._randomAfterPlayPenalty.Count)] : this.noPenalty;
  }

  public bool IsPenaltyOnSnap(int teamOnOffense) => false;

  private Penalty CheckForOnSnapPenalty(TeamData team, int teamOnOffense)
  {
    this._onSnapPenalties.Clear();
    for (int playerIndex = 0; playerIndex < team.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      Penalty randomOnSnapPenalty = this.GetRandomOnSnapPenalty(team.GetPlayer(playerIndex), team.TeamIndex, teamOnOffense);
      if (randomOnSnapPenalty.GetPenaltyType() != PenaltyType.None)
        this._onSnapPenalties.Add(randomOnSnapPenalty);
    }
    return this._onSnapPenalties.Count > 0 ? this._onSnapPenalties[UnityEngine.Random.Range(0, this._onSnapPenalties.Count)] : this.noPenalty;
  }

  private Penalty GetRandomOnSnapPenalty(PlayerData player, int teamToCheck, int teamOnOffense)
  {
    this._randomOnSnapPenalties.Clear();
    Position playerPosition = player.PlayerPosition;
    if (teamToCheck != teamOnOffense && TeamData.LIST_OF_DEFENSIVE_POSITIONS.Contains(playerPosition))
    {
      if (playerPosition != Position.DL)
      {
        if (playerPosition == Position.LB || playerPosition == Position.DB)
          ;
      }
      else if ((double) UnityEngine.Random.Range(0.0f, 1f) * (double) this.RANGE_OFFSIDES < (double) this.GetPlayerPenaltyPercentage(player.Discipline) && this.OptDefenseOffsides)
        this._randomOnSnapPenalties.Add(new Penalty("Offsides", "Defense", 5, PenaltyTime.DuringPlay, PenaltyType.Offsides, PenaltyDownResult.Repeat, teamToCheck, player));
    }
    return this._randomOnSnapPenalties.Count > 0 ? this._randomOnSnapPenalties[UnityEngine.Random.Range(0, this._randomOnSnapPenalties.Count)] : this.noPenalty;
  }

  public void ClearPenalty()
  {
    this.isPenaltyOnPlay = false;
    this.penaltyOnPlay = this.noPenalty;
    this.allPenaltiesOnPlay = new List<Penalty>();
    PenaltyManager.PREPLAY_PENALTY_CHECKTIME = (float) UnityEngine.Random.Range(500, 1500);
  }

  public bool IsAgainstPlayer1() => this.penaltyOnPlay.GetTeamIndex() == PersistentData.GetUserTeamIndex();

  public bool IsAgainstPlayer2() => this.penaltyOnPlay.GetTeamIndex() == PersistentData.GetCompTeamIndex();

  public bool DecidePenaltyForAI(int decidingTeamIndex)
  {
    bool flag = false;
    float penaltyFieldPosition1 = MatchManager.instance.FindAcceptPenaltyFieldPosition(this.GetPenaltyOnPlay().GetPenaltyYards());
    float penaltyFieldPosition2 = MatchManager.instance.FindDeclinePenaltyFieldPosition();
    if (PersistentData.GetOffensiveTeamIndex() == decidingTeamIndex)
      flag = (bool) ProEra.Game.MatchState.Turnover || (bool) ProEra.Game.MatchState.RunningPat || global::Game.PET_IsSafety || !global::Game.PET_IsMadeFG && !global::Game.PET_IsTouchdown && Field.FurtherDownfield(penaltyFieldPosition1, penaltyFieldPosition2);
    else if (PersistentData.GetDefensiveTeamIndex() == decidingTeamIndex)
    {
      if (MatchManager.instance.GetDeclinePenaltyDownAndDistance().Equals("Turnover on downs"))
        return false;
      if (PlayState.IsKickoff)
        flag = Field.FurtherDownfield(Field.KICKOFF_OOB_LOCATION, SingletonBehaviour<FieldManager, MonoBehaviour>.instance.tempBallPos);
      else if ((bool) ProEra.Game.MatchState.Turnover || global::Game.PET_IsSafety || ProEra.Game.MatchState.Down.Value > 4)
        flag = false;
      else if (global::Game.PET_IsMadeFG || global::Game.PET_IsTouchdown)
      {
        flag = true;
      }
      else
      {
        float firstObjectZPos = global::Game.OffenseGoingNorth ? Mathf.Min(ProEra.Game.MatchState.FirstDown.Value, Field.OFFENSIVE_GOAL_LINE) : Mathf.Max(ProEra.Game.MatchState.FirstDown.Value, Field.OFFENSIVE_GOAL_LINE);
        float tempBallPos = SingletonBehaviour<FieldManager, MonoBehaviour>.instance.tempBallPos;
        if (MatchManager.down == 4 && this.GetPenaltyOnPlay().GetPenaltyTime() == PenaltyTime.PrePlay)
          return true;
        flag = (MatchManager.down != 4 || !Field.FurtherDownfield(firstObjectZPos, tempBallPos)) && !Field.FurtherDownfield(penaltyFieldPosition1, penaltyFieldPosition2) && (this.downIfDecline <= this.downIfAccept || this.downIfDecline != 4);
      }
    }
    return flag;
  }

  private float GetPlayerPenaltyPercentage(int disciplineValue) => this.BasePlayerPenaltyPercentage + (float) (PenaltyManager.BASE_PLAYER_DISCIPLINE - disciplineValue) * (this.DISCIPLINE_PERCENT_INCREASE / (float) (100 - PenaltyManager.BASE_PLAYER_DISCIPLINE));

  private void SetPenalty(Penalty p1, Penalty p2)
  {
    if (p1.GetPenaltyType() != PenaltyType.None && p2.GetPenaltyType() != PenaltyType.None)
      this.penaltyOnPlay = UnityEngine.Random.Range(0, 2) == 0 ? p1 : p2;
    else if (p1.GetPenaltyType() != PenaltyType.None)
      this.penaltyOnPlay = p1;
    else if (p2.GetPenaltyType() != PenaltyType.None)
      this.penaltyOnPlay = p2;
    else
      this.penaltyOnPlay = this.noPenalty;
  }

  private bool PlayerOnField(int playerIndex)
  {
    List<PlayerAI> curUserScriptRef = MatchManager.instance.playersManager.curUserScriptRef;
    List<PlayerAI> curCompScriptRef = MatchManager.instance.playersManager.curCompScriptRef;
    for (int index = 0; index < curUserScriptRef.Count; ++index)
    {
      if (curUserScriptRef[index].indexOnTeam == playerIndex)
        return true;
    }
    for (int index = 0; index < curCompScriptRef.Count; ++index)
    {
      if (curCompScriptRef[index].indexOnTeam == playerIndex)
        return true;
    }
    return false;
  }
}
