// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Ball
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using Vars;

namespace ProEra.Game
{
  public static class Ball
  {
    public static float BALL_ON_GROUND_HEIGHT = 0.091f;
    public static float BALL_HIT_GROUND_HEIGHT = 0.364f;
    public static float BALL_ON_TEE_HEIGHT = 0.182f;
    public static float BALL_LENGTH = 0.359f;

    public static class State
    {
      public static readonly VariableEnum<EBallState> BallState = new VariableEnum<EBallState>(EBallState.OnTee);
      public static readonly VariableFloat Volume = new VariableFloat(0.0f);
      public static Vector3 BallPosition = Vector3.zero;
    }
  }
}
