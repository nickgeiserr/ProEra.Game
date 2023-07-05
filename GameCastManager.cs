// Decompiled with JetBrains decompiler
// Type: GameCastManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameCastManager : MonoBehaviour
{
  [SerializeField]
  protected PlayerProfile _playerProfile;
  private string _homeTeamAbbr = "";
  private string _awayTeamAbbr;
  private bool _offNorth;
  private bool _offHome;
  private string _playerName;
  private readonly LinksHandler _linksHandler = new LinksHandler();

  public event Action<string, string> OnCastWritten;

  private void Start()
  {
    PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);
    this._linksHandler.SetLinks(new List<EventHandle>()
    {
      ProEra.Game.MatchState.CurrentMatchState.Link<EMatchState>(new Action<EMatchState>(this.HandleMatchStateChanged))
    });
  }

  private void Update()
  {
  }

  private void HandleGameEvent(PEGameplayEvent e)
  {
    if (!(e is PEPlayOverEvent))
      return;
    try
    {
      this.WriteGameCast();
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ("Failed to write GameCast: " + ex?.ToString()));
    }
  }

  private void WriteGameCast()
  {
    List<PEGameplayEvent> playEvents = PEGameplayEventManager.PlayEvents;
    PEPlaySelectedEvent start = (PEPlaySelectedEvent) null;
    PEGameplayEvent peGameplayEvent1 = (PEGameplayEvent) null;
    PEGameplayEvent peGameplayEvent2 = (PEGameplayEvent) null;
    PEGameplayEvent peGameplayEvent3 = (PEGameplayEvent) null;
    PEPenaltyEvent pePenaltyEvent = (PEPenaltyEvent) null;
    PEPlayOverEvent endEvent = (PEPlayOverEvent) null;
    PEQBRunningEvent rushEvent1 = (PEQBRunningEvent) null;
    for (int index = 0; index < playEvents.Count; ++index)
    {
      Debug.Log((object) playEvents[index].GetType());
      if (playEvents[index] is PEBallKickedEvent || playEvents[index] is PEBallThrownEvent)
        peGameplayEvent1 = playEvents[index];
      else if (playEvents[index] is PEBallHandoffEvent || playEvents[index] is PEBallCaughtEvent || playEvents[index] is PEKickReturnEvent)
        peGameplayEvent2 = playEvents[index];
      else if (playEvents[index] is PETackleEvent)
        peGameplayEvent3 = playEvents[index];
      else if (playEvents[index] is PEPenaltyEvent)
        pePenaltyEvent = (PEPenaltyEvent) playEvents[index];
      else if (playEvents[index] is PEPlayOverEvent)
        endEvent = (PEPlayOverEvent) playEvents[index];
      else if (playEvents[index] is PEPlaySelectedEvent)
        start = (PEPlaySelectedEvent) playEvents[index];
      else if (playEvents[index] is PEBallHikedEvent)
      {
        PEBallHikedEvent peBallHikedEvent = (PEBallHikedEvent) playEvents[index];
      }
      else if (playEvents[index] is PEQBRunningEvent peqbRunningEvent)
        rushEvent1 = peqbRunningEvent;
    }
    if (start == null)
      return;
    this._offNorth = start.OffenseGoingNorth;
    this._offHome = start.HomeOnOffense;
    string str1 = this.GetNumberString(start.Down) + " & " + start.Distance.ToString() + " at " + this.GetFieldPosition(start.BallPosition.z) + "\n";
    int num1 = ProEra.Game.MatchState.GameLength.Value - Mathf.CeilToInt(start.GameTime);
    int num2 = num1 % 60;
    string str2 = "(" + (num1 / 60 % 60).ToString() + ":" + num2.ToString("00") + " - " + this.GetNumberString(start.Quarter) + ") ";
    if (start.OffenseData.GetFormation().GetBaseFormation() == BaseFormation.Shotgun)
      str2 += "(Shotgun) ";
    PlayDataOff offData = start.OffenseData;
    PlayType playType = offData.GetPlayType();
    int distance = this.GetDistance(start.BallPosition.z, endEvent.BallPosition.z);
    if (endEvent.Type == PlayEndType.PrePlayPenalty && pePenaltyEvent != null)
    {
      str2 = str2 + "PENALTY on " + (this._offHome ? this._homeTeamAbbr : this._awayTeamAbbr) + "-";
      if (pePenaltyEvent.Type == PenaltyType.DelayOfGame)
        str2 = str2 + "Delay of Game, 5 yards, enforced at " + this.GetFieldPosition(start.BallPosition.z) + " - No Play.";
    }
    else if (peGameplayEvent2 == null && peGameplayEvent3 != null && rushEvent1 == null && distance < 0)
    {
      PETackleEvent peTackleEvent = (PETackleEvent) peGameplayEvent3;
      str2 = str2 + (peTackleEvent.UserTackled ? this._playerName : peTackleEvent.BallHolder.playerName) + " sacked at " + this.GetFieldPosition(endEvent.BallPosition.z) + " for " + this.GetDistance(start.BallPosition.z, endEvent.BallPosition.z).ToString() + " yards" + " (" + peTackleEvent.Tackler.playerName + ")";
    }
    else if (global::Game.IsQBKneel)
    {
      if ((UnityEngine.Object) endEvent.BallHolder != (UnityEngine.Object) null)
        str2 += endEvent.BallHolder.playerName;
      str2 = str2 + " kneels to " + this.GetFieldPosition(endEvent.BallPosition.z) + " for " + this.GetDistance(start.BallPosition.z, endEvent.BallPosition.z).ToString() + " yards";
    }
    else if (global::Game.IsQBSpike)
      str2 = str2 + global::Game.OffensiveQB.playerName + " spikes at " + this.GetFieldPosition(endEvent.BallPosition.z) + " for " + this.GetDistance(start.BallPosition.z, endEvent.BallPosition.z).ToString() + " yards";
    else if (playType == PlayType.Kickoff)
    {
      PEBallKickedEvent peBallKickedEvent = (PEBallKickedEvent) peGameplayEvent1;
      str1 = "";
      str2 = str2 + peBallKickedEvent.Kicker.playerName + " kicks ";
      if (peGameplayEvent2 != null)
      {
        PEKickReturnEvent peKickReturnEvent = (PEKickReturnEvent) peGameplayEvent2;
        str2 = str2 + this.GetDistance(peBallKickedEvent.BallPosition.z, peGameplayEvent2.BallPosition.z).ToString() + " yards from " + this.GetFieldPosition(peBallKickedEvent.BallPosition.z) + " to " + this.GetFieldPosition(peGameplayEvent2.BallPosition.z) + ". " + peKickReturnEvent.Receiver.playerName + " to " + this.GetFieldPosition(endEvent.BallPosition.z) + " for " + this.GetDistance(endEvent.BallPosition.z, peKickReturnEvent.BallPosition.z).ToString() + " yards";
        if (peGameplayEvent3 != null)
        {
          PETackleEvent peTackleEvent = (PETackleEvent) peGameplayEvent3;
          str2 = str2 + " (" + peTackleEvent.Tackler.playerName + ")";
        }
      }
      else if (endEvent.Type == PlayEndType.OOB)
        str2 = str2 + this.GetDistance(peBallKickedEvent.BallPosition.z, endEvent.BallPosition.z).ToString() + " yards from " + this.GetFieldPosition(peBallKickedEvent.BallPosition.z) + " to " + this.GetFieldPosition(endEvent.BallPosition.z) + ", out of bounds";
    }
    else if (playType == PlayType.Pass && rushEvent1 == null)
    {
      PEBallThrownEvent peBallThrownEvent = (PEBallThrownEvent) peGameplayEvent1;
      if (peBallThrownEvent != null)
      {
        string str3 = peBallThrownEvent.IsUser ? this._playerName : peBallThrownEvent.Thrower.playerName;
        if (endEvent.Type != PlayEndType.Touchdown)
        {
          string str4 = str2 + str3 + " pass ";
          if (peGameplayEvent2 != null)
          {
            PEBallCaughtEvent peBallCaughtEvent = (PEBallCaughtEvent) peGameplayEvent2;
            string direction = this.GetDirection(peBallThrownEvent.BallPosition.x, peBallCaughtEvent.BallPosition.x);
            string distanceDescription = this.GetDistanceDescription(peBallThrownEvent.BallPosition.z, peBallCaughtEvent.BallPosition.z);
            string str5 = str4 + distanceDescription + " " + direction;
            if (!peBallCaughtEvent.Interception)
            {
              string str6 = str5 + " to " + peBallCaughtEvent.Receiver.playerName;
              str2 = (endEvent.Type != PlayEndType.OOB ? str6 + " to " : str6 + " pushed ob at ") + this.GetFieldPosition(endEvent.BallPosition.z) + " for " + this.GetDistance(start.BallPosition.z, endEvent.BallPosition.z).ToString() + " yards";
            }
            else
              str2 = str5 + " intended for " + ((UnityEngine.Object) peBallThrownEvent.IntendedReceiver == (UnityEngine.Object) null ? "no one" : peBallThrownEvent.IntendedReceiver.playerName) + " INTERCEPTED by " + peBallCaughtEvent.Receiver.playerName + " at " + this.GetFieldPosition(peBallCaughtEvent.BallPosition.z) + ". " + peBallCaughtEvent.Receiver.playerName + " to " + this.GetFieldPosition(endEvent.BallPosition.z) + " for " + this.GetDistance(peBallCaughtEvent.BallPosition.z, endEvent.BallPosition.z).ToString() + " yards";
            if (peGameplayEvent3 != null)
            {
              PETackleEvent peTackleEvent = (PETackleEvent) peGameplayEvent3;
              str2 = str2 + " (" + peTackleEvent.Tackler.playerName + ")";
            }
          }
          else
          {
            string direction = this.GetDirection(peBallThrownEvent.BallPosition.x, endEvent.BallPosition.x);
            string distanceDescription = this.GetDistanceDescription(peBallThrownEvent.BallPosition.z, endEvent.BallPosition.z);
            str2 = str4 + " incomplete " + distanceDescription + " " + direction + " to " + ((UnityEngine.Object) peBallThrownEvent.IntendedReceiver == (UnityEngine.Object) null ? "no one" : peBallThrownEvent.IntendedReceiver.playerName);
          }
        }
        else
        {
          PEBallCaughtEvent peBallCaughtEvent = (PEBallCaughtEvent) peGameplayEvent2;
          if (peGameplayEvent2 != null && (UnityEngine.Object) peBallCaughtEvent.Receiver != (UnityEngine.Object) null)
            str2 = str2 + peBallCaughtEvent.Receiver.playerName + " " + this.GetDistance(start.BallPosition.z, endEvent.BallPosition.z).ToString() + " yard pass from " + str3 + ". ";
        }
      }
    }
    else if (playType == PlayType.Run || rushEvent1 != null)
    {
      PETackleEvent peTackleEvent = (PETackleEvent) null;
      if (peGameplayEvent3 != null)
        peTackleEvent = (PETackleEvent) peGameplayEvent3;
      if (rushEvent1 != null)
      {
        str2 += GetRushFormatString(rushEvent1.QB, (PEGameplayEvent) rushEvent1);
        if (peTackleEvent != null)
          str2 += GetTacklerFormatString(peTackleEvent.Tackler.playerName);
      }
      else
      {
        PEBallHandoffEvent rushEvent2 = (PEBallHandoffEvent) peGameplayEvent2;
        if (rushEvent2 != null && (UnityEngine.Object) rushEvent2.Receiver != (UnityEngine.Object) null)
          str2 += GetRushFormatString(rushEvent2.Receiver, (PEGameplayEvent) rushEvent2);
        else if (rushEvent2 == null)
          Debug.LogWarning((object) "Had a run play with with a null run event!");
        else if ((UnityEngine.Object) rushEvent2.Receiver == (UnityEngine.Object) null)
          Debug.LogWarning((object) "Had a run play with with a null receiver!");
        if (peTackleEvent != null)
          str2 += GetTacklerFormatString(peTackleEvent.Tackler.playerName);
      }
    }
    else if (playType == PlayType.Punt)
    {
      if (peGameplayEvent1 != null)
      {
        PEBallKickedEvent peBallKickedEvent = (PEBallKickedEvent) peGameplayEvent1;
        if (peBallKickedEvent != null)
        {
          str2 = str2 + peBallKickedEvent.Kicker.playerName + " punts ";
          if (peGameplayEvent2 != null)
          {
            try
            {
              PEKickReturnEvent peKickReturnEvent = (PEKickReturnEvent) peGameplayEvent2;
              str2 = str2 + this.GetDistance(peBallKickedEvent.BallPosition.z, peGameplayEvent2.BallPosition.z).ToString() + " yards to " + this.GetFieldPosition(peGameplayEvent2.BallPosition.z) + ", Center-" + peBallKickedEvent.Center.playerName + ". ";
              this._offNorth = !this._offNorth;
              str2 = str2 + peKickReturnEvent.Receiver.playerName + " to " + this.GetFieldPosition(endEvent.BallPosition.z) + " for " + this.GetDistance(peKickReturnEvent.BallPosition.z, endEvent.BallPosition.z).ToString() + " yards";
              if (peGameplayEvent3 != null)
              {
                PETackleEvent peTackleEvent = (PETackleEvent) peGameplayEvent3;
                str2 = str2 + " (" + peTackleEvent.Tackler.playerName + ")";
              }
            }
            catch (Exception ex)
            {
              Debug.LogException(ex);
            }
          }
          else if (endEvent.Type == PlayEndType.OOB)
            str2 = str2 + this.GetDistance(peBallKickedEvent.BallPosition.z, endEvent.BallPosition.z).ToString() + " yards to " + this.GetFieldPosition(endEvent.BallPosition.z) + ", Center-" + peBallKickedEvent.Center.playerName + ", out of bounds";
        }
      }
      else
        str2 += " Failed punt";
    }
    else if (playType == PlayType.FG)
    {
      PEBallKickedEvent peBallKickedEvent = (PEBallKickedEvent) peGameplayEvent1;
      if (peBallKickedEvent != null)
      {
        if (!peBallKickedEvent.IsPAT)
        {
          string str7 = str2 + peBallKickedEvent.Kicker.playerName + " " + this.GetDistance(peBallKickedEvent.BallPosition.z, endEvent.BallPosition.z).ToString() + " yard field goal is ";
          if (endEvent.Type == PlayEndType.MadeFG)
            str7 += "GOOD, ";
          else if (endEvent.Type == PlayEndType.MissedFG)
          {
            string direction = this.GetDirection(peBallKickedEvent.BallPosition.x, endEvent.BallPosition.x);
            str7 = str7 + "NO GOOD, Wide " + direction + ", ";
          }
          str2 = str7 + "Center-" + peBallKickedEvent.Center.playerName + ", Holder-" + peBallKickedEvent.Holder.playerName;
        }
        else
        {
          str1 = "";
          string str8 = "(" + peBallKickedEvent.Kicker.playerName;
          if (endEvent.Type == PlayEndType.MadeFG)
            str8 += " PAT good";
          else if (endEvent.Type == PlayEndType.MissedFG)
            str8 += " PAT failed";
          str2 = str8 + ")";
        }
      }
    }
    Debug.Log((object) ("GC\n" + str1 + str2));
    Action<string, string> onCastWritten = this.OnCastWritten;
    if (onCastWritten == null)
      return;
    onCastWritten(str1, str2);

    string GetRushFormatString(PlayerAI player, PEGameplayEvent rushEvent)
    {
      string str = playType == PlayType.Run ? offData.GetPlayConceptString() : "SCRAMBLE";
      return player.playerName + " " + str + " " + this.GetDirection(rushEvent.BallPosition.x, endEvent.BallPosition.x) + " to " + this.GetFieldPosition(endEvent.BallPosition.z) + " for " + this.GetDistance(start.BallPosition.z, endEvent.BallPosition.z).ToString() + " yards";
    }

    static string GetTacklerFormatString(string playerName) => " (" + playerName + ")";
  }

  private string GetFieldPosition(float ballPos)
  {
    int lineByFieldLocation = Field.GetYardLineByFieldLocation(ballPos);
    return (this._offNorth && this._offHome || !this._offNorth && !this._offHome ? ((double) ballPos < 0.0 ? this._awayTeamAbbr : this._homeTeamAbbr) : ((double) ballPos <= 0.0 ? this._homeTeamAbbr : this._awayTeamAbbr)) + " " + lineByFieldLocation.ToString();
  }

  private string GetNumberString(int num)
  {
    switch (num)
    {
      case 1:
        return "1st";
      case 2:
        return "2nd";
      case 3:
        return "3rd";
      case 4:
        return "4th";
      default:
        return "";
    }
  }

  private void HandleMatchStateChanged(EMatchState state)
  {
    if (state == EMatchState.Beginning || !(this._homeTeamAbbr == ""))
      return;
    this._homeTeamAbbr = PersistentData.GetHomeTeamAbbreviation();
    this._awayTeamAbbr = PersistentData.GetAwayTeamAbbreviation();
    this._playerName = (string) this._playerProfile.Customization.LastName;
  }

  private string GetDistanceText(float start, float end) => this.GetDistance(start, end).ToString();

  private int GetDistance(float start, float end) => Field.ConvertDistanceToYards(end - start) * (this._offNorth ? 1 : -1);

  private string GetDistanceDescription(float start, float end)
  {
    float distance = (float) this.GetDistance(start, end);
    if ((double) distance < 10.0)
      return "short";
    return (double) distance < 20.0 ? "" : "deep";
  }

  private string GetDirection(float start, float end)
  {
    float distance = (float) this.GetDistance(start, end);
    if ((double) distance > 3.0)
      return "right";
    return (double) distance < -3.0 ? "left" : "middle";
  }
}
