// Decompiled with JetBrains decompiler
// Type: Field
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using TB12;
using UnityEngine;

public static class Field
{
  public static float ONE_YARD = 0.9144f;
  public static float ONE_UNIT_PER_YARD = 1.0936f;
  public static float CATCH_HALO = Field.ONE_YARD * 3f;
  public static float THROW_LENIENCY = Field.FIVE_YARDS;
  public static float FIELD_WIDTH = Field.ONE_FOOT * 160f;
  public static float FIELD_LENGTH = Field.ONE_HUNDRED_YARDS;
  public static float NORTH_GOAL_LINE = Field.MIDFIELD + Field.ONE_YARD * 50f;
  public static float SOUTH_GOAL_LINE = Field.MIDFIELD - Field.ONE_YARD * 50f;
  public static float NORTH_BACK_OF_ENDZONE = Field.NORTH_GOAL_LINE + Field.TEN_YARDS;
  public static float SOUTH_BACK_OF_ENDZONE = Field.SOUTH_GOAL_LINE - Field.TEN_YARDS;
  public static float NORTH_TWO_POINT_ATTEMPT_LINE = Field.NORTH_GOAL_LINE - Field.TWO_YARDS;
  public static float SOUTH_TWO_POINT_ATTEMPT_LINE = Field.SOUTH_GOAL_LINE + Field.TWO_YARDS;
  public static float NORTH_PAT_ATTEMPT_LINE = Field.NORTH_GOAL_LINE - Field.ONE_YARD * 15f;
  public static float SOUTH_PAT_ATTEMPT_LINE = Field.SOUTH_GOAL_LINE + Field.ONE_YARD * 15f;
  public static float FG_CROSSBAR_HEIGHT = Field.ONE_FOOT * 10f;
  public static float FG_POST_WIDTH = Field.ONE_FOOT * 18.6f;
  public static float FG_HALF_POST_WIDTH = Field.FG_POST_WIDTH * 0.5f;
  public static float FIVE_YARD_LINE = Field.SOUTH_GOAL_LINE + Field.FIVE_YARDS;
  public static float TEN_YARD_LINE = Field.SOUTH_GOAL_LINE + Field.TEN_YARDS;
  public static float TWENTY_YARD_LINE = Field.SOUTH_GOAL_LINE + Field.ONE_YARD * 20f;
  public static float TWENTY_FIVE_YARD_LINE = Field.SOUTH_GOAL_LINE + Field.ONE_YARD * 25f;
  public static float THIRTY_YARD_LINE = Field.SOUTH_GOAL_LINE + Field.ONE_YARD * 30f;
  public static float THIRTY_FIVE_YARD_LINE = Field.SOUTH_GOAL_LINE + Field.ONE_YARD * 35f;
  public static float FORTY_YARD_LINE = Field.SOUTH_GOAL_LINE + Field.ONE_YARD * 40f;
  public static float FORTY_FIVE_YARD_LINE = Field.SOUTH_GOAL_LINE + Field.ONE_YARD * 45f;
  public static float MIDFIELD = 0.0f;
  public static float DEFAULT_HASH_OFFSET = 2.8194f;
  public static float HASH_OFFSET = 0.0f;
  public static float OUT_OF_BOUNDS = Field.FIELD_WIDTH / 2f;

  public static float ONE_INCH => Field.ONE_FOOT / 12f;

  public static float THREE_INCHES => Field.ONE_FOOT / 4f;

  public static float SIX_INCHES => Field.ONE_FOOT / 2f;

  public static float NINE_INCHES => Field.ONE_FOOT * 0.75f;

  public static float FIFTEEN_INCHES => Field.THIRTY_INCHES / 2f;

  public static float ONE_HALF_YARD => Field.ONE_YARD / 2f;

  public static float TWO_FEET => Field.ONE_FOOT * 2f;

  public static float THIRTY_INCHES => Field.ONE_FOOT * 2.5f;

  public static float ONE_YARD_SIX_INCHES => Field.ONE_YARD + Field.SIX_INCHES;

