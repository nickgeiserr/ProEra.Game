// Decompiled with JetBrains decompiler
// Type: CPUDefenseAdjustmentManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class CPUDefenseAdjustmentManager : MonoBehaviour
{
  public static CPUDefenseAdjustmentManager instance;
  private float[] gapPositions;
  private float[] techPositions;
  private float C_XPos;
  private float RG_XPos = 2f;
  private float RT_XPos = 4f;
  private float RTE_XPos = 6f;
  private const float OVER_SHIFT_CHANCE_RUN = 0.25f;
  private const float UNDER_SHIFT_CHANCE_RUN = 0.1f;
  private const float OVER_SHIFT_CHANCE_PASS = 0.25f;
  private const float UNDER_SHIFT_CHANCE_PASS = 0.1f;
  private bool checkForShift = true;
  private Vector3[] adjustedPositions;
  private bool PlayCalledComplete;
  private Position[] playerPositions;
  private float[] playerXPos;
  private float[] playerZPos;
  private bool ENABLE_DL_SHIFT;
  private const float MIN_PREPLAY_TIME_BEFORE_SHIFT = 1.5f;
  private const float LB_SHOWBLITZ_CHANCE_HAS_BLITZ_ASSIGNMENT = 0.75f;
  private const float LB_SHOWBLITZ_CHANCE_NO_BLITZ_ASSIGNMENT = 0.25f;
  private const float DB_SHOWBLITZ_CHANCE_HAS_BLITZ_ASSIGNMENT = 0.5f;
  private const float DB_SHOWBLITZ_CHANCE_NO_BLITZ_ASSIGNMENT = 0.1f;
  private const float SAFETY_SHOWBLITZ_CHANCE_HAS_BLITZ_ASSIGNMENT = 0.9f;
  private const float SAFETY_SHOWBLITZ_CHANCE_NO_BLITZ_ASSIGNMENT = 0.05f;

  private void Awake()
  {
    if ((UnityEngine.Object) CPUDefenseAdjustmentManager.instance == (UnityEngine.Object) null)
      CPUDefenseAdjustmentManager.instance = this;
    this.gapPositions = new float[4];
    this.gapPositions[0] = 1f;
    this.gapPositions[1] = 3f;
    this.gapPositions[2] = 5f;
    this.gapPositions[3] = 7f;
    this.techPositions = new float[11];
    this.techPositions[0] = this.C_XPos;
    this.techPositions[1] = this.C_XPos + 0.5f;
    this.techPositions[2] = this.RG_XPos - 0.5f;
    this.techPositions[3] = this.RG_XPos;
    this.techPositions[4] = this.RG_XPos + 0.5f;
    this.techPositions[5] = this.RT_XPos - 0.5f;
    this.techPositions[6] = this.RT_XPos;
    this.techPositions[7] = this.RT_XPos + 0.5f;
    this.techPositions[8] = this.RTE_XPos - 0.5f;
    this.techPositions[9] = this.RTE_XPos;
    this.techPositions[10] = this.RTE_XPos + 1f;
    this.adjustedPositions = new Vector3[11];
    NotificationCenter.AddListener("setupPlayEnd", new Callback(this.OnPlaySetupEnd));
    PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);
  }

  private void HandleGameEvent(PEGameplayEvent e)
  {
    switch (e)
    {
      case PEPlayOverEvent pePlayOverEvent:
        this.OnPlayComplete(pePlayOverEvent.Type.ToString());
        break;
      case PEAudibleCalledEvent _:
        this.OnAudibleCalled();
        break;
    }
  }

  private void OnDisable() => NotificationCenter.RemoveListener("setupPlayEnd", new Callback(this.OnPlaySetupEnd));

  public void CheckForDefenseShifts()
  {
    if (!global::Game.IsPlayInactive || !this.PlayCalledComplete || !this.checkForShift || (double) MatchManager.instance.prePlayTimer <= 1.5 || !global::Game.IsNotKickoff || global::Game.IsPunt || global::Game.IsRunningPAT || MatchManager.instance.GetNumberOfSetPlayersDef() < 11)
      return;
    if (this.ENABLE_DL_SHIFT && (MatchManager.instance.playManager.savedDefPlay.GetFormation().GetBaseFormation() == BaseFormation.Four_Three || MatchManager.instance.playManager.savedDefPlay.GetFormation().GetBaseFormation() == BaseFormation.Four_Four || MatchManager.instance.playManager.savedDefPlay.GetFormation().GetBaseFormation() == BaseFormation.Nickel || MatchManager.instance.playManager.savedDefPlay.GetFormation().GetBaseFormation() == BaseFormation.Dime))
      this.CheckDLShifts();
    this.CheckShifts();
    this.CheckShowBlitz();
    this.checkForShift = false;
  }

  public void SetDEShift(
    int playerIndex,
    PlayerAI player,
    Position playerTeamPos,
    LOS_Side playerSide,
    Vector3 newPos)
  {
    if (player.onOffense)
    {
      Debug.LogError((object) string.Format("Error! Show blitz assignment on offensive player -- #{0}, {1}, {2} ", (object) player.number, (object) player.position, (object) player.playerName));
    }
    else
    {
      Vector3 vector3 = new Vector3(MatchManager.instance.ballHashPosition, 0.0f, ProEra.Game.MatchState.BallOn.Value);
      Debug.DrawLine(vector3 + Vector3.left * 20f, vector3 + Vector3.right * 20f, Color.magenta, 10f);
      newPos *= Field.ONE_YARD;
      if (playerSide == LOS_Side.Side_Left)
        newPos.x *= (float) -global::Game.OffensiveFieldDirection;
      newPos += vector3;
      newPos.z = player.trans.position.z;
      if (this.CanShiftToPos(playerIndex, newPos))
      {
        this.SetPlayerAdjustPos(playerIndex, newPos);
        player.behaviorTree.SetVariableValue("DoDefenseShift", (object) true);
        player.behaviorTree.SetVariableValue("ShiftPosition", (object) newPos);
        player.behaviorTree.SetVariableValue("ShiftRole", (object) (int) playerTeamPos);
        Debug.DrawLine(player.trans.position, newPos, Color.cyan, 10f);
        AIUtil.DrawDebugCube(newPos + Vector3.up * 1f, new Vector3(0.5f, 0.5f, 0.5f), Color.cyan, 10f);
      }
      else
      {
        Debug.DrawLine(player.trans.position, newPos, Color.cyan, 10f);
        AIUtil.DrawDebugCube(newPos + Vector3.up * 1f, new Vector3(0.5f, 0.5f, 0.5f), Color.red, 10f);
      }
    }
  }

  public void SetDBShift(
    int playerIndex,
    PlayerAI player,
    Position playerTeamPos,
    LOS_Side playerSide,
    Vector3 shiftPos)
  {
    if (player.onOffense)
      Debug.LogError((object) ("Error! Show blitz assignment on offensive player -- #" + player.number.ToString() + ", " + player.position.ToString() + ", " + player.playerName.ToString()));
    else if (this.CanShiftToPos(playerIndex, shiftPos))
    {
      this.SetPlayerAdjustPos(playerIndex, shiftPos);
      player.behaviorTree.SetVariableValue("DoDefenseShift", (object) true);
      player.behaviorTree.SetVariableValue("ShiftPosition", (object) shiftPos);
      player.behaviorTree.SetVariableValue("ShiftRole", (object) (int) playerTeamPos);
    }
    else
    {
      Debug.DrawLine(player.trans.position, shiftPos, Color.red, 10f);
      AIUtil.DrawDebugCube(shiftPos + Vector3.up * 1f, new Vector3(0.5f, 0.5f, 0.5f), Color.red, 10f);
    }
  }

  public void SetShowBlitz(
    int playerIndex,
    PlayerAI player,
    Position playerTeamPos,
    float blitzGapPos)
  {
    if (player.onOffense)
    {
      Debug.LogError((object) string.Format("Error! Show blitz assignment on offensive player -- #{0}, {1}, {2} ", (object) player.number, (object) player.position, (object) player.playerName));
    }
    else
    {
      Vector3 vector3_1 = new Vector3(MatchManager.instance.ballHashPosition, 0.0f, ProEra.Game.MatchState.BallOn.Value);
      Vector3 vector3_2 = new Vector3(blitzGapPos * Field.ONE_YARD, 0.0f, (float) global::Game.OffensiveFieldDirection * 2f * Field.ONE_YARD) + vector3_1;
      if (this.CanShiftToPos(playerIndex, vector3_2))
      {
        this.SetPlayerAdjustPos(playerIndex, vector3_2);
        player.showingBlitz = true;
        player.behaviorTree.SetVariableValue("DoShowBlitz", (object) true);
        player.behaviorTree.SetVariableValue("ShiftPosition", (object) vector3_2);
        player.behaviorTree.SetVariableValue("ShiftRole", (object) (int) playerTeamPos);
      }
      else
      {
        Debug.DrawLine(player.trans.position, vector3_2, Color.red, 10f);
        AIUtil.DrawDebugCube(vector3_2 + Vector3.up * 1f, new Vector3(0.5f, 0.5f, 0.5f), Color.red, 10f);
      }
    }
  }

  private void DLOverShift()
  {
    for (int index = 0; index < 11; ++index)
    {
      if (global::Game.DefensivePlayers[index].animatorCommunicator.atPlayPosition)
      {
        if (AIUtil.GetWeakSide() == AIUtil.GetPlayerSide(this.playerXPos[index], (float) global::Game.OffensiveFieldDirection))
        {
          if (this.playerPositions[index] == Position.DE)
            this.SetDEShift(index, global::Game.DefensivePlayers[index], this.playerPositions[index], AIUtil.GetPlayerSide(this.playerXPos[index], (float) global::Game.OffensiveFieldDirection), new Vector3(this.techPositions[6], 0.0f, this.playerZPos[index]));
          else if (this.playerPositions[index] == Position.DT)
            this.SetDEShift(index, global::Game.DefensivePlayers[index], this.playerPositions[index], AIUtil.GetPlayerSide(this.playerXPos[index], (float) global::Game.OffensiveFieldDirection), new Vector3(this.techPositions[1], 0.0f, this.playerZPos[index]));
        }
        else if (this.playerPositions[index] == Position.DT || this.playerPositions[index] == Position.NT)
          this.SetDEShift(index, global::Game.DefensivePlayers[index], this.playerPositions[index], AIUtil.GetPlayerSide(this.playerXPos[index], (float) global::Game.OffensiveFieldDirection), new Vector3(this.techPositions[4], 0.0f, this.playerZPos[index]));
      }
    }
  }

  private void DLUnderShift()
  {
    for (int index = 0; index < 11; ++index)
    {
      if (global::Game.DefensivePlayers[index].animatorCommunicator.atPlayPosition)
      {
        LOS_Side weakSide = AIUtil.GetWeakSide();
        LOS_Side playerSide = weakSide == LOS_Side.Side_Left ? LOS_Side.Side_Right : LOS_Side.Side_Left;
        if (playerSide == AIUtil.GetPlayerSide(this.playerXPos[index], (float) global::Game.OffensiveFieldDirection))
        {
          if (this.playerPositions[index] == Position.DE)
            this.SetDEShift(index, global::Game.DefensivePlayers[index], this.playerPositions[index], playerSide, new Vector3(this.techPositions[6], 0.0f, this.playerZPos[index]));
          else if (this.playerPositions[index] == Position.DT)
            this.SetDEShift(index, global::Game.DefensivePlayers[index], this.playerPositions[index], playerSide, new Vector3(this.techPositions[1], 0.0f, this.playerZPos[index]));
        }
        else if (this.playerPositions[index] == Position.DT || this.playerPositions[index] == Position.NT)
          this.SetDEShift(index, global::Game.DefensivePlayers[index], this.playerPositions[index], weakSide, new Vector3(this.techPositions[4], 0.0f, this.playerZPos[index]));
      }
    }
  }

  private void CheckDLShifts()
  {
    if (!global::Game.IsPlayInactive || !this.PlayCalledComplete)
      return;
    float num1;
    float num2;
    if (AIUtil.GuessIsRunPlay(MatchManager.instance.playManager.savedOffPlay.GetFormation()))
    {
      num1 = 0.25f;
      num2 = 0.1f;
    }
    else
    {
      num1 = 0.25f;
      num2 = 0.1f;
    }
    float num3 = UnityEngine.Random.value;
    if ((double) num3 > (double) num1 + (double) num2)
      return;
    if ((double) num1 <= (double) num2 && (double) num3 <= (double) num1 || (double) num1 > (double) num2 && (double) num3 >= (double) num2)
      this.DLOverShift();
    else
      this.DLUnderShift();
  }

  private void CheckShifts()
  {
    if (!global::Game.IsPlayInactive || !this.PlayCalledComplete)
      return;
    float z = 0.0f;
    float x = 0.0f;
    for (int index = 0; index < 11; ++index)
    {
      Position playerPosition = this.playerPositions[index];
      PlayerAI player = (PlayerAI) null;
      bool flag = false;
      if (playerPosition == Position.SS || playerPosition == Position.FS || playerPosition == Position.CB || playerPosition == Position.DB || playerPosition == Position.LB || playerPosition == Position.MLB || playerPosition == Position.OLB)
      {
        player = !global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[index] : MatchManager.instance.playersManager.curCompScriptRef[index];
        z = player.trans.position.z;
        x = player.trans.position.x;
        if ((UnityEngine.Object) player.ManCoverTarget != (UnityEngine.Object) null)
        {
          x = player.ManCoverTarget.trans.position.x;
          flag = true;
        }
        if (playerPosition != Position.LB && playerPosition != Position.MLB && playerPosition != Position.OLB)
        {
          if ((double) Mathf.Abs(Field.OFFENSIVE_GOAL_LINE - (float) ProEra.Game.MatchState.BallOn) < 10.0 * (double) Field.ONE_YARD && (double) Mathf.Abs(player.trans.position.z - (float) ProEra.Game.MatchState.BallOn) > 4.0 * (double) Field.ONE_YARD)
          {
            z = (float) ProEra.Game.MatchState.BallOn + (float) global::Game.OffensiveFieldDirection * 4f * Field.ONE_YARD;
            flag = true;
          }
          if ((double) Mathf.Abs(Field.OFFENSIVE_GOAL_LINE - (float) ProEra.Game.MatchState.BallOn) < 5.0 * (double) Field.ONE_YARD && (playerPosition == Position.CB || playerPosition == Position.DB) && (UnityEngine.Object) player.ManCoverTarget == (UnityEngine.Object) null)
          {
            KeyValuePair<int, float> keyValuePair = MatchManager.instance.playersManager.receiversLeftToRight[0];
            float num1 = keyValuePair.Value;
            keyValuePair = MatchManager.instance.playersManager.receiversLeftToRight[MatchManager.instance.playersManager.receiversLeftToRight.Count - 1];
            float num2 = keyValuePair.Value;
            x = AIUtil.GetPlayerSide(this.playerXPos[index], (float) global::Game.OffensiveFieldDirection) != LOS_Side.Side_Left ? num2 : num1;
            z = (float) ProEra.Game.MatchState.BallOn + (float) global::Game.OffensiveFieldDirection * 1.5f * Field.ONE_YARD;
            flag = true;
          }
        }
      }
      Vector3 vector3 = new Vector3(x, 0.0f, z);
      if (flag && (bool) (UnityEngine.Object) player && (double) Vector3.Distance(player.trans.position, vector3) > 0.25 * (double) Field.ONE_YARD)
        this.SetDBShift(index, player, playerPosition, AIUtil.GetPlayerSide(this.playerXPos[index], (float) global::Game.OffensiveFieldDirection), vector3);
    }
  }

  private void CheckShowBlitz()
  {
    if (!global::Game.IsPlayInactive || !this.PlayCalledComplete)
      return;
    for (int index = 0; index < 11; ++index)
    {
      Position playerPosition = this.playerPositions[index];
      float blitzGapPos = 0.0f;
      float num = 0.0f;
      PlayerAI player = !global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curUserScriptRef[index] : MatchManager.instance.playersManager.curCompScriptRef[index];
      if (player.onOffense)
        Debug.LogError((object) " Error--trying to show blitz on a offensive player");
      else if (!player.showingBlitz && player.savedStance != 0 && (double) Mathf.Abs((float) ProEra.Game.MatchState.BallOn - player.trans.position.z) >= 1.5 * (double) Field.ONE_YARD)
      {
        switch (playerPosition)
        {
          case Position.OLB:
          case Position.ILB:
          case Position.MLB:
          case Position.FS:
          case Position.SS:
            if (((SharedVariable<bool>) player.behaviorTree.GetVariable("DoDefenseShift")).Value)
            {
              Debug.Log((object) (playerPosition.ToString() + " -- Cannot show blitz-- player will shift pos"));
              continue;
            }
            SharedBool variable = (SharedBool) player.behaviorTree.GetVariable("GetToFormPos");
            if (!player.animatorCommunicator.atPlayPosition || variable.Value)
            {
              Debug.Log((object) (playerPosition.ToString() + " -- Cannot show blitz-- player is not at formation postion!"));
              continue;
            }
            if (!((UnityEngine.Object) player.ManCoverTarget != (UnityEngine.Object) null) || !((UnityEngine.Object) player.coveringPlayerScript != (UnityEngine.Object) null) || AIUtil.IsInBackfield(player.coveringPlayerScript.trans.position.z))
            {
              switch (playerPosition)
              {
                case Position.OLB:
                  num = player.defType == EPlayAssignmentId.Blitz ? 0.75f : 0.25f;
                  blitzGapPos = this.GetGapPosition(GapName.Gap_D, AIUtil.GetPlayerSide(player.trans.position.x, (float) global::Game.OffensiveFieldDirection));
                  break;
                case Position.ILB:
                  num = player.defType == EPlayAssignmentId.Blitz ? 0.75f : 0.25f;
                  blitzGapPos = this.GetGapPosition(GapName.Gap_A, AIUtil.GetPlayerSide(player.trans.position.x, (float) global::Game.OffensiveFieldDirection));
                  break;
                case Position.MLB:
                  num = player.defType == EPlayAssignmentId.Blitz ? 0.75f : 0.25f;
                  blitzGapPos = this.GetGapPosition(GapName.Gap_A, AIUtil.GetPlayerSide(player.trans.position.x, (float) global::Game.OffensiveFieldDirection));
                  break;
                case Position.CB:
                  num = player.defType == EPlayAssignmentId.Blitz ? 0.5f : 0.1f;
                  blitzGapPos = this.GetGapPosition(GapName.Gap_D, AIUtil.GetPlayerSide(player.trans.position.x, (float) global::Game.OffensiveFieldDirection));
                  break;
                case Position.FS:
                  num = player.defType == EPlayAssignmentId.Blitz ? 0.9f : 0.05f;
                  blitzGapPos = this.GetGapPosition(GapName.Gap_B, AIUtil.GetPlayerSide(player.trans.position.x, (float) global::Game.OffensiveFieldDirection));
                  break;
                case Position.SS:
                  num = player.defType == EPlayAssignmentId.Blitz ? 0.9f : 0.05f;
                  blitzGapPos = this.GetGapPosition(GapName.Gap_B, AIUtil.GetPlayerSide(player.trans.position.x, (float) global::Game.OffensiveFieldDirection));
                  break;
              }
              if ((double) UnityEngine.Random.value <= (double) num)
              {
                this.SetShowBlitz(index, player, playerPosition, blitzGapPos);
                continue;
              }
              continue;
            }
            continue;
          case Position.CB:
            if (AIUtil.IsNickelBack(MatchManager.instance.playManager.savedDefPlay, index) || AIUtil.IsDimeBack(MatchManager.instance.playManager.savedDefPlay, index))
              goto case Position.OLB;
            else
              continue;
          default:
            continue;
        }
      }
    }
  }

  public float GetGapPosition(GapName gap, LOS_Side side)
  {
    int index = (int) gap;
    return index >= 0 && index < this.gapPositions.Length ? (float) FieldState.OffensiveFieldDirection * (side == LOS_Side.Side_Right ? this.gapPositions[index] : -1f * this.gapPositions[index]) : 0.0f;
  }

  public float GetTechniquePosition(TechniqueName tech, LOS_Side side)
  {
    int index = (int) tech;
    if (index < 0 || index >= this.techPositions.Length)
      return 0.0f;
    return side != LOS_Side.Side_Right ? -1f * this.techPositions[index] : this.techPositions[index];
  }

  private bool CanShiftToPos(int playerIndex, Vector3 newPos)
  {
    for (int index = 0; index < 11; ++index)
    {
      if (index != playerIndex && (double) Vector3.Distance(newPos, this.adjustedPositions[index]) < (double) Field.ONE_YARD)
        return false;
    }
    return true;
  }

  private void SetPlayerAdjustPos(int playerIndex, Vector3 newPos) => this.adjustedPositions[playerIndex] = newPos;

  private void OnPlaySetupEnd()
  {
    this.playerPositions = MatchManager.instance.playManager.savedDefPlay.GetFormation().GetPositions();
    this.playerXPos = MatchManager.instance.playManager.savedDefPlay.GetFormation().GetXLocations();
    this.playerZPos = MatchManager.instance.playManager.savedDefPlay.GetFormation().GetZLocations();
    for (int index = 0; index < 11; ++index)
    {
      this.adjustedPositions[index].x = this.playerXPos[index];
      this.adjustedPositions[index].y = 0.0f;
      this.adjustedPositions[index].z = this.playerZPos[index];
    }
    this.PlayCalledComplete = true;
  }

  private void OnPlayComplete(string type)
  {
    this.PlayCalledComplete = false;
    this.checkForShift = true;
  }

  private void OnAudibleCalled() => this.checkForShift = true;
}
