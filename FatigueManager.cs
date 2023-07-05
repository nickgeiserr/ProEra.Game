// Decompiled with JetBrains decompiler
// Type: FatigueManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class FatigueManager : MonoBehaviour
{
  public static FatigueManager self;
  private static Color fatigue1 = new Color(0.003921569f, 0.4862745f, 0.7294118f);
  private static Color fatigue2 = new Color(0.945098042f, 0.768627465f, 0.05882353f);
  private static Color fatigue3 = new Color(0.945098042f, 0.05882353f, 0.05882353f);
  private static int playRecoveryAmt = -7;
  private static float runPerYardAmt = 0.5f;
  private static int specialActionAmt = 3;
  private static int blockAmt = 3;
  private static int catchBallAmt = 5;
  private static int breakTackleAmt = 7;
  private static int getTackledAmt = 5;
  private static int pullingLinemanAmt = 10;
  private static int tackleAttemptAmt = 5;
  private static int tackleMadeAmt = 5;

  private void Awake()
  {
    if ((Object) FatigueManager.self == (Object) null)
      FatigueManager.self = this;
    else
      Debug.Log((object) "Another FatigueManager already exists...there should only be one");
  }

  private static void AddFatigue(PlayerData player, float amt)
  {
    if ((double) amt > 0.0)
      amt *= (float) (1.0 - (double) player.Endurance * (1.0 / 400.0));
    player.Fatigue -= Mathf.RoundToInt(amt);
    player.Fatigue = Mathf.Clamp(player.Fatigue, 0, 100);
  }

  private static void AddFatigue(bool onUserTeam, int indexOnTeam, float amt) => FatigueManager.AddFatigue((onUserTeam ? MatchManager.instance.playersManager.userTeamData : MatchManager.instance.playersManager.compTeamData).GetPlayer(indexOnTeam), amt);

  public static void RecoverAllPlayers()
  {
    for (int playerIndex = 0; playerIndex < MatchManager.instance.playersManager.userTeamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      FatigueManager.AddFatigue(MatchManager.instance.playersManager.userTeamData.GetPlayer(playerIndex), (float) FatigueManager.playRecoveryAmt);
      FatigueManager.AddFatigue(MatchManager.instance.playersManager.compTeamData.GetPlayer(playerIndex), (float) FatigueManager.playRecoveryAmt);
    }
  }

  public static void ResetAllFatigueVales()
  {
    for (int playerIndex = 0; playerIndex < MatchManager.instance.playersManager.userTeamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      FatigueManager.AddFatigue(MatchManager.instance.playersManager.userTeamData.GetPlayer(playerIndex), -100f);
      FatigueManager.AddFatigue(MatchManager.instance.playersManager.userTeamData.GetPlayer(playerIndex), -100f);
    }
  }

  public static void RunDistance(PlayerAI p)
  {
    double num = (double) Vector3.Distance(p.GetPlayStartPosition(), p.trans.position) * 2.0;
    double runPerYardAmt = (double) FatigueManager.runPerYardAmt;
  }

  public static void DoSpecialAction(PlayerAI p) => FatigueManager.AddFatigue(p.onUserTeam, p.indexOnTeam, (float) FatigueManager.specialActionAmt);

  public static void EngageBlock(PlayerAI p) => FatigueManager.AddFatigue(p.onUserTeam, p.indexOnTeam, (float) FatigueManager.blockAmt);

  public static void CatchBall(PlayerAI p) => FatigueManager.AddFatigue(p.onUserTeam, p.indexOnTeam, (float) FatigueManager.catchBallAmt);

  public static void BreakTackle(PlayerAI p) => FatigueManager.AddFatigue(p.onUserTeam, p.indexOnTeam, (float) FatigueManager.breakTackleAmt);

  public static void GetTackled(PlayerAI p) => FatigueManager.AddFatigue(p.onUserTeam, p.indexOnTeam, (float) FatigueManager.getTackledAmt);

  public static void PullLineman(PlayerAI p) => FatigueManager.AddFatigue(p.onUserTeam, p.indexOnTeam, (float) FatigueManager.pullingLinemanAmt);

  public static void AttemptingTackle(PlayerAI p) => FatigueManager.AddFatigue(p.onUserTeam, p.indexOnTeam, (float) FatigueManager.tackleAttemptAmt);

  public static void MakeTackle(PlayerAI p) => FatigueManager.AddFatigue(p.onUserTeam, p.indexOnTeam, (float) FatigueManager.tackleMadeAmt);

  public static Color GetFatigueColor(float fatigue)
  {
    if ((double) fatigue > 85.0)
      return FatigueManager.fatigue1;
    return (double) fatigue > 65.0 ? FatigueManager.fatigue2 : FatigueManager.fatigue3;
  }
}