  public static float ONE_YARD_FIFTEEN_INCHES => Field.ONE_YARD + Field.FIFTEEN_INCHES;

  public static float ONE_YARD_TWO_FEET => Field.ONE_YARD + Field.TWO_FEET;

  public static float ONE_YARD_THIRTY_INCHES => Field.ONE_YARD + Field.THIRTY_INCHES;

  public static float TWO_YARDS => Field.ONE_YARD * 2f;

  public static float THREE_YARDS => Field.ONE_YARD * 3f;

  public static float FOUR_YARDS => Field.ONE_YARD * 4f;

  public static float FIVE_YARDS => Field.ONE_YARD * 5f;

  public static float SIX_YARDS => Field.ONE_YARD * 6f;

  public static float SEVEN_YARDS => Field.ONE_YARD * 7f;

  public static float EIGHT_YARDS => Field.ONE_YARD * 8f;

  public static float NINE_YARDS => Field.ONE_YARD * 9f;

  public static float TEN_YARDS => Field.ONE_YARD * 10f;

  public static float ONE_HUNDRED_YARDS => Field.ONE_YARD * 100f;

  public static float ONE_FOOT => Field.ONE_YARD / 3f;

  public static float ONE_MILE => Field.ONE_FOOT * 5280f;

  public static float ONE_MILE_PER_HOUR => Field.ONE_MILE / 3600f;

  public static float POSITIVE_HASH_XPOS
  {
    get
    {
      if ((double) Field.HASH_OFFSET == 0.0)
        Field.HASH_OFFSET = PersistentData.GetHomeTeamData().GetTeamHashLocation();
      return Field.HASH_OFFSET;
    }
  }

  public static float NEGATIVE_HASH_XPOS => Field.POSITIVE_HASH_XPOS * -1f;

  public static float ONE_YARD_FORWARD => !(bool) ProEra.Game.MatchState.Turnover ? (!(bool) FieldState.OffenseGoingNorth ? -Field.ONE_YARD : Field.ONE_YARD) : (!(bool) FieldState.OffenseGoingNorth ? Field.ONE_YARD : -Field.ONE_YARD);

  public static float ONE_YARD_LEFT => !(bool) ProEra.Game.MatchState.Turnover ? (!(bool) FieldState.OffenseGoingNorth ? Field.ONE_YARD : -Field.ONE_YARD) : (!(bool) FieldState.OffenseGoingNorth ? -Field.ONE_YARD : Field.ONE_YARD);

  public static float ONE_YARD_RIGHT => !(bool) ProEra.Game.MatchState.Turnover ? (!(bool) FieldState.OffenseGoingNorth ? -Field.ONE_YARD : Field.ONE_YARD) : (!(bool) FieldState.OffenseGoingNorth ? Field.ONE_YARD : -Field.ONE_YARD);

