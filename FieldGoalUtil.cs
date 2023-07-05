// Decompiled with JetBrains decompiler
// Type: FieldGoalUtil
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UDB;
using UnityEngine;

public static class FieldGoalUtil
{
  private static float DEBUG_LINE_DURATION = 5f;
  private static bool DRAW_DEBUG_LINES = false;

  public static Vector3 GetKickLandingPos(
    float kickPower,
    float kickAccuracy,
    FieldGoalConfig fieldGoalConfig)
  {
    float yards1 = (float) Field.ConvertDistanceToYards(Field.GetDistanceDownfield(Field.OFFENSIVE_GOAL_LINE, ProEra.Game.MatchState.BallOn.Value));
    if ((double) yards1 < (double) fieldGoalConfig.kickPowerMaxYardsFromGoalLine)
    {
      float num = MathUtils.MapRange(yards1, (float) fieldGoalConfig.kickPowerMinYardsFromGoalLine, (float) fieldGoalConfig.kickPowerMaxYardsFromGoalLine, fieldGoalConfig.kickPowerReductionAtMinDist, fieldGoalConfig.kickPowerReductionAtMaxDist);
      kickPower -= num;
    }
    float yards2 = MathUtils.MapRange(kickPower, (float) fieldGoalConfig.kickPowerForMinYards, (float) fieldGoalConfig.kickPowerForMaxYards, fieldGoalConfig.fieldGoalYardsMin, fieldGoalConfig.fieldGoalYardsMax);
    float distance = Field.ConvertYardsToDistance(yards2);
    Vector3 start = new Vector3(SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position.x, 0.0f, SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position.z);
    Vector3 end1 = new Vector3(0.0f, 0.0f, Field.OFFENSIVE_BACK_OF_ENDZONE);
    Vector3 vector3_1 = end1 - start;
    float num1 = MathUtils.MapRange(kickAccuracy, (float) fieldGoalConfig.kickAccuracyForMaxVariance, (float) fieldGoalConfig.kickAccuracyForMinVariance, fieldGoalConfig.fieldGoalMaxVarianceDegrees, fieldGoalConfig.fieldGoalMinVarianceDegrees);
    float y = Random.Range(-num1, num1);
    Vector3 vector3_2 = Quaternion.Euler(0.0f, y, 0.0f) * vector3_1;
    Vector3 kickLandingPos = (distance * Vector3.Normalize(vector3_2) + start) with
    {
      y = Field.FG_CROSSBAR_HEIGHT
    };
    Debug.Log((object) string.Format("[Field Goal]: kickPower = {0}; kickAccuracy = {1}", (object) kickPower, (object) kickAccuracy));
    Debug.Log((object) string.Format("[Field Goal]: distance = {0} yards ({1} meters)", (object) yards2, (object) distance));
    Debug.Log((object) string.Format("[Field Goal]: angleError = {0} degrees (max variance = {1} degrees)", (object) y, (object) num1));
    if (FieldGoalUtil.DRAW_DEBUG_LINES)
    {
      Debug.DrawLine(start, end1, Color.white, FieldGoalUtil.DEBUG_LINE_DURATION);
      Debug.DrawLine(start, new Vector3(kickLandingPos.x, 0.0f, kickLandingPos.z), Color.yellow, FieldGoalUtil.DEBUG_LINE_DURATION);
      Vector3 end2 = Vector3.Normalize(Quaternion.Euler(0.0f, -num1, 0.0f) * vector3_1) * distance + start;
      Vector3 end3 = Vector3.Normalize(Quaternion.Euler(0.0f, num1, 0.0f) * vector3_1) * distance + start;
      Debug.DrawLine(start, end2, Color.red, FieldGoalUtil.DEBUG_LINE_DURATION);
      Debug.DrawLine(start, end3, Color.red, FieldGoalUtil.DEBUG_LINE_DURATION);
    }
    return kickLandingPos;
  }
}
