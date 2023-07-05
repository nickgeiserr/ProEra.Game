// Decompiled with JetBrains decompiler
// Type: AIUtil
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public class AIUtil
{
  public static readonly float SPEEDRATING_TO_VELFACTOR = 8.046f;
  public static float MAX_YARDS_FROMSIDELINE_TO_VEER = 1.5f;
  public static float MIN_YARDS_FROM_SIDELINE_TO_AVOID_DEF = 3f;
  public static float START_VEER_DISTANCE = 6f;
  public static float MIN_TURN_TO_EZ_SPEED = 30f;
  public static float MAX_TURN_TO_EZ_SPEED = 90f;
  public static float DECIDE_PYLON_TO_STRAIGHT_DIST_PERCENT = 1.25f;

  public static bool GuessIsRunPlay(FormationPositions offFormation)
  {
    float num = 0.0f;
    int backsInFormation = offFormation.GetBacksInFormation();
    int tesInFormation = offFormation.GetTEsInFormation();
    int receiversInFormation = offFormation.GetReceiversInFormation();
    if (backsInFormation > 1)
      num += (float) backsInFormation * 0.2f;
    if (tesInFormation > 1)
      num += (float) tesInFormation * 0.25f;
    if (receiversInFormation > 4)
      num = 0.0f;
    int yards = Field.ConvertDistanceToYards(Field.FindDifference(ProEra.Game.MatchState.FirstDown.Value, ProEra.Game.MatchState.BallOn.Value));
    if (ProEra.Game.MatchState.Down.Value >= 3 && yards >= 5)
      num += 0.9f;
    int score1 = ProEra.Game.MatchState.Stats.User.Score;
    int score2 = ProEra.Game.MatchState.Stats.Comp.Score;
    if (ProEra.Game.MatchState.IsOffenseWinning && MatchManager.instance.timeManager.GetDisplayMinutes() < 2 && MatchManager.instance.timeManager.IsFourthQuarter())
      num += 0.5f;
    if (ProEra.Game.MatchState.IsDefenseWinning && MatchManager.instance.timeManager.GetDisplayMinutes() < 2 && MatchManager.instance.timeManager.IsFourthQuarter())
      num -= 0.9f;
    return (double) UnityEngine.Random.value < 0.10000000149011612 ? MatchManager.instance.playManager.savedOffPlay.GetPlayType() == PlayType.Run : (double) UnityEngine.Random.value < (double) num;
  }

  public static LOS_Side GetStrongSide()
  {
    LOS_Side strongSide = LOS_Side.Side_Left;
    FormationPositions formation = MatchManager.instance.playManager.savedOffPlay.GetFormation();
    if (formation.NumTELeft() == formation.NumTERight())
      strongSide = formation.NumHBLeft() <= formation.NumHBRight() ? (formation.NumHBRight() <= formation.NumHBLeft() ? LOS_Side.Side_Right : LOS_Side.Side_Right) : LOS_Side.Side_Left;
    if (formation.NumTELeft() > formation.NumTERight())
      strongSide = LOS_Side.Side_Left;
    else if (formation.NumTERight() > formation.NumTELeft())
      strongSide = LOS_Side.Side_Right;
    return strongSide;
  }

  public static LOS_Side GetWeakSide() => AIUtil.GetStrongSide() == LOS_Side.Side_Left ? LOS_Side.Side_Right : LOS_Side.Side_Left;

  public static LOS_Side GetPlayerSide(float offSetFromLos, float offDir)
  {
    offSetFromLos *= offDir;
    LOS_Side playerSide = LOS_Side.Side_None;
    if ((double) offSetFromLos > 0.5)
      playerSide = LOS_Side.Side_Right;
    else if ((double) offSetFromLos < -0.5)
      playerSide = LOS_Side.Side_Left;
    return playerSide;
  }

  public static float Remap(float aLow, float aHigh, float bLow, float bHigh, float valueToRemap)
  {
    float t = Mathf.InverseLerp(aLow, aHigh, valueToRemap);
    return Mathf.Lerp(bLow, bHigh, t);
  }

  public static bool IsInBackfield(float zPos) => ((double) ProEra.Game.MatchState.BallOn.Value - (double) zPos) * (double) global::Game.OffensiveFieldDirection > 4.0 * (double) Field.ONE_UNIT_PER_YARD;

  public static bool IsNickelBack(PlayDataDef playData, int formationIndex) => playData.GetFormation().GetBaseFormation() == BaseFormation.Nickel && formationIndex == 6;

  public static bool IsDimeBack(PlayDataDef playData, int formationIndex) => playData.GetFormation().GetBaseFormation() == BaseFormation.Dime && formationIndex == 5;

  public static Vector3 GetBestAngleToEZ(PlayerAI player)
  {
    if ((UnityEngine.Object) player == (UnityEngine.Object) null)
      return Vector3.zero;
    float offensiveGoalLine = Field.OFFENSIVE_GOAL_LINE;
    Vector3 position = player.trans.position;
    float distance = Field.ConvertYardsToDistance(AIUtil.START_VEER_DISTANCE);
    float max = Field.OUT_OF_BOUNDS - Field.ConvertYardsToDistance(AIUtil.MAX_YARDS_FROMSIDELINE_TO_VEER);
    Vector3 b1 = new Vector3(Mathf.Clamp(position.x, -max, max), 0.0f, offensiveGoalLine);
    float num1 = Vector3.Distance(position, b1);
    Vector3 vector3 = b1;
    float num2 = 0.0f;
    if ((double) position.x < -(double) distance && (double) position.x > -(double) max)
    {
      Vector3 b2 = new Vector3(-Field.OUT_OF_BOUNDS, 0.0f, offensiveGoalLine);
      double num3 = (double) Vector3.Distance(position, b2);
      num2 = Mathf.Abs(-Field.OUT_OF_BOUNDS - position.x);
      double num4 = (double) num1 * (double) AIUtil.DECIDE_PYLON_TO_STRAIGHT_DIST_PERCENT;
      if (num3 < num4)
        vector3 = b2;
    }
    else if ((double) position.x > (double) distance && (double) position.x < (double) max)
    {
      Vector3 b3 = new Vector3(Field.OUT_OF_BOUNDS, 0.0f, offensiveGoalLine);
      double num5 = (double) Vector3.Distance(position, b3);
      num2 = Field.OUT_OF_BOUNDS - position.x;
      double num6 = (double) num1 * (double) AIUtil.DECIDE_PYLON_TO_STRAIGHT_DIST_PERCENT;
      if (num5 < num6)
        vector3 = b3;
    }
    Vector3 normalized = (vector3 - player.trans.position).normalized;
    float num7 = Mathf.Lerp(AIUtil.MIN_TURN_TO_EZ_SPEED, AIUtil.MAX_TURN_TO_EZ_SPEED, (float) (1.0 - (double) num2 / ((double) Field.OUT_OF_BOUNDS - (double) AIUtil.START_VEER_DISTANCE)));
    return Vector3.RotateTowards(player.animatorCommunicator.velocity, normalized, (float) Math.PI / 180f * num7, 1f).normalized;
  }

  public static void DrawDebugCross(Vector3 pos, Vector3 size, Color col, float dur)
  {
    Debug.DrawLine(pos - Vector3.left * size.x, pos + Vector3.left * size.x, col, dur);
    Debug.DrawLine(pos - Vector3.forward * size.z, pos + Vector3.forward * size.z, col, dur);
  }

  public static void DrawDebugCube(Vector3 centerPos, Vector3 size, Color col, float dur)
  {
    Vector3 vector3_1 = centerPos + Vector3.left * size.x - Vector3.forward * size.z + Vector3.up * size.y;
    Vector3 vector3_2 = centerPos + Vector3.left * size.x + Vector3.forward * size.z + Vector3.up * size.y;
    Vector3 vector3_3 = centerPos + Vector3.right * size.x + Vector3.forward * size.z + Vector3.up * size.y;
    Vector3 vector3_4 = centerPos + Vector3.right * size.x - Vector3.forward * size.z + Vector3.up * size.y;
    Vector3 vector3_5 = centerPos + Vector3.left * size.x - Vector3.forward * size.z + Vector3.down * size.y;
    Vector3 vector3_6 = centerPos + Vector3.left * size.x + Vector3.forward * size.z + Vector3.down * size.y;
    Vector3 vector3_7 = centerPos + Vector3.right * size.x + Vector3.forward * size.z + Vector3.down * size.y;
    Vector3 vector3_8 = centerPos + Vector3.right * size.x - Vector3.forward * size.z + Vector3.down * size.y;
    Debug.DrawLine(vector3_1, vector3_2, col, dur);
    Debug.DrawLine(vector3_2, vector3_3, col, dur);
    Debug.DrawLine(vector3_3, vector3_4, col, dur);
    Debug.DrawLine(vector3_4, vector3_1, col, dur);
    Debug.DrawLine(vector3_5, vector3_6, col, dur);
    Debug.DrawLine(vector3_6, vector3_7, col, dur);
    Debug.DrawLine(vector3_7, vector3_8, col, dur);
    Debug.DrawLine(vector3_8, vector3_5, col, dur);
    Debug.DrawLine(vector3_1, vector3_5, col, dur);
    Debug.DrawLine(vector3_2, vector3_6, col, dur);
    Debug.DrawLine(vector3_3, vector3_7, col, dur);
    Debug.DrawLine(vector3_4, vector3_8, col, dur);
  }
}