  public static float OFFENSIVE_GOAL_LINE => !(bool) ProEra.Game.MatchState.Turnover ? (!(bool) FieldState.OffenseGoingNorth ? Field.SOUTH_GOAL_LINE : Field.NORTH_GOAL_LINE) : (!(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE : Field.SOUTH_GOAL_LINE);

  public static float DEFENSIVE_GOAL_LINE => !(bool) ProEra.Game.MatchState.Turnover ? (!(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE : Field.SOUTH_GOAL_LINE) : (!(bool) FieldState.OffenseGoingNorth ? Field.SOUTH_GOAL_LINE : Field.NORTH_GOAL_LINE);

  public static float OFFENSIVE_BACK_OF_ENDZONE => !(bool) FieldState.OffenseGoingNorth ? Field.SOUTH_BACK_OF_ENDZONE : Field.NORTH_BACK_OF_ENDZONE;

  public static float DEFENSIVE_BACK_OF_ENDZONE => !(bool) FieldState.OffenseGoingNorth ? Field.NORTH_BACK_OF_ENDZONE : Field.SOUTH_BACK_OF_ENDZONE;

  public static float DEFAULT_PAT_LOCATION => !global::Game.OffenseGoingNorth ? Field.SOUTH_PAT_ATTEMPT_LINE : Field.NORTH_PAT_ATTEMPT_LINE;

  public static float OWN_FIVE_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE - 5f * Field.ONE_YARD : Field.FIVE_YARD_LINE;

  public static float OPPONENT_FIVE_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.FIVE_YARD_LINE : Field.NORTH_GOAL_LINE - 5f * Field.ONE_YARD;

  public static float OWN_TEN_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE - 10f * Field.ONE_YARD : Field.TEN_YARD_LINE;

  public static float OPPONENT_TEN_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.TEN_YARD_LINE : Field.NORTH_GOAL_LINE - 10f * Field.ONE_YARD;

  public static float OPPONENT_TWENTY_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.TWENTY_YARD_LINE : Field.NORTH_GOAL_LINE - 20f * Field.ONE_YARD;

  public static float OWN_TWENTY_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE - 20f * Field.ONE_YARD : Field.TWENTY_YARD_LINE;

  public static float OWN_TWENTY_FIVE_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE - 25f * Field.ONE_YARD : Field.TWENTY_FIVE_YARD_LINE;

  public static float OPPONENT_TWENTY_FIVE_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.TWENTY_FIVE_YARD_LINE : Field.NORTH_GOAL_LINE - 25f * Field.ONE_YARD;

  public static float OWN_FORTY_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE - 40f * Field.ONE_YARD : Field.FORTY_YARD_LINE;

  public static float OPPONENT_FORTY_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.FORTY_YARD_LINE : Field.NORTH_GOAL_LINE - 4f * Field.ONE_YARD;

  public static float OWN_FORTY_FIVE_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE - 45f * Field.ONE_YARD : Field.FORTY_FIVE_YARD_LINE;

  public static float OPPONENT_FORTY_FIVE_YARD_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.FORTY_FIVE_YARD_LINE : Field.NORTH_GOAL_LINE - 45f * Field.ONE_YARD;

  public static float TWO_POINT_ATTEMPT_LINE => !(bool) FieldState.OffenseGoingNorth ? Field.SOUTH_TWO_POINT_ATTEMPT_LINE : Field.NORTH_TWO_POINT_ATTEMPT_LINE;

  public static float KICKOFF_LOCATION => !(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE - 35f * Field.ONE_YARD : Field.THIRTY_FIVE_YARD_LINE;

  public static float SAFETY_KICKOFF_LOCATION => !(bool) FieldState.OffenseGoingNorth ? Field.NORTH_GOAL_LINE - 25f * Field.ONE_YARD : Field.TWENTY_FIVE_YARD_LINE;

  public static float KICKOFF_OOB_LOCATION => !(bool) FieldState.OffenseGoingNorth ? Field.THIRTY_FIVE_YARD_LINE : Field.FIELD_LENGTH - Field.THIRTY_FIVE_YARD_LINE;

  public static float LEFT_OUT_OF_BOUNDS => !(bool) ProEra.Game.MatchState.Turnover ? (!(bool) FieldState.OffenseGoingNorth ? Field.OUT_OF_BOUNDS : -Field.OUT_OF_BOUNDS) : (!(bool) FieldState.OffenseGoingNorth ? -Field.OUT_OF_BOUNDS : Field.OUT_OF_BOUNDS);

  public static float RIGHT_OUT_OF_BOUNDS => !(bool) ProEra.Game.MatchState.Turnover ? (!(bool) FieldState.OffenseGoingNorth ? -Field.OUT_OF_BOUNDS : Field.OUT_OF_BOUNDS) : (!(bool) FieldState.OffenseGoingNorth ? Field.OUT_OF_BOUNDS : -Field.OUT_OF_BOUNDS);

  public static float LEFT_OOB_DIRECTION => (double) Field.LEFT_OUT_OF_BOUNDS <= 0.0 ? -1f : 1f;

  public static float RIGHT_OOB_DIRECTION => (double) Field.RIGHT_OUT_OF_BOUNDS <= 0.0 ? -1f : 1f;

  public static Quaternion DEFENSE_TOWARDS_LOS_QUATERNION => Quaternion.Euler(Field.DEFENSE_TOWARDS_LOS_VECTOR);

  public static Vector3 DEFENSE_TOWARDS_LOS_VECTOR => !(bool) FieldState.OffenseGoingNorth ? new Vector3(0.0f, 0.0f, 0.0f) : new Vector3(0.0f, 180f, 0.0f);

  public static Quaternion OFFENSE_TOWARDS_LOS_QUATERNION => Quaternion.Euler(Field.OFFENSE_TOWARDS_LOS_VECTOR);

  public static Vector3 OFFENSE_TOWARDS_LOS_VECTOR => !(bool) FieldState.OffenseGoingNorth ? new Vector3(0.0f, 180f, 0.0f) : new Vector3(0.0f, 0.0f, 0.0f);

  public static void FlipFieldDirection()
  {
    if (AppState.GameMode == EGameMode.kPracticeMode)
      return;
    FieldState.OffenseGoingNorth.Toggle();
  }

  public static Vector3 FlipVectorByFieldDirection(Vector3 v) => new Vector3(v.x * (float) FieldState.OffensiveFieldDirection, v.y, v.z * (float) FieldState.OffensiveFieldDirection);

  public static bool IsObjectOutOfBounds(Vector3 checkOOB) => Field.MoreLeftOf(checkOOB.x, Field.LEFT_OUT_OF_BOUNDS) || Field.MoreRightOf(checkOOB.x, Field.RIGHT_OUT_OF_BOUNDS);

  public static float GetFieldLocationByYardline(int yardLine, bool ownSideForOffense) => ownSideForOffense ? Field.DEFENSIVE_GOAL_LINE + (float) yardLine * Field.ONE_YARD * (float) FieldState.OffensiveFieldDirection : Field.OFFENSIVE_GOAL_LINE - (float) yardLine * Field.ONE_YARD * (float) FieldState.OffensiveFieldDirection;

  public static int GetYardLineByFieldLocation(float fieldLocation) => Mathf.RoundToInt((float) (50.0 - (double) Mathf.Abs(fieldLocation) / (double) Field.ONE_YARD));

  public static string GetYardLineStringByFieldLocation(float fieldLocation) => ((double) fieldLocation < (double) Field.MIDFIELD ? (!global::Game.OffenseGoingNorth ? PersistentData.GetDefensiveTeamData().GetAbbreviation() : PersistentData.GetOffensiveTeamData().GetAbbreviation()) : (!global::Game.OffenseGoingNorth ? PersistentData.GetOffensiveTeamData().GetAbbreviation() : PersistentData.GetDefensiveTeamData().GetAbbreviation())) + string.Format("{0}", (object) Field.GetYardLineByFieldLocation(fieldLocation));

  public static bool MoreLeftOf(float firstObjectXPos, float secondObjectXPos)
  {
    bool flag = (double) firstObjectXPos < (double) secondObjectXPos;
    return !(bool) ProEra.Game.MatchState.Turnover ? ((bool) FieldState.OffenseGoingNorth ? flag : !flag) : ((bool) FieldState.OffenseGoingNorth ? !flag : flag);
  }

  public static bool MoreRightOf(float firstObjectXPos, float secondObjectXPos)
  {
    bool flag = (double) firstObjectXPos > (double) secondObjectXPos;
    return !(bool) ProEra.Game.MatchState.Turnover ? ((bool) FieldState.OffenseGoingNorth ? flag : !flag) : ((bool) FieldState.OffenseGoingNorth ? !flag : flag);
  }

  public static bool IsOnRightSideOfField(float xPos) => !(bool) ProEra.Game.MatchState.Turnover ? ((bool) FieldState.OffenseGoingNorth ? (double) xPos > 0.0 : (double) xPos < 0.0) : ((bool) FieldState.OffenseGoingNorth ? (double) xPos < 0.0 : (double) xPos > 0.0);

  public static bool IsOnLeftSideOfField(float xPos) => !Field.IsOnRightSideOfField(xPos);

  public static bool FurtherDownfield(float firstObjectZPos, float secondObjectZPos) => !(bool) ProEra.Game.MatchState.Turnover ? ((bool) FieldState.OffenseGoingNorth ? (double) firstObjectZPos >= (double) secondObjectZPos : (double) firstObjectZPos <= (double) secondObjectZPos) : ((bool) FieldState.OffenseGoingNorth ? (double) firstObjectZPos <= (double) secondObjectZPos : (double) firstObjectZPos >= (double) secondObjectZPos);

  public static float MostDownfield(float a, float b) => !Field.FurtherDownfield(a, b) ? b : a;

  public static float LeastDownfield(float a, float b) => !Field.FurtherDownfield(a, b) ? a : b;

  public static float GetDistanceDownfield(float firstObjectZPos, float secondObjectZPos)
  {
    float distanceDownfield = firstObjectZPos - secondObjectZPos;
    if (ProEra.Game.MatchState.Turnover.Value == FieldState.OffenseGoingNorth.Value)
      distanceDownfield *= -1f;
    return distanceDownfield;
  }

  public static Vector3 AdjustToBeInbounds(Vector3 adjustThis)
  {
    float num1 = 0.5f * Field.LEFT_OOB_DIRECTION;
    float num2 = 0.5f * Field.RIGHT_OOB_DIRECTION;
    if (Field.MoreRightOf(adjustThis.x, Field.RIGHT_OUT_OF_BOUNDS))
      adjustThis.x = Field.RIGHT_OUT_OF_BOUNDS + num1;
    else if (Field.MoreLeftOf(adjustThis.x, Field.LEFT_OUT_OF_BOUNDS))
      adjustThis.x = Field.LEFT_OUT_OF_BOUNDS + num2;
    float num3 = -0.5f * (float) FieldState.OffensiveFieldDirection;
    if (Field.FurtherDownfield(adjustThis.z, Field.OFFENSIVE_BACK_OF_ENDZONE + num3))
      adjustThis.z = Field.OFFENSIVE_BACK_OF_ENDZONE + num3;
    else if (Field.FurtherDownfield(Field.DEFENSIVE_BACK_OF_ENDZONE + num3, adjustThis.z))
      adjustThis.z = Field.DEFENSIVE_BACK_OF_ENDZONE + num3;
    return adjustThis;
  }

  public static int GetFieldGoalRange(int kickPower) => (int) (Mathf.Max(33f, Mathf.Lerp(0.0f, 64f, (float) kickPower / 99f)) - 17f);

  public static bool IsBetweenGoalPosts(float xPos) => -(double) Field.FG_HALF_POST_WIDTH <= (double) xPos && (double) xPos <= (double) Field.FG_HALF_POST_WIDTH;

  public static bool InFrontOf(GameObject firstObject, GameObject secondObject) => Field.InFrontOf(firstObject.transform, secondObject.transform);

  public static bool InFrontOf(Transform firstObject, Transform secondObject) => (double) Mathf.Abs(Vector3.Dot((secondObject.position - firstObject.position).normalized, firstObject.forward)) > 0.0;

  public static float FindDifference(float firstObject, float secondObject) => (bool) FieldState.OffenseGoingNorth ? firstObject - secondObject : secondObject - firstObject;

  public static bool IsBehindLineOfScrimmage(float objectZPos) => Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, objectZPos);

  public static bool IsBeyondLineOfScrimmage(float objectZPos) => Field.FurtherDownfield(objectZPos, ProEra.Game.MatchState.BallOn.Value);

  public static int ConvertDistanceToYards(float distance) => Mathf.RoundToInt(distance / Field.ONE_YARD);

  public static float ConvertYardsToDistance(float yards) => yards * Field.ONE_YARD;
}
