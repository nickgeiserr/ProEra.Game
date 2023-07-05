// Decompiled with JetBrains decompiler
// Type: ProjectConstants.PlayEditorConfiguration
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace ProjectConstants
{
  public static class PlayEditorConfiguration
  {
    public static class Measurements
    {
      public static float PlayerSprintingSpeed = 5.9f;
      public static float PlayerWalkingSpeed = 1.51f;
      public static float PlayerMaxBackpedalingSpeed = 4f;
      public static float PlayerMaxShufflingSpeed = 4f;
      public static float PlayerMaxKickstepingSpeed = 4f;
      public static float BallFreeFlightSpeed = 18.1f;
      public static float BallPassBulletFlightSpeed = 28f;
      public static float BallPassRegularFlightSpeed = 24f;
      public static float BallPassLobFlightSpeed = 18f;
      public static float BallPassPitchFlightSpeed = 20f;
      public static float BallPassShotgunFlightSpeed = 22f;
      public static float FieldWidth = 120f;
      public static float FieldHeight = 53.3f;
      public static float PlayAreaLength = 100f;
      public static float PlayAreaWidth = 53.3f;
    }

    public static class Whiteboard
    {
      public static class Routes
      {
        public static float RoutePointsDistanceThreshold = 0.4f;
        public static float PreSnapMovementFinishTime = -3f;
        public static float PostSnapMovementStartTime = 0.25f;
        public const float LinearMovementTurnThreashold = 100f;
        public const float PlayerTurnRate = 360f;
      }

      public static class HuddleGeneration
      {
        public static float ConstantHuddleTimeDuration = 5f;
        public static float DistanceFromLine = 10f;
      }
    }
  }
}
